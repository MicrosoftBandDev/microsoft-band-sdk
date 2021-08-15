// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.StringExtensions
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band
{
  internal static class StringExtensions
  {
    public static string Truncate(this string s, int length) => s.Length <= length ? s : s.Substring(0, length);

    public static string TruncateTrimDanglingHighSurrogate(this string s, int length)
    {
      length = s.LengthTruncatedTrimDanglingHighSurrogate(length);
      return s.Truncate(length);
    }

    public static int LengthTruncatedTrimDanglingHighSurrogate(this string s, int length)
    {
      if (s.Length <= length)
        return s.Length;
      if (length > 0 && char.IsHighSurrogate(s[length - 1]))
        --length;
      return length;
    }
  }
}
