// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Bgr565Pbgra32ConversionStream
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.IO;

namespace Microsoft.Band
{
  internal class Bgr565Pbgra32ConversionStream : ImageConversionStreamBase
  {
    private const int pixelWidthFactor = 2;
    private int argb32Index;
    private ByteArrayProxyStream writeProxy;

    public Bgr565Pbgra32ConversionStream(int argb32ByteCount) => this.Bgr565Array = new byte[argb32ByteCount / 2];

    public Bgr565Pbgra32ConversionStream(byte[] bgr565Array) => this.Bgr565Array = bgr565Array;

    public override long Length => (long) (this.Bgr565Array.Length * 2);

    public override long Position
    {
      get => (long) this.argb32Index;
      set => throw new InvalidOperationException();
    }

    public byte[] Bgr565Array { get; private set; }

    public override int Read(byte[] argb32Array, int offset, int count)
    {
      if (this.writeProxy == null)
        this.writeProxy = new ByteArrayProxyStream();
      this.writeProxy.SetBuffer(argb32Array, offset, count);
      try
      {
        this.CopyToInternal((Stream) this.writeProxy, count);
      }
      finally
      {
        this.writeProxy.ForgetBuffer();
      }
      return count;
    }

    public override void Write(byte[] argb32Array, int offset, int count)
    {
      int index = this.argb32Index / 2;
      for (; (long) this.argb32Index < this.Length && count > 0; --count)
      {
        switch (this.argb32Index % 4)
        {
          case 0:
            this.Bgr565Array[index] |= (byte) (((uint) argb32Array[offset] & 248U) >> 3);
            break;
          case 1:
            this.Bgr565Array[index++] |= (byte) (((int) argb32Array[offset] & 28) << 3);
            this.Bgr565Array[index] |= (byte) (((uint) argb32Array[offset] & 224U) >> 5);
            break;
          case 2:
            this.Bgr565Array[index++] |= (byte) ((uint) argb32Array[offset] & 248U);
            break;
        }
        ++offset;
        ++this.argb32Index;
      }
    }

    public new void CopyTo(Stream dest) => this.CopyToInternal(dest, (int) this.Length - this.argb32Index);

    public new void CopyTo(Stream dest, int bufferSize) => this.CopyToInternal(dest, (int) this.Length - this.argb32Index);

    private void CopyToInternal(Stream dest, int count)
    {
      int index = this.argb32Index / 2;
      for (; (long) this.argb32Index < this.Length && count > 0; --count)
      {
        switch (this.argb32Index % 4)
        {
          case 0:
            dest.WriteByte((byte) (((int) this.Bgr565Array[index] & 31) * (int) byte.MaxValue / 31));
            break;
          case 1:
            dest.WriteByte((byte) ((((int) this.Bgr565Array[index++] & 224) >> 5 | ((int) this.Bgr565Array[index] & 7) << 3) * (int) byte.MaxValue / 63));
            break;
          case 2:
            dest.WriteByte((byte) ((((int) this.Bgr565Array[index] & 248) >> 3) * (int) byte.MaxValue / 31));
            break;
          case 3:
            dest.WriteByte(byte.MaxValue);
            ++index;
            break;
        }
        ++this.argb32Index;
      }
    }
  }
}
