// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.DescriptorProto
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class DescriptorProto : 
    IMessage<DescriptorProto>,
    IMessage,
    IEquatable<DescriptorProto>,
    IDeepCloneable<DescriptorProto>
  {
    public const int NameFieldNumber = 1;
    public const int FieldFieldNumber = 2;
    public const int ExtensionFieldNumber = 6;
    public const int NestedTypeFieldNumber = 3;
    public const int EnumTypeFieldNumber = 4;
    public const int ExtensionRangeFieldNumber = 5;
    public const int OneofDeclFieldNumber = 8;
    public const int OptionsFieldNumber = 7;
    public const int ReservedRangeFieldNumber = 9;
    public const int ReservedNameFieldNumber = 10;
    private static readonly MessageParser<DescriptorProto> _parser = new MessageParser<DescriptorProto>((Func<DescriptorProto>) (() => new DescriptorProto()));
    private string name_ = "";
    private static readonly FieldCodec<FieldDescriptorProto> _repeated_field_codec = FieldCodec.ForMessage<FieldDescriptorProto>(18U, FieldDescriptorProto.Parser);
    private readonly RepeatedField<FieldDescriptorProto> field_ = new RepeatedField<FieldDescriptorProto>();
    private static readonly FieldCodec<FieldDescriptorProto> _repeated_extension_codec = FieldCodec.ForMessage<FieldDescriptorProto>(50U, FieldDescriptorProto.Parser);
    private readonly RepeatedField<FieldDescriptorProto> extension_ = new RepeatedField<FieldDescriptorProto>();
    private static readonly FieldCodec<DescriptorProto> _repeated_nestedType_codec = FieldCodec.ForMessage<DescriptorProto>(26U, DescriptorProto.Parser);
    private readonly RepeatedField<DescriptorProto> nestedType_ = new RepeatedField<DescriptorProto>();
    private static readonly FieldCodec<EnumDescriptorProto> _repeated_enumType_codec = FieldCodec.ForMessage<EnumDescriptorProto>(34U, EnumDescriptorProto.Parser);
    private readonly RepeatedField<EnumDescriptorProto> enumType_ = new RepeatedField<EnumDescriptorProto>();
    private static readonly FieldCodec<DescriptorProto.Types.ExtensionRange> _repeated_extensionRange_codec = FieldCodec.ForMessage<DescriptorProto.Types.ExtensionRange>(42U, DescriptorProto.Types.ExtensionRange.Parser);
    private readonly RepeatedField<DescriptorProto.Types.ExtensionRange> extensionRange_ = new RepeatedField<DescriptorProto.Types.ExtensionRange>();
    private static readonly FieldCodec<OneofDescriptorProto> _repeated_oneofDecl_codec = FieldCodec.ForMessage<OneofDescriptorProto>(66U, OneofDescriptorProto.Parser);
    private readonly RepeatedField<OneofDescriptorProto> oneofDecl_ = new RepeatedField<OneofDescriptorProto>();
    private MessageOptions options_;
    private static readonly FieldCodec<DescriptorProto.Types.ReservedRange> _repeated_reservedRange_codec = FieldCodec.ForMessage<DescriptorProto.Types.ReservedRange>(74U, DescriptorProto.Types.ReservedRange.Parser);
    private readonly RepeatedField<DescriptorProto.Types.ReservedRange> reservedRange_ = new RepeatedField<DescriptorProto.Types.ReservedRange>();
    private static readonly FieldCodec<string> _repeated_reservedName_codec = FieldCodec.ForString(82U);
    private readonly RepeatedField<string> reservedName_ = new RepeatedField<string>();

    public static MessageParser<DescriptorProto> Parser => DescriptorProto._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[2];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => DescriptorProto.Descriptor;

    public DescriptorProto()
    {
    }

    public DescriptorProto(DescriptorProto other)
      : this()
    {
      this.name_ = other.name_;
      this.field_ = other.field_.Clone();
      this.extension_ = other.extension_.Clone();
      this.nestedType_ = other.nestedType_.Clone();
      this.enumType_ = other.enumType_.Clone();
      this.extensionRange_ = other.extensionRange_.Clone();
      this.oneofDecl_ = other.oneofDecl_.Clone();
      this.Options = other.options_ != null ? other.Options.Clone() : (MessageOptions) null;
      this.reservedRange_ = other.reservedRange_.Clone();
      this.reservedName_ = other.reservedName_.Clone();
    }

    public DescriptorProto Clone() => new DescriptorProto(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public RepeatedField<FieldDescriptorProto> Field => this.field_;

    public RepeatedField<FieldDescriptorProto> Extension => this.extension_;

    public RepeatedField<DescriptorProto> NestedType => this.nestedType_;

    public RepeatedField<EnumDescriptorProto> EnumType => this.enumType_;

    public RepeatedField<DescriptorProto.Types.ExtensionRange> ExtensionRange => this.extensionRange_;

    public RepeatedField<OneofDescriptorProto> OneofDecl => this.oneofDecl_;

    public MessageOptions Options
    {
      get => this.options_;
      set => this.options_ = value;
    }

    public RepeatedField<DescriptorProto.Types.ReservedRange> ReservedRange => this.reservedRange_;

    public RepeatedField<string> ReservedName => this.reservedName_;

    public override bool Equals(object other) => this.Equals(other as DescriptorProto);

    public bool Equals(DescriptorProto other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.field_.Equals(other.field_) && (this.extension_.Equals(other.extension_) && this.nestedType_.Equals(other.nestedType_)) && (this.enumType_.Equals(other.enumType_) && this.extensionRange_.Equals(other.extensionRange_) && (this.oneofDecl_.Equals(other.oneofDecl_) && object.Equals((object) this.Options, (object) other.Options))) && (this.reservedRange_.Equals(other.reservedRange_) && this.reservedName_.Equals(other.reservedName_)));

    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.Name.Length != 0)
        num1 ^= this.Name.GetHashCode();
      int num2 = num1 ^ this.field_.GetHashCode() ^ this.extension_.GetHashCode() ^ this.nestedType_.GetHashCode() ^ this.enumType_.GetHashCode() ^ this.extensionRange_.GetHashCode() ^ this.oneofDecl_.GetHashCode();
      if (this.options_ != null)
        num2 ^= this.Options.GetHashCode();
      return num2 ^ this.reservedRange_.GetHashCode() ^ this.reservedName_.GetHashCode();
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Name.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.Name);
      }
      this.field_.WriteTo(output, DescriptorProto._repeated_field_codec);
      this.nestedType_.WriteTo(output, DescriptorProto._repeated_nestedType_codec);
      this.enumType_.WriteTo(output, DescriptorProto._repeated_enumType_codec);
      this.extensionRange_.WriteTo(output, DescriptorProto._repeated_extensionRange_codec);
      this.extension_.WriteTo(output, DescriptorProto._repeated_extension_codec);
      if (this.options_ != null)
      {
        output.WriteRawTag((byte) 58);
        output.WriteMessage((IMessage) this.Options);
      }
      this.oneofDecl_.WriteTo(output, DescriptorProto._repeated_oneofDecl_codec);
      this.reservedRange_.WriteTo(output, DescriptorProto._repeated_reservedRange_codec);
      this.reservedName_.WriteTo(output, DescriptorProto._repeated_reservedName_codec);
    }

    public int CalculateSize()
    {
      int num1 = 0;
      if (this.Name.Length != 0)
        num1 += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      int num2 = num1 + this.field_.CalculateSize(DescriptorProto._repeated_field_codec) + this.extension_.CalculateSize(DescriptorProto._repeated_extension_codec) + this.nestedType_.CalculateSize(DescriptorProto._repeated_nestedType_codec) + this.enumType_.CalculateSize(DescriptorProto._repeated_enumType_codec) + this.extensionRange_.CalculateSize(DescriptorProto._repeated_extensionRange_codec) + this.oneofDecl_.CalculateSize(DescriptorProto._repeated_oneofDecl_codec);
      if (this.options_ != null)
        num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Options);
      return num2 + this.reservedRange_.CalculateSize(DescriptorProto._repeated_reservedRange_codec) + this.reservedName_.CalculateSize(DescriptorProto._repeated_reservedName_codec);
    }

    public void MergeFrom(DescriptorProto other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      this.field_.Add(other.field_);
      this.extension_.Add(other.extension_);
      this.nestedType_.Add(other.nestedType_);
      this.enumType_.Add(other.enumType_);
      this.extensionRange_.Add(other.extensionRange_);
      this.oneofDecl_.Add(other.oneofDecl_);
      if (other.options_ != null)
      {
        if (this.options_ == null)
          this.options_ = new MessageOptions();
        this.Options.MergeFrom(other.Options);
      }
      this.reservedRange_.Add(other.reservedRange_);
      this.reservedName_.Add(other.reservedName_);
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
            this.field_.AddEntriesFrom(input, DescriptorProto._repeated_field_codec);
            continue;
          case 26:
            this.nestedType_.AddEntriesFrom(input, DescriptorProto._repeated_nestedType_codec);
            continue;
          case 34:
            this.enumType_.AddEntriesFrom(input, DescriptorProto._repeated_enumType_codec);
            continue;
          case 42:
            this.extensionRange_.AddEntriesFrom(input, DescriptorProto._repeated_extensionRange_codec);
            continue;
          case 50:
            this.extension_.AddEntriesFrom(input, DescriptorProto._repeated_extension_codec);
            continue;
          case 58:
            if (this.options_ == null)
              this.options_ = new MessageOptions();
            input.ReadMessage((IMessage) this.options_);
            continue;
          case 66:
            this.oneofDecl_.AddEntriesFrom(input, DescriptorProto._repeated_oneofDecl_codec);
            continue;
          case 74:
            this.reservedRange_.AddEntriesFrom(input, DescriptorProto._repeated_reservedRange_codec);
            continue;
          case 82:
            this.reservedName_.AddEntriesFrom(input, DescriptorProto._repeated_reservedName_codec);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }

    [DebuggerNonUserCode]
    public static class Types
    {
      [DebuggerNonUserCode]
      internal sealed class ExtensionRange : 
        IMessage<DescriptorProto.Types.ExtensionRange>,
        IMessage,
        IEquatable<DescriptorProto.Types.ExtensionRange>,
        IDeepCloneable<DescriptorProto.Types.ExtensionRange>
      {
        public const int StartFieldNumber = 1;
        public const int EndFieldNumber = 2;
        private static readonly MessageParser<DescriptorProto.Types.ExtensionRange> _parser = new MessageParser<DescriptorProto.Types.ExtensionRange>((Func<DescriptorProto.Types.ExtensionRange>) (() => new DescriptorProto.Types.ExtensionRange()));
        private int start_;
        private int end_;

        public static MessageParser<DescriptorProto.Types.ExtensionRange> Parser => DescriptorProto.Types.ExtensionRange._parser;

        public static MessageDescriptor Descriptor => DescriptorProto.Descriptor.NestedTypes[0];

        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => DescriptorProto.Types.ExtensionRange.Descriptor;

        public ExtensionRange()
        {
        }

        public ExtensionRange(DescriptorProto.Types.ExtensionRange other)
          : this()
        {
          this.start_ = other.start_;
          this.end_ = other.end_;
        }

        public DescriptorProto.Types.ExtensionRange Clone() => new DescriptorProto.Types.ExtensionRange(this);

        public int Start
        {
          get => this.start_;
          set => this.start_ = value;
        }

        public int End
        {
          get => this.end_;
          set => this.end_ = value;
        }

        public override bool Equals(object other) => this.Equals(other as DescriptorProto.Types.ExtensionRange);

        public bool Equals(DescriptorProto.Types.ExtensionRange other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Start == other.Start && this.End == other.End);

        public override int GetHashCode()
        {
          int num = 1;
          if (this.Start != 0)
            num ^= this.Start.GetHashCode();
          if (this.End != 0)
            num ^= this.End.GetHashCode();
          return num;
        }

        public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

        public void WriteTo(CodedOutputStream output)
        {
          if (this.Start != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.Start);
          }
          if (this.End == 0)
            return;
          output.WriteRawTag((byte) 16);
          output.WriteInt32(this.End);
        }

        public int CalculateSize()
        {
          int num = 0;
          if (this.Start != 0)
            num += 1 + CodedOutputStream.ComputeInt32Size(this.Start);
          if (this.End != 0)
            num += 1 + CodedOutputStream.ComputeInt32Size(this.End);
          return num;
        }

        public void MergeFrom(DescriptorProto.Types.ExtensionRange other)
        {
          if (other == null)
            return;
          if (other.Start != 0)
            this.Start = other.Start;
          if (other.End == 0)
            return;
          this.End = other.End;
        }

        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.Start = input.ReadInt32();
                continue;
              case 16:
                this.End = input.ReadInt32();
                continue;
              default:
                input.SkipLastField();
                continue;
            }
          }
        }
      }

      [DebuggerNonUserCode]
      internal sealed class ReservedRange : 
        IMessage<DescriptorProto.Types.ReservedRange>,
        IMessage,
        IEquatable<DescriptorProto.Types.ReservedRange>,
        IDeepCloneable<DescriptorProto.Types.ReservedRange>
      {
        public const int StartFieldNumber = 1;
        public const int EndFieldNumber = 2;
        private static readonly MessageParser<DescriptorProto.Types.ReservedRange> _parser = new MessageParser<DescriptorProto.Types.ReservedRange>((Func<DescriptorProto.Types.ReservedRange>) (() => new DescriptorProto.Types.ReservedRange()));
        private int start_;
        private int end_;

        public static MessageParser<DescriptorProto.Types.ReservedRange> Parser => DescriptorProto.Types.ReservedRange._parser;

        public static MessageDescriptor Descriptor => DescriptorProto.Descriptor.NestedTypes[1];

        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => DescriptorProto.Types.ReservedRange.Descriptor;

        public ReservedRange()
        {
        }

        public ReservedRange(DescriptorProto.Types.ReservedRange other)
          : this()
        {
          this.start_ = other.start_;
          this.end_ = other.end_;
        }

        public DescriptorProto.Types.ReservedRange Clone() => new DescriptorProto.Types.ReservedRange(this);

        public int Start
        {
          get => this.start_;
          set => this.start_ = value;
        }

        public int End
        {
          get => this.end_;
          set => this.end_ = value;
        }

        public override bool Equals(object other) => this.Equals(other as DescriptorProto.Types.ReservedRange);

        public bool Equals(DescriptorProto.Types.ReservedRange other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Start == other.Start && this.End == other.End);

        public override int GetHashCode()
        {
          int num = 1;
          if (this.Start != 0)
            num ^= this.Start.GetHashCode();
          if (this.End != 0)
            num ^= this.End.GetHashCode();
          return num;
        }

        public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

        public void WriteTo(CodedOutputStream output)
        {
          if (this.Start != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.Start);
          }
          if (this.End == 0)
            return;
          output.WriteRawTag((byte) 16);
          output.WriteInt32(this.End);
        }

        public int CalculateSize()
        {
          int num = 0;
          if (this.Start != 0)
            num += 1 + CodedOutputStream.ComputeInt32Size(this.Start);
          if (this.End != 0)
            num += 1 + CodedOutputStream.ComputeInt32Size(this.End);
          return num;
        }

        public void MergeFrom(DescriptorProto.Types.ReservedRange other)
        {
          if (other == null)
            return;
          if (other.Start != 0)
            this.Start = other.Start;
          if (other.End == 0)
            return;
          this.End = other.End;
        }

        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.Start = input.ReadInt32();
                continue;
              case 16:
                this.End = input.ReadInt32();
                continue;
              default:
                input.SkipLastField();
                continue;
            }
          }
        }
      }
    }
  }
}
