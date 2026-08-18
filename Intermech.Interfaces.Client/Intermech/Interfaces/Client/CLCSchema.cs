// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CLCSchema
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.LifeCycles;
using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

internal class CLCSchema : CacheObject, IDBLCSchema, IDBSecurity, IDeletable, IDBSubjectArea
{
  public CLCSchema(ClientSession uSession, int schemaID)
    : base(uSession, schemaID)
  {
    this.InitOptions(16 /*0x10*/, (long) schemaID, "IMS_LC_SCHEMAS", LocalizationHolder.rm.GetString("Interfaces.Client_156"));
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetLCSchema(this._id);
  }

  public int SchemaID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
  }

  public string Name
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Name != value))
        return;
      this._clientSession.Session.GetLCSchema(this._id).Name = value;
      this.ReloadClientCache();
    }
  }

  public string Note
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_NOTE"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Note != value))
        return;
      this._clientSession.Session.GetLCSchema(this._id).Note = value;
      this.ReloadClientCache();
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
      if (!(this.GUID != value))
        return;
      this._clientSession.Session.GetLCSchema(this._id).GUID = value;
      this.ReloadClientCache();
    }
  }

  public bool IsDefaultSchema
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_DEFAULT"]) != 0;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.IsDefaultSchema == value)
        return;
      this._clientSession.Session.GetLCSchema(this._id).IsDefaultSchema = value;
      this.ReloadClientCache();
    }
  }

  public byte[] DrawData
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_DRAW_DATA"] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[0]["F_DRAW_DATA"];
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this._clientSession.Session.GetLCSchema(this._id).DrawData = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>
  /// Опции (содержат битовые флаги для управления свойствами схемы ЖЦ)
  /// </summary>
  public LCSchemaOptions Options
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (LCSchemaOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.Options == value)
        return;
      this._clientSession.Session.GetLCSchema(this._id).Options = value;
      this.ReloadClientCache();
    }
  }

  public DBLCSchemaProperties SchemaProperties
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new DBLCSchemaProperties(this.SchemaID, this.Name, this.Note, this.GUID, this.IsDefaultSchema, this.SubjectAreas, this.Options);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this._clientSession.Session.GetLCSchema(this._id).SchemaProperties = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>Возвращает коллекцию шагов данного жизненного цикла</summary>
  public IDBLifecycleStepCollection GetStepsCollection()
  {
    this._clientSession.Guard.ValidateCall();
    return (IDBLifecycleStepCollection) new CLifecycleStepCollection(this._clientSession, (IDBLCSchema) this, 0);
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_27"), (object) this.Name);
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).IsLastDefault;
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
    return (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(16 /*0x10*/);
    (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetLCSchema(this._id) as IDBSecurity).RestoreAdminAccess();
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = (this._clientSession.Session.GetLCSchema(this._id) as IDeletable).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
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
      (this._clientSession.Session.GetLCSchema(this._id) as IDBSubjectArea).SubjectAreas = value;
      this._clientSession.ClientCache.ClearVisibleList(16 /*0x10*/);
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
}
