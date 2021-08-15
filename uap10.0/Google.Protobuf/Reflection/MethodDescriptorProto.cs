// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.MethodDescriptorProto
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class MethodDescriptorProto : 
    IMessage<MethodDescriptorProto>,
    IMessage,
    IEquatable<MethodDescriptorProto>,
    IDeepCloneable<MethodDescriptorProto>
  {
    public const int NameFieldNumber = 1;
    public const int InputTypeFieldNumber = 2;
    public const int OutputTypeFieldNumber = 3;
    public const int OptionsFieldNumber = 4;
    public const int ClientStreamingFieldNumber = 5;
    public const int ServerStreamingFieldNumber = 6;
    private static readonly MessageParser<MethodDescriptorProto> _parser = new MessageParser<MethodDescriptorProto>((Func<MethodDescriptorProto>) (() => new MethodDescriptorProto()));
    private string name_ = "";
    private string inputType_ = "";
    private string outputType_ = "";
    private MethodOptions options_;
    private bool clientStreaming_;
    private bool serverStreaming_;

    public static MessageParser<MethodDescriptorProto> Parser => MethodDescriptorProto._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[8];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => MethodDescriptorProto.Descriptor;

    public MethodDescriptorProto()
    {
    }

    public MethodDescriptorProto(MethodDescriptorProto other)
      : this()
    {
      this.name_ = other.name_;
      this.inputType_ = other.inputType_;
      this.outputType_ = other.outputType_;
      this.Options = other.options_ != null ? other.Options.Clone() : (MethodOptions) null;
      this.clientStreaming_ = other.clientStreaming_;
      this.serverStreaming_ = other.serverStreaming_;
    }

    public MethodDescriptorProto Clone() => new MethodDescriptorProto(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string InputType
    {
      get => this.inputType_;
      set => this.inputType_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string OutputType
    {
      get => this.outputType_;
      set => this.outputType_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public MethodOptions Options
    {
      get => this.options_;
      set => this.options_ = value;
    }

    public bool ClientStreaming
    {
      get => this.clientStreaming_;
      set => this.clientStreaming_ = value;
    }

    public bool ServerStreaming
    {
      get => this.serverStreaming_;
      set => this.serverStreaming_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as MethodDescriptorProto);

    public bool Equals(MethodDescriptorProto other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && !(this.InputType != other.InputType) && (!(this.OutputType != other.OutputType) && object.Equals((object) this.Options, (object) other.Options)) && (this.ClientStreaming == other.ClientStreaming && this.ServerStreaming == other.ServerStreaming));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      if (this.InputType.Length != 0)
        num ^= this.InputType.GetHashCode();
      if (this.OutputType.Length != 0)
        num ^= this.OutputType.GetHashCode();
      if (this.options_ != null)
        num ^= this.Options.GetHashCode();
      if (this.ClientStreaming)
        num ^= this.ClientStreaming.GetHashCode();
      if (this.ServerStreaming)
        num ^= this.ServerStreaming.GetHashCode();
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
      if (this.InputType.Length != 0)
      {
        output.WriteRawTag((byte) 18);
        output.WriteString(this.InputType);
      }
      if (this.OutputType.Length != 0)
      {
        output.WriteRawTag((byte) 26);
        output.WriteString(this.OutputType);
      }
      if (this.options_ != null)
      {
        output.WriteRawTag((byte) 34);
        output.WriteMessage((IMessage) this.Options);
      }
      if (this.ClientStreaming)
      {
        output.WriteRawTag((byte) 40);
        output.WriteBool(this.ClientStreaming);
      }
      if (!this.ServerStreaming)
        return;
      output.WriteRawTag((byte) 48);
      output.WriteBool(this.ServerStreaming);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.InputType.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.InputType);
      if (this.OutputType.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.OutputType);
      if (this.options_ != null)
        num += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Options);
      if (this.ClientStreaming)
        num += 2;
      if (this.ServerStreaming)
        num += 2;
      return num;
    }

    public void MergeFrom(MethodDescriptorProto other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.InputType.Length != 0)
        this.InputType = other.InputType;
      if (other.OutputType.Length != 0)
        this.OutputType = other.OutputType;
      if (other.options_ != null)
      {
        if (this.options_ == null)
          this.options_ = new MethodOptions();
        this.Options.MergeFrom(other.Options);
      }
      if (other.ClientStreaming)
        this.ClientStreaming = other.ClientStreaming;
      if (!other.ServerStreaming)
        return;
      this.ServerStreaming = other.ServerStreaming;
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
            this.InputType = input.ReadString();
            continue;
          case 26:
            this.OutputType = input.ReadString();
            continue;
          case 34:
            if (this.options_ == null)
              this.options_ = new MethodOptions();
            input.ReadMessage((IMessage) this.options_);
            continue;
          case 40:
            this.ClientStreaming = input.ReadBool();
            continue;
          case 48:
            this.ServerStreaming = input.ReadBool();
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
