// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.BandTileClosedEvent
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;

namespace Microsoft.Band.Tiles
{
  internal class BandTileClosedEvent : BandTileEventBase, IBandTileClosedEvent, IBandTileEvent
  {
    public BandTileClosedEvent(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    internal override void Dispatch(BandClient client) => client.DispatchTileClosedEvent(this);

    public BandTileClosedEvent(IDictionary<string, object> valueSet)
      : base(valueSet)
    {
    }

    public override void EncodeToDictionary(IDictionary<string, object> valueSet)
    {
      valueSet["Type"] = (object) "TileClosedEvent";
      base.EncodeToDictionary(valueSet);
    }
  }
}
