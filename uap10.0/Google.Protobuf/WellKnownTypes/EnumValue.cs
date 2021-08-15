// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.EnumValue
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
  public sealed class EnumValue : 
    IMessage<EnumValue>,
    IMessage,
    IEquatable<EnumValue>,
    IDeepCloneable<EnumValue>
  {
    public const int NameFieldNumber = 1;
    public const int NumberFieldNumber = 2;
    public const int OptionsFieldNumber = 3;
    private static readonly MessageParser<EnumValue> _parser = new MessageParser<EnumValue>((Func<EnumValue>) (() => new EnumValue()));
    private string name_ = "";
    private int number_;
    private static readonly FieldCodec<Option> _repeated_options_codec = FieldCodec.ForMessage<Option>(26U, Option.Parser);
    private readonly RepeatedField<Option> options_ = new RepeatedField<Option>();

    public static MessageParser<EnumValue> Parser => EnumValue._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Type.Descriptor.MessageTypes[3];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => EnumValue.Descriptor;

    public EnumValue()
    {
    }

    public EnumValue(EnumValue other)
      : this()
    {
      this.name_ = other.name_;
      this.number_ = other.number_;
      this.options_ = other.options_.Clone();
    }

    public EnumValue Clone() => new EnumValue(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public int Number
    {
      get => this.number_;
      set => this.number_ = value;
    }

    public RepeatedField<Option> Options => this.options_;

    public override bool Equals(object other) => this.Equals(other as EnumValue);

    public bool Equals(EnumValue other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.Number == other.Number && this.options_.Equals(other.options_));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      if (this.Number != 0)
        num ^= this.Number.GetHashCode();
      return num ^ this.options_.GetHashCode();
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Name.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.Name);
      }
      if (this.Number != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.Number);
      }
      this.options_.WriteTo(output, EnumValue._repeated_options_codec);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.Number != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Number);
      return num + this.options_.CalculateSize(EnumValue._repeated_options_codec);
    }

    public void MergeFrom(EnumValue other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.Number != 0)
        this.Number = other.Number;
      this.options_.Add(other.options_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 10:
            this.Name = input.ReadString();
            continue;
          case 16:
            this.Number = input.ReadInt32();
            continue;
          case 26:
            this.options_.AddEntriesFrom(input, EnumValue._repeated_options_codec);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
