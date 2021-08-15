// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.ListValue
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class ListValue : 
    IMessage<ListValue>,
    IMessage,
    IEquatable<ListValue>,
    IDeepCloneable<ListValue>
  {
    public const int ValuesFieldNumber = 1;
    private static readonly MessageParser<ListValue> _parser = new MessageParser<ListValue>((Func<ListValue>) (() => new ListValue()));
    private static readonly FieldCodec<Value> _repeated_values_codec = FieldCodec.ForMessage<Value>(10U, Value.Parser);
    private readonly RepeatedField<Value> values_ = new RepeatedField<Value>();

    public static MessageParser<ListValue> Parser => ListValue._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Struct.Descriptor.MessageTypes[2];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => ListValue.Descriptor;

    public ListValue()
    {
    }

    public ListValue(ListValue other)
      : this()
    {
      this.values_ = other.values_.Clone();
    }

    public ListValue Clone() => new ListValue(this);

    public RepeatedField<Value> Values => this.values_;

    public override bool Equals(object other) => this.Equals(other as ListValue);

    public bool Equals(ListValue other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.values_.Equals(other.values_));

    public override int GetHashCode() => 1 ^ this.values_.GetHashCode();

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output) => this.values_.WriteTo(output, ListValue._repeated_values_codec);

    public int CalculateSize() => 0 + this.values_.CalculateSize(ListValue._repeated_values_codec);

    public void MergeFrom(ListValue other)
    {
      if (other == null)
        return;
      this.values_.Add(other.values_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          input.SkipLastField();
        else
          this.values_.AddEntriesFrom(input, ListValue._repeated_values_codec);
      }
    }
  }
}
