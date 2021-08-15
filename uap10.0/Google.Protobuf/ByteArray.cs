// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.ByteArray
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;

namespace Google.Protobuf
{
  internal static class ByteArray
  {
    private const int CopyThreshold = 12;

    internal static void Copy(byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
    {
      if (count > 12)
      {
        Buffer.BlockCopy((Array) src, srcOffset, (Array) dst, dstOffset, count);
      }
      else
      {
        int num = srcOffset + count;
        for (int index = srcOffset; index < num; ++index)
          dst[dstOffset++] = src[index];
      }
    }

    internal static void Reverse(byte[] bytes)
    {
      int index1 = 0;
      for (int index2 = bytes.Length - 1; index1 < index2; --index2)
      {
        byte num = bytes[index1];
        bytes[index1] = bytes[index2];
        bytes[index2] = num;
        ++index1;
      }
    }
  }
}
