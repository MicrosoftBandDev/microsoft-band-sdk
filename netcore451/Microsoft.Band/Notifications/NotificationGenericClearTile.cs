// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Notifications.NotificationGenericClearTile
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Google.Protobuf;
using Microsoft.Band.Protobuf;
using System;

namespace Microsoft.Band.Notifications
{
  internal sealed class NotificationGenericClearTile : NotificationBase
  {
    public NotificationGenericClearTile(Guid tileId)
      : base(tileId)
    {
    }

    internal override int GetSerializedProtobufByteCount() => 0 + 2 + ProtoGuid.GetSerializedProtobufByteCount() + (1 + CodedOutputStream.ComputeEnumSize(1));

    internal override void SerializeProtobufToBand(CodedOutputStream output)
    {
      output.WriteRawTag((byte) 10);
      output.WriteLength(ProtoGuid.GetSerializedProtobufByteCount());
      ProtoGuid.SerializeProtobufToBand(output, this.TileId);
      output.WriteRawTag((byte) 24);
      output.WriteEnum(1);
    }
  }
}
