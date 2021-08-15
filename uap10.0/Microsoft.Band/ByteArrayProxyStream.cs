// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.ByteArrayProxyStream
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Band
{
  [ExcludeFromTestCodeCoverage]
  internal class ByteArrayProxyStream : ImageConversionStreamBase
  {
    private byte[] buffer;
    private int offset;
    private int length;
    private int position;

    public void SetBuffer(byte[] buffer, int offset, int length)
    {
      this.buffer = buffer;
      this.offset = offset;
      this.length = length;
      this.position = 0;
    }

    public void ForgetBuffer() => this.buffer = (byte[]) null;

    public override bool CanRead => false;

    public override long Length => (long) this.length;

    public override long Position
    {
      get => (long) this.position;
      set => throw new InvalidOperationException();
    }

    public override int Read(byte[] argb32Array, int offset, int count) => throw new InvalidOperationException();

    public override void Write(byte[] argb32Array, int offset, int count) => throw new InvalidOperationException();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void WriteByte(byte value) => this.buffer[this.offset + this.position++] = value;
  }
}
