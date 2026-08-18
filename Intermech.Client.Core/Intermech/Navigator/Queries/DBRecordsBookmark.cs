
// Type: Intermech.Navigator.Queries.DBRecordsBookmark
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Реализует закладку для обозначения начала следующей читаемой порции данных,
/// задаваемого с помощью значений ключевого поля и первого поля, по которому
/// выполнена сортировке.
/// </summary>
/// <remarks>
/// Эта закладка используется для реализации запросов к источникам данных,
/// поддерживающим интерфейс IDBRecords.
/// </remarks>
internal class DBRecordsBookmark
{
  private long keyValue;
  private List<object> orderValue;

  /// <summary>Создает закладку.</summary>
  /// <param name="keyValue">
  /// Значение ключевого поля источника данных,
  /// взятое из последней прочитанной записи
  /// </param>
  /// <param name="orderValue">
  /// Значение первого поля, по которому выполнена сортировка,
  /// взятое из последней записи
  /// </param>
  public DBRecordsBookmark(long keyValue, List<object> orderValue)
  {
    this.keyValue = keyValue;
    this.orderValue = orderValue;
  }

  /// <summary>
  /// Возвращает значение ключевого поля источника данных,
  /// взятое из последней прочитанной записи.
  /// </summary>
  public long KeyValue => this.keyValue;

  /// <summary>
  /// Возвращает значение первого поля, по которому выполнена сортировка,
  /// взятое из последней записи.
  /// </summary>
  public List<object> OrderValue => this.orderValue;
}
