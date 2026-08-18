// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBObjectService : AttributableCreatorContainer, IDBObjectService
{
  private IDBObjectCreator _DBObjectCreator;

  public DBObjectService() => this._DBObjectCreator = (IDBObjectCreator) new DBObjectCreator();

  protected override string KeyFieldName => "F_OBJECT_ID";

  protected override string SystemTableName => "IMS_OBJECTS";

  public IDBObject[] GetObjects(IUserSession uSession, long[] objectIDs, bool failIfNotFound)
  {
    if (objectIDs.Length == 0)
      return new IDBObject[0];
    if (objectIDs.Length == 1)
      return new IDBObject[1]
      {
        this.GetObject(uSession, objectIDs[0], failIfNotFound, false)
      };
    string notFoundMessage = !failIfNotFound ? string.Empty : "Объект номер {0} не найден.";
    DataTable mainTable = this.GetMainTable(uSession, objectIDs, notFoundMessage);
    List<IDBObject> dbObjectList = new List<IDBObject>(mainTable.Rows.Count);
    while (mainTable.Rows.Count > 0)
    {
      int int32 = Convert.ToInt32(mainTable.Rows[0]["F_OBJECT_TYPE"]);
      Guid guid;
      IDBObjectCreator creatorByGuid = this.GetCreatorByGuid((uSession as UserSession).DBCache, int32, out guid);
      dbObjectList.Add(creatorByGuid.CreateObject(uSession, guid, mainTable));
      mainTable.Rows.RemoveAt(0);
    }
    return dbObjectList.ToArray();
  }

  public IDBObject GetObject(
    IUserSession uSession,
    long objectID,
    bool failIfNotFound,
    bool getWorkCopy)
  {
    if (objectID == 0L)
    {
      if (failIfNotFound)
        throw new ObjectNotFoundException(objectID);
      return (IDBObject) null;
    }
    IDbManager dataManager = (uSession as UserSession).DataManager;
    DataTable objectParams = dataManager.ExecuteDataTable("SELECT * FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objID", (object) objectID));
    if (getWorkCopy && objectParams.Rows.Count > 0 && objectID > 0L && Convert.ToInt64(objectParams.Rows[0]["F_CHKOUT_BY"]) == uSession.UserID)
      objectParams = dataManager.ExecuteDataTable("SELECT * FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objID", (object) -objectID));
    if (objectParams.Rows.Count == 0)
    {
      if (failIfNotFound)
        throw new ObjectNotFoundException(objectID);
      return (IDBObject) null;
    }
    int int32 = Convert.ToInt32(objectParams.Rows[0]["F_OBJECT_TYPE"]);
    Guid guid;
    return this.GetCreatorByGuid((uSession as UserSession).DBCache, int32, out guid).CreateObject(uSession, guid, objectParams);
  }

  public IDBObject GetObjectActual(IUserSession uSession, long objectID, bool failIfNotFound)
  {
    IDbManager dataManager = (uSession as UserSession).DataManager;
    DataTable objectParams = dataManager.ExecuteDataTable("SELECT * FROM IMS_OBJECTS WHERE F_OBJECT_ID IN (:objID, :m_objID) ", dataManager.Parameter("objID", (object) objectID), dataManager.Parameter("m_objID", (object) -objectID));
    if (objectParams.Rows.Count == 0)
    {
      if (failIfNotFound)
        throw new ObjectNotFoundException(objectID);
      return (IDBObject) null;
    }
    if (objectParams.Rows.Count > 1)
    {
      long int64 = Convert.ToInt64(objectParams.Rows[0]["F_CHKOUT_BY"]);
      if (Convert.ToInt64(objectParams.Rows[0]["F_OBJECT_ID"]) < 0L)
      {
        if (int64 != uSession.UserID)
          objectParams.Rows.RemoveAt(0);
      }
      else if (int64 == uSession.UserID)
        objectParams.Rows.RemoveAt(0);
    }
    int int32 = Convert.ToInt32(objectParams.Rows[0]["F_OBJECT_TYPE"]);
    Guid guid;
    return this.GetCreatorByGuid((uSession as UserSession).DBCache, int32, out guid).CreateObject(uSession, guid, objectParams);
  }

  private IDBObjectCreator GetCreatorByGuid(ICacheDataset dbCache, int objectTypeID, out Guid guid)
  {
    guid = dbCache.GetObjectTypeGuid(objectTypeID, true);
    creatorByGuid = this.GetCreator((object) guid) as IDBObjectCreator;
    while (creatorByGuid == null)
    {
      objectTypeID = dbCache.GetObjectTypeParentID(objectTypeID);
      if (objectTypeID > -1)
      {
        guid = dbCache.GetObjectTypeGuid(objectTypeID, true);
        creatorByGuid = this.GetCreator((object) guid) as IDBObjectCreator;
      }
      else
      {
        guid = new Guid("cad0001e-306c-11d8-b4e9-00304f19f545");
        if (!(this.GetCreator((object) guid) is IDBObjectCreator creatorByGuid))
          creatorByGuid = this._DBObjectCreator;
      }
    }
    return creatorByGuid;
  }
}
