// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ITablesDisplayService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Интерфейс для работы с настройками отображения таблиц IMBASE.
/// </summary>
public interface ITablesDisplayService
{
  /// <summary>
  /// Получение режима отображения таблицы. Может быть "Общий", "Персональный", "Для роли".
  /// </summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <returns>Режима отображения таблицы</returns>
  DisplayMode GetDisplayModeForUser(Guid userGuid);

  /// <summary>Удаление режима отображения для пользователя.</summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  void RemoveDisplayModeForUser(Guid userGuid);

  /// <summary>Получение глобального идентификатора колонки.</summary>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <param name="mode">Режим сортировки</param>
  /// <returns>Глобальный идентификатор колонки</returns>
  Guid GetSortedColumnGuid(Guid objGuid, Guid userGuid, out string mode);

  /// <summary>Удаление колонки для сортировки для пользователя.</summary>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  void RemoveObjectSortedColumnForUser(Guid objGuid, Guid userGuid);

  /// <summary>Получение общих настроек отображения.</summary>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <returns>Настройки отображения</returns>
  string GetGeneralSettingsForObject(Guid objGuid);

  /// <summary>Получение настроек отображения объекта для роли.</summary>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="roleGuids">Список глобальных идентификаторов ролей</param>
  /// <returns>Настройки отображения</returns>
  string GetObjectSettingsForRoles(Guid objGuid, List<Guid> roleGuids);

  /// <summary>Удаление настроек отображения объекта для ролей.</summary>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="roleGuids">Список глобальных идентификаторов ролей</param>
  void RemoveObjectSettingsForRoles(Guid objGuid, List<Guid> roleGuids);

  /// <summary>
  /// Получение настроек отображения объекта для пользователя.
  /// </summary>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <returns>Настройки отображения</returns>
  string GetObjectSettingsForUser(Guid objGuid, Guid userGuid);

  /// <summary>
  /// Удаление настроек отображения объекта для пользователей.
  /// </summary>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="userGuids">Список глобальных идентификаторов пользователей</param>
  void RemoveObjectSettingsForUsers(Guid objGuid, List<Guid> userGuids);

  /// <summary>Удаление настроект отображения объекта.</summary>
  /// <param name="objGuids">Список глобальных идентификаторов объектов</param>
  void RemoveSettingsForObject(List<Guid> objGuids);

  /// <summary>
  /// Удаление настроек отображения для всех объектов связанных с указанными ролями.
  /// </summary>
  /// <param name="roleGuids">Список глобальных идентификаторов ролей</param>
  void RemoveSettingsForRole(List<Guid> roleGuids);

  /// <summary>
  /// Удаление настроек отображения для всех объектов связанных с указанными пользователями.
  /// </summary>
  /// <param name="userGuids">Список глобальных идентификаторов пользователей</param>
  void RemoveSettingsForUser(List<Guid> userGuids);

  /// <summary>Сохранение настроек отображения объектов.</summary>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <param name="sortedColumn">Колонка, по которой происходила сортировка</param>
  /// <param name="mode">Режим сортировки</param>
  /// <param name="displayMode">Режима отображения таблицы</param>
  /// <param name="gSettings">Настройки отображения для общего режима</param>
  /// <param name="uSettings">Настройки отображения для пользователя</param>
  /// <param name="rSettings">Настройки отображения для роли</param>
  void SaveSettingsForObject(
    Guid objGuid,
    Guid userGuid,
    Guid sortedColumn,
    string mode,
    DisplayMode displayMode,
    string gSettings,
    string uSettings,
    string rSettings);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sourceObjGuid"></param>
  /// <param name="targetObjGuid"></param>
  /// <returns></returns>
  void CloneSettings(Guid sourceObjGuid, Guid targetObjGuid);
}
