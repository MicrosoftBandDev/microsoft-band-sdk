// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Collections.MapField`2
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Compatibility;
using Google.Protobuf.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Google.Protobuf.Collections
{
  public sealed class MapField<TKey, TValue> : 
    IDeepCloneable<MapField<TKey, TValue>>,
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEquatable<MapField<TKey, TValue>>,
    IDictionary,
    ICollection,
    IEnumerable
  {
    private readonly bool allowNullValues;
    private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> map = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
    private readonly LinkedList<KeyValuePair<TKey, TValue>> list = new LinkedList<KeyValuePair<TKey, TValue>>();

    public MapField()
      : this(typeof (IMessage).IsAssignableFrom(typeof (TValue)) || Nullable.GetUnderlyingType(typeof (TValue)) != null)
    {
    }

    public MapField(bool allowNullValues) => this.allowNullValues = !allowNullValues || !typeof (TValue).IsValueType() || Nullable.GetUnderlyingType(typeof (TValue)) != null ? allowNullValues : throw new ArgumentException(nameof (allowNullValues), "Non-nullable value types do not support null values");

    public MapField<TKey, TValue> Clone()
    {
      MapField<TKey, TValue> mapField = new MapField<TKey, TValue>(this.allowNullValues);
      if (typeof (IDeepCloneable<TValue>).IsAssignableFrom(typeof (TValue)))
      {
        foreach (KeyValuePair<TKey, TValue> keyValuePair in this.list)
          mapField.Add(keyValuePair.Key, (object) keyValuePair.Value == null ? keyValuePair.Value : ((IDeepCloneable<TValue>) (object) keyValuePair.Value).Clone());
      }
      else
        mapField.Add((IDictionary<TKey, TValue>) this);
      return mapField;
    }

    public void Add(TKey key, TValue value)
    {
      if (this.ContainsKey(key))
        throw new ArgumentException("Key already exists in map", nameof (key));
      this[key] = value;
    }

    public bool ContainsKey(TKey key)
    {
      Preconditions.CheckNotNullUnconstrained<TKey>(key, nameof (key));
      return this.map.ContainsKey(key);
    }

    private bool ContainsValue(TValue value)
    {
      EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;
      return this.list.Any<KeyValuePair<TKey, TValue>>((Func<KeyValuePair<TKey, TValue>, bool>) (pair => comparer.Equals(pair.Value, value)));
    }

    public bool Remove(TKey key)
    {
      Preconditions.CheckNotNullUnconstrained<TKey>(key, nameof (key));
      LinkedListNode<KeyValuePair<TKey, TValue>> node;
      if (!this.map.TryGetValue(key, out node))
        return false;
      this.map.Remove(key);
      node.List.Remove(node);
      return true;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      LinkedListNode<KeyValuePair<TKey, TValue>> linkedListNode;
      if (this.map.TryGetValue(key, out linkedListNode))
      {
        value = linkedListNode.Value.Value;
        return true;
      }
      value = default (TValue);
      return false;
    }

    public TValue this[TKey key]
    {
      get
      {
        Preconditions.CheckNotNullUnconstrained<TKey>(key, nameof (key));
        TValue obj;
        if (this.TryGetValue(key, out obj))
          return obj;
        throw new KeyNotFoundException();
      }
      set
      {
        Preconditions.CheckNotNullUnconstrained<TKey>(key, nameof (key));
        if ((object) value == null && !this.allowNullValues)
          Preconditions.CheckNotNullUnconstrained<TValue>(value, nameof (value));
        KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
        LinkedListNode<KeyValuePair<TKey, TValue>> linkedListNode1;
        if (this.map.TryGetValue(key, out linkedListNode1))
        {
          linkedListNode1.Value = keyValuePair;
        }
        else
        {
          LinkedListNode<KeyValuePair<TKey, TValue>> linkedListNode2 = this.list.AddLast(keyValuePair);
          this.map[key] = linkedListNode2;
        }
      }
    }

    public ICollection<TKey> Keys => (ICollection<TKey>) new MapField<TKey, TValue>.MapView<TKey>(this, (Func<KeyValuePair<TKey, TValue>, TKey>) (pair => pair.Key), new Func<TKey, bool>(this.ContainsKey));

    public ICollection<TValue> Values => (ICollection<TValue>) new MapField<TKey, TValue>.MapView<TValue>(this, (Func<KeyValuePair<TKey, TValue>, TValue>) (pair => pair.Value), new Func<TValue, bool>(this.ContainsValue));

    public void Add(IDictionary<TKey, TValue> entries)
    {
      Preconditions.CheckNotNull<IDictionary<TKey, TValue>>(entries, nameof (entries));
      foreach (KeyValuePair<TKey, TValue> entry in (IEnumerable<KeyValuePair<TKey, TValue>>) entries)
        this.Add(entry.Key, entry.Value);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => (IEnumerator<KeyValuePair<TKey, TValue>>) this.list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    void ICollection<KeyValuePair<TKey, TValue>>.Add(
      KeyValuePair<TKey, TValue> item)
    {
      this.Add(item.Key, item.Value);
    }

    public void Clear()
    {
      this.list.Clear();
      this.map.Clear();
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(
      KeyValuePair<TKey, TValue> item)
    {
      TValue y;
      return this.TryGetValue(item.Key, out y) && EqualityComparer<TValue>.Default.Equals(item.Value, y);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
      KeyValuePair<TKey, TValue>[] array,
      int arrayIndex)
    {
      this.list.CopyTo(array, arrayIndex);
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(
      KeyValuePair<TKey, TValue> item)
    {
      if ((object) item.Key == null)
        throw new ArgumentException("Key is null", nameof (item));
      LinkedListNode<KeyValuePair<TKey, TValue>> node;
      if (!this.map.TryGetValue(item.Key, out node) || !EqualityComparer<TValue>.Default.Equals(item.Value, node.Value.Value))
        return false;
      this.map.Remove(item.Key);
      node.List.Remove(node);
      return true;
    }

    public bool AllowsNullValues => this.allowNullValues;

    public int Count => this.list.Count;

    public bool IsReadOnly => false;

    public override bool Equals(object other) => this.Equals(other as MapField<TKey, TValue>);

    public override int GetHashCode()
    {
      EqualityComparer<TValue> equalityComparer = EqualityComparer<TValue>.Default;
      int num = 0;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this.list)
        num ^= keyValuePair.Key.GetHashCode() * 31 + equalityComparer.GetHashCode(keyValuePair.Value);
      return num;
    }

    public bool Equals(MapField<TKey, TValue> other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      if (other.Count != this.Count)
        return false;
      EqualityComparer<TValue> equalityComparer = EqualityComparer<TValue>.Default;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
      {
        TValue x;
        if (!other.TryGetValue(keyValuePair.Key, out x) || !equalityComparer.Equals(x, keyValuePair.Value))
          return false;
      }
      return true;
    }

    public void AddEntriesFrom(CodedInputStream input, MapField<TKey, TValue>.Codec codec)
    {
      MapField<TKey, TValue>.Codec.MessageAdapter messageAdapter = new MapField<TKey, TValue>.Codec.MessageAdapter(codec);
      do
      {
        messageAdapter.Reset();
        input.ReadMessage((IMessage) messageAdapter);
        this[messageAdapter.Key] = messageAdapter.Value;
      }
      while (input.MaybeConsumeTag(codec.MapTag));
    }

    public void WriteTo(CodedOutputStream output, MapField<TKey, TValue>.Codec codec)
    {
      MapField<TKey, TValue>.Codec.MessageAdapter messageAdapter = new MapField<TKey, TValue>.Codec.MessageAdapter(codec);
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this.list)
      {
        messageAdapter.Key = keyValuePair.Key;
        messageAdapter.Value = keyValuePair.Value;
        output.WriteTag(codec.MapTag);
        output.WriteMessage((IMessage) messageAdapter);
      }
    }

    public int CalculateSize(MapField<TKey, TValue>.Codec codec)
    {
      if (this.Count == 0)
        return 0;
      MapField<TKey, TValue>.Codec.MessageAdapter messageAdapter = new MapField<TKey, TValue>.Codec.MessageAdapter(codec);
      int num = 0;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this.list)
      {
        messageAdapter.Key = keyValuePair.Key;
        messageAdapter.Value = keyValuePair.Value;
        num += CodedOutputStream.ComputeRawVarint32Size(codec.MapTag);
        num += CodedOutputStream.ComputeMessageSize((IMessage) messageAdapter);
      }
      return num;
    }

    void IDictionary.Add(object key, object value) => this.Add((TKey) key, (TValue) value);

    bool IDictionary.Contains(object key) => key is TKey key1 && this.ContainsKey(key1);

    IDictionaryEnumerator IDictionary.GetEnumerator() => (IDictionaryEnumerator) new MapField<TKey, TValue>.DictionaryEnumerator(this.GetEnumerator());

    void IDictionary.Remove(object key)
    {
      Preconditions.CheckNotNull<object>(key, nameof (key));
      if (!(key is TKey key1))
        return;
      this.Remove(key1);
    }

    void ICollection.CopyTo(Array array, int index) => ((ICollection) this.Select<KeyValuePair<TKey, TValue>, DictionaryEntry>((Func<KeyValuePair<TKey, TValue>, DictionaryEntry>) (pair => new DictionaryEntry((object) pair.Key, (object) pair.Value))).ToList<DictionaryEntry>()).CopyTo(array, index);

    bool IDictionary.IsFixedSize => false;

    ICollection IDictionary.Keys => (ICollection) this.Keys;

    ICollection IDictionary.Values => (ICollection) this.Values;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => (object) this;

    object IDictionary.this[object key]
    {
      get
      {
        Preconditions.CheckNotNull<object>(key, nameof (key));
        if (!(key is TKey key1))
          return (object) null;
        TValue obj;
        this.TryGetValue(key1, out obj);
        return (object) obj;
      }
      set => this[(TKey) key] = (TValue) value;
    }

    private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private readonly IEnumerator<KeyValuePair<TKey, TValue>> enumerator;

      internal DictionaryEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> enumerator) => this.enumerator = enumerator;

      public bool MoveNext() => this.enumerator.MoveNext();

      public void Reset() => this.enumerator.Reset();

      public object Current => (object) this.Entry;

      public DictionaryEntry Entry => new DictionaryEntry(this.Key, this.Value);

      public object Key => (object) this.enumerator.Current.Key;

      public object Value => (object) this.enumerator.Current.Value;
    }

    public sealed class Codec
    {
      private readonly FieldCodec<TKey> keyCodec;
      private readonly FieldCodec<TValue> valueCodec;
      private readonly uint mapTag;

      public Codec(FieldCodec<TKey> keyCodec, FieldCodec<TValue> valueCodec, uint mapTag)
      {
        this.keyCodec = keyCodec;
        this.valueCodec = valueCodec;
        this.mapTag = mapTag;
      }

      internal uint MapTag => this.mapTag;

      internal class MessageAdapter : IMessage
      {
        private readonly MapField<TKey, TValue>.Codec codec;

        internal TKey Key { get; set; }

        internal TValue Value { get; set; }

        internal MessageAdapter(MapField<TKey, TValue>.Codec codec) => this.codec = codec;

        internal void Reset()
        {
          this.Key = this.codec.keyCodec.DefaultValue;
          this.Value = this.codec.valueCodec.DefaultValue;
        }

        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            if ((int) num == (int) this.codec.keyCodec.Tag)
              this.Key = this.codec.keyCodec.Read(input);
            else if ((int) num == (int) this.codec.valueCodec.Tag)
              this.Value = this.codec.valueCodec.Read(input);
            else
              input.SkipLastField();
          }
        }

        public void WriteTo(CodedOutputStream output)
        {
          this.codec.keyCodec.WriteTagAndValue(output, this.Key);
          this.codec.valueCodec.WriteTagAndValue(output, this.Value);
        }

        public int CalculateSize() => this.codec.keyCodec.CalculateSizeWithTag(this.Key) + this.codec.valueCodec.CalculateSizeWithTag(this.Value);

        MessageDescriptor IMessage.Descriptor => (MessageDescriptor) null;
      }
    }

    private class MapView<T> : ICollection<T>, IEnumerable<T>, ICollection, IEnumerable
    {
      private readonly MapField<TKey, TValue> parent;
      private readonly Func<KeyValuePair<TKey, TValue>, T> projection;
      private readonly Func<T, bool> containsCheck;

      internal MapView(
        MapField<TKey, TValue> parent,
        Func<KeyValuePair<TKey, TValue>, T> projection,
        Func<T, bool> containsCheck)
      {
        this.parent = parent;
        this.projection = projection;
        this.containsCheck = containsCheck;
      }

      public int Count => this.parent.Count;

      public bool IsReadOnly => true;

      public bool IsSynchronized => false;

      public object SyncRoot => (object) this.parent;

      public void Add(T item) => throw new NotSupportedException();

      public void Clear() => throw new NotSupportedException();

      public bool Contains(T item) => this.containsCheck(item);

      public void CopyTo(T[] array, int arrayIndex)
      {
        if (arrayIndex < 0)
          throw new ArgumentOutOfRangeException(nameof (arrayIndex));
        if (arrayIndex + this.Count >= array.Length)
          throw new ArgumentException("Not enough space in the array", nameof (array));
        foreach (T obj in this)
          array[arrayIndex++] = obj;
      }

      public IEnumerator<T> GetEnumerator() => this.parent.list.Select<KeyValuePair<TKey, TValue>, T>(this.projection).GetEnumerator();

      public bool Remove(T item) => throw new NotSupportedException();

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      public void CopyTo(Array array, int index)
      {
        if (index < 0)
          throw new ArgumentOutOfRangeException(nameof (index));
        if (index + this.Count >= array.Length)
          throw new ArgumentException("Not enough space in the array", nameof (array));
        foreach (T obj in this)
          array.SetValue((object) obj, index++);
      }
    }
  }
}
