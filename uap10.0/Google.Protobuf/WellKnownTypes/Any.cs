// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Any
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Any : IMessage<Any>, IMessage, IEquatable<Any>, IDeepCloneable<Any>
  {
    public const int TypeUrlFieldNumber = 1;
    public const int ValueFieldNumber = 2;
    private static readonly MessageParser<Any> _parser = new MessageParser<Any>((Func<Any>) (() => new Any()));
    private string typeUrl_ = "";
    private ByteString value_ = ByteString.Empty;

    public static MessageParser<Any> Parser => Any._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Any.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Any.Descriptor;

    public Any()
    {
    }

    public Any(Any other)
      : this()
    {
      this.typeUrl_ = other.typeUrl_;
      this.value_ = other.value_;
    }

    public Any Clone() => new Any(this);

    public string TypeUrl
    {
      get => this.typeUrl_;
      set => this.typeUrl_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public ByteString Value
    {
      get => this.value_;
      set => this.value_ = Preconditions.CheckNotNull<ByteString>(value, nameof (value));
    }

    public override bool Equals(object other) => this.Equals(other as Any);

    public bool Equals(Any other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.TypeUrl != other.TypeUrl) && !(this.Value != other.Value));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.TypeUrl.Length != 0)
        num ^= this.TypeUrl.GetHashCode();
      if (this.Value.Length != 0)
        num ^= this.Value.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.TypeUrl.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.TypeUrl);
      }
      if (this.Value.Length == 0)
        return;
      output.WriteRawTag((byte) 18);
      output.WriteBytes(this.Value);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.TypeUrl.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.TypeUrl);
      if (this.Value.Length != 0)
        num += 1 + CodedOutputStream.ComputeBytesSize(this.Value);
      return num;
    }

    public void MergeFrom(Any other)
    {
      if (other == null)
        return;
      if (other.TypeUrl.Length != 0)
        this.TypeUrl = other.TypeUrl;
      if (other.Value.Length == 0)
        return;
      this.Value = other.Value;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 10:
            this.TypeUrl = input.ReadString();
            continue;
          case 18:
            this.Value = input.ReadBytes();
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }
  }
}
