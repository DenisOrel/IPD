// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyCollectionAdapterBase`1
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
public abstract class ReadOnlyCollectionAdapterBase<T> : 
  WrapperBase<IReadOnlyCollection<T>>,
  IReadOnlyCollection<T>,
  IEnumerable<T>,
  IEnumerable,
  IEquatable<IReadOnlyCollection<T>>,
  IEquatable<ICollection<T>>,
  IEquatable<ICollection>,
  ISerializable,
  ICapacity
{
  private const string SerializeArrayName = "AsArray";

  [NotNull]
  protected IReadOnlyCollection<T> Collection
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject;
  }

  protected ReadOnlyCollectionAdapterBase([NotNull] IReadOnlyCollection<T> collection)
    : base(collection)
  {
  }

  protected ReadOnlyCollectionAdapterBase([NotNull] SerializationInfo info, StreamingContext context)
    : base((IReadOnlyCollection<T>) ListFactory.Create<T>((T[]) info.GetValue("AsArray", typeof (T[]))))
  {
  }

  protected override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this.Collection.AsArray<T>());
  }

  public int Capacity
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Collection.Count;
    }
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<T> GetEnumerator() => this.Collection.GetEnumerator();

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.Collection.GetEnumerator();

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Collection.Count;
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
      case IReadOnlyCollection<T> other1:
        return this.Equals(other1);
      case ICollection<T> other2:
        return this.Equals(other2);
      case ICollection other3:
        return this.Equals(other3);
      default:
        return base.Equals(obj);
    }
  }

  public bool Equals([CanBeNull] ICollection<T> other)
  {
    if (other == null)
      return false;
    if (this == other || this.WrappedObject == other)
      return true;
    return this.WrappedObject is ICollection<T> wrappedObject && other.Equals((object) wrappedObject);
  }

  public bool Equals([CanBeNull] ICollection other)
  {
    if (other == null)
      return false;
    if (this == other || this.WrappedObject == other)
      return true;
    return this.WrappedObject is ICollection wrappedObject && other.Equals((object) wrappedObject);
  }
}
