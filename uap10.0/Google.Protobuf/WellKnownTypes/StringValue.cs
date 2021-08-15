// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.StringValue
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class StringValue : 
    IMessage<StringValue>,
    IMessage,
    IEquatable<StringValue>,
    IDeepCloneable<StringValue>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<StringValue> _parser = new MessageParser<StringValue>((Func<StringValue>) (() => new StringValue()));
    private string value_ = "";

    public static MessageParser<StringValue> Parser => StringValue._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[7];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => StringValue.Descriptor;

    public StringValue()
    {
    }

    public StringValue(StringValue other)
      : this()
    {
      this.value_ = other.value_;
    }

    public StringValue Clone() => new StringValue(this);

    public string Value
    {
      get => this.value_;
      set => this.value_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public override bool Equals(object other) => this.Equals(other as StringValue);

    public bool Equals(StringValue other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Value != other.Value));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Value.Length != 0)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Value.Length == 0)
        return;
      output.WriteRawTag((byte) 10);
      output.WriteString(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Value.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Value);
      return num;
    }

    public void MergeFrom(StringValue other)
    {
      if (other == null || other.Value.Length == 0)
        return;
      this.Value = other.Value;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          input.SkipLastField();
        else
          this.Value = input.ReadString();
      }
    }
  }
}
