// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Empty
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Empty : IMessage<Empty>, IMessage, IEquatable<Empty>, IDeepCloneable<Empty>
  {
    private static readonly MessageParser<Empty> _parser = new MessageParser<Empty>((Func<Empty>) (() => new Empty()));

    public static MessageParser<Empty> Parser => Empty._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Empty.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Empty.Descriptor;

    public Empty()
    {
    }

    public Empty(Empty other)
      : this()
    {
    }

    public Empty Clone() => new Empty(this);

    public override bool Equals(object other) => this.Equals(other as Empty);

    public bool Equals(Empty other)
    {
      if (object.ReferenceEquals((object) other, (object) null))
        return false;
      return !object.ReferenceEquals((object) other, (object) this) || true;
    }

    public override int GetHashCode() => 1;

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
    }

    public int CalculateSize() => 0;

    public void MergeFrom(Empty other)
    {
    }

    public void MergeFrom(CodedInputStream input)
    {
      while (input.ReadTag() != 0U)
        input.SkipLastField();
    }
  }
}
