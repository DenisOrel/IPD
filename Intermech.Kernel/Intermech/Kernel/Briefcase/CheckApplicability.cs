// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckApplicability
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckApplicability : CheckItem<IDBRelationsApplicability, DataRow>
{
  private int _briefID;

  public CheckApplicability(
    UserSession userSession,
    DataSet metaData,
    DataRow briefRow,
    string addUniidentifiler,
    int briefID,
    CheckOptions options)
    : base(userSession, metaData, BriefcaseConsts.logApplicabilityCategory, briefRow, options)
  {
    this._briefID = briefID;
    this.UniIdentifiler = addUniidentifiler;
  }

  public override void Initialize()
  {
    DataRow dataRow1 = this.metaData.Tables["IMS_RELATION_TYPES"].Rows.Find(this.briefRow["F_RELATION_TYPE"]);
    IDBRelationType relationType = this.session.GetRelationType(new Guid(Convert.ToString(dataRow1["F_GUID"])), false);
    if (relationType == null)
    {
      this.UniIdentifiler = string.Format(BriefcaseConsts.logApplicabilityUniIdentifiler, (object) string.Format(BriefcaseConsts.logFormatGUID, dataRow1["F_GUID"]), (object) string.Empty);
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logRelationTypeNotFound, Helper.ValueToLog(dataRow1["F_DESCRIPTION"], dataRow1["F_GUID"], true), string.Empty);
    }
    else if (Convert.ToString(dataRow1["F_DESCRIPTION"]) == relationType.Description)
      this.UniIdentifiler = string.Format(BriefcaseConsts.logApplicabilityUniIdentifiler, (object) string.Format(BriefcaseConsts.logFormatName, (object) relationType.Description), (object) string.Empty);
    else
      this.UniIdentifiler = string.Format(BriefcaseConsts.logApplicabilityUniIdentifiler, (object) string.Format(BriefcaseConsts.logFormatGUID, dataRow1["F_GUID"]), (object) string.Empty);
    DataRow dataRow2 = this.metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(this.briefRow["F_INOBJECT_TYPE"]);
    IDBObjectType objectType1 = this.session.GetObjectType(new Guid(Convert.ToString(dataRow2["F_GUID"])), false);
    if (objectType1 == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeNotFound, Helper.ValueToLog(dataRow2["F_OBJ_TYPE_NAME"], dataRow2["F_GUID"], true), string.Empty);
      this.UniIdentifiler += $"{{{dataRow2["F_GUID"]}}}";
    }
    else if (Convert.ToString(dataRow2["F_OBJ_TYPE_NAME"]) == objectType1.ObjectTypeName)
      this.UniIdentifiler += $"{objectType1.ObjectTypeName}";
    else
      this.UniIdentifiler += $"{{{dataRow2["F_GUID"]}}}";
    DataRow dataRow3 = this.metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(this.briefRow["F_OBJECT_TYPE"]);
    IDBObjectType objectType2 = this.session.GetObjectType(new Guid(Convert.ToString(dataRow3["F_GUID"])), false);
    if (objectType2 == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeNotFound, Helper.ValueToLog(dataRow3["F_OBJ_TYPE_NAME"], dataRow3["F_GUID"], true), string.Empty);
      this.UniIdentifiler += $" и {{{dataRow3["F_GUID"]}}}";
    }
    else if (Convert.ToString(dataRow3["F_OBJ_TYPE_NAME"]) == objectType2.ObjectTypeName)
      this.UniIdentifiler += $" и {objectType2.ObjectTypeName}";
    else
      this.UniIdentifiler += $" и {{{dataRow3["F_GUID"]}}}";
    if (relationType == null || objectType1 == null || objectType2 == null)
      return;
    this.item = this.session.GetRelationsApplicabilityCollection().GetApplicability(relationType.RelationType, objectType2.ObjectType, objectType1.ObjectType);
    if (this.item != null || !this.noneSynhronizingError)
      return;
    this.AddErrorToLog(string.Format(BriefcaseConsts.logObjectTypeRelationNotFound, (object) this.UniIdentifiler));
  }

  protected override void OnCheck()
  {
    bool boolean;
    if (!CheckHelper.CompareBoolean(this.briefRow, "F_CLONE_RELATIONS", this.item.PropertiesStructure.CloneChildRelations))
    {
      string cloneChildRelations = BriefcaseConsts.logApplicabilityCloneChildRelations;
      boolean = Convert.ToBoolean(this.briefRow["F_CLONE_RELATIONS"]);
      string briefValue = boolean.ToString();
      string dbValue = this.item.PropertiesStructure.CloneChildRelations.ToString();
      this.AddWarningToLog(cloneChildRelations, briefValue, dbValue);
    }
    RelationsApplicabilityProperties propertiesStructure;
    if (this.CheckMaximumLinks(this.briefRow, this.item.PropertiesStructure.MaximumLinks) == CheckResult.Less && this.noneSynhronizingError)
    {
      string applicabilityMaximumLinks = BriefcaseConsts.logApplicabilityMaximumLinks;
      string briefValue = Convert.ToString(this.briefRow["F_MAX_LINKS"]);
      propertiesStructure = this.item.PropertiesStructure;
      string dbValue = propertiesStructure.MaximumLinks.ToString();
      this.AddErrorToLog(applicabilityMaximumLinks, briefValue, dbValue);
    }
    CheckResult checkResult = this.CheckApplicabilityMode(this.briefRow, this.item.PropertiesStructure.ApplicabilityMode);
    if (checkResult == CheckResult.ErrorNotSinhronize && this.noneSynhronizingError || checkResult == CheckResult.ErrorSinhronize && this.synhronizingError || checkResult == CheckResult.Error)
      this.AddErrorToLog(BriefcaseConsts.logApplicabilityApplicabilityMode, EnumDescConverter.GetEnumDescription((Enum) (ApplicabilityModes) Convert.ToInt32(this.briefRow["F_MIN_LINKS"])), EnumDescConverter.GetEnumDescription((Enum) this.item.PropertiesStructure.ApplicabilityMode));
    if (!this.synhronizingError)
      return;
    if (!this.CheckRelationConstraintMode(this.briefRow, this.item.PropertiesStructure.RelationConstraintMode))
      this.AddWarningToLog(BriefcaseConsts.logApplicabilityRelationConstraintMode, EnumDescConverter.GetEnumDescription((Enum) (RelationConstraintModes) Convert.ToInt32(this.briefRow["F_CONSTRAINT_MODE"])), EnumDescConverter.GetEnumDescription((Enum) this.item.PropertiesStructure.RelationConstraintMode));
    if (!CheckHelper.CompareBoolean(this.briefRow, "F_CONTENT", this.item.PropertiesStructure.IsContent))
    {
      string applicabilityIsContent = BriefcaseConsts.logApplicabilityIsContent;
      boolean = Convert.ToBoolean(this.briefRow["F_CONTENT"]);
      string briefValue = boolean.ToString();
      propertiesStructure = this.item.PropertiesStructure;
      string dbValue = propertiesStructure.IsContent.ToString();
      this.AddWarningToLog(applicabilityIsContent, briefValue, dbValue);
    }
    ApplicabilityOptions int32 = (ApplicabilityOptions) Convert.ToInt32(this.briefRow["F_OPTIONS"]);
    if ((int32 & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.EnableMultiLink != ((this.item.PropertiesStructure.Options & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.EnableMultiLink))
      this.AddWarningToLog(BriefcaseConsts.logApplicabilityOptionsEnableMultilink, (int32 & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.EnableMultiLink ? Consts.YesValue : Consts.NoValue, (this.item.PropertiesStructure.Options & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.EnableMultiLink ? Consts.YesValue : Consts.NoValue);
    if ((int32 & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation != ((this.item.PropertiesStructure.Options & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation))
      this.AddWarningToLog(BriefcaseConsts.logApplicabilityOptionsDefaultRelation, (int32 & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation ? Consts.YesValue : Consts.NoValue, (this.item.PropertiesStructure.Options & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation ? Consts.YesValue : Consts.NoValue);
    if (CheckHelper.CompareBoolean(this.briefRow, "F_CHKOUTFILE", this.item.PropertiesStructure.CheckoutFiles))
      return;
    string applicabilityCheckoutFiles = BriefcaseConsts.logApplicabilityCheckoutFiles;
    boolean = Convert.ToBoolean(this.briefRow["F_CHKOUTFILE"]);
    string briefValue1 = boolean.ToString();
    propertiesStructure = this.item.PropertiesStructure;
    string dbValue1 = propertiesStructure.CheckoutFiles.ToString();
    this.AddWarningToLog(applicabilityCheckoutFiles, briefValue1, dbValue1);
  }

  private CheckResult CheckMaximumLinks(DataRow briefRow, int maximumLinks)
  {
    int int32 = Convert.ToInt32(briefRow["F_MAX_LINKS"]);
    if (int32 > maximumLinks)
      return CheckResult.Greater;
    return int32 < maximumLinks ? CheckResult.Less : CheckResult.Equal;
  }

  private CheckResult CheckApplicabilityMode(DataRow briefRow, ApplicabilityModes applicabilityMode)
  {
    ApplicabilityModes int32 = (ApplicabilityModes) Convert.ToInt32(briefRow["F_MIN_LINKS"]);
    if (int32 == ApplicabilityModes.Disabled && applicabilityMode != ApplicabilityModes.Disabled)
      return CheckResult.ErrorSinhronize;
    if (int32 == ApplicabilityModes.Enabled && applicabilityMode == ApplicabilityModes.Disabled)
      return CheckResult.Error;
    return int32 == ApplicabilityModes.Required && applicabilityMode == ApplicabilityModes.Disabled || int32 == ApplicabilityModes.AnyRequired && applicabilityMode == ApplicabilityModes.Disabled ? CheckResult.ErrorNotSinhronize : CheckResult.Equal;
  }

  private bool CheckRelationConstraintMode(
    DataRow briefRow,
    RelationConstraintModes relationConstraintMode)
  {
    return (RelationConstraintModes) Convert.ToInt32(briefRow["F_CONSTRAINT_MODE"]) == relationConstraintMode;
  }
}
