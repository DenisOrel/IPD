// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportAttributeGroup
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportAttributeGroup : ImportItem
{
  public ImportAttributeGroup(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    ImportItemOptions options)
    : base(userSession, briefRow, metaData, options)
  {
    this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_283"), briefRow["F_GROUP_NAME"]);
  }

  public override bool Import()
  {
    try
    {
      int int32 = Convert.ToInt32(this.briefRow["F_PARENT_ID"]);
      int parentGroupID = int32 == 0 ? int32 : Helper.GetConformityAttribureGroup(this.session, this.metaData.Tables["IMS_ATTR_GROUPS"], int32);
      IDBAttributesGroup attributesGroup = this.session.GetAttributesGroup(new Guid(this.briefRow["F_GUID"].ToString()), false);
      if (attributesGroup != null)
      {
        if (this.LangEquals && !this.CreateOnly)
        {
          if (parentGroupID == -1)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1163"), this.briefRow["F_GUID"]));
          if (attributesGroup.ParentID != parentGroupID)
          {
            this.AddToLog(string.Format(BriefcaseConsts.ImportAttributesGroupParentID, (object) attributesGroup.ParentID, (object) parentGroupID));
            attributesGroup.ParentID = parentGroupID;
          }
          if (attributesGroup.GroupName != this.briefRow["F_GROUP_NAME"].ToString())
          {
            this.AddToLog(string.Format(BriefcaseConsts.ImportAttributesGroupName, (object) attributesGroup.GroupName, this.briefRow["F_GROUP_NAME"]));
            attributesGroup.GroupName = this.briefRow["F_GROUP_NAME"].ToString();
          }
          if (!CheckHelper.CompareString(this.briefRow, "F_NOTE", attributesGroup.Note))
          {
            this.AddToLog(string.Format(BriefcaseConsts.ImportAttributesGroupNote, (object) attributesGroup.GroupName));
            attributesGroup.Note = this.briefRow["F_NOTE"].ToString();
          }
          if (!CheckHelper.CheckArea(this.session, this.metaData, this.briefRow, (attributesGroup as IDBSubjectArea).SubjectAreas))
          {
            this.AddToLog(string.Format(BriefcaseConsts.ImportAttributesGroupArea, (object) attributesGroup.GroupName));
            (attributesGroup as IDBSubjectArea).SubjectAreas = Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, this.briefRow["F_AREA_ID"].ToString());
          }
          if (!CheckHelper.CheckLanguageID(this.session, this.metaData, this.briefRow, (attributesGroup as IDBLanguage).LanguageID))
          {
            this.AddToLog(string.Format(BriefcaseConsts.ImportAttributesGroupLanguage, (object) attributesGroup.GroupName));
            (attributesGroup as IDBLanguage).LanguageID = Helper.GetConformityLanguage(this.session, this.metaData, this.briefRow["F_LANGUAGE_ID"].ToString());
          }
        }
        else
          this.AddToLog(string.Format(BriefcaseConsts.ImportAttributesGroupNotSynhronize, (object) attributesGroup.GroupName));
      }
      else
      {
        (parentGroupID != 0 ? this.session.GetAttributesGroupCollection(parentGroupID) : this.session.GetAttributesGroupCollection()).Create(this.briefRow["F_GROUP_NAME"].ToString(), this.briefRow["F_NOTE"].ToString(), Helper.GetConformityLanguage(this.session, this.metaData, this.briefRow["F_LANGUAGE_ID"].ToString()), Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, this.briefRow["F_AREA_ID"].ToString()), new Guid(this.briefRow["F_GUID"].ToString()));
        this.AddToLog(string.Format(BriefcaseConsts.ImportAttributesGroup, this.briefRow["F_GROUP_NAME"]));
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
