// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckLanguage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckLanguage(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckItem<IDBLanguageType, DataRow>(session, metaData, 9, briefRow, options)
{
  public override void Initialize()
  {
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, this.briefRow["F_GUID"]);
    this.item = this.session.GetLanguage(new Guid(Convert.ToString(this.briefRow["F_GUID"])), false);
    string str = Convert.ToString(this.briefRow["F_LANGUAGE_NAME"]);
    if (this.item == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logLanguageNotFound, Helper.ValueToLog(this.briefRow["F_LANGUAGE_NAME"], this.briefRow["F_GUID"], true), string.Empty);
      if (this.session.GetLanguage(str, false) == null || !this.synhronizingError)
        return;
      this.AddErrorToLog("В базе назначения уже существует языковой вариант с таким наименованием", str);
    }
    else
    {
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
      if (!str.Equals(this.item.LanguageName))
      {
        if (this.session.GetLanguage(str, false) == null || !this.synhronizingError)
          return;
        this.AddErrorToLog(BriefcaseConsts.logLanguageName, str, this.item.LanguageName);
      }
      else
        this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatName, (object) this.item.LanguageName);
    }
  }

  protected override void OnCheck()
  {
    if (!this.synhronizingError || CheckHelper.CompareBoolean(this.briefRow, "F_DEFAULT", this.item.IsDefaultLanguage))
      return;
    this.AddWarningToLog(BriefcaseConsts.logLanguageIsDefault, Convert.ToBoolean(this.briefRow["F_DEFAULT"]) ? Consts.YesValue : Consts.NoValue, this.item.IsDefaultLanguage ? Consts.YesValue : Consts.NoValue);
  }
}
