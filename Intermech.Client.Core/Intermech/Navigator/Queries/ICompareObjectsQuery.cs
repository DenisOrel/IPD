
// Type: Intermech.Navigator.Queries.ICompareObjectsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System.Data;


namespace Intermech.Navigator.Queries;

/// <summary>Интерфейс, который должны поддерживать query, используемые при выборе дочерних данных в ноде, которая используется для
/// сравнения актуального состава с сохранённым</summary>
public interface ICompareObjectsQuery
{
  /// <summary>Полученный набор данных</summary>
  [CanBeNull]
  DataTable DataTable { get; }

  /// <summary>Номер колонки в полученном наборе данных, в котором хранится идентификатор связи</summary>
  int PrjLinkIDColumnNum { get; }

  /// <summary>Номер колонки в полученном наборе данных, в котором должен хранится результат сравнения составов</summary>
  int CompareResultColumnNum { get; }

  /// <summary>True, если выборка выбирает данные из актуального состава, иначе - из сохранённого</summary>
  bool ActualComposition { get; }
}
