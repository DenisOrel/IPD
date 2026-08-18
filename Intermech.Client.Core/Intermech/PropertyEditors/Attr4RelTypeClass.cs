
// Type: Intermech.PropertyEditors.Attr4RelTypeClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Expressions;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Атрибуты на типы связей</summary>
public class Attr4RelTypeClass : Attr4TypeClass
{
  private PropDescriptor defaultPropDescriptor;
  private PropDescriptor defaultAsIntPropDescriptor;
  private PropDescriptor defaultAsIntListPropDescriptor;
  private PropDescriptor defaultAsDoublePropDescriptor;
  private PropDescriptor defaultAsDoubleListPropDescriptor;
  private PropDescriptor defaultAsStringPropDescriptor;
  private PropDescriptor defaultAsStringListPropDescriptor;
  private PropDescriptor defaultAsBooleanPropDescriptor;
  private PropDescriptor defaultAsDateTimePropDescriptor;
  private PropDescriptor defaultAsDateTimeListPropDescriptor;
  private PropDescriptor defaultAsGuidPropDescriptor;
  private PropDescriptor defaultAsGuidListPropDescriptor;
  private PropDescriptor defaultAsObjectPropDescriptor;
  private PropDescriptor defaultAsObjectListPropDescriptor;
  private PropDescriptor defaultAsObjectIDPropDescriptor;
  private PropDescriptor defaultAsObjectIDListPropDescriptor;
  private PropDescriptor defaultAsMeasuredPropDescriptor;
  private PropDescriptor validationRulePropDescriptor;
  private PropDescriptor computedPropDescriptor;
  private PropDescriptor formulaPropDescriptor;
  private PropDescriptor maskPropDescriptor;
  private PropDescriptor attrIdPropDescriptor;
  private PropDescriptor attrGuidPropDescriptor;
  private PropDescriptor attrNamePropDescriptor;
  private object formulaEditor;
  private object validationEditor;
  private bool validationRuleAsFormula;
  private PropDescriptor optionSaveInLogPropDescriptor;
  private PropDescriptor optionSavePrivateHistory;
  private PropDescriptor optionSaveCommonHistory;
  private PropDescriptor optionDisableNulls;
  private PropDescriptor optionGetDescriptionEvent;
  private PropDescriptor optionInternal;
  private PropDescriptor optionModifyInBase;
  private PropDescriptor optionDisableManualEdit;
  private PropDescriptor optionDontCopyPrototypeValue;
  private PropDescriptor optionDontCopyPrototypeValue4Article;
  private PropDescriptor optionDontCopyVersionValue;
  private PropDescriptor optionCopyValues2ChildObject;
  private PropDescriptor masterPropDescriptor;
  private PropDescriptor sourcePropDescriptor;
  private bool _BlockOnChange;
  private Attribute4RelationTypeProperties _Attribute4RelationTypeProperties;
  private DataTable _PossibleValuesDataTable;
  private AttributeTypeProperties _AttributeTypeProperties;

  public Attribute4RelationTypeProperties Attribute4RelationTypeProperties
  {
    get => this._Attribute4RelationTypeProperties;
    set => this._Attribute4RelationTypeProperties = value;
  }

  public override string AttributeName => this._AttributeTypeProperties.Name;

  public override int AttributeID => this._AttributeTypeProperties.AttributeID;

  public override string Formula => this._Attribute4RelationTypeProperties.Formula;

  public DataTable PossibleValuesDataTable => this._PossibleValuesDataTable;

  public AttributeTypeProperties AttributeTypeProperties => this._AttributeTypeProperties;

  private bool possibleValuesReadonly
  {
    get
    {
      return this._PossibleValuesDataTable == null || this._AttributeTypeProperties.MultiValueMode == MultiValueModes.MultiValues || this._AttributeTypeProperties.MultiValueMode == MultiValueModes.SingleValue;
    }
  }

  public Attr4RelTypeClass(
    Attribute4RelationTypeProperties aAttribute4RelationTypeProperties,
    AttributeTypeProperties atp,
    DataTable possibleValuesDataTable)
  {
    this._Attribute4RelationTypeProperties = aAttribute4RelationTypeProperties;
    this._AttributeTypeProperties = atp;
    this._PossibleValuesDataTable = possibleValuesDataTable;
  }

  public Attr4RelTypeClass(
    Attribute4RelationTypeProperties aAttribute4RelationTypeProperties,
    AttributeTypeProperties atp,
    DataTable possibleValuesDataTable,
    EventsHolder.GetListDelegate aGetMaster)
    : this(aAttribute4RelationTypeProperties, atp, possibleValuesDataTable)
  {
    this._getMasterList = aGetMaster;
  }

  public static Attr4RelTypeClass Clone(Attr4RelTypeClass src)
  {
    DataTable possibleValuesDataTable = src.PossibleValuesDataTable == null ? (DataTable) null : src.PossibleValuesDataTable.Clone();
    return new Attr4RelTypeClass(src.Attribute4RelationTypeProperties, new AttributeTypeProperties(src.AttributeTypeProperties), possibleValuesDataTable, src.GetMasterList);
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    this.validationRuleAsFormula = false;
    this.validationEditor = (object) null;
    TypeConverter converter = (TypeConverter) null;
    if (this._AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink || this._AttributeTypeProperties.FieldType == FieldTypes.ftObjectLinkByID)
      converter = (TypeConverter) new ValidationRuleConverter();
    else if (!AttributesTypeHelper.IsComplexAttributeType(this._AttributeTypeProperties.FieldType))
    {
      this.validationRuleAsFormula = true;
      converter = TypeDescriptor.GetConverter(typeof (string));
      this.validationEditor = (object) new AttributeFormulaUITypeEditor(this._AttributeTypeProperties.AttributeID, AttributableElements.Relation, this._Attribute4RelationTypeProperties.RelationType, true);
    }
    else if (this._AttributeTypeProperties.FieldType == FieldTypes.ftMeasured)
    {
      converter = TypeDescriptor.GetConverter(typeof (string));
      this.validationEditor = (object) new MeasuredCustomEditor(this._AttributeTypeProperties.AttributeID);
    }
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_56"), (object) null, typeof (RequiredModePropertyClass), (TypeConverter) new RequiredModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), LocalizationHolder.rm.GetString("Client.Core_56"), false, true, false));
    this.validationRulePropDescriptor = new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_1166"), (object) null, typeof (string), converter, this.validationEditor, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), LocalizationHolder.rm.GetString("Client.Core_1167"), false, true, false);
    pdc.Add((PropertyDescriptor) this.validationRulePropDescriptor);
    this.computedPropDescriptor = new PropDescriptor(2, (object) this, EnumTypeHelper.GetDescription(typeof (ComputeValueModes)), (object) null, typeof (ComputeValueModePropertyClass), (TypeConverter) new ComputeValueModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), EnumTypeHelper.GetDescription(typeof (ComputeValueModes)), false, true, false);
    pdc.Add((PropertyDescriptor) this.computedPropDescriptor);
    this.formulaEditor = (object) new AttributeFormulaUITypeEditor(this._AttributeTypeProperties.AttributeID, AttributableElements.Relation, this._Attribute4RelationTypeProperties.RelationType);
    this.formulaPropDescriptor = new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_41"), (object) null, typeof (string), TypeDescriptor.GetConverter(typeof (string)), this.formulaEditor, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), LocalizationHolder.rm.GetString("Client.Core_41"), false, true, false);
    pdc.Add((PropertyDescriptor) this.formulaPropDescriptor);
    string name = LocalizationHolder.rm.GetString("Client.Core_43");
    string description = name;
    string caption = VisualCategoriesHelper.GetCaption(VisualCategories.InputControl);
    this.defaultAsIntPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (long), (TypeConverter) new Int64CustomConverter(), (object) null, caption, description, false, true, false);
    this.defaultAsIntListPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (Int64PropertyClass), (TypeConverter) new IntTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new IntDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsDoublePropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (double), (TypeConverter) new DoubleCustomConverter(), (object) null, caption, description, false, true, false);
    this.defaultAsDoubleListPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (DoublePropertyClass), (TypeConverter) new DoubleTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new DoubleDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsStringPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (string), (TypeConverter) null, (object) null, caption, description, false, true, false);
    this.defaultAsStringListPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (StringPropertyClass), (TypeConverter) new StringTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new StringDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsBooleanPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (bool), (TypeConverter) new YesNoConverter(), (object) null, caption, description, false, true, false);
    this.defaultAsDateTimePropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (DateTime), (TypeConverter) new DateTimeNowConverter(), (object) new DateTimeNowEditor(), caption, description, false, true, false);
    this.defaultAsDateTimeListPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (DateTimePropertyClass), (TypeConverter) new DateTimeTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new DateTimeDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsGuidPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (Guid), (TypeConverter) new GuidCustomConverter(), (object) null, caption, description, false, true, false);
    this.defaultAsGuidListPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (GuidPropertyClass), (TypeConverter) new GuidTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new GuidDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsObjectPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectEditor(new EventsHolder.GetListDelegate(this.GetObjTypeList))
    {
      CurrentUserCustomProcessing = true
    }, caption, description, false, true, true);
    this.defaultAsObjectListPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectDropDownSelector(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsObjectIDPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectEditor(new EventsHolder.GetListDelegate(this.GetObjTypeList), false)
    {
      CurrentUserCustomProcessing = true
    }, caption, description, false, true, true);
    this.defaultAsObjectIDListPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectDropDownSelector(new EventsHolder.GetListDelegate(this.GetListByType), false), caption, description, false, true, true);
    this.defaultAsMeasuredPropDescriptor = new PropDescriptor(4, (object) this, name, (object) null, typeof (string), (TypeConverter) null, (object) new MeasureEditor(new EventsHolder.GetListDelegate(this.GetMeasureDescriptorList), new GetDefaultMeasureIDDelegate(this.GetDefaultMeasureID)), caption, description, false, true, false);
    this.defaultPropDescriptor = this.defaultAsStringPropDescriptor;
    pdc.Add((PropertyDescriptor) this.defaultPropDescriptor);
    pdc.Add((PropertyDescriptor) new FieldTypePropDescriptor(5, (object) this, EnumTypeHelper.GetDescription(typeof (FieldTypes)), (object) null, typeof (FieldTypePropertyClass), (TypeConverter) new FieldTypesConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), EnumTypeHelper.GetDescription(typeof (FieldTypes)), true, true, false));
    this.attrIdPropDescriptor = new PropDescriptor(6, (object) this, LocalizationHolder.rm.GetString("Client.Core_37"), (object) null, typeof (long), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Ident, true, true, false);
    pdc.Add((PropertyDescriptor) this.attrIdPropDescriptor);
    this.attrGuidPropDescriptor = new PropDescriptor(7, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_GUID, true, true, false);
    pdc.Add((PropertyDescriptor) this.attrGuidPropDescriptor);
    this.attrNamePropDescriptor = new PropDescriptor(8, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Name, true, true, false);
    pdc.Add((PropertyDescriptor) this.attrNamePropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(9, (object) this, EnumTypeHelper.GetDescription(typeof (OptimizationModes)), (object) null, typeof (OptimizationModePropertyClass), (TypeConverter) new OptimizationModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), EnumTypeHelper.GetDescription(typeof (OptimizationModes)), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(10, (object) this, LocalizationHolder.rm.GetString("Client.Core_44"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), LocalizationHolder.rm.GetString("Client.Core_44"), false, true, false));
    this.maskPropDescriptor = new PropDescriptor(11, (object) this, LocalizationHolder.rm.GetString("Client.Core_45"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), LocalizationHolder.rm.GetString("Client.Core_46"), false, true, false);
    pdc.Add((PropertyDescriptor) this.maskPropDescriptor);
    this.optionSaveInLogPropDescriptor = new PropDescriptor(12, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SaveInLog), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SaveInLogPropDescriptor, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSaveInLogPropDescriptor);
    this.optionSavePrivateHistory = new PropDescriptor(13, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SavePrivateHistory), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SavePrivateHistory, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSavePrivateHistory);
    this.optionSaveCommonHistory = new PropDescriptor(14, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SaveCommonHistory), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SaveCommonHistory, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSaveCommonHistory);
    this.optionDisableNulls = new PropDescriptor(15, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DisableNulls), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), AttributeOptionsHelper.GetCaption(AttributeOptions.DisableNulls), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableNulls);
    this.optionGetDescriptionEvent = new PropDescriptor(16 /*0x10*/, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.GetDescriptionEvent), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_GetDescriptionEvent, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionGetDescriptionEvent);
    this.optionInternal = new PropDescriptor(17, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.Internal), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), PropDescriptions.Attribute_Internal, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionInternal);
    this.optionModifyInBase = new PropDescriptor(18, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.ModifyInBase), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_ModifyInBase, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionModifyInBase);
    this.optionDisableManualEdit = new PropDescriptor(19, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DisableManualEdit), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DisableManualEdit, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableManualEdit);
    this.optionDontCopyPrototypeValue = new PropDescriptor(20, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyPrototypeValue), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DontCopyPrototypeValue, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyPrototypeValue);
    this.optionDontCopyVersionValue = new PropDescriptor(21, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyVersionValue), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DontCopyVersionValue, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyVersionValue);
    this.optionCopyValues2ChildObject = new PropDescriptor(22, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.CopyValues2ChildObject), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_CopyValues2ChildObject, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionCopyValues2ChildObject);
    this.masterPropDescriptor = new PropDescriptor(23, (object) this, LocalizationHolder.rm.GetString("Client.Core_47"), (object) null, typeof (AttributePropertyClass), (TypeConverter) new AttributeTypeConverter(new EventsHolder.GetListDelegate(this.GetMasterListProc)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.DataSources), LocalizationHolder.rm.GetString("Client.Core_47"), false, true, true);
    pdc.Add((PropertyDescriptor) this.masterPropDescriptor);
    this.sourcePropDescriptor = new PropDescriptor(24, (object) this, LocalizationHolder.rm.GetString("Client.Core_48"), (object) null, typeof (AttributePropertyClass), (TypeConverter) null, (object) new AttributeEditor(false, (FieldTypes[]) null, (int[]) null), VisualCategoriesHelper.GetCaption(VisualCategories.DataSources), LocalizationHolder.rm.GetString("Client.Core_48"), false, true, true);
    pdc.Add((PropertyDescriptor) this.sourcePropDescriptor);
    bool browsable = this.IsRelationTypeAllowableForObjectType(new Guid("cad00132-306c-11d8-b4e9-00304f19f545"));
    this.optionDontCopyPrototypeValue4Article = new PropDescriptor(25, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyPrototypeAttributeValueForArticle), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DontCopyPrototypeValue4Article, false, browsable, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyPrototypeValue4Article);
  }

  private bool IsRelationTypeAllowableForObjectType(Guid objtypeGuid)
  {
    List<int> applicabilityRelationTypesId = MetaDataHelper.GetApplicabilityRelationTypesID(objtypeGuid);
    // ISSUE: explicit non-virtual call
    return applicabilityRelationTypesId != null && __nonvirtual (applicabilityRelationTypesId.Contains(this.Attribute4RelationTypeProperties.RelationType));
  }

  private long GetDefaultMeasureID(object sender, params object[] args)
  {
    long defaultMeasureId = -1;
    if (this._AttributeTypeProperties.FieldType == FieldTypes.ftMeasured && this._AttributeTypeProperties.SizeType != -1L)
      defaultMeasureId = MeasureHelper.GetBaseMeasureID(this._AttributeTypeProperties.SizeType);
    return defaultMeasureId;
  }

  public void FillValues(PropertyGrid pg)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AttributeTypePropertiesValidator validatorForRelationType = sessionKeeper.Session.GetAttributeTypeCollection(-1, CoreConsts.FilterRecords).GetValidatorForRelationType(this._Attribute4RelationTypeProperties.AttributeID);
      this.defaultPropDescriptor.SetValue((object) this, (object) null);
      this.defaultPropDescriptor.SetReadOnly(validatorForRelationType.DefaultValue == null);
      if (validatorForRelationType.DefaultValue != null)
        this.AssignDefaultPropDescriptor(false, this.possibleValuesReadonly);
      this.formulaPropDescriptor.SetReadOnly(validatorForRelationType.Formula == null);
      this.maskPropDescriptor.SetReadOnly(validatorForRelationType.Mask == null);
      this.maskPropDescriptor.SetEditor(this.maskPropDescriptor.IsReadOnly || this._AttributeTypeProperties.FieldType != FieldTypes.ftDateTime ? (object) (DateTimeMaskEditor) null : (object) new DateTimeMaskEditor());
      this.PropDescriptorCollection[0].SetValue((object) this, (object) new RequiredModePropertyClass(this._Attribute4RelationTypeProperties.RequiredMode));
      this.PropDescriptorCollection[1].SetValue((object) this, (object) this._Attribute4RelationTypeProperties.ValidationRule);
      this.PropDescriptorCollection[2].SetValue((object) this, (object) new ComputeValueModePropertyClass(this._Attribute4RelationTypeProperties.ComputeValueMode));
      this.PropDescriptorCollection[3].SetValue((object) this, (object) this._Attribute4RelationTypeProperties.Formula);
      this.PropDescriptorCollection[5].SetValue((object) this, (object) new FieldTypePropertyClass(this._AttributeTypeProperties.FieldType));
      this.PropDescriptorCollection[6].SetValue((object) this, (object) this._AttributeTypeProperties.AttributeID);
      this.PropDescriptorCollection[7].SetValue((object) this, (object) this._AttributeTypeProperties.AttributeGuid);
      this.PropDescriptorCollection[8].SetValue((object) this, (object) this._AttributeTypeProperties.Name);
      this.PropDescriptorCollection[23].SetValue((object) this, this._Attribute4RelationTypeProperties.MasterAttributeID == 0 ? (object) (AttributePropertyClass) null : (object) new AttributePropertyClass(this._Attribute4RelationTypeProperties.MasterAttributeID));
      this.PropDescriptorCollection[24].SetValue((object) this, this._Attribute4RelationTypeProperties.SourceAttributeID == 0 ? (object) (AttributePropertyClass) null : (object) new AttributePropertyClass(this._Attribute4RelationTypeProperties.SourceAttributeID));
      AttributeEditor editor = (AttributeEditor) this.sourcePropDescriptor.GetEditor(typeof (AttributeEditor));
      if (editor != null)
      {
        editor.ExcludeAttributeId = new int[1]
        {
          this._Attribute4RelationTypeProperties.AttributeID
        };
        editor.FilterByTypes = new FieldTypes[1]
        {
          this._AttributeTypeProperties.FieldType
        };
      }
      if (!this.defaultPropDescriptor.IsReadOnly)
        this.SetDefaultPropDescriptorValue(this._Attribute4RelationTypeProperties.DefaultValue, this.possibleValuesReadonly);
      this.PropDescriptorCollection[9].SetValue((object) this, (object) new OptimizationModePropertyClass(this._Attribute4RelationTypeProperties.OptimizationMode));
      this.PropDescriptorCollection[10].SetValue((object) this, (object) new BoolPropertyClass(this._Attribute4RelationTypeProperties.IsContent));
      this.PropDescriptorCollection[11].SetValue((object) this, (object) this._Attribute4RelationTypeProperties.Mask);
      this.PropDescriptorCollection[12].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog));
      this.PropDescriptorCollection[13].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.SavePrivateHistory) == AttributeOptions.SavePrivateHistory));
      this.PropDescriptorCollection[14].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory));
      this.PropDescriptorCollection[15].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls));
      this.PropDescriptorCollection[16 /*0x10*/].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent));
      this.PropDescriptorCollection[17].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.Internal) == AttributeOptions.Internal));
      this.PropDescriptorCollection[18].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase));
      this.PropDescriptorCollection[19].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit));
      this.PropDescriptorCollection[20].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.DontCopyPrototypeValue));
      this.PropDescriptorCollection[21].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.DontCopyVersionValue) == AttributeOptions.DontCopyVersionValue));
      this.PropDescriptorCollection[22].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.CopyValues2ChildObject) == AttributeOptions.CopyValues2ChildObject));
      this.PropDescriptorCollection[25].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4RelationTypeProperties.Options & AttributeOptions.DontCopyPrototypeAttributeValueForArticle) == AttributeOptions.DontCopyPrototypeAttributeValueForArticle));
      this.CheckFormulaState();
      pg.Refresh();
    }
  }

  public void SaveValues()
  {
    this._Attribute4RelationTypeProperties.RequiredMode = ((RequiredModePropertyClass) this.PropDescriptorCollection[0].GetValue((object) this)).RequiredMode;
    this._Attribute4RelationTypeProperties.ValidationRule = this.PropDescriptorCollection[1].GetValue((object) this).ToString();
    this._Attribute4RelationTypeProperties.ComputeValueMode = ((ComputeValueModePropertyClass) this.PropDescriptorCollection[2].GetValue((object) this)).ComputeValueMode;
    this._Attribute4RelationTypeProperties.Formula = this.formulaPropDescriptor.IsReadOnly ? string.Empty : (this.PropDescriptorCollection[3].GetValue((object) this) != null ? this.PropDescriptorCollection[3].GetValue((object) this).ToString() : string.Empty);
    this._Attribute4RelationTypeProperties.DefaultValue = this.defaultPropDescriptor.IsReadOnly ? (object) null : this.GetDefaultPropDescriptorValue(this.possibleValuesReadonly);
    this._Attribute4RelationTypeProperties.OptimizationMode = ((OptimizationModePropertyClass) this.PropDescriptorCollection[9].GetValue((object) this)).OptimizationMode;
    this._Attribute4RelationTypeProperties.IsContent = ((BoolPropertyClass) this.PropDescriptorCollection[10].GetValue((object) this)).Boolean;
    this._Attribute4RelationTypeProperties.Mask = (string) this.PropDescriptorCollection[11].GetValue((object) this);
    int num = ((BoolPropertyClass) this.PropDescriptorCollection[12].GetValue((object) this)).Boolean ? 1 : 0;
    bool boolean1 = ((BoolPropertyClass) this.PropDescriptorCollection[13].GetValue((object) this)).Boolean;
    bool boolean2 = ((BoolPropertyClass) this.PropDescriptorCollection[14].GetValue((object) this)).Boolean;
    bool boolean3 = ((BoolPropertyClass) this.PropDescriptorCollection[15].GetValue((object) this)).Boolean;
    bool boolean4 = ((BoolPropertyClass) this.PropDescriptorCollection[16 /*0x10*/].GetValue((object) this)).Boolean;
    bool boolean5 = ((BoolPropertyClass) this.PropDescriptorCollection[17].GetValue((object) this)).Boolean;
    bool boolean6 = ((BoolPropertyClass) this.PropDescriptorCollection[18].GetValue((object) this)).Boolean;
    bool boolean7 = ((BoolPropertyClass) this.PropDescriptorCollection[19].GetValue((object) this)).Boolean;
    bool boolean8 = ((BoolPropertyClass) this.PropDescriptorCollection[20].GetValue((object) this)).Boolean;
    bool boolean9 = ((BoolPropertyClass) this.PropDescriptorCollection[21].GetValue((object) this)).Boolean;
    bool boolean10 = ((BoolPropertyClass) this.PropDescriptorCollection[25].GetValue((object) this)).Boolean;
    bool boolean11 = ((BoolPropertyClass) this.PropDescriptorCollection[22].GetValue((object) this)).Boolean;
    this._Attribute4RelationTypeProperties.Options = AttributeOptions.None;
    if (num != 0)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.SaveInLog;
    if (boolean1)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.SavePrivateHistory;
    if (boolean2)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.SaveCommonHistory;
    if (boolean3)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.DisableNulls;
    if (boolean4)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.GetDescriptionEvent;
    if (boolean5)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.Internal;
    if (boolean6)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.ModifyInBase;
    if (boolean7)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.DisableManualEdit;
    if (boolean8)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.DontCopyPrototypeValue;
    if (boolean9)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.DontCopyVersionValue;
    if (boolean10)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.DontCopyPrototypeAttributeValueForArticle;
    if (boolean11)
      this._Attribute4RelationTypeProperties.Options |= AttributeOptions.CopyValues2ChildObject;
    object obj1 = this.masterPropDescriptor.GetValue((object) this);
    this._Attribute4RelationTypeProperties.MasterAttributeID = obj1 == null ? 0 : ((AttributePropertyClass) obj1).Attribute;
    object obj2 = this.sourcePropDescriptor.GetValue((object) this);
    this._Attribute4RelationTypeProperties.SourceAttributeID = obj2 == null ? 0 : ((AttributePropertyClass) obj2).Attribute;
  }

  private void CheckFormulaState()
  {
    if (((ComputeValueModePropertyClass) this.computedPropDescriptor.GetValue((object) this)).ComputeValueMode == ComputeValueModes.NotComputableValue)
    {
      this.formulaPropDescriptor.SetReadOnly(true);
      this.formulaPropDescriptor.SetEditor((object) null);
    }
    else
    {
      this.formulaPropDescriptor.SetReadOnly(false);
      this.formulaPropDescriptor.SetEditor(this.formulaEditor);
    }
  }

  public bool ChangeEventProcessing(object s, PropertyValueChangedEventArgs e)
  {
    if (this._BlockOnChange)
      return false;
    this._BlockOnChange = true;
    try
    {
      if (((PropDescriptor) e.ChangedItem.PropertyDescriptor).PropID == 23 && (this.masterPropDescriptor.GetValue((object) this) == null || ((AttributePropertyClass) this.masterPropDescriptor.GetValue((object) this)).Attribute == 0))
      {
        this.sourcePropDescriptor.ResetValue((object) this);
        ((Control) s).Refresh();
      }
      if (((PropDescriptor) e.ChangedItem.PropertyDescriptor).PropID == 2)
      {
        this.CheckFormulaState();
        ((Control) s).Refresh();
      }
    }
    finally
    {
      this._BlockOnChange = false;
    }
    return true;
  }

  private AttributeTypePropertiesValidator GetValidator()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetAttributeTypeCollection(-1, CoreConsts.FilterRecords).GetValidatorForRelationType(this.Attribute4RelationTypeProperties.AttributeID);
  }

  public ArrayList GetList(object s, params object[] args)
  {
    ArrayList list = (ArrayList) null;
    AttributeTypePropertiesValidator validator = this.GetValidator();
    if (s is DropDownTypeConverter)
      list = ((DropDownTypeConverter) s).GetStandardValuesCustomList((ITypeDescriptorContext) null, (object) validator);
    return list;
  }

  private void AssignDefaultPropDescriptor(bool withSaveValues, bool possibleValuesReadonly)
  {
    object obj = (object) null;
    if (withSaveValues)
      obj = this.defaultPropDescriptor.GetValue((object) this);
    FieldTypes fieldType = this._AttributeTypeProperties.FieldType;
    this._pdc = PropDescriptorHolder.RemovePDCItem(this.PropDescriptorCollection, 4);
    try
    {
      switch (fieldType)
      {
        case FieldTypes.ftInteger:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsIntPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsIntListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftDouble:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsDoublePropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsDoubleListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftDateTime:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsDateTimePropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsDateTimeListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftObjectLink:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsObjectPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsObjectListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftBoolean:
          this.defaultPropDescriptor = this.defaultAsBooleanPropDescriptor;
          break;
        case FieldTypes.ftMeasured:
          this.defaultPropDescriptor = this.defaultAsMeasuredPropDescriptor;
          break;
        case FieldTypes.ftGuid:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsGuidPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsGuidListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftObjectLinkByID:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsObjectIDPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsObjectIDListPropDescriptor;
          obj = (object) null;
          break;
        default:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsStringPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsStringListPropDescriptor;
          obj = (object) null;
          break;
      }
    }
    finally
    {
      this.PropDescriptorCollection.Insert(4, (PropertyDescriptor) this.defaultPropDescriptor);
      if (withSaveValues)
        this.defaultPropDescriptor.SetValue((object) this, obj);
    }
  }

  private ArrayList GetObjTypeList(object s, params object[] values)
  {
    return ObjectEditor.GetObjTypeListByAttrId(this._AttributeTypeProperties.AttributeID);
  }

  private void SetDefaultPropDescriptorValue(object aDefaultValue, bool aPossibleValuesReadonly)
  {
    FieldTypes fieldType = this._AttributeTypeProperties.FieldType;
    switch (fieldType)
    {
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        object obj = (object) null;
        if (aDefaultValue != null && aDefaultValue.ToString() != string.Empty)
          obj = !(aDefaultValue.ToString() == Consts.CurrentUserFunction) ? (object) new ObjectPropertyClass(Convert.ToInt64(aDefaultValue), fieldType == FieldTypes.ftObjectLink) : (object) new ObjectPropertyClass(ObjectPropertyClassVariant.opcvCurrentUser, fieldType == FieldTypes.ftObjectLink);
        this.PropDescriptorCollection[4].SetValue((object) this, obj);
        break;
      default:
        if (aPossibleValuesReadonly)
        {
          this.PropDescriptorCollection[4].SetValue((object) this, AttributeValuesEditor.AttributeValueTransformationByCultureInfo(aDefaultValue, fieldType));
          break;
        }
        if (aDefaultValue != null && aDefaultValue.ToString() != string.Empty)
        {
          switch (fieldType)
          {
            case FieldTypes.ftString:
              this.PropDescriptorCollection[4].SetValue((object) this, (object) new StringPropertyClass(Convert.ToString(aDefaultValue), string.Empty, this._PossibleValuesDataTable));
              return;
            case FieldTypes.ftInteger:
              this.PropDescriptorCollection[4].SetValue((object) this, (object) new Int64PropertyClass(Convert.ToInt64(aDefaultValue), string.Empty, this._PossibleValuesDataTable));
              return;
            case FieldTypes.ftDouble:
              this.PropDescriptorCollection[4].SetValue((object) this, (object) new DoublePropertyClass(Convert.ToDouble(aDefaultValue), string.Empty, this._PossibleValuesDataTable));
              return;
            case FieldTypes.ftDateTime:
              this.PropDescriptorCollection[4].SetValue((object) this, (object) new DateTimePropertyClass(Convert.ToDateTime(aDefaultValue), string.Empty, this._PossibleValuesDataTable));
              return;
            case FieldTypes.ftGuid:
              this.PropDescriptorCollection[4].SetValue((object) this, (object) new GuidPropertyClass(new Guid(Convert.ToString(aDefaultValue)), string.Empty, this._PossibleValuesDataTable));
              return;
            default:
              this.PropDescriptorCollection[4].SetValue((object) this, aDefaultValue);
              return;
          }
        }
        else
        {
          this.PropDescriptorCollection[4].SetValue((object) this, aDefaultValue);
          break;
        }
    }
  }

  private object GetDefaultPropDescriptorValue(bool aPossibleValuesReadonly)
  {
    FieldTypes fieldType = this._AttributeTypeProperties.FieldType;
    object propDescriptorValue = this.PropDescriptorCollection[4].GetValue((object) this);
    if (fieldType == FieldTypes.ftObjectLink || fieldType == FieldTypes.ftObjectLinkByID)
    {
      if (propDescriptorValue != null)
        propDescriptorValue = ((ObjectPropertyClass) propDescriptorValue).ObjectPropertyClassVariant != ObjectPropertyClassVariant.opcvCurrentUser ? (object) ((ObjectPropertyClass) propDescriptorValue).ObjectID : (object) Consts.CurrentUserFunction;
    }
    else if (!aPossibleValuesReadonly && propDescriptorValue != null && propDescriptorValue.ToString() != string.Empty)
    {
      switch (fieldType)
      {
        case FieldTypes.ftString:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
        case FieldTypes.ftInteger:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
        case FieldTypes.ftDouble:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
        case FieldTypes.ftDateTime:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
        case FieldTypes.ftGuid:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
      }
    }
    if (propDescriptorValue is PropertyClass)
      propDescriptorValue = (object) null;
    return propDescriptorValue;
  }

  public ArrayList GetListByType(object s, params object[] args)
  {
    if (args.Length == 0)
      return (ArrayList) null;
    System.Type type = args[0] as System.Type;
    if (type == (System.Type) null || this._PossibleValuesDataTable == null)
      return (ArrayList) null;
    ArrayList listByType = new ArrayList();
    foreach (DataRow row in (InternalDataCollectionBase) this._PossibleValuesDataTable.Rows)
    {
      object obj = (object) null;
      try
      {
        if (type == typeof (long))
          obj = (object) new Int64PropertyClass(Convert.ToInt64(row["F_INTEGER_VALUE"]), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null);
        if (type == typeof (double))
          obj = (object) new DoublePropertyClass(Convert.ToDouble(row["F_DOUBLE_VALUE"]), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null);
        if (type == typeof (string))
          obj = (object) new StringPropertyClass(row["F_STRING_VALUE"].ToString(), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null);
        if (type == typeof (DateTime))
          obj = (object) new DateTimePropertyClass(Convert.ToDateTime(row["F_DATE_VALUE"]), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null);
        if (type == typeof (Guid))
          obj = (object) new GuidPropertyClass(new Guid(Convert.ToString(row["F_STRING_VALUE"])), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null);
        if (type == typeof (ObjectPropertyClass))
          obj = (object) Convert.ToInt64(row["F_INTEGER_VALUE"]);
      }
      catch
      {
      }
      if (obj != null)
        listByType.Add(obj);
    }
    return listByType;
  }

  private ArrayList GetMeasureDescriptorList(object s, params object[] args)
  {
    ArrayList measureDescriptorList = (ArrayList) null;
    if (this._AttributeTypeProperties.FieldType == FieldTypes.ftMeasured)
      measureDescriptorList = MeasureEditor.GetMeasureDescriptorListByAttributeId(this._AttributeTypeProperties.AttributeID);
    return measureDescriptorList;
  }

  private ArrayList GetMasterListProc(object s, params object[] args)
  {
    return this._getMasterList != null ? this._getMasterList(s, args) : (ArrayList) null;
  }
}
