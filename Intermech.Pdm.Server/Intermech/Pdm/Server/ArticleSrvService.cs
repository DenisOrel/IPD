// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.ArticleSrvService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Helpers;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;

#nullable disable
namespace Intermech.Pdm.Server;

internal class ArticleSrvService : LongLifeObject, IArticleService
{
  private const string _materialSeparator = "&^";

  private IUserSession convertToUserSession(object usObject)
  {
    switch (usObject)
    {
      case IUserSession _:
        return usObject as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      default:
        return (IUserSession) null;
    }
  }

  private DataRowCollection SelectMainDocuments(
    long articleID,
    string filtrationRuleSettings,
    IUserSession usersession,
    ref long otherReturns,
    bool includeAllDrawings = false)
  {
    IDBObject dbObject1 = usersession.GetObject(articleID);
    IDBRelationType relationType = usersession.GetRelationType(new Guid("cad00154-306c-11d8-b4e9-00304f19f545"));
    IDBRelationCollection relationCollection = usersession.GetRelationCollection(relationType.RelationType);
    relationCollection.FiltrationOwnerID = filtrationRuleSettings;
    ArticleSrvService.SelectMainDocumentsType mainDocumentsType = ArticleSrvService.SelectMainDocumentsType.None;
    long objectId = dbObject1.ObjectID;
    if (MetaDataHelper.IsObjectTypeChildOf(dbObject1.ObjectType, new Guid("cad00250-306c-11d8-b4e9-00304f19f545")))
      mainDocumentsType = ArticleSrvService.SelectMainDocumentsType.forPart;
    else if (MetaDataHelper.IsObjectTypeChildOf(dbObject1.ObjectType, new Guid("cad00132-306c-11d8-b4e9-00304f19f545")) || MetaDataHelper.IsObjectTypeChildOf(dbObject1.ObjectType, new Guid("cad0025f-306c-11d8-b4e9-00304f19f545")) || MetaDataHelper.IsObjectTypeChildOf(dbObject1.ObjectType, new Guid("cad0025e-306c-11d8-b4e9-00304f19f545")))
      mainDocumentsType = ArticleSrvService.SelectMainDocumentsType.forAssembly;
    else if (MetaDataHelper.GetObjectTypeChildrenID(new Guid("cad00583-306c-11d8-b4e9-00304f19f545")).Contains(dbObject1.ObjectType))
    {
      IDBAttribute byGuid = dbObject1.Attributes.FindByGUID(new Guid("cad00622-306c-11d8-b4e9-00304f19f545"));
      if (byGuid != null && byGuid.AsInteger != 0L)
      {
        IDBObject dbObject2 = usersession.GetObject(byGuid.AsInteger);
        if (dbObject2 != null)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(dbObject2.ObjectType, new Guid("cad00250-306c-11d8-b4e9-00304f19f545")))
          {
            mainDocumentsType = ArticleSrvService.SelectMainDocumentsType.forPart;
            dbObject1 = dbObject2;
          }
          else if (MetaDataHelper.IsObjectTypeChildOf(dbObject2.ObjectType, new Guid("cad00132-306c-11d8-b4e9-00304f19f545")))
            mainDocumentsType = ArticleSrvService.SelectMainDocumentsType.forAssembly;
          if (mainDocumentsType != ArticleSrvService.SelectMainDocumentsType.None)
            objectId = dbObject2.ObjectID;
        }
      }
    }
    if (mainDocumentsType == ArticleSrvService.SelectMainDocumentsType.forPart)
    {
      IDBAttribute byGuid = dbObject1.Attributes.FindByGUID(new Guid("cad00624-306c-11d8-b4e9-00304f19f545"));
      if (byGuid != null && Convert.ToBoolean(byGuid.Value))
      {
        otherReturns = 0L;
        return (DataRowCollection) null;
      }
      ColumnDescriptor[] columns = new ColumnDescriptor[3]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) usersession.GetAttributeType(new Guid("cad00625-306c-11d8-b4e9-00304f19f545")).AttributeID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
      DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, columns), objectId);
      if (dataTable.Rows.Count == 1)
        return dataTable.Rows;
      if (dataTable.Rows.Count > 1)
      {
        DataTable toTable = dataTable.Clone();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (CompareValuesHelper.NormalizedValue(row[1]) != null)
          {
            DataSetProcessor.AddRow(toTable, row, true);
            break;
          }
        }
        if (toTable.Rows.Count == 0)
        {
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00261-306c-11d8-b4e9-00304f19f545"));
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (childrenIdRecursive.IndexOf(Convert.ToInt32(row[2])) >= 0)
            {
              DataSetProcessor.AddRow(toTable, row, true);
              if (!includeAllDrawings)
                break;
            }
          }
        }
        return toTable.Rows.Count != 1 ? dataTable.Rows : toTable.Rows;
      }
      otherReturns = 0L;
      return (DataRowCollection) null;
    }
    if (mainDocumentsType == ArticleSrvService.SelectMainDocumentsType.forAssembly)
    {
      ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.Equal, (object) usersession.GetObjectType(new Guid("cad00133-306c-11d8-b4e9-00304f19f545")).ObjectType, LogicalOperators.AND, 0, false);
      DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
      {
        conditionStructure
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      }), objectId);
      if (dataTable.Rows.Count > 0)
        return dataTable.Rows;
      otherReturns = 0L;
      return (DataRowCollection) null;
    }
    otherReturns = 0L;
    return (DataRowCollection) null;
  }

  private IDBObject FindBaseArticleForDesValue(
    IDBObject document,
    int attrDesID,
    string designationStamp,
    string filtrationRuleSettings,
    IUserSession usersession)
  {
    IDBRelationType relationType = usersession.GetRelationType(new Guid("cad00154-306c-11d8-b4e9-00304f19f545"));
    IDBRelationCollection relationCollection = usersession.GetRelationCollection(relationType.RelationType);
    ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.In, (object) this.GetArticleTypes(usersession), LogicalOperators.AND, 0, false);
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) attrDesID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    relationCollection.FiltrationOwnerID = filtrationRuleSettings;
    DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new ColumnDescriptor[2]
    {
      columnDescriptor1,
      columnDescriptor2
    }), document.ObjectID);
    if (dataTable.Rows.Count <= 0 || !(usersession.GetCustomService(typeof (IDocumentTypeSettingsService)) is IDocumentTypeSettingsService customService))
      return (IDBObject) null;
    DocumentTypeSettings settings = customService.GetSettings(usersession.SessionGUID, document.ObjectType);
    if (settings.DocumentTypeCodeInDesignation && settings.DocumentTypeCode != string.Empty)
      designationStamp = DocumentsHelper.RemoveDocCode(usersession, designationStamp, settings.DocumentTypeCode);
    long num = 0;
    long objectID = 0;
    bool flag = true;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (flag)
      {
        num = Math.Abs(int64);
        objectID = int64;
        flag = false;
      }
      else if (Math.Abs(int64) < num)
        objectID = int64;
      if (Convert.ToString(row[1]).Equals(designationStamp))
        return usersession.GetObject(int64, true);
    }
    return usersession.GetObject(objectID, true);
  }

  private int[] GetArticleTypes(IUserSession usersession)
  {
    List<int> intList = new List<int>();
    IDBObjectType objectType = usersession.GetObjectType(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
    intList.Add(objectType.ObjectType);
    foreach (DataRow row in (InternalDataCollectionBase) usersession.GetObjectTypeCollection(objectType.ObjectType).SelectRecursive(string.Empty).Rows)
      intList.Add(Convert.ToInt32(row["F_OBJECT_TYPE"]));
    return intList.ToArray();
  }

  private long FindObjectByNormalizedKey(
    IUserSession usersession,
    string objectKey,
    string filtrationRuleSettings,
    Guid collectionType)
  {
    if (string.IsNullOrEmpty(objectKey) || !(usersession.GetObjectCollection(collectionType) is DBObjectCollection objectCollection))
      return 0;
    objectCollection.GlobalSelectMode = true;
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad0011a-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) objectKey, LogicalOperators.AND, 0);
    DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_ID,
      (object) -2
    }));
    if (dataTable.Rows.Count == 1)
      return Convert.ToInt64(dataTable.Rows[0][1]);
    if (dataTable.Rows.Count <= 0)
      return 0;
    IDBObject objectByVersionsRule = usersession.GetObjectByVersionsRule(Convert.ToInt64(dataTable.Rows[0][0]), filtrationRuleSettings, false);
    return objectByVersionsRule == null ? 0L : objectByVersionsRule.ObjectID;
  }

  private string GetDocumentKey(string designation, string name)
  {
    if (!string.IsNullOrEmpty(designation))
      return designation;
    return !string.IsNullOrEmpty(name) ? name : string.Empty;
  }

  private string GetArticleKey(string designation, string okpCode, string name)
  {
    if (!string.IsNullOrEmpty(designation))
      return designation;
    if (!string.IsNullOrEmpty(okpCode))
      return okpCode;
    return !string.IsNullOrEmpty(name) ? name : string.Empty;
  }

  private string GetArticleKey(IDBObject obj)
  {
    return this.GetArticleKey(this.TryGetStringAttribute(obj, new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")), this.TryGetStringAttribute(obj, new Guid("cad0038a-306c-11d8-b4e9-00304f19f545")), this.TryGetStringAttribute(obj, new Guid("cad00020-306c-11d8-b4e9-00304f19f545")));
  }

  private string GetMaterialKey(IDBObject obj)
  {
    string stringAttribute = this.TryGetStringAttribute(obj, new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    return !string.IsNullOrEmpty(stringAttribute) ? stringAttribute : string.Empty;
  }

  private string TryGetStringAttribute(IDBObject obj, Guid attributeGuid)
  {
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(attributeGuid);
    return attributeByGuid != null && !attributeByGuid.IsNull ? attributeByGuid.AsString : (string) null;
  }

  public long FindArticleID(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    return this.FindArticleID(designation, okpCode, name, filtrationRuleSettings, session, false);
  }

  public long FindArticleID(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session,
    bool firstInMaterials)
  {
    IUserSession userSession = this.convertToUserSession(session);
    if (userSession == null)
      return 0;
    Guid guid1 = new Guid("cad00268-306c-11d8-b4e9-00304f19f545");
    Guid guid2 = new Guid("cad00170-306c-11d8-b4e9-00304f19f545");
    string articleKey = this.GetArticleKey(designation, okpCode, name);
    long objectByNormalizedKey = this.FindObjectByNormalizedKey(userSession, articleKey, filtrationRuleSettings, firstInMaterials ? guid2 : guid1);
    if (objectByNormalizedKey == 0L)
      objectByNormalizedKey = this.FindObjectByNormalizedKey(userSession, articleKey, filtrationRuleSettings, firstInMaterials ? guid1 : guid2);
    return objectByNormalizedKey;
  }

  public IDBObject FindArticleObject(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    return this.FindArticleObject(designation, okpCode, name, filtrationRuleSettings, session, false);
  }

  public IDBObject FindArticleObject(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session,
    bool firstInMaterials)
  {
    long articleId = this.FindArticleID(designation, okpCode, name, filtrationRuleSettings, session, firstInMaterials);
    if (articleId != 0L)
    {
      IUserSession userSession = this.convertToUserSession(session);
      if (userSession != null)
        return userSession.GetObject(articleId);
    }
    return (IDBObject) null;
  }

  public List<LinkedObject> FindArticlesAndRelationsWithoutFiltration(
    long documentID,
    string versionsRule,
    object session)
  {
    return new LinkedObject4DocumentFinder(this).FindArticles(documentID, versionsRule, this.convertToUserSession(session), true);
  }

  public long[] FindArticlesWithoutFiltration(long documentID, string versionsRule, object session)
  {
    return new OnlyArticles4DocumentFinder(this).FindArticles(documentID, versionsRule, this.convertToUserSession(session), true).ToArray();
  }

  public long[] FindArticles(long documentID, string filtrationRuleSettings, object session)
  {
    return new OnlyArticles4DocumentFinder(this).FindArticles(documentID, filtrationRuleSettings, this.convertToUserSession(session), false).ToArray();
  }

  private Guid GetGroupID(IDBObject articleObj, int attrArticleGroupID)
  {
    Guid groupId = Guid.Empty;
    IDBAttribute attributeById = articleObj.GetAttributeByID(attrArticleGroupID);
    if (attributeById != null && GuidHelper.IsGuid(attributeById.AsString))
      groupId = new Guid(attributeById.AsString);
    return groupId;
  }

  internal long[] FindArticlesByGroupIDAttr(
    Guid articleGroupID,
    int articleAttrID,
    int objectTypeID,
    IUserSession userSession,
    bool withoutFiltration)
  {
    List<long> longList = new List<long>();
    if (userSession != null && articleGroupID != Guid.Empty)
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
      };
      IDBObjectCollection objectCollection = userSession.GetObjectCollection(objectTypeID);
      objectCollection.ShowAllModifications = true;
      DBRecordSetParams paramsSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(articleAttrID, RelationalOperators.Equal, (object) articleGroupID, LogicalOperators.AND, 0, true)
      }, columns);
      if (withoutFiltration)
        FiltrationHelper.BlockPluginFiltrations(ref paramsSet, (HybridDictionary) null);
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramsSet).Rows)
        longList.Add(Convert.ToInt64(row[0]));
    }
    return longList.ToArray();
  }

  private long[] FindArticlesByGroupID(long articleID, object session, bool withoutFiltration)
  {
    IUserSession userSession = this.convertToUserSession(session);
    if (userSession != null)
    {
      int attributeId = userSession.GetAttributeType(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), true).AttributeID;
      IDBObject articleObj = userSession.GetObject(articleID);
      Guid groupId = this.GetGroupID(articleObj, attributeId);
      if (groupId != Guid.Empty)
        return this.FindArticlesByGroupIDAttr(groupId, attributeId, articleObj.ObjectType, userSession, withoutFiltration);
    }
    return new long[0];
  }

  public long[] FindArticlesByGroupID(long articleID, object session)
  {
    return this.FindArticlesByGroupID(articleID, session, false);
  }

  public long[] FindArticlesByGroupIDWithoutFiltration(long articleID, object session)
  {
    return this.FindArticlesByGroupID(articleID, session, true);
  }

  public long FindMainDocumentID(long articleID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    if (userSession == null)
      return 0;
    long otherReturns = 0;
    DataRowCollection dataRowCollection = this.SelectMainDocuments(articleID, filtrationRuleSettings, userSession, ref otherReturns);
    return dataRowCollection == null ? otherReturns : Convert.ToInt64(dataRowCollection[0][0]);
  }

  public long[] FindMainDocuments(long articleID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    if (userSession == null)
      return new List<long>().ToArray();
    long otherReturns = 0;
    DataRowCollection dataRowCollection = this.SelectMainDocuments(articleID, filtrationRuleSettings, userSession, ref otherReturns);
    if (dataRowCollection == null)
      return new List<long>().ToArray();
    List<long> longList = new List<long>();
    foreach (DataRow dataRow in (InternalDataCollectionBase) dataRowCollection)
      longList.Add(Convert.ToInt64(dataRow[0]));
    return longList.ToArray();
  }

  public IDBObject FindMainDocument(long articleID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    long mainDocumentId = this.FindMainDocumentID(articleID, filtrationRuleSettings, session);
    return mainDocumentId == 0L ? (IDBObject) null : userSession.GetObject(mainDocumentId);
  }

  public long[] FindMainDocuments(long[] articleIDs, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    List<long> longList = new List<long>();
    foreach (long articleId in articleIDs)
    {
      long otherReturns = 0;
      DataRowCollection dataRowCollection = this.SelectMainDocuments(articleId, filtrationRuleSettings, userSession, ref otherReturns);
      if (dataRowCollection == null)
        longList.Add(otherReturns);
      longList.Add(Convert.ToInt64(dataRowCollection[0][0]));
    }
    return longList.ToArray();
  }

  public long[] FindMainDocumentIDsForAllDrawings(
    long[] articleIDs,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    List<long> longList = new List<long>();
    foreach (long articleId in articleIDs)
    {
      long otherReturns = 0;
      DataRowCollection dataRowCollection = this.SelectMainDocuments(articleId, filtrationRuleSettings, userSession, ref otherReturns, true);
      if (dataRowCollection == null)
      {
        longList.Add(otherReturns);
      }
      else
      {
        for (int index = 0; index < dataRowCollection.Count; ++index)
          longList.Add(Convert.ToInt64(dataRowCollection[index][0]));
      }
    }
    return longList.ToArray();
  }

  public IDBObject FindBaseArticle(long documentID, string filtrationRuleSettings, object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    IDBObject document = userSession.GetObject(documentID, true);
    IDBAttribute attributeByGuid = document.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
    return attributeByGuid == null || attributeByGuid.Value == null || attributeByGuid.Value == DBNull.Value ? (IDBObject) null : this.FindBaseArticleForDesValue(document, attributeByGuid.AttributeID, attributeByGuid.Value.ToString(), filtrationRuleSettings, userSession);
  }

  public IDBObject FindBaseArticleForValue(
    long documentID,
    string value,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    IDBObject document = userSession.GetObject(documentID, true);
    IDBAttribute attributeByGuid = document.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
    return attributeByGuid == null ? (IDBObject) null : this.FindBaseArticleForDesValue(document, attributeByGuid.AttributeID, value, filtrationRuleSettings, userSession);
  }

  public List<long> GetListInstances(long articleID, object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    List<long> listInstances = new List<long>();
    IDBObject dbObject = userSession.GetObject(articleID, false);
    if (dbObject == null)
    {
      listInstances.Add(articleID);
      return listInstances;
    }
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid != null && GuidHelper.IsGuid(attributeByGuid.AsString))
      listInstances = this.GetListInstances(attributeByGuid.Value, (object) userSession);
    else
      listInstances.Add(articleID);
    return listInstances;
  }

  public List<long> GetListInstances(object groupID, object session)
  {
    if (groupID == null)
      throw new ArgumentNullException(nameof (groupID));
    IUserSession userSession = session != null ? this.convertToUserSession(session) : throw new ArgumentNullException(nameof (session));
    List<long> listInstances = new List<long>();
    if (groupID == null && groupID == DBNull.Value)
      return listInstances;
    conditionValue = Guid.Empty;
    if (groupID is string)
    {
      if (GuidHelper.IsGuid((string) groupID))
        conditionValue = new Guid((string) groupID);
    }
    else if (!(groupID is Guid conditionValue))
      ;
    if (conditionValue != Guid.Empty)
    {
      foreach (DataRow row in (InternalDataCollectionBase) userSession.GetObjectCollection(new Guid("cad00268-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue, LogicalOperators.OR, 0)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      })).Rows)
        listInstances.Add(Convert.ToInt64(row[0]));
    }
    return listInstances;
  }

  public List<QuickObjectInfo> FindListInstances(
    long documentID,
    string filtrationRuleSettings,
    object session)
  {
    List<QuickObjectInfo> listInstances = new List<QuickObjectInfo>();
    IUserSession userSession = this.convertToUserSession(session);
    IDBObject dbObject = userSession.GetObject(documentID, false);
    if (dbObject == null)
      return listInstances;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
    string designation = attributeByGuid == null || attributeByGuid.IsNull ? string.Empty : attributeByGuid.AsString;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad001f9-306c-11d8-b4e9-00304f19f545");
    IDBRelationType relationType = userSession.GetRelationType(new Guid("cad00154-306c-11d8-b4e9-00304f19f545"));
    IDBRelationCollection relationCollection = userSession.GetRelationCollection(relationType.RelationType);
    ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.In, (object) this.GetArticleTypes(userSession), LogicalOperators.AND, 0, false);
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) attributeByGuid.AttributeID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor3 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor4 = new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor5 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor6 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor7 = new ColumnDescriptor((object) attributeTypeId, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
    relationCollection.FiltrationOwnerID = filtrationRuleSettings;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new ColumnDescriptor[7]
    {
      columnDescriptor1,
      columnDescriptor2,
      columnDescriptor3,
      columnDescriptor4,
      columnDescriptor5,
      columnDescriptor6,
      columnDescriptor7
    });
    DataTable dataTable = relationCollection.EntersInVersion(paramSet, dbObject.ObjectID);
    if (dataTable.Rows.Count > 0 && userSession.GetCustomService(typeof (IDocumentTypeSettingsService)) is IDocumentTypeSettingsService customService)
    {
      DocumentsHelper.GetSeparatorInDesignation(userSession);
      DocumentTypeSettings settings = customService.GetSettings(userSession.SessionGUID, dbObject.ObjectType);
      if (settings.DocumentTypeCodeInDesignation && settings.DocumentTypeCode != string.Empty)
        designation = DocumentsHelper.RemoveDocCode(userSession, designation, settings.DocumentTypeCode);
      long num = 0;
      bool flag = true;
      List<QuickObjectInfo> quickObjectInfoList = new List<QuickObjectInfo>();
      QuickObjectInfo quickObjectInfo1 = new QuickObjectInfo();
      quickObjectInfo1.ObjectTypeID = -1;
      List<Guid> guidList = new List<Guid>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        if (flag)
        {
          num = Math.Abs(int64);
          flag = false;
        }
        else
          Math.Abs(int64);
        QuickObjectInfo quickObjectInfo2 = new QuickObjectInfo(Convert.ToInt64(row[0]), Convert.ToString(row[3]), Convert.ToInt32(row[2]), new Guid(row[4].ToString()), Convert.ToInt64(row[5]));
        if (quickObjectInfo1.ObjectTypeID == -1 && Convert.ToString(row[1]).Equals(designation))
          quickObjectInfo1 = quickObjectInfo2;
        else
          quickObjectInfoList.Add(quickObjectInfo2);
        string str = Convert.ToString(row[6]);
        if (!string.IsNullOrEmpty(str) && GuidHelper.IsGuid(str))
        {
          Guid guid = new Guid(str);
          if (!guidList.Contains(guid))
            guidList.Add(guid);
        }
      }
      if (quickObjectInfo1.ObjectTypeID == -1)
      {
        foreach (QuickObjectInfo quickObjectInfo3 in quickObjectInfoList)
        {
          if (num == Math.Abs(quickObjectInfo3.ObjectID))
          {
            listInstances.Add(quickObjectInfo3);
            break;
          }
        }
        foreach (QuickObjectInfo quickObjectInfo4 in quickObjectInfoList)
        {
          if (num != Math.Abs(quickObjectInfo4.ObjectID))
            listInstances.Add(quickObjectInfo4);
        }
      }
      else
      {
        listInstances.Add(quickObjectInfo1);
        foreach (QuickObjectInfo quickObjectInfo5 in quickObjectInfoList)
          listInstances.Add(quickObjectInfo5);
      }
      if (guidList.Count > 0)
      {
        conditionStructure = guidList.Count != 1 ? new ConditionStructure(attributeTypeId, RelationalOperators.In, (object) guidList.ToArray(), LogicalOperators.NONE, 0, false) : new ConditionStructure(attributeTypeId, RelationalOperators.Equal, (object) guidList[0], LogicalOperators.NONE, 0, false);
        foreach (DataRow row in (InternalDataCollectionBase) userSession.GetObjectCollection(new Guid("cad00268-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          conditionStructure
        }, new ColumnDescriptor[6]
        {
          columnDescriptor1,
          columnDescriptor2,
          columnDescriptor3,
          columnDescriptor4,
          columnDescriptor5,
          columnDescriptor6
        })).Rows)
        {
          long objectID = Convert.ToInt64(row[0]);
          if (!listInstances.Exists((Predicate<QuickObjectInfo>) (x => x.ObjectID == objectID)))
            listInstances.Add(new QuickObjectInfo(Convert.ToInt64(row[0]), Convert.ToString(row[3]), Convert.ToInt32(row[2]), new Guid(row[4].ToString()), Convert.ToInt64(row[5])));
        }
      }
    }
    return listInstances;
  }

  public IDBObject FindMaterial(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    if (userSession != null)
    {
      string articleKey = this.GetArticleKey(designation, okpCode, name);
      long objectByNormalizedKey = this.FindObjectByNormalizedKey(userSession, articleKey, filtrationRuleSettings, new Guid("cad00170-306c-11d8-b4e9-00304f19f545"));
      if (objectByNormalizedKey != 0L)
        return userSession.GetObject(objectByNormalizedKey);
    }
    return (IDBObject) null;
  }

  public IDBObject FindMaterial(
    string designation,
    string okpCode,
    string name,
    int materialType,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    if (userSession != null)
    {
      string articleKey = this.GetArticleKey(designation, okpCode, name);
      IDBObjectType objectType = userSession.GetObjectType(materialType, true);
      long objectByNormalizedKey = this.FindObjectByNormalizedKey(userSession, articleKey, filtrationRuleSettings, (objectType as IDBGuid).GUID);
      if (objectByNormalizedKey != 0L)
        return userSession.GetObject(objectByNormalizedKey);
    }
    return (IDBObject) null;
  }

  public long GetMaterialID(string name, string filtrationRuleSettings, object session)
  {
    return this.GetMaterialID(name, filtrationRuleSettings, session, true);
  }

  public long GetMaterialID(
    string name,
    string filtrationRuleSettings,
    object session,
    bool trueMaterialsOnly)
  {
    IUserSession userSession = this.convertToUserSession(session);
    int length = name.IndexOf("&^");
    string str1 = length >= 0 ? name.Substring(0, length) : name;
    string str2 = length >= 0 ? name.Substring(length + 2) : string.Empty;
    if (!trueMaterialsOnly && !string.IsNullOrEmpty(str2))
    {
      Guid guidFromImbaseKey = this.TryGetGuidFromImbaseKey(str2);
      if (guidFromImbaseKey != Guid.Empty)
      {
        long nonMaterialByGuid = this.TryGetNonMaterialByGuid(guidFromImbaseKey, filtrationRuleSettings, userSession);
        if (nonMaterialByGuid != 0L)
          return nonMaterialByGuid;
      }
    }
    if (str1 != string.Empty)
    {
      IDBObject material = this.FindMaterial(string.Empty, string.Empty, str1, filtrationRuleSettings, (object) userSession);
      if (material != null)
        return material.ObjectID;
    }
    long materialIdByImbase = this.TryGetMaterialIDByIMBASE(userSession, str2, str1);
    if (materialIdByImbase != 0L)
      return materialIdByImbase;
    IDBObject dbObject = userSession.GetObjectCollection(new Guid("cad0081d-306c-11d8-b4e9-00304f19f545")).Create();
    IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(userSession.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AttributeID, false);
    if (dbAttribute != null)
      dbAttribute.AsString = str1;
    dbObject.CommitCreation(true);
    return dbObject.ObjectID;
  }

  private Guid TryGetGuidFromImbaseKey(string imbaseKey)
  {
    Guid result;
    return imbaseKey.StartsWith("IG", StringComparison.CurrentCultureIgnoreCase) && Guid.TryParse(imbaseKey.Substring(2), out result) ? result : Guid.Empty;
  }

  private long TryGetNonMaterialByGuid(
    Guid materialGuid,
    string filtrationOwnerId,
    IUserSession userSession)
  {
    IDBObject objectByVersionsRule = userSession.GetObjectByVersionsRule(materialGuid, filtrationOwnerId, false);
    return objectByVersionsRule != null && !objectByVersionsRule.isParentType(new Guid("cad00170-306c-11d8-b4e9-00304f19f545")) ? objectByVersionsRule.ObjectID : 0L;
  }

  private long TryGetMaterialIDByIMBASE(
    IUserSession userSession,
    string oldImbaseKey,
    string materialName)
  {
    if (!(userSession.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService1))
      return 0;
    if (!string.IsNullOrEmpty(oldImbaseKey))
    {
      long idByOldImbaseKey = customService1.GetObjectIdByOldImbaseKey(userSession.SessionGUID, oldImbaseKey, -1, false, out ScanOldKeyStatus _);
      if (idByOldImbaseKey != -1L)
        return idByOldImbaseKey;
    }
    if (!(userSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService2) || string.IsNullOrEmpty(materialName))
      return 0;
    int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00170-306c-11d8-b4e9-00304f19f545"));
    int attributeId = MetaDataHelper.GetAttributeID((object) "cad00020-306c-11d8-b4e9-00304f19f545");
    Tuple<long, long, long> record = new ImbaseSearchTool(userSession, customService1, customService2).FindRecord(objectTypeId, true, attributeId, materialName);
    return record == null ? 0L : customService1.CreateObject(userSession.SessionGUID, record.Item1, record.Item2, record.Item3, true, -1);
  }

  public string GetMaterialName(long materialID, object session)
  {
    IDBObject dbObject = this.convertToUserSession(session).GetObject(materialID);
    StringBuilder stringBuilder = new StringBuilder();
    string materialKey = this.GetMaterialKey(dbObject);
    if (!string.IsNullOrEmpty(materialKey))
      stringBuilder.Append(materialKey);
    stringBuilder.Append("&^");
    stringBuilder.Append("IG");
    stringBuilder.Append(dbObject.GUID.ToString());
    return stringBuilder.ToString();
  }

  public long FindDocumentID(
    string designation,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    IUserSession userSession = this.convertToUserSession(session);
    if (userSession == null)
      return 0;
    string documentKey = this.GetDocumentKey(designation, name);
    return this.FindObjectByNormalizedKey(userSession, documentKey, filtrationRuleSettings, new Guid("cad0057f-306c-11d8-b4e9-00304f19f545"));
  }

  public IDBObject FindDocumentObject(
    string designation,
    string name,
    string filtrationRuleSettings,
    object session)
  {
    long documentId = this.FindDocumentID(designation, name, filtrationRuleSettings, session);
    if (documentId != 0L)
    {
      IUserSession userSession = this.convertToUserSession(session);
      if (userSession != null)
        return userSession.GetObject(documentId);
    }
    return (IDBObject) null;
  }

  private enum SelectMainDocumentsType
  {
    None,
    forPart,
    forAssembly,
  }
}
