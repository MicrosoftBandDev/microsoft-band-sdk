// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.FloatValue
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class FloatValue : 
    IMessage<FloatValue>,
    IMessage,
    IEquatable<FloatValue>,
    IDeepCloneable<FloatValue>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<FloatValue> _parser = new MessageParser<FloatValue>((Func<FloatValue>) (() => new FloatValue()));
    private float value_;

    public static MessageParser<FloatValue> Parser => FloatValue._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[1];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FloatValue.Descriptor;

    public FloatValue()
    {
    }

    public FloatValue(FloatValue other)
      : this()
    {
      this.value_ = other.value_;
    }

    public FloatValue Clone() => new FloatValue(this);

    public float Value
    {
      get => this.value_;
      set => this.value_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as FloatValue);

    public bool Equals(FloatValue other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || (double) this.Value == (double) other.Value);

    public override int GetHashCode()
    {
      int num = 1;
      if ((double) this.Value != 0.0)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if ((double) this.Value == 0.0)
        return;
      output.WriteRawTag((byte) 13);
      output.WriteFloat(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if ((double) this.Value != 0.0)
        num += 5;
      return num;
    }

    public void MergeFrom(FloatValue other)
    {
      if (other == null || (double) other.Value == 0.0)
        return;
      this.Value = other.Value;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 13U)
          input.SkipLastField();
        else
          this.Value = input.ReadFloat();
      }
    }
  }
}
