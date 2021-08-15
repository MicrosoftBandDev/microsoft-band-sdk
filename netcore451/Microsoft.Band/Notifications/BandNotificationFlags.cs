// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Notifications.BandNotificationFlags
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Notifications
{
  [Flags]
  internal enum BandNotificationFlags : byte
  {
    UnmodifiedNotificationSettings = 0,
    ForceNotificationDialog = 1,
    SuppressNotificationDialog = 2,
    SuppressSmsReply = 4,
    MaxValue = SuppressSmsReply | SuppressNotificationDialog | ForceNotificationDialog, // 0x07
  }
}
