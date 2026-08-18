
// Type: Intermech.Navigator.Snapshots.RelatedObjectsSavedInSnapshotCompositeQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Snapshots;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Navigator.Snapshots;

/// <summary>Составная Query выступающая единым источником данных для всех Query частей сохранённого в итерации состава ноды объекта</summary>
public class RelatedObjectsSavedInSnapshotCompositeQuery : 
  AdvCompositeQuery,
  INodeQuery,
  IDataTableSource
{
  /// <summary>Интерфейс итерации</summary>
  [NotNull]
  private readonly ISnapshot _snapshot;
  /// <summary>Идентификатор версии объекта, чей состав зачитывается</summary>
  [NotEmpty]
  private readonly long _projID;
  /// <summary>Таблица данных</summary>
  [NotNull]
  private readonly Lazy<DataTable> _lazyDataTable;

  /// <summary>Constructor</summary>
  public RelatedObjectsSavedInSnapshotCompositeQuery(
    [NotNull, ItemNotNull] List<QuerySlot> subQueries,
    [NotNull] ISnapshot snapshot,
    [NotEmpty] long projID,
    [CanBeNull] AdvCompositeQuery.delegateResultQueriesPostProcessing afterExecute = null)
    : base(subQueries, afterExecute)
  {
    this._snapshot = snapshot;
    this._projID = projID;
    this._lazyDataTable = new Lazy<DataTable>(new Func<DataTable>(this.CreateDataTable));
  }

  [NotNull]
  private DataTable CreateDataTable()
  {
    return this._snapshot.Invoke<DataTable>((ServerEntityHandler<IDBObjectSnapshot, DataTable>) (snapshot => snapshot.ConsistFromSnapshotObjects(Math.Abs(this._projID))));
  }

  /// <summary>Получить таблицу данных</summary>
  public DataTable DataTable
  {
    [DebuggerStepThrough] get => this._lazyDataTable.Value;
  }
}
