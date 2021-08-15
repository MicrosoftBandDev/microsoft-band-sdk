// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Collections.RepeatedField`1
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Compatibility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Google.Protobuf.Collections
{
  public sealed class RepeatedField<T> : 
    IList<T>,
    ICollection<T>,
    IEnumerable<T>,
    IList,
    ICollection,
    IEnumerable,
    IDeepCloneable<RepeatedField<T>>,
    IEquatable<RepeatedField<T>>
  {
    private const int MinArraySize = 8;
    private static readonly T[] EmptyArray = new T[0];
    private T[] array = RepeatedField<T>.EmptyArray;
    private int count;

    public RepeatedField<T> Clone()
    {
      RepeatedField<T> repeatedField = new RepeatedField<T>();
      if (this.array != RepeatedField<T>.EmptyArray)
      {
        repeatedField.array = (T[]) this.array.Clone();
        if (repeatedField.array is IDeepCloneable<T>[] array2)
        {
          for (int index = 0; index < this.count; ++index)
            repeatedField.array[index] = array2[index].Clone();
        }
      }
      repeatedField.count = this.count;
      return repeatedField;
    }

    public void AddEntriesFrom(CodedInputStream input, FieldCodec<T> codec)
    {
      uint lastTag = input.LastTag;
      Func<CodedInputStream, T> valueReader = codec.ValueReader;
      if (typeof (T).IsValueType() && WireFormat.GetTagWireType(lastTag) == WireFormat.WireType.LengthDelimited)
      {
        int byteLimit = input.ReadLength();
        if (byteLimit <= 0)
          return;
        int oldLimit = input.PushLimit(byteLimit);
        while (!input.ReachedLimit)
          this.Add(valueReader(input));
        input.PopLimit(oldLimit);
      }
      else
      {
        do
        {
          this.Add(valueReader(input));
        }
        while (input.MaybeConsumeTag(lastTag));
      }
    }

    public int CalculateSize(FieldCodec<T> codec)
    {
      if (this.count == 0)
        return 0;
      uint tag = codec.Tag;
      if (typeof (T).IsValueType() && WireFormat.GetTagWireType(tag) == WireFormat.WireType.LengthDelimited)
      {
        int packedDataSize = this.CalculatePackedDataSize(codec);
        return CodedOutputStream.ComputeRawVarint32Size(tag) + CodedOutputStream.ComputeLengthSize(packedDataSize) + packedDataSize;
      }
      Func<T, int> valueSizeCalculator = codec.ValueSizeCalculator;
      int num = this.count * CodedOutputStream.ComputeRawVarint32Size(tag);
      for (int index = 0; index < this.count; ++index)
        num += valueSizeCalculator(this.array[index]);
      return num;
    }

    private int CalculatePackedDataSize(FieldCodec<T> codec)
    {
      int fixedSize = codec.FixedSize;
      if (fixedSize != 0)
        return fixedSize * this.Count;
      Func<T, int> valueSizeCalculator = codec.ValueSizeCalculator;
      int num = 0;
      for (int index = 0; index < this.count; ++index)
        num += valueSizeCalculator(this.array[index]);
      return num;
    }

    public void WriteTo(CodedOutputStream output, FieldCodec<T> codec)
    {
      if (this.count == 0)
        return;
      Action<CodedOutputStream, T> valueWriter = codec.ValueWriter;
      uint tag = codec.Tag;
      if (typeof (T).IsValueType() && WireFormat.GetTagWireType(tag) == WireFormat.WireType.LengthDelimited)
      {
        uint packedDataSize = (uint) this.CalculatePackedDataSize(codec);
        output.WriteTag(tag);
        output.WriteRawVarint32(packedDataSize);
        for (int index = 0; index < this.count; ++index)
          valueWriter(output, this.array[index]);
      }
      else
      {
        for (int index = 0; index < this.count; ++index)
        {
          output.WriteTag(tag);
          valueWriter(output, this.array[index]);
        }
      }
    }

    private void EnsureSize(int size)
    {
      if (this.array.Length >= size)
        return;
      size = Math.Max(size, 8);
      T[] objArray = new T[Math.Max(this.array.Length * 2, size)];
      Array.Copy((Array) this.array, 0, (Array) objArray, 0, this.array.Length);
      this.array = objArray;
    }

    public void Add(T item)
    {
      if ((object) item == null)
        throw new ArgumentNullException(nameof (item));
      this.EnsureSize(this.count + 1);
      this.array[this.count++] = item;
    }

    public void Clear()
    {
      this.array = RepeatedField<T>.EmptyArray;
      this.count = 0;
    }

    public bool Contains(T item) => this.IndexOf(item) != -1;

    public void CopyTo(T[] array, int arrayIndex) => Array.Copy((Array) this.array, 0, (Array) array, arrayIndex, this.count);

    public bool Remove(T item)
    {
      int destinationIndex = this.IndexOf(item);
      if (destinationIndex == -1)
        return false;
      Array.Copy((Array) this.array, destinationIndex + 1, (Array) this.array, destinationIndex, this.count - destinationIndex - 1);
      --this.count;
      this.array[this.count] = default (T);
      return true;
    }

    public int Count => this.count;

    public bool IsReadOnly => false;

    public void Add(RepeatedField<T> values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      this.EnsureSize(this.count + values.count);
      Array.Copy((Array) values.array, 0, (Array) this.array, this.count, values.count);
      this.count += values.count;
    }

    public void Add(IEnumerable<T> values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      foreach (T obj in values)
        this.Add(obj);
    }

    public IEnumerator<T> GetEnumerator()
    {
      for (int i = 0; i < this.count; ++i)
        yield return this.array[i];
    }

    public override bool Equals(object obj) => this.Equals(obj as RepeatedField<T>);

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public override int GetHashCode()
    {
      int num = 0;
      for (int index = 0; index < this.count; ++index)
        num = num * 31 + this.array[index].GetHashCode();
      return num;
    }

    public bool Equals(RepeatedField<T> other)
    {
      if (object.ReferenceEquals((object) other, (object) null))
        return false;
      if (object.ReferenceEquals((object) other, (object) this))
        return true;
      if (other.Count != this.Count)
        return false;
      EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
      for (int index = 0; index < this.count; ++index)
      {
        if (!equalityComparer.Equals(this.array[index], other.array[index]))
          return false;
      }
      return true;
    }

    public int IndexOf(T item)
    {
      if ((object) item == null)
        throw new ArgumentNullException(nameof (item));
      EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
      for (int index = 0; index < this.count; ++index)
      {
        if (equalityComparer.Equals(this.array[index], item))
          return index;
      }
      return -1;
    }

    public void Insert(int index, T item)
    {
      if ((object) item == null)
        throw new ArgumentNullException(nameof (item));
      if (index < 0 || index > this.count)
        throw new ArgumentOutOfRangeException(nameof (index));
      this.EnsureSize(this.count + 1);
      Array.Copy((Array) this.array, index, (Array) this.array, index + 1, this.count - index);
      this.array[index] = item;
      ++this.count;
    }

    public void RemoveAt(int index)
    {
      if (index < 0 || index >= this.count)
        throw new ArgumentOutOfRangeException(nameof (index));
      Array.Copy((Array) this.array, index + 1, (Array) this.array, index, this.count - index - 1);
      --this.count;
      this.array[this.count] = default (T);
    }

    public T this[int index]
    {
      get => index >= 0 && index < this.count ? this.array[index] : throw new ArgumentOutOfRangeException(nameof (index));
      set
      {
        if (index < 0 || index >= this.count)
          throw new ArgumentOutOfRangeException(nameof (index));
        this.array[index] = (object) value != null ? value : throw new ArgumentNullException(nameof (value));
      }
    }

    bool IList.IsFixedSize => false;

    void ICollection.CopyTo(Array array, int index) => Array.Copy((Array) this.array, 0, array, index, this.count);

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => (object) this;

    object IList.this[int index]
    {
      get => (object) this[index];
      set => this[index] = (T) value;
    }

    int IList.Add(object value)
    {
      this.Add((T) value);
      return this.count - 1;
    }

    bool IList.Contains(object value) => value is T obj && this.Contains(obj);

    int IList.IndexOf(object value) => !(value is T obj) ? -1 : this.IndexOf(obj);

    void IList.Insert(int index, object value) => this.Insert(index, (T) value);

    void IList.Remove(object value)
    {
      if (!(value is T obj))
        return;
      this.Remove(obj);
    }
  }
}
