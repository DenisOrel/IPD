// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IFindIPSObjectsService
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.IpsXmlViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Серверный сервис для поиска существующих объектов IPS по описаниям объектов из индекса XML
/// </summary>
public interface IFindIPSObjectsService
{
  /// <summary>
  /// Отыскать в конфигурации импорта правила поиска указанного типа объектов
  /// </summary>
  /// <param name="kernel">Микроядро XML</param>
  /// <param name="xmlObj">Искомый объект</param>
  /// <returns>Список узлов Consts.xmlNodeImportAttribute ([attribute]),
  /// содержащих перечень атрибутов, по которым требуется искать объект</returns>
  XmlImportBase GetObjectSearchRules(IKernel kernel, IImObject xmlObj);

  /// <summary>
  /// Отыскать в конфигурации импорта правила импорта указанного типа объектов
  /// </summary>
  /// <param name="kernel">Микроядро XML</param>
  /// <param name="xmlObj">Искомый объект</param>
  /// <returns>Список узлов Consts.xmlNodeImportAttribute ([attribute]),
  /// содержащих перечень атрибутов, по которым требуется импортировать объект</returns>
  XmlImportBase GetObjectImportRules(IKernel kernel, IImObject xmlObj);

  /// <summary>
  /// Отыскать в конфигурации импорта правила создания указанного типа объектов
  /// </summary>
  /// <param name="kernel">Микроядро XML</param>
  /// <param name="xmlObj">Искомый объект</param>
  /// <returns>Список узлов Consts.xmlNodeImportAttribute ([attribute]),
  /// содержащих перечень атрибутов, по которым требуется создавать объект</returns>
  XmlImportBase GetObjectCreateRules(IKernel kernel, IImObject xmlObj);

  /// <summary>
  /// Отыскать идентификатор версии объекта IPS по его идентификатору в базе данных индекса
  /// </summary>
  /// <param name="kernel">Микроядро XML</param>
  /// <param name="xmlObjectId">Идентификатор версии объекта в индексе</param>
  /// <returns>Идентификатор версии объекта в IPS или -1</returns>
  long GetIPSObjectID(IKernel kernel, long xmlObjectId);

  /// <summary>
  /// Отыскать существующий объект IPS по его описанию из индекса XML.
  /// Правила поиска будут взяты из настроек импорта, которые могут храниться
  /// в сервисах микроядра. Если настроек нет, будут применены правила поиска
  /// по умолчанию
  /// </summary>
  /// <param name="kernel">Микроядро XML</param>
  /// <param name="xmlObj">Описание искомого объекта из индекса XML</param>
  /// <returns>Описание найденного объекта или null</returns>
  IDBObject FindIPSObject(IKernel kernel, IImObject xmlObj);

  /// <summary>Отыскать существующий объект IPS по его Guid</summary>
  /// <param name="session">Сессия</param>
  /// <param name="guid">Уникальный глобальный идентификатор версии объекта</param>
  /// <returns>Найденный объект или null</returns>
  IDBObject FindByGuid(IUserSession session, Guid guid);

  /// <summary>Поиск в базе IPS объекта по глобальному ид-ру</summary>
  /// <param name="session">Сессия</param>
  /// <param name="guid">Глобальный ид-р объекта (не версии)</param>
  /// <param name="versionRule"></param>
  /// <returns></returns>
  IDBObject FindByIdGUID(IUserSession session, Guid guid, string versionRule);

  /// <summary>Отыскать объект IPS по всем указанным атрибутам</summary>
  /// <param name="session">Сессия</param>
  /// <param name="kernel">Микроядро XML</param>
  /// <param name="obj">Описание искомого объекта из индекса XML</param>
  /// <param name="ruleAttrItems">Настройки правила поиска для атрибутов</param>
  /// <param name="extParams"> Дополнительные параметры поиска объекта
  /// "logicalAND"   : True - атрибуты объединяются в условия оператором "И", false - оператором "ИЛИ"
  /// "searchObjType": Гл. ид. типа объекта по которому ищем объекты в базе, если он не задан тип определяем у самого объекта
  /// </param>
  /// <returns>Найденный объект или null</returns>
  IDBObject FindByAttributes(
    IUserSession session,
    IKernel kernel,
    IImObject obj,
    List<XmlImportBase> ruleAttrItems,
    HybridDictionary extParams = null);

  /// <summary>
  /// Вернуть Guid папки ImBase для импорта (из конфигурации или Guid системной папки)
  /// </summary>
  /// <param name="kernel">Микроядро XML</param>
  /// <returns>Guid папки ImBase для импорта (из конфигурации или Guid системной папки)</returns>
  Guid GetCommonImbaseFolder(IKernel kernel);

  /// <summary>
  /// Вернуть Guid таблицы ImBase для импорта (из конфигурации или Guid системной таблицы)
  /// </summary>
  /// <param name="kernel">Микроядро XML</param>
  /// <returns>Guid таблицы ImBase для импорта (из конфигурации или Guid системной таблицы)</returns>
  Guid GetCommonImbaseTable(IKernel kernel);
}
