// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Maybe`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

[Serializable]
public readonly struct Maybe<TValue>([CanBeNull] TValue value) : 
  IEquatable<Maybe<TValue>>,
  IStructuralEquatable,
  IComparable<Maybe<TValue>>,
  IComparable,
  IStructuralComparable
{
  public static readonly Maybe<TValue> Empty = new Maybe<TValue>();
  public static readonly bool Nullable = typeof (TValue).IsByRef;
  public readonly bool HasValue = true;
  [CanBeNull]
  internal readonly TValue _Value = value;
  [CanBeNull]
  private static EqualityComparer<TValue> _defaultEqualityComparer;
  private static Comparer<TValue> _defaultComparer;

  public bool TryGetValue([CanBeNull] out TValue value)
  {
    if (this.HasValue)
    {
      value = this._Value;
      return true;
    }
    value = default (TValue);
    return false;
  }

  [CanBeNull]
  public TValue Value
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (!this.HasValue)
        throw new InvalidOperationException("No value!");
      return this._Value;
    }
  }

  public bool EmptyOrNull
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (!this.HasValue)
        return true;
      return Maybe<TValue>.Nullable && (object) this._Value == null;
    }
  }

  public bool IsNull
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Maybe<TValue>.Nullable && (object) this._Value == null;
    }
  }

  public bool NotNull
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return !Maybe<TValue>.Nullable || (object) this._Value != null;
    }
  }

  [CanBeNull]
  public TValue GetValueOrDefault() => !this.HasValue ? default (TValue) : this._Value;

  [CanBeNull]
  public TValue GetValueOrDefault([CanBeNull] TValue defaultValue)
  {
    return !this.HasValue ? defaultValue : this._Value;
  }

  public override string ToString()
  {
    return !this.HasValue || Maybe<TValue>.Nullable && (object) this._Value == null ? string.Empty : this._Value.ToString();
  }

  public override int GetHashCode()
  {
    return !this.HasValue ? int.MinValue : (Maybe<TValue>._defaultEqualityComparer ?? (Maybe<TValue>._defaultEqualityComparer = EqualityComparer<TValue>.Default)).GetHashCode(this._Value);
  }

  public int GetHashCode([CanBeNull] IEqualityComparer comparer)
  {
    if (!this.HasValue)
      return int.MinValue;
    IEqualityComparer equalityComparer1 = comparer;
    if (equalityComparer1 == null)
    {
      EqualityComparer<TValue> equalityComparer2 = Maybe<TValue>._defaultEqualityComparer;
      equalityComparer1 = equalityComparer2 != null ? (IEqualityComparer) equalityComparer2 : (IEqualityComparer) (Maybe<TValue>._defaultEqualityComparer = EqualityComparer<TValue>.Default);
    }
    comparer = equalityComparer1;
    return comparer.GetHashCode((object) this._Value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(Maybe<TValue> other)
  {
    if (!this.HasValue && !other.HasValue)
      return true;
    return this.HasValue && other.HasValue && object.Equals((object) this._Value, (object) other.Value);
  }

  public bool Equals([CanBeNull] object obj, [CanBeNull] IEqualityComparer comparer)
  {
    if (!(obj is Maybe<TValue> maybe))
      return false;
    if (!this.HasValue && !maybe.HasValue)
      return true;
    if (!this.HasValue || !maybe.HasValue)
      return false;
    return comparer == null ? object.Equals((object) this._Value, (object) maybe.Value) : comparer.Equals((object) this._Value, (object) maybe.Value);
  }

  public override bool Equals(object other)
  {
    if (!this.HasValue)
      return other == null;
    return other != null && object.Equals((object) this._Value, other);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int CompareTo(Maybe<TValue> other)
  {
    return this.HasValue ? (!other.HasValue ? 1 : (Maybe<TValue>._defaultComparer ?? (Maybe<TValue>._defaultComparer = Comparer<TValue>.Default)).Compare(this.Value, other.Value)) : (!other.HasValue ? 0 : -1);
  }

  public int CompareTo([CanBeNull] object obj, [CanBeNull] IComparer comparer)
  {
    if (!(obj is Maybe<TValue> maybe))
      return 1;
    if (this.HasValue)
    {
      if (!maybe.HasValue)
        return 1;
      IComparer comparer1 = comparer;
      if (comparer1 == null)
      {
        Comparer<TValue> defaultComparer = Maybe<TValue>._defaultComparer;
        comparer1 = defaultComparer != null ? (IComparer) defaultComparer : (IComparer) (Maybe<TValue>._defaultComparer = Comparer<TValue>.Default);
      }
      comparer = comparer1;
      return comparer.Compare((object) this.Value, (object) maybe.Value);
    }
    return !maybe.HasValue ? 0 : -1;
  }

  public int CompareTo([CanBeNull] object obj)
  {
    return obj == null || !(obj is Maybe<TValue> other) ? 1 : this.CompareTo(other);
  }

  public static implicit operator Maybe<TValue>([CanBeNull] TValue value)
  {
    return new Maybe<TValue>(value);
  }

  [CanBeNull]
  public static implicit operator TValue(in Maybe<TValue> value) => value.Value;

  public static bool operator ==(in Maybe<TValue> left, in Maybe<TValue> right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(in Maybe<TValue> left, in Maybe<TValue> right)
  {
    return !left.Equals(right);
  }

  public static bool operator <(in Maybe<TValue> left, in Maybe<TValue> right)
  {
    return left.CompareTo(right) < 0;
  }

  public static bool operator >(in Maybe<TValue> left, in Maybe<TValue> right)
  {
    return left.CompareTo(right) > 0;
  }

  public static bool operator <=(in Maybe<TValue> left, in Maybe<TValue> right)
  {
    return left.CompareTo(right) <= 0;
  }

  public static bool operator >=(in Maybe<TValue> left, in Maybe<TValue> right)
  {
    return left.CompareTo(right) >= 0;
  }
}
