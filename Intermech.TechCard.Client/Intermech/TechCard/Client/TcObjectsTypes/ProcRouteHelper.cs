// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.ProcRouteHelper
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>Класс Нelper для маршрута обработки</summary>
public static class ProcRouteHelper
{
  /// <summary>Значение по атрибута "Маршрут обработки по умолчанию"</summary>
  /// <returns></returns>
  public static object RouteProcDefaultAttrValue
  {
    get
    {
      object defaultAttrValue = (object) DBNull.Value;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.ProcRouteDefaultAttrID);
      if (attributeType != null)
      {
        defaultAttrValue = attributeType.DefaultValue;
        if ((defaultAttrValue == null || defaultAttrValue == DBNull.Value || defaultAttrValue.ToString() == string.Empty) && attributeType.PossibleValues != null && attributeType.PossibleValues.Count > 0)
          defaultAttrValue = attributeType.PossibleValues[0];
      }
      if (defaultAttrValue == null || defaultAttrValue == DBNull.Value || defaultAttrValue.ToString() == string.Empty)
        defaultAttrValue = (object) "Default";
      return defaultAttrValue;
    }
  }

  /// <summary>Получение МО по умолчанию для ДСЕ</summary>
  /// <param name="objArtId">Ид. версии изделия</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="createIfNotFound">Режим создания нового МО, если он не найден</param>
  /// <returns></returns>
  public static long GetDefaultProcRouteForArticle(
    long objArtId,
    IUserSession session,
    bool createIfNotFound = true)
  {
    return ProcRouteHelper.GetDefaultProcRouteForArticle(objArtId, session, createIfNotFound, out IList<long> _);
  }

  /// <summary>Получение МО по умолчанию для ДСЕ</summary>
  /// <param name="objArtId">Ид. версии изделия</param>
  /// <param name="session">Пользов. сессия</param>
  /// <param name="createIfNotFound">Режим создания нового МО, если он не найден</param>
  /// <param name="createdObjects">Идентификаторы объектов, которые потребовалось создать</param>
  /// <returns></returns>
  public static long GetDefaultProcRouteForArticle(
    long objArtId,
    IUserSession session,
    bool createIfNotFound,
    out IList<long> createdObjects)
  {
    long procRouteForArticle = 0;
    if (objArtId == 0L || session == null)
    {
      createdObjects = (IList<long>) new List<long>();
      return procRouteForArticle;
    }
    if (!ProcRouteHelper.GetDefaultProcRouteForArticles((IList<long>) new long[1]
    {
      objArtId
    }, session, createIfNotFound, out createdObjects).TryGetValue(objArtId, out procRouteForArticle))
      procRouteForArticle = 0L;
    return procRouteForArticle;
  }

  /// <summary>Получение МО по умолчанию для ДСЕ</summary>
  /// <param name="objArtList">Ид. версий изделий</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="createIfNotFound">Режим создания нового МО, если он не найден</param>
  /// <returns></returns>
  public static Dictionary<long, long> GetDefaultProcRouteForArticles(
    IList<long> objArtList,
    IUserSession session,
    bool createIfNotFound = true)
  {
    return ProcRouteHelper.GetDefaultProcRouteForArticles(objArtList, session, createIfNotFound, out IList<long> _);
  }

  /// <summary>Получение МО по умолчанию для ДСЕ</summary>
  /// <param name="objArtList">Ид. версий изделий</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="createIfNotFound">Режим создания нового МО, если он не найден</param>
  /// <param name="createdObjects">Идентификаторы объектов, которые потребовалось создать</param>
  /// <returns></returns>
  public static Dictionary<long, long> GetDefaultProcRouteForArticles(
    IList<long> objArtList,
    IUserSession session,
    bool createIfNotFound,
    out IList<long> createdObjects)
  {
    Dictionary<long, long> routeForArticles = new Dictionary<long, long>();
    createdObjects = (IList<long>) new List<long>();
    if (objArtList == null || objArtList.Count == 0 || session == null)
      return routeForArticles;
    string key = TechCardConsts.AttributeTypes.ProcRouteDefaultAttrGuid.ToString();
    Dictionary<string, ColumnDescriptor> dictionary = new Dictionary<string, ColumnDescriptor>()
    {
      {
        key,
        new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ProcRouteDefaultAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
      }
    };
    ConditionStructure[] conditionStructureArray = new ConditionStructure[2]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingID).ToArray(), LogicalOperators.AND, 0, false),
      new ConditionStructure(TechCardConsts.AttributeTypes.ProcRouteDefaultAttrID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
    };
    IList<long> projIdList = objArtList;
    IUserSession userSession = session;
    int[] relations = new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    };
    ConditionStructure[] conditions = conditionStructureArray;
    Dictionary<string, ColumnDescriptor> columns = dictionary;
    foreach (TechCardUtils.SostavTreeItem sostavTreeItem in TechCardUtils.GetChildSostavTree(projIdList, userSession, (IEnumerable<int>) relations, false, conditions, columns))
    {
      if (sostavTreeItem.Values.ContainsKey(key))
      {
        object obj = sostavTreeItem.Values[key];
        if (obj != null && obj.ToString() != string.Empty && !routeForArticles.ContainsKey(sostavTreeItem.ProjID))
          routeForArticles.Add(sostavTreeItem.ProjID, sostavTreeItem.PartID);
      }
    }
    if (!createIfNotFound || routeForArticles.Count == objArtList.Count)
      return routeForArticles;
    IDBObjectCollection objectCollection = session.GetObjectCollection(TechCardConsts.ObjectTypes.ProcRoutingID);
    if (objectCollection == null)
      return routeForArticles;
    List<IDBRelation> source = new List<IDBRelation>();
    foreach (long objArt in (IEnumerable<long>) objArtList)
    {
      if (!routeForArticles.ContainsKey(objArt))
      {
        IDBObject projDbObject = session.GetObject(objArt, false);
        if (projDbObject != null)
        {
          IDBObject dbObject = objectCollection.Create();
          ObjInfoItem classifyObjectItem = new ObjInfoItem(dbObject);
          ObjInfoItem contextObjectItem = new ObjInfoItem(objArt);
          List<AttributeValues> attributeValuesList = ProcRouteHelper.ClassifyNewObject(session, new TechCardClassifyObjectParams(classifyObjectItem, contextObjectItem));
          attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.ProcRouteDefaultAttrID, ProcRouteHelper.RouteProcDefaultAttrValue));
          dbObject.SetAttributesValues(attributeValuesList.ToArray());
          IDBRelationCollection relationCollection = session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
          TechcardClientUtils.StartCreateRelations(projDbObject.ObjectID, session);
          try
          {
            source.Add(TechcardClientUtils.CreateRelation(relationCollection, projDbObject, dbObject));
            if (dbObject.IsCreationMode)
              dbObject.CommitCreation(false, dbObject.ObjectModifyMode == ObjectModifyModes.Checkout);
          }
          finally
          {
            TechcardClientUtils.StopCreateRelations(session);
          }
          createdObjects.Add(dbObject.ObjectID);
          routeForArticles.Add(objArt, dbObject.ObjectID);
        }
      }
    }
    if (source.Count != 0)
    {
      NotificationQueue notificationQueue = new NotificationQueue();
      notificationQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source.Select<IDBRelation, int>((System.Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
      notificationQueue.FlushQueue();
    }
    return routeForArticles;
  }

  /// <summary>
  /// Классифицировать атрибуты нового объекта маршрута обработки
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="classifyBaseObjectParams">Параметры классификации объекта</param>
  /// <returns></returns>
  public static List<AttributeValues> ClassifyNewObject(
    IUserSession session,
    TechCardClassifyObjectParams classifyBaseObjectParams)
  {
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>(3);
    string attributeValue1;
    string attributeValue2;
    if (service == null || !(service.ClassifyObjectAttribute(session, new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyBaseObjectParams), (ITechCardClassifyObjectStrategy) new TechCardClassifyObjectDesignationStrategy(), out attributeValue1) | service.ClassifyObjectAttribute(session, new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyBaseObjectParams), (ITechCardClassifyObjectStrategy) new TechCardClassifyObjectNameStrategy(), out attributeValue2)))
      return attributeValuesList;
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) attributeValue2));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) attributeValue1));
    return attributeValuesList;
  }

  /// <summary>Получения изделий для МО</summary>
  /// <param name="procRouteId">Ид. версии МО</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns></returns>
  public static List<long> GetArticlesForProcRoute(long procRouteId, IUserSession session)
  {
    List<long> articlesForProcRoute = new List<long>();
    if (procRouteId == 0L || session == null)
      return articlesForProcRoute;
    List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
    List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingID);
    DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(TechCardConsts.RelTypes.TechRelationID, TechCardConsts.ObjectTypes.ProcRoutingID, -1);
    if (applicabilitiesList != null)
    {
      int idxFldInObjType = applicabilitiesList.Columns.IndexOf("F_INOBJECT_TYPE");
      childrenIdRecursive1.AddRange((IEnumerable<int>) applicabilitiesList.AsEnumerable().Select<DataRow, int>((System.Func<DataRow, int>) (row => Convert.ToInt32(row[idxFldInObjType]))));
    }
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    };
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      new ObjInfoItem(procRouteId)
    }, (IEnumerable<int>) childrenIdRecursive1, (IEnumerable<int>) childrenIdRecursive2, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, false, false, 2, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
    DataTable dataTable = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row["F_PROJ_ID"]);
        if (int64 != 0L)
          articlesForProcRoute.Add(int64);
      }
    }
    return articlesForProcRoute;
  }

  /// <summary>
  /// Получение информации об контекста изделия в текущем окне
  /// </summary>
  /// <param name="artObjId"></param>
  /// <param name="projArtObjId"></param>
  /// <param name="zakazObjId"></param>
  /// <returns></returns>
  public static bool GetArticleContextInfo(
    long artObjId,
    out long projArtObjId,
    out long zakazObjId)
  {
    projArtObjId = 0L;
    zakazObjId = 0L;
    IEnumerable<RelObjInfoItem> relObjInfoItems;
    if (!TechcardClientControlsUtils.GetItemsApplicabilityInfo(ObjectExtensions.GetItems(artObjId), (IServiceProvider) ApplicationServices.Container, out relObjInfoItems))
      return false;
    RelObjInfoItem relObjInfoItem1 = relObjInfoItems.FirstOrDefault<RelObjInfoItem>();
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) relObjInfoItem1?.ProjInfo))
      return false;
    if (MetaDataHelper.IsObjectTypeChildOf(relObjInfoItem1.ProjInfo.ObjTypeID, TechCardConsts.ObjectTypes.ZakazObjectID))
    {
      projArtObjId = relObjInfoItem1.ProjInfo.ObjectID;
      zakazObjId = relObjInfoItem1.ProjInfo.ObjectID;
      return true;
    }
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
    if (!childrenIdRecursive.Contains(relObjInfoItem1.ProjInfo.ObjTypeID))
      return false;
    projArtObjId = relObjInfoItem1.ProjInfo.ObjectID;
    foreach (RelObjInfoItem relObjInfoItem2 in relObjInfoItems)
    {
      if (!ObjInfoItem.IsEmpty((ITypedInfoItem) relObjInfoItem2?.ProjInfo))
      {
        if (MetaDataHelper.IsObjectTypeChildOf(relObjInfoItem2.ProjInfo.ObjTypeID, TechCardConsts.ObjectTypes.ZakazObjectID))
        {
          zakazObjId = relObjInfoItem2.ProjInfo.ObjectID;
          break;
        }
        if (!childrenIdRecursive.Contains(relObjInfoItem2.ProjInfo.ObjTypeID))
          break;
      }
      else
        break;
    }
    return true;
  }
}
