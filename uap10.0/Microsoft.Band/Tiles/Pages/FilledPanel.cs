// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.FilledPanel
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System.Collections.Generic;

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class FilledPanel : PagePanel
  {
    public FilledPanel(params PageElement[] elements)
      : base(elements)
    {
    }

    public FilledPanel(IEnumerable<PageElement> elements)
      : base(elements)
    {
    }

    public ElementColorSource BackgroundColorSource
    {
      get => this.ColorSource;
      set => this.ColorSource = value;
    }

    public BandColor BackgroundColor
    {
      get => this.Color;
      set => this.Color = value;
    }

    internal override PageElementType TypeId => PageElementType.FilledQuad;
  }
}
