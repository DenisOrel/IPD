// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Container`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class Container<T> : IDisposable, IEquatable<T> where T : class
{
  [CanBeNull]
  public Action<T> FinishAction { get; }

  [CanBeNull]
  public T Value { get; }

  public Container([CanBeNull] T value, [CanBeNull] Action<T> finishAction)
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

  protected bool Equals([CanBeNull] Container<T> other)
  {
    return EqualityComparer<T>.Default.Equals(this.Value, other != null ? other.Value : default (T));
  }

  bool IEquatable<T>.Equals([CanBeNull] T other)
  {
    return EqualityComparer<T>.Default.Equals(this.Value, other);
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    if (obj.GetType() == this.GetType())
      return this.Equals((Container<T>) obj);
    return obj.GetType() == typeof (T) && this.Equals((object) (T) obj);
  }

  public override int GetHashCode()
  {
    // ISSUE: variable of a boxed type
    __Boxed<T> local = (object) this.Value;
    return local == null ? 0 : local.GetHashCode();
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator T([CanBeNull] Container<T> holder)
  {
    return holder == null ? default (T) : holder.Value;
  }

  public override string ToString() => this.Value?.ToString() ?? string.Empty;
}
