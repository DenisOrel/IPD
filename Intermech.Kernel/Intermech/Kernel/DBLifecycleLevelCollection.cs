// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBLifecycleLevelCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBLifecycleLevelCollection : 
  DBCollection,
  IDBLifecycleLevelCollection,
  IDBCollection,
  IDBSecurity
{
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  public DBLifecycleLevelCollection(UserSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this._DBTableName = "IMS_LEVELS";
    this._DBKeyField = "F_LEVEL_ID";
    this._AreaSupport = filterRecs;
    this._LanguageSupport = false;
    this.InitSecurityOptions(8, 0L);
  }

  static DBLifecycleLevelCollection()
  {
    DBLifecycleLevelCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBLifecycleLevelCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBLifecycleLevelCollection.metadataActions.Add(ActionType.Create, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBLifecycleLevelCollection.metadataActions);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_765");

  public int Create(string levelName, string litera, string areaID, Guid newGuid, bool isDefault)
  {
    int num = 0;
    IDbManager dataManager = this.UserSession.DataManager;
    this._LastEventID = this.AddEvent(0L, ActionType.Create, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_766"), (object) levelName));
    this.CheckAccess(ActionType.Create);
    this.UserSession.StartTransaction();
    try
    {
      SqlHelper.ValidateEmptyValue(levelName, LocalizationHolder.rm.GetString("Kernel_767"));
      if (newGuid == Guid.Empty)
        newGuid = Guid.NewGuid();
      if (areaID != "")
        this.UserSession.GetSubjectAreaCollection().ValidateAriasString(areaID);
      dataManager.ExecuteSpNonQuery("IMS_ADD_LEVEL", dataManager.Parameter("inLEVEL_NAME", (object) levelName), dataManager.Parameter("inLITERA", (object) litera), dataManager.Parameter("inAREA_ID", (object) areaID), dataManager.Parameter("inGUID", (object) newGuid.ToString()), dataManager.OutputParameter("outLEVEL_ID", (object) num));
      int int32 = Convert.ToInt32(dataManager.GetOutputParameterValue("outLEVEL_ID"));
      DataTable dataTable = dataManager.ExecuteDataTable(sc_13205.ssp_appserver_13206() + int32.ToString());
      if (dataTable.Rows.Count != 1)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13205.ssp_appserver_13207()), (object) int32));
      this.UserSession.DBCache.AddRow("IMS_LEVELS", dataTable.Rows[0], (IUserSession) this.UserSession);
      DBLifecycleLevel lifecycleLevel = this.UserSession.GetLifecycleLevel(int32) as DBLifecycleLevel;
      lifecycleLevel.SetCreatorAccess();
      lifecycleLevel.LoggingOn = false;
      lifecycleLevel.IsDefaultLevel = isDefault;
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).CloseEvent(this._LastEventID, 0L, (long) int32, string.Format(LocalizationHolder.rm.GetString("Kernel_769"), (object) levelName), "", EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      this.UserSession.Commit();
      return int32;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      string str = string.Format(LocalizationHolder.rm.GetString(sc_13205.ssp_appserver_13208()), (object) levelName, (object) ex.Message);
      if (ex.Message.IndexOf("IMS_LEVELS_LEVEL_NAME") >= 0)
        str = string.Format(LocalizationHolder.rm.GetString(sc_13205.ssp_appserver_13209()), (object) levelName);
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str);
      if (!(ex is AccessDeniedException))
        throw new KernelException(str, ex);
      throw;
    }
  }
}
