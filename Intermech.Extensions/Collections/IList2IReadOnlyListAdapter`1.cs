// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.IList2IReadOnlyListAdapter`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
public sealed class IList2IReadOnlyListAdapter<T> : 
  IReadOnlyList<T>,
  IReadOnlyCollection<T>,
  IEnumerable<T>,
  IEnumerable,
  IEquatable<IList2IReadOnlyListAdapter<T>>,
  IEquatable<IList<T>>,
  IEquatable<IList>,
  ISerializable,
  ICapacity
{
  internal const string SerializeArrayName = "AsArray";
  [NotNull]
  private readonly IList<T> _list;

  public IList2IReadOnlyListAdapter([NotNull] IList<T> list) => this._list = list;

  private IList2IReadOnlyListAdapter([NotNull] SerializationInfo info, StreamingContext context)
  {
    this._list = (IList<T>) new SerializableList<T>((IEnumerable<T>) (T[]) info.GetValue("AsArray", typeof (T[])));
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this._list.ToArray<T>(this._list.Count));
  }

  public IEnumerator<T> GetEnumerator() => this._list.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => this._list.GetEnumerator();

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._list.Count;
  }

  [CanBeNull]
  public T this[int index]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._list[index];
  }

  public override int GetHashCode() => this._list.GetHashCode();

  public override string ToString() => this._list.ToString();

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    return this == obj || this._list == obj || obj is IList2IReadOnlyListAdapter<T> other1 && this.Equals(other1) || obj is IList<T> other2 && this.Equals(other2) || obj is IList other3 && this.Equals(other3) || obj.Equals((object) this._list);
  }

  public bool Equals([CanBeNull] IList2IReadOnlyListAdapter<T> other)
  {
    return other != null && this != other && this._list != other && other.Equals(this._list);
  }

  public bool Equals([CanBeNull] IList<T> other)
  {
    return other != null && this != other && this._list != other && other.Equals((object) this._list);
  }

  public bool Equals([CanBeNull] IList other)
  {
    return other != null && this != other && this._list != other && this._list is IList list && other.Equals((object) list);
  }

  public int Capacity
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._list.GetRecommendedCapacity<T>();
    }
  }
}
