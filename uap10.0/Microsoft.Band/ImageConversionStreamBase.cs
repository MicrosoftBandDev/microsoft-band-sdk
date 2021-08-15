// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.ImageConversionStreamBase
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.IO;

namespace Microsoft.Band
{
  [ExcludeFromTestCodeCoverage]
  internal abstract class ImageConversionStreamBase : Stream
  {
    public override bool CanRead => true;

    public override bool CanWrite => true;

    public override bool CanSeek => false;

    public override bool CanTimeout => false;

    public override void SetLength(long value) => throw new InvalidOperationException();

    public override long Seek(long offset, SeekOrigin origin) => throw new InvalidOperationException();

    public override void Flush()
    {
    }
  }
}
