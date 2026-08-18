// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImMetaData
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Метаданные из файла "Портфеля IPS"</summary>
public interface IImMetaData : IAssignable
{
  /// <summary>Контейнер сервисов</summary>
  IServiceProvider Services { get; }

  /// <summary>
  /// Объект для потокобезопасного доступа к содержимому класса
  /// </summary>
  object SyncRoot { get; }

  /// <summary>
  /// В данном словарике хранятся соответствия [Guid чего-то] =&gt; [ImGlobals - информация о типе метаданных]
  /// </summary>
  IDictionary<Guid, ImGlobals> GlobalsGuid { get; }

  /// <summary>
  /// В данном словарике хранятся краткие описания типов объектов
  /// [ID типа объекта] =&gt; [ImObjectType - описание типа объекта]
  /// </summary>
  IDictionary<int, IImObjectType> ObjectTypes { get; }

  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа объекта] =&gt; [ID типа объекта]
  /// </summary>
  IDictionary<Guid, int> ObjectsGuid2ID { get; }

  /// <summary>
  /// В данном словарике хранятся краткие описания типов связей
  /// [ID типа связи] =&gt; [ImRelationType - краткое описание типа связи]
  /// </summary>
  IDictionary<int, IImRelationType> RelationTypes { get; }

  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа связи] =&gt; [ID типа связи]
  /// </summary>
  IDictionary<Guid, int> RelationsGuid2ID { get; }

  /// <summary>
  /// В данном словарике хранятся краткие описания типов атрибутов
  /// [ID типа атрибута] =&gt; [ImAttributeType - описание типа атрибута]
  /// </summary>
  IDictionary<int, IImAttributeType> AttrTypes { get; }

  /// <summary>
  /// В данном словарике хранятся соответствия [Guid типа атрибута] =&gt; [ID типа атрибута]
  /// </summary>
  IDictionary<Guid, int> AttrsGuid2ID { get; }

  /// <summary>
  /// В данном словарике хранятся соответствия имён атрибутов их идентификаторам
  /// [Имя типа атрибута] =&gt; [Int32 идентификатор типа атрибута]
  /// </summary>
  IDictionary<string, int> AttrNameTypes { get; }

  /// <summary>
  /// Возвращает идентификатор типа объектов по строковому представлению его глобального идентификатора
  /// </summary>
  /// <param name="Guid">Guid типа объекта в виде строки</param>
  int GetObjectTypeID(string Guid);

  /// <summary>Получить по Guid типа объекта его Int32-идентификатор</summary>
  /// <param name="objTypeGuid">Guid типа объекта</param>
  /// <returns>Идентификатор типа объекта. -1 - тип объекта не найден</returns>
  int GetObjectTypeID(Guid objTypeGuid);

  /// <summary>
  /// Получить по Int32-идентификатору типа объекта его Guid-идентификатор
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Идентификатор типа объекта. Guid.Empty - тип объекта не найден</returns>
  Guid GetObjectTypeGuid(int objTypeID);

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе объекта
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>true, если тип объекта существует</returns>
  bool ExistsObjectType(int objTypeID);

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе объекта
  /// </summary>
  /// <param name="objTypeGuid">Guid типа объекта</param>
  /// <returns>true, если тип объекта существует</returns>
  bool ExistsObjectType(Guid objTypeGuid);

  /// <summary>Получить краткую информацию о типе объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Краткая информация о типе объекта или null</returns>
  IImObjectType GetObjectType(int objTypeID);

  /// <summary>Получить краткую информацию о типе объекта</summary>
  /// <param name="objTypeGuid">Идентификатор типа объекта</param>
  /// <returns>Краткая информация о типе объекта или null</returns>
  IImObjectType GetObjectType(Guid objTypeGuid);

  /// <summary>Получить название типа объектов (например, "Детали")</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Название типа объектов (например, "Детали")</returns>
  string GetObjectTypeName(int objTypeID);

  /// <summary>Получить название типа объектов (например, "Детали")</summary>
  /// <param name="objTypeGuid">Идентификатор типа объекта</param>
  /// <returns>Название типа объектов (например, "Детали")</returns>
  string GetObjectTypeName(Guid objTypeGuid);

  /// <summary>Получить список описаний всех типов объектов</summary>
  /// <returns>Список описаний всех типов объектов</returns>
  IList<IImObjectType> GetObjectTypesList();

  /// <summary>Получить по Guid типа связи его Int32-идентификатор</summary>
  /// <param name="relTypeGuid">Guid типа связи</param>
  /// <returns>Идентификатор типа связи. -1 - тип связи не найден</returns>
  int GetRelationTypeID(Guid relTypeGuid);

  /// <summary>
  /// Получить по Int32-идентификатору типа связи её Guid-идентификатор
  /// </summary>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <returns>Идентификатор типа связи. Guid.Empty - тип связи не найден</returns>
  Guid GetRelationTypeGuid(int relTypeID);

  /// <summary>
  /// Возвращает идентификатор типа связи по строковому представлению её глобального идентификатора
  /// </summary>
  /// <param name="Guid">Guid типа связи в виде строки</param>
  int GetRelationTypeID(string Guid);

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе связи
  /// </summary>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <returns>true, если тип связи существует</returns>
  bool ExistsRelationType(int relTypeID);

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе связи
  /// </summary>
  /// <param name="relTypeGuid">Guid типа связи</param>
  /// <returns>true, если тип связи существует</returns>
  bool ExistsRelationType(Guid relTypeGuid);

  /// <summary>Получить краткую информацию о типе связи</summary>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <returns>Краткая информация о типе связи или null</returns>
  IImRelationType GetRelationType(int relTypeID);

  /// <summary>Получить краткую информацию о типе связи</summary>
  /// <param name="relTypeGuid">Идентификатор типа связи</param>
  /// <returns>Краткая информация о типе связи или null</returns>
  IImRelationType GetRelationType(Guid relTypeGuid);

  /// <summary>
  /// Получить название типа связи (например, "Проектная связь")
  /// </summary>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <returns>Название типа связи (например, "")</returns>
  string GetRelationTypeName(int relTypeID);

  /// <summary>
  /// Получить название типа связи (например, "Проектная связь")
  /// </summary>
  /// <param name="relTypeGuid">Идентификатор типа связи</param>
  /// <returns>Название типа связи (например, "Проектная связь")</returns>
  string GetRelationTypeName(Guid relTypeGuid);

  /// <summary>Получить список описаний всех типов связей</summary>
  /// <returns>Список описаний всех типов связей</returns>
  IList<IImRelationType> GetRelationTypesList();

  /// <summary>
  /// Получить по Guid типа атрибута его Int32-идентификатор
  /// </summary>
  /// <param name="attrTypeGuid">Guid типа атрибута</param>
  /// <returns>Идентификатор типа атрибута. -1 - тип атрибута не найден</returns>
  int GetAttributeTypeID(Guid attrTypeGuid);

  /// <summary>
  /// Получить по Int32-идентификатору типа атрибута его Guid-идентификатор
  /// </summary>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <returns>Идентификатор типа атрибута. Guid.Empty - тип атрибута не найден</returns>
  Guid GetAttributeTypeGuid(int attrTypeID);

  /// <summary>
  /// Возвращает идентификатор типа атрибута по строковому представлению его глобального идентификатора
  /// </summary>
  /// <param name="Guid">Guid типа атрибута в виде строки</param>
  int GetAttributeTypeID(string Guid);

  /// <summary>
  /// Возвращает идентификатор типа атрибута по его названию
  /// </summary>
  /// <param name="attrName">Название типа атрибута</param>
  int GetAttributeByTypeNameID(string attrName);

  /// <summary>Возвращает Guid типа атрибута по его названию</summary>
  /// <param name="attrName">Название типа атрибута</param>
  Guid GetAttributeByTypeNameGuid(string attrName);

  /// <summary>Получить список всех типов атрибутов</summary>
  /// <returns>Список всех типов атрибутов</returns>
  IList<int> GetAttributeTypesIDList();

  /// <summary>Получить список Guid всех типов атрибутов</summary>
  /// <returns>Список Guid всех типов атрибутов</returns>
  IList<Guid> GetAttributeTypesGuidList();

  /// <summary>Получить список описаний всех типов атрибутов</summary>
  /// <returns>Список описаний всех типов атрибутов</returns>
  IList<IImAttributeType> GetAttributeTypesList();

  /// <summary>
  /// Получить Int32-идентификатор типа атрибута по его имени, Guid или числовому идентификатору.
  /// Сгенерирует исключение, если в метод засунуть объект некорректного типа
  /// </summary>
  /// <param name="attributeID">Имя атрибута, Guid или числовой идентификатор</param>
  /// <returns>Int32-идентификатор или Consts.UnknownIDx32, если тип атрибута не найден</returns>
  int GetAttributeID(object attributeID);

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе атрибута
  /// </summary>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <returns>true, если тип атрибута существует</returns>
  bool ExistsAttributeType(int attrTypeID);

  /// <summary>
  /// Проверить, существует ли в кэше информация об указанном типе атрибута
  /// </summary>
  /// <param name="attrTypeGuid">Guid типа атрибута</param>
  /// <returns>true, если тип атрибута существует</returns>
  bool ExistsAttributeType(Guid attrTypeGuid);

  /// <summary>Получить краткую информацию о типе атрибута</summary>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <returns>Краткая информация о типе атрибута или null</returns>
  IImAttributeType GetAttributeType(int attrTypeID);

  /// <summary>Хранятся ли в атрибуте системные данные</summary>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <returns>true, если в атрибуте хранятся системные данные</returns>
  bool HasAttributeSystemData(int attrTypeID);

  /// <summary>Хранятся ли в атрибуте системные данные</summary>
  /// <param name="attrTypeGuid">Guid типа атрибута</param>
  /// <returns>true, если в атрибуте хранятся системные данные</returns>
  bool HasAttributeSystemData(Guid attrTypeGuid);

  /// <summary>Получить краткую информацию о типе атрибута</summary>
  /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
  /// <returns>Краткая информация о типе атрибута или null</returns>
  IImAttributeType GetAttributeType(Guid attrTypeGuid);

  /// <summary>Получить название типа атрибута</summary>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <returns>Название типа атрибута</returns>
  string GetAttributeTypeName(int attrTypeID);

  /// <summary>Получить название типа атрибута</summary>
  /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
  /// <returns>Название типа атрибута</returns>
  string GetAttributeTypeName(Guid attrTypeGuid);

  /// <summary>
  /// Получить по Guid какого-то элемента метаданных его тип
  /// </summary>
  /// <param name="guid">Guid какого-то элемента метаданных</param>
  /// <returns>Тип метаданных для указанного элемента</returns>
  ImGlobals GetGlobalsByGuid(Guid guid);

  /// <summary>
  /// Получить по Guid какого-то элемента метаданных его описание
  /// </summary>
  /// <param name="guid">Guid какого-то элемента метаданных</param>
  /// <returns>Описание метаданных для указанного элемента</returns>
  IDisplayable GetDisplayableByGuid(Guid guid);
}
