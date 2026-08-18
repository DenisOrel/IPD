// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ComparerByKey`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class ComparerByKey<T, TKey> : IComparer<T>
{
  private static bool? _referenceType;
  private static bool? _referenceKeyType;
  [CanBeNull]
  private readonly ComparerByKey<T, TKey>.CompareMethodDelegate _compareMethod;
  [CanBeNull]
  private IComparer<TKey> _keyComparer;
  [NotNull]
  private readonly ComparerByKey<T, TKey>.KeySelectorMethodDelegate _keySelectorMethod;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsReferenceType()
  {
    return ComparerByKey<T, TKey>._referenceType ?? (ComparerByKey<T, TKey>._referenceType = new bool?(!typeof (T).IsValueType)).Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsKeyReferenceType()
  {
    return ComparerByKey<T, TKey>._referenceKeyType ?? (ComparerByKey<T, TKey>._referenceKeyType = new bool?(!typeof (TKey).IsValueType)).Value;
  }

  public ComparerByKey(
    [NotNull] ComparerByKey<T, TKey>.CompareMethodDelegate compareMethod,
    [NotNull] ComparerByKey<T, TKey>.KeySelectorMethodDelegate keySelectorMethod)
  {
    this._compareMethod = compareMethod;
    this._keySelectorMethod = keySelectorMethod;
  }

  public ComparerByKey(
    [NotNull] ComparerByKey<T, TKey>.KeySelectorMethodDelegate keySelectorMethod,
    [NotNull] ComparerByKey<T, TKey>.CompareMethodDelegate compareMethod)
  {
    this._compareMethod = compareMethod;
    this._keySelectorMethod = keySelectorMethod;
  }

  public ComparerByKey(
    [NotNull] ComparerByKey<T, TKey>.KeySelectorMethodDelegate keySelectorMethod,
    [NotNull] IComparer<TKey> keyComparer)
  {
    this._keySelectorMethod = keySelectorMethod;
    this._keyComparer = keyComparer;
  }

  public ComparerByKey(
    [NotNull] ComparerByKey<T, TKey>.KeySelectorMethodDelegate keySelectorMethod)
  {
    this._keySelectorMethod = keySelectorMethod;
  }

  public int Compare([CanBeNull] T first, [CanBeNull] T second)
  {
    if (ComparerByKey<T, TKey>.IsReferenceType())
    {
      bool flag1 = (object) first == null;
      bool flag2 = (object) second == null;
      if (flag1 & flag2)
        return 0;
      if (flag2)
        return 1;
      if (flag1)
        return -1;
    }
    TKey key1 = this._keySelectorMethod(first);
    TKey key2 = this._keySelectorMethod(second);
    if (ComparerByKey<T, TKey>.IsKeyReferenceType())
    {
      bool flag3 = (object) key1 == null;
      bool flag4 = (object) key2 == null;
      if (flag3 & flag4)
        return 0;
      if (flag4)
        return 1;
      if (flag3)
        return -1;
    }
    return this._compareMethod != null ? this._compareMethod(key1, key2) : (this._keyComparer ?? (this._keyComparer = (IComparer<TKey>) Comparer<TKey>.Default)).Compare(key1, key2);
  }

  public delegate int CompareMethodDelegate([NotNull] TKey first, [NotNull] TKey second);

  [CanBeNull]
  public delegate TKey KeySelectorMethodDelegate([CanBeNull] T item);
}
