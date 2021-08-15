// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.UninterpretedOption
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class UninterpretedOption : 
    IMessage<UninterpretedOption>,
    IMessage,
    IEquatable<UninterpretedOption>,
    IDeepCloneable<UninterpretedOption>
  {
    public const int NameFieldNumber = 2;
    public const int IdentifierValueFieldNumber = 3;
    public const int PositiveIntValueFieldNumber = 4;
    public const int NegativeIntValueFieldNumber = 5;
    public const int DoubleValueFieldNumber = 6;
    public const int StringValueFieldNumber = 7;
    public const int AggregateValueFieldNumber = 8;
    private static readonly MessageParser<UninterpretedOption> _parser = new MessageParser<UninterpretedOption>((Func<UninterpretedOption>) (() => new UninterpretedOption()));
    private static readonly FieldCodec<UninterpretedOption.Types.NamePart> _repeated_name_codec = FieldCodec.ForMessage<UninterpretedOption.Types.NamePart>(18U, UninterpretedOption.Types.NamePart.Parser);
    private readonly RepeatedField<UninterpretedOption.Types.NamePart> name_ = new RepeatedField<UninterpretedOption.Types.NamePart>();
    private string identifierValue_ = "";
    private ulong positiveIntValue_;
    private long negativeIntValue_;
    private double doubleValue_;
    private ByteString stringValue_ = ByteString.Empty;
    private string aggregateValue_ = "";

    public static MessageParser<UninterpretedOption> Parser => UninterpretedOption._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[16];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => UninterpretedOption.Descriptor;

    public UninterpretedOption()
    {
    }

    public UninterpretedOption(UninterpretedOption other)
      : this()
    {
      this.name_ = other.name_.Clone();
      this.identifierValue_ = other.identifierValue_;
      this.positiveIntValue_ = other.positiveIntValue_;
      this.negativeIntValue_ = other.negativeIntValue_;
      this.doubleValue_ = other.doubleValue_;
      this.stringValue_ = other.stringValue_;
      this.aggregateValue_ = other.aggregateValue_;
    }

    public UninterpretedOption Clone() => new UninterpretedOption(this);

    public RepeatedField<UninterpretedOption.Types.NamePart> Name => this.name_;

    public string IdentifierValue
    {
      get => this.identifierValue_;
      set => this.identifierValue_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public ulong PositiveIntValue
    {
      get => this.positiveIntValue_;
      set => this.positiveIntValue_ = value;
    }

    public long NegativeIntValue
    {
      get => this.negativeIntValue_;
      set => this.negativeIntValue_ = value;
    }

    public double DoubleValue
    {
      get => this.doubleValue_;
      set => this.doubleValue_ = value;
    }

    public ByteString StringValue
    {
      get => this.stringValue_;
      set => this.stringValue_ = Preconditions.CheckNotNull<ByteString>(value, nameof (value));
    }

    public string AggregateValue
    {
      get => this.aggregateValue_;
      set => this.aggregateValue_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public override bool Equals(object other) => this.Equals(other as UninterpretedOption);

    public bool Equals(UninterpretedOption other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.name_.Equals(other.name_) && !(this.IdentifierValue != other.IdentifierValue) && ((long) this.PositiveIntValue == (long) other.PositiveIntValue && this.NegativeIntValue == other.NegativeIntValue) && (this.DoubleValue == other.DoubleValue && !(this.StringValue != other.StringValue) && !(this.AggregateValue != other.AggregateValue)));

    public override int GetHashCode()
    {
      int num = 1 ^ this.name_.GetHashCode();
      if (this.IdentifierValue.Length != 0)
        num ^= this.IdentifierValue.GetHashCode();
      if (this.PositiveIntValue != 0UL)
        num ^= this.PositiveIntValue.GetHashCode();
      if (this.NegativeIntValue != 0L)
        num ^= this.NegativeIntValue.GetHashCode();
      if (this.DoubleValue != 0.0)
        num ^= this.DoubleValue.GetHashCode();
      if (this.StringValue.Length != 0)
        num ^= this.StringValue.GetHashCode();
      if (this.AggregateValue.Length != 0)
        num ^= this.AggregateValue.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      this.name_.WriteTo(output, UninterpretedOption._repeated_name_codec);
      if (this.IdentifierValue.Length != 0)
      {
        output.WriteRawTag((byte) 26);
        output.WriteString(this.IdentifierValue);
      }
      if (this.PositiveIntValue != 0UL)
      {
        output.WriteRawTag((byte) 32);
        output.WriteUInt64(this.PositiveIntValue);
      }
      if (this.NegativeIntValue != 0L)
      {
        output.WriteRawTag((byte) 40);
        output.WriteInt64(this.NegativeIntValue);
      }
      if (this.DoubleValue != 0.0)
      {
        output.WriteRawTag((byte) 49);
        output.WriteDouble(this.DoubleValue);
      }
      if (this.StringValue.Length != 0)
      {
        output.WriteRawTag((byte) 58);
        output.WriteBytes(this.StringValue);
      }
      if (this.AggregateValue.Length == 0)
        return;
      output.WriteRawTag((byte) 66);
      output.WriteString(this.AggregateValue);
    }

    public int CalculateSize()
    {
      int num = 0 + this.name_.CalculateSize(UninterpretedOption._repeated_name_codec);
      if (this.IdentifierValue.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.IdentifierValue);
      if (this.PositiveIntValue != 0UL)
        num += 1 + CodedOutputStream.ComputeUInt64Size(this.PositiveIntValue);
      if (this.NegativeIntValue != 0L)
        num += 1 + CodedOutputStream.ComputeInt64Size(this.NegativeIntValue);
      if (this.DoubleValue != 0.0)
        num += 9;
      if (this.StringValue.Length != 0)
        num += 1 + CodedOutputStream.ComputeBytesSize(this.StringValue);
      if (this.AggregateValue.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.AggregateValue);
      return num;
    }

    public void MergeFrom(UninterpretedOption other)
    {
      if (other == null)
        return;
      this.name_.Add(other.name_);
      if (other.IdentifierValue.Length != 0)
        this.IdentifierValue = other.IdentifierValue;
      if (other.PositiveIntValue != 0UL)
        this.PositiveIntValue = other.PositiveIntValue;
      if (other.NegativeIntValue != 0L)
        this.NegativeIntValue = other.NegativeIntValue;
      if (other.DoubleValue != 0.0)
        this.DoubleValue = other.DoubleValue;
      if (other.StringValue.Length != 0)
        this.StringValue = other.StringValue;
      if (other.AggregateValue.Length == 0)
        return;
      this.AggregateValue = other.AggregateValue;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 18:
            this.name_.AddEntriesFrom(input, UninterpretedOption._repeated_name_codec);
            continue;
          case 26:
            this.IdentifierValue = input.ReadString();
            continue;
          case 32:
            this.PositiveIntValue = input.ReadUInt64();
            continue;
          case 40:
            this.NegativeIntValue = input.ReadInt64();
            continue;
          case 49:
            this.DoubleValue = input.ReadDouble();
            continue;
          case 58:
            this.StringValue = input.ReadBytes();
            continue;
          case 66:
            this.AggregateValue = input.ReadString();
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
      internal sealed class NamePart : 
        IMessage<UninterpretedOption.Types.NamePart>,
        IMessage,
        IEquatable<UninterpretedOption.Types.NamePart>,
        IDeepCloneable<UninterpretedOption.Types.NamePart>
      {
        public const int NamePart_FieldNumber = 1;
        public const int IsExtensionFieldNumber = 2;
        private static readonly MessageParser<UninterpretedOption.Types.NamePart> _parser = new MessageParser<UninterpretedOption.Types.NamePart>((Func<UninterpretedOption.Types.NamePart>) (() => new UninterpretedOption.Types.NamePart()));
        private string namePart_ = "";
        private bool isExtension_;

        public static MessageParser<UninterpretedOption.Types.NamePart> Parser => UninterpretedOption.Types.NamePart._parser;

        public static MessageDescriptor Descriptor => UninterpretedOption.Descriptor.NestedTypes[0];

        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => UninterpretedOption.Types.NamePart.Descriptor;

        public NamePart()
        {
        }

        public NamePart(UninterpretedOption.Types.NamePart other)
          : this()
        {
          this.namePart_ = other.namePart_;
          this.isExtension_ = other.isExtension_;
        }

        public UninterpretedOption.Types.NamePart Clone() => new UninterpretedOption.Types.NamePart(this);

        public string NamePart_
        {
          get => this.namePart_;
          set => this.namePart_ = Preconditions.CheckNotNull<string>(value, nameof (value));
        }

        public bool IsExtension
        {
          get => this.isExtension_;
          set => this.isExtension_ = value;
        }

        public override bool Equals(object other) => this.Equals(other as UninterpretedOption.Types.NamePart);

        public bool Equals(UninterpretedOption.Types.NamePart other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.NamePart_ != other.NamePart_) && this.IsExtension == other.IsExtension);

        public override int GetHashCode()
        {
          int num = 1;
          if (this.NamePart_.Length != 0)
            num ^= this.NamePart_.GetHashCode();
          if (this.IsExtension)
            num ^= this.IsExtension.GetHashCode();
          return num;
        }

        public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

        public void WriteTo(CodedOutputStream output)
        {
          if (this.NamePart_.Length != 0)
          {
            output.WriteRawTag((byte) 10);
            output.WriteString(this.NamePart_);
          }
          if (!this.IsExtension)
            return;
          output.WriteRawTag((byte) 16);
          output.WriteBool(this.IsExtension);
        }

        public int CalculateSize()
        {
          int num = 0;
          if (this.NamePart_.Length != 0)
            num += 1 + CodedOutputStream.ComputeStringSize(this.NamePart_);
          if (this.IsExtension)
            num += 2;
          return num;
        }

        public void MergeFrom(UninterpretedOption.Types.NamePart other)
        {
          if (other == null)
            return;
          if (other.NamePart_.Length != 0)
            this.NamePart_ = other.NamePart_;
          if (!other.IsExtension)
            return;
          this.IsExtension = other.IsExtension;
        }

        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 10:
                this.NamePart_ = input.ReadString();
                continue;
              case 16:
                this.IsExtension = input.ReadBool();
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
