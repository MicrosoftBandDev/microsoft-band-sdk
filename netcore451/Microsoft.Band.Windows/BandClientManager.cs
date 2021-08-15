// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandClientManager
// Assembly: Microsoft.Band.Windows, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 28C7C615-6BA3-4124-96FB-B8960DA222E2
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.Windows.dll

using Microsoft.Band.Store;
using Microsoft.Band.Windows;
using System;
using System.Threading.Tasks;

namespace Microsoft.Band
{
  public class BandClientManager : IBandClientManager
  {
    private static readonly BandClientManager instance = new BandClientManager();
    private const int MaximumBluetoothConnectAttempts = 3;

    private BandClientManager()
    {
    }

    public static IBandClientManager Instance => (IBandClientManager) BandClientManager.instance;

    public Task<IBandInfo[]> GetBandsAsync() => this.GetBandsAsync(false);

    public Task<IBandInfo[]> GetBandsAsync(bool isBackground)
    {
      Guid serviceGuid = new Guid(isBackground ? "{A502CA9B-2BA5-413C-A4E0-13804E47B38F}" : "{A502CA9A-2BA5-413C-A4E0-13804E47B38F}");
      return Task.Run<IBandInfo[]>((Func<IBandInfo[]>) (() => (IBandInfo[]) BluetoothTransport.GetConnectedDevices(serviceGuid, (ILoggerProvider) new LoggerProviderStub())));
    }

    public async Task<IBandClient> ConnectAsync(IBandInfo bandInfo)
    {
      if (bandInfo == null)
        throw new ArgumentNullException(nameof (bandInfo));
      if (!(bandInfo is BluetoothDeviceInfo bluetoothDeviceInfo))
        throw new ArgumentException(StoreResources.DeviceInfoNotBluetooth);
      BluetoothTransport deviceTransport = (BluetoothTransport) null;
      PushServiceTransport pushServiceTransport = (PushServiceTransport) null;
      BandStoreClient client = (BandStoreClient) null;
      try
      {
        LoggerProviderStub loggerProvider = new LoggerProviderStub();
        deviceTransport = await BluetoothTransport.CreateAsync(bandInfo, (ILoggerProvider) loggerProvider, (ushort) 3).ConfigureAwait(false);
        pushServiceTransport = new PushServiceTransport(bluetoothDeviceInfo, (ILoggerProvider) loggerProvider);
        client = new BandStoreClient(bluetoothDeviceInfo, (IDeviceTransport) deviceTransport, (IReadableTransport) pushServiceTransport, (ILoggerProvider) loggerProvider, StoreApplicationPlatformProvider.Current);
        client.InitializeCachedProperties();
        client.CheckFirmwareSdkBit(FirmwareSdkCheckPlatform.Windows, (byte) 0);
        loggerProvider.Log(ProviderLogLevel.Info, "Created BandClient(IBandInfo bandinfo)", new object[0]);
        loggerProvider = (LoggerProviderStub) null;
      }
      catch
      {
        if (client != null)
        {
          client.Dispose();
        }
        else
        {
          deviceTransport?.Dispose();
          pushServiceTransport?.Dispose();
        }
        throw;
      }
      return (IBandClient) client;
    }
  }
}
