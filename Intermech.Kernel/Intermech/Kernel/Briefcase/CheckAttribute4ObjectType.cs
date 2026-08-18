// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttribute4ObjectType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckAttribute4ObjectType(
  UserSession session,
  IDBObjectType objType,
  IDictionary<string, bool> formulaAttributes,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckAttribute4Type<IDBObjectType>(session, objType, formulaAttributes, BriefcaseConsts.logAttribute4ObjectTypeCategory, metaData, briefRow, options)
{
  protected override void FormingUniIdentifiler(string uidAttribute)
  {
    DataRow dataRow = this.metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(this.briefRow["F_OBJECT_TYPE"]);
    this.UniIdentifiler = string.Format(BriefcaseConsts.logAttribute4ObjectTypeFormatName, (object) uidAttribute, dataRow["F_OBJ_TYPE_NAME"]);
  }

  protected override void OnCheck()
  {
    if (!this.Existing || this.item.Attributes.GetAttributeByID(this.attrType.AttributeID, false) == null)
    {
      if (this.isSynhronizing)
      {
        if (!this.CheckPublicAttribute(this.item))
          return;
        if ((Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 8) == 8 && CompareValuesHelper.NormalizedValue(this.briefRow["F_DEFAULT_VALUE"]) == null)
        {
          DataTable dataTable = this.session.DataManager.ExecuteDataTable($"SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :v_id AND F_OBJECT_ID > 0 AND F_LEVEL_ID <> {this.session.IdentHelper.DeletedID}", this.session.DataManager.Parameter("v_id", (object) this.item.ObjectType));
          if (dataTable.Rows.Count > 0)
          {
            DataRow dataRow = this.metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(this.briefRow["F_ATTRIBUTE_ID"]);
            this.AddErrorToLog(string.Format(BriefcaseConsts.logAttribute4ObjectTypeAddNullVAlueAttribute, dataRow["F_NAME"], (object) dataTable.Rows.Count));
          }
        }
      }
      if (!this.noneSynhronizingError)
        return;
      DataRow dataRow1 = this.metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(this.briefRow["F_ATTRIBUTE_ID"]);
      this.AddErrorToLog(BriefcaseConsts.logObjectTypeAttributeNotPresent, Helper.ValueToLog(dataRow1["F_NAME"], dataRow1["F_GUID"], true), string.Empty);
    }
    else
    {
      Attribute4ObjectTypeProperties propertiesStructure = ((this.item.Attributes as IDBAttribute4ObjectTypeCollection).GetAttributeByID(this.attrType.AttributeID) as IDBAttributeType4Object).Attribute4ObjectPropertiesStructure;
      if (this.synhronizingError)
      {
        switch (this.CheckInheritModes(this.session, this.item, this.briefRow, propertiesStructure.AttributeID, propertiesStructure.InheritMode))
        {
          case CheckResult.NotEqual:
            this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeInheritMode, EnumDescConverter.GetEnumDescription((Enum) (InheritModes) Convert.ToInt32(this.briefRow["F_PUBLIC"])), EnumDescConverter.GetEnumDescription((Enum) propertiesStructure.InheritMode));
            break;
          case CheckResult.ErrorSinhronize:
            this.AddErrorToLog(BriefcaseConsts.logAttributeInheritMode, EnumDescConverter.GetEnumDescription((Enum) (InheritModes) Convert.ToInt32(this.briefRow["F_PUBLIC"])), EnumDescConverter.GetEnumDescription((Enum) propertiesStructure.InheritMode));
            break;
        }
        if (!CheckHelper.CheckLevelID(this.session, this.metaData.Tables["IMS_LEVELS"], this.briefRow, propertiesStructure.LevelID))
        {
          string empty1 = string.Empty;
          string log1;
          if (Convert.ToInt32(this.briefRow["F_LEVEL_ID"]) == 0)
          {
            log1 = LocalizationHolder.rm.GetString("Kernel_259");
          }
          else
          {
            DataRow dataRow = this.metaData.Tables["IMS_LEVELS"].Rows.Find(this.briefRow["F_LEVEL_ID"]);
            log1 = Helper.ValueToLog(dataRow["F_LEVEL_NAME"], dataRow["F_GUID"], true);
          }
          string empty2 = string.Empty;
          string log2;
          if (propertiesStructure.LevelID == 0)
          {
            log2 = LocalizationHolder.rm.GetString("Kernel_260");
          }
          else
          {
            IDBLifecycleLevelType lifecycleLevel = this.session.GetLifecycleLevel(propertiesStructure.LevelID);
            log2 = Helper.ValueToLog((object) lifecycleLevel.LevelName, (object) lifecycleLevel.GUID, true);
          }
          this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeLevelID, log1, log2);
        }
      }
      this.CheckAttributeProperties(this.formulaAttributes, propertiesStructure.RequiredMode, propertiesStructure.IsContent, propertiesStructure.Mask, propertiesStructure.ComputeValueMode, propertiesStructure.OptimizationMode, propertiesStructure.ValidationRule, propertiesStructure.DefaultValue, propertiesStructure.SourceAttributeID, propertiesStructure.MasterAttributeID, propertiesStructure.Options);
      CheckResult checkResult = CheckHelper.CheckUniqueValueModes(this.briefRow, propertiesStructure.UniqueValueMode, this.synhronizingError);
      if (checkResult == CheckResult.Equal)
        return;
      string enumDescription1 = EnumDescConverter.GetEnumDescription((Enum) propertiesStructure.UniqueValueMode);
      string enumDescription2 = EnumDescConverter.GetEnumDescription((Enum) (UniqueValueModes) Convert.ToInt32(this.briefRow["F_UNIQUE"]));
      if (checkResult == CheckResult.Error)
        this.AddErrorToLog(BriefcaseConsts.logAttributeUniqueValueMode, enumDescription2, enumDescription1);
      else
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeUniqueValueMode, enumDescription2, enumDescription1);
    }
  }

  private bool CheckPublicAttribute(IDBObjectType objType)
  {
    if (Convert.ToInt32(this.briefRow["F_PUBLIC"]) != 2)
    {
      int int32_1 = Convert.ToInt32(this.briefRow["F_OBJECT_TYPE"]);
      int int32_2 = Convert.ToInt32(this.briefRow["F_ATTRIBUTE_ID"]);
      DataRow[] dataRowArray1 = this.metaData.Tables["IMS_OBJTYPES_TREE"].Select("F_OBJECT_TYPE=" + int32_1.ToString());
      if (dataRowArray1.Length == 1)
      {
        DataRow dataRow1 = this.metaData.Tables["IMS_ATTR4OBJ_TYPES"].Rows.Find(new object[2]
        {
          (object) int32_2,
          (object) Convert.ToInt32(dataRowArray1[0]["F_PARENT_ID"])
        });
        if (dataRow1 != null && Convert.ToInt32(dataRow1["F_PUBLIC"]) == 2)
        {
          DataRow[] dataRowArray2 = this.metaData.Tables["IMS_OBJTYPES_TREE"].Select("F_PARENT_ID = " + Convert.ToString(dataRowArray1[0]["F_PARENT_ID"]));
          if (dataRowArray2.Length != 0)
          {
            foreach (DataRow dataRow2 in dataRowArray2)
            {
              if (Convert.ToInt32(this.metaData.Tables["IMS_ATTR4OBJ_TYPES"].Rows.Find(new object[2]
              {
                (object) int32_2,
                dataRow2["F_OBJECT_TYPE"]
              })["F_PUBLIC"]) == 2)
              {
                this.AddErrorToLog(string.Format(BriefcaseConsts.logObjectTypeAttributeInvalidInheriteMode, (object) string.Empty, (object) objType.ObjectTypeName));
                return false;
              }
            }
          }
        }
      }
    }
    return true;
  }

  private CheckResult CheckInheritModes(
    UserSession session,
    IDBObjectType objType,
    DataRow briefRow,
    int attributeID,
    InheritModes inheritMode)
  {
    InheritModes int32 = (InheritModes) Convert.ToInt32(briefRow["F_PUBLIC"]);
    if (int32 == InheritModes.Inherited && inheritMode != InheritModes.Inherited)
    {
      if (session.GetObjectTypeCollection(objType.ObjectType).Select(string.Empty).Rows.Count > 0 || objType.ParentTypeID < 0)
        return CheckResult.ErrorSinhronize;
      if (!((session.GetObjectType(objType.ParentTypeID).Attributes as IDBAttribute4ObjectTypeCollection).GetAttributeByID(attributeID) is IDBAttributeType4Object attributeById))
        return CheckResult.Equal;
      return attributeById.InheritMode == InheritModes.Private ? CheckResult.ErrorSinhronize : CheckResult.NotEqual;
    }
    if (int32 == InheritModes.Private && inheritMode != InheritModes.Private || int32 == InheritModes.Public && inheritMode != InheritModes.Public)
    {
      foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectTypeCollection(objType.ObjectType).Select(string.Empty).Rows)
      {
        if (inheritMode == InheritModes.Public)
        {
          if (session.ObjectsSelect(Convert.ToInt32(row["F_OBJECT_TYPE"]), new DBRecordSetParams((ConditionStructure[]) null, new object[1]
          {
            (object) -2
          })).Rows.Count > 0)
            return CheckResult.ErrorSinhronize;
        }
        IDBObjectType objectType = session.GetObjectType(Convert.ToInt32(row["F_OBJECT_TYPE"]), false);
        if (objectType != null && (objectType.Attributes as IDBAttribute4ObjectTypeCollection).GetAttributeByID(attributeID) is IDBAttributeType4Object attributeById && attributeById.InheritMode == InheritModes.Inherited)
          return CheckResult.ErrorSinhronize;
      }
      return CheckResult.Warning;
    }
    return int32 == inheritMode ? CheckResult.Equal : CheckResult.NotEqual;
  }
}
