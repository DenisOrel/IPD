// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ArchiveService
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Signs.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Archives.Server;

public class ArchiveService : LongLifeObject, IArchiveService
{
  public bool CheckArticlesAccessMode;

  public bool CopyArcVisibility { get; private set; }

  public ArchiveService(IUserSession session) => this.LoadInternalSettings(session);

  internal void LoadInternalSettings(IUserSession session)
  {
    this.CheckArticlesAccessMode = session.Configurations.ReadBool("ARCHIVES", "SECURITY", "ART_ACCESS", false, DBConfigMode.GlobalOnly);
    this.CopyArcVisibility = session.Configurations.ReadBool("ARCHIVES", "SECURITY", "COPY_ARC_VISIBLE", false, DBConfigMode.GlobalOnly);
  }

  public string ValidateDocsStorageID(long arcID, Guid sessionGuid)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      throw new KernelException($"Сессия {sessionGuid} не найдена");
    IDBObject dbObject1 = sessionById.IsAdmin ? sessionById.GetObject(arcID) : throw new KernelExceptionID(126);
    IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(new Guid("cad0005c-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || attributeByGuid.AsInteger == 0L)
      return $"Для архива '{dbObject1.Caption}' файловый шкаф не задан.";
    long asInteger = attributeByGuid.AsInteger;
    IDBObject dbObject2 = sessionById.GetObject(asInteger);
    IDBObjectCollection objectCollection = sessionById.GetObjectCollection(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
    objectCollection.ShowAllModifications = true;
    DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(SystemGUIDs.attributeArchive, RelationalOperators.Equal, (object) arcID, LogicalOperators.NONE, 0)
    }, new object[1]{ (object) -2 }));
    if (dataTable.Rows.Count == 0)
      return $"В архиве '{dbObject1.Caption}' документы не найдены.";
    List<long> longList = new List<long>();
    IDbManager dataManager = sessionById.DataManager;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      IDBObject dbObject3 = sessionById.GetObject(Convert.ToInt64(row[0]), false);
      if (dbObject3 != null)
      {
        for (int AttrIndex = 0; AttrIndex < dbObject3.Attributes.Count; ++AttrIndex)
        {
          IDBAttribute attribute = dbObject3.Attributes[AttrIndex];
          if (attribute.DataType == FieldTypes.ftBlob || attribute.DataType == FieldTypes.ftFile)
          {
            for (int index = 0; index < attribute.ValuesCount; ++index)
            {
              attribute.Index = index;
              if (Convert.ToInt64(attribute.AsDouble) != asInteger)
                longList.Add(dbObject3.ObjectID);
            }
          }
        }
        if (dataManager.ExecuteDataTable($"SELECT F_ATTRIBUTE_ID FROM IMS_OBJ_SNAPATTRS S WHERE F_OBJECT_ID = :objID AND F_DOUBLE_VALUE <> :storID AND F_ATTRIBUTE_ID IN (SELECT A.F_ATTRIBUTE_ID FROM IMS_ATTRIBUTES A WHERE A.F_ATTRIBUTE_ID = S.F_ATTRIBUTE_ID AND F_ATTRIBUTE_TYPE IN ({11},{6}))", dataManager.Parameter("objID", (object) dbObject3.ObjectID), dataManager.Parameter("storID", (object) asInteger)).Rows.Count > 0)
          longList.Add(dbObject3.ObjectID);
      }
    }
    if (longList.Count == 0)
      return $"Все документы архива '{dbObject1.Caption}' находятся в файловом шкафу '{dbObject2.Caption}'.";
    throw new ObjectsFoundException($"В архиве '{dbObject1.Caption}' найдены документы, файлы которых не размещены в файловом шкафу '{dbObject2.Caption}':", "Проверка размещения файлов и двоичных данных архива в файловом шкафу", longList.ToArray());
  }

  public int RemoveDocs2ArcStorage(long arcID, Guid sessionGuid)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      throw new KernelException($"Сессия {sessionGuid} не найдена");
    IDBObject dbObject1 = sessionById.IsAdmin ? sessionById.GetObject(arcID) : throw new KernelExceptionID(126);
    IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(new Guid("cad0005c-306c-11d8-b4e9-00304f19f545"));
    long num1 = attributeByGuid != null && attributeByGuid.AsInteger != 0L ? attributeByGuid.AsInteger : throw new KernelException($"Для архива '{dbObject1.Caption}' файловый шкаф не задан.");
    sessionById.GetObject(num1);
    IDBObjectCollection objectCollection = sessionById.GetObjectCollection(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
    objectCollection.ShowAllModifications = true;
    DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(SystemGUIDs.attributeArchive, RelationalOperators.Equal, (object) arcID, LogicalOperators.AND, 0)
    }, new object[1]{ (object) -2 }));
    int num2 = 0;
    if (dataTable.Rows.Count > 0)
    {
      IDbManager dataManager = sessionById.DataManager;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        IDBObject dbObject2 = sessionById.GetObject(Convert.ToInt64(row[0]), false);
        if (dbObject2 != null)
        {
          IDBLifecycleLevelType lifecycleLevel = sessionById.GetLifecycleLevel((dbObject2 as IDBLifecycleLevel).LevelID, true);
          if (lifecycleLevel.StorageID == 0L || lifecycleLevel.StorageID == num1)
          {
            for (int AttrIndex = 0; AttrIndex < dbObject2.Attributes.Count; ++AttrIndex)
            {
              IDBAttribute attribute = dbObject2.Attributes[AttrIndex];
              if (attribute.DataType == FieldTypes.ftBlob || attribute.DataType == FieldTypes.ftFile)
              {
                for (int index = 0; index < attribute.ValuesCount; ++index)
                {
                  attribute.Index = index;
                  if (Convert.ToInt64(attribute.AsDouble) != num1)
                  {
                    (attribute as IBlobWriterEx).RemoveToStorage(num1);
                    ++num2;
                  }
                }
              }
            }
          }
        }
      }
    }
    return num2;
  }

  public bool GetAtriclesAccessMode() => this.CheckArticlesAccessMode;

  public bool CheckArchiveSettings(
    long archiveID,
    ArchiveTypesUsingMode mode,
    List<int> archiveTypesIDs,
    Guid sessionGuid)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return false;
    switch (mode)
    {
      case ArchiveTypesUsingMode.AnyType:
        return true;
      case ArchiveTypesUsingMode.PermittedTypes:
        if (archiveTypesIDs.Count == 0)
          throw new Exception(string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_21")));
        if (this.DocsInArchiveAreProper((IUserSession) sessionById, archiveID, archiveTypesIDs, true))
          return true;
        break;
      case ArchiveTypesUsingMode.ForbiddenTypes:
        if (archiveTypesIDs.Count == 0)
          return true;
        if (archiveTypesIDs.Count == 1 && archiveTypesIDs[0] == ConstsHolder.DocTypeID)
          throw new Exception(string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_22")));
        if (this.DocsInArchiveAreProper((IUserSession) sessionById, archiveID, archiveTypesIDs, false))
          return true;
        break;
    }
    return false;
  }

  public void UpdateArchiveStructure(
    List<long> arcIDs,
    List<int> attrTypeIDs,
    ArchiveStructureChangeAction action,
    Guid sessionID)
  {
    if (!(UserSession.GetSessionByID(sessionID) is UserSession sessionById) || arcIDs.Count <= 0)
      return;
    sessionById.StartTransaction();
    try
    {
      if (action == ArchiveStructureChangeAction.AddNewToArchive)
      {
        MetaDataHelper.SyncMetadata(sessionById.CacheDataSet, true);
        action = ArchiveStructureChangeAction.AddToArchive;
      }
      if (action == ArchiveStructureChangeAction.AddToArchive)
        this.AddCurrentAttributeType(arcIDs, attrTypeIDs, sessionById);
      else if (action == ArchiveStructureChangeAction.DeleteFromArchive || action == ArchiveStructureChangeAction.DeleteFromArchiveAndDocs)
        this.DeleteAttributeTypesFromArchiveStructureAttrAndDefaultAttrValuesAttribute(arcIDs, attrTypeIDs, action == ArchiveStructureChangeAction.DeleteFromArchiveAndDocs, sessionById);
      sessionById.Commit();
    }
    catch (Exception ex)
    {
      sessionById.Rollback();
      throw;
    }
  }

  public List<int> GetArchivePermittedTypesIDs(
    long archiveID,
    bool isNeedChildsIDs,
    Guid sessionGuid)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      throw new KernelExceptionID(210, (object) "ArchiveServiceGetPermittedTypes");
    List<string> list = ((IEnumerable<string>) sessionById.GetObjectAttributeByID(archiveID, MetaDataHelper.GetAttributeTypeID(ConstsHolder.AttributeObjectTypeGuids)).Descriptions).ToList<string>();
    List<int> permittedTypesIds = new List<int>();
    foreach (string str in list)
    {
      if (GuidHelper.IsGuid(str))
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(str));
        if (isNeedChildsIDs)
        {
          List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId);
          List<int> childrenIdRecursive2 = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(objectTypeId);
          permittedTypesIds.AddRange(childrenIdRecursive1.Union<int>((IEnumerable<int>) childrenIdRecursive2));
        }
        else
          permittedTypesIds.Add(objectTypeId);
      }
    }
    return permittedTypesIds;
  }

  public void DeleteAttributesFromDefaultAttrValuesAttribute(
    long archiveId,
    List<int> attrTypeIDsForDeleting,
    Guid sessionID)
  {
    IDBObject dbObject = (UserSession.GetSessionByID(sessionID) as UserSession).GetObject(archiveId);
    IDBAttribute attributeById = dbObject.GetAttributeByID(ConstsHolder.ArchiveStructureAttrValuesByDefaultAttrID);
    if (attributeById == null)
      return;
    List<object> collection = new List<object>((IEnumerable<object>) attributeById.Values);
    List<object> objectList = new List<object>((IEnumerable<object>) collection);
    foreach (int attrTypeID in attrTypeIDsForDeleting)
    {
      Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeID);
      foreach (object obj in collection)
      {
        if (obj.ToString().Contains(attributeTypeGuid.ToString()))
          objectList.Remove(obj);
      }
    }
    if (objectList.Count == 0)
      objectList.Add((object) string.Empty);
    dbObject.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(ConstsHolder.ArchiveStructureAttrValuesByDefaultAttrID)
      {
        Values = objectList.ToArray()
      }
    }, false, true);
  }

  public Dictionary<Guid, object> GetArchiveStructureDefaultAttrValues(
    long archiveId,
    Guid sessionID)
  {
    IDBObject dbObject = (UserSession.GetSessionByID(sessionID) as UserSession).GetObject(archiveId);
    Dictionary<Guid, object> defaultAttrValues = new Dictionary<Guid, object>();
    int valuesByDefaultAttrId = ConstsHolder.ArchiveStructureAttrValuesByDefaultAttrID;
    IDBAttribute attributeById = dbObject.GetAttributeByID(valuesByDefaultAttrId);
    if (attributeById.IsNull && attributeById.ValuesCount == 1)
      return defaultAttrValues;
    foreach (object obj in attributeById.Values)
    {
      string str1 = obj.ToString();
      int length = str1.IndexOf(ConstsHolder.Separator, StringComparison.Ordinal);
      string str2 = str1.Substring(0, length);
      if (GuidHelper.IsGuid(str2))
      {
        Guid key = new Guid(str2);
        string str3 = str1.Substring(length + 1);
        defaultAttrValues.Add(key, (object) str3);
      }
    }
    return defaultAttrValues;
  }

  public bool CanPlaceToArchive(IDBObject archiveObject, IDBObject obj, out string errorMessage)
  {
    bool archive = MetaDataHelper.GetAttribute4ObjectType(obj.ObjectType, ConstsHolder.ArchiveAttrID) != null;
    if (archive)
    {
      try
      {
        this.ValidatePlaceToArchive(archiveObject, obj);
        errorMessage = string.Empty;
        if (obj.isParentType(ConstsHolder.DocTypeGuid))
        {
          if (!obj.IsCreationMode)
          {
            archive = (ServerServices.GetService(typeof (ISignsService)) as ISignsService).CheckSigns(new long[1]
            {
              obj.ObjectID
            }, archiveObject.ObjectID, (GraphsSet) null, (obj as DBObject).UserSession.SessionGUID, true, false, out errorMessage, out object[] _);
            if (!archive)
            {
              if (errorMessage == null)
                errorMessage = "Для выполнения действия отсутствуют требуемые подписи.";
            }
          }
        }
      }
      catch (Exception ex)
      {
        errorMessage = ex.Message;
        archive = false;
      }
    }
    else
      errorMessage = $"Объекты типа '{MetaDataHelper.GetObjectTypeName(obj.ObjectType)}' нельзя помещать в архивы.";
    return archive;
  }

  public void ValidatePlaceToArchive(Guid sessionID, long archiveID, long objectID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionID) as UserSession;
    IDBObject dbObject = sessionById.GetObject(objectID);
    this.ValidatePlaceToArchive(sessionById.GetObject(archiveID), dbObject);
  }

  public void ValidatePlaceToArchive(IDBObject archiveObject, IDBObject obj)
  {
    UserSession userSession = (obj as DBSessionable).UserSession;
    if (!MetaDataHelper.IsObjectTypeChildOf(obj.ObjectType, ConstsHolder.DocTypeID))
      return;
    try
    {
      this.ThrowExceptionIfObjectCheckedOutByAnotherUser(userSession, obj);
      this.CheckAccessForRemoveDocumentFromArchive(obj, (IUserSession) userSession);
      this.CheckAccessForAddingDocumentToArchive(archiveObject.ObjectID, obj, (IUserSession) userSession);
      this.ThrowExceptionIfObjectTypeCantBePlacedInArchive((IUserSession) userSession, archiveObject, obj.TypeID);
    }
    finally
    {
      obj.ClearObjectAccessCache();
    }
  }

  private static bool IsPermittedType(int objectTypeID, List<int> permittedTypesIDs)
  {
    foreach (int permittedTypesId in permittedTypesIDs)
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(permittedTypesId);
      if (objectTypeID == permittedTypesId || childrenIdRecursive.Contains(objectTypeID))
        return true;
    }
    return false;
  }

  private static bool IsInPermittedTypesList(List<int> permittedTypesIDs, int[] objectTypeIDs)
  {
    foreach (int objectTypeId in objectTypeIDs)
    {
      if (!ArchiveService.IsPermittedType(objectTypeId, permittedTypesIDs))
        return false;
    }
    return true;
  }

  private void ThrowExceptionIfObjectTypeCantBePlacedInArchive(
    IUserSession session,
    IDBObject archiveObject,
    int objTypeId)
  {
    int asInteger = (int) archiveObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(ConstsHolder.ArchiveTypesUsingModeGuid)).AsInteger;
    List<int> permittedTypesIds = this.GetArchivePermittedTypesIDs(archiveObject.ObjectID, true, session.SessionGUID);
    switch (asInteger)
    {
      case 1:
        if (ArchiveService.IsInPermittedTypesList(permittedTypesIds, new int[1]
        {
          objTypeId
        }))
          break;
        throw new Exception(string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_20"), (object) archiveObject.Caption, (object) MetaDataHelper.GetObjectTypeName(objTypeId)));
      case 2:
        if (!ArchiveService.IsInPermittedTypesList(permittedTypesIds, new int[1]
        {
          objTypeId
        }))
          break;
        throw new Exception(string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_20"), (object) archiveObject.Caption, (object) MetaDataHelper.GetObjectTypeName(objTypeId)));
    }
  }

  private void CheckAccessForAddingDocumentToArchive(
    long newArchiveId,
    IDBObject documentToPlaceInArchiveObject,
    IUserSession session)
  {
    if (!(session.GetObject(newArchiveId, false) is ArchiveDBObject archiveDbObject))
      return;
    archiveDbObject._ArchivedObject = documentToPlaceInArchiveObject;
    archiveDbObject.AccessChecker.CheckAccess(ActionType.Create);
  }

  private void CheckAccessForRemoveDocumentFromArchive(
    IDBObject documentToPlaceInArchive,
    IUserSession session)
  {
    if (documentToPlaceInArchive.IsCreationMode)
      return;
    IDBAttribute attributeById = documentToPlaceInArchive.GetAttributeByID(ConstsHolder.ArchiveAttrID);
    if (attributeById == null || attributeById.IsNull || !(session.GetObject(attributeById.AsInteger, false) is ArchiveDBObject archiveDbObject))
      return;
    archiveDbObject._ArchivedObject = documentToPlaceInArchive;
    archiveDbObject.AccessChecker.CheckAccess(ActionType.Remove);
  }

  private void ThrowExceptionIfObjectCheckedOutByAnotherUser(
    UserSession session,
    IDBObject objectToPlaceInArchive)
  {
    if (objectToPlaceInArchive.CheckoutBy != 0L && objectToPlaceInArchive.CheckoutBy != session.UserID)
      throw new Exception(string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_5"), (object) objectToPlaceInArchive.Caption, (object) session.DBCache.GetObjectInfo(session.DataManager, objectToPlaceInArchive.CheckoutBy).Caption));
  }

  private bool DocsInArchiveAreProper(
    IUserSession session,
    long archiveID,
    List<int> archiveTypesIDs,
    bool typesArePermitted)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(ConstsHolder.DocTypeID);
    int docsNumberInArchive1 = this.GetDocsNumberInArchive(objectCollection, archiveID);
    int docsNumberInArchive2 = this.GetProperDocsNumberInArchive(objectCollection, archiveID, archiveTypesIDs);
    return typesArePermitted ? docsNumberInArchive1 == docsNumberInArchive2 : docsNumberInArchive2 == 0;
  }

  private int GetProperDocsNumberInArchive(
    IDBObjectCollection docObjects,
    long archiveID,
    List<int> archiveTypesIDs)
  {
    List<int> intList = new List<int>();
    foreach (int archiveTypesId in archiveTypesIDs)
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(archiveTypesId);
      intList.AddRange((IEnumerable<int>) childrenIdRecursive);
    }
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(ConstsHolder.ArchiveAttrID, RelationalOperators.Equal, (object) archiveID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object),
      new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    });
    return docObjects.Select(paramSet).Rows.Count;
  }

  private int GetDocsNumberInArchive(IDBObjectCollection docObjects, long archiveID)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.ArchiveAttrID, RelationalOperators.Equal, (object) archiveID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    });
    return docObjects.Select(paramSet).Rows.Count;
  }

  private void AddCurrentAttributeType(
    List<long> arcIDs,
    List<int> attrTypeIDs,
    UserSession session)
  {
    foreach (long arcId in arcIDs)
    {
      IDBObject dbObject1 = session.GetObject(arcId);
      List<object> objectList = new List<object>((IEnumerable<object>) dbObject1.GetAttributeByID(ConstsHolder.ArchiveStructureAttrID).Values);
      DataTable dataTable = session.GetObjectCollection(ConstsHolder.DocTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(ConstsHolder.ArcAttrGuid, RelationalOperators.Equal, (object) arcId, LogicalOperators.NONE, 0)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      }));
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (int64 != 0L)
          {
            IDBObject dbObject2 = session.GetObject(int64);
            if (dbObject2 != null)
            {
              foreach (int attrTypeId in attrTypeIDs)
                dbObject2.Attributes.AddAttribute(attrTypeId, false);
            }
          }
        }
      }
      foreach (int attrTypeId in attrTypeIDs)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
        if (!objectList.Contains((object) attributeTypeGuid.ToString()))
          objectList.Add((object) attributeTypeGuid.ToString());
      }
      if (objectList.Contains((object) string.Empty))
        objectList.Remove((object) string.Empty);
      if (objectList.Contains((object) DBNull.Value))
        objectList.Remove((object) DBNull.Value);
      dbObject1.SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(ConstsHolder.ArchiveStructureAttrID)
        {
          Values = objectList.ToArray()
        }
      }, false, true);
    }
  }

  private void DeleteAttributeTypesFromArchiveStructureAttrAndDefaultAttrValuesAttribute(
    List<long> archiveIDs,
    List<int> attrTypeIDsForDeleting,
    bool deleteFromDocs,
    UserSession session)
  {
    foreach (long archiveId in archiveIDs)
    {
      if (deleteFromDocs)
      {
        DataTable dataTable = session.GetObjectCollection(ConstsHolder.DocTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(ConstsHolder.ArcAttrGuid, RelationalOperators.Equal, (object) archiveId, LogicalOperators.NONE, 0)
        }, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }));
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            if (int64 != 0L)
            {
              IDBObject dbObject = session.GetObject(int64);
              if (dbObject != null)
              {
                foreach (int attributeID in attrTypeIDsForDeleting)
                  dbObject.GetAttributeByID(attributeID)?.Delete(0L);
              }
            }
          }
        }
      }
      IDBObject dbObject1 = session.GetObject(archiveId);
      List<object> objectList = new List<object>((IEnumerable<object>) dbObject1.GetAttributeByID(ConstsHolder.ArchiveStructureAttrID).Values);
      foreach (int attrTypeID in attrTypeIDsForDeleting)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeID);
        if (objectList.Contains((object) attributeTypeGuid.ToString()))
          objectList.Remove((object) attributeTypeGuid.ToString());
      }
      if (objectList.Count == 0)
        objectList.Add((object) string.Empty);
      dbObject1.SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(ConstsHolder.ArchiveStructureAttrID)
        {
          Values = objectList.ToArray()
        }
      }, false, true);
      this.DeleteAttributesFromDefaultAttrValuesAttribute(archiveId, attrTypeIDsForDeleting, session.SessionGUID);
    }
  }
}
