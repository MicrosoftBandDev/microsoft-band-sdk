// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.ICargoWriter
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band
{
  internal interface ICargoWriter : IDisposable
  {
    void Write(byte[] buffer, int offset, int count);

    void Write(byte[] buffer);

    void WriteByte(byte b);

    void WriteByte(byte b, int count);

    void WriteInt16(short i);

    void WriteUInt16(ushort i);

    void WriteInt32(int i);

    void WriteUInt32(uint i);

    void WriteInt64(long i);

    void WriteUInt64(ulong i);

    void WriteBool32(bool b);

    void WriteGuid(Guid guid);

    void WriteString(string s);

    void WriteStringWithPadding(string s, int exactLength);

    void WriteStringWithTruncation(string s, int maxLength);

    void Flush();
  }
}
