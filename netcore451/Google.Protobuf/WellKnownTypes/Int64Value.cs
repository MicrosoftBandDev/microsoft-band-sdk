// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Int64Value
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Int64Value : 
    IMessage<Int64Value>,
    IMessage,
    IEquatable<Int64Value>,
    IDeepCloneable<Int64Value>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<Int64Value> _parser = new MessageParser<Int64Value>((Func<Int64Value>) (() => new Int64Value()));
    private long value_;

    public static MessageParser<Int64Value> Parser => Int64Value._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[2];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Int64Value.Descriptor;

    public Int64Value()
    {
    }

    public Int64Value(Int64Value other)
      : this()
    {
      this.value_ = other.value_;
    }

    public Int64Value Clone() => new Int64Value(this);

    public long Value
    {
      get => this.value_;
      set => this.value_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as Int64Value);

    public bool Equals(Int64Value other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Value == other.Value);

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Value != 0L)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Value == 0L)
        return;
      output.WriteRawTag((byte) 8);
      output.WriteInt64(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Value != 0L)
        num += 1 + CodedOutputStream.ComputeInt64Size(this.Value);
      return num;
    }

    public void MergeFrom(Int64Value other)
    {
      if (other == null || other.Value == 0L)
        return;
      this.Value = other.Value;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 8U)
          input.SkipLastField();
        else
          this.Value = input.ReadInt64();
      }
    }
  }
}
