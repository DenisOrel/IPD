
// Type: Intermech.Navigator.ColumnTransforms.IdToStringTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Cache;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.ColumnTransforms;

/// <summary>
/// Реализует преобразование значения колонки идентификатора в строку.
/// </summary>
internal class IdToStringTransform : INodeColumnTransform
{
  private ICacheManager cache;
  private string emptyValue;

  public IdToStringTransform(ICacheManager cache)
    : this(cache, string.Empty)
  {
  }

  public IdToStringTransform(ICacheManager cache, string emptyValue)
  {
    this.cache = cache;
    this.emptyValue = emptyValue;
  }

  /// <summary>
  /// Возвращает тип значения, образуемого при выполнении преобразования.
  /// </summary>
  public Type DataType => typeof (string);

  /// <summary>Выполнить преобразование</summary>
  /// <param name="sourceValue">Исходные данные</param>
  /// <param name="column">Описание колонки</param>
  /// <param name="mapping">Ссылка на объект типа Intermech.Navigator.Queries.RecordMapping</param>
  /// <param name="allValues">Все допустимые значения в строке с данными</param>
  /// <returns>Новое значение</returns>
  public object Apply(object sourceValue, NodeColumn column, object mapping, object[] allValues)
  {
    object newValue = this.cache[(object) Convert.ToInt32(sourceValue)] ?? (object) this.emptyValue;
    return CellValue.GetValue(sourceValue, column, newValue);
  }
}
