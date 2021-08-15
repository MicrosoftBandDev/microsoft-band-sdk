// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.FieldOptions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class FieldOptions : 
    IMessage<FieldOptions>,
    IMessage,
    IEquatable<FieldOptions>,
    IDeepCloneable<FieldOptions>
  {
    public const int CtypeFieldNumber = 1;
    public const int PackedFieldNumber = 2;
    public const int JstypeFieldNumber = 6;
    public const int LazyFieldNumber = 5;
    public const int DeprecatedFieldNumber = 3;
    public const int WeakFieldNumber = 10;
    public const int UninterpretedOptionFieldNumber = 999;
    private static readonly MessageParser<FieldOptions> _parser = new MessageParser<FieldOptions>((Func<FieldOptions>) (() => new FieldOptions()));
    private FieldOptions.Types.CType ctype_;
    private bool packed_;
    private FieldOptions.Types.JSType jstype_;
    private bool lazy_;
    private bool deprecated_;
    private bool weak_;
    private static readonly FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec = FieldCodec.ForMessage<Google.Protobuf.Reflection.UninterpretedOption>(7994U, Google.Protobuf.Reflection.UninterpretedOption.Parser);
    private readonly RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_ = new RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>();

    public static MessageParser<FieldOptions> Parser => FieldOptions._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[11];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FieldOptions.Descriptor;

    public FieldOptions() => this.OnConstruction();

    public FieldOptions(FieldOptions other)
      : this()
    {
      this.ctype_ = other.ctype_;
      this.packed_ = other.packed_;
      this.jstype_ = other.jstype_;
      this.lazy_ = other.lazy_;
      this.deprecated_ = other.deprecated_;
      this.weak_ = other.weak_;
      this.uninterpretedOption_ = other.uninterpretedOption_.Clone();
    }

    public FieldOptions Clone() => new FieldOptions(this);

    public FieldOptions.Types.CType Ctype
    {
      get => this.ctype_;
      set => this.ctype_ = value;
    }

    public bool Packed
    {
      get => this.packed_;
      set => this.packed_ = value;
    }

    public FieldOptions.Types.JSType Jstype
    {
      get => this.jstype_;
      set => this.jstype_ = value;
    }

    public bool Lazy
    {
      get => this.lazy_;
      set => this.lazy_ = value;
    }

    public bool Deprecated
    {
      get => this.deprecated_;
      set => this.deprecated_ = value;
    }

    public bool Weak
    {
      get => this.weak_;
      set => this.weak_ = value;
    }

    public RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption => this.uninterpretedOption_;

    public override bool Equals(object other) => this.Equals(other as FieldOptions);

    public bool Equals(FieldOptions other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Ctype == other.Ctype && this.Packed == other.Packed && (this.Jstype == other.Jstype && this.Lazy == other.Lazy) && (this.Deprecated == other.Deprecated && this.Weak == other.Weak && this.uninterpretedOption_.Equals(other.uninterpretedOption_)));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Ctype != FieldOptions.Types.CType.STRING)
        num ^= this.Ctype.GetHashCode();
      if (this.Packed)
        num ^= this.Packed.GetHashCode();
      if (this.Jstype != FieldOptions.Types.JSType.JS_NORMAL)
        num ^= this.Jstype.GetHashCode();
      if (this.Lazy)
        num ^= this.Lazy.GetHashCode();
      if (this.Deprecated)
        num ^= this.Deprecated.GetHashCode();
      if (this.Weak)
        num ^= this.Weak.GetHashCode();
      return num ^ this.uninterpretedOption_.GetHashCode();
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Ctype != FieldOptions.Types.CType.STRING)
      {
        output.WriteRawTag((byte) 8);
        output.WriteEnum((int) this.Ctype);
      }
      if (this.Packed)
      {
        output.WriteRawTag((byte) 16);
        output.WriteBool(this.Packed);
      }
      if (this.Deprecated)
      {
        output.WriteRawTag((byte) 24);
        output.WriteBool(this.Deprecated);
      }
      if (this.Lazy)
      {
        output.WriteRawTag((byte) 40);
        output.WriteBool(this.Lazy);
      }
      if (this.Jstype != FieldOptions.Types.JSType.JS_NORMAL)
      {
        output.WriteRawTag((byte) 48);
        output.WriteEnum((int) this.Jstype);
      }
      if (this.Weak)
      {
        output.WriteRawTag((byte) 80);
        output.WriteBool(this.Weak);
      }
      this.uninterpretedOption_.WriteTo(output, FieldOptions._repeated_uninterpretedOption_codec);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Ctype != FieldOptions.Types.CType.STRING)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.Ctype);
      if (this.Packed)
        num += 2;
      if (this.Jstype != FieldOptions.Types.JSType.JS_NORMAL)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.Jstype);
      if (this.Lazy)
        num += 2;
      if (this.Deprecated)
        num += 2;
      if (this.Weak)
        num += 2;
      return num + this.uninterpretedOption_.CalculateSize(FieldOptions._repeated_uninterpretedOption_codec);
    }

    public void MergeFrom(FieldOptions other)
    {
      if (other == null)
        return;
      if (other.Ctype != FieldOptions.Types.CType.STRING)
        this.Ctype = other.Ctype;
      if (other.Packed)
        this.Packed = other.Packed;
      if (other.Jstype != FieldOptions.Types.JSType.JS_NORMAL)
        this.Jstype = other.Jstype;
      if (other.Lazy)
        this.Lazy = other.Lazy;
      if (other.Deprecated)
        this.Deprecated = other.Deprecated;
      if (other.Weak)
        this.Weak = other.Weak;
      this.uninterpretedOption_.Add(other.uninterpretedOption_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 8:
            this.ctype_ = (FieldOptions.Types.CType) input.ReadEnum();
            continue;
          case 16:
            this.Packed = input.ReadBool();
            continue;
          case 24:
            this.Deprecated = input.ReadBool();
            continue;
          case 40:
            this.Lazy = input.ReadBool();
            continue;
          case 48:
            this.jstype_ = (FieldOptions.Types.JSType) input.ReadEnum();
            continue;
          case 80:
            this.Weak = input.ReadBool();
            continue;
          case 7994:
            this.uninterpretedOption_.AddEntriesFrom(input, FieldOptions._repeated_uninterpretedOption_codec);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }

    private void OnConstruction() => this.Packed = true;

    [DebuggerNonUserCode]
    public static class Types
    {
      internal enum CType
      {
        STRING,
        CORD,
        STRING_PIECE,
      }

      internal enum JSType
      {
        JS_NORMAL,
        JS_STRING,
        JS_NUMBER,
      }
    }
  }
}
