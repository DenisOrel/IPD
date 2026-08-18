// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBLifecycleStepCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Kernel.LifeCycles;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class DBLifecycleStepCollection : DBCollection, IDBLifecycleStepCollection
{
  private int _ObjectTypeID;
  private DBLCSchema _Schema;

  public DBLifecycleStepCollection(UserSession uSession, IDBLCSchema schema, int objectTypeID)
    : base(uSession, false)
  {
    this._Schema = schema as DBLCSchema;
    this.ParentID = (object) schema.SchemaID;
    this._DBKeyField = "F_LC_STEP";
    this._DBTableName = "IMS_LC_STEPS";
    this._AreaSupport = false;
    this._LanguageSupport = false;
    this._ObjectTypeID = objectTypeID;
  }

  public int SchemaID => this._Schema.SchemaID;

  public int ObjectTypeID => this._ObjectTypeID;

  public void CopyTo(int toSchemaID)
  {
  }

  public IDBLifecycleStep Create(DBLifecycleStepProperties lcProps)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    long EventID = this._Schema.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_792"), (object) lcProps.LCName));
    this._Schema.CheckAccess(ActionType.EditProperties);
    this.CheckChangeEnable();
    this._Schema.CloseEvent(EventID, EventlogRecordType.AccessGranted);
    int num = 0;
    this.UserSession.StartTransaction();
    IDBLifecycleStep lifecycleStep;
    try
    {
      if (lcProps.StepGuid == Guid.Empty)
        lcProps.StepGuid = Guid.NewGuid();
      if ((lcProps.Options & LCStepOptions.BaseVersion) == LCStepOptions.BaseVersion && (lcProps.Options & LCStepOptions.DisableParallelVersions) == LCStepOptions.None)
        throw new KernelExceptionID(sc_13274.ssp_appserver_13276(660668380));
      SqlHelper.ValidateEmptyValue(lcProps.LCName, LocalizationHolder.rm.GetString("Kernel_793"));
      if (this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select($"F_LC_NAME = {SqlHelper.QString(lcProps.LCName)} AND F_SCHEMA_ID = {this.SchemaID.ToString()} AND F_DELETED = 0").Length != 0)
        throw new KernelExceptionID(sc_13274.ssp_appserver_13277(989280574), (object) lcProps.LCName);
      dataManager.ExecuteSpNonQuery("IMS_ADD_LC_STEP", dataManager.Parameter("inLEVEL_ID", (object) lcProps.LevelID), dataManager.Parameter("inLC_NAME", (object) lcProps.LCName), dataManager.Parameter("inNOTE", (object) lcProps.Note), dataManager.Parameter("inSCHEMA_ID", (object) this.SchemaID), dataManager.Parameter("inACCESS_TYPE", (object) Convert.ToInt32((object) lcProps.AccessType)), dataManager.Parameter("inGUID", (object) lcProps.StepGuid.ToString()), dataManager.OutputParameter("outLC_STEP", (object) num));
      int int32 = Convert.ToInt32(dataManager.GetOutputParameterValue("outLC_STEP"));
      if (lcProps.Options != LCStepOptions.None)
        dataManager.ExecuteNonQuery("UPDATE IMS_LC_STEPS SET F_OPTIONS = :opt1 WHERE F_LC_STEP = :stepID", dataManager.Parameter("opt1", (object) (int) lcProps.Options), dataManager.Parameter("stepID", (object) int32));
      DataTable dataTable = dataManager.ExecuteDataTable(sc_13274.ssp_appserver_13278() + int32.ToString());
      if (dataTable.Rows.Count != 1)
        throw new KernelExceptionID(sc_13274.ssp_appserver_13279(64991270), (object) int32);
      this.UserSession.DBCache.AddRow("IMS_LC_STEPS", dataTable.Rows[0], (IUserSession) this.UserSession);
      lifecycleStep = this.UserSession.GetLifecycleStep(int32);
      lifecycleStep.IsFirstStep = lcProps.FirstStep;
      lifecycleStep.ObjectModifyMode = lcProps.ObjectModifyMode;
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      string str = string.Format(LocalizationHolder.rm.GetString(sc_13274.ssp_appserver_13280()), (object) lcProps.LCName, (object) ex.Message);
      this._Schema.CloseEvent(EventID, EventlogRecordType.Error, str);
      throw new KernelException(str, ex);
    }
    return lifecycleStep;
  }

  public void SetLinks(DataTable linksList, bool deleteNotExists)
  {
    DataSet schema = this.GetSchema();
    long EventID = this._Schema.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_795"));
    this._Schema.CheckAccess(ActionType.EditProperties);
    this.CheckChangeEnable();
    this._Schema.CloseEvent(EventID, EventlogRecordType.AccessGranted);
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) linksList.Rows)
      {
        int int32_1 = Convert.ToInt32(row["F_FROM_STEP"]);
        int int32_2 = Convert.ToInt32(row["F_TO_STEP"]);
        if (int32_1 == int32_2)
          throw new KernelExceptionID(sc_13274.ssp_appserver_13281(1555383863), (object) int32_1);
        string str1 = "0";
        if (row["F_ROUTE_ID"] != DBNull.Value)
          str1 = row["F_ROUTE_ID"].ToString();
        string str2 = "0";
        if (row["F_PARAMS"] != DBNull.Value)
          str2 = row["F_PARAMS"].ToString();
        DataRow[] dataRowArray = schema.Tables[1].Select($"F_FROM_STEP = {int32_1} AND F_TO_STEP = {int32_2}");
        if (dataRowArray.Length == 0)
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_13274.ssp_appserver_13282(), (object) int32_1, (object) int32_2, (object) str1, (object) SqlHelper.QString(row["F_NOTE"].ToString()), (object) str2));
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_13274.ssp_appserver_13283(), (object) int32_1, (object) int32_2, (object) str1, (object) SqlHelper.QString(row["F_NOTE"].ToString()), (object) str2));
          schema.Tables[1].Rows.Remove(dataRowArray[0]);
        }
      }
      if (deleteNotExists)
      {
        schema.Tables[1].AcceptChanges();
        foreach (DataRow row in (InternalDataCollectionBase) schema.Tables[1].Rows)
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_13274.ssp_appserver_13284(), row["F_FROM_STEP"], row["F_TO_STEP"]));
      }
      this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_LC_LINKS");
    }
    catch (Exception ex)
    {
      this._Schema.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
  }

  public void DeleteLink(int fromStepID, int toStepID)
  {
    long EventID = this._Schema.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_796"), (object) this.UserSession.GetLifecycleStep(fromStepID).LCName, (object) this.UserSession.GetLifecycleStep(toStepID).LCName));
    this._Schema.CheckAccess(ActionType.EditProperties);
    this.CheckChangeEnable();
    this._Schema.CloseEvent(EventID, EventlogRecordType.AccessGranted);
    try
    {
      string condition = $"F_FROM_STEP = {fromStepID} AND F_TO_STEP = {toStepID}";
      this.UserSession.DataManager.ExecuteNonQuery(sc_13274.ssp_appserver_13285() + condition);
      this.UserSession.DBCache.DeleteRecords("IMS_LC_LINKS", condition, (IUserSession) this.UserSession);
    }
    catch (Exception ex)
    {
      this._Schema.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
  }

  public DataSet GetSchema()
  {
    DataSet schema = new DataSet();
    DataTable dataTable1 = this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Clone();
    DataTable dataTable2 = this.UserSession.DBCache.GetTable("IMS_LC_LINKS").Clone();
    SqlHelper.AssignRows(dataTable1, (IEnumerable<DataRow>) this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select($"F_SCHEMA_ID = {this.SchemaID} AND F_DELETED = 0"));
    if (dataTable1.Rows.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(" OR ");
        stringBuilder.AppendFormat("F_FROM_STEP = {0} OR F_TO_STEP = {0}", dataTable1.Rows[index]["F_LC_STEP"]);
        if (dataTable1.Rows[index]["F_GUID"] == DBNull.Value)
        {
          IDBGuid lifecycleStep = this.UserSession.GetLifecycleStep(Convert.ToInt32(dataTable1.Rows[index]["F_LC_STEP"])) as IDBGuid;
          dataTable1.Rows[index]["F_GUID"] = (object) lifecycleStep.GUID.ToString();
        }
      }
      SqlHelper.AssignRows(dataTable2, (IEnumerable<DataRow>) this.UserSession.DBCache.GetTable("IMS_LC_LINKS").Select(stringBuilder.ToString()));
    }
    schema.Tables.Add(dataTable1);
    schema.Tables.Add(dataTable2);
    return schema;
  }

  private void CheckChangeEnable()
  {
    if (!this.UserSession.CanChangeObjectElement(16 /*0x10*/, (object) this._Schema.SchemaID, ObligatoryElementKeys.GetKeyForObjectProperty("F_SCHEMA_DATA")))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_916"), (object) LocalizationHolder.rm.GetString("Kernel_922")));
  }

  public void SetSchema(DataSet dsSchema)
  {
    long EventID = this._Schema.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_797"));
    this._Schema.CheckAccess(ActionType.EditProperties);
    this._Schema.CloseEvent(EventID, EventlogRecordType.AccessGranted);
    this.CheckChangeEnable();
    this.GetSchema();
    this.UserSession.StartTransaction();
    try
    {
      DataTable table1 = dsSchema.Tables["IMS_LC_STEPS"];
      DataTable table2 = dsSchema.Tables["IMS_LC_LINKS"];
      foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
      {
        int int32 = Convert.ToInt32(row["F_LC_STEP"]);
        if (int32 < 0)
        {
          IDBLifecycleStep dbLifecycleStep = this.Create(new DBLifecycleStepProperties(row));
          foreach (DataRow dataRow in table2.Select(string.Format("F_FROM_STEP = {0} OR F_TO_STEP = {0}", (object) int32)))
          {
            if (Convert.ToInt32(dataRow["F_FROM_STEP"]) == int32)
              dataRow["F_FROM_STEP"] = (object) dbLifecycleStep.LCStep;
            if (Convert.ToInt32(dataRow["F_TO_STEP"]) == int32)
              dataRow["F_TO_STEP"] = (object) dbLifecycleStep.LCStep;
          }
        }
        else
        {
          IDBLifecycleStep lifecycleStep = this.UserSession.GetLifecycleStep(int32);
          if (Convert.ToInt32(row["F_DELETED"]) != 0)
            lifecycleStep.Delete(0L);
          else
            lifecycleStep.Properties = new DBLifecycleStepProperties(row);
        }
      }
      table2.AcceptChanges();
      this.SetLinks(table2, true);
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this._Schema.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
  }

  public int GetFirstStep()
  {
    int firstStep = -1;
    DataRow[] dataRowArray1 = this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select($"F_SCHEMA_ID = {this.SchemaID} AND F_FIRST <> 0");
    if (dataRowArray1.Length != 0)
      return Convert.ToInt32(dataRowArray1[0]["F_LC_STEP"]);
    DataRow[] dataRowArray2 = this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select("F_SCHEMA_ID = " + this.SchemaID.ToString());
    if (dataRowArray2.Length == 0)
      throw new KernelExceptionID(sc_13274.ssp_appserver_13286(345413126), (object) this._Schema.Name);
    foreach (DataRow dataRow in dataRowArray2)
    {
      IDBLifecycleLevelType lifecycleLevel = this.UserSession.GetLifecycleLevel(Convert.ToInt32(dataRow["F_LEVEL_ID"]));
      if (lifecycleLevel.IsDefaultLevel)
      {
        firstStep = Convert.ToInt32(dataRow["F_LC_STEP"]);
        break;
      }
      if (lifecycleLevel.LevelID == this.UserSession.IdentHelper.CreatedLevelID || firstStep == -1)
        firstStep = Convert.ToInt32(dataRow["F_LC_STEP"]);
    }
    return firstStep;
  }

  public void SetObjectsLCStep(long[] objectIDs, int lcStep)
  {
    this.UserSession.ClearObjectSmartCache();
    for (int index = 0; index < objectIDs.Length; ++index)
      this.UserSession.GetObject(objectIDs[index]).LCStep = lcStep;
  }

  public ObjectSteps[] GetObjectsSteps(long[] objectIDs)
  {
    LifecycleSteps lifecycleSteps = new LifecycleSteps();
    for (int index = 0; index < objectIDs.Length; ++index)
    {
      IDBObject dbObject = this.UserSession.GetObject(objectIDs[index]);
      IDBLifecycleStep lifecycleStep = this.UserSession.GetLifecycleStep(dbObject.LCStep);
      lifecycleSteps.Add(new LifecycleStep(dbObject.LCStep, -1));
      foreach (int nextStep in lifecycleStep.GetNextSteps())
        lifecycleSteps.Add(new LifecycleStep(nextStep, 1));
    }
    int length = lifecycleSteps.GoodCount(objectIDs.Length);
    if (length <= 0)
      return (ObjectSteps[]) null;
    ObjectSteps[] objectsSteps = new ObjectSteps[length];
    int index1 = 0;
    foreach (LifecycleStep lcStep in lifecycleSteps._LCStepList)
    {
      if (lcStep.Attr == -1 || lcStep.Attr == objectIDs.Length)
      {
        IDBLifecycleStep lifecycleStep = this.UserSession.GetLifecycleStep(lcStep.Step);
        int atribute = lcStep.Attr == -1 ? 0 : 1;
        objectsSteps[index1] = new ObjectSteps(lcStep.Step, lifecycleStep.LCName, atribute, ((IDBLifecycleLevel) lifecycleStep).LevelIcon);
        ++index1;
      }
    }
    return objectsSteps;
  }

  public IDBLifecycleStep FindSameStep(IDBLifecycleStep oldStep, out string errorMsg)
  {
    IDBLifecycleStep sameStep = (IDBLifecycleStep) null;
    errorMsg = string.Empty;
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select($"F_SCHEMA_ID = {this.SchemaID} AND F_DELETED = 0 AND (F_LEVEL_ID = {oldStep.LevelID} OR F_LC_NAME = '{oldStep.LCName}')");
    if (dataRowArray.Length != 0)
    {
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        if (oldStep.LCName == dataRowArray[index]["F_LC_NAME"].ToString())
        {
          if (oldStep.LevelID == Convert.ToInt32(dataRowArray[index]["F_LEVEL_ID"]))
          {
            sameStep = this.UserSession.GetLifecycleStep(Convert.ToInt32(dataRowArray[index]["F_LC_STEP"]));
            errorMsg = string.Empty;
            break;
          }
          errorMsg = $"Уровень продвижения '{MetaDataHelper.GetLCLevelName(Convert.ToInt32(dataRowArray[index]["F_LEVEL_ID"]))}' найденного шага ЖЦ '{oldStep.LCName}' не соответствует уровню продвижения '{MetaDataHelper.GetLCLevelName(oldStep.LevelID)}' исходного шага ЖЦ.";
          sameStep = (IDBLifecycleStep) null;
          break;
        }
        if (oldStep.LevelID == Convert.ToInt32(dataRowArray[index]["F_LEVEL_ID"]))
        {
          if (sameStep == null)
          {
            sameStep = this.UserSession.GetLifecycleStep(Convert.ToInt32(dataRowArray[index]["F_LC_STEP"]));
          }
          else
          {
            errorMsg = $"В схеме ЖЦ '{this.UserSession.GetLCSchema(this.SchemaID).Name}' найдено более одного шага с уровнем продвижения '{MetaDataHelper.GetLCLevelName(oldStep.LevelID)}'. Объект будет переведён на шаг '{sameStep.LCName}'.";
            break;
          }
        }
      }
    }
    else
      errorMsg = "Похожих шагов ЖЦ в схеме на найдено.";
    return sameStep;
  }

  public ObjectSteps[] GetObjectsSteps(List<int> stepsID)
  {
    if (stepsID.Count == 0)
      return (ObjectSteps[]) null;
    List<ObjectSteps> objectStepsList = new List<ObjectSteps>();
    int[] nextSteps1 = this.UserSession.GetLifecycleStep(stepsID[0]).GetNextSteps();
    List<int> intList = new List<int>(nextSteps1.Length);
    intList.AddRange((IEnumerable<int>) nextSteps1);
    for (int index1 = 1; index1 < stepsID.Count; ++index1)
    {
      int[] nextSteps2 = this.UserSession.GetLifecycleStep(stepsID[index1]).GetNextSteps();
      for (int index2 = intList.Count - 1; index2 >= 0; --index2)
      {
        bool flag = false;
        for (int index3 = 0; index3 < nextSteps2.Length; ++index3)
        {
          if (nextSteps2[index3] == intList[index2])
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          intList.RemoveAt(index2);
      }
    }
    if (intList.Count == 0)
      return (ObjectSteps[]) null;
    for (int index = 0; index < stepsID.Count; ++index)
    {
      IDBLifecycleStep lifecycleStep = this.UserSession.GetLifecycleStep(stepsID[index]);
      objectStepsList.Add(new ObjectSteps(stepsID[index], lifecycleStep.LCName, 0, ((IDBLifecycleLevel) lifecycleStep).LevelIcon));
    }
    for (int index = 0; index < intList.Count; ++index)
    {
      IDBLifecycleStep lifecycleStep = this.UserSession.GetLifecycleStep(intList[index]);
      objectStepsList.Add(new ObjectSteps(intList[index], lifecycleStep.LCName, -1, ((IDBLifecycleLevel) lifecycleStep).LevelIcon));
    }
    return objectStepsList.ToArray();
  }

  internal void RemoveObjectTypeData()
  {
    if (this.ObjectTypeID <= 0)
      return;
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(this.ObjectTypeID);
    IContainerService service = ServerServices.GetService(typeof (IContainerService)) as IContainerService;
    DataTable table = this.UserSession.DBCache.GetTable(this.DBTableName);
    int index = this.SchemaID;
    string filterExpression = "F_SCHEMA_ID = " + index.ToString();
    DataRow[] dataRowArray = table.Select(filterExpression);
    for (index = 0; index < dataRowArray.Length; ++index)
    {
      DataRow dataRow = dataRowArray[index];
      int int32 = Convert.ToInt32(dataRow["F_LC_STEP"]);
      Guid LCStepGuid = new Guid(dataRow["F_GUID"].ToString());
      service.DeleteContainerForLCStepObjectType((object) this.UserSession, LCStepGuid, objectTypeGuid);
      if (this.UserSession.GetLifecycleStep(int32, false, this.ObjectTypeID) is DBLifecycleStep lifecycleStep)
        lifecycleStep.DeleteAccess();
    }
  }
}
