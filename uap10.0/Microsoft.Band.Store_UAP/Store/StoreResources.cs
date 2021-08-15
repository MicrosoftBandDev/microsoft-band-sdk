// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Store.StoreResources
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

namespace Microsoft.Band.Store
{
  internal static class StoreResources
  {
    public static string DeviceInfoNotBluetooth => "BandInfo must be for a Bluetooth connection.";

    public static string DeviceInfoNotUsb => "BandInfo must be for a USB connection.";

    public static string PushServiceNotFound => "Band does not support push service.";

    public static string RfComm_FromId_ReturnedNull => "A non-specific error occurred while attempting to acquire the Bluetooth device service. This error can occur if the application manifest does not have the required permissions for opening the Bluetooth connection to the Microsoft Band, or if the user denies access.";

    public static string RfComm_FromId_Threw => "An error occurred while attempting to acquire the Bluetooth device service. This error can occur if the paired device is unreachable or has become unpaired from the current host.";
  }
}
