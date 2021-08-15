// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.SourceContext
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class SourceContext : 
    IMessage<SourceContext>,
    IMessage,
    IEquatable<SourceContext>,
    IDeepCloneable<SourceContext>
  {
    public const int FileNameFieldNumber = 1;
    private static readonly MessageParser<SourceContext> _parser = new MessageParser<SourceContext>((Func<SourceContext>) (() => new SourceContext()));
    private string fileName_ = "";

    public static MessageParser<SourceContext> Parser => SourceContext._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.SourceContext.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SourceContext.Descriptor;

    public SourceContext()
    {
    }

    public SourceContext(SourceContext other)
      : this()
    {
      this.fileName_ = other.fileName_;
    }

    public SourceContext Clone() => new SourceContext(this);

    public string FileName
    {
      get => this.fileName_;
      set => this.fileName_ = Preconditions.CheckNotNull<string>(value, nameof (value));
    }

    public override bool Equals(object other) => this.Equals(other as SourceContext);

    public bool Equals(SourceContext other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || !(this.FileName != other.FileName));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.FileName.Length != 0)
        num ^= this.FileName.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.FileName.Length == 0)
        return;
      output.WriteRawTag((byte) 10);
      output.WriteString(this.FileName);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.FileName.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.FileName);
      return num;
    }

    public void MergeFrom(SourceContext other)
    {
      if (other == null || other.FileName.Length == 0)
        return;
      this.FileName = other.FileName;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          input.SkipLastField();
        else
          this.FileName = input.ReadString();
      }
    }
  }
}
