// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.FixedDictionary
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class FixedDictionary
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static EmptyDictionary<TKey, TValue> Empty<TKey, TValue>()
  {
    return EmptyDictionary<TKey, TValue>.Singleton.Instance;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary1<TKey, TValue> Create<TKey, TValue>([NotNull] TKey key, [CanBeNull] TValue value)
  {
    return new FixedDictionary1<TKey, TValue>((IEqualityComparer<TKey>) null, key, value);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary1<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key,
    [CanBeNull] TValue value)
  {
    return new FixedDictionary1<TKey, TValue>(keyComparer, key, value);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary2<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1)
  {
    return new FixedDictionary2<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary2<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1)
  {
    return new FixedDictionary2<TKey, TValue>(keyComparer, key0, value0, key1, value1);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary3<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2)
  {
    return new FixedDictionary3<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary3<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2)
  {
    return new FixedDictionary3<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary4<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3)
  {
    return new FixedDictionary4<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary4<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3)
  {
    return new FixedDictionary4<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary5<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4)
  {
    return new FixedDictionary5<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary5<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4)
  {
    return new FixedDictionary5<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary6<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5)
  {
    return new FixedDictionary6<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary6<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5)
  {
    return new FixedDictionary6<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary7<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6)
  {
    return new FixedDictionary7<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary7<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6)
  {
    return new FixedDictionary7<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary8<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7)
  {
    return new FixedDictionary8<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary8<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7)
  {
    return new FixedDictionary8<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary9<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8)
  {
    return new FixedDictionary9<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary9<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8)
  {
    return new FixedDictionary9<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary10<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9)
  {
    return new FixedDictionary10<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary10<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9)
  {
    return new FixedDictionary10<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary11<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10)
  {
    return new FixedDictionary11<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary11<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10)
  {
    return new FixedDictionary11<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary12<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10,
    [NotNull] TKey key11,
    [CanBeNull] TValue value11)
  {
    return new FixedDictionary12<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10, key11, value11);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary12<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10,
    [NotNull] TKey key11,
    [CanBeNull] TValue value11)
  {
    return new FixedDictionary12<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10, key11, value11);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary13<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10,
    [NotNull] TKey key11,
    [CanBeNull] TValue value11,
    [NotNull] TKey key12,
    [CanBeNull] TValue value12)
  {
    return new FixedDictionary13<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10, key11, value11, key12, value12);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary13<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10,
    [NotNull] TKey key11,
    [CanBeNull] TValue value11,
    [NotNull] TKey key12,
    [CanBeNull] TValue value12)
  {
    return new FixedDictionary13<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10, key11, value11, key12, value12);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary14<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10,
    [NotNull] TKey key11,
    [CanBeNull] TValue value11,
    [NotNull] TKey key12,
    [CanBeNull] TValue value12,
    [NotNull] TKey key13,
    [CanBeNull] TValue value13)
  {
    return new FixedDictionary14<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10, key11, value11, key12, value12, key13, value13);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary14<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10,
    [NotNull] TKey key11,
    [CanBeNull] TValue value11,
    [NotNull] TKey key12,
    [CanBeNull] TValue value12,
    [NotNull] TKey key13,
    [CanBeNull] TValue value13)
  {
    return new FixedDictionary14<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10, key11, value11, key12, value12, key13, value13);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary15<TKey, TValue> Create<TKey, TValue>(
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10,
    [NotNull] TKey key11,
    [CanBeNull] TValue value11,
    [NotNull] TKey key12,
    [CanBeNull] TValue value12,
    [NotNull] TKey key13,
    [CanBeNull] TValue value13,
    [NotNull] TKey key14,
    [CanBeNull] TValue value14)
  {
    return new FixedDictionary15<TKey, TValue>((IEqualityComparer<TKey>) null, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10, key11, value11, key12, value12, key13, value13, key14, value14);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FixedDictionary15<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10,
    [NotNull] TKey key11,
    [CanBeNull] TValue value11,
    [NotNull] TKey key12,
    [CanBeNull] TValue value12,
    [NotNull] TKey key13,
    [CanBeNull] TValue value13,
    [NotNull] TKey key14,
    [CanBeNull] TValue value14)
  {
    return new FixedDictionary15<TKey, TValue>(keyComparer, key0, value0, key1, value1, key2, value2, key3, value3, key4, value4, key5, value5, key6, value6, key7, value7, key8, value8, key9, value9, key10, value10, key11, value11, key12, value12, key13, value13, key14, value14);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyDictionary<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] params (TKey Key, TValue Value)[] keyValues)
  {
    return FixedDictionary.Create<TKey, TValue>((IEnumerable<(TKey, TValue)>) keyValues, keyComparer, keyValues.Length);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyDictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] params (TKey Key, TValue Value)[] keyValues)
  {
    return FixedDictionary.Create<TKey, TValue>((IEnumerable<(TKey, TValue)>) keyValues, (IEqualityComparer<TKey>) null, keyValues.Length);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyDictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] IEnumerable<(TKey Key, TValue Value)> keyValues,
    int count)
  {
    return FixedDictionary.Create<TKey, TValue>(keyValues, (IEqualityComparer<TKey>) null, count);
  }

  [NotNull]
  public static IReadOnlyDictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] IEnumerable<(TKey Key, TValue Value)> keyValues,
    [CanBeNull] IEqualityComparer<TKey> keyComparer = null,
    int count = -1)
  {
    int result;
    if (count < 0 && keyValues.TryGetCount<(TKey, TValue)>(out result))
      count = result;
    if (count >= 0)
    {
      switch (count)
      {
        case 0:
          return (IReadOnlyDictionary<TKey, TValue>) FixedDictionary.Empty<TKey, TValue>();
        case 1:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary1<TKey, TValue>(keyComparer, keyValues);
        case 2:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary2<TKey, TValue>(keyComparer, keyValues);
        case 3:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary3<TKey, TValue>(keyComparer, keyValues);
        case 4:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary4<TKey, TValue>(keyComparer, keyValues);
        case 5:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary5<TKey, TValue>(keyComparer, keyValues);
        case 6:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary6<TKey, TValue>(keyComparer, keyValues);
        case 7:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary7<TKey, TValue>(keyComparer, keyValues);
        case 8:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary8<TKey, TValue>(keyComparer, keyValues);
        case 9:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary9<TKey, TValue>(keyComparer, keyValues);
        case 10:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary10<TKey, TValue>(keyComparer, keyValues);
        case 11:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary11<TKey, TValue>(keyComparer, keyValues);
        case 12:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary12<TKey, TValue>(keyComparer, keyValues);
        case 13:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary13<TKey, TValue>(keyComparer, keyValues);
        case 14:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary14<TKey, TValue>(keyComparer, keyValues);
        case 15:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary15<TKey, TValue>(keyComparer, keyValues);
        default:
          Dictionary<TKey, TValue> dictionary1 = new Dictionary<TKey, TValue>(count, keyComparer);
          dictionary1.AddRange<TKey, TValue>(keyValues);
          return (IReadOnlyDictionary<TKey, TValue>) dictionary1;
      }
    }
    else
    {
      Dictionary<TKey, TValue> dictionary2 = new Dictionary<TKey, TValue>(keyComparer);
      dictionary2.AddRange<TKey, TValue>(keyValues);
      return (IReadOnlyDictionary<TKey, TValue>) dictionary2;
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyDictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] IEnumerable<KeyValuePair<TKey, TValue>> keyValues,
    int count)
  {
    return FixedDictionary.Create<TKey, TValue>(keyValues, (IEqualityComparer<TKey>) null, count);
  }

  [NotNull]
  public static IReadOnlyDictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] IEnumerable<KeyValuePair<TKey, TValue>> keyValues,
    [CanBeNull] IEqualityComparer<TKey> keyComparer = null,
    int count = -1)
  {
    int result;
    if (count < 0 && keyValues.TryGetCount<KeyValuePair<TKey, TValue>>(out result))
      count = result;
    if (count >= 0)
    {
      switch (count)
      {
        case 0:
          return (IReadOnlyDictionary<TKey, TValue>) FixedDictionary.Empty<TKey, TValue>();
        case 1:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary1<TKey, TValue>(keyComparer, keyValues);
        case 2:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary2<TKey, TValue>(keyComparer, keyValues);
        case 3:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary3<TKey, TValue>(keyComparer, keyValues);
        case 4:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary4<TKey, TValue>(keyComparer, keyValues);
        case 5:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary5<TKey, TValue>(keyComparer, keyValues);
        case 6:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary6<TKey, TValue>(keyComparer, keyValues);
        case 7:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary7<TKey, TValue>(keyComparer, keyValues);
        case 8:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary8<TKey, TValue>(keyComparer, keyValues);
        case 9:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary9<TKey, TValue>(keyComparer, keyValues);
        case 10:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary10<TKey, TValue>(keyComparer, keyValues);
        case 11:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary11<TKey, TValue>(keyComparer, keyValues);
        case 12:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary12<TKey, TValue>(keyComparer, keyValues);
        case 13:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary13<TKey, TValue>(keyComparer, keyValues);
        case 14:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary14<TKey, TValue>(keyComparer, keyValues);
        case 15:
          return (IReadOnlyDictionary<TKey, TValue>) new FixedDictionary15<TKey, TValue>(keyComparer, keyValues);
        default:
          Dictionary<TKey, TValue> dictionary1 = new Dictionary<TKey, TValue>(count, keyComparer);
          dictionary1.AddRange<TKey, TValue>(keyValues);
          return (IReadOnlyDictionary<TKey, TValue>) dictionary1;
      }
    }
    else
    {
      Dictionary<TKey, TValue> dictionary2 = new Dictionary<TKey, TValue>(keyComparer);
      dictionary2.AddRange<TKey, TValue>(keyValues);
      return (IReadOnlyDictionary<TKey, TValue>) dictionary2;
    }
  }
}
