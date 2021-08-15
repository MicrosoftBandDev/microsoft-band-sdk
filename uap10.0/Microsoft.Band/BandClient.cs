// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandClient
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Google.Protobuf;
using Microsoft.Band.Notifications;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band
{
  internal abstract class BandClient : 
    IBandClient,
    IDisposable,
    IBandNotificationManager,
    IBandPersonalizationManager,
    IBandSensorManager,
    IBandTileManager
  {
    internal readonly ILoggerProvider loggerProvider;
    internal readonly IApplicationPlatformProvider applicationPlatformProvider;
    protected bool disposed;
    internal object protocolLock;
    private IDeviceTransport deviceTransport;
    internal FirmwareApp runningFirmwareApp;
    private BandClient.CachedThings cachedThings;
    private const int MaxSensorSubscriptionQueueItemCount = 1000;
    private const int MaxTileEventQueueItemCount = 200;
    private readonly object streamingLock = new object();
    private Task streamingTask;
    private ManualResetEvent streamingTaskAwake;
    private AutoResetEvent streamingDataReceivedEvent;
    private CancellationTokenSource streamingTaskCancel;
    private HashSet<byte> subscribedSensorTypes = new HashSet<byte>();
    private Queue<BandSensorReadingBase> sensorEventQueue = new Queue<BandSensorReadingBase>();
    private bool eventingIsSubscribed;
    private Queue<BandTileEventBase> tileEventQueue = new Queue<BandTileEventBase>();
    private Guid? currentAppId;
    private Dictionary<Guid, bool> tileIdOwnership = new Dictionary<Guid, bool>();
    private static readonly BandSensorSampleDeserializer[] BandSensorSampleDeserializerTable = BandSensorSampleDeserializer.InitDeserializerTable();
    internal AccelerometerSensor accelerometer;
    internal GyroscopeSensor gyroscope;
    internal DistanceSensor distance;
    internal HeartRateSensor heartRate;
    internal ContactSensor contact;
    internal SkinTemperatureSensor skinTemperature;
    internal UVSensor uv;
    internal PedometerSensor pedometer;
    internal CaloriesSensor calories;
    internal GsrSensor gsr;
    internal RRIntervalSensor rrInterval;
    internal AmbientLightSensor als;
    internal BarometerSensor barometer;
    internal AltimeterSensor altimeter;
    private object tileEventLock = new object();

    protected IDeviceTransport DeviceTransport => this.deviceTransport;

    protected bool Disposed => this.disposed;

    internal BandClient(
      IDeviceTransport deviceTransport,
      ILoggerProvider loggerProvider,
      IApplicationPlatformProvider applicationPlatformProvider)
    {
      if (applicationPlatformProvider == null)
        throw new ArgumentNullException(nameof (applicationPlatformProvider));
      this.deviceTransport = deviceTransport;
      this.loggerProvider = loggerProvider;
      this.applicationPlatformProvider = applicationPlatformProvider;
      this.disposed = false;
      this.protocolLock = new object();
      this.cachedThings = new BandClient.CachedThings(this);
      if (this.deviceTransport == null)
        return;
      this.deviceTransport.Disconnected += new EventHandler<TransportDisconnectedEventArgs>(this.DeviceTransport_Disconnected);
    }

    internal FirmwareApp FirmwareApp => this.runningFirmwareApp;

    internal CargoVersions FirmwareVersions { get; private set; }

    internal BandTypeConstants BandTypeConstants => this.FirmwareVersions.ApplicationVersion.PCBId < (byte) 20 ? BandTypeConstants.Cargo : BandTypeConstants.Envoy;

    public IBandNotificationManager NotificationManager => (IBandNotificationManager) this;

    public IBandPersonalizationManager PersonalizationManager => (IBandPersonalizationManager) this;

    public IBandTileManager TileManager => (IBandTileManager) this;

    public IBandSensorManager SensorManager => (IBandSensorManager) this;

    internal void InitializeCachedProperties()
    {
      this.runningFirmwareApp = this.GetRunningFirmwareAppFromBand();
      this.FirmwareVersions = this.GetFirmwareVersionsFromBand();
    }

    public Task<string> GetFirmwareVersionAsync() => this.GetFirmwareVersionAsync(CancellationToken.None);

    public Task<string> GetFirmwareVersionAsync(CancellationToken token) => Task.FromResult<string>(string.Format("{0:0}.{1:0}.{2:0}.{3:0}", (object) this.FirmwareVersions.ApplicationVersion.VersionMajor, (object) this.FirmwareVersions.ApplicationVersion.VersionMinor, (object) this.FirmwareVersions.ApplicationVersion.BuildNumber, (object) this.FirmwareVersions.ApplicationVersion.Revision));

    public Task<string> GetHardwareVersionAsync() => this.GetHardwareVersionAsync(CancellationToken.None);

    public Task<string> GetHardwareVersionAsync(CancellationToken token) => Task.FromResult<string>(this.FirmwareVersions.ApplicationVersion.PCBId.ToString());

    protected abstract void OnDisconnected(TransportDisconnectedEventArgs args);

    private void DeviceTransport_Disconnected(object sender, TransportDisconnectedEventArgs args)
    {
      if (this.cachedThings != null)
        this.cachedThings.Clear();
      this.OnDisconnected(args);
    }

    internal FirmwareApp GetRunningFirmwareAppFromBand()
    {
      this.loggerProvider.Log(ProviderLogLevel.Verbose, "Retrieving running firmware app");
      using (CargoCommandReader cargoCommandReader = this.ProtocolBeginRead(DeviceCommands.CargoCoreModuleWhoAmI, 1, CommandStatusHandling.DoNotCheck))
      {
        int num = (int) cargoCommandReader.ReadByte();
        BandClient.CheckStatus(cargoCommandReader.CommandStatus, CommandStatusHandling.ThrowOnlySeverityError, this.loggerProvider);
        return (FirmwareApp) num;
      }
    }

    internal CargoVersions GetFirmwareVersionsFromBand()
    {
      this.CheckIfDisposed();
      if (this.deviceTransport == null)
        throw new InvalidOperationException(BandResources.OperationRequiredConnectedDevice);
      CargoVersions cargoVersions = new CargoVersions();
      int bytesToRead = CargoVersion.GetSerializedByteCount() * 3;
      try
      {
        using (CargoCommandReader cargoCommandReader = this.ProtocolBeginRead(DeviceCommands.CargoCoreModuleGetVersion, bytesToRead, CommandStatusHandling.DoNotCheck))
        {
          for (int index = 0; index < 3; ++index)
          {
            CargoVersion cargoVersion = CargoVersion.DeserializeFromBand((ICargoReader) cargoCommandReader);
            if (string.IsNullOrWhiteSpace(cargoVersion.AppName) || cargoVersion.VersionMajor == (ushort) 0)
            {
              InvalidDataException invalidDataException = new InvalidDataException(BandResources.InvalidAppAmount);
              this.loggerProvider.LogException(ProviderLogLevel.Error, (Exception) invalidDataException);
              throw invalidDataException;
            }
            string appName = cargoVersion.AppName;
            if (!(appName == "1BL"))
            {
              if (!(appName == "2UP"))
              {
                if (appName == "App")
                  cargoVersions.ApplicationVersion = cargoVersion;
                else
                  throw new InvalidDataException(string.Format("Firmware version name \"{0}\" read from the device was not recognized.", (object) cargoVersion.AppName));
              }
              else
                cargoVersions.UpdaterVersion = cargoVersion;
            }
            else
              cargoVersions.BootloaderVersion = cargoVersion;
          }
          BandClient.CheckStatus(cargoCommandReader.CommandStatus, CommandStatusHandling.ThrowOnlySeverityError, this.loggerProvider);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
      return cargoVersions;
    }

    internal void CheckFirmwareSdkBit(FirmwareSdkCheckPlatform platform, byte reserved)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnected();
      Action<ICargoWriter> writeArgBuf = (Action<ICargoWriter>) (w =>
      {
        w.WriteByte((byte) platform);
        w.WriteByte(reserved);
        w.WriteUInt16((ushort) 3);
      });
      if ((int) this.ProtocolWriteWithArgs(DeviceCommands.CargoCoreModuleSdkCheck, 4, writeArgBuf, statusHandling: CommandStatusHandling.DoNotThrow).Status != (int) DeviceStatusCodeUtils.Success)
        throw new BandException(BandResources.SdkVersionNotSupported);
    }

    internal void CheckIfDisposed()
    {
      if (this.disposed)
      {
        ObjectDisposedException disposedException = new ObjectDisposedException(nameof (BandClient));
        this.loggerProvider.LogException(ProviderLogLevel.Error, (Exception) disposedException);
        throw disposedException;
      }
    }

    internal void CheckIfDisconnected()
    {
      if (this.deviceTransport == null)
      {
        InvalidOperationException operationException = new InvalidOperationException(BandResources.OperationRequiredConnectedDevice);
        this.loggerProvider.LogException(ProviderLogLevel.Error, (Exception) operationException);
        throw operationException;
      }
    }

    internal void CheckIfDisconnectedOrUpdateMode()
    {
      this.CheckIfDisconnected();
      if (this.runningFirmwareApp != FirmwareApp.App)
      {
        BandIOException bandIoException = new BandIOException(string.Format(BandResources.DeviceInNonAppMode, (object) this.runningFirmwareApp));
        this.loggerProvider.LogException(ProviderLogLevel.Error, (Exception) bandIoException);
        throw bandIoException;
      }
    }

    internal void CheckIfNotEnvoy()
    {
      if (this.BandTypeConstants.BandType != BandType.Envoy)
        throw new InvalidOperationException("Envoy required");
    }

    public void Dispose() => this.Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.disposed)
        return;
      IDeviceTransport deviceTransport = this.deviceTransport;
      if (deviceTransport != null)
      {
        object streamingLock = this.streamingLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(streamingLock, ref lockTaken);
          this.StopStreamingSubscriptionTasks();
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(streamingLock);
        }
        deviceTransport.Dispose();
        this.deviceTransport = (IDeviceTransport) null;
        this.loggerProvider.Log(ProviderLogLevel.Info, "BandClient Transport disposed.");
      }
      this.streamingDataReceivedEvent?.Dispose();
      this.disposed = true;
    }

    public Task ShowDialogAsync(Guid tileId, string title, string body) => this.ShowDialogAsync(tileId, title, body, CancellationToken.None);

    public Task ShowDialogAsync(
      Guid tileId,
      string title,
      string body,
      CancellationToken cancel)
    {
      if (tileId == Guid.Empty)
        throw new ArgumentException(BandResources.NotificationInvalidTileId, nameof (tileId));
      if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(body))
        throw new ArgumentException(BandResources.NotificationFieldsEmpty);
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      return Task.Run((Action) (() => this.ShowDialogWithOwnerValidation(tileId, title, body, cancel)), cancel);
    }

    internal void ShowDialogWithOwnerValidation(
      Guid tileId,
      string title,
      string body,
      CancellationToken cancel)
    {
      if (!this.TileInstalledAndOwned(tileId, cancel))
        return;
      this.ShowDialogHelper(tileId, title ?? string.Empty, body ?? string.Empty, cancel);
    }

    public Task SendMessageAsync(
      Guid tileId,
      string title,
      string body,
      DateTimeOffset timestamp,
      MessageFlags flags = MessageFlags.None)
    {
      return this.SendMessageAsync(tileId, title, body, timestamp, flags, CancellationToken.None);
    }

    public Task SendMessageAsync(
      Guid tileId,
      string title,
      string body,
      DateTimeOffset timestamp,
      MessageFlags flags,
      CancellationToken cancel)
    {
      if (tileId == Guid.Empty)
        throw new ArgumentException(BandResources.NotificationInvalidTileId, nameof (tileId));
      if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(body))
        throw new ArgumentException(BandResources.NotificationFieldsEmpty);
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      return Task.Run((Action) (() => this.SendMessageWithOwnerValidation(tileId, title, body, timestamp, flags, cancel)), cancel);
    }

    internal void SendMessageWithOwnerValidation(
      Guid tileId,
      string title,
      string body,
      DateTimeOffset timestamp,
      MessageFlags flags,
      CancellationToken cancel)
    {
      if (!this.TileInstalledAndOwned(tileId, cancel))
        return;
      this.SendMessage(tileId, title ?? string.Empty, body ?? string.Empty, timestamp, flags, cancel);
    }

    public Task VibrateAsync(VibrationType vibrationType) => this.VibrateAsync(vibrationType, CancellationToken.None);

    public Task VibrateAsync(VibrationType vibrationType, CancellationToken cancel)
    {
      BandVibrationType bandVibrationType = vibrationType.ToBandVibrationType();
      return Task.Run((Action) (() => this.VibrateHelper(bandVibrationType, cancel)), cancel);
    }

    internal void SendMessage(
      Guid tileId,
      string title,
      string body,
      DateTimeOffset timestamp,
      MessageFlags flags,
      CancellationToken token)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      token.ThrowIfCancellationRequested();
      NotificationMessaging notificationMessaging = new NotificationMessaging(tileId)
      {
        Timestamp = timestamp,
        Title = title,
        Body = body
      };
      if (!flags.HasFlag((Enum) MessageFlags.ShowDialog))
        notificationMessaging.Flags = (byte) 2;
      token.ThrowIfCancellationRequested();
      this.SendNotification(NotificationID.Messaging, NotificationPBMessageType.Messaging, (NotificationBase) notificationMessaging);
    }

    protected void ShowDialogHelper(
      Guid tileId,
      string title,
      string body,
      CancellationToken token,
      BandNotificationFlags flagbits = BandNotificationFlags.UnmodifiedNotificationSettings)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      token.ThrowIfCancellationRequested();
      NotificationGenericDialog notificationGenericDialog = new NotificationGenericDialog(tileId)
      {
        Title = title,
        Body = body
      };
      if (flagbits.HasFlag((Enum) BandNotificationFlags.ForceNotificationDialog))
        notificationGenericDialog.Flags |= (byte) 1;
      else
        notificationGenericDialog.Flags &= (byte) 254;
      token.ThrowIfCancellationRequested();
      this.SendNotification(NotificationID.GenericDialog, NotificationPBMessageType.GenericDialog, (NotificationBase) notificationGenericDialog);
    }

    protected void SendNotification(
      NotificationID notificationId,
      NotificationPBMessageType notificationPbType,
      NotificationBase notification)
    {
      int argBufSize = 0;
      Action<ICargoWriter> writeArgBuf = (Action<ICargoWriter>) null;
      ushort commandId;
      int byteCount;
      switch (this.BandTypeConstants.BandType)
      {
        case BandType.Cargo:
          commandId = DeviceCommands.CargoNotification;
          byteCount = 2 + notification.GetSerializedByteCount();
          break;
        case BandType.Envoy:
          commandId = DeviceCommands.CargoNotificationProtoBuf;
          byteCount = notification.GetSerializedProtobufByteCount();
          argBufSize = 4;
          writeArgBuf = (Action<ICargoWriter>) (w =>
          {
            w.WriteUInt16((ushort) byteCount);
            w.WriteUInt16((ushort) notificationPbType);
          });
          break;
        default:
          throw new InvalidOperationException();
      }
      using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(commandId, argBufSize, byteCount, writeArgBuf, CommandStatusHandling.DoNotCheck))
      {
        switch (this.BandTypeConstants.BandType)
        {
          case BandType.Cargo:
            cargoCommandWriter.WriteUInt16((ushort) notificationId);
            notification.SerializeToBand((ICargoWriter) cargoCommandWriter);
            break;
          case BandType.Envoy:
            CodedOutputStream output = new CodedOutputStream((Stream) cargoCommandWriter, byteCount);
            notification.SerializeProtobufToBand(output);
            output.Flush();
            break;
        }
        BandClient.CheckStatus(cargoCommandWriter.CommandStatus, CommandStatusHandling.ThrowOnlySeverityError, this.loggerProvider);
      }
    }

    protected void VibrateHelper(BandVibrationType bandVibrationType, CancellationToken cancel)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      cancel.ThrowIfCancellationRequested();
      Action<ICargoWriter> writeData = (Action<ICargoWriter>) (w => w.WriteByte((byte) bandVibrationType));
      this.ProtocolWriteWithData(DeviceCommands.CargoHapticPlayVibrationStream, 1, writeData);
    }

    public Task<BandImage> GetMeTileImageAsync() => this.GetMeTileImageAsync(CancellationToken.None);

    public Task<BandImage> GetMeTileImageAsync(CancellationToken cancel) => Task.Run<BandImage>((Func<BandImage>) (() => this.GetMeTileImageInternal(cancel)), cancel);

    public Task SetMeTileImageAsync(BandImage image) => this.SetMeTileImageAsync(image, CancellationToken.None);

    public Task SetMeTileImageAsync(BandImage image, CancellationToken cancel) => image != null ? Task.Run((Action) (() => this.SetMeTileImageInternal(image, uint.MaxValue, cancel)), cancel) : Task.Run((Action) (() => this.ClearMeTileImageInternal(cancel)), cancel);

    public Task<BandTheme> GetThemeAsync() => this.GetThemeAsync(CancellationToken.None);

    public Task<BandTheme> GetThemeAsync(CancellationToken cancel) => Task.Run<BandTheme>((Func<BandTheme>) (() => this.GetThemeInternal(cancel)), cancel);

    public Task SetThemeAsync(BandTheme theme) => this.SetThemeAsync(theme, CancellationToken.None);

    public Task SetThemeAsync(BandTheme theme, CancellationToken cancel) => theme == null ? Task.Run((Action) (() => this.ResetThemeInternal(cancel)), cancel) : Task.Run((Action) (() => this.SetThemeInternal(theme, cancel)), cancel);

    protected BandImage GetMeTileImageInternal(CancellationToken cancel)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      cancel.ThrowIfCancellationRequested();
      uint imageId = 0;
      Action<ICargoReader> readData1 = (Action<ICargoReader>) (r => imageId = r.ReadUInt32());
      this.ProtocolRead(DeviceCommands.CargoSystemSettingsGetMeTileImageID, 4, readData1);
      if (imageId == 0U)
        return (BandImage) null;
      cancel.ThrowIfCancellationRequested();
      int byteCount = (int) this.BandTypeConstants.MeTileWidth * (int) this.BandTypeConstants.MeTileHeight * 2;
      byte[] pixelData = (byte[]) null;
      Action<ICargoReader> readData2 = (Action<ICargoReader>) (r => pixelData = r.ReadExact(byteCount));
      this.ProtocolRead(DeviceCommands.CargoFireballUIReadMeTileImage, byteCount, readData2, 60000);
      return new BandImage((int) this.BandTypeConstants.MeTileWidth, (int) this.BandTypeConstants.MeTileHeight, pixelData);
    }

    protected void SetMeTileImageInternal(BandImage image, uint imageId, CancellationToken cancel)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      this.ValidateMeTileImage(image, imageId);
      cancel.ThrowIfCancellationRequested();
      this.RunUsingSynchronizedFirmwareUI((Action) (() => this.SetMeTileImageInternal(image, imageId)));
    }

    protected void ClearMeTileImageInternal(CancellationToken cancel)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      cancel.ThrowIfCancellationRequested();
      this.ProtocolWrite(DeviceCommands.CargoFireballUIClearMeTileImage, 60000);
    }

    protected void SetMeTileImageInternal(BandImage image, uint imageId)
    {
      Action<ICargoWriter> writeArgBuf = (Action<ICargoWriter>) (w => w.WriteUInt32(imageId));
      Action<ICargoWriter> writeData = (Action<ICargoWriter>) (w => w.Write(image.PixelData));
      this.ProtocolWrite(DeviceCommands.CargoFireballUIWriteMeTileImageWithID, 4, image.PixelData.Length, writeArgBuf, writeData, 60000);
    }

    protected BandTheme GetThemeInternal(CancellationToken cancel)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      cancel.ThrowIfCancellationRequested();
      using (CargoCommandReader cargoCommandReader = this.ProtocolBeginRead(DeviceCommands.CargoThemeColorGetFirstPartyTheme, 24, CommandStatusHandling.ThrowOnlySeverityError))
        return new BandTheme()
        {
          Base = new BandColor(cargoCommandReader.ReadUInt32()),
          Highlight = new BandColor(cargoCommandReader.ReadUInt32()),
          Lowlight = new BandColor(cargoCommandReader.ReadUInt32()),
          SecondaryText = new BandColor(cargoCommandReader.ReadUInt32()),
          HighContrast = new BandColor(cargoCommandReader.ReadUInt32()),
          Muted = new BandColor(cargoCommandReader.ReadUInt32())
        };
    }

    protected void ResetThemeInternal(CancellationToken cancel)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      cancel.ThrowIfCancellationRequested();
      this.RunUsingSynchronizedFirmwareUI((Action) (() => this.ProtocolWrite(DeviceCommands.CargoThemeColorReset)));
    }

    protected void SetThemeInternal(BandTheme theme, CancellationToken cancel)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      cancel.ThrowIfCancellationRequested();
      this.RunUsingSynchronizedFirmwareUI((Action) (() => this.SetThemeInternal(theme)));
    }

    protected void SetThemeInternal(BandTheme theme)
    {
      using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoThemeColorSetFirstPartyTheme, 24, CommandStatusHandling.ThrowOnlySeverityError))
      {
        cargoCommandWriter.WriteUInt32(theme.Base.ToRgb());
        cargoCommandWriter.WriteUInt32(theme.Highlight.ToRgb());
        cargoCommandWriter.WriteUInt32(theme.Lowlight.ToRgb());
        cargoCommandWriter.WriteUInt32(theme.SecondaryText.ToRgb());
        cargoCommandWriter.WriteUInt32(theme.HighContrast.ToRgb());
        cargoCommandWriter.WriteUInt32(theme.Muted.ToRgb());
      }
    }

    protected void ValidateMeTileImage(BandImage image, uint imageId = 4294967295)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (image.Width != (int) this.BandTypeConstants.MeTileWidth)
        throw new ArgumentException(string.Format(BandResources.MeTileImageWidthError, (object) this.BandTypeConstants.MeTileWidth));
      switch (this.BandTypeConstants.BandType)
      {
        case BandType.Cargo:
          if (image.Height != (int) BandTypeConstants.Cargo.MeTileHeight)
            throw new ArgumentException(string.Format(BandResources.MeTileHeightHeightError, (object) BandTypeConstants.Cargo.MeTileHeight));
          break;
        case BandType.Envoy:
          if (image.Height != (int) BandTypeConstants.Cargo.MeTileHeight && image.Height != (int) BandTypeConstants.Envoy.MeTileHeight)
            throw new ArgumentException(string.Format(BandResources.MeTileHeightHeightError, (object) BandTypeConstants.Envoy.MeTileHeight));
          break;
        default:
          throw new InvalidOperationException("Internal error: BandClass unrecognized");
      }
      if (imageId == 0U)
        throw new ArgumentOutOfRangeException(nameof (imageId));
    }

    internal CargoStatus ProtocolRead(
      ushort commandId,
      int dataSize,
      Action<ICargoReader> readData,
      int timeout = 5000,
      CommandStatusHandling statusHandling = CommandStatusHandling.ThrowOnlySeverityError)
    {
      return this.ProtocolRead(commandId, 0, dataSize, (Action<ICargoWriter>) null, readData, timeout, statusHandling);
    }

    internal CargoStatus ProtocolRead(
      ushort commandId,
      int argBufSize,
      int dataSize,
      Action<ICargoWriter> writeArgBuf,
      Action<ICargoReader> readData,
      int timeout = 5000,
      CommandStatusHandling statusHandling = CommandStatusHandling.ThrowOnlySeverityError)
    {
      if (argBufSize < 0)
        throw new ArgumentOutOfRangeException(nameof (argBufSize));
      if (dataSize < 0)
        throw new ArgumentOutOfRangeException(nameof (dataSize));
      if (argBufSize > 0 && writeArgBuf == null)
        throw new ArgumentNullException(nameof (writeArgBuf));
      if (dataSize > 0 && readData == null)
        throw new ArgumentNullException(nameof (readData));
      try
      {
        object protocolLock = this.protocolLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(protocolLock, ref lockTaken);
          this.deviceTransport.CargoStream.WriteTimeout = 5000;
          this.deviceTransport.WriteCommandPacket(commandId, (uint) argBufSize, (uint) dataSize, writeArgBuf, true);
          if (dataSize > 0)
          {
            this.deviceTransport.CargoStream.ReadTimeout = timeout;
            readData((ICargoReader) this.deviceTransport.CargoReader);
          }
          this.deviceTransport.CargoStream.ReadTimeout = 5000;
          CargoStatus status = this.deviceTransport.CargoReader.ReadStatusPacket();
          BandClient.CheckStatus(status, statusHandling, this.loggerProvider);
          return status;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(protocolLock);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    internal CargoCommandReader ProtocolBeginRead(
      ushort commandId,
      int bytesToRead,
      CommandStatusHandling statusHandling)
    {
      return this.ProtocolBeginRead(commandId, 0, bytesToRead, (Action<ICargoWriter>) null, statusHandling);
    }

    internal CargoCommandReader ProtocolBeginRead(
      ushort commandId,
      int argBufSize,
      int bytesToRead,
      Action<ICargoWriter> writeArgBuf,
      CommandStatusHandling statusHandling)
    {
      try
      {
        object protocolLock = this.protocolLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(protocolLock, ref lockTaken);
          this.deviceTransport.CargoStream.ReadTimeout = 5000;
          this.deviceTransport.CargoStream.WriteTimeout = 5000;
          this.deviceTransport.WriteCommandPacket(commandId, (uint) argBufSize, (uint) bytesToRead, writeArgBuf, true);
          return new CargoCommandReader(this.deviceTransport, bytesToRead, this.protocolLock, this.loggerProvider, statusHandling);
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(protocolLock);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    internal CargoStatus ProtocolWrite(
      ushort commandId,
      int timeout = 5000,
      bool swallowStatusReadException = false,
      CommandStatusHandling statusHandling = CommandStatusHandling.ThrowOnlySeverityError)
    {
      try
      {
        object protocolLock = this.protocolLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(protocolLock, ref lockTaken);
          this.deviceTransport.CargoStream.WriteTimeout = 5000;
          this.deviceTransport.WriteCommandPacket(commandId, 0U, 0U, (Action<ICargoWriter>) null, true);
          CargoStatus status;
          try
          {
            this.deviceTransport.CargoStream.ReadTimeout = timeout;
            status = this.deviceTransport.CargoReader.ReadStatusPacket();
          }
          catch
          {
            if (swallowStatusReadException)
              return new CargoStatus();
            throw;
          }
          BandClient.CheckStatus(status, statusHandling, this.loggerProvider);
          return status;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(protocolLock);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    internal CargoStatus ProtocolWriteWithArgs(
      ushort commandId,
      int argBufSize,
      Action<ICargoWriter> writeArgBuf,
      int timeout = 5000,
      CommandStatusHandling statusHandling = CommandStatusHandling.ThrowOnlySeverityError)
    {
      if (argBufSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (argBufSize));
      if (writeArgBuf == null)
        throw new ArgumentNullException(nameof (writeArgBuf));
      try
      {
        object protocolLock = this.protocolLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(protocolLock, ref lockTaken);
          this.deviceTransport.CargoStream.WriteTimeout = 5000;
          this.deviceTransport.WriteCommandPacket(commandId, (uint) argBufSize, 0U, writeArgBuf, true);
          this.deviceTransport.CargoStream.ReadTimeout = timeout;
          CargoStatus status = this.deviceTransport.CargoReader.ReadStatusPacket();
          BandClient.CheckStatus(status, statusHandling, this.loggerProvider);
          return status;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(protocolLock);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    internal CargoStatus ProtocolWriteWithData(
      ushort commandId,
      int dataSize,
      Action<ICargoWriter> writeData,
      int timeout = 5000,
      CommandStatusHandling statusHandling = CommandStatusHandling.ThrowOnlySeverityError)
    {
      if (dataSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (dataSize));
      if (writeData == null)
        throw new ArgumentNullException(nameof (writeData));
      try
      {
        object protocolLock = this.protocolLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(protocolLock, ref lockTaken);
          this.deviceTransport.CargoStream.WriteTimeout = 5000;
          this.deviceTransport.WriteCommandPacket(commandId, 0U, (uint) dataSize, (Action<ICargoWriter>) null, false);
          writeData((ICargoWriter) this.deviceTransport.CargoWriter);
          this.deviceTransport.CargoWriter.Flush();
          this.deviceTransport.CargoStream.ReadTimeout = timeout;
          CargoStatus status = this.deviceTransport.CargoReader.ReadStatusPacket();
          BandClient.CheckStatus(status, statusHandling, this.loggerProvider);
          return status;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(protocolLock);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    internal CargoStatus ProtocolWrite(
      ushort commandId,
      int argBufSize,
      int dataSize,
      Action<ICargoWriter> writeArgBuf,
      Action<ICargoWriter> writeData,
      int timeout = 5000,
      CommandStatusHandling statusHandling = CommandStatusHandling.ThrowOnlySeverityError)
    {
      if (argBufSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (argBufSize));
      if (writeArgBuf == null)
        throw new ArgumentNullException(nameof (writeArgBuf));
      if (dataSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (dataSize));
      if (writeData == null)
        throw new ArgumentNullException(nameof (writeData));
      try
      {
        object protocolLock = this.protocolLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(protocolLock, ref lockTaken);
          this.deviceTransport.CargoStream.WriteTimeout = 5000;
          this.deviceTransport.WriteCommandPacket(commandId, (uint) argBufSize, (uint) dataSize, writeArgBuf, false);
          writeData((ICargoWriter) this.deviceTransport.CargoWriter);
          this.deviceTransport.CargoWriter.Flush();
          this.deviceTransport.CargoStream.ReadTimeout = timeout;
          CargoStatus status = this.deviceTransport.CargoReader.ReadStatusPacket();
          BandClient.CheckStatus(status, statusHandling, this.loggerProvider);
          return status;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(protocolLock);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    internal CargoCommandWriter ProtocolBeginWrite(
      ushort commandId,
      int dataSize,
      CommandStatusHandling statusHandling)
    {
      return this.ProtocolBeginWrite(commandId, 0, dataSize, (Action<ICargoWriter>) null, statusHandling);
    }

    internal CargoCommandWriter ProtocolBeginWrite(
      ushort commandId,
      int argBufSize,
      int dataSize,
      Action<ICargoWriter> writeArgBuf,
      CommandStatusHandling statusHandling)
    {
      if (dataSize == 0)
      {
        ArgumentException argumentException = new ArgumentException("dataSize may not be zero");
        this.loggerProvider.LogException(ProviderLogLevel.Error, (Exception) argumentException);
        throw argumentException;
      }
      try
      {
        object protocolLock = this.protocolLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(protocolLock, ref lockTaken);
          this.deviceTransport.CargoStream.ReadTimeout = 5000;
          this.deviceTransport.CargoStream.WriteTimeout = 5000;
          this.deviceTransport.WriteCommandPacket(commandId, (uint) argBufSize, (uint) dataSize, writeArgBuf, false);
          return new CargoCommandWriter(this.deviceTransport, dataSize, this.protocolLock, this.loggerProvider, statusHandling);
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(protocolLock);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    internal static void CheckStatus(
      CargoStatus status,
      CommandStatusHandling statusHandling,
      ILoggerProvider loggerProvider)
    {
      if (statusHandling == CommandStatusHandling.DoNotCheck || (int) status.Status == (int) DeviceStatusCodeUtils.Success)
        return;
      string message = string.Format(BandResources.CommandStatusError, (object) status.Status);
      switch (statusHandling)
      {
        case CommandStatusHandling.ThrowAnyNonZero:
          loggerProvider.Log(ProviderLogLevel.Error, message);
          throw new BandOperationException(status.Status, message);
        case CommandStatusHandling.ThrowOnlySeverityError:
          if (DeviceStatusCodeUtils.IsSeverityError(status.Status))
          {
            loggerProvider.Log(ProviderLogLevel.Error, message);
            throw new BandOperationException(status.Status, message);
          }
          break;
      }
      loggerProvider.Log(ProviderLogLevel.Verbose, message);
    }

    protected Task StreamingTask
    {
      get => this.streamingTask;
      set => this.streamingTask = value;
    }

    protected CancellationTokenSource StreamingTaskCancel
    {
      get => this.streamingTaskCancel;
      set => this.streamingTaskCancel = value;
    }

    protected object StreamingLock => this.streamingLock;

    protected ManualResetEvent StreamingTaskAwake => this.streamingTaskAwake;

    protected HashSet<byte> SubscribedSensorTypes => this.subscribedSensorTypes;

    protected bool EventingIsSubscribed => this.eventingIsSubscribed;

    public IBandSensor<IBandAccelerometerReading> Accelerometer
    {
      get
      {
        if (this.accelerometer == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.accelerometer == null)
              this.accelerometer = new AccelerometerSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandAccelerometerReading>) this.accelerometer;
      }
    }

    public IBandSensor<IBandGyroscopeReading> Gyroscope
    {
      get
      {
        if (this.gyroscope == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.gyroscope == null)
              this.gyroscope = new GyroscopeSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandGyroscopeReading>) this.gyroscope;
      }
    }

    public IBandSensor<IBandDistanceReading> Distance
    {
      get
      {
        if (this.distance == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.distance == null)
              this.distance = new DistanceSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandDistanceReading>) this.distance;
      }
    }

    public IBandSensor<IBandHeartRateReading> HeartRate
    {
      get
      {
        if (this.heartRate == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.heartRate == null)
              this.heartRate = new HeartRateSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandHeartRateReading>) this.heartRate;
      }
    }

    public IBandContactSensor Contact
    {
      get
      {
        if (this.contact == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.contact == null)
              this.contact = new ContactSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandContactSensor) this.contact;
      }
    }

    public IBandSensor<IBandSkinTemperatureReading> SkinTemperature
    {
      get
      {
        if (this.skinTemperature == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.skinTemperature == null)
              this.skinTemperature = new SkinTemperatureSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandSkinTemperatureReading>) this.skinTemperature;
      }
    }

    public IBandSensor<IBandUVReading> UV
    {
      get
      {
        if (this.uv == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.uv == null)
              this.uv = new UVSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandUVReading>) this.uv;
      }
    }

    public IBandSensor<IBandPedometerReading> Pedometer
    {
      get
      {
        if (this.pedometer == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.pedometer == null)
              this.pedometer = new PedometerSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandPedometerReading>) this.pedometer;
      }
    }

    public IBandSensor<IBandCaloriesReading> Calories
    {
      get
      {
        if (this.calories == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.calories == null)
              this.calories = new CaloriesSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandCaloriesReading>) this.calories;
      }
    }

    public IBandSensor<IBandGsrReading> Gsr
    {
      get
      {
        if (this.gsr == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.gsr == null)
              this.gsr = new GsrSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandGsrReading>) this.gsr;
      }
    }

    public IBandSensor<IBandRRIntervalReading> RRInterval
    {
      get
      {
        if (this.rrInterval == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.rrInterval == null)
              this.rrInterval = new RRIntervalSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandRRIntervalReading>) this.rrInterval;
      }
    }

    public IBandSensor<IBandAmbientLightReading> AmbientLight
    {
      get
      {
        if (this.als == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.als == null)
              this.als = new AmbientLightSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandAmbientLightReading>) this.als;
      }
    }

    public IBandSensor<IBandBarometerReading> Barometer
    {
      get
      {
        if (this.barometer == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.barometer == null)
              this.barometer = new BarometerSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandBarometerReading>) this.barometer;
      }
    }

    public IBandSensor<IBandAltimeterReading> Altimeter
    {
      get
      {
        if (this.altimeter == null)
        {
          object streamingLock = this.streamingLock;
          bool lockTaken = false;
          try
          {
            Monitor.Enter(streamingLock, ref lockTaken);
            if (this.altimeter == null)
              this.altimeter = new AltimeterSensor(this);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(streamingLock);
          }
        }
        return (IBandSensor<IBandAltimeterReading>) this.altimeter;
      }
    }

    internal bool IsSensorSubscribed(SubscriptionType type) => this.SubscribedSensorTypes.Contains((byte) type);

    internal void EventingSubscribe()
    {
      this.CheckIfDisposed();
      object streamingLock = this.streamingLock;
      bool lockTaken = false;
      try
      {
        Monitor.Enter(streamingLock, ref lockTaken);
        this.StartOrAwakeStreamingSubscriptionTasks();
        this.eventingIsSubscribed = true;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(streamingLock);
      }
    }

    internal void SensorSubscribe(SubscriptionType type)
    {
      this.CheckIfDisposed();
      object streamingLock = this.streamingLock;
      bool lockTaken1 = false;
      try
      {
        Monitor.Enter(streamingLock, ref lockTaken1);
        this.StartOrAwakeStreamingSubscriptionTasks();
        if (this.IsSensorSubscribed(type))
          return;
        this.ExecuteSensorSubscribeCommand(type);
        HashSet<byte> subscribedSensorTypes = this.SubscribedSensorTypes;
        bool lockTaken2 = false;
        try
        {
          Monitor.Enter((object) subscribedSensorTypes, ref lockTaken2);
          this.SubscribedSensorTypes.Add((byte) type);
        }
        finally
        {
          if (lockTaken2)
            Monitor.Exit((object) subscribedSensorTypes);
        }
      }
      finally
      {
        if (lockTaken1)
          Monitor.Exit(streamingLock);
      }
    }

    internal void EventingUnsubscribe()
    {
      this.CheckIfDisposed();
      object streamingLock = this.streamingLock;
      bool lockTaken = false;
      try
      {
        Monitor.Enter(streamingLock, ref lockTaken);
        if (!this.EventingIsSubscribed)
          return;
        this.eventingIsSubscribed = false;
        if (this.SubscribedSensorTypes.Count != 0)
          return;
        this.StopStreamingSubscriptionTasks();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(streamingLock);
      }
    }

    internal void SensorUnsubscribe(SubscriptionType type)
    {
      this.CheckIfDisposed();
      object streamingLock = this.streamingLock;
      bool lockTaken1 = false;
      try
      {
        Monitor.Enter(streamingLock, ref lockTaken1);
        if (!this.IsSensorSubscribed(type))
          return;
        this.ExecuteSensorUnsubscribeCommand(type);
        bool flag = false;
        HashSet<byte> subscribedSensorTypes = this.SubscribedSensorTypes;
        bool lockTaken2 = false;
        try
        {
          Monitor.Enter((object) subscribedSensorTypes, ref lockTaken2);
          this.SubscribedSensorTypes.Remove((byte) type);
          flag = this.SubscribedSensorTypes.Count == 0 && !this.EventingIsSubscribed;
        }
        finally
        {
          if (lockTaken2)
            Monitor.Exit((object) subscribedSensorTypes);
        }
        if (!flag)
          return;
        this.StopStreamingSubscriptionTasks();
      }
      finally
      {
        if (lockTaken1)
          Monitor.Exit(streamingLock);
      }
    }

    protected virtual void ExecuteSensorSubscribeCommand(SubscriptionType type)
    {
      Action<ICargoWriter> writeArgBuf = (Action<ICargoWriter>) (w =>
      {
        w.WriteByte((byte) type);
        w.WriteBool32(false);
      });
      this.loggerProvider.Log(ProviderLogLevel.Info, "Remote subscribing to {0} sensor.", (object) type.ToString());
      this.ProtocolWriteWithArgs(DeviceCommands.CargoRemoteSubscriptionSubscribe, 5, writeArgBuf);
    }

    protected virtual void ExecuteSensorUnsubscribeCommand(SubscriptionType type)
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Remote unsubscribing to {0} sensor.", (object) type.ToString());
      Action<ICargoWriter> writeArgBuf = (Action<ICargoWriter>) (w => w.WriteByte((byte) type));
      this.ProtocolWriteWithArgs(DeviceCommands.CargoRemoteSubscriptionUnsubscribe, 1, writeArgBuf);
    }

    protected virtual void StartOrAwakeStreamingSubscriptionTasks()
    {
      if (!this.currentAppId.HasValue)
        this.currentAppId = new Guid?(this.applicationPlatformProvider.GetApplicationIdAsync(CancellationToken.None).Result);
      if (this.streamingTask == null)
      {
        this.streamingDataReceivedEvent = new AutoResetEvent(false);
        this.streamingTaskAwake = new ManualResetEvent(false);
        this.streamingTaskCancel = new CancellationTokenSource();
        this.loggerProvider.Log(ProviderLogLevel.Info, "Starting the streaming tasks...");
        Task.Run((Action) (() => this.FireSubscribedEvents(this.streamingDataReceivedEvent, this.streamingTaskCancel.Token)));
        using (ManualResetEvent started = new ManualResetEvent(false))
        {
          this.streamingTask = Task.Run((Action) (() => this.StreamBandData(started, this.streamingTaskCancel.Token)));
          started.WaitOne();
        }
      }
      else
        this.streamingTaskAwake.Set();
    }

    protected virtual void StopStreamingSubscriptionTasks()
    {
      if (this.streamingTask == null)
        return;
      this.loggerProvider.Log(ProviderLogLevel.Info, "Signaling the streaming tasks to stop...");
      this.streamingTaskCancel.Cancel();
      this.streamingTask.Wait();
      this.streamingTaskCancel.Dispose();
      this.streamingTaskCancel = (CancellationTokenSource) null;
      this.streamingTask = (Task) null;
      this.streamingTaskAwake.Dispose();
      this.streamingTaskAwake = (ManualResetEvent) null;
      this.streamingDataReceivedEvent = (AutoResetEvent) null;
      Queue<BandSensorReadingBase> sensorEventQueue = this.sensorEventQueue;
      bool lockTaken1 = false;
      try
      {
        Monitor.Enter((object) sensorEventQueue, ref lockTaken1);
        this.sensorEventQueue.Clear();
      }
      finally
      {
        if (lockTaken1)
          Monitor.Exit((object) sensorEventQueue);
      }
      Queue<BandTileEventBase> tileEventQueue = this.tileEventQueue;
      bool lockTaken2 = false;
      try
      {
        Monitor.Enter((object) tileEventQueue, ref lockTaken2);
        this.tileEventQueue.Clear();
      }
      finally
      {
        if (lockTaken2)
          Monitor.Exit((object) tileEventQueue);
      }
      this.loggerProvider.Log(ProviderLogLevel.Info, "Streaming task has stopped");
    }

    private void FireSubscribedEvents(AutoResetEvent awake, CancellationToken stop)
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Starting task that fires events for streaming data...");
      WaitHandle[] waitHandles = new WaitHandle[2]
      {
        (WaitHandle) awake,
        stop.WaitHandle
      };
      while (!stop.IsCancellationRequested && WaitHandle.WaitAny(waitHandles) == 0)
      {
        int num1 = 0;
        int num2 = 0;
        do
        {
          BandSensorReadingBase sensorReadingBase = (BandSensorReadingBase) null;
          BandTileEventBase bandTileEventBase = (BandTileEventBase) null;
          Queue<BandSensorReadingBase> sensorEventQueue = this.sensorEventQueue;
          bool lockTaken1 = false;
          try
          {
            Monitor.Enter((object) sensorEventQueue, ref lockTaken1);
            if ((num1 = this.sensorEventQueue.Count) > 0)
              sensorReadingBase = this.sensorEventQueue.Dequeue();
          }
          finally
          {
            if (lockTaken1)
              Monitor.Exit((object) sensorEventQueue);
          }
          if (!stop.IsCancellationRequested)
          {
            if (num1 > 0)
              sensorReadingBase.Dispatch(this);
            Queue<BandTileEventBase> tileEventQueue = this.tileEventQueue;
            bool lockTaken2 = false;
            try
            {
              Monitor.Enter((object) tileEventQueue, ref lockTaken2);
              if ((num2 = this.tileEventQueue.Count) > 0)
                bandTileEventBase = this.tileEventQueue.Dequeue();
            }
            finally
            {
              if (lockTaken2)
                Monitor.Exit((object) tileEventQueue);
            }
            if (!stop.IsCancellationRequested)
            {
              if (num2 > 0)
                bandTileEventBase.Dispatch(this);
            }
            else
              break;
          }
          else
            break;
        }
        while (!stop.IsCancellationRequested && (num1 > 0 || num2 > 0));
      }
      awake.Dispose();
      this.loggerProvider.Log(ProviderLogLevel.Info, "Stopping task that fires events for streaming data...");
    }

    protected abstract void StreamBandData(ManualResetEvent started, CancellationToken stop);

    protected int ProcessSensorSubscriptionPayload(ICargoReader reader)
    {
      RemoteSubscriptionSampleHeader sampleHeader = RemoteSubscriptionSampleHeader.DeserializeFromBand(reader);
      switch (sampleHeader.SubscriptionType)
      {
        case SubscriptionType.Accelerometer32MS:
        case SubscriptionType.AccelerometerGyroscope32MS:
        case SubscriptionType.Accelerometer16MS:
        case SubscriptionType.AccelerometerGyroscope16MS:
          int num = 0;
          BandSensorSampleDeserializer sampleDeserializer = BandClient.TryGetBandSensorSampleDeserializer(sampleHeader);
          if (sampleDeserializer == null)
          {
            this.loggerProvider.Log(ProviderLogLevel.Warning, "Unsupported subscription type {0} received.", (object) sampleHeader.SubscriptionType);
          }
          else
          {
            num = sampleDeserializer.GetSerializeByteCount(sampleHeader);
            if ((int) sampleHeader.SampleSize % num != 0)
            {
              this.loggerProvider.Log(ProviderLogLevel.Error, "Subscription type {0} sample array size is not multiple of sample size.", (object) sampleHeader.SubscriptionType);
              num = 0;
            }
          }
          if (num == 0)
          {
            reader.ReadExactAndDiscard((int) sampleHeader.SampleSize);
          }
          else
          {
            DateTimeOffset now = DateTimeOffset.Now;
            Queue<BandSensorReadingBase> sensorEventQueue = this.sensorEventQueue;
            bool lockTaken = false;
            try
            {
              Monitor.Enter((object) sensorEventQueue, ref lockTaken);
              for (int index = (int) sampleHeader.SampleSize / num; index > 0; --index)
              {
                this.sensorEventQueue.Enqueue(sampleDeserializer.DeserializeFromBand(reader, sampleHeader, now));
                if (this.sensorEventQueue.Count > 1000)
                  this.sensorEventQueue.Dequeue();
                this.streamingDataReceivedEvent.Set();
              }
            }
            finally
            {
              if (lockTaken)
                Monitor.Exit((object) sensorEventQueue);
            }
          }
          return RemoteSubscriptionSampleHeader.GetSerializedByteCount() + (int) sampleHeader.SampleSize;
        default:
          this.loggerProvider.Log(ProviderLogLevel.Info, "QueueSensorSubscriptionPayload(): Type: {0}, Missed Samples: {1}, Sample Size: {2}", (object) sampleHeader.SubscriptionType, (object) sampleHeader.NumMissedSamples, (object) sampleHeader.SampleSize);
          goto case SubscriptionType.Accelerometer32MS;
      }
    }

    private static BandSensorSampleDeserializer TryGetBandSensorSampleDeserializer(
      RemoteSubscriptionSampleHeader sampleHeader)
    {
      return sampleHeader.SubscriptionType >= (SubscriptionType) BandClient.BandSensorSampleDeserializerTable.Length ? (BandSensorSampleDeserializer) null : BandClient.BandSensorSampleDeserializerTable[(int) sampleHeader.SubscriptionType];
    }

    protected int ProcessTileEventPayload(ICargoReader reader)
    {
      byte[] tileFriendlyName;
      BandTileEventBase bandTileEventBase = BandTileEventBase.DeserializeFromBand(reader, DateTimeOffset.Now, out tileFriendlyName);
      if (bandTileEventBase != null)
      {
        this.loggerProvider.Log(ProviderLogLevel.Info, "QueueTileEventPayload(): Type: {0}, TileId: {1}", (object) bandTileEventBase.GetType().Name, (object) bandTileEventBase.TileId);
        bool flag;
        if (!this.tileIdOwnership.TryGetValue(bandTileEventBase.TileId, out flag))
        {
          Guid? currentAppId = this.currentAppId;
          Guid applicationIdFromName = BandClient.GetApplicationIdFromName(tileFriendlyName, (ushort) 0);
          flag = currentAppId.HasValue && (!currentAppId.HasValue || currentAppId.GetValueOrDefault() == applicationIdFromName);
          this.tileIdOwnership.Add(bandTileEventBase.TileId, flag);
        }
        if (flag)
        {
          Queue<BandTileEventBase> tileEventQueue = this.tileEventQueue;
          bool lockTaken = false;
          try
          {
            Monitor.Enter((object) tileEventQueue, ref lockTaken);
            this.tileEventQueue.Enqueue(bandTileEventBase);
            if (this.tileEventQueue.Count > 200)
              this.tileEventQueue.Dequeue();
            this.streamingDataReceivedEvent.Set();
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit((object) tileEventQueue);
          }
        }
      }
      return BandTileEventBase.GetSerializedByteCount();
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    internal void DispatchTileOpenedEvent(BandTileOpenedEvent tileEvent)
    {
      EventHandler<BandTileEventArgs<IBandTileOpenedEvent>> tileOpened = this.TileOpened;
      if (tileOpened == null)
        return;
      try
      {
        tileOpened((object) this, new BandTileEventArgs<IBandTileOpenedEvent>((IBandTileOpenedEvent) tileEvent));
      }
      catch (Exception ex)
      {
        this.loggerProvider.LogException(ProviderLogLevel.Error, ex);
        Environment.FailFast("BandClient.TileOpened event handler threw an exception that was not handled by the application.", ex);
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    internal void DispatchTileButtonPressedEvent(BandTileButtonPressedEvent tileEvent)
    {
      EventHandler<BandTileEventArgs<IBandTileButtonPressedEvent>> tileButtonPressed = this.TileButtonPressed;
      if (tileButtonPressed == null)
        return;
      try
      {
        tileButtonPressed((object) this, new BandTileEventArgs<IBandTileButtonPressedEvent>((IBandTileButtonPressedEvent) tileEvent));
      }
      catch (Exception ex)
      {
        this.loggerProvider.LogException(ProviderLogLevel.Error, ex);
        Environment.FailFast("BandClient.TileButtonPressed event handler threw an exception that was not handled by the application.", ex);
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    internal void DispatchTileClosedEvent(BandTileClosedEvent tileEvent)
    {
      EventHandler<BandTileEventArgs<IBandTileClosedEvent>> tileClosed = this.TileClosed;
      if (tileClosed == null)
        return;
      try
      {
        tileClosed((object) this, new BandTileEventArgs<IBandTileClosedEvent>((IBandTileClosedEvent) tileEvent));
      }
      catch (Exception ex)
      {
        this.loggerProvider.LogException(ProviderLogLevel.Error, ex);
        Environment.FailFast("BandClient.TileClosed event handler threw an exception that was not handled by the application.", ex);
      }
    }

    public static Guid GetApplicationIdFromName(
      byte[] nameAndOwnerId,
      ushort friendlyNameLength)
    {
      return nameAndOwnerId != null && nameAndOwnerId.Length >= 16 && friendlyNameLength <= (ushort) 21 ? BandBitConverter.ToGuid(nameAndOwnerId, nameAndOwnerId.Length - 16) : Guid.Empty;
    }

    public event EventHandler<BandTileEventArgs<IBandTileOpenedEvent>> TileOpened;

    public event EventHandler<BandTileEventArgs<IBandTileButtonPressedEvent>> TileButtonPressed;

    public event EventHandler<BandTileEventArgs<IBandTileClosedEvent>> TileClosed;

    public Task StartReadingsAsync() => this.StartReadingsAsync(CancellationToken.None);

    public virtual Task StartReadingsAsync(CancellationToken token) => Task.Run((Action) (() =>
    {
      object tileEventLock = this.tileEventLock;
      bool lockTaken = false;
      try
      {
        Monitor.Enter(tileEventLock, ref lockTaken);
        this.EventingSubscribe();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(tileEventLock);
      }
    }), token);

    public Task StopReadingsAsync() => this.StopReadingsAsync(CancellationToken.None);

    public virtual Task StopReadingsAsync(CancellationToken token) => Task.Run((Action) (() =>
    {
      object tileEventLock = this.tileEventLock;
      bool lockTaken = false;
      try
      {
        Monitor.Enter(tileEventLock, ref lockTaken);
        this.EventingUnsubscribe();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(tileEventLock);
      }
    }), token);

    public Task<IEnumerable<BandTile>> GetTilesAsync() => this.GetTilesAsync(CancellationToken.None);

    public Task<IEnumerable<BandTile>> GetTilesAsync(CancellationToken token)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      return Task.Run<IEnumerable<BandTile>>((Func<Task<IEnumerable<BandTile>>>) (async () =>
      {
        Guid applicationId;
        Guid guid = applicationId;
        applicationId = await this.applicationPlatformProvider.GetApplicationIdAsync(token).ConfigureAwait(false);
        token.ThrowIfCancellationRequested();
        BandTile[] array = this.GetInstalledTiles().Where<TileData>((Func<TileData, bool>) (tile => tile.OwnerId == applicationId)).Select<TileData, BandTile>((Func<TileData, BandTile>) (tile => tile.ToBandTile())).ToArray<BandTile>();
        foreach (BandTile bandTile in array)
        {
          for (uint layoutIndex = 0; layoutIndex < 5U; ++layoutIndex)
          {
            token.ThrowIfCancellationRequested();
            PageLayout layout = this.DynamicPageLayoutGetLayout(bandTile.TileId, layoutIndex);
            if (layout != null)
              bandTile.PageLayouts.Add(layout);
            else
              break;
          }
        }
        return (IEnumerable<BandTile>) array;
      }), token);
    }

    public Task<bool> AddTileAsync(BandTile tile) => this.AddTileAsync(tile, CancellationToken.None);

    public async Task<bool> AddTileAsync(BandTile tile, CancellationToken token)
    {
      if (tile == null)
        throw new ArgumentNullException(nameof (tile));
      if (string.IsNullOrWhiteSpace(tile.Name))
        throw new ArgumentException(BandResources.BandTileEmptyName, nameof (tile));
      if (tile.SmallIcon == null)
        throw new ArgumentException(BandResources.BandTileNoSmallIcon, nameof (tile));
      if (tile.TileIcon == null)
        throw new ArgumentException(BandResources.BandTileNoTileIcon, nameof (tile));
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      if (tile.AdditionalIcons.Count + 2 > this.BandTypeConstants.MaxIconsPerTile)
        throw new ArgumentException(BandResources.BandTileTooManyIcons, nameof (tile));
      if (tile.PageLayouts.Count > 5)
        throw new ArgumentException(BandResources.BandTileTooManyTemplates, nameof (tile));
      foreach (PageLayout pageLayout in (IEnumerable<PageLayout>) tile.PageLayouts)
      {
        if (pageLayout == null)
          throw new InvalidOperationException(BandResources.BandTileNullTemplateEncountered);
        if (pageLayout.GetSerializedByteCountAndValidate() > 768)
          throw new ArgumentException(BandResources.BandTilePageTemplateBlobTooBig);
        if (this.BandTypeConstants.BandType == BandType.Cargo && pageLayout.Elements.Any<PageElement>((Func<PageElement, bool>) (element => element is IconButton)))
          throw new ArgumentException(BandResources.IconButtonsAreNotSupportedOnCargo);
      }
      token.ThrowIfCancellationRequested();
      IList<TileData> installedTiles = (IList<TileData>) null;
      if (!await Task.Run<bool>((Func<bool>) (() =>
      {
        if (BandClient.KnownTiles.AllTileGuids.Contains(tile.TileId))
          return false;
        token.ThrowIfCancellationRequested();
        installedTiles = this.GetInstalledTilesNoIcons();
        if (installedTiles.Any<TileData>((Func<TileData, bool>) (installedTile => installedTile.AppID == tile.TileId)))
          throw new InvalidOperationException(BandResources.BandTileIdAlreadyInstalled);
        token.ThrowIfCancellationRequested();
        if ((int) this.GetTileCapacity() == installedTiles.Count)
          throw new InvalidOperationException(BandResources.BandAtMaxTileCapacity);
        token.ThrowIfCancellationRequested();
        return !this.GetDefaultTilesNoIconsInternal().Any<TileData>((Func<TileData, bool>) (defaultTile => defaultTile.AppID == tile.TileId));
      })))
        return false;
      if (!await this.applicationPlatformProvider.GetAddTileConsentAsync(tile, token))
        return false;
      token.ThrowIfCancellationRequested();
      await Task.Run((Func<Task>) (async () =>
      {
        Guid applicationIdAsync = await this.applicationPlatformProvider.GetApplicationIdAsync(token);
        token.ThrowIfCancellationRequested();
        this.AddTile(tile, applicationIdAsync, (IEnumerable<TileData>) installedTiles);
      }), token);
      return true;
    }

    public Task<bool> SetPagesAsync(Guid tileId, params PageData[] pages) => this.SetPagesAsync(tileId, CancellationToken.None, (IEnumerable<PageData>) pages);

    public Task<bool> SetPagesAsync(Guid tileId, IEnumerable<PageData> pages) => this.SetPagesAsync(tileId, CancellationToken.None, pages);

    public Task<bool> SetPagesAsync(
      Guid tileId,
      CancellationToken token,
      params PageData[] pages)
    {
      return this.SetPagesAsync(tileId, token, (IEnumerable<PageData>) pages);
    }

    public Task<bool> SetPagesAsync(
      Guid tileId,
      CancellationToken token,
      IEnumerable<PageData> pages)
    {
      if (tileId == Guid.Empty)
        throw new ArgumentException(string.Format(BandResources.SetPagesEmptyGuid, (object) tileId));
      PageData[] pageList = pages != null ? pages.ToArray<PageData>() : throw new ArgumentNullException(nameof (pages));
      if (pageList.Length == 0)
        throw new ArgumentException(string.Format(BandResources.GenericCountZero, (object) nameof (pages)));
      foreach (PageData pageData in pageList)
      {
        if (pageData == null)
          throw new ArgumentException(BandResources.GenericNullCollectionElement, nameof (pages));
      }
      if (BandClient.KnownTiles.AllTileGuids.Contains(tileId))
        return Task.FromResult<bool>(false);
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      return Task.Run<bool>((Func<bool>) (() => this.SetPagesWithOwnerValidation(tileId, token, pageList)));
    }

    internal bool SetPagesWithOwnerValidation(
      Guid tileId,
      CancellationToken token,
      PageData[] pages)
    {
      if (!this.TileInstalledAndOwned(tileId, token))
        return false;
      this.SetPages(tileId, token, (IEnumerable<PageData>) pages);
      return true;
    }

    internal void SetPages(Guid tileId, CancellationToken token, IEnumerable<PageData> pages)
    {
      foreach (PageData page in pages)
      {
        token.ThrowIfCancellationRequested();
        int lengthAndValidate = page.GetSerializedLengthAndValidate(this.BandTypeConstants);
        int dataSize = 40 + lengthAndValidate;
        using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoNotification, dataSize, CommandStatusHandling.DoNotCheck))
        {
          cargoCommandWriter.WriteUInt16((ushort) 101);
          cargoCommandWriter.WriteGuid(tileId);
          cargoCommandWriter.WriteUInt16((ushort) lengthAndValidate);
          cargoCommandWriter.WriteUInt16((ushort) page.PageLayoutIndex);
          cargoCommandWriter.WriteGuid(page.PageId);
          cargoCommandWriter.WriteByte((byte) 0);
          cargoCommandWriter.WriteByte((byte) 0);
          page.SerializeToBand((ICargoWriter) cargoCommandWriter);
          BandClient.CheckStatus(cargoCommandWriter.CommandStatus, CommandStatusHandling.ThrowAnyNonZero, this.loggerProvider);
        }
      }
    }

    public Task<bool> RemovePagesAsync(Guid tileId) => this.RemovePagesAsync(tileId, CancellationToken.None);

    public Task<bool> RemovePagesAsync(Guid tileId, CancellationToken token)
    {
      if (tileId == Guid.Empty)
        throw new ArgumentException(string.Format(BandResources.RemovePagesEmptyGuid, (object) tileId));
      if (BandClient.KnownTiles.AllTileGuids.Contains(tileId))
        return Task.FromResult<bool>(false);
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      return Task.Run<bool>((Func<bool>) (() => this.RemovePagesWithOwnerValidation(tileId, token)));
    }

    private bool RemovePagesWithOwnerValidation(Guid tileId, CancellationToken token)
    {
      if (!this.TileInstalledAndOwned(tileId, token))
        return false;
      this.RemovePages(tileId, token);
      return true;
    }

    protected void RemovePages(Guid tileId, CancellationToken token)
    {
      token.ThrowIfCancellationRequested();
      this.SendNotification(NotificationID.GenericClearTile, NotificationPBMessageType.TileManagement, (NotificationBase) new NotificationGenericClearTile(tileId));
    }

    public Task<bool> RemoveTileAsync(BandTile tile) => this.RemoveTileAsync(tile, CancellationToken.None);

    public Task<bool> RemoveTileAsync(BandTile tile, CancellationToken token)
    {
      if (tile == null)
        throw new ArgumentNullException(nameof (tile));
      return this.RemoveTileAsync(tile.TileId, token);
    }

    public Task<bool> RemoveTileAsync(Guid tileId) => this.RemoveTileAsync(tileId, CancellationToken.None);

    public Task<bool> RemoveTileAsync(Guid tileId, CancellationToken token)
    {
      if (tileId == Guid.Empty)
        throw new ArgumentException(string.Format(BandResources.RemoveTileEmptyTileId, (object) tileId));
      if (BandClient.KnownTiles.AllTileGuids.Contains(tileId))
        return Task.FromResult<bool>(false);
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      Func<TileData, bool> func;
      return Task.Run<bool>((Func<Task<bool>>) (async () =>
      {
        Guid applicationId;
        Guid guid = applicationId;
        applicationId = await this.applicationPlatformProvider.GetApplicationIdAsync(token).ConfigureAwait(false);
        token.ThrowIfCancellationRequested();
        IList<TileData> installedTilesNoIcons = this.GetInstalledTilesNoIcons();
        if (installedTilesNoIcons.FirstOrDefault<TileData>((Func<TileData, bool>) (tile => tile.OwnerId == applicationId && tile.AppID == tileId)) == null)
          return false;
        token.ThrowIfCancellationRequested();
        this.RemoveTile(tileId, (IEnumerable<TileData>) installedTilesNoIcons);
        token.ThrowIfCancellationRequested();
        if (this.GetInstalledTilesNoIcons().Any<TileData>(func ?? (func = (Func<TileData, bool>) (tile => tile.AppID == tileId))))
          throw new BandException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, BandResources.RemoveTileFailed, (object) tileId));
        return true;
      }), token);
    }

    public Task<int> GetRemainingTileCapacityAsync() => this.GetRemainingTileCapacityAsync(CancellationToken.None);

    public Task<int> GetRemainingTileCapacityAsync(CancellationToken token)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      return Task.Run<int>((Func<int>) (() => this.GetRemainingTileCapacity(token)));
    }

    private int GetRemainingTileCapacity(CancellationToken token)
    {
      token.ThrowIfCancellationRequested();
      int tileCapacity = (int) this.GetTileCapacity();
      token.ThrowIfCancellationRequested();
      int count = this.GetInstalledTilesNoIcons().Count;
      return tileCapacity - count;
    }

    protected uint GetTileCapacity() => this.cachedThings.GetTileCapacity();

    protected uint GetTileMaxAllocatedCapacity() => this.cachedThings.GetTileMaxAllocatedCapacity();

    internal IList<TileData> GetInstalledTiles()
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Obtaining the AppList from the KDevice");
      return (IList<TileData>) this.GetTilesHelper(DeviceCommands.CargoInstalledAppListGet, true).OrderBy<TileData, uint>((Func<TileData, uint>) (t => t.StartStripOrder)).ToList<TileData>();
    }

    protected IList<TileData> GetInstalledTilesNoIcons()
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Obtaining the AppList from the KDevice without images");
      return (IList<TileData>) this.GetTilesHelper(DeviceCommands.CargoInstalledAppListGetNoImages, false).OrderBy<TileData, uint>((Func<TileData, uint>) (t => t.StartStripOrder)).ToList<TileData>();
    }

    internal IList<TileData> GetDefaultTilesInternal()
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Obtaining the default AppList from the KDevice");
      return this.GetTilesHelper(DeviceCommands.CargoInstalledAppListGetDefaults, true);
    }

    protected IList<TileData> GetDefaultTilesNoIconsInternal()
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Obtaining the default AppList from the KDevice without images");
      return this.GetTilesHelper(DeviceCommands.CargoInstalledAppListGetDefaultsNoImages, false);
    }

    private TileData GetInstalledTileNoIcons(Guid applicationId, Guid tileId) => this.GetInstalledTilesNoIcons().FirstOrDefault<TileData>((Func<TileData, bool>) (tile => tile.OwnerId == applicationId && tile.AppID == tileId));

    private IList<TileData> GetTilesHelper(ushort commandId, bool withIcons)
    {
      DisposableList<PooledBuffer> disposableList = (DisposableList<PooledBuffer>) null;
      if (withIcons)
        disposableList = new DisposableList<PooledBuffer>();
      using (disposableList)
      {
        try
        {
          int allocatedCapacity = (int) this.GetTileMaxAllocatedCapacity();
          int num1 = 0;
          List<TileData> tileDataList = new List<TileData>(allocatedCapacity);
          if (withIcons)
            num1 += 1024 * allocatedCapacity;
          int bytesToRead = num1 + (4 + TileData.GetSerializedByteCount() * allocatedCapacity);
          using (CargoCommandReader cargoCommandReader = this.ProtocolBeginRead(commandId, bytesToRead, CommandStatusHandling.DoNotCheck))
          {
            if (withIcons)
            {
              for (int index = 0; index < allocatedCapacity; ++index)
              {
                PooledBuffer buffer = BufferServer.GetBuffer(1024);
                disposableList.Add(buffer);
                cargoCommandReader.ReadExact(buffer.Buffer, 0, buffer.Length);
              }
            }
            uint num2 = cargoCommandReader.ReadUInt32();
            for (int index = 0; (long) index < (long) num2; ++index)
              tileDataList.Add(TileData.DeserializeFromBand((ICargoReader) cargoCommandReader));
            if (cargoCommandReader.BytesRemaining > 0)
              cargoCommandReader.ReadToEndAndDiscard();
            BandClient.CheckStatus(cargoCommandReader.CommandStatus, CommandStatusHandling.ThrowOnlySeverityError, this.loggerProvider);
          }
          if (withIcons)
          {
            for (int index = 0; index < tileDataList.Count; ++index)
              tileDataList[index].Icon = BandIconRleCodec.DecodeTileIconRle(disposableList[index]);
          }
          return (IList<TileData>) tileDataList;
        }
        catch (BandIOException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          throw new BandIOException(ex.Message, ex);
        }
      }
    }

    private void AddTile(BandTile tile, Guid applicationId, IEnumerable<TileData> installedTiles)
    {
      this.loggerProvider.Log(ProviderLogLevel.Verbose, "Adding new tile");
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      installedTiles = (IEnumerable<TileData>) installedTiles.OrderBy<TileData, uint>((Func<TileData, uint>) (t => t.StartStripOrder));
      if (tile.Theme != null)
        this.ValidateTileTheme(tile.Theme, tile.TileId);
      this.RunUsingSynchronizedFirmwareUI((Action) (() =>
      {
        this.AddTileInsideSync(tile, applicationId, installedTiles);
        if (tile.Theme == null)
          return;
        this.SetTileThemeInternal(tile.Theme, tile.TileId);
      }), (Action) (() => this.AddTileOutsideSync(tile)));
    }

    private void AddTileInsideSync(
      BandTile tile,
      Guid applicationId,
      IEnumerable<TileData> orderedInstalledTiles)
    {
      IEnumerable<BandIcon> icons = EnumerableExtensions.Concat<BandIcon>(tile.TileIcon, tile.SmallIcon).Concat<BandIcon>((IEnumerable<BandIcon>) tile.AdditionalIcons);
      this.RegisterTileIcons(tile.TileId, tile.Name, icons, false);
      this.SetTileIconIndexes(tile.TileId, 0U, 1U, 1U);
      int startStripOrder = orderedInstalledTiles.Count<TileData>();
      this.SetStartStripData(orderedInstalledTiles.Concat<TileData>(tile.ToTileData(startStripOrder, applicationId)), startStripOrder + 1);
    }

    private void AddTileOutsideSync(BandTile tile)
    {
      for (int index = 0; index < tile.PageLayouts.Count; ++index)
        this.DynamicPageLayoutSetLayout(tile.TileId, (uint) index, tile.PageLayouts[index]);
      for (uint count = (uint) tile.PageLayouts.Count; count < 5U; ++count)
        this.DynamicPageLayoutRemoveLayout(tile.TileId, count);
    }

    protected void RegisterTileIcons(
      Guid tileId,
      string friendlyName,
      IEnumerable<BandIcon> icons,
      bool iconsAlreadyRegistered)
    {
      using (DisposableList<PooledBuffer> disposableList = new DisposableList<PooledBuffer>())
      {
        int dataSize = 20;
        foreach (BandIcon icon in icons)
        {
          PooledBuffer pooledBuffer = BandIconRleCodec.EncodeTileIconRle(icon);
          disposableList.Add(pooledBuffer);
          dataSize += pooledBuffer.Length;
        }
        ushort commandId;
        if (iconsAlreadyRegistered)
        {
          commandId = DeviceCommands.CargoDynamicAppRegisterAppIcons;
          this.loggerProvider.Log(ProviderLogLevel.Verbose, "Invoking DynamicAppUpdateStrappIcons for strapp: {0}", (object) friendlyName);
        }
        else
        {
          commandId = DeviceCommands.CargoDynamicAppRegisterApp;
          this.loggerProvider.Log(ProviderLogLevel.Verbose, "Invoking DynamicAppRegisterStrapp for strapp: {0}", (object) friendlyName);
        }
        try
        {
          using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(commandId, dataSize, CommandStatusHandling.ThrowOnlySeverityError))
          {
            cargoCommandWriter.WriteGuid(tileId);
            cargoCommandWriter.WriteInt32(disposableList.Count);
            foreach (PooledBuffer pooledBuffer in (List<PooledBuffer>) disposableList)
              cargoCommandWriter.Write(pooledBuffer.Buffer, 0, pooledBuffer.Length);
          }
        }
        catch (BandIOException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          throw new BandIOException(ex.Message, ex);
        }
      }
    }

    private void RemoveTile(Guid guid, IEnumerable<TileData> installedTiles)
    {
      this.loggerProvider.Log(ProviderLogLevel.Verbose, "Removing tile");
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      installedTiles = (IEnumerable<TileData>) installedTiles.OrderBy<TileData, uint>((Func<TileData, uint>) (t => t.StartStripOrder));
      this.RunUsingSynchronizedFirmwareUI((Action) (() => this.RemoveTileInsideSync(guid, installedTiles)));
    }

    private void RemoveTileInsideSync(Guid guid, IEnumerable<TileData> orderedInstalledTiles)
    {
      this.UnregisterTileIcons(guid);
      TileData[] array = orderedInstalledTiles.Where<TileData>((Func<TileData, bool>) (tile => tile.AppID != guid)).ToArray<TileData>();
      this.SetStartStripData((IEnumerable<TileData>) array, array.Length);
    }

    protected void UnregisterTileIcons(Guid guid)
    {
      this.loggerProvider.Log(ProviderLogLevel.Verbose, "Invoking DynamicAppRemoveStrapp for strapp: {0}", (object) guid.ToString());
      Action<ICargoWriter> writeData = (Action<ICargoWriter>) (w => w.WriteGuid(guid));
      this.ProtocolWriteWithData(DeviceCommands.CargoDynamicAppRemoveApp, 16, writeData, 60000);
    }

    protected void RunUsingSynchronizedFirmwareUI(Action insideSync, Action afterSync = null)
    {
      if (insideSync == null)
        throw new ArgumentNullException(nameof (insideSync));
      bool flag = false;
      try
      {
        this.FirmwareUiSyncStart();
        insideSync();
        flag = true;
      }
      finally
      {
        try
        {
          this.FirmwareUiSyncEnd();
        }
        catch
        {
          if (flag)
            throw;
        }
      }
      if (afterSync == null)
        return;
      afterSync();
    }

    private void FirmwareUiSyncStart()
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Starting the startStrip sync");
      this.ProtocolWrite(DeviceCommands.CargoInstalledAppListStartStripSyncStart, 60000);
    }

    private void FirmwareUiSyncEnd()
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Ending the startStrip sync");
      this.ProtocolWrite(DeviceCommands.CargoInstalledAppListStartStripSyncEnd, 60000);
    }

    protected void SetTileIconIndexes(
      Guid tileId,
      uint tileIconIndex,
      uint badgeIconIndex,
      uint notificationIconIndex)
    {
      this.SetMainIconIndex(tileId, tileIconIndex);
      this.SetBadgeIconIndex(tileId, badgeIconIndex);
      this.SetNotificationIconIndex(tileId, notificationIconIndex);
    }

    protected void SetMainIconIndex(Guid tileId, uint iconIndex)
    {
      this.loggerProvider.Log(ProviderLogLevel.Verbose, "Invoking DynamicAppSetTileIconIndex for tile: {0}", (object) tileId);
      this.SetTileIconIndex(tileId, DeviceCommands.CargoDynamicAppSetAppTileIndex, iconIndex);
    }

    protected void SetBadgeIconIndex(Guid tileId, uint iconIndex)
    {
      this.loggerProvider.Log(ProviderLogLevel.Verbose, "Invoking DynamicAppSetBadgeIconIndex for tile: {0}", (object) tileId);
      this.SetTileIconIndex(tileId, DeviceCommands.CargoDynamicAppSetAppBadgeTileIndex, iconIndex);
    }

    protected void SetNotificationIconIndex(Guid tileId, uint iconIndex)
    {
      if (this.BandTypeConstants.BandType == BandType.Envoy)
      {
        this.loggerProvider.Log(ProviderLogLevel.Verbose, "Invoking DynamicAppSetNotificationIconIndex for tile: {0}", (object) tileId);
        this.SetTileIconIndex(tileId, DeviceCommands.CargoDynamicAppSetAppNotificationTileIndex, iconIndex);
      }
      else
        this.loggerProvider.Log(ProviderLogLevel.Verbose, "Silently ignoring SetNotificationIconIndex() for Cargo device, tile: {0}", (object) tileId);
    }

    private void SetTileIconIndex(Guid guid, ushort iconIndexCommandId, uint iconIndex)
    {
      int dataSize = 20;
      try
      {
        using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(iconIndexCommandId, dataSize, CommandStatusHandling.ThrowOnlySeverityError))
        {
          cargoCommandWriter.WriteGuid(guid);
          cargoCommandWriter.WriteUInt32(iconIndex);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    protected void SetStartStripData(IEnumerable<TileData> orderedList, int count)
    {
      Action<ICargoWriter> writeArgBuf = (Action<ICargoWriter>) (w => w.WriteInt32(count));
      int num = 0;
      this.loggerProvider.Log(ProviderLogLevel.Info, "Setting the installed AppList");
      int dataSize = 4 + count * TileData.GetSerializedByteCount();
      try
      {
        using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoInstalledAppListSet, 4, dataSize, writeArgBuf, CommandStatusHandling.DoNotCheck))
        {
          this.deviceTransport.CargoStream.ReadTimeout = 60000;
          cargoCommandWriter.WriteUInt32((uint) count);
          foreach (TileData tileData in orderedList.Take<TileData>(count))
            tileData.SerializeToBand((ICargoWriter) cargoCommandWriter, new uint?((uint) num++));
          cargoCommandWriter.Flush();
          BandClient.CheckStatus(cargoCommandWriter.CommandStatus, CommandStatusHandling.ThrowOnlySeverityError, this.loggerProvider);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    protected void SetTileThemeInternal(BandTheme theme, Guid id, CancellationToken cancel)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnectedOrUpdateMode();
      this.ValidateTileTheme(theme, id);
      cancel.ThrowIfCancellationRequested();
      this.RunUsingSynchronizedFirmwareUI((Action) (() => this.SetTileThemeInternal(theme, id)));
    }

    protected void SetTileThemeInternal(BandTheme theme, Guid id)
    {
      int dataSize = 40;
      try
      {
        using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoThemeColorSetCustomTheme, dataSize, CommandStatusHandling.ThrowOnlySeverityError))
        {
          cargoCommandWriter.WriteUInt32(theme.Base.ToRgb());
          cargoCommandWriter.WriteUInt32(theme.Highlight.ToRgb());
          cargoCommandWriter.WriteUInt32(theme.Lowlight.ToRgb());
          cargoCommandWriter.WriteUInt32(theme.SecondaryText.ToRgb());
          cargoCommandWriter.WriteUInt32(theme.HighContrast.ToRgb());
          cargoCommandWriter.WriteUInt32(theme.Muted.ToRgb());
          cargoCommandWriter.WriteGuid(id);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    protected void ValidateTileTheme(BandTheme theme, Guid id)
    {
    }

    protected void DynamicPageLayoutRemoveLayout(Guid appId, uint layoutIndex)
    {
      int dataSize = 20;
      using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoDynamicPageLayoutRemove, dataSize, CommandStatusHandling.ThrowOnlySeverityError))
      {
        cargoCommandWriter.WriteGuid(appId);
        cargoCommandWriter.WriteUInt32(layoutIndex);
      }
    }

    protected void DynamicPageLayoutSetSerializedLayout(
      Guid appId,
      uint layoutIndex,
      byte[] layoutBlob)
    {
      if (layoutBlob.Length > 768)
        throw new ArgumentException(BandResources.BandTilePageTemplateBlobTooBig);
      int dataSize = 24 + layoutBlob.Length;
      try
      {
        using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoDynamicPageLayoutSet, dataSize, CommandStatusHandling.DoNotCheck))
        {
          ICargoStream cargoStream = this.deviceTransport.CargoStream;
          int readTimeout = cargoStream.ReadTimeout;
          int writeTimeout = cargoStream.WriteTimeout;
          try
          {
            cargoStream.ReadTimeout *= 2;
            cargoStream.WriteTimeout *= 2;
            cargoCommandWriter.WriteGuid(appId);
            cargoCommandWriter.WriteUInt32(layoutIndex);
            cargoCommandWriter.WriteInt32(layoutBlob.Length);
            cargoCommandWriter.Write(layoutBlob);
            BandClient.CheckStatus(cargoCommandWriter.CommandStatus, CommandStatusHandling.ThrowOnlySeverityError, this.loggerProvider);
          }
          finally
          {
            cargoStream.ReadTimeout = readTimeout;
            cargoStream.WriteTimeout = writeTimeout;
          }
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    protected void DynamicPageLayoutSetLayout(Guid appId, uint layoutIndex, PageLayout layout)
    {
      int countAndValidate = layout.GetSerializedByteCountAndValidate();
      if (countAndValidate > 768)
        throw new ArgumentException(BandResources.BandTilePageTemplateBlobTooBig);
      int dataSize = 24 + countAndValidate;
      try
      {
        using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoDynamicPageLayoutSet, dataSize, CommandStatusHandling.DoNotCheck))
        {
          cargoCommandWriter.WriteGuid(appId);
          cargoCommandWriter.WriteUInt32(layoutIndex);
          cargoCommandWriter.WriteInt32(countAndValidate);
          layout.SerializeToBand((ICargoWriter) cargoCommandWriter);
          BandClient.CheckStatus(cargoCommandWriter.CommandStatus, CommandStatusHandling.ThrowOnlySeverityError, this.loggerProvider);
        }
      }
      catch (BandIOException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new BandIOException(ex.Message, ex);
      }
    }

    protected byte[] DynamicPageLayoutGetSerializedLayout(Guid appId, uint layoutIndex)
    {
      int serializedByteCount = GetPageLayoutArgs.GetSerializedByteCount();
      Action<ICargoWriter> writeArgBuf = (Action<ICargoWriter>) (w => GetPageLayoutArgs.SerializeToBand(w, appId, layoutIndex));
      using (CargoCommandReader cargoCommandReader = this.ProtocolBeginRead(DeviceCommands.CargoDynamicPageLayoutGet, serializedByteCount, 768, writeArgBuf, CommandStatusHandling.DoNotCheck))
      {
        byte[] numArray = cargoCommandReader.ReadExact(768);
        BandClient.CheckStatus(cargoCommandReader.CommandStatus, CommandStatusHandling.ThrowOnlySeverityError, this.loggerProvider);
        return numArray;
      }
    }

    protected PageLayout DynamicPageLayoutGetLayout(Guid appId, uint layoutIndex)
    {
      int serializedByteCount = GetPageLayoutArgs.GetSerializedByteCount();
      Action<ICargoWriter> writeArgBuf = (Action<ICargoWriter>) (w => GetPageLayoutArgs.SerializeToBand(w, appId, layoutIndex));
      PageLayout pageLayout = (PageLayout) null;
      using (CargoCommandReader cargoCommandReader = this.ProtocolBeginRead(DeviceCommands.CargoDynamicPageLayoutGet, serializedByteCount, 768, writeArgBuf, CommandStatusHandling.DoNotCheck))
      {
        try
        {
          pageLayout = PageLayout.DeserializeFromBand((ICargoReader) cargoCommandReader);
        }
        finally
        {
          cargoCommandReader.ReadToEndAndDiscard();
        }
        BandClient.CheckStatus(cargoCommandReader.CommandStatus, CommandStatusHandling.ThrowAnyNonZero, this.loggerProvider);
      }
      return pageLayout;
    }

    public bool TileInstalledAndOwned(Guid tileId, CancellationToken token)
    {
      Guid result = this.applicationPlatformProvider.GetApplicationIdAsync(token).Result;
      TileData installedTileNoIcons = this.GetInstalledTileNoIcons(result, tileId);
      return installedTileNoIcons != null && !(installedTileNoIcons.OwnerId != result);
    }

    protected class CachedThings
    {
      private BandClient bandClient;
      private uint apiVersion;
      private uint tileCapacity;
      private uint tileMaxAllocatedCapacity;

      public CachedThings(BandClient bandClient) => this.bandClient = bandClient;

      public uint GetApiVersion()
      {
        if (this.apiVersion == 0U)
        {
          this.CheckCanUseProtocol();
          this.apiVersion = this.ReadApiVersion();
        }
        return this.apiVersion;
      }

      public uint GetTileCapacity()
      {
        if (this.tileCapacity == 0U)
        {
          this.CheckCanUseProtocol();
          this.tileCapacity = this.ReadTileCapacity();
        }
        return this.tileCapacity;
      }

      public uint GetTileMaxAllocatedCapacity()
      {
        if (this.tileMaxAllocatedCapacity == 0U)
        {
          if (this.GetApiVersion() >= 32U)
          {
            this.CheckCanUseProtocol();
            this.tileMaxAllocatedCapacity = this.ReadTileMaxAllocatedCapacity();
          }
          else
            this.tileMaxAllocatedCapacity = 15U;
        }
        return this.tileMaxAllocatedCapacity;
      }

      public void Clear()
      {
        this.apiVersion = 0U;
        this.tileCapacity = 0U;
        this.tileMaxAllocatedCapacity = 0U;
      }

      private uint ReadApiVersion()
      {
        uint version = 0;
        Action<ICargoReader> readData = (Action<ICargoReader>) (r => version = r.ReadUInt32());
        this.bandClient.ProtocolRead(DeviceCommands.CargoCoreModuleGetApiVersion, 4, readData);
        return version;
      }

      private uint ReadTileCapacity()
      {
        uint capacity = 0;
        Action<ICargoReader> readData = (Action<ICargoReader>) (r => capacity = r.ReadUInt32());
        this.bandClient.ProtocolRead(DeviceCommands.CargoInstalledAppListGetMaxTileCount, 4, readData);
        return capacity;
      }

      private uint ReadTileMaxAllocatedCapacity()
      {
        uint capacity = 0;
        Action<ICargoReader> readData = (Action<ICargoReader>) (r => capacity = r.ReadUInt32());
        this.bandClient.ProtocolRead(DeviceCommands.CargoInstalledAppListGetMaxTileAllocatedCount, 4, readData);
        return capacity;
      }

      private void CheckCanUseProtocol()
      {
        this.bandClient.CheckIfDisposed();
        this.bandClient.CheckIfDisconnectedOrUpdateMode();
      }
    }

    private static class KnownTiles
    {
      public const string Workouts = "2af008a7-cd03-a04d-bb33-be904e6a2924";
      public const string Run = "65bd93db-4293-46af-9a28-bdd6513b4677";
      public const string Bike = "96430fcb-0060-41cb-9de2-e00cac97f85d";
      public const string Sleep = "23e7bc94-f90d-44e0-843f-250910fdf74e";
      public const string Exercise = "a708f02a-03cd-4da0-bb33-be904e6a2924";
      public const string AlarmStopwatch = "d36a92ea-3e85-4aed-a726-2898a6f2769b";
      public const string UV = "59976cf5-15c8-4799-9e31-f34c765a6bd1";
      public const string Weather = "69a39b4e-084b-4b53-9a1b-581826df9e36";
      public const string Finance = "5992928a-bd79-4bb5-9678-f08246d03e68";
      public const string Starbucks = "64a29f65-70bb-4f32-99a2-0f250a05d427";
      public const string GuidedWorkouts = "0281c878-afa8-40ff-acfd-bca06c5c4922";
      public const string Email = "823ba55a-7c98-4261-ad5e-929031289c6e";
      public const string Facebook = "fd06b486-bbda-4da5-9014-124936386237";
      public const string Twitter = "2e76a806-f509-4110-9c03-43dd2359d2ad";
      public const string Cortana = "d7fb5ff5-906a-4f2c-8269-dde6a75138c4";
      public const string Lync = "c06dc40e-61d2-485c-99de-20bf991a504d";
      public const string FBMessenger = "76b08699-2f2e-9041-96c2-1f4bfc7eab10";
      public const string Feed = "4076b009-0455-4af7-a705-6d4acd45a556";
      public const string Whatsapp = "73942f52-23dc-464a-a7e1-b3a6ba95321f";
      public const string SMS = "b4edbc35-027b-4d10-a797-1099cd2ad98a";
      public const string Calls = "22B1C099-F2BE-4BAC-8ED8-2D6B0B3C25D1";
      public const string Calendar = "ec149021-ce45-40e9-aeee-08f86e4746a7";
      public static readonly string[] AllTileIds = new string[22]
      {
        "2af008a7-cd03-a04d-bb33-be904e6a2924",
        "65bd93db-4293-46af-9a28-bdd6513b4677",
        "96430fcb-0060-41cb-9de2-e00cac97f85d",
        "23e7bc94-f90d-44e0-843f-250910fdf74e",
        "a708f02a-03cd-4da0-bb33-be904e6a2924",
        "d36a92ea-3e85-4aed-a726-2898a6f2769b",
        "59976cf5-15c8-4799-9e31-f34c765a6bd1",
        "69a39b4e-084b-4b53-9a1b-581826df9e36",
        "5992928a-bd79-4bb5-9678-f08246d03e68",
        "64a29f65-70bb-4f32-99a2-0f250a05d427",
        "0281c878-afa8-40ff-acfd-bca06c5c4922",
        "823ba55a-7c98-4261-ad5e-929031289c6e",
        "fd06b486-bbda-4da5-9014-124936386237",
        "2e76a806-f509-4110-9c03-43dd2359d2ad",
        "d7fb5ff5-906a-4f2c-8269-dde6a75138c4",
        "c06dc40e-61d2-485c-99de-20bf991a504d",
        "76b08699-2f2e-9041-96c2-1f4bfc7eab10",
        "4076b009-0455-4af7-a705-6d4acd45a556",
        "73942f52-23dc-464a-a7e1-b3a6ba95321f",
        "b4edbc35-027b-4d10-a797-1099cd2ad98a",
        "22B1C099-F2BE-4BAC-8ED8-2D6B0B3C25D1",
        "ec149021-ce45-40e9-aeee-08f86e4746a7"
      };
      public static readonly HashSet<Guid> AllTileGuids = new HashSet<Guid>(((IEnumerable<string>) BandClient.KnownTiles.AllTileIds).Select<string, Guid>((Func<string, Guid>) (id => Guid.Parse(id))));
    }
  }
}
