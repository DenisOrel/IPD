// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyDictionaryAdapterBase`2
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
public abstract class ReadOnlyDictionaryAdapterBase<TKey, TValue>(
  [NotNull] IReadOnlyDictionary<TKey, TValue> dictionary) : 
  WrapperBase<IReadOnlyDictionary<TKey, TValue>>(dictionary),
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable,
  IEquatable<IReadOnlyDictionary<TKey, TValue>>,
  IEquatable<IDictionary<TKey, TValue>>,
  IEquatable<IDictionary>,
  ISerializable
{
  private const string SerializeArrayName = "AsArray";

  [NotNull]
  protected IReadOnlyDictionary<TKey, TValue> Dictionary
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject;
  }

  protected ReadOnlyDictionaryAdapterBase([NotNull] SerializationInfo info, StreamingContext context)
    : this((IReadOnlyDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>((KeyValuePair<TKey, TValue>[]) info.GetValue("AsArray", typeof (KeyValuePair<TKey, TValue>[])) ?? throw new KeyNotFoundException("AsArray")))
  {
  }

  protected override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this.Dictionary.AsArray<KeyValuePair<TKey, TValue>>());
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.Dictionary.GetEnumerator();

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.Dictionary.GetEnumerator();

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary.Count;
    }
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key) => this.Dictionary.ContainsKey(key);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue(TKey key, out TValue value)
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
  }

  [NotNull]
  [ItemNotNull]
  public IEnumerable<TKey> Keys
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary.Keys;
    }
  }

  [NotNull]
  [ItemCanBeNull]
  public IEnumerable<TValue> Values
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary.Values;
    }
  }

  public override int GetHashCode() => base.GetHashCode();

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj || this.WrappedObject == obj)
      return true;
    switch (obj)
    {
      case IReadOnlyDictionary<TKey, TValue> other1:
        return this.Equals(other1);
      case IDictionary<TKey, TValue> other2:
        return this.Equals(other2);
      case IDictionary other3:
        return this.Equals(other3);
      default:
        return base.Equals(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] IDictionary<TKey, TValue> other)
  {
    if (other == null)
      return false;
    if (this == other || this.WrappedObject == other)
      return true;
    return this.WrappedObject is IDictionary<TKey, TValue> wrappedObject && other.Equals((object) wrappedObject);
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
