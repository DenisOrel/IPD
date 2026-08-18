// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyListAdapterBase`1
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
public abstract class ReadOnlyListAdapterBase<T> : 
  WrapperBase<IReadOnlyList<T>>,
  IReadOnlyList<T>,
  IReadOnlyCollection<T>,
  IEnumerable<T>,
  IEnumerable,
  IEquatable<IReadOnlyList<T>>,
  IEquatable<IList<T>>,
  IEquatable<IList>,
  ISerializable
{
  private const string SerializeArrayName = "AsArray";

  [NotNull]
  protected IReadOnlyList<T> List
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject;
  }

  protected ReadOnlyListAdapterBase([NotNull] IReadOnlyList<T> list)
    : base(list)
  {
  }

  protected ReadOnlyListAdapterBase([NotNull] SerializationInfo info, StreamingContext context)
    : base((IReadOnlyList<T>) ListFactory.Create<T>((T[]) info.GetValue("AsArray", typeof (T[]))))
  {
  }

  protected override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this.List.AsArray<T>());
  }

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.List.Count;
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<T> GetEnumerator() => this.List.GetEnumerator();

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.List.GetEnumerator();

  [CanBeNull]
  public T this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.List[index];
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
      case IReadOnlyList<T> other1:
        return this.Equals(other1);
      case IList<T> other2:
        return this.Equals(other2);
      case IList other3:
        return this.Equals(other3);
      default:
        return base.Equals(obj);
    }
  }

  public bool Equals([CanBeNull] IList<T> other)
  {
    if (other == null)
      return false;
    if (this == other || this.WrappedObject == other)
      return true;
    return this.WrappedObject is IList<T> wrappedObject && other.Equals((object) wrappedObject);
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
