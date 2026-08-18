// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBClassifier
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBClassifier(UserSession uSession, DataTable objectsTable) : DBObject(uSession, objectsTable), IDBClassifier
{
  protected override void DoDelete()
  {
    base.DoDelete();
    if (this.ObjectType != MetaDataHelper.GetObjectTypeID(DBClassifierCreator.ClassifCommonGuid) && this.ObjectType != MetaDataHelper.GetObjectTypeID(DBClassifierCreator.ClassifPersonGuid))
      return;
    (this.UserSession.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).DeleteClassifierFromCache(this.ObjectID);
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
    if (this.ObjectType != MetaDataHelper.GetObjectTypeID(DBClassifierCreator.ClassifCommonGuid) && this.ObjectType != MetaDataHelper.GetObjectTypeID(DBClassifierCreator.ClassifPersonGuid) || this.IsCreationMode || attribute.AttributeID != MetaDataHelper.GetAttributeTypeID("cad00149-306c-11d8-b4e9-00304f19f545"))
      return;
    ISelectionsService customService = this.UserSession.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
    customService.DeleteClassifierFromCache(this.ObjectID);
    customService.AddClassifierToCache((IUserSession) this.UserSession, this.ObjectID);
  }

  protected override void DoBeforeCommitCreation()
  {
    base.DoBeforeCommitCreation();
    string nextClassifierKey = (this.UserSession.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).GenerateNextClassifierKey((object) this.UserSession, this.ObjectType, this.ID);
    this.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(DBClassifierCreator.ClassifFolderKeyGuid), false, new object[1]
    {
      (object) nextClassifierKey
    });
  }

  protected override void DoAfterCommitCreation()
  {
    base.DoAfterCommitCreation();
    ISelectionsService customService = this.UserSession.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
    if (this.ObjectType != MetaDataHelper.GetObjectTypeID(DBClassifierCreator.ClassifCommonGuid) && this.ObjectType != MetaDataHelper.GetObjectTypeID(DBClassifierCreator.ClassifPersonGuid))
      return;
    customService.AddClassifierToCache((IUserSession) this.UserSession, this.ObjectID);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.IncludeInComposition, this.GetDefaultAccess(ActionType.IncludeInComposition));
    this.AccessActions.Add(ActionType.ExcludeFromComposition, this.GetDefaultAccess(ActionType.ExcludeFromComposition));
  }

  public static void RebuildKeys(IUserSession session, long[] objectIDs)
  {
    DBClassifier.RebuildKeys(session, objectIDs, string.Empty, true);
  }

  public static void RebuildKeys(
    IUserSession session,
    long[] objectIDs,
    string logFileName,
    bool throwException)
  {
    List<long> handledObjects = new List<long>();
    ISelectionsService service = ServerServices.GetService(typeof (ISelectionsService)) as ISelectionsService;
    IDBAttributeType attributeType = session.GetAttributeType(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"));
    int objectType1 = session.GetObjectType(new Guid("cad0014e-306c-11d8-b4e9-00304f19f545")).ObjectType;
    int objectType2 = session.GetObjectType(new Guid("cad0014f-306c-11d8-b4e9-00304f19f545")).ObjectType;
    int objectType3 = session.GetObjectType(new Guid("cad00150-306c-11d8-b4e9-00304f19f545")).ObjectType;
    int objectType4 = session.GetObjectType(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")).ObjectType;
    int objectType5 = session.GetObjectType(DBClassifierCreator.ImbaseCatalogTypeGUID).ObjectType;
    int objectType6 = session.GetObjectType(DBClassifierCreator.ImbaseFolderTypeGUID).ObjectType;
    int objectType7 = session.GetObjectType(DBClassifierCreator.ImbaseTableRefTypeGUID).ObjectType;
    int objectType8 = session.GetObjectType(DBClassifierCreator.ImbaseCatalogRecordTypeGUID).ObjectType;
    List<int> childrenIdRecursive1 = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(session.GetObjectType(new Guid("CAD00220-306C-11D8-B4E9-00304F19F545")).ObjectType);
    List<int> childrenIdRecursive2 = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(objectType4);
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.SortedRelationTypeID);
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) attributeType.AttributeID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    };
    for (int index = 0; index < objectIDs.Length; ++index)
    {
      long objectId = objectIDs[index];
      if (!handledObjects.Contains(objectId))
      {
        try
        {
          DataTable dataTable1 = (session as UserSession).DataManager.ExecuteDataTable($"SELECT F_OBJECT_TYPE, F_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID={objectId}");
          if (dataTable1.Rows.Count == 0)
            throw new Exception("Объект не найден в таблице IMS_OBJECTS");
          int int32 = Convert.ToInt32(dataTable1.Rows[0][0]);
          long int64 = Convert.ToInt64(dataTable1.Rows[0][1]);
          string str = Convert.ToString((session as UserSession).DataManager.ExecuteScalar($"SELECT F_STRING_VALUE FROM {(session as UserSession).DBCache.GetAttributesTableName(int32)} WHERE F_OBJECT_ID={objectId} AND F_ATTRIBUTE_ID={attributeType.AttributeID} AND F_INLIST_ID=0"));
          if (str == string.Empty)
          {
            if (int32 == objectType1 || int32 == objectType2 || int32 == objectType5)
              str = service.GenerateNextTopLevelKey((object) session, int32);
            else if (int32 == objectType8 || int32 == objectType6 || int32 == objectType7 || int32 == objectType3)
            {
              relationCollection.ChildObjectTypes = int32 == objectType3 ? (IList<int>) childrenIdRecursive2 : (IList<int>) childrenIdRecursive1;
              DataTable dataTable2 = relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, columns), int64);
              string parentKey = dataTable2.Rows.Count == 1 ? Convert.ToString(dataTable2.Rows[0][1]) : string.Empty;
              if (!(parentKey == string.Empty))
                str = service.GenerateNextClassifierKey((object) session, Convert.ToInt32(dataTable2.Rows[0][2]), parentKey, int32);
              else
                continue;
            }
          }
          if (!(str == string.Empty))
          {
            DBClassifier.SetValueRecursive(session as UserSession, relationCollection, objectId, int32, attributeType.AttributeID, str, int32 == objectType1 || int32 == objectType2 || int32 == objectType3 ? childrenIdRecursive2 : childrenIdRecursive1, handledObjects);
            handledObjects.Add(objectId);
          }
        }
        catch (Exception ex)
        {
          if (logFileName != string.Empty)
            (ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper).AddToTrace($"Ошибка при попытке перестроить ключ классификатора для объекта {objectId}: {ex.Message}", Consts.traceAlways, logFileName);
          if (throwException)
            throw;
        }
      }
    }
  }

  private static void SetValueRecursive(
    UserSession session,
    IDBRelationCollection rellColl,
    long objectID,
    int typeID,
    int folderKeyID,
    string value,
    List<int> localTypes,
    List<long> handledObjects)
  {
    DBClassifier.SetValue(session, objectID, typeID, folderKeyID, value);
    rellColl.ChildObjectTypes = (IList<int>) localTypes;
    DataTable dataTable = rellColl.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -2,
      (object) -7
    }), objectID);
    string str = string.Empty;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      str = ClassifierKeyValueGenerator.GetNextKeyValue(str);
      long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
      if (!handledObjects.Contains(int64))
      {
        DBClassifier.SetValueRecursive(session, rellColl, int64, Convert.ToInt32(dataTable.Rows[index][1]), folderKeyID, value + str, localTypes, handledObjects);
        handledObjects.Add(int64);
      }
    }
  }

  private static void SetValue(
    UserSession session,
    long objectID,
    int objectType,
    int folderKeyID,
    string value)
  {
    string attributesTableName = session.DBCache.GetAttributesTableName(objectType);
    IDbDataParameter dbDataParameter1 = session.DataManager.Parameter("v_attrID", (object) folderKeyID);
    IDbDataParameter dbDataParameter2 = session.DataManager.Parameter("v_objID", (object) objectID);
    IDbDataParameter dbDataParameter3 = session.DataManager.Parameter("v_string", (object) value);
    object obj = session.DataManager.ExecuteScalar($"SELECT F_STRING_VALUE FROM {attributesTableName} WHERE F_ATTRIBUTE_ID = :v_attrID AND F_OBJECT_ID = :v_objID AND F_INLIST_ID = 0", dbDataParameter1, dbDataParameter2);
    if (obj != null && Convert.ToString(obj).Equals(value))
      return;
    string commandText = obj == null ? $"INSERT INTO {attributesTableName} (F_ATTRIBUTE_ID, F_OBJECT_ID, F_INLIST_ID, F_STRING_VALUE) VALUES (:v_attrID, :v_objID, 0, :v_string)" : $"UPDATE {attributesTableName} SET F_STRING_VALUE = :v_string WHERE F_ATTRIBUTE_ID = :v_attrID AND F_OBJECT_ID = :v_objID AND F_INLIST_ID = 0";
    session.StartTransaction();
    try
    {
      session.DataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter2, dbDataParameter3);
      string[] updateTables = session.DBCache.GetUpdateTables(folderKeyID, objectType, -1);
      if (updateTables == null || updateTables.Length == 0)
      {
        session.Commit();
      }
      else
      {
        for (int index = 0; index < updateTables.Length; ++index)
          session.DataManager.ExecuteNonQuery($"UPDATE {updateTables[index]} SET F{folderKeyID} = :v_string WHERE F_OBJECT_ID = :v_objID", dbDataParameter3, dbDataParameter2);
        if (SelectionSrvService.isClassifier(objectType))
          session.DataManager.ExecuteNonQuery("UPDATE IMS_SELECTIONS SET F_FOLDER_KEY = :v_string WHERE F_FOLDER_ID = :v_objID", dbDataParameter3, dbDataParameter2);
        session.Commit();
      }
    }
    catch (Exception ex)
    {
      session.Rollback();
      (ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper).AddToTrace($"Ошибка при попытке перестроить ключ классификатора для объекта {objectID}: {ex.Message}", Consts.traceAlways, string.Empty);
    }
  }

  public void RebuildKeys()
  {
    DBClassifier.RebuildKeys(this.Session, new long[1]
    {
      this.ObjectID
    });
  }
}
