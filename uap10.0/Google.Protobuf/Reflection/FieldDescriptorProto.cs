// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.FieldDescriptorProto
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class FieldDescriptorProto : 
    IMessage<FieldDescriptorProto>,
    IMessage,
    IEquatable<FieldDescriptorProto>,
    IDeepCloneable<FieldDescriptorProto>
  {
    public const int NameFieldNumber = 1;
    public const int NumberFieldNumber = 3;
    public const int LabelFieldNumber = 4;
    public const int TypeFieldNumber = 5;
    public const int TypeNameFieldNumber = 6;
    public const int ExtendeeFieldNumber = 2;
    public const int DefaultValueFieldNumber = 7;
    public const int OneofIndexFieldNumber = 9;
    public const int OptionsFieldNumber = 8;
    private static readonly MessageParser<FieldDescriptorProto> _parser = new MessageParser<FieldDescriptorProto>((Func<FieldDescriptorProto>) (() => new FieldDescriptorProto()));
    private string name_ = "";
    private int number_;
    private FieldDescriptorProto.Types.Label label_ = FieldDescriptorProto.Types.Label.LABEL_OPTIONAL;
    private FieldDescriptorProto.Types.Type type_ = FieldDescriptorProto.Types.Type.TYPE_DOUBLE;
    private string typeName_ = "";
    private string extendee_ = "";
    private string defaultValue_ = "";
    private int oneofIndex_;
    private FieldOptions options_;

    public static MessageParser<FieldDescriptorProto> Parser => FieldDescriptorProto._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[3];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FieldDescriptorProto.Descriptor;

    public FieldDescriptorProto() => this.OnConstruction();

    public FieldDescriptorProto(FieldDescriptorProto other)
      : this()
    {
      this.name_ = other.name_;
      this.number_ = other.number_;
      this.label_ = other.label_;
      this.type_ = other.type_;
      this.typeName_ = other.typeName_;
      this.extendee_ = other.extendee_;
      this.defaultValue_ = other.defaultValue_;
      this.oneofIndex_ = other.oneofIndex_;
      this.Options = other.options_ != null ? other.Options.Clone() : (FieldOptions) null;
    }

    public FieldDescriptorProto Clone() => new FieldDescriptorProto(this);

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

    public FieldDescriptorProto.Types.Label Label
    {
      get => this.label_;
      set => this.label_ = value;
    }

    public FieldDescriptorProto.Types.Type Type
    {
      get => this.type_;
      set => this.type_ = value;
    }

    public string TypeName
    {
      get => this.typeName_;
      set => this.typeName_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string Extendee
    {
      get => this.extendee_;
      set => this.extendee_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string DefaultValue
    {
      get => this.defaultValue_;
      set => this.defaultValue_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public int OneofIndex
    {
      get => this.oneofIndex_;
      set => this.oneofIndex_ = value;
    }

    public FieldOptions Options
    {
      get => this.options_;
      set => this.options_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as FieldDescriptorProto);

    public bool Equals(FieldDescriptorProto other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.Number == other.Number && (this.Label == other.Label && this.Type == other.Type) && (!(this.TypeName != other.TypeName) && !(this.Extendee != other.Extendee) && (!(this.DefaultValue != other.DefaultValue) && this.OneofIndex == other.OneofIndex)) && object.Equals((object) this.Options, (object) other.Options));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      if (this.Number != 0)
        num ^= this.Number.GetHashCode();
      if (this.Label != FieldDescriptorProto.Types.Label.LABEL_OPTIONAL)
        num ^= this.Label.GetHashCode();
      if (this.Type != FieldDescriptorProto.Types.Type.TYPE_DOUBLE)
        num ^= this.Type.GetHashCode();
      if (this.TypeName.Length != 0)
        num ^= this.TypeName.GetHashCode();
      if (this.Extendee.Length != 0)
        num ^= this.Extendee.GetHashCode();
      if (this.DefaultValue.Length != 0)
        num ^= this.DefaultValue.GetHashCode();
      if (this.OneofIndex != 0)
        num ^= this.OneofIndex.GetHashCode();
      if (this.options_ != null)
        num ^= this.Options.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Name.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.Name);
      }
      if (this.Extendee.Length != 0)
      {
        output.WriteRawTag((byte) 18);
        output.WriteString(this.Extendee);
      }
      if (this.Number != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.Number);
      }
      if (this.Label != FieldDescriptorProto.Types.Label.LABEL_OPTIONAL)
      {
        output.WriteRawTag((byte) 32);
        output.WriteEnum((int) this.Label);
      }
      if (this.Type != FieldDescriptorProto.Types.Type.TYPE_DOUBLE)
      {
        output.WriteRawTag((byte) 40);
        output.WriteEnum((int) this.Type);
      }
      if (this.TypeName.Length != 0)
      {
        output.WriteRawTag((byte) 50);
        output.WriteString(this.TypeName);
      }
      if (this.DefaultValue.Length != 0)
      {
        output.WriteRawTag((byte) 58);
        output.WriteString(this.DefaultValue);
      }
      if (this.options_ != null)
      {
        output.WriteRawTag((byte) 66);
        output.WriteMessage((IMessage) this.Options);
      }
      if (this.OneofIndex == 0)
        return;
      output.WriteRawTag((byte) 72);
      output.WriteInt32(this.OneofIndex);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.Number != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Number);
      if (this.Label != FieldDescriptorProto.Types.Label.LABEL_OPTIONAL)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.Label);
      if (this.Type != FieldDescriptorProto.Types.Type.TYPE_DOUBLE)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.Type);
      if (this.TypeName.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.TypeName);
      if (this.Extendee.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Extendee);
      if (this.DefaultValue.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.DefaultValue);
      if (this.OneofIndex != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.OneofIndex);
      if (this.options_ != null)
        num += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Options);
      return num;
    }

    public void MergeFrom(FieldDescriptorProto other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.Number != 0)
        this.Number = other.Number;
      if (other.Label != FieldDescriptorProto.Types.Label.LABEL_OPTIONAL)
        this.Label = other.Label;
      if (other.Type != FieldDescriptorProto.Types.Type.TYPE_DOUBLE)
        this.Type = other.Type;
      if (other.TypeName.Length != 0)
        this.TypeName = other.TypeName;
      if (other.Extendee.Length != 0)
        this.Extendee = other.Extendee;
      if (other.DefaultValue.Length != 0)
        this.DefaultValue = other.DefaultValue;
      if (other.OneofIndex != 0)
        this.OneofIndex = other.OneofIndex;
      if (other.options_ == null)
        return;
      if (this.options_ == null)
        this.options_ = new FieldOptions();
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
            this.Extendee = input.ReadString();
            continue;
          case 24:
            this.Number = input.ReadInt32();
            continue;
          case 32:
            this.label_ = (FieldDescriptorProto.Types.Label) input.ReadEnum();
            continue;
          case 40:
            this.type_ = (FieldDescriptorProto.Types.Type) input.ReadEnum();
            continue;
          case 50:
            this.TypeName = input.ReadString();
            continue;
          case 58:
            this.DefaultValue = input.ReadString();
            continue;
          case 66:
            if (this.options_ == null)
              this.options_ = new FieldOptions();
            input.ReadMessage((IMessage) this.options_);
            continue;
          case 72:
            this.OneofIndex = input.ReadInt32();
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }

    private void OnConstruction() => this.OneofIndex = -1;

    [DebuggerNonUserCode]
    public static class Types
    {
      internal enum Type
      {
        TYPE_DOUBLE = 1,
        TYPE_FLOAT = 2,
        TYPE_INT64 = 3,
        TYPE_UINT64 = 4,
        TYPE_INT32 = 5,
        TYPE_FIXED64 = 6,
        TYPE_FIXED32 = 7,
        TYPE_BOOL = 8,
        TYPE_STRING = 9,
        TYPE_GROUP = 10, // 0x0000000A
        TYPE_MESSAGE = 11, // 0x0000000B
        TYPE_BYTES = 12, // 0x0000000C
        TYPE_UINT32 = 13, // 0x0000000D
        TYPE_ENUM = 14, // 0x0000000E
        TYPE_SFIXED32 = 15, // 0x0000000F
        TYPE_SFIXED64 = 16, // 0x00000010
        TYPE_SINT32 = 17, // 0x00000011
        TYPE_SINT64 = 18, // 0x00000012
      }

      internal enum Label
      {
        LABEL_OPTIONAL = 1,
        LABEL_REQUIRED = 2,
        LABEL_REPEATED = 3,
      }
    }
  }
}
