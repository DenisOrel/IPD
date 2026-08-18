// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.LifeCycles.DBLCSchemaCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.LifeCycles;

internal class DBLCSchemaCollection : DBCollection, IDBSecurity, IDBLCSchemaCollection
{
  internal static int DefaultSchemaID;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  static DBLCSchemaCollection()
  {
    DBLCSchemaCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBLCSchemaCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBLCSchemaCollection.metadataActions.Add(ActionType.Create, false);
  }

  public DBLCSchemaCollection(UserSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this._DBTableName = "IMS_LC_SCHEMAS";
    this._DBKeyField = "F_SCHEMA_ID";
    this._AreaSupport = filterRecs;
    this._LanguageSupport = false;
    this.InitSecurityOptions(16 /*0x10*/, 0L);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBLCSchemaCollection.metadataActions);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_389");

  public int Create(DBLCSchemaProperties properties)
  {
    int num = 0;
    IDbManager dataManager = this.UserSession.DataManager;
    this._LastEventID = this.AddEvent(0L, ActionType.Create, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_390"), (object) properties.Name));
    this.UserSession.StartTransaction();
    try
    {
      this.CheckAccess(ActionType.Create);
      SqlHelper.ValidateEmptyValue(properties.Name, LocalizationHolder.rm.GetString("Kernel_391"));
      if (properties.GUID == Guid.Empty)
        properties.GUID = Guid.NewGuid();
      if (properties.AreaID != string.Empty)
        this.UserSession.GetSubjectAreaCollection().ValidateAriasString(properties.AreaID);
      dataManager.ExecuteSpNonQuery(sc_13165.ssp_appserver_13166(), dataManager.Parameter("inNAME", (object) properties.Name), dataManager.Parameter("inNOTE", (object) properties.Note), dataManager.Parameter("inGUID", (object) properties.GUID.ToString()), dataManager.Parameter("inAREA_ID", (object) properties.AreaID), dataManager.Parameter("inOPTIONS", (object) (int) properties.Options), dataManager.OutputParameter("outSCHEMA_ID", (object) num));
      int int32 = Convert.ToInt32(dataManager.GetOutputParameterValue("outSCHEMA_ID"));
      DataTable dataTable = dataManager.ExecuteDataTable(sc_13165.ssp_appserver_13167() + int32.ToString());
      if (dataTable.Rows.Count != 1)
        throw new KernelExceptionID(sc_13165.ssp_appserver_13168(1790033717), (object) int32);
      this.UserSession.DBCache.AddRow("IMS_LC_SCHEMAS", dataTable.Rows[0], (IUserSession) this.UserSession);
      DBLCSchema lcSchema = this.UserSession.GetLCSchema(int32) as DBLCSchema;
      lcSchema.SetCreatorAccess();
      lcSchema.LoggingOn = false;
      lcSchema.IsDefaultSchema = properties.IsDefaultSchema;
      if (!properties.CreateEmptySchema)
      {
        IDBLifecycleStepCollection stepsCollection = lcSchema.GetStepsCollection();
        DataSet schema = stepsCollection.GetSchema();
        IDBLifecycleStep dbLifecycleStep1 = stepsCollection.Create(new DBLifecycleStepProperties(0, 0, LocalizationHolder.rm.GetString("Kernel_392"), "", LCAccessTypes.CheckAll, this.UserSession.IdentHelper.CreatedLevelID, ObjectModifyModes.InBase, Guid.Empty, true, LCStepOptions.None));
        IDBLifecycleStep dbLifecycleStep2 = stepsCollection.Create(new DBLifecycleStepProperties(0, 0, LocalizationHolder.rm.GetString("Kernel_393"), "", LCAccessTypes.CheckAll, this.UserSession.IdentHelper.DeletedID, ObjectModifyModes.CantModify, Guid.Empty, false, LCStepOptions.None));
        DataTable table = schema.Tables["IMS_LC_LINKS"];
        DataRow row1 = table.NewRow();
        row1["F_FROM_STEP"] = (object) dbLifecycleStep1.LCStep;
        row1["F_TO_STEP"] = (object) dbLifecycleStep2.LCStep;
        table.Rows.Add(row1);
        DataRow row2 = table.NewRow();
        row2["F_FROM_STEP"] = (object) dbLifecycleStep2.LCStep;
        row2["F_TO_STEP"] = (object) dbLifecycleStep1.LCStep;
        table.Rows.Add(row2);
        table.AcceptChanges();
        stepsCollection.SetLinks(table, true);
      }
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).CloseEvent(this._LastEventID, 0L, (long) int32, string.Format(LocalizationHolder.rm.GetString("Kernel_394"), (object) properties.Name), string.Empty, EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      this.UserSession.Commit();
      return int32;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      string str = string.Format(LocalizationHolder.rm.GetString(sc_13165.ssp_appserver_13169()), (object) properties.Name, (object) ex.Message);
      if (ex.Message.IndexOf("IMS_LC_SCHEMAS_NAME") >= 0)
        str = string.Format(LocalizationHolder.rm.GetString(sc_13165.ssp_appserver_13170()), (object) properties.Name);
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str);
      if (!(ex is AccessDeniedException))
        throw new KernelException(str, ex);
      throw;
    }
  }

  public int GetDefaultSchemaID()
  {
    if (DBLCSchemaCollection.DefaultSchemaID == 0)
    {
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_LC_SCHEMAS").Select("F_DEFAULT <> 0");
      DBLCSchemaCollection.DefaultSchemaID = dataRowArray.Length == 0 ? Convert.ToInt32(this.UserSession.DBCache.GetTable("IMS_LC_SCHEMAS").Rows[0]["F_SCHEMA_ID"]) : Convert.ToInt32(dataRowArray[0]["F_SCHEMA_ID"]);
    }
    return DBLCSchemaCollection.DefaultSchemaID;
  }
}
