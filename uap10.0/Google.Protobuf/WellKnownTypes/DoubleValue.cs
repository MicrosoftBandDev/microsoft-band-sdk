// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.DoubleValue
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class DoubleValue : 
    IMessage<DoubleValue>,
    IMessage,
    IEquatable<DoubleValue>,
    IDeepCloneable<DoubleValue>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<DoubleValue> _parser = new MessageParser<DoubleValue>((Func<DoubleValue>) (() => new DoubleValue()));
    private double value_;

    public static MessageParser<DoubleValue> Parser => DoubleValue._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => DoubleValue.Descriptor;

    public DoubleValue()
    {
    }

    public DoubleValue(DoubleValue other)
      : this()
    {
      this.value_ = other.value_;
    }

    public DoubleValue Clone() => new DoubleValue(this);

    public double Value
    {
      get => this.value_;
      set => this.value_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as DoubleValue);

    public bool Equals(DoubleValue other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Value == other.Value);

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Value != 0.0)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Value == 0.0)
        return;
      output.WriteRawTag((byte) 9);
      output.WriteDouble(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Value != 0.0)
        num += 9;
      return num;
    }

    public void MergeFrom(DoubleValue other)
    {
      if (other == null || other.Value == 0.0)
        return;
      this.Value = other.Value;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 9U)
          input.SkipLastField();
        else
          this.Value = input.ReadDouble();
      }
    }
  }
}
