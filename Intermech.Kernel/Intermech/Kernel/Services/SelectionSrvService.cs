// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.SelectionSrvService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Interfaces.Server;
using Intermech.Kernel.NotifySamples;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services;

public class SelectionSrvService : LongLifeObject, ISelectionsService
{
  private Intermech.Interfaces.SelectionService.SelectionService selectionService;
  private readonly ConcurrentDictionary<Guid, SelectionStructureCopier> _selectionStructureCopiers;
  private ConcurrentBag<ClassifierToObjTypeStructure> classifToObjTypeCache;
  private const long AllUsers = 0;
  private const int AllObjectTypes = -1;
  private int _ClassifCommonID;
  private int _ClassifPersonID;

  public SelectionSrvService()
  {
    this.selectionService = new Intermech.Interfaces.SelectionService.SelectionService(false);
    IDBObjectCreator creatorInstance1 = (IDBObjectCreator) new DBClassifierCreator();
    ICreatorContainer service1 = ServerServices.GetService(typeof (IDBObjectService)) as ICreatorContainer;
    service1.AddCreator((object) DBClassifierCreator.ClassifFolderGuid, (object) creatorInstance1);
    service1.AddCreator((object) DBClassifierCreator.ClassifCommonGuid, (object) creatorInstance1);
    service1.AddCreator((object) DBClassifierCreator.ClassifPersonGuid, (object) creatorInstance1);
    IDBObjectCreator creatorInstance2 = (IDBObjectCreator) new DBSelectionCreator();
    service1.AddCreator((object) new Guid("cad00122-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance2);
    service1.AddCreator((object) new Guid("cad00123-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance2);
    service1.AddCreator((object) NotifySamplesConst.NotifySamplesTypeGuid, (object) creatorInstance2);
    if (ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service2)
    {
      service2.AfterCacheReload += new CacheReloadHandler(this.OnEventLogHelper_AfterCacheReload);
      service2.AfterCreateRelationExEvent += new CreateRelationExHandler(this.OnEventLogHelper_AfterCreateRelationExEvent);
    }
    this._selectionStructureCopiers = new ConcurrentDictionary<Guid, SelectionStructureCopier>();
  }

  private void OnEventLogHelper_AfterCreateRelationExEvent(
    IDBRelation sender,
    IUserSession session,
    int assignMode)
  {
    if (((sender as DBRelation).Applicability.Options & ApplicabilityOptions.AutoClassificationChildObject) != ApplicabilityOptions.AutoClassificationChildObject)
      return;
    ISelectionsService service = ServerServices.GetService(typeof (ISelectionsService)) as ISelectionsService;
    long classifierForObject = service.GetClassifierForObject((object) session, sender.ProjObject.ID);
    if (classifierForObject == -1L || !sender.PartObject.IsCreationMode)
      return;
    ClassifiedError classifiedError = service.GetObjectClassificator((object) session, classifierForObject).ClassifyObjects(new long[1]
    {
      sender.PartObject.ObjectID
    });
    if (classifiedError.Exception != null)
      throw new Exception($"Ошибка при классификации {sender.PartObject.NameInMessages}: {classifiedError.Exception.Message}", classifiedError.Exception);
    IDBObject dbObject = session.GetObject(classifierForObject);
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(DBClassifierCreator.ClassifFolderKeyGuid);
    string folderKey = attributeByGuid != null ? Convert.ToString(attributeByGuid.Value) : string.Empty;
    ClassifierProcessor.DoAddBlankObject(session as UserSession, classifierForObject, sender.PartObject.ObjectID, sender.PartObject.ID, folderKey);
    if (this.AfterClassifyObjectsEvent == null)
      return;
    IDBObject classifier = session.GetObject(this.GetRootClassifier((object) session, dbObject), false);
    this.AfterClassifyObjectsEvent(session, classifier, dbObject, new long[1]
    {
      sender.PartObject.ObjectID
    });
  }

  public void LoadClassifierToObjTypeCache()
  {
    this.classifToObjTypeCache = new ConcurrentBag<ClassifierToObjTypeStructure>();
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (LoadClassifierToObjTypeCache));
    try
    {
      this._ClassifCommonID = sessionTemporaryClone.IdentHelper.GetObjectTypeID(DBClassifierCreator.ClassifCommonGuid.ToString());
      this._ClassifPersonID = sessionTemporaryClone.IdentHelper.GetObjectTypeID(DBClassifierCreator.ClassifPersonGuid.ToString());
      IDBObjectCollection objectCollection1 = sessionTemporaryClone.GetObjectCollection(DBClassifierCreator.ClassifCommonGuid);
      this.AddToClassifierToObjTypeCache(sessionTemporaryClone, objectCollection1);
      IDBObjectCollection objectCollection2 = sessionTemporaryClone.GetObjectCollection(DBClassifierCreator.ClassifPersonGuid);
      (objectCollection2 as DBObjectCollection).GlobalSelectMode = true;
      this.AddToClassifierToObjTypeCache(sessionTemporaryClone, objectCollection2);
    }
    finally
    {
      sessionTemporaryClone?.Logout(nameof (LoadClassifierToObjTypeCache));
    }
  }

  private void OnEventLogHelper_AfterCacheReload(IDbManager db)
  {
    this.LoadClassifierToObjTypeCache();
  }

  public void DeleteClassifierFromCache(long classifierID)
  {
    ConcurrentBag<ClassifierToObjTypeStructure> concurrentBag = new ConcurrentBag<ClassifierToObjTypeStructure>();
    foreach (ClassifierToObjTypeStructure objTypeStructure in this.classifToObjTypeCache)
    {
      if (objTypeStructure.ClassifierID != classifierID)
        concurrentBag.Add(objTypeStructure);
    }
    this.classifToObjTypeCache = concurrentBag;
  }

  private List<ClassifierToObjTypeStructure> GetClassifierToObjType(
    IUserSession session,
    long classifierID)
  {
    List<ClassifierToObjTypeStructure> classifierToObjType = new List<ClassifierToObjTypeStructure>();
    int objTypeID1 = -1;
    IDBObject dbObject = session.GetObject(classifierID);
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad00e8f-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1.AsInteger != 3L && attributeByGuid1.AsInteger != 4L)
      return (List<ClassifierToObjTypeStructure>) null;
    IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
    long ownerId = dbObject.ObjectType == this._ClassifPersonID ? dbObject.OwnerID : 0L;
    if (attributeByGuid2 != null && attributeByGuid2.Values.Length != 0)
    {
      foreach (object obj in attributeByGuid2.Values)
      {
        int objTypeID2 = -1;
        string str = Convert.ToString(obj);
        Guid objTypeGuid = GuidHelper.IsGuid(str) ? new Guid(str) : Guid.Empty;
        if (objTypeGuid == Guid.Empty && attributeByGuid2.Values.Length == 1)
        {
          classifierToObjType.Add(new ClassifierToObjTypeStructure(Math.Abs(classifierID), objTypeID2, ownerId));
          break;
        }
        if (!(objTypeGuid == Guid.Empty))
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(objTypeGuid);
          if (objectTypeId != -1)
            classifierToObjType.Add(new ClassifierToObjTypeStructure(Math.Abs(classifierID), objectTypeId, ownerId));
        }
      }
    }
    else
      classifierToObjType.Add(new ClassifierToObjTypeStructure(Math.Abs(classifierID), objTypeID1, ownerId));
    return classifierToObjType;
  }

  public void AddClassifierToCache(IUserSession session, long classifierID)
  {
    List<ClassifierToObjTypeStructure> classifierToObjType = this.GetClassifierToObjType(session, classifierID);
    if (classifierToObjType == null)
      return;
    for (int index = 0; index < classifierToObjType.Count; ++index)
      this.classifToObjTypeCache.Add(classifierToObjType[index]);
  }

  public long[] GetClassifierForObjType(object userSession, int objType)
  {
    IUserSession userSession1 = this.convertToUserSession(userSession);
    List<int> intList = new List<int>(1);
    intList.Add(objType);
    if (objType != -1)
    {
      for (IDBObjectType objectType = userSession1.GetObjectType(objType); objectType.ParentTypeID != -1; objectType = userSession1.GetObjectType(objectType.ParentTypeID))
        intList.Add(objectType.ParentTypeID);
    }
    List<long> longList = new List<long>();
    foreach (ClassifierToObjTypeStructure objTypeStructure in this.classifToObjTypeCache)
    {
      if (objTypeStructure.UserID == 0L || objTypeStructure.UserID == userSession1.UserID)
      {
        if (objType == -1)
          longList.Add(objTypeStructure.ClassifierID);
        else if ((intList.Contains(objTypeStructure.ObjectTypeID) || objTypeStructure.ObjectTypeID == -1) && this.CheckVisible(userSession1, objTypeStructure.ClassifierID))
          longList.Add(objTypeStructure.ClassifierID);
      }
    }
    return longList.Count > 0 ? longList.ToArray() : (long[]) null;
  }

  private bool CheckVisible(IUserSession session, long classifierID)
  {
    IDBObject dbObject = session.GetObject(classifierID);
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0062f-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || attributeByGuid.IsNull)
      return true;
    ObjectsVisibility settings = new ObjectsVisibility();
    settings.Assign(attributeByGuid.Value);
    return DBRecordSet.ObjectsVisibilityFiltration.Visible(session, settings, dbObject.OwnerID);
  }

  public int[] GetObjectTypesForClassifier(object userSession, long classifierID)
  {
    List<int> intList = new List<int>(1);
    foreach (ClassifierToObjTypeStructure objTypeStructure in this.classifToObjTypeCache)
    {
      if (objTypeStructure.ClassifierID == classifierID && !intList.Contains(objTypeStructure.ObjectTypeID))
        intList.Add(objTypeStructure.ObjectTypeID);
    }
    return intList.Count > 0 ? intList.ToArray() : (int[]) null;
  }

  private void AddToClassifierToObjTypeCache(IUserSession session, IDBObjectCollection objColl)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad00e8f-306c-11d8-b4e9-00304f19f545");
    DataTable dataTable = objColl.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(attributeTypeId, RelationalOperators.In, (object) new int[2]
      {
        3,
        4
      }, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 }));
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      this.AddClassifierToCache(session, Convert.ToInt64(row[0]));
  }

  protected IUserSession convertToUserSession(object usObject)
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

  public bool SetConditionStructures(
    object userSessionGuid,
    long selectionID,
    ConditionStructure[] conditionStructures)
  {
    IUserSession userSession = this.convertToUserSession(userSessionGuid);
    using (UserSessionContext.CaptureSession(userSession.SessionGUID))
      return this.selectionService.SetConditionStructures((object) userSession, selectionID, conditionStructures);
  }

  public ConditionStructure[] GetConditionStructures(
    object userSessionGuid,
    long selectionID,
    long objectID)
  {
    IUserSession userSession = this.convertToUserSession(userSessionGuid);
    using (UserSessionContext.CaptureSession(userSession.SessionGUID))
      return this.selectionService.GetConditionStructures((object) userSession, selectionID, objectID);
  }

  public ConditionStructure[] GetConditionStructures(object userSessionGuid, long selectionID)
  {
    IUserSession userSession = this.convertToUserSession(userSessionGuid);
    using (UserSessionContext.CaptureSession(userSession.SessionGUID))
      return this.selectionService.GetConditionStructures((object) userSession, selectionID);
  }

  public void UpdateCashe(object userSessionGuid, long selectionID)
  {
    IUserSession userSession = this.convertToUserSession(userSessionGuid);
    using (UserSessionContext.CaptureSession(userSession.SessionGUID))
      this.selectionService.UpdateCashe((object) userSession, selectionID);
  }

  public void UpdateCashe(object userSessionGuid)
  {
    IUserSession userSession = this.convertToUserSession(userSessionGuid);
    using (UserSessionContext.CaptureSession(userSession.SessionGUID))
      this.selectionService.UpdateCashe((object) userSession);
  }

  public void ClearCashe() => this.selectionService.ClearCashe();

  internal static bool isClassifier(int objectTypeID)
  {
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objectTypeID);
    return objectTypeGuid == DBClassifierCreator.ClassifFolderGuid || objectTypeGuid == DBClassifierCreator.ClassifPersonGuid || objectTypeGuid == DBClassifierCreator.ClassifCommonGuid;
  }

  public IObjectClassificator GetObjectClassificator(object userSessionGuid, long classifierID)
  {
    IUserSession userSession = this.convertToUserSession(userSessionGuid);
    IDBObject childClassifier = userSession.GetObject(classifierID);
    if (!SelectionSrvService.isClassifier(childClassifier.ObjectType))
      return (IObjectClassificator) null;
    bool obligatoryCalculated = false;
    long rootClassifier = (ServerServices.GetService(typeof (ISelectionsService)) as ISelectionsService).GetRootClassifier((object) userSession, childClassifier);
    IDBAttribute attributeByGuid = (rootClassifier != 0L ? (IDBAttributable) userSession.GetObject(rootClassifier) : (IDBAttributable) childClassifier).GetAttributeByGuid(new Guid("cad001d8-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid != null && Convert.ToInt32(attributeByGuid.Value) == 1)
      obligatoryCalculated = true;
    return (IObjectClassificator) new ObjectClassificator(userSession as UserSession, obligatoryCalculated, classifierID);
  }

  public void IncludeObjects(object userSessionGuid, long selectionID, long[] objectIDs)
  {
    IUserSession userSession = this.convertToUserSession(userSessionGuid);
    IDBObject selection = userSession.GetObject(selectionID);
    this.ClassifierProcessorAdd(userSession as UserSession, selection, objectIDs);
  }

  public void IncludeObjects(object userSessionGuid, Guid selectionGuid, long[] objectIDs)
  {
    IUserSession userSession = this.convertToUserSession(userSessionGuid);
    IDBObject selection = userSession.GetObject(selectionGuid);
    this.ClassifierProcessorAdd(userSession as UserSession, selection, objectIDs);
  }

  private void ClassifierProcessorAdd(
    UserSession userSession,
    IDBObject selection,
    long[] objectIDs)
  {
    IDBAttribute attributeByGuid = selection.GetAttributeByGuid(DBClassifierCreator.ClassifFolderKeyGuid);
    string folderKey = attributeByGuid != null ? Convert.ToString(attributeByGuid.Value) : string.Empty;
    userSession.StartTransaction();
    try
    {
      if (folderKey.Length >= 2)
        ClassifierProcessor.DeleteFromClassifier(userSession, folderKey.Substring(0, 2), objectIDs);
      ClassifierProcessor.Add(userSession, selection.ObjectID, objectIDs, folderKey);
      userSession.Commit();
    }
    catch
    {
      userSession.Rollback();
      throw;
    }
    if (this.AfterClassifyObjectsEvent == null)
      return;
    IDBObject classifier = userSession.GetObject(this.GetRootClassifier((object) userSession, selection), false);
    this.AfterClassifyObjectsEvent((IUserSession) userSession, classifier, selection, objectIDs);
  }

  public event OnClassifyObjectsHandler AfterClassifyObjectsEvent;

  public void ExcludeObjects(object userSessionGuid, long selectionID, long[] objectIDs)
  {
    ClassifierProcessor.Delete(this.convertToUserSession(userSessionGuid) as UserSession, selectionID, objectIDs);
  }

  public void ExcludeObjectsByID(object userSessionGuid, long selectionID, long[] IDs)
  {
    ClassifierProcessor.DeleteByID(this.convertToUserSession(userSessionGuid) as UserSession, selectionID, IDs);
  }

  public bool ExistsObject(object userSessionGuid, long selectionID, long objectID)
  {
    return ClassifierProcessor.Exists(this.convertToUserSession(userSessionGuid) as UserSession, selectionID, objectID);
  }

  public long[] ExistsObjectsID(object userSessionGuid, long folderID, long[] objectIDs)
  {
    return ClassifierProcessor.ExistsObjectsID(this.convertToUserSession(userSessionGuid) as UserSession, folderID, objectIDs);
  }

  public void SetShowInternalFolders(bool newValue)
  {
    this.selectionService.SetShowInternalFolders(newValue);
  }

  public bool GetShowInternalFolders() => this.selectionService.GetShowInternalFolders();

  public long GetClassifierForObject(object userSession, long ID)
  {
    IDbManager dataManager = (this.convertToUserSession(userSession) as UserSession).DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_FOLDER_ID FROM IMS_SELECTIONS WHERE F_ID = :f_id", dataManager.Parameter("f_id", (object) ID));
    return dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : -1L;
  }

  public string GenerateNextTopLevelKey(object userSession)
  {
    IUserSession userSession1 = this.convertToUserSession(userSession);
    IDBObjectType objectType = userSession1.GetObjectType(new Guid("cad00157-306c-11d8-b4e9-00304f19f545"));
    return this.GenerateNextTopLevelKey((object) userSession1, objectType.ObjectType);
  }

  public string GenerateNextTopLevelKey(object userSession, int objTypeID)
  {
    IUserSession userSession1 = this.convertToUserSession(userSession);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545")
    });
    DataTable dataTable;
    if (objTypeID == MetaDataHelper.GetObjectTypeID("cad0014e-306c-11d8-b4e9-00304f19f545") || objTypeID == MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545") || objTypeID == MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545"))
    {
      dataTable = userSession1.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad0014e-306c-11d8-b4e9-00304f19f545")).Select(paramSet);
      IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("SelectionSrvService.GenerateKey");
      try
      {
        sessionTemporaryClone.ShowPersonalObjects = true;
        IDBObjectCollection objectCollection = sessionTemporaryClone.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545"));
        DataSetProcessor.AddTable(dataTable, objectCollection.Select(paramSet), true);
      }
      finally
      {
        sessionTemporaryClone?.Logout("SelectionSrvService.GenerateKey");
      }
    }
    else
      dataTable = userSession1.GetObjectCollection(objTypeID).SelectWithLocalObjects(paramSet);
    return this.GetNextKeyFromValues(dataTable, string.Empty);
  }

  public string GenerateNextClassifierKey(
    object userSession,
    int parentTypeID,
    string parentKey,
    int objTypeID)
  {
    IUserSession userSession1 = this.convertToUserSession(userSession);
    if (objTypeID == -1)
      throw new Exception("Не указан тип объектов, для объекта которого генерируется ключ папки классификатора");
    parentKey = !(parentKey == string.Empty) ? parentKey.Substring(0, parentKey.Length - parentKey.Length % 2) : throw new Exception(LocalizationHolder.rm.GetString("Kernel_1138"));
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545");
    if (!(userSession1.GetObjectType(objTypeID).Attributes.GetAttributeByID(attributeTypeId) is IDBAttributeType4Object attributeById))
      return string.Empty;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(attributeTypeId, RelationalOperators.StartString, (object) parentKey, LogicalOperators.AND, 0, true)
    }, new object[1]{ (object) attributeTypeId });
    DataTable dataTable = (DataTable) null;
    if (attributeById.UniqueMode == UniqueValueModes.TypeOnly || attributeById.UniqueMode == UniqueValueModes.VerTypeOnly)
    {
      dataTable = userSession1.GetObjectCollection(objTypeID).SelectWithLocalObjects(paramSet);
    }
    else
    {
      DataTable applicabilitiesList = userSession1.GetRelationsApplicabilityCollection().GetApplicabilitiesList(userSession1.IdentHelper.SortedRelationTypeID, -1, parentTypeID);
      for (int index = 0; index < applicabilitiesList.Rows.Count; ++index)
      {
        IDBObjectCollection objectCollection = userSession1.GetObjectCollection(Convert.ToInt32(applicabilitiesList.Rows[index]["F_OBJECT_TYPE"]));
        if (index == 0)
          dataTable = objectCollection.SelectWithLocalObjects(paramSet);
        else
          DataSetProcessor.AddTable(dataTable, objectCollection.SelectWithLocalObjects(paramSet), true);
      }
    }
    return this.GetNextKeyFromValues(dataTable, parentKey);
  }

  private string GetNextKeyFromValues(DataTable tableValues, string parentKey)
  {
    string empty = string.Empty;
    if (tableValues.Rows.Count > 0)
    {
      List<string> stringList = new List<string>(tableValues.Rows.Count);
      int num = parentKey.Length + 2;
      foreach (DataRow row in (InternalDataCollectionBase) tableValues.Rows)
      {
        string str = Convert.ToString(row[0]);
        if (str.Length == num)
          stringList.Add(str);
      }
      if (stringList.Count > 0)
      {
        stringList.Sort(new Comparison<string>(string.CompareOrdinal));
        empty = stringList[stringList.Count - 1];
      }
    }
    string str1 = empty.Length > parentKey.Length ? empty.Substring(parentKey.Length, 2) : string.Empty;
    return parentKey + ClassifierKeyValueGenerator.GetNextKeyValue(str1);
  }

  public void DisableConditionStructures(long selectionID, List<int> conditionIndexes)
  {
  }

  public bool IsEnabledConditionStructure(long selectionID, int conditionIndex) => true;

  public bool CanUpperMemo(object userSession)
  {
    return (this.convertToUserSession(userSession) as UserSession).DataManager.DataProvider.CanUpperMemo;
  }

  public void SetTemporaryValues(long selectionID, List<object[]> values)
  {
  }

  public List<object[]> GetTemporaryValues(long selectionID) => (List<object[]>) null;

  public bool IsTemporaryValuesPresent(long selectionID) => false;

  public void RemoveTemporaryValues(long selectionID)
  {
  }

  public long GetRootClassifier(object userSession, long childClassifierID)
  {
    IUserSession userSession1 = this.convertToUserSession(userSession);
    IDBObject childClassifier = userSession1.GetObject(childClassifierID);
    return this.GetRootClassifier((object) userSession1, childClassifier);
  }

  public long GetRootClassifier(object userSession, IDBObject childClassifier)
  {
    IUserSession userSession1 = this.convertToUserSession(userSession);
    IDBAttribute attributeByGuid = childClassifier.GetAttributeByGuid(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null)
    {
      IDBRelationCollection relationCollection = userSession1.GetRelationCollection(MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545")));
      if (userSession1.UserID == userSession1.IdentHelper.SystemID)
        (relationCollection as DBRelationCollection)._ShowPersonalObjects = true;
      return this.GetRootClassifierFromRelation(userSession1, relationCollection, childClassifier.ObjectID, childClassifier.ID, childClassifier.ObjectType);
    }
    string str = attributeByGuid.Value.ToString();
    if (str.Length <= 2)
      return childClassifier.ObjectID;
    string conditionValue = str.Substring(0, 2);
    IDBObjectCollection objectCollection = userSession1.GetObjectCollection(MetaDataHelper.GetObjectTypeID(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")));
    if (userSession1.UserID == userSession1.IdentHelper.SystemID)
      (objectCollection as DBObjectCollection)._ShowPersonalObjects = true;
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0);
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    };
    DataTable dataTable = objectCollection.SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, columns));
    if (dataTable.Rows.Count == 1)
      return Convert.ToInt64(dataTable.Rows[0][0]);
    if (dataTable.Rows.Count <= 1)
      return 0;
    IDBRelationCollection relationCollection1 = userSession1.GetRelationCollection(MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545")));
    if (userSession1.UserID == userSession1.IdentHelper.SystemID)
      (relationCollection1 as DBRelationCollection)._ShowPersonalObjects = true;
    return this.GetRootClassifierFromRelation(userSession1, relationCollection1, childClassifier.ObjectID, childClassifier.ID, childClassifier.ObjectType);
  }

  private long GetRootClassifierFromRelation(
    IUserSession session,
    IDBRelationCollection rellColls,
    long childClassifObjectID,
    long childClassifID,
    int childObjectType)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00150-306c-11d8-b4e9-00304f19f545");
    if (objectTypeId == childObjectType)
      rellColls.ChildObjectTypes = (IList<int>) new int[2]
      {
        MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545"),
        objectTypeId
      };
    DataTable dataTable = rellColls.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
    {
      (object) -2,
      (object) -3,
      (object) -7
    }), childClassifID);
    return dataTable.Rows.Count > 0 ? this.GetRootClassifierFromRelation(session, rellColls, Convert.ToInt64(dataTable.Rows[0][0]), Convert.ToInt64(dataTable.Rows[0][1]), Convert.ToInt32(dataTable.Rows[0][2])) : childClassifObjectID;
  }

  public Dictionary<int, List<long>> IncludedObjects(object userSession, long selectionID)
  {
    IDbManager dataManager = (this.convertToUserSession(userSession) as UserSession).DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("select a.F_OBJECT_ID, b.F_OBJECT_TYPE from IMS_SELECTIONS a, IMS_OBJECTS b where a.F_OBJECT_ID = b.F_OBJECT_ID and a.F_FOLDER_ID = :v_selection_id ORDER BY b.F_OBJECT_TYPE", dataManager.Parameter("v_selection_id", (object) selectionID));
    if (dataTable.Rows.Count == 0)
      return (Dictionary<int, List<long>>) null;
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    int key = -1;
    List<long> source = (List<long>) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row[1]);
      if (key == -1 || key != int32)
      {
        if (source != null)
          dictionary.Add(key, source.ToList<long>());
        key = int32;
        source = new List<long>();
      }
      source.Add(Convert.ToInt64(row[0]));
    }
    dictionary.Add(key, source);
    return dictionary;
  }

  public Guid StartCopyStructure(object userSession, string name, long prototypeID, long parentID)
  {
    SelectionStructureCopier selectionStructureCopier = new SelectionStructureCopier(this.convertToUserSession(userSession), name, prototypeID, parentID);
    this._selectionStructureCopiers.TryAdd(selectionStructureCopier.GUID, selectionStructureCopier);
    selectionStructureCopier.Start();
    return selectionStructureCopier.GUID;
  }

  public void StopCopyStructure(Guid copierGuid)
  {
    SelectionStructureCopier selectionStructureCopier;
    if (!this._selectionStructureCopiers.TryRemove(copierGuid, out selectionStructureCopier))
      return;
    selectionStructureCopier.Stop();
  }

  public StructureCopierStateInfo GetCopyStructureInfo(Guid copierGuid)
  {
    SelectionStructureCopier selectionStructureCopier;
    return this._selectionStructureCopiers.TryGetValue(copierGuid, out selectionStructureCopier) ? selectionStructureCopier.StateInfo : (StructureCopierStateInfo) null;
  }

  public string GenerateNextClassifierKey(object userSession, int objType, long id)
  {
    IUserSession userSession1 = this.convertToUserSession(userSession);
    IDBRelationCollection relationCollection = userSession1.GetRelationCollection(userSession1.IdentHelper.SortedRelationTypeID);
    relationCollection.ChildObjectTypes = (IList<int>) MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(MetaDataHelper.GetObjectTypeID(SelectionSrvService.isClassifier(objType) ? new Guid("cad00157-306c-11d8-b4e9-00304f19f545") : Intermech.Imbase.Consts.ImbaseRootObjectTypeGUID));
    DataTable dataTable = relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    }), id);
    string nextClassifierKey = string.Empty;
    if (dataTable.Rows.Count == 0)
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(DBClassifierCreator.ClassifCommonGuid);
      childrenIdRecursive.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(DBClassifierCreator.ClassifPersonGuid));
      childrenIdRecursive.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(DBClassifierCreator.ImbaseCatalogTypeGUID));
      if (childrenIdRecursive.Contains(objType))
        nextClassifierKey = this.GenerateNextTopLevelKey((object) userSession1, objType);
    }
    else
    {
      string parentKey = Convert.ToString(dataTable.Rows[0][0]);
      nextClassifierKey = parentKey != string.Empty ? this.GenerateNextClassifierKey((object) userSession1, Convert.ToInt32(dataTable.Rows[0][1]), parentKey, objType) : string.Empty;
    }
    return nextClassifierKey;
  }
}
