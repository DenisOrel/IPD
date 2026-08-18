// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributesGroupCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CAttributesGroupCollection.</summary>
internal class CAttributesGroupCollection : 
  CacheObjectsCollection,
  IDBAttributesGroupCollection,
  IDBCollection,
  IDBSecurity
{
  public CAttributesGroupCollection(ClientSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this.InitOptions("IMS_ATTR_GROUPS", "F_GROUP_ID");
    this.ParentID = (object) -1;
  }

  public int Create(
    string groupName,
    string groupNote,
    string languageID,
    string areaID,
    Guid guid)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetAttributesGroupCollection(this._Filtering).Create(groupName, groupNote, languageID, areaID, guid);
    this.ReloadCache(12);
    return num;
  }

  public long Count
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (long) this.GetCount();
    }
  }

  protected override string GetParentSQL()
  {
    this._clientSession.Guard.ValidateCall();
    return Convert.ToInt32(this.ParentID) < 0 ? string.Empty : $" F_PARENT_ID = {this.ParentID.ToString()} ";
  }

  public override object ParentID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return base.ParentID;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      int int32 = Convert.ToInt32(value);
      if (int32 > 0)
        this._clientSession.GetAttributesGroup(int32, true);
      base.ParentID = value;
    }
  }

  public int[] GetVisibleList()
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetAttributesGroupCollection(this._Filtering).GetVisibleList();
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return LocalizationHolder.rm.GetString("Interfaces.Client_2");
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(12, 0L);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(12);
    (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetAttributesGroupCollection() as IDBSecurity).RestoreAdminAccess();
  }
}
