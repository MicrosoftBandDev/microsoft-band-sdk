// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Personalization.BandImage
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Personalization
{
  public class BandImage
  {
    internal BandImage(int width, int height, byte[] pixelData)
    {
      if (pixelData == null)
        throw new ArgumentNullException(nameof (pixelData));
      if (width < 0)
        throw new ArgumentOutOfRangeException(nameof (width));
      if (height < 0)
        throw new ArgumentOutOfRangeException(nameof (height));
      if (pixelData.Length != width * height * 2)
        throw new ArgumentException(BandResources.ImageDimensionPixelDataMismatch);
      this.Width = width;
      this.Height = height;
      this.PixelData = pixelData;
    }

    public int Width { get; private set; }

    public int Height { get; private set; }

    internal byte[] PixelData { get; private set; }
  }
}
