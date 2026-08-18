// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICurrentUserAndRole
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Projects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Navigator;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс, помогающий определять идентификаторы текущего пользователя и роли
/// </summary>
public interface ICurrentUserAndRole
{
  /// <summary>Идентификатор текущего пользователя</summary>
  long UserID { get; }

  /// <summary>Guid текущего пользователя</summary>
  Guid UserGuid { get; }

  /// <summary>Имя текущего пользователя</summary>
  string UserName { get; }

  /// <summary>Идентификатор текущей роли</summary>
  long RoleID { get; }

  /// <summary>Guid текущей роли</summary>
  Guid RoleGuid { get; }

  /// <summary>
  /// Обладает ли текущий пользователь правами администратора
  /// </summary>
  bool IsAdmin { get; }

  /// <summary>Ид. объекта-юзера</summary>
  long ID { get; }

  /// <summary>
  /// Некое уникальное число для текущего сеанса работы клиента IPS с сервером приложений.
  /// Внимание! Выполняется обращение к серверу приложений!
  /// </summary>
  long ClientConnectionID { get; }

  /// <summary>Идентификатор текущего проекта</summary>
  long ProjectID { get; }

  /// <summary>
  /// Идентификатор текущего проекта
  /// (значение кэшировано)
  /// </summary>
  long CachedProjectID { get; }

  /// <summary>
  /// Заблокировано ли изменение текущего контекста редактирования
  /// </summary>
  bool LockEditingContextID { get; set; }

  /// <summary>
  /// Источник информации о текущем контексте редактирования (глобальный, оконный)
  /// </summary>
  EditingContextSource EditingContextSource { get; set; }

  /// <summary>
  /// Источник информации о текущем контексте редактирования (глобальный, оконный)
  /// </summary>
  EditingContextSource CachedEditingContextSource { get; set; }

  /// <summary>
  /// Идентификатор текущего контекста редактирования (глобально для всех сессий пользователя, читается из сессии)
  /// </summary>
  long EditingContextID { get; set; }

  /// <summary>
  /// Проверить, является ли текущий контекст редактирования извещением об изменении
  /// </summary>
  bool IsECOEditingContext { get; }

  /// <summary>
  /// Режим автопополнения контекста редактирования (кэшированное значение)
  /// </summary>
  EditingContextMode CachedContextMode { get; set; }

  /// <summary>
  /// Идентификатор текущего контекста редактирования (кэшированное значение)
  /// </summary>
  long CachedEditingContextID { get; set; }

  /// <summary>
  /// Номер группы изменений текущего контекста редактирования (кэшированное значение)
  /// </summary>
  long CachedEditingContextModificationID { get; }

  /// <summary>
  /// Метод позволяет передать информацию о текущем контексте редактирования на сервер приложений.
  /// В кэш ничего не записывается, из кэша ничего не читается
  /// </summary>
  void ReplaceEditingContext(CurrentEditingContext editingContext);

  /// <summary>
  /// Метод позволяет обновить контекст редактирования в случае переподключения клиентской программы к серверу приложений и прочих ситуациях
  /// </summary>
  void RefreshEditingContext();

  /// <summary>Заблокированы ли настройки контекстных меню</summary>
  bool BlockedMenus { get; }

  /// <summary>
  /// Заблокированы ли настройки видимости закладок Навигатора
  /// </summary>
  bool BlockedViews { get; }

  /// <summary>
  /// Заблокированы ли настройки отображения для узлов, содержащих составы
  /// </summary>
  bool BlockedCompositions { get; }

  /// <summary>Заблокированы ли панели инструментов составов</summary>
  bool BlockedToolbars { get; }

  /// <summary>
  /// Видна ли панель инструментов "Контекст редактирования"
  /// </summary>
  bool IsContextToolbarVisible { get; }

  /// <summary>Режим работы текущего контекста редактирования</summary>
  EditingContextMode EditingContextMode { get; set; }

  /// <summary>
  /// Можно ли выбрать режим автоматического пополнения для указанного контекста редактирования
  /// </summary>
  /// <param name="contextID">Идентификатор версии объекта с контекстом</param>
  /// <returns>Можно включить режим автоматического пополнения указанного контекста редактирования или нет</returns>
  CanSetContextModeCode CanSetContextAutoUpdateMode(long contextID);

  /// <summary>
  /// Можно ли оставить режим автоматического пополнения для указанного контекста редактирования
  /// </summary>
  /// <param name="contextID">Идентификатор версии объекта с контекстом</param>
  /// <returns>true - можно оставить режим автоматического пополнения указанного контекста редактирования</returns>
  bool CanLeaveContextAutoUpdateMode(long contextID);

  /// <summary>
  /// Способ фильтрации списков объектов в зависимости от их принадлежности к проектам
  /// </summary>
  ProjectFiltrationModes ProjectFiltrationMode { get; }

  /// <summary>
  /// Способ фильтрации списков объектов в зависимости от их принадлежности к проектам
  /// (значение кэшировано)
  /// </summary>
  ProjectFiltrationModes CachedProjectFiltrationMode { get; }

  /// <summary>Текущее правило по сортировке и отображению составов</summary>
  CompositionsAutosortRule Rule { get; set; }

  /// <summary>
  /// Применяются ли события в текущем правиле по сортировке и отображению составов
  /// </summary>
  bool UseRuleEvents { get; set; }

  /// <summary>Настройки видов Навигатора для текущей роли</summary>
  Dictionary<NavigatorColumnsKey, NavigatorColumns> RoleNavStreams { get; set; }

  /// <summary>Пакет колонок по-умолчанию</summary>
  ColumnPack DefaultColumnPack { get; }

  /// <summary>
  /// Установить текущее значение проекта и режим отображения объектов в проекте
  /// </summary>
  /// <param name="projectID">Идентификатор текущего проекта</param>
  /// <param name="projectFiltrationMode">Способ фильтрации списков объектов в зависимости от их принадлежности к проектам</param>
  /// <param name="silentMode">Тихий режим позволяет избежать ругани при загрузке клиента, когда клиент пытается включить сохраненный проект, который уже удалили или который имеет неверный уровень доступа</param>
  void SetCurrentProject(
    long projectID,
    ProjectFiltrationModes projectFiltrationMode,
    bool silentMode = false);

  /// <summary>Режим работы без диалога с пользователем</summary>
  bool SilentMode { get; set; }

  /// <summary>Флаг того, что текущий IPS Client работает с порталом</summary>
  bool PortalClient { get; set; }

  bool EnabledPdmConfigurator { get; set; }

  /// <summary>Размер пакета данных</summary>
  int MaxRows { get; }

  /// <summary>Перечитывает размер пакета</summary>
  void ReloadMaxRows();

  /// <summary>Режим разработчика</summary>
  bool DeveloperMode { get; }
}
