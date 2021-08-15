// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.MethodDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

namespace Google.Protobuf.Reflection
{
  public sealed class MethodDescriptor : DescriptorBase
  {
    private readonly MethodDescriptorProto proto;
    private readonly ServiceDescriptor service;
    private MessageDescriptor inputType;
    private MessageDescriptor outputType;

    public ServiceDescriptor Service => this.service;

    public MessageDescriptor InputType => this.inputType;

    public MessageDescriptor OutputType => this.outputType;

    public bool IsClientStreaming => this.proto.ClientStreaming;

    public bool IsServerStreaming => this.proto.ServerStreaming;

    internal MethodDescriptor(
      MethodDescriptorProto proto,
      FileDescriptor file,
      ServiceDescriptor parent,
      int index)
      : base(file, parent.FullName + "." + proto.Name, index)
    {
      this.proto = proto;
      this.service = parent;
      file.DescriptorPool.AddSymbol((IDescriptor) this);
    }

    internal MethodDescriptorProto Proto => this.proto;

    public override string Name => this.proto.Name;

    internal void CrossLink()
    {
      IDescriptor descriptor1 = this.File.DescriptorPool.LookupSymbol(this.Proto.InputType, (IDescriptor) this);
      this.inputType = descriptor1 is MessageDescriptor ? (MessageDescriptor) descriptor1 : throw new DescriptorValidationException((IDescriptor) this, "\"" + this.Proto.InputType + "\" is not a message type.");
      IDescriptor descriptor2 = this.File.DescriptorPool.LookupSymbol(this.Proto.OutputType, (IDescriptor) this);
      this.outputType = descriptor2 is MessageDescriptor ? (MessageDescriptor) descriptor2 : throw new DescriptorValidationException((IDescriptor) this, "\"" + this.Proto.OutputType + "\" is not a message type.");
    }
  }
}
