// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Api
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Api : IMessage<Api>, IMessage, IEquatable<Api>, IDeepCloneable<Api>
  {
    public const int NameFieldNumber = 1;
    public const int MethodsFieldNumber = 2;
    public const int OptionsFieldNumber = 3;
    public const int VersionFieldNumber = 4;
    public const int SourceContextFieldNumber = 5;
    private static readonly MessageParser<Api> _parser = new MessageParser<Api>((Func<Api>) (() => new Api()));
    private string name_ = "";
    private static readonly FieldCodec<Method> _repeated_methods_codec = FieldCodec.ForMessage<Method>(18U, Method.Parser);
    private readonly RepeatedField<Method> methods_ = new RepeatedField<Method>();
    private static readonly FieldCodec<Option> _repeated_options_codec = FieldCodec.ForMessage<Option>(26U, Option.Parser);
    private readonly RepeatedField<Option> options_ = new RepeatedField<Option>();
    private string version_ = "";
    private SourceContext sourceContext_;

    public static MessageParser<Api> Parser => Api._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Api.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Api.Descriptor;

    public Api()
    {
    }

    public Api(Api other)
      : this()
    {
      this.name_ = other.name_;
      this.methods_ = other.methods_.Clone();
      this.options_ = other.options_.Clone();
      this.version_ = other.version_;
      this.SourceContext = other.sourceContext_ != null ? other.SourceContext.Clone() : (SourceContext) null;
    }

    public Api Clone() => new Api(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public RepeatedField<Method> Methods => this.methods_;

    public RepeatedField<Option> Options => this.options_;

    public string Version
    {
      get => this.version_;
      set => this.version_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public SourceContext SourceContext
    {
      get => this.sourceContext_;
      set => this.sourceContext_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as Api);

    public bool Equals(Api other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.methods_.Equals(other.methods_) && (this.options_.Equals(other.options_) && !(this.Version != other.Version)) && object.Equals((object) this.SourceContext, (object) other.SourceContext));

    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.Name.Length != 0)
        num1 ^= this.Name.GetHashCode();
      int num2 = num1 ^ this.methods_.GetHashCode() ^ this.options_.GetHashCode();
      if (this.Version.Length != 0)
        num2 ^= this.Version.GetHashCode();
      if (this.sourceContext_ != null)
        num2 ^= this.SourceContext.GetHashCode();
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
      this.methods_.WriteTo(output, Api._repeated_methods_codec);
      this.options_.WriteTo(output, Api._repeated_options_codec);
      if (this.Version.Length != 0)
      {
        output.WriteRawTag((byte) 34);
        output.WriteString(this.Version);
      }
      if (this.sourceContext_ == null)
        return;
      output.WriteRawTag((byte) 42);
      output.WriteMessage((IMessage) this.SourceContext);
    }

    public int CalculateSize()
    {
      int num1 = 0;
      if (this.Name.Length != 0)
        num1 += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      int num2 = num1 + this.methods_.CalculateSize(Api._repeated_methods_codec) + this.options_.CalculateSize(Api._repeated_options_codec);
      if (this.Version.Length != 0)
        num2 += 1 + CodedOutputStream.ComputeStringSize(this.Version);
      if (this.sourceContext_ != null)
        num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SourceContext);
      return num2;
    }

    public void MergeFrom(Api other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      this.methods_.Add(other.methods_);
      this.options_.Add(other.options_);
      if (other.Version.Length != 0)
        this.Version = other.Version;
      if (other.sourceContext_ == null)
        return;
      if (this.sourceContext_ == null)
        this.sourceContext_ = new SourceContext();
      this.SourceContext.MergeFrom(other.SourceContext);
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
            this.methods_.AddEntriesFrom(input, Api._repeated_methods_codec);
            continue;
          case 26:
            this.options_.AddEntriesFrom(input, Api._repeated_options_codec);
            continue;
          case 34:
            this.Version = input.ReadString();
            continue;
          case 42:
            if (this.sourceContext_ == null)
              this.sourceContext_ = new SourceContext();
            input.ReadMessage((IMessage) this.sourceContext_);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
