// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.GeneralCollectionAdapterBase
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
public abstract class GeneralCollectionAdapterBase([NotNull] ICollection collection) : 
  WrapperBase<ICollection>(collection),
  ICollection,
  IEnumerable
{
  protected const string SerializeArrayName = "AsArray";

  [NotNull]
  protected ICollection Collection
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject;
  }

  IEnumerator IEnumerable.GetEnumerator() => this.WrappedObject.GetEnumerator();

  void ICollection.CopyTo(Array array, int index) => this.WrappedObject.CopyTo(array, index);

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.Count;
    }
  }

  object ICollection.SyncRoot => this.WrappedObject.SyncRoot;

  bool ICollection.IsSynchronized => this.WrappedObject.IsSynchronized;
}
