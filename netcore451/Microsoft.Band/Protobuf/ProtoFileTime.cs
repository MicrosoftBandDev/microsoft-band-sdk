// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Protobuf.ProtoFileTime
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Google.Protobuf;
using System;

namespace Microsoft.Band.Protobuf
{
  internal class ProtoFileTime
  {
    internal static int GetSerializedProtobufByteCount() => 10;

    internal static void SerializeProtobufToBand(CodedOutputStream output, DateTime timeStamp) => ProtoFileTime.SerializeProtobufToBand(output, timeStamp.ToFileTime());

    internal static void SerializeProtobufToBand(CodedOutputStream output, DateTimeOffset timeStamp) => ProtoFileTime.SerializeProtobufToBand(output, timeStamp.ToFileTime());

    private static void SerializeProtobufToBand(CodedOutputStream output, long fileTime)
    {
      output.WriteRawTag((byte) 13);
      output.WriteFixed32((uint) fileTime);
      output.WriteRawTag((byte) 21);
      output.WriteFixed32((uint) (fileTime >> 32));
    }
  }
}
