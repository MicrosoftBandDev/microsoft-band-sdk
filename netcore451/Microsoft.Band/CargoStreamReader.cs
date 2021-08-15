// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.CargoStreamReader
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.IO;
using System.Text;

namespace Microsoft.Band
{
  internal sealed class CargoStreamReader : CargoStreamBase, ICargoReader, IDisposable
  {
    private ICargoStream stream;
    private BufferPool bufferPool;
    private PooledBuffer buffer;
    private int bufferedBytes;
    private int bufferOffset;

    public CargoStreamReader(ICargoStream source, BufferPool bufferPool)
    {
      this.stream = source;
      this.bufferPool = bufferPool;
    }

    public override bool CanRead => true;

    private int ReadToBuffer()
    {
      if (this.buffer == null)
        this.buffer = this.bufferPool.GetBuffer();
      int num = this.stream.Read(this.buffer.Buffer, this.bufferedBytes, this.bufferPool.BufferSize - this.bufferedBytes);
      this.bufferedBytes += num;
      return num;
    }

    private void ReadToBufferMinimum(int minCount)
    {
      int length = this.bufferedBytes - this.bufferOffset;
      if (length > 0 && length < minCount)
      {
        Array.Copy((Array) this.buffer.Buffer, this.bufferOffset, (Array) this.buffer.Buffer, 0, length);
        this.bufferedBytes = length;
        this.bufferOffset = 0;
      }
      int buffer;
      for (; length < minCount; length += buffer)
      {
        buffer = this.ReadToBuffer();
        if (buffer == 0)
          throw new EndOfStreamException();
      }
    }

    private void AdvanceBuffer(int count)
    {
      if (count == this.bufferedBytes - this.bufferOffset)
      {
        this.bufferedBytes = 0;
        this.bufferOffset = 0;
      }
      else
        this.bufferOffset += count;
    }

    public int Read(byte[] destination) => this.Read(destination, 0, destination.Length);

    public override int Read(byte[] destination, int offset, int count)
    {
      this.CheckIfDisposed();
      if (this.bufferedBytes == 0)
      {
        if (count >= this.bufferPool.BufferSize)
          return this.stream.Read(destination, offset, this.bufferPool.BufferSize);
        this.ReadToBuffer();
      }
      int num = Math.Min(this.bufferedBytes - this.bufferOffset, count);
      Array.Copy((Array) this.buffer.Buffer, this.bufferOffset, (Array) destination, offset, num);
      this.AdvanceBuffer(num);
      return num;
    }

    public void ReadExact(byte[] destination, int offset, int count)
    {
      this.CheckIfDisposed();
      int num;
      for (int index = 0; index < count; index += num)
      {
        num = this.Read(destination, offset + index, count - index);
        if (num == 0)
          throw new EndOfStreamException();
      }
    }

    public byte[] ReadExact(int count)
    {
      this.CheckIfDisposed();
      byte[] destination = new byte[count];
      this.ReadExact(destination, 0, count);
      return destination;
    }

    public void ReadExactAndDiscard(int count)
    {
      this.CheckIfDisposed();
      int count1;
      for (int index = 0; index < count; index += count1)
      {
        if (this.bufferedBytes == 0)
          this.ReadToBuffer();
        count1 = Math.Min(count - index, this.bufferedBytes - this.bufferOffset);
        if (count1 == 0)
          throw new EndOfStreamException();
        this.AdvanceBuffer(count1);
      }
    }

    public byte ReadByte()
    {
      this.CheckIfDisposed();
      int num1 = 1;
      this.ReadToBufferMinimum(num1);
      int num2 = (int) this.buffer.Buffer[this.bufferOffset];
      this.AdvanceBuffer(num1);
      return (byte) num2;
    }

    public short ReadInt16()
    {
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(2);
      int int16 = (int) BitConverter.ToInt16(this.buffer.Buffer, this.bufferOffset);
      this.AdvanceBuffer(2);
      return (short) int16;
    }

    public ushort ReadUInt16()
    {
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(2);
      int uint16 = (int) BitConverter.ToUInt16(this.buffer.Buffer, this.bufferOffset);
      this.AdvanceBuffer(2);
      return (ushort) uint16;
    }

    public int ReadInt32()
    {
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(4);
      int int32 = BitConverter.ToInt32(this.buffer.Buffer, this.bufferOffset);
      this.AdvanceBuffer(4);
      return int32;
    }

    public uint ReadUInt32()
    {
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(4);
      int uint32 = (int) BitConverter.ToUInt32(this.buffer.Buffer, this.bufferOffset);
      this.AdvanceBuffer(4);
      return (uint) uint32;
    }

    public long ReadInt64()
    {
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(8);
      long int64 = BitConverter.ToInt64(this.buffer.Buffer, this.bufferOffset);
      this.AdvanceBuffer(8);
      return int64;
    }

    public ulong ReadUInt64()
    {
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(8);
      long uint64 = (long) BitConverter.ToUInt64(this.buffer.Buffer, this.bufferOffset);
      this.AdvanceBuffer(8);
      return (ulong) uint64;
    }

    public bool ReadBool32()
    {
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(4);
      int num = (uint) BitConverter.ToInt32(this.buffer.Buffer, this.bufferOffset) > 0U ? 1 : 0;
      this.AdvanceBuffer(4);
      return num != 0;
    }

    public Guid ReadGuid()
    {
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(16);
      Guid guid = BandBitConverter.ToGuid(this.buffer.Buffer, this.bufferOffset);
      this.AdvanceBuffer(16);
      return guid;
    }

    public string ReadString(int length)
    {
      int num = length * 2;
      if (num > this.buffer.Length)
        throw new BandIOException("Unsupported string length");
      this.CheckIfDisposed();
      this.ReadToBufferMinimum(num);
      int count = 0;
      while (count < num && this.buffer.Buffer[this.bufferOffset + count] != (byte) 0)
        count += 2;
      string str = Encoding.Unicode.GetString(this.buffer.Buffer, this.bufferOffset, count);
      this.AdvanceBuffer(num);
      return str;
    }

    public new void CopyTo(Stream stream, int count)
    {
      this.CheckIfDisposed();
      int num = 0;
      while (num < count)
      {
        if (this.bufferedBytes == 0)
          this.ReadToBuffer();
        int count1 = Math.Min(count - num, this.bufferedBytes - this.bufferOffset);
        stream.Write(this.buffer.Buffer, this.bufferOffset, count1);
        num += count1;
        this.AdvanceBuffer(count1);
      }
    }

    public CargoStatus ReadStatusPacket()
    {
      this.CheckIfDisposed();
      for (int index = 0; index < 2; ++index)
      {
        if ((int) this.ReadByte() != (42750 >> index * 8 & (int) byte.MaxValue))
          throw new IOException(BandResources.BadDeviceCommandStatusPacket);
      }
      return new CargoStatus()
      {
        PacketType = 42750,
        Status = this.ReadUInt32()
      };
    }

    private void CheckIfDisposed()
    {
      if (this.stream == null)
        throw new ObjectDisposedException(nameof (CargoStreamReader));
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      ICargoStream stream = this.stream;
      if (stream != null)
      {
        stream.Dispose();
        this.stream = (ICargoStream) null;
      }
      PooledBuffer buffer = this.buffer;
      if (buffer == null)
        return;
      buffer.Dispose();
      this.buffer = (PooledBuffer) null;
    }
  }
}
