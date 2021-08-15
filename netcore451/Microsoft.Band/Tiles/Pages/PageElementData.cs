// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.PageElementData
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Tiles.Pages
{
  public abstract class PageElementData
  {
    private readonly PageElementType elementTypeId;

    internal PageElementData(PageElementType elementType, short elementId)
    {
      this.elementTypeId = elementType;
      this.ElementId = elementId;
    }

    public short ElementId { get; set; }

    internal virtual void Validate(BandTypeConstants constants)
    {
    }

    internal virtual int GetSerializedLength() => 4;

    internal virtual void SerializeToBand(ICargoWriter writer)
    {
      writer.WriteUInt16((ushort) this.elementTypeId);
      writer.WriteInt16(this.ElementId);
    }
  }
}
