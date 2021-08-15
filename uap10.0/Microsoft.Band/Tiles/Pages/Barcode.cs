// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.Barcode
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class Barcode : PageElement
  {
    public Barcode(BarcodeType barcodeType) => this.BarcodeType = barcodeType;

    public BarcodeType BarcodeType { get; private set; }

    internal override PageElementType TypeId
    {
      get
      {
        switch (this.BarcodeType)
        {
          case BarcodeType.Code39:
            return PageElementType.BarcodeCode39;
          default:
            return PageElementType.BarcodePdf417;
        }
      }
    }
  }
}
