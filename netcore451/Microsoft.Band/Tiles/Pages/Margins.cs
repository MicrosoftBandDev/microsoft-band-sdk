// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.Margins
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Tiles.Pages
{
  public struct Margins
  {
    private readonly short left;
    private readonly short top;
    private readonly short right;
    private readonly short bottom;

    public Margins(short left, short top, short right, short bottom)
    {
      this.left = left;
      this.top = top;
      this.right = right;
      this.bottom = bottom;
    }

    public short Left => this.left;

    public short Top => this.top;

    public short Right => this.right;

    public short Bottom => this.bottom;

    internal void SerializeToBand(ICargoWriter writer)
    {
      writer.WriteInt16(this.Left);
      writer.WriteInt16(this.Top);
      writer.WriteInt16(this.Right);
      writer.WriteInt16(this.Bottom);
    }

    internal static Margins DeserializeFromBand(ICargoReader reader) => new Margins(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
  }
}
