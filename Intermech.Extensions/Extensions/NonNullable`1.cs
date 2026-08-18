// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.NonNullable`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

[method: DebuggerHidden]
[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct NonNullable<T>([NotNull] T value) : 
  IDisposable,
  IEquatable<NonNullable<T>>,
  IEquatable<T>,
  IComparable<NonNullable<T>>,
  IComparable<T>
  where T : class
{
  [NotNull]
  private readonly T _obj = value;

  public void Dispose()
  {
    if (!(this._obj is IDisposable disposable))
      return;
    disposable.Dispose();
  }

  [NotNull]
  [DebuggerHidden]
  public T Value
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._obj;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator NonNullable<T>([NotNull] T value) => new NonNullable<T>(value);

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator T(NonNullable<T> nonNullable) => nonNullable.Value;

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override int GetHashCode() => this._obj.GetHashCode();

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override string ToString() => this._obj.ToString();

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(NonNullable<T> other) => this.Equals(other.Value);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] T other)
  {
    if ((object) other == null)
      return false;
    return (object) this._obj == (object) other || this._obj.Equals((object) other);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override bool Equals([CanBeNull] object obj)
  {
    T obj1;
    switch (obj)
    {
      case null:
        return false;
      case NonNullable<T> nonNullable:
        obj1 = nonNullable.Value;
        break;
      case T obj2:
        obj1 = obj2;
        break;
      default:
        return false;
    }
    return (object) this._obj == (object) obj1 || this._obj.Equals((object) obj1);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(NonNullable<T> left, NonNullable<T> right) => left.Equals(right);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(NonNullable<T> left, [NotNull] T right) => left.Equals(right);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==([NotNull] T left, NonNullable<T> right) => right.Equals(left);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(NonNullable<T> left, NonNullable<T> right) => !left.Equals(right);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(NonNullable<T> left, [NotNull] T right) => !left.Equals(right);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=([NotNull] T left, NonNullable<T> right) => !right.Equals(left);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int CompareTo(NonNullable<T> other)
  {
    return !(this._obj is IComparable<T> comparable) ? 0 : comparable.CompareTo(other.Value);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int CompareTo([CanBeNull] T other)
  {
    return (object) other == null || !(this._obj is IComparable<T> comparable) ? 0 : comparable.CompareTo(other);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(NonNullable<T> left, NonNullable<T> right)
  {
    return left.CompareTo(right) > 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(NonNullable<T> left, NonNullable<T> right)
  {
    return left.CompareTo(right) < 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(NonNullable<T> left, NonNullable<T> right)
  {
    return left.CompareTo(right) >= 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(NonNullable<T> left, NonNullable<T> right)
  {
    return left.CompareTo(right) <= 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >(NonNullable<T> left, [NotNull] T right)
  {
    return left.CompareTo(right) > 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <(NonNullable<T> left, [NotNull] T right)
  {
    return left.CompareTo(right) < 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=(NonNullable<T> left, [NotNull] T right)
  {
    return left.CompareTo(right) >= 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=(NonNullable<T> left, [NotNull] T right)
  {
    return left.CompareTo(right) <= 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >([NotNull] T left, NonNullable<T> right)
  {
    return right.CompareTo(left) <= 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <([NotNull] T left, NonNullable<T> right)
  {
    return right.CompareTo(left) >= 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator >=([NotNull] T left, NonNullable<T> right)
  {
    return right.CompareTo(left) < 0;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator <=([NotNull] T left, NonNullable<T> right)
  {
    return right.CompareTo(left) > 0;
  }
}
