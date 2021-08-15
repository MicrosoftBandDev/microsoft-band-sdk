// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.PageRect
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Tiles.Pages
{
  public struct PageRect
  {
    private short x;
    private short y;
    private short width;
    private short height;

    public PageRect(short x, short y, short width, short height)
    {
      this.x = x;
      this.y = y;
      this.width = width;
      this.height = height;
    }

    public short X => this.x;

    public short Y => this.y;

    public short Width => this.width;

    public short Height => this.height;

    internal void SerializeToBand(ICargoWriter writer)
    {
      writer.WriteInt16(this.X);
      writer.WriteInt16(this.Y);
      writer.WriteInt16((short) ((int) this.X + (int) this.Width));
      writer.WriteInt16((short) ((int) this.Y + (int) this.Height));
    }

    internal static PageRect DeserializeFromBand(ICargoReader reader)
    {
      PageRect pageRect = new PageRect(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
      pageRect.width -= pageRect.X;
      pageRect.height -= pageRect.Y;
      return pageRect;
    }
  }
}
