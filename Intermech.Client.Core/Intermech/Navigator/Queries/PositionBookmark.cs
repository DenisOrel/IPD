
// Type: Intermech.Navigator.Queries.PositionBookmark
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Queries;

/// <summary>
/// Реализует закладку для обозначения начала следующей
/// читаемой порции данных, задаваемого с помощью
/// порядкового индекса первой читаемой записи.
/// </summary>
public class PositionBookmark
{
  private int position;

  /// <summary>Создает закладку.</summary>
  /// <param name="position">Порядковый индекс первой читаемой записи</param>
  public PositionBookmark(int position) => this.position = position;

  /// <summary>Возвращает порядковый индекс первой читаемой записи.</summary>
  public int Position => this.position;
}
