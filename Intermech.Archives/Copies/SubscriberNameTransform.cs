// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.SubscriberNameTransform
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// Преобразование идентификаторов пользователей в их имена
/// </summary>
public class SubscriberNameTransform : INodeColumnTransform
{
  /// <summary>Ссылка на кэш</summary>
  private IUserNamesCache _namesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;

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
    return sourceValue == DBNull.Value ? (object) string.Empty : CellValue.GetValue(sourceValue, column, (object) this._namesCache.GetUserName(Convert.ToInt64(sourceValue)));
  }
}
