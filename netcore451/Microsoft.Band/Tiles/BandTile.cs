// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.BandTile
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Band.Tiles
{
  public sealed class BandTile
  {
    internal const short NameMaxLength = 21;
    internal const short MaxIcons = 10;
    internal const short MaxLayouts = 5;
    internal const short MaxPages = 8;
    internal const int MaxBinaryLayoutSize = 768;
    private readonly IList<BandIcon> additionalIcons = (IList<BandIcon>) new List<BandIcon>();
    private string name;
    private BandIcon tileIcon;
    private BandIcon smallIcon;
    private readonly Guid tileId;

    public BandTile(Guid tileId)
    {
      this.tileId = !(tileId == Guid.Empty) ? tileId : throw new ArgumentException(BandResources.BandTileEmptyTileId, nameof (tileId));
      this.IsBadgingEnabled = true;
      this.PageLayouts = (IList<PageLayout>) new List<PageLayout>();
    }

    public Guid TileId => this.tileId;

    public string Name
    {
      get => this.name;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new ArgumentNullException(nameof (value));
        this.name = value.Length <= 21 ? value : throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, BandResources.BandTileNameLengthExceeded, (object) (ushort) 21), nameof (value));
      }
    }

    public BandIcon TileIcon
    {
      get => this.tileIcon;
      set => this.tileIcon = value != null ? value : throw new ArgumentNullException(nameof (value));
    }

    public BandIcon SmallIcon
    {
      get => this.smallIcon;
      set => this.smallIcon = value != null ? value : throw new ArgumentNullException(nameof (value));
    }

    public BandTheme Theme { get; set; }

    public IList<PageLayout> PageLayouts { get; private set; }

    public bool IsBadgingEnabled { get; set; }

    public IList<BandIcon> AdditionalIcons => this.additionalIcons;

    public bool IsScreenTimeoutDisabled { get; set; }
  }
}
