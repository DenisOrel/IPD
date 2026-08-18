
// Type: Intermech.Navigator.Queries.QueryEvents
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Queries;

/// <summary>
/// Вспомогательный статический класс для вызова событий от Queries
/// </summary>
public static class QueryEvents
{
  /// <summary>
  /// Событие генерируется перед получением записей в методе GetDataTable у Query и позволяет
  /// модифицировать условия получения записей
  /// </summary>
  public static event BeforeClientRecordsSelectHandler BeforeClientRecordsSelectEvent;

  /// <summary>
  /// Вызвать подписчиков события BeforeClientRecordsSelectHandler
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="args">Арументы события</param>
  public static void FireBeforeClientRecordsSelect(
    object sender,
    BeforeClientRecordsSelectEventArgs args)
  {
    if (QueryEvents.BeforeClientRecordsSelectEvent == null)
      return;
    QueryEvents.BeforeClientRecordsSelectEvent(sender, args);
  }
}
