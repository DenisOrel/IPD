// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.DBEditingContextsService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Pools;
using Intermech.Search.EditingContexts;
using Intermech.Search.Utilities;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Intermech.Kernel.Services;

public class DBEditingContextsService : 
  LongLifeObject,
  IDBEditingContextsServerService,
  IDBEditingContextsService
{
  public static TimeSpan SyncDelta = new TimeSpan(12, 0, 0);
  private static int attrLinkedContextNumber = 0;
  private static int attrVersionsRule = 0;
  private static int objtypeEditingContext = -1;
  private object syncRoot = new object();
  private object syncRoot4Modify = new object();
  private Dictionary<Guid, CurrentEditingContext> currentContexts = new Dictionary<Guid, CurrentEditingContext>();
  private Dictionary<Tuple<long, long>, EditingContextSource> сontextsSource = new Dictionary<Tuple<long, long>, EditingContextSource>();
  private Dictionary<long, DBEditingContextsService.CachedEditingContext> contextsCache = new Dictionary<long, DBEditingContextsService.CachedEditingContext>();

  protected virtual void InitializeService()
  {
    if (DBEditingContextsService.attrLinkedContextNumber != 0)
      return;
    DBEditingContextsService.attrLinkedContextNumber = MetaDataHelper.GetAttributeTypeID("cad014ff-306c-11d8-b4e9-00304f19f545");
    DBEditingContextsService.attrVersionsRule = MetaDataHelper.GetAttributeTypeID("cad00696-306c-11d8-b4e9-00304f19f545");
    DBEditingContextsService.objtypeEditingContext = MetaDataHelper.GetObjectTypeID("cad014d2-306c-11d8-b4e9-00304f19f545");
  }

  private IUserSession GetUserSession(object usrSession)
  {
    this.InitializeService();
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession));
      default:
        return (IUserSession) null;
    }
  }

  protected virtual void UpdateDateTime(UserSession session, long contextID, DateTime datetime)
  {
  }

  public virtual bool AddToContext(
    object usrSession,
    long contextID,
    long linkedContextNumber,
    long fID,
    long versionID,
    bool writeModificationID,
    bool exceptIfFail)
  {
    return this.AddToContext(usrSession, contextID, linkedContextNumber, (IList<long>) new List<long>(1)
    {
      fID
    }, (IList<long>) new List<long>(1) { versionID }, writeModificationID, exceptIfFail);
  }

  public bool CanLinkContexts(
    object usrSession,
    EditingContextsObjectContainer contextMain,
    EditingContextsObjectContainer ctxToLink,
    bool exceptIfFail)
  {
    if (contextMain == null || ctxToLink == null || contextMain.Objects == null || contextMain.Objects.Count == 0 || ctxToLink.Objects == null || ctxToLink.Objects.Count == 0)
      return true;
    if (Math.Abs(contextMain.ContextID) == Math.Abs(ctxToLink.ContextID))
      return false;
    for (int index1 = 0; index1 < ctxToLink.Objects.Count; ++index1)
    {
      EditingContextsObjectVersion contextsObjectVersion = ctxToLink.Objects[index1];
      if (contextMain.ExistsObject(contextsObjectVersion.F_ID) && Math.Abs(contextMain.GetObjectVersion(contextsObjectVersion.F_ID)) != Math.Abs(contextsObjectVersion.F_OBJECT_ID))
      {
        if (!exceptIfFail)
          return false;
        if (!(this.GetUserSession(usrSession) is UserSession userSession))
          throw new KernelExceptionID(sc_13916.ssp_appserver_13917(10046870), (object) "DBEditingContextsService.CanLinkContexts");
        List<long> linkedContexts = this.GetLinkedContexts((object) userSession, ctxToLink.ModificationID);
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          if (linkedContexts.Count > 0)
          {
            stringBuilder.Append("[");
            for (int index2 = 0; index2 < linkedContexts.Count; ++index2)
            {
              stringBuilder.Append(linkedContexts[index2]);
              if (index2 < linkedContexts.Count - 1)
                stringBuilder.Append(", ");
            }
            stringBuilder.Append("]");
          }
          if (stringBuilder.Length == 0)
          {
            stringBuilder.Append("[");
            stringBuilder.Append(ctxToLink.ContextID);
            stringBuilder.Append("]");
          }
          throw new KernelExceptionID(sc_13916.ssp_appserver_13918(203837341), (object) contextMain.ContextID, (object) stringBuilder.ToString(), (object) contextMain.GetObjectVersion(contextsObjectVersion.F_ID), (object) contextsObjectVersion.F_OBJECT_ID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(contextMain.GetObjectVersion(contextsObjectVersion.F_ID)), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(contextsObjectVersion.F_OBJECT_ID));
        }
      }
    }
    return true;
  }

  protected virtual void InternalAddToCachedContext(
    long contextID,
    long linkedContextNumber,
    long fID,
    long versionID)
  {
    lock (this.syncRoot)
    {
      if (!this.contextsCache.ContainsKey(contextID))
        return;
      this.contextsCache[contextID].Container.DeleteObject(fID);
      this.contextsCache[contextID].Container.AddVersion(new EditingContextsObjectVersion(contextID, fID, versionID, linkedContextNumber), (ObjectVersionDescription) null);
    }
  }

  public virtual bool AddToContext(
    object usrSession,
    long contextID,
    long linkedContextNumber,
    IList<long> fIDs,
    IList<long> versionIDs,
    bool writeModificationID,
    bool exceptIfFail)
  {
    return this.AddToContext(usrSession, contextID, linkedContextNumber, fIDs, versionIDs, writeModificationID, exceptIfFail, true);
  }

  internal bool AddToContext(
    object usrSession,
    long contextID,
    long linkedContextNumber,
    IList<long> fIDs,
    IList<long> versionIDs,
    bool writeModificationID,
    bool exceptIfFail,
    bool checkEditMode)
  {
    IEditingContextServerService contextServerService = this.GetUserSession(usrSession) is UserSession userSession ? (IEditingContextServerService) userSession.GetCustomService(typeof (IEditingContextServerService)) : throw new KernelExceptionID(sc_13916.ssp_appserver_13919(1279720025), (object) "DBEditingContextsService.AddToContext");
    IDBObject editingContextActualCopy = userSession.GetObject(contextID, false) ?? userSession.GetObject(-contextID, false);
    if (checkEditMode && (editingContextActualCopy == null || editingContextActualCopy != null && !contextServerService.CheckEditingContextEditRights(userSession.SessionGUID, editingContextActualCopy.ObjectID)))
      return false;
    if (editingContextActualCopy is IDBEditingContextsObject && fIDs.Any<long>((System.Func<long, bool>) (o => ((IDBEditingContextsObject) editingContextActualCopy).ExistsObject(o, true, false))))
      throw new ArgumentException("Невозможно выполнить операцию. Другая версия одного из включаемых в контекст объектов уже принадлежит этому контексту или связанному с ним.");
    lock (this.syncRoot4Modify)
    {
      IDBTransactions customService = userSession.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      IDBEditingContextsObject objectActualCopy = userSession.GetObjectActualCopy(Math.Abs(contextID), exceptIfFail) as IDBEditingContextsObject;
      (objectActualCopy as IDBSecurity).CheckAccess(ActionType.Edit, true, true);
      bool flag = MetaDataHelper.IsSimpleEditingContext(objectActualCopy.ObjectType);
      if (customService == null || contextID == 0L || linkedContextNumber == 0L || fIDs == null || versionIDs == null || fIDs.Count == 0 || versionIDs.Count != fIDs.Count)
        return false;
      IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID));
      IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":F_OBJECT_ID", (object) 0L);
      IDbDataParameter dbDataParameter3 = userSession.DataManager.Parameter(":F_ID", (object) 0L);
      IDbDataParameter dbDataParameter4 = userSession.DataManager.Parameter(":F_MODIFICATION_ID", (object) Math.Abs(linkedContextNumber));
      string commandText = $"INSERT INTO {"IMS_VERSIONS_CONTEXT"} ({"F_CONTEXT_ID"}, {"F_MODIFICATION_ID"}, {"F_ID"}, {"F_OBJECT_ID"}) VALUES (:F_CONTEXT_ID, :F_MODIFICATION_ID, :F_ID, :F_OBJECT_ID)";
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        for (int index = 0; index < fIDs.Count; ++index)
        {
          if (!flag & writeModificationID)
          {
            if (!(userSession.GetObject(versionIDs[index], false) is DBObject dbObject1) && versionIDs[index] > 0L)
              dbObject1 = userSession.GetObject(-versionIDs[index], false) as DBObject;
            if (dbObject1 != null)
            {
              if (dbObject1.ModificationID == 0L || dbObject1.ModificationID == linkedContextNumber)
              {
                IMSObjectType objectType = MetaDataHelper.GetObjectType(dbObject1.ObjectType);
                if (objectType != null && objectType.VersionsMode == ObjectVersionModes.MultiVersion && !MetaDataHelper.IsObjectTypeEditingContext(objectType.ObjectTypeID))
                {
                  dbObject1.SetModificationID(Math.Abs(linkedContextNumber));
                  if (dbObject1.ObjectID < 0L && !dbObject1.IsCreationMode && userSession.GetObject(Math.Abs(versionIDs[index]), false) is DBObject dbObject2)
                    dbObject2.SetModificationID(Math.Abs(linkedContextNumber));
                }
                else
                  continue;
              }
              else
                continue;
            }
          }
          dbDataParameter3.Value = (object) fIDs[index];
          dbDataParameter2.Value = (object) Math.Abs(versionIDs[index]);
          try
          {
            userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter4, dbDataParameter3, dbDataParameter2);
          }
          catch (Exception ex)
          {
            IDBObject dbObject = userSession.GetObject(versionIDs[index], false);
            string str = dbObject == null ? "Object N" + versionIDs[index].ToString() : dbObject.Caption;
            throw new KernelExceptionID(390, (object) versionIDs[index], (object) str, ex).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(versionIDs[index]));
          }
          this.InternalAddToCachedContext(Math.Abs(contextID), Math.Abs(linkedContextNumber), fIDs[index], Math.Abs(versionIDs[index]));
        }
        this.UpdateDateTime(userSession, contextID, DateTime.UtcNow);
        if (customService.InTransaction)
          customService.Commit();
        lock (this.syncRoot)
        {
          if (this.contextsCache.ContainsKey(contextID))
            this.contextsCache[contextID].Container.ClearCacheTables();
        }
        return true;
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
      }
    }
    return false;
  }

  public bool ReplaceInSimpleContext(
    object usrSession,
    long contextID,
    long linkedContextNumber,
    long fID,
    long newVersionID,
    bool exceptIfFail)
  {
    return this.ReplaceInSimpleContext(usrSession, contextID, linkedContextNumber, (IList<long>) new long[1]
    {
      fID
    }, (IList<long>) new long[1]{ newVersionID }, (exceptIfFail ? 1 : 0) != 0);
  }

  public bool ReplaceInSimpleContext(
    object usrSession,
    long contextID,
    long linkedContextNumber,
    IList<long> fIDs,
    IList<long> newVersionIDs,
    bool exceptIfFail)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13920(321633710), (object) "DBEditingContextsService.ReplaceInSimpleContext");
    lock (this.syncRoot4Modify)
    {
      IDBTransactions customService = userSession.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      IDBEditingContextsObject objectActualCopy = userSession.GetObjectActualCopy(Math.Abs(contextID), exceptIfFail) as IDBEditingContextsObject;
      (objectActualCopy as IDBSecurity).CheckAccess(ActionType.Edit, true, true);
      if (!MetaDataHelper.IsSimpleEditingContext(objectActualCopy.ObjectType))
        throw new ArgumentException(nameof (contextID));
      if (customService == null || contextID == 0L || linkedContextNumber == 0L || fIDs == null || newVersionIDs == null || fIDs.Count == 0 || newVersionIDs.Count != fIDs.Count)
        return false;
      IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID));
      IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":F_OBJECT_ID", (object) 0L);
      IDbDataParameter dbDataParameter3 = userSession.DataManager.Parameter(":F_ID", (object) 0L);
      IDbDataParameter dbDataParameter4 = userSession.DataManager.Parameter(":F_MODIFICATION_ID", (object) Math.Abs(linkedContextNumber));
      string commandText = string.Format("UPDATE {0} SET {4} = :F_OBJECT_ID WHERE {1} = :F_CONTEXT_ID AND {2} = :F_MODIFICATION_ID AND {3} = :F_ID", (object) "IMS_VERSIONS_CONTEXT", (object) "F_CONTEXT_ID", (object) "F_MODIFICATION_ID", (object) "F_ID", (object) "F_OBJECT_ID");
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        for (int index = 0; index < fIDs.Count; ++index)
        {
          dbDataParameter3.Value = (object) fIDs[index];
          dbDataParameter2.Value = (object) Math.Abs(newVersionIDs[index]);
          userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter4, dbDataParameter3, dbDataParameter2);
          this.InternalAddToCachedContext(Math.Abs(contextID), Math.Abs(linkedContextNumber), fIDs[index], Math.Abs(newVersionIDs[index]));
        }
        this.UpdateDateTime(userSession, contextID, DateTime.UtcNow);
        if (customService.InTransaction)
          customService.Commit();
        lock (this.syncRoot)
        {
          if (this.contextsCache.ContainsKey(contextID))
            this.contextsCache[contextID].Container.ClearCacheTables();
        }
        return true;
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
      }
    }
    return false;
  }

  public virtual bool ReleaseContextObjects(object usrSession, long contextID, bool exceptIfFail)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13921(1885641175), (object) "DBEditingContextsService.ClearContext");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService) || contextID == 0L)
        return false;
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        IDBEditingContextsObject objectActualCopy1 = userSession.GetObjectActualCopy(Math.Abs(contextID), exceptIfFail) as IDBEditingContextsObject;
        bool flag = MetaDataHelper.IsSimpleEditingContext(objectActualCopy1.ObjectType);
        List<EditingContextsObjectVersion> contextsObjectVersionList = this.SelectContextsInfo(contextID, objectActualCopy1.LinkedContextNumber, (IUserSession) userSession);
        List<long> longList = new List<long>();
        for (int index = 0; index < contextsObjectVersionList.Count; ++index)
        {
          if (contextsObjectVersionList[index].F_CONTEXT_ID != Math.Abs(contextID) && longList.IndexOf(contextsObjectVersionList[index].F_OBJECT_ID) < 0)
            longList.Add(contextsObjectVersionList[index].F_OBJECT_ID);
        }
        if (!flag)
        {
          for (int index = 0; index < contextsObjectVersionList.Count; ++index)
          {
            if (longList.IndexOf(contextsObjectVersionList[index].F_OBJECT_ID) < 0 && userSession.GetObjectActualCopy(contextsObjectVersionList[index].F_OBJECT_ID, false) is DBObject objectActualCopy2 && !MetaDataHelper.IsObjectTypeEditingContext(objectActualCopy2.ObjectType))
            {
              objectActualCopy2.SetModificationID(0L);
              if (objectActualCopy2.ObjectID < 0L && userSession.GetObject(Math.Abs(contextsObjectVersionList[index].F_OBJECT_ID), false) is DBObject dbObject)
                dbObject.SetModificationID(0L);
            }
          }
        }
        customService.Commit();
        return true;
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
      }
    }
    return false;
  }

  public virtual bool ClearContext(object usrSession, long contextID, bool exceptIfFail)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13922(1969190084), (object) "DBEditingContextsService.ClearContext");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService) || contextID == 0L)
        return false;
      IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID));
      string commandText = string.Format("DELETE FROM {0} WHERE {0}.{1} = :F_CONTEXT_ID", (object) "IMS_VERSIONS_CONTEXT", (object) "F_CONTEXT_ID");
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter);
        this.UpdateDateTime(userSession, contextID, DateTime.UtcNow);
        customService.Commit();
        return true;
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
      }
    }
    return false;
  }

  public virtual bool DeleteFromContext(
    object usrSession,
    long contextID,
    long versionID,
    bool exceptIfFail,
    bool clearModifiationID)
  {
    return this.DeleteFromContext(usrSession, contextID, (IList<long>) new List<long>(1)
    {
      versionID
    }, exceptIfFail, clearModifiationID);
  }

  public virtual bool DeleteFromContext(
    object usrSession,
    long contextID,
    IList<long> versionIDs,
    bool exceptIfFail,
    bool clearModifiationID)
  {
    return this.DeleteFromContext(usrSession, contextID, versionIDs, exceptIfFail, clearModifiationID, true);
  }

  internal bool DeleteFromContext(
    object usrSession,
    long contextID,
    IList<long> versionIDs,
    bool exceptIfFail,
    bool clearModifiationID,
    bool checkRulesOnClearModifiationID)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13923(1345179025), (object) "DBEditingContextsService.DeleteFromContext");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService) || contextID == 0L || versionIDs == null || versionIDs.Count == 0)
        return false;
      IDBEditingContextsObject editingContextsObject = contextID >= 0L ? userSession.GetObjectActualCopy(Math.Abs(contextID), exceptIfFail) as IDBEditingContextsObject : userSession.GetObject(contextID, false) as IDBEditingContextsObject;
      if (editingContextsObject == null)
        return false;
      bool flag = MetaDataHelper.IsSimpleEditingContext(editingContextsObject.ObjectType);
      IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID));
      IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":F_OBJECT_ID", (object) 0L);
      string commandText = string.Format("DELETE FROM {0} WHERE {0}.{1} = :F_CONTEXT_ID AND {0}.{2} = :F_OBJECT_ID", (object) "IMS_VERSIONS_CONTEXT", (object) "F_CONTEXT_ID", (object) "F_OBJECT_ID");
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        List<EditingContextsObjectVersion> contextsObjectVersionList = this.SelectContextsInfo(contextID, editingContextsObject.LinkedContextNumber, (IUserSession) userSession);
        List<long> longList = new List<long>();
        for (int index = 0; index < contextsObjectVersionList.Count; ++index)
        {
          if (Math.Abs(contextsObjectVersionList[index].F_CONTEXT_ID) != Math.Abs(contextID) && longList.IndexOf(contextsObjectVersionList[index].F_OBJECT_ID) < 0)
            longList.Add(contextsObjectVersionList[index].F_OBJECT_ID);
        }
        for (int index = 0; index < versionIDs.Count; ++index)
        {
          dbDataParameter2.Value = (object) Math.Abs(versionIDs[index]);
          userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter2);
        }
        if (!flag)
        {
          for (int index = 0; index < versionIDs.Count; ++index)
          {
            if (longList.IndexOf(-versionIDs[index]) < 0 && longList.IndexOf(versionIDs[index]) < 0)
            {
              DBObject objectActualCopy = userSession.GetObjectActualCopy(versionIDs[index], false) as DBObject;
              if (clearModifiationID && objectActualCopy != null && !MetaDataHelper.IsObjectTypeEditingContext(objectActualCopy.ObjectType))
              {
                objectActualCopy.SetModificationID(0L, checkRulesOnClearModifiationID);
                if (objectActualCopy.ObjectID < 0L)
                {
                  DBObject dbObject = userSession.GetObject(Math.Abs(versionIDs[index]), false) as DBObject;
                  if (clearModifiationID && dbObject != null)
                    dbObject.SetModificationID(0L, checkRulesOnClearModifiationID);
                }
              }
            }
          }
        }
        this.UpdateDateTime(userSession, contextID, DateTime.UtcNow);
        if (customService.InTransaction)
          customService.Commit();
        this.RemoveVersionsFromCache(versionIDs, (IList<long>) null);
        return true;
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
      }
    }
    return false;
  }

  public virtual bool DeleteObjectFromContext(
    object usrSession,
    long contextID,
    long fID,
    bool exceptIfFail,
    bool clearModifiationID)
  {
    return this.DeleteObjectsFromContext(usrSession, contextID, (IList<long>) new List<long>(1)
    {
      fID
    }, exceptIfFail, clearModifiationID);
  }

  public virtual bool DeleteObjectsFromContext(
    object usrSession,
    long contextID,
    IList<long> fIDs,
    bool exceptIfFail,
    bool clearModifiationID)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13924(1846915706), (object) "DBEditingContextsService.DeleteObjectsFromContext");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService) || contextID == 0L || fIDs == null || fIDs.Count == 0 || !(userSession.GetObjectActualCopy(Math.Abs(contextID), exceptIfFail) is IDBEditingContextsObject objectActualCopy1))
        return false;
      bool flag = MetaDataHelper.IsSimpleEditingContext(objectActualCopy1.ObjectType);
      IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID));
      IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":F_ID", (object) 0L);
      string commandText = string.Format("DELETE FROM {0} WHERE {0}.{1} = :F_CONTEXT_ID AND {0}.{2} = :F_ID", (object) "IMS_VERSIONS_CONTEXT", (object) "F_CONTEXT_ID", (object) "F_ID");
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        List<EditingContextsObjectVersion> contextsObjectVersionList = this.SelectContextInfo(contextID, objectActualCopy1.LinkedContextNumber, (IUserSession) userSession);
        List<long> longList = new List<long>();
        for (int index = 0; index < contextsObjectVersionList.Count; ++index)
        {
          if (Math.Abs(contextsObjectVersionList[index].F_CONTEXT_ID) != Math.Abs(contextID) && longList.IndexOf(contextsObjectVersionList[index].F_OBJECT_ID) < 0)
            longList.Add(contextsObjectVersionList[index].F_OBJECT_ID);
        }
        for (int index1 = 0; index1 < fIDs.Count; ++index1)
        {
          dbDataParameter2.Value = (object) fIDs[index1];
          userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter2);
          if (!flag)
          {
            for (int index2 = 0; index2 < contextsObjectVersionList.Count; ++index2)
            {
              if (contextsObjectVersionList[index2].F_ID == fIDs[index1] && longList.IndexOf(contextsObjectVersionList[index1].F_OBJECT_ID) < 0)
              {
                DBObject objectActualCopy2 = userSession.GetObjectActualCopy(contextsObjectVersionList[index2].F_OBJECT_ID, false) as DBObject;
                if (clearModifiationID && objectActualCopy2 != null && !MetaDataHelper.IsObjectTypeEditingContext(objectActualCopy2.ObjectType))
                {
                  objectActualCopy2.SetModificationID(0L);
                  if (objectActualCopy2.ObjectID < 0L && !objectActualCopy2.IsCreationMode)
                  {
                    DBObject dbObject = userSession.GetObject(Math.Abs(contextsObjectVersionList[index2].F_OBJECT_ID), false) as DBObject;
                    if (clearModifiationID && dbObject != null)
                      dbObject.SetModificationID(0L);
                  }
                }
              }
            }
          }
        }
        this.UpdateDateTime(userSession, contextID, DateTime.UtcNow);
        if (customService.InTransaction)
          customService.Commit();
        this.RemoveObjectsFromCache(fIDs, (IList<long>) new long[1]
        {
          contextID
        });
        return true;
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
      }
    }
    return false;
  }

  public virtual bool ExistsInContext(object usrSession, long contextID, long versionID)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13925(1465361043), (object) "DBEditingContextsService.ExistsInContext");
    if (contextID == 0L || versionID == 0L)
      return false;
    IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID));
    IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":F_OBJECT_ID", (object) Math.Abs(versionID));
    string commandText = string.Format("SELECT {0}.{1} FROM {0} WHERE {0}.{1} = :F_CONTEXT_ID AND {0}.{2} = :F_OBJECT_ID", (object) "IMS_VERSIONS_CONTEXT", (object) "F_CONTEXT_ID", (object) "F_OBJECT_ID");
    object obj = userSession.DataManager.ExecuteScalar(commandText, dbDataParameter1, dbDataParameter2);
    if (obj == null || obj == DBNull.Value)
      return false;
    long result = 0;
    return long.TryParse(obj.ToString(), out result) && result != 0L;
  }

  public virtual long ExistsInContexts(object usrSession, long linkedContextNumber, long versionID)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13926(1766116075), (object) "DBEditingContextsService.ExistsInContexts");
    if (linkedContextNumber == 0L || versionID == 0L)
      return 0;
    IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":F_MODIFICATION_ID", (object) linkedContextNumber);
    IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":F_OBJECT_ID", (object) Math.Abs(versionID));
    string commandText = string.Format("SELECT {0}.{1} FROM {0} WHERE {0}.{2} = :F_MODIFICATION_ID AND {0}.{3} = :F_OBJECT_ID", (object) "IMS_VERSIONS_CONTEXT", (object) "F_CONTEXT_ID", (object) "F_MODIFICATION_ID", (object) "F_OBJECT_ID");
    object obj = userSession.DataManager.ExecuteScalar(commandText, dbDataParameter1, dbDataParameter2);
    if (obj == null || obj == DBNull.Value)
      return 0;
    long result = 0;
    return !long.TryParse(obj.ToString(), out result) ? 0L : result;
  }

  public List<long> GetLinkedContextsQuick(object usrSession, long linkedContextNumber)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13927(1479279350), (object) "DBEditingContextsService.GetLinkedContextsQuick");
    List<long> linkedContextsQuick = new List<long>();
    if (linkedContextNumber == 0L)
      return linkedContextsQuick;
    long userId = userSession.UserID;
    IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":F_MODIFICATION_ID", (object) linkedContextNumber);
    string commandText = string.Format("SELECT DISTINCT A.{2}, B.{3} FROM {0} A, {1} B WHERE A.{4} = :{4} AND (A.{4} = B.{5} OR  A.{2} = B.{5})", (object) "IMS_VERSIONS_CONTEXT", (object) "IMS_OBJECTS_VIEW", (object) "F_CONTEXT_ID", (object) "F_CHKOUT_BY", (object) "F_MODIFICATION_ID", (object) "F_OBJECT_ID");
    DataTable dataTable = userSession.DataManager.ExecuteDataTable(commandText, dbDataParameter);
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dataTable.Rows[index];
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        if ((int64Value1 >= 0L || int64Value2 == userId) && (int64Value1 <= 0L || int64Value2 == 0L) && !linkedContextsQuick.Contains(int64Value1))
          linkedContextsQuick.Add(int64Value1);
      }
      List<long> longList = new List<long>();
      for (int index = 0; index < linkedContextsQuick.Count; ++index)
      {
        long num = linkedContextsQuick[index];
        if (num <= 0L || !linkedContextsQuick.Contains(-num))
          longList.Add(num);
      }
      linkedContextsQuick = longList;
    }
    return linkedContextsQuick;
  }

  public List<long> GetLinkedContexts(object usrSession, long linkedContextNumber)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13916.ssp_appserver_13928(521797540), (object) "DBEditingContextsService.GetLinkedContexts");
    List<long> linkedContexts = new List<long>();
    if (linkedContextNumber == 0L)
      return linkedContexts;
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) MetaDataHelper.GetEditingContextTopObjectsIDs());
    try
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
      object[] objArray = new object[0];
      SortOrders[] sortOrdersArray = new SortOrders[0];
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(DBEditingContextsService.attrLinkedContextNumber, RelationalOperators.Equal, (object) Math.Abs(linkedContextNumber).ToString(), LogicalOperators.NONE, 0, true)
      }, columns);
      for (int index = 0; index < childrenIdRecursive.Count; ++index)
      {
        IDBObjectCollection objectCollection = userSession.GetObjectCollection(childrenIdRecursive[index]);
        objectCollection.ShowAllModifications = true;
        DataTable dataTable = objectCollection.Select(paramSet);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            if (linkedContexts.IndexOf(int64) < 0 && linkedContexts.IndexOf(Math.Abs(int64)) < 0)
              linkedContexts.Add(int64);
          }
        }
      }
    }
    catch
    {
    }
    return linkedContexts;
  }

  public List<long> GetAllLinkedContexts(object usrSession, List<long> contextObjectIDs)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13916.ssp_appserver_13929(1751549904), (object) "DBEditingContextsService.GetAllLinkedContexts");
    List<long> allLinkedContexts = new List<long>();
    List<long> longList = new List<long>();
    if (contextObjectIDs == null || contextObjectIDs.Count == 0)
      return allLinkedContexts;
    foreach (long num in contextObjectIDs.ToArray())
      contextObjectIDs.Add(-num);
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) MetaDataHelper.GetEditingContextTopObjectsIDs());
    try
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) DBEditingContextsService.attrLinkedContextNumber, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
      object[] objArray = new object[0];
      SortOrders[] sortOrdersArray = new SortOrders[0];
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) contextObjectIDs.ToArray(), LogicalOperators.NONE, 0, true)
      }, columns);
      for (int index = 0; index < childrenIdRecursive.Count; ++index)
      {
        IDBObjectCollection objectCollection = userSession.GetObjectCollection(childrenIdRecursive[index]);
        objectCollection.ShowAllModifications = true;
        DataTable dataTable = objectCollection.Select(paramSet);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64Value = DataSetProcessor.GetInt64Value(row[0], 0L);
            long num = Math.Abs(DataSetProcessor.GetInt64Value(row[1], 0L));
            if (int64Value != 0L)
            {
              if (allLinkedContexts.IndexOf(int64Value) < 0 && allLinkedContexts.IndexOf(Math.Abs(int64Value)) < 0)
                allLinkedContexts.Add(int64Value);
              if (longList.IndexOf(num) < 0)
                longList.Add(num);
            }
          }
        }
      }
    }
    catch
    {
    }
    if (longList.Count > 0)
    {
      try
      {
        ColumnDescriptor[] columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
        };
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(DBEditingContextsService.attrLinkedContextNumber, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
        }, columns);
        for (int index = 0; index < childrenIdRecursive.Count; ++index)
        {
          IDBObjectCollection objectCollection = userSession.GetObjectCollection(childrenIdRecursive[index]);
          objectCollection.ShowAllModifications = true;
          DataTable dataTable = objectCollection.Select(paramSet);
          if (dataTable != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              long int64 = Convert.ToInt64(row[0]);
              if (allLinkedContexts.IndexOf(int64) < 0 && allLinkedContexts.IndexOf(Math.Abs(int64)) < 0)
                allLinkedContexts.Add(int64);
            }
          }
        }
      }
      catch
      {
      }
    }
    return allLinkedContexts;
  }

  public EditingContextsObjectContainer GetEditingContextsObject(
    object usrSession,
    long ContextID,
    bool withDescriptions,
    bool useCache)
  {
    if (ContextID == 0L)
      return (EditingContextsObjectContainer) null;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13916.ssp_appserver_13930(799087538), (object) "DBEditingContextsService.GetEditingContextsObject");
    if (useCache)
    {
      DBEditingContextsService.CachedEditingContext cachedEditingContext = (DBEditingContextsService.CachedEditingContext) null;
      lock (this.syncRoot)
      {
        if (this.contextsCache.ContainsKey(Math.Abs(ContextID)))
        {
          cachedEditingContext = this.contextsCache[Math.Abs(ContextID)];
          if (cachedEditingContext.Container != null && !(cachedEditingContext.LoadTime == DateTime.MinValue))
          {
            if (!(DateTime.UtcNow - cachedEditingContext.LoadTime > DBEditingContextsService.SyncDelta))
              goto label_13;
          }
          this.contextsCache.Remove(Math.Abs(ContextID));
          cachedEditingContext = (DBEditingContextsService.CachedEditingContext) null;
        }
      }
label_13:
      if (cachedEditingContext != null && cachedEditingContext.Container != null)
      {
        IDBObject objectActualCopy = userSession.GetObjectActualCopy(Math.Abs(ContextID), false);
        if (objectActualCopy != null)
          cachedEditingContext.Container.ContextID = objectActualCopy.ObjectID;
        return cachedEditingContext.Container;
      }
    }
    try
    {
      if (!(userSession.GetObjectActualCopy(Math.Abs(ContextID), false) is IDBEditingContextsObject objectActualCopy1))
        return (EditingContextsObjectContainer) null;
      long num = Math.Abs(objectActualCopy1.LinkedContextNumber);
      List<EditingContextsObjectVersion> objects = this.SelectContextsInfo(ContextID, num, userSession);
      List<ObjectVersionDescription> descriptions = withDescriptions ? this.SelectContextsDescriptions(ContextID, userSession) : (List<ObjectVersionDescription>) null;
      EditingContextsObjectContainer editingContextsObject = new EditingContextsObjectContainer(ContextID, num, objectActualCopy1.ObjectType, objects, descriptions);
      int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545");
      int objectTypeId2 = MetaDataHelper.GetObjectTypeID("cad0146b-306c-11d8-b4e9-00304f19f545");
      List<long> longList1 = new List<long>();
      List<int> typeList = new List<int>();
      if (withDescriptions)
      {
        for (int index = 0; index < descriptions.Count; ++index)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(descriptions[index].F_OBJECT_TYPE, objectTypeId2))
            descriptions[index].Options |= ObjectVersionDescriptionOptions.IsContext;
          if (MetaDataHelper.IsObjectTypeChildOf(descriptions[index].F_OBJECT_TYPE, objectTypeId1))
          {
            descriptions[index].Options |= ObjectVersionDescriptionOptions.IsECO;
            if (longList1.IndexOf(descriptions[index].F_OBJECT_ID) < 0)
            {
              longList1.Add(descriptions[index].F_OBJECT_ID);
              if (typeList.IndexOf(descriptions[index].F_OBJECT_TYPE) < 0)
                typeList.Add(descriptions[index].F_OBJECT_TYPE);
            }
          }
        }
      }
      if (longList1.Count > 0)
      {
        CompositionLoadService service = ServerServices.GetService(typeof (ICompositionLoadService)) as CompositionLoadService;
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(2);
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
        List<int> enabledObjectTypes = MetaDataHelper.GetTopParentEnabledObjectTypes((IEnumerable<int>) typeList);
        int relationTypeId1 = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
        List<int> intList1 = enabledObjectTypes.Count == 1 ? MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(enabledObjectTypes[0], relationTypeId1)) : new List<int>();
        IUserSession usrSession1 = userSession;
        long[] array = longList1.ToArray();
        int relationTypeId2 = relationTypeId1;
        List<int> compositionTypes = service.GetPresentCompositionTypes((object) usrSession1, (IEnumerable<long>) array, relationTypeId2, true);
        List<int> intList2 = compositionTypes != null ? MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) compositionTypes) : new List<int>();
        IDBRelationCollection relationCollection = userSession.GetRelationCollection(relationTypeId1);
        relationCollection.ChildObjectTypes = (IList<int>) intList2;
        relationCollection.FiltrationOwnerID = "cad005ac-306c-11d8-b4e9-00304f19f5455";
        foreach (long projectID in longList1)
        {
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], columnDescriptorList.ToArray());
          DataTable dataTable = relationCollection.ConsistFrom(paramSet, projectID);
          if (dataTable != null)
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              long int64Value1 = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
              long int64Value2 = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 1, 0L);
              DataSetProcessor.GetInt64Value(dataTable.Rows[index], 2, 0L);
              long int64Value3 = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 3, 0L);
              if (int64Value1 != 0L && int64Value2 != 0L && int64Value3 != 0L)
              {
                ObjectVersionDescription description = editingContextsObject.GetDescription(int64Value1);
                if (description != null)
                {
                  description.Options |= ObjectVersionDescriptionOptions.FromECOComposition;
                  if (description.ECOs == null)
                    description.ECOs = new List<long>();
                  if (!(description.Tag is List<long> longList2))
                  {
                    longList2 = new List<long>();
                    description.Tag = (object) longList2;
                  }
                  longList2.Add(int64Value2);
                  if (description.ECOs.IndexOf(int64Value3) < 0)
                    description.ECOs.Add(int64Value3);
                }
              }
            }
          }
        }
      }
      if (editingContextsObject.Descriptions != null)
      {
        bool flag = false;
        for (int index = editingContextsObject.Descriptions.Count - 1; index >= 0; --index)
        {
          ObjectVersionDescription description = editingContextsObject.Descriptions[index];
          if ((description.Options & ObjectVersionDescriptionOptions.InvalidDescription) == ObjectVersionDescriptionOptions.InvalidDescription)
          {
            IDBObject objectActualCopy2 = userSession.GetObjectActualCopy(-Math.Abs(description.F_OBJECT_ID), false);
            IDBObject source = objectActualCopy2 == null || objectActualCopy2.CheckoutBy != userSession.UserID ? userSession.GetObjectActualCopy(Math.Abs(description.F_OBJECT_ID), false) : objectActualCopy2;
            if (source == null)
            {
              editingContextsObject.Descriptions.RemoveAt(index);
              editingContextsObject.DeleteVersion(description.F_OBJECT_ID);
            }
            else
              description.Assign((object) source);
            flag = true;
          }
        }
        if (flag)
          editingContextsObject.ClearCacheTables();
      }
      else
        editingContextsObject.ClearCacheTables();
      if (useCache)
      {
        EditingContextsObjectContainer container = !withDescriptions ? editingContextsObject : editingContextsObject.Clone() as EditingContextsObjectContainer;
        if (withDescriptions)
        {
          container.Descriptions.Clear();
          container.ClearCacheTables();
        }
        DBEditingContextsService.CachedEditingContext cachedEditingContext = new DBEditingContextsService.CachedEditingContext(container);
        lock (this.syncRoot)
          this.contextsCache[Math.Abs(ContextID)] = cachedEditingContext;
      }
      return editingContextsObject;
    }
    catch
    {
    }
    return (EditingContextsObjectContainer) null;
  }

  public void SetEditingContextsObject(
    object usrSession,
    EditingContextsObjectContainer context,
    bool exceptIfFail)
  {
    this.SetEditingContextsObject(usrSession, context, exceptIfFail, true, false);
  }

  public List<long> GetDeltaECOComposiotions(object usrSession, long ecoID)
  {
    List<long> ecoComposiotions = new List<long>();
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13931(582571409), (object) "DBEditingContextsService.GetDeltaECOComposiotions");
    if (ecoID > 0L)
      return ecoComposiotions;
    QuickObjectInfo objectInfo = userSession.GetObjectInfo(ecoID);
    if (objectInfo.Empty || !MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545")))
      return ecoComposiotions;
    CompositionLoadService service = ServerServices.GetService(typeof (ICompositionLoadService)) as CompositionLoadService;
    int relationTypeId = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(objectInfo.ObjectTypeID, relationTypeId));
    List<long> longList1 = service.LoadCompositionObjects((object) userSession, ecoID, relationTypeId, "cad005ac-306c-11d8-b4e9-00304f19f5455", childrenIdRecursive.ToArray());
    List<long> longList2 = service.LoadCompositionObjects((object) userSession, Math.Abs(ecoID), relationTypeId, "cad005ac-306c-11d8-b4e9-00304f19f5455", childrenIdRecursive.ToArray());
    for (int index = 0; index < longList1.Count; ++index)
    {
      if (longList2.IndexOf(longList1[index]) < 0)
        ecoComposiotions.Add(longList1[index]);
    }
    return ecoComposiotions;
  }

  public void SetEditingContextsObject(
    object usrSession,
    EditingContextsObjectContainer context,
    bool exceptIfFail,
    bool syncComposition,
    bool removeNonVersionedObjects = false)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13932(199971323), (object) "DBEditingContextsService.SetEditingContextsObject");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService) || context == null || context.ContextID == 0L)
        return;
      IDBEditingContextsObject objectActualCopy1 = userSession.GetObjectActualCopy(Math.Abs(context.ContextID), exceptIfFail) as IDBEditingContextsObject;
      long modificationID = 0;
      if (objectActualCopy1 != null)
      {
        (objectActualCopy1 as IDBSecurity).CheckAccess(ActionType.Edit, true, true);
        modificationID = objectActualCopy1.LinkedContextNumber;
      }
      QuickObjectInfo objectInfo = userSession.GetObjectInfo(context.ContextID);
      bool flag = !objectInfo.Empty && MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
      if (syncComposition & flag)
      {
        CompositionLoadService service = ServerServices.GetService(typeof (ICompositionLoadService)) as CompositionLoadService;
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(2);
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
        int relationTypeId1 = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
        List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(objectInfo.ObjectTypeID, relationTypeId1));
        UserSession usrSession1 = userSession;
        long contextId = context.ContextID;
        int relationTypeId2 = relationTypeId1;
        List<ColumnDescriptor> columns = columnDescriptorList;
        int[] array = childrenIdRecursive.ToArray();
        DataTable dataTable = service.LoadComposition((object) usrSession1, contextId, relationTypeId2, (IEnumerable<ColumnDescriptor>) columns, "cad005ac-306c-11d8-b4e9-00304f19f5455", array);
        if (dataTable != null)
        {
          List<long> longList = new List<long>();
          EditingContextsObjectContainer contextsObjectContainer = context.SimpleClone();
          contextsObjectContainer.ContextID = context.ContextID;
          contextsObjectContainer.ModificationID = context.ModificationID;
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            long num1 = Math.Abs(DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L));
            long num2 = Math.Abs(DataSetProcessor.GetInt64Value(dataTable.Rows[index], 1, 0L));
            if (num1 != 0L && num2 != 0L && longList.IndexOf(num1) < 0 && !contextsObjectContainer.ExistsVersion(num1, false))
            {
              contextsObjectContainer.AddVersion(new EditingContextsObjectVersion(context.ContextID, num2, num1, context.ModificationID), new ObjectVersionDescription(num2, num1, 0, 0, 0L, 0L, string.Empty, 0L, context.ModificationID, 0L, ObjectVersionDescriptionOptions.FromECOComposition));
              longList.Add(num1);
            }
          }
          context = contextsObjectContainer;
        }
      }
      context.ModificationID = Math.Abs(context.ModificationID);
      bool inTransaction = customService.InTransaction;
      try
      {
        context.ClearCacheTables();
        IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(context.ContextID));
        IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":F_OBJECT_ID", (object) 0L);
        IDbDataParameter dbDataParameter3 = userSession.DataManager.Parameter(":F_ID", (object) 0L);
        IDbDataParameter dbDataParameter4 = userSession.DataManager.Parameter(":F_MODIFICATION_ID", (object) Math.Abs(context.ModificationID));
        string commandText = $"INSERT INTO {"IMS_VERSIONS_CONTEXT"} ({"F_CONTEXT_ID"}, {"F_MODIFICATION_ID"}, {"F_ID"}, {"F_OBJECT_ID"}) VALUES (:F_CONTEXT_ID, :F_MODIFICATION_ID, :F_ID, :F_OBJECT_ID)";
        customService.StartTransaction();
        EditingContextsObjectContainer editingContextsObject = this.GetEditingContextsObject((object) userSession, context.ContextID, false, false);
        List<long> versionsId = editingContextsObject.GetVersionsID(true, userSession.UserID);
        foreach (long num in versionsId.ToArray())
        {
          ObjectVersionDescription description = context.GetDescription(num);
          if (context.ExistsVersion(num, true) || ((description == null ? 0 : (!ObjectTypeHelper.IsVersionedObjectTypeID(description.F_OBJECT_TYPE) ? 1 : 0)) & (removeNonVersionedObjects ? 1 : 0)) != 0)
            versionsId.Remove(num);
        }
        if (versionsId.Count > 0 && !editingContextsObject.SimpleContext)
        {
          for (int index = 0; index < versionsId.Count; ++index)
          {
            if (!editingContextsObject.ExistsLinkedVersion(versionsId[index]) && userSession.GetObjectActualCopy(Math.Abs(versionsId[index]), false) is DBObject objectActualCopy2 && !MetaDataHelper.IsObjectTypeEditingContext(objectActualCopy2.ObjectType) && (objectActualCopy2.ModificationID == 0L || objectActualCopy2.ModificationID == context.ModificationID))
            {
              objectActualCopy2.SetModificationID(9223372036854775806L);
              if (objectActualCopy2.ObjectID < 0L && userSession.GetObject(Math.Abs(versionsId[index]), false) is DBObject dbObject)
                dbObject.SetModificationID(9223372036854775806L);
            }
          }
        }
        this.ClearContext((object) userSession, context.ContextID, exceptIfFail);
        for (int index = 0; index < context.Objects.Count; ++index)
        {
          EditingContextsObjectVersion contextsObjectVersion = context.Objects[index];
          if (Math.Abs(contextsObjectVersion.F_CONTEXT_ID) == Math.Abs(context.ContextID))
          {
            if (!context.SimpleContext && userSession.GetObjectActualCopy(contextsObjectVersion.F_OBJECT_ID, false) is DBObject objectActualCopy3)
            {
              if (objectActualCopy3.ModificationID == 0L || objectActualCopy3.ModificationID == context.ModificationID)
              {
                IMSObjectType objectType = MetaDataHelper.GetObjectType(objectActualCopy3.ObjectType);
                if (flag || objectType != null && objectType.VersionsMode == ObjectVersionModes.MultiVersion)
                {
                  if (objectActualCopy3.ModificationID != context.ModificationID && !MetaDataHelper.IsObjectTypeEditingContext(objectActualCopy3.ObjectType))
                  {
                    objectActualCopy3.SetModificationID(Math.Abs(context.ModificationID));
                    if (objectActualCopy3.ObjectID < 0L && userSession.GetObject(Math.Abs(contextsObjectVersion.F_OBJECT_ID), false) is DBObject dbObject)
                      dbObject.SetModificationID(Math.Abs(context.ModificationID));
                  }
                }
                else
                  continue;
              }
              else
                continue;
            }
            dbDataParameter2.Value = (object) Math.Abs(contextsObjectVersion.F_OBJECT_ID);
            dbDataParameter3.Value = (object) contextsObjectVersion.F_ID;
            userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter4, dbDataParameter3, dbDataParameter2);
          }
        }
        if (versionsId.Count > 0 && !editingContextsObject.SimpleContext)
        {
          for (int index = 0; index < versionsId.Count; ++index)
          {
            if (!editingContextsObject.ExistsLinkedVersion(versionsId[index]) && userSession.GetObjectActualCopy(Math.Abs(versionsId[index]), false) is DBObject objectActualCopy4 && !MetaDataHelper.IsObjectTypeEditingContext(objectActualCopy4.ObjectType) && (objectActualCopy4.ModificationID == 0L || objectActualCopy4.ModificationID == 9223372036854775806L))
            {
              objectActualCopy4.SetModificationID(0L);
              if (objectActualCopy4.ObjectID < 0L && userSession.GetObject(Math.Abs(versionsId[index]), false) is DBObject dbObject)
                dbObject.SetModificationID(0L);
            }
          }
        }
        if (!customService.InTransaction)
          return;
        customService.Commit();
        if (objectActualCopy1 == null)
          return;
        this.RemoveFromCache(modificationID);
      }
      catch
      {
        customService.Rollback();
        if (!(exceptIfFail | inTransaction))
          return;
        throw;
      }
    }
  }

  private List<long> InternalFindObjectsContexts(
    UserSession session,
    List<long> versionIDs,
    bool exceptIfFail)
  {
    List<long> objectsContexts = new List<long>();
    if (versionIDs == null || versionIDs.Count == 0 || session == null)
      return objectsContexts;
    DataTable dataTable = (DataTable) null;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>(versionIDs.Count);
      for (int index = 0; index < versionIDs.Count; ++index)
      {
        stringBuilder.Append(index < versionIDs.Count - 1 ? $":p{index.ToString()}," : $":p{index.ToString()}");
        dbDataParameterList.Add(session.DataManager.Parameter($":p{index.ToString()}", (object) Math.Abs(versionIDs[index])));
      }
      string commandText = string.Format("SELECT DISTINCT {1} FROM {0} WHERE {2} IN ({3})", (object) "IMS_VERSIONS_CONTEXT", (object) "F_CONTEXT_ID", (object) "F_OBJECT_ID", (object) stringBuilder.ToString());
      dataTable = session.DataManager.ExecuteDataTable(commandText, dbDataParameterList.ToArray());
    }
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
        if (int64Value != 0L && objectsContexts.IndexOf(int64Value) < 0)
          objectsContexts.Add(int64Value);
      }
    }
    return objectsContexts;
  }

  public virtual List<long> FindObjectsContexts(
    object usrSession,
    List<long> versionIDs,
    bool exceptIfFail)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13933(1070793414), (object) "DBEditingContextsService.FindObjectsContexts");
    List<long> objectsContexts1 = new List<long>();
    int count;
    for (int index1 = 0; index1 < versionIDs.Count; index1 += count)
    {
      count = index1 < versionIDs.Count - 1000 ? 1000 : versionIDs.Count - index1;
      List<long> objectsContexts2 = this.InternalFindObjectsContexts(userSession, versionIDs.GetRange(index1, count), exceptIfFail);
      for (int index2 = 0; index2 < objectsContexts2.Count; ++index2)
      {
        if (objectsContexts1.IndexOf(objectsContexts2[index2]) < 0)
          objectsContexts1.Add(objectsContexts2[index2]);
      }
    }
    return objectsContexts1;
  }

  public virtual long GetUserContextID(Guid id)
  {
    if (id == Guid.Empty)
      return 0;
    lock (this.currentContexts)
    {
      if (this.currentContexts.ContainsKey(id))
        return this.currentContexts[id].ContextID;
    }
    return 0;
  }

  public virtual bool SetUserContextID(Guid id, long contextID, long modificationID)
  {
    if (id == Guid.Empty)
      return false;
    lock (this.currentContexts)
    {
      EditingContextMode contextMode = contextID != 0L ? this.GetUserContextMode(id) : EditingContextMode.Default;
      modificationID = contextID != 0L ? modificationID : 0L;
      this.currentContexts[id] = new CurrentEditingContext(contextID, modificationID, contextMode);
    }
    return true;
  }

  public virtual EditingContextMode GetUserContextMode(Guid id)
  {
    lock (this.currentContexts)
    {
      if (this.currentContexts.ContainsKey(id))
        return this.currentContexts[id].ContextMode;
    }
    return EditingContextMode.Default;
  }

  public virtual long GetModificationID(Guid id)
  {
    lock (this.currentContexts)
    {
      if (this.currentContexts.ContainsKey(id))
        return this.currentContexts[id].ModificationID;
    }
    return 0;
  }

  public virtual bool SetUserContextMode(Guid id, EditingContextMode mode)
  {
    if (id == Guid.Empty)
      return false;
    lock (this.currentContexts)
    {
      long userContextId = this.GetUserContextID(id);
      long modificationId = userContextId != 0L ? this.GetModificationID(id) : 0L;
      mode = userContextId != 0L ? mode : EditingContextMode.Default;
      this.currentContexts[id] = new CurrentEditingContext(userContextId, modificationId, mode);
    }
    return true;
  }

  public virtual CurrentEditingContext GetUserContext(Guid id)
  {
    lock (this.currentContexts)
    {
      if (this.currentContexts.ContainsKey(id))
        return this.currentContexts[id];
    }
    return CurrentEditingContext.Empty;
  }

  public virtual CurrentEditingContext SetUserContext(
    Guid id,
    long contextID,
    long modificationID,
    EditingContextMode mode)
  {
    if (id == Guid.Empty)
      return CurrentEditingContext.Empty;
    CurrentEditingContext currentEditingContext = new CurrentEditingContext(contextID, modificationID, mode);
    lock (this.currentContexts)
      this.currentContexts[id] = currentEditingContext;
    return currentEditingContext;
  }

  public virtual CurrentEditingContext SetUserContext(Guid id, CurrentEditingContext context)
  {
    if (id == Guid.Empty)
      return CurrentEditingContext.Empty;
    lock (this.currentContexts)
    {
      if (context == null)
      {
        if (this.currentContexts.ContainsKey(id))
          this.currentContexts.Remove(id);
      }
      else
        this.currentContexts[id] = context;
    }
    return this.GetUserContext(id);
  }

  public virtual void RemoveUsersContext(long contextID)
  {
    lock (this.currentContexts)
    {
      List<Guid> guidList = new List<Guid>();
      foreach (KeyValuePair<Guid, CurrentEditingContext> currentContext in this.currentContexts)
      {
        if (currentContext.Value.ContextID == contextID)
          guidList.Add(currentContext.Key);
      }
      if (guidList.Count == 0)
        return;
      foreach (Guid key in guidList)
        this.currentContexts.Remove(key);
    }
  }

  public virtual void ResetCache()
  {
    lock (this.syncRoot)
      this.contextsCache.Clear();
  }

  public virtual void RemoveFromCache(long modificationID)
  {
    if (modificationID == 0L)
      return;
    lock (this.syncRoot)
    {
      List<long> longList = new List<long>();
      foreach (KeyValuePair<long, DBEditingContextsService.CachedEditingContext> keyValuePair in this.contextsCache)
      {
        if (keyValuePair.Value.Container == null || keyValuePair.Value.Container.ModificationID == modificationID)
          longList.Add(keyValuePair.Key);
      }
      for (int index = 0; index < longList.Count; ++index)
        this.contextsCache.Remove(longList[index]);
    }
  }

  public virtual void RemoveVersionFromCache(long versionID, IList<long> fromContexts)
  {
    this.RemoveVersionsFromCache((IList<long>) new long[1]
    {
      versionID
    }, fromContexts);
  }

  public virtual void RemoveVersionsFromCache(IList<long> versionIDs, IList<long> fromContexts)
  {
    if (versionIDs == null || versionIDs.Count == 0)
      return;
    foreach (KeyValuePair<long, DBEditingContextsService.CachedEditingContext> keyValuePair in this.contextsCache.ToArray<KeyValuePair<long, DBEditingContextsService.CachedEditingContext>>())
    {
      for (int index = 0; index < versionIDs.Count; ++index)
      {
        if (fromContexts == null || fromContexts.IndexOf(Math.Abs(keyValuePair.Key)) >= 0)
          keyValuePair.Value.Container.DeleteVersion(versionIDs[index]);
      }
    }
  }

  public virtual void RemoveObjectFromCache(long fID, IList<long> fromContexts)
  {
    this.RemoveObjectsFromCache((IList<long>) new long[1]
    {
      fID
    }, fromContexts);
  }

  public virtual void RemoveObjectsFromCache(IList<long> fIDs, IList<long> fromContexts)
  {
    if (fIDs == null || fIDs.Count == 0)
      return;
    foreach (KeyValuePair<long, DBEditingContextsService.CachedEditingContext> keyValuePair in this.contextsCache.ToArray<KeyValuePair<long, DBEditingContextsService.CachedEditingContext>>())
    {
      for (int index = 0; index < fIDs.Count; ++index)
      {
        if (fromContexts == null || fromContexts.IndexOf(Math.Abs(keyValuePair.Key)) >= 0)
          keyValuePair.Value.Container.DeleteObject(fIDs[index]);
      }
    }
  }

  public virtual void UpdateModificationInCache(long contextID, long newModificationID)
  {
    if (contextID == 0L)
      return;
    lock (this.syncRoot)
    {
      if (!this.contextsCache.ContainsKey(contextID))
        return;
      DBEditingContextsService.CachedEditingContext cachedEditingContext = this.contextsCache[contextID];
      if (cachedEditingContext == null || cachedEditingContext.Container == null)
        return;
      cachedEditingContext.Container.ModificationID = newModificationID;
    }
  }

  public virtual bool HasUserContextSourceInfo(long userID, long roleID)
  {
    lock (this.сontextsSource)
      return this.сontextsSource.ContainsKey(new Tuple<long, long>(userID, roleID));
  }

  public virtual EditingContextSource GetUserContextSource(long userID, long roleID)
  {
    lock (this.сontextsSource)
    {
      Tuple<long, long> key = new Tuple<long, long>(userID, roleID);
      if (this.сontextsSource.ContainsKey(key))
        return this.сontextsSource[key];
    }
    return EditingContextSource.SessionContext;
  }

  public virtual void SetUserContextSource(long userID, long roleID, EditingContextSource value)
  {
    lock (this.сontextsSource)
      this.сontextsSource[new Tuple<long, long>(userID, roleID)] = value;
  }

  public virtual void RemoveUserContextSource(long userID, long roleID)
  {
    lock (this.сontextsSource)
    {
      Tuple<long, long> key = new Tuple<long, long>(userID, roleID);
      if (!this.сontextsSource.ContainsKey(key))
        return;
      this.сontextsSource.Remove(key);
    }
  }

  public virtual List<EditingContextsObjectVersion> SelectContextInfo(
    long contextID,
    long linkedContextNumber,
    IUserSession serverSession)
  {
    List<EditingContextsObjectVersion> contextsObjectVersionList = new List<EditingContextsObjectVersion>();
    if (!(serverSession is UserSession userSession) || contextID == 0L || linkedContextNumber == 0L)
      return contextsObjectVersionList;
    IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID));
    string commandText = string.Format("SELECT {1}, {2} FROM {0} WHERE {3} = :F_CONTEXT_ID", (object) "IMS_VERSIONS_CONTEXT", (object) "F_ID", (object) "F_OBJECT_ID", (object) "F_CONTEXT_ID");
    DataTable dataTable = userSession.DataManager.ExecuteDataTable(commandText, dbDataParameter);
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dataTable.Rows[index];
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        contextsObjectVersionList.Add(new EditingContextsObjectVersion(contextID, int64Value1, int64Value2, linkedContextNumber));
      }
    }
    return contextsObjectVersionList;
  }

  public virtual List<ObjectVersionDescription> SelectContextDescriptions(
    long contextID,
    long linkedContextID,
    IUserSession serverSession)
  {
    List<ObjectVersionDescription> versionDescriptionList = new List<ObjectVersionDescription>();
    if (!(serverSession is UserSession userSession) || contextID == 0L)
      return versionDescriptionList;
    SortedDictionary<int, List<long>> versions = this.SelectContextObjectTypes((IUserSession) userSession, contextID);
    List<object> objectList = ObjectVersionDescriptionsHelper.LoadObjectDescriptionsFast((IUserSession) userSession, typeof (ObjectVersionDescription), versions);
    for (int index = 0; index < objectList.Count; ++index)
    {
      if (objectList[index] is ObjectVersionDescription versionDescription)
        versionDescriptionList.Add(versionDescription);
    }
    if (versionDescriptionList.Contains(new ObjectVersionDescription()
    {
      F_OBJECT_ID = contextID
    }) || !(userSession.GetObjectActualCopy(Math.Abs(contextID), false) is IDBEditingContextsObject objectActualCopy))
      return versionDescriptionList;
    versionDescriptionList.Add(new ObjectVersionDescription((IDBObject) objectActualCopy));
    return versionDescriptionList;
  }

  public virtual List<EditingContextsObjectVersion> SelectContextsInfo(
    long contextID,
    long linkedContextNumber,
    IUserSession serverSession)
  {
    List<EditingContextsObjectVersion> contextsObjectVersionList = new List<EditingContextsObjectVersion>();
    if (!(serverSession is UserSession userSession) || contextID == 0L || linkedContextNumber == 0L)
      return contextsObjectVersionList;
    List<long> linkedContexts = this.GetLinkedContexts((object) userSession, linkedContextNumber);
    if (linkedContexts.IndexOf(Math.Abs(contextID)) < 0 && linkedContexts.IndexOf(-Math.Abs(contextID)) < 0)
      linkedContexts.Add(Math.Abs(contextID));
    if (linkedContexts.Count == 0)
      return contextsObjectVersionList;
    if (linkedContexts.Count == 1)
      return this.SelectContextInfo(linkedContexts[0], linkedContextNumber, (IUserSession) userSession);
    DataTable dataTable = (DataTable) null;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
      for (int index = 0; index < linkedContexts.Count; ++index)
      {
        stringBuilder.Append(index < linkedContexts.Count - 1 ? $":p{index.ToString()}," : $":p{index.ToString()}");
        dbDataParameterList.Add(userSession.DataManager.Parameter($":p{index.ToString()}", (object) Math.Abs(linkedContexts[index])));
      }
      string commandText = string.Format("SELECT {1}, {2}, {3} FROM {0} WHERE {3} IN ({4})", (object) "IMS_VERSIONS_CONTEXT", (object) "F_ID", (object) "F_OBJECT_ID", (object) "F_CONTEXT_ID", (object) stringBuilder.ToString());
      dataTable = userSession.DataManager.ExecuteDataTable(commandText, dbDataParameterList.ToArray());
    }
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dataTable.Rows[index];
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        long int64Value3 = DataSetProcessor.GetInt64Value(row, 2, 0L);
        contextsObjectVersionList.Add(new EditingContextsObjectVersion(int64Value3, int64Value1, int64Value2, linkedContextNumber));
      }
    }
    return contextsObjectVersionList;
  }

  public virtual SortedDictionary<int, List<long>> SelectContextObjectTypes(
    IUserSession serverSession,
    long contextID)
  {
    SortedDictionary<int, List<long>> sortedDictionary = new SortedDictionary<int, List<long>>();
    if (!(serverSession is UserSession userSession) || contextID == 0L)
      return sortedDictionary;
    long userId = userSession.UserID;
    IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":parF_CONTEXT_ID", (object) Math.Abs(contextID));
    string commandText = string.Format("SELECT DISTINCT A.{2}, B.{4}, B.{5} FROM {0} A, {1} B WHERE A.{3} = :parF_CONTEXT_ID AND A.{2} = B.{2}", (object) "IMS_VERSIONS_CONTEXT", (object) "IMS_OBJECTS", (object) "F_OBJECT_ID", (object) "F_CONTEXT_ID", (object) "F_OBJECT_TYPE", (object) "F_CHKOUT_BY");
    DataTable dataTable = userSession.DataManager.ExecuteDataTable(commandText, dbDataParameter);
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dataTable.Rows[index];
        long num = DataSetProcessor.GetInt64Value(row, 0, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(row, 1, -1);
        long int64Value = DataSetProcessor.GetInt64Value(row, 2, 0L);
        if (int32Value != -1 && num != 0L)
        {
          if (int64Value == userId)
            num = -Math.Abs(num);
          if (!sortedDictionary.ContainsKey(int32Value))
            sortedDictionary[int32Value] = new List<long>();
          sortedDictionary[int32Value].Add(num);
        }
      }
    }
    return sortedDictionary;
  }

  public virtual SortedDictionary<int, List<long>> SelectLinkedContextsObjectTypes(
    IUserSession serverSession,
    long modificationID)
  {
    SortedDictionary<int, List<long>> sortedDictionary = new SortedDictionary<int, List<long>>();
    if (!(serverSession is UserSession userSession) || modificationID == 0L)
      return sortedDictionary;
    IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":parF_MODIFICATION_ID", (object) modificationID);
    string commandText = string.Format("SELECT DISTINCT A.{2}, B.{4} FROM {0} A, {1} B WHERE A.{3} = :parF_MODIFICATION_ID AND A.{2} = B.{2}", (object) "IMS_VERSIONS_CONTEXT", (object) "IMS_OBJECTS", (object) "F_OBJECT_ID", (object) "F_MODIFICATION_ID", (object) "F_OBJECT_TYPE");
    DataTable dataTable = userSession.DataManager.ExecuteDataTable(commandText, dbDataParameter);
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dataTable.Rows[index];
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(row, 1, -1);
        if (int32Value != -1 && int64Value != 0L)
        {
          if (!sortedDictionary.ContainsKey(int32Value))
            sortedDictionary[int32Value] = new List<long>();
          sortedDictionary[int32Value].Add(int64Value);
        }
      }
    }
    return sortedDictionary;
  }

  public virtual List<ObjectVersionDescription> SelectContextsDescriptions(
    long contextID,
    IUserSession serverSession)
  {
    List<ObjectVersionDescription> versionDescriptionList1 = new List<ObjectVersionDescription>();
    if (!(serverSession is UserSession userSession) || contextID == 0L || !(userSession.GetObjectActualCopy(Math.Abs(contextID), false) is IDBEditingContextsObject objectActualCopy1))
      return versionDescriptionList1;
    versionDescriptionList1.Add(new ObjectVersionDescription((IDBObject) objectActualCopy1));
    long num = Math.Abs(objectActualCopy1.LinkedContextNumber);
    List<long> linkedContexts = this.GetLinkedContexts((object) userSession, num);
    if (linkedContexts.IndexOf(-contextID) >= 0)
      linkedContexts.Remove(-contextID);
    if (linkedContexts.IndexOf(contextID) >= 0)
      linkedContexts.Remove(contextID);
    linkedContexts.Insert(0, contextID);
    for (int index1 = 0; index1 < linkedContexts.Count; ++index1)
    {
      ObjectVersionDescription versionDescription = new ObjectVersionDescription();
      versionDescription.F_OBJECT_ID = linkedContexts[index1];
      List<ObjectVersionDescription> versionDescriptionList2 = this.SelectContextDescriptions(linkedContexts[index1], num, (IUserSession) userSession);
      for (int index2 = 0; index2 < versionDescriptionList2.Count; ++index2)
      {
        if (Math.Abs(versionDescriptionList2[index2].F_OBJECT_ID) != Math.Abs(contextID))
        {
          if ((versionDescriptionList2[index2].Options & ObjectVersionDescriptionOptions.InvalidDescription) == ObjectVersionDescriptionOptions.InvalidDescription)
          {
            IDBObject source = userSession.GetObject(versionDescriptionList2[index2].F_OBJECT_ID, false);
            if (source != null)
              versionDescriptionList2[index2].Assign((object) source);
            else
              continue;
          }
          if (!versionDescriptionList1.Contains(versionDescriptionList2[index2]))
            versionDescriptionList1.Add(versionDescriptionList2[index2]);
        }
      }
      if (!versionDescriptionList1.Contains(versionDescription) && userSession.GetObjectActualCopy(Math.Abs(linkedContexts[index1]), false) is IDBEditingContextsObject objectActualCopy2)
        versionDescriptionList1.Add(new ObjectVersionDescription((IDBObject) objectActualCopy2));
    }
    long userId = userSession.UserID;
    for (int index = versionDescriptionList1.Count - 1; index >= 0; --index)
    {
      ObjectVersionDescription versionDescription = versionDescriptionList1[index];
    }
    return versionDescriptionList1;
  }

  public virtual bool DeleteFromIMS_VERSIONS_CONTEXT(
    object usrSession,
    long versionID,
    bool exceptIfFail)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13934(1450431437), (object) "DBEditingContextsService.DeleteFromIMS_VERSIONS_CONTEXT");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService) || versionID == 0L)
        return false;
      IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":F_OBJECT_ID", (object) Math.Abs(versionID));
      string commandText = string.Format("DELETE FROM {0} WHERE {0}.{1} = :{1}", (object) "IMS_VERSIONS_CONTEXT", (object) "F_OBJECT_ID");
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter);
        if (customService.InTransaction)
          customService.Commit();
        this.RemoveVersionFromCache(versionID, (IList<long>) null);
        return true;
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
      }
    }
    return false;
  }

  public virtual bool Replace_ModificationID_IMS_VERSIONS_CONTEXT(
    object usrSession,
    long contextID,
    long newModificationID,
    bool exceptIfFail)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13935(320183559), (object) "DBEditingContextsService.Replace_ModificationID_IMS_VERSIONS_CONTEXT");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService) || contextID == 0L)
        return false;
      IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID));
      IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":NEW_MID", (object) Math.Abs(newModificationID));
      string commandText = $"UPDATE {"IMS_VERSIONS_CONTEXT"} SET {"F_MODIFICATION_ID"} = :NEW_MID WHERE {"F_CONTEXT_ID"} = :F_CONTEXT_ID";
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter2, dbDataParameter1);
        if (customService.InTransaction)
          customService.Commit();
        this.ResetCache();
        return true;
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
      }
    }
    return false;
  }

  public List<long> ClearModificationGroupID(
    object usrSession,
    List<long> versionIDs,
    bool exceptIfFail)
  {
    List<long> longList1 = new List<long>();
    if (versionIDs == null || versionIDs.Count == 0)
      return longList1;
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13936(733339611), (object) "DBEditingContextsService.ClearModificationGroupID");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService))
        return longList1;
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        List<long> longList2 = new List<long>();
        List<long> longList3 = new List<long>();
        List<long> longList4 = new List<long>();
        for (int index = 0; index < versionIDs.Count; ++index)
        {
          if (longList2.IndexOf(versionIDs[index]) < 0)
          {
            longList2.Add(versionIDs[index]);
            IDBObject objectActualCopy1 = userSession.GetObjectActualCopy(versionIDs[index], false);
            if (objectActualCopy1 != null && objectActualCopy1.ModificationID != 0L)
            {
              long num = Math.Abs(objectActualCopy1.ModificationID);
              if (num > 0L)
              {
                object obj = userSession.DataManager.ExecuteScalar("SELECT F_CONTEXT_ID FROM IMS_VERSIONS_CONTEXT WHERE F_OBJECT_ID = :objID", userSession.DataManager.Parameter("objID", (object) Math.Abs(objectActualCopy1.ObjectID)));
                if (obj == null || obj == DBNull.Value)
                {
                  (objectActualCopy1 as DBObject).SetModificationID(0L);
                  if (objectActualCopy1.ObjectID < 0L)
                  {
                    IDBObject dbObject = (IDBObject) (userSession.GetObject(Math.Abs(versionIDs[index]), false) as DBObject);
                    if (dbObject != null)
                      (dbObject as DBObject).SetModificationID(0L);
                  }
                  longList1.Add(versionIDs[index]);
                  continue;
                }
              }
              bool flag1 = false;
              bool flag2 = false;
              if (MetaDataHelper.IsObjectTypeEditingContext(objectActualCopy1.ObjectType) && objectActualCopy1.ModificationID != 0L)
              {
                flag1 = true;
                flag2 = true;
              }
              if (MetaDataHelper.IsObjectTypeEditingContext(objectActualCopy1.ObjectType) || objectActualCopy1.ModificationID == 0L || longList3.IndexOf(num) < 0)
              {
                if (!flag1 && longList4.IndexOf(num) < 0)
                {
                  IDBObject objectActualCopy2 = userSession.GetObjectActualCopy(Math.Abs(num), false);
                  if (objectActualCopy2 != null)
                  {
                    longList3.Add(Math.Abs(objectActualCopy2.ObjectID));
                    continue;
                  }
                  longList4.Add(num);
                }
                if (flag2 || longList4.IndexOf(num) >= 0)
                {
                  (objectActualCopy1 as DBObject).SetModificationID(0L);
                  if (objectActualCopy1.ObjectID < 0L)
                  {
                    IDBObject dbObject = (IDBObject) (userSession.GetObject(Math.Abs(versionIDs[index]), false) as DBObject);
                    if (dbObject != null)
                      (dbObject as DBObject).SetModificationID(0L);
                  }
                  longList1.Add(versionIDs[index]);
                }
              }
            }
          }
        }
        if (customService.InTransaction)
          customService.Commit();
      }
      catch
      {
        customService.Rollback();
        if (exceptIfFail | inTransaction)
          throw;
        longList1.Clear();
      }
    }
    return longList1;
  }

  public void ForceClearModificationGroupID(
    object usrSession,
    List<long> versionIDs,
    bool exceptIfFail)
  {
    if (versionIDs == null || versionIDs.Count == 0)
      return;
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13916.ssp_appserver_13937(557899293), (object) "DBEditingContextsService.ClearModificationGroupID");
    lock (this.syncRoot4Modify)
    {
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService))
        return;
      bool inTransaction = customService.InTransaction;
      try
      {
        customService.StartTransaction();
        List<long> longList = new List<long>();
        for (int index = 0; index < versionIDs.Count; ++index)
        {
          if (longList.IndexOf(versionIDs[index]) < 0)
          {
            longList.Add(versionIDs[index]);
            IDBObject objectActualCopy = userSession.GetObjectActualCopy(versionIDs[index], false);
            if (objectActualCopy != null && objectActualCopy.ModificationID != 0L)
            {
              Math.Abs(objectActualCopy.ModificationID);
              (objectActualCopy as DBObject).SetModificationID(0L);
              if (objectActualCopy.ObjectID < 0L)
              {
                IDBObject dbObject = (IDBObject) (userSession.GetObject(Math.Abs(versionIDs[index]), false) as DBObject);
                if (dbObject != null)
                  (dbObject as DBObject).SetModificationID(0L);
              }
            }
          }
        }
        if (!customService.InTransaction)
          return;
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        if (!(exceptIfFail | inTransaction))
          return;
        throw;
      }
    }
  }

  private sealed class CachedEditingContext
  {
    public EditingContextsObjectContainer Container;
    public DateTime LoadTime = DateTime.MinValue;

    public CachedEditingContext()
    {
    }

    public CachedEditingContext(EditingContextsObjectContainer container)
    {
      this.Container = container;
      if (this.Container == null || this.Container.ContextID == 0L || this.Container.ModificationID == 0L || this.Container.Objects == null)
        return;
      this.LoadTime = DateTime.UtcNow;
    }

    public override bool Equals(object obj)
    {
      return obj is DBEditingContextsService.CachedEditingContext cachedEditingContext && this.Container != null && cachedEditingContext.Container != null && this.Container.ContextID == cachedEditingContext.Container.ContextID && this.Container.ModificationID == cachedEditingContext.Container.ModificationID;
    }

    public override int GetHashCode()
    {
      return this.Container == null ? base.GetHashCode() : this.Container.ContextID.GetHashCode();
    }
  }
}
