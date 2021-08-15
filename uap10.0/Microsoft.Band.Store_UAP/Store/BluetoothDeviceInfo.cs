// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Store.BluetoothDeviceInfo
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System;
using System.Security;
using Windows.Devices.Enumeration;

namespace Microsoft.Band.Store
{
  internal sealed class BluetoothDeviceInfo : IBandInfo
  {
    public string Name { get; private set; }

    public BandConnectionType ConnectionType => BandConnectionType.Bluetooth;

    public DeviceInformation Peer { [SecurityCritical] get; [SecurityCritical] private set; }

    [SecurityCritical]
    public BluetoothDeviceInfo(DeviceInformation peer)
    {
      this.Peer = peer != null ? peer : throw new ArgumentNullException(nameof (peer));
      this.Name = peer.Name;
    }
  }
}
