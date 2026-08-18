// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportLCLevel
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportLCLevel : ImportItem
{
  public ImportLCLevel(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    ImportItemOptions options)
    : base(userSession, briefRow, metaData, options)
  {
    this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_286"), briefRow["F_LEVEL_NAME"]);
  }

  public override bool Import()
  {
    try
    {
      IDBLifecycleLevelType lifecycleLevel1 = this.session.GetLifecycleLevel(new Guid(this.briefRow["F_GUID"].ToString()), false);
      if (lifecycleLevel1 != null)
      {
        if (this.LangEquals && !this.CreateOnly)
        {
          if (this.briefRow["F_LEVEL_NAME"].ToString() != lifecycleLevel1.LevelName)
          {
            this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCLevelName, (object) lifecycleLevel1.LevelName, this.briefRow["F_LEVEL_NAME"]));
            lifecycleLevel1.LevelName = this.briefRow["F_LEVEL_NAME"].ToString();
          }
          if (!CheckHelper.CheckIcons(this.briefRow, lifecycleLevel1.LevelIcon))
          {
            lifecycleLevel1.LevelIcon = this.briefRow["F_ICON"] as byte[];
            this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCLevelIcon, (object) lifecycleLevel1.LevelName));
          }
          if (!CheckHelper.CheckArea(this.session, this.metaData, this.briefRow, (lifecycleLevel1 as IDBSubjectArea).SubjectAreas))
          {
            (lifecycleLevel1 as IDBSubjectArea).SubjectAreas = Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, this.briefRow["F_AREA_ID"].ToString());
            this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCLevelSubjectArea, (object) lifecycleLevel1.LevelName));
          }
          if (!CheckHelper.CompareString(this.briefRow, "F_LITERA", lifecycleLevel1.Litera))
          {
            lifecycleLevel1.Litera = this.briefRow["F_LITERA"].ToString();
            this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCLevelLitera, (object) lifecycleLevel1.LevelName));
          }
        }
        else
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCLevelNotSynhronize, (object) lifecycleLevel1.LevelName));
      }
      else
      {
        IDBLifecycleLevelCollection lifecycleLevelCollection = this.session.GetLifecycleLevelCollection();
        bool isDefault = lifecycleLevelCollection.Select(string.Empty).Rows.Count == 0;
        lifecycleLevelCollection.Create(this.briefRow["F_LEVEL_NAME"].ToString(), this.briefRow["F_LITERA"].ToString(), Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, this.briefRow["F_AREA_ID"].ToString()), new Guid(this.briefRow["F_GUID"].ToString()), isDefault);
        if (this.briefRow["F_ICON"] != null && this.briefRow["F_ICON"] != DBNull.Value && (this.briefRow["F_ICON"] as byte[]).Length != 0)
        {
          IDBLifecycleLevelType lifecycleLevel2 = this.session.GetLifecycleLevel(new Guid(this.briefRow["F_GUID"].ToString()), false);
          if (lifecycleLevel2 != null)
            lifecycleLevel2.LevelIcon = this.briefRow["F_ICON"] as byte[];
        }
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogLCLevel, (object) this.briefRow["F_LEVEL_NAME"].ToString()));
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
