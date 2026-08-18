
// Type: Intermech.PropertyEditors.ImbaseFilterEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class ImbaseFilterEditor : UITypeEditor
{
  /// <summary>Идентификатор объекта</summary>
  private long _objID;
  /// <summary>Идентификаторы каталогов/справочников IMBASE</summary>
  private List<long> _catalogIDs;
  /// <summary>
  /// 
  /// </summary>
  private ImbaseCatalogSelectMode _selectMode = ImbaseCatalogSelectMode.imcmNone;
  /// <summary>Типы создаваемых объектов</summary>
  private int[] _needTypeIDs;
  /// <summary>Номер выбранной записи ссылки на таблицу IMBASE</summary>
  private long _recID = -1;

  /// <summary>Конструктор.</summary>
  /// <param name="catalogIDs">Идентификатор каталога/справочника IMBASE</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="selectMode">Режим выбора объектов из каталогов Imbase</param>
  public ImbaseFilterEditor(List<long> catalogIDs, long objID, ImbaseCatalogSelectMode selectMode)
  {
    this._catalogIDs = catalogIDs;
    this._objID = objID;
    this._selectMode = selectMode;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="catalogIDs">Идентификатор каталога/справочника IMBASE</param>
  /// <param name="needTypeID">Тип создаваемых объектов</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="selectMode">Режим выбора объектов из каталогов Imbase</param>
  public ImbaseFilterEditor(
    List<long> catalogIDs,
    int[] needTypeIDs,
    long objID,
    ImbaseCatalogSelectMode selectMode)
    : this(catalogIDs, objID, selectMode)
  {
    this._needTypeIDs = needTypeIDs;
  }

  /// <summary>Получить дескриптор свойства атрибута "Код IMBASE".</summary>
  /// <param name="collection">Коллекция дескрипторов</param>
  /// <returns>Дескриптор свойства атрибута "Код IMBASE"</returns>
  private SimplePropDescriptor GetCodeImbasePropertyDescriptor(
    PropertyDescriptorCollection collection)
  {
    SimplePropDescriptor propertyDescriptor1 = (SimplePropDescriptor) null;
    if (collection != null)
    {
      int attributeId = MetaDataHelper.GetAttributeID((object) new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
      foreach (PropertyDescriptor propertyDescriptor2 in collection)
      {
        if (propertyDescriptor2 is SimplePropDescriptor aPD && aPD.AttributeValuePropertyClass != null && aPD.AttributeValuePropertyClass.AttributeValue != null && aPD.AttributeValuePropertyClass.AttributeValue.AttributeID == attributeId)
        {
          propertyDescriptor1 = aPD;
          object avValue = AttributeValuesEditor.GetAVValue((PropDescriptor) aPD, aPD.AttributeValuePropertyClass.AttributeValue, aPD.Component);
          long result = -1;
          if (long.TryParse(Convert.ToString(avValue), out result))
          {
            this._recID = result;
            break;
          }
          break;
        }
      }
    }
    return propertyDescriptor1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sel"></param>
  /// <param name="attrID"></param>
  /// <param name="oldObjID"></param>
  /// <returns></returns>
  public object EditValue(IImbaseFilterSelector sel, int attrID, long oldObjID)
  {
    long aObjectID;
    if (attrID == Intermech.Imbase.Consts.ImbaseObjectRefAttID)
    {
      if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject)
      {
        aObjectID = sel.SelectImbaseObject(this._catalogIDs, (int[]) null, this._objID, oldObjID, this._selectMode);
      }
      else
      {
        sel.RecordID = this._recID;
        aObjectID = sel.SelectImbaseObject(this._catalogIDs, (int[]) null, this._objID, oldObjID, ImbaseCatalogSelectMode.imcmAllowSelectRow);
        this._recID = sel.RecordID;
      }
    }
    else
      aObjectID = sel.SelectImbaseObject(this._catalogIDs, this._needTypeIDs, this._objID, oldObjID, this._selectMode);
    return oldObjID != aObjectID ? (object) new ObjectPropertyClass(aObjectID) : (object) new ObjectPropertyClass(oldObjID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    object obj = value;
    if (this._catalogIDs != null && this._catalogIDs.Count > 0 && ServicesManager.GetService(typeof (IImbaseFilterSelector)) is IImbaseFilterSelector service)
    {
      SimplePropDescriptor simplePropDescriptor = (SimplePropDescriptor) null;
      if (context != null && context.Instance is PropDescriptorHolder)
        simplePropDescriptor = this.GetCodeImbasePropertyDescriptor((context.Instance as PropDescriptorHolder).PropDescriptorCollection);
      long objectId = value != null ? (value as ObjectPropertyClass).ObjectID : 0L;
      long recId = this._recID;
      int attrID = 0;
      if (context.PropertyDescriptor is SimplePropDescriptor propertyDescriptor && propertyDescriptor.AttributeValuePropertyClass != null && propertyDescriptor.AttributeValuePropertyClass.AttributeValue != null)
        attrID = propertyDescriptor.AttributeValuePropertyClass.AttributeValue.AttributeID;
      obj = this.EditValue(service, attrID, objectId);
      if (simplePropDescriptor != null && recId != this._recID)
      {
        AttributeValues attributeValue = simplePropDescriptor.AttributeValuePropertyClass.AttributeValue;
        attributeValue.Values = new object[1]
        {
          (object) this._recID
        };
        object pdValue = AttributeValuesEditor.GetPDValue(attributeValue, 0, -1L, AttributableElements.None, string.Empty, (DataTable) null);
        simplePropDescriptor.SetValue(simplePropDescriptor.Component, pdValue);
        simplePropDescriptor.ValueChanged = true;
      }
      else if ((obj as ObjectPropertyClass).ObjectID == objectId)
        obj = value;
    }
    return obj;
  }
}
