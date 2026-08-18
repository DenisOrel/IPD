// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportSubjectArea
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportSubjectArea : ImportItem
{
  public ImportSubjectArea(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    ImportItemOptions options)
    : base(userSession, briefRow, metaData, options)
  {
    this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_323"), briefRow["F_AREA_NAME"]);
  }

  public override bool Import()
  {
    try
    {
      IDBSubjectAreaType subjectAreaType = this.session.GetSubjectAreaType(new Guid(this.briefRow["F_GUID"].ToString()), false);
      if (subjectAreaType != null)
      {
        if (this.LangEquals && !this.CreateOnly)
        {
          if (this.briefRow["F_AREA_NAME"].ToString() != subjectAreaType.AreaName)
          {
            this.AddToLog(string.Format(BriefcaseConsts.ImportLogSubjectAreaName, (object) subjectAreaType.AreaName, this.briefRow["F_AREA_NAME"]));
            subjectAreaType.AreaName = this.briefRow["F_AREA_NAME"].ToString();
          }
          if (!CheckHelper.CompareString(this.briefRow, "F_AREA_NOTE", subjectAreaType.Note))
          {
            subjectAreaType.Note = this.briefRow["F_AREA_NOTE"].ToString();
            this.AddToLog(string.Format(BriefcaseConsts.ImportLogSubjectAreaNote, (object) subjectAreaType.AreaName));
          }
        }
        else
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogSubjectAreaNotSynhronize, (object) subjectAreaType.AreaName));
      }
      else
      {
        int num = (int) this.session.GetSubjectAreaCollection().Create(this.briefRow["F_AREA_NAME"].ToString(), this.briefRow["F_AREA_NOTE"].ToString(), new Guid(this.briefRow["F_GUID"].ToString()));
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogSubjectArea, (object) this.briefRow["F_AREA_NAME"].ToString()));
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
