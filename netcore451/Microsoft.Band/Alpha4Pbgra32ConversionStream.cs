// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Alpha4Pbgra32ConversionStream
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.IO;

namespace Microsoft.Band
{
  internal class Alpha4Pbgra32ConversionStream : ImageConversionStreamBase
  {
    private const int pixelWidthFactor = 4;
    private int argb32ByteCount;
    private int argb32Index;
    private ByteArrayProxyStream writeProxy;

    public Alpha4Pbgra32ConversionStream(int argb32ByteCount)
    {
      int num = argb32ByteCount / 4;
      this.argb32ByteCount = argb32ByteCount;
      this.Alpha4Array = new byte[num / 2 + num % 2];
    }

    public Alpha4Pbgra32ConversionStream(byte[] alpha4Array, int argb32ByteCount)
    {
      this.argb32ByteCount = argb32ByteCount;
      this.Alpha4Array = alpha4Array;
    }

    public override long Length => (long) this.argb32ByteCount;

    public override long Position
    {
      get => (long) this.argb32Index;
      set => throw new InvalidOperationException();
    }

    public byte[] Alpha4Array { get; private set; }

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
      int num = this.argb32Index / 4;
      for (; (long) this.argb32Index < this.Length && count > 0; --count)
      {
        if (this.argb32Index % 4 == 3)
        {
          switch (num % 2)
          {
            case 0:
              this.Alpha4Array[num++ / 2] |= (byte) ((uint) argb32Array[offset] & 240U);
              break;
            case 1:
              this.Alpha4Array[num++ / 2] |= (byte) (((int) argb32Array[offset] & 240) >> 4);
              break;
          }
        }
        ++offset;
        ++this.argb32Index;
      }
    }

    public new void CopyTo(Stream dest) => this.CopyToInternal(dest, (int) this.Length - this.argb32Index);

    private void CopyToInternal(Stream dest, int count)
    {
      int num = this.argb32Index / 4;
      for (; (long) this.argb32Index < this.Length && count > 0; --count)
      {
        switch (num % 2)
        {
          case 0:
            dest.WriteByte((byte) ((((int) this.Alpha4Array[num / 2] & 240) >> 4) * (int) byte.MaxValue / 15));
            break;
          case 1:
            dest.WriteByte((byte) (((int) this.Alpha4Array[num / 2] & 15) * (int) byte.MaxValue / 15));
            break;
        }
        if (this.argb32Index % 4 == 3)
          ++num;
        ++this.argb32Index;
      }
    }
  }
}
