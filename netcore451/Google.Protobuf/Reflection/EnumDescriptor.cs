// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.EnumDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System.Collections.Generic;

namespace Google.Protobuf.Reflection
{
  public sealed class EnumDescriptor : DescriptorBase
  {
    private readonly EnumDescriptorProto proto;
    private readonly MessageDescriptor containingType;
    private readonly IList<EnumValueDescriptor> values;
    private readonly System.Type generatedType;

    internal EnumDescriptor(
      EnumDescriptorProto proto,
      FileDescriptor file,
      MessageDescriptor parent,
      int index,
      System.Type generatedType)
      : base(file, file.ComputeFullName(parent, proto.Name), index)
    {
      EnumDescriptor parent1 = this;
      this.proto = proto;
      this.generatedType = generatedType;
      this.containingType = parent;
      if (proto.Value.Count == 0)
        throw new DescriptorValidationException((IDescriptor) this, "Enums must contain at least one value.");
      this.values = DescriptorUtil.ConvertAndMakeReadOnly<EnumValueDescriptorProto, EnumValueDescriptor>((IList<EnumValueDescriptorProto>) proto.Value, (DescriptorUtil.IndexedConverter<EnumValueDescriptorProto, EnumValueDescriptor>) ((value, i) => new EnumValueDescriptor(value, file, parent1, i)));
      this.File.DescriptorPool.AddSymbol((IDescriptor) this);
    }

    internal EnumDescriptorProto Proto => this.proto;

    public override string Name => this.proto.Name;

    public System.Type GeneratedType => this.generatedType;

    public MessageDescriptor ContainingType => this.containingType;

    public IList<EnumValueDescriptor> Values => this.values;

    public EnumValueDescriptor FindValueByNumber(int number) => this.File.DescriptorPool.FindEnumValueByNumber(this, number);

    public EnumValueDescriptor FindValueByName(string name) => this.File.DescriptorPool.FindSymbol<EnumValueDescriptor>(this.FullName + "." + name);
  }
}
