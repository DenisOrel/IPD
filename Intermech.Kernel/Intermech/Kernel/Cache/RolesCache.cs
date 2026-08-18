// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Cache.RolesCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.CustomServices;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Kernel.Cache;

public class RolesCache : LongLifeObject, IRolesCache, IRolesService
{
  private ConcurrentDictionary<long, List<long>> _userRoles;
  private ConcurrentDictionary<long, string> _roles;
  private RolesCacheSynchronizer _rolesSync;

  public RolesCache(IUserSession systemSession)
  {
    this.ReloadRoles(systemSession, false);
    IEventLogHelper service1 = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    service1.AfterCreateRelationExEvent += new CreateRelationExHandler(this.AfterCreateRelation);
    service1.AfterDeleteRelationEvent += new DeleteRelationHandler(this.AfterDeleteRelation);
    (ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).AddService(typeof (IRolesService), (object) this);
    IServerSynchronizersManager service2 = ServerServices.GetService(typeof (IServerSynchronizersManager)) as IServerSynchronizersManager;
    this._rolesSync = new RolesCacheSynchronizer(this);
    RolesCacheSynchronizer rolesSync = this._rolesSync;
    service2.RegisterSynchronizer((IServerSynchronizer) rolesSync);
  }

  private void AfterDeleteRelation(IDBRelation sender, long deleteMode, IUserSession session)
  {
    this.RelationInitReload(sender as DBRelation, session);
  }

  private void AfterCreateRelation(IDBRelation sender, IUserSession session, int assignMode)
  {
    this.RelationInitReload(sender as DBRelation, session);
  }

  private void RelationInitReload(DBRelation sender, IUserSession session)
  {
    if (sender.TypeID != session.IdentHelper.SimpleRelationTypeID)
      return;
    int objectType = sender.ProjObject.ObjectType;
    if (objectType != session.IdentHelper.RolesTypeID && objectType != session.IdentHelper.GroupsTypeID)
      return;
    this.InitReload(session);
  }

  private void InitReload(IUserSession session)
  {
    (ServerServices.GetService(typeof (IDelayedUpdaterService)) as IDelayedUpdaterService).ReloadRolesCache();
  }

  public void ReloadRoles(IUserSession systemSession, bool reloadMode)
  {
    IDbManager dataManager = (systemSession as UserSession).DataManager;
    ConcurrentDictionary<long, List<long>> newUserRoles = new ConcurrentDictionary<long, List<long>>();
    ConcurrentDictionary<long, string> concurrentDictionary = new ConcurrentDictionary<long, string>();
    try
    {
      DataTable dataTable1 = systemSession.ObjectsSelect(systemSession.IdentHelper.RolesTypeID, new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -2,
        (object) -50
      }));
      IDBRelationCollection relationCollection = systemSession.GetRelationCollection(systemSession.IdentHelper.SimpleRelationTypeID);
      relationCollection.LocalTypesMode = true;
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -2,
        (object) -7
      });
      foreach (DataRow row1 in (InternalDataCollectionBase) dataTable1.Rows)
      {
        long int64_1 = Convert.ToInt64(row1[0]);
        concurrentDictionary[int64_1] = row1[1].ToString();
        foreach (DataRow row2 in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, int64_1).Rows)
        {
          long int64_2 = Convert.ToInt64(row2[0]);
          int int32 = Convert.ToInt32(row2[1]);
          if (int32 == systemSession.IdentHelper.UsersTypeID)
          {
            AddRole2User(int64_2, int64_1);
          }
          else
          {
            ICompositionLoadService service = ServerServices.GetService(typeof (ICompositionLoadService)) as ICompositionLoadService;
            ColumnDescriptor[] columns = new ColumnDescriptor[2]
            {
              new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
              new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
            };
            DataTable dataTable2 = service.LoadComplexCompositions((object) systemSession, (IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
            {
              new ObjInfoItem(int64_2, int32)
            }, (IEnumerable<int>) new int[1]
            {
              systemSession.IdentHelper.SimpleRelationTypeID
            }, (IEnumerable<int>) new int[1]
            {
              systemSession.IdentHelper.UsersTypeID
            }, (IEnumerable<ColumnDescriptor>) columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, "cad001e2-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, -1);
            if (dataTable2 != null)
            {
              foreach (DataRow row3 in (InternalDataCollectionBase) dataTable2.Rows)
                AddRole2User(Convert.ToInt64(row3[0]), int64_1);
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      concurrentDictionary[systemSession.IdentHelper.AdminRoleID] = "Администратор";
      newUserRoles[systemSession.IdentHelper.SysdbaID] = new List<long>(1)
      {
        systemSession.IdentHelper.AdminRoleID
      };
      (systemSession as UserSession).EventLogHelper.AddToTrace("Ошибка инициализации кэша ролей: " + ex.Message);
    }
    if (reloadMode)
      this._rolesSync.AddEvent(string.Empty, (systemSession as UserSession).DataManager);
    this._roles = concurrentDictionary;
    this._userRoles = newUserRoles;

    void AddRole2User(long userID, long roleID)
    {
      List<long> longList;
      if (newUserRoles.TryGetValue(userID, out longList))
      {
        if (longList.Contains(roleID))
          return;
        longList.Add(roleID);
      }
      else
        newUserRoles[userID] = new List<long>() { roleID };
    }
  }

  public RoleProperties[] GetRolesList(long userID)
  {
    if (userID < 0L)
    {
      ConcurrentDictionary<long, string> roles = this._roles;
      List<RoleProperties> rolePropertiesList = new List<RoleProperties>(roles.Count);
      foreach (KeyValuePair<long, string> keyValuePair in roles)
        rolePropertiesList.Add(new RoleProperties(keyValuePair.Key, keyValuePair.Value));
      return rolePropertiesList.ToArray();
    }
    List<long> longList;
    if (!this._userRoles.TryGetValue(userID, out longList))
      return new RoleProperties[0];
    RoleProperties[] rolesList = new RoleProperties[longList.Count];
    for (int index = 0; index < longList.Count; ++index)
    {
      string roleName;
      if (!this._roles.TryGetValue(longList[index], out roleName))
        roleName = "Неизвестная роль N" + longList[index].ToString();
      rolesList[index] = new RoleProperties(longList[index], roleName);
    }
    return rolesList;
  }

  public void ValidateUserRole(long userID, long roleID, string userName)
  {
    if (!this._roles.Keys.Contains(roleID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12982.ssp_appserver_12983()), (object) roleID));
    bool flag = false;
    List<long> longList;
    if (this._userRoles.TryGetValue(userID, out longList))
      flag = longList.Contains(roleID);
    if (!flag)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12982.ssp_appserver_12984()), (object) this._roles[roleID], (object) userName));
  }
}
