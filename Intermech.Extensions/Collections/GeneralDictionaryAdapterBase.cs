// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.GeneralDictionaryAdapterBase
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
public abstract class GeneralDictionaryAdapterBase([NotNull] IDictionary dictionary) : 
  WrapperBase<IDictionary>(dictionary),
  IDictionary,
  ICollection,
  IEnumerable
{
  protected const string SerializeArrayName = "AsArray";

  [NotNull]
  protected IDictionary Dictionary
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject;
  }

  public int Count
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.Count;
    }
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Clear() => this.WrappedObject.Clear();

  public bool IsReadOnly
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.IsReadOnly;
    }
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.WrappedObject.GetEnumerator();

  void ICollection.CopyTo(Array array, int index) => this.WrappedObject.CopyTo(array, index);

  bool ICollection.IsSynchronized => this.WrappedObject.IsSynchronized;

  object ICollection.SyncRoot => this.WrappedObject.SyncRoot;

  void IDictionary.Remove([NotNull] object key) => this.WrappedObject.Remove(key);

  [CanBeNull]
  object IDictionary.this[[NotNull] object key]
  {
    get => this.WrappedObject[key];
    set => this.WrappedObject[key] = value;
  }

  [NotNull]
  [ItemNotNull]
  ICollection IDictionary.Keys => this.WrappedObject.Keys;

  [NotNull]
  [ItemCanBeNull]
  ICollection IDictionary.Values => this.WrappedObject.Values;

  bool IDictionary.Contains([NotNull] object key) => this.WrappedObject.Contains(key);

  void IDictionary.Add([NotNull] object key, [CanBeNull] object value)
  {
    this.WrappedObject.Add(key, value);
  }

  IDictionaryEnumerator IDictionary.GetEnumerator() => this.WrappedObject.GetEnumerator();

  bool IDictionary.IsFixedSize => this.WrappedObject.IsFixedSize;
}
