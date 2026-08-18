// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportRelationType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportRelationType : ImportItem
{
  private List<SaveImportValues> _defaultValueObjectLink;
  private List<SaveImportValues> _measuredValueObjectLink;
  private List<SaveImportValues> _attributesOptimizationMode;

  public ImportRelationType(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    List<SaveImportValues> defaultValueObjectLink,
    List<SaveImportValues> measuredValueObjectLink,
    List<SaveImportValues> attributesOptimizationMode,
    ImportItemOptions options)
    : base(userSession, briefRow, metaData, options)
  {
    this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_315"), briefRow["F_DESCRIPTION"]);
    this._defaultValueObjectLink = defaultValueObjectLink;
    this._measuredValueObjectLink = measuredValueObjectLink;
    this._attributesOptimizationMode = attributesOptimizationMode;
  }

  public override bool Import()
  {
    try
    {
      RelationTypeProperties relationProperties = new RelationTypeProperties(this.briefRow)
      {
        AreaID = Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, this.briefRow["F_AREA_ID"].ToString())
      };
      IDBRelationType relationType1 = this.session.GetRelationType(new Guid(this.briefRow["F_GUID"].ToString()), false);
      bool flag = false;
      int relationType2;
      if (relationType1 != null)
      {
        relationType2 = relationType1.RelationType;
        relationProperties.RelationType = relationType1.RelationType;
        if (this.LangEquals && !this.CreateOnly)
        {
          relationType1.PropertiesStructure = relationProperties;
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogRelationTypeProperties, (object) relationType1.Description));
        }
        else
          this.AddToLog(string.Format(BriefcaseConsts.ImportLogRelationTypeNotSynhronized, (object) relationType1.Description));
      }
      else
      {
        relationType2 = this.session.GetRelationTypeCollection().Create(relationProperties);
        relationType1 = this.session.GetRelationType(relationType2);
        this.AddToLog(string.Format(BriefcaseConsts.ImportLogRelationType, (object) this.briefRow["F_DESCRIPTION"].ToString()));
        flag = true;
      }
      if (flag || !this.CreateOnly)
      {
        byte[] numArray = (byte[]) null;
        if (this.briefRow["F_ICON"] != DBNull.Value)
          numArray = (byte[]) this.briefRow["F_ICON"];
        relationType1.Icon = numArray;
      }
      List<Tuple<int, int>> tupleList1 = new List<Tuple<int, int>>();
      List<Tuple<int, int>> tupleList2 = new List<Tuple<int, int>>();
      List<Tuple<int, string>> tupleList3 = new List<Tuple<int, string>>();
      IDBAttribute4RelationTypeCollection attributes1 = relationType1.Attributes as IDBAttribute4RelationTypeCollection;
      foreach (DataRow row in this.metaData.Tables["IMS_ATTR4RELATION_TYPES"].Select("F_RELATION_TYPE=" + this.briefRow["F_RELATION_TYPE"]))
      {
        object measuredDefaultVAlue = (object) null;
        Attribute4RelationTypeProperties attrProperties = new Attribute4RelationTypeProperties(row);
        attrProperties.AttributeID = Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], attrProperties.AttributeID);
        IDBAttributeType4Relation attributeById = attributes1.GetAttributeByID(attrProperties.AttributeID) as IDBAttributeType4Relation;
        if (this.CreateOnly && attributeById != null)
        {
          this.AddToLog($"Cвойства атрибута \"{attributeById.Name}\" для типа связей \"{relationType1.Description}\" не синхронизированы.");
        }
        else
        {
          IDBAttributeType attributeType = this.session.GetAttributeType(attrProperties.AttributeID, false);
          if (attributeType != null)
          {
            attrProperties.RelationType = relationType2;
            if (attrProperties.MasterAttributeID > 0)
            {
              int conformityAttribureType = Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], attrProperties.MasterAttributeID);
              tupleList1.Add(new Tuple<int, int>(attrProperties.AttributeID, conformityAttribureType));
              attrProperties.MasterAttributeID = attributeById != null ? attributeById.MasterAttributeID : 0;
            }
            if (attrProperties.SourceAttributeID > 0)
            {
              int conformityAttribureType = Helper.GetConformityAttribureType(this.session, this.metaData.Tables["IMS_ATTRIBUTES"], attrProperties.SourceAttributeID);
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
              if (attrProperties.OptimizationMode != attributeById.OptimizationMode)
              {
                this._attributesOptimizationMode.Add(new SaveImportValues(attrProperties.AttributeID, -1, relationType2, (object) attrProperties.OptimizationMode));
                attrProperties.OptimizationMode = attributeById.OptimizationMode;
              }
              if (attrProperties.Formula != attributeById.Formula)
              {
                tupleList3.Add(new Tuple<int, string>(attrProperties.AttributeID, attrProperties.Formula));
                attrProperties.Formula = string.Empty;
              }
              attributeById.Attribute4RelationPropertiesStructure = attrProperties;
              this.AddToLog(string.Format(BriefcaseConsts.ImportLogAttr4RelationTypeProperties, (object) attributeById.Name, (object) relationType1.Description));
            }
            else
            {
              if (attrProperties.OptimizationMode != OptimizationModes.Write)
              {
                this._attributesOptimizationMode.Add(new SaveImportValues(attrProperties.AttributeID, -1, relationType2, (object) attrProperties.OptimizationMode));
                attrProperties.OptimizationMode = OptimizationModes.Write;
              }
              if (attrProperties.Formula != string.Empty)
              {
                tupleList3.Add(new Tuple<int, string>(attrProperties.AttributeID, attrProperties.Formula));
                attrProperties.Formula = string.Empty;
              }
              attributeById = attributes1.Create(attrProperties);
              this.AddToLog(string.Format(BriefcaseConsts.ImportLogAttr4RelationType, (object) this.session.GetAttributeType(attrProperties.AttributeID).Name, (object) relationType1.Description));
            }
            if (attributeById.AttributeType == FieldTypes.ftMeasured && CompareValuesHelper.NormalizedValue(measuredDefaultVAlue) != null)
              this._measuredValueObjectLink.Add(new SaveImportValues(attrProperties.AttributeID, -1, relationType2, (object) null, measuredDefaultVAlue));
            if (attributeType.AttributeType == FieldTypes.ftObjectLink && CompareValuesHelper.NormalizedValue(measuredDefaultVAlue) != null)
              this._defaultValueObjectLink.Add(new SaveImportValues(attrProperties.AttributeID, -1, relationType2, measuredDefaultVAlue));
          }
        }
      }
      IDBAttribute4RelationTypeCollection attributes2 = relationType1.Attributes as IDBAttribute4RelationTypeCollection;
      if (tupleList1.Count > 0)
      {
        foreach (Tuple<int, int> tuple in tupleList1)
          attributes2.GetAttributeByID(tuple.Item1).MasterAttributeID = tuple.Item2;
      }
      if (tupleList2.Count > 0)
      {
        foreach (Tuple<int, int> tuple in tupleList2)
          attributes2.GetAttributeByID(tuple.Item1).SourceAttributeID = tuple.Item2;
      }
      if (tupleList3.Count > 0)
      {
        foreach (Tuple<int, string> tuple in tupleList3)
          attributes2.GetAttributeByID(tuple.Item1).Formula = tuple.Item2;
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
