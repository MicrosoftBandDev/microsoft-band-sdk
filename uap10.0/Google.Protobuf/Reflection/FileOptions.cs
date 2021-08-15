// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.FileOptions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class FileOptions : 
    IMessage<FileOptions>,
    IMessage,
    IEquatable<FileOptions>,
    IDeepCloneable<FileOptions>
  {
    public const int JavaPackageFieldNumber = 1;
    public const int JavaOuterClassnameFieldNumber = 8;
    public const int JavaMultipleFilesFieldNumber = 10;
    public const int JavaGenerateEqualsAndHashFieldNumber = 20;
    public const int JavaStringCheckUtf8FieldNumber = 27;
    public const int OptimizeForFieldNumber = 9;
    public const int GoPackageFieldNumber = 11;
    public const int CcGenericServicesFieldNumber = 16;
    public const int JavaGenericServicesFieldNumber = 17;
    public const int PyGenericServicesFieldNumber = 18;
    public const int DeprecatedFieldNumber = 23;
    public const int CcEnableArenasFieldNumber = 31;
    public const int ObjcClassPrefixFieldNumber = 36;
    public const int CsharpNamespaceFieldNumber = 37;
    public const int JavananoUseDeprecatedPackageFieldNumber = 38;
    public const int UninterpretedOptionFieldNumber = 999;
    private static readonly MessageParser<FileOptions> _parser = new MessageParser<FileOptions>((Func<FileOptions>) (() => new FileOptions()));
    private string javaPackage_ = "";
    private string javaOuterClassname_ = "";
    private bool javaMultipleFiles_;
    private bool javaGenerateEqualsAndHash_;
    private bool javaStringCheckUtf8_;
    private FileOptions.Types.OptimizeMode optimizeFor_ = FileOptions.Types.OptimizeMode.SPEED;
    private string goPackage_ = "";
    private bool ccGenericServices_;
    private bool javaGenericServices_;
    private bool pyGenericServices_;
    private bool deprecated_;
    private bool ccEnableArenas_;
    private string objcClassPrefix_ = "";
    private string csharpNamespace_ = "";
    private bool javananoUseDeprecatedPackage_;
    private static readonly FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec = FieldCodec.ForMessage<Google.Protobuf.Reflection.UninterpretedOption>(7994U, Google.Protobuf.Reflection.UninterpretedOption.Parser);
    private readonly RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_ = new RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>();

    public static MessageParser<FileOptions> Parser => FileOptions._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[9];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FileOptions.Descriptor;

    public FileOptions()
    {
    }

    public FileOptions(FileOptions other)
      : this()
    {
      this.javaPackage_ = other.javaPackage_;
      this.javaOuterClassname_ = other.javaOuterClassname_;
      this.javaMultipleFiles_ = other.javaMultipleFiles_;
      this.javaGenerateEqualsAndHash_ = other.javaGenerateEqualsAndHash_;
      this.javaStringCheckUtf8_ = other.javaStringCheckUtf8_;
      this.optimizeFor_ = other.optimizeFor_;
      this.goPackage_ = other.goPackage_;
      this.ccGenericServices_ = other.ccGenericServices_;
      this.javaGenericServices_ = other.javaGenericServices_;
      this.pyGenericServices_ = other.pyGenericServices_;
      this.deprecated_ = other.deprecated_;
      this.ccEnableArenas_ = other.ccEnableArenas_;
      this.objcClassPrefix_ = other.objcClassPrefix_;
      this.csharpNamespace_ = other.csharpNamespace_;
      this.javananoUseDeprecatedPackage_ = other.javananoUseDeprecatedPackage_;
      this.uninterpretedOption_ = other.uninterpretedOption_.Clone();
    }

    public FileOptions Clone() => new FileOptions(this);

    public string JavaPackage
    {
      get => this.javaPackage_;
      set => this.javaPackage_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string JavaOuterClassname
    {
      get => this.javaOuterClassname_;
      set => this.javaOuterClassname_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public bool JavaMultipleFiles
    {
      get => this.javaMultipleFiles_;
      set => this.javaMultipleFiles_ = value;
    }

    public bool JavaGenerateEqualsAndHash
    {
      get => this.javaGenerateEqualsAndHash_;
      set => this.javaGenerateEqualsAndHash_ = value;
    }

    public bool JavaStringCheckUtf8
    {
      get => this.javaStringCheckUtf8_;
      set => this.javaStringCheckUtf8_ = value;
    }

    public FileOptions.Types.OptimizeMode OptimizeFor
    {
      get => this.optimizeFor_;
      set => this.optimizeFor_ = value;
    }

    public string GoPackage
    {
      get => this.goPackage_;
      set => this.goPackage_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public bool CcGenericServices
    {
      get => this.ccGenericServices_;
      set => this.ccGenericServices_ = value;
    }

    public bool JavaGenericServices
    {
      get => this.javaGenericServices_;
      set => this.javaGenericServices_ = value;
    }

    public bool PyGenericServices
    {
      get => this.pyGenericServices_;
      set => this.pyGenericServices_ = value;
    }

    public bool Deprecated
    {
      get => this.deprecated_;
      set => this.deprecated_ = value;
    }

    public bool CcEnableArenas
    {
      get => this.ccEnableArenas_;
      set => this.ccEnableArenas_ = value;
    }

    public string ObjcClassPrefix
    {
      get => this.objcClassPrefix_;
      set => this.objcClassPrefix_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string CsharpNamespace
    {
      get => this.csharpNamespace_;
      set => this.csharpNamespace_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public bool JavananoUseDeprecatedPackage
    {
      get => this.javananoUseDeprecatedPackage_;
      set => this.javananoUseDeprecatedPackage_ = value;
    }

    public RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption => this.uninterpretedOption_;

    public override bool Equals(object other) => this.Equals(other as FileOptions);

    public bool Equals(FileOptions other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.JavaPackage != other.JavaPackage) && !(this.JavaOuterClassname != other.JavaOuterClassname) && (this.JavaMultipleFiles == other.JavaMultipleFiles && this.JavaGenerateEqualsAndHash == other.JavaGenerateEqualsAndHash) && (this.JavaStringCheckUtf8 == other.JavaStringCheckUtf8 && this.OptimizeFor == other.OptimizeFor && (!(this.GoPackage != other.GoPackage) && this.CcGenericServices == other.CcGenericServices)) && (this.JavaGenericServices == other.JavaGenericServices && this.PyGenericServices == other.PyGenericServices && (this.Deprecated == other.Deprecated && this.CcEnableArenas == other.CcEnableArenas) && (!(this.ObjcClassPrefix != other.ObjcClassPrefix) && !(this.CsharpNamespace != other.CsharpNamespace) && (this.JavananoUseDeprecatedPackage == other.JavananoUseDeprecatedPackage && this.uninterpretedOption_.Equals(other.uninterpretedOption_)))));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.JavaPackage.Length != 0)
        num ^= this.JavaPackage.GetHashCode();
      if (this.JavaOuterClassname.Length != 0)
        num ^= this.JavaOuterClassname.GetHashCode();
      if (this.JavaMultipleFiles)
        num ^= this.JavaMultipleFiles.GetHashCode();
      if (this.JavaGenerateEqualsAndHash)
        num ^= this.JavaGenerateEqualsAndHash.GetHashCode();
      if (this.JavaStringCheckUtf8)
        num ^= this.JavaStringCheckUtf8.GetHashCode();
      if (this.OptimizeFor != FileOptions.Types.OptimizeMode.SPEED)
        num ^= this.OptimizeFor.GetHashCode();
      if (this.GoPackage.Length != 0)
        num ^= this.GoPackage.GetHashCode();
      if (this.CcGenericServices)
        num ^= this.CcGenericServices.GetHashCode();
      if (this.JavaGenericServices)
        num ^= this.JavaGenericServices.GetHashCode();
      if (this.PyGenericServices)
        num ^= this.PyGenericServices.GetHashCode();
      if (this.Deprecated)
        num ^= this.Deprecated.GetHashCode();
      if (this.CcEnableArenas)
        num ^= this.CcEnableArenas.GetHashCode();
      if (this.ObjcClassPrefix.Length != 0)
        num ^= this.ObjcClassPrefix.GetHashCode();
      if (this.CsharpNamespace.Length != 0)
        num ^= this.CsharpNamespace.GetHashCode();
      if (this.JavananoUseDeprecatedPackage)
        num ^= this.JavananoUseDeprecatedPackage.GetHashCode();
      return num ^ this.uninterpretedOption_.GetHashCode();
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.JavaPackage.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.JavaPackage);
      }
      if (this.JavaOuterClassname.Length != 0)
      {
        output.WriteRawTag((byte) 66);
        output.WriteString(this.JavaOuterClassname);
      }
      if (this.OptimizeFor != FileOptions.Types.OptimizeMode.SPEED)
      {
        output.WriteRawTag((byte) 72);
        output.WriteEnum((int) this.OptimizeFor);
      }
      if (this.JavaMultipleFiles)
      {
        output.WriteRawTag((byte) 80);
        output.WriteBool(this.JavaMultipleFiles);
      }
      if (this.GoPackage.Length != 0)
      {
        output.WriteRawTag((byte) 90);
        output.WriteString(this.GoPackage);
      }
      if (this.CcGenericServices)
      {
        output.WriteRawTag((byte) 128, (byte) 1);
        output.WriteBool(this.CcGenericServices);
      }
      if (this.JavaGenericServices)
      {
        output.WriteRawTag((byte) 136, (byte) 1);
        output.WriteBool(this.JavaGenericServices);
      }
      if (this.PyGenericServices)
      {
        output.WriteRawTag((byte) 144, (byte) 1);
        output.WriteBool(this.PyGenericServices);
      }
      if (this.JavaGenerateEqualsAndHash)
      {
        output.WriteRawTag((byte) 160, (byte) 1);
        output.WriteBool(this.JavaGenerateEqualsAndHash);
      }
      if (this.Deprecated)
      {
        output.WriteRawTag((byte) 184, (byte) 1);
        output.WriteBool(this.Deprecated);
      }
      if (this.JavaStringCheckUtf8)
      {
        output.WriteRawTag((byte) 216, (byte) 1);
        output.WriteBool(this.JavaStringCheckUtf8);
      }
      if (this.CcEnableArenas)
      {
        output.WriteRawTag((byte) 248, (byte) 1);
        output.WriteBool(this.CcEnableArenas);
      }
      if (this.ObjcClassPrefix.Length != 0)
      {
        output.WriteRawTag((byte) 162, (byte) 2);
        output.WriteString(this.ObjcClassPrefix);
      }
      if (this.CsharpNamespace.Length != 0)
      {
        output.WriteRawTag((byte) 170, (byte) 2);
        output.WriteString(this.CsharpNamespace);
      }
      if (this.JavananoUseDeprecatedPackage)
      {
        output.WriteRawTag((byte) 176, (byte) 2);
        output.WriteBool(this.JavananoUseDeprecatedPackage);
      }
      this.uninterpretedOption_.WriteTo(output, FileOptions._repeated_uninterpretedOption_codec);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.JavaPackage.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.JavaPackage);
      if (this.JavaOuterClassname.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.JavaOuterClassname);
      if (this.JavaMultipleFiles)
        num += 2;
      if (this.JavaGenerateEqualsAndHash)
        num += 3;
      if (this.JavaStringCheckUtf8)
        num += 3;
      if (this.OptimizeFor != FileOptions.Types.OptimizeMode.SPEED)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.OptimizeFor);
      if (this.GoPackage.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.GoPackage);
      if (this.CcGenericServices)
        num += 3;
      if (this.JavaGenericServices)
        num += 3;
      if (this.PyGenericServices)
        num += 3;
      if (this.Deprecated)
        num += 3;
      if (this.CcEnableArenas)
        num += 3;
      if (this.ObjcClassPrefix.Length != 0)
        num += 2 + CodedOutputStream.ComputeStringSize(this.ObjcClassPrefix);
      if (this.CsharpNamespace.Length != 0)
        num += 2 + CodedOutputStream.ComputeStringSize(this.CsharpNamespace);
      if (this.JavananoUseDeprecatedPackage)
        num += 3;
      return num + this.uninterpretedOption_.CalculateSize(FileOptions._repeated_uninterpretedOption_codec);
    }

    public void MergeFrom(FileOptions other)
    {
      if (other == null)
        return;
      if (other.JavaPackage.Length != 0)
        this.JavaPackage = other.JavaPackage;
      if (other.JavaOuterClassname.Length != 0)
        this.JavaOuterClassname = other.JavaOuterClassname;
      if (other.JavaMultipleFiles)
        this.JavaMultipleFiles = other.JavaMultipleFiles;
      if (other.JavaGenerateEqualsAndHash)
        this.JavaGenerateEqualsAndHash = other.JavaGenerateEqualsAndHash;
      if (other.JavaStringCheckUtf8)
        this.JavaStringCheckUtf8 = other.JavaStringCheckUtf8;
      if (other.OptimizeFor != FileOptions.Types.OptimizeMode.SPEED)
        this.OptimizeFor = other.OptimizeFor;
      if (other.GoPackage.Length != 0)
        this.GoPackage = other.GoPackage;
      if (other.CcGenericServices)
        this.CcGenericServices = other.CcGenericServices;
      if (other.JavaGenericServices)
        this.JavaGenericServices = other.JavaGenericServices;
      if (other.PyGenericServices)
        this.PyGenericServices = other.PyGenericServices;
      if (other.Deprecated)
        this.Deprecated = other.Deprecated;
      if (other.CcEnableArenas)
        this.CcEnableArenas = other.CcEnableArenas;
      if (other.ObjcClassPrefix.Length != 0)
        this.ObjcClassPrefix = other.ObjcClassPrefix;
      if (other.CsharpNamespace.Length != 0)
        this.CsharpNamespace = other.CsharpNamespace;
      if (other.JavananoUseDeprecatedPackage)
        this.JavananoUseDeprecatedPackage = other.JavananoUseDeprecatedPackage;
      this.uninterpretedOption_.Add(other.uninterpretedOption_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 10:
            this.JavaPackage = input.ReadString();
            continue;
          case 66:
            this.JavaOuterClassname = input.ReadString();
            continue;
          case 72:
            this.optimizeFor_ = (FileOptions.Types.OptimizeMode) input.ReadEnum();
            continue;
          case 80:
            this.JavaMultipleFiles = input.ReadBool();
            continue;
          case 90:
            this.GoPackage = input.ReadString();
            continue;
          case 128:
            this.CcGenericServices = input.ReadBool();
            continue;
          case 136:
            this.JavaGenericServices = input.ReadBool();
            continue;
          case 144:
            this.PyGenericServices = input.ReadBool();
            continue;
          case 160:
            this.JavaGenerateEqualsAndHash = input.ReadBool();
            continue;
          case 184:
            this.Deprecated = input.ReadBool();
            continue;
          case 216:
            this.JavaStringCheckUtf8 = input.ReadBool();
            continue;
          case 248:
            this.CcEnableArenas = input.ReadBool();
            continue;
          case 290:
            this.ObjcClassPrefix = input.ReadString();
            continue;
          case 298:
            this.CsharpNamespace = input.ReadString();
            continue;
          case 304:
            this.JavananoUseDeprecatedPackage = input.ReadBool();
            continue;
          case 7994:
            this.uninterpretedOption_.AddEntriesFrom(input, FileOptions._repeated_uninterpretedOption_codec);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }

    [DebuggerNonUserCode]
    public static class Types
    {
      internal enum OptimizeMode
      {
        SPEED = 1,
        CODE_SIZE = 2,
        LITE_RUNTIME = 3,
      }
    }
  }
}
