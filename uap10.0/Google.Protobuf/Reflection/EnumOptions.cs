// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.EnumOptions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class EnumOptions : 
    IMessage<EnumOptions>,
    IMessage,
    IEquatable<EnumOptions>,
    IDeepCloneable<EnumOptions>
  {
    public const int AllowAliasFieldNumber = 2;
    public const int DeprecatedFieldNumber = 3;
    public const int UninterpretedOptionFieldNumber = 999;
    private static readonly MessageParser<EnumOptions> _parser = new MessageParser<EnumOptions>((Func<EnumOptions>) (() => new EnumOptions()));
    private bool allowAlias_;
    private bool deprecated_;
    private static readonly FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec = FieldCodec.ForMessage<Google.Protobuf.Reflection.UninterpretedOption>(7994U, Google.Protobuf.Reflection.UninterpretedOption.Parser);
    private readonly RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_ = new RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>();

    public static MessageParser<EnumOptions> Parser => EnumOptions._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[12];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => EnumOptions.Descriptor;

    public EnumOptions()
    {
    }

    public EnumOptions(EnumOptions other)
      : this()
    {
      this.allowAlias_ = other.allowAlias_;
      this.deprecated_ = other.deprecated_;
      this.uninterpretedOption_ = other.uninterpretedOption_.Clone();
    }

    public EnumOptions Clone() => new EnumOptions(this);

    public bool AllowAlias
    {
      get => this.allowAlias_;
      set => this.allowAlias_ = value;
    }

    public bool Deprecated
    {
      get => this.deprecated_;
      set => this.deprecated_ = value;
    }

    public RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption => this.uninterpretedOption_;

    public override bool Equals(object other) => this.Equals(other as EnumOptions);

    public bool Equals(EnumOptions other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.AllowAlias == other.AllowAlias && this.Deprecated == other.Deprecated && this.uninterpretedOption_.Equals(other.uninterpretedOption_));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.AllowAlias)
        num ^= this.AllowAlias.GetHashCode();
      if (this.Deprecated)
        num ^= this.Deprecated.GetHashCode();
      return num ^ this.uninterpretedOption_.GetHashCode();
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.AllowAlias)
      {
        output.WriteRawTag((byte) 16);
        output.WriteBool(this.AllowAlias);
      }
      if (this.Deprecated)
      {
        output.WriteRawTag((byte) 24);
        output.WriteBool(this.Deprecated);
      }
      this.uninterpretedOption_.WriteTo(output, EnumOptions._repeated_uninterpretedOption_codec);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.AllowAlias)
        num += 2;
      if (this.Deprecated)
        num += 2;
      return num + this.uninterpretedOption_.CalculateSize(EnumOptions._repeated_uninterpretedOption_codec);
    }

    public void MergeFrom(EnumOptions other)
    {
      if (other == null)
        return;
      if (other.AllowAlias)
        this.AllowAlias = other.AllowAlias;
      if (other.Deprecated)
        this.Deprecated = other.Deprecated;
      this.uninterpretedOption_.Add(other.uninterpretedOption_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 16:
            this.AllowAlias = input.ReadBool();
            continue;
          case 24:
            this.Deprecated = input.ReadBool();
            continue;
          case 7994:
            this.uninterpretedOption_.AddEntriesFrom(input, EnumOptions._repeated_uninterpretedOption_codec);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
