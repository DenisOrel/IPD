// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttributes4RelationType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckAttributes4RelationType(
  UserSession session,
  IDBRelationType relType,
  IDictionary<string, bool> formulaAttributes,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckAttribute4Type<IDBRelationType>(session, relType, formulaAttributes, BriefcaseConsts.logAttribute4RelationTypeCategory, metaData, briefRow, options)
{
  protected override void FormingUniIdentifiler(string uidAttribute)
  {
    DataRow dataRow = this.metaData.Tables["IMS_RELATION_TYPES"].Rows.Find(this.briefRow["F_RELATION_TYPE"]);
    this.UniIdentifiler = string.Format(BriefcaseConsts.logAttribute4RelationTypeFormatName, (object) uidAttribute, dataRow["F_DESCRIPTION"]);
  }

  protected override void OnCheck()
  {
    if ((!this.Existing || this.item.Attributes.GetAttributeByID(this.attrType.AttributeID, false) == null) && (Convert.ToInt32(this.briefRow["F_OPTIONS"]) & 8) == 8 && CompareValuesHelper.NormalizedValue(this.briefRow["F_DEFAULT_VALUE"]) == null)
    {
      DataTable dataTable = this.session.DataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_RELATION_TYPE = :v_id", this.session.DataManager.Parameter("v_id", (object) this.item.RelationType));
      if (dataTable.Rows.Count > 0)
      {
        DataRow dataRow = this.metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(this.briefRow["F_ATTRIBUTE_ID"]);
        this.AddErrorToLog(string.Format(BriefcaseConsts.logAttribute4RelationTypeAddNullValueAttribute, dataRow["F_NAME"], (object) dataTable.Rows.Count));
      }
    }
    if (!this.Existing)
      return;
    if (!(this.item.Attributes.GetAttributeByID(this.attrType.AttributeID) is IDBAttributeType4Relation attributeById))
    {
      if (!this.isSynhronizing)
        this.AddErrorToLog(BriefcaseConsts.logAttributeNotFoundInRelationType);
      else
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeNotFoundInRelationType);
    }
    else
    {
      Attribute4RelationTypeProperties propertiesStructure = attributeById.Attribute4RelationPropertiesStructure;
      this.CheckAttributeProperties(this.formulaAttributes, propertiesStructure.RequiredMode, propertiesStructure.IsContent, propertiesStructure.Mask, propertiesStructure.ComputeValueMode, propertiesStructure.OptimizationMode, propertiesStructure.ValidationRule, propertiesStructure.DefaultValue, propertiesStructure.SourceAttributeID, propertiesStructure.MasterAttributeID, propertiesStructure.Options);
    }
  }
}
