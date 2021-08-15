// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.UInt32Value
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class UInt32Value : 
    IMessage<UInt32Value>,
    IMessage,
    IEquatable<UInt32Value>,
    IDeepCloneable<UInt32Value>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<UInt32Value> _parser = new MessageParser<UInt32Value>((Func<UInt32Value>) (() => new UInt32Value()));
    private uint value_;

    public static MessageParser<UInt32Value> Parser => UInt32Value._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[5];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => UInt32Value.Descriptor;

    public UInt32Value()
    {
    }

    public UInt32Value(UInt32Value other)
      : this()
    {
      this.value_ = other.value_;
    }

    public UInt32Value Clone() => new UInt32Value(this);

    public uint Value
    {
      get => this.value_;
      set => this.value_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as UInt32Value);

    public bool Equals(UInt32Value other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || (int) this.Value == (int) other.Value);

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Value != 0U)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Value == 0U)
        return;
      output.WriteRawTag((byte) 8);
      output.WriteUInt32(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Value != 0U)
        num += 1 + CodedOutputStream.ComputeUInt32Size(this.Value);
      return num;
    }

    public void MergeFrom(UInt32Value other)
    {
      if (other == null || other.Value == 0U)
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
          this.Value = input.ReadUInt32();
      }
    }
  }
}
