// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IComponentSelectionCommandService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Клиентская служба по обработке команд для Подборных компонент
/// </summary>
public interface IComponentSelectionCommandService
{
  /// <summary>Создать новый подборный компонент</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="projectIDs">Идентификаторы исполнений в которые входит основной компонент</param>
  /// <param name="relationGuids">Глобальные идентификаторы связей с основным компонентом в исполнениях</param>
  long CreateNew(IUserSession session, long[] projectIDs, Guid[] relationGuids);

  /// <summary>Добавить существующий объект</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="projectIDs">Идентификаторы исполнений в которые входит основной компонент</param>
  /// <param name="relationGuids">Глобальные идентификаторы связей с основным компонентом в исполнениях</param>
  long[] AddExisting(IUserSession session, long[] projectIDs, Guid[] relationGuids);

  /// <summary>Добавить из IMBASE</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="projectIDs">Идентификаторы исполнений в которые входит основной компонент</param>
  /// <param name="relationGuids">Глобальные идентификаторы связей с основным компонентом в исполнениях</param>
  long[] AddFromImbase(IUserSession session, long[] projectIDs, Guid[] relationGuids);

  /// <summary>Сброс подбора для указанного основного компонента</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="projectIDs">Идентификаторы исполнений в которые входит основной компонент</param>
  /// <param name="relationGuids">Глобальные идентификаторы связей с основным компонентом в исполнениях</param>
  void Reset(IUserSession session, long[] projectIDs, Guid[] relationGuids);
}
