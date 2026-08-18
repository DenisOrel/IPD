// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyList`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

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

[Serializable]
public class ReadOnlyList<T> : 
  IReadOnlyList<T>,
  IReadOnlyCollection<T>,
  IEnumerable<T>,
  IEnumerable,
  IEquatable<ReadOnlyList<T>>,
  IEquatable<IReadOnlyList<T>>,
  IEquatable<IList>,
  ISerializable,
  IDeserializationCallback,
  ICapacity
{
  internal const string SerializeArrayName = "AsArray";
  [NotNull]
  private readonly IReadOnlyList<T> _list;

  public ReadOnlyList()
    : this(0)
  {
  }

  public ReadOnlyList(int capacity, [CanBeNull] IEnumerable<T> enumeration = null)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    this._list = enumeration is ReadOnlyList<T> readOnlyList ? readOnlyList._list : (IReadOnlyList<T>) ListFactory.Create<T>(enumeration, capacity);
  }

  public ReadOnlyList([CanBeNull] IEnumerable<T> enumeration, int capacity = 16 /*0x10*/)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    this._list = enumeration is ReadOnlyList<T> readOnlyList ? readOnlyList._list : (IReadOnlyList<T>) ListFactory.Create<T>(enumeration, capacity);
  }

  protected ReadOnlyList([NotNull] IReadOnlyList<T> list) => this._list = list;

  protected ReadOnlyList([NotNull] IList<T> list) => this._list = list.WrapAsReadOnly<T>();

  protected ReadOnlyList([NotNull] SerializationInfo info, StreamingContext context)
  {
    this._list = (IReadOnlyList<T>) ListFactory.Create<T>(info.GetValue<T[]>("AsArray"));
    this.OnDeserialization((object) this);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this._list.AsArray<T>());
  }

  public virtual void OnDeserialization([CanBeNull] object sender)
  {
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<T> GetEnumerator() => this._list.GetEnumerator();

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => this._list.GetEnumerator();

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._list.Count;
  }

  [CanBeNull]
  public T this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._list[index];
    }
  }

  [DebuggerStepThrough]
  [CanBeNull]
  [ContractAnnotation("null => null; notnull => notnull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator List<T>([CanBeNull] ReadOnlyList<T> readOnlyList)
  {
    return readOnlyList == null ? (List<T>) null : readOnlyList.AsList<T>();
  }

  [DebuggerStepThrough]
  [CanBeNull]
  [ContractAnnotation("null => null; notnull => notnull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator ReadOnlyList<T>([CanBeNull] List<T> list)
  {
    return list == null ? (ReadOnlyList<T>) null : new ReadOnlyList<T>((IReadOnlyList<T>) list);
  }

  public int Capacity
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._list.GetRecommendedCapacity<T>();
    }
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    return this == obj || this._list == obj || obj is ReadOnlyList<T> other1 && this.Equals(other1) || obj is IReadOnlyList<T> other2 && this.Equals(other2) || obj is IList other3 && this.Equals(other3) || this._list.Equals(obj);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] ReadOnlyList<T> other)
  {
    if (other == null)
      return false;
    return this == other || this._list == other._list || this._list.Equals((object) other._list);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] IReadOnlyList<T> other)
  {
    if (other == null)
      return false;
    return this == other || this._list == other || other.Equals((object) this._list);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] IList other)
  {
    if (other == null)
      return false;
    if (this == other || this._list == other)
      return true;
    return this._list is IList list && other.Equals((object) list);
  }

  public override int GetHashCode() => this._list.GetHashCode();

  public override string ToString() => this._list.ToString();
}
