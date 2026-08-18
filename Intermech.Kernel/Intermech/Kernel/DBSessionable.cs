// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBSessionable
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Localization;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;


namespace Intermech.Kernel;

public abstract class DBSessionable : DBStoredObject, IDBLocalizable, IDBLastAccessInfo
{
  protected long _LastEventID;
  private UserSession _UserSession;
  protected int _CategoryType;
  protected long _CategoryID;
  protected internal bool _Deleted;
  protected Dictionary<ActionType, bool> AccessActions;
  internal bool LoggingOn = true;
  public static ListDictionary AccessLogKeywords = new ListDictionary();
  internal bool _LastDeny;
  internal bool _GrantAlways;
  internal bool _LastDefault;
  public const string logDelimiter = "-";
  public const string blankString = " ";
  internal const string logS1 = "1";
  internal const string logS2 = "2";
  internal const string logS3 = "3";
  internal const string logS4 = "4";
  internal const string logS5 = "5";
  internal const string logS6 = "6";
  internal const string logS7 = "7";
  public const int LastChecksInLog = 10;
  public const int LastChecksInLogCache = 5;
  public const int MinLogListRecords = 100;
  public const int MaxLogListRecords = 300;
  internal long _AccessOwnerID;
  internal List<long> _ExtendedUserID;
  protected bool UseAccessCache = true;
  protected bool EnableCheckAccessLog = true;
  internal DataTable _AccessCacheTable;
  internal string _CheckAccessSQL;
  private ActionType currActionType = ActionType.Unknown;
  private HybridDictionary _pluginsData;

  static DBSessionable()
  {
    DBSessionable.AccessLogKeywords[(object) "-"] = (object) "------------------------------------";
    DBSessionable.AccessLogKeywords[(object) " "] = (object) " ";
    DBSessionable.AccessLogKeywords[(object) "1"] = (object) LocalizationHolder.rm.GetString("Kernel_800");
    DBSessionable.AccessLogKeywords[(object) "2"] = (object) LocalizationHolder.rm.GetString("Kernel_801");
    DBSessionable.AccessLogKeywords[(object) "3"] = (object) LocalizationHolder.rm.GetString("Kernel_802");
    DBSessionable.AccessLogKeywords[(object) "4"] = (object) LocalizationHolder.rm.GetString("Kernel_803");
    DBSessionable.AccessLogKeywords[(object) "5"] = (object) LocalizationHolder.rm.GetString("Kernel_804");
    DBSessionable.AccessLogKeywords[(object) "6"] = (object) LocalizationHolder.rm.GetString("Kernel_805");
    DBSessionable.AccessLogKeywords[(object) "7"] = (object) LocalizationHolder.rm.GetString("AlwaysAllow");
  }

  public DBSessionable(UserSession uSession) => this._UserSession = uSession;

  public UserSession UserSession
  {
    [DebuggerStepThrough] get => this._UserSession;
  }

  public bool ActionTypeExists(ActionType anAction) => this.AccessActions.ContainsKey(anAction);

  public Dictionary<ActionType, ActionCategory> GetAccessTypesCategory()
  {
    Dictionary<ActionType, ActionCategory> accessTypesCategory = new Dictionary<ActionType, ActionCategory>();
    foreach (ActionType key in this.AccessActions.Keys)
      accessTypesCategory.Add(key, this.GetActionCategory(key));
    return accessTypesCategory;
  }

  protected virtual string GetExtendedAccessSQL()
  {
    if (this._CheckAccessSQL != null)
      return this._CheckAccessSQL;
    return this.IsUserOwner() ? this.UserSession.IdentHelper.OwnerGroupID.ToString() : string.Empty;
  }

  public int LoadCacheTable(ActionType atype)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    this._AccessCacheTable = dataManager.ExecuteDataTable($"SELECT F_RIGHT_TYPE, F_USER_ID, F_BEGIN_DATE, F_END_DATE, F_CATEGORY_ID FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = :ct AND F_RIGHT_ID = :at AND F_USER_ID IN ({this.UserSession.DBSecurity.GetGroupsSQL(this.GetExtendedAccessSQL())}) ORDER BY F_CATEGORY_ID ASC, F_RIGHT_TYPE DESC", dataManager.Parameter("ct", (object) this._CategoryType), dataManager.Parameter("at", (object) (int) atype));
    this.currActionType = atype;
    return this._AccessCacheTable.Rows.Count;
  }

  public int LoadCacheTable(ActionType atype, long minCategoryID, long maxCategoryID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    this._AccessCacheTable = dataManager.ExecuteDataTable($"SELECT F_RIGHT_TYPE, F_USER_ID, F_BEGIN_DATE, F_END_DATE, F_CATEGORY_ID FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = :ct AND (F_CATEGORY_ID BETWEEN :catID1 AND :catID2) AND F_RIGHT_ID = :at AND F_USER_ID IN ({this.UserSession.DBSecurity.GetGroupsSQL(this.GetExtendedAccessSQL())}) ORDER BY F_CATEGORY_ID ASC, F_RIGHT_TYPE DESC", dataManager.Parameter("catID1", (object) minCategoryID), dataManager.Parameter("catID2", (object) maxCategoryID), dataManager.Parameter("ct", (object) this._CategoryType), dataManager.Parameter("at", (object) (int) atype));
    this.currActionType = atype;
    return this._AccessCacheTable.Rows.Count;
  }

  public void ClearCacheTable()
  {
    this.currActionType = ActionType.Unknown;
    this._AccessCacheTable = (DataTable) null;
  }

  private AccessType GetAccessTypeInCache(
    long categoryID,
    out long userID,
    DataRow[] rows_param,
    ActionType at)
  {
    if (this.currActionType != at)
      throw new KernelException($"Кэш прав доступа был загружен для прав доступа {this.currActionType}. Проверка производится для права {at}.");
    DataRow[] dataRowArray = rows_param == null ? this._AccessCacheTable.Select("F_CATEGORY_ID = " + categoryID.ToString()) : rows_param;
    for (int index = 0; index < dataRowArray.Length; ++index)
    {
      AccessType int32 = (AccessType) Convert.ToInt32(dataRowArray[index][0]);
      userID = Convert.ToInt64(dataRowArray[index][1]);
      if (dataRowArray[index][2] != DBNull.Value && dataRowArray[index][3] != DBNull.Value)
      {
        DateTime utcNow = DateTime.UtcNow;
        if (Convert.ToDateTime(dataRowArray[index][2]) > utcNow || Convert.ToDateTime(dataRowArray[index][3]) < utcNow)
          continue;
      }
      return int32;
    }
    userID = 0L;
    return AccessType.Default;
  }

  public IUserSession Session
  {
    [DebuggerStepThrough] get => (IUserSession) this._UserSession;
  }

  protected IEventLogHelper EventHelper
  {
    [DebuggerStepThrough] get => this.UserSession.EventLogHelper;
  }

  public long AddEvent(
    long objectID,
    ActionType eventType,
    EventlogRecordType auditType,
    string note)
  {
    return this.AddEvent(objectID, 0L, eventType, auditType, note);
  }

  public virtual long AddEvent(
    long objectID,
    long relationID,
    ActionType eventType,
    EventlogRecordType auditType,
    string note)
  {
    this._LastEventID = !this.LoggingOn || !this.UserSession.LoggingOn ? 0L : this.EventHelper.AddEvent(objectID, relationID, this._CategoryType, this._CategoryID, this.ObjectName, note, eventType, auditType, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
    return this._LastEventID;
  }

  internal virtual long AddEvent(
    long objectID,
    long relationID,
    long categoryID,
    int categoryType,
    ActionType eventType,
    EventlogRecordType auditType,
    string note)
  {
    this._LastEventID = !this.LoggingOn || !this.UserSession.LoggingOn ? 0L : this.EventHelper.AddEvent(objectID, relationID, categoryType, categoryID, this.ObjectName, note, eventType, auditType, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
    return this._LastEventID;
  }

  public long AddEvent(long objectID, ActionType eventType, EventlogRecordType auditType)
  {
    return this.AddEvent(objectID, 0L, eventType, auditType, "");
  }

  public long AddEvent(
    long objectID,
    long relationID,
    ActionType eventType,
    EventlogRecordType auditType)
  {
    return this.AddEvent(objectID, relationID, eventType, auditType, "");
  }

  public long CloseEvent(long EventID, EventlogRecordType AuditType, string Note)
  {
    return this.EventHelper.CloseEvent(EventID, AuditType, Note, (IUserSession) this.UserSession);
  }

  public long CloseEvent(long EventID, EventlogRecordType AuditType)
  {
    return this.EventHelper.CloseEvent(EventID, AuditType, "$NO$", (IUserSession) this.UserSession);
  }

  public long CloseEvent(EventlogRecordType AuditType)
  {
    return this.EventHelper.CloseEvent(this._LastEventID, AuditType, "$NO$", (IUserSession) this.UserSession);
  }

  public virtual bool Deleted
  {
    [DebuggerStepThrough] get => this._Deleted;
    protected set
    {
      if (this._Deleted == value)
        return;
      this._Deleted = value;
      this.ClearLanguages();
    }
  }

  public ListDictionary ActionNames
  {
    [DebuggerStepThrough] get => (ListDictionary) null;
  }

  public virtual string ObjectName
  {
    [DebuggerStepThrough] get => string.Empty;
  }

  public virtual string ObjectNameEx
  {
    [DebuggerStepThrough] get => this.ObjectName;
  }

  protected virtual void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.AccessActions = new Dictionary<ActionType, bool>(4);
    this.AccessActions.Add(ActionType.GetAccess, false);
    this.AccessActions.Add(ActionType.SetAccess, false);
    this._CategoryType = aCategoryType;
    this._CategoryID = aCategoryID;
  }

  protected void InitStaticSecurityOptions(
    int aCategoryType,
    long aCategoryID,
    Dictionary<ActionType, bool> actions)
  {
    this.AccessActions = actions;
    this._CategoryType = aCategoryType;
    this._CategoryID = aCategoryID;
  }

  public virtual bool CheckAccess(ActionType anAction, bool aDefaultAccess, CheckAccessFlags flags)
  {
    if (this._Deleted)
      throw new AlreadyDeletedException();
    if (this._CategoryType == 0)
      throw new KernelException(sc_12771.ssp_appserver_12772());
    bool flag = this.DoCheckAccess(new CategoryValue(this._CategoryType, this._CategoryID, anAction), (flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException, aDefaultAccess, (flags & CheckAccessFlags.BatchCheck) == CheckAccessFlags.None);
    if (this._LastEventID > 0L & flag)
      this._LastEventID = this.CloseEvent(EventlogRecordType.AccessGranted);
    return flag;
  }

  public bool CheckAccess(ActionType anAction, bool aDefaultAccess, bool aThrowACException)
  {
    return aThrowACException ? this.CheckAccess(anAction, aDefaultAccess, CheckAccessFlags.ThrowACException) : this.CheckAccess(anAction, aDefaultAccess, CheckAccessFlags.None);
  }

  public bool CheckAccess(ActionType anAction, bool aDefaultAccess)
  {
    return this.CheckAccess(anAction, aDefaultAccess, true);
  }

  public bool CheckAccess(ActionType anAction)
  {
    bool aDefaultAccess;
    return this.AccessActions.TryGetValue(anAction, out aDefaultAccess) ? this.CheckAccess(anAction, aDefaultAccess, true) : throw new KernelException(string.Format(sc_12771.ssp_appserver_12773(), (object) anAction));
  }

  public virtual long AccessOwnerID
  {
    [DebuggerStepThrough] get => this._AccessOwnerID;
  }

  public virtual bool IsUserOwner() => this.AccessOwnerID == this.UserSession.UserID;

  protected virtual List<long> GetExtendedUserID() => this._ExtendedUserID;

  private void GetAccessDataFunc(IDataReader reader, ExecuteReaderArgs args)
  {
    while (reader.Read())
    {
      Tuple<AccessType, long> tuple = new Tuple<AccessType, long>((AccessType) Convert.ToInt32(reader.GetValue(0)), Convert.ToInt64(reader.GetValue(1)));
      object obj1 = reader.GetValue(2);
      object obj2 = reader.GetValue(3);
      if (obj1 != DBNull.Value && obj2 != DBNull.Value)
      {
        DateTime utcNow = DateTime.UtcNow;
        if (Convert.ToDateTime(obj1) > utcNow || Convert.ToDateTime(obj2) < utcNow)
          continue;
      }
      args.Result = (object) tuple;
      return;
    }
    args.Result = (object) new Tuple<AccessType, long>(AccessType.Default, 0L);
  }

  protected virtual long AccessConditionID => 0;

  public virtual bool EnabledConditionAccess => false;

  private AccessType GetAccessType(CategoryValue aCategory, out long userID)
  {
    if (this._AccessCacheTable != null)
      return this.GetAccessTypeInCache(aCategory.CategoryID, out userID, (DataRow[]) null, aCategory.ActionID);
    IDbManager dataManager = this.UserSession.DataManager;
    string str1 = this.UserSession.DBSecurity.GetGroupsSQL(this.GetExtendedAccessSQL());
    List<long> extendedUserId = this.GetExtendedUserID();
    if (extendedUserId != null)
    {
      for (int index = 0; index < extendedUserId.Count; ++index)
        str1 = $"{str1},{extendedUserId[index].ToString()}";
    }
    string str2 = string.Empty;
    if (this.EnabledConditionAccess)
    {
      long accessConditionId = this.AccessConditionID;
      if (accessConditionId > 0L)
        str2 = $" AND F_CONDITION_ID IN (0, {accessConditionId})";
    }
    ExecuteReaderArgs args = new ExecuteReaderArgs((object) null);
    dataManager.ExecuteReader($"SELECT F_RIGHT_TYPE, F_USER_ID, F_BEGIN_DATE, F_END_DATE FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = :ct AND F_CATEGORY_ID = :ci AND F_RIGHT_ID = :at AND F_USER_ID IN ({str1}){str2} ORDER BY F_RIGHT_TYPE DESC", new ExecuteReaderDelegate(this.GetAccessDataFunc), args, dataManager.Parameter("ct", (object) aCategory.CategoryType), dataManager.Parameter("ci", (object) aCategory.CategoryID), dataManager.Parameter("at", (object) (int) aCategory.ActionID));
    Tuple<AccessType, long> result = args.Result as Tuple<AccessType, long>;
    userID = result.Item2;
    return result.Item1;
  }

  public void RestoreAdminAccess()
  {
    if (this.UserSession.UserID != this.UserSession.IdentHelper.SysdbaID)
      throw new KernelExceptionID(sc_12771.ssp_appserver_12774(1336823633));
    this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = :ct AND F_CATEGORY_ID = :ci AND F_USER_ID IN (:ui1, :ui2)", this.UserSession.DataManager.Parameter("ct", (object) this._CategoryType), this.UserSession.DataManager.Parameter("ci", (object) Math.Abs(this._CategoryID)), this.UserSession.DataManager.Parameter("ui1", (object) this.UserSession.IdentHelper.AdminRoleID), this.UserSession.DataManager.Parameter("ui2", (object) this.UserSession.IdentHelper.SysdbaID));
    this.UserSession.DBSecurity.ClearCacheForGroup(this.UserSession.IdentHelper.AdminRoleID, new CategoryValue(0, Math.Abs(this._CategoryID), ActionType.Any));
  }

  public virtual long GetCategoryID4ActionName(long _categoryID) => _categoryID;

  protected bool DoCheckMetadataAccess(
    CategoryValue aCategory,
    DataRow[] access_rows,
    ActionType actType)
  {
    bool flag;
    switch (this.GetAccessTypeInCache(aCategory.CategoryID, out long _, access_rows, actType))
    {
      case AccessType.NoGrant:
        flag = false;
        break;
      case AccessType.Grant:
        flag = true;
        break;
      case AccessType.Deny:
        flag = false;
        break;
      case AccessType.GrantAlways:
        flag = true;
        break;
      default:
        flag = this.GetDefaultAccess(aCategory.ActionID);
        break;
    }
    return flag;
  }

  protected virtual int GetCheckAccessHash() => 0;

  internal bool DoCheckAccess(
    CategoryValue aCategory,
    bool ThrowACException,
    bool DefaultAccess,
    bool addDelimiter)
  {
    aCategory.CategoryID = Math.Abs(aCategory.CategoryID);
    string str = string.Empty;
    bool enableCheckAccessLog = this.EnableCheckAccessLog;
    if (enableCheckAccessLog)
    {
      if (addDelimiter)
        this.UserSession.LogList.Add("-");
      else
        this.UserSession.LogList.Add(" ");
      str = this.UserSession.EventLogHelper.GetActionName(aCategory.CategoryType, this.GetCategoryID4ActionName(aCategory.CategoryID), aCategory.ActionID);
      this.UserSession.LogList.Add(string.Format("1" + LocalizationHolder.rm.GetString("Kernel_806"), (object) str, (object) this.ObjectNameEx));
    }
    AccessInfo accessInfo;
    if (this.UseAccessCache)
    {
      ((IDBSecurityCache) this.UserSession.DBSecurity).ClearCacheIfNeed();
      accessInfo = ((IDBSecurityCache) this.UserSession.DBSecurity).CheckAccessInCache(aCategory);
      if (accessInfo != null && accessInfo.CheckAccessHashCode != this.GetCheckAccessHash())
        accessInfo = (AccessInfo) null;
    }
    else
      accessInfo = (AccessInfo) null;
    this._LastDeny = false;
    this._LastDefault = false;
    this._GrantAlways = false;
    bool result;
    if (accessInfo == null)
    {
      result = this.GetDefaultAccess(aCategory.ActionID);
      long userID;
      switch (this.GetAccessType(aCategory, out userID))
      {
        case AccessType.Default:
          this._LastDefault = true;
          if (enableCheckAccessLog)
          {
            str = !result ? "6" : "5";
            break;
          }
          break;
        case AccessType.NoGrant:
          str = "3";
          result = false;
          goto default;
        case AccessType.Grant:
          str = "2";
          result = true;
          goto default;
        case AccessType.Deny:
          str = "4";
          this._LastDeny = true;
          result = false;
          goto default;
        case AccessType.GrantAlways:
          str = "7";
          this._GrantAlways = true;
          result = true;
          goto default;
        default:
          if (enableCheckAccessLog)
          {
            QuickObjectInfo objectInfo = this.UserSession.DBCache.GetObjectInfo(this.UserSession.DataManager, userID);
            str = (objectInfo.ObjectTypeID != this.UserSession.IdentHelper.GroupsTypeID ? (objectInfo.ObjectTypeID != this.UserSession.IdentHelper.UsersTypeID ? (objectInfo.ObjectTypeID != this.UserSession.IdentHelper.RolesTypeID ? str + LocalizationHolder.rm.GetString("Kernel_810") : str + LocalizationHolder.rm.GetString("Kernel_809")) : str + LocalizationHolder.rm.GetString("Kernel_808")) : str + LocalizationHolder.rm.GetString("Kernel_807")) + objectInfo.Caption;
            break;
          }
          break;
      }
      if (enableCheckAccessLog)
        this.UserSession.LogList.Add(str);
      if (this.UseAccessCache)
        ((IDBSecurityCache) this.UserSession.DBSecurity).AddToCache(aCategory, new AccessInfo(result, this._LastDeny, this._LastDefault, this._GrantAlways, this.UserSession.LogList, this.GetCheckAccessHash()));
    }
    else
    {
      result = accessInfo.Result;
      this._LastDeny = accessInfo.DenyMode;
      this._GrantAlways = accessInfo.GrantAlwaysMode;
      this._LastDefault = accessInfo.DefaultAccess;
      if (enableCheckAccessLog)
        this.UserSession.LogList.Add(accessInfo.CheckLogString[accessInfo.CheckLogString.Count - 1]);
    }
    if (enableCheckAccessLog && this.UserSession.LogList.Count > 300)
      this.UserSession.LogList.RemoveRange(0, 200);
    return !(!result & ThrowACException) ? result : throw new AccessDeniedException((IUserSession) this.UserSession);
  }

  public IUserSession GetSession() => (IUserSession) this.UserSession;

  protected virtual CategoryValue[] GetChildsList() => (CategoryValue[]) null;

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get => this._LastDeny;
  }

  public bool IsAccessTypeGrantAlways
  {
    [DebuggerStepThrough] get => this._GrantAlways;
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get => this._LastDefault;
  }

  public virtual void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    try
    {
      if (!this.CheckAccess(ActionType.SetAccess))
        this.AddEvent(this.ObjectID, ActionType.SetAccess, EventlogRecordType.AccessDenied);
    }
    catch
    {
      this.AddEvent(this.ObjectID, ActionType.SetAccess, EventlogRecordType.AccessDenied);
      throw;
    }
    IDbManager dataManager = this.UserSession.DataManager;
    this.UserSession.StartTransaction();
    try
    {
      int columnIndex = accessList.Columns.IndexOf("F_CATEGORY_ID");
      for (int index = accessList.Rows.Count - 1; index >= 0; --index)
      {
        if (Convert.ToInt32(accessList.Rows[index]["F_RIGHT_TYPE"]) == Consts.DeleteRecord && Convert.ToInt32(accessList.Rows[index]["F_KEY"]) > 0)
        {
          dataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE F_KEY = :id OR F_PARENT_KEY = :id", dataManager.Parameter("id", (object) Convert.ToInt64(accessList.Rows[index]["F_KEY"])));
          accessList.Rows[index].Delete();
        }
        else if (Convert.ToInt64(accessList.Rows[index][columnIndex]) < 0L)
          accessList.Rows[index][columnIndex] = (object) -Convert.ToInt64(accessList.Rows[index][columnIndex]);
      }
      accessList.AcceptChanges();
      for (int index1 = 0; index1 < accessList.Rows.Count; ++index1)
      {
        DataRow row = accessList.Rows[index1];
        if (Convert.ToInt32(row["F_RIGHT_TYPE"]) != Consts.DeleteRecord && Convert.ToInt32(row["F_PARENT_KEY"]) != -1)
        {
          long int64 = Convert.ToInt64(row["F_USER_ID"]);
          int int32 = Convert.ToInt32(row["F_RIGHT_ID"]);
          if (int64 == this.UserSession.IdentHelper.AdminRoleID && (int32 == 19 || int32 == 18) && this.GetActionCategory((ActionType) int32) == ActionCategory.Admin && Convert.ToInt32(accessList.Rows[index1]["F_RIGHT_TYPE"]) != 2)
          {
            bool flag = false;
            for (int index2 = 0; index2 < accessList.Rows.Count; ++index2)
            {
              if (index2 != index1 && Convert.ToInt64(accessList.Rows[index2]["F_USER_ID"]) != int64 && Convert.ToInt32(accessList.Rows[index2]["F_RIGHT_ID"]) == int32 && Convert.ToInt32(accessList.Rows[index2]["F_RIGHT_TYPE"]) == 2)
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              throw new KernelExceptionID(sc_12771.ssp_appserver_12775(950829868));
          }
        }
      }
      DataTable accessList1 = this.GetAccessList(out ActionProperties[] _, out QuickObjectInfo[] _);
      foreach (DataRow row in (InternalDataCollectionBase) accessList.Rows)
      {
        if (Convert.ToInt64(row["F_PARENT_KEY"]) == 0L)
        {
          long int64_1 = Convert.ToInt64(row["F_KEY"]);
          int int32 = Convert.ToInt32(row["F_RIGHT_TYPE"]);
          if (int32 == Consts.DeleteRecord)
          {
            if (int64_1 > 0L)
              dataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE F_KEY = :id OR F_PARENT_KEY = :id", dataManager.Parameter("id", (object) int64_1));
          }
          else
          {
            this.UserSession.DBSecurity.ClearCacheForGroup(Convert.ToInt64(row["F_USER_ID"]), new CategoryValue(this._CategoryType, Math.Abs(this._CategoryID), (ActionType) Convert.ToInt32(row["F_RIGHT_ID"])));
            if (int64_1 > 0L)
            {
              DataRow[] dataRowArray = accessList1.Select("F_KEY = " + row["F_KEY"].ToString());
              if (dataRowArray.Length != 0)
              {
                if (row["F_BEGIN_DATE"] is DateTime)
                  row["F_BEGIN_DATE"] = (object) (Convert.ToDateTime(row["F_BEGIN_DATE"]) - this.UserSession.TimeZoneOffset);
                if (row["F_END_DATE"] is DateTime)
                  row["F_END_DATE"] = (object) (Convert.ToDateTime(row["F_END_DATE"]) - this.UserSession.TimeZoneOffset);
                if (int32 != Convert.ToInt32(dataRowArray[0]["F_RIGHT_TYPE"]) || row["F_BEGIN_DATE"] != DBNull.Value || row["F_END_DATE"] != DBNull.Value || dataRowArray[0]["F_END_DATE"] != DBNull.Value || dataRowArray[0]["F_BEGIN_DATE"] != DBNull.Value || !row["F_RIGHT_ID"].Equals(dataRowArray[0]["F_RIGHT_ID"]) || !row["F_CONDITION_ID"].Equals(dataRowArray[0]["F_CONDITION_ID"]) || Convert.ToInt64(dataRowArray[0]["F_OWNER_ID"]) != this.UserSession.UserID)
                  dataManager.ExecuteNonQuery("UPDATE IMS_CATEGORY_ACCESS SET F_RIGHT_TYPE = :rt, F_RIGHT_ID = :rID, F_OWNER_ID = :ownID, F_BEGIN_DATE = :d0, F_END_DATE = :d1, F_CONDITION_ID = :cond1 WHERE F_KEY = :id OR F_PARENT_KEY = :id", dataManager.Parameter("rt", (object) int32), dataManager.Parameter("rID", (object) Convert.ToInt32(row["F_RIGHT_ID"])), dataManager.Parameter("ownID", (object) this.UserSession.UserID), dataManager.Parameter("id", row["F_KEY"]), dataManager.Parameter("d0", row["F_BEGIN_DATE"]), dataManager.Parameter("d1", row["F_END_DATE"]), dataManager.Parameter("cond1", row["F_CONDITION_ID"]));
              }
            }
            else
            {
              long num = 0;
              dataManager.ExecuteSpNonQuery("IMS_ADD_CATEGORY_ACCESS", dataManager.Parameter("inCATEGORY_TYPE", (object) this._CategoryType), dataManager.Parameter("inCATEGORY_ID", (object) Math.Abs(this._CategoryID)), dataManager.Parameter("inRIGHT_ID", row["F_RIGHT_ID"]), dataManager.Parameter("inUSER_ID", row["F_USER_ID"]), dataManager.Parameter("inRIGHT_TYPE", row["F_RIGHT_TYPE"]), dataManager.Parameter("inOWNER_ID", (object) this.UserSession.UserID), dataManager.Parameter("inPARENT_KEY", (object) 0L), dataManager.OutputParameter("outKEY", (object) num));
              long int64_2 = Convert.ToInt64(dataManager.GetOutputParameterValue("outKEY"));
              if (row["F_END_DATE"] is DateTime)
                dataManager.ExecuteNonQuery("UPDATE IMS_CATEGORY_ACCESS SET F_BEGIN_DATE = :d0, F_END_DATE = :d1 WHERE F_KEY = :id", dataManager.Parameter("d0", (object) (Convert.ToDateTime(row["F_BEGIN_DATE"]) - this.UserSession.TimeZoneOffset)), dataManager.Parameter("d1", (object) (Convert.ToDateTime(row["F_END_DATE"]) - this.UserSession.TimeZoneOffset)), dataManager.Parameter("id", (object) int64_2));
              if (row["F_CONDITION_ID"] != DBNull.Value && Convert.ToInt64(row["F_CONDITION_ID"]) > 0L)
                dataManager.ExecuteNonQuery("UPDATE IMS_CATEGORY_ACCESS SET F_CONDITION_ID = :cond1 WHERE F_KEY = :id", dataManager.Parameter("cond1", row["F_CONDITION_ID"]), dataManager.Parameter("id", (object) int64_2));
              CategoryValue[] childsList = this.GetChildsList();
              if (childsList != null)
              {
                foreach (CategoryValue categoryValue in childsList)
                {
                  dataManager.ExecuteSpNonQuery("IMS_ADD_CATEGORY_ACCESS", dataManager.Parameter("inCATEGORY_TYPE", (object) categoryValue.CategoryType), dataManager.Parameter("inCATEGORY_ID", (object) categoryValue.CategoryID), dataManager.Parameter("inRIGHT_ID", row["F_RIGHT_ID"]), dataManager.Parameter("inUSER_ID", row["F_USER_ID"]), dataManager.Parameter("inRIGHT_TYPE", row["F_RIGHT_TYPE"]), dataManager.Parameter("inOWNER_ID", (object) this.UserSession.UserID), dataManager.Parameter("inPARENT_KEY", (object) int64_2), dataManager.OutputParameter("outKEY", (object) num));
                  int64_2 = Convert.ToInt64(dataManager.GetOutputParameterValue("outKEY"));
                  if (row["F_END_DATE"] is DateTime)
                    dataManager.ExecuteNonQuery("UPDATE IMS_CATEGORY_ACCESS SET F_BEGIN_DATE = :d0, F_END_DATE = :d1 WHERE F_KEY = :id", dataManager.Parameter("d0", (object) (Convert.ToDateTime(row["F_BEGIN_DATE"]) - this.UserSession.TimeZoneOffset)), dataManager.Parameter("d1", (object) (Convert.ToDateTime(row["F_END_DATE"]) - this.UserSession.TimeZoneOffset)), dataManager.Parameter("id", (object) int64_2));
                  if (row["F_CONDITION_ID"] != DBNull.Value && Convert.ToInt64(row["F_CONDITION_ID"]) > 0L)
                    dataManager.ExecuteNonQuery("UPDATE IMS_CATEGORY_ACCESS SET F_CONDITION_ID = :cond1 WHERE F_KEY = :id", dataManager.Parameter("cond1", row["F_CONDITION_ID"]), dataManager.Parameter("id", (object) int64_2));
                }
              }
            }
          }
        }
      }
      this.UserSession.Commit();
      this.AddEvent(this.ObjectID, ActionType.SetAccess, EventlogRecordType.AccessGranted);
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.AddEvent(this.ObjectID, ActionType.SetAccess, EventlogRecordType.Error, ex.Message);
      throw;
    }
  }

  public virtual long ObjectID => 0;

  internal void SetCreatorAccess()
  {
    if (this.UserSession.IsAdmin)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    long num = 0;
    foreach (ActionType key in this.AccessActions.Keys)
      dataManager.ExecuteSpNonQuery("IMS_ADD_CATEGORY_ACCESS", dataManager.Parameter("inCATEGORY_TYPE", (object) this._CategoryType), dataManager.Parameter("inCATEGORY_ID", (object) Math.Abs(this._CategoryID)), dataManager.Parameter("inRIGHT_ID", (object) (int) key), dataManager.Parameter("inUSER_ID", (object) this.UserSession.UserID), dataManager.Parameter("inRIGHT_TYPE", (object) 2), dataManager.Parameter("inOWNER_ID", (object) this.UserSession.UserID), dataManager.Parameter("inPARENT_KEY", (object) 0L), dataManager.OutputParameter("outKEY", (object) num));
  }

  public virtual DataTable GetAccessList(
    out ActionProperties[] actions,
    out QuickObjectInfo[] users)
  {
    this.CheckAccess(ActionType.GetAccess, false, true);
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable accessList = dataManager.ExecuteDataTable("SELECT * FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_ID = :catID AND F_CATEGORY_TYPE = :catType ORDER BY F_USER_ID", dataManager.Parameter("catID", (object) Math.Abs(this._CategoryID)), dataManager.Parameter("catType", (object) this._CategoryType));
    if (!this.CheckAccess(ActionType.SetAccess, false, false))
      accessList.ExtendedProperties[(object) "ReadOnly"] = (object) 1;
    accessList.Columns["F_BEGIN_DATE"].DateTimeMode = DataSetDateTime.Unspecified;
    accessList.Columns["F_END_DATE"].DateTimeMode = DataSetDateTime.Unspecified;
    bool flag1 = false;
    bool flag2 = false;
    long[] adminRoles = DBRoleObject.GetAdminRoles();
    long userID = -1;
    if (accessList.Rows.Count > 0)
    {
      Dictionary<ActionType, bool> dictionary = new Dictionary<ActionType, bool>((IDictionary<ActionType, bool>) this.AccessActions);
      List<DBSessionable.AccessRecord> accessRecordList = (List<DBSessionable.AccessRecord>) null;
      for (int index1 = 0; index1 < accessList.Rows.Count; ++index1)
      {
        DataRow row = accessList.Rows[index1];
        long int64 = Convert.ToInt64(row["F_USER_ID"]);
        ActionType int32 = (ActionType) Convert.ToInt32(row["F_RIGHT_ID"]);
        if (int64 != userID && userID >= 0L)
        {
          if (dictionary.Count > 0)
          {
            if (accessRecordList == null)
              accessRecordList = new List<DBSessionable.AccessRecord>(dictionary.Count);
            foreach (KeyValuePair<ActionType, bool> keyValuePair in dictionary)
              accessRecordList.Add(new DBSessionable.AccessRecord(userID, keyValuePair.Key, keyValuePair.Value));
          }
          dictionary = new Dictionary<ActionType, bool>((IDictionary<ActionType, bool>) this.AccessActions);
        }
        userID = int64;
        if (row["F_BEGIN_DATE"] is DateTime)
        {
          row["F_BEGIN_DATE"] = (object) (Convert.ToDateTime(row["F_BEGIN_DATE"]) + this.UserSession.TimeZoneOffset);
          userID = -1L;
        }
        dictionary.Remove(int32);
        if (row["F_END_DATE"] is DateTime)
          row["F_END_DATE"] = (object) (Convert.ToDateTime(row["F_END_DATE"]) + this.UserSession.TimeZoneOffset);
        for (int index2 = 0; index2 < adminRoles.Length; ++index2)
        {
          if (adminRoles[index2] == int64)
            adminRoles[index2] = 0L;
        }
        if (int64 == this.UserSession.IdentHelper.AllUsersGroupID)
          flag1 = true;
        else if (int64 == this.UserSession.IdentHelper.InternalServiceRoleID)
          flag2 = true;
      }
      if (dictionary.Count > 0)
      {
        if (accessRecordList == null)
          accessRecordList = new List<DBSessionable.AccessRecord>(dictionary.Count);
        foreach (KeyValuePair<ActionType, bool> keyValuePair in dictionary)
          accessRecordList.Add(new DBSessionable.AccessRecord(userID, keyValuePair.Key, keyValuePair.Value));
      }
      if (accessRecordList != null)
      {
        foreach (DBSessionable.AccessRecord accessRecord in accessRecordList)
        {
          DataRow row = accessList.NewRow();
          row["F_PARENT_KEY"] = (object) -1;
          row["F_KEY"] = (object) 0;
          row["F_CATEGORY_ID"] = (object) this._CategoryID;
          row["F_CATEGORY_TYPE"] = (object) this._CategoryType;
          row["F_BEGIN_DATE"] = (object) DBNull.Value;
          row["F_END_DATE"] = (object) DBNull.Value;
          row["F_OWNER_ID"] = (object) this.UserSession.IdentHelper.SystemID;
          row["F_USER_ID"] = (object) accessRecord.UserID;
          row["F_RIGHT_ID"] = (object) accessRecord.ActionID;
          row["F_RIGHT_TYPE"] = (object) (accessRecord.AccessResult ? 2 : 1);
          row["F_CONDITION_ID"] = (object) 0;
          accessList.Rows.Add(row);
        }
      }
    }
    for (int index = 0; index < adminRoles.Length; ++index)
    {
      if (adminRoles[index] != 0L)
      {
        foreach (KeyValuePair<ActionType, bool> accessAction in this.AccessActions)
        {
          DataRow row = accessList.NewRow();
          row["F_PARENT_KEY"] = (object) -1;
          row["F_KEY"] = (object) 0;
          row["F_CATEGORY_ID"] = (object) this._CategoryID;
          row["F_CATEGORY_TYPE"] = (object) this._CategoryType;
          row["F_BEGIN_DATE"] = (object) DBNull.Value;
          row["F_END_DATE"] = (object) DBNull.Value;
          row["F_OWNER_ID"] = (object) this.UserSession.IdentHelper.SystemID;
          row["F_USER_ID"] = (object) adminRoles[index];
          row["F_RIGHT_ID"] = (object) accessAction.Key;
          row["F_RIGHT_TYPE"] = (object) 2;
          row["F_CONDITION_ID"] = (object) 0;
          accessList.Rows.Add(row);
        }
      }
    }
    if (!flag1)
    {
      foreach (KeyValuePair<ActionType, bool> accessAction in this.AccessActions)
      {
        DataRow row = accessList.NewRow();
        row["F_PARENT_KEY"] = (object) -1;
        row["F_KEY"] = (object) 0;
        row["F_CATEGORY_ID"] = (object) this._CategoryID;
        row["F_CATEGORY_TYPE"] = (object) this._CategoryType;
        row["F_BEGIN_DATE"] = (object) DBNull.Value;
        row["F_END_DATE"] = (object) DBNull.Value;
        row["F_OWNER_ID"] = (object) this.UserSession.IdentHelper.SystemID;
        row["F_USER_ID"] = (object) this.UserSession.IdentHelper.AllUsersGroupID;
        row["F_RIGHT_ID"] = (object) accessAction.Key;
        row["F_RIGHT_TYPE"] = (object) (this.GetActionCategory(accessAction.Key) != ActionCategory.Admin ? 2 : 1);
        row["F_CONDITION_ID"] = (object) 0;
        accessList.Rows.Add(row);
      }
    }
    if (!flag2)
    {
      foreach (KeyValuePair<ActionType, bool> accessAction in this.AccessActions)
      {
        DataRow row = accessList.NewRow();
        row["F_PARENT_KEY"] = (object) -1;
        row["F_KEY"] = (object) 0;
        row["F_CATEGORY_ID"] = (object) this._CategoryID;
        row["F_CATEGORY_TYPE"] = (object) this._CategoryType;
        row["F_BEGIN_DATE"] = (object) DBNull.Value;
        row["F_END_DATE"] = (object) DBNull.Value;
        row["F_OWNER_ID"] = (object) this.UserSession.IdentHelper.SystemID;
        row["F_USER_ID"] = (object) this.UserSession.IdentHelper.InternalServiceRoleID;
        row["F_RIGHT_ID"] = (object) accessAction.Key;
        row["F_RIGHT_TYPE"] = (object) AccessType.NoGrant;
        row["F_CONDITION_ID"] = (object) 0;
        accessList.Rows.Add(row);
      }
    }
    accessList.AcceptChanges();
    actions = new ActionProperties[this.AccessActions.Count];
    int index3 = 0;
    foreach (KeyValuePair<ActionType, bool> accessAction in this.AccessActions)
    {
      actions[index3] = new ActionProperties(this.EventHelper.GetActionName(this._CategoryType, this.GetCategoryID4ActionName(Math.Abs(this._CategoryID)), accessAction.Key), accessAction.Key, accessAction.Value, this.GetActionCategory(accessAction.Key));
      if (actions[index3].Name == string.Empty)
        actions[index3].Name = ActionTypeHelper.GetCaption(actions[index3].ActionID);
      actions[index3++].ConnectedActions = this.GetConnectedActions(accessAction.Key);
    }
    List<QuickObjectInfo> quickObjectInfoList = new List<QuickObjectInfo>();
    foreach (DataRow row in (InternalDataCollectionBase) accessList.Rows)
    {
      long int64 = Convert.ToInt64(row["F_USER_ID"]);
      bool flag3 = false;
      for (int index4 = 0; index4 < quickObjectInfoList.Count; ++index4)
      {
        if (quickObjectInfoList[index4].ObjectID == int64)
        {
          flag3 = true;
          break;
        }
      }
      if (!flag3)
        quickObjectInfoList.Add(this.UserSession.DBCache.GetObjectInfo(dataManager, int64));
    }
    users = quickObjectInfoList.ToArray();
    return accessList;
  }

  public virtual ActionCategory GetActionCategory(ActionType actionType)
  {
    ActionCategory actionCategory;
    switch (actionType)
    {
      case ActionType.Create:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.CreateChildItem:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Edit:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.EditProperties:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Delete:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Remove:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Read:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.Write:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.View:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.Open:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.Execute:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.AddLink:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.EditLink:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.DeleteLink:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.List:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.Compute:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Print:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.Copy:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Login:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.GetLinks:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.Send:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.NextLCStep:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Purge:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Cancel:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.CheckOut:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.CheckIn:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.Save:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.SaveToDisk:
        actionCategory = ActionCategory.Read;
        break;
      case ActionType.IncludeInComposition:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.ExcludeFromComposition:
        actionCategory = ActionCategory.Write;
        break;
      case ActionType.EditAuthenticalFiles:
        actionCategory = ActionCategory.Write;
        break;
      default:
        actionCategory = ActionCategory.Admin;
        break;
    }
    return actionCategory;
  }

  public virtual IDBSecurity[] GetRelatedSecurity() => (IDBSecurity[]) null;

  public CategoryDescriptor Descriptor
  {
    get => new CategoryDescriptor(this._CategoryType, this._CategoryID);
  }

  protected virtual ActionType[] GetConnectedActions(ActionType actionType)
  {
    ActionType[] connectedActions = (ActionType[]) null;
    if (actionType == ActionType.GetAccess)
      connectedActions = new ActionType[1]
      {
        ActionType.SetAccess
      };
    return connectedActions;
  }

  public bool IsIdenticalAccess(long[] categoryID)
  {
    this.CheckCategoryArray(categoryID);
    bool flag = true;
    ActionProperties[] actions;
    QuickObjectInfo[] users;
    DataTable accessList1 = this.GetSecurityByID(categoryID[0]).GetAccessList(out actions, out users);
    int columnIndex1 = accessList1.Columns.IndexOf("F_RIGHT_ID");
    int columnIndex2 = accessList1.Columns.IndexOf("F_USER_ID");
    int columnIndex3 = accessList1.Columns.IndexOf("F_RIGHT_TYPE");
    int columnIndex4 = accessList1.Columns.IndexOf("F_BEGIN_DATE");
    int columnIndex5 = accessList1.Columns.IndexOf("F_END_DATE");
    for (int index1 = 1; index1 < categoryID.Length; ++index1)
    {
      DataTable accessList2 = this.GetSecurityByID(categoryID[index1]).GetAccessList(out actions, out users);
      if (accessList1.Rows.Count != accessList2.Rows.Count)
      {
        flag = false;
        break;
      }
      for (int index2 = 0; index2 < accessList2.Rows.Count; ++index2)
      {
        flag = false;
        for (int index3 = 0; index3 < accessList1.Rows.Count; ++index3)
        {
          flag = Convert.ToInt32(accessList1.Rows[index3][columnIndex1]) == Convert.ToInt32(accessList2.Rows[index2][columnIndex1]) && Convert.ToInt64(accessList1.Rows[index3][columnIndex2]) == Convert.ToInt64(accessList2.Rows[index2][columnIndex2]) && Convert.ToInt32(accessList1.Rows[index3][columnIndex3]) == Convert.ToInt32(accessList2.Rows[index2][columnIndex3]) && accessList1.Rows[index3][columnIndex4].Equals(accessList2.Rows[index2][columnIndex4]) && accessList1.Rows[index3][columnIndex5].Equals(accessList2.Rows[index2][columnIndex5]);
          if (flag)
            break;
        }
        if (!flag)
          break;
      }
      if (!flag)
        break;
    }
    return flag;
  }

  protected void CheckCategoryArray(long[] categoryID)
  {
    if (categoryID == null || categoryID.Length == 0)
      throw new KernelException("categoryID must have at least one element.");
  }

  public void SetAccess(long[] categoryID, DataTable accessList, params object[] AddInfo)
  {
    if (categoryID.Length == 1 && categoryID[0] == this._CategoryID)
    {
      this.SetAccess(accessList, AddInfo);
    }
    else
    {
      this.CheckCategoryArray(categoryID);
      this.UserSession.StartTransaction();
      try
      {
        int columnIndex1 = accessList.Columns.IndexOf("F_CATEGORY_ID");
        int columnIndex2 = accessList.Columns.IndexOf("F_KEY");
        for (int index1 = 0; index1 < categoryID.Length; ++index1)
        {
          IDBSecurity securityById = this.GetSecurityByID(categoryID[index1]);
          if (securityById != null)
          {
            this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_ID = :cat_id AND F_CATEGORY_TYPE = :cat_type", this.UserSession.DataManager.Parameter("cat_id", (object) categoryID[index1]), this.UserSession.DataManager.Parameter("cat_type", (object) this._CategoryType));
            for (int index2 = 0; index2 < accessList.Rows.Count; ++index2)
            {
              accessList.Rows[index2][columnIndex2] = (object) 0;
              accessList.Rows[index2][columnIndex1] = (object) categoryID[index1];
            }
            accessList.AcceptChanges();
            securityById.SetAccess(accessList, AddInfo);
          }
        }
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  protected virtual IDBSecurity GetSecurityByID(long categoryID)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public virtual IDBSecurityCollection GetRelatedSecurityCollection(long[] categoryID)
  {
    return (IDBSecurityCollection) null;
  }

  protected void PurgeAccess() => this.PurgeAccess(this.CategoryType, this.CategoryID);

  protected void PurgeAccess(int CategoryType, long categoryID)
  {
    this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = :catType AND F_CATEGORY_ID = :catID", this.UserSession.DataManager.Parameter("catType", (object) CategoryType), this.UserSession.DataManager.Parameter("catID", (object) categoryID));
  }

  protected void PurgeAccess(int CategoryType, long minCategoryID, long maxCategoryID)
  {
    this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE (F_CATEGORY_TYPE = :catType) AND (F_CATEGORY_ID BETWEEN :catID1 AND :catID2)", this.UserSession.DataManager.Parameter("catType", (object) CategoryType), this.UserSession.DataManager.Parameter("catID1", (object) minCategoryID), this.UserSession.DataManager.Parameter("catID2", (object) maxCategoryID));
  }

  public virtual string SecurityCollectionName => string.Empty;

  public virtual bool IsCompatibleElements(long[] categoryID)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public virtual List<ActionType> GetPossibleActions()
  {
    List<ActionType> possibleActions = new List<ActionType>(this.AccessActions.Keys.Count);
    possibleActions.AddRange((IEnumerable<ActionType>) this.AccessActions.Keys);
    return possibleActions;
  }

  public int CategoryType => this._CategoryType;

  public long CategoryID => this._CategoryID;

  public virtual bool GetDefaultAccess(ActionType at)
  {
    switch (this.GetActionCategory(at))
    {
      case ActionCategory.Read:
      case ActionCategory.Write:
        return this.UserSession.RoleID != this.UserSession.IdentHelper.InternalServiceRoleID;
      case ActionCategory.Admin:
        return this.UserSession.IsAdmin;
      default:
        return false;
    }
  }

  public string Languages
  {
    get
    {
      if (this is IDBGuid dbGuid)
      {
        object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_LANGUAGES FROM IMS_LOCALIZATION WHERE F_GUID = :guid1", this.UserSession.DataManager.Parameter("guid1", (object) dbGuid.GUID.ToString()));
        if (obj != null)
          return obj.ToString();
      }
      return string.Empty;
    }
    set
    {
      if (!(this is IDBGuid dbGuid))
        return;
      value = value.Trim();
      if (value == string.Empty)
      {
        this.ClearLanguages();
      }
      else
      {
        string languages = this.Languages;
        if (!(languages != value))
          return;
        this.UserSession.GetLanguageCollection().CheckValidLanguageID(value);
        if (languages == string.Empty)
          this.UserSession.DataManager.ExecuteNonQuery("INSERT INTO IMS_LOCALIZATION (F_LANGUAGES, F_GUID) VALUES (:langs, :guid1)", this.UserSession.DataManager.Parameter("langs", (object) value), this.UserSession.DataManager.Parameter("guid1", (object) dbGuid.GUID.ToString()));
        else
          this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_LOCALIZATION SET F_LANGUAGES = :langs WHERE F_GUID = :guid1", this.UserSession.DataManager.Parameter("langs", (object) value), this.UserSession.DataManager.Parameter("guid1", (object) dbGuid.GUID.ToString()));
      }
    }
  }

  public void ClearLanguages()
  {
    if (!(this is IDBGuid dbGuid))
      return;
    this.ClearLanguages(dbGuid.GUID);
  }

  internal void ClearLanguages(Guid guid)
  {
    this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_LOCALIZATION WHERE F_GUID = :guid1", this.UserSession.DataManager.Parameter("guid1", (object) guid.ToString()));
  }

  bool IDBLastAccessInfo.IsAccessTypeDeny
  {
    [DebuggerStepThrough] get => this._LastDeny;
    [DebuggerStepThrough] set => this._LastDeny = value;
  }

  bool IDBLastAccessInfo.IsAccessTypeGrantAlways
  {
    [DebuggerStepThrough] get => this._GrantAlways;
    [DebuggerStepThrough] set => this._GrantAlways = value;
  }

  bool IDBLastAccessInfo.IsLastDefault
  {
    [DebuggerStepThrough] get => this._LastDefault;
    [DebuggerStepThrough] set => this._LastDefault = value;
  }

  private bool PluginsDataCreated
  {
    [DebuggerStepThrough] get => this._pluginsData != null;
  }

  private HybridDictionary PluginsData
  {
    [DebuggerStepThrough] get
    {
      if (this._pluginsData == null)
        this._pluginsData = new HybridDictionary();
      return this._pluginsData;
    }
  }

  public object GetPluginsData(object key)
  {
    return key != null && this.PluginsDataCreated && this.PluginsData.Count != 0 ? this.PluginsData[key] : (object) null;
  }

  public void SetPluginsData(object key, object value)
  {
    if (key == null)
      return;
    this.PluginsData[key] = value;
  }

  public void RemovePluginsData(object key)
  {
    if (key == null || !this.PluginsDataCreated)
      return;
    this.PluginsData.Remove(key);
  }

  private class AccessRecord
  {
    public bool AccessResult { get; private set; }

    public long UserID { get; private set; }

    public ActionType ActionID { get; private set; }

    public AccessRecord(long userID, ActionType actionID, bool result)
    {
      this.AccessResult = result;
      this.UserID = userID;
      this.ActionID = actionID;
    }
  }
}
