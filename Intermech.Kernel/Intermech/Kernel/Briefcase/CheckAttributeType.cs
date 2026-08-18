// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckAttributeType(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckItem<IDBAttributeType, DataRow>(session, metaData, 3, briefRow, options)
{
  public override void Initialize()
  {
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, this.briefRow["F_GUID"]);
    this.item = this.session.GetAttributeType(new Guid(Convert.ToString(this.briefRow["F_GUID"])), false);
    string str = Convert.ToString(this.briefRow["F_NAME"]);
    if (this.item == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logAttributeNotFound, Helper.ValueToLog(this.briefRow["F_NAME"], this.briefRow["F_GUID"], true), string.Empty);
      if (this.session.GetAttributeType(str, false) == null || !this.synhronizingError)
        return;
      this.AddErrorToLog("В базе назначения уже существует атрибут с таким наименованием", str);
    }
    else
    {
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
      if (!str.Equals(this.item.Name))
      {
        if (this.session.GetAttributeType(str, false) == null || !this.synhronizingError)
          return;
        this.AddErrorToLog(BriefcaseConsts.logName, str);
      }
      else
        this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatName, (object) str);
    }
  }

  protected override void OnCheck()
  {
    if (!this.CheckFieldTypes(this.briefRow, this.item))
    {
      this.AddErrorToLog(BriefcaseConsts.logAttributeFieldTypeError, EnumDescConverter.GetEnumDescription((Enum) (FieldTypes) Convert.ToInt32(this.briefRow["F_ATTRIBUTE_TYPE"])), EnumDescConverter.GetEnumDescription((Enum) this.item.AttributeType));
    }
    else
    {
      CheckResult checkResult = this.CheckFieldTypesSize(this.briefRow, this.item);
      if (checkResult != CheckResult.Equal)
        this.AddInfoInLog(CheckMetadataLogItemType.WarningLostData, checkResult == CheckResult.NotEqual ? BriefcaseConsts.logAttributeFieldType : BriefcaseConsts.logAttributeLostData, EnumDescConverter.GetEnumDescription((Enum) (FieldTypes) Convert.ToInt32(this.briefRow["F_ATTRIBUTE_TYPE"])) + (Convert.ToInt32(this.briefRow["F_ATTRIBUTE_TYPE"]) == 1 ? $" ({(object) Convert.ToInt32(this.briefRow["F_SIZE_TYPE"])})" : string.Empty), EnumDescConverter.GetEnumDescription((Enum) this.item.AttributeType) + (this.item.AttributeType == FieldTypes.ftString ? $" ({(object) this.item.SizeType})" : string.Empty));
    }
    Hashtable hashtable = CheckHelper.CheckOptions(this.briefRow, this.item.Options);
    if ((CheckResult) hashtable[(object) AttributeOptions.DisableNulls] != CheckResult.Equal)
    {
      string dbValue = (this.item.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls ? LocalizationHolder.rm.GetString("Kernel_270") : LocalizationHolder.rm.GetString("Kernel_271");
      string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 8) == 8 ? LocalizationHolder.rm.GetString("Kernel_272") : LocalizationHolder.rm.GetString("Kernel_273");
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
    if (this.synhronizingError)
    {
      if (!CheckHelper.CompareString(this.briefRow, "F_NAME", this.item.Name))
        this.AddWarningToLog(string.Format(BriefcaseConsts.logName, (object) LocalizationHolder.rm.GetString("Kernel_269")), Convert.ToString(this.briefRow["F_NAME"]), this.item.Name);
      if (!CheckHelper.CompareString(this.briefRow, "F_ALIAS", this.item.Alias))
        this.AddWarningToLog(BriefcaseConsts.logAlias, Convert.ToString(this.briefRow["F_ALIAS"]), this.item.Alias);
      if ((CheckResult) hashtable[(object) AttributeOptions.SaveCommonHistory] == CheckResult.NotEqual)
      {
        string dbValue = (this.item.Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory ? Consts.YesValue : Consts.NoValue;
        string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 4) == 4 ? Consts.YesValue : Consts.NoValue;
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSaveCommonHistory, briefValue, dbValue);
      }
      if ((CheckResult) hashtable[(object) AttributeOptions.DisableManualEdit] == CheckResult.NotEqual)
      {
        string dbValue = (this.item.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit ? Consts.YesValue : Consts.NoValue;
        string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 128 /*0x80*/) == 128 /*0x80*/ ? Consts.YesValue : Consts.NoValue;
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeDisableManualEdit, briefValue, dbValue);
      }
      if ((CheckResult) hashtable[(object) AttributeOptions.GetDescriptionEvent] == CheckResult.NotEqual)
      {
        string dbValue = (this.item.Options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent ? Consts.YesValue : Consts.NoValue;
        string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/ ? Consts.YesValue : Consts.NoValue;
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeGetDescriptionEvent, briefValue, dbValue);
      }
      if ((CheckResult) hashtable[(object) AttributeOptions.Internal] == CheckResult.NotEqual)
      {
        string dbValue = (this.item.Options & AttributeOptions.Internal) == AttributeOptions.Internal ? Consts.YesValue : Consts.NoValue;
        string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 32 /*0x20*/) == 32 /*0x20*/ ? Consts.YesValue : Consts.NoValue;
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeInternal, briefValue, dbValue);
      }
      if ((CheckResult) hashtable[(object) AttributeOptions.ModifyInBase] == CheckResult.NotEqual)
      {
        string dbValue = (this.item.Options & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase ? Consts.YesValue : Consts.NoValue;
        string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 64 /*0x40*/) == 64 /*0x40*/ ? Consts.YesValue : Consts.NoValue;
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeModifyInBase, briefValue, dbValue);
      }
      if ((CheckResult) hashtable[(object) AttributeOptions.SaveInLog] == CheckResult.NotEqual)
      {
        string dbValue = (this.item.Options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog ? Consts.YesValue : Consts.NoValue;
        string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 1) == 1 ? Consts.YesValue : Consts.NoValue;
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSaveInLog, briefValue, dbValue);
      }
      if ((CheckResult) hashtable[(object) AttributeOptions.SavePrivateHistory] == CheckResult.NotEqual)
      {
        string dbValue = (this.item.Options & AttributeOptions.SavePrivateHistory) == AttributeOptions.SavePrivateHistory ? Consts.YesValue : Consts.NoValue;
        string briefValue = (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 2) == 2 ? Consts.YesValue : Consts.NoValue;
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSavePrivateHistory, briefValue, dbValue);
      }
    }
    if (!CheckHelper.CheckDefaultValue(this.item, this.briefRow, this.item.DefaultValue))
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeDefaultValue, Convert.ToString(this.briefRow["F_DEFAULT_VALUE"]), this.item.DefaultValue != null ? this.item.DefaultValue.ToString() : string.Empty);
    if (this.synhronizingError)
    {
      if (!CheckHelper.CompareBoolean(this.briefRow, "F_CONTENT", this.item.IsContent))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeModifyDate, Convert.ToBoolean(this.briefRow["F_CONTENT"]) ? Consts.YesValue : Consts.NoValue, this.item.IsContent ? Consts.YesValue : Consts.NoValue);
      if (!CheckHelper.CompareString(this.briefRow, "F_MASK", this.item.Mask))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeMask, Convert.ToString(this.briefRow["F_MASK"]), this.item.Mask);
      if (!CheckHelper.CompareString(this.briefRow, "F_SHORT_NAME", this.item.ShortName))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeShortName, Convert.ToString(this.briefRow["F_SHORT_NAME"]), this.item.ShortName);
      if (!CheckHelper.CompareString(this.briefRow, "F_NOTE", this.item.Note))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeNote, Convert.ToString(this.briefRow["F_NOTE"]), this.item.Note);
      if (!CheckHelper.CheckComputed(this.briefRow, this.item.Computed))
        this.AddErrorToLog(BriefcaseConsts.logAttributeComputeValueMode, EnumDescConverter.GetEnumDescription((Enum) (ComputeValueModes) Convert.ToInt32(this.briefRow["F_COMPUTED"])), EnumDescConverter.GetEnumDescription((Enum) this.item.Computed));
      if (this.item.Computed != ComputeValueModes.NotComputableValue && Convert.ToInt32(this.briefRow["F_COMPUTED"]) != 0 && !CheckHelper.CompareString(this.briefRow, "F_FORMULA", this.item.Formula))
        this.AddErrorToLog(BriefcaseConsts.logAttributeFormula, Convert.ToString(this.briefRow["F_FORMULA"]), this.item.Formula);
      if (!CheckHelper.CheckOptimizationModes(this.briefRow, this.item.OptimizationMode))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeOptimizationMode, EnumDescConverter.GetEnumDescription((Enum) (OptimizationModes) Convert.ToInt32(this.briefRow["F_INVIEW"])), EnumDescConverter.GetEnumDescription((Enum) this.item.OptimizationMode));
      if (!CheckHelper.CheckLevelID(this.session, this.metaData.Tables["IMS_LEVELS"], this.briefRow, this.item.LevelID))
      {
        string empty1 = string.Empty;
        string log1;
        if (Convert.ToInt32(this.briefRow["F_LEVEL_ID"]) == 0)
        {
          log1 = LocalizationHolder.rm.GetString("Kernel_274");
        }
        else
        {
          DataRow dataRow = this.metaData.Tables["IMS_LEVELS"].Rows.Find(this.briefRow["F_LEVEL_ID"]);
          log1 = Helper.ValueToLog(dataRow["F_LEVEL_NAME"], dataRow["F_GUID"], true);
        }
        string empty2 = string.Empty;
        string log2;
        if (this.item.LevelID == 0)
        {
          log2 = LocalizationHolder.rm.GetString("Kernel_275");
        }
        else
        {
          IDBLifecycleLevelType lifecycleLevel = this.session.GetLifecycleLevel(this.item.LevelID);
          log2 = Helper.ValueToLog((object) lifecycleLevel.LevelName, (object) lifecycleLevel.GUID, true);
        }
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeLevelID, log1, log2);
      }
      if (!CheckHelper.CheckLanguageID(this.session, this.metaData, this.briefRow, (this.item as IDBLanguage).LanguageID))
      {
        IDBLanguageType language1 = this.session.GetLanguage(Helper.GetConformityLanguage(this.session, this.metaData, Convert.ToString(this.briefRow["F_LANGUAGE_ID"]).Trim()));
        IDBLanguageType language2 = this.session.GetLanguage((this.item as IDBLanguage).LanguageID);
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeLanguage, language1.LanguageName, language2.LanguageName);
      }
      if (!CheckHelper.CheckArea(this.session, this.metaData, this.briefRow, (this.item as IDBSubjectArea).SubjectAreas))
      {
        IDBSubjectAreaCollection subjectAreaCollection = this.session.GetSubjectAreaCollection();
        string areasCaption1 = subjectAreaCollection.GetAreasCaption(Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, Convert.ToString(this.briefRow["F_AREA_ID"]).Trim()));
        string areasCaption2 = subjectAreaCollection.GetAreasCaption((this.item as IDBSubjectArea).SubjectAreas);
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypeSubjectAreas, areasCaption1, areasCaption2);
      }
    }
    switch (this.CheckMultiValueModes(this.briefRow, this.item.MultipleValued))
    {
      case CheckResult.NotEqual:
        this.AddErrorToLog(BriefcaseConsts.logAttributeMultiValueMode, EnumDescConverter.GetEnumDescription((Enum) (MultiValueModes) Convert.ToInt32(this.briefRow["F_MULTIPLE_VALUED"])), EnumDescConverter.GetEnumDescription((Enum) this.item.MultipleValued));
        break;
      case CheckResult.Warning:
        int num1 = (int) this.CheckPossibleValues(this.session, this.metaData, this.briefRow, (FieldTypes) Convert.ToInt32(this.briefRow["F_ATTRIBUTE_TYPE"]), this.item, this.isSynhronizing);
        if (num1 == 12 && this.noneSynhronizingError)
          this.AddErrorToLog(BriefcaseConsts.logAttributePossibleValues);
        if (num1 == 11 && this.synhronizingError)
        {
          this.AddErrorToLog(BriefcaseConsts.logAttributeInvalidPossibleValues);
          break;
        }
        break;
    }
    CheckResult checkResult1 = CheckHelper.CheckUniqueValueModes(this.briefRow, this.item.UniqueMode, this.synhronizingError);
    if (checkResult1 != CheckResult.Equal)
    {
      string enumDescription1 = EnumDescConverter.GetEnumDescription((Enum) this.item.UniqueMode);
      string enumDescription2 = EnumDescConverter.GetEnumDescription((Enum) (UniqueValueModes) Convert.ToInt32(this.briefRow["F_UNIQUE"]));
      if (checkResult1 == CheckResult.Error)
        this.AddErrorToLog(BriefcaseConsts.logAttributeUniqueValueMode, enumDescription2, enumDescription1);
      if (checkResult1 == CheckResult.Warning)
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeUniqueValueMode, enumDescription2, enumDescription1);
    }
    int num2;
    switch (CheckHelper.CheckSourceAttributes(this.item.SourceAttributeID, this.briefRow, this.metaData, (IUserSession) this.session))
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
        IDBAttributeType attributeType1 = this.session.GetAttributeType(this.item.SourceAttributeID, false);
        string log3;
        if (attributeType1 == null)
        {
          num2 = this.item.SourceAttributeID;
          log3 = num2.ToString();
        }
        else
          log3 = Helper.ValueToLog((object) attributeType1.Name, (object) (attributeType1 as IDBGuid).GUID, true);
        string dbValue1 = log3;
        string briefValue1 = sourceAttributeRow2 != null ? Helper.ValueToLog(sourceAttributeRow2["F_NAME"], sourceAttributeRow2["F_GUID"], true) : Convert.ToString(this.briefRow["F_SOURCE_ID"]);
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeSourceAttribute, briefValue1, dbValue1);
        break;
    }
    switch (CheckHelper.CheckMasterAttributes(this.item.MasterAttributeID, this.briefRow, this.metaData, (IUserSession) this.session))
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
        IDBAttributeType attributeType2 = this.session.GetAttributeType(this.item.MasterAttributeID, false);
        string log4;
        if (attributeType2 == null)
        {
          num2 = this.item.MasterAttributeID;
          log4 = num2.ToString();
        }
        else
          log4 = Helper.ValueToLog((object) attributeType2.Name, (object) (attributeType2 as IDBGuid).GUID, true);
        string dbValue2 = log4;
        string briefValue2 = masterAttributeRow2 != null ? Helper.ValueToLog(masterAttributeRow2["F_NAME"], masterAttributeRow2["F_GUID"], true) : Convert.ToString(this.briefRow["F_MASTER_ID"]);
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeMasterAttribute, briefValue2, dbValue2);
        break;
    }
  }

  private CheckResult CheckFieldTypesSize(DataRow briefRow, IDBAttributeType attrType)
  {
    FieldTypes int32 = (FieldTypes) Convert.ToInt32(briefRow["F_ATTRIBUTE_TYPE"]);
    if (Helper.CheckSize(int32, Convert.ToInt64(briefRow["F_SIZE_TYPE"]), attrType.AttributeType, attrType.SizeType))
      return CheckResult.Equal;
    return int32 == attrType.AttributeType ? CheckResult.NotEqual : CheckResult.NotFound;
  }

  private bool CheckFieldTypes(DataRow briefRow, IDBAttributeType attrType)
  {
    return attrType.IsCompatibleType((FieldTypes) Convert.ToInt32(briefRow["F_ATTRIBUTE_TYPE"]));
  }

  private CheckResult CheckMultiValueModes(DataRow briefRow, MultiValueModes multiValueMode)
  {
    MultiValueModes int32 = (MultiValueModes) Convert.ToInt32(briefRow["F_MULTIPLE_VALUED"]);
    if (int32 == MultiValueModes.SingleValue && multiValueMode != MultiValueModes.SingleValue || int32 == MultiValueModes.MultiValues && multiValueMode != MultiValueModes.MultiValues)
      return CheckResult.NotEqual;
    if (int32 == MultiValueModes.SingleValueFromList && (multiValueMode == MultiValueModes.MultiValuesFromList || multiValueMode == MultiValueModes.SingleValueFromList))
      return CheckResult.Warning;
    if (int32 == MultiValueModes.SingleValueFromList && (multiValueMode == MultiValueModes.SingleValue || multiValueMode == MultiValueModes.MultiValues))
      return CheckResult.NotEqual;
    if (int32 == MultiValueModes.MultiValuesFromList && multiValueMode == MultiValueModes.MultiValuesFromList)
      return CheckResult.Warning;
    return int32 == MultiValueModes.MultiValuesFromList && multiValueMode != MultiValueModes.MultiValuesFromList && multiValueMode != MultiValueModes.MultiValues ? CheckResult.NotEqual : CheckResult.Equal;
  }

  private CheckResult CheckPossibleValues(
    UserSession session,
    DataSet metaData,
    DataRow briefRow,
    FieldTypes brefType,
    IDBAttributeType attrType,
    bool synhronize)
  {
    object[] possibleValuesArray = attrType.GetPossibleValuesArray();
    int int32 = Convert.ToInt32(briefRow["F_SIZE_TYPE"]);
    DataRow[] dataRowArray = metaData.Tables["IMS_POSSIBLE_VALUES"].Select(string.Format("{1} = {0} AND {2} = -1 AND {3} = -1", briefRow["F_ATTRIBUTE_ID"], (object) "F_ATTRIBUTE_ID", (object) "F_OBJECT_TYPE", (object) "F_RELATION_TYPE"), "F_INLIST_ID");
    if (attrType.AttributeType == FieldTypes.ftObjectLink)
    {
      foreach (DataRow dataRow in dataRowArray)
      {
        Guid objectGUID = new Guid(Convert.ToString(dataRow["F_INTEGERGUID"]));
        IDBObject dbObject = session.GetObject(objectGUID, false);
        if (dbObject == null || !this.InPossibleValues(attrType.ValueFieldName, possibleValuesArray, (object) dbObject.ObjectID))
          return CheckResult.ErrorNotSinhronize;
      }
    }
    else if (possibleValuesArray != null && possibleValuesArray.Length != 0)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      List<FieldTypes> convertList = new List<FieldTypes>();
      RelationalOperators[] enabledOperators = new RelationalOperators[0];
      bool computableAttribute = false;
      AttributeCacheHelper.GetAttributeTypeValues(brefType, Convert.ToInt32(briefRow["F_ATTRIBUTE_ID"]), ref empty1, ref empty3, ref convertList, ref enabledOperators, ref computableAttribute, ref empty2);
      foreach (DataRow dataRow in dataRowArray)
      {
        if (!this.InPossibleValues((attrType as DBAttributeType).PossibleValueFieldName, possibleValuesArray, dataRow[empty2]) && !synhronize)
          return CheckResult.ErrorNotSinhronize;
        if (attrType.AttributeType == FieldTypes.ftString && Convert.ToString(dataRow["F_STRING_VALUE"]).Length > int32)
          return CheckResult.ErrorSinhronize;
      }
    }
    else if (dataRowArray.Length != 0)
      return CheckResult.ErrorNotSinhronize;
    return CheckResult.Equal;
  }

  private bool InPossibleValues(string valueFieldName, object[] array, object searchValue)
  {
    foreach (object val1 in array)
    {
      switch (valueFieldName)
      {
        case "F_INTEGER_VALUE":
          if (CompareValuesHelper.CompareIntValues(val1, searchValue))
            return true;
          break;
        case "F_DOUBLE_VALUE":
          if (CompareValuesHelper.CompareFloatValues(val1, searchValue))
            return true;
          break;
        case "F_DATE_VALUE":
          if (CompareValuesHelper.CompareDateTimeValues(val1, searchValue))
            return true;
          break;
        default:
          if (CompareValuesHelper.CompareStringValues(val1, searchValue))
            return true;
          break;
      }
    }
    return false;
  }
}
