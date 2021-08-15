// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Store.StreamSocketStream
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System;
using System.IO;
using System.Threading;
using Windows.Networking.Sockets;

namespace Microsoft.Band.Store
{
  internal sealed class StreamSocketStream : ICargoStream, IDisposable
  {
    private StreamSocket socket;
    private Stream inputStream;
    private Stream outputStream;
    private int readTimeout;
    private int writeTimeout;
    private bool isDisposed;

    public StreamSocketStream(StreamSocket socket)
    {
      this.socket = socket != null ? socket : throw new ArgumentNullException(nameof (socket));
      this.inputStream = WindowsRuntimeStreamExtensions.AsStreamForRead(socket.InputStream, 0);
      this.outputStream = WindowsRuntimeStreamExtensions.AsStreamForWrite(socket.OutputStream, 0);
    }

    public StreamSocket Socket
    {
      get
      {
        this.CheckIsDisposed();
        return this.socket;
      }
    }

    public int ReadTimeout
    {
      get => this.readTimeout;
      set => this.readTimeout = value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }

    public int WriteTimeout
    {
      get => this.writeTimeout;
      set => this.writeTimeout = value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }

    public CancellationToken Cancel { get; set; }

    public int Read(byte[] buffer, int offset, int count)
    {
      this.CheckIsDisposed();
      int num = 0;
      using (CancellationTokenSource cancellationTokenSource1 = new CancellationTokenSource(this.readTimeout))
      {
        CancellationTokenSource cancellationTokenSource2 = (CancellationTokenSource) null;
        if (this.Cancel != CancellationToken.None)
          cancellationTokenSource2 = CancellationTokenSource.CreateLinkedTokenSource(this.Cancel, cancellationTokenSource1.Token);
        try
        {
          num = this.inputStream.ReadAsync(buffer, offset, count, cancellationTokenSource2 != null ? cancellationTokenSource2.Token : cancellationTokenSource1.Token).Result;
        }
        catch (AggregateException ex)
        {
          this.HandleAggregateIOException(ex);
        }
        finally
        {
          cancellationTokenSource2?.Dispose();
        }
      }
      return num;
    }

    public void Write(byte[] buffer, int offset, int count)
    {
      this.CheckIsDisposed();
      using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(this.writeTimeout))
      {
        try
        {
          this.outputStream.WriteAsync(buffer, offset, count, cancellationTokenSource.Token).Wait();
        }
        catch (AggregateException ex)
        {
          this.HandleAggregateIOException(ex);
        }
      }
    }

    public void Flush()
    {
      this.CheckIsDisposed();
      using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(this.writeTimeout))
      {
        try
        {
          this.outputStream.FlushAsync(cancellationTokenSource.Token).Wait();
        }
        catch (AggregateException ex)
        {
          this.HandleAggregateIOException(ex);
        }
      }
    }

    private void HandleAggregateIOException(AggregateException e)
    {
      if (e.InnerExceptions.Count != 1)
        throw e;
      if (e.InnerException is OperationCanceledException)
      {
        if (this.Cancel != CancellationToken.None)
          this.Cancel.ThrowIfCancellationRequested();
        throw new TimeoutException();
      }
      throw e.InnerException;
    }

    public void CheckIsDisposed()
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (StreamSocketStream));
    }

    public void Dispose()
    {
      if (this.isDisposed)
        return;
      this.isDisposed = true;
      this.inputStream.Dispose();
      this.outputStream.Dispose();
      this.socket.Dispose();
    }
  }
}
