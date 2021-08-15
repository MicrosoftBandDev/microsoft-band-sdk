// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Protobuf.CodedOutputStreamExtensions
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Google.Protobuf;
using System;

namespace Microsoft.Band.Protobuf
{
  internal static class CodedOutputStreamExtensions
  {
    public static int ComputeStringSize(string value, int maxByteCount)
    {
      int danglingHighSurrogate = BandUtf8Encoding.GetUtf8ByteCountTrimDanglingHighSurrogate(value, maxByteCount);
      return CodedOutputStream.ComputeLengthSize(danglingHighSurrogate) + danglingHighSurrogate;
    }

    public static void WriteString(this CodedOutputStream output, string value, int maxByteCount)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (maxByteCount < 0)
        throw new ArgumentOutOfRangeException(nameof (maxByteCount));
      output.WriteString(value.Truncate(BandUtf8Encoding.GetCharCountToMaxUtf8ByteCountTrimDanglingHighSurrogate(value, maxByteCount)));
    }

    public static void WriteBytes(this CodedOutputStream output, byte[] value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      output.WriteBytes(ByteString.Unsafe.FromBytes(value));
    }
  }
}
