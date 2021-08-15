// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Notifications.NotificationMessaging
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Google.Protobuf;
using Microsoft.Band.Protobuf;
using System;

namespace Microsoft.Band.Notifications
{
  internal class NotificationMessaging : NotificationBase
  {
    public NotificationMessaging(Guid tileId)
      : base(tileId)
    {
    }

    public DateTimeOffset Timestamp { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public byte Flags { get; set; }

    internal override int GetSerializedByteCount()
    {
      int num1 = 0;
      int num2 = 0;
      if (!string.IsNullOrEmpty(this.Title))
        num1 = this.Title.LengthTruncatedTrimDanglingHighSurrogate(20);
      if (!string.IsNullOrEmpty(this.Body))
        num2 = this.Body.LengthTruncatedTrimDanglingHighSurrogate(160);
      return base.GetSerializedByteCount() + 2 + 2 + CargoFileTime.GetSerializedByteCount() + 1 + 1 + num1 * 2 + num2 * 2;
    }

    internal override void SerializeToBand(ICargoWriter writer)
    {
      int maxLength1 = 0;
      int maxLength2 = 0;
      if (!string.IsNullOrEmpty(this.Title))
        maxLength1 = this.Title.LengthTruncatedTrimDanglingHighSurrogate(20);
      if (!string.IsNullOrEmpty(this.Body))
        maxLength2 = this.Body.LengthTruncatedTrimDanglingHighSurrogate(160);
      base.SerializeToBand(writer);
      writer.WriteUInt16((ushort) (maxLength1 * 2));
      writer.WriteUInt16((ushort) (maxLength2 * 2));
      CargoFileTime.SerializeToBandFromDateTimeOffset(writer, this.Timestamp);
      writer.WriteByte(this.Flags);
      writer.WriteByte((byte) 0);
      if (maxLength1 > 0)
        writer.WriteStringWithTruncation(this.Title, maxLength1);
      if (maxLength2 <= 0)
        return;
      writer.WriteStringWithTruncation(this.Body, maxLength2);
    }

    internal override int GetSerializedProtobufByteCount()
    {
      int num = 0 + 2 + ProtoFileTime.GetSerializedProtobufByteCount() + 2 + ProtoGuid.GetSerializedProtobufByteCount();
      if (!string.IsNullOrEmpty(this.Title))
        num += 1 + CodedOutputStreamExtensions.ComputeStringSize(this.Title, 40);
      if (!string.IsNullOrEmpty(this.Body))
        num += 1 + CodedOutputStreamExtensions.ComputeStringSize(this.Body, 320);
      if (this.Flags != (byte) 0)
        num += 1 + CodedOutputStream.ComputeUInt32Size((uint) this.Flags);
      return num;
    }

    internal override void SerializeProtobufToBand(CodedOutputStream output)
    {
      output.WriteRawTag((byte) 10);
      output.WriteLength(ProtoFileTime.GetSerializedProtobufByteCount());
      ProtoFileTime.SerializeProtobufToBand(output, this.Timestamp);
      output.WriteRawTag((byte) 18);
      output.WriteLength(ProtoGuid.GetSerializedProtobufByteCount());
      ProtoGuid.SerializeProtobufToBand(output, this.TileId);
      if (!string.IsNullOrEmpty(this.Title))
      {
        output.WriteRawTag((byte) 42);
        output.WriteString(this.Title, 40);
      }
      if (!string.IsNullOrEmpty(this.Body))
      {
        output.WriteRawTag((byte) 50);
        output.WriteString(this.Body, 320);
      }
      if (this.Flags == (byte) 0)
        return;
      output.WriteRawTag((byte) 80);
      output.WriteUInt32((uint) this.Flags);
    }
  }
}
