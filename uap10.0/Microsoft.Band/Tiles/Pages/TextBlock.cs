// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.TextBlock
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class TextBlock : PageElement
  {
    private const int serializedCustomByteCount = 4;

    public TextBlock()
      : base(CommonElementColors.White)
    {
      this.AutoWidth = true;
    }

    public TextBlockFont Font { get; set; }

    public short Baseline { get; set; }

    public TextBlockBaselineAlignment BaselineAlignment { get; set; }

    public bool AutoWidth { get; set; }

    public new ElementColorSource ColorSource
    {
      get => base.ColorSource;
      set => base.ColorSource = value;
    }

    public new BandColor Color
    {
      get => base.Color;
      set => base.Color = value;
    }

    internal override PageElementType TypeId => PageElementType.Text;

    protected override int SerializedCustomByteCount => 4;

    protected override uint AttributesToCustomStyleMask() => this.AutoWidthToTextStyleMask() | this.BaselineAlignmentToTextStyleMask();

    private uint AutoWidthToTextStyleMask() => !this.AutoWidth ? 0U : 16384U;

    private uint BaselineAlignmentToTextStyleMask()
    {
      switch (this.BaselineAlignment)
      {
        case TextBlockBaselineAlignment.Absolute:
          return 2048;
        case TextBlockBaselineAlignment.Relative:
          return 4096;
        default:
          return 0;
      }
    }

    protected override void CustomStyleMaskToAttributes(uint mask)
    {
      this.TextStyleMaskToAutoWidth(mask);
      this.TextStyleMaskToBaselineAlignment(mask);
    }

    private void TextStyleMaskToAutoWidth(uint mask) => this.AutoWidth = ((TextBlock.TextStyleMask) mask).HasFlag((Enum) TextBlock.TextStyleMask.AutoResizeWidth);

    private void TextStyleMaskToBaselineAlignment(uint mask)
    {
      if (((TextBlock.TextStyleMask) mask).HasFlag((Enum) TextBlock.TextStyleMask.VerticalBaselineAbsolute))
        this.BaselineAlignment = TextBlockBaselineAlignment.Absolute;
      else if (((TextBlock.TextStyleMask) mask).HasFlag((Enum) TextBlock.TextStyleMask.VerticalBaselineRelative))
        this.BaselineAlignment = TextBlockBaselineAlignment.Relative;
      else
        this.BaselineAlignment = TextBlockBaselineAlignment.Automatic;
    }

    internal override void SerializeCustomFieldsToBand(ICargoWriter writer)
    {
      writer.WriteUInt16((ushort) this.Font);
      writer.WriteInt16(this.Baseline);
    }

    internal override void DeserializeCustomFieldsFromBand(ICargoReader reader)
    {
      this.Font = (TextBlockFont) reader.ReadUInt16();
      this.Baseline = reader.ReadInt16();
    }

    [Flags]
    private enum TextStyleMask : uint
    {
      None = 0,
      VerticalBaselineAbsolute = 2048, // 0x00000800
      VerticalBaselineRelative = 4096, // 0x00001000
      AutoResizeWidth = 16384, // 0x00004000
    }
  }
}
