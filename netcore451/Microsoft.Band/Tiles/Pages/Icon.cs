// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.Icon
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class Icon : PageElement
  {
    public Icon()
      : base(CommonElementColors.White)
    {
    }

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

    internal override PageElementType TypeId => PageElementType.Icon;
  }
}
