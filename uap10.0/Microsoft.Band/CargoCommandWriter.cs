// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.CargoCommandWriter
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.IO;
using System.Threading;

namespace Microsoft.Band
{
  internal sealed class CargoCommandWriter : CargoStreamBase, ICargoWriter, IDisposable
  {
    private IDeviceTransport transport;
    private int byteCount;
    private object protocolLock;
    private int bytesTransferred;
    private CargoStatus? status;
    private ILoggerProvider loggerProvider;
    private CommandStatusHandling statusHandling;

    public CargoCommandWriter(
      IDeviceTransport transport,
      int byteCount,
      object protocolLock,
      ILoggerProvider loggerProvider,
      CommandStatusHandling statusHandling)
    {
      this.transport = transport;
      this.byteCount = byteCount;
      this.protocolLock = protocolLock;
      this.loggerProvider = loggerProvider;
      this.statusHandling = statusHandling;
      Monitor.Enter(protocolLock);
    }

    public override long Length => (long) this.byteCount;

    public override long Position
    {
      get
      {
        this.CheckIfDisposed();
        return (long) this.bytesTransferred;
      }
      set => throw new InvalidOperationException();
    }

    public override bool CanWrite => true;

    public int BytesRemaining
    {
      get
      {
        this.CheckIfDisposed();
        return this.byteCount - this.bytesTransferred;
      }
    }

    public CargoStatus CommandStatus => this.status.HasValue ? this.status.Value : throw new InvalidOperationException(BandResources.CargoCommandStatusUnavailable);

    public override void Write(byte[] buffer, int offset, int count)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(count);
      this.transport.CargoWriter.Write(buffer, offset, count);
      this.IncrementBytesTransferred(count);
    }

    public void Write(byte[] buffer) => this.Write(buffer, 0, buffer.Length);

    public new void WriteByte(byte b)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(1);
      this.transport.CargoWriter.WriteByte(b);
      this.IncrementBytesTransferred(1);
    }

    public void WriteByte(byte b, int count)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(count);
      this.transport.CargoWriter.WriteByte(b, count);
      this.IncrementBytesTransferred(count);
    }

    public void WriteInt16(short i)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(2);
      this.transport.CargoWriter.WriteInt16(i);
      this.IncrementBytesTransferred(2);
    }

    public void WriteUInt16(ushort i)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(2);
      this.transport.CargoWriter.WriteUInt16(i);
      this.IncrementBytesTransferred(2);
    }

    public void WriteInt32(int i)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(4);
      this.transport.CargoWriter.WriteInt32(i);
      this.IncrementBytesTransferred(4);
    }

    public void WriteUInt32(uint i)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(4);
      this.transport.CargoWriter.WriteUInt32(i);
      this.IncrementBytesTransferred(4);
    }

    public void WriteInt64(long i)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(8);
      this.transport.CargoWriter.WriteInt64(i);
      this.IncrementBytesTransferred(8);
    }

    public void WriteUInt64(ulong i)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(8);
      this.transport.CargoWriter.WriteUInt64(i);
      this.IncrementBytesTransferred(8);
    }

    public void WriteBool32(bool b)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(4);
      this.transport.CargoWriter.WriteBool32(b);
      this.IncrementBytesTransferred(4);
    }

    public void WriteGuid(Guid guid)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(16);
      this.transport.CargoWriter.WriteGuid(guid);
      this.IncrementBytesTransferred(16);
    }

    public void WriteString(string s)
    {
      this.CheckIfDisposed();
      if (s.Length == 0)
        return;
      int count = s.Length * 2;
      this.CheckIfEof(count);
      this.transport.CargoWriter.WriteString(s);
      this.IncrementBytesTransferred(count);
    }

    public void WriteStringWithPadding(string s, int exactLength)
    {
      this.CheckIfDisposed();
      if (exactLength == 0)
        return;
      int count = exactLength * 2;
      this.CheckIfEof(count);
      this.transport.CargoWriter.WriteStringWithPadding(s, exactLength);
      this.IncrementBytesTransferred(count);
    }

    public void WriteStringWithTruncation(string s, int maxLength)
    {
      this.CheckIfDisposed();
      if (maxLength == 0)
        return;
      int count = Math.Min(s.Length, maxLength) * 2;
      this.CheckIfEof(count);
      this.transport.CargoWriter.WriteStringWithTruncation(s, maxLength);
      this.IncrementBytesTransferred(count);
    }

    public int CopyFromStream(Stream stream, int count)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(count);
      if (count == 0)
        return 0;
      int count1 = this.transport.CargoWriter.CopyFromStream(stream, count);
      this.IncrementBytesTransferred(count1);
      return count1;
    }

    public int CopyFromStream(Stream stream) => this.CopyFromStream(stream, this.BytesRemaining);

    public override void Flush()
    {
      this.CheckIfDisposed();
      this.transport.CargoWriter.Flush();
    }

    private void IncrementBytesTransferred(int count)
    {
      this.bytesTransferred += count;
      if (this.BytesRemaining != 0)
        return;
      this.transport.CargoWriter.Flush();
      this.status = new CargoStatus?(this.transport.CargoReader.ReadStatusPacket());
      BandClient.CheckStatus(this.status.Value, this.statusHandling, this.loggerProvider);
      Monitor.Exit(this.protocolLock);
      this.protocolLock = (object) null;
    }

    private void CheckIfDisposed()
    {
      if (this.transport == null)
        throw new ObjectDisposedException(nameof (CargoCommandWriter));
    }

    private void CheckIfEof(int count)
    {
      if (count > this.BytesRemaining)
        throw new EndOfStreamException(BandResources.EofExceptionOnWrite);
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing || this.transport == null)
        return;
      this.transport = (IDeviceTransport) null;
      if (this.protocolLock == null)
        return;
      Monitor.Exit(this.protocolLock);
      this.protocolLock = (object) null;
    }
  }
}
