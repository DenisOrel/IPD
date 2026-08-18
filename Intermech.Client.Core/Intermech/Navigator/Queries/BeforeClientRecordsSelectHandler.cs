
// Type: Intermech.Navigator.Queries.BeforeClientRecordsSelectHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Queries;

/// <summary>
/// Обработчик события, срабатывающего перед получением записей в методе GetDataTable у Query.
/// Позволяет модифицировать параметры запроса с помощью args.Parameters или отменить запрос с помощью исключения.
/// </summary>
/// <param name="sender">sender - Query, у которой вызвали GetDataTable</param>
/// <param name="args">Аргументы события</param>
public delegate void BeforeClientRecordsSelectHandler(
  object sender,
  BeforeClientRecordsSelectEventArgs args);
