
// Type: Intermech.Navigator.DBObjects.ListAttributeTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Класс для преобразователя, который работает со списковыми атрибутами
/// </summary>
/// <summary>Создать экземпляр класса</summary>
/// <param name="AnAttrID">Идентификатор атрибута, по которому идёт преобразование</param>
public class ListAttributeTransform(int AnAttrID) : 
  AttributeTransform(AnAttrID),
  IListAttributeTransform,
  IAttributeTransform,
  INodeColumnTransform
{
  /// <summary>Тип данных атрибута</summary>
  public Type DataType => typeof (string);

  /// <summary>Выполнить преобразование</summary>
  /// <param name="sourceValue">Исходные данные</param>
  /// <param name="column">Описание колонки</param>
  /// <param name="adapter">Ссылка на объект типа Intermech.Navigator.Queries.RecordAdapter</param>
  /// <param name="allValues">Все допустимые значения в строке с данными</param>
  /// <returns>Новое значение</returns>
  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    lock (this._syncRoot)
    {
      if (this._attrMetadata.AttrID != this._attrID || !this._attrMetadata.IsAttrList || this._attrMetadata.AttrPossibleValues == null)
        return sourceValue;
      for (int index = 0; index < this._attrMetadata.AttrPossibleValues.Count; ++index)
      {
        if (this._attrMetadata.AttrPossibleValues[index] is MyElement attrPossibleValue && ObjectsCompareHelper.CompareValues(attrPossibleValue.Value, sourceValue, this._attrMetadata.AttrType))
          return attrPossibleValue.Caption != null ? CellValue.GetValue(sourceValue, column, (object) attrPossibleValue.Caption) : sourceValue;
      }
    }
    return sourceValue;
  }
}
