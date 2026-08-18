// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckRelationType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckRelationType(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckAttributableType<IDBRelationType, DataRow>(session, metaData, 6, briefRow, options)
{
  public override void Initialize()
  {
    this.item = this.session.GetRelationType(new Guid(Convert.ToString(this.briefRow["F_GUID"])), false);
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, this.briefRow["F_GUID"]);
    string str = Convert.ToString(this.briefRow["F_DESCRIPTION"]);
    if (this.item == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logRelationTypeNotFound, Helper.ValueToLog(this.briefRow["F_DESCRIPTION"], this.briefRow["F_GUID"], true), string.Empty);
      if (this.session.GetRelationType(str, false) == null || !this.synhronizingError)
        return;
      this.AddErrorToLog("В базе назначения уже существует тип связей с таким наименованием", str);
    }
    else
    {
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
      if (!str.Equals(this.item.Description))
      {
        if (this.session.GetRelationType(str, false) == null || !this.synhronizingError)
          return;
        this.AddErrorToLog(BriefcaseConsts.logRelationTypeName, Convert.ToString(this.briefRow["F_DESCRIPTION"]), this.item.Description);
      }
      else
        this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatName, (object) this.item.Description);
    }
  }

  protected override void OnCheck()
  {
    int num = (int) CheckHelper.CheckAnyAttributes(this.briefRow, this.item.AnyAttributes);
    if (num == 9)
      this.AddErrorToLog(BriefcaseConsts.logObjectTypeAnyAttributes, Convert.ToBoolean(this.briefRow["F_ANY_ATTRIBUTES"]) ? Consts.YesValue : Consts.NoValue, this.item.AnyAttributes ? Consts.YesValue : Consts.NoValue);
    if (num == 10 && this.synhronizingError)
      this.AddWarningToLog(BriefcaseConsts.logObjectTypeAnyAttributes, Convert.ToBoolean(this.briefRow["F_ANY_ATTRIBUTES"]) ? Consts.YesValue : Consts.NoValue, this.item.AnyAttributes ? Consts.YesValue : Consts.NoValue);
    if (this.synhronizingError)
    {
      if (!CheckHelper.CompareString(this.briefRow, "F_DESCRIPTION", this.item.Description))
        this.AddWarningToLog(BriefcaseConsts.logRelationTypeName, Convert.ToString(this.briefRow["F_DESCRIPTION"]), this.item.Description);
      if (!CheckHelper.CheckArea(this.session, this.metaData, this.briefRow, (this.item as IDBSubjectArea).SubjectAreas))
      {
        IDBSubjectAreaCollection subjectAreaCollection = this.session.GetSubjectAreaCollection();
        this.AddWarningToLog(BriefcaseConsts.logObjectTypeSubjectAreas, subjectAreaCollection.GetAreasCaption(Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, Convert.ToString(this.briefRow["F_AREA_ID"]))), subjectAreaCollection.GetAreasCaption((this.item as IDBSubjectArea).SubjectAreas));
      }
      if (!CheckHelper.CompareBoolean(this.briefRow, "F_CHKOUTFILE", this.item.CheckoutFile))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logRelationTypeCheckoutFiles, Convert.ToBoolean(this.briefRow["F_CHKOUTFILE"]) ? Consts.YesValue : Consts.NoValue, this.item.CheckoutFile ? Consts.YesValue : Consts.NoValue);
      if (!CheckHelper.CompareString(this.briefRow, "F_SHORT_NAME", this.item.ShortName))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeShortName, Convert.ToString(this.briefRow["F_SHORT_NAME"]), this.item.ShortName);
      if (!CheckHelper.CheckIcons(this.briefRow, this.item.Icon))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypeIcon);
      if (!CheckHelper.CompareString(this.briefRow, "F_NOTE", this.item.Note))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeNote, Convert.ToString(this.briefRow["F_NOTE"]), this.item.Note);
      if (!CheckHelper.CompareString(this.briefRow, "F_REVERSE_NAME", this.item.ReverseName))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logRelationTypeReverseName, Convert.ToString(this.briefRow["F_REVERSE_NAME"]), this.item.ReverseName);
      if (!CheckHelper.CompareString(this.briefRow, "F_TYPE_NAME", this.item.TypeName))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logRelationTypeTypeName, Convert.ToString(this.briefRow["F_TYPE_NAME"]), this.item.TypeName);
    }
    this.CheckAttributes();
  }

  protected override DataRow[] GetTypeAttributes()
  {
    return this.metaData.Tables["IMS_ATTR4RELATION_TYPES"].Select($"{"F_RELATION_TYPE"}={Convert.ToInt32(this.briefRow["F_RELATION_TYPE"])}");
  }

  protected override int CheckAttribute(
    DataRow attrRow,
    IDictionary<string, bool> formulaAttributes)
  {
    CheckAttributes4RelationType attributes4RelationType = new CheckAttributes4RelationType(this.session, this.item, formulaAttributes, this.metaData, attrRow, this.options);
    attributes4RelationType.Initialize();
    attributes4RelationType.Check();
    if (attributes4RelationType.AttributeID != 0 && attributes4RelationType.Log.Count > 0)
      this.infoLog.AddRange((IEnumerable<CheckMetadataLogItem>) attributes4RelationType.Log);
    return attributes4RelationType.AttributeID;
  }

  protected override void CheckAttributesCollection(List<int> presentAttributes)
  {
    CheckAttribute4RelationTypeCollection relationTypeCollection = new CheckAttribute4RelationTypeCollection(this.session, presentAttributes, this.item, this.UniIdentifiler, this.options);
    relationTypeCollection.Compare();
    if (relationTypeCollection.Log.Count <= 0)
      return;
    this.infoLog.AddRange((IEnumerable<CheckMetadataLogItem>) relationTypeCollection.Log);
  }
}
