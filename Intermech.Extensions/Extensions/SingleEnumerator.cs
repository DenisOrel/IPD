// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SingleEnumerator
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

public static class SingleEnumerator
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerator<T> FromObject<T>([CanBeNull] T value) where T : class
  {
    return (IEnumerator<T>) new SingleEnumerator.ObjectSingleEnumerator<T>(value);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerator<T> FromStruct<T>(T value) where T : struct
  {
    return (IEnumerator<T>) new SingleEnumerator.ValueSingleEnumerator<T>(value);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerator<T> FromTemplate<T>([CanBeNull] T value)
  {
    return (IEnumerator<T>) new SingleEnumerator.TemplateEnumerator<T>(value);
  }

  private sealed class ObjectSingleEnumerator<T> : 
    IEnumerator<T>,
    IDisposable,
    IEnumerator,
    IEquatable<T>,
    IEquatable<SingleEnumerator.ObjectSingleEnumerator<T>>
    where T : class
  {
    [CanBeNull]
    private readonly T _value;
    private bool _moved;

    public ObjectSingleEnumerator([CanBeNull] T value)
    {
      this._value = value is SingleEnumerator.ObjectSingleEnumerator<T> singleEnumerator ? singleEnumerator._value : value;
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      int num = !this._moved ? 1 : 0;
      this._moved = true;
      return num != 0;
    }

    public void Reset() => this._moved = false;

    public T Current => this._value;

    object IEnumerator.Current => (object) this._value;

    [CanBeNull]
    public static implicit operator T(
      [NotNull] SingleEnumerator.ObjectSingleEnumerator<T> enumerator)
    {
      return enumerator._value;
    }

    public override string ToString() => this._value?.ToString() ?? "NULL";

    public override int GetHashCode()
    {
      // ISSUE: variable of a boxed type
      __Boxed<T> local = (object) this._value;
      return local == null ? 0 : local.GetHashCode();
    }

    public bool Equals([CanBeNull] T other)
    {
      return (object) other != null && object.Equals((object) this._value, (object) other);
    }

    public bool Equals([CanBeNull] SingleEnumerator.ObjectSingleEnumerator<T> other)
    {
      return other != null && object.Equals((object) this._value, (object) other._value);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      switch (obj)
      {
        case T objB:
          return object.Equals((object) this, (object) objB);
        case SingleEnumerator.ObjectSingleEnumerator<T> singleEnumerator:
          return object.Equals((object) this, (object) singleEnumerator._value);
        default:
          return false;
      }
    }
  }

  private sealed class ValueSingleEnumerator<T> : 
    IEnumerator<T>,
    IDisposable,
    IEnumerator,
    IEquatable<T>,
    IEquatable<SingleEnumerator.ValueSingleEnumerator<T>>
    where T : struct
  {
    [CanBeNull]
    private object _boxed;
    private bool _moved;

    public ValueSingleEnumerator(T value) => this.Current = value;

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      int num = !this._moved ? 1 : 0;
      this._moved = true;
      return num != 0;
    }

    public void Reset() => this._moved = false;

    public T Current { get; }

    [NotNull]
    object IEnumerator.Current => this._boxed ?? (this._boxed = (object) this.Current);

    public static implicit operator T(
      [NotNull] SingleEnumerator.ValueSingleEnumerator<T> enumerator)
    {
      return enumerator.Current;
    }

    public override string ToString() => this.Current.ToString();

    public override int GetHashCode() => this.Current.GetHashCode();

    public bool Equals(T other) => object.Equals((object) this.Current, (object) other);

    public bool Equals([CanBeNull] SingleEnumerator.ValueSingleEnumerator<T> other)
    {
      return other != null && object.Equals((object) this.Current, (object) other.Current);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      switch (obj)
      {
        case T objB:
          return object.Equals((object) this, (object) objB);
        case SingleEnumerator.ValueSingleEnumerator<T> singleEnumerator:
          return object.Equals((object) this, (object) singleEnumerator.Current);
        default:
          return false;
      }
    }
  }

  private sealed class TemplateEnumerator<T> : 
    IEnumerator<T>,
    IDisposable,
    IEnumerator,
    IEquatable<T>,
    IEquatable<SingleEnumerator.TemplateEnumerator<T>>
  {
    [CanBeNull]
    private readonly T _value;
    private bool _moved;

    public TemplateEnumerator([CanBeNull] T value)
    {
      this._value = value is SingleEnumerator.TemplateEnumerator<T> templateEnumerator ? templateEnumerator._value : value;
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      int num = !this._moved ? 1 : 0;
      this._moved = true;
      return num != 0;
    }

    public void Reset() => this._moved = false;

    public T Current => this._value;

    object IEnumerator.Current => (object) this._value;

    [CanBeNull]
    public static implicit operator T([NotNull] SingleEnumerator.TemplateEnumerator<T> enumerator)
    {
      return enumerator._value;
    }

    public override string ToString()
    {
      T obj1 = this._value;
      ref T local1 = ref obj1;
      string str;
      if ((object) default (T) == null)
      {
        T obj2 = local1;
        ref T local2 = ref obj2;
        if ((object) obj2 == null)
        {
          str = (string) null;
          goto label_4;
        }
        local1 = ref local2;
      }
      str = local1.ToString();
label_4:
      return str ?? "NULL";
    }

    public override int GetHashCode()
    {
      T obj1 = this._value;
      ref T local1 = ref obj1;
      if ((object) default (T) == null)
      {
        T obj2 = local1;
        ref T local2 = ref obj2;
        if ((object) obj2 == null)
          return 0;
        local1 = ref local2;
      }
      return local1.GetHashCode();
    }

    public bool Equals([CanBeNull] T other)
    {
      return (object) other != null && object.Equals((object) this._value, (object) other);
    }

    public bool Equals([CanBeNull] SingleEnumerator.TemplateEnumerator<T> other)
    {
      return other != null && object.Equals((object) this._value, (object) other._value);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      switch (obj)
      {
        case T objB:
          return object.Equals((object) this, (object) objB);
        case SingleEnumerator.TemplateEnumerator<T> templateEnumerator:
          return object.Equals((object) this, (object) templateEnumerator._value);
        default:
          return false;
      }
    }
  }
}
