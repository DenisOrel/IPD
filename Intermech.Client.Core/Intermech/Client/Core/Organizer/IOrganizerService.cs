
// Type: Intermech.Client.Core.Organizer.IOrganizerService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Сервис для работы с нодами, которые должны являться дочерними нодами OrganizerRootNode.
/// </summary>
public interface IOrganizerService
{
  /// <summary>Регистрация подузла узла "Органайзер".</summary>
  /// <param name="objTypeGuid">GUID узла</param>
  /// <param name="objTypeID">Идентификатор типа объектов, которые будух входить в данный узел</param>
  /// <param name="conditions">Условия выбора объектов</param>
  /// <param name="columns">Коллекция отображаемых колонок</param>
  /// <param name="caption">Наименование узла</param>
  /// <param name="icoIndex">Индекс иконки для узла в ICategoryTypeIconService</param>
  IDescriptor RegisterNode(
    Guid objTypeGuid,
    int objTypeID,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    int icoIndex);

  /// <summary>Регистрация подузла узла "Органайзер".</summary>
  /// <param name="objTypeGuid">GUID узла</param>
  /// <param name="objTypeID">Идентификатор типа объектов, которые будух входить в данный узел</param>
  /// <param name="conditions">Условия выбора объектов</param>
  /// <param name="columns">Коллекция отображаемых колонок</param>
  /// <param name="caption">Наименование узла</param>
  /// <param name="icoIndex">Индекс иконки для узла в ICategoryTypeIconService</param>
  /// <param name="requiredCommans">Команды контекстного меню, которые необходимо добавить для элементов подузла узла "Органайзер"</param>
  /// <param name="superfluousCommands">Команды контекстного меню, которые необходимо убрать для элементов подузла узла "Органайзер"</param>
  /// <param name="requiredViews">Вложенные закладки, которые необходимо добавить для элементов подузла узла "Органайзер"</param>
  /// <param name="superfluousViews">Вложенные закладки, которые необходимо убрать для элементов подузла узла "Органайзер"</param>
  IDescriptor RegisterNode(
    Guid objTypeGuid,
    int objTypeID,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    int icoIndex,
    Dictionary<string, CommandInfo> requiredCommans,
    List<string> superfluousCommands,
    Dictionary<string, ViewInfo> requiredViews,
    List<string> superfluousViews);

  /// <summary>Регистрация подузла узла "Органайзер".</summary>
  /// <param name="objTypeGuid">GUID узла</param>
  /// <param name="objTypeID">Идентификатор типа объектов, которые будух входить в данный узел</param>
  /// <param name="conditions">Условия выбора объектов</param>
  /// <param name="columns">Коллекция отображаемых колонок</param>
  /// <param name="caption">Наименование узла</param>
  /// <param name="ico">Иконка для узла</param>
  /// <param name="requiredCommans">Команды контекстного меню, которые необходимо добавить для элементов подузла узла "Органайзер"</param>
  /// <param name="superfluousCommands">Команды контекстного меню, которые необходимо убрать для элементов подузла узла "Органайзер"</param>
  /// <param name="requiredViews">Вложенные закладки, которые необходимо добавить для элементов подузла узла "Органайзер"</param>
  /// <param name="superfluousViews">Вложенные закладки, которые необходимо убрать для элементов подузла узла "Органайзер"</param>
  IDescriptor RegisterNode(
    Guid objTypeGuid,
    int objTypeID,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    Icon ico,
    Dictionary<string, CommandInfo> requiredCommans,
    List<string> superfluousCommands,
    Dictionary<string, ViewInfo> requiredViews,
    List<string> superfluousViews);

  /// <summary>
  /// Регистрация подузла узла "Органайзер", который выбирает данные при помощи DBRelationCollection
  /// </summary>
  /// <param name="nodeGuid">GUID узла</param>
  /// <param name="relTypeID">Тип связи, по которой производится поиск</param>
  /// <param name="objTypeID">Тип объекта, применяемость которого ищется</param>
  /// <param name="objTypeIDs">Типы объектов в составе</param>
  /// <param name="conditions">Условия</param>
  /// <param name="columns">Коллекция отображаемых колонок</param>
  /// <param name="caption">Наименование узла</param>
  /// <param name="icoIndex">Иконка для узла</param>
  IDescriptor RegisterNode(
    Guid nodeGuid,
    int relTypeID,
    int objTypeID,
    int[] objTypeIDs,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    int icoIndex);

  /// <summary>
  /// Регистрация типа объектов, о которых необходимо напоминать пользователю.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов</param>
  /// <param name="conditions">Условия, по которым выбираются объекты о которых необходимо напоминать</param>
  void RegisterTypeForReminder(int objTypeID, ConditionStructure[] conditions);
}
