
// Type: Intermech.Client.Core.Redline.RankGraphsInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Redline;

/// <summary>
/// Класс для хранения результатов выбора пользователем должности и граф для подписей в данной должности
/// </summary>
[System.Diagnostics.DebuggerDisplay("{RankCaption} => ['{DebuggerDisplay, nq}']")]
public class RankGraphsInfo
{
  private string DebuggerDisplay => $"{string.Join("' '", this.Graphs)}";

  /// <summary>Конструктор</summary>
  /// <param name="rankID">ObjectID должности</param>
  /// <param name="rankCaption">Заголовок должности</param>
  /// <param name="graphs">Список граф для подписи</param>
  public RankGraphsInfo(long rankID, string rankCaption, string[] graphs)
  {
    this.Graphs = graphs;
    this.RankCaption = rankCaption;
    this.RankID = rankID;
  }

  /// <summary>ObjectID должности</summary>
  public long RankID { get; private set; }

  /// <summary>Заголовок должности</summary>
  public string RankCaption { get; private set; }

  /// <summary>Массив граф для подписи в данной должности (строковый ид. графы, строковая расшифровка графы)</summary>
  public string[] Graphs { get; private set; }
}
