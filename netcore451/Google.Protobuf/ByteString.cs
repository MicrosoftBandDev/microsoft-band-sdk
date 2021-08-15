// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.ByteString
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Google.Protobuf
{
  public sealed class ByteString : IEnumerable<byte>, IEnumerable, IEquatable<ByteString>
  {
    private static readonly ByteString empty = new ByteString(new byte[0]);
    private readonly byte[] bytes;

    internal static ByteString AttachBytes(byte[] bytes) => new ByteString(bytes);

    private ByteString(byte[] bytes) => this.bytes = bytes;

    public static ByteString Empty => ByteString.empty;

    public int Length => this.bytes.Length;

    public bool IsEmpty => this.Length == 0;

    public byte[] ToByteArray() => (byte[]) this.bytes.Clone();

    public string ToBase64() => Convert.ToBase64String(this.bytes);

    public static ByteString FromBase64(string bytes) => !(bytes == "") ? new ByteString(Convert.FromBase64String(bytes)) : ByteString.Empty;

    public static ByteString CopyFrom(params byte[] bytes) => new ByteString((byte[]) bytes.Clone());

    public static ByteString CopyFrom(byte[] bytes, int offset, int count)
    {
      byte[] numArray = new byte[count];
      ByteArray.Copy(bytes, offset, numArray, 0, count);
      return new ByteString(numArray);
    }

    public static ByteString CopyFrom(string text, Encoding encoding) => new ByteString(encoding.GetBytes(text));

    public static ByteString CopyFromUtf8(string text) => ByteString.CopyFrom(text, Encoding.UTF8);

    public byte this[int index] => this.bytes[index];

    public string ToString(Encoding encoding) => encoding.GetString(this.bytes, 0, this.bytes.Length);

    public string ToStringUtf8() => this.ToString(Encoding.UTF8);

    public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>) this.bytes).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public CodedInputStream CreateCodedInput() => new CodedInputStream(this.bytes);

    public static bool operator ==(ByteString lhs, ByteString rhs)
    {
      if (object.ReferenceEquals((object) lhs, (object) rhs))
        return true;
      if (object.ReferenceEquals((object) lhs, (object) null) || object.ReferenceEquals((object) rhs, (object) null) || lhs.bytes.Length != rhs.bytes.Length)
        return false;
      for (int index = 0; index < lhs.Length; ++index)
      {
        if ((int) rhs.bytes[index] != (int) lhs.bytes[index])
          return false;
      }
      return true;
    }

    public static bool operator !=(ByteString lhs, ByteString rhs) => !(lhs == rhs);

    public override bool Equals(object obj) => this == obj as ByteString;

    public override int GetHashCode()
    {
      int num1 = 23;
      foreach (byte num2 in this.bytes)
        num1 = num1 << 8 | (int) num2;
      return num1;
    }

    public bool Equals(ByteString other) => this == other;

    internal void WriteRawBytesTo(CodedOutputStream outputStream) => outputStream.WriteRawBytes(this.bytes, 0, this.bytes.Length);

    public void CopyTo(byte[] array, int position) => ByteArray.Copy(this.bytes, 0, array, position, this.bytes.Length);

    public void WriteTo(Stream outputStream) => outputStream.Write(this.bytes, 0, this.bytes.Length);

    public static class Unsafe
    {
      public static ByteString FromBytes(byte[] bytes) => new ByteString(bytes);

      public static byte[] GetBuffer(ByteString bytes) => bytes.bytes;
    }
  }
}
