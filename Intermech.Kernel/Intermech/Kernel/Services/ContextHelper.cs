// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ContextHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services;

internal static class ContextHelper
{
  public static long FindContextID(UserSession session, object guid)
  {
    object obj = session.DataManager.ExecuteScalar("SELECT F_ID FROM IMS_GUID_RESOLVE WHERE F_GUID = :guid AND F_CATEGORY_TYPE = :categoryID", session.DataManager.Parameter(nameof (guid), (object) new Guid((string) guid)), session.DataManager.Parameter("categoryID", (object) 27));
    return obj == DBNull.Value || obj == null ? 0L : Convert.ToInt64(obj);
  }

  public static Guid FindContextGuid(UserSession session, long modifID, bool create)
  {
    object obj = session.DataManager.ExecuteScalar("SELECT F_GUID FROM IMS_GUID_RESOLVE WHERE F_ID = :modifID AND F_CATEGORY_TYPE = :categoryID", session.DataManager.Parameter(nameof (modifID), (object) modifID), session.DataManager.Parameter("categoryID", (object) 27));
    if (obj != DBNull.Value && obj != null)
      return new Guid(Convert.ToString(obj));
    if (!create)
      return Guid.Empty;
    Guid guid = Guid.NewGuid();
    ContextHelper.WriteNewContext(session, guid, modifID);
    return guid;
  }

  public static void WriteNewContext(UserSession session, Guid guid, long modifID)
  {
    session.DataManager.ExecuteNonQuery("INSERT INTO IMS_GUID_RESOLVE (F_GUID, F_ID, F_CATEGORY_TYPE) VALUES (:guid, :modifID, :categoryID)", session.DataManager.Parameter(nameof (guid), (object) guid), session.DataManager.Parameter(nameof (modifID), (object) modifID), session.DataManager.Parameter("categoryID", (object) 27));
  }

  public static bool GetContextContents(
    UserSession session,
    long contextID,
    out Guid modificationID,
    out List<long> objectIDs)
  {
    modificationID = Guid.Empty;
    objectIDs = (List<long>) null;
    DataTable dataTable = session.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID, F_MODIFICATION_ID FROM IMS_VERSIONS_CONTEXT WHERE F_CONTEXT_ID=:contextID", session.DataManager.Parameter(nameof (contextID), (object) contextID));
    if (dataTable.Rows.Count <= 0)
      return false;
    modificationID = ContextHelper.FindContextGuid(session, Convert.ToInt64(dataTable.Rows[0][1]), false);
    objectIDs = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      objectIDs.Add(Convert.ToInt64(row[0]));
    return true;
  }

  public static bool GetContextContents(
    UserSession session,
    long contextID,
    out long modificationID,
    out List<long> objectIDs)
  {
    modificationID = 0L;
    objectIDs = (List<long>) null;
    Guid modificationID1 = Guid.Empty;
    int num = ContextHelper.GetContextContents(session, contextID, out modificationID1, out objectIDs) ? 1 : 0;
    if (num == 0)
      return num != 0;
    modificationID = ContextHelper.FindContextID(session, (object) modificationID1.ToString());
    return num != 0;
  }

  public static void RestoreChangesGroupNums(
    IUserSession session,
    IEventLogHelper eventHelper,
    List<Tuple<long, Guid, Guid>> changesGroupNums)
  {
    if (changesGroupNums.Count <= 0)
      return;
    List<Tuple<Guid, long>> tupleList = new List<Tuple<Guid, long>>();
    foreach (Tuple<long, Guid, Guid> changesGroupNum in changesGroupNums)
    {
      Tuple<long, Guid, Guid> cgn = changesGroupNum;
      Tuple<Guid, long> tuple = tupleList.Find((Predicate<Tuple<Guid, long>>) (x => x.Item1.Equals(cgn.Item3)));
      long contextId;
      if (tuple != null)
      {
        contextId = tuple.Item2;
      }
      else
      {
        contextId = ContextHelper.FindContextID(session as UserSession, (object) cgn.Item3.ToString());
        if (contextId == 0L)
        {
          eventHelper.AddToTrace($"Для объекта {cgn.Item1} не найден номер группы изменений. Возможно контекст не был импортирован.", Consts.traceAlways, string.Empty);
          continue;
        }
        tupleList.Add(new Tuple<Guid, long>(cgn.Item3, contextId));
      }
      (session.GetObject(cgn.Item1) as DBObject).SetModificationID(contextId);
    }
  }

  public static void RestoreContexts(
    IUserSession session,
    List<Tuple<Guid, Guid, Guid[]>> contexts,
    Dictionary<Guid, ImportedInfo> links)
  {
    if (contexts.Count <= 0)
      return;
    IDbManager dataManager = (session as UserSession).DataManager;
    bool flag = false;
    foreach (Tuple<Guid, Guid, Guid[]> context in contexts)
    {
      long objectId = links[context.Item1].ObjectId;
      long contextId = ContextHelper.FindContextID(session as UserSession, (object) context.Item2.ToString());
      foreach (Guid key in context.Item3)
      {
        ImportedInfo link = links[key];
        object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_VERSIONS_CONTEXT WHERE F_CONTEXT_ID=:f_context_id AND F_ID=:f_id AND F_MODIFICATION_ID=:f_modif_id", dataManager.Parameter("f_context_id", (object) objectId), dataManager.Parameter("f_id", (object) link.Id), dataManager.Parameter("f_modif_id", (object) contextId));
        if (obj == null || obj == DBNull.Value)
        {
          dataManager.AddBatchSQL("INSERT INTO IMS_VERSIONS_CONTEXT (F_CONTEXT_ID, F_ID, F_OBJECT_ID, F_MODIFICATION_ID) VALUES (:f_context_id, :f_id, :f_object_id, :f_modif_id)", new DbCommandParam[4]
          {
            dataManager.BatchParameter("f_context_id", DbType.Int64, (object) objectId),
            dataManager.BatchParameter("f_id", DbType.Int64, (object) link.Id),
            dataManager.BatchParameter("f_object_id", DbType.Int64, (object) link.ObjectId),
            dataManager.BatchParameter("f_modif_id", DbType.Int64, (object) contextId)
          });
          flag = true;
        }
        else if (Convert.ToInt64(obj) != link.ObjectId)
          dataManager.ExecuteScalar("UPDATE IMS_VERSIONS_CONTEXT SET F_OBJECT_ID=:f_object_id WHERE F_CONTEXT_ID=:f_context_id AND F_ID=:f_id AND F_MODIFICATION_ID=:f_modif_id", dataManager.Parameter("f_object_id", (object) link.ObjectId), dataManager.Parameter("f_context_id", (object) objectId), dataManager.Parameter("f_id", (object) link.Id), dataManager.Parameter("f_modif_id", (object) contextId));
      }
    }
    if (!flag)
      return;
    dataManager.ExecuteBatchSQL();
  }

  public static void ClearContext(IUserSession session, long contextID)
  {
    List<long> versionIDs = new List<long>();
    IDbManager dataManager = (session as UserSession).DataManager;
    foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_VERSIONS_CONTEXT WHERE F_CONTEXT_ID = :F_CONTEXT_ID", dataManager.Parameter(":F_CONTEXT_ID", (object) Math.Abs(contextID))).Rows)
      versionIDs.Add(Convert.ToInt64(row[0]));
    if (versionIDs.Count <= 0)
      return;
    (ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService as DBEditingContextsService).DeleteFromContext((object) session, contextID, (IList<long>) versionIDs, true, true, false);
  }
}
