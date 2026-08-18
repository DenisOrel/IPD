// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.GeneralListAdapterBase
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Common;
using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Collections;

[DebuggerDisplay("Count = {Count}")]
public abstract class GeneralListAdapterBase([NotNull] IList list) : 
  WrapperBase<IList>(list),
  IList,
  ICollection,
  IEnumerable
{
  protected const string SerializeArrayName = "AsArray";

  [NotNull]
  protected IList List
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject;
  }

  IEnumerator IEnumerable.GetEnumerator() => this.WrappedObject.GetEnumerator();

  void ICollection.CopyTo([NotNull] Array array, int index)
  {
    this.WrappedObject.CopyTo(array, index);
  }

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.Count;
    }
  }

  object ICollection.SyncRoot
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.SyncRoot;
    }
  }

  bool ICollection.IsSynchronized
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.IsSynchronized;
    }
  }

  int IList.Add([CanBeNull] object value) => this.WrappedObject.Add(value);

  bool IList.Contains([CanBeNull] object value) => this.WrappedObject.Contains(value);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Clear() => this.WrappedObject.Clear();

  int IList.IndexOf([CanBeNull] object value) => this.WrappedObject.IndexOf(value);

  void IList.Insert(int index, [CanBeNull] object value) => this.WrappedObject.Insert(index, value);

  void IList.Remove([CanBeNull] object value) => this.WrappedObject.Remove(value);

  void IList.RemoveAt(int index) => this.WrappedObject.RemoveAt(index);

  [CanBeNull]
  object IList.this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject[index];
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.WrappedObject[index] = value;
    }
  }

  public bool IsReadOnly
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.IsReadOnly;
    }
  }

  bool IList.IsFixedSize
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.IsFixedSize;
    }
  }
}
