// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.OneofDescriptorProto
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class OneofDescriptorProto : 
    IMessage<OneofDescriptorProto>,
    IMessage,
    IEquatable<OneofDescriptorProto>,
    IDeepCloneable<OneofDescriptorProto>
  {
    public const int NameFieldNumber = 1;
    private static readonly MessageParser<OneofDescriptorProto> _parser = new MessageParser<OneofDescriptorProto>((Func<OneofDescriptorProto>) (() => new OneofDescriptorProto()));
    private string name_ = "";

    public static MessageParser<OneofDescriptorProto> Parser => OneofDescriptorProto._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[4];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => OneofDescriptorProto.Descriptor;

    public OneofDescriptorProto()
    {
    }

    public OneofDescriptorProto(OneofDescriptorProto other)
      : this()
    {
      this.name_ = other.name_;
    }

    public OneofDescriptorProto Clone() => new OneofDescriptorProto(this);

    public string Name
    {
      get => this.name_;
      set => this.name_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public override bool Equals(object other) => this.Equals(other as OneofDescriptorProto);

    public bool Equals(OneofDescriptorProto other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.Name != other.Name));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Name.Length == 0)
        return;
      output.WriteRawTag((byte) 10);
      output.WriteString(this.Name);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      return num;
    }

    public void MergeFrom(OneofDescriptorProto other)
    {
      if (other == null || other.Name.Length == 0)
        return;
      this.Name = other.Name;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          input.SkipLastField();
        else
          this.Name = input.ReadString();
      }
    }
  }
}
