// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.FileDescriptorProto
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class FileDescriptorProto : 
    IMessage<FileDescriptorProto>,
    IMessage,
    IEquatable<FileDescriptorProto>,
    IDeepCloneable<FileDescriptorProto>
  {
    public const int NameFieldNumber = 1;
    public const int PackageFieldNumber = 2;
    public const int DependencyFieldNumber = 3;
    public const int PublicDependencyFieldNumber = 10;
    public const int WeakDependencyFieldNumber = 11;
    public const int MessageTypeFieldNumber = 4;
    public const int EnumTypeFieldNumber = 5;
    public const int ServiceFieldNumber = 6;
    public const int ExtensionFieldNumber = 7;
    public const int OptionsFieldNumber = 8;
    public const int SourceCodeInfoFieldNumber = 9;
    public const int SyntaxFieldNumber = 12;
    private static readonly MessageParser<FileDescriptorProto> _parser = new MessageParser<FileDescriptorProto>((Func<FileDescriptorProto>) (() => new FileDescriptorProto()));
    private string name_ = "";
    private string package_ = "";
    private static readonly FieldCodec<string> _repeated_dependency_codec = FieldCodec.ForString(26U);
    private readonly RepeatedField<string> dependency_ = new RepeatedField<string>();
    private static readonly FieldCodec<int> _repeated_publicDependency_codec = FieldCodec.ForInt32(80U);
    private readonly RepeatedField<int> publicDependency_ = new RepeatedField<int>();
    private static readonly FieldCodec<int> _repeated_weakDependency_codec = FieldCodec.ForInt32(88U);
    private readonly RepeatedField<int> weakDependency_ = new RepeatedField<int>();
    private static readonly FieldCodec<DescriptorProto> _repeated_messageType_codec = FieldCodec.ForMessage<DescriptorProto>(34U, DescriptorProto.Parser);
    private readonly RepeatedField<DescriptorProto> messageType_ = new RepeatedField<DescriptorProto>();
    private static readonly FieldCodec<EnumDescriptorProto> _repeated_enumType_codec = FieldCodec.ForMessage<EnumDescriptorProto>(42U, EnumDescriptorProto.Parser);
    private readonly RepeatedField<EnumDescriptorProto> enumType_ = new RepeatedField<EnumDescriptorProto>();
    private static readonly FieldCodec<ServiceDescriptorProto> _repeated_service_codec = FieldCodec.ForMessage<ServiceDescriptorProto>(50U, ServiceDescriptorProto.Parser);
    private readonly RepeatedField<ServiceDescriptorProto> service_ = new RepeatedField<ServiceDescriptorProto>();
    private static readonly FieldCodec<FieldDescriptorProto> _repeated_extension_codec = FieldCodec.ForMessage<FieldDescriptorProto>(58U, FieldDescriptorProto.Parser);
    private readonly RepeatedField<FieldDescriptorProto> extension_ = new RepeatedField<FieldDescriptorProto>();
    private FileOptions options_;
    private SourceCodeInfo sourceCodeInfo_;
    private string syntax_ = "";

    public static MessageParser<FileDescriptorProto> Parser => FileDescriptorProto._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[1];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FileDescriptorProto.Descriptor;

    public FileDescriptorProto()
    {
    }

    public FileDescriptorProto(FileDescriptorProto other)
      : this()
    {
      this.name_ = other.name_;
      this.package_ = other.package_;
      this.dependency_ = other.dependency_.Clone();
      this.publicDependency_ = other.publicDependency_.Clone();
      this.weakDependency_ = other.weakDependency_.Clone();
      this.messageType_ = other.messageType_.Clone();
      this.enumType_ = other.enumType_.Clone();
      this.service_ = other.service_.Clone();
      this.extension_ = other.extension_.Clone();
      this.Options = other.options_ != null ? other.Options.Clone() : (FileOptions) null;
      this.SourceCodeInfo = other.sourceCodeInfo_ != null ? other.SourceCodeInfo.Clone() : (SourceCodeInfo) null;
      this.syntax_ = other.syntax_;
    }

    public FileDescriptorProto Clone() => new FileDescriptorProto(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string Package
    {
      get => this.package_;
      set => this.package_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public RepeatedField<string> Dependency => this.dependency_;

    public RepeatedField<int> PublicDependency => this.publicDependency_;

    public RepeatedField<int> WeakDependency => this.weakDependency_;

    public RepeatedField<DescriptorProto> MessageType => this.messageType_;

    public RepeatedField<EnumDescriptorProto> EnumType => this.enumType_;

    public RepeatedField<ServiceDescriptorProto> Service => this.service_;

    public RepeatedField<FieldDescriptorProto> Extension => this.extension_;

    public FileOptions Options
    {
      get => this.options_;
      set => this.options_ = value;
    }

    public SourceCodeInfo SourceCodeInfo
    {
      get => this.sourceCodeInfo_;
      set => this.sourceCodeInfo_ = value;
    }

    public string Syntax
    {
      get => this.syntax_;
      set => this.syntax_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public override bool Equals(object other) => this.Equals(other as FileDescriptorProto);

    public bool Equals(FileDescriptorProto other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && !(this.Package != other.Package) && (this.dependency_.Equals(other.dependency_) && this.publicDependency_.Equals(other.publicDependency_)) && (this.weakDependency_.Equals(other.weakDependency_) && this.messageType_.Equals(other.messageType_) && (this.enumType_.Equals(other.enumType_) && this.service_.Equals(other.service_))) && (this.extension_.Equals(other.extension_) && object.Equals((object) this.Options, (object) other.Options) && (object.Equals((object) this.SourceCodeInfo, (object) other.SourceCodeInfo) && !(this.Syntax != other.Syntax))));

    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.Name.Length != 0)
        num1 ^= this.Name.GetHashCode();
      if (this.Package.Length != 0)
        num1 ^= this.Package.GetHashCode();
      int num2 = num1 ^ this.dependency_.GetHashCode() ^ this.publicDependency_.GetHashCode() ^ this.weakDependency_.GetHashCode() ^ this.messageType_.GetHashCode() ^ this.enumType_.GetHashCode() ^ this.service_.GetHashCode() ^ this.extension_.GetHashCode();
      if (this.options_ != null)
        num2 ^= this.Options.GetHashCode();
      if (this.sourceCodeInfo_ != null)
        num2 ^= this.SourceCodeInfo.GetHashCode();
      if (this.Syntax.Length != 0)
        num2 ^= this.Syntax.GetHashCode();
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
      if (this.Package.Length != 0)
      {
        output.WriteRawTag((byte) 18);
        output.WriteString(this.Package);
      }
      this.dependency_.WriteTo(output, FileDescriptorProto._repeated_dependency_codec);
      this.messageType_.WriteTo(output, FileDescriptorProto._repeated_messageType_codec);
      this.enumType_.WriteTo(output, FileDescriptorProto._repeated_enumType_codec);
      this.service_.WriteTo(output, FileDescriptorProto._repeated_service_codec);
      this.extension_.WriteTo(output, FileDescriptorProto._repeated_extension_codec);
      if (this.options_ != null)
      {
        output.WriteRawTag((byte) 66);
        output.WriteMessage((IMessage) this.Options);
      }
      if (this.sourceCodeInfo_ != null)
      {
        output.WriteRawTag((byte) 74);
        output.WriteMessage((IMessage) this.SourceCodeInfo);
      }
      this.publicDependency_.WriteTo(output, FileDescriptorProto._repeated_publicDependency_codec);
      this.weakDependency_.WriteTo(output, FileDescriptorProto._repeated_weakDependency_codec);
      if (this.Syntax.Length == 0)
        return;
      output.WriteRawTag((byte) 98);
      output.WriteString(this.Syntax);
    }

    public int CalculateSize()
    {
      int num1 = 0;
      if (this.Name.Length != 0)
        num1 += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.Package.Length != 0)
        num1 += 1 + CodedOutputStream.ComputeStringSize(this.Package);
      int num2 = num1 + this.dependency_.CalculateSize(FileDescriptorProto._repeated_dependency_codec) + this.publicDependency_.CalculateSize(FileDescriptorProto._repeated_publicDependency_codec) + this.weakDependency_.CalculateSize(FileDescriptorProto._repeated_weakDependency_codec) + this.messageType_.CalculateSize(FileDescriptorProto._repeated_messageType_codec) + this.enumType_.CalculateSize(FileDescriptorProto._repeated_enumType_codec) + this.service_.CalculateSize(FileDescriptorProto._repeated_service_codec) + this.extension_.CalculateSize(FileDescriptorProto._repeated_extension_codec);
      if (this.options_ != null)
        num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Options);
      if (this.sourceCodeInfo_ != null)
        num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SourceCodeInfo);
      if (this.Syntax.Length != 0)
        num2 += 1 + CodedOutputStream.ComputeStringSize(this.Syntax);
      return num2;
    }

    public void MergeFrom(FileDescriptorProto other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.Package.Length != 0)
        this.Package = other.Package;
      this.dependency_.Add(other.dependency_);
      this.publicDependency_.Add(other.publicDependency_);
      this.weakDependency_.Add(other.weakDependency_);
      this.messageType_.Add(other.messageType_);
      this.enumType_.Add(other.enumType_);
      this.service_.Add(other.service_);
      this.extension_.Add(other.extension_);
      if (other.options_ != null)
      {
        if (this.options_ == null)
          this.options_ = new FileOptions();
        this.Options.MergeFrom(other.Options);
      }
      if (other.sourceCodeInfo_ != null)
      {
        if (this.sourceCodeInfo_ == null)
          this.sourceCodeInfo_ = new SourceCodeInfo();
        this.SourceCodeInfo.MergeFrom(other.SourceCodeInfo);
      }
      if (other.Syntax.Length == 0)
        return;
      this.Syntax = other.Syntax;
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
            this.Package = input.ReadString();
            continue;
          case 26:
            this.dependency_.AddEntriesFrom(input, FileDescriptorProto._repeated_dependency_codec);
            continue;
          case 34:
            this.messageType_.AddEntriesFrom(input, FileDescriptorProto._repeated_messageType_codec);
            continue;
          case 42:
            this.enumType_.AddEntriesFrom(input, FileDescriptorProto._repeated_enumType_codec);
            continue;
          case 50:
            this.service_.AddEntriesFrom(input, FileDescriptorProto._repeated_service_codec);
            continue;
          case 58:
            this.extension_.AddEntriesFrom(input, FileDescriptorProto._repeated_extension_codec);
            continue;
          case 66:
            if (this.options_ == null)
              this.options_ = new FileOptions();
            input.ReadMessage((IMessage) this.options_);
            continue;
          case 74:
            if (this.sourceCodeInfo_ == null)
              this.sourceCodeInfo_ = new SourceCodeInfo();
            input.ReadMessage((IMessage) this.sourceCodeInfo_);
            continue;
          case 80:
          case 82:
            this.publicDependency_.AddEntriesFrom(input, FileDescriptorProto._repeated_publicDependency_codec);
            continue;
          case 88:
          case 90:
            this.weakDependency_.AddEntriesFrom(input, FileDescriptorProto._repeated_weakDependency_codec);
            continue;
          case 98:
            this.Syntax = input.ReadString();
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
