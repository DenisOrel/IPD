// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DataRowsList
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Common;
using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class DataRowsList([NotNull] DataRowCollection dataRows) : 
  WrapperBase<DataRowCollection>(dataRows),
  IEquatable<WrapperBase<DataRowCollection>>,
  IEquatable<DataRowCollection>,
  IReadOnlyList<DataRow>,
  IReadOnlyCollection<DataRow>,
  IEnumerable<DataRow>,
  IEnumerable
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<DataRow> GetEnumerator() => this.WrappedObject.Cast<DataRow>().GetEnumerator();

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) this.WrappedObject.Cast<DataRow>().GetEnumerator();
  }

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject.Count;
  }

  [NotNull]
  public DataRow this[int index]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.WrappedObject[index];
  }

  [NotNull]
  [ItemNotNull]
  public static implicit operator DataRowsList([NotNull] DataRowCollection dataRowCollection)
  {
    return new DataRowsList(dataRowCollection);
  }

  [NotNull]
  [ItemNotNull]
  public static implicit operator DataRowsList([NotNull] DataTable dataTable)
  {
    return new DataRowsList(dataTable.Rows);
  }
}
