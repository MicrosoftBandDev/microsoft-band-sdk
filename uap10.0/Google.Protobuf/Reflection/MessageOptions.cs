// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.MessageOptions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class MessageOptions : 
    IMessage<MessageOptions>,
    IMessage,
    IEquatable<MessageOptions>,
    IDeepCloneable<MessageOptions>
  {
    public const int MessageSetWireFormatFieldNumber = 1;
    public const int NoStandardDescriptorAccessorFieldNumber = 2;
    public const int DeprecatedFieldNumber = 3;
    public const int MapEntryFieldNumber = 7;
    public const int UninterpretedOptionFieldNumber = 999;
    private static readonly MessageParser<MessageOptions> _parser = new MessageParser<MessageOptions>((Func<MessageOptions>) (() => new MessageOptions()));
    private bool messageSetWireFormat_;
    private bool noStandardDescriptorAccessor_;
    private bool deprecated_;
    private bool mapEntry_;
    private static readonly FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec = FieldCodec.ForMessage<Google.Protobuf.Reflection.UninterpretedOption>(7994U, Google.Protobuf.Reflection.UninterpretedOption.Parser);
    private readonly RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_ = new RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>();

    public static MessageParser<MessageOptions> Parser => MessageOptions._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[10];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => MessageOptions.Descriptor;

    public MessageOptions()
    {
    }

    public MessageOptions(MessageOptions other)
      : this()
    {
      this.messageSetWireFormat_ = other.messageSetWireFormat_;
      this.noStandardDescriptorAccessor_ = other.noStandardDescriptorAccessor_;
      this.deprecated_ = other.deprecated_;
      this.mapEntry_ = other.mapEntry_;
      this.uninterpretedOption_ = other.uninterpretedOption_.Clone();
    }

    public MessageOptions Clone() => new MessageOptions(this);

    public bool MessageSetWireFormat
    {
      get => this.messageSetWireFormat_;
      set => this.messageSetWireFormat_ = value;
    }

    public bool NoStandardDescriptorAccessor
    {
      get => this.noStandardDescriptorAccessor_;
      set => this.noStandardDescriptorAccessor_ = value;
    }

    public bool Deprecated
    {
      get => this.deprecated_;
      set => this.deprecated_ = value;
    }

    public bool MapEntry
    {
      get => this.mapEntry_;
      set => this.mapEntry_ = value;
    }

    public RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption => this.uninterpretedOption_;

    public override bool Equals(object other) => this.Equals(other as MessageOptions);

    public bool Equals(MessageOptions other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.MessageSetWireFormat == other.MessageSetWireFormat && this.NoStandardDescriptorAccessor == other.NoStandardDescriptorAccessor && (this.Deprecated == other.Deprecated && this.MapEntry == other.MapEntry) && this.uninterpretedOption_.Equals(other.uninterpretedOption_));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.MessageSetWireFormat)
        num ^= this.MessageSetWireFormat.GetHashCode();
      if (this.NoStandardDescriptorAccessor)
        num ^= this.NoStandardDescriptorAccessor.GetHashCode();
      if (this.Deprecated)
        num ^= this.Deprecated.GetHashCode();
      if (this.MapEntry)
        num ^= this.MapEntry.GetHashCode();
      return num ^ this.uninterpretedOption_.GetHashCode();
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.MessageSetWireFormat)
      {
        output.WriteRawTag((byte) 8);
        output.WriteBool(this.MessageSetWireFormat);
      }
      if (this.NoStandardDescriptorAccessor)
      {
        output.WriteRawTag((byte) 16);
        output.WriteBool(this.NoStandardDescriptorAccessor);
      }
      if (this.Deprecated)
      {
        output.WriteRawTag((byte) 24);
        output.WriteBool(this.Deprecated);
      }
      if (this.MapEntry)
      {
        output.WriteRawTag((byte) 56);
        output.WriteBool(this.MapEntry);
      }
      this.uninterpretedOption_.WriteTo(output, MessageOptions._repeated_uninterpretedOption_codec);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.MessageSetWireFormat)
        num += 2;
      if (this.NoStandardDescriptorAccessor)
        num += 2;
      if (this.Deprecated)
        num += 2;
      if (this.MapEntry)
        num += 2;
      return num + this.uninterpretedOption_.CalculateSize(MessageOptions._repeated_uninterpretedOption_codec);
    }

    public void MergeFrom(MessageOptions other)
    {
      if (other == null)
        return;
      if (other.MessageSetWireFormat)
        this.MessageSetWireFormat = other.MessageSetWireFormat;
      if (other.NoStandardDescriptorAccessor)
        this.NoStandardDescriptorAccessor = other.NoStandardDescriptorAccessor;
      if (other.Deprecated)
        this.Deprecated = other.Deprecated;
      if (other.MapEntry)
        this.MapEntry = other.MapEntry;
      this.uninterpretedOption_.Add(other.uninterpretedOption_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 8:
            this.MessageSetWireFormat = input.ReadBool();
            continue;
          case 16:
            this.NoStandardDescriptorAccessor = input.ReadBool();
            continue;
          case 24:
            this.Deprecated = input.ReadBool();
            continue;
          case 56:
            this.MapEntry = input.ReadBool();
            continue;
          case 7994:
            this.uninterpretedOption_.AddEntriesFrom(input, MessageOptions._repeated_uninterpretedOption_codec);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
