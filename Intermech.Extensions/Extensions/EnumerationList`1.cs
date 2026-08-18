// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EnumerationList`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Extensions;

public class EnumerationList<T> : 
  IDisposable,
  IEnumerable<T>,
  IEnumerable,
  IReadOnlyCollection<T>,
  ICollection,
  IReadOnlyList<T>,
  ICollection<T>,
  IList<T>
{
  [NotNull]
  private IEnumerable<T> _enumeration;
  private int _count;
  [CanBeNull]
  private IList<T> _list;

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public EnumerationList([NotNull, NoEnumeration] IEnumerable<T> enumeration)
  {
    this._enumeration = enumeration;
    this._count = -1;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public EnumerationList([NotNull, NoEnumeration] IEnumerable<T> enumeration, int count)
  {
    this._enumeration = enumeration;
    this._count = count;
  }

  public void Dispose()
  {
    IEnumerable<T> notNullRef = Interlocked.Exchange<IEnumerable<T>>(ref this._enumeration, (IEnumerable<T>) null);
    Intermech.Diagnostics.Check.NotDisposed((object) notNullRef);
    IList<T> objList = Interlocked.Exchange<IList<T>>(ref this._list, (IList<T>) null);
    if (objList != null && objList != notNullRef && objList is IDisposable disposable1)
      disposable1.Dispose();
    if (!(notNullRef is IDisposable disposable2))
      return;
    disposable2.Dispose();
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._enumeration.GetEnumerator();

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<T> GetEnumerator() => this._enumeration.GetEnumerator();

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo([NotNull] Array array, int index)
  {
    if (this._enumeration is ICollection enumeration)
      enumeration.CopyTo(array, index);
    else
      ((ICollection) this.AsList()).CopyTo(array, index);
  }

  public int Count
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      IList<T> list = this._list;
      if (list != null)
        return list.Count;
      int? count = this._enumeration.TryGetCount<T>();
      if (count.HasValue)
        return count.GetValueOrDefault();
      return this._count == -1 ? this.List.Count : this._count;
    }
  }

  [NotNull]
  public IList<T> List
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._list == null)
      {
        this._list = this._enumeration as IList<T>;
        if (this._list == null)
        {
          int? countOrCapacity = this._enumeration.TryGetCountOrCapacity<T>();
          this._list = countOrCapacity.HasValue ? (IList<T>) new System.Collections.Generic.List<T>(countOrCapacity.Value) : (IList<T>) new System.Collections.Generic.List<T>();
          this._list.AddRange<T>(this._enumeration);
          this._enumeration = (IEnumerable<T>) this._list;
        }
        this._count = -1;
      }
      return this._list;
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public System.Collections.Generic.List<T> AsList()
  {
    IList<T> list = this.List;
    if (list is System.Collections.Generic.List<T> objList1)
      return objList1;
    System.Collections.Generic.List<T> objList2 = new System.Collections.Generic.List<T>(list.Count);
    objList2.AddRange(this._enumeration);
    this._list = (IList<T>) objList2;
    this._enumeration = (IEnumerable<T>) this._list;
    this._count = -1;
    return objList2;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator System.Collections.Generic.List<T>(
    [NotNull] EnumerationList<T> enumerationList)
  {
    return enumerationList.AsList();
  }

  [CanBeNull]
  public T this[int index]
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.List[index];
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.List[index] = value;
    }
  }

  public bool IsReadOnly
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => false;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add([CanBeNull] T item) => this.List.Add(item);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Clear() => this.List.Clear();

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Contains([CanBeNull] T item) => this.List.Contains(item);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo([NotNull] T[] array, int arrayIndex) => this.List.CopyTo(array, arrayIndex);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove([CanBeNull] T item) => this.List.Remove(item);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int IndexOf([CanBeNull] T item) => this.List.IndexOf(item);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Insert(int index, [CanBeNull] T item) => this.List.Insert(index, item);

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void RemoveAt(int index) => this.List.RemoveAt(index);

  public object SyncRoot
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (object) this._enumeration;
    }
  }

  public bool IsSynchronized
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._list == null ? this._enumeration is ICollection enumeration && enumeration.IsSynchronized : this._list is ICollection list && list.IsSynchronized;
    }
  }
}
