// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Phone.PushServiceTransport
// Assembly: Microsoft.Band.Phone_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 29D5BD9B-4535-4F2F-BDC5-1BF47D7C3CF4
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Phone_UAP.dll

using Microsoft.Band.Store;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Foundation;

namespace Microsoft.Band.Phone
{
  internal sealed class PushServiceTransport : 
    BluetoothTransportBase,
    IReadableTransport,
    IDisposable
  {
    private readonly BluetoothDeviceInfo associatedBand;

    public PushServiceTransport(BluetoothDeviceInfo deviceInfo, ILoggerProvider loggerProvider)
      : base(loggerProvider)
    {
      this.associatedBand = deviceInfo;
    }

    public void Connect(ushort maxConnectAttempts = 1)
    {
      Guid guid = new Guid("{C742E1A2-6320-5ABC-9643-D206C677E580}");
      foreach (RfcommDeviceService rfcommService in (IEnumerable<RfcommDeviceService>) ((Task<RfcommDeviceService>) WindowsRuntimeSystemExtensions.AsTask<RfcommDeviceService>((IAsyncOperation<M0>) RfcommDeviceService.FromIdAsync(this.associatedBand.Peer.Id))).Result.Device.RfcommServices)
      {
        if (rfcommService.ServiceId.Uuid == guid)
        {
          this.Connect(rfcommService, maxConnectAttempts);
          this.CargoStream.ReadTimeout = int.MaxValue;
          return;
        }
      }
      throw new BandIOException(StoreResources.PushServiceNotFound);
    }
  }
}
