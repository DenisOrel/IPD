// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Articles4DocumentFinder`1
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

internal abstract class Articles4DocumentFinder<TResult>
{
  private readonly ArticleSrvService _articleSrvService;

  public Articles4DocumentFinder(ArticleSrvService articleSrvService)
  {
    this._articleSrvService = articleSrvService;
  }

  public List<TResult> FindArticles(
    long documentID,
    string filtrationRuleSettings,
    IUserSession session,
    bool withoutFiltration)
  {
    List<TResult> result = new List<TResult>();
    IDBRelationType relationType = session.GetRelationType(new Guid("cad00154-306c-11d8-b4e9-00304f19f545"));
    IDBRelationCollection relationCollection = session.GetRelationCollection(relationType.RelationType);
    relationCollection.FiltrationOwnerID = filtrationRuleSettings;
    DBRecordSetParams paramsSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    if (withoutFiltration)
      FiltrationHelper.BlockPluginFiltrations(ref paramsSet, (HybridDictionary) null);
    DataTable dataTable1 = relationCollection.EntersInVersion(paramsSet, documentID);
    if (dataTable1.Rows.Count == 0)
      return result;
    int attributeId = session.GetAttributeType(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), true).AttributeID;
    long int64 = Convert.ToInt64(dataTable1.Rows[0][0]);
    int int32 = Convert.ToInt32(dataTable1.Rows[0][1]);
    IDBAttribute attributeById = session.GetObject(int64).GetAttributeByID(attributeId);
    Guid articleGroupID = attributeById == null || !GuidHelper.IsGuid(attributeById.AsString) ? Guid.Empty : new Guid(attributeById.AsString);
    if (articleGroupID != Guid.Empty)
    {
      dataTable1.Dispose();
      foreach (long objectID in this._articleSrvService.FindArticlesByGroupIDAttr(articleGroupID, attributeId, int32, session, withoutFiltration))
      {
        if (objectID.Equals(int64))
          result.Add(this.GetResultItem(objectID, Convert.ToInt64(dataTable1.Rows[0][2])));
        else
          result.Add(this.GetResultItem(objectID, 0L));
      }
    }
    else
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        result.Add(this.GetResultItem(Convert.ToInt64(row[0]), Convert.ToInt64(row[2])));
      bool flag = dataTable1.Rows.Count == 0 || Convert.ToBoolean(dataTable1.ExtendedProperties[(object) "Eof"]);
      if (!flag)
      {
        paramsSet.RecordCount = -2;
        this.SetParamsLastKey(result, paramsSet);
      }
      while (!flag)
      {
        DataTable dataTable2 = relationCollection.EntersInVersion(paramsSet, documentID);
        if (dataTable2 != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
            result.Add(this.GetResultItem(Convert.ToInt64(row[0]), Convert.ToInt64(row[2])));
        }
        flag = Convert.ToBoolean(dataTable2.ExtendedProperties[(object) "Eof"]);
        if (!flag && dataTable2.Rows.Count > 0)
          this.SetParamsLastKey(result, paramsSet);
        dataTable2.Dispose();
      }
    }
    return result;
  }

  private void SetParamsLastKey(List<TResult> result, DBRecordSetParams recordSetParams)
  {
    long lastKeyValue = this.GetLastKeyValue(result[result.Count - 1]);
    recordSetParams.LastKeyValue = lastKeyValue;
    recordSetParams.LastOrderValue = (object) lastKeyValue;
  }

  protected abstract TResult GetResultItem(long objectID, long relationID);

  protected abstract long GetLastKeyValue(TResult lastItem);
}
