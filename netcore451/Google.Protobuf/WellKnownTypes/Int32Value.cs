// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Int32Value
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Int32Value : 
    IMessage<Int32Value>,
    IMessage,
    IEquatable<Int32Value>,
    IDeepCloneable<Int32Value>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<Int32Value> _parser = new MessageParser<Int32Value>((Func<Int32Value>) (() => new Int32Value()));
    private int value_;

    public static MessageParser<Int32Value> Parser => Int32Value._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[4];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Int32Value.Descriptor;

    public Int32Value()
    {
    }

    public Int32Value(Int32Value other)
      : this()
    {
      this.value_ = other.value_;
    }

    public Int32Value Clone() => new Int32Value(this);

    public int Value
    {
      get => this.value_;
      set => this.value_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as Int32Value);

    public bool Equals(Int32Value other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Value == other.Value);

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Value != 0)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Value == 0)
        return;
      output.WriteRawTag((byte) 8);
      output.WriteInt32(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Value != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Value);
      return num;
    }

    public void MergeFrom(Int32Value other)
    {
      if (other == null || other.Value == 0)
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
          this.Value = input.ReadInt32();
      }
    }
  }
}
