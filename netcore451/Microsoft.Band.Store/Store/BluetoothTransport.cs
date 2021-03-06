// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Store.BluetoothTransport
// Assembly: Microsoft.Band.Store, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 91750BE8-70C6-4542-841C-664EE611AF0B
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.Store.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Foundation;

namespace Microsoft.Band.Store
{
  internal sealed class BluetoothTransport : 
    BluetoothTransportBase,
    IDeviceTransport,
    IReadableTransport,
    IDisposable
  {
    private CargoStreamWriter cargoWriter;

    private BluetoothTransport(RfcommDeviceService service, ILoggerProvider loggerProvider)
      : base(service, loggerProvider)
    {
    }

    public CargoStreamWriter CargoWriter
    {
      get
      {
        this.CheckIfDisposed();
        return this.cargoWriter;
      }
    }

    internal static BluetoothDeviceInfo[] GetConnectedDevices(
      Guid serviceUuid,
      ILoggerProvider loggerProvider)
    {
      DeviceInformationCollection results;
      try
      {
        int millisecondsTimeout = 5000;
        IAsyncOperation<DeviceInformationCollection> allAsync = DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.FromUuid(serviceUuid)));
        loggerProvider.Log(ProviderLogLevel.Info, "Bluetooth peer search started");
        if (!allAsync.AsTask<DeviceInformationCollection>().Wait(millisecondsTimeout))
        {
          loggerProvider.Log(ProviderLogLevel.Error, "Bluetooth peer search timed out");
          ((IAsyncInfo) allAsync).Cancel();
          throw new TimeoutException();
        }
        loggerProvider.Log(ProviderLogLevel.Info, "Bluetooth peer search completed, getting results");
        results = allAsync.GetResults();
        loggerProvider.Log(ProviderLogLevel.Info, "Bluetooth peer search results retrieved");
      }
      catch (Exception ex)
      {
        throw new BandIOException("Error getting list of Bluetooth peers.", ex);
      }
      BluetoothDeviceInfo[] bluetoothDeviceInfoArray = new BluetoothDeviceInfo[((IReadOnlyCollection<DeviceInformation>) results).Count];
      loggerProvider.Log(ProviderLogLevel.Info, "Bluetooth peers found: {0}", (object) ((IReadOnlyCollection<DeviceInformation>) results).Count);
      for (int index = 0; index < ((IReadOnlyCollection<DeviceInformation>) results).Count; ++index)
      {
        DeviceInformation peer = ((IReadOnlyList<DeviceInformation>) results)[index];
        loggerProvider.Log(ProviderLogLevel.Info, "  Peer: Name: {0}, Id: {1}", (object) peer.Name, (object) peer.Id);
        bluetoothDeviceInfoArray[index] = new BluetoothDeviceInfo(peer);
      }
      return bluetoothDeviceInfoArray;
    }

    internal static BluetoothTransport Create(
      IBandInfo deviceInfo,
      ILoggerProvider loggerProvider,
      ushort maxConnectAttempts = 1)
    {
      BluetoothDeviceInfo correctDeviceInfo = BluetoothTransport.GetCorrectDeviceInfo(deviceInfo);
      RfcommDeviceService result;
      try
      {
        loggerProvider.Log(ProviderLogLevel.Info, "Start getting RfcommDeviceService from Id.");
        result = RfcommDeviceService.FromIdAsync(correctDeviceInfo.Peer.Id).AsTask<RfcommDeviceService>().Result;
        loggerProvider.Log(ProviderLogLevel.Info, "Finish getting RfcommDeviceService from Id.");
      }
      catch (Exception ex)
      {
        throw new BandIOException(StoreResources.RfComm_FromId_Threw, ex);
      }
      if (result == null)
        throw new BandAccessDeniedException(StoreResources.RfComm_FromId_ReturnedNull);
      return BluetoothTransport.GetTransport(result, loggerProvider, maxConnectAttempts);
    }

    internal static async Task<BluetoothTransport> CreateAsync(
      IBandInfo deviceInfo,
      ILoggerProvider loggerProvider,
      ushort maxConnectAttempts = 1)
    {
      RfcommDeviceService service = (RfcommDeviceService) null;
      BluetoothDeviceInfo correctDeviceInfo = BluetoothTransport.GetCorrectDeviceInfo(deviceInfo);
      try
      {
        RfcommDeviceService rfcommDeviceService = service;
        service = await RfcommDeviceService.FromIdAsync(correctDeviceInfo.Peer.Id);
      }
      catch (Exception ex)
      {
        throw new BandIOException(StoreResources.RfComm_FromId_Threw, ex);
      }
      if (service == null)
        throw new BandAccessDeniedException(StoreResources.RfComm_FromId_ReturnedNull);
      return await Task.Run<BluetoothTransport>((Func<BluetoothTransport>) (() => BluetoothTransport.GetTransport(service, loggerProvider, maxConnectAttempts)));
    }

    private static BluetoothDeviceInfo GetCorrectDeviceInfo(IBandInfo deviceInfo)
    {
      if (deviceInfo == null)
        throw new ArgumentNullException(nameof (deviceInfo));
      return deviceInfo is BluetoothDeviceInfo bluetoothDeviceInfo ? bluetoothDeviceInfo : throw new ArgumentException(StoreResources.DeviceInfoNotBluetooth);
    }

    private static BluetoothTransport GetTransport(
      RfcommDeviceService service,
      ILoggerProvider loggerProvider,
      ushort maxConnectAttempts)
    {
      BluetoothTransport bluetoothTransport = new BluetoothTransport(service, loggerProvider);
      bluetoothTransport.Connect(maxConnectAttempts);
      return bluetoothTransport;
    }

    public void Connect(ushort maxConnectAttempts = 1)
    {
      this.Connect(this.rfcommService, maxConnectAttempts);
      this.cargoWriter = new CargoStreamWriter(this.CargoStream, this.loggerProvider, BufferServer.Pool_8192);
    }

    public void WriteCommandPacket(
      ushort commandId,
      uint argBufSize,
      uint dataStageSize,
      Action<ICargoWriter> writeArgBuf,
      bool flush)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnected();
      this.CargoWriter.WriteCommandPacket(commandId, argBufSize, dataStageSize, writeArgBuf, true, flush);
    }

    public override void Disconnect()
    {
      if (this.IsConnected)
      {
        base.Disconnect();
        CargoStreamWriter cargoWriter = this.cargoWriter;
        if (cargoWriter != null)
        {
          cargoWriter.Dispose();
          this.cargoWriter = (CargoStreamWriter) null;
        }
      }
      this.RaiseDisconnectedEvent(new TransportDisconnectedEventArgs(TransportDisconnectedReason.DisconnectCall));
    }
  }
}
