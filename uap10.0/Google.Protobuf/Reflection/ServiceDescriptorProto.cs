// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.ServiceDescriptorProto
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class ServiceDescriptorProto : 
    IMessage<ServiceDescriptorProto>,
    IMessage,
    IEquatable<ServiceDescriptorProto>,
    IDeepCloneable<ServiceDescriptorProto>
  {
    public const int NameFieldNumber = 1;
    public const int MethodFieldNumber = 2;
    public const int OptionsFieldNumber = 3;
    private static readonly MessageParser<ServiceDescriptorProto> _parser = new MessageParser<ServiceDescriptorProto>((Func<ServiceDescriptorProto>) (() => new ServiceDescriptorProto()));
    private string name_ = "";
    private static readonly FieldCodec<MethodDescriptorProto> _repeated_method_codec = FieldCodec.ForMessage<MethodDescriptorProto>(18U, MethodDescriptorProto.Parser);
    private readonly RepeatedField<MethodDescriptorProto> method_ = new RepeatedField<MethodDescriptorProto>();
    private ServiceOptions options_;

    public static MessageParser<ServiceDescriptorProto> Parser => ServiceDescriptorProto._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[7];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => ServiceDescriptorProto.Descriptor;

    public ServiceDescriptorProto()
    {
    }

    public ServiceDescriptorProto(ServiceDescriptorProto other)
      : this()
    {
      this.name_ = other.name_;
      this.method_ = other.method_.Clone();
      this.Options = other.options_ != null ? other.Options.Clone() : (ServiceOptions) null;
    }

    public ServiceDescriptorProto Clone() => new ServiceDescriptorProto(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public RepeatedField<MethodDescriptorProto> Method => this.method_;

    public ServiceOptions Options
    {
      get => this.options_;
      set => this.options_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as ServiceDescriptorProto);

    public bool Equals(ServiceDescriptorProto other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.method_.Equals(other.method_) && object.Equals((object) this.Options, (object) other.Options));

    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.Name.Length != 0)
        num1 ^= this.Name.GetHashCode();
      int num2 = num1 ^ this.method_.GetHashCode();
      if (this.options_ != null)
        num2 ^= this.Options.GetHashCode();
      return num2;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Name.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.Name);
      }
      this.method_.WriteTo(output, ServiceDescriptorProto._repeated_method_codec);
      if (this.options_ == null)
        return;
      output.WriteRawTag((byte) 26);
      output.WriteMessage((IMessage) this.Options);
    }

    public int CalculateSize()
    {
      int num1 = 0;
      if (this.Name.Length != 0)
        num1 += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      int num2 = num1 + this.method_.CalculateSize(ServiceDescriptorProto._repeated_method_codec);
      if (this.options_ != null)
        num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Options);
      return num2;
    }

    public void MergeFrom(ServiceDescriptorProto other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      this.method_.Add(other.method_);
      if (other.options_ == null)
        return;
      if (this.options_ == null)
        this.options_ = new ServiceOptions();
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
            this.method_.AddEntriesFrom(input, ServiceDescriptorProto._repeated_method_codec);
            continue;
          case 26:
            if (this.options_ == null)
              this.options_ = new ServiceOptions();
            input.ReadMessage((IMessage) this.options_);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
