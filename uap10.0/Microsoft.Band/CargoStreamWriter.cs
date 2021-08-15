// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.CargoStreamWriter
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.IO;
using System.Text;

namespace Microsoft.Band
{
  internal sealed class CargoStreamWriter : 
    CargoStreamBase,
    ICargoStreamWriter,
    ICargoWriter,
    IDisposable
  {
    private ICargoStream stream;
    private ILoggerProvider loggerProvider;
    private BufferPool bufferPool;
    private PooledBuffer buffer;
    private int bufferedBytes;

    public CargoStreamWriter(
      ICargoStream source,
      ILoggerProvider loggerProvider,
      BufferPool bufferPool)
    {
      this.stream = source;
      this.loggerProvider = loggerProvider;
      this.bufferPool = bufferPool;
    }

    public override bool CanWrite => true;

    private void WriteFromBuffer()
    {
      this.stream.Write(this.buffer.Buffer, 0, this.bufferedBytes);
      this.bufferedBytes = 0;
    }

    private void EnsureBufferSpace(int minCount)
    {
      if (this.buffer == null)
        this.buffer = this.bufferPool.GetBuffer();
      if (minCount <= this.bufferPool.BufferSize - this.bufferedBytes || this.bufferedBytes <= 0)
        return;
      this.WriteFromBuffer();
    }

    private void AdvanceBuffer(int count)
    {
      this.bufferedBytes += count;
      if (this.bufferedBytes != this.bufferPool.BufferSize)
        return;
      this.WriteFromBuffer();
      this.bufferedBytes = 0;
    }

    public void Write(byte[] source) => this.Write(source, 0, source.Length);

    public override void Write(byte[] source, int offset, int count)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(count);
      int num1 = 0;
      int num2;
      for (num2 = Math.Min(count - num1, this.bufferPool.BufferSize); num1 < count && num2 >= this.bufferPool.BufferSize; num2 = Math.Min(count - num1, this.bufferPool.BufferSize))
      {
        this.stream.Write(source, offset + num1, num2);
        num1 += num2;
      }
      if (num2 <= 0)
        return;
      Array.Copy((Array) source, offset + num1, (Array) this.buffer.Buffer, this.bufferedBytes, num2);
      this.AdvanceBuffer(num2);
    }

    public new void WriteByte(byte value)
    {
      this.CheckIfDisposed();
      int num = 1;
      this.EnsureBufferSpace(num);
      this.buffer.Buffer[this.bufferedBytes] = value;
      this.AdvanceBuffer(num);
    }

    public void WriteByte(byte value, int count)
    {
      this.CheckIfDisposed();
      int num = 1;
      while (count-- > 0)
      {
        this.EnsureBufferSpace(num);
        this.buffer.Buffer[this.bufferedBytes] = value;
        this.AdvanceBuffer(num);
      }
    }

    public void WriteInt16(short i)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(2);
      BandBitConverter.GetBytes(i, this.buffer.Buffer, this.bufferedBytes);
      this.AdvanceBuffer(2);
    }

    public void WriteUInt16(ushort i)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(2);
      BandBitConverter.GetBytes(i, this.buffer.Buffer, this.bufferedBytes);
      this.AdvanceBuffer(2);
    }

    public void WriteInt32(int i)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(4);
      BandBitConverter.GetBytes(i, this.buffer.Buffer, this.bufferedBytes);
      this.AdvanceBuffer(4);
    }

    public void WriteUInt32(uint i)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(4);
      BandBitConverter.GetBytes(i, this.buffer.Buffer, this.bufferedBytes);
      this.AdvanceBuffer(4);
    }

    public void WriteInt64(long i)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(8);
      BandBitConverter.GetBytes(i, this.buffer.Buffer, this.bufferedBytes);
      this.AdvanceBuffer(8);
    }

    public void WriteUInt64(ulong i)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(8);
      BandBitConverter.GetBytes(i, this.buffer.Buffer, this.bufferedBytes);
      this.AdvanceBuffer(8);
    }

    public void WriteBool32(bool b)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(4);
      BandBitConverter.GetBytes(b ? 1 : 0, this.buffer.Buffer, this.bufferedBytes);
      this.AdvanceBuffer(4);
    }

    public void WriteGuid(Guid guid)
    {
      this.CheckIfDisposed();
      this.EnsureBufferSpace(16);
      BandBitConverter.GetBytes(guid, this.buffer.Buffer, this.bufferedBytes);
      this.AdvanceBuffer(16);
    }

    public void WriteString(string s)
    {
      if (s.Length == 0)
        return;
      int minCount = s.Length * 2;
      if (minCount > this.buffer.Length)
        throw new BandIOException("Unsupported string length");
      this.CheckIfDisposed();
      this.EnsureBufferSpace(minCount);
      this.AdvanceBuffer(Encoding.Unicode.GetBytes(s, 0, s.Length, this.buffer.Buffer, this.bufferedBytes));
    }

    public void WriteStringWithPadding(string s, int exactLength)
    {
      int charCount = Math.Min(s.LengthTruncatedTrimDanglingHighSurrogate(exactLength - 1), exactLength - 1);
      int num = exactLength * 2;
      if (num > this.buffer.Length)
        throw new BandIOException("Unsupported string length");
      if (num == 0)
        return;
      this.CheckIfDisposed();
      this.EnsureBufferSpace(num);
      int danglingHighSurrogate = Encoding.Unicode.GetBytesTrimDanglingHighSurrogate(s, charCount, this.buffer.Buffer, this.bufferedBytes);
      Array.Clear((Array) this.buffer.Buffer, this.bufferedBytes + danglingHighSurrogate, num - danglingHighSurrogate);
      this.AdvanceBuffer(num);
    }

    public void WriteStringWithTruncation(string s, int maxLength)
    {
      int charCount = Math.Min(s.LengthTruncatedTrimDanglingHighSurrogate(maxLength), maxLength);
      if (charCount == 0)
        return;
      int minCount = charCount * 2;
      if (minCount > this.buffer.Length)
        throw new BandIOException("Unsupported string length");
      this.CheckIfDisposed();
      this.EnsureBufferSpace(minCount);
      this.AdvanceBuffer(Encoding.Unicode.GetBytesTrimDanglingHighSurrogate(s, charCount, this.buffer.Buffer, this.bufferedBytes));
    }

    public int CopyFromStream(Stream stream, int count)
    {
      this.CheckIfDisposed();
      int num1 = 0;
      while (num1 < count)
      {
        int num2 = Math.Min(count - num1, this.bufferPool.BufferSize);
        this.EnsureBufferSpace(num2);
        int count1 = stream.Read(this.buffer.Buffer, this.bufferedBytes, num2);
        if (count1 != 0)
        {
          num1 += count1;
          this.AdvanceBuffer(count1);
        }
        else
          break;
      }
      return num1;
    }

    public void WriteCommandPacket(
      ushort commandId,
      uint argBufSize,
      uint dataStageSize,
      Action<ICargoWriter> writeArgBuf,
      bool prependPacketSize,
      bool flush)
    {
      this.CheckIfDisposed();
      if (argBufSize > 0U)
      {
        if (writeArgBuf == null)
          throw new ArgumentException("writeArgBuf must not be null", nameof (writeArgBuf));
        if (argBufSize > 55U)
          throw new ArgumentOutOfRangeException(nameof (argBufSize));
      }
      Facility category;
      TX isTXCommand;
      byte index;
      DeviceCommands.LookupCommand(commandId, out category, out isTXCommand, out index);
      this.loggerProvider.Log(ProviderLogLevel.Info, "Device Command: Facility: {0}, TX: {1}, Index: 0x{2:X2}, Arg Buf Size: {3}, Data Size: {4}", (object) category, (object) isTXCommand, (object) index, (object) argBufSize, (object) dataStageSize);
      try
      {
        if (prependPacketSize)
          this.WriteByte((byte) (8U + argBufSize));
        this.WriteUInt16((ushort) 12025);
        this.WriteUInt16(commandId);
        this.WriteUInt32(dataStageSize);
        if (argBufSize > 0U)
          writeArgBuf((ICargoWriter) this);
        if (!flush)
          return;
        this.Flush();
      }
      catch (Exception ex)
      {
        throw new BandIOException(BandResources.StreamWriteFailure, ex);
      }
    }

    public override void Flush()
    {
      this.CheckIfDisposed();
      if (this.bufferedBytes > 0)
        this.WriteFromBuffer();
      this.stream.Flush();
    }

    private void CheckIfDisposed()
    {
      if (this.stream == null)
        throw new ObjectDisposedException(nameof (CargoStreamWriter));
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
