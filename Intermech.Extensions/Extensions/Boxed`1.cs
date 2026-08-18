// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Boxed`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

[Serializable]
public sealed class Boxed<T> : IEquatable<Boxed<T>>, IEquatable<T>, IDisposable where T : struct
{
  private readonly T _value;

  internal Boxed(T value) => this._value = value;

  public void Dispose()
  {
    if (!(this._value is IDisposable disposable))
      return;
    disposable.Dispose();
  }

  public override bool Equals(object other)
  {
    if (other == null)
      return false;
    switch (other)
    {
      case T obj:
        return this._value.Equals((object) obj);
      case Boxed<T> boxed:
        return this._value.Equals((object) boxed._value);
      default:
        return false;
    }
  }

  public bool Equals(T other) => this._value.Equals((object) other);

  public bool Equals([CanBeNull] Boxed<T> other)
  {
    return other != null && this._value.Equals((object) other._value);
  }

  public override int GetHashCode() => this._value.GetHashCode();

  public override string ToString() => this._value.ToString();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator T([NotNull] Boxed<T> boxed) => boxed._value;

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static explicit operator Boxed<T>(T value) => new Boxed<T>(value);
}
