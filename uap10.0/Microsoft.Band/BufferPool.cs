// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BufferPool
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Band
{
  internal sealed class BufferPool
  {
    private Queue<PooledBuffer> pool;

    internal BufferPool(int bufferSize, int maxBuffers)
    {
      if (bufferSize < 2)
        throw new ArgumentOutOfRangeException(nameof (bufferSize));
      if (maxBuffers < 1)
        throw new ArgumentOutOfRangeException(nameof (maxBuffers));
      this.BufferSize = bufferSize;
      this.MaxBuffers = maxBuffers;
      this.pool = new Queue<PooledBuffer>();
    }

    internal int BufferSize { get; private set; }

    internal int MaxBuffers { get; private set; }

    internal int Count => this.pool.Count;

    internal PooledBuffer GetBuffer(bool zero = false) => this.GetBuffer(this.BufferSize);

    internal PooledBuffer GetBuffer(int size, bool zero = false)
    {
      if (size < 0 || size > this.BufferSize)
        throw new ArgumentOutOfRangeException(nameof (size));
      PooledBuffer pooledBuffer = (PooledBuffer) null;
      if (this.pool.Count == 0)
      {
        pooledBuffer = new PooledBuffer(this, new byte[this.BufferSize], size);
      }
      else
      {
        Queue<PooledBuffer> pool = this.pool;
        bool lockTaken = false;
        try
        {
          Monitor.Enter((object) pool, ref lockTaken);
          if (this.pool.Count == 0)
          {
            pooledBuffer = new PooledBuffer(this, new byte[this.BufferSize], size);
          }
          else
          {
            pooledBuffer = this.pool.Dequeue();
            pooledBuffer.Undispose(size);
            if (zero)
              Array.Clear((Array) pooledBuffer.Buffer, 0, pooledBuffer.Buffer.Length);
          }
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit((object) pool);
        }
      }
      return pooledBuffer;
    }

    internal void ReleaseBuffer(PooledBuffer buffer)
    {
      if (buffer.Buffer.Length != this.BufferSize)
        throw new ArgumentException("The provided buffer does not belong to this pool", nameof (buffer));
      Queue<PooledBuffer> pool = this.pool;
      bool lockTaken = false;
      try
      {
        Monitor.Enter((object) pool, ref lockTaken);
        if (this.pool.Count >= this.MaxBuffers)
          return;
        this.pool.Enqueue(buffer);
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) pool);
      }
    }
  }
}
