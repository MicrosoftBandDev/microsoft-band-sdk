// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Store.BandStoreClient
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using Microsoft.Band.Sensors;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace Microsoft.Band.Store
{
  internal class BandStoreClient : BandClient
  {
    public EventHandler Disconnected;
    private IReadableTransport pushServiceTransport;
    private readonly BluetoothDeviceInfo bluetoothBand;
    private static readonly TimeSpan PushServiceReconnectDelay = TimeSpan.FromSeconds(5.0);
    private static readonly Guid PushServiceGuid = Guid.Parse("{d8895bfd-0461-400d-bd52-dbe2a3c33021}");

    protected override void OnDisconnected(TransportDisconnectedEventArgs args)
    {
      if (args.Reason != TransportDisconnectedReason.TransportIssue)
        return;
      EventHandler disconnected = this.Disconnected;
      if (disconnected == null)
        return;
      disconnected((object) this, (EventArgs) args);
    }

    protected BluetoothDeviceInfo BluetoothBand => this.bluetoothBand;

    internal BandStoreClient(
      BluetoothDeviceInfo deviceInfo,
      IDeviceTransport deviceTransport,
      IReadableTransport pushTransport,
      ILoggerProvider loggerProvider,
      IApplicationPlatformProvider applicationPlatformProvider)
      : base(deviceTransport, loggerProvider, applicationPlatformProvider)
    {
      this.bluetoothBand = deviceInfo;
      this.pushServiceTransport = pushTransport;
      BandStoreClient.ApplicationSuspensionManager.Instance.Suspending += new EventHandler(this.AppSuspending);
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    private void AppSuspending(object sender, EventArgs e)
    {
      try
      {
        this.Dispose();
      }
      catch (Exception ex)
      {
        try
        {
          this.loggerProvider.LogException(ProviderLogLevel.Error, ex);
        }
        catch
        {
        }
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    protected override void Dispose(bool disposing)
    {
      bool flag = false;
      if (disposing && !this.Disposed)
      {
        flag = true;
        try
        {
          BandStoreClient.ApplicationSuspensionManager.Instance.Suspending -= new EventHandler(this.AppSuspending);
        }
        catch
        {
        }
      }
      base.Dispose(disposing);
      if (!flag || this.pushServiceTransport == null)
        return;
      ((IDisposable) this.pushServiceTransport).Dispose();
      this.pushServiceTransport = (IReadableTransport) null;
    }

    protected override void ExecuteSensorSubscribeCommand(SubscriptionType type)
    {
      using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoRemoteSubscriptionSubscribeId, 21, CommandStatusHandling.ThrowOnlySeverityError))
      {
        cargoCommandWriter.WriteByte((byte) type);
        cargoCommandWriter.WriteBool32(false);
        cargoCommandWriter.WriteGuid(BandStoreClient.PushServiceGuid);
      }
    }

    protected override void ExecuteSensorUnsubscribeCommand(SubscriptionType type)
    {
      int dataSize = 17;
      using (CargoCommandWriter cargoCommandWriter = this.ProtocolBeginWrite(DeviceCommands.CargoRemoteSubscriptionUnsubscribeId, dataSize, CommandStatusHandling.ThrowOnlySeverityError))
      {
        cargoCommandWriter.WriteByte((byte) type);
        cargoCommandWriter.WriteGuid(BandStoreClient.PushServiceGuid);
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    protected virtual void StreamBandData(ManualResetEvent started, CancellationToken stop)
    {
      this.loggerProvider.Log(ProviderLogLevel.Info, "Push service streaming task starting...");
      WaitHandle[] waitHandles = (WaitHandle[]) null;
      bool flag = true;
      int num1 = 0;
      while (!stop.IsCancellationRequested)
      {
        if (!flag)
        {
          try
          {
            this.pushServiceTransport.Disconnect();
          }
          catch
          {
          }
          if (waitHandles == null)
            waitHandles = new WaitHandle[2]
            {
              (WaitHandle) this.StreamingTaskAwake,
              stop.WaitHandle
            };
          this.StreamingTaskAwake.Reset();
          int num2;
          if (num1 == 0)
          {
            ++num1;
            num2 = WaitHandle.WaitAny(waitHandles, BandStoreClient.PushServiceReconnectDelay);
          }
          else
            num2 = WaitHandle.WaitAny(waitHandles);
          if (num2 == 1)
            break;
        }
        try
        {
          this.pushServiceTransport.Connect();
          this.pushServiceTransport.CargoStream.Cancel = stop;
          if (!stop.IsCancellationRequested)
          {
            if (!flag)
            {
              HashSet<byte> subscribedSensorTypes = this.SubscribedSensorTypes;
              bool lockTaken = false;
              try
              {
                Monitor.Enter((object) subscribedSensorTypes, ref lockTaken);
                if (!stop.IsCancellationRequested)
                {
                  foreach (SubscriptionType subscribedSensorType in this.SubscribedSensorTypes)
                  {
                    this.ExecuteSensorSubscribeCommand(subscribedSensorType);
                    if (stop.IsCancellationRequested)
                      break;
                  }
                }
                else
                  break;
              }
              finally
              {
                if (lockTaken)
                  Monitor.Exit((object) subscribedSensorTypes);
              }
            }
          }
          else
            break;
        }
        catch (Exception ex)
        {
          this.loggerProvider.LogException(ProviderLogLevel.Error, ex);
          continue;
        }
        finally
        {
          if (flag)
          {
            flag = false;
            started.Set();
          }
        }
        num1 = 0;
        try
        {
          while (!stop.IsCancellationRequested)
            this.ProcessPushServicePacket(this.pushServiceTransport.CargoReader);
        }
        catch (OperationCanceledException ex)
        {
          break;
        }
        catch (Exception ex)
        {
          this.loggerProvider.LogException(ProviderLogLevel.Error, ex);
        }
      }
      try
      {
        this.pushServiceTransport.Disconnect();
      }
      catch
      {
      }
    }

    private void ProcessPushServicePacket(CargoStreamReader reader)
    {
      int num = 0;
      PushServicePacketHeader servicePacketHeader = PushServicePacketHeader.DeserializeFromBand((ICargoReader) reader);
      switch (servicePacketHeader.Type)
      {
        case PushServicePacketType.RemoteSubscription:
          while (num < servicePacketHeader.Length)
            num += this.ProcessSensorSubscriptionPayload((ICargoReader) reader);
          break;
        case PushServicePacketType.StrappEvent:
          while (num < servicePacketHeader.Length)
            num += this.ProcessTileEventPayload((ICargoReader) reader);
          break;
        default:
          this.loggerProvider.Log(ProviderLogLevel.Warning, "Unsupported push service type {0} received.", (object) servicePacketHeader.Type.ToString());
          this.pushServiceTransport.CargoReader.ReadExactAndDiscard(servicePacketHeader.Length);
          break;
      }
    }

    private sealed class ApplicationSuspensionManager
    {
      public static readonly BandStoreClient.ApplicationSuspensionManager Instance = new BandStoreClient.ApplicationSuspensionManager();

      [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
      public ApplicationSuspensionManager()
      {
        try
        {
          Application current = Application.Current;
          // ISSUE: method pointer
          WindowsRuntimeMarshal.AddEventHandler<SuspendingEventHandler>((Func<M0, EventRegistrationToken>) new Func<SuspendingEventHandler, EventRegistrationToken>(current.add_Suspending), new Action<EventRegistrationToken>(current.remove_Suspending), (M0) new SuspendingEventHandler((object) this, __methodptr(OnCurrentApplicationSuspending)));
        }
        catch
        {
        }
      }

      public event EventHandler Suspending;

      private void OnCurrentApplicationSuspending(object sender, SuspendingEventArgs e) => this.NotifySuspending();

      private void NotifySuspending()
      {
        EventHandler suspending = this.Suspending;
        if (suspending == null)
          return;
        suspending((object) this, EventArgs.Empty);
      }
    }
  }
}
