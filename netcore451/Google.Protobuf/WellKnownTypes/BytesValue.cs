// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.BytesValue
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class BytesValue : 
    IMessage<BytesValue>,
    IMessage,
    IEquatable<BytesValue>,
    IDeepCloneable<BytesValue>
  {
    public const int ValueFieldNumber = 1;
    private static readonly MessageParser<BytesValue> _parser = new MessageParser<BytesValue>((Func<BytesValue>) (() => new BytesValue()));
    private ByteString value_ = ByteString.Empty;

    public static MessageParser<BytesValue> Parser => BytesValue._parser;

    public static MessageDescriptor Descriptor => Wrappers.Descriptor.MessageTypes[8];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => BytesValue.Descriptor;

    public BytesValue()
    {
    }

    public BytesValue(BytesValue other)
      : this()
    {
      this.value_ = other.value_;
    }

    public BytesValue Clone() => new BytesValue(this);

    public ByteString Value
    {
      get => this.value_;
      set => this.value_ = Preconditions.CheckNotNull<ByteString>(value, nameof (value));
    }

    public override bool Equals(object other) => this.Equals(other as BytesValue);

    public bool Equals(BytesValue other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Value != other.Value));

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
      output.WriteBytes(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Value.Length != 0)
        num += 1 + CodedOutputStream.ComputeBytesSize(this.Value);
      return num;
    }

    public void MergeFrom(BytesValue other)
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
          this.Value = input.ReadBytes();
      }
    }
  }
}
