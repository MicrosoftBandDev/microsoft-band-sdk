// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandBitConverter
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Text;

namespace Microsoft.Band
{
  internal static class BandBitConverter
  {
    private static readonly char[] HexCharTable = "0123456789ABCDEF".ToCharArray();

    public static void GetBytes(short i, byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length < offset + 2)
        throw new ArgumentOutOfRangeException(nameof (offset));
      for (int index = 0; index < 16; index += 8)
        buffer[offset++] = (byte) ((int) i >> index & (int) byte.MaxValue);
    }

    public static void GetBytes(ushort i, byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length < offset + 2)
        throw new ArgumentOutOfRangeException(nameof (offset));
      for (int index = 0; index < 16; index += 8)
        buffer[offset++] = (byte) ((int) i >> index & (int) byte.MaxValue);
    }

    public static void GetBytes(int i, byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length < offset + 4)
        throw new ArgumentOutOfRangeException(nameof (offset));
      for (int index = 0; index < 32; index += 8)
        buffer[offset++] = (byte) (i >> index & (int) byte.MaxValue);
    }

    public static void GetBytes(uint i, byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length < offset + 4)
        throw new ArgumentOutOfRangeException(nameof (offset));
      for (int index = 0; index < 32; index += 8)
        buffer[offset++] = (byte) (i >> index & (uint) byte.MaxValue);
    }

    public static void GetBytes(long i, byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length < offset + 8)
        throw new ArgumentOutOfRangeException(nameof (offset));
      for (int index = 0; index < 64; index += 8)
        buffer[offset++] = (byte) ((ulong) (i >> index) & (ulong) byte.MaxValue);
    }

    public static void GetBytes(ulong i, byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length < offset + 8)
        throw new ArgumentOutOfRangeException(nameof (offset));
      for (int index = 0; index < 64; index += 8)
        buffer[offset++] = (byte) (i >> index & (ulong) byte.MaxValue);
    }

    public static Guid ToGuid(byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length < offset + 16)
        throw new ArgumentOutOfRangeException(nameof (offset));
      return buffer.Length == 16 ? new Guid(buffer) : new Guid(BitConverter.ToInt32(buffer, offset), BitConverter.ToInt16(buffer, offset + 4), BitConverter.ToInt16(buffer, offset + 6), buffer[offset + 8], buffer[offset + 9], buffer[offset + 10], buffer[offset + 11], buffer[offset + 12], buffer[offset + 13], buffer[offset + 14], buffer[offset + 15]);
    }

    public static void GetBytes(Guid guid, byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length < offset + 16)
        throw new ArgumentOutOfRangeException(nameof (offset));
      guid.ToByteArray().CopyTo((Array) buffer, offset);
    }

    public static string ToString(byte[] buffer) => buffer != null ? BandBitConverter.ToStringInternal(buffer, 0, buffer.Length) : throw new ArgumentNullException(nameof (buffer));

    public static string ToString(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || buffer.Length != 0 && offset >= buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (count < 0 || offset + count > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      return BandBitConverter.ToStringInternal(buffer, offset, count);
    }

    private static string ToStringInternal(byte[] buffer, int offset, int count)
    {
      StringBuilder stringBuilder = new StringBuilder(count * 2);
      while (count > 0)
      {
        byte num = buffer[offset];
        stringBuilder.Append(BandBitConverter.HexCharTable[(int) num >> 4 & 15]);
        stringBuilder.Append(BandBitConverter.HexCharTable[(int) num & 15]);
        --count;
        ++offset;
      }
      return stringBuilder.ToString();
    }
  }
}
