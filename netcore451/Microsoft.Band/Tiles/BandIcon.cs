// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.BandIcon
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Tiles
{
  public class BandIcon
  {
    internal BandIcon(int width, int height, byte[] iconData)
    {
      if (width < 0)
        throw new ArgumentOutOfRangeException(nameof (width));
      if (height < 0)
        throw new ArgumentOutOfRangeException(nameof (height));
      if (iconData == null)
        throw new ArgumentNullException(nameof (iconData));
      long num = (long) (width * height);
      if ((long) iconData.Length != num / 2L + num % 2L)
        throw new ArgumentException(BandResources.ImageDimensionPixelDataMismatch);
      this.Width = width;
      this.Height = height;
      this.IconData = iconData;
    }

    public int Height { get; private set; }

    public int Width { get; private set; }

    internal byte[] IconData { get; private set; }
  }
}
