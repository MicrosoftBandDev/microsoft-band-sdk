// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.IconData
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class IconData : PageElementData
  {
    public IconData(short elementId, ushort iconIndex)
      : base(PageElementType.Icon, elementId)
    {
      this.IconIndex = iconIndex;
    }

    public ushort IconIndex { get; set; }

    internal override int GetSerializedLength() => base.GetSerializedLength() + 2;

    internal override void SerializeToBand(ICargoWriter writer)
    {
      base.SerializeToBand(writer);
      writer.WriteUInt16(this.IconIndex);
    }
  }
}
