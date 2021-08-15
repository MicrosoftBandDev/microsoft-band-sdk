// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.CargoStreamBase
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.IO;

namespace Microsoft.Band
{
  internal abstract class CargoStreamBase : Stream
  {
    public override long Length => throw new InvalidOperationException();

    public override long Position
    {
      get => throw new InvalidOperationException();
      set => throw new InvalidOperationException();
    }

    public override bool CanRead => false;

    public override bool CanWrite => false;

    public override bool CanSeek => false;

    public override int Read(byte[] buffer, int offset, int count) => throw new InvalidOperationException();

    public override void Write(byte[] buffer, int offset, int count) => throw new InvalidOperationException();

    public override void SetLength(long value) => throw new InvalidOperationException();

    public override long Seek(long offset, SeekOrigin origin) => throw new InvalidOperationException();

    public override void Flush() => throw new InvalidOperationException();
  }
}
