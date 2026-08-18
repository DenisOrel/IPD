// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckLCStep
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.LifeCycles;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckLCStep(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckItem<IDBLifecycleStep, DataRow>(session, metaData, 7, briefRow, options)
{
  public override void Initialize()
  {
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, this.briefRow["F_GUID"]);
    this.item = this.session.GetLifecycleStep(new Guid(Convert.ToString(this.briefRow["F_GUID"])), false);
    if (this.item == null || this.item.IsDeleted)
    {
      if (!this.noneSynhronizingError)
        return;
      this.AddErrorToLog(BriefcaseConsts.logLCStepNotFound, Helper.ValueToLog(this.briefRow["F_LC_NAME"], this.briefRow["F_GUID"], true), string.Empty);
    }
    else
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
  }

  protected override void OnCheck()
  {
    if (this.synhronizingError)
    {
      if (!CheckHelper.CompareString(this.briefRow, "F_LC_NAME", this.item.LCName))
        this.AddWarningToLog(BriefcaseConsts.logLCStepName, Convert.ToString(this.briefRow["F_LC_NAME"]), this.item.LCName);
      if (!this.CheckAccessType(this.briefRow, this.item.AccessType))
        this.AddWarningToLog(BriefcaseConsts.logLCStepAccessType, EnumDescConverter.GetEnumDescription((Enum) (LCAccessTypes) Convert.ToInt32(this.briefRow["F_ACCESS_TYPE"])), EnumDescConverter.GetEnumDescription((Enum) this.item.AccessType));
      if (!CheckHelper.CompareBoolean(this.briefRow, "F_FIRST", this.item.IsFirstStep))
        this.AddWarningToLog(BriefcaseConsts.logLCStepIsFirstStep, Convert.ToBoolean(this.briefRow["F_FIRST"]) ? Consts.YesValue : Consts.NoValue, this.item.IsFirstStep ? Consts.YesValue : Consts.NoValue);
      if (!CheckHelper.CheckLevelID(this.session, this.metaData.Tables["IMS_LEVELS"], this.briefRow, this.item.LevelID))
      {
        string empty1 = string.Empty;
        string log1;
        if (Convert.ToInt32(this.briefRow["F_LEVEL_ID"]) == 0)
        {
          log1 = LocalizationHolder.rm.GetString("Kernel_276");
        }
        else
        {
          DataRow dataRow = this.metaData.Tables["IMS_LEVELS"].Rows.Find(this.briefRow["F_LEVEL_ID"]);
          log1 = Helper.ValueToLog(dataRow["F_LEVEL_NAME"], dataRow["F_GUID"], true);
        }
        string empty2 = string.Empty;
        string log2;
        if (this.item.LevelID == 0)
        {
          log2 = LocalizationHolder.rm.GetString("Kernel_277");
        }
        else
        {
          IDBLifecycleLevelType lifecycleLevel = this.session.GetLifecycleLevel(this.item.LevelID);
          log2 = Helper.ValueToLog((object) lifecycleLevel.LevelName, (object) lifecycleLevel.GUID, true);
        }
        this.AddErrorToLog(BriefcaseConsts.logAttributeLevelID, log1, log2);
      }
      LCStepOptions int32 = (LCStepOptions) Convert.ToInt32(this.briefRow["F_OPTIONS"]);
      if ((this.item.Options & LCStepOptions.DisableParallelVersions) != LCStepOptions.DisableParallelVersions && (int32 & LCStepOptions.DisableParallelVersions) == LCStepOptions.DisableParallelVersions)
        this.AddErrorToLog(BriefcaseConsts.logLCStepDisableParallelVersions, Consts.YesValue, Consts.NoValue);
      if ((this.item.Options & LCStepOptions.BaseVersion) != LCStepOptions.BaseVersion && (int32 & LCStepOptions.BaseVersion) == LCStepOptions.BaseVersion)
        this.AddErrorToLog(BriefcaseConsts.logLCStepBaseVersion, Consts.YesValue, Consts.NoValue);
      if (!CheckHelper.CompareString(this.briefRow, "F_NOTE", this.item.Note))
        this.AddWarningToLog(BriefcaseConsts.logAttributeNote, Convert.ToString(this.briefRow["F_NOTE"]), this.item.Note);
      if (!this.CheckObjectModifyMode(this.briefRow, this.item.ObjectModifyMode))
        this.AddWarningToLog(BriefcaseConsts.logLCStepObjectModifyMode, EnumDescConverter.GetEnumDescription((Enum) (ObjectModifyModes) Convert.ToInt32(this.briefRow["F_MODIFY_MODE"])), EnumDescConverter.GetEnumDescription((Enum) this.item.ObjectModifyMode));
    }
    DataRow dataRow1 = this.metaData.Tables["IMS_LC_SCHEMAS"].Rows.Find((object) Convert.ToInt32(this.briefRow["F_SCHEMA_ID"]));
    IDBLCSchema lcSchema = this.session.GetLCSchema(this.item.SchemaID);
    if (lcSchema == null || dataRow1 == null || lcSchema.GUID.Equals(new Guid(Convert.ToString(dataRow1["F_GUID"]))))
      return;
    this.AddWarningToLog(BriefcaseConsts.logLCSchemeID, Convert.ToString(dataRow1["F_GUID"]), lcSchema.GUID.ToString());
  }

  private bool CheckAccessType(DataRow briefRow, LCAccessTypes accessType)
  {
    return (LCAccessTypes) Convert.ToInt32(briefRow["F_ACCESS_TYPE"]) == accessType;
  }

  private bool CheckObjectModifyMode(DataRow briefRow, ObjectModifyModes objectModifyMode)
  {
    return (ObjectModifyModes) Convert.ToInt32(briefRow["F_MODIFY_MODE"]) == objectModifyMode;
  }
}
