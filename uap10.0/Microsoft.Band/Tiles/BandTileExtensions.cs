// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.BandTileExtensions
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Tiles
{
  internal static class BandTileExtensions
  {
    public static TileData ToTileData(
      this BandTile tile,
      int startStripOrder,
      Guid applicationId)
    {
      TileSettings tileSettings1 = TileSettings.EnableNotification;
      if (tile.IsBadgingEnabled)
        tileSettings1 |= TileSettings.EnableBadging;
      if (tile.IsScreenTimeoutDisabled)
        tileSettings1 |= TileSettings.ScreenTimeoutDisabled;
      TileSettings tileSettings2 = tileSettings1 | TileSettings.EnableAutoUpdate;
      TileData tileData = new TileData();
      tileData.AppID = tile.TileId;
      tileData.StartStripOrder = (uint) startStripOrder;
      tileData.ThemeColor = 0U;
      tileData.SettingsMask = tileSettings2;
      tileData.SetNameAndOwnerId(tile.Name, applicationId);
      return tileData;
    }
  }
}
