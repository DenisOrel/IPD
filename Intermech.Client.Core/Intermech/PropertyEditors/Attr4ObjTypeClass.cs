
// Type: Intermech.PropertyEditors.Attr4ObjTypeClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DatabaseConfigurator;
using Intermech.Expressions;
using Intermech.Extensions;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Атрибуты на типы объектов</summary>
public class Attr4ObjTypeClass : Attr4TypeClass
{
  private PropDescriptor publicPropDescriptor;
  private PropDescriptor validationRulePropDescriptor;
  private PropDescriptor computedPropDescriptor;
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
  private PropDescriptor formulaPropDescriptor;
  private PropDescriptor maskPropDescriptor;
  private PropDescriptor attrTypePropDescriptor;
  private PropDescriptor attrIdPropDescriptor;
  private PropDescriptor attrGuidPropDescriptor;
  private PropDescriptor attrNamePropDescriptor;
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
  private PropDescriptor optionAddToGlobalIndex;
  private PropDescriptor optionDisableSplitIndexValue;
  private PropDescriptor optionDontCopyVersionValue;
  private PropDescriptor optionCopyValues2ChildObject;
  private PropDescriptor masterPropDescriptor;
  private PropDescriptor sourcePropDescriptor;
  private object measureEditor;
  private object formulaEditor;
  private object sourceEditor;
  private object validationEditor;
  private bool validationRuleAsFormula;
  /// <summary>Возможные значения аттрибута</summary>
  private DataTable _PossibleValuesDataTable;
  /// <summary>Описание аттрибута</summary>
  private AttributeTypeProperties _AttributeTypeProperties;
  /// <summary>Аттрибут в контексте типа объекта</summary>
  private Attribute4ObjectTypeProperties _Attribute4ObjectTypeProperties;
  /// <summary>
  /// 
  /// </summary>
  private bool _BlockOnChange;

  /// <summary>Конструктор</summary>
  /// <param name="aAttribute4ObjectTypeProperties"></param>
  /// <param name="atp"></param>
  /// <param name="possibleValuesDataTable"></param>
  public Attr4ObjTypeClass(
    Attribute4ObjectTypeProperties aAttribute4ObjectTypeProperties,
    AttributeTypeProperties atp,
    DataTable possibleValuesDataTable)
  {
    this._Attribute4ObjectTypeProperties = aAttribute4ObjectTypeProperties;
    this._AttributeTypeProperties = atp;
    this._PossibleValuesDataTable = possibleValuesDataTable;
  }

  /// <summary>Конструктор</summary>
  /// <param name="aAttribute4ObjectTypeProperties"></param>
  /// <param name="atp"></param>
  /// <param name="possibleValuesDataTable"></param>
  /// <param name="aGetMaster"></param>
  public Attr4ObjTypeClass(
    Attribute4ObjectTypeProperties aAttribute4ObjectTypeProperties,
    AttributeTypeProperties atp,
    DataTable possibleValuesDataTable,
    EventsHolder.GetListDelegate aGetMaster)
    : this(aAttribute4ObjectTypeProperties, atp, possibleValuesDataTable)
  {
    this._getMasterList = aGetMaster;
  }

  /// <summary>Cоздание кастом свойств (описаний)</summary>
  /// <param name="pdc"></param>
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
      this.validationEditor = (object) new AttributeFormulaUITypeEditor(this._AttributeTypeProperties.AttributeID, AttributableElements.Object, this._Attribute4ObjectTypeProperties.ObjectType, true);
    }
    else if (this._AttributeTypeProperties.FieldType == FieldTypes.ftMeasured)
    {
      converter = TypeDescriptor.GetConverter(typeof (string));
      this.validationEditor = (object) new MeasuredCustomEditor(this._AttributeTypeProperties.AttributeID);
    }
    this.publicPropDescriptor = new PropDescriptor(0, (object) this, EnumTypeHelper.GetDescription(typeof (InheritModes)), (object) null, typeof (InheritModePropertyClass), (TypeConverter) new InheritModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), EnumTypeHelper.GetDescription(typeof (InheritModes)), false, true, false);
    pdc.Add((PropertyDescriptor) this.publicPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_40"), (object) null, typeof (RequiredModePropertyClass), (TypeConverter) new RequiredModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), LocalizationHolder.rm.GetString("Client.Core_40"), false, true, false));
    this.validationRulePropDescriptor = new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_1166"), (object) null, typeof (string), converter, this.validationEditor, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), LocalizationHolder.rm.GetString("Client.Core_1167"), false, true, false);
    pdc.Add((PropertyDescriptor) this.validationRulePropDescriptor);
    this.computedPropDescriptor = new PropDescriptor(3, (object) this, EnumTypeHelper.GetDescription(typeof (ComputeValueModes)), (object) null, typeof (ComputeValueModePropertyClass), (TypeConverter) new ComputeValueModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), EnumTypeHelper.GetDescription(typeof (ComputeValueModes)), false, true, false);
    pdc.Add((PropertyDescriptor) this.computedPropDescriptor);
    this.formulaEditor = (object) new AttributeFormulaUITypeEditor(this._AttributeTypeProperties.AttributeID, AttributableElements.Object, this._Attribute4ObjectTypeProperties.ObjectType);
    this.formulaPropDescriptor = new PropDescriptor(4, (object) this, LocalizationHolder.rm.GetString("Client.Core_41"), (object) null, typeof (string), TypeDescriptor.GetConverter(typeof (string)), this.formulaEditor, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), LocalizationHolder.rm.GetString("Client.Core_41"), false, true, false);
    pdc.Add((PropertyDescriptor) this.formulaPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(5, (object) this, EnumTypeHelper.GetDescription(typeof (UniqueValueModes)), (object) null, typeof (UniqueValueModePropertyClass), (TypeConverter) new UniqueValueModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), EnumTypeHelper.GetDescription(typeof (UniqueValueModes)), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(6, (object) this, LocalizationHolder.rm.GetString("Client.Core_42"), (object) null, typeof (LevelPropertyClass), (TypeConverter) new LevelConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), LocalizationHolder.rm.GetString("Client.Core_42"), false, true, false));
    string name = LocalizationHolder.rm.GetString("Client.Core_43");
    string description = name;
    string caption = VisualCategoriesHelper.GetCaption(VisualCategories.InputControl);
    this.defaultAsIntPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (long), (TypeConverter) new Int64CustomConverter(), (object) null, caption, description, false, true, false);
    this.defaultAsIntListPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (Int64PropertyClass), (TypeConverter) new IntTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new IntDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsDoublePropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (double), (TypeConverter) new DoubleCustomConverter(), (object) null, caption, description, false, true, false);
    this.defaultAsDoubleListPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (DoublePropertyClass), (TypeConverter) new DoubleTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new DoubleDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsStringPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (string), (TypeConverter) null, (object) null, caption, description, false, true, false);
    this.defaultAsStringListPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (StringPropertyClass), (TypeConverter) new StringTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new StringDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsBooleanPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (bool), (TypeConverter) new YesNoConverter(), (object) null, caption, description, false, true, false);
    this.defaultAsDateTimePropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (DateTime), (TypeConverter) new DateTimeNowConverter(), (object) new DateTimeNowEditor(), caption, description, false, true, false);
    this.defaultAsDateTimeListPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (DateTimePropertyClass), (TypeConverter) new DateTimeTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new DateTimeDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsGuidPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (Guid), (TypeConverter) new GuidCustomConverter(), (object) null, caption, description, false, true, false);
    this.defaultAsGuidListPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (GuidPropertyClass), (TypeConverter) new GuidTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new GuidDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsObjectPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectEditor(new EventsHolder.GetListDelegate(this.GetObjTypeList))
    {
      CurrentUserCustomProcessing = true
    }, caption, description, false, true, true);
    this.defaultAsObjectListPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectDropDownSelector(new EventsHolder.GetListDelegate(this.GetListByType)), caption, description, false, true, true);
    this.defaultAsObjectIDPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectEditor(new EventsHolder.GetListDelegate(this.GetObjTypeList), false)
    {
      CurrentUserCustomProcessing = true
    }, caption, description, false, true, true);
    this.defaultAsObjectIDListPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectDropDownSelector(new EventsHolder.GetListDelegate(this.GetListByType), false), caption, description, false, true, true);
    this.measureEditor = (object) new MeasureEditor(new EventsHolder.GetListDelegate(this.GetMeasureDescriptorList), new GetDefaultMeasureIDDelegate(this.GetDefaultMeasureID));
    this.defaultAsMeasuredPropDescriptor = new PropDescriptor(7, (object) this, name, (object) null, typeof (string), (TypeConverter) null, this.measureEditor, caption, description, false, true, false);
    this.defaultPropDescriptor = this.defaultAsStringPropDescriptor;
    pdc.Add((PropertyDescriptor) this.defaultPropDescriptor);
    this.attrTypePropDescriptor = (PropDescriptor) new FieldTypePropDescriptor(8, (object) this, EnumTypeHelper.GetDescription(typeof (FieldTypes)), (object) null, typeof (FieldTypePropertyClass), (TypeConverter) new FieldTypesConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), EnumTypeHelper.GetDescription(typeof (FieldTypes)), true, true, false);
    pdc.Add((PropertyDescriptor) this.attrTypePropDescriptor);
    this.attrIdPropDescriptor = new PropDescriptor(9, (object) this, LocalizationHolder.rm.GetString("Client.Core_37"), (object) null, typeof (long), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Ident, true, true, false);
    pdc.Add((PropertyDescriptor) this.attrIdPropDescriptor);
    this.attrGuidPropDescriptor = new PropDescriptor(10, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_GUID, true, true, false);
    pdc.Add((PropertyDescriptor) this.attrGuidPropDescriptor);
    this.attrNamePropDescriptor = new PropDescriptor(11, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Name, true, true, false);
    pdc.Add((PropertyDescriptor) this.attrNamePropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(12, (object) this, EnumTypeHelper.GetDescription(typeof (OptimizationModes)), (object) null, typeof (OptimizationModePropertyClass), (TypeConverter) new OptimizationModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), EnumTypeHelper.GetDescription(typeof (OptimizationModes)), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(13, (object) this, LocalizationHolder.rm.GetString("Client.Core_44"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), LocalizationHolder.rm.GetString("Client.Core_44"), false, true, false));
    this.maskPropDescriptor = new PropDescriptor(14, (object) this, LocalizationHolder.rm.GetString("Client.Core_45"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), LocalizationHolder.rm.GetString("Client.Core_46"), false, true, false);
    pdc.Add((PropertyDescriptor) this.maskPropDescriptor);
    this.optionSaveInLogPropDescriptor = new PropDescriptor(15, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SaveInLog), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SaveInLogPropDescriptor, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSaveInLogPropDescriptor);
    this.optionSavePrivateHistory = new PropDescriptor(16 /*0x10*/, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SavePrivateHistory), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SavePrivateHistory, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSavePrivateHistory);
    this.optionSaveCommonHistory = new PropDescriptor(17, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SaveCommonHistory), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SaveCommonHistory, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSaveCommonHistory);
    this.optionDisableNulls = new PropDescriptor(18, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DisableNulls), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), AttributeOptionsHelper.GetCaption(AttributeOptions.DisableNulls), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableNulls);
    this.optionGetDescriptionEvent = new PropDescriptor(19, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.GetDescriptionEvent), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_GetDescriptionEvent, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionGetDescriptionEvent);
    this.optionInternal = new PropDescriptor(20, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.Internal), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), PropDescriptions.Attribute_Internal, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionInternal);
    this.optionModifyInBase = new PropDescriptor(21, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.ModifyInBase), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_ModifyInBase, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionModifyInBase);
    this.optionDisableManualEdit = new PropDescriptor(22, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DisableManualEdit), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DisableManualEdit, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableManualEdit);
    this.optionDontCopyPrototypeValue = new PropDescriptor(23, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyPrototypeValue), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DontCopyPrototypeValue, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyPrototypeValue);
    this.optionAddToGlobalIndex = new PropDescriptor(24, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.AddToGlobalIndex), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_AddToGlobalIndex, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionAddToGlobalIndex);
    this.optionDisableSplitIndexValue = new PropDescriptor(25, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DisableSplitIndexValue), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_DisableSplitIndexValue, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableSplitIndexValue);
    this.optionDontCopyVersionValue = new PropDescriptor(26, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyVersionValue), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_DontCopyVersionValue, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyVersionValue);
    this.optionCopyValues2ChildObject = new PropDescriptor(27, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.CopyValues2ChildObject), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_CopyValues2ChildObject, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionCopyValues2ChildObject);
    this.masterPropDescriptor = new PropDescriptor(28, (object) this, LocalizationHolder.rm.GetString("Client.Core_47"), (object) null, typeof (AttributePropertyClass), (TypeConverter) new AttributeTypeConverter(new EventsHolder.GetListDelegate(this.GetMasterListProc)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.DataSources), LocalizationHolder.rm.GetString("Client.Core_47"), true, true, true);
    pdc.Add((PropertyDescriptor) this.masterPropDescriptor);
    this.sourceEditor = (object) new AttributeEditor(false, (FieldTypes[]) null, (int[]) null);
    this.sourcePropDescriptor = new PropDescriptor(29, (object) this, LocalizationHolder.rm.GetString("Client.Core_48"), (object) null, typeof (AttributePropertyClass), (TypeConverter) null, this.sourceEditor, VisualCategoriesHelper.GetCaption(VisualCategories.DataSources), LocalizationHolder.rm.GetString("Client.Core_48"), true, true, true);
    pdc.Add((PropertyDescriptor) this.sourcePropDescriptor);
    bool browsable = Attr4ObjTypeClass.IsAttributeDefinedForType(this.AttributeID, new Guid("cad00132-306c-11d8-b4e9-00304f19f545"));
    this.optionDontCopyPrototypeValue4Article = new PropDescriptor(30, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyPrototypeAttributeValueForArticle), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DontCopyPrototypeValue4Article, false, browsable, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyPrototypeValue4Article);
  }

  /// <summary>
  /// Проверить, определен ли атрибут для данного типа или хотя бы одного из его подтипов
  /// </summary>
  internal static bool IsAttributeDefinedForType(int attributeID, Guid objtypeGuid)
  {
    int id = MetaDataHelper.GetObjectTypeID(objtypeGuid);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(id, CoreConsts.FilterRecords);
      if (objectTypeCollection != null)
        return objectTypeCollection.GetUsedByAttribute(attributeID).Rows.Any((System.Func<DataRow, bool>) (r => Convert.ToInt32(r[0]) == id));
    }
    return false;
  }

  /// <summary>Категория, которую обрабатывает Holder</summary>
  public override int Category => 22;

  /// <summary>Идентификатор в рамках категории</summary>
  public override object Id => (object) this.Attribute4ObjectTypeProperties;

  /// <summary>Назначение идентификатора в рамках категории</summary>
  /// <param name="aId"></param>
  public override void SetId(object aId) => base.SetId(aId);

  /// <summary>Аттрибут в контексте типа объекта</summary>
  public Attribute4ObjectTypeProperties Attribute4ObjectTypeProperties
  {
    [DebuggerStepThrough] get => this._Attribute4ObjectTypeProperties;
    [DebuggerStepThrough] set => this._Attribute4ObjectTypeProperties = value;
  }

  /// <summary>Наименование типа аттрибута</summary>
  public override string AttributeName
  {
    [DebuggerStepThrough] get => this._AttributeTypeProperties.Name;
  }

  /// <summary>Ид. типа аттрибутов</summary>
  public override int AttributeID
  {
    [DebuggerStepThrough] get => this._AttributeTypeProperties.AttributeID;
  }

  /// <summary>Формула вычисления значения аттрибута</summary>
  public override string Formula
  {
    [DebuggerStepThrough] get => this._Attribute4ObjectTypeProperties.Formula;
  }

  /// <summary>Возможные значения аттрибута</summary>
  public DataTable PossibleValuesDataTable
  {
    [DebuggerStepThrough] get => this._PossibleValuesDataTable;
  }

  /// <summary>Описания аттрибута</summary>
  public AttributeTypeProperties AttributeTypeProperties
  {
    [DebuggerStepThrough] get => this._AttributeTypeProperties;
  }

  /// <summary>
  /// 
  /// </summary>
  public bool PossibleValuesReadOnly
  {
    get
    {
      return this._PossibleValuesDataTable == null || this._AttributeTypeProperties.MultiValueMode == MultiValueModes.MultiValues || this._AttributeTypeProperties.MultiValueMode == MultiValueModes.SingleValue;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="s"></param>
  /// <param name="e"></param>
  /// <returns></returns>
  public bool ChangeEventProcessing(object s, PropertyValueChangedEventArgs e)
  {
    if (this._BlockOnChange)
      return false;
    this._BlockOnChange = true;
    try
    {
      bool flag = false;
      switch (((PropDescriptor) e.ChangedItem.PropertyDescriptor).PropID)
      {
        case 0:
          if (((InheritModePropertyClass) e.ChangedItem.Value).InheritMode == InheritModes.Inherited)
          {
            this.PropDescriptorCollection[0].SetValue((object) this, e.OldValue);
            string Message = LocalizationHolder.rm.GetString("Client.Core_49");
            int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_50"), Message, MessageBoxButtons.OK);
            return false;
          }
          this.ProcessReadOnly(this.GetValidator());
          this.ProcessPublicProp();
          flag = true;
          break;
        case 3:
          this.CheckFormulaState();
          flag = true;
          break;
        case 24:
          if (!this.optionAddToGlobalIndex.IsReadOnly)
          {
            BoolPropertyClass boolPropertyClass = (BoolPropertyClass) this.optionAddToGlobalIndex.GetValue((object) this);
            if (boolPropertyClass != null && !boolPropertyClass.Boolean)
            {
              this.optionDisableSplitIndexValue.SetValue((object) this, (object) new BoolPropertyClass(false));
              this.optionDisableSplitIndexValue.SetReadOnly(true);
            }
            else
              this.optionDisableSplitIndexValue.SetReadOnly(false);
            flag = true;
            break;
          }
          break;
        case 28:
          if (this.masterPropDescriptor.GetValue((object) this) == null || ((AttributePropertyClass) this.masterPropDescriptor.GetValue((object) this)).Attribute == 0)
          {
            this.sourcePropDescriptor.ResetValue((object) this);
            flag = true;
            break;
          }
          break;
      }
      this.ChangeEventDataToRegisteredPropertyDescriptors((EventArgs) e);
      if (flag)
      {
        if (s is PropertyGrid)
          ((Control) s).Refresh();
      }
    }
    finally
    {
      this._BlockOnChange = false;
    }
    return true;
  }

  /// <summary>Создание клона объекта</summary>
  /// <param name="src"></param>
  /// <returns></returns>
  public static Attr4ObjTypeClass Clone(Attr4ObjTypeClass src)
  {
    DataTable possibleValuesDataTable = src.PossibleValuesDataTable == null ? (DataTable) null : src.PossibleValuesDataTable.Clone();
    return new Attr4ObjTypeClass(src.Attribute4ObjectTypeProperties, new AttributeTypeProperties(src.AttributeTypeProperties), possibleValuesDataTable, src.GetMasterList);
  }

  /// <summary>Заполнение PropertyGrid</summary>
  /// <param name="pg"></param>
  public void FillValues(PropertyGrid pg)
  {
    AttributeTypePropertiesValidator validator = this.GetValidator();
    if (validator.DefaultValue != null)
      this.AssignDefaultPropDescriptor(false, this.PossibleValuesReadOnly);
    this.defaultPropDescriptor.SetValue((object) this, (object) null);
    this.ProcessReadOnly(validator);
    this.PropDescriptorCollection[0].SetValue((object) this, (object) new InheritModePropertyClass(this._Attribute4ObjectTypeProperties.InheritMode));
    this.PropDescriptorCollection[1].SetValue((object) this, (object) new RequiredModePropertyClass(this._Attribute4ObjectTypeProperties.RequiredMode));
    this.PropDescriptorCollection[2].SetValue((object) this, (object) this._Attribute4ObjectTypeProperties.ValidationRule);
    this.PropDescriptorCollection[3].SetValue((object) this, (object) new ComputeValueModePropertyClass(this._Attribute4ObjectTypeProperties.ComputeValueMode));
    this.PropDescriptorCollection[4].SetValue((object) this, (object) this._Attribute4ObjectTypeProperties.Formula);
    this.PropDescriptorCollection[5].SetValue((object) this, (object) new UniqueValueModePropertyClass(this._Attribute4ObjectTypeProperties.UniqueValueMode));
    this.PropDescriptorCollection[6].SetValue((object) this, (object) new LevelPropertyClass(this._Attribute4ObjectTypeProperties.LevelID));
    this.PropDescriptorCollection[8].SetValue((object) this, (object) new FieldTypePropertyClass(this._AttributeTypeProperties.FieldType));
    this.PropDescriptorCollection[9].SetValue((object) this, (object) this._AttributeTypeProperties.AttributeID);
    this.PropDescriptorCollection[10].SetValue((object) this, (object) this._AttributeTypeProperties.AttributeGuid);
    this.PropDescriptorCollection[11].SetValue((object) this, (object) this._AttributeTypeProperties.Name);
    this.PropDescriptorCollection[12].SetValue((object) this, (object) new OptimizationModePropertyClass(this._Attribute4ObjectTypeProperties.OptimizationMode));
    this.PropDescriptorCollection[13].SetValue((object) this, (object) new BoolPropertyClass(this._Attribute4ObjectTypeProperties.IsContent));
    this.PropDescriptorCollection[14].SetValue((object) this, (object) this._Attribute4ObjectTypeProperties.Mask);
    this.PropDescriptorCollection[15].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog));
    this.PropDescriptorCollection[16 /*0x10*/].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.SavePrivateHistory) == AttributeOptions.SavePrivateHistory));
    this.PropDescriptorCollection[17].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory));
    this.PropDescriptorCollection[18].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls));
    this.PropDescriptorCollection[19].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent));
    this.PropDescriptorCollection[20].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.Internal) == AttributeOptions.Internal));
    this.PropDescriptorCollection[21].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase));
    this.PropDescriptorCollection[22].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit));
    this.PropDescriptorCollection[23].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.DontCopyPrototypeValue));
    this.PropDescriptorCollection[30].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.DontCopyPrototypeAttributeValueForArticle) == AttributeOptions.DontCopyPrototypeAttributeValueForArticle));
    this.PropDescriptorCollection[24].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex));
    this.PropDescriptorCollection[25].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue));
    this.PropDescriptorCollection[26].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.DontCopyVersionValue) == AttributeOptions.DontCopyVersionValue));
    this.PropDescriptorCollection[27].SetValue((object) this, (object) new BoolPropertyClass((this._Attribute4ObjectTypeProperties.Options & AttributeOptions.CopyValues2ChildObject) == AttributeOptions.CopyValues2ChildObject));
    this.PropDescriptorCollection[28].SetValue((object) this, this._Attribute4ObjectTypeProperties.MasterAttributeID == 0 ? (object) (AttributePropertyClass) null : (object) new AttributePropertyClass(this._Attribute4ObjectTypeProperties.MasterAttributeID));
    this.PropDescriptorCollection[29].SetValue((object) this, this._Attribute4ObjectTypeProperties.SourceAttributeID == 0 ? (object) (AttributePropertyClass) null : (object) new AttributePropertyClass(this._Attribute4ObjectTypeProperties.SourceAttributeID));
    AttributeEditor editor = (AttributeEditor) this.sourcePropDescriptor.GetEditor(typeof (AttributeEditor));
    if (editor != null)
    {
      editor.ExcludeAttributeId = new int[1]
      {
        this._Attribute4ObjectTypeProperties.AttributeID
      };
      editor.FilterByTypes = new FieldTypes[1]
      {
        this._AttributeTypeProperties.FieldType
      };
    }
    if (!this.defaultPropDescriptor.IsReadOnly)
      this.SetDefaultPropDescriptorValue(this._Attribute4ObjectTypeProperties.DefaultValue, this.PossibleValuesReadOnly);
    this.ProcessPublicProp();
    this.AddRegisteredPropertyDescriptors();
    pg?.Refresh();
  }

  /// <summary>
  /// Получение стандартных значений для тек. типа аттрибута
  /// </summary>
  /// <param name="s"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  public ArrayList GetList(object s, params object[] args)
  {
    ArrayList list = (ArrayList) null;
    if (s is DropDownTypeConverter)
    {
      AttributeTypePropertiesValidator validator = this.GetValidator();
      list = ((DropDownTypeConverter) s).GetStandardValuesCustomList((ITypeDescriptorContext) null, (object) validator);
    }
    return list;
  }

  /// <summary>Получение списка возможных значений для типа</summary>
  /// <param name="s"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  public ArrayList GetListByType(object s, params object[] args)
  {
    if (args == null || args.Length == 0)
      return (ArrayList) null;
    System.Type type = args[0] as System.Type;
    if (type == (System.Type) null || this._PossibleValuesDataTable == null)
      return (ArrayList) null;
    ArrayList listByType = new ArrayList();
    int columnIndex1 = this._PossibleValuesDataTable.Columns.IndexOf("F_INTEGER_VALUE");
    int columnIndex2 = this._PossibleValuesDataTable.Columns.IndexOf("F_DOUBLE_VALUE");
    int columnIndex3 = this._PossibleValuesDataTable.Columns.IndexOf("F_STRING_VALUE");
    int columnIndex4 = this._PossibleValuesDataTable.Columns.IndexOf("F_DATE_VALUE");
    int columnIndex5 = this._PossibleValuesDataTable.Columns.IndexOf("F_DESCRIPTION");
    int num = 0;
    if (type == typeof (double))
      num = 1;
    if (type == typeof (string))
      num = 2;
    if (type == typeof (DateTime))
      num = 3;
    if (type == typeof (Guid))
      num = 4;
    if (type == typeof (ObjectPropertyClass))
      num = 5;
    foreach (DataRow row in (InternalDataCollectionBase) this._PossibleValuesDataTable.Rows)
    {
      object obj = (object) null;
      try
      {
        switch (num)
        {
          case 0:
            obj = (object) new Int64PropertyClass(Convert.ToInt64(row[columnIndex1]), Convert.ToString(row[columnIndex5]), (DataTable) null);
            break;
          case 1:
            obj = (object) new DoublePropertyClass(Convert.ToDouble(row[columnIndex2]), Convert.ToString(row[columnIndex5]), (DataTable) null);
            break;
          case 2:
            obj = (object) new StringPropertyClass(row[columnIndex3].ToString(), Convert.ToString(row[columnIndex5]), (DataTable) null);
            break;
          case 3:
            obj = (object) new DateTimePropertyClass(Convert.ToDateTime(row[columnIndex4]), Convert.ToString(row[columnIndex5]), (DataTable) null);
            break;
          case 4:
            obj = (object) new GuidPropertyClass(new Guid(Convert.ToString(row[columnIndex3])), Convert.ToString(row[columnIndex5]), (DataTable) null);
            break;
          case 5:
            obj = (object) Convert.ToInt64(row[columnIndex1]);
            break;
        }
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
      if (obj != null)
        listByType.Add(obj);
    }
    return listByType;
  }

  /// <summary>
  /// 
  /// </summary>
  public void SaveValues()
  {
    this._Attribute4ObjectTypeProperties.InheritMode = ((InheritModePropertyClass) this.PropDescriptorCollection[0].GetValue((object) this)).InheritMode;
    this._Attribute4ObjectTypeProperties.RequiredMode = ((RequiredModePropertyClass) this.PropDescriptorCollection[1].GetValue((object) this)).RequiredMode;
    object obj1 = this.PropDescriptorCollection[2].GetValue((object) this);
    this._Attribute4ObjectTypeProperties.ValidationRule = obj1 == null ? string.Empty : obj1.ToString();
    this._Attribute4ObjectTypeProperties.ComputeValueMode = ((ComputeValueModePropertyClass) this.PropDescriptorCollection[3].GetValue((object) this)).ComputeValueMode;
    object obj2 = this.PropDescriptorCollection[4].GetValue((object) this);
    this._Attribute4ObjectTypeProperties.Formula = obj2 == null ? string.Empty : obj2.ToString();
    this._Attribute4ObjectTypeProperties.UniqueValueMode = ((UniqueValueModePropertyClass) this.PropDescriptorCollection[5].GetValue((object) this)).UniqueValueMode;
    this._Attribute4ObjectTypeProperties.LevelID = ((LevelPropertyClass) this.PropDescriptorCollection[6].GetValue((object) this)).Level;
    this._Attribute4ObjectTypeProperties.DefaultValue = this.GetDefaultPropDescriptorValue(this.PossibleValuesReadOnly);
    this._Attribute4ObjectTypeProperties.OptimizationMode = ((OptimizationModePropertyClass) this.PropDescriptorCollection[12].GetValue((object) this)).OptimizationMode;
    this._Attribute4ObjectTypeProperties.IsContent = ((BoolPropertyClass) this.PropDescriptorCollection[13].GetValue((object) this)).Boolean;
    this._Attribute4ObjectTypeProperties.Mask = (string) this.PropDescriptorCollection[14].GetValue((object) this);
    int num = ((BoolPropertyClass) this.PropDescriptorCollection[15].GetValue((object) this)).Boolean ? 1 : 0;
    bool boolean1 = ((BoolPropertyClass) this.PropDescriptorCollection[16 /*0x10*/].GetValue((object) this)).Boolean;
    bool boolean2 = ((BoolPropertyClass) this.PropDescriptorCollection[17].GetValue((object) this)).Boolean;
    bool boolean3 = ((BoolPropertyClass) this.PropDescriptorCollection[18].GetValue((object) this)).Boolean;
    bool boolean4 = ((BoolPropertyClass) this.PropDescriptorCollection[19].GetValue((object) this)).Boolean;
    bool boolean5 = ((BoolPropertyClass) this.PropDescriptorCollection[20].GetValue((object) this)).Boolean;
    bool boolean6 = ((BoolPropertyClass) this.PropDescriptorCollection[21].GetValue((object) this)).Boolean;
    bool boolean7 = ((BoolPropertyClass) this.PropDescriptorCollection[22].GetValue((object) this)).Boolean;
    bool boolean8 = ((BoolPropertyClass) this.PropDescriptorCollection[23].GetValue((object) this)).Boolean;
    bool boolean9 = ((BoolPropertyClass) this.PropDescriptorCollection[24].GetValue((object) this)).Boolean;
    bool boolean10 = ((BoolPropertyClass) this.PropDescriptorCollection[25].GetValue((object) this)).Boolean;
    bool boolean11 = ((BoolPropertyClass) this.PropDescriptorCollection[26].GetValue((object) this)).Boolean;
    bool boolean12 = ((BoolPropertyClass) this.PropDescriptorCollection[30].GetValue((object) this)).Boolean;
    bool boolean13 = ((BoolPropertyClass) this.PropDescriptorCollection[27].GetValue((object) this)).Boolean;
    this._Attribute4ObjectTypeProperties.Options = AttributeOptions.None;
    if (num != 0)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.SaveInLog;
    if (boolean1)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.SavePrivateHistory;
    if (boolean2)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.SaveCommonHistory;
    if (boolean3)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.DisableNulls;
    if (boolean4)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.GetDescriptionEvent;
    if (boolean5)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.Internal;
    if (boolean6)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.ModifyInBase;
    if (boolean7)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.DisableManualEdit;
    if (boolean8)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.DontCopyPrototypeValue;
    if (boolean9)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.AddToGlobalIndex;
    if (boolean10)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.DisableSplitIndexValue;
    if (boolean11)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.DontCopyVersionValue;
    if (boolean12)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.DontCopyPrototypeAttributeValueForArticle;
    if (boolean13)
      this._Attribute4ObjectTypeProperties.Options |= AttributeOptions.CopyValues2ChildObject;
    object obj3 = this.masterPropDescriptor.GetValue((object) this);
    this._Attribute4ObjectTypeProperties.MasterAttributeID = obj3 == null ? 0 : ((AttributePropertyClass) obj3).Attribute;
    object obj4 = this.sourcePropDescriptor.GetValue((object) this);
    this._Attribute4ObjectTypeProperties.SourceAttributeID = obj4 == null ? 0 : ((AttributePropertyClass) obj4).Attribute;
    this.ApplyToRegisteredPropertyDescriptors(this.Id);
  }

  /// <summary>Отмена изменений</summary>
  public void CancelChanges() => this.CancelToRegisteredPropertyDescriptors();

  /// <summary>Удвление элемента</summary>
  public void DeleteValues()
  {
    this.ChangeEventDataToRegisteredPropertyDescriptors((EventArgs) new DeleteIDEvenArgs());
  }

  private ArrayList GetObjTypeList(object s, params object[] values)
  {
    return ObjectEditor.GetObjTypeListByAttrId(this._AttributeTypeProperties.AttributeID);
  }

  /// <summary>Получение ед. измерения по-умолчанию</summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  private long GetDefaultMeasureID(object sender, params object[] args)
  {
    long defaultMeasureId = -1;
    if (this._AttributeTypeProperties.FieldType == FieldTypes.ftMeasured && this._AttributeTypeProperties.SizeType != -1L)
      defaultMeasureId = MeasureHelper.GetBaseMeasureID(this._AttributeTypeProperties.SizeType);
    return defaultMeasureId;
  }

  /// <summary>Получение списка мастер аттрибутов</summary>
  /// <param name="s"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  private ArrayList GetMasterListProc(object s, params object[] args)
  {
    return this._getMasterList != null ? this._getMasterList(s, args) : (ArrayList) null;
  }

  /// <summary>
  /// 
  /// </summary>
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

  /// <summary>
  /// 
  /// </summary>
  private void ProcessPublicProp()
  {
    bool flag = ((InheritModePropertyClass) this.publicPropDescriptor.GetValue((object) this)).InheritMode == InheritModes.Inherited;
    if (flag)
    {
      for (int index = 0; index < this.PropDescriptorCollection.Count; ++index)
      {
        if (this.PropDescriptorCollection[index] != this.publicPropDescriptor && this.PropDescriptorCollection[index] != this.attrTypePropDescriptor)
          ((PropDescriptor) this.PropDescriptorCollection[index]).SetReadOnly(true);
      }
    }
    else if (!((BoolPropertyClass) this.optionAddToGlobalIndex.GetValue((object) this)).Boolean)
      this.optionDisableSplitIndexValue.SetReadOnly(true);
    else
      this.optionDisableSplitIndexValue.SetReadOnly(false);
    if (this._AttributeTypeProperties.FieldType == FieldTypes.ftMeasured)
    {
      if (this.defaultPropDescriptor.IsReadOnly)
        this.defaultPropDescriptor.SetEditor((object) null);
      else
        this.defaultPropDescriptor.SetEditor(this.measureEditor);
    }
    if (flag)
    {
      this.formulaPropDescriptor.SetEditor((object) null);
      this.sourcePropDescriptor.SetEditor((object) null);
      this.validationRulePropDescriptor.SetEditor((object) null);
    }
    else
    {
      if (((ComputeValueModePropertyClass) this.computedPropDescriptor.GetValue((object) this)).ComputeValueMode == ComputeValueModes.NotComputableValue)
      {
        this.formulaPropDescriptor.SetEditor((object) null);
        this.formulaPropDescriptor.SetReadOnly(true);
      }
      else
      {
        this.formulaPropDescriptor.SetEditor(this.formulaEditor);
        this.formulaPropDescriptor.SetReadOnly(false);
      }
      this.sourcePropDescriptor.SetEditor(this.sourceEditor);
      this.validationRulePropDescriptor.SetEditor(this.validationEditor);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="atpValidator"></param>
  private void ProcessReadOnly(AttributeTypePropertiesValidator atpValidator)
  {
    for (int index = 0; index < this.PropDescriptorCollection.Count; ++index)
    {
      if (this.PropDescriptorCollection[index] != this.publicPropDescriptor && this.PropDescriptorCollection[index] != this.attrTypePropDescriptor && this.PropDescriptorCollection[index] != this.attrIdPropDescriptor && this.PropDescriptorCollection[index] != this.attrGuidPropDescriptor && this.PropDescriptorCollection[index] != this.attrNamePropDescriptor)
        ((PropDescriptor) this.PropDescriptorCollection[index]).SetReadOnly(false);
    }
    this.defaultPropDescriptor.SetReadOnly(atpValidator.DefaultValue == null);
    this.formulaPropDescriptor.SetReadOnly(atpValidator.Formula == null);
    this.maskPropDescriptor.SetReadOnly(atpValidator.Mask == null);
    this.maskPropDescriptor.SetEditor(this.maskPropDescriptor.IsReadOnly || this._AttributeTypeProperties.FieldType != FieldTypes.ftDateTime ? (object) (DateTimeMaskEditor) null : (object) new DateTimeMaskEditor());
  }

  /// <summary>Получение валидатора для тек. аттрибута</summary>
  /// <returns></returns>
  private AttributeTypePropertiesValidator GetValidator()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetAttributeTypeCollection(-1, CoreConsts.FilterRecords).GetValidatorForObjectType(this.Attribute4ObjectTypeProperties.AttributeID);
  }

  /// <summary>Назначение дескриптора по-умолчанию</summary>
  /// <param name="withSaveValues">Признак сохранения тек. значения</param>
  /// <param name="possibleValuesReadonly"></param>
  private void AssignDefaultPropDescriptor(bool withSaveValues, bool possibleValuesReadonly)
  {
    object obj = (object) null;
    if (withSaveValues && this.defaultPropDescriptor != null)
      obj = this.defaultPropDescriptor.GetValue((object) this);
    this._pdc = PropDescriptorHolder.RemovePDCItem(this.PropDescriptorCollection, 7);
    FieldTypes fieldType = this._AttributeTypeProperties.FieldType;
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
      this.PropDescriptorCollection.Insert(7, (PropertyDescriptor) this.defaultPropDescriptor);
      if (withSaveValues)
        this.defaultPropDescriptor.SetValue((object) this, obj);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aDefaultValue"></param>
  /// <param name="aPossibleValuesReadonly"></param>
  private void SetDefaultPropDescriptorValue(object aDefaultValue, bool aPossibleValuesReadonly)
  {
    FieldTypes fieldType = this._AttributeTypeProperties.FieldType;
    switch (fieldType)
    {
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        object obj = (object) null;
        if (aDefaultValue != null && aDefaultValue.ToString() != string.Empty)
          obj = !(aDefaultValue.ToString() == Intermech.Consts.CurrentUserFunction) ? (object) new ObjectPropertyClass(Convert.ToInt64(aDefaultValue), fieldType == FieldTypes.ftObjectLink) : (object) new ObjectPropertyClass(ObjectPropertyClassVariant.opcvCurrentUser, fieldType == FieldTypes.ftObjectLink);
        this.PropDescriptorCollection[7].SetValue((object) this, obj);
        break;
      default:
        if (aPossibleValuesReadonly)
        {
          this.PropDescriptorCollection[7].SetValue((object) this, AttributeValuesEditor.AttributeValueTransformationByCultureInfo(aDefaultValue, fieldType));
          break;
        }
        if (aDefaultValue != null && aDefaultValue.ToString() != string.Empty)
        {
          switch (fieldType)
          {
            case FieldTypes.ftString:
              this.PropDescriptorCollection[7].SetValue((object) this, (object) new StringPropertyClass(Convert.ToString(aDefaultValue), string.Empty, this._PossibleValuesDataTable));
              return;
            case FieldTypes.ftInteger:
              this.PropDescriptorCollection[7].SetValue((object) this, (object) new Int64PropertyClass(Convert.ToInt64(aDefaultValue), string.Empty, this._PossibleValuesDataTable));
              return;
            case FieldTypes.ftDouble:
              this.PropDescriptorCollection[7].SetValue((object) this, (object) new DoublePropertyClass(Convert.ToDouble(aDefaultValue), string.Empty, this._PossibleValuesDataTable));
              return;
            case FieldTypes.ftDateTime:
              this.PropDescriptorCollection[7].SetValue((object) this, (object) new DateTimePropertyClass(Convert.ToDateTime(aDefaultValue), string.Empty, this._PossibleValuesDataTable));
              return;
            case FieldTypes.ftGuid:
              this.PropDescriptorCollection[7].SetValue((object) this, (object) new GuidPropertyClass(new Guid(Convert.ToString(aDefaultValue)), string.Empty, this._PossibleValuesDataTable));
              return;
            default:
              this.PropDescriptorCollection[7].SetValue((object) this, aDefaultValue);
              return;
          }
        }
        else
        {
          this.PropDescriptorCollection[7].SetValue((object) this, aDefaultValue);
          break;
        }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aPossibleValuesReadonly"></param>
  /// <returns></returns>
  private object GetDefaultPropDescriptorValue(bool aPossibleValuesReadonly)
  {
    FieldTypes fieldType = this._AttributeTypeProperties.FieldType;
    object propDescriptorValue = this.PropDescriptorCollection[7].GetValue((object) this);
    if (fieldType == FieldTypes.ftObjectLink || fieldType == FieldTypes.ftObjectLinkByID)
    {
      if (propDescriptorValue != null)
        propDescriptorValue = ((ObjectPropertyClass) propDescriptorValue).ObjectPropertyClassVariant != ObjectPropertyClassVariant.opcvCurrentUser ? (object) ((ObjectPropertyClass) propDescriptorValue).ObjectID : (object) Intermech.Consts.CurrentUserFunction;
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

  /// <summary>
  /// 
  /// </summary>
  /// <param name="s"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  private ArrayList GetMeasureDescriptorList(object s, params object[] args)
  {
    ArrayList measureDescriptorList = (ArrayList) null;
    if (this._AttributeTypeProperties.FieldType == FieldTypes.ftMeasured)
      measureDescriptorList = MeasureEditor.GetMeasureDescriptorListByAttributeId(this._AttributeTypeProperties.AttributeID);
    return measureDescriptorList;
  }
}
