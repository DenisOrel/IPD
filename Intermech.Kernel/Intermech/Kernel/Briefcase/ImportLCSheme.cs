// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportLCSheme
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportLCSheme : ImportItem
{
  public ImportLCSheme(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    ImportItemOptions options)
    : base(userSession, briefRow, metaData, options)
  {
    this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_287"), briefRow["F_NAME"]);
  }

  public override bool Import()
  {
    try
    {
      DBLCSchemaProperties properties = new DBLCSchemaProperties(this.briefRow);
      properties.AreaID = Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, properties.AreaID);
      int schemaId = properties.SchemaID;
      IDBLCSchema lcSchema = this.session.GetLCSchema(new Guid(this.briefRow["F_GUID"].ToString()), false);
      bool flag1 = true;
      bool flag2 = false;
      if (lcSchema != null)
      {
        if (this.CreateOnly)
        {
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCSchemeNotSynhronized, (object) lcSchema.Name));
          return true;
        }
        properties.SchemaID = lcSchema.SchemaID;
        if (lcSchema.SchemaProperties.IsDefaultSchema && !properties.IsDefaultSchema)
          properties.IsDefaultSchema = true;
        if (this.LangEquals)
        {
          lcSchema.SchemaProperties = properties;
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCSchemeProperties, (object) lcSchema.Name));
        }
        else
        {
          flag1 = false;
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCSchemeNotSynhronized, (object) lcSchema.Name));
        }
      }
      else
      {
        int schemaID = this.session.GetLCSchemaCollection().Create(properties);
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCScheme, (object) properties.Name));
        lcSchema = this.session.GetLCSchema(schemaID);
        flag2 = true;
      }
      if (flag1)
      {
        bool flag3 = true;
        if (!flag2 && (ServerServices.GetService(typeof (IObligatoryObjectsService)) as IObligatoryObjectsService).IsObligatoryObjectElement(16 /*0x10*/, (object) lcSchema.SchemaID, ObligatoryElementKeys.GetKeyForObjectProperty("F_SCHEMA_DATA")))
        {
          flag3 = false;
          this.AddToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_948"), (object) lcSchema.Name));
        }
        if (flag3)
        {
          byte[] numArray = (byte[]) null;
          if (CompareValuesHelper.NormalizedValue(this.briefRow["F_DRAW_DATA"]) != null)
            numArray = this.briefRow["F_DRAW_DATA"] as byte[];
          lcSchema.DrawData = numArray;
          if (flag2)
          {
            DataTable table = lcSchema.GetStepsCollection().GetSchema().Tables["IMS_LC_STEPS"];
            for (int index = 0; index < table.Rows.Count; ++index)
              this.session.GetLifecycleStep(Convert.ToInt32(table.Rows[index]["F_LC_STEP"])).Delete((long) Consts.PurgeMode);
          }
          this.SetSchema(schemaId, lcSchema);
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      this.ErrorException = new Exception(this.UniIdentifiler, ex);
      return false;
    }
  }

  private void SetSchema(int briefSchemeID, IDBLCSchema lcSchema)
  {
    DataTable toTable = this.metaData.Tables["IMS_LC_STEPS"].Clone();
    DataTable dataTable = this.metaData.Tables["IMS_LC_LINKS"].Clone();
    IDBLifecycleStepCollection stepsCollection = lcSchema.GetStepsCollection();
    SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) this.metaData.Tables["IMS_LC_STEPS"].Select($"F_SCHEMA_ID = {briefSchemeID} AND F_DELETED = 0"));
    if (toTable.Rows.Count <= 0)
      return;
    Dictionary<int, int> dictionary = new Dictionary<int, int>(toTable.Rows.Count);
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < toTable.Rows.Count; ++index)
    {
      if (index > 0)
        stringBuilder.Append(" OR ");
      stringBuilder.AppendFormat("(F_FROM_STEP = {0} OR F_TO_STEP = {0})", toTable.Rows[index]["F_LC_STEP"]);
      toTable.Rows[index]["F_SCHEMA_ID"] = (object) lcSchema.SchemaID;
      int conformityLcLevel = Helper.GetConformityLCLevel(this.session, this.metaData.Tables["IMS_LEVELS"], Convert.ToInt32(toTable.Rows[index]["F_LEVEL_ID"]));
      toTable.Rows[index]["F_LEVEL_ID"] = (object) conformityLcLevel;
      Guid anLCGuid = new Guid(Convert.ToString(toTable.Rows[index]["F_GUID"]));
      IDBLifecycleStep dbLifecycleStep = this.session.GetLifecycleStep(anLCGuid, false);
      if (dbLifecycleStep != null)
      {
        if (dbLifecycleStep.IsDeleted)
        {
          Guid newValue = Guid.NewGuid();
          this.session.DataManager.ExecuteNonQuery($"{sc_12919.ssp_appserver_12920()}{SqlHelper.QString(newValue.ToString())} WHERE F_LC_STEP = {dbLifecycleStep.LCStep.ToString()}");
          this.session.DBCache.ChangeTableValue("F_LC_STEP = " + dbLifecycleStep.LCStep.ToString(), "IMS_LC_STEPS", "F_GUID", (object) newValue, (IUserSession) this.session);
        }
        else if (dbLifecycleStep.SchemaID != lcSchema.SchemaID)
        {
          dbLifecycleStep = (IDBLifecycleStep) null;
          this.AddToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_288"), toTable.Rows[index]["F_LC_NAME"], this.briefRow["F_NAME"]));
          anLCGuid = Guid.NewGuid();
          toTable.Rows[index]["F_GUID"] = (object) anLCGuid;
        }
      }
      else
      {
        DataRow[] dataRowArray = this.session.DBCache.GetTable("IMS_LC_STEPS").Select($"F_LC_NAME = {SqlHelper.QString(toTable.Rows[index]["F_LC_NAME"].ToString())} AND F_SCHEMA_ID = {lcSchema.SchemaID} AND F_DELETED = 0");
        if (dataRowArray.Length == 1)
        {
          this.AddToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_289"), toTable.Rows[index]["F_LC_NAME"], this.briefRow["F_NAME"]));
          dbLifecycleStep = this.session.GetLifecycleStep(new Guid(dataRowArray[0]["F_GUID"].ToString()), false);
          anLCGuid = (dbLifecycleStep as IDBGuid).GUID;
          toTable.Rows[index]["F_GUID"] = (object) anLCGuid;
        }
      }
      DBLifecycleStepProperties lcProps = new DBLifecycleStepProperties(toTable.Rows[index]);
      if (dbLifecycleStep == null)
      {
        dbLifecycleStep = stepsCollection.Create(lcProps);
      }
      else
      {
        lcProps.LCStep = dbLifecycleStep.LCStep;
        dbLifecycleStep.Properties = lcProps;
      }
      dictionary.Add(Convert.ToInt32(toTable.Rows[index]["F_LC_STEP"]), dbLifecycleStep.LCStep);
    }
    SqlHelper.AssignRows(dataTable, (IEnumerable<DataRow>) this.metaData.Tables["IMS_LC_LINKS"].Select(stringBuilder.ToString()));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      dataTable.Rows[index]["F_FROM_STEP"] = (object) dictionary[Convert.ToInt32(dataTable.Rows[index]["F_FROM_STEP"])];
      dataTable.Rows[index]["F_TO_STEP"] = (object) dictionary[Convert.ToInt32(dataTable.Rows[index]["F_TO_STEP"])];
    }
    dataTable.AcceptChanges();
    stepsCollection.SetLinks(dataTable, false);
  }
}
