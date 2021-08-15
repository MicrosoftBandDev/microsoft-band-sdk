// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Option
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Option : IMessage<Option>, IMessage, IEquatable<Option>, IDeepCloneable<Option>
  {
    public const int NameFieldNumber = 1;
    public const int ValueFieldNumber = 2;
    private static readonly MessageParser<Option> _parser = new MessageParser<Option>((Func<Option>) (() => new Option()));
    private string name_ = "";
    private Any value_;

    public static MessageParser<Option> Parser => Option._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Type.Descriptor.MessageTypes[4];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Option.Descriptor;

    public Option()
    {
    }

    public Option(Option other)
      : this()
    {
      this.name_ = other.name_;
      this.Value = other.value_ != null ? other.Value.Clone() : (Any) null;
    }

    public Option Clone() => new Option(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public Any Value
    {
      get => this.value_;
      set => this.value_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as Option);

    public bool Equals(Option other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name) && object.Equals((object) this.Value, (object) other.Value));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      if (this.value_ != null)
        num ^= this.Value.GetHashCode();
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
      if (this.value_ == null)
        return;
      output.WriteRawTag((byte) 18);
      output.WriteMessage((IMessage) this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.value_ != null)
        num += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Value);
      return num;
    }

    public void MergeFrom(Option other)
    {
      if (other == null)
        return;
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.value_ == null)
        return;
      if (this.value_ == null)
        this.value_ = new Any();
      this.Value.MergeFrom(other.Value);
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
            if (this.value_ == null)
              this.value_ = new Any();
            input.ReadMessage((IMessage) this.value_);
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
