// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IInverseImbaseSynchObjectsService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
public interface IInverseImbaseSynchObjectsService
{
  /// <summary>Получить информацию об обработанных объектах.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="count">Общее количество элементов</param>
  /// <param name="current">Количество обработанных элементов</param>
  /// <returns>Таблица с информацией об обработанных объектах</returns>
  DataTable GetInfoAboutObjectsProcessed(Guid taskGuid, out int count, out int current);

  /// <summary>Остановить выполнение задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  void StopTask(Guid taskGuid);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sessionGuid"></param>
  /// <param name="taskGuid"></param>
  /// <param name="objIDs"></param>
  /// <param name="attrIDs"></param>
  void UpdateInfo(Guid sessionGuid, Guid taskGuid, List<long> objIDs, List<int> attrIDs);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sessionGuid"></param>
  /// <param name="taskGuid"></param>
  /// <param name="objTypeID"></param>
  /// <param name="attrIDs"></param>
  void UpdateInfo(Guid sessionGuid, Guid taskGuid, int objTypeID, List<int> attrIDs);
}
