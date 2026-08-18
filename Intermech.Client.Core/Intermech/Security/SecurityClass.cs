
// Type: Intermech.Security.SecurityClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Security;

/// <summary>Summary description for SecurityClass.</summary>
internal class SecurityClass
{
  private bool initialized;
  private object[] ids;
  private ISecurityCallback iSecurityCallback;
  private bool isCompatibleRightsAbstract = true;
  private SecurityHolderClass securityHolderClass;

  public bool Initialized => this.initialized;

  public bool IsChanged
  {
    get => this.initialized && this.securityHolderClass.IsChangedFlag;
    set
    {
      if (!this.initialized)
        return;
      this.securityHolderClass.IsChangedFlag = value;
    }
  }

  public bool IsCompatibleRightsAbstract => this.isCompatibleRightsAbstract;

  public SecurityHolderClass SecurityHolderClass => this.securityHolderClass;

  public void Load(object[] aIds, ISecurityCallback aISecurityCallback)
  {
    this.ids = aIds;
    this.iSecurityCallback = aISecurityCallback;
    this.DoLoad();
  }

  public bool Save()
  {
    if (!this.IsChanged)
      return true;
    this.DoSave();
    this.IsChanged = false;
    return true;
  }

  private void DoLoad()
  {
    this.initialized = false;
    this.securityHolderClass = (SecurityHolderClass) null;
    this.isCompatibleRightsAbstract = true;
    SecurityHolderClass securityHolderClass = new SecurityHolderClass(this.ids, this.iSecurityCallback);
    if (securityHolderClass.Initialized)
    {
      this.securityHolderClass = securityHolderClass;
      this.initialized = true;
    }
    else
      this.isCompatibleRightsAbstract = securityHolderClass.IsCompatibleRights;
  }

  private bool DoSave()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.initialized && this.securityHolderClass.IsChanged && this.securityHolderClass.RelatedSecurityExpanded)
      {
        SecurityHolderClass securityHolderClass = this.securityHolderClass;
        if (this.ids.Length == 1)
        {
          for (int index1 = 0; index1 < securityHolderClass.Count; ++index1)
          {
            if (securityHolderClass[index1].IsChangedFlag)
            {
              for (int index2 = 0; index2 < this.ids.Length; ++index2)
              {
                IDBSecurity security = SecurityHolderClass.FindSecurity(securityHolderClass[index1].Owner.GetSecurity(sessionKeeper.Session, this.ids[index2]), securityHolderClass[index1].CategoryDescriptor);
                if (security == null)
                  return false;
                HybridDictionary uidHash = SecurityProcs.SaveUIDByHash(securityHolderClass[index1].AccessDataTable);
                DataTable dt = securityHolderClass[index1].AccessDataTable.Copy();
                try
                {
                  security.SetAccess(SecurityProcs.ExcludeDefaultRights(SecurityProcs.DegroupRightsByUID(securityHolderClass[index1].AccessDataTable)));
                }
                catch
                {
                  securityHolderClass[index1].AssignAccessDataTable(dt);
                  throw;
                }
                securityHolderClass[index1].AssignAccessDataTable(SecurityProcs.RestoreUIDByHash(SecurityProcs.GroupRightsByUID(security.GetAccessList(out ActionProperties[] _, out QuickObjectInfo[] _)), uidHash));
              }
              securityHolderClass[index1].IsChangedFlag = false;
            }
          }
        }
        else if (securityHolderClass.Count > 0)
        {
          IDBSecurity security = securityHolderClass.GetSecurity(sessionKeeper.Session, this.ids[0]);
          if (security != null && security is IDBSecurityCollection securityCollection1)
          {
            IDBSecurityCollection securityCollection = securityCollection1.GetRelatedSecurityCollection(SecurityClass.ObjectArrayToInt64Array(this.ids));
            if (securityCollection != null)
            {
              HybridDictionary uidHash = SecurityProcs.SaveUIDByHash(securityHolderClass[0].AccessDataTable);
              DataTable dt = securityHolderClass[0].AccessDataTable.Copy();
              try
              {
                securityCollection.SetAccess(SecurityClass.ObjectArrayToInt64Array(this.ids), SecurityProcs.ExcludeDefaultRights(SecurityProcs.DegroupRightsByUID(securityHolderClass[0].AccessDataTable)));
              }
              catch
              {
                securityHolderClass[0].AssignAccessDataTable(dt);
                throw;
              }
              securityHolderClass[0].AssignAccessDataTable(SecurityProcs.RestoreUIDByHash(SecurityProcs.GroupRightsByUID(security.GetAccessList(out ActionProperties[] _, out QuickObjectInfo[] _)), uidHash));
              securityHolderClass[0].IsChangedFlag = false;
            }
          }
        }
        this.securityHolderClass.IsChanged = false;
      }
      if (this.initialized)
      {
        if (this.securityHolderClass.IsChangedFlag)
        {
          IDBSecurity security1 = this.securityHolderClass.SecurityCallback.GetSecurity(sessionKeeper.Session, this.ids[0]);
          if (security1 != null)
          {
            if (security1 is IDBSecurityCollection securityCollection)
            {
              if (this.securityHolderClass.IsCompatibleRights)
              {
                HybridDictionary uidHash = SecurityProcs.SaveUIDByHash(this.securityHolderClass.AccessDataTable);
                DataTable dt = this.securityHolderClass.AccessDataTable.Copy();
                try
                {
                  securityCollection.SetAccess(SecurityClass.ObjectArrayToInt64Array(this.ids), SecurityProcs.ExcludeDefaultRights(SecurityProcs.DegroupRightsByUID(this.securityHolderClass.AccessDataTable)));
                }
                catch
                {
                  this.securityHolderClass.AssignAccessDataTable(dt);
                  throw;
                }
                this.securityHolderClass.AssignAccessDataTable(SecurityProcs.RestoreUIDByHash(SecurityProcs.GroupRightsByUID(security1.GetAccessList(out ActionProperties[] _, out QuickObjectInfo[] _)), uidHash));
              }
            }
            else
            {
              for (int index = 0; index < this.ids.Length; ++index)
              {
                IDBSecurity security2 = this.securityHolderClass.SecurityCallback.GetSecurity(sessionKeeper.Session, this.ids[index]);
                if (security2 == null)
                  return false;
                HybridDictionary uidHash = SecurityProcs.SaveUIDByHash(this.securityHolderClass.AccessDataTable);
                DataTable dt = this.securityHolderClass.AccessDataTable.Copy();
                try
                {
                  security2.SetAccess(SecurityProcs.ExcludeDefaultRights(SecurityProcs.DegroupRightsByUID(this.securityHolderClass.AccessDataTable)));
                }
                catch
                {
                  this.securityHolderClass.AssignAccessDataTable(dt);
                  throw;
                }
                this.securityHolderClass.AssignAccessDataTable(SecurityProcs.RestoreUIDByHash(SecurityProcs.GroupRightsByUID(security2.GetAccessList(out ActionProperties[] _, out QuickObjectInfo[] _)), uidHash));
              }
            }
          }
          this.securityHolderClass.IsChangedFlag = false;
        }
      }
    }
    return true;
  }

  public static long[] ObjectArrayToInt64Array(object[] ids)
  {
    List<long> longList = new List<long>();
    if (ids != null)
    {
      for (int index = 0; index < ids.Length; ++index)
      {
        try
        {
          long int64 = Convert.ToInt64(ids[index]);
          longList.Add(int64);
        }
        catch
        {
        }
      }
    }
    return longList.ToArray();
  }
}
