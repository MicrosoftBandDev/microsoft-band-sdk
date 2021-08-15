// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.ServiceDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System.Collections.Generic;

namespace Google.Protobuf.Reflection
{
  public sealed class ServiceDescriptor : DescriptorBase
  {
    private readonly ServiceDescriptorProto proto;
    private readonly IList<MethodDescriptor> methods;

    internal ServiceDescriptor(ServiceDescriptorProto proto, FileDescriptor file, int index)
      : base(file, file.ComputeFullName((MessageDescriptor) null, proto.Name), index)
    {
      ServiceDescriptor parent = this;
      this.proto = proto;
      this.methods = DescriptorUtil.ConvertAndMakeReadOnly<MethodDescriptorProto, MethodDescriptor>((IList<MethodDescriptorProto>) proto.Method, (DescriptorUtil.IndexedConverter<MethodDescriptorProto, MethodDescriptor>) ((method, i) => new MethodDescriptor(method, file, parent, i)));
      file.DescriptorPool.AddSymbol((IDescriptor) this);
    }

    public override string Name => this.proto.Name;

    internal ServiceDescriptorProto Proto => this.proto;

    public IList<MethodDescriptor> Methods => this.methods;

    public MethodDescriptor FindMethodByName(string name) => this.File.DescriptorPool.FindSymbol<MethodDescriptor>(this.FullName + "." + name);

    internal void CrossLink()
    {
      foreach (MethodDescriptor method in (IEnumerable<MethodDescriptor>) this.methods)
        method.CrossLink();
    }
  }
}
