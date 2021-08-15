// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Protobuf.ProtoGuid
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Google.Protobuf;
using System;

namespace Microsoft.Band.Protobuf
{
  internal sealed class ProtoGuid
  {
    internal static int GetSerializedProtobufByteCount() => 18;

    internal static void SerializeProtobufToBand(CodedOutputStream output, Guid guid)
    {
      output.WriteRawTag((byte) 10);
      output.WriteBytes(guid.ToByteArray());
    }
  }
}
