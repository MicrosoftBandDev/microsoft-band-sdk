// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.FileDescriptorSet
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class FileDescriptorSet : 
    IMessage<FileDescriptorSet>,
    IMessage,
    IEquatable<FileDescriptorSet>,
    IDeepCloneable<FileDescriptorSet>
  {
    public const int FileFieldNumber = 1;
    private static readonly MessageParser<FileDescriptorSet> _parser = new MessageParser<FileDescriptorSet>((Func<FileDescriptorSet>) (() => new FileDescriptorSet()));
    private static readonly FieldCodec<FileDescriptorProto> _repeated_file_codec = FieldCodec.ForMessage<FileDescriptorProto>(10U, FileDescriptorProto.Parser);
    private readonly RepeatedField<FileDescriptorProto> file_ = new RepeatedField<FileDescriptorProto>();

    public static MessageParser<FileDescriptorSet> Parser => FileDescriptorSet._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FileDescriptorSet.Descriptor;

    public FileDescriptorSet()
    {
    }

    public FileDescriptorSet(FileDescriptorSet other)
      : this()
    {
      this.file_ = other.file_.Clone();
    }

    public FileDescriptorSet Clone() => new FileDescriptorSet(this);

    public RepeatedField<FileDescriptorProto> File => this.file_;

    public override bool Equals(object other) => this.Equals(other as FileDescriptorSet);

    public bool Equals(FileDescriptorSet other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.file_.Equals(other.file_));

    public override int GetHashCode() => 1 ^ this.file_.GetHashCode();

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output) => this.file_.WriteTo(output, FileDescriptorSet._repeated_file_codec);

    public int CalculateSize() => 0 + this.file_.CalculateSize(FileDescriptorSet._repeated_file_codec);

    public void MergeFrom(FileDescriptorSet other)
    {
      if (other == null)
        return;
      this.file_.Add(other.file_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          input.SkipLastField();
        else
          this.file_.AddEntriesFrom(input, FileDescriptorSet._repeated_file_codec);
      }
    }
  }
}
