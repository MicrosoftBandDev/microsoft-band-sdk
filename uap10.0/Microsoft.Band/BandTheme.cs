// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandTheme
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band
{
  public sealed class BandTheme
  {
    internal const ushort ColorCount = 6;

    public BandColor Base { get; set; }

    public BandColor Highlight { get; set; }

    public BandColor Lowlight { get; set; }

    public BandColor SecondaryText { get; set; }

    public BandColor HighContrast { get; set; }

    public BandColor Muted { get; set; }
  }
}
