// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.BandTileEventBase
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;

namespace Microsoft.Band.Tiles
{
  internal abstract class BandTileEventBase : IBandTileEvent
  {
    private static readonly int serializedByteCountMinusContext = 102;
    private static readonly int serializedByteCount = 4 + BandTileEventBase.serializedByteCountMinusContext;
    protected const string TypeKey = "Type";
    protected const string TileOpenedTypeName = "TileOpenedEvent";
    protected const string TileButtonPressedTypeName = "TileButtonPressedEvent";
    protected const string TileClosedTypeName = "TileClosedEvent";
    protected const string TileIdName = "TileId";
    protected const string TimestampName = "Timestamp";

    protected BandTileEventBase(DateTimeOffset timestamp) => this.Timestamp = timestamp;

    public Guid TileId { get; protected set; }

    public DateTimeOffset Timestamp { get; private set; }

    internal abstract void Dispatch(BandClient client);

    internal static int GetSerializedByteCount() => BandTileEventBase.serializedByteCount;

    internal static BandTileEventBase DeserializeFromBand(
      ICargoReader reader,
      DateTimeOffset timestamp,
      out byte[] tileFriendlyName)
    {
      BandTileEventBase emptyTileEvent = BandTileEventBase.GetEmptyTileEvent(reader, timestamp);
      if (emptyTileEvent != null)
      {
        emptyTileEvent.DeserializeFromBand(reader, out tileFriendlyName);
      }
      else
      {
        tileFriendlyName = (byte[]) null;
        reader.ReadExactAndDiscard(BandTileEventBase.serializedByteCountMinusContext);
      }
      return emptyTileEvent;
    }

    private static BandTileEventBase GetEmptyTileEvent(
      ICargoReader reader,
      DateTimeOffset timestamp)
    {
      switch (reader.ReadUInt32())
      {
        case 0:
          return (BandTileEventBase) new BandTileOpenedEvent(timestamp);
        case 1:
          return (BandTileEventBase) new BandTileButtonPressedEvent(timestamp);
        case 2:
          return (BandTileEventBase) new BandTileClosedEvent(timestamp);
        default:
          return (BandTileEventBase) null;
      }
    }

    private void DeserializeFromBand(ICargoReader reader, out byte[] strappFriendlyName)
    {
      this.DeserializeDataFromBand(reader);
      strappFriendlyName = new byte[60];
      reader.Read(strappFriendlyName);
      reader.ReadExactAndDiscard(8);
    }

    protected virtual void DeserializeDataFromBand(ICargoReader reader)
    {
      this.TileId = reader.ReadGuid();
      reader.ReadExactAndDiscard(18);
    }

    public static BandTileEventBase ConstructFromDictionary(
      IDictionary<string, object> valueSet)
    {
      object obj;
      valueSet.TryGetValue("Type", out obj);
      string str = obj as string;
      if (str == "TileOpenedEvent")
        return (BandTileEventBase) new BandTileOpenedEvent(valueSet);
      if (str == "TileButtonPressedEvent")
        return (BandTileEventBase) new BandTileButtonPressedEvent(valueSet);
      return str == "TileClosedEvent" ? (BandTileEventBase) new BandTileClosedEvent(valueSet) : (BandTileEventBase) null;
    }

    public BandTileEventBase(IDictionary<string, object> valueSet)
    {
      this.TileId = (Guid) valueSet[nameof (TileId)];
      this.Timestamp = (DateTimeOffset) valueSet[nameof (Timestamp)];
    }

    public virtual void EncodeToDictionary(IDictionary<string, object> valueSet)
    {
      valueSet["TileId"] = (object) this.TileId;
      valueSet["Timestamp"] = (object) this.Timestamp;
    }
  }
}
