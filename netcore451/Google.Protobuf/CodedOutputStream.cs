// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.CodedOutputStream
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.IO;
using System.Text;

namespace Google.Protobuf
{
  public sealed class CodedOutputStream
  {
    private const int LittleEndian64Size = 8;
    private const int LittleEndian32Size = 4;
    internal static readonly Encoding Utf8Encoding = Encoding.UTF8;
    public static readonly int DefaultBufferSize = 4096;
    private readonly byte[] buffer;
    private readonly int limit;
    private int position;
    private readonly Stream output;

    public static int ComputeDoubleSize(double value) => 8;

    public static int ComputeFloatSize(float value) => 4;

    public static int ComputeUInt64Size(ulong value) => CodedOutputStream.ComputeRawVarint64Size(value);

    public static int ComputeInt64Size(long value) => CodedOutputStream.ComputeRawVarint64Size((ulong) value);

    public static int ComputeInt32Size(int value) => value >= 0 ? CodedOutputStream.ComputeRawVarint32Size((uint) value) : 10;

    public static int ComputeFixed64Size(ulong value) => 8;

    public static int ComputeFixed32Size(uint value) => 4;

    public static int ComputeBoolSize(bool value) => 1;

    public static int ComputeStringSize(string value)
    {
      int byteCount = CodedOutputStream.Utf8Encoding.GetByteCount(value);
      return CodedOutputStream.ComputeLengthSize(byteCount) + byteCount;
    }

    public static int ComputeGroupSize(IMessage value) => value.CalculateSize();

    public static int ComputeMessageSize(IMessage value)
    {
      int size = value.CalculateSize();
      return CodedOutputStream.ComputeLengthSize(size) + size;
    }

    public static int ComputeBytesSize(ByteString value) => CodedOutputStream.ComputeLengthSize(value.Length) + value.Length;

    public static int ComputeUInt32Size(uint value) => CodedOutputStream.ComputeRawVarint32Size(value);

    public static int ComputeEnumSize(int value) => CodedOutputStream.ComputeInt32Size(value);

    public static int ComputeSFixed32Size(int value) => 4;

    public static int ComputeSFixed64Size(long value) => 8;

    public static int ComputeSInt32Size(int value) => CodedOutputStream.ComputeRawVarint32Size(CodedOutputStream.EncodeZigZag32(value));

    public static int ComputeSInt64Size(long value) => CodedOutputStream.ComputeRawVarint64Size(CodedOutputStream.EncodeZigZag64(value));

    public static int ComputeLengthSize(int length) => CodedOutputStream.ComputeRawVarint32Size((uint) length);

    public static int ComputeRawVarint32Size(uint value)
    {
      if (((int) value & (int) sbyte.MinValue) == 0)
        return 1;
      if (((int) value & -16384) == 0)
        return 2;
      if (((int) value & -2097152) == 0)
        return 3;
      return ((int) value & -268435456) == 0 ? 4 : 5;
    }

    public static int ComputeRawVarint64Size(ulong value)
    {
      if (((long) value & (long) sbyte.MinValue) == 0L)
        return 1;
      if (((long) value & -16384L) == 0L)
        return 2;
      if (((long) value & -2097152L) == 0L)
        return 3;
      if (((long) value & -268435456L) == 0L)
        return 4;
      if (((long) value & -34359738368L) == 0L)
        return 5;
      if (((long) value & -4398046511104L) == 0L)
        return 6;
      if (((long) value & -562949953421312L) == 0L)
        return 7;
      if (((long) value & -72057594037927936L) == 0L)
        return 8;
      return ((long) value & long.MinValue) == 0L ? 9 : 10;
    }

    public static int ComputeTagSize(int fieldNumber) => CodedOutputStream.ComputeRawVarint32Size(WireFormat.MakeTag(fieldNumber, WireFormat.WireType.Varint));

    public CodedOutputStream(byte[] flatArray)
      : this(flatArray, 0, flatArray.Length)
    {
    }

    private CodedOutputStream(byte[] buffer, int offset, int length)
    {
      this.output = (Stream) null;
      this.buffer = buffer;
      this.position = offset;
      this.limit = offset + length;
    }

    private CodedOutputStream(Stream output, byte[] buffer)
    {
      this.output = output;
      this.buffer = buffer;
      this.position = 0;
      this.limit = buffer.Length;
    }

    public CodedOutputStream(Stream output)
      : this(output, CodedOutputStream.DefaultBufferSize)
    {
    }

    public CodedOutputStream(Stream output, int bufferSize)
      : this(output, new byte[bufferSize])
    {
    }

    public long Position => this.output != null ? this.output.Position + (long) this.position : (long) this.position;

    public void WriteDouble(double value) => this.WriteRawLittleEndian64((ulong) BitConverter.DoubleToInt64Bits(value));

    public void WriteFloat(float value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      if (!BitConverter.IsLittleEndian)
        ByteArray.Reverse(bytes);
      if (this.limit - this.position >= 4)
      {
        this.buffer[this.position++] = bytes[0];
        this.buffer[this.position++] = bytes[1];
        this.buffer[this.position++] = bytes[2];
        this.buffer[this.position++] = bytes[3];
      }
      else
        this.WriteRawBytes(bytes, 0, 4);
    }

    public void WriteUInt64(ulong value) => this.WriteRawVarint64(value);

    public void WriteInt64(long value) => this.WriteRawVarint64((ulong) value);

    public void WriteInt32(int value)
    {
      if (value >= 0)
        this.WriteRawVarint32((uint) value);
      else
        this.WriteRawVarint64((ulong) value);
    }

    public void WriteFixed64(ulong value) => this.WriteRawLittleEndian64(value);

    public void WriteFixed32(uint value) => this.WriteRawLittleEndian32(value);

    public void WriteBool(bool value) => this.WriteRawByte(value ? (byte) 1 : (byte) 0);

    public void WriteString(string value)
    {
      int byteCount = CodedOutputStream.Utf8Encoding.GetByteCount(value);
      this.WriteLength(byteCount);
      if (this.limit - this.position >= byteCount)
      {
        if (byteCount == value.Length)
        {
          for (int index = 0; index < byteCount; ++index)
            this.buffer[this.position + index] = (byte) value[index];
        }
        else
          CodedOutputStream.Utf8Encoding.GetBytes(value, 0, value.Length, this.buffer, this.position);
        this.position += byteCount;
      }
      else
        this.WriteRawBytes(CodedOutputStream.Utf8Encoding.GetBytes(value));
    }

    public void WriteMessage(IMessage value)
    {
      this.WriteLength(value.CalculateSize());
      value.WriteTo(this);
    }

    public void WriteBytes(ByteString value)
    {
      this.WriteLength(value.Length);
      value.WriteRawBytesTo(this);
    }

    public void WriteUInt32(uint value) => this.WriteRawVarint32(value);

    public void WriteEnum(int value) => this.WriteInt32(value);

    public void WriteSFixed32(int value) => this.WriteRawLittleEndian32((uint) value);

    public void WriteSFixed64(long value) => this.WriteRawLittleEndian64((ulong) value);

    public void WriteSInt32(int value) => this.WriteRawVarint32(CodedOutputStream.EncodeZigZag32(value));

    public void WriteSInt64(long value) => this.WriteRawVarint64(CodedOutputStream.EncodeZigZag64(value));

    public void WriteLength(int length) => this.WriteRawVarint32((uint) length);

    public void WriteTag(int fieldNumber, WireFormat.WireType type) => this.WriteRawVarint32(WireFormat.MakeTag(fieldNumber, type));

    public void WriteTag(uint tag) => this.WriteRawVarint32(tag);

    public void WriteRawTag(byte b1) => this.WriteRawByte(b1);

    public void WriteRawTag(byte b1, byte b2)
    {
      this.WriteRawByte(b1);
      this.WriteRawByte(b2);
    }

    public void WriteRawTag(byte b1, byte b2, byte b3)
    {
      this.WriteRawByte(b1);
      this.WriteRawByte(b2);
      this.WriteRawByte(b3);
    }

    public void WriteRawTag(byte b1, byte b2, byte b3, byte b4)
    {
      this.WriteRawByte(b1);
      this.WriteRawByte(b2);
      this.WriteRawByte(b3);
      this.WriteRawByte(b4);
    }

    public void WriteRawTag(byte b1, byte b2, byte b3, byte b4, byte b5)
    {
      this.WriteRawByte(b1);
      this.WriteRawByte(b2);
      this.WriteRawByte(b3);
      this.WriteRawByte(b4);
      this.WriteRawByte(b5);
    }

    internal void WriteRawVarint32(uint value)
    {
      if (value < 128U && this.position < this.limit)
      {
        this.buffer[this.position++] = (byte) value;
      }
      else
      {
        for (; value > (uint) sbyte.MaxValue && this.position < this.limit; value >>= 7)
          this.buffer[this.position++] = (byte) ((int) value & (int) sbyte.MaxValue | 128);
        for (; value > (uint) sbyte.MaxValue; value >>= 7)
          this.WriteRawByte((byte) ((int) value & (int) sbyte.MaxValue | 128));
        if (this.position < this.limit)
          this.buffer[this.position++] = (byte) value;
        else
          this.WriteRawByte((byte) value);
      }
    }

    internal void WriteRawVarint64(ulong value)
    {
      for (; value > (ulong) sbyte.MaxValue && this.position < this.limit; value >>= 7)
        this.buffer[this.position++] = (byte) (value & (ulong) sbyte.MaxValue | 128UL);
      for (; value > (ulong) sbyte.MaxValue; value >>= 7)
        this.WriteRawByte((byte) (value & (ulong) sbyte.MaxValue | 128UL));
      if (this.position < this.limit)
        this.buffer[this.position++] = (byte) value;
      else
        this.WriteRawByte((byte) value);
    }

    internal void WriteRawLittleEndian32(uint value)
    {
      if (this.position + 4 > this.limit)
      {
        this.WriteRawByte((byte) value);
        this.WriteRawByte((byte) (value >> 8));
        this.WriteRawByte((byte) (value >> 16));
        this.WriteRawByte((byte) (value >> 24));
      }
      else
      {
        this.buffer[this.position++] = (byte) value;
        this.buffer[this.position++] = (byte) (value >> 8);
        this.buffer[this.position++] = (byte) (value >> 16);
        this.buffer[this.position++] = (byte) (value >> 24);
      }
    }

    internal void WriteRawLittleEndian64(ulong value)
    {
      if (this.position + 8 > this.limit)
      {
        this.WriteRawByte((byte) value);
        this.WriteRawByte((byte) (value >> 8));
        this.WriteRawByte((byte) (value >> 16));
        this.WriteRawByte((byte) (value >> 24));
        this.WriteRawByte((byte) (value >> 32));
        this.WriteRawByte((byte) (value >> 40));
        this.WriteRawByte((byte) (value >> 48));
        this.WriteRawByte((byte) (value >> 56));
      }
      else
      {
        this.buffer[this.position++] = (byte) value;
        this.buffer[this.position++] = (byte) (value >> 8);
        this.buffer[this.position++] = (byte) (value >> 16);
        this.buffer[this.position++] = (byte) (value >> 24);
        this.buffer[this.position++] = (byte) (value >> 32);
        this.buffer[this.position++] = (byte) (value >> 40);
        this.buffer[this.position++] = (byte) (value >> 48);
        this.buffer[this.position++] = (byte) (value >> 56);
      }
    }

    internal void WriteRawByte(byte value)
    {
      if (this.position == this.limit)
        this.RefreshBuffer();
      this.buffer[this.position++] = value;
    }

    internal void WriteRawByte(uint value) => this.WriteRawByte((byte) value);

    internal void WriteRawBytes(byte[] value) => this.WriteRawBytes(value, 0, value.Length);

    internal void WriteRawBytes(byte[] value, int offset, int length)
    {
      if (this.limit - this.position >= length)
      {
        ByteArray.Copy(value, offset, this.buffer, this.position, length);
        this.position += length;
      }
      else
      {
        int count = this.limit - this.position;
        ByteArray.Copy(value, offset, this.buffer, this.position, count);
        offset += count;
        length -= count;
        this.position = this.limit;
        this.RefreshBuffer();
        if (length <= this.limit)
        {
          ByteArray.Copy(value, offset, this.buffer, 0, length);
          this.position = length;
        }
        else
          this.output.Write(value, offset, length);
      }
    }

    internal static uint EncodeZigZag32(int n) => (uint) (n << 1 ^ n >> 31);

    internal static ulong EncodeZigZag64(long n) => (ulong) (n << 1 ^ n >> 63);

    private void RefreshBuffer()
    {
      if (this.output == null)
        throw new CodedOutputStream.OutOfSpaceException();
      this.output.Write(this.buffer, 0, this.position);
      this.position = 0;
    }

    public void Flush()
    {
      if (this.output == null)
        return;
      this.RefreshBuffer();
    }

    public void CheckNoSpaceLeft()
    {
      if (this.SpaceLeft != 0)
        throw new InvalidOperationException("Did not write as much data as expected.");
    }

    public int SpaceLeft
    {
      get
      {
        if (this.output == null)
          return this.limit - this.position;
        throw new InvalidOperationException("SpaceLeft can only be called on CodedOutputStreams that are writing to a flat array.");
      }
    }

    public sealed class OutOfSpaceException : IOException
    {
      internal OutOfSpaceException()
        : base("CodedOutputStream was writing to a flat byte array and ran out of space.")
      {
      }
    }
  }
}
