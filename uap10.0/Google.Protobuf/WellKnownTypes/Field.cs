// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Field
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
  public sealed class Field : IMessage<Field>, IMessage, IEquatable<Field>, IDeepCloneable<Field>
  {
    public const int KindFieldNumber = 1;
    public const int CardinalityFieldNumber = 2;
    public const int NumberFieldNumber = 3;
    public const int NameFieldNumber = 4;
    public const int TypeUrlFieldNumber = 6;
    public const int OneofIndexFieldNumber = 7;
    public const int PackedFieldNumber = 8;
    public const int OptionsFieldNumber = 9;
    private static readonly MessageParser<Field> _parser = new MessageParser<Field>((Func<Field>) (() => new Field()));
    private Field.Types.Kind kind_;
    private Field.Types.Cardinality cardinality_;
    private int number_;
    private string name_ = "";
    private string typeUrl_ = "";
    private int oneofIndex_;
    private bool packed_;
    private static readonly FieldCodec<Option> _repeated_options_codec = FieldCodec.ForMessage<Option>(74U, Option.Parser);
    private readonly RepeatedField<Option> options_ = new RepeatedField<Option>();

    public static MessageParser<Field> Parser => Field._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Type.Descriptor.MessageTypes[1];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Field.Descriptor;

    public Field()
    {
    }

    public Field(Field other)
      : this()
    {
      this.kind_ = other.kind_;
      this.cardinality_ = other.cardinality_;
      this.number_ = other.number_;
      this.name_ = other.name_;
      this.typeUrl_ = other.typeUrl_;
      this.oneofIndex_ = other.oneofIndex_;
      this.packed_ = other.packed_;
      this.options_ = other.options_.Clone();
    }

    public Field Clone() => new Field(this);

    public Field.Types.Kind Kind
    {
      get => this.kind_;
      set => this.kind_ = value;
    }

    public Field.Types.Cardinality Cardinality
    {
      get => this.cardinality_;
      set => this.cardinality_ = value;
    }

    public int Number
    {
      get => this.number_;
      set => this.number_ = value;
    }

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string TypeUrl
    {
      get => this.typeUrl_;
      set => this.typeUrl_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public int OneofIndex
    {
      get => this.oneofIndex_;
      set => this.oneofIndex_ = value;
    }

    public bool Packed
    {
      get => this.packed_;
      set => this.packed_ = value;
    }

    public RepeatedField<Option> Options => this.options_;

    public override bool Equals(object other) => this.Equals(other as Field);

    public bool Equals(Field other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Kind == other.Kind && this.Cardinality == other.Cardinality && (this.Number == other.Number && !(this.Name != other.Name)) && (!(this.TypeUrl != other.TypeUrl) && this.OneofIndex == other.OneofIndex && (this.Packed == other.Packed && this.options_.Equals(other.options_))));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Kind != Field.Types.Kind.TYPE_UNKNOWN)
        num ^= this.Kind.GetHashCode();
      if (this.Cardinality != Field.Types.Cardinality.CARDINALITY_UNKNOWN)
        num ^= this.Cardinality.GetHashCode();
      if (this.Number != 0)
        num ^= this.Number.GetHashCode();
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      if (this.TypeUrl.Length != 0)
        num ^= this.TypeUrl.GetHashCode();
      if (this.OneofIndex != 0)
        num ^= this.OneofIndex.GetHashCode();
      if (this.Packed)
        num ^= this.Packed.GetHashCode();
      return num ^ this.options_.GetHashCode();
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Kind != Field.Types.Kind.TYPE_UNKNOWN)
      {
        output.WriteRawTag((byte) 8);
        output.WriteEnum((int) this.Kind);
      }
      if (this.Cardinality != Field.Types.Cardinality.CARDINALITY_UNKNOWN)
      {
        output.WriteRawTag((byte) 16);
        output.WriteEnum((int) this.Cardinality);
      }
      if (this.Number != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.Number);
      }
      if (this.Name.Length != 0)
      {
        output.WriteRawTag((byte) 34);
        output.WriteString(this.Name);
      }
      if (this.TypeUrl.Length != 0)
      {
        output.WriteRawTag((byte) 50);
        output.WriteString(this.TypeUrl);
      }
      if (this.OneofIndex != 0)
      {
        output.WriteRawTag((byte) 56);
        output.WriteInt32(this.OneofIndex);
      }
      if (this.Packed)
      {
        output.WriteRawTag((byte) 64);
        output.WriteBool(this.Packed);
      }
      this.options_.WriteTo(output, Field._repeated_options_codec);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Kind != Field.Types.Kind.TYPE_UNKNOWN)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.Kind);
      if (this.Cardinality != Field.Types.Cardinality.CARDINALITY_UNKNOWN)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.Cardinality);
      if (this.Number != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Number);
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.TypeUrl.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.TypeUrl);
      if (this.OneofIndex != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.OneofIndex);
      if (this.Packed)
        num += 2;
      return num + this.options_.CalculateSize(Field._repeated_options_codec);
    }

    public void MergeFrom(Field other)
    {
      if (other == null)
        return;
      if (other.Kind != Field.Types.Kind.TYPE_UNKNOWN)
        this.Kind = other.Kind;
      if (other.Cardinality != Field.Types.Cardinality.CARDINALITY_UNKNOWN)
        this.Cardinality = other.Cardinality;
      if (other.Number != 0)
        this.Number = other.Number;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.TypeUrl.Length != 0)
        this.TypeUrl = other.TypeUrl;
      if (other.OneofIndex != 0)
        this.OneofIndex = other.OneofIndex;
      if (other.Packed)
        this.Packed = other.Packed;
      this.options_.Add(other.options_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 8:
            this.kind_ = (Field.Types.Kind) input.ReadEnum();
            continue;
          case 16:
            this.cardinality_ = (Field.Types.Cardinality) input.ReadEnum();
            continue;
          case 24:
            this.Number = input.ReadInt32();
            continue;
          case 34:
            this.Name = input.ReadString();
            continue;
          case 50:
            this.TypeUrl = input.ReadString();
            continue;
          case 56:
            this.OneofIndex = input.ReadInt32();
            continue;
          case 64:
            this.Packed = input.ReadBool();
            continue;
          case 74:
            this.options_.AddEntriesFrom(input, Field._repeated_options_codec);
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
      public enum Kind
      {
        TYPE_UNKNOWN = 0,
        TYPE_DOUBLE = 1,
        TYPE_FLOAT = 2,
        TYPE_INT64 = 3,
        TYPE_UINT64 = 4,
        TYPE_INT32 = 5,
        TYPE_FIXED64 = 6,
        TYPE_FIXED32 = 7,
        TYPE_BOOL = 8,
        TYPE_STRING = 9,
        TYPE_MESSAGE = 11, // 0x0000000B
        TYPE_BYTES = 12, // 0x0000000C
        TYPE_UINT32 = 13, // 0x0000000D
        TYPE_ENUM = 14, // 0x0000000E
        TYPE_SFIXED32 = 15, // 0x0000000F
        TYPE_SFIXED64 = 16, // 0x00000010
        TYPE_SINT32 = 17, // 0x00000011
        TYPE_SINT64 = 18, // 0x00000012
      }

      public enum Cardinality
      {
        CARDINALITY_UNKNOWN,
        CARDINALITY_OPTIONAL,
        CARDINALITY_REQUIRED,
        CARDINALITY_REPEATED,
      }
    }
  }
}
