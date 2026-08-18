
// Type: Intermech.Navigator.Parts.CompositeBookmark
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Parts;

/// <summary>Закладка для композитного запроса.</summary>
internal class CompositeBookmark
{
  private int queryIndex;
  private object queryBookmark;

  /// <summary>
  /// Создает закладку, позволяя указать порядковый номер подзапроса,
  /// входящего в состав композитного запроса, и закладку, которую
  /// сформировал этот подзапрос.
  /// </summary>
  /// <param name="queryIndex">Порядковый номер подзапроса.</param>
  /// <param name="queryBookmark">Закладка, сформированная подзапросом.</param>
  public CompositeBookmark(int queryIndex, object queryBookmark)
  {
    this.queryIndex = queryIndex;
    this.queryBookmark = queryBookmark;
  }

  /// <summary>
  /// Возвращает порядковый номер подзапроса, входящего в состав
  /// композитного запроса.
  /// </summary>
  public int QueryIndex => this.queryIndex;

  /// <summary>
  /// Возвращает закладку, которую сформировал подзапрос, входящий в состав
  /// композитного запроса.
  /// </summary>
  public object QueryBookmark => this.queryBookmark;
}
