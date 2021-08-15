// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.WriteableBitmapExtensions
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using Microsoft.Band.Personalization;
using Microsoft.Band.Tiles;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Imaging;

namespace Microsoft.Band
{
  public static class WriteableBitmapExtensions
  {
    public static BandImage ToBandImage(this WriteableBitmap bitmap)
    {
      if (bitmap == null)
        throw new ArgumentNullException(nameof (bitmap));
      try
      {
        byte[] pixelArrayBgr565 = bitmap.GetPixelArrayBgr565();
        return new BandImage(((BitmapSource) bitmap).PixelWidth, ((BitmapSource) bitmap).PixelHeight, pixelArrayBgr565);
      }
      catch (Exception ex)
      {
        throw new BandException("Failed to decode bitmap", ex);
      }
    }

    public static BandIcon ToBandIcon(this WriteableBitmap bitmap)
    {
      if (bitmap == null)
        throw new ArgumentNullException(nameof (bitmap));
      byte[] pixelArrayAlpha4;
      try
      {
        pixelArrayAlpha4 = bitmap.GetPixelArrayAlpha4();
      }
      catch (Exception ex)
      {
        throw new BandException("Failed to decode bitmap", ex);
      }
      return new BandIcon(((BitmapSource) bitmap).PixelWidth, ((BitmapSource) bitmap).PixelHeight, pixelArrayAlpha4);
    }

    internal static byte[] GetPixelArrayBgr565(this WriteableBitmap bitmap)
    {
      using (Stream stream = WindowsRuntimeBufferExtensions.AsStream(bitmap.PixelBuffer))
      {
        using (Bgr565Pbgra32ConversionStream conversionStream = new Bgr565Pbgra32ConversionStream((int) stream.Length))
        {
          stream.CopyTo((Stream) conversionStream, 8192);
          return conversionStream.Bgr565Array;
        }
      }
    }

    internal static byte[] GetPixelArrayAlpha4(this WriteableBitmap bitmap)
    {
      using (Stream stream = WindowsRuntimeBufferExtensions.AsStream(bitmap.PixelBuffer))
      {
        using (Alpha4Pbgra32ConversionStream conversionStream = new Alpha4Pbgra32ConversionStream((int) stream.Length))
        {
          stream.CopyTo((Stream) conversionStream, 8192);
          return conversionStream.Alpha4Array;
        }
      }
    }

    internal static void SetPixelArrayBgr565(this WriteableBitmap bitmap, byte[] bgr565Array)
    {
      using (Stream dest = WindowsRuntimeBufferExtensions.AsStream(bitmap.PixelBuffer))
      {
        using (Bgr565Pbgra32ConversionStream conversionStream = new Bgr565Pbgra32ConversionStream(bgr565Array))
          conversionStream.CopyTo(dest);
      }
    }

    internal static void SetPixelArrayAlpha4(
      this WriteableBitmap bitmap,
      byte[] alpha4Array,
      int argb32ByteCount)
    {
      using (Stream dest = WindowsRuntimeBufferExtensions.AsStream(bitmap.PixelBuffer))
      {
        using (Alpha4Pbgra32ConversionStream conversionStream = new Alpha4Pbgra32ConversionStream(alpha4Array, argb32ByteCount))
          conversionStream.CopyTo(dest);
      }
    }
  }
}
