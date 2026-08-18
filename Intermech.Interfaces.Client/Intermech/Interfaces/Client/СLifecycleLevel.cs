// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.СLifecycleLevel
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

/// <summary>Summary description for СLifecycleLevel</summary>
internal class СLifecycleLevel : 
  CacheObject,
  IDBLifecycleLevelType,
  IDBSubjectArea,
  IDeletable,
  IDBSecurity
{
  public СLifecycleLevel(ClientSession uSession, int aLevelID)
    : base(uSession, aLevelID)
  {
    this.InitOptions(8, (long) aLevelID, "IMS_LEVELS", LocalizationHolder.rm.GetString("Interfaces.Client_120"));
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetLifecycleLevel(this._id);
  }

  public string Litera
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_LITERA"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Litera != value))
        return;
      this._clientSession.Session.GetLifecycleLevel(this._id).Litera = value;
      this.ReloadClientCache();
    }
  }

  public bool IsDefaultLevel
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToBoolean(this.paramsTable[0]["F_DEFAULT"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.IsDefaultLevel == value)
        return;
      this._clientSession.Session.GetLifecycleLevel(this._id).IsDefaultLevel = value;
      this.ReloadClientCache();
    }
  }

  public int LevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public override Guid GUID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return base.GUID;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(value != this.GUID))
        return;
      this._clientSession.Session.GetLifecycleLevel(this._id).GUID = value;
      this.ReloadClientCache();
    }
  }

  public byte[] LevelIcon
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_ICON"] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[0]["F_ICON"];
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this._clientSession.Session.GetLifecycleLevel(this._id).LevelIcon = value;
      this.ReloadClientCache();
    }
  }

  public string LevelName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_LEVEL_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.LevelName != value))
        return;
      this._clientSession.Session.GetLifecycleLevel(this._id).LevelName = value;
      this.ReloadClientCache();
    }
  }

  public long StorageID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt64(this.paramsTable[0]["F_STORAGE_ID"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.StorageID == value)
        return;
      this._clientSession.Session.GetLifecycleLevel(this._id).StorageID = value;
      this.ReloadClientCache();
    }
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_28"), (object) this.LevelName);
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(this._CategoryType, this._CategoryID);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(8);
    (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSecurity).RestoreAdminAccess();
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_AREA_ID"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.SubjectAreas != value))
        return;
      (this._clientSession.Session.GetLifecycleLevel(this._id) as IDBSubjectArea).SubjectAreas = value;
      this._clientSession.ClientCache.ClearVisibleList(8);
      this.ReloadClientCache();
    }
  }

  public string SubjectAreasCaption
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.Session.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
    }
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = (this._clientSession.Session.GetLifecycleLevel(this._id) as IDeletable).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
  }

  public DBLifecycleStepProperties DefaultPropertiesForLCStep()
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetLifecycleLevel(this._id).DefaultPropertiesForLCStep();
  }
}
