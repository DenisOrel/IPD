// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.BiDirectionalLookup`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

public class BiDirectionalLookup<TKey, TValue>
{
  public const int DefaultCapacity = 16 /*0x10*/;
  [NotNull]
  private readonly Dictionary<TKey, List<TValue>> _key2ValuesDictionary;
  [NotNull]
  private readonly Dictionary<TValue, List<TKey>> _value2KeysDictionary;
  [NotNull]
  private readonly List<(TKey Key, TValue Value)> _keyValueList;
  [CanBeNull]
  private HashSet<(TKey Key, TValue Value)> _keyValueHashSet;
  private bool _keyValueUnique = true;

  public bool KeyValueUnique
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._keyValueUnique;
    set
    {
      if (value == this._keyValueUnique)
        return;
      if (value)
      {
        this._keyValueHashSet = new HashSet<(TKey, TValue)>(this._keyValueList.Capacity);
        this._keyValueHashSet.AddRangeCheckUnique<(TKey, TValue)>((IEnumerable<(TKey, TValue)>) this._keyValueList);
      }
      else
        this._keyValueHashSet = (HashSet<(TKey, TValue)>) null;
      this._keyValueUnique = value;
    }
  }

  public BiDirectionalLookup()
    : this(16 /*0x10*/)
  {
  }

  public BiDirectionalLookup(
    [CanBeNull] IEnumerable<(TKey Key, TValue Value)> enumeration,
    [CanBeNull] IEqualityComparer<TKey> keysComparer = null,
    [CanBeNull] IEqualityComparer<TValue> valuesComparer = null)
    : this(16 /*0x10*/)
  {
  }

  public BiDirectionalLookup(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration,
    [CanBeNull] IEqualityComparer<TKey> keysComparer = null,
    [CanBeNull] IEqualityComparer<TValue> valuesComparer = null)
    : this(16 /*0x10*/, enumeration != null ? enumeration.Select<KeyValuePair<TKey, TValue>, (TKey, TValue)>((Func<KeyValuePair<TKey, TValue>, (TKey, TValue)>) (keyValue => (keyValue.Key, keyValue.Value))) : (IEnumerable<(TKey, TValue)>) null, keysComparer, valuesComparer)
  {
  }

  public BiDirectionalLookup(
    [CanBeEmpty] int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration,
    [CanBeNull] IEqualityComparer<TKey> keysComparer = null,
    [CanBeNull] IEqualityComparer<TValue> valuesComparer = null)
    : this(capacity, enumeration != null ? enumeration.Select<KeyValuePair<TKey, TValue>, (TKey, TValue)>((Func<KeyValuePair<TKey, TValue>, (TKey, TValue)>) (keyValue => (keyValue.Key, keyValue.Value))) : (IEnumerable<(TKey, TValue)>) null, keysComparer, valuesComparer)
  {
  }

  public BiDirectionalLookup(
    [CanBeNull] IEqualityComparer<TKey> keysComparer,
    [CanBeNull] IEqualityComparer<TValue> valuesComparer)
    : this(16 /*0x10*/, keysComparer: keysComparer, valuesComparer: valuesComparer)
  {
  }

  public BiDirectionalLookup(
    [CanBeEmpty] int capacity,
    [CanBeNull] IEnumerable<(TKey Key, TValue Value)> enumeration = null,
    [CanBeNull] IEqualityComparer<TKey> keysComparer = null,
    [CanBeNull] IEqualityComparer<TValue> valuesComparer = null)
  {
    int? nullable = enumeration != null ? enumeration.TryGetCount<(TKey, TValue)>() : new int?();
    if (nullable.HasValue)
      capacity = Math.Max(capacity, nullable.Value);
    this._key2ValuesDictionary = keysComparer != null ? new Dictionary<TKey, List<TValue>>(keysComparer) : new Dictionary<TKey, List<TValue>>(capacity);
    this._value2KeysDictionary = valuesComparer != null ? new Dictionary<TValue, List<TKey>>(valuesComparer) : new Dictionary<TValue, List<TKey>>(capacity);
    this._keyValueList = new List<(TKey, TValue)>(capacity);
    if (this.KeyValueUnique)
      this._keyValueHashSet = new HashSet<(TKey, TValue)>(this._keyValueList.Capacity);
    if (enumeration == null)
      return;
    foreach ((TKey Key, TValue Value) keyValue in enumeration)
      this.Add(keyValue);
  }

  [NotNull]
  private static BiDirectionalLookup<TKey, TValue>.NonUniqueKeyValueException ThrowNonUniqueException(
    (TKey Key, TValue Value) keyValue,
    [CanBeNull] string message = null)
  {
    return new BiDirectionalLookup<TKey, TValue>.NonUniqueKeyValueException(keyValue.Key, keyValue.Value, message);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add(KeyValuePair<TKey, TValue> keyValue, [CanBeNull] string exceptMessageIfNotUnique = null)
  {
    this.Add((keyValue.Key, keyValue.Value), exceptMessageIfNotUnique);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add([NotNull] TKey key, [NotNull] TValue value, [CanBeNull] string exceptMessageIfNotUnique = null)
  {
    this.Add((key, value), exceptMessageIfNotUnique);
  }

  public void Add((TKey Key, TValue Value) keyValue, [CanBeNull] string exceptMessageIfNotUnique = null)
  {
    if (this.KeyValueUnique)
      this._keyValueHashSet.AddCheckUnique<(TKey, TValue)>(keyValue, (Func<(TKey, TValue), Exception>) (_ => (Exception) BiDirectionalLookup<TKey, TValue>.ThrowNonUniqueException(keyValue, exceptMessageIfNotUnique)));
    this._keyValueList.Add(keyValue);
    this._key2ValuesDictionary.LazyGet<TKey, List<TValue>>(keyValue.Key, (Func<List<TValue>>) (() => new List<TValue>())).Add(keyValue.Value);
    this._value2KeysDictionary.LazyGet<TValue, List<TKey>>(keyValue.Value, (Func<List<TKey>>) (() => new List<TKey>())).Add(keyValue.Key);
  }

  public void AddRange(
    [NotNull] IEnumerable<(TKey Key, TValue Value)> keyValues,
    [CanBeNull] string exceptMessageIfNotUnique = null)
  {
    foreach ((TKey Key, TValue Value) keyValue in keyValues)
      this.Add(keyValue, exceptMessageIfNotUnique);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddRange(
    [NotNull] IEnumerable<KeyValuePair<TKey, TValue>> keyValues,
    [CanBeNull] string exceptMessageIfNotUnique = null)
  {
    foreach (KeyValuePair<TKey, TValue> keyValue in keyValues)
      this.Add((keyValue.Key, keyValue.Value), exceptMessageIfNotUnique);
  }

  [NotNull]
  public IReadOnlyCollection<TValue> GetValuesByKey([NotNull] TKey key)
  {
    return (IReadOnlyCollection<TValue>) this._key2ValuesDictionary.GetOrDefault<TKey, List<TValue>>(key) ?? (IReadOnlyCollection<TValue>) Array.Empty<TValue>();
  }

  [NotNull]
  public IReadOnlyCollection<TKey> GetKeysByValue([NotNull] TValue value)
  {
    return (IReadOnlyCollection<TKey>) this._value2KeysDictionary.GetOrDefault<TValue, List<TKey>>(value) ?? (IReadOnlyCollection<TKey>) Array.Empty<TKey>();
  }

  [Serializable]
  public class NonUniqueKeyValueException : 
    Exception,
    ISerializable,
    IEquatable<BiDirectionalLookup<TKey, TValue>.NonUniqueKeyValueException>
  {
    [CanBeNull]
    public TKey Key { get; }

    [CanBeNull]
    public TValue Value { get; }

    protected NonUniqueKeyValueException()
    {
    }

    internal NonUniqueKeyValueException([NotNull] TKey key, [NotNull] TValue value, [CanBeNull, CanBeEmpty] string message = null)
      : base(string.IsNullOrWhiteSpace(message) ? $"Элемент с ключом \"{key}\" значением \"{value}\" должен быть уникален" : message)
    {
      this.Key = key;
      this.Value = value;
    }

    protected NonUniqueKeyValueException([NotNull] SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.Key = (TKey) info.GetValue(nameof (Key), typeof (TKey));
      this.Value = (TValue) info.GetValue(nameof (Value), typeof (TValue));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("Key", (object) this.Key);
      info.AddValue("Value", (object) this.Value);
    }

    public override int GetHashCode() => (this.Key, this.Value).GetHashCode();

    public override bool Equals([CanBeNull] object obj)
    {
      return obj is BiDirectionalLookup<TKey, TValue>.NonUniqueKeyValueException other && this.Equals(other);
    }

    public bool Equals(
      [CanBeNull] BiDirectionalLookup<TKey, TValue>.NonUniqueKeyValueException other)
    {
      if (other == null)
        return false;
      if (this == other)
        return true;
      return object.Equals((object) this.Key, (object) other.Key) && object.Equals((object) this.Value, (object) other.Value);
    }
  }
}
