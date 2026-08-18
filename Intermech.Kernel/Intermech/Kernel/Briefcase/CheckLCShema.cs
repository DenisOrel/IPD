// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckLCShema
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.LifeCycles;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckLCShema(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckItem<IDBLCSchema, DataRow>(session, metaData, 16 /*0x10*/, briefRow, options)
{
  public override void Initialize()
  {
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, this.briefRow["F_GUID"]);
    this.item = this.session.GetLCSchema(new Guid(Convert.ToString(this.briefRow["F_GUID"])), false);
    string str = Convert.ToString(this.briefRow["F_NAME"]);
    if (this.item == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logLCSchemeNotFound, Helper.ValueToLog(this.briefRow["F_NAME"], this.briefRow["F_GUID"], true), string.Empty);
      if (this.session.GetLCSchema(str, false) == null || !this.synhronizingError)
        return;
      this.AddErrorToLog("В базе назначения уже существует схема ЖЦ с таким наименованием", str);
    }
    else
    {
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
      if (!str.Equals(this.item.Name))
      {
        if (this.session.GetLCSchema(str, false) == null || !this.synhronizingError)
          return;
        this.AddErrorToLog(BriefcaseConsts.logLCSchemeName, str, this.item.Name);
      }
      else
        this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatName, (object) this.item.Name);
    }
  }

  protected override void OnCheck()
  {
    DataRow[] dataRowArray1 = this.metaData.Tables["IMS_LC_STEPS"].Select($"{"F_SCHEMA_ID"} = {this.briefRow["F_SCHEMA_ID"]}");
    if (this.synhronizingError)
    {
      foreach (DataRow dataRow in dataRowArray1)
      {
        DataRow[] dataRowArray2 = this.session.DBCache.GetTable("IMS_LC_STEPS").Select($"F_GUID = {DataSetProcessor.QString(Convert.ToString(dataRow["F_GUID"]))} AND F_DELETED = 0");
        if (dataRowArray2 != null && dataRowArray2.Length == 1)
        {
          IDBLCSchema lcSchema = this.session.GetLCSchema(Convert.ToInt32(dataRowArray2[0]["F_SCHEMA_ID"]));
          if (lcSchema.GUID != new Guid(Convert.ToString(this.briefRow["F_GUID"])))
            this.AddErrorToLog(string.Format(BriefcaseConsts.logLCSchemeLCStepAnotherSheme, dataRow["F_GUID"]), Convert.ToString(this.briefRow["F_GUID"]), lcSchema.GUID.ToString());
        }
      }
      if (!CheckHelper.CompareString(this.briefRow, "F_NOTE", this.item.Note))
        this.AddWarningToLog(BriefcaseConsts.logLCSchemeNote, Convert.ToString(this.briefRow["F_NOTE"]), this.item.Note);
      if (!CheckHelper.CompareBoolean(this.briefRow, "F_DEFAULT", this.item.IsDefaultSchema))
        this.AddWarningToLog(BriefcaseConsts.logLCSchemeIsDefault, Convert.ToBoolean(this.briefRow["F_DEFAULT"]) ? Consts.YesValue : Consts.NoValue, this.item.IsDefaultSchema ? Consts.YesValue : Consts.NoValue);
      if (!CheckHelper.CheckArea(this.session, this.metaData, this.briefRow, (this.item as IDBSubjectArea).SubjectAreas))
      {
        IDBSubjectAreaCollection subjectAreaCollection = this.session.GetSubjectAreaCollection();
        this.AddWarningToLog(BriefcaseConsts.logLCSchemeSubjectAreas, subjectAreaCollection.GetAreasCaption(Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, Convert.ToString(this.briefRow["F_AREA_ID"]))), subjectAreaCollection.GetAreasCaption((this.item as IDBSubjectArea).SubjectAreas));
      }
      if (!CheckHelper.CheckBlob(this.briefRow["F_DRAW_DATA"], this.item.DrawData))
        this.AddWarningToLog(BriefcaseConsts.logLCSchemeDrawData);
    }
    if (!this.noneSynhronizingError)
      return;
    DataTable dataTable = (this.item.GetStepsCollection() as IDBCollection).Select(string.Empty);
    foreach (DataRow dataRow in dataRowArray1)
    {
      DataRow[] dataRowArray3 = dataTable.Select($"{"F_GUID"} = {DataSetProcessor.QString(Convert.ToString(dataRow["F_GUID"]))}");
      if (dataRowArray3 == null || dataRowArray3.Length == 0)
        this.AddErrorToLog(BriefcaseConsts.logLCSchemeLCStepNotPresent, Convert.ToString(dataRow["F_GUID"]), string.Empty);
    }
  }
}
