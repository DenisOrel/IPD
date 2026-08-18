// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseSynchObjectsService
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
/// Интерфейс для синхронизации выбранных объектов со справочником IMBASE.
/// </summary>
public interface IImbaseSynchObjectsService
{
  /// <summary>
  /// Синхронизация перечня объектов, сгруппированных по типу
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="objDict">Список выделенных объектов, которые необходимо синхронизировать, сгруппированых по типам</param>
  /// <param name="createVersion">Необходимость создания новой версии</param>
  /// <param name="bindingAttrID">Атрибут для связи объектов с IMBASE (для объектов не связанных с IMBASE)</param>
  void SynchronizeObjects(
    Guid sessionGuid,
    Guid taskGuid,
    Dictionary<int, List<long>> objDict,
    bool createVersion,
    int bindingAttrID = 0);

  /// <summary>Синхронизация всех объектов, указанного типа</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="typeID">Тип синхронизируемых объектов</param>
  /// <param name="createVersion">Необходимость создания новой версии</param>
  /// <param name="bindingAttrID">Атрибут для связи объектов с IMBASE (для объектов не связанных с IMBASE)</param>
  void SynchronizeObjects(
    Guid sessionGuid,
    Guid taskGuid,
    int typeID,
    bool createVersion,
    int bindingAttrID = 0);

  /// <summary>Получить информацию об обработанных объектах.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="count">Общее количество элементов</param>
  /// <param name="current">Количество обработанных элементов</param>
  /// <returns>Таблица с информацией об обработанных объектах</returns>
  DataTable GetInfoAboutObjectsProcessed(Guid taskGuid, out int count, out int current);

  /// <summary>Остановить выполнение задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  void StopTask(Guid taskGuid);
}
