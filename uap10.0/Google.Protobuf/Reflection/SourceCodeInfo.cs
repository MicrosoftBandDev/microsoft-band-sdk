// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.SourceCodeInfo
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal sealed class SourceCodeInfo : 
    IMessage<SourceCodeInfo>,
    IMessage,
    IEquatable<SourceCodeInfo>,
    IDeepCloneable<SourceCodeInfo>
  {
    public const int LocationFieldNumber = 1;
    private static readonly MessageParser<SourceCodeInfo> _parser = new MessageParser<SourceCodeInfo>((Func<SourceCodeInfo>) (() => new SourceCodeInfo()));
    private static readonly FieldCodec<SourceCodeInfo.Types.Location> _repeated_location_codec = FieldCodec.ForMessage<SourceCodeInfo.Types.Location>(10U, SourceCodeInfo.Types.Location.Parser);
    private readonly RepeatedField<SourceCodeInfo.Types.Location> location_ = new RepeatedField<SourceCodeInfo.Types.Location>();

    public static MessageParser<SourceCodeInfo> Parser => SourceCodeInfo._parser;

    public static MessageDescriptor Descriptor => DescriptorProtoFile.Descriptor.MessageTypes[17];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SourceCodeInfo.Descriptor;

    public SourceCodeInfo()
    {
    }

    public SourceCodeInfo(SourceCodeInfo other)
      : this()
    {
      this.location_ = other.location_.Clone();
    }

    public SourceCodeInfo Clone() => new SourceCodeInfo(this);

    public RepeatedField<SourceCodeInfo.Types.Location> Location => this.location_;

    public override bool Equals(object other) => this.Equals(other as SourceCodeInfo);

    public bool Equals(SourceCodeInfo other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.location_.Equals(other.location_));

    public override int GetHashCode() => 1 ^ this.location_.GetHashCode();

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output) => this.location_.WriteTo(output, SourceCodeInfo._repeated_location_codec);

    public int CalculateSize() => 0 + this.location_.CalculateSize(SourceCodeInfo._repeated_location_codec);

    public void MergeFrom(SourceCodeInfo other)
    {
      if (other == null)
        return;
      this.location_.Add(other.location_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          input.SkipLastField();
        else
          this.location_.AddEntriesFrom(input, SourceCodeInfo._repeated_location_codec);
      }
    }

    [DebuggerNonUserCode]
    public static class Types
    {
      [DebuggerNonUserCode]
      internal sealed class Location : 
        IMessage<SourceCodeInfo.Types.Location>,
        IMessage,
        IEquatable<SourceCodeInfo.Types.Location>,
        IDeepCloneable<SourceCodeInfo.Types.Location>
      {
        public const int PathFieldNumber = 1;
        public const int SpanFieldNumber = 2;
        public const int LeadingCommentsFieldNumber = 3;
        public const int TrailingCommentsFieldNumber = 4;
        public const int LeadingDetachedCommentsFieldNumber = 6;
        private static readonly MessageParser<SourceCodeInfo.Types.Location> _parser = new MessageParser<SourceCodeInfo.Types.Location>((Func<SourceCodeInfo.Types.Location>) (() => new SourceCodeInfo.Types.Location()));
        private static readonly FieldCodec<int> _repeated_path_codec = FieldCodec.ForInt32(10U);
        private readonly RepeatedField<int> path_ = new RepeatedField<int>();
        private static readonly FieldCodec<int> _repeated_span_codec = FieldCodec.ForInt32(18U);
        private readonly RepeatedField<int> span_ = new RepeatedField<int>();
        private string leadingComments_ = "";
        private string trailingComments_ = "";
        private static readonly FieldCodec<string> _repeated_leadingDetachedComments_codec = FieldCodec.ForString(50U);
        private readonly RepeatedField<string> leadingDetachedComments_ = new RepeatedField<string>();

        public static MessageParser<SourceCodeInfo.Types.Location> Parser => SourceCodeInfo.Types.Location._parser;

        public static MessageDescriptor Descriptor => SourceCodeInfo.Descriptor.NestedTypes[0];

        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SourceCodeInfo.Types.Location.Descriptor;

        public Location()
        {
        }

        public Location(SourceCodeInfo.Types.Location other)
          : this()
        {
          this.path_ = other.path_.Clone();
          this.span_ = other.span_.Clone();
          this.leadingComments_ = other.leadingComments_;
          this.trailingComments_ = other.trailingComments_;
          this.leadingDetachedComments_ = other.leadingDetachedComments_.Clone();
        }

        public SourceCodeInfo.Types.Location Clone() => new SourceCodeInfo.Types.Location(this);

        public RepeatedField<int> Path => this.path_;

        public RepeatedField<int> Span => this.span_;

        public string LeadingComments
        {
          get => this.leadingComments_;
          set => this.leadingComments_ = Preconditions.CheckNotNull<string>(value, nameof (value));
        }

        public string TrailingComments
        {
          get => this.trailingComments_;
          set => this.trailingComments_ = Preconditions.CheckNotNull<string>(value, nameof (value));
        }

        public RepeatedField<string> LeadingDetachedComments => this.leadingDetachedComments_;

        public override bool Equals(object other) => this.Equals(other as SourceCodeInfo.Types.Location);

        public bool Equals(SourceCodeInfo.Types.Location other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.path_.Equals(other.path_) && this.span_.Equals(other.span_) && (!(this.LeadingComments != other.LeadingComments) && !(this.TrailingComments != other.TrailingComments)) && this.leadingDetachedComments_.Equals(other.leadingDetachedComments_));

        public override int GetHashCode()
        {
          int num = 1 ^ this.path_.GetHashCode() ^ this.span_.GetHashCode();
          if (this.LeadingComments.Length != 0)
            num ^= this.LeadingComments.GetHashCode();
          if (this.TrailingComments.Length != 0)
            num ^= this.TrailingComments.GetHashCode();
          return num ^ this.leadingDetachedComments_.GetHashCode();
        }

        public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

        public void WriteTo(CodedOutputStream output)
        {
          this.path_.WriteTo(output, SourceCodeInfo.Types.Location._repeated_path_codec);
          this.span_.WriteTo(output, SourceCodeInfo.Types.Location._repeated_span_codec);
          if (this.LeadingComments.Length != 0)
          {
            output.WriteRawTag((byte) 26);
            output.WriteString(this.LeadingComments);
          }
          if (this.TrailingComments.Length != 0)
          {
            output.WriteRawTag((byte) 34);
            output.WriteString(this.TrailingComments);
          }
          this.leadingDetachedComments_.WriteTo(output, SourceCodeInfo.Types.Location._repeated_leadingDetachedComments_codec);
        }

        public int CalculateSize()
        {
          int num = 0 + this.path_.CalculateSize(SourceCodeInfo.Types.Location._repeated_path_codec) + this.span_.CalculateSize(SourceCodeInfo.Types.Location._repeated_span_codec);
          if (this.LeadingComments.Length != 0)
            num += 1 + CodedOutputStream.ComputeStringSize(this.LeadingComments);
          if (this.TrailingComments.Length != 0)
            num += 1 + CodedOutputStream.ComputeStringSize(this.TrailingComments);
          return num + this.leadingDetachedComments_.CalculateSize(SourceCodeInfo.Types.Location._repeated_leadingDetachedComments_codec);
        }

        public void MergeFrom(SourceCodeInfo.Types.Location other)
        {
          if (other == null)
            return;
          this.path_.Add(other.path_);
          this.span_.Add(other.span_);
          if (other.LeadingComments.Length != 0)
            this.LeadingComments = other.LeadingComments;
          if (other.TrailingComments.Length != 0)
            this.TrailingComments = other.TrailingComments;
          this.leadingDetachedComments_.Add(other.leadingDetachedComments_);
        }

        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
              case 10:
                this.path_.AddEntriesFrom(input, SourceCodeInfo.Types.Location._repeated_path_codec);
                continue;
              case 16:
              case 18:
                this.span_.AddEntriesFrom(input, SourceCodeInfo.Types.Location._repeated_span_codec);
                continue;
              case 26:
                this.LeadingComments = input.ReadString();
                continue;
              case 34:
                this.TrailingComments = input.ReadString();
                continue;
              case 50:
                this.leadingDetachedComments_.AddEntriesFrom(input, SourceCodeInfo.Types.Location._repeated_leadingDetachedComments_codec);
                continue;
              default:
                input.SkipLastField();
                continue;
            }
          }
        }
      }
    }
  }
}
