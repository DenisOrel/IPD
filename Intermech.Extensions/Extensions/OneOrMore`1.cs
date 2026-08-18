// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.OneOrMore`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public readonly struct OneOrMore<T> : 
  IEquatable<OneOrMore<T>>,
  IEquatable<T>,
  IEquatable<T[]>,
  IEquatable<IList<T>>,
  IEquatable<IReadOnlyList<T>>,
  IEquatable<ICollection<T>>,
  IEquatable<IReadOnlyCollection<T>>,
  IEnumerable<T>,
  IEnumerable,
  IReadOnlyCollection<T>
{
  private readonly bool _hasValue;
  [CanBeNull]
  private readonly T _singleValue;
  [CanBeNull]
  private readonly IReadOnlyList<T> _multipleValues;

  internal bool HasValue
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._hasValue;
  }

  [Conditional("DEBUG")]
  public void CheckHasValue()
  {
    if (!this._hasValue)
      throw new ValueEmptyException("Value not assigned!");
  }

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      IReadOnlyList<T> multipleValues = this._multipleValues;
      return multipleValues == null ? 1 : multipleValues.Count;
    }
  }

  [CanBeNull]
  public T Value
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._singleValue;
  }

  [NotNull]
  public IReadOnlyList<T> Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._multipleValues;
  }

  public bool OneValue
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._multipleValues == null;
  }

  public bool MultipleValues
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._multipleValues != null;
  }

  public OneOrMore([CanBeNull] T value)
  {
    this._singleValue = value;
    this._multipleValues = (IReadOnlyList<T>) null;
    this._hasValue = true;
  }

  public OneOrMore([NotNull] IEnumerable<T> values)
  {
    this._multipleValues = (IReadOnlyList<T>) values.ToArray<T>();
    switch (this._multipleValues.Count)
    {
      case 0:
        throw new ArgumentCollectionIsEmptyException(nameof (values));
      case 1:
        this._singleValue = this._multipleValues[0];
        this._multipleValues = (IReadOnlyList<T>) null;
        break;
      default:
        this._singleValue = default (T);
        break;
    }
    this._hasValue = true;
  }

  public OneOrMore([NotNull] IReadOnlyList<T> values, bool dontCopySourceUseRef)
  {
    this._multipleValues = dontCopySourceUseRef ? values : (IReadOnlyList<T>) values.ToArray<T>();
    switch (this._multipleValues.Count)
    {
      case 0:
        throw new ArgumentCollectionIsEmptyException(nameof (values));
      case 1:
        this._singleValue = this._multipleValues[0];
        this._multipleValues = (IReadOnlyList<T>) null;
        break;
      default:
        this._singleValue = default (T);
        break;
    }
    this._hasValue = true;
  }

  public override int GetHashCode()
  {
    if (!this._hasValue)
      return 0;
    if (this._multipleValues != null)
      return this._multipleValues.GetHashCode();
    T singleValue = this._singleValue;
    ref T local1 = ref singleValue;
    if ((object) default (T) == null)
    {
      T obj = local1;
      ref T local2 = ref obj;
      if ((object) obj == null)
        return 0;
      local1 = ref local2;
    }
    return local1.GetHashCode();
  }

  public override string ToString()
  {
    if (!this._hasValue)
      return string.Empty;
    string str1 = this._multipleValues?.ToString();
    if (str1 != null)
      return str1;
    T singleValue = this._singleValue;
    ref T local1 = ref singleValue;
    string str2;
    if ((object) default (T) == null)
    {
      T obj = local1;
      ref T local2 = ref obj;
      if ((object) obj == null)
      {
        str2 = (string) null;
        goto label_7;
      }
      local1 = ref local2;
    }
    str2 = local1.ToString();
label_7:
    return str2 ?? string.Empty;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator T(OneOrMore<T> oneOrMore) => oneOrMore.Value;

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator T[](OneOrMore<T> oneOrMore)
  {
    return oneOrMore.Values.ToArray<T>(oneOrMore.Count);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator OneOrMore<T>([NotNull] T value) => new OneOrMore<T>(value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator OneOrMore<T>([NotNull] T[] values)
  {
    return new OneOrMore<T>((IEnumerable<T>) values);
  }

  public bool Equals(OneOrMore<T> other)
  {
    if (this._multipleValues == null)
      return object.Equals((object) this._singleValue, (object) other._singleValue);
    if (other._multipleValues == this._multipleValues)
      return true;
    return other._multipleValues != null && this._multipleValues.Count == other._multipleValues.Count && this._multipleValues.SequenceEqual<T>((IEnumerable<T>) other._multipleValues);
  }

  public bool Equals(T other)
  {
    return this._multipleValues == null && object.Equals((object) this._singleValue, (object) other);
  }

  public bool Equals(T[] other)
  {
    if (other == null || other.Length == 0)
      return false;
    if (other.Length != 1)
    {
      if (other == this._multipleValues)
        return true;
      return this._multipleValues != null && this._multipleValues.Count == other.Length && this._multipleValues.SequenceEqual<T>((IEnumerable<T>) other);
    }
    return this._multipleValues == null && object.Equals((object) this._singleValue, (object) other[0]);
  }

  public bool Equals(IReadOnlyList<T> other)
  {
    if (other == null || other.Count <= 0)
      return false;
    if (other.Count != 1)
    {
      if (other == this._multipleValues)
        return true;
      return this._multipleValues != null && this._multipleValues.Count == other.Count && this._multipleValues.SequenceEqual<T>((IEnumerable<T>) other);
    }
    return this._multipleValues == null && object.Equals((object) this._singleValue, (object) other[0]);
  }

  public bool Equals(IList<T> other)
  {
    if (other == null || other.Count <= 0)
      return false;
    if (other.Count != 1)
    {
      if (other == this._multipleValues)
        return true;
      return this._multipleValues != null && this._multipleValues.Count == other.Count && this._multipleValues.SequenceEqual<T>((IEnumerable<T>) other);
    }
    return this._multipleValues == null && object.Equals((object) this._singleValue, (object) other[0]);
  }

  public bool Equals(IReadOnlyCollection<T> other)
  {
    if (other == null || other.Count <= 0)
      return false;
    if (other.Count != 1)
    {
      if (other == this._multipleValues)
        return true;
      return this._multipleValues != null && this._multipleValues.Count == other.Count && this._multipleValues.SequenceEqual<T>((IEnumerable<T>) other);
    }
    return this._multipleValues == null && object.Equals((object) this._singleValue, (object) other.First<T>());
  }

  public bool Equals(ICollection<T> other)
  {
    if (other == null || other.Count <= 0)
      return false;
    if (other.Count != 1)
    {
      if (other == this._multipleValues)
        return true;
      return this._multipleValues != null && this._multipleValues.Count == other.Count && this._multipleValues.SequenceEqual<T>((IEnumerable<T>) other);
    }
    return this._multipleValues == null && object.Equals((object) this._singleValue, (object) other.First<T>());
  }

  public override bool Equals(object obj)
  {
    switch (obj)
    {
      case OneOrMore<T> other1:
        return this.Equals(other1);
      case T other2:
        return this.Equals(other2);
      case T[] other3:
        return this.Equals(other3);
      case IList<T> other4:
        return this.Equals(other4);
      case IReadOnlyList<T> other5:
        return this.Equals(other5);
      case ICollection<T> other6:
        return this.Equals(other6);
      case IReadOnlyCollection<T> other7:
        return this.Equals(other7);
      default:
        return false;
    }
  }

  public static bool operator ==(OneOrMore<T> left, OneOrMore<T> right) => left.Equals(right);

  public static bool operator !=(OneOrMore<T> left, OneOrMore<T> right) => !left.Equals(right);

  public static bool operator ==(OneOrMore<T> left, [CanBeNull] T right) => left.Equals(right);

  public static bool operator !=(OneOrMore<T> left, [CanBeNull] T right) => !left.Equals(right);

  public static bool operator ==([CanBeNull] T left, OneOrMore<T> right) => right.Equals(left);

  public static bool operator !=([CanBeNull] T left, OneOrMore<T> right) => !right.Equals(left);

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public IEnumerator<T> GetEnumerator()
  {
    return !this.OneValue ? this.Values.GetEnumerator() : SingleEnumerator.FromTemplate<T>(this.Value);
  }
}
