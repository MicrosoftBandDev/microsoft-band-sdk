// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.TileData
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Text;

namespace Microsoft.Band.Tiles
{
  internal class TileData
  {
    private Guid appID;
    private const int serializedByteCount = 88;

    public Guid AppID
    {
      get => this.appID;
      set => this.appID = value;
    }

    public uint StartStripOrder { get; set; }

    public uint ThemeColor { get; set; }

    public ushort FriendlyNameLength { get; set; }

    public TileSettings SettingsMask { get; set; }

    public byte[] NameAndOwnerId { get; set; }

    public string FriendlyName => this.NameAndOwnerId != null && this.FriendlyNameLength > (ushort) 0 && this.NameAndOwnerId.Length >= (int) this.FriendlyNameLength * 2 ? Encoding.Unicode.GetString(this.NameAndOwnerId, 0, (int) this.FriendlyNameLength * 2) : string.Empty;

    public Guid OwnerId => BandClient.GetApplicationIdFromName(this.NameAndOwnerId, this.FriendlyNameLength);

    public BandIcon Icon { get; set; }

    public void SetNameAndOwnerId(string name, Guid ownerId)
    {
      if (name != null && name.Length > 29)
        throw new ArgumentException(string.Format(BandResources.GenericLengthExceeded, (object) nameof (name)));
      if (name != null && name.Length > 21 && ownerId != Guid.Empty)
        throw new ArgumentException(BandResources.BandTileOwnedTileNameExceedsLength, nameof (name));
      byte[] numArray = new byte[60];
      if (ownerId != Guid.Empty)
        BandBitConverter.GetBytes(ownerId, numArray, numArray.Length - 16);
      if (!string.IsNullOrEmpty(name))
      {
        int danglingHighSurrogate = Encoding.Unicode.GetBytesTrimDanglingHighSurrogate(name, name.Length, numArray, 0);
        this.NameAndOwnerId = numArray;
        this.FriendlyNameLength = (ushort) (danglingHighSurrogate / 2);
      }
      else
      {
        this.NameAndOwnerId = numArray;
        this.FriendlyNameLength = (ushort) 0;
      }
    }

    public static int GetSerializedByteCount() => 88;

    public void SerializeToBand(ICargoWriter writer, uint? order = null)
    {
      writer.WriteGuid(this.appID);
      writer.WriteUInt32(order.HasValue ? order.Value : this.StartStripOrder);
      writer.WriteUInt32(this.ThemeColor);
      writer.WriteUInt16(this.FriendlyNameLength);
      writer.WriteUInt16((ushort) this.SettingsMask);
      writer.Write(this.NameAndOwnerId, 0, 60);
    }

    public static TileData DeserializeFromBand(ICargoReader reader) => new TileData()
    {
      AppID = reader.ReadGuid(),
      StartStripOrder = reader.ReadUInt32(),
      ThemeColor = reader.ReadUInt32(),
      FriendlyNameLength = reader.ReadUInt16(),
      SettingsMask = (TileSettings) reader.ReadUInt16(),
      NameAndOwnerId = reader.ReadExact(60)
    };
  }
}
