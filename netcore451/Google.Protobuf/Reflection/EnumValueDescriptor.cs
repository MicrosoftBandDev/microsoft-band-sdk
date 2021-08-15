// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.EnumValueDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

namespace Google.Protobuf.Reflection
{
  public sealed class EnumValueDescriptor : DescriptorBase
  {
    private readonly EnumDescriptor enumDescriptor;
    private readonly EnumValueDescriptorProto proto;

    internal EnumValueDescriptor(
      EnumValueDescriptorProto proto,
      FileDescriptor file,
      EnumDescriptor parent,
      int index)
      : base(file, parent.FullName + "." + proto.Name, index)
    {
      this.proto = proto;
      this.enumDescriptor = parent;
      file.DescriptorPool.AddSymbol((IDescriptor) this);
      file.DescriptorPool.AddEnumValueByNumber(this);
    }

    internal EnumValueDescriptorProto Proto => this.proto;

    public override string Name => this.proto.Name;

    public int Number => this.Proto.Number;

    public EnumDescriptor EnumDescriptor => this.enumDescriptor;
  }
}
