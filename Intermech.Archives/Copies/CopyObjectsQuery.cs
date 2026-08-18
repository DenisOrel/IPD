// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopyObjectsQuery
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// 
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="support"></param>
/// <param name="objTypeID"></param>
/// <param name="conditions"></param>
/// <param name="services"></param>
public class CopyObjectsQuery(
  INodeQuerySupport support,
  int objTypeID,
  ConditionStructure[] conditions,
  IServiceProvider services) : ObjectsQuery(support, objTypeID, conditions, services)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="bookmark"></param>
  /// <param name="count"></param>
  /// <param name="mapping"></param>
  /// <returns></returns>
  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(bookmark, count, mapping) with
    {
      Contents = new ColumnContents[mapping.Fields.Length]
    };
    List<object> objectList1 = new List<object>((IEnumerable<object>) queryParams.Columns);
    if (objectList1.Contains((object) ConstsHolder.AlbumSubscriberID))
      queryParams.Contents[objectList1.IndexOf((object) ConstsHolder.AlbumSubscriberID)] = ColumnContents.ID;
    if (mapping.SortFields != null && mapping.SortFields.Length != 0 && queryParams.SortColumns != null)
    {
      List<object> objectList2 = new List<object>((IEnumerable<object>) queryParams.SortColumns);
      if (objectList2.Contains((object) ConstsHolder.AlbumSubscriberID))
      {
        if (queryParams.SortContents == null)
          queryParams.SortContents = new ColumnContents[mapping.SortFields.Length];
        queryParams.SortContents[objectList2.IndexOf((object) ConstsHolder.AlbumSubscriberID)] = ColumnContents.ID;
      }
    }
    return queryParams;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="recordIds"></param>
  /// <param name="mapping"></param>
  /// <returns></returns>
  protected override DBRecordSetParams GetQueryParams(object[] recordIds, RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(recordIds, mapping) with
    {
      Contents = new ColumnContents[mapping.Fields.Length]
    };
    List<object> objectList1 = new List<object>((IEnumerable<object>) queryParams.Columns);
    if (objectList1.Contains((object) ConstsHolder.AlbumSubscriberID))
      queryParams.Contents[objectList1.IndexOf((object) ConstsHolder.AlbumSubscriberID)] = ColumnContents.ID;
    if (mapping.SortFields != null && mapping.SortFields.Length != 0 && queryParams.SortColumns != null)
    {
      List<object> objectList2 = new List<object>((IEnumerable<object>) queryParams.SortColumns);
      if (objectList2.Contains((object) ConstsHolder.AlbumSubscriberID))
      {
        if (queryParams.SortContents == null)
          queryParams.SortContents = new ColumnContents[mapping.SortFields.Length];
        queryParams.SortContents[objectList2.IndexOf((object) ConstsHolder.AlbumSubscriberID)] = ColumnContents.ID;
      }
    }
    return queryParams;
  }
}
