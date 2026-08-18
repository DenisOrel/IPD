// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EqualityComparerByKey`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class EqualityComparerByKey<T, TKey> : IEqualityComparer<T>
{
  private static bool? _referenceType;
  private static bool? _referenceKeyType;
  [CanBeNull]
  private readonly EqualityComparerByKey<T, TKey>.CompareMethodDelegate _compareMethod;
  [CanBeNull]
  private IEqualityComparer<TKey> _keyComparer;
  [NotNull]
  private readonly EqualityComparerByKey<T, TKey>.KeySelectorMethodDelegate _keySelectorMethod;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsReferenceType()
  {
    return EqualityComparerByKey<T, TKey>._referenceType ?? (EqualityComparerByKey<T, TKey>._referenceType = new bool?(!typeof (T).IsValueType)).Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsKeyReferenceType()
  {
    return EqualityComparerByKey<T, TKey>._referenceKeyType ?? (EqualityComparerByKey<T, TKey>._referenceKeyType = new bool?(!typeof (TKey).IsValueType)).Value;
  }

  public EqualityComparerByKey(
    [NotNull] EqualityComparerByKey<T, TKey>.CompareMethodDelegate compareMethod,
    [NotNull] EqualityComparerByKey<T, TKey>.KeySelectorMethodDelegate keySelectorMethod)
  {
    this._compareMethod = compareMethod;
    this._keySelectorMethod = keySelectorMethod;
  }

  public EqualityComparerByKey(
    [NotNull] EqualityComparerByKey<T, TKey>.KeySelectorMethodDelegate keySelectorMethod,
    [NotNull] EqualityComparerByKey<T, TKey>.CompareMethodDelegate compareMethod)
  {
    this._compareMethod = compareMethod;
    this._keySelectorMethod = keySelectorMethod;
  }

  public EqualityComparerByKey(
    [NotNull] EqualityComparerByKey<T, TKey>.KeySelectorMethodDelegate keySelectorMethod,
    [NotNull] IEqualityComparer<TKey> keyComparer)
  {
    this._keySelectorMethod = keySelectorMethod;
    this._keyComparer = keyComparer;
  }

  public EqualityComparerByKey(
    [NotNull] EqualityComparerByKey<T, TKey>.KeySelectorMethodDelegate keySelectorMethod)
  {
    this._keySelectorMethod = keySelectorMethod;
  }

  public bool Equals([CanBeNull] T first, [CanBeNull] T second)
  {
    if (EqualityComparerByKey<T, TKey>.IsReferenceType())
    {
      bool flag1 = (object) first == null;
      bool flag2 = (object) second == null;
      if (flag1 & flag2)
        return true;
      if (flag1 | flag2)
        return false;
    }
    TKey key1 = this._keySelectorMethod(first);
    TKey key2 = this._keySelectorMethod(second);
    if (EqualityComparerByKey<T, TKey>.IsKeyReferenceType())
    {
      bool flag3 = (object) key1 == null;
      bool flag4 = (object) key2 == null;
      if (flag3 & flag4)
        return true;
      if (flag3 | flag4)
        return false;
    }
    return this._compareMethod != null ? this._compareMethod(key1, key2) : (this._keyComparer ?? (this._keyComparer = (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default)).Equals(key1, key2);
  }

  public int GetHashCode(T obj)
  {
    return !EqualityComparerByKey<T, TKey>.IsReferenceType() || (object) obj != null ? obj.GetHashCode() : throw new ArgumentNullException(nameof (obj));
  }

  public delegate bool CompareMethodDelegate([NotNull] TKey first, [NotNull] TKey second);

  [CanBeNull]
  public delegate TKey KeySelectorMethodDelegate([CanBeNull] T item);
}
