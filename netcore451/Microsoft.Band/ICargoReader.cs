// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.ICargoReader
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band
{
  internal interface ICargoReader : IDisposable
  {
    int Read(byte[] buffer, int offset, int count);

    int Read(byte[] buffer);

    void ReadExact(byte[] buffer, int offset, int count);

    byte[] ReadExact(int count);

    void ReadExactAndDiscard(int count);

    byte ReadByte();

    short ReadInt16();

    ushort ReadUInt16();

    int ReadInt32();

    uint ReadUInt32();

    long ReadInt64();

    ulong ReadUInt64();

    bool ReadBool32();

    Guid ReadGuid();

    string ReadString(int length);
  }
}
