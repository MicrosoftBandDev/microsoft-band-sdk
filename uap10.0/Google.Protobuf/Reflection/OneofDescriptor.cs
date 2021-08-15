// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.OneofDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Compatibility;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Google.Protobuf.Reflection
{
  public sealed class OneofDescriptor : DescriptorBase
  {
    private readonly OneofDescriptorProto proto;
    private MessageDescriptor containingType;
    private IList<FieldDescriptor> fields;
    private readonly OneofAccessor accessor;

    internal OneofDescriptor(
      OneofDescriptorProto proto,
      FileDescriptor file,
      MessageDescriptor parent,
      int index,
      string clrName)
      : base(file, file.ComputeFullName(parent, proto.Name), index)
    {
      this.proto = proto;
      this.containingType = parent;
      file.DescriptorPool.AddSymbol((IDescriptor) this);
      this.accessor = this.CreateAccessor(clrName);
    }

    public override string Name => this.proto.Name;

    public MessageDescriptor ContainingType => this.containingType;

    public IList<FieldDescriptor> Fields => this.fields;

    public OneofAccessor Accessor => this.accessor;

    internal void CrossLink()
    {
      List<FieldDescriptor> fieldDescriptorList = new List<FieldDescriptor>();
      foreach (FieldDescriptor fieldDescriptor in (IEnumerable<FieldDescriptor>) this.ContainingType.Fields.InDeclarationOrder())
      {
        if (fieldDescriptor.ContainingOneof == this)
          fieldDescriptorList.Add(fieldDescriptor);
      }
      this.fields = (IList<FieldDescriptor>) new ReadOnlyCollection<FieldDescriptor>((IList<FieldDescriptor>) fieldDescriptorList);
    }

    private OneofAccessor CreateAccessor(string clrName)
    {
      if (this.containingType.GeneratedType == null || clrName == null)
        return (OneofAccessor) null;
      return new OneofAccessor(this.containingType.GeneratedType.GetProperty(clrName + "Case") ?? throw new DescriptorValidationException((IDescriptor) this, "Property " + clrName + "Case not found in " + (object) this.containingType.GeneratedType), this.containingType.GeneratedType.GetMethod("Clear" + clrName) ?? throw new DescriptorValidationException((IDescriptor) this, "Method Clear" + clrName + " not found in " + (object) this.containingType.GeneratedType), this);
    }
  }
}
