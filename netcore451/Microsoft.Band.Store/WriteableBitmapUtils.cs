// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.WriteableBitmapUtils
// Assembly: Microsoft.Band.Store, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 91750BE8-70C6-4542-841C-664EE611AF0B
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.Store.dll

using Windows.UI.Xaml.Media.Imaging;

namespace Microsoft.Band
{
  internal static class WriteableBitmapUtils
  {
    public static WriteableBitmap GetEmptyWriteableBitmap(
      int pixelWidth,
      int pixelHeight)
    {
      return new WriteableBitmap(pixelWidth, pixelHeight);
    }
  }
}
