// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.CodedInputStream
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Protobuf
{
  public sealed class CodedInputStream
  {
    internal const int DefaultRecursionLimit = 64;
    internal const int DefaultSizeLimit = 67108864;
    internal const int BufferSize = 4096;
    private readonly byte[] buffer;
    private int bufferSize;
    private int bufferSizeAfterLimit;
    private int bufferPos;
    private readonly Stream input;
    private uint lastTag;
    private uint nextTag;
    private bool hasNextTag;
    private int totalBytesRetired;
    private int currentLimit = int.MaxValue;
    private int recursionDepth;
    private readonly int recursionLimit;
    private readonly int sizeLimit;

    public CodedInputStream(byte[] buffer)
      : this((Stream) null, Preconditions.CheckNotNull<byte[]>(buffer, nameof (buffer)), 0, buffer.Length)
    {
    }

    public CodedInputStream(byte[] buffer, int offset, int length)
      : this((Stream) null, Preconditions.CheckNotNull<byte[]>(buffer, nameof (buffer)), offset, offset + length)
    {
      if (offset < 0 || offset > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (offset), "Offset must be within the buffer");
      if (length < 0 || offset + length > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (length), "Length must be non-negative and within the buffer");
    }

    public CodedInputStream(Stream input)
      : this(input, new byte[4096], 0, 0)
    {
      Preconditions.CheckNotNull<Stream>(input, nameof (input));
    }

    internal CodedInputStream(Stream input, byte[] buffer, int bufferPos, int bufferSize)
    {
      this.input = input;
      this.buffer = buffer;
      this.bufferPos = bufferPos;
      this.bufferSize = bufferSize;
      this.sizeLimit = 67108864;
      this.recursionLimit = 64;
    }

    internal CodedInputStream(
      Stream input,
      byte[] buffer,
      int bufferPos,
      int bufferSize,
      int sizeLimit,
      int recursionLimit)
      : this(input, buffer, bufferPos, bufferSize)
    {
      if (sizeLimit <= 0)
        throw new ArgumentOutOfRangeException(nameof (sizeLimit), "Size limit must be positive");
      if (recursionLimit <= 0)
        throw new ArgumentOutOfRangeException("recursionLimit!", "Recursion limit must be positive");
      this.sizeLimit = sizeLimit;
      this.recursionLimit = recursionLimit;
    }

    public static CodedInputStream CreateWithLimits(
      Stream input,
      int sizeLimit,
      int recursionLimit)
    {
      return new CodedInputStream(input, new byte[4096], 0, 0, sizeLimit, recursionLimit);
    }

    public long Position => this.input != null ? this.input.Position - (long) (this.bufferSize + this.bufferSizeAfterLimit - this.bufferPos) : (long) this.bufferPos;

    internal uint LastTag => this.lastTag;

    public int SizeLimit => this.sizeLimit;

    public int RecursionLimit => this.recursionLimit;

    internal void CheckReadEndOfStreamTag()
    {
      if (this.lastTag != 0U)
        throw InvalidProtocolBufferException.MoreDataAvailable();
    }

    public uint PeekTag()
    {
      if (this.hasNextTag)
        return this.nextTag;
      uint lastTag = this.lastTag;
      this.nextTag = this.ReadTag();
      this.hasNextTag = true;
      this.lastTag = lastTag;
      return this.nextTag;
    }

    public uint ReadTag()
    {
      if (this.hasNextTag)
      {
        this.lastTag = this.nextTag;
        this.hasNextTag = false;
        return this.lastTag;
      }
      if (this.bufferPos + 2 <= this.bufferSize)
      {
        int num1 = (int) this.buffer[this.bufferPos++];
        if (num1 < 128)
        {
          this.lastTag = (uint) num1;
        }
        else
        {
          int num2 = num1 & (int) sbyte.MaxValue;
          byte[] buffer = this.buffer;
          int index = this.bufferPos++;
          int num3;
          if ((num3 = (int) buffer[index]) < 128)
          {
            this.lastTag = (uint) (num2 | num3 << 7);
          }
          else
          {
            this.bufferPos -= 2;
            this.lastTag = this.ReadRawVarint32();
          }
        }
      }
      else
      {
        if (this.IsAtEnd)
        {
          this.lastTag = 0U;
          return 0;
        }
        this.lastTag = this.ReadRawVarint32();
      }
      return this.lastTag != 0U ? this.lastTag : throw InvalidProtocolBufferException.InvalidTag();
    }

    public void SkipLastField()
    {
      if (this.lastTag == 0U)
        throw new InvalidOperationException("SkipLastField cannot be called at the end of a stream");
      switch (WireFormat.GetTagWireType(this.lastTag))
      {
        case WireFormat.WireType.Varint:
          int num1 = (int) this.ReadRawVarint32();
          break;
        case WireFormat.WireType.Fixed64:
          long num2 = (long) this.ReadFixed64();
          break;
        case WireFormat.WireType.LengthDelimited:
          this.SkipRawBytes(this.ReadLength());
          break;
        case WireFormat.WireType.StartGroup:
          this.SkipGroup();
          break;
        case WireFormat.WireType.Fixed32:
          int num3 = (int) this.ReadFixed32();
          break;
      }
    }

    private void SkipGroup()
    {
      ++this.recursionDepth;
      if (this.recursionDepth >= this.recursionLimit)
        throw InvalidProtocolBufferException.RecursionLimitExceeded();
      uint tag;
      do
      {
        tag = this.ReadTag();
        if (tag == 0U)
          throw InvalidProtocolBufferException.TruncatedMessage();
        this.SkipLastField();
      }
      while (WireFormat.GetTagWireType(tag) != WireFormat.WireType.EndGroup);
      --this.recursionDepth;
    }

    public double ReadDouble() => BitConverter.Int64BitsToDouble((long) this.ReadRawLittleEndian64());

    public float ReadFloat()
    {
      if (BitConverter.IsLittleEndian && 4 <= this.bufferSize - this.bufferPos)
      {
        float single = BitConverter.ToSingle(this.buffer, this.bufferPos);
        this.bufferPos += 4;
        return single;
      }
      byte[] bytes = this.ReadRawBytes(4);
      if (!BitConverter.IsLittleEndian)
        ByteArray.Reverse(bytes);
      return BitConverter.ToSingle(bytes, 0);
    }

    public ulong ReadUInt64() => this.ReadRawVarint64();

    public long ReadInt64() => (long) this.ReadRawVarint64();

    public int ReadInt32() => (int) this.ReadRawVarint32();

    public ulong ReadFixed64() => this.ReadRawLittleEndian64();

    public uint ReadFixed32() => this.ReadRawLittleEndian32();

    public bool ReadBool() => this.ReadRawVarint32() != 0U;

    public string ReadString()
    {
      int num = this.ReadLength();
      if (num == 0)
        return "";
      if (num > this.bufferSize - this.bufferPos)
        return CodedOutputStream.Utf8Encoding.GetString(this.ReadRawBytes(num), 0, num);
      string str = CodedOutputStream.Utf8Encoding.GetString(this.buffer, this.bufferPos, num);
      this.bufferPos += num;
      return str;
    }

    public void ReadMessage(IMessage builder)
    {
      int byteLimit = this.ReadLength();
      if (this.recursionDepth >= this.recursionLimit)
        throw InvalidProtocolBufferException.RecursionLimitExceeded();
      int oldLimit = this.PushLimit(byteLimit);
      ++this.recursionDepth;
      builder.MergeFrom(this);
      this.CheckReadEndOfStreamTag();
      if (!this.ReachedLimit)
        throw InvalidProtocolBufferException.TruncatedMessage();
      --this.recursionDepth;
      this.PopLimit(oldLimit);
    }

    public ByteString ReadBytes()
    {
      int num = this.ReadLength();
      if (num > this.bufferSize - this.bufferPos || num <= 0)
        return ByteString.AttachBytes(this.ReadRawBytes(num));
      ByteString byteString = ByteString.CopyFrom(this.buffer, this.bufferPos, num);
      this.bufferPos += num;
      return byteString;
    }

    public uint ReadUInt32() => this.ReadRawVarint32();

    public int ReadEnum() => (int) this.ReadRawVarint32();

    public int ReadSFixed32() => (int) this.ReadRawLittleEndian32();

    public long ReadSFixed64() => (long) this.ReadRawLittleEndian64();

    public int ReadSInt32() => CodedInputStream.DecodeZigZag32(this.ReadRawVarint32());

    public long ReadSInt64() => CodedInputStream.DecodeZigZag64(this.ReadRawVarint64());

    public int ReadLength() => (int) this.ReadRawVarint32();

    public bool MaybeConsumeTag(uint tag)
    {
      if ((int) this.PeekTag() != (int) tag)
        return false;
      this.hasNextTag = false;
      return true;
    }

    private uint SlowReadRawVarint32()
    {
      int num1 = (int) this.ReadRawByte();
      if (num1 < 128)
        return (uint) num1;
      int num2 = num1 & (int) sbyte.MaxValue;
      int num3;
      int num4;
      if ((num3 = (int) this.ReadRawByte()) < 128)
      {
        num4 = num2 | num3 << 7;
      }
      else
      {
        int num5 = num2 | (num3 & (int) sbyte.MaxValue) << 7;
        int num6;
        if ((num6 = (int) this.ReadRawByte()) < 128)
        {
          num4 = num5 | num6 << 14;
        }
        else
        {
          int num7 = num5 | (num6 & (int) sbyte.MaxValue) << 14;
          int num8;
          if ((num8 = (int) this.ReadRawByte()) < 128)
          {
            num4 = num7 | num8 << 21;
          }
          else
          {
            int num9;
            num4 = num7 | (num8 & (int) sbyte.MaxValue) << 21 | (num9 = (int) this.ReadRawByte()) << 28;
            if (num9 >= 128)
            {
              for (int index = 0; index < 5; ++index)
              {
                if (this.ReadRawByte() < (byte) 128)
                  return (uint) num4;
              }
              throw InvalidProtocolBufferException.MalformedVarint();
            }
          }
        }
      }
      return (uint) num4;
    }

    internal uint ReadRawVarint32()
    {
      if (this.bufferPos + 5 > this.bufferSize)
        return this.SlowReadRawVarint32();
      int num1 = (int) this.buffer[this.bufferPos++];
      if (num1 < 128)
        return (uint) num1;
      int num2 = num1 & (int) sbyte.MaxValue;
      byte[] buffer1 = this.buffer;
      int index1 = this.bufferPos++;
      int num3;
      int num4;
      if ((num3 = (int) buffer1[index1]) < 128)
      {
        num4 = num2 | num3 << 7;
      }
      else
      {
        int num5 = num2 | (num3 & (int) sbyte.MaxValue) << 7;
        byte[] buffer2 = this.buffer;
        int index2 = this.bufferPos++;
        int num6;
        if ((num6 = (int) buffer2[index2]) < 128)
        {
          num4 = num5 | num6 << 14;
        }
        else
        {
          int num7 = num5 | (num6 & (int) sbyte.MaxValue) << 14;
          byte[] buffer3 = this.buffer;
          int index3 = this.bufferPos++;
          int num8;
          if ((num8 = (int) buffer3[index3]) < 128)
          {
            num4 = num7 | num8 << 21;
          }
          else
          {
            int num9 = num7 | (num8 & (int) sbyte.MaxValue) << 21;
            byte[] buffer4 = this.buffer;
            int index4 = this.bufferPos++;
            int num10;
            int num11 = (num10 = (int) buffer4[index4]) << 28;
            num4 = num9 | num11;
            if (num10 >= 128)
            {
              for (int index5 = 0; index5 < 5; ++index5)
              {
                if (this.ReadRawByte() < (byte) 128)
                  return (uint) num4;
              }
              throw InvalidProtocolBufferException.MalformedVarint();
            }
          }
        }
      }
      return (uint) num4;
    }

    internal static uint ReadRawVarint32(Stream input)
    {
      int num1 = 0;
      int num2;
      for (num2 = 0; num2 < 32; num2 += 7)
      {
        int num3 = input.ReadByte();
        if (num3 == -1)
          throw InvalidProtocolBufferException.TruncatedMessage();
        num1 |= (num3 & (int) sbyte.MaxValue) << num2;
        if ((num3 & 128) == 0)
          return (uint) num1;
      }
      for (; num2 < 64; num2 += 7)
      {
        int num3 = input.ReadByte();
        if (num3 == -1)
          throw InvalidProtocolBufferException.TruncatedMessage();
        if ((num3 & 128) == 0)
          return (uint) num1;
      }
      throw InvalidProtocolBufferException.MalformedVarint();
    }

    internal ulong ReadRawVarint64()
    {
      int num1 = 0;
      ulong num2 = 0;
      for (; num1 < 64; num1 += 7)
      {
        byte num3 = this.ReadRawByte();
        num2 |= (ulong) ((int) num3 & (int) sbyte.MaxValue) << num1;
        if (((int) num3 & 128) == 0)
          return num2;
      }
      throw InvalidProtocolBufferException.MalformedVarint();
    }

    internal uint ReadRawLittleEndian32() => (uint) ((int) this.ReadRawByte() | (int) this.ReadRawByte() << 8 | (int) this.ReadRawByte() << 16 | (int) this.ReadRawByte() << 24);

    internal ulong ReadRawLittleEndian64() => (ulong) ((long) this.ReadRawByte() | (long) this.ReadRawByte() << 8 | (long) this.ReadRawByte() << 16 | (long) this.ReadRawByte() << 24 | (long) this.ReadRawByte() << 32 | (long) this.ReadRawByte() << 40 | (long) this.ReadRawByte() << 48 | (long) this.ReadRawByte() << 56);

    internal static int DecodeZigZag32(uint n) => (int) (n >> 1) ^ -((int) n & 1);

    internal static long DecodeZigZag64(ulong n) => (long) (n >> 1) ^ -((long) n & 1L);

    internal int PushLimit(int byteLimit)
    {
      if (byteLimit < 0)
        throw InvalidProtocolBufferException.NegativeSize();
      byteLimit += this.totalBytesRetired + this.bufferPos;
      int currentLimit = this.currentLimit;
      this.currentLimit = byteLimit <= currentLimit ? byteLimit : throw InvalidProtocolBufferException.TruncatedMessage();
      this.RecomputeBufferSizeAfterLimit();
      return currentLimit;
    }

    private void RecomputeBufferSizeAfterLimit()
    {
      this.bufferSize += this.bufferSizeAfterLimit;
      int num = this.totalBytesRetired + this.bufferSize;
      if (num > this.currentLimit)
      {
        this.bufferSizeAfterLimit = num - this.currentLimit;
        this.bufferSize -= this.bufferSizeAfterLimit;
      }
      else
        this.bufferSizeAfterLimit = 0;
    }

    internal void PopLimit(int oldLimit)
    {
      this.currentLimit = oldLimit;
      this.RecomputeBufferSizeAfterLimit();
    }

    internal bool ReachedLimit => this.currentLimit != int.MaxValue && this.totalBytesRetired + this.bufferPos >= this.currentLimit;

    public bool IsAtEnd => this.bufferPos == this.bufferSize && !this.RefillBuffer(false);

    private bool RefillBuffer(bool mustSucceed)
    {
      if (this.bufferPos < this.bufferSize)
        throw new InvalidOperationException("RefillBuffer() called when buffer wasn't empty.");
      if (this.totalBytesRetired + this.bufferSize == this.currentLimit)
      {
        if (mustSucceed)
          throw InvalidProtocolBufferException.TruncatedMessage();
        return false;
      }
      this.totalBytesRetired += this.bufferSize;
      this.bufferPos = 0;
      this.bufferSize = this.input == null ? 0 : this.input.Read(this.buffer, 0, this.buffer.Length);
      if (this.bufferSize < 0)
        throw new InvalidOperationException("Stream.Read returned a negative count");
      if (this.bufferSize == 0)
      {
        if (mustSucceed)
          throw InvalidProtocolBufferException.TruncatedMessage();
        return false;
      }
      this.RecomputeBufferSizeAfterLimit();
      int num = this.totalBytesRetired + this.bufferSize + this.bufferSizeAfterLimit;
      if (num > this.sizeLimit || num < 0)
        throw InvalidProtocolBufferException.SizeLimitExceeded();
      return true;
    }

    internal byte ReadRawByte()
    {
      if (this.bufferPos == this.bufferSize)
        this.RefillBuffer(true);
      return this.buffer[this.bufferPos++];
    }

    internal byte[] ReadRawBytes(int size)
    {
      if (size < 0)
        throw InvalidProtocolBufferException.NegativeSize();
      if (this.totalBytesRetired + this.bufferPos + size > this.currentLimit)
      {
        this.SkipRawBytes(this.currentLimit - this.totalBytesRetired - this.bufferPos);
        throw InvalidProtocolBufferException.TruncatedMessage();
      }
      if (size <= this.bufferSize - this.bufferPos)
      {
        byte[] dst = new byte[size];
        ByteArray.Copy(this.buffer, this.bufferPos, dst, 0, size);
        this.bufferPos += size;
        return dst;
      }
      if (size < this.buffer.Length)
      {
        byte[] dst = new byte[size];
        int num = this.bufferSize - this.bufferPos;
        ByteArray.Copy(this.buffer, this.bufferPos, dst, 0, num);
        this.bufferPos = this.bufferSize;
        this.RefillBuffer(true);
        while (size - num > this.bufferSize)
        {
          Buffer.BlockCopy((Array) this.buffer, 0, (Array) dst, num, this.bufferSize);
          num += this.bufferSize;
          this.bufferPos = this.bufferSize;
          this.RefillBuffer(true);
        }
        ByteArray.Copy(this.buffer, 0, dst, num, size - num);
        this.bufferPos = size - num;
        return dst;
      }
      int bufferPos = this.bufferPos;
      int bufferSize = this.bufferSize;
      this.totalBytesRetired += this.bufferSize;
      this.bufferPos = 0;
      this.bufferSize = 0;
      int val1 = size - (bufferSize - bufferPos);
      List<byte[]> numArrayList = new List<byte[]>();
      while (val1 > 0)
      {
        byte[] buffer = new byte[Math.Min(val1, this.buffer.Length)];
        int num;
        for (int offset = 0; offset < buffer.Length; offset += num)
        {
          num = this.input == null ? -1 : this.input.Read(buffer, offset, buffer.Length - offset);
          if (num <= 0)
            throw InvalidProtocolBufferException.TruncatedMessage();
          this.totalBytesRetired += num;
        }
        val1 -= buffer.Length;
        numArrayList.Add(buffer);
      }
      byte[] dst1 = new byte[size];
      int num1 = bufferSize - bufferPos;
      ByteArray.Copy(this.buffer, bufferPos, dst1, 0, num1);
      foreach (byte[] numArray in numArrayList)
      {
        Buffer.BlockCopy((Array) numArray, 0, (Array) dst1, num1, numArray.Length);
        num1 += numArray.Length;
      }
      return dst1;
    }

    private void SkipRawBytes(int size)
    {
      if (size < 0)
        throw InvalidProtocolBufferException.NegativeSize();
      if (this.totalBytesRetired + this.bufferPos + size > this.currentLimit)
      {
        this.SkipRawBytes(this.currentLimit - this.totalBytesRetired - this.bufferPos);
        throw InvalidProtocolBufferException.TruncatedMessage();
      }
      if (size <= this.bufferSize - this.bufferPos)
      {
        this.bufferPos += size;
      }
      else
      {
        int num = this.bufferSize - this.bufferPos;
        this.totalBytesRetired += this.bufferSize;
        this.bufferPos = 0;
        this.bufferSize = 0;
        if (num >= size)
          return;
        if (this.input == null)
          throw InvalidProtocolBufferException.TruncatedMessage();
        this.SkipImpl(size - num);
        this.totalBytesRetired += size - num;
      }
    }

    private void SkipImpl(int amountToSkip)
    {
      if (this.input.CanSeek)
      {
        long position = this.input.Position;
        this.input.Position += (long) amountToSkip;
        if (this.input.Position != position + (long) amountToSkip)
          throw InvalidProtocolBufferException.TruncatedMessage();
      }
      else
      {
        byte[] buffer = new byte[Math.Min(1024, amountToSkip)];
        int num;
        for (; amountToSkip > 0; amountToSkip -= num)
        {
          num = this.input.Read(buffer, 0, Math.Min(buffer.Length, amountToSkip));
          if (num <= 0)
            throw InvalidProtocolBufferException.TruncatedMessage();
        }
      }
    }
  }
}
