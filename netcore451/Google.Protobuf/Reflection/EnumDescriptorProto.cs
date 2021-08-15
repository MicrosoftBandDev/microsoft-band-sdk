// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.EnumDescriptorProto
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class EnumDescriptorProto : 
    IMessage<EnumDescriptorProto>,
    IMessage,
    IEquatable<EnumDescriptorProto>,
    IDeepCloneable<EnumDescriptorProto>
  {
    public const int NameFieldNumber = 1;
    public const int ValueFieldNumber = 2;
    public const int OptionsFieldNumber = 3;
    private static readonly MessageParser<EnumDescriptorProto> _parser = new MessageParser<EnumDescriptorProto>((Func<EnumDescriptorProto>) (() => new EnumDescriptorProto()));
    private string name_ = "";
    private static readonly FieldCodec<EnumValueDescriptorProto> _repeated_value_codec = FieldCodec.ForMessage<EnumValueDescriptorProto>(18U, EnumValueDescriptorProto.Parser);
    private readonly RepeatedField<EnumValueDescriptorProto> value_ = new RepeatedField<EnumValueDescriptorProto>();
    private EnumOptions options_;

    public static MessageParser<EnumDescriptorProto> Parser => EnumDescriptorProto._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[5];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => EnumDescriptorProto.Descriptor;

    public EnumDescriptorProto()
    {
    }

    public EnumDescriptorProto(EnumDescriptorProto other)
      : this()
    {
      this.name_ = other.name_;
      this.value_ = other.value_.Clone();
      this.Options = other.options_ != null ? other.Options.Clone() : (EnumOptions) null;
    }

    public EnumDescriptorProto Clone() => new EnumDescriptorProto(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public RepeatedField<EnumValueDescriptorProto> Value => this.value_;

    public EnumOptions Options
    {
      get => this.options_;
      set => this.options_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as EnumDescriptorProto);

    public bool Equals(EnumDescriptorProto other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.value_.Equals(other.value_) && object.Equals((object) this.Options, (object) other.Options));

    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.Name.Length != 0)
        num1 ^= this.Name.GetHashCode();
      int num2 = num1 ^ this.value_.GetHashCode();
      if (this.options_ != null)
        num2 ^= this.Options.GetHashCode();
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
      this.value_.WriteTo(output, EnumDescriptorProto._repeated_value_codec);
      if (this.options_ == null)
        return;
      output.WriteRawTag((byte) 26);
      output.WriteMessage((IMessage) this.Options);
    }

    public int CalculateSize()
    {
      int num1 = 0;
      if (this.Name.Length != 0)
        num1 += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      int num2 = num1 + this.value_.CalculateSize(EnumDescriptorProto._repeated_value_codec);
      if (this.options_ != null)
        num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Options);
      return num2;
    }

    public void MergeFrom(EnumDescriptorProto other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      this.value_.Add(other.value_);
      if (other.options_ == null)
        return;
      if (this.options_ == null)
        this.options_ = new EnumOptions();
      this.Options.MergeFrom(other.Options);
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
            this.value_.AddEntriesFrom(input, EnumDescriptorProto._repeated_value_codec);
            continue;
          case 26:
            if (this.options_ == null)
              this.options_ = new EnumOptions();
            input.ReadMessage((IMessage) this.options_);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
