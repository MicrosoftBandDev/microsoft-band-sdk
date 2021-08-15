// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.CargoCommandReader
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.IO;
using System.Threading;

namespace Microsoft.Band
{
  internal sealed class CargoCommandReader : CargoStreamBase, ICargoReader, IDisposable
  {
    private IDeviceTransport transport;
    private int byteCount;
    private object protocolLock;
    private int bytesTransferred;
    private CargoStatus? status;
    private ILoggerProvider loggerProvider;
    private CommandStatusHandling statusHandling;

    public CargoCommandReader(
      IDeviceTransport transport,
      int byteCount,
      object protocolLock,
      ILoggerProvider loggerProvider,
      CommandStatusHandling statusHandling)
    {
      this.transport = transport;
      this.byteCount = byteCount;
      this.protocolLock = protocolLock;
      this.statusHandling = statusHandling;
      this.loggerProvider = loggerProvider;
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

    public override bool CanRead => true;

    public int BytesRemaining
    {
      get
      {
        this.CheckIfDisposed();
        return this.byteCount - this.bytesTransferred;
      }
    }

    public CargoStatus CommandStatus => this.status.HasValue ? this.status.Value : throw new InvalidOperationException(BandResources.CargoCommandStatusUnavailable);

    public override int Read(byte[] buffer, int offset, int count)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(count);
      int num = this.transport.CargoReader.Read(buffer, offset, count);
      this.bytesTransferred += num;
      if (this.BytesRemaining == 0)
        this.FinalizeCommand();
      return num;
    }

    public void ReadExact(byte[] buffer, int offset, int count)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(count);
      this.transport.CargoReader.ReadExact(buffer, offset, count);
      this.bytesTransferred += count;
      if (this.BytesRemaining != 0)
        return;
      this.FinalizeCommand();
    }

    public byte[] ReadExact(int count)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(count);
      byte[] numArray = this.transport.CargoReader.ReadExact(count);
      this.bytesTransferred += count;
      if (this.BytesRemaining != 0)
        return numArray;
      this.FinalizeCommand();
      return numArray;
    }

    public void ReadExactAndDiscard(int count)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(count);
      this.transport.CargoReader.ReadExactAndDiscard(count);
      this.bytesTransferred += count;
      if (this.BytesRemaining != 0)
        return;
      this.FinalizeCommand();
    }

    public int Read(byte[] buffer) => this.Read(buffer, 0, buffer.Length);

    public byte ReadByte()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(1);
      int num = (int) this.transport.CargoReader.ReadByte();
      ++this.bytesTransferred;
      if (this.BytesRemaining != 0)
        return (byte) num;
      this.FinalizeCommand();
      return (byte) num;
    }

    public int ReadToEnd(byte[] buffer, int offset)
    {
      this.CheckIfDisposed();
      if (this.BytesRemaining == 0)
        return 0;
      int num = 0;
      this.transport.CargoReader.ReadExact(buffer, offset, this.BytesRemaining);
      this.bytesTransferred += this.BytesRemaining;
      if (this.BytesRemaining != 0)
        return num;
      this.FinalizeCommand();
      return num;
    }

    public void ReadToEndAndDiscard()
    {
      this.CheckIfDisposed();
      if (this.BytesRemaining == 0)
        return;
      this.transport.CargoReader.ReadExactAndDiscard(this.BytesRemaining);
      this.bytesTransferred += this.BytesRemaining;
      this.FinalizeCommand();
    }

    public short ReadInt16()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(2);
      int num = (int) this.transport.CargoReader.ReadInt16();
      this.bytesTransferred += 2;
      if (this.BytesRemaining != 0)
        return (short) num;
      this.FinalizeCommand();
      return (short) num;
    }

    public ushort ReadUInt16()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(2);
      int num = (int) this.transport.CargoReader.ReadUInt16();
      this.bytesTransferred += 2;
      if (this.BytesRemaining != 0)
        return (ushort) num;
      this.FinalizeCommand();
      return (ushort) num;
    }

    public int ReadInt32()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(4);
      int num = this.transport.CargoReader.ReadInt32();
      this.bytesTransferred += 4;
      if (this.BytesRemaining != 0)
        return num;
      this.FinalizeCommand();
      return num;
    }

    public uint ReadUInt32()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(4);
      int num = (int) this.transport.CargoReader.ReadUInt32();
      this.bytesTransferred += 4;
      if (this.BytesRemaining != 0)
        return (uint) num;
      this.FinalizeCommand();
      return (uint) num;
    }

    public long ReadInt64()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(8);
      long num = this.transport.CargoReader.ReadInt64();
      this.bytesTransferred += 8;
      if (this.BytesRemaining != 0)
        return num;
      this.FinalizeCommand();
      return num;
    }

    public ulong ReadUInt64()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(8);
      long num = (long) this.transport.CargoReader.ReadUInt64();
      this.bytesTransferred += 8;
      if (this.BytesRemaining != 0)
        return (ulong) num;
      this.FinalizeCommand();
      return (ulong) num;
    }

    public bool ReadBool32()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(4);
      int num = this.transport.CargoReader.ReadBool32() ? 1 : 0;
      this.bytesTransferred += 4;
      if (this.BytesRemaining != 0)
        return num != 0;
      this.FinalizeCommand();
      return num != 0;
    }

    public Guid ReadGuid()
    {
      this.CheckIfDisposed();
      this.CheckIfEof(16);
      Guid guid = this.transport.CargoReader.ReadGuid();
      this.bytesTransferred += 16;
      if (this.BytesRemaining != 0)
        return guid;
      this.FinalizeCommand();
      return guid;
    }

    public string ReadString(int length)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(8);
      int num = length * 2;
      string str = this.transport.CargoReader.ReadString(length);
      this.bytesTransferred += num;
      if (this.BytesRemaining != 0)
        return str;
      this.FinalizeCommand();
      return str;
    }

    public new void CopyTo(Stream stream, int count)
    {
      this.CheckIfDisposed();
      this.CheckIfEof(count);
      if (count == 0)
        return;
      this.transport.CargoReader.CopyTo(stream, count);
      this.bytesTransferred += count;
      if (this.BytesRemaining != 0)
        return;
      this.FinalizeCommand();
    }

    public new void CopyTo(Stream stream) => this.CopyTo(stream, this.BytesRemaining);

    private void FinalizeCommand()
    {
      this.status = new CargoStatus?(this.transport.CargoReader.ReadStatusPacket());
      BandClient.CheckStatus(this.status.Value, this.statusHandling, this.loggerProvider);
      Monitor.Exit(this.protocolLock);
      this.protocolLock = (object) null;
    }

    private void CheckIfDisposed()
    {
      if (this.transport == null)
        throw new ObjectDisposedException(nameof (CargoCommandReader));
    }

    private void CheckIfEof(int count)
    {
      if (count > this.BytesRemaining)
        throw new EndOfStreamException();
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
