// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CLifecycleStep
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CLifecycleStep.</summary>
internal class CLifecycleStep : 
  CacheObject,
  IDBLifecycleStep,
  IDeletable,
  IDBLifecycleLevel,
  IDBSecurity
{
  private readonly int _ObjectTypeID;

  public CLifecycleStep(ClientSession uSession, int aLCStepID, int objectTypeID)
    : base(uSession, aLCStepID)
  {
    this._ObjectTypeID = objectTypeID;
    this.InitOptions(7, (long) aLCStepID, "IMS_LC_STEPS", LocalizationHolder.rm.GetString("Interfaces.Client_121"));
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID);
  }

  public bool IsDeleted
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_DELETED"]) != 0;
    }
  }

  public int SchemaID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_SCHEMA_ID"]);
    }
  }

  public int ObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ObjectTypeID;
    }
  }

  public int GetDeleteStepID()
  {
    this._clientSession.Guard.ValidateCall();
    int[] nextSteps = this.GetNextSteps();
    if (nextSteps.Length == 0)
      return -1;
    StringBuilder stringBuilder = new StringBuilder("(");
    for (int index = 0; index < nextSteps.Length; ++index)
    {
      if (index > 0)
        stringBuilder.Append(" OR ");
      stringBuilder.AppendFormat("F_LC_STEP = {0}", (object) nextSteps[index]);
    }
    stringBuilder.AppendFormat(") AND (F_LEVEL_ID = {0})", (object) this._clientSession.IdentHelper.DeletedID);
    DataRow[] dataRowArray = this._clientSession.ClientCache.GetTable("IMS_LC_STEPS").Select(stringBuilder.ToString());
    return dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_LC_STEP"]) : -1;
  }

  public int LevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_LEVEL_ID"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.LevelID == value)
        return;
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).LevelID = value;
      this.ReloadClientCache();
    }
  }

  public LCStepOptions Options
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (LCStepOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.Options == value)
        return;
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).Options = value;
      this.ReloadClientCache();
    }
  }

  public ObjectModifyModes ObjectModifyMode
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (ObjectModifyModes) Convert.ToInt32(this.paramsTable[0]["F_MODIFY_MODE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.ObjectModifyMode == value)
        return;
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).ObjectModifyMode = value;
      this.ReloadClientCache();
    }
  }

  public int[] GetNextSteps()
  {
    this._clientSession.Guard.ValidateCall();
    DataRow[] dataRowArray = this._clientSession.ClientCache.GetTable("IMS_LC_LINKS").Select("F_FROM_STEP = " + this.LCStep.ToString());
    int[] nextSteps = new int[dataRowArray.Length];
    for (int index = 0; index < nextSteps.Length; ++index)
      nextSteps[index] = Convert.ToInt32(dataRowArray[index]["F_TO_STEP"]);
    return nextSteps;
  }

  public int GetNextStep(int levelID)
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).GetNextStep(levelID);
  }

  public string LCName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_LC_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.LCName != value))
        return;
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).LCName = value;
      this.ReloadClientCache();
    }
  }

  public bool IsFirstStep
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_FIRST"]) != 0;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!value || this.IsFirstStep == value)
        return;
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).IsFirstStep = value;
      this.ReloadClientCache();
    }
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
  }

  public DBLifecycleStepProperties Properties
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new DBLifecycleStepProperties(this.LCStep, this.ObjectTypeID, this.LCName, this.Note, this.AccessType, this.LevelID, this.ObjectModifyMode, this.GUID, this.IsFirstStep, this.Options);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).Properties = value;
      this.ReloadClientCache();
    }
  }

  public LCAccessTypes AccessType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (LCAccessTypes) Convert.ToInt32(this.paramsTable[0]["F_ACCESS_TYPE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.AccessType == value)
        return;
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).AccessType = value;
      this.ReloadClientCache();
    }
  }

  public int LCStep
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
  }

  public string Note
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      object obj = this.paramsTable[0]["F_NOTE"];
      return obj == DBNull.Value ? "" : obj.ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Note != value))
        return;
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).Note = value;
      this.ReloadClientCache();
    }
  }

  public IDBSecurity GetAttributeSecurity(int attrID)
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).GetAttributeSecurity(attrID);
  }

  /// <summary>
  /// Возвращает идентификатор шага ЖЦ, на который будут вытесняться версии с данного шага (или 0)
  /// </summary>
  public int AutoTransferStepID
  {
    get
    {
      int autoTransferStepId = 0;
      DataRow[] dataRowArray = this._clientSession.ClientCache.GetTable("IMS_LC_LINKS").Select("F_FROM_STEP = " + (object) this.LCStep);
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        if ((Convert.ToInt32(dataRowArray[index]["F_PARAMS"]) & 1) == 1)
        {
          autoTransferStepId = Convert.ToInt32(dataRowArray[index]["F_TO_STEP"]);
          break;
        }
      }
      return autoTransferStepId;
    }
  }

  public string Litera
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.GetLifecycleLevel(this.LevelID).Litera;
    }
  }

  public byte[] LevelIcon
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.GetLifecycleLevel(this.LevelID).LevelIcon;
    }
  }

  public string LevelName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.GetLifecycleLevel(this.LevelID).LevelName;
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
      this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).SetGUID(value);
      this.ReloadClientCache();
    }
  }

  public void SetGUID(Guid guid)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID).SetGUID(guid);
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(this._CategoryType, this._CategoryID);
    }
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_30"), (object) this.LCName, (object) this._clientSession.GetLCSchema(this.SchemaID).Name);
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).IsLastDefault;
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetLifecycleStep(this._id, this.ObjectTypeID) as IDBSecurity).RestoreAdminAccess();
  }
}
