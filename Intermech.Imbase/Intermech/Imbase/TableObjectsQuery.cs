// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableObjectsQuery
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using System;

#nullable disable
namespace Intermech.Imbase;

internal class TableObjectsQuery : ObjectsQuery
{
  private TableReferenceNode _node;

  public TableObjectsQuery(
    INodeQuerySupport support,
    int objTypeID,
    ConditionStructure[] conditions)
    : base(support, objTypeID, conditions, (IServiceProvider) null)
  {
    if (!(support is TableObjectsPart tableObjectsPart))
      return;
    this._node = tableObjectsPart._node;
  }

  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(bookmark, count, mapping);
    if (this._node != null)
      this._node.OnBeforeSelect(ref queryParams);
    return queryParams;
  }

  protected override DBRecordSetParams GetQueryParams(object[] recordIds, RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(recordIds, mapping);
    if (this._node != null)
      this._node.OnBeforeSelect(ref queryParams);
    return queryParams;
  }
}
