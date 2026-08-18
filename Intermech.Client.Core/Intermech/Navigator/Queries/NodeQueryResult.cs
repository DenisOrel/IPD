
// Type: Intermech.Navigator.Queries.NodeQueryResult
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Queries;

/// <summary>
/// Описывает результат выполнения запроса к источнику данных.
/// </summary>
public class NodeQueryResult
{
  private object bookmark;
  private int recordCount;
  private long totalCount;
  private object[] fieldsOrder;
  private static readonly NodeQueryResult emptyQueryResult = new NodeQueryResult(0, 0L, (object[]) null);

  /// <summary>
  /// Создает описатель результата выполнения запроса к источнику данных.
  /// </summary>
  /// <param name="bookmark">
  /// Закладка, определяющая позицию для чтения следующей порции данных.
  /// Если она равна null, то данные прочины полностью
  /// </param>
  /// <param name="recordCount">Количество прочитанных записей</param>
  /// <param name="totalCount"></param>
  /// <param name="fieldsOrder">
  /// Массив идентификаторов полей данных, расположенных в том порядке,
  /// в котором их вернул источник данных
  /// </param>
  public NodeQueryResult(object bookmark, int recordCount, long totalCount, object[] fieldsOrder)
  {
    this.bookmark = bookmark;
    this.recordCount = recordCount;
    this.totalCount = totalCount;
    this.fieldsOrder = fieldsOrder;
  }

  /// <summary>
  /// Создает описатель результата выполнения запроса к источнику данных,
  /// в результате которого были прочитаны все оставшиеся записи (т.е.
  /// закладка для чтения следующей порции равна null.
  /// </summary>
  /// <param name="recordCount">Количество прочитанных записей</param>
  /// 
  ///             Массив идентификаторов полей данных, расположенных в том порядке,
  ///             в котором их вернул источник данных
  ///             <param name="totalCount"> </param>
  /// <param name="fieldsOrder"> </param>
  public NodeQueryResult(int recordCount, long totalCount, object[] fieldsOrder)
    : this((object) null, recordCount, totalCount, fieldsOrder)
  {
  }

  /// <summary>
  /// Возвращает закладку для чтения следующей порции данных. Если она
  /// равна null, то данные прочины полностью.
  /// </summary>
  public object Bookmark => this.bookmark;

  /// <summary>Возвращает количество прочитанных записей.</summary>
  public int RecordCount => this.recordCount;

  /// <summary>Возвращает количество всех записей.</summary>
  public long TotalCount => this.totalCount;

  /// <summary>
  /// Возвращает массив идентификаторов полей данных, расположенных в том порядке,
  /// в котором их вернул источник данных.
  /// </summary>
  public object[] FieldsOrder => this.fieldsOrder;

  /// <summary>
  /// Возвращает описатель выполнения запроса к пустому источнику данных.
  /// </summary>
  public static NodeQueryResult Empty => NodeQueryResult.emptyQueryResult;
}
