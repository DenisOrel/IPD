// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ValuesDictionary`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
public class ValuesDictionary<TKey, TValue> : 
  Dictionary<TKey, TValue>,
  IDictionary<TKey, TValue>,
  ICollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable,
  IDictionary,
  ICollection,
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  ISerializable,
  IDeserializationCallback,
  ICollection<(TKey, TValue)>,
  IEnumerable<(TKey, TValue)>,
  IReadOnlyCollection<(TKey, TValue)>
{
  public ValuesDictionary()
  {
  }

  public ValuesDictionary(int capacity)
    : base(capacity)
  {
  }

  public ValuesDictionary([NotNull] IEqualityComparer<TKey> comparer)
    : base(comparer)
  {
  }

  public ValuesDictionary([NotNull] IDictionary<TKey, TValue> dictionary)
    : base(dictionary)
  {
  }

  public ValuesDictionary(int capacity, [NotNull] IEqualityComparer<TKey> comparer)
    : base(comparer)
  {
  }

  public ValuesDictionary([NotNull] IDictionary<TKey, TValue> dictionary, [NotNull] IEqualityComparer<TKey> comparer)
    : base(dictionary, comparer)
  {
  }

  protected ValuesDictionary([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public IEnumerator<(TKey, TValue)> GetEnumerator()
  {
    return this.Select<KeyValuePair<TKey, TValue>, (TKey, TValue)>((Func<KeyValuePair<TKey, TValue>, (TKey, TValue)>) (pair => (pair.Key, pair.Value))).GetEnumerator();
  }

  public void Add((TKey, TValue) item)
  {
    ((ICollection<KeyValuePair<TKey, TValue>>) this).Add(new KeyValuePair<TKey, TValue>(item.Item1, item.Item2));
  }

  public bool Contains((TKey, TValue) item)
  {
    return ((ICollection<KeyValuePair<TKey, TValue>>) this).Contains(new KeyValuePair<TKey, TValue>(item.Item1, item.Item2));
  }

  public void CopyTo((TKey, TValue)[] array, int arrayIndex)
  {
    Intermech.Diagnostics.Check.NotNull<(TKey, TValue)[]>(array, nameof (array));
    Intermech.Diagnostics.Check.ArgumentInRange(array.Length >= arrayIndex + this.Count, "array.Length >= arrayIndex + Count");
    foreach ((TKey, TValue) tuple in this)
      array[arrayIndex++] = tuple;
  }

  public bool Remove((TKey, TValue) item)
  {
    return ((ICollection<KeyValuePair<TKey, TValue>>) this).Remove(new KeyValuePair<TKey, TValue>(item.Item1, item.Item2));
  }

  public bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>) this).IsReadOnly;
}
