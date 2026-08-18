// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Receptures.IRecepturesService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase.Receptures;

/// <summary>Сервис для работы с таблицами рецептур</summary>
public interface IRecepturesService
{
  /// <summary>Инициализировать кэш рецептур</summary>
  void InitCache();

  /// <summary>Проверка - является ли запись рецептурой</summary>
  /// <param name="recordInfo"></param>
  /// <returns></returns>
  bool RecordHasRecepture(ReceptureItemInfo recordInfo);

  /// <summary>Получение состава рецептуры</summary>
  /// <param name="session"></param>
  /// <param name="recordInfo"></param>
  /// <returns></returns>
  List<Tuple<ReceptureItemInfo, MeasuredValue>> GetReceptureComposition(
    IUserSession session,
    ReceptureItemInfo recordInfo);

  /// <summary>Обновление кэша после редактирования таблицы рецептур</summary>
  /// <param name="session"></param>
  /// <param name="receptureTableId"></param>
  /// <param name="dtData"></param>
  void UpdateCacheAfterTableMixEdit(IUserSession session, long receptureTableId, DataTable dtData);

  /// <summary>Обновление кэша на других серверах приложений</summary>
  /// <param name="session"></param>
  /// <param name="receptureTableId"></param>
  void UpdateCacheOnAnotherServers(IUserSession session, long receptureTableId);
}
