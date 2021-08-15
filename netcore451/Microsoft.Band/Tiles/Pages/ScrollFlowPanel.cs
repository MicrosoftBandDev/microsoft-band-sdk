// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.ScrollFlowPanel
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System.Collections.Generic;

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class ScrollFlowPanel : FlowPanel
  {
    public ScrollFlowPanel(params PageElement[] elements)
      : base(CommonElementColors.White, (IEnumerable<PageElement>) elements)
    {
    }

    public ScrollFlowPanel(IEnumerable<PageElement> elements)
      : base(CommonElementColors.White, elements)
    {
    }

    public ElementColorSource ScrollBarColorSource
    {
      get => this.ColorSource;
      set => this.ColorSource = value;
    }

    public BandColor ScrollBarColor
    {
      get => this.Color;
      set => this.Color = value;
    }

    internal override PageElementType TypeId => PageElementType.ScrollFlowList;
  }
}
