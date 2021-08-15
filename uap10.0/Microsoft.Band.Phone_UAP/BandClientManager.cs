// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandClientManager
// Assembly: Microsoft.Band.Phone_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 29D5BD9B-4535-4F2F-BDC5-1BF47D7C3CF4
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Phone_UAP.dll

using Microsoft.Band.Phone;
using Microsoft.Band.Store;
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
        client.CheckFirmwareSdkBit(FirmwareSdkCheckPlatform.WindowsPhone, (byte) 0);
        loggerProvider.Log(ProviderLogLevel.Info, "Created BandClient(IBandInfo bandinfo)", Array.Empty<object>());
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
