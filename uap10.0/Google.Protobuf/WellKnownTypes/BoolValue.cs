// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.BoolValue
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class BoolValue : 
    IMessage<BoolValue>,
    IMessage,
    IEquatable<BoolValue>,
    IDeepCloneable<BoolValue>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<BoolValue> _parser = new MessageParser<BoolValue>((Func<BoolValue>) (() => new BoolValue()));
    private bool value_;

    public static MessageParser<BoolValue> Parser => BoolValue._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[6];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => BoolValue.Descriptor;

    public BoolValue()
    {
    }

    public BoolValue(BoolValue other)
      : this()
    {
      this.value_ = other.value_;
    }

    public BoolValue Clone() => new BoolValue(this);

    public bool Value
    {
      get => this.value_;
      set => this.value_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as BoolValue);

    public bool Equals(BoolValue other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Value == other.Value);

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Value)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (!this.Value)
        return;
      output.WriteRawTag((byte) 8);
      output.WriteBool(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Value)
        num += 2;
      return num;
    }

    public void MergeFrom(BoolValue other)
    {
      if (other == null || !other.Value)
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
          this.Value = input.ReadBool();
      }
    }
  }
}
