// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.FieldMask
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
  public sealed class FieldMask : 
    IMessage<FieldMask>,
    IMessage,
    IEquatable<FieldMask>,
    IDeepCloneable<FieldMask>
  {
    public const int PathsFieldNumber = 1;
    private static readonly MessageParser<FieldMask> _parser = new MessageParser<FieldMask>((Func<FieldMask>) (() => new FieldMask()));
    private static readonly FieldCodec<string> _repeated_paths_codec = FieldCodec.ForString(10U);
    private readonly RepeatedField<string> paths_ = new RepeatedField<string>();

    public static MessageParser<FieldMask> Parser => FieldMask._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.FieldMask.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FieldMask.Descriptor;

    public FieldMask()
    {
    }

    public FieldMask(FieldMask other)
      : this()
    {
      this.paths_ = other.paths_.Clone();
    }

    public FieldMask Clone() => new FieldMask(this);

    public RepeatedField<string> Paths => this.paths_;

    public override bool Equals(object other) => this.Equals(other as FieldMask);

    public bool Equals(FieldMask other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.paths_.Equals(other.paths_));

    public override int GetHashCode() => 1 ^ this.paths_.GetHashCode();

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output) => this.paths_.WriteTo(output, FieldMask._repeated_paths_codec);

    public int CalculateSize() => 0 + this.paths_.CalculateSize(FieldMask._repeated_paths_codec);

    public void MergeFrom(FieldMask other)
    {
      if (other == null)
        return;
      this.paths_.Add(other.paths_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          input.SkipLastField();
        else
          this.paths_.AddEntriesFrom(input, FieldMask._repeated_paths_codec);
      }
    }
  }
}
