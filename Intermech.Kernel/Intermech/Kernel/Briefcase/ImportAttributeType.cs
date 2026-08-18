// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportAttributeType
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

internal sealed class ImportAttributeType : ImportItem
{
  private ImportAttributesStore _attributesStore;
  private ImportStore _importStore;
  private List<SaveImportValues> AttributesOptimizationMode;

  public ImportAttributeType(
    UserSession session,
    DataRow briefRow,
    DataSet metaData,
    ImportAttributesStore attributesStore,
    ImportStore importStore,
    List<SaveImportValues> attributesOptimizationMode,
    ImportItemOptions options)
    : base(session, briefRow, metaData, options)
  {
    this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_284"), briefRow["F_NAME"]);
    this._attributesStore = attributesStore;
    this._importStore = importStore;
    this.AttributesOptimizationMode = attributesOptimizationMode;
  }

  public override bool Import()
  {
    try
    {
      IDBAttributeType attributeType = this.session.GetAttributeType(new Guid(this.briefRow["F_GUID"].ToString()), false);
      if ((!this.LangEquals || this.CreateOnly) && attributeType != null)
      {
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogAttributeTypeNotSynhronized, (object) attributeType.Name));
        return true;
      }
      AttributeTypeProperties attrProperties = new AttributeTypeProperties(this.briefRow);
      if (attrProperties.FieldType == FieldTypes.ftObjectLink && this.briefRow["F_SIZE_TYPE"] != null && Convert.ToInt32(this.briefRow["F_SIZE_TYPE"]) > 0)
      {
        this._attributesStore.ObjectLinkToAttributeType.Add(this.briefRow["F_GUID"], this.briefRow["F_SIZE_TYPE"]);
        attrProperties.SizeType = attributeType != null ? attributeType.SizeType : -1L;
      }
      if (this.briefRow["F_MASTER_ID"] != null && Convert.ToInt32(this.briefRow["F_MASTER_ID"]) > 0)
      {
        this._attributesStore.MasterAttrToAttributeType.Add(this.briefRow["F_GUID"], this.briefRow["F_MASTER_ID"]);
        attrProperties.MasterAttributeID = attributeType != null ? attributeType.MasterAttributeID : 0;
      }
      if (this.briefRow["F_SOURCE_ID"] != null && Convert.ToInt32(this.briefRow["F_SOURCE_ID"]) > 0)
      {
        this._attributesStore.SourceAttrToAttributeType.Add(this.briefRow["F_GUID"], this.briefRow["F_SOURCE_ID"]);
        attrProperties.SourceAttributeID = attributeType != null ? attributeType.SourceAttributeID : 0;
      }
      attrProperties.LanguageID = Helper.GetConformityLanguage(this.session, this.metaData, this.briefRow["F_LANGUAGE_ID"].ToString());
      attrProperties.AreaID = Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, this.briefRow["F_AREA_ID"].ToString());
      if (this.briefRow["F_DEFAULT_VALUE"] == null || this.briefRow["F_DEFAULT_VALUE"].ToString() == string.Empty || this.briefRow["F_DEFAULT_VALUE"] == DBNull.Value)
        attrProperties.DefaultValue = (object) null;
      object measuredDefaultVAlue = (object) null;
      long num1 = -1;
      AttributeTypePossibleValues typePossibleValues = (AttributeTypePossibleValues) null;
      if (attrProperties.MultiValueMode == MultiValueModes.MultiValuesFromList || attrProperties.MultiValueMode == MultiValueModes.SingleValueFromList)
      {
        DataRow[] fromRows = this.metaData.Tables["IMS_POSSIBLE_VALUES"].Select(string.Format("{1} = {0} AND {2} = -1 AND {3} = -1", (object) "F_ATTRIBUTE_ID", this.briefRow["F_ATTRIBUTE_ID"], (object) "F_OBJECT_TYPE", (object) "F_RELATION_TYPE"), "F_INLIST_ID");
        if (attrProperties.FieldType == FieldTypes.ftObjectLink)
        {
          typePossibleValues = new AttributeTypePossibleValues(-1, attrProperties.FieldType);
          foreach (DataRow dataRow in fromRows)
            typePossibleValues.AddValue(dataRow["F_INLIST_ID"], dataRow["F_INTEGER_VALUE"], dataRow["F_DESCRIPTION"]);
          attrProperties.DefaultValue = attributeType != null ? attributeType.DefaultValue : (object) DBNull.Value;
          if (Convert.ToString(attrProperties.DefaultValue) != Consts.CurrentUserFunction)
            measuredDefaultVAlue = attrProperties.DefaultValue;
        }
        else
        {
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          List<FieldTypes> convertList = new List<FieldTypes>();
          RelationalOperators[] enabledOperators = (RelationalOperators[]) null;
          bool computableAttribute = false;
          AttributeCacheHelper.GetAttributeTypeValues(attrProperties.FieldType, attrProperties.AttributeID, ref empty1, ref empty3, ref convertList, ref enabledOperators, ref computableAttribute, ref empty2);
          DataTable toTable = new DataTable("IMS_POSSIBLE_VALUES");
          DataColumn dataColumn1 = new DataColumn("F_INLIST_ID", typeof (int));
          DataColumn dataColumn2 = new DataColumn(empty2)
          {
            DataType = this.metaData.Tables["IMS_POSSIBLE_VALUES"].Columns[empty2].DataType
          };
          DataColumn dataColumn3 = new DataColumn("F_DESCRIPTION", typeof (string));
          toTable.Columns.AddRange(new DataColumn[3]
          {
            dataColumn1,
            dataColumn2,
            dataColumn3
          });
          DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
          attrProperties.PossibleValues = toTable;
        }
      }
      else if (attrProperties.FieldType == FieldTypes.ftObjectLink && attrProperties.DefaultValue != null && attrProperties.DefaultValue != DBNull.Value)
      {
        measuredDefaultVAlue = attrProperties.DefaultValue;
        attrProperties.DefaultValue = attributeType != null ? attributeType.DefaultValue : (object) DBNull.Value;
      }
      if (attrProperties.FieldType == FieldTypes.ftMeasured)
      {
        num1 = attrProperties.SizeType;
        attrProperties.SizeType = attributeType != null ? attributeType.SizeType : -1L;
        if (attrProperties.DefaultValue != null && attrProperties.DefaultValue != DBNull.Value)
        {
          measuredDefaultVAlue = attrProperties.DefaultValue;
          attrProperties.DefaultValue = attributeType != null ? attributeType.DefaultValue : (object) DBNull.Value;
        }
      }
      string str = string.Empty;
      List<IDBAttributesGroup> attributeGroups = this.GetAttributeGroups();
      int num2;
      if (attributeType != null)
      {
        if (attrProperties.OptimizationMode != attributeType.OptimizationMode)
        {
          this.AttributesOptimizationMode.Add(new SaveImportValues(attributeType.AttributeID, (object) attrProperties.OptimizationMode));
          attrProperties.OptimizationMode = attributeType.OptimizationMode;
        }
        if (attrProperties.Formula != attributeType.Formula)
        {
          str = attrProperties.Formula;
          attrProperties.Formula = attributeType.Formula;
        }
        attrProperties.AttributeID = attributeType.AttributeID;
        num2 = attributeType.AttributeID;
        DataTable possibleValues = attrProperties.PossibleValues;
        attrProperties.PossibleValues = (DataTable) null;
        object obj = (object) null;
        bool flag = false;
        if (possibleValues != null && attrProperties.DefaultValue != null && !Convert.ToString(attributeType.DefaultValue).Equals(Convert.ToString(attrProperties.DefaultValue)))
        {
          obj = attrProperties.DefaultValue;
          attrProperties.DefaultValue = (object) null;
          flag = true;
        }
        int num3 = attributeType.AttributeType != attrProperties.FieldType ? 1 : 0;
        attributeType.PropertiesStructure = attrProperties;
        if (num3 != 0)
          attributeType = this.session.GetAttributeType((attributeType as IDBGuid).GUID);
        if (possibleValues != null)
        {
          attributeType.SetNewPossibleValues(possibleValues);
          if (flag)
            attributeType.DefaultValue = obj;
        }
        foreach (IDBAttributesGroup dbAttributesGroup in attributeGroups)
        {
          DataRow[] dataRowArray = this.session.CacheDataSet.Tables["IMS_ATTR_IN_GROUPS"].Select($"F_GROUP_ID={(object) dbAttributesGroup.GroupID} AND F_ATTRIBUTE_ID={num2.ToString()}");
          if (dataRowArray == null || dataRowArray.Length == 0)
            dbAttributesGroup.IncludeAttribute(num2);
        }
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogAttributeTypeProperties, (object) attributeType.Name));
      }
      else
      {
        if (attrProperties.Formula != string.Empty)
        {
          str = attrProperties.Formula;
          attrProperties.Formula = string.Empty;
        }
        OptimizationModes optimizationModes = OptimizationModes.NotFound;
        if (attrProperties.OptimizationMode != OptimizationModes.Write)
        {
          optimizationModes = attrProperties.OptimizationMode;
          attrProperties.OptimizationMode = OptimizationModes.Write;
        }
        IDBAttributeTypeCollection attributeTypeCollection = (IDBAttributeTypeCollection) null;
        DataRow[] dataRowArray = this.metaData.Tables["IMS_ATTR_IN_GROUPS"].Select("F_ATTRIBUTE_ID=" + this.briefRow["F_ATTRIBUTE_ID"].ToString());
        if (dataRowArray.Length != 0)
        {
          DataRow dataRow = this.metaData.Tables["IMS_ATTR_GROUPS"].Rows.Find(dataRowArray[0]["F_GROUP_ID"]);
          if (dataRow != null)
          {
            IDBAttributesGroup attributesGroup = this.session.GetAttributesGroup(new Guid(dataRow["F_GUID"].ToString()), false);
            if (attributesGroup != null)
              attributeTypeCollection = this.session.GetAttributeTypeCollection(attributesGroup.GroupID);
          }
        }
        if (attributeTypeCollection == null)
          attributeTypeCollection = this.session.GetAttributeTypeCollection(-1);
        num2 = (attributeTypeCollection as DBAttributeTypeCollection).CreateFast(attrProperties);
        foreach (IDBAttributesGroup dbAttributesGroup in attributeGroups)
          (dbAttributesGroup as DBAttributesGroup).FastIncludeAttribute(num2);
        if (optimizationModes != OptimizationModes.NotFound)
          this.AttributesOptimizationMode.Add(new SaveImportValues(num2, (object) optimizationModes));
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogAttributeType, this.briefRow["F_NAME"]));
      }
      if (attrProperties.FieldType == FieldTypes.ftMeasured)
      {
        if (num1 != -1L)
          this._importStore.MeasureValueObjectLink.Add(new SaveImportValues(num2, -1, -1, (object) num1, measuredDefaultVAlue));
      }
      else if (measuredDefaultVAlue != null)
        this._importStore.DefaultValueObjectLink.Add(new SaveImportValues(num2, measuredDefaultVAlue));
      if (typePossibleValues != null)
      {
        typePossibleValues.AttributeID = num2;
        this._importStore.PossibleValuesAttributeType.Add(typePossibleValues);
      }
      if (str != string.Empty)
        this._attributesStore.AttributeFormules.Add((object) num2, (object) str);
      return true;
    }
    catch (Exception ex)
    {
      this.ErrorException = new Exception(this.UniIdentifiler, ex);
      return false;
    }
  }

  private List<IDBAttributesGroup> GetAttributeGroups()
  {
    List<IDBAttributesGroup> attributeGroups = new List<IDBAttributesGroup>();
    foreach (DataRow dataRow1 in this.metaData.Tables["IMS_ATTR_IN_GROUPS"].Select("F_ATTRIBUTE_ID=" + this.briefRow["F_ATTRIBUTE_ID"].ToString()))
    {
      DataRow dataRow2 = this.metaData.Tables["IMS_ATTR_GROUPS"].Rows.Find(dataRow1["F_GROUP_ID"]);
      if (dataRow2 != null)
      {
        IDBAttributesGroup attributesGroup = this.session.GetAttributesGroup(new Guid(dataRow2["F_GUID"].ToString()), false);
        if (attributesGroup != null)
          attributeGroups.Add(attributesGroup);
      }
    }
    return attributeGroups;
  }
}
