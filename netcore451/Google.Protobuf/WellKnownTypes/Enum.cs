// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Enum
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
  public sealed class Enum : IMessage<Enum>, IMessage, IEquatable<Enum>, IDeepCloneable<Enum>
  {
    public const int NameFieldNumber = 1;
    public const int EnumvalueFieldNumber = 2;
    public const int OptionsFieldNumber = 3;
    public const int SourceContextFieldNumber = 4;
    private static readonly MessageParser<Enum> _parser = new MessageParser<Enum>((Func<Enum>) (() => new Enum()));
    private string name_ = "";
    private static readonly FieldCodec<EnumValue> _repeated_enumvalue_codec = FieldCodec.ForMessage<EnumValue>(18U, EnumValue.Parser);
    private readonly RepeatedField<EnumValue> enumvalue_ = new RepeatedField<EnumValue>();
    private static readonly FieldCodec<Option> _repeated_options_codec = FieldCodec.ForMessage<Option>(26U, Option.Parser);
    private readonly RepeatedField<Option> options_ = new RepeatedField<Option>();
    private SourceContext sourceContext_;

    public static MessageParser<Enum> Parser => Enum._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Type.Descriptor.MessageTypes[2];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Enum.Descriptor;

    public Enum()
    {
    }

    public Enum(Enum other)
      : this()
    {
      this.name_ = other.name_;
      this.enumvalue_ = other.enumvalue_.Clone();
      this.options_ = other.options_.Clone();
      this.SourceContext = other.sourceContext_ != null ? other.SourceContext.Clone() : (SourceContext) null;
    }

    public Enum Clone() => new Enum(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public RepeatedField<EnumValue> Enumvalue => this.enumvalue_;

    public RepeatedField<Option> Options => this.options_;

    public SourceContext SourceContext
    {
      get => this.sourceContext_;
      set => this.sourceContext_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as Enum);

    public bool Equals(Enum other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.enumvalue_.Equals(other.enumvalue_) && (this.options_.Equals(other.options_) && object.Equals((object) this.SourceContext, (object) other.SourceContext)));

    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.Name.Length != 0)
        num1 ^= this.Name.GetHashCode();
      int num2 = num1 ^ this.enumvalue_.GetHashCode() ^ this.options_.GetHashCode();
      if (this.sourceContext_ != null)
        num2 ^= this.SourceContext.GetHashCode();
      return num2;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Name.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.Name);
      }
      this.enumvalue_.WriteTo(output, Enum._repeated_enumvalue_codec);
      this.options_.WriteTo(output, Enum._repeated_options_codec);
      if (this.sourceContext_ == null)
        return;
      output.WriteRawTag((byte) 34);
      output.WriteMessage((IMessage) this.SourceContext);
    }

    public int CalculateSize()
    {
      int num1 = 0;
      if (this.Name.Length != 0)
        num1 += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      int num2 = num1 + this.enumvalue_.CalculateSize(Enum._repeated_enumvalue_codec) + this.options_.CalculateSize(Enum._repeated_options_codec);
      if (this.sourceContext_ != null)
        num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SourceContext);
      return num2;
    }

    public void MergeFrom(Enum other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      this.enumvalue_.Add(other.enumvalue_);
      this.options_.Add(other.options_);
      if (other.sourceContext_ == null)
        return;
      if (this.sourceContext_ == null)
        this.sourceContext_ = new SourceContext();
      this.SourceContext.MergeFrom(other.SourceContext);
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
          case 18:
            this.enumvalue_.AddEntriesFrom(input, Enum._repeated_enumvalue_codec);
            continue;
          case 26:
            this.options_.AddEntriesFrom(input, Enum._repeated_options_codec);
            continue;
          case 34:
            if (this.sourceContext_ == null)
              this.sourceContext_ = new SourceContext();
            input.ReadMessage((IMessage) this.sourceContext_);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
