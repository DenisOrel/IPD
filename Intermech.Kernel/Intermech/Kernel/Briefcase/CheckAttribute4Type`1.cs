// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttribute4Type`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel.Briefcase;

internal abstract class CheckAttribute4Type<T> : CheckItem<T, DataRow>
{
  protected IDBAttributeType attrType;
  protected IDictionary<string, bool> formulaAttributes;

  public CheckAttribute4Type(
    UserSession session,
    T item,
    IDictionary<string, bool> formulaAttributes,
    string category,
    DataSet metaData,
    DataRow briefRow,
    CheckOptions options)
    : base(session, metaData, category, briefRow, options)
  {
    this.item = item;
    this.formulaAttributes = formulaAttributes;
  }

  public override void Initialize()
  {
    string g = Convert.ToString(this.metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(this.briefRow["F_ATTRIBUTE_ID"])["F_GUID"]);
    this.attrType = this.session.GetAttributeType(new Guid(g), false);
    this.FormingUniIdentifiler(this.attrType != null ? this.attrType.Name : g);
    if (this.attrType == null)
      return;
    this.isSystemGUID = (this.attrType as IDBGuid).IsSystemGUID;
  }

  public int AttributeID => this.attrType == null ? 0 : this.attrType.AttributeID;

  protected abstract void FormingUniIdentifiler(string uidAttribute);

  protected void CheckAttributeProperties(
    IDictionary<string, bool> formulaAttributes,
    RequiredModes requiredMode,
    bool isContent,
    string mask,
    ComputeValueModes computeValueMode,
    OptimizationModes optimizationMode,
    string validationRule,
    object defaultValue,
    int sourceAttributeID,
    int masterAttributeID,
    AttributeOptions options)
  {
    if (!CheckHelper.CheckDefaultValue(this.attrType, this.briefRow, defaultValue))
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeDefaultValue, CompareValuesHelper.NormalizedValue(this.briefRow["F_DEFAULT_VALUE"]) != null ? Convert.ToString(this.briefRow["F_DEFAULT_VALUE"], (IFormatProvider) CultureInfo.InvariantCulture) : string.Empty, Convert.ToString(defaultValue));
    switch (CheckHelper.CheckSourceAttributes(sourceAttributeID, this.briefRow, this.metaData, (IUserSession) this.session))
    {
      case CheckResult.NotFound:
        DataRow sourceAttributeRow1 = Helper.GetSourceAttributeRow(this.briefRow, this.metaData);
        if (this.noneSynhronizingError)
        {
          this.AddErrorToLog(BriefcaseConsts.logAttributeSourceAttributeNotFound, Helper.ValueToLog(sourceAttributeRow1["F_NAME"], sourceAttributeRow1["F_GUID"], true), string.Empty);
          break;
        }
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSourceAttributeNotFound, Helper.ValueToLog(sourceAttributeRow1["F_NAME"], sourceAttributeRow1["F_GUID"], true), string.Empty);
        break;
      case CheckResult.NotEqual:
        DataRow sourceAttributeRow2 = Helper.GetSourceAttributeRow(this.briefRow, this.metaData);
        IDBAttributeType attributeType1 = this.session.GetAttributeType(sourceAttributeID, false);
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSourceAttribute, sourceAttributeRow2 != null ? Helper.ValueToLog(sourceAttributeRow2["F_NAME"], sourceAttributeRow2["F_GUID"], true) : Convert.ToString(this.briefRow["F_SOURCE_ID"]), attributeType1 != null ? Helper.ValueToLog((object) attributeType1.Name, (object) (attributeType1 as IDBGuid).GUID, true) : sourceAttributeID.ToString());
        break;
    }
    switch (CheckHelper.CheckMasterAttributes(masterAttributeID, this.briefRow, this.metaData, (IUserSession) this.session))
    {
      case CheckResult.NotFound:
        DataRow masterAttributeRow1 = Helper.GetMasterAttributeRow(this.briefRow, this.metaData);
        if (this.noneSynhronizingError)
        {
          this.AddErrorToLog(BriefcaseConsts.logAttributeMasterAttributeNotFound, Helper.ValueToLog(masterAttributeRow1["F_NAME"], masterAttributeRow1["F_GUID"], true), string.Empty);
          break;
        }
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeMasterAttributeNotFound, Helper.ValueToLog(masterAttributeRow1["F_NAME"], masterAttributeRow1["F_GUID"], true), string.Empty);
        break;
      case CheckResult.NotEqual:
        DataRow masterAttributeRow2 = Helper.GetMasterAttributeRow(this.briefRow, this.metaData);
        IDBAttributeType attributeType2 = this.session.GetAttributeType(masterAttributeID, false);
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeMasterAttribute, masterAttributeRow2 != null ? Helper.ValueToLog(masterAttributeRow2["F_NAME"], masterAttributeRow2["F_GUID"], true) : Convert.ToString(this.briefRow["F_MASTER_ID"]), attributeType2 != null ? Helper.ValueToLog((object) attributeType2.Name, (object) (attributeType2 as IDBGuid).GUID, true) : masterAttributeID.ToString());
        break;
    }
    Hashtable hashtable = CheckHelper.CheckOptions(this.briefRow, options);
    if ((CheckResult) hashtable[(object) AttributeOptions.DisableNulls] != CheckResult.Equal)
    {
      string dbValue = (options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls ? LocalizationHolder.rm.GetString("Kernel_261") : LocalizationHolder.rm.GetString("Kernel_262");
      string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 8) == 8 ? LocalizationHolder.rm.GetString("Kernel_263") : LocalizationHolder.rm.GetString("Kernel_264");
      switch ((CheckResult) hashtable[(object) AttributeOptions.DisableNulls])
      {
        case CheckResult.ErrorSinhronize:
          this.AddInfoInLog(this.synhronizingError ? CheckMetadataLogItemType.Error : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeDisableNullsError, briefValue, dbValue);
          break;
        case CheckResult.ErrorNotSinhronize:
          if (this.noneSynhronizingError)
          {
            this.AddErrorToLog(BriefcaseConsts.logAttributeDisableNullsWarning, briefValue, dbValue);
            break;
          }
          break;
      }
    }
    if (!this.synhronizingError)
      return;
    if (!this.CheckRequiredModes(this.briefRow, requiredMode))
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeRequiredMode, EnumDescConverter.GetEnumDescription((Enum) (RequiredModes) Convert.ToInt32(this.briefRow["F_REQUIRED"])), EnumDescConverter.GetEnumDescription((Enum) requiredMode));
    if (!CheckHelper.CompareBoolean(this.briefRow, "F_CONTENT", isContent))
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeModifyDate, Convert.ToBoolean(this.briefRow["F_CONTENT"]) ? Consts.YesValue : Consts.NoValue, isContent ? Consts.YesValue : Consts.NoValue);
    if (!CheckHelper.CompareString(this.briefRow, "F_MASK", mask))
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeMask, Convert.ToString(this.briefRow["F_MASK"]), mask);
    if (!CheckHelper.CheckComputed(this.briefRow, computeValueMode))
      this.AddErrorToLog(BriefcaseConsts.logAttributeComputeValueMode, EnumDescConverter.GetEnumDescription((Enum) (ComputeValueModes) Convert.ToInt32(this.briefRow["F_COMPUTED"])), EnumDescConverter.GetEnumDescription((Enum) computeValueMode));
    if (computeValueMode != ComputeValueModes.NotComputableValue && !this.CheckAttributesInFormula(Convert.ToString(this.briefRow["F_FORMULA"]).ToUpper(), formulaAttributes))
      this.AddErrorToLog(BriefcaseConsts.logAttributeNotValidFormula, Convert.ToString(this.briefRow["F_FORMULA"]), string.Empty);
    if (!CheckHelper.CheckOptimizationModes(this.briefRow, optimizationMode))
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeOptimizationMode, EnumDescConverter.GetEnumDescription((Enum) (OptimizationModes) Convert.ToInt32(this.briefRow["F_INVIEW"])), EnumDescConverter.GetEnumDescription((Enum) optimizationMode));
    if (!CheckHelper.CompareString(this.briefRow, "F_VALIDATION_RULE", validationRule))
      this.AddErrorToLog(BriefcaseConsts.logAttributeValidationRule, Convert.ToString(this.briefRow["F_VALIDATION_RULE"]), validationRule);
    if ((CheckResult) hashtable[(object) AttributeOptions.SaveCommonHistory] == CheckResult.NotEqual)
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSaveCommonHistory, (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 4) == 4 ? Consts.YesValue : Consts.NoValue, (options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory ? Consts.YesValue : Consts.NoValue);
    if ((CheckResult) hashtable[(object) AttributeOptions.DisableManualEdit] == CheckResult.NotEqual)
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeDisableManualEdit, (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 128 /*0x80*/) == 128 /*0x80*/ ? Consts.YesValue : Consts.NoValue, (options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit ? Consts.YesValue : Consts.NoValue);
    if ((CheckResult) hashtable[(object) AttributeOptions.GetDescriptionEvent] == CheckResult.NotEqual)
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeGetDescriptionEvent, (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/ ? Consts.YesValue : Consts.NoValue, (options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent ? Consts.YesValue : Consts.NoValue);
    if ((CheckResult) hashtable[(object) AttributeOptions.Internal] == CheckResult.NotEqual)
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeInternal, (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 32 /*0x20*/) == 32 /*0x20*/ ? Consts.YesValue : Consts.NoValue, (options & AttributeOptions.Internal) == AttributeOptions.Internal ? Consts.YesValue : Consts.NoValue);
    if ((CheckResult) hashtable[(object) AttributeOptions.ModifyInBase] == CheckResult.NotEqual)
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeModifyInBase, (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 64 /*0x40*/) == 64 /*0x40*/ ? Consts.YesValue : Consts.NoValue, (options & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase ? Consts.YesValue : Consts.NoValue);
    if ((CheckResult) hashtable[(object) AttributeOptions.SaveInLog] == CheckResult.NotEqual)
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSaveInLog, (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 1) == 1 ? Consts.YesValue : Consts.NoValue, (options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog ? Consts.YesValue : Consts.NoValue);
    if ((CheckResult) hashtable[(object) AttributeOptions.SavePrivateHistory] != CheckResult.NotEqual)
      return;
    this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSavePrivateHistory, (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 2) == 2 ? Consts.YesValue : Consts.NoValue, (options & AttributeOptions.SavePrivateHistory) == AttributeOptions.SavePrivateHistory ? Consts.YesValue : Consts.NoValue);
  }

  private bool CheckRequiredModes(DataRow briefRow, RequiredModes requiredMode)
  {
    return (RequiredModes) Convert.ToInt32(briefRow["F_REQUIRED"]) == requiredMode;
  }

  private bool CheckAttributesInFormula(
    string newFormula,
    IDictionary<string, bool> formulaAttributes)
  {
    if (!string.IsNullOrEmpty(newFormula))
    {
      try
      {
        using (Parser parser = new Parser())
        {
          parser.AutoDetectVariables = true;
          parser.Validate = false;
          ExpressionVariablesCollection variables = parser.Parse(newFormula).Variables;
          for (int index = 0; index < variables.Count; ++index)
          {
            if (!(variables[index].Name.ToUpper() == "VALUE") && (!formulaAttributes.ContainsKey(variables[index].Name.ToUpper()) || !formulaAttributes[variables[index].Name.ToUpper()]))
              return false;
          }
        }
      }
      catch
      {
        return false;
      }
    }
    return true;
  }

  public override bool Existing => (object) this.item != null && this.attrType != null;
}
