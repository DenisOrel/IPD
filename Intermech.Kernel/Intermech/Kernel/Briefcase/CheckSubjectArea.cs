// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckSubjectArea
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckSubjectArea(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckItem<IDBSubjectAreaType, DataRow>(session, metaData, 11, briefRow, options)
{
  public override void Initialize()
  {
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, this.briefRow["F_GUID"]);
    this.item = this.session.GetSubjectAreaType(new Guid(this.briefRow["F_GUID"].ToString()), false);
    if (this.item == null)
    {
      if (!this.noneSynhronizingError)
        return;
      this.AddErrorToLog(BriefcaseConsts.logSubjectAreaNotFound, Helper.ValueToLog(this.briefRow["F_AREA_NAME"], this.briefRow["F_GUID"], true), string.Empty);
    }
    else
    {
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
      string str = Convert.ToString(this.briefRow["F_AREA_NAME"]);
      if (!str.Equals(this.item.AreaName))
        return;
      this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatName, (object) str);
    }
  }

  protected override void OnCheck()
  {
    if (!this.isSynhronizing && !this.isErrorAlways)
      return;
    if (!CheckHelper.CompareString(this.briefRow, "F_AREA_NAME", this.item.AreaName))
      this.AddWarningToLog(BriefcaseConsts.logSubjectAreaName, Convert.ToString(this.briefRow["F_AREA_NAME"]), this.item.AreaName);
    if (CheckHelper.CompareString(this.briefRow, "F_AREA_NOTE", this.item.Note))
      return;
    this.AddWarningToLog(BriefcaseConsts.logAttributeNote, Convert.ToString(this.briefRow["F_AREA_NOTE"]), this.item.Note);
  }
}
