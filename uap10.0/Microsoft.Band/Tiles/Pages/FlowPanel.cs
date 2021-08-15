// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.FlowPanel
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;

namespace Microsoft.Band.Tiles.Pages
{
  public class FlowPanel : PagePanel
  {
    public FlowPanel(params PageElement[] elements)
      : this(CommonElementColors.Black, (IEnumerable<PageElement>) elements)
    {
    }

    public FlowPanel(IEnumerable<PageElement> elements)
      : this(CommonElementColors.Black, elements)
    {
    }

    protected FlowPanel(BandColor color, IEnumerable<PageElement> elements)
      : base(color, elements)
    {
    }

    public FlowPanelOrientation Orientation { get; set; }

    internal override PageElementType TypeId => PageElementType.FlowList;

    protected override uint AttributesToCustomStyleMask() => this.Orientation != FlowPanelOrientation.Horizontal ? 4096U : 2048U;

    protected override void CustomStyleMaskToAttributes(uint mask) => this.Orientation = ((FlowPanelOrientation) mask).HasFlag((Enum) FlowPanelOrientation.Vertical) ? FlowPanelOrientation.Vertical : FlowPanelOrientation.Horizontal;

    [Flags]
    private enum FlowlistStyleMask : uint
    {
      Horizontal = 2048, // 0x00000800
      Vertical = 4096, // 0x00001000
    }
  }
}
