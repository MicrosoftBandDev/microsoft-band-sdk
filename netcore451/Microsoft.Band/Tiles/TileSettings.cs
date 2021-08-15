// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.TileSettings
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Tiles
{
  [Flags]
  internal enum TileSettings : ushort
  {
    None = 0,
    EnableNotification = 1,
    EnableBadging = 2,
    UseCustomColorForTile = 4,
    EnableAutoUpdate = 8,
    ScreenTimeout30Seconds = 16, // 0x0010
    ScreenTimeoutDisabled = 32, // 0x0020
  }
}
