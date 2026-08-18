// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.RestructuringPropGridDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class RestructuringPropGridDescriptor : ICustomTypeDescriptor
{
  private Control _owner;
  private PropertyDescriptorCollection _pdc;
  private bool _keepData;
  private Dictionary<string, object> _possibleValues;
  private MultiValueModes _multiValueMode;

  internal RestructuringTablesAttrSettings Settings { get; private set; }

  internal int Options
  {
    get => this.Settings.Options;
    set => this._pdc.Find("F_OPTIONS", true)?.SetValue((object) this, (object) value);
  }

  internal Dictionary<string, object> PossibleValues => this._possibleValues;

  internal RestructuringPropGridDescriptor(
    Control owner,
    IDBAttributeType attrType,
    int required,
    int unique,
    object defaultValue,
    int options,
    string units)
  {
    if (attrType == null)
      return;
    this._owner = owner;
    this.Settings = new RestructuringTablesAttrSettings(attrType.PropertiesStructure.AttributeGuid, attrType.PropertiesStructure.AttributeID, attrType.Name, attrType.AttributeType, required, unique, defaultValue, options, units);
    this.Settings.Formula = attrType.Formula;
    this._keepData = (this.Options & Convert.ToInt32((object) AttributeOptions.ImbaseFlag_TableRecordRef)) == 0;
    this._multiValueMode = attrType.MultipleValued;
    if (this._multiValueMode == MultiValueModes.MultiValuesFromList || this._multiValueMode == MultiValueModes.SingleValueFromList)
    {
      DataTable possibleValues = attrType.GetPossibleValues();
      if ((attrType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
      {
        this._possibleValues = new Dictionary<string, object>(possibleValues.Rows.Count + 1);
        this._possibleValues.Add(string.Empty, (object) string.Empty);
      }
      else
        this._possibleValues = new Dictionary<string, object>(possibleValues.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
      {
        string key = Convert.ToString(row[attrType.PossibleValueFieldName]);
        if (!string.IsNullOrEmpty(key) && !this._possibleValues.ContainsKey(key))
          this._possibleValues.Add(key, !string.IsNullOrEmpty(Convert.ToString(row["F_DESCRIPTION"])) ? row["F_DESCRIPTION"] : row[attrType.PossibleValueFieldName]);
      }
    }
    this.CreatePDC();
  }

  private void pdDefaultValue_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    if (this.Settings.Type == FieldTypes.ftObjectLink && e.Value != null)
    {
      string str = e.Value.ToString();
      if (GuidHelper.IsGuid(str))
        this.Settings.DefaultValue = new Guid(str) != Guid.Empty ? (object) str : (object) (string) null;
      else
        this.Settings.DefaultValue = (object) null;
    }
    else
      this.Settings.DefaultValue = e.Value;
  }

  private void pdFormula_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    this.Settings.Formula = Convert.ToString(e.Value);
  }

  private void pdKeep_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    bool result = false;
    if (bool.TryParse(Convert.ToString(e.Value), out result) & result)
      this.Options &= ~Convert.ToInt32((object) AttributeOptions.ImbaseFlag_TableRecordRef);
    else
      this.Options |= Convert.ToInt32((object) AttributeOptions.ImbaseFlag_TableRecordRef);
  }

  private void pdOptions_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    int result = -1;
    if (!int.TryParse(Convert.ToString(e.Value), out result))
      return;
    this.Settings.Options = result;
  }

  private void pdRequired_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    int result = -1;
    if (!int.TryParse(Convert.ToString(e.Value), out result))
      return;
    this.Settings.Required = result;
  }

  private void pdUnique_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    bool result = false;
    if (!bool.TryParse(Convert.ToString(e.Value), out result))
      return;
    this.Settings.Unique = result ? 1 : 0;
  }

  private void pdUnits_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    string g = Convert.ToString(e.Value);
    if (!string.IsNullOrEmpty(g))
    {
      Guid guid = new Guid(g);
      if (Guid.Empty == guid)
      {
        this.Settings.Units = string.Empty;
      }
      else
      {
        if (string.IsNullOrEmpty(g) || !(this.Settings.Units != g))
          return;
        this.Settings.Units = g;
      }
    }
    else
      this.Settings.Units = string.Empty;
  }

  public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes((object) this, true);

  public string GetClassName() => TypeDescriptor.GetClassName((object) this, true);

  public string GetComponentName() => TypeDescriptor.GetComponentName((object) this, true);

  public TypeConverter GetConverter() => TypeDescriptor.GetConverter((object) this, true);

  public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent((object) this, true);

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this, true);
  }

  public object GetEditor(System.Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this, attributes, true);
  }

  public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents((object) this, true);

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => this.GetProperties();

  public PropertyDescriptorCollection GetProperties() => this._pdc;

  public object GetPropertyOwner(PropertyDescriptor pd) => (object) this;

  private void CreatePDC()
  {
    this._pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    string category = LocalizationHolder.rma.GetString("Attribute.StructureEditor.Category.Name");
    List<Attribute> attributeList = new List<Attribute>();
    if (this.Settings.Type == FieldTypes.ftBoolean)
    {
      object obj = this.Settings.DefaultValue == DBNull.Value || this.Settings.DefaultValue == null ? (object) false : this.Settings.DefaultValue;
      attributeList.Add((Attribute) new DefaultValueAttribute(obj));
    }
    else if (this.Settings.Type == FieldTypes.ftObjectLink)
      attributeList.Add((Attribute) new DefaultValueAttribute((string) null));
    else
      attributeList.Add((Attribute) new DefaultValueAttribute(this.Settings.DefaultValue));
    attributeList.Add((Attribute) new CategoryAttribute(category));
    attributeList.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.DefaultValue.Name"));
    attributeList.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.DefaultValue.Description"));
    int num = this._multiValueMode == MultiValueModes.MultiValuesFromList ? 1 : (this._multiValueMode == MultiValueModes.SingleValueFromList ? 1 : 0);
    System.Type type1 = num != 0 ? typeof (FromListConverter) : this.GetConverterForDefaultValue();
    System.Type type2 = num != 0 ? typeof (FromListEditor) : this.GetEditorForDefaultValue();
    if (type1 != (System.Type) null)
      attributeList.Add((Attribute) new TypeConverterAttribute(type1));
    if (type2 != (System.Type) null)
      attributeList.Add((Attribute) new EditorAttribute(type2, typeof (UITypeEditor)));
    System.Type type3 = AttributesTypeHelper.GetTypeOfAttributeValue(this.Settings.Type);
    if ((object) type3 == null)
      type3 = typeof (string);
    System.Type propType = type3;
    StructureEditorPropertyDescriptor propertyDescriptor1 = new StructureEditorPropertyDescriptor(attributeList.ToArray(), propType, "F_DEFAULT_VALUE", this.Settings.DefaultValue);
    propertyDescriptor1.AfterSetValue += new PropertySetValue(this.pdDefaultValue_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor1);
    StructureEditorPropertyDescriptor propertyDescriptor2 = new StructureEditorPropertyDescriptor(new List<Attribute>()
    {
      (Attribute) new DefaultValueAttribute(""),
      (Attribute) new CategoryAttribute(category),
      (Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Formula.Name"),
      (Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Formula.Description"),
      (Attribute) new EditorAttribute(typeof (AttributeFormulaUITypeEditor), typeof (UITypeEditor))
    }.ToArray(), typeof (string), "F_FORMULA", (object) this.Settings.Formula);
    propertyDescriptor2.AfterSetValue += new PropertySetValue(this.pdFormula_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor2);
    StructureEditorPropertyDescriptor propertyDescriptor3 = new StructureEditorPropertyDescriptor(new List<Attribute>()
    {
      (Attribute) new BrowsableAttribute(this.Settings.Type == FieldTypes.ftString),
      (Attribute) new CategoryAttribute(category),
      (Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Keep.Name"),
      (Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Keep.Description"),
      (Attribute) new TypeConverterAttribute(typeof (KeepConverter)),
      (Attribute) new EditorAttribute(typeof (KeepEditor), typeof (UITypeEditor))
    }.ToArray(), typeof (bool), "Keep", (object) this._keepData);
    propertyDescriptor3.AfterSetValue += new PropertySetValue(this.pdKeep_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor3);
    StructureEditorPropertyDescriptor propertyDescriptor4 = new StructureEditorPropertyDescriptor(new List<Attribute>()
    {
      (Attribute) new DefaultValueAttribute(0),
      (Attribute) new CategoryAttribute(category),
      (Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Options.Name"),
      (Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Options.Description"),
      (Attribute) new TypeConverterAttribute(typeof (OptionsConverter)),
      (Attribute) new EditorAttribute(typeof (OptionsEditor), typeof (UITypeEditor))
    }.ToArray(), typeof (int), "F_OPTIONS", (object) this.Options);
    propertyDescriptor4.AfterSetValue += new PropertySetValue(this.pdOptions_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor4);
    StructureEditorPropertyDescriptor propertyDescriptor5 = new StructureEditorPropertyDescriptor(new List<Attribute>()
    {
      (Attribute) new DefaultValueAttribute(2),
      (Attribute) new CategoryAttribute(category),
      (Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Required.Name"),
      (Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Required.Description"),
      (Attribute) new TypeConverterAttribute(typeof (RequiredConverter)),
      (Attribute) new EditorAttribute(typeof (RequiredEditor), typeof (UITypeEditor))
    }.ToArray(), typeof (int), "F_REQUIRED", (object) this.Settings.Required);
    propertyDescriptor5.AfterSetValue += new PropertySetValue(this.pdRequired_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor5);
    StructureEditorPropertyDescriptor propertyDescriptor6 = new StructureEditorPropertyDescriptor(new List<Attribute>()
    {
      (Attribute) new DefaultValueAttribute(false),
      (Attribute) new CategoryAttribute(category),
      (Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Unique.Name"),
      (Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Unique.Description"),
      (Attribute) new TypeConverterAttribute(typeof (BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)),
      (Attribute) new BrowsableAttribute(false)
    }.ToArray(), typeof (bool), "F_UNIQUE", (object) this.Settings.Unique);
    propertyDescriptor6.AfterSetValue += new PropertySetValue(this.pdUnique_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor6);
    StructureEditorPropertyDescriptor propertyDescriptor7 = new StructureEditorPropertyDescriptor(new List<Attribute>()
    {
      (Attribute) new BrowsableAttribute(this.Settings.Type == FieldTypes.ftMeasured),
      (Attribute) new DefaultValueAttribute(""),
      (Attribute) new CategoryAttribute(category),
      (Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Units.Name"),
      (Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Units.Description"),
      (Attribute) new TypeConverterAttribute(typeof (UnitsConverter)),
      (Attribute) new EditorAttribute(typeof (UnitsEditor1), typeof (UITypeEditor))
    }.ToArray(), typeof (string), "F_UNITS", (object) this.Settings.Units);
    propertyDescriptor7.AfterSetValue += new PropertySetValue(this.pdUnits_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor7);
  }

  private System.Type GetConverterForDefaultValue()
  {
    switch (this.Settings.Type)
    {
      case FieldTypes.ftInteger:
        return typeof (StructureEditorIntegerConverter);
      case FieldTypes.ftDouble:
        return typeof (StructureEditorDoubleConverter);
      case FieldTypes.ftDateTime:
        return typeof (StructureEditorDateTimeConverter);
      case FieldTypes.ftObjectLink:
        return typeof (ObjectLinkConverter);
      case FieldTypes.ftBoolean:
        return typeof (StructureEditorBooleanConverter);
      case FieldTypes.ftMeasured:
        return typeof (StructureEditorDoubleConverter);
      case FieldTypes.ftGuid:
        return typeof (StructureEditorGUIDConverter);
      default:
        return (System.Type) null;
    }
  }

  private System.Type GetEditorForDefaultValue()
  {
    switch (this.Settings.Type)
    {
      case FieldTypes.ftDateTime:
        return typeof (StructureEditorDateTimeEditor);
      case FieldTypes.ftObjectLink:
        return typeof (ObjectLinkEditor);
      case FieldTypes.ftBoolean:
        return typeof (StructureEditorBooleanEditor);
      default:
        return (System.Type) null;
    }
  }
}
