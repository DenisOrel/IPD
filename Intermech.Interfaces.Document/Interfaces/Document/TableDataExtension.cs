// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TableDataExtension
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

public static class TableDataExtension
{
  /// <summary>
  /// Определяет, является ли данная таблица таблицей продолжения потока на текущей странице
  /// </summary>
  /// <param name="table">текущая таблица</param>
  /// <returns></returns>
  public static bool IsContinuationTable(this TableData table)
  {
    return table.IsTopLevelTable && table.IsPageFlow && !table.IsStartFlowTable;
  }

  /// <summary>
  /// Определяет, есть ли у данной таблицы таблица продолжения на этой странице
  /// </summary>
  /// <param name="table">текущая таблица</param>
  /// <returns></returns>
  public static bool HasContinuation(this TableData table)
  {
    return table.NextTable != null && table.NextTable.IsContinuationTable();
  }
}
