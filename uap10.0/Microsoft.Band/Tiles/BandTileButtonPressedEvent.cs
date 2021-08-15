// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.BandTileButtonPressedEvent
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;

namespace Microsoft.Band.Tiles
{
  internal class BandTileButtonPressedEvent : 
    BandTileEventBase,
    IBandTileButtonPressedEvent,
    IBandTileEvent
  {
    private const string PageIdName = "PageId";
    private const string ElementIdName = "ElementId";

    internal BandTileButtonPressedEvent(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public Guid PageId { get; private set; }

    public ushort ElementId { get; private set; }

    internal override void Dispatch(BandClient client) => client.DispatchTileButtonPressedEvent(this);

    protected override void DeserializeDataFromBand(ICargoReader reader)
    {
      this.TileId = reader.ReadGuid();
      this.PageId = reader.ReadGuid();
      this.ElementId = reader.ReadUInt16();
    }

    public BandTileButtonPressedEvent(IDictionary<string, object> valueSet)
      : base(valueSet)
    {
      this.PageId = (Guid) valueSet[nameof (PageId)];
      this.ElementId = (ushort) valueSet[nameof (ElementId)];
    }

    public override void EncodeToDictionary(IDictionary<string, object> valueSet)
    {
      valueSet["Type"] = (object) "TileButtonPressedEvent";
      valueSet["PageId"] = (object) this.PageId;
      valueSet["ElementId"] = (object) this.ElementId;
      base.EncodeToDictionary(valueSet);
    }
  }
}
