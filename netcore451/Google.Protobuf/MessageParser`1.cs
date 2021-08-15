// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.MessageParser`1
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.IO;

namespace Google.Protobuf
{
  public sealed class MessageParser<T> where T : IMessage<T>
  {
    private readonly Func<T> factory;

    public MessageParser(Func<T> factory) => this.factory = factory;

    internal T CreateTemplate() => this.factory();

    public T ParseFrom(byte[] data)
    {
      Preconditions.CheckNotNull<byte[]>(data, nameof (data));
      T message = this.factory();
      message.MergeFrom(data);
      return message;
    }

    public T ParseFrom(ByteString data)
    {
      Preconditions.CheckNotNull<ByteString>(data, nameof (data));
      T message = this.factory();
      message.MergeFrom(data);
      return message;
    }

    public T ParseFrom(Stream input)
    {
      T message = this.factory();
      message.MergeFrom(input);
      return message;
    }

    public T ParseDelimitedFrom(Stream input)
    {
      T message = this.factory();
      message.MergeDelimitedFrom(input);
      return message;
    }

    public T ParseFrom(CodedInputStream input)
    {
      T obj = this.factory();
      obj.MergeFrom(input);
      return obj;
    }
  }
}
