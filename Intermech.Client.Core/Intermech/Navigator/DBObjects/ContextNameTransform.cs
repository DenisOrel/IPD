
// Type: Intermech.Navigator.DBObjects.ContextNameTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Преобразование идентификаторов проектов в их названия</summary>
public class ContextNameTransform : INodeColumnTransform
{
  /// <summary>Ссылка на кэш</summary>
  private IContextNamesCache _namesCache = CacheManager.Cache("ContextNamesCache") as IContextNamesCache;

  /// <summary>Тип данных</summary>
  public Type DataType => typeof (string);

  /// <summary>Выполнить преобразование</summary>
  /// <param name="sourceValue">Исходные данные</param>
  /// <param name="column">Описание колонки</param>
  /// <param name="adapter">Ссылка на объект типа Intermech.Navigator.Queries.RecordAdapter</param>
  /// <param name="allValues">Все допустимые значения в строке с данными</param>
  /// <returns>Новое значение</returns>
  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    return CellValue.GetValue(sourceValue, column, (object) this._namesCache.GetContextName(Convert.ToInt64(sourceValue)));
  }
}
