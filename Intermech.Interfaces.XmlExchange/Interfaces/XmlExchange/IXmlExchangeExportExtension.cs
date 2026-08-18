// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IXmlExchangeExportExtension
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.XmlExchange.Common;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Интерфейс для расширений задачи экспорта данных</summary>
public interface IXmlExchangeExportExtension : IXmlExchangeExtension
{
  /// <summary>
  /// Действия, которые может выполнять расширение задачи экспорта
  /// </summary>
  XmlExportExtAction Actions { get; }

  /// <summary>
  /// Проверить, может ли расширение выполнить указанное действие
  /// </summary>
  /// <param name="action">Проверяемое действие</param>
  /// <returns>true - расширение может выполнить указанное действие</returns>
  bool CanProcess(XmlExportExtAction action);

  /// <summary>Выполнить определенное действие задачи у расширения</summary>
  /// <param name="action">Выполняемое действие</param>
  /// <param name="subTask">Задача экспорта данных</param>
  /// <param name="args">Дополнительные аргументы метода</param>
  /// <returns>Результат выполнения действия в виде ключей-значений</returns>
  Dictionary<string, object> Execute(
    XmlExportExtAction action,
    object subTask,
    params object[] args);

  /// <summary>
  /// Получение связанных объектов для экспорта, а также выгрузка доп. информация о объекте
  /// </summary>
  /// <remarks>Если объект исключается из экспорта в objRecord может быть null </remarks>
  /// <param name="subTask">Подзадача экспорта</param>
  /// <param name="dbObject">Экспортируемый объект</param>
  /// <param name="objRecord">Экспортируемые данные объекта</param>
  /// <param name="objExportData">Параметры обработки объекта</param>
  /// <returns>Список связанных объектов для экспорта типа XmlObjExportData</returns>
  [Obsolete("Будет удалено в версии IPS 7.0")]
  object[] GetObjectLinkedInfo(
    object subTask,
    IDBObject dbObject,
    ObjectRecord objRecord,
    object objExportData);

  /// <summary>
  /// Получение связанных объектов для экспорта, а также выгрузка доп. информация о связи
  /// </summary>
  /// <remarks>Если объект исключается из экспорта в objRecord может быть null </remarks>
  /// <param name="subTask">Подзадача экспорта</param>
  /// <param name="dbRelation">Экспортируемая связь</param>
  /// <param name="relRecord">Экспортируемые данные связи</param>
  /// <param name="partExportData">Параметры обработки связи (дочернего объекта)</param>
  /// <param name="projObjInfo">Информация о родительском объекте</param>
  /// <returns>Список связанных объектов для экспорта типа XmlObjExportData</returns>
  [Obsolete("Будет удалено в версии IPS 7.0")]
  object[] GetRelationLinkedInfo(
    object subTask,
    IDBRelation dbRelation,
    RelationRecord relRecord,
    object partExportData,
    ObjInfoItem projObjInfo);

  /// <summary>
  /// Получение связанны объектов для экспорта, а также выгрузка доп. информация об атрибуте
  /// </summary>
  /// <remarks></remarks>
  /// <param name="subTask">Подзадача экспорта</param>
  /// <param name="dbAttributable">Экспортируемый объект или связь</param>
  /// <param name="objExportData">Параметры обработки объекта / связи</param>
  /// <param name="attrRow">Данные о атрибуте</param>
  /// <param name="attrRecord">Экспортируемые данные атрибута</param>
  /// <returns>Список связанных объектов для экспорта типа XmlObjExportData</returns>
  [Obsolete("Будет удалено в версии IPS 7.0")]
  object[] GetAttributeLinkedInfo(
    object subTask,
    IDBAttributable dbAttributable,
    object objExportData,
    DataRow attrRow,
    object attrRecord);
}
