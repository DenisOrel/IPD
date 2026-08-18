// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ICompareBackgroundReader
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Интерфейс на поток выполняющий запрос состава для сравнения его с другими составами
/// </summary>
public interface ICompareBackgroundReader : IBackgroundReader
{
  /// <summary>Запуск выполнения запроса</summary>
  /// <param name="mapping">Схема соответствия виртуальных колонок и полей данных</param>
  /// <param name="info">Доп. информация</param>
  /// <param name="objectIDs">Сравниваемые объекты</param>
  /// <param name="scheme">Виртуальная схема для поиска состава</param>
  void Execute(
    object mapping,
    CompareObjectsInfo info,
    List<Tuple<long, int>> objectIDs,
    RuntimeSearchScheme scheme);
}
