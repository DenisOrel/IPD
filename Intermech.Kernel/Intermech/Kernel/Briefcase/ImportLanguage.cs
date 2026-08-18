// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportLanguage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportLanguage : ImportItem
{
  public ImportLanguage(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    ImportItemOptions options)
    : base(userSession, briefRow, metaData, options)
  {
    this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_285"), briefRow["F_LANGUAGE_NAME"]);
  }

  public override bool Import()
  {
    try
    {
      IDBLanguageType language = this.session.GetLanguage(new Guid(this.briefRow["F_GUID"].ToString()), false);
      if (language != null)
      {
        if (!this.LangEquals || this.CreateOnly)
        {
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogLanguageNotSynhronize, (object) language.LanguageName));
          return true;
        }
        if (this.briefRow["F_LANGUAGE_NAME"].ToString() != language.LanguageName)
        {
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogLanguageName, (object) language.LanguageName, this.briefRow["F_LANGUAGE_NAME"]));
          language.LanguageName = this.briefRow["F_LANGUAGE_NAME"].ToString();
          language.CultureID = this.briefRow["F_CULTURE_ID"].ToString();
        }
      }
      else
      {
        int num = (int) this.session.GetLanguageCollection().Create(this.briefRow["F_LANGUAGE_NAME"].ToString(), new Guid(this.briefRow["F_GUID"].ToString()), this.briefRow["F_CULTURE_ID"].ToString());
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogNewLanguage, this.briefRow["F_LANGUAGE_NAME"]));
      }
      return true;
    }
    catch (Exception ex)
    {
      this.ErrorException = new Exception(this.UniIdentifiler, ex);
      return false;
    }
  }
}
