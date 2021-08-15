// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.PooledBuffer
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band
{
  internal sealed class PooledBuffer : IDisposable
  {
    private BufferPool pool;
    private byte[] buffer;
    private int length;
    private bool disposed;

    internal PooledBuffer(byte[] buffer, int length)
      : this((BufferPool) null, buffer, length)
    {
    }

    internal PooledBuffer(BufferPool pool, byte[] buffer, int length)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (length < 0 || length > buffer.Length)
        throw new ArgumentOutOfRangeException("size");
      if (pool != null && buffer.Length != pool.BufferSize)
        throw new ArgumentException("The provided buffer does not belong to the provided pool");
      this.pool = pool;
      this.buffer = buffer;
      this.length = length;
    }

    internal byte[] Buffer
    {
      get
      {
        if (this.disposed)
          throw new ObjectDisposedException(nameof (PooledBuffer));
        return this.buffer;
      }
    }

    internal int Length
    {
      get
      {
        if (this.disposed)
          throw new ObjectDisposedException(nameof (PooledBuffer));
        return this.length;
      }
      set
      {
        if (this.disposed)
          throw new ObjectDisposedException(nameof (PooledBuffer));
        if (value < 0 || value > this.buffer.Length)
          throw new ArgumentOutOfRangeException();
        this.length = value;
      }
    }

    public void Dispose()
    {
      if (this.pool == null || this.disposed)
        return;
      this.pool.ReleaseBuffer(this);
      this.disposed = true;
    }

    internal void Undispose(int length)
    {
      if (length < 0 || length > this.buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (length));
      this.disposed = false;
      this.length = length;
    }
  }
}
