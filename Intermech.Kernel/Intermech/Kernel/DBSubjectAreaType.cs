// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBSubjectAreaType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

public class DBSubjectAreaType : DBSessionable, IDBSubjectAreaType, IDeletable, IDBGuid
{
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(6);
  private char _AreaID;

  public DBSubjectAreaType(UserSession uSession, char anAreaID)
    : base(uSession)
  {
    this._AreaID = anAreaID;
    this.paramsTable.Create(this.UserSession.DBCache.GetTable("IMS_SUBJECT_AREAS").Rows.Find((object) anAreaID));
    if (this.paramsTable.RowsCount == 0)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12313.ssp_appserver_12314()), (object) anAreaID));
    this.InitSecurityOptions(11, 0L);
  }

  static DBSubjectAreaType()
  {
    DBSubjectAreaType.metadataActions.Add(ActionType.GetAccess, false);
    DBSubjectAreaType.metadataActions.Add(ActionType.SetAccess, false);
    DBSubjectAreaType.metadataActions.Add(ActionType.Create, false);
    DBSubjectAreaType.metadataActions.Add(ActionType.Delete, false);
    DBSubjectAreaType.metadataActions.Add(ActionType.List, true);
    DBSubjectAreaType.metadataActions.Add(ActionType.EditProperties, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBSubjectAreaType.metadataActions);
  }

  public override string ObjectName
  {
    get => string.Format(LocalizationHolder.rm.GetString("Kernel_819"), (object) this.AreaName);
  }

  public char AreaID => this._AreaID;

  public string AreaName
  {
    get => this.paramsTable[90].ToString();
    set
    {
      if (!(this.AreaName != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_820") + value : LocalizationHolder.rm.GetString("Kernel_821"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_AREA_NAME");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_822"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString(sc_12313.ssp_appserver_12315()), value.Length, Consts.MaxObjectNameLength);
        this.UserSession.DataManager.ExecuteNonQuery(sc_12313.ssp_appserver_12316() + SqlHelper.QString(value) + sc_12313.ssp_appserver_12317() + SqlHelper.QString(this.AreaID.ToString()));
        this.UserSession.DBCache.ChangeTableValue("F_AREA_ID = " + SqlHelper.QString(this.AreaID.ToString()), "IMS_SUBJECT_AREAS", "F_AREA_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[90] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_12313.ssp_appserver_12318());
        if (ex.Message.IndexOf("IMS_SUBJECT_AREAS_AREA_NAME") >= 0)
        {
          string message = string.Format(LocalizationHolder.rm.GetString(sc_12313.ssp_appserver_12319()), (object) value);
          this.CloseEvent(EventID, EventlogRecordType.Error, str + message);
          throw new AlreadyExistsException(message);
        }
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw;
      }
    }
  }

  public string Note
  {
    get => this.paramsTable[91].ToString();
    set
    {
      if (!(this.Note != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_825") + value : LocalizationHolder.rm.GetString("Kernel_826"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_AREA_NOTE");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_12313.ssp_appserver_12320() + SqlHelper.QString(value) + sc_12313.ssp_appserver_12321() + SqlHelper.QString(this.AreaID.ToString()));
        this.UserSession.DBCache.ChangeTableValue(sc_12313.ssp_appserver_12322() + SqlHelper.QString(this.AreaID.ToString()), "IMS_SUBJECT_AREAS", "F_AREA_NOTE", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[91] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_12313.ssp_appserver_12323()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  private void CheckAreaTable(string tableName, string categoryName)
  {
    if (Convert.ToInt64(this.UserSession.DataManager.ExecuteScalar(string.Format("SELECT COUNT(*) FROM {0} WHERE F_AREA_ID LIKE " + SqlHelper.QString($"%{this.AreaID.ToString()}%"), (object) tableName))) > 0L)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12313.ssp_appserver_12324()), (object) this.AreaName, (object) categoryName));
  }

  public virtual int Delete(long DeleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, LocalizationHolder.rm.GetString(sc_12313.ssp_appserver_12325()));
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied);
    this.CheckAccess(ActionType.Delete);
    if (!this.UserSession.CanChangeObject(11, (object) this.AreaID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_923"), (object) this.AreaName));
    try
    {
      IDbManager dataManager = this.UserSession.DataManager;
      this.CheckAreaTable("IMS_ATTR_GROUPS", LocalizationHolder.rm.GetString("Kernel_830"));
      this.CheckAreaTable("IMS_ATTRIBUTES", LocalizationHolder.rm.GetString("Kernel_831"));
      this.CheckAreaTable("IMS_LEVELS", LocalizationHolder.rm.GetString("Kernel_832"));
      this.CheckAreaTable("IMS_OBJECT_TYPES", LocalizationHolder.rm.GetString("Kernel_833"));
      this.CheckAreaTable("IMS_RELATION_TYPES", LocalizationHolder.rm.GetString("Kernel_834"));
      string str1 = sc_12313.ssp_appserver_12326();
      char areaId = this.AreaID;
      string str2 = SqlHelper.QString(areaId.ToString());
      string commandText = str1 + str2;
      dataManager.ExecuteNonQuery(commandText);
      ICacheDataset dbCache = this.UserSession.DBCache;
      areaId = this.AreaID;
      string condition = "F_AREA_ID = " + SqlHelper.QString(areaId.ToString());
      UserSession userSession = this.UserSession;
      dbCache.DeleteRecords("IMS_SUBJECT_AREAS", condition, (IUserSession) userSession);
      this.Deleted = true;
    }
    catch (Exception ex)
    {
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
    return 0;
  }

  public void SetGUID(Guid guid) => this.GUID = guid;

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(11, (object) this.AreaID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_917"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public Guid GUID
  {
    get => new Guid(this.paramsTable[76].ToString());
    set
    {
      if (!(value != this.GUID))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_835") + value.ToString());
      this.CheckAccess(ActionType.EditProperties);
      if (!this.UserSession.CanChangeObject(11, (object) this.AreaID))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_917"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_12313.ssp_appserver_12327(984275921));
        this.UserSession.DataManager.ExecuteNonQuery(sc_12313.ssp_appserver_12328() + SqlHelper.QString(value.ToString()) + sc_12313.ssp_appserver_12329() + SqlHelper.QString(this.AreaID.ToString()));
        this.UserSession.DBCache.ChangeTableValue(sc_12313.ssp_appserver_12330() + SqlHelper.QString(this.AreaID.ToString()), "IMS_SUBJECT_AREAS", sc_12313.ssp_appserver_12331(), (object) value, (IUserSession) this.UserSession);
        this.paramsTable[76] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_836") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }
}
