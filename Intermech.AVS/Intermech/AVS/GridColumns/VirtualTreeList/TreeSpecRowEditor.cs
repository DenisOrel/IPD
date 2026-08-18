// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.TreeSpecRowEditor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Редактор для табличного вида СП</summary>
public class TreeSpecRowEditor : UITypeEditor
{
  private int attributeId;
  private ColumnTag tag;
  private AVSWindow aVSWindow;

  public TreeSpecRowEditor(int attributeId) => this.AttributeId = attributeId;

  /// <summary>Идентификатор аттрибута</summary>
  public int AttributeId
  {
    get => this.attributeId;
    set => this.attributeId = value;
  }

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

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    switch (this.GetEditStyle(context))
    {
      case UITypeEditorEditStyle.Modal:
        return this.ModalEdit(context, provider, value);
      case UITypeEditorEditStyle.DropDown:
        return this.ModalEdit(context, provider, value);
      default:
        return value;
    }
  }

  public object DropDownEdit(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    return (object) null;
  }

  public object ModalEdit(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
  {
    object obj1 = value;
    try
    {
      if (!(context.Instance is AVSRow instance))
        return value;
      ColumnTag tag = this.Tag;
      if (tag == null)
        return value;
      AvsRowAttributeInfo rowAttributeInfo = tag.SpecRowAttributeInfo;
      if (rowAttributeInfo == null)
        return value;
      AVSDocument avsDocument = this.AVSWindow.AVSDocument;
      int num = 0;
      int relationIndex = 0;
      if (instance.IsFormB)
      {
        num = tag.ProductIndex;
        relationIndex = instance.GetRelationIndexForProduct(avsDocument.productsInfo[num].Id);
      }
      else if (instance.Relations != null && instance.Relations.Count > 0)
        num = avsDocument.GetProductIndex(instance.Relations[0].ProjectId);
      AttributeProcessor attributeProcessor = instance.GetAttributeProcessor(rowAttributeInfo, relationIndex != -1 ? relationIndex : 0, true);
      if (attributeProcessor == null)
        return value;
      bool attributeReadOnly = instance.GetAttributeReadOnly(rowAttributeInfo, relationIndex != -1 ? relationIndex : 0, instance.Relations);
      if (instance.IsFormB && AVSRow.IsCountField(rowAttributeInfo))
      {
        if (!attributeReadOnly)
        {
          instance.SetCount(num, value, true);
          relationIndex = instance.GetRelationIndexForProduct(avsDocument.productsInfo[num].Id);
        }
      }
      else if (!object.Equals(value, (object) "см. по исполнениям") && instance.CanInplaceEdit(rowAttributeInfo, relationIndex))
      {
        object obj2 = value;
        if (value is string)
        {
          TypeConverter typeConverter = attributeProcessor?.GetTypeConverter(new AttributeValues(rowAttributeInfo.AttributeId, value));
          if (typeConverter != null)
          {
            object initValue = typeConverter.ConvertFromString(attributeProcessor.GetAttributeContext(new AttributeValues(rowAttributeInfo.AttributeId, value)), (string) value);
            obj2 = attributeProcessor.GetAVValue(new AttributeValues(rowAttributeInfo.AttributeId, initValue));
          }
        }
        instance.SetFieldValue(rowAttributeInfo, -1, num, obj2, true, false, true, true, true, false);
      }
      if (AVSRow.IsCountField(rowAttributeInfo))
      {
        object attrValue;
        if ((attrValue = instance.GetFieldValue(rowAttributeInfo, relationIndex, num, false, false)) is MeasuredValue measuredValue)
        {
          string caption = measuredValue.Caption;
          if (!string.IsNullOrEmpty(caption))
            attrValue = (object) AVSRow.ConvertCountToMeasuredValue((object) caption, false);
        }
        object res = (object) null;
        bool allProducts = instance.IsFormB && avsDocument.productsInfo != null && avsDocument.productsInfo.Count > 1;
        if (instance.CallCountDocCellEditor(num, attrValue, out res, ref allProducts))
        {
          if (!attributeReadOnly)
          {
            if (!allProducts)
              instance.SetCount(num, res, true);
            else
              instance.SetCountToAllProducts(res, true);
          }
          else
            instance.SetCountMeasure(allProducts ? -1 : num, res, true);
          if (avsDocument.IsSpecification)
            relationIndex = instance.GetRelationIndexForProduct(avsDocument.productsInfo[num].Id);
          object obj3 = (object) "";
          if (relationIndex != -1)
            obj3 = instance.GetFieldValue(rowAttributeInfo, relationIndex, num, (List<RelationAttributeValuesCache>) null, false, false);
          obj1 = obj3;
        }
      }
      else
      {
        bool originalValue = false;
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(rowAttributeInfo.AttributeId);
        if (attributeType != null && attributeType.PossibleValues != null && attributeType.PossibleValues.Count > 0)
          originalValue = true;
        object obj4 = (object) null;
        if (relationIndex != -1)
          obj4 = instance.GetFieldValue(rowAttributeInfo, relationIndex, num, (List<RelationAttributeValuesCache>) null, false, false, originalValue);
        object initValue1 = obj4;
        if (obj4 is AVSObjectInfo avsObjectInfo)
          initValue1 = (object) avsObjectInfo.Id;
        if (rowAttributeInfo.FieldType != FieldTypes.ftString && initValue1 is string && (string) initValue1 == "")
          initValue1 = (object) null;
        object initValue2 = attributeProcessor.EditValue(new AttributeValues(rowAttributeInfo.AttributeId, initValue1), editorControl: ((Control) context).Parent, controlBounds: new Rectangle?(((Control) context).Bounds));
        if (instance.IsFormB && AVSRow.IsCountField(rowAttributeInfo))
        {
          instance.SetCount(num, initValue2, true);
          relationIndex = instance.GetRelationIndexForProduct(avsDocument.productsInfo[num].Id);
        }
        else
        {
          if (rowAttributeInfo.FieldType == FieldTypes.ftObjectLink)
            initValue2 = (object) new AVSObjectInfo(Convert.ToInt64(initValue2), attributeProcessor.GetViewValue(new AttributeValues(rowAttributeInfo.AttributeId, initValue2)));
          instance.SetFieldValue(rowAttributeInfo, -1, num, initValue2, true, false, true, true, false, true);
        }
        object obj5 = (object) "";
        if (relationIndex != -1)
          obj5 = instance.GetFieldValue(rowAttributeInfo, relationIndex, num, (List<RelationAttributeValuesCache>) null, false, false);
        obj1 = obj5;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return obj1;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    UITypeEditorEditStyle editStyle = UITypeEditorEditStyle.None;
    AvsRowAttributeInfo rowAttributeInfo = this.Tag.SpecRowAttributeInfo;
    if (rowAttributeInfo == null)
      return UITypeEditorEditStyle.None;
    AVSRow instance = context.Instance as AVSRow;
    AVSDocument avsDocument = this.AVSWindow.AVSDocument;
    int num1 = 0;
    int num2 = 0;
    if (instance.IsFormB || avsDocument.AvsDocumentForm == AVSDocumentForm.V)
    {
      num1 = this.Tag.ProductIndex;
      if (avsDocument.productsInfo.Count > num1)
        num2 = instance.GetRelationIndexForProduct(avsDocument.productsInfo[num1].Id);
    }
    else if (instance.Relations != null && instance.Relations.Count > 0)
      num1 = avsDocument.GetProductIndex(instance.Relations[0].ProjectId);
    bool flag = this.AVSWindow.ReadOnly;
    if (!flag)
    {
      if (instance.IsNoteRow)
      {
        TextData cellForAttribute = instance.GetDocumentCellForAttribute(rowAttributeInfo, num1);
        flag = cellForAttribute != null && cellForAttribute.ReadOnly;
      }
      else
        flag = instance.GetAttributeReadOnly(rowAttributeInfo, num2 != -1 ? num2 : 0, instance.Relations);
    }
    if (!flag)
    {
      if (AVSRow.IsCountField(rowAttributeInfo) && !rowAttributeInfo.IsDocField && instance.HasAnyRelations)
      {
        editStyle = UITypeEditorEditStyle.Modal;
      }
      else
      {
        List<UITypeEditorEditStyle> editorStyles = instance.GetEditorStyles(rowAttributeInfo, num2 != -1 ? num2 : 0);
        if (editorStyles == null || editorStyles.Count == 0)
          return UITypeEditorEditStyle.None;
        if (editorStyles.Contains(UITypeEditorEditStyle.DropDown))
          editStyle = UITypeEditorEditStyle.DropDown;
        else if (editorStyles.Contains(UITypeEditorEditStyle.Modal))
          editStyle = UITypeEditorEditStyle.Modal;
      }
    }
    else if (AVSRow.IsCountField(rowAttributeInfo))
      editStyle = UITypeEditorEditStyle.Modal;
    return editStyle;
  }
}
