
// Type: Intermech.Navigator.DBObjects.UserNameTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;
using System.Globalization;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Преобразование идентификаторов пользователей в их имена
/// </summary>
public class UserNameTransform : INodeColumnTransform
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
    string empty = string.Empty;
    string userName;
    if (sourceValue is long)
      userName = this._namesCache.GetUserName(Convert.ToInt64(sourceValue));
    else if (GuidHelper.IsGuid(Convert.ToString(sourceValue)))
      userName = this._namesCache.GetUserName(new Guid(Convert.ToString(sourceValue)));
    else if (sourceValue is IConvertible)
    {
      try
      {
        userName = this._namesCache.GetUserName(((IConvertible) sourceValue).ToInt64((IFormatProvider) CultureInfo.CurrentCulture));
      }
      catch
      {
        userName = sourceValue.ToString();
      }
    }
    else
      userName = Convert.ToString(sourceValue);
    return CellValue.GetValue(sourceValue, column, (object) userName);
  }
}
