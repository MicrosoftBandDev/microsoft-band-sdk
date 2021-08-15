// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.IconButtonData
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class IconButtonData : PageElementData
  {
    public IconButtonData(
      short elementId,
      ushort iconIndex,
      ushort pressedIconIndex,
      BandColor iconColor,
      BandColor pressedIconColor,
      BandColor backgroundColor,
      BandColor pressedBackgroundColor)
      : base(PageElementType.InteractiveButtonWithIcon, elementId)
    {
      this.IconIndex = iconIndex;
      this.PressedIconIndex = pressedIconIndex;
      this.IconColor = iconColor;
      this.PressedIconColor = pressedIconColor;
      this.BackgroundColor = backgroundColor;
      this.PressedBackgroundColor = pressedBackgroundColor;
    }

    public ushort IconIndex { get; set; }

    public ushort PressedIconIndex { get; set; }

    public BandColor IconColor { get; set; }

    public BandColor PressedIconColor { get; set; }

    public BandColor BackgroundColor { get; set; }

    public BandColor PressedBackgroundColor { get; set; }

    internal override int GetSerializedLength() => base.GetSerializedLength() + 2 + 2 + 2 + 2 + 2 + 4 + 4 + 4 + 4;

    internal override void SerializeToBand(ICargoWriter writer)
    {
      base.SerializeToBand(writer);
      writer.WriteUInt16(this.IconIndex);
      writer.WriteUInt16(this.PressedIconIndex);
      writer.WriteUInt16((ushort) 13001);
      writer.WriteUInt16((ushort) this.ElementId);
      writer.WriteUInt16((ushort) 4);
      writer.WriteUInt32(this.BackgroundColor.ToRgb((byte) 1));
      writer.WriteUInt32(this.PressedBackgroundColor.ToRgb((byte) 1));
      writer.WriteUInt32(this.IconColor.ToRgb((byte) 1));
      writer.WriteUInt32(this.PressedIconColor.ToRgb((byte) 1));
    }
  }
}
