// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DistributeResult
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Результат распределения данных по странице</summary>
public enum DistributeResult
{
  /// <summary>Не удалось распределить</summary>
  None,
  /// <summary>Удалось распределить в заданную область часть данных</summary>
  Part,
  /// <summary>Элемент был удалён, как пустой "хвост" цепочки</summary>
  Deleted,
  /// <summary>Распределение должно вернуться к предыдущей странице.
  /// <remarks> Срабатывает когда элемент на "Последней" странице,
  /// или не удалось сохранить ячейку TryNotBreak целой на следующей странице</remarks>
  /// </summary>
  BackToPrevious,
  /// <summary>Удалось распределить все оставшиеся данные в заданную область</summary>
  All,
}
