// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckLCLevel
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckLCLevel(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckItem<IDBLifecycleLevelType, DataRow>(session, metaData, 8, briefRow, options)
{
  public override void Initialize()
  {
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, this.briefRow["F_GUID"]);
    this.item = this.session.GetLifecycleLevel(new Guid(Convert.ToString(this.briefRow["F_GUID"])), false);
    string str = Convert.ToString(this.briefRow["F_LEVEL_NAME"]);
    if (this.item == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logLCLevelNotFound, Helper.ValueToLog(this.briefRow["F_LEVEL_NAME"], this.briefRow["F_GUID"], true), string.Empty);
      if (this.session.GetLifecycleLevel(str, false) == null || !this.synhronizingError)
        return;
      this.AddErrorToLog("В базе назначения уже существует уровень продвижения с таким наименованием", str);
    }
    else
    {
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
      if (!str.Equals(this.item.LevelName))
      {
        if (this.session.GetLifecycleLevel(str, false) == null || !this.synhronizingError)
          return;
        this.AddErrorToLog(BriefcaseConsts.logLCLevelName, str, this.item.LevelName);
      }
      else
        this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatName, (object) this.item.LevelName);
    }
  }

  protected override void OnCheck()
  {
    if (this.synhronizingError)
    {
      if (!CheckHelper.CompareString(this.briefRow, "F_LEVEL_NAME", this.item.LevelName))
        this.AddWarningToLog(BriefcaseConsts.logLCLevelName, Convert.ToString(this.briefRow["F_LEVEL_NAME"]), this.item.LevelName);
      if (!CheckHelper.CheckIcons(this.briefRow, this.item.LevelIcon))
        this.AddWarningToLog(BriefcaseConsts.logObjectTypeIcon);
      if (!CheckHelper.CompareBoolean(this.briefRow, "F_DEFAULT", this.item.IsDefaultLevel))
        this.AddWarningToLog(BriefcaseConsts.logLCLevelIsDefault, Convert.ToBoolean(this.briefRow["F_DEFAULT"]) ? Consts.YesValue : Consts.NoValue, this.item.IsDefaultLevel ? Consts.YesValue : Consts.NoValue);
      if (!CheckHelper.CheckArea(this.session, this.metaData, this.briefRow, (this.item as IDBSubjectArea).SubjectAreas))
      {
        IDBSubjectAreaCollection subjectAreaCollection = this.session.GetSubjectAreaCollection();
        string areasCaption1 = subjectAreaCollection.GetAreasCaption(Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, Convert.ToString(this.briefRow["F_AREA_ID"]).Trim()));
        string areasCaption2 = subjectAreaCollection.GetAreasCaption((this.item as IDBSubjectArea).SubjectAreas);
        this.AddWarningToLog(BriefcaseConsts.logObjectTypeSubjectAreas, areasCaption1, areasCaption2);
      }
    }
    if (CheckHelper.CompareString(this.briefRow, "F_LITERA", this.item.Litera))
      return;
    this.AddWarningToLog(BriefcaseConsts.logLCLevelLitera, Convert.ToString(this.briefRow["F_LITERA"]), this.item.Litera);
  }
}
