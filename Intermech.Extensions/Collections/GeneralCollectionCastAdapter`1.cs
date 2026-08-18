// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.GeneralCollectionCastAdapter`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
internal sealed class GeneralCollectionCastAdapter<TMapped>([NotNull] ICollection collection) : 
  GeneralCollectionAdapterBase(collection),
  ICollection,
  IEnumerable,
  ICollection<TMapped>,
  IEnumerable<TMapped>,
  IReadOnlyCollection<TMapped>,
  IEquatable<ICollection>,
  ISerializable
{
  private GeneralCollectionCastAdapter([NotNull] SerializationInfo info, StreamingContext context)
    : this((ICollection) ((TMapped[]) info.GetValue("AsArray", typeof (TMapped[])) ?? throw new KeyNotFoundException("AsArray")))
  {
  }

  protected override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this.WrappedObject.Cast<TMapped>().AsArray<TMapped>(this.WrappedObject.Count));
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.WrappedObject.Cast<TMapped>().GetEnumerator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo([NotNull] TMapped[] array, int arrayIndex)
  {
    this.WrappedObject.CopyTo((Array) array, arrayIndex);
  }

  void ICollection<TMapped>.Add([CanBeNull] TMapped item)
  {
    switch (this.WrappedObject)
    {
      case IList list:
        list.Add((object) item);
        break;
      case ICollection<TMapped> mappeds:
        mappeds.Add(item);
        break;
      default:
        throw new NotSupportedException($"ICollection<{typeof (TMapped)}>.Add({typeof (TMapped)} item) for {this.WrappedObject.GetType()} wrapped by {typeof (GeneralCollectionCastAdapter<TMapped>)}");
    }
  }

  void ICollection<TMapped>.Clear()
  {
    switch (this.WrappedObject)
    {
      case IList list:
        list.Clear();
        break;
      case ICollection<TMapped> mappeds:
        mappeds.Clear();
        break;
      default:
        throw new NotSupportedException($"ICollection<{typeof (TMapped)}>.Clear() for {this.WrappedObject.GetType()} wrapped by {typeof (GeneralCollectionCastAdapter<TMapped>)}");
    }
  }

  bool ICollection<TMapped>.Contains([CanBeNull] TMapped item)
  {
    return this.WrappedObject.Cast<TMapped>().Contains<TMapped>(item);
  }

  bool ICollection<TMapped>.Remove([CanBeNull] TMapped item)
  {
    switch (this.WrappedObject)
    {
      case IList list:
        bool flag = list.Contains((object) item);
        list.Remove((object) item);
        return flag;
      case ICollection<TMapped> mappeds:
        return mappeds.Remove(item);
      default:
        throw new NotSupportedException($"ICollection<{typeof (TMapped)}>.Remove({typeof (TMapped)} item) for {this.WrappedObject.GetType()} wrapped by {typeof (GeneralCollectionCastAdapter<TMapped>)}");
    }
  }

  bool ICollection<TMapped>.IsReadOnly
  {
    get
    {
      switch (this.WrappedObject)
      {
        case IList list:
          return list.IsReadOnly;
        case ICollection<TMapped> mappeds:
          return mappeds.IsReadOnly;
        default:
          return true;
      }
    }
  }
}
