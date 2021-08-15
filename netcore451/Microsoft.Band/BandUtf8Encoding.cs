// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandUtf8Encoding
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System.Text;

namespace Microsoft.Band
{
  internal static class BandUtf8Encoding
  {
    private static readonly Encoding Utf8Encoding = Encoding.UTF8;

    public static int GetUtf8ByteCountTrimDanglingHighSurrogate(string s, int maxByteCount) => BandUtf8Encoding.GetUtf8ByteOrCharacterCountToMaxUtf8ByteCountTrimDanglingHighSurrogate(s, maxByteCount, BandUtf8Encoding.CountValue.Bytes);

    public static int GetCharCountToMaxUtf8ByteCountTrimDanglingHighSurrogate(
      string s,
      int maxByteCount)
    {
      return BandUtf8Encoding.GetUtf8ByteOrCharacterCountToMaxUtf8ByteCountTrimDanglingHighSurrogate(s, maxByteCount, BandUtf8Encoding.CountValue.Characters);
    }

    public static int GetUtfBytesTrimDanglingHighSurrogate(
      string s,
      int charIndex,
      byte[] bytes,
      int byteIndex,
      int maxByteCount)
    {
      int num = 0;
      int byteCount = 0;
      charIndex = 0;
      while (charIndex < s.Length && num < maxByteCount)
      {
        int charCount = 1;
        if (char.IsHighSurrogate(s[charIndex]))
        {
          if (charIndex != s.Length - 1)
            charCount = 2;
          else
            break;
        }
        BandUtf8Encoding.GetCharAndUtf8ByteCount(s, charIndex, out charCount, out byteCount);
        if (byteCount != 0 && num + byteCount <= maxByteCount)
        {
          num += byteCount;
          BandUtf8Encoding.Utf8Encoding.GetBytes(s, charIndex, charCount, bytes, byteIndex);
          charIndex += charCount;
          byteIndex += byteCount;
        }
        else
          break;
      }
      return byteIndex;
    }

    private static int GetUtf8ByteOrCharacterCountToMaxUtf8ByteCountTrimDanglingHighSurrogate(
      string s,
      int maxByteCount,
      BandUtf8Encoding.CountValue countValue)
    {
      int num1 = 0;
      int num2 = 0;
      int charCount = 0;
      for (int offset = 0; offset < s.Length; offset += charCount)
      {
        int byteCount;
        BandUtf8Encoding.GetCharAndUtf8ByteCount(s, offset, out charCount, out byteCount);
        if (byteCount != 0)
        {
          int num3 = num1 + byteCount;
          if (num3 <= maxByteCount)
          {
            num1 = num3;
            num2 += charCount;
          }
          else
            break;
        }
        else
          break;
      }
      return countValue == BandUtf8Encoding.CountValue.Bytes || countValue != BandUtf8Encoding.CountValue.Characters ? num1 : num2;
    }

    private static void GetCharAndUtf8ByteCount(
      string s,
      int offset,
      out int charCount,
      out int byteCount)
    {
      char c = s[offset];
      if (c < '\u0080')
      {
        charCount = 1;
        byteCount = 1;
      }
      else if (c < 'ࠀ')
      {
        charCount = 1;
        byteCount = 2;
      }
      else if (c < '\uD800')
      {
        charCount = 1;
        byteCount = 3;
      }
      else if (char.IsHighSurrogate(c))
      {
        if (offset + 1 < s.Length)
        {
          charCount = 2;
          byteCount = 4;
        }
        else
        {
          charCount = 0;
          byteCount = 0;
        }
      }
      else
      {
        charCount = 1;
        byteCount = 3;
      }
    }

    private enum CountValue
    {
      Bytes,
      Characters,
    }
  }
}
