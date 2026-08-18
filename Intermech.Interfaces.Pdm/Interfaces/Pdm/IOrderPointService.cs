// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IOrderPointService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Серверная служба для работы с точками заказов</summary>
public interface IOrderPointService
{
  /// <summary>
  /// Получить развернутый состав сборочной единицы типом связи "Состав изделия".
  /// </summary>
  /// <param name="sessionGuid">Сессия</param>
  /// <param name="assemblyUnitObjectID">Object_ID сборочной единицы</param>
  /// <returns>Развернутый состав сборочной единицы по связи "Состав изделия"</returns>
  Dictionary<long, long> GetDeployedCompositionInfo(Guid sessionGuid, long assemblyUnitObjectID);

  /// <summary>Получить список точек заказов для сборочной единицы</summary>
  /// <param name="assemblyUnitObjectID">ID сборочной единицы</param>
  /// <returns></returns>
  List<long> GetOrderPoints(Guid sessionGuid, long assemblyUnitObjectID);

  /// <summary>Получить непосредственный состав точки заказа</summary>
  /// <param name="sessionGuid">Сессия.</param>
  /// <param name="pointID">ID точки заказа</param>
  /// <returns></returns>
  List<long> GetPointComposition(Guid sessionGuid, long pointID);
}
