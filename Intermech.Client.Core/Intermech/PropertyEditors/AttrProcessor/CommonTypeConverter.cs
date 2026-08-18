
// Type: Intermech.PropertyEditors.AttrProcessor.CommonTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Globalization;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// Базовый конвертор для работы в связке с AttributeProcessor.
/// Является предком оригинальных и пользовательских конверторов.
/// </summary>
public class CommonTypeConverter : TypeConverter
{
  protected MultiValueModes? multiValueModes;
  protected AttributeProcessor attributeProcessor;
  protected int attributeId;
  private Type typeOfAttributeValue;
  private TypeConverter standartConverter;
  private IMSAttributeType imsAttrType;
  protected List<UITypeEditorEditStyle> styles;

  public CommonTypeConverter(int aAttributeId, AttributeProcessor aAttributeProcessor)
  {
    this.attributeId = aAttributeId;
    this.attributeProcessor = aAttributeProcessor;
  }

  /// <summary>
  /// ищем тип атрибута FieldTypes,
  /// по этому типу берем тип значений (AttributeValues.Values) и
  /// его конвертор для вызова по умолчанию
  /// </summary>
  private void InitStandartAttributeValueConverter()
  {
    if (this.typeOfAttributeValue == (Type) null)
      this.typeOfAttributeValue = this.attributeProcessor.GetPropertyType(this.attributeId);
    if (this.standartConverter == null)
      this.standartConverter = TypeDescriptor.GetConverter(this.typeOfAttributeValue);
    if (this.imsAttrType != null)
      return;
    this.imsAttrType = MetaDataHelper.GetAttributeType(this.attributeId);
  }

  protected void InitMultiValuesModes()
  {
    if (this.multiValueModes.HasValue)
      return;
    this.multiValueModes = new MultiValueModes?(((ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.attributeId, false) ?? throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_921") + this.attributeId.ToString() + LocalizationHolder.rm.GetString("Client.Core_922"))).MultipleValued);
  }

  /// <summary>вернуть список допустимых стилей редакторов</summary>
  /// <returns></returns>
  public virtual List<UITypeEditorEditStyle> GetPossibleEditorControlStyle()
  {
    List<UITypeEditorEditStyle> editorControlStyle = new List<UITypeEditorEditStyle>();
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.attributeId, false);
    if (attributeType != null && MultiValueModesHelper.IsValuedFromList(attributeType.MultipleValued))
      editorControlStyle.Add(UITypeEditorEditStyle.DropDown);
    return editorControlStyle;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    this.InitStandartAttributeValueConverter();
    return this.standartConverter != null ? this.standartConverter.CanConvertFrom(context, sourceType) : base.CanConvertFrom(context, sourceType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    this.InitMultiValuesModes();
    this.InitStandartAttributeValueConverter();
    if (destinationType == typeof (string) && MultiValueModesHelper.IsValuedFromList(this.multiValueModes.Value))
      return true;
    return this.standartConverter != null ? this.standartConverter.CanConvertTo(context, destinationType) : base.CanConvertTo(context, destinationType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    this.InitStandartAttributeValueConverter();
    if (value != null && value is DBNull)
      value = (object) null;
    if ((value == null || value.ToString() == string.Empty) && this.imsAttrType != null && (this.imsAttrType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
      return (object) DBNull.Value;
    return this.standartConverter != null ? this.standartConverter.ConvertFrom(context, culture, value) : base.ConvertFrom(context, culture, value);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    this.InitMultiValuesModes();
    this.InitStandartAttributeValueConverter();
    if (destinationType == typeof (string) && MultiValueModesHelper.IsValuedFromList(this.multiValueModes.Value))
    {
      DataTable possibleValues = ClientCommons.GetPossibleValues(this.attributeId);
      string valueFieldName = ClientCommons.ExtractValueFieldName(possibleValues);
      foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
      {
        if (row[valueFieldName].Equals(value))
        {
          string str = Convert.ToString(row["F_DESCRIPTION"]);
          return str != string.Empty ? (object) str : (object) value?.ToString();
        }
      }
      return (object) string.Empty;
    }
    return this.standartConverter != null ? this.standartConverter.ConvertTo(context, culture, value, destinationType) : base.ConvertTo(context, culture, value, destinationType);
  }

  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    this.InitStandartAttributeValueConverter();
    return this.standartConverter != null ? this.standartConverter.CreateInstance(context, propertyValues) : base.CreateInstance(context, propertyValues);
  }

  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
  {
    this.InitStandartAttributeValueConverter();
    return this.standartConverter != null ? this.standartConverter.GetCreateInstanceSupported(context) : base.GetCreateInstanceSupported(context);
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    this.InitStandartAttributeValueConverter();
    return this.standartConverter != null ? this.standartConverter.GetProperties(context, value, attributes) : base.GetProperties(context, value, attributes);
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    this.InitStandartAttributeValueConverter();
    return this.standartConverter != null ? this.standartConverter.GetPropertiesSupported(context) : base.GetPropertiesSupported(context);
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    DataTable possibleValues = ClientCommons.GetPossibleValues(this.attributeId);
    string valueFieldName = ClientCommons.ExtractValueFieldName(possibleValues);
    ArrayList arrayList = new ArrayList();
    foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
    {
      object obj = row[valueFieldName];
      arrayList.Add(obj);
    }
    return new TypeConverter.StandardValuesCollection((ICollection) arrayList.ToArray());
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
  {
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.attributeId, false);
    return attributeType != null ? MultiValueModesHelper.IsValuedFromList(attributeType.MultipleValued) : base.GetStandardValuesExclusive(context);
  }

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
  {
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.attributeId, false);
    return attributeType != null ? MultiValueModesHelper.IsValuedFromList(attributeType.MultipleValued) : base.GetStandardValuesSupported(context);
  }

  public override bool IsValid(ITypeDescriptorContext context, object value)
  {
    List<ValidationResult> results = (List<ValidationResult>) null;
    return this.IsValidExt(context, value, ref results);
  }

  /// <summary>
  /// Общий метод проверки. Выдает отчет о валидации в виде списка.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="value"></param>
  /// <param name="results">для одиночного value в случае ошибки приходит список с одним элементом</param>
  /// <returns></returns>
  public virtual bool IsValidExt(
    ITypeDescriptorContext context,
    object value,
    ref List<ValidationResult> results)
  {
    this.InitStandartAttributeValueConverter();
    results = (List<ValidationResult>) null;
    bool flag = this.standartConverter == null ? base.IsValid(context, value) : this.standartConverter.IsValid(context, value);
    if (!flag)
    {
      results = new List<ValidationResult>();
      results.Add(new ValidationResult(this.attributeId, AttributeProcessorConsts.msgInvalidValue));
    }
    return flag;
  }

  /// <summary>получить контрол для редактирования значения</summary>
  /// <param name="style">желаемый стиль контрола.
  /// для Modal возвращается форма, для DropDown - Control.
  /// при Modal конвертор может получить из контрола форму обертыванием в класс EditorControlForm</param>
  /// <returns></returns>
  public virtual IAttributeEditorControl GetEditorControl(UITypeEditorEditStyle style)
  {
    IAttributeEditorControl iAttributeEditorControl = (IAttributeEditorControl) null;
    if (style == UITypeEditorEditStyle.None)
      return (IAttributeEditorControl) null;
    if (this.styles == null)
      this.styles = this.GetPossibleEditorControlStyle();
    bool flag = AttributeProcessorProcs.IsValuedFromList(this.attributeId);
    if (!flag && this.styles.IndexOf(style) == -1)
      return (IAttributeEditorControl) null;
    if (flag)
    {
      iAttributeEditorControl = (IAttributeEditorControl) new DropDownEditorControl();
      if (style == UITypeEditorEditStyle.Modal)
      {
        EditorControlForm editorControlForm = new EditorControlForm();
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.attributeId);
        if (attributeType != null)
          editorControlForm.Text = attributeType.Name;
        editorControlForm.AssignControl(iAttributeEditorControl);
        iAttributeEditorControl = (IAttributeEditorControl) editorControlForm;
      }
    }
    return iAttributeEditorControl;
  }
}
