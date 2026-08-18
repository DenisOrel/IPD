// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.DictionaryAdapterBase`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
public abstract class DictionaryAdapterBase<TKey, TValue>([NotNull] IDictionary<TKey, TValue> dictionary) : 
  WrapperBase<IDictionary<TKey, TValue>>(dictionary),
  IDictionary<TKey, TValue>,
  ICollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable,
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IDictionary,
  ICollection,
  IEquatable<IDictionary<TKey, TValue>>,
  IEquatable<IReadOnlyDictionary<TKey, TValue>>,
  IEquatable<IDictionary>,
  ISerializable,
  IDeserializationCallback
{
  private const string SerializeArrayName = "AsArray";

  [NotNull]
  protected IDictionary<TKey, TValue> Dictionary
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject;
  }

  protected DictionaryAdapterBase([NotNull] SerializationInfo info, StreamingContext context)
    : this((IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>((KeyValuePair<TKey, TValue>[]) info.GetValue("AsArray", typeof (KeyValuePair<TKey, TValue>[])) ?? throw new KeyNotFoundException("AsArray")))
  {
  }

  protected override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this.Dictionary.AsArray<KeyValuePair<TKey, TValue>>());
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key) => this.Dictionary.ContainsKey(key);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add([NotNull] TKey key, [CanBeNull] TValue value) => this.Dictionary.Add(key, value);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove([NotNull] TKey key) => this.Dictionary.Remove(key);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, out TValue value)
  {
    return this.Dictionary.TryGetValue(key, out value);
  }

  [CanBeNull]
  public TValue this[[NotNull] TKey key]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary[key];
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.Dictionary[key] = value;
    }
  }

  IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
  {
    get => (IEnumerable<TKey>) this.Dictionary.Keys;
  }

  ICollection IDictionary.Values => ((IDictionary) this.Dictionary).Values;

  ICollection IDictionary.Keys => ((IDictionary) this.Dictionary).Keys;

  IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
  {
    get => (IEnumerable<TValue>) this.Dictionary.Values;
  }

  public ICollection<TKey> Keys
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary.Keys;
    }
  }

  public ICollection<TValue> Values
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary.Values;
    }
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add(KeyValuePair<TKey, TValue> item) => this.Dictionary.Add(item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Contains(object key) => ((IDictionary) this.Dictionary).Contains(key);

  void IDictionary.Add([NotNull] object key, [CanBeNull] object value)
  {
    ((IDictionary) this.Dictionary).Add(key, value);
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Clear() => this.Dictionary.Clear();

  IDictionaryEnumerator IDictionary.GetEnumerator()
  {
    return ((IDictionary) this.Dictionary).GetEnumerator();
  }

  void IDictionary.Remove(object key) => ((IDictionary) this.Dictionary).Remove(key);

  [CanBeNull]
  object IDictionary.this[[NotNull] object key]
  {
    get => ((IDictionary) this.Dictionary)[key];
    set => ((IDictionary) this.Dictionary)[key] = value;
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Contains(KeyValuePair<TKey, TValue> item) => this.Dictionary.Contains(item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
  {
    this.Dictionary.CopyTo(array, arrayIndex);
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove(KeyValuePair<TKey, TValue> item)
  {
    return ((ICollection<KeyValuePair<TKey, TValue>>) this.Dictionary).Remove(item);
  }

  void ICollection.CopyTo(Array array, int index)
  {
    ((ICollection) this.Dictionary).CopyTo(array, index);
  }

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary.Count;
    }
  }

  object ICollection.SyncRoot => ((ICollection) this.Dictionary).SyncRoot;

  bool ICollection.IsSynchronized => ((ICollection) this.Dictionary).IsSynchronized;

  bool IDictionary.IsReadOnly => ((IDictionary) this.Dictionary).IsReadOnly;

  bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
  {
    get => ((IDictionary) this.Dictionary).IsReadOnly;
  }

  bool IDictionary.IsFixedSize => ((IDictionary) this.Dictionary).IsFixedSize;

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.Dictionary.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.Dictionary.GetEnumerator();

  public override int GetHashCode() => base.GetHashCode();

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj || this.WrappedObject == obj)
      return true;
    switch (obj)
    {
      case IDictionary<TKey, TValue> other1:
        return this.Equals(other1);
      case IReadOnlyDictionary<TKey, TValue> other2:
        return this.Equals(other2);
      case IDictionary other3:
        return this.Equals(other3);
      default:
        return base.Equals(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] IReadOnlyDictionary<TKey, TValue> other)
  {
    if (other == null)
      return false;
    if (this == other || this.WrappedObject == other)
      return true;
    return this.WrappedObject is IReadOnlyDictionary<TKey, TValue> wrappedObject && other.Equals((object) wrappedObject);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] IDictionary other)
  {
    if (other == null)
      return false;
    if (this == other || this.WrappedObject == other)
      return true;
    return this.WrappedObject is IDictionary wrappedObject && other.Equals((object) wrappedObject);
  }
}
