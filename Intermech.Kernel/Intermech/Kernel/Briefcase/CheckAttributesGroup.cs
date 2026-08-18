// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttributesGroup
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckAttributesGroup(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckItem<IDBAttributesGroup, DataRow>(session, metaData, 12, briefRow, options)
{
  public override void Initialize()
  {
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, this.briefRow["F_GUID"]);
    this.item = this.session.GetAttributesGroup(new Guid(Convert.ToString(this.briefRow["F_GUID"])), false);
    string str = Convert.ToString(this.briefRow["F_GROUP_NAME"]);
    if (this.item == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logAttributesGroupNotFound, Helper.ValueToLog(this.briefRow["F_GROUP_NAME"], this.briefRow["F_GUID"], true), string.Empty);
      if (this.session.GetAttributesGroup(str, false) == null || !this.synhronizingError)
        return;
      this.AddErrorToLog("В базе назначения уже существует группа атрибутов с таким наименованием", str);
    }
    else
    {
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
      if (!str.Equals(this.item.GroupName))
      {
        if (this.session.GetAttributesGroup(str, false) == null || !this.synhronizingError)
          return;
        this.AddErrorToLog(BriefcaseConsts.logAttributesGroupName, str);
      }
      else
        this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatName, (object) this.item.GroupName);
    }
  }

  protected override void OnCheck()
  {
    if (this.synhronizingError)
    {
      if (!CheckHelper.CompareString(this.briefRow, "F_GROUP_NAME", this.item.GroupName))
        this.AddWarningToLog(BriefcaseConsts.logAttributesGroupName, Convert.ToString(this.briefRow["F_GROUP_NAME"]), this.item.GroupName);
      if (!CheckHelper.CompareString(this.briefRow, "F_NOTE", this.item.Note))
        this.AddWarningToLog(BriefcaseConsts.logAttributeNote, Convert.ToString(this.briefRow["F_NOTE"]), this.item.Note);
      if (!CheckHelper.CheckArea(this.session, this.metaData, this.briefRow, (this.item as IDBSubjectArea).SubjectAreas))
      {
        IDBSubjectAreaCollection subjectAreaCollection = this.session.GetSubjectAreaCollection();
        this.AddWarningToLog(BriefcaseConsts.logObjectTypeSubjectAreas, subjectAreaCollection.GetAreasCaption(Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, Convert.ToString(this.briefRow["F_AREA_ID"]))), subjectAreaCollection.GetAreasCaption((this.item as IDBSubjectArea).SubjectAreas));
      }
      if (!CheckHelper.CheckLanguageID(this.session, this.metaData, this.briefRow, (this.item as IDBLanguage).LanguageID))
      {
        IDBLanguageType language1 = this.session.GetLanguage(Helper.GetConformityLanguage(this.session, this.metaData, Convert.ToString(this.briefRow["F_LANGUAGE_ID"])));
        IDBLanguageType language2 = this.session.GetLanguage((this.item as IDBLanguage).LanguageID);
        this.AddWarningToLog(BriefcaseConsts.logAttributeLanguage, language1.LanguageName, language2.LanguageName);
      }
    }
    CheckAttributesGroupCollection attributesGroupCollection = new CheckAttributesGroupCollection(this.session, this.metaData, this.item, Convert.ToInt32(this.briefRow["F_GROUP_ID"]), this.UniIdentifiler, this.options);
    attributesGroupCollection.Compare();
    if (attributesGroupCollection.Log.Count <= 0)
      return;
    this.infoLog.AddRange((IEnumerable<CheckMetadataLogItem>) attributesGroupCollection.Log);
  }
}
