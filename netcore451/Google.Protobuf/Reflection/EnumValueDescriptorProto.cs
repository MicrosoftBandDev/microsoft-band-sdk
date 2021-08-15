// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.EnumValueDescriptorProto
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class EnumValueDescriptorProto : 
    IMessage<EnumValueDescriptorProto>,
    IMessage,
    IEquatable<EnumValueDescriptorProto>,
    IDeepCloneable<EnumValueDescriptorProto>
  {
    public const int NameFieldNumber = 1;
    public const int NumberFieldNumber = 2;
    public const int OptionsFieldNumber = 3;
    private static readonly MessageParser<EnumValueDescriptorProto> _parser = new MessageParser<EnumValueDescriptorProto>((Func<EnumValueDescriptorProto>) (() => new EnumValueDescriptorProto()));
    private string name_ = "";
    private int number_;
    private EnumValueOptions options_;

    public static MessageParser<EnumValueDescriptorProto> Parser => EnumValueDescriptorProto._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[6];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => EnumValueDescriptorProto.Descriptor;

    public EnumValueDescriptorProto()
    {
    }

    public EnumValueDescriptorProto(EnumValueDescriptorProto other)
      : this()
    {
      this.name_ = other.name_;
      this.number_ = other.number_;
      this.Options = other.options_ != null ? other.Options.Clone() : (EnumValueOptions) null;
    }

    public EnumValueDescriptorProto Clone() => new EnumValueDescriptorProto(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public int Number
    {
      get => this.number_;
      set => this.number_ = value;
    }

    public EnumValueOptions Options
    {
      get => this.options_;
      set => this.options_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as EnumValueDescriptorProto);

    public bool Equals(EnumValueDescriptorProto other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && this.Number == other.Number && object.Equals((object) this.Options, (object) other.Options));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      if (this.Number != 0)
        num ^= this.Number.GetHashCode();
      if (this.options_ != null)
        num ^= this.Options.GetHashCode();
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
      if (this.Number != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.Number);
      }
      if (this.options_ == null)
        return;
      output.WriteRawTag((byte) 26);
      output.WriteMessage((IMessage) this.Options);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.Number != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Number);
      if (this.options_ != null)
        num += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Options);
      return num;
    }

    public void MergeFrom(EnumValueDescriptorProto other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.Number != 0)
        this.Number = other.Number;
      if (other.options_ == null)
        return;
      if (this.options_ == null)
        this.options_ = new EnumValueOptions();
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
          case 16:
            this.Number = input.ReadInt32();
            continue;
          case 26:
            if (this.options_ == null)
              this.options_ = new EnumValueOptions();
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
