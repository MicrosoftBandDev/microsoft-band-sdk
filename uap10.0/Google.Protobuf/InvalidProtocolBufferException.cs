// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.InvalidProtocolBufferException
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System.IO;

namespace Google.Protobuf
{
  public sealed class InvalidProtocolBufferException : IOException
  {
    internal InvalidProtocolBufferException(string message)
      : base(message)
    {
    }

    internal static InvalidProtocolBufferException MoreDataAvailable() => new InvalidProtocolBufferException("Completed reading a message while more data was available in the stream.");

    internal static InvalidProtocolBufferException TruncatedMessage() => new InvalidProtocolBufferException("While parsing a protocol message, the input ended unexpectedly in the middle of a field.  This could mean either than the input has been truncated or that an embedded message misreported its own length.");

    internal static InvalidProtocolBufferException NegativeSize() => new InvalidProtocolBufferException("CodedInputStream encountered an embedded string or message which claimed to have negative size.");

    internal static InvalidProtocolBufferException MalformedVarint() => new InvalidProtocolBufferException("CodedInputStream encountered a malformed varint.");

    internal static InvalidProtocolBufferException InvalidTag() => new InvalidProtocolBufferException("Protocol message contained an invalid tag (zero).");

    internal static InvalidProtocolBufferException InvalidEndTag() => new InvalidProtocolBufferException("Protocol message end-group tag did not match expected tag.");

    internal static InvalidProtocolBufferException RecursionLimitExceeded() => new InvalidProtocolBufferException("Protocol message had too many levels of nesting.  May be malicious.  Use CodedInputStream.SetRecursionLimit() to increase the depth limit.");

    internal static InvalidProtocolBufferException SizeLimitExceeded() => new InvalidProtocolBufferException("Protocol message was too large.  May be malicious.  Use CodedInputStream.SetSizeLimit() to increase the size limit.");

    internal static InvalidProtocolBufferException InvalidMessageStreamTag() => new InvalidProtocolBufferException("Stream of protocol messages had invalid tag. Expected tag is length-delimited field 1.");
  }
}
