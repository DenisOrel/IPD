// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ValueContainer`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class ValueContainer<T> : IDisposable, IEquatable<T> where T : struct
{
  [CanBeNull]
  public Action<T> FinishAction { get; }

  public T Value { get; }

  public ValueContainer(T value, [CanBeNull] Action<T> finishAction)
  {
    this.Value = value;
    this.FinishAction = finishAction;
  }

  public void Dispose()
  {
    Action<T> finishAction = this.FinishAction;
    if (finishAction == null)
      return;
    finishAction(this.Value);
  }

  protected bool Equals([CanBeNull] ValueContainer<T> other)
  {
    return this.Value.Equals((object) other?.Value);
  }

  bool IEquatable<T>.Equals(T other) => EqualityComparer<T>.Default.Equals(this.Value, other);

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    if (obj.GetType() == this.GetType())
      return this.Equals((ValueContainer<T>) obj);
    return obj.GetType() == typeof (T) && this.Equals((object) (T) obj);
  }

  public override int GetHashCode() => this.Value.GetHashCode();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator T([CanBeNull] ValueContainer<T> holder)
  {
    return holder == null ? default (T) : holder.Value;
  }

  public override string ToString() => this.Value.ToString();
}
