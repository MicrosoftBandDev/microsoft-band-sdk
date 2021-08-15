// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.MessageDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Google.Protobuf.Reflection
{
  public sealed class MessageDescriptor : DescriptorBase
  {
    private static readonly HashSet<string> WellKnownTypeNames = new HashSet<string>()
    {
      "google/protobuf/any.proto",
      "google/protobuf/api.proto",
      "google/protobuf/duration.proto",
      "google/protobuf/empty.proto",
      "google/protobuf/wrappers.proto",
      "google/protobuf/timestamp.proto",
      "google/protobuf/field_mask.proto",
      "google/protobuf/source_context.proto",
      "google/protobuf/struct.proto",
      "google/protobuf/type.proto"
    };
    private readonly DescriptorProto proto;
    private readonly MessageDescriptor containingType;
    private readonly IList<MessageDescriptor> nestedTypes;
    private readonly IList<EnumDescriptor> enumTypes;
    private readonly IList<FieldDescriptor> fieldsInDeclarationOrder;
    private readonly IList<FieldDescriptor> fieldsInNumberOrder;
    private readonly MessageDescriptor.FieldCollection fields;
    private readonly IList<OneofDescriptor> oneofs;
    private readonly System.Type generatedType;

    internal MessageDescriptor(
      DescriptorProto proto,
      FileDescriptor file,
      MessageDescriptor parent,
      int typeIndex,
      GeneratedCodeInfo generatedCodeInfo)
      : base(file, file.ComputeFullName(parent, proto.Name), typeIndex)
    {
      MessageDescriptor parent1 = this;
      this.proto = proto;
      this.generatedType = generatedCodeInfo == null ? (System.Type) null : generatedCodeInfo.ClrType;
      this.containingType = parent;
      this.oneofs = DescriptorUtil.ConvertAndMakeReadOnly<OneofDescriptorProto, OneofDescriptor>((IList<OneofDescriptorProto>) proto.OneofDecl, (DescriptorUtil.IndexedConverter<OneofDescriptorProto, OneofDescriptor>) ((oneof, index) => new OneofDescriptor(oneof, file, parent1, index, generatedCodeInfo == null ? (string) null : generatedCodeInfo.OneofNames[index])));
      this.nestedTypes = DescriptorUtil.ConvertAndMakeReadOnly<DescriptorProto, MessageDescriptor>((IList<DescriptorProto>) proto.NestedType, (DescriptorUtil.IndexedConverter<DescriptorProto, MessageDescriptor>) ((type, index) => new MessageDescriptor(type, file, parent1, index, generatedCodeInfo == null ? (GeneratedCodeInfo) null : generatedCodeInfo.NestedTypes[index])));
      this.enumTypes = DescriptorUtil.ConvertAndMakeReadOnly<EnumDescriptorProto, EnumDescriptor>((IList<EnumDescriptorProto>) proto.EnumType, (DescriptorUtil.IndexedConverter<EnumDescriptorProto, EnumDescriptor>) ((type, index) => new EnumDescriptor(type, file, parent1, index, generatedCodeInfo == null ? (System.Type) null : generatedCodeInfo.NestedEnums[index])));
      this.fieldsInDeclarationOrder = DescriptorUtil.ConvertAndMakeReadOnly<FieldDescriptorProto, FieldDescriptor>((IList<FieldDescriptorProto>) proto.Field, (DescriptorUtil.IndexedConverter<FieldDescriptorProto, FieldDescriptor>) ((field, index) => new FieldDescriptor(field, file, parent1, index, generatedCodeInfo == null ? (string) null : generatedCodeInfo.PropertyNames[index])));
      this.fieldsInNumberOrder = (IList<FieldDescriptor>) new ReadOnlyCollection<FieldDescriptor>((IList<FieldDescriptor>) this.fieldsInDeclarationOrder.OrderBy<FieldDescriptor, int>((Func<FieldDescriptor, int>) (field => field.FieldNumber)).ToArray<FieldDescriptor>());
      file.DescriptorPool.AddSymbol((IDescriptor) this);
      this.fields = new MessageDescriptor.FieldCollection(this);
    }

    private int CountTotalGeneratedTypes() => this.nestedTypes.Sum<MessageDescriptor>((Func<MessageDescriptor, int>) (nested => nested.CountTotalGeneratedTypes())) + this.enumTypes.Count;

    public override string Name => this.proto.Name;

    internal DescriptorProto Proto => this.proto;

    public System.Type GeneratedType => this.generatedType;

    internal bool IsWellKnownType => this.File.Package == "google.protobuf" && MessageDescriptor.WellKnownTypeNames.Contains(this.File.Name);

    public MessageDescriptor ContainingType => this.containingType;

    public MessageDescriptor.FieldCollection Fields => this.fields;

    public IList<MessageDescriptor> NestedTypes => this.nestedTypes;

    public IList<EnumDescriptor> EnumTypes => this.enumTypes;

    public IList<OneofDescriptor> Oneofs => this.oneofs;

    public FieldDescriptor FindFieldByName(string name) => this.File.DescriptorPool.FindSymbol<FieldDescriptor>(this.FullName + "." + name);

    public FieldDescriptor FindFieldByNumber(int number) => this.File.DescriptorPool.FindFieldByNumber(this, number);

    public T FindDescriptor<T>(string name) where T : class, IDescriptor => this.File.DescriptorPool.FindSymbol<T>(this.FullName + "." + name);

    internal void CrossLink()
    {
      foreach (MessageDescriptor nestedType in (IEnumerable<MessageDescriptor>) this.nestedTypes)
        nestedType.CrossLink();
      foreach (FieldDescriptor fieldDescriptor in (IEnumerable<FieldDescriptor>) this.fieldsInDeclarationOrder)
        fieldDescriptor.CrossLink();
      foreach (OneofDescriptor oneof in (IEnumerable<OneofDescriptor>) this.oneofs)
        oneof.CrossLink();
    }

    public sealed class FieldCollection
    {
      private readonly MessageDescriptor messageDescriptor;

      internal FieldCollection(MessageDescriptor messageDescriptor) => this.messageDescriptor = messageDescriptor;

      public IList<FieldDescriptor> InDeclarationOrder() => this.messageDescriptor.fieldsInDeclarationOrder;

      public IList<FieldDescriptor> InFieldNumberOrder() => this.messageDescriptor.fieldsInNumberOrder;

      public FieldDescriptor this[int number] => this.messageDescriptor.FindFieldByNumber(number) ?? throw new KeyNotFoundException("No such field number");

      public FieldDescriptor this[string name] => this.messageDescriptor.FindFieldByName(name) ?? throw new KeyNotFoundException("No such field name");
    }
  }
}
