// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.UInt64Value
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class UInt64Value : 
    IMessage<UInt64Value>,
    IMessage,
    IEquatable<UInt64Value>,
    IDeepCloneable<UInt64Value>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<UInt64Value> _parser = new MessageParser<UInt64Value>((Func<UInt64Value>) (() => new UInt64Value()));
    private ulong value_;

    public static MessageParser<UInt64Value> Parser => UInt64Value._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[3];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => UInt64Value.Descriptor;

    public UInt64Value()
    {
    }

    public UInt64Value(UInt64Value other)
      : this()
    {
      this.value_ = other.value_;
    }

    public UInt64Value Clone() => new UInt64Value(this);

    public ulong Value
    {
      get => this.value_;
      set => this.value_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as UInt64Value);

    public bool Equals(UInt64Value other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || (long) this.Value == (long) other.Value);

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Value != 0UL)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Value == 0UL)
        return;
      output.WriteRawTag((byte) 8);
      output.WriteUInt64(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Value != 0UL)
        num += 1 + CodedOutputStream.ComputeUInt64Size(this.Value);
      return num;
    }

    public void MergeFrom(UInt64Value other)
    {
      if (other == null || other.Value == 0UL)
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
          this.Value = input.ReadUInt64();
      }
    }
  }
}
