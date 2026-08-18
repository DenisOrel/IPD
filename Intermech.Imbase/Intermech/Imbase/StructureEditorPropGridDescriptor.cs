// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.StructureEditorPropGridDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

internal class StructureEditorPropGridDescriptor : ICustomTypeDescriptor
{
  private Control _owner;
  private PropertyDescriptorCollection _pdc;
  private AttributeTypeProperties _attrTypeProps;
  private bool _keepData;
  private bool _useDefaults;
  private Dictionary<string, object> _possibleValues;
  private DataTable _dtData;
  private int _required = 2;
  private int _computed;
  private int _unique;
  private int _options;
  private string _unitStrGuid = string.Empty;

  internal static List<AttributeTypeProperties> AttTypePropsList { get; set; }

  internal Guid AttributeGuid => this._attrTypeProps.AttributeGuid;

  internal FieldTypes FieldType => this._attrTypeProps.FieldType;

  internal MultiValueModes MultiValueMode => this._attrTypeProps.MultiValueMode;

  internal string Name => this._attrTypeProps.Name;

  internal int AttributeID => this._attrTypeProps.AttributeID;

  internal AttributeTypeProperties AttrTypeProps
  {
    get => this._attrTypeProps;
    set
    {
      if (StructureEditorPropGridDescriptor.AttTypePropsList.Contains(this._attrTypeProps))
        StructureEditorPropGridDescriptor.AttTypePropsList.Remove(this._attrTypeProps);
      this._attrTypeProps = value;
      StructureEditorPropGridDescriptor.AttTypePropsList.Add(this._attrTypeProps);
    }
  }

  internal int Computed
  {
    get => this._computed;
    set => this._computed = value;
  }

  internal object DefaultValue
  {
    get => this._attrTypeProps.DefaultValue;
    set => this.SetValue("F_DEFAULT_VALUE", value);
  }

  internal string Formula
  {
    get => this._attrTypeProps.Formula;
    set => this.SetValue("F_FORMULA", (object) value);
  }

  internal int Options
  {
    get => this._options;
    set => this.SetValue("F_OPTIONS", (object) value);
  }

  internal Dictionary<string, object> PossibleValues => this._possibleValues;

  internal object[] FilteredPossibleValues
  {
    get
    {
      object[] filteredPossibleValues = (object[]) null;
      PropertyDescriptor propertyDescriptor = this._pdc.Find("F_FILTERED_POSSIBLE_VALUES", false);
      if (propertyDescriptor != null)
        filteredPossibleValues = propertyDescriptor.GetValue((object) null) as object[];
      return filteredPossibleValues;
    }
  }

  internal Tuple<string, List<Tuple<object, object>>> DependenPossibleValues
  {
    get
    {
      Tuple<string, List<Tuple<object, object>>> dependenPossibleValues = (Tuple<string, List<Tuple<object, object>>>) null;
      PropertyDescriptor propertyDescriptor = this._pdc.Find("F_DEPEND_POSSIBLE_VALUES", false);
      if (propertyDescriptor != null)
        dependenPossibleValues = propertyDescriptor.GetValue((object) null) as Tuple<string, List<Tuple<object, object>>>;
      return dependenPossibleValues;
    }
  }

  internal int Required
  {
    get => this._required != 2 || this._computed != 0 ? 0 : 2;
    set => this._required = value;
  }

  internal int Unique
  {
    get => this._unique;
    set => this.SetValue("F_UNIQUE", (object) value);
  }

  internal string Units
  {
    get => this._unitStrGuid;
    set => this.SetValue("F_UNITS", (object) value);
  }

  internal bool EditableAttribute => (this._options & 8388608 /*0x800000*/) != 0;

  internal bool HasColumn(string columnName) => this._dtData.Columns.Contains(columnName);

  internal StructureEditorPropGridDescriptor(
    Control owner,
    Guid attrGuid,
    int required,
    int computed,
    string formula,
    int unique,
    object defaultValue,
    int options,
    string units,
    IDBAttributeType attrType,
    DataTable dtData,
    IUserSession session)
  {
    if (attrType == null)
      return;
    this._owner = owner;
    this._dtData = dtData;
    this._required = required;
    this._computed = computed;
    this._unique = unique;
    this._options = options;
    this._unitStrGuid = units;
    this._attrTypeProps = new AttributeTypeProperties(attrType.Name, attrType.AttributeType);
    this._attrTypeProps.AttributeID = attrType.PropertiesStructure.AttributeID;
    this._attrTypeProps.AttributeGuid = attrGuid;
    this._attrTypeProps.MultiValueMode = attrType.MultipleValued;
    this._attrTypeProps.DefaultValue = defaultValue;
    this._attrTypeProps.Formula = formula;
    this._keepData = (this.Options & Convert.ToInt32((object) AttributeOptions.ImbaseFlag_TableRecordRef)) == 0;
    this._useDefaults = (this.Options & Convert.ToInt32((object) AttributeOptions.Imbase_DontUseDefaultsWithNull)) == 0;
    StructureEditorPropGridDescriptor.AttTypePropsList.Add(this._attrTypeProps);
    if (this._attrTypeProps.MultiValueMode == MultiValueModes.MultiValuesFromList || this._attrTypeProps.MultiValueMode == MultiValueModes.SingleValueFromList)
    {
      DataTable possibleValues = attrType.GetPossibleValues();
      if ((attrType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
      {
        this._possibleValues = new Dictionary<string, object>(possibleValues.Rows.Count + 1);
        this._possibleValues.Add(string.Empty, (object) string.Empty);
      }
      else
        this._possibleValues = new Dictionary<string, object>(possibleValues.Rows.Count);
      if (attrType.AttributeType == FieldTypes.ftObjectLink)
      {
        foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
        {
          if (!DBNull.Value.Equals(row[1]))
          {
            long int64 = Convert.ToInt64(row[1]);
            QuickObjectInfo objectInfo = session.GetObjectInfo(int64);
            if (!objectInfo.Empty)
              this._possibleValues.Add(objectInfo.VersionGuid.ToString(), (object) objectInfo.Caption);
          }
        }
      }
      else
      {
        foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
        {
          if (row[attrType.PossibleValueFieldName] != null && row[attrType.PossibleValueFieldName] != DBNull.Value)
          {
            string key = row[attrType.PossibleValueFieldName].ToString();
            if (!this._possibleValues.ContainsKey(key))
            {
              object obj = row["F_DESCRIPTION"];
              this._possibleValues.Add(key, obj == DBNull.Value || obj == null || string.IsNullOrEmpty(obj.ToString()) ? row[attrType.PossibleValueFieldName] : obj);
            }
          }
        }
      }
    }
    this.CreatePDC();
  }

  private void DefaultValue_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    if (StructureEditorPropGridDescriptor.AttTypePropsList.Contains(this._attrTypeProps))
      StructureEditorPropGridDescriptor.AttTypePropsList.Remove(this._attrTypeProps);
    if (this._attrTypeProps.FieldType == FieldTypes.ftObjectLink && e.Value != null)
    {
      string str = e.Value.ToString();
      if (GuidHelper.IsGuid(str))
      {
        Guid guid = new Guid(str);
        this._attrTypeProps.DefaultValue = !(guid != Guid.Empty) ? (object) null : (object) guid;
      }
      else
        this._attrTypeProps.DefaultValue = (object) null;
    }
    else
      this._attrTypeProps.DefaultValue = e.Value;
    StructureEditorPropGridDescriptor.AttTypePropsList.Add(this._attrTypeProps);
  }

  private void EditableAttribute_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    bool result = false;
    if (bool.TryParse(Convert.ToString(e.Value), out result) & result)
      this.Options |= 8388608 /*0x800000*/;
    else
      this.Options &= -8388609;
  }

  private void Formula_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    if (StructureEditorPropGridDescriptor.AttTypePropsList.Contains(this._attrTypeProps))
      StructureEditorPropGridDescriptor.AttTypePropsList.Remove(this._attrTypeProps);
    this._attrTypeProps.Formula = e.Value != null ? e.Value.ToString() : string.Empty;
    StructureEditorPropGridDescriptor.AttTypePropsList.Add(this._attrTypeProps);
    this.Computed = string.IsNullOrEmpty(this._attrTypeProps.Formula) || this.Required != 0 ? 0 : 2;
  }

  private void Keep_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    bool result = false;
    if (bool.TryParse(e.Value != null ? e.Value.ToString() : string.Empty, out result) & result)
      this.Options &= ~Convert.ToInt32((object) AttributeOptions.ImbaseFlag_TableRecordRef);
    else
      this.Options |= Convert.ToInt32((object) AttributeOptions.ImbaseFlag_TableRecordRef);
  }

  private void UseDefaults_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    bool result = false;
    if (bool.TryParse(e.Value != null ? e.Value.ToString() : string.Empty, out result) & result)
      this.Options &= ~Convert.ToInt32((object) AttributeOptions.Imbase_DontUseDefaultsWithNull);
    else
      this.Options |= Convert.ToInt32((object) AttributeOptions.Imbase_DontUseDefaultsWithNull);
  }

  private void Options_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    int result = -1;
    if (!int.TryParse(e.Value != null ? e.Value.ToString() : string.Empty, out result))
      return;
    this._options = result;
  }

  private void Required_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    int result = -1;
    if (!int.TryParse(e.Value != null ? e.Value.ToString() : string.Empty, out result))
      return;
    this._required = result;
    this.Computed = string.IsNullOrEmpty(this._attrTypeProps.Formula) || result != 0 ? 0 : 2;
  }

  private void Unique_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null)
      return;
    bool result = false;
    if (!bool.TryParse(e.Value != null ? e.Value.ToString() : string.Empty, out result))
      return;
    if (this.Required == 2 & result)
    {
      string name = this._attrTypeProps.AttributeGuid.ToString();
      if (this._dtData != null)
      {
        if (this._dtData.Columns.Contains(name))
        {
          try
          {
            this._dtData.Columns[name].Unique = result;
          }
          catch (Exception ex)
          {
            Trace.WriteLine(ex.Message);
            string caption = LocalizationHolder.rm.GetString("Imbase.StructureEditor.ChangedUnique.Caption");
            int num = (int) MessageBox.Show((IWin32Window) this._owner, LocalizationHolder.rm.GetString("Imbase.StructureEditor.ChangedUnique.Message"), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
          }
        }
      }
    }
    this._unique = result ? 1 : 0;
  }

  private void Units_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null || this._dtData == null)
      return;
    string g = e.Value != null ? e.Value.ToString() : string.Empty;
    if (string.IsNullOrEmpty(g) || Guid.Empty.ToString() == g)
    {
      this._unitStrGuid = string.Empty;
    }
    else
    {
      if (!string.IsNullOrEmpty(this.Units) && this.Units != g && this._dtData.Columns.Contains(this._attrTypeProps.AttributeGuid.ToString()))
      {
        string caption = LocalizationHolder.rm.GetString("Imbase.StructureEditor.ChangedUnits.Caption");
        switch (MessageBox.Show(LocalizationHolder.rm.GetString("Imbase.StructureEditor.ChangedUnits.Message"), caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
        {
          case DialogResult.Cancel:
            this._pdc["F_UNITS"].SetValue((object) this, (object) this.Units);
            return;
          case DialogResult.Yes:
            long measureID = -1;
            long toMeasureID = -1;
            try
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                measureID = sessionKeeper.Session.GetObjectInfo(new Guid(this.Units)).ObjectID;
                toMeasureID = sessionKeeper.Session.GetObjectInfo(new Guid(g)).ObjectID;
              }
            }
            catch (Exception ex)
            {
            }
            if (measureID != -1L && toMeasureID != -1L)
            {
              IEnumerator enumerator = this._dtData.Rows.GetEnumerator();
              try
              {
                while (enumerator.MoveNext())
                {
                  DataRow current = (DataRow) enumerator.Current;
                  if (current.RowState != DataRowState.Deleted)
                  {
                    object obj = current[this._attrTypeProps.AttributeGuid.ToString()];
                    if (obj != null)
                    {
                      double result = 0.0;
                      if (double.TryParse(obj.ToString(), out result) && result != 0.0)
                      {
                        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(new MeasuredValue(result, measureID), toMeasureID);
                        if (measuredValue != null)
                          current[this._attrTypeProps.AttributeGuid.ToString()] = (object) measuredValue.Value;
                      }
                    }
                  }
                }
                break;
              }
              finally
              {
                if (enumerator is IDisposable disposable)
                  disposable.Dispose();
              }
            }
            else
              break;
        }
      }
      this._unitStrGuid = g;
    }
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

  private string ConvertFormula(string strFormula, bool bDirection)
  {
    return string.IsNullOrEmpty(strFormula) ? string.Empty : TableEditor.RenameFormulaFields(strFormula, StructureEditorPropGridDescriptor.AttTypePropsList.ToArray(), bDirection);
  }

  private void CreatePDC()
  {
    this._pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    string category = LocalizationHolder.rma.GetString("Attribute.StructureEditor.Category.Name");
    List<Attribute> attributeList1 = new List<Attribute>();
    if (this._attrTypeProps.FieldType == FieldTypes.ftBoolean)
    {
      object obj = this._attrTypeProps.DefaultValue == DBNull.Value || this._attrTypeProps.DefaultValue == null ? (object) false : this._attrTypeProps.DefaultValue;
      attributeList1.Add((Attribute) new DefaultValueAttribute(obj));
    }
    else if (this._attrTypeProps.FieldType == FieldTypes.ftObjectLink)
      attributeList1.Add((Attribute) new DefaultValueAttribute((string) null));
    else
      attributeList1.Add((Attribute) new DefaultValueAttribute(this._attrTypeProps.DefaultValue));
    attributeList1.Add((Attribute) new CategoryAttribute(category));
    attributeList1.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.DefaultValue.Name"));
    attributeList1.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.DefaultValue.Description"));
    int num = this._attrTypeProps.MultiValueMode == MultiValueModes.MultiValuesFromList ? 1 : (this._attrTypeProps.MultiValueMode == MultiValueModes.SingleValueFromList ? 1 : 0);
    System.Type type1 = num != 0 ? typeof (ValueFromListConverter) : this.GetConverterForDefaultValue();
    System.Type type2 = num != 0 ? typeof (ValueFromListEditor) : this.GetEditorForDefaultValue();
    if (type1 != (System.Type) null)
      attributeList1.Add((Attribute) new TypeConverterAttribute(type1));
    if (type2 != (System.Type) null)
      attributeList1.Add((Attribute) new EditorAttribute(type2, typeof (UITypeEditor)));
    System.Type propType = AttributesTypeHelper.GetTypeOfAttributeValue(this._attrTypeProps.FieldType);
    if (propType == (System.Type) null)
      propType = typeof (string);
    StructureEditorPropertyDescriptor propertyDescriptor1 = new StructureEditorPropertyDescriptor(attributeList1.ToArray(), propType, "F_DEFAULT_VALUE", this.DefaultValue);
    propertyDescriptor1.AfterSetValue += new PropertySetValue(this.DefaultValue_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor1);
    attributeList1.Clear();
    attributeList1.Add((Attribute) new DefaultValueAttribute(""));
    attributeList1.Add((Attribute) new CategoryAttribute(category));
    attributeList1.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Formula.Name"));
    attributeList1.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Formula.Description"));
    attributeList1.Add((Attribute) new TypeConverterAttribute(typeof (FormulaConverter)));
    attributeList1.Add((Attribute) new EditorAttribute(typeof (FormulaEditor), typeof (UITypeEditor)));
    StructureEditorPropertyDescriptor propertyDescriptor2 = new StructureEditorPropertyDescriptor(attributeList1.ToArray(), typeof (string), "F_FORMULA", (object) this.Formula);
    propertyDescriptor2.AfterSetValue += new PropertySetValue(this.Formula_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor2);
    attributeList1.Clear();
    attributeList1.Add((Attribute) new BrowsableAttribute(this._attrTypeProps.FieldType == FieldTypes.ftString));
    attributeList1.Add((Attribute) new CategoryAttribute(category));
    attributeList1.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Keep.Name"));
    attributeList1.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Keep.Description"));
    attributeList1.Add((Attribute) new TypeConverterAttribute(typeof (KeepConverter)));
    attributeList1.Add((Attribute) new EditorAttribute(typeof (KeepEditor), typeof (UITypeEditor)));
    StructureEditorPropertyDescriptor propertyDescriptor3 = new StructureEditorPropertyDescriptor(attributeList1.ToArray(), typeof (bool), "Keep", (object) this._keepData);
    propertyDescriptor3.AfterSetValue += new PropertySetValue(this.Keep_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor3);
    List<Attribute> attributeList2 = new List<Attribute>();
    attributeList2.Add((Attribute) new CategoryAttribute(category));
    attributeList2.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.UseDefault.Name"));
    attributeList2.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.UseDefault.Description"));
    attributeList2.Add((Attribute) new TypeConverterAttribute(typeof (StructureEditorBooleanConverter)));
    attributeList2.Add((Attribute) new EditorAttribute(typeof (StructureEditorBooleanEditor), typeof (UITypeEditor)));
    StructureEditorPropertyDescriptor propertyDescriptor4 = new StructureEditorPropertyDescriptor(attributeList2.ToArray(), typeof (bool), "UseDefaults", (object) this._useDefaults);
    propertyDescriptor4.AfterSetValue += new PropertySetValue(this.UseDefaults_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor4);
    attributeList2.Clear();
    attributeList2.Add((Attribute) new DefaultValueAttribute(0));
    attributeList2.Add((Attribute) new CategoryAttribute(category));
    attributeList2.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Options.Name"));
    attributeList2.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Options.Description"));
    attributeList2.Add((Attribute) new TypeConverterAttribute(typeof (OptionsConverter)));
    attributeList2.Add((Attribute) new EditorAttribute(typeof (OptionsEditor), typeof (UITypeEditor)));
    StructureEditorPropertyDescriptor propertyDescriptor5 = new StructureEditorPropertyDescriptor(attributeList2.ToArray(), typeof (int), "F_OPTIONS", (object) this.Options);
    propertyDescriptor5.AfterSetValue += new PropertySetValue(this.Options_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor5);
    attributeList2.Clear();
    attributeList2.Add((Attribute) new DefaultValueAttribute(2));
    attributeList2.Add((Attribute) new CategoryAttribute(category));
    attributeList2.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Required.Name"));
    attributeList2.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Required.Description"));
    attributeList2.Add((Attribute) new TypeConverterAttribute(typeof (RequiredConverter)));
    attributeList2.Add((Attribute) new EditorAttribute(typeof (RequiredEditor), typeof (UITypeEditor)));
    StructureEditorPropertyDescriptor propertyDescriptor6 = new StructureEditorPropertyDescriptor(attributeList2.ToArray(), typeof (int), "F_REQUIRED", (object) this.Required);
    propertyDescriptor6.AfterSetValue += new PropertySetValue(this.Required_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor6);
    attributeList2.Clear();
    attributeList2.Add((Attribute) new DefaultValueAttribute(false));
    attributeList2.Add((Attribute) new CategoryAttribute(category));
    attributeList2.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Unique.Name"));
    attributeList2.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Unique.Description"));
    attributeList2.Add((Attribute) new TypeConverterAttribute(typeof (BooleanConverter)));
    attributeList2.Add((Attribute) new EditorAttribute(typeof (StructureEditorBooleanEditor), typeof (UITypeEditor)));
    attributeList2.Add((Attribute) new BrowsableAttribute(false));
    StructureEditorPropertyDescriptor propertyDescriptor7 = new StructureEditorPropertyDescriptor(attributeList2.ToArray(), typeof (bool), "F_UNIQUE", (object) this.Unique);
    propertyDescriptor7.AfterSetValue += new PropertySetValue(this.Unique_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor7);
    if (this._attrTypeProps.FieldType == FieldTypes.ftMeasured)
    {
      attributeList2.Clear();
      attributeList2.Add((Attribute) new DefaultValueAttribute(""));
      attributeList2.Add((Attribute) new CategoryAttribute(category));
      attributeList2.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.Units.Name"));
      attributeList2.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.Units.Description"));
      attributeList2.Add((Attribute) new TypeConverterAttribute(typeof (UnitsConverter)));
      attributeList2.Add((Attribute) new EditorAttribute(typeof (UnitsEditor), typeof (UITypeEditor)));
      StructureEditorPropertyDescriptor propertyDescriptor8 = new StructureEditorPropertyDescriptor(attributeList2.ToArray(), typeof (string), "F_UNITS", (object) this.Units);
      propertyDescriptor8.AfterSetValue += new PropertySetValue(this.Units_AfterSetValue);
      this._pdc.Add((PropertyDescriptor) propertyDescriptor8);
    }
    attributeList2.Clear();
    attributeList2.Add((Attribute) new CategoryAttribute(category));
    attributeList2.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.EditableAttribute.Name"));
    attributeList2.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.EditableAttribute.Description"));
    attributeList2.Add((Attribute) new TypeConverterAttribute(typeof (StructureEditorBooleanConverter)));
    attributeList2.Add((Attribute) new EditorAttribute(typeof (StructureEditorBooleanEditor), typeof (UITypeEditor)));
    StructureEditorPropertyDescriptor propertyDescriptor9 = new StructureEditorPropertyDescriptor(attributeList2.ToArray(), typeof (bool), "F_EDITABLE", (object) this.EditableAttribute);
    propertyDescriptor9.AfterSetValue += new PropertySetValue(this.EditableAttribute_AfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor9);
    if (num != 0)
    {
      attributeList2.Clear();
      attributeList2.Add((Attribute) new DefaultValueAttribute(""));
      attributeList2.Add((Attribute) new CategoryAttribute("Список"));
      attributeList2.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.PossibleValues.Name"));
      attributeList2.Add((Attribute) new TypeConverterAttribute(typeof (PossibleValuesFromListConverter)));
      attributeList2.Add((Attribute) new EditorAttribute(typeof (PossibleValuesFromListEditor), typeof (UITypeEditor)));
      string name = this._attrTypeProps.AttributeGuid.ToString();
      object obj = (object) null;
      if (this._dtData.Columns.Contains(name))
        obj = this._dtData.Columns[name].ExtendedProperties[(object) "F_FILTERED_POSSIBLE_VALUES"];
      this._pdc.Add((PropertyDescriptor) new StructureEditorPropertyDescriptor(attributeList2.ToArray(), typeof (object[]), "F_FILTERED_POSSIBLE_VALUES", obj));
    }
    if (this._attrTypeProps.MultiValueMode != MultiValueModes.SingleValueFromList)
      return;
    attributeList2.Clear();
    attributeList2.Add((Attribute) new DefaultValueAttribute((string) null));
    attributeList2.Add((Attribute) new CategoryAttribute("Список"));
    attributeList2.Add((Attribute) new CustomDisplayName("Imbase.Table.AttributeRedactor.DependValues.Name"));
    attributeList2.Add((Attribute) new CustomDescription("Imbase.Table.AttributeRedactor.DependValues.Description"));
    attributeList2.Add((Attribute) new TypeConverterAttribute(typeof (DependencyListConverter)));
    attributeList2.Add((Attribute) new EditorAttribute(typeof (DependencyListEditor), typeof (UITypeEditor)));
    string name1 = this._attrTypeProps.AttributeGuid.ToString();
    object obj1 = (object) null;
    if (this._dtData.Columns.Contains(name1))
      obj1 = this._dtData.Columns[name1].ExtendedProperties[(object) "F_DEPEND_POSSIBLE_VALUES"];
    else
      attributeList2.Add((Attribute) ReadOnlyAttribute.Yes);
    this._pdc.Add((PropertyDescriptor) new StructureEditorPropertyDescriptor(attributeList2.ToArray(), typeof (Tuple<string, List<Tuple<object, object>>>), "F_DEPEND_POSSIBLE_VALUES", obj1));
  }

  private System.Type GetConverterForDefaultValue()
  {
    switch (this._attrTypeProps.FieldType)
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
    switch (this._attrTypeProps.FieldType)
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

  internal void ChangeAttrTypeProps(Guid g, int id, string name, FieldTypes type)
  {
    if (!StructureEditorPropGridDescriptor.AttTypePropsList.Contains(this._attrTypeProps))
      return;
    StructureEditorPropGridDescriptor.AttTypePropsList.Remove(this._attrTypeProps);
    this._attrTypeProps.AttributeGuid = g;
    this._attrTypeProps.AttributeID = id;
    this._attrTypeProps.Name = name;
    this._attrTypeProps.FieldType = type;
    StructureEditorPropGridDescriptor.AttTypePropsList.Add(this._attrTypeProps);
  }

  internal void SetValue(string propertyName, object value)
  {
    this._pdc.Find(propertyName, true)?.SetValue((object) this, value);
  }
}
