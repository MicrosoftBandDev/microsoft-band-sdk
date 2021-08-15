// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.WrappedTextBlock
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class WrappedTextBlock : PageElement
  {
    private const int serializedCustomByteCount = 2;

    public WrappedTextBlock()
      : base(CommonElementColors.White)
    {
      this.AutoHeight = true;
    }

    public WrappedTextBlockFont Font { get; set; }

    public bool AutoHeight { get; set; }

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

    internal override PageElementType TypeId => PageElementType.WrappableText;

    protected override int SerializedCustomByteCount => 2;

    protected override uint AttributesToCustomStyleMask() => !this.AutoHeight ? 0U : 8192U;

    protected override void CustomStyleMaskToAttributes(uint mask) => this.AutoHeight = ((WrappedTextBlock.TextStyleMask) mask).HasFlag((Enum) WrappedTextBlock.TextStyleMask.AutoResize);

    internal override void SerializeCustomFieldsToBand(ICargoWriter writer) => writer.WriteUInt16((ushort) this.Font);

    internal override void DeserializeCustomFieldsFromBand(ICargoReader reader) => this.Font = (WrappedTextBlockFont) reader.ReadUInt16();

    [Flags]
    private enum TextStyleMask : uint
    {
      None = 0,
      AutoResize = 8192, // 0x00002000
    }
  }
}
