// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Notifications.NotificationBase
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Google.Protobuf;
using System;

namespace Microsoft.Band.Notifications
{
  internal abstract class NotificationBase
  {
    protected NotificationBase(Guid tileId) => this.TileId = tileId;

    protected NotificationBase(ref Guid tileId) => this.TileId = tileId;

    public Guid TileId { get; private set; }

    internal virtual int GetSerializedByteCount() => 16;

    internal virtual void SerializeToBand(ICargoWriter writer) => writer.WriteGuid(this.TileId);

    internal abstract int GetSerializedProtobufByteCount();

    internal abstract void SerializeProtobufToBand(CodedOutputStream output);
  }
}
