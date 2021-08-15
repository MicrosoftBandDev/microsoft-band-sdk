// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandTypeConstants
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Band
{
  internal class BandTypeConstants
  {
    internal static readonly BandTypeConstants Cargo = new BandTypeConstants(BandType.Cargo);
    internal static readonly BandTypeConstants Envoy = new BandTypeConstants(BandType.Envoy);

    protected BandTypeConstants(BandType bandType) => this.BandType = bandType;

    public BandType BandType { get; private set; }

    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
    public ushort MeTileWidth => 310;

    public ushort MeTileHeight
    {
      get
      {
        switch (this.BandType)
        {
          case BandType.Cargo:
            return 102;
          case BandType.Envoy:
            return 128;
          default:
            throw new InvalidOperationException();
        }
      }
    }

    public ushort TileIconPreferredSize
    {
      get
      {
        switch (this.BandType)
        {
          case BandType.Cargo:
            return 46;
          case BandType.Envoy:
            return 48;
          default:
            throw new InvalidOperationException();
        }
      }
    }

    public ushort BadgeIconPreferredSize
    {
      get
      {
        switch (this.BandType)
        {
          case BandType.Cargo:
            return 24;
          case BandType.Envoy:
            return 24;
          default:
            throw new InvalidOperationException();
        }
      }
    }

    public ushort NotificiationIconPreferredSize
    {
      get
      {
        switch (this.BandType)
        {
          case BandType.Cargo:
            return 36;
          case BandType.Envoy:
            return 36;
          default:
            throw new InvalidOperationException();
        }
      }
    }

    public int MaxIconsPerTile
    {
      get
      {
        switch (this.BandType)
        {
          case BandType.Cargo:
            return 10;
          case BandType.Envoy:
            return 15;
          default:
            throw new InvalidOperationException();
        }
      }
    }

    internal T GetBandSpecificValue<T>(T cargo, T envoy)
    {
      switch (this.BandType)
      {
        case BandType.Cargo:
          return cargo;
        case BandType.Envoy:
          return envoy;
        default:
          throw new InvalidOperationException();
      }
    }
  }
}
