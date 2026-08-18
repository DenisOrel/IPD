// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBSecurity
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using Intermech.Threading;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public sealed class DBSecurity : DBSessionable, IDBSecurity, IDBSecurityCache
{
  private List<long> _GroupsList;
  internal List<long> _GroupsList_ID = new List<long>();
  internal string _OwnerGroupsSQL;
  internal string _GroupsSQL;
  private bool _IsAdminMode;
  private bool _CacheOn = true;
  private AtomicBoolean _NeedClearCache = new AtomicBoolean(false);
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(8);
  private static string GroupsTypesSQL;

  static DBSecurity()
  {
    DBSecurity.metadataActions.Add(ActionType.GetAccess, false);
    DBSecurity.metadataActions.Add(ActionType.SetAccess, false);
    DBSecurity.metadataActions.Add(ActionType.Login, true);
    DBSecurity.metadataActions.Add(ActionType.Export, false);
    DBSecurity.metadataActions.Add(ActionType.Import, false);
    DBSecurity.metadataActions.Add(ActionType.ShowHistory, false);
    DBSecurity.metadataActions.Add(ActionType.AdminProcedure, false);
    DBSecurity.metadataActions.Add(ActionType.AdminTaskManager, false);
  }

  public DBSecurity(UserSession uSession)
    : base(uSession)
  {
    this._GroupsList = new List<long>();
    this._IsAdminMode = DBRoleObject.IsAdminRole(uSession.RoleID);
    this.InitSecurityOptions(14, 0L);
  }

  internal void SetClearCacheFlag() => this._NeedClearCache.Value = true;

  internal void RaceSetClearCacheFlag() => this._NeedClearCache.TryModify(false, true);

  public static string GetSecurityLevelDescription(IUserSession session, long securityLevel)
  {
    DataRow[] possibleValuesRows = session.GetAttributeType(session.IdentHelper.SecurityLevelID).GetPossibleValuesRows();
    string levelDescription = securityLevel.ToString();
    for (int index = 0; index < possibleValuesRows.Length; ++index)
    {
      if ((long) Convert.ToInt32(possibleValuesRows[index]["F_INTEGER_VALUE"]) == securityLevel)
      {
        levelDescription = possibleValuesRows[index]["F_DESCRIPTION"].ToString();
        break;
      }
    }
    return levelDescription;
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_798");

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBSecurity.metadataActions);
  }

  public long[] GetGroupsList() => this._GroupsList.ToArray();

  public long[] GetGroupsListRecursive()
  {
    return (ServerServices.GetService(typeof (IUsersGroupsListCache)) as IUsersGroupsListCache).GetGroupsListRecursive(this.UserSession.UserID);
  }

  public List<long> GetGroupsArrayList() => this._GroupsList;

  public List<long> GetGroupsIDArrayList() => this._GroupsList_ID;

  public string GetGroupsSQL(string addIdents)
  {
    return this.IsAdminMode ? (addIdents != string.Empty ? $"{this.UserSession.UserID},{this.UserSession.RoleID},{addIdents}" : $"{this.UserSession.UserID},{this.UserSession.RoleID}") : (addIdents != string.Empty ? $"{this._GroupsSQL},{addIdents}" : this._GroupsSQL);
  }

  public bool IsAdminMode => this._IsAdminMode;

  public bool IsInternalUser
  {
    get => this.UserSession.RoleID == this.UserSession.IdentHelper.InternalServiceRoleID;
  }

  public void SetAccess(DataTable accessList, int categoryType, long categoryID)
  {
    this._CategoryType = categoryType;
    this._CategoryID = categoryID;
    try
    {
      this.SetAccess(accessList);
    }
    finally
    {
      this._CategoryType = 14;
      this._CategoryID = 0L;
    }
  }

  private static string GetGroupsTypesStr(UserSession session)
  {
    if (DBSecurity.GroupsTypesSQL != null)
      return DBSecurity.GroupsTypesSQL;
    DataTable table = session.DBCache.GetTable("IMS_OBJTYPES_TREE");
    string groupsTypesStr = session.IdentHelper.GroupsTypeID.ToString();
    string filterExpression = "F_PARENT_ID = " + groupsTypesStr;
    foreach (DataRow dataRow in table.Select(filterExpression))
      groupsTypesStr = $"{groupsTypesStr},{dataRow["F_OBJECT_TYPE"].ToString()}";
    DBSecurity.GroupsTypesSQL = groupsTypesStr;
    return groupsTypesStr;
  }

  private void AddParentGroups(string grpsID, StringBuilder sb)
  {
    string groupsTypesStr = DBSecurity.GetGroupsTypesStr(this.UserSession);
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable(SqlHelper.GetEntersInSQL(grpsID, groupsTypesStr, this.UserSession.DataManager));
    StringBuilder stringBuilder = new StringBuilder();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (this._GroupsList.IndexOf(int64) < 0)
      {
        this._GroupsList.Add(int64);
        this._GroupsList_ID.Add(Convert.ToInt64(row[1]));
        sb.Append("," + int64.ToString());
        stringBuilder.Append(row[1].ToString() + ",");
      }
    }
    if (stringBuilder.Length <= 0)
      return;
    --stringBuilder.Length;
    this.AddParentGroups(stringBuilder.ToString(), sb);
  }

  public void LoadGroupsList()
  {
    lock (this._GroupsList)
    {
      this._GroupsList.Clear();
      this._GroupsList_ID.Clear();
      ((IDBSecurityCache) this).ClearCache();
      this._GroupsList.Add(this.UserSession.UserID);
      this._GroupsList.Add(this.UserSession.RoleID);
      this._GroupsList_ID.Add(this.UserSession.RoleID_ID);
      if (DBRoleObject.IsAdminRole(this.UserSession.RoleID))
        this._IsAdminMode = true;
      StringBuilder sb = new StringBuilder(this.UserSession.UserID.ToString());
      try
      {
        DataTable objectsList = SqlHelper.GetObjectsList(this.UserSession.ID, DBSecurity.GetGroupsTypesStr(this.UserSession), false, this.UserSession.DataManager);
        StringBuilder stringBuilder = (StringBuilder) null;
        foreach (DataRow row in (InternalDataCollectionBase) objectsList.Rows)
        {
          this._GroupsList.Add(Convert.ToInt64(row[0]));
          this._GroupsList_ID.Add(Convert.ToInt64(row[3]));
          sb.Append("," + row[0].ToString());
          if (UserSession.SubGroupsSecurity)
          {
            if (stringBuilder == null)
              stringBuilder = new StringBuilder(row[3].ToString() + ",");
            else
              stringBuilder.Append(row[3].ToString() + ",");
          }
          this.UserSession.DBCache.AddObjectInfo(new QuickObjectInfo(Convert.ToInt64(row[0]), row[1].ToString(), this.UserSession.IdentHelper.GroupsTypeID, new Guid(row[2].ToString()), Convert.ToInt64(row[3])));
        }
        if (stringBuilder != null)
        {
          --stringBuilder.Length;
          this.AddParentGroups(stringBuilder.ToString(), sb);
        }
      }
      catch (Exception ex)
      {
        this.UserSession.EventLogHelper.AddToTrace($"Ошибка вызова LoadGroupsList() для пользователя {this.UserSession.UserName}(N{this.UserSession.UserID}): {ex.Message}");
      }
      this._OwnerGroupsSQL = sb.ToString();
      sb.Clear();
      foreach (long groups in this._GroupsList)
        sb.AppendFormat("{0},", (object) groups);
      this._GroupsSQL = sb.ToString().Substring(0, sb.Length - 1);
    }
  }

  public void LoadGroupsList(List<long> groupsList, List<long> groupsList_ID, string groupsSQL)
  {
    this._GroupsList.Clear();
    this._GroupsList.AddRange((IEnumerable<long>) groupsList);
    this._GroupsList_ID.Clear();
    this._GroupsList_ID.AddRange((IEnumerable<long>) groupsList_ID);
    this._GroupsSQL = groupsSQL;
    if (!DBRoleObject.IsAdminRole(this.UserSession.RoleID))
      return;
    this._IsAdminMode = true;
  }

  void IDBSecurityCache.ClearCache()
  {
    this.UserSession.AccessCache.Clear();
    this._NeedClearCache.Value = false;
  }

  void IDBSecurityCache.ClearCache(CategoryValue aCategory)
  {
    this.UserSession.AccessCache.TryRemove(aCategory, out AccessInfo _);
  }

  void IDBSecurityCache.AddToCache(CategoryValue aCategory, AccessInfo accessResult)
  {
    this.UserSession.AccessCache[aCategory] = accessResult;
  }

  public bool CheckAccess(CategoryValue aCategory, bool aDefaultAccess, bool aThrowACException)
  {
    this._CategoryID = aCategory.CategoryID;
    this._CategoryType = aCategory.CategoryType;
    return this.CheckAccess(aCategory.ActionID, aDefaultAccess, aThrowACException);
  }

  public void ClearCacheForGroup(long aGroup, CategoryValue aCategory)
  {
    if (this._GroupsList.IndexOf(aGroup) <= -1)
      return;
    if (aCategory.CategoryType == 0)
      ((IDBSecurityCache) this).ClearCache();
    else
      ((IDBSecurityCache) this).ClearCache(aCategory);
  }

  AccessInfo IDBSecurityCache.CheckAccessInCache(CategoryValue aCategory)
  {
    AccessInfo accessInfo;
    if (this._CacheOn && this.UserSession.AccessCache.TryGetValue(aCategory, out accessInfo))
    {
      if (accessInfo.AddTime + Consts.CacheClearPeriod > DateTime.Now)
        return accessInfo;
      this.UserSession.AccessCache.TryRemove(aCategory, out accessInfo);
    }
    return (AccessInfo) null;
  }

  void IDBSecurityCache.ClearCacheIfNeed()
  {
    if (!this._CacheOn || !this._NeedClearCache.Value)
      return;
    ((IDBSecurityCache) this).ClearCache();
  }

  void IDBSecurityCache.ClearCategoryCache(
    int categoryType,
    long categoryID,
    Dictionary<ActionType, bool> accessActions)
  {
    CategoryValue key = new CategoryValue(categoryType, categoryID, ActionType.Any);
    foreach (KeyValuePair<ActionType, bool> accessAction in accessActions)
    {
      key.ActionID = accessAction.Key;
      this.UserSession.AccessCache.TryRemove(key, out AccessInfo _);
    }
  }
}
