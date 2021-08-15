// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.MessageExtensions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System.IO;

namespace Google.Protobuf
{
  public static class MessageExtensions
  {
    public static void MergeFrom(this IMessage message, byte[] data)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      Preconditions.CheckNotNull<byte[]>(data, nameof (data));
      CodedInputStream input = new CodedInputStream(data);
      message.MergeFrom(input);
      input.CheckReadEndOfStreamTag();
    }

    public static void MergeFrom(this IMessage message, ByteString data)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      Preconditions.CheckNotNull<ByteString>(data, nameof (data));
      CodedInputStream codedInput = data.CreateCodedInput();
      message.MergeFrom(codedInput);
      codedInput.CheckReadEndOfStreamTag();
    }

    public static void MergeFrom(this IMessage message, Stream input)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      Preconditions.CheckNotNull<Stream>(input, nameof (input));
      CodedInputStream input1 = new CodedInputStream(input);
      message.MergeFrom(input1);
      input1.CheckReadEndOfStreamTag();
    }

    public static void MergeDelimitedFrom(this IMessage message, Stream input)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      Preconditions.CheckNotNull<Stream>(input, nameof (input));
      int size = (int) CodedInputStream.ReadRawVarint32(input);
      Stream input1 = (Stream) new LimitedInputStream(input, size);
      message.MergeFrom(input1);
    }

    public static byte[] ToByteArray(this IMessage message)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      byte[] flatArray = new byte[message.CalculateSize()];
      CodedOutputStream output = new CodedOutputStream(flatArray);
      message.WriteTo(output);
      output.CheckNoSpaceLeft();
      return flatArray;
    }

    public static void WriteTo(this IMessage message, Stream output)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      Preconditions.CheckNotNull<Stream>(output, nameof (output));
      CodedOutputStream output1 = new CodedOutputStream(output);
      message.WriteTo(output1);
      output1.Flush();
    }

    public static void WriteDelimitedTo(this IMessage message, Stream output)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      Preconditions.CheckNotNull<Stream>(output, nameof (output));
      CodedOutputStream output1 = new CodedOutputStream(output);
      output1.WriteRawVarint32((uint) message.CalculateSize());
      message.WriteTo(output1);
      output1.Flush();
    }

    public static ByteString ToByteString(this IMessage message)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      return ByteString.AttachBytes(message.ToByteArray());
    }
  }
}
