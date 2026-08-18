// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.TreeSpecRowConverter
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Output;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Конвертер для табличного вида спецификации</summary>
public class TreeSpecRowConverter : TypeConverter
{
  private int attributeId;
  private ColumnTag tag;
  private AVSWindow aVSWindow;

  public TreeSpecRowConverter(int attributeId) => this.AttributeId = attributeId;

  /// <summary>Идентификатор атрибута</summary>
  public int AttributeId
  {
    get => this.attributeId;
    set => this.attributeId = value;
  }

  /// <summary>Тэг колонки</summary>
  public ColumnTag Tag
  {
    get => this.tag;
    set => this.tag = value;
  }

  public AVSWindow AVSWindow
  {
    get => this.aVSWindow;
    set => this.aVSWindow = value;
  }

  public bool CanConvertFrom(AVSRow specRow)
  {
    AVSDocument avsDocument = this.AVSWindow.AVSDocument;
    if (avsDocument == null)
      return false;
    ColumnTag tag = this.Tag;
    AvsRowAttributeInfo rowAttributeInfo = tag.SpecRowAttributeInfo;
    if (rowAttributeInfo == null)
      return false;
    int num1 = 0;
    int num2 = 0;
    if (specRow.IsFormB || avsDocument.AvsDocumentForm == AVSDocumentForm.V)
    {
      num1 = tag.ProductIndex;
      if (avsDocument.productsInfo.Count > num1)
        num2 = specRow.GetRelationIndexForProduct(avsDocument.productsInfo[num1].Id);
    }
    else if (specRow.Relations != null && specRow.Relations.Count > 0)
      num1 = avsDocument.GetProductIndex(specRow.Relations[0].ProjectId);
    bool flag = this.AVSWindow.ReadOnly;
    if (!flag)
    {
      if (specRow.IsNoteRow)
      {
        TextData cellForAttribute = specRow.GetDocumentCellForAttribute(rowAttributeInfo, num1);
        flag = cellForAttribute != null && cellForAttribute.ReadOnly;
      }
      else
        flag = specRow.GetAttributeReadOnly(rowAttributeInfo, num2 != -1 ? num2 : 0, specRow.Relations);
    }
    if (!flag)
      flag = !specRow.CanInplaceEdit(rowAttributeInfo, num2 != -1 ? num2 : 0);
    return !flag;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return this.CanConvertFrom(context.Instance as AVSRow);
  }

  public object SetValueToSpecificationRow(AVSRow specRow, object value)
  {
    object specificationRow = value;
    AVSDocument avsDocument = this.AVSWindow.AVSDocument;
    if (avsDocument == null)
      return specificationRow;
    try
    {
      ColumnTag tag = this.Tag;
      AvsRowAttributeInfo rowAttributeInfo = tag.SpecRowAttributeInfo;
      if (rowAttributeInfo == null || specRow == null)
        return specificationRow;
      int num = specRow.InCommonData_AV ? -1 : 0;
      int relationIndex = specRow.InCommonData_AV ? -1 : 0;
      if (specRow.IsFormB || avsDocument.AvsDocumentForm == AVSDocumentForm.V)
      {
        if (AVSRow.IsCountField(rowAttributeInfo))
        {
          num = tag.ProductIndex;
          relationIndex = specRow.GetRelationIndexForProduct(avsDocument.productsInfo[num].Id);
        }
        else
        {
          num = -1;
          relationIndex = -1;
        }
      }
      else if (!specRow.InCommonData_AV && specRow.HasRelation)
        num = avsDocument.GetProductIndex(specRow.Relations[0].ProjectId);
      if (AVSRow.IsCountField(rowAttributeInfo) && specRow.SectionID == AVSDocument.ObjID_SectionMaterials)
      {
        string text = value as string;
        string str = "";
        if (relationIndex != -1)
          str = specRow.GetFieldValue(rowAttributeInfo, relationIndex, num, false, false) as string;
        if (text != null && text != str)
        {
          MeasuredValue measuredValue = specRow.ValidateMaterialCount(text, new double?(), relationIndex);
          if (measuredValue != null)
          {
            avsDocument.ValidateValue = true;
            specificationRow = (object) measuredValue.ToString();
          }
        }
      }
      object obj;
      try
      {
        object fieldValue = specRow.GetFieldValue(rowAttributeInfo, relationIndex, num, false, false, true);
        if (fieldValue != null)
        {
          if (!fieldValue.Equals(specificationRow))
          {
            if (fieldValue is DBNull)
            {
              if (specificationRow.Equals((object) string.Empty))
                goto label_38;
            }
            if (fieldValue.Equals((object) string.Empty))
            {
              if (specificationRow is DBNull)
                goto label_38;
            }
          }
          else
            goto label_38;
        }
        if (specRow.IsFormB && AVSRow.IsCountField(rowAttributeInfo))
        {
          specRow.SetCount(num, value, true);
          if (specRow.IsFormB && value != null && value.ToString() != "")
          {
            specRow.Section.UpdateViewNodes(avsDocument.skipLinesSchema, false, false, false, false, false, EmptyRowUpdateMode.DontChange);
            if (this.AVSWindow.Document.NeedUpdateLayoutFlag)
              this.AVSWindow.Document.UpdateLayout(true);
          }
          relationIndex = specRow.GetRelationIndexForProduct(avsDocument.productsInfo[num].Id);
          TextData cellForAttribute = specRow.GetDocumentCellForAttribute(rowAttributeInfo, num);
          CellOutputMapping attributeMapping = specRow.GetCellAttributeMapping(cellForAttribute);
          specificationRow = (object) specRow.GetTextForDocCell(attributeMapping, rowAttributeInfo, relationIndex, num, false, false);
        }
        else
        {
          if (rowAttributeInfo.FieldType == FieldTypes.ftBoolean)
          {
            if (value is string)
            {
              try
              {
                obj = new CustomBooleanConverter().ConvertFromString((string) value);
              }
              catch
              {
              }
            }
          }
          if (AVSRow.IsCountField(rowAttributeInfo) && specRow.HasRelation)
            specRow.SetCount(num, value, true);
          else
            specRow.SetFieldValue(rowAttributeInfo, -1, -1, value, true, false, true, true, false, false);
          specificationRow = (object) specRow.GetFieldStringValue(rowAttributeInfo, 0, num, (List<RelationAttributeValuesCache>) null, false, true);
        }
      }
      catch (Exception ex)
      {
        obj = (object) specRow.GetFieldStringValue(rowAttributeInfo, relationIndex, num, (List<RelationAttributeValuesCache>) null, false, true);
        throw;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      throw;
    }
label_38:
    return specificationRow;
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return this.SetValueToSpecificationRow(context.Instance as AVSRow, value);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return destinationType == typeof (string) ? (object) Convert.ToString(value) : base.ConvertTo(context, culture, value, destinationType);
  }

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
  {
    return base.GetStandardValuesSupported(context);
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    return base.GetStandardValues(context);
  }
}
