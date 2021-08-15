// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Collections.ReadOnlyDictionary`2
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Google.Protobuf.Collections
{
  internal sealed class ReadOnlyDictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
  {
    private readonly IDictionary<TKey, TValue> wrapped;

    public ReadOnlyDictionary(IDictionary<TKey, TValue> wrapped) => this.wrapped = wrapped;

    public void Add(TKey key, TValue value) => throw new InvalidOperationException();

    public bool ContainsKey(TKey key) => this.wrapped.ContainsKey(key);

    public ICollection<TKey> Keys => this.wrapped.Keys;

    public bool Remove(TKey key) => throw new InvalidOperationException();

    public bool TryGetValue(TKey key, out TValue value) => this.wrapped.TryGetValue(key, out value);

    public ICollection<TValue> Values => this.wrapped.Values;

    public TValue this[TKey key]
    {
      get => this.wrapped[key];
      set => throw new InvalidOperationException();
    }

    public void Add(KeyValuePair<TKey, TValue> item) => throw new InvalidOperationException();

    public void Clear() => throw new InvalidOperationException();

    public bool Contains(KeyValuePair<TKey, TValue> item) => this.wrapped.Contains(item);

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => this.wrapped.CopyTo(array, arrayIndex);

    public int Count => this.wrapped.Count;

    public bool IsReadOnly => true;

    public bool Remove(KeyValuePair<TKey, TValue> item) => throw new InvalidOperationException();

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.wrapped.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.wrapped.GetEnumerator();

    public override bool Equals(object obj) => this.wrapped.Equals(obj);

    public override int GetHashCode() => this.wrapped.GetHashCode();

    public override string ToString() => this.wrapped.ToString();
  }
}
