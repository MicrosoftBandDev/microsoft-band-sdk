// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.FieldDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Compatibility;
using System;
using System.Reflection;

namespace Google.Protobuf.Reflection
{
  public sealed class FieldDescriptor : DescriptorBase, IComparable<FieldDescriptor>
  {
    private readonly FieldDescriptorProto proto;
    private EnumDescriptor enumType;
    private MessageDescriptor messageType;
    private readonly MessageDescriptor containingType;
    private readonly OneofDescriptor containingOneof;
    private FieldType fieldType;
    private readonly string propertyName;
    private IFieldAccessor accessor;

    internal FieldDescriptor(
      FieldDescriptorProto proto,
      FileDescriptor file,
      MessageDescriptor parent,
      int index,
      string propertyName)
      : base(file, file.ComputeFullName(parent, proto.Name), index)
    {
      this.proto = proto;
      if (proto.Type != (FieldDescriptorProto.Types.Type) 0)
        this.fieldType = FieldDescriptor.GetFieldTypeFromProtoType(proto.Type);
      if (this.FieldNumber <= 0)
        throw new DescriptorValidationException((IDescriptor) this, "Field numbers must be positive integers.");
      this.containingType = parent;
      if (proto.OneofIndex != -1)
      {
        if (proto.OneofIndex < 0 || proto.OneofIndex >= parent.Proto.OneofDecl.Count)
          throw new DescriptorValidationException((IDescriptor) this, "FieldDescriptorProto.oneof_index is out of range for type " + parent.Name);
        this.containingOneof = parent.Oneofs[proto.OneofIndex];
      }
      file.DescriptorPool.AddSymbol((IDescriptor) this);
      this.propertyName = propertyName;
    }

    public override string Name => this.proto.Name;

    internal FieldDescriptorProto Proto => this.proto;

    public IFieldAccessor Accessor => this.accessor;

    private static FieldType GetFieldTypeFromProtoType(FieldDescriptorProto.Types.Type type)
    {
      switch (type)
      {
        case FieldDescriptorProto.Types.Type.TYPE_DOUBLE:
          return FieldType.Double;
        case FieldDescriptorProto.Types.Type.TYPE_FLOAT:
          return FieldType.Float;
        case FieldDescriptorProto.Types.Type.TYPE_INT64:
          return FieldType.Int64;
        case FieldDescriptorProto.Types.Type.TYPE_UINT64:
          return FieldType.UInt64;
        case FieldDescriptorProto.Types.Type.TYPE_INT32:
          return FieldType.Int32;
        case FieldDescriptorProto.Types.Type.TYPE_FIXED64:
          return FieldType.Fixed64;
        case FieldDescriptorProto.Types.Type.TYPE_FIXED32:
          return FieldType.Fixed32;
        case FieldDescriptorProto.Types.Type.TYPE_BOOL:
          return FieldType.Bool;
        case FieldDescriptorProto.Types.Type.TYPE_STRING:
          return FieldType.String;
        case FieldDescriptorProto.Types.Type.TYPE_GROUP:
          return FieldType.Group;
        case FieldDescriptorProto.Types.Type.TYPE_MESSAGE:
          return FieldType.Message;
        case FieldDescriptorProto.Types.Type.TYPE_BYTES:
          return FieldType.Bytes;
        case FieldDescriptorProto.Types.Type.TYPE_UINT32:
          return FieldType.UInt32;
        case FieldDescriptorProto.Types.Type.TYPE_ENUM:
          return FieldType.Enum;
        case FieldDescriptorProto.Types.Type.TYPE_SFIXED32:
          return FieldType.SFixed32;
        case FieldDescriptorProto.Types.Type.TYPE_SFIXED64:
          return FieldType.SFixed64;
        case FieldDescriptorProto.Types.Type.TYPE_SINT32:
          return FieldType.SInt32;
        case FieldDescriptorProto.Types.Type.TYPE_SINT64:
          return FieldType.SInt64;
        default:
          throw new ArgumentException("Invalid type specified");
      }
    }

    public bool IsRepeated => this.Proto.Label == FieldDescriptorProto.Types.Label.LABEL_REPEATED;

    public bool IsMap => this.fieldType == FieldType.Message && this.messageType.Proto.Options != null && this.messageType.Proto.Options.MapEntry;

    public bool IsPacked => this.Proto.Options == null || this.Proto.Options.Packed;

    public MessageDescriptor ContainingType => this.containingType;

    public OneofDescriptor ContainingOneof => this.containingOneof;

    public FieldType FieldType => this.fieldType;

    public int FieldNumber => this.Proto.Number;

    public int CompareTo(FieldDescriptor other)
    {
      if (other.containingType != this.containingType)
        throw new ArgumentException("FieldDescriptors can only be compared to other FieldDescriptors for fields of the same message type.");
      return this.FieldNumber - other.FieldNumber;
    }

    public EnumDescriptor EnumType
    {
      get
      {
        if (this.fieldType != FieldType.Enum)
          throw new InvalidOperationException("EnumType is only valid for enum fields.");
        return this.enumType;
      }
    }

    public MessageDescriptor MessageType
    {
      get
      {
        if (this.fieldType != FieldType.Message)
          throw new InvalidOperationException("MessageType is only valid for enum fields.");
        return this.messageType;
      }
    }

    internal void CrossLink()
    {
      if (this.Proto.TypeName != "")
      {
        IDescriptor descriptor = this.File.DescriptorPool.LookupSymbol(this.Proto.TypeName, (IDescriptor) this);
        if (this.Proto.Type != (FieldDescriptorProto.Types.Type) 0)
        {
          switch (descriptor)
          {
            case MessageDescriptor _:
              this.fieldType = FieldType.Message;
              break;
            case EnumDescriptor _:
              this.fieldType = FieldType.Enum;
              break;
            default:
              throw new DescriptorValidationException((IDescriptor) this, "\"" + this.Proto.TypeName + "\" is not a type.");
          }
        }
        if (this.fieldType == FieldType.Message)
        {
          this.messageType = descriptor is MessageDescriptor ? (MessageDescriptor) descriptor : throw new DescriptorValidationException((IDescriptor) this, "\"" + this.Proto.TypeName + "\" is not a message type.");
          if (this.Proto.DefaultValue != "")
            throw new DescriptorValidationException((IDescriptor) this, "Messages can't have default values.");
        }
        else
        {
          if (this.fieldType != FieldType.Enum)
            throw new DescriptorValidationException((IDescriptor) this, "Field with primitive type has type_name.");
          this.enumType = descriptor is EnumDescriptor ? (EnumDescriptor) descriptor : throw new DescriptorValidationException((IDescriptor) this, "\"" + this.Proto.TypeName + "\" is not an enum type.");
        }
      }
      else if (this.fieldType == FieldType.Message || this.fieldType == FieldType.Enum)
        throw new DescriptorValidationException((IDescriptor) this, "Field with message or enum type missing type_name.");
      this.File.DescriptorPool.AddFieldByNumber(this);
      if (this.containingType != null && this.containingType.Proto.Options != null && this.containingType.Proto.Options.MessageSetWireFormat)
        throw new DescriptorValidationException((IDescriptor) this, "MessageSet format is not supported.");
      this.accessor = this.CreateAccessor(this.propertyName);
    }

    private IFieldAccessor CreateAccessor(string propertyName)
    {
      if (this.containingType.GeneratedType == null || propertyName == null)
        return (IFieldAccessor) null;
      PropertyInfo property = this.containingType.GeneratedType.GetProperty(propertyName);
      if (property == null)
        throw new DescriptorValidationException((IDescriptor) this, "Property " + propertyName + " not found in " + (object) this.containingType.GeneratedType);
      if (this.IsMap)
        return (IFieldAccessor) new MapFieldAccessor(property, this);
      return !this.IsRepeated ? (IFieldAccessor) new SingleFieldAccessor(property, this) : (IFieldAccessor) new RepeatedFieldAccessor(property, this);
    }
  }
}
