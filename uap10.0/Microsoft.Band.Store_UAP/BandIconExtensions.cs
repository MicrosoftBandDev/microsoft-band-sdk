// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandIconExtensions
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using Microsoft.Band.Tiles;
using System;
using Windows.UI.Xaml.Media.Imaging;

namespace Microsoft.Band
{
  public static class BandIconExtensions
  {
    public static WriteableBitmap ToWriteableBitmap(this BandIcon icon)
    {
      try
      {
        WriteableBitmap emptyWriteableBitmap = WriteableBitmapUtils.GetEmptyWriteableBitmap(icon.Width, icon.Height);
        emptyWriteableBitmap.SetPixelArrayAlpha4(icon.IconData, icon.Width * icon.Height * 4);
        return emptyWriteableBitmap;
      }
      catch (Exception ex)
      {
        throw new BandException("Failed to decode bitmap", ex);
      }
    }
  }
}
