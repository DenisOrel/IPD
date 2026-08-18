// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ListAdapterBase`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[DebuggerDisplay("Count = {Count}")]
[Serializable]
public abstract class ListAdapterBase<T>([NotNull] IList<T> list) : 
  WrapperBase<IList<T>>(list),
  IList<T>,
  ICollection<T>,
  IEnumerable<T>,
  IEnumerable,
  IReadOnlyList<T>,
  IReadOnlyCollection<T>,
  IEquatable<IList<T>>,
  IEquatable<IReadOnlyList<T>>,
  IEquatable<IList>,
  ISerializable
{
  private const string SerializeArrayName = "AsArray";

  [NotNull]
  protected IList<T> List
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject;
  }

  protected ListAdapterBase([NotNull] SerializationInfo info, StreamingContext context)
    : this((IList<T>) ((T[]) info.GetValue("AsArray", typeof (T[])) ?? throw new KeyNotFoundException("AsArray")))
  {
  }

  protected override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this.List.AsArray<T>());
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add([CanBeNull] T item) => this.List.Add(item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Clear() => this.List.Clear();

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Contains([CanBeNull] T item) => this.List.Contains(item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo([NotNull] T[] array, int arrayIndex) => this.List.CopyTo(array, arrayIndex);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove([CanBeNull] T item) => this.List.Remove(item);

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.List.Count;
  }

  public bool IsReadOnly
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.List.IsReadOnly;
    }
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<T> GetEnumerator() => this.List.GetEnumerator();

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.List.GetEnumerator();

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int IndexOf([CanBeNull] T item) => this.List.IndexOf(item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Insert(int index, [CanBeNull] T item) => this.List.Insert(index, item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void RemoveAt(int index) => this.List.RemoveAt(index);

  [CanBeNull]
  public T this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.List[index];
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.List[index] = value;
    }
  }

  public override int GetHashCode() => base.GetHashCode();

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj || this.WrappedObject == obj)
      return true;
    switch (obj)
    {
      case IList<T> other1:
        return this.Equals(other1);
      case IReadOnlyList<T> other2:
        return this.Equals(other2);
      case IList other3:
        return this.Equals(other3);
      default:
        return base.Equals(obj);
    }
  }

  public bool Equals([CanBeNull] IReadOnlyList<T> other)
  {
    if (other == null)
      return false;
    if (this == other || this.WrappedObject == other)
      return true;
    return this.WrappedObject is IReadOnlyList<T> wrappedObject && other.Equals((object) wrappedObject);
  }

  public bool Equals([CanBeNull] IList other)
  {
    if (other == null)
      return false;
    if (this == other || this.WrappedObject == other)
      return true;
    return this.WrappedObject is IList wrappedObject && other.Equals((object) wrappedObject);
  }
}
