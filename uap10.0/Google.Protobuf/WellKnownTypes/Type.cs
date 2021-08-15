// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Type
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
  public sealed class Type : IMessage<Type>, IMessage, IEquatable<Type>, IDeepCloneable<Type>
  {
    public const int NameFieldNumber = 1;
    public const int FieldsFieldNumber = 2;
    public const int OneofsFieldNumber = 3;
    public const int OptionsFieldNumber = 4;
    public const int SourceContextFieldNumber = 5;
    private static readonly MessageParser<Type> _parser = new MessageParser<Type>((Func<Type>) (() => new Type()));
    private string name_ = "";
    private static readonly FieldCodec<Field> _repeated_fields_codec = FieldCodec.ForMessage<Field>(18U, Field.Parser);
    private readonly RepeatedField<Field> fields_ = new RepeatedField<Field>();
    private static readonly FieldCodec<string> _repeated_oneofs_codec = FieldCodec.ForString(26U);
    private readonly RepeatedField<string> oneofs_ = new RepeatedField<string>();
    private static readonly FieldCodec<Option> _repeated_options_codec = FieldCodec.ForMessage<Option>(34U, Option.Parser);
    private readonly RepeatedField<Option> options_ = new RepeatedField<Option>();
    private SourceContext sourceContext_;

    public static MessageParser<Type> Parser => Type._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Type.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Type.Descriptor;

    public Type()
    {
    }

    public Type(Type other)
      : this()
    {
      this.name_ = other.name_;
      this.fields_ = other.fields_.Clone();
      this.oneofs_ = other.oneofs_.Clone();
      this.options_ = other.options_.Clone();
      this.SourceContext = other.sourceContext_ != null ? other.SourceContext.Clone() : (SourceContext) null;
    }

    public Type Clone() => new Type(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public RepeatedField<Field> Fields => this.fields_;

    public RepeatedField<string> Oneofs => this.oneofs_;

    public RepeatedField<Option> Options => this.options_;

    public SourceContext SourceContext
    {
      get => this.sourceContext_;
      set => this.sourceContext_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as Type);

    public bool Equals(Type other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.fields_.Equals(other.fields_) && (this.oneofs_.Equals(other.oneofs_) && this.options_.Equals(other.options_)) && object.Equals((object) this.SourceContext, (object) other.SourceContext));

    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.Name.Length != 0)
        num1 ^= this.Name.GetHashCode();
      int num2 = num1 ^ this.fields_.GetHashCode() ^ this.oneofs_.GetHashCode() ^ this.options_.GetHashCode();
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
      this.fields_.WriteTo(output, Type._repeated_fields_codec);
      this.oneofs_.WriteTo(output, Type._repeated_oneofs_codec);
      this.options_.WriteTo(output, Type._repeated_options_codec);
      if (this.sourceContext_ == null)
        return;
      output.WriteRawTag((byte) 42);
      output.WriteMessage((IMessage) this.SourceContext);
    }

    public int CalculateSize()
    {
      int num1 = 0;
      if (this.Name.Length != 0)
        num1 += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      int num2 = num1 + this.fields_.CalculateSize(Type._repeated_fields_codec) + this.oneofs_.CalculateSize(Type._repeated_oneofs_codec) + this.options_.CalculateSize(Type._repeated_options_codec);
      if (this.sourceContext_ != null)
        num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SourceContext);
      return num2;
    }

    public void MergeFrom(Type other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      this.fields_.Add(other.fields_);
      this.oneofs_.Add(other.oneofs_);
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
            this.fields_.AddEntriesFrom(input, Type._repeated_fields_codec);
            continue;
          case 26:
            this.oneofs_.AddEntriesFrom(input, Type._repeated_oneofs_codec);
            continue;
          case 34:
            this.options_.AddEntriesFrom(input, Type._repeated_options_codec);
            continue;
          case 42:
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
