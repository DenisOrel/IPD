// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.DBDocumentHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Tools.Data;

public static class DBDocumentHelper
{
  public static List<long> FindArticleDocuments(
    long articleId,
    bool documentBranchOnly,
    VersionsRulePackage rule)
  {
    if (articleId == 0L)
      throw new ArgumentException();
    if (rule == null)
      throw new ArgumentNullException(nameof (rule));
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = rule.OwnerId;
      if (documentBranchOnly)
        relationCollection.ObjectTypeID = IDCache.Default.AllDocuments.Id;
      dataTable = relationCollection.ConsistFrom(paramSet, articleId);
    }
    List<long> articleDocuments = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      articleDocuments.Add(Convert.ToInt64(row[0]));
    return articleDocuments;
  }

  public static List<Tuple<Guid, long>> FindArticleDocuments(
    long articleId,
    bool documentBranchOnly,
    bool integratorAddedOnly,
    VersionsRulePackage rule)
  {
    if (articleId == 0L)
      throw new ArgumentException();
    if (rule == null)
      throw new ArgumentNullException(nameof (rule));
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_PRJ_GUID,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    if (integratorAddedOnly)
      paramSet.Conditions = new ConditionStructure[1]
      {
        new ConditionStructure(IDCache.Default.BasedOnCADModel.Id, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true)
      };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = rule.OwnerId;
      if (documentBranchOnly)
        relationCollection.ObjectTypeID = IDCache.Default.AllDocuments.Id;
      dataTable = relationCollection.ConsistFrom(paramSet, articleId);
    }
    List<Tuple<Guid, long>> articleDocuments = new List<Tuple<Guid, long>>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      articleDocuments.Add(Tuple.Create<Guid, long>(new Guid(Convert.ToString(row[0])), Convert.ToInt64(row[1])));
    return articleDocuments;
  }

  /// <summary>Находит все изделия, выпускаемые по документу.</summary>
  /// <param name="documentObjectId">Идентификатор версии документа</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="integratedOnly">Признак, что следует искать только те изделия, которые были добавлены интегратором</param>
  /// <returns>Таблица с результатами поиска. Столбцы - идентификатор связи, идентификатор версии изделия, идентификатор типа изделия</returns>
  public static DataTable FindDocumentArticles(
    long documentObjectId,
    VersionsRulePackage versionsRule,
    bool integratedOnly)
  {
    if (documentObjectId == 0L)
      throw new ArgumentException();
    if (versionsRule == null)
      throw new ArgumentNullException("versionRule");
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    if (integratedOnly)
    {
      ConditionStructure conditionStructure1 = new ConditionStructure(IDCache.Default.ObjectExternalKey.Id, RelationalOperators.NotEmpty, (object) null, LogicalOperators.OR, 1, true);
      ConditionStructure conditionStructure2 = new ConditionStructure(IDCache.Default.BasedOnCADModel.Id, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, -1, true);
      paramSet.Conditions = new ConditionStructure[2]
      {
        conditionStructure1,
        conditionStructure2
      };
    }
    paramSet.Columns = new object[4]
    {
      (object) ObligatoryObjectAttributes.F_PRJ_GUID,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) IDCache.Default.FixedRelation.Id
    };
    paramSet.ColumnsInfo = new ColumnInfo[4]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_PRJ_GUID, AttributeSourceTypes.Auto, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Auto, (object) null),
      new ColumnInfo((object) IDCache.Default.FixedRelation.Id, AttributeSourceTypes.Relation, (object) null)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = versionsRule.OwnerId;
      relationCollection.ObjectTypeID = IDCache.Default.AllArticles.Id;
      return relationCollection.EntersInVersion(paramSet, documentObjectId);
    }
  }

  public static List<long> FindDocumentDrawings(
    long modelObjectId,
    VersionsRulePackage versionsRule,
    IList<int> drawingTypes)
  {
    if (modelObjectId == 0L)
      throw new ArgumentException();
    if (versionsRule == null)
      throw new ArgumentNullException(nameof (versionsRule));
    if (drawingTypes == null)
      throw new ArgumentNullException(nameof (drawingTypes));
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.DocumentTree.Id);
      relationCollection.FiltrationOwnerID = versionsRule.OwnerId;
      dataTable = relationCollection.EntersInVersion(paramSet, modelObjectId);
    }
    List<long> documentDrawings = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row[0]);
      if (drawingTypes.Contains(int32))
        documentDrawings.Add(Convert.ToInt64(row[1]));
    }
    return documentDrawings;
  }

  public static string GetCADConfigurationFile(long articleId, long documentId)
  {
    if (articleId == 0L)
      throw new ArgumentException();
    if (documentId == 0L)
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(articleId, documentId, IDCache.Default.ArticleToDocumentTree.Id, true);
      if (relation == null)
        return string.Empty;
      IDBAttribute attributeById = relation.GetAttributeByID(IDCache.Default.CADConfigurationFile.Id);
      if (attributeById == null || attributeById.IsNull)
        return string.Empty;
      string str = (string) attributeById.Value;
      return string.IsNullOrEmpty(str) ? string.Empty : str;
    }
  }

  public static IList<long> Checkout(
    IList<long> objectList,
    DBDocumentHelper.CheckoutErrorHandler errorHandler)
  {
    if (objectList == null)
      throw new ArgumentNullException();
    IInvokeService service1 = ServiceUtils.GetService<IInvokeService>((object) ServicesManager.ServiceContainer, true);
    IList<long> longList1 = service1.InvokeFunc<IList<long>>(-1, (Func<IList<long>>) (() =>
    {
      IObjectsCheckOutService service2 = ServiceUtils.GetService<IObjectsCheckOutService>((object) ServicesManager.ServiceContainer, true);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        try
        {
          IList<long> longList2 = (IList<long>) new List<long>(0);
          try
          {
            sessionKeeper.Session.StartLogHistory();
            longList2 = service2.CheckOut(sessionKeeper.Session, objectList, true);
          }
          finally
          {
            sessionKeeper.Session.StopLogHistory();
          }
          List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
          if (modificationsHistoryList != null)
          {
            for (int index = 0; index < modificationsHistoryList.Count; ++index)
            {
              CategoryValue categoryValue1 = modificationsHistoryList[index];
              if (categoryValue1.ActionID == ActionType.CheckOut && categoryValue1.CategoryType == 1)
              {
                ++index;
                CategoryValue categoryValue2 = modificationsHistoryList[index];
                if (!objectList.Contains(categoryValue1.CategoryID) && !longList2.Contains(categoryValue2.CategoryID))
                {
                  objectList.Add(categoryValue1.CategoryID);
                  longList2.Add(categoryValue2.CategoryID);
                }
              }
            }
          }
          return longList2;
        }
        catch (KernelException ex)
        {
          if (errorHandler != null)
            return errorHandler(objectList, ex);
          throw;
        }
      }
    }));
    if (longList1 == null)
      throw new AbortException();
    if (longList1.Count != objectList.Count)
      throw new InvalidOperationException();
    List<long> oldIds = new List<long>(objectList.Count);
    List<long> newIds = new List<long>(objectList.Count);
    for (int index = 0; index < objectList.Count; ++index)
    {
      long num1 = objectList[index];
      long num2 = longList1[index];
      if (num2 != num1)
      {
        oldIds.Add(num1);
        newIds.Add(num2);
      }
    }
    if (newIds.Count > 0)
      service1.InvokeAction(-1, (Action) (() => ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) oldIds, (IList<long>) newIds))));
    return longList1;
  }

  public delegate IList<long> CheckoutErrorHandler(IList<long> objectList, KernelException x);
}
