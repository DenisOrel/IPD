// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportObjectType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportObjectType : ImportItem
{
  private Hashtable _importedObjectTypes;
  private List<SaveImportValues> _defaultValueObjectLink;
  private List<SaveImportValues> _measuredValueObjectLink;
  private List<SaveImportValues> _attributesOptimizationMode;
  private readonly IgnoringErrors _ignoringErrors;
  private List<int> _newObjectTypes;
  private DBObjectTypeCollection _collectionForCreate;
  private TypeFormules _formules;
  private readonly ImportObjectTypesStore _objTypeStore;

  public ImportObjectType(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    Hashtable ImportedObjectTypes,
    List<SaveImportValues> defaultValueObjectLink,
    List<SaveImportValues> measuredValueObjectLink,
    List<SaveImportValues> attributesOptimizationMode,
    IgnoringErrors ignoring,
    List<int> newObjectTypes,
    DBObjectTypeCollection collectionForCreate,
    ImportObjectTypesStore objTypeStore,
    ImportItemOptions options)
    : base(userSession, briefRow, metaData, options)
  {
    this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_307"), briefRow["F_OBJ_TYPE_NAME"]);
    this._importedObjectTypes = ImportedObjectTypes;
    this._defaultValueObjectLink = defaultValueObjectLink;
    this._measuredValueObjectLink = measuredValueObjectLink;
    this._attributesOptimizationMode = attributesOptimizationMode;
    this._ignoringErrors = ignoring;
    this._newObjectTypes = newObjectTypes;
    this._collectionForCreate = collectionForCreate;
    this._objTypeStore = objTypeStore;
  }

  private int GetDefaultRelationType()
  {
    IDBRelationType relationType = this.session.GetRelationType(new Guid("cad00022-306c-11d8-b4e9-00304f19f545"), false);
    return relationType == null ? -1 : relationType.RelationType;
  }

  private bool AddFormula(int attributeID, string formula)
  {
    if (formula == string.Empty)
      return false;
    if (this._formules == null)
      this._formules = new TypeFormules(new Guid(Convert.ToString(this.briefRow["F_GUID"])));
    this._formules.Formules.Add(attributeID, formula);
    return true;
  }

  public override bool Import()
  {
    try
    {
      IDBObjectType objectType1 = this.session.GetObjectType(new Guid(this.briefRow["F_GUID"].ToString()), false);
      bool flag = objectType1 == null;
      ObjectTypeProperties objectTypeProperties = new ObjectTypeProperties(this.briefRow)
      {
        AreaID = Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, this.briefRow["F_AREA_ID"].ToString())
      };
      int captionAttribute = objectTypeProperties.CaptionAttribute;
      objectTypeProperties.CaptionAttribute = flag ? 0 : (objectType1 != null ? objectType1.CaptionAttribute : 0);
      objectTypeProperties.DefaultRelation = objectTypeProperties.DefaultRelation <= 0 ? (objectType1 != null ? objectType1.DefaultRelation : this.GetDefaultRelationType()) : Helper.GetConformityRelationType((IUserSession) this.session, this.metaData.Tables["IMS_RELATION_TYPES"], objectTypeProperties.DefaultRelation);
      objectTypeProperties.SchemaID = Helper.GetConformityLCSchemes(this.session, this.metaData, objectTypeProperties.SchemaID);
      int objectType2;
      if (objectType1 != null)
      {
        objectTypeProperties.ObjectType = objectType1.ObjectType;
        objectType2 = objectType1.ObjectType;
        if (this.LangEquals && !this.CreateOnly)
        {
          objectType1.PropertiesStructure = objectTypeProperties;
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogObjectTypeProperties, (object) objectType1.ObjectTypeName));
        }
        else
          this.AddToLog($"Тип объектов \"{objectType1.ObjectTypeName}\" не синхронизирован");
      }
      else
      {
        int parentID = -2;
        DataRow[] dataRowArray = this.metaData.Tables["IMS_OBJTYPES_TREE"].Select("F_OBJECT_TYPE=" + this.briefRow["F_OBJECT_TYPE"].ToString());
        if (dataRowArray.Length != 0)
        {
          DataRow dataRow = this.metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(dataRowArray[0]["F_PARENT_ID"]);
          if (dataRow != null)
          {
            IDBObjectType objectType3 = this.session.GetObjectType(new Guid(dataRow["F_GUID"].ToString()), false);
            if (objectType3 != null)
              parentID = objectType3.ObjectType;
          }
        }
        this.briefRow["F_AREA_ID"] = (object) objectTypeProperties.AreaID;
        this.briefRow["F_CAPTION_ATTRIBUTE"] = (object) objectTypeProperties.CaptionAttribute;
        this.briefRow["F_DEFAULT_RELATION"] = (object) objectTypeProperties.DefaultRelation;
        this.briefRow["F_SCHEMA_ID"] = (object) objectTypeProperties.SchemaID;
        objectType2 = this._collectionForCreate.Create(this.briefRow, parentID);
        objectType1 = this.session.GetObjectType(objectType2);
        if ((objectTypeProperties.Options & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType)
        {
          this.session.DataManager.DataProvider.CreateObjectsTypeAttrView((objectType1 as DBObjectType).AttributesTableName, this.session.DataManager);
          this.session.DataManager.DataProvider.CreateObjectsTypeAttrIndexes((objectType1 as DBObjectType).AttributesTableName, this.session.DataManager, (objectTypeProperties.Options & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex);
        }
        this._newObjectTypes.Add(objectType2);
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogObjectType, this.briefRow["F_OBJ_TYPE_NAME"]));
      }
      DataRow[] dataRowArray1 = this.metaData.Tables["IMS_ATTR4OBJ_TYPES"].Select("F_OBJECT_TYPE=" + this.briefRow["F_OBJECT_TYPE"]);
      if (dataRowArray1 != null && dataRowArray1.Length != 0)
      {
        if (flag)
        {
          foreach (DataRow row in (InternalDataCollectionBase) DataSetProcessor.FormDataTable(dataRowArray1).Rows)
          {
            if (Convert.ToInt32(row["F_PUBLIC"]) != 2)
            {
              IDBAttributeType attributeType = this.session.GetAttributeType(new Guid(this.metaData.Tables["IMS_ATTRIBUTES"].Rows.Find((object) Convert.ToInt32(row["F_ATTRIBUTE_ID"]))["F_GUID"].ToString()));
              row["F_ATTRIBUTE_ID"] = (object) attributeType.AttributeID;
              int int32_1 = Convert.ToInt32(row["F_MASTER_ID"]);
              if (int32_1 > 0)
                row["F_MASTER_ID"] = (object) Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], int32_1);
              int int32_2 = Convert.ToInt32(row["F_SOURCE_ID"]);
              if (int32_2 > 0)
                row["F_SOURCE_ID"] = (object) Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], int32_2);
              row["F_OBJECT_TYPE"] = (object) objectType2;
              row["F_LEVEL_ID"] = (object) Helper.GetConformityLCLevel(this.session, this.metaData.Tables["IMS_LEVELS"], Convert.ToInt32(row["F_LEVEL_ID"]));
              if (attributeType.AttributeType == FieldTypes.ftMeasured && CompareValuesHelper.NormalizedValue(row["F_DEFAULT_VALUE"]) != null)
              {
                this._measuredValueObjectLink.Add(new SaveImportValues(attributeType.AttributeID, objectType2, -1, (object) null, row["F_DEFAULT_VALUE"]));
                row["F_DEFAULT_VALUE"] = (object) DBNull.Value;
              }
              if (attributeType.AttributeType == FieldTypes.ftObjectLink && CompareValuesHelper.NormalizedValue(row["F_DEFAULT_VALUE"]) != null && Convert.ToString(row["F_DEFAULT_VALUE"]) != Consts.CurrentUserFunction)
              {
                this._defaultValueObjectLink.Add(new SaveImportValues(attributeType.AttributeID, objectType2, -1, row["F_DEFAULT_VALUE"]));
                row["F_DEFAULT_VALUE"] = (object) DBNull.Value;
              }
              if (this.AddFormula(attributeType.AttributeID, Convert.ToString(row["F_FORMULA"])))
                row["F_FORMULA"] = (object) string.Empty;
              DataTable dataTable = this.session.DataManager.ExecuteDataTable("SELECT F_ATTRIBUTE_ID FROM IMS_ATTR4OBJ_TYPES WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attr", this.session.DataManager.Parameter("objType", (object) objectType2), this.session.DataManager.Parameter("attr", (object) attributeType.AttributeID));
              if (dataTable.Rows != null && dataTable.Rows.Count > 0)
                this._collectionForCreate.UpdateAttribute(row);
              else
                this._collectionForCreate.CreateAttribute(row);
            }
          }
        }
        else
          this.RefreshAttributes(objectType1, dataRowArray1);
      }
      if (!this.CreateOnly)
      {
        byte[] numArray = (byte[]) null;
        if (this.briefRow["F_ICON"] != DBNull.Value)
          numArray = (byte[]) this.briefRow["F_ICON"];
        objectType1.Icon = numArray;
        if (captionAttribute != 0)
          this._objTypeStore.CaptionAttributes.Add(new Tuple<int, int>(objectType1.ObjectType, Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], captionAttribute)));
        if (this._formules != null)
          this._objTypeStore.AttributeFormules.Add(this._formules);
      }
      this._importedObjectTypes.Add(this.briefRow["F_OBJECT_TYPE"], (object) objectType2);
      return true;
    }
    catch (Exception ex)
    {
      this.ErrorException = new Exception(this.UniIdentifiler, ex);
      return false;
    }
  }

  private void RefreshAttributes(IDBObjectType objType, DataRow[] rows_all)
  {
    List<Tuple<int, int>> tupleList1 = new List<Tuple<int, int>>();
    List<Tuple<int, int>> tupleList2 = new List<Tuple<int, int>>();
    IDBAttribute4ObjectTypeCollection attributes = objType.Attributes as IDBAttribute4ObjectTypeCollection;
    foreach (DataRow row in rows_all)
    {
      object measuredDefaultVAlue = (object) null;
      Attribute4ObjectTypeProperties attrProperties = new Attribute4ObjectTypeProperties(row);
      attrProperties.AttributeID = Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], attrProperties.AttributeID);
      IDBAttributeType4Object attributeById = attributes.GetAttributeByID(attrProperties.AttributeID) as IDBAttributeType4Object;
      if (this.CreateOnly && attributeById != null)
      {
        this.AddToLog($"Cвойства атрибута \"{attributeById.Name}\" для типа объектов \"{objType.ObjectTypeName}\" не синхронизированы.");
      }
      else
      {
        IDBAttributeType attributeType = this.session.GetAttributeType(attrProperties.AttributeID, false);
        if (attributeType != null)
        {
          attrProperties.ObjectType = objType.ObjectType;
          attrProperties.LevelID = Helper.GetConformityLCLevel(this.session, this.metaData.Tables["IMS_LEVELS"], attrProperties.LevelID);
          if (attrProperties.MasterAttributeID > 0)
          {
            int conformityAttribureType = Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], attrProperties.MasterAttributeID);
            attrProperties.MasterAttributeID = attributeById != null ? attributeById.MasterAttributeID : 0;
            if (conformityAttribureType > 0)
              tupleList1.Add(new Tuple<int, int>(attrProperties.AttributeID, conformityAttribureType));
          }
          if (attrProperties.SourceAttributeID > 0)
          {
            int conformityAttribureType = Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], attrProperties.SourceAttributeID);
            if (conformityAttribureType > 0)
              tupleList2.Add(new Tuple<int, int>(attrProperties.AttributeID, conformityAttribureType));
            attrProperties.SourceAttributeID = attributeById != null ? attributeById.SourceAttributeID : 0;
          }
          if (attributeType.AttributeType == FieldTypes.ftMeasured && CompareValuesHelper.NormalizedValue(attrProperties.DefaultValue) != null)
          {
            measuredDefaultVAlue = attrProperties.DefaultValue;
            attrProperties.DefaultValue = attributeById != null ? attributeById.DefaultValue : (object) DBNull.Value;
          }
          if (attributeType.AttributeType == FieldTypes.ftObjectLink)
          {
            if (CompareValuesHelper.NormalizedValue(attrProperties.DefaultValue) == null)
              attrProperties.DefaultValue = (object) DBNull.Value;
            else if (attrProperties.DefaultValue.ToString() != Consts.CurrentUserFunction)
            {
              measuredDefaultVAlue = attrProperties.DefaultValue;
              attrProperties.DefaultValue = attributeById != null ? attributeById.DefaultValue : (object) DBNull.Value;
            }
          }
          if (attributeById != null)
          {
            if (attrProperties.Formula != attributeById.Formula)
            {
              this.AddFormula(attrProperties.AttributeID, attrProperties.Formula);
              attrProperties.Formula = string.Empty;
            }
            switch (attributeById.InheritMode)
            {
              case InheritModes.Private:
              case InheritModes.Public:
                if (attrProperties.InheritMode == InheritModes.Inherited)
                {
                  attributeById.Delete(0L);
                  break;
                }
                if (attrProperties.OptimizationMode != attributeById.OptimizationMode)
                {
                  this._attributesOptimizationMode.Add(new SaveImportValues(attrProperties.AttributeID, objType.ObjectType, -1, (object) attrProperties.OptimizationMode));
                  attrProperties.OptimizationMode = attributeById.OptimizationMode;
                }
                attributeById.Attribute4ObjectPropertiesStructure = attrProperties;
                break;
              case InheritModes.Inherited:
                if (attrProperties.InheritMode != InheritModes.Inherited)
                {
                  if (attrProperties.OptimizationMode != OptimizationModes.Write)
                  {
                    this._attributesOptimizationMode.Add(new SaveImportValues(attrProperties.AttributeID, objType.ObjectType, -1, (object) attrProperties.OptimizationMode));
                    attrProperties.OptimizationMode = attributeById.OptimizationMode;
                  }
                  (objType.Attributes as IDBAttribute4ObjectTypeCollection).Create(attrProperties);
                  break;
                }
                break;
            }
            this.AddToLog(string.Format(BriefcaseConsts.ImportLogAttr4ObjectTypeProperties, (object) attributeById.Name, (object) objType.ObjectTypeName));
          }
          else if (attrProperties.InheritMode != InheritModes.Inherited)
          {
            if (attrProperties.OptimizationMode != OptimizationModes.Write)
            {
              this._attributesOptimizationMode.Add(new SaveImportValues(attrProperties.AttributeID, objType.ObjectType, -1, (object) attrProperties.OptimizationMode));
              attrProperties.OptimizationMode = OptimizationModes.Write;
            }
            if (attrProperties.Formula != string.Empty)
            {
              this.AddFormula(attrProperties.AttributeID, attrProperties.Formula);
              attrProperties.Formula = string.Empty;
            }
            attributeById = attributes.Create(attrProperties);
            this.AddToLog(string.Format(BriefcaseConsts.ImportLogAttr4ObjectType, (object) this.session.GetAttributeType(attrProperties.AttributeID).Name, (object) objType.ObjectTypeName));
          }
          if (attrProperties.InheritMode != InheritModes.Inherited)
          {
            if (attributeById.AttributeType == FieldTypes.ftMeasured && CompareValuesHelper.NormalizedValue(measuredDefaultVAlue) != null)
              this._measuredValueObjectLink.Add(new SaveImportValues(attrProperties.AttributeID, objType.ObjectType, -1, (object) null, measuredDefaultVAlue));
            if (attributeType.AttributeType == FieldTypes.ftObjectLink && measuredDefaultVAlue != DBNull.Value && measuredDefaultVAlue != null && measuredDefaultVAlue.ToString() != string.Empty)
              this._defaultValueObjectLink.Add(new SaveImportValues(attrProperties.AttributeID, objType.ObjectType, -1, measuredDefaultVAlue));
          }
        }
      }
    }
    if (tupleList1.Count > 0)
    {
      foreach (Tuple<int, int> tuple in tupleList1)
        attributes.GetAttributeByID(tuple.Item1).MasterAttributeID = tuple.Item2;
    }
    if (tupleList2.Count <= 0)
      return;
    foreach (Tuple<int, int> tuple in tupleList2)
      attributes.GetAttributeByID(tuple.Item1).SourceAttributeID = tuple.Item2;
  }
}
