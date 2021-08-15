// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.FieldCodec`1
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections.Generic;

namespace Google.Protobuf
{
  public sealed class FieldCodec<T>
  {
    private static readonly T DefaultDefault;
    private readonly Func<CodedInputStream, T> reader;
    private readonly Action<CodedOutputStream, T> writer;
    private readonly Func<T, int> sizeCalculator;
    private readonly uint tag;
    private readonly int tagSize;
    private readonly int fixedSize;
    private readonly T defaultValue;

    static FieldCodec()
    {
      if (typeof (T) == typeof (string))
      {
        FieldCodec<T>.DefaultDefault = (T) "";
      }
      else
      {
        if (typeof (T) != typeof (ByteString))
          return;
        FieldCodec<T>.DefaultDefault = (T) ByteString.Empty;
      }
    }

    private static Func<T, bool> CreateDefaultValueCheck<TTmp>(Func<TTmp, bool> check) => (Func<T, bool>) check;

    internal FieldCodec(
      Func<CodedInputStream, T> reader,
      Action<CodedOutputStream, T> writer,
      Func<T, int> sizeCalculator,
      uint tag)
      : this(reader, writer, sizeCalculator, tag, FieldCodec<T>.DefaultDefault)
    {
    }

    internal FieldCodec(
      Func<CodedInputStream, T> reader,
      Action<CodedOutputStream, T> writer,
      Func<T, int> sizeCalculator,
      uint tag,
      T defaultValue)
    {
      this.reader = reader;
      this.writer = writer;
      this.sizeCalculator = sizeCalculator;
      this.fixedSize = 0;
      this.tag = tag;
      this.defaultValue = defaultValue;
      this.tagSize = CodedOutputStream.ComputeRawVarint32Size(tag);
    }

    internal FieldCodec(
      Func<CodedInputStream, T> reader,
      Action<CodedOutputStream, T> writer,
      int fixedSize,
      uint tag)
    {
      this.reader = reader;
      this.writer = writer;
      this.sizeCalculator = (Func<T, int>) (_ => fixedSize);
      this.fixedSize = fixedSize;
      this.tag = tag;
      this.tagSize = CodedOutputStream.ComputeRawVarint32Size(tag);
    }

    internal Func<T, int> ValueSizeCalculator => this.sizeCalculator;

    internal Action<CodedOutputStream, T> ValueWriter => this.writer;

    internal Func<CodedInputStream, T> ValueReader => this.reader;

    internal int FixedSize => this.fixedSize;

    public uint Tag => this.tag;

    public T DefaultValue => this.defaultValue;

    public void WriteTagAndValue(CodedOutputStream output, T value)
    {
      if (this.IsDefault(value))
        return;
      output.WriteTag(this.tag);
      this.writer(output, value);
    }

    public T Read(CodedInputStream input) => this.reader(input);

    public int CalculateSizeWithTag(T value) => !this.IsDefault(value) ? this.sizeCalculator(value) + this.tagSize : 0;

    private bool IsDefault(T value) => EqualityComparer<T>.Default.Equals(value, this.defaultValue);
  }
}
