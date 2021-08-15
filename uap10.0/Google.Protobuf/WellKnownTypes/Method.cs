// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Method
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
  public sealed class Method : IMessage<Method>, IMessage, IEquatable<Method>, IDeepCloneable<Method>
  {
    public const int NameFieldNumber = 1;
    public const int RequestTypeUrlFieldNumber = 2;
    public const int RequestStreamingFieldNumber = 3;
    public const int ResponseTypeUrlFieldNumber = 4;
    public const int ResponseStreamingFieldNumber = 5;
    public const int OptionsFieldNumber = 6;
    private static readonly MessageParser<Method> _parser = new MessageParser<Method>((Func<Method>) (() => new Method()));
    private string name_ = "";
    private string requestTypeUrl_ = "";
    private bool requestStreaming_;
    private string responseTypeUrl_ = "";
    private bool responseStreaming_;
    private static readonly FieldCodec<Option> _repeated_options_codec = FieldCodec.ForMessage<Option>(50U, Option.Parser);
    private readonly RepeatedField<Option> options_ = new RepeatedField<Option>();

    public static MessageParser<Method> Parser => Method._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Api.Descriptor.MessageTypes[1];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Method.Descriptor;

    public Method()
    {
    }

    public Method(Method other)
      : this()
    {
      this.name_ = other.name_;
      this.requestTypeUrl_ = other.requestTypeUrl_;
      this.requestStreaming_ = other.requestStreaming_;
      this.responseTypeUrl_ = other.responseTypeUrl_;
      this.responseStreaming_ = other.responseStreaming_;
      this.options_ = other.options_.Clone();
    }

    public Method Clone() => new Method(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public string RequestTypeUrl
    {
      get => this.requestTypeUrl_;
      set => this.requestTypeUrl_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public bool RequestStreaming
    {
      get => this.requestStreaming_;
      set => this.requestStreaming_ = value;
    }

    public string ResponseTypeUrl
    {
      get => this.responseTypeUrl_;
      set => this.responseTypeUrl_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public bool ResponseStreaming
    {
      get => this.responseStreaming_;
      set => this.responseStreaming_ = value;
    }

    public RepeatedField<Option> Options => this.options_;

    public override bool Equals(object other) => this.Equals(other as Method);

    public bool Equals(Method other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && !(this.RequestTypeUrl != other.RequestTypeUrl) && (this.RequestStreaming == other.RequestStreaming && !(this.ResponseTypeUrl != other.ResponseTypeUrl)) && (this.ResponseStreaming == other.ResponseStreaming && this.options_.Equals(other.options_)));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      if (this.RequestTypeUrl.Length != 0)
        num ^= this.RequestTypeUrl.GetHashCode();
      if (this.RequestStreaming)
        num ^= this.RequestStreaming.GetHashCode();
      if (this.ResponseTypeUrl.Length != 0)
        num ^= this.ResponseTypeUrl.GetHashCode();
      if (this.ResponseStreaming)
        num ^= this.ResponseStreaming.GetHashCode();
      return num ^ this.options_.GetHashCode();
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Name.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.Name);
      }
      if (this.RequestTypeUrl.Length != 0)
      {
        output.WriteRawTag((byte) 18);
        output.WriteString(this.RequestTypeUrl);
      }
      if (this.RequestStreaming)
      {
        output.WriteRawTag((byte) 24);
        output.WriteBool(this.RequestStreaming);
      }
      if (this.ResponseTypeUrl.Length != 0)
      {
        output.WriteRawTag((byte) 34);
        output.WriteString(this.ResponseTypeUrl);
      }
      if (this.ResponseStreaming)
      {
        output.WriteRawTag((byte) 40);
        output.WriteBool(this.ResponseStreaming);
      }
      this.options_.WriteTo(output, Method._repeated_options_codec);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.RequestTypeUrl.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.RequestTypeUrl);
      if (this.RequestStreaming)
        num += 2;
      if (this.ResponseTypeUrl.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.ResponseTypeUrl);
      if (this.ResponseStreaming)
        num += 2;
      return num + this.options_.CalculateSize(Method._repeated_options_codec);
    }

    public void MergeFrom(Method other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.RequestTypeUrl.Length != 0)
        this.RequestTypeUrl = other.RequestTypeUrl;
      if (other.RequestStreaming)
        this.RequestStreaming = other.RequestStreaming;
      if (other.ResponseTypeUrl.Length != 0)
        this.ResponseTypeUrl = other.ResponseTypeUrl;
      if (other.ResponseStreaming)
        this.ResponseStreaming = other.ResponseStreaming;
      this.options_.Add(other.options_);
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
            this.RequestTypeUrl = input.ReadString();
            continue;
          case 24:
            this.RequestStreaming = input.ReadBool();
            continue;
          case 34:
            this.ResponseTypeUrl = input.ReadString();
            continue;
          case 40:
            this.ResponseStreaming = input.ReadBool();
            continue;
          case 50:
            this.options_.AddEntriesFrom(input, Method._repeated_options_codec);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
