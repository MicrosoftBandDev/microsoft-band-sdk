// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.LimitedInputStream
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.IO;

namespace Google.Protobuf
{
  internal sealed class LimitedInputStream : Stream
  {
    private readonly Stream proxied;
    private int bytesLeft;

    internal LimitedInputStream(Stream proxied, int size)
    {
      this.proxied = proxied;
      this.bytesLeft = size;
    }

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override void Flush()
    {
    }

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
      get => throw new NotSupportedException();
      set => throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this.bytesLeft <= 0)
        return 0;
      int num = this.proxied.Read(buffer, offset, Math.Min(this.bytesLeft, count));
      this.bytesLeft -= num;
      return num;
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
  }
}
