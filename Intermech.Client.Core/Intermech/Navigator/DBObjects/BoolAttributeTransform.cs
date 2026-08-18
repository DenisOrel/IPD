
// Type: Intermech.Navigator.DBObjects.BoolAttributeTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Класс для преобразователя, который работает со логическими атрибутами
/// </summary>
public class BoolAttributeTransform : 
  IBoolAttributeTransform,
  IAttributeTransform,
  INodeColumnTransform
{
  /// <summary>Внутреннее поле для синхронизации</summary>
  protected object _syncRoot = new object();
  /// <summary>
  /// Идентификатор атрибута, по которому идёт преобразование
  /// </summary>
  public int _attrID = -1;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="AnAttrID">Идентификатор атрибута, по которому идёт преобразование</param>
  public BoolAttributeTransform(int AnAttrID) => this._attrID = AnAttrID;

  /// <summary>Идентификатор атрибута</summary>
  public int AttrID
  {
    get => this._attrID;
    set
    {
      if (this._attrID == value)
        return;
      lock (this._syncRoot)
        this._attrID = value;
    }
  }

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
      if (MetaDataHelper.GetAttributeType(this._attrID).RealFieldType != FieldTypes.ftBoolean || sourceValue == null || sourceValue == DBNull.Value)
        return sourceValue;
      if (Convert.ToBoolean(sourceValue))
        return CellValue.GetValue(sourceValue, column, (object) LocalizationHolder.rm.GetString("Client.Core_246"));
      if (!Convert.ToBoolean(sourceValue))
        return CellValue.GetValue(sourceValue, column, (object) LocalizationHolder.rm.GetString("Client.Core_247"));
    }
    return sourceValue;
  }
}
