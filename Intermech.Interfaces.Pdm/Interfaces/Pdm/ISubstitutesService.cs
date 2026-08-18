// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ISubstitutesService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Серверная служба для работы с допустимыми заменами</summary>
public interface ISubstitutesService
{
  /// <summary>
  /// Загрузить состав указанного объекта по определённым правилам фильтрации состава
  /// </summary>
  /// <param name="sessionID">Идентификатор сессии (Guid), в рамках которой выполняется работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации состава</param>
  /// <param name="contexts">Список контекстов, в рамках которых выполняется чтение состава</param>
  /// <param name="projID">Идентификатор версии объекта, состав которого будет загружен</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет загружен состав</param>
  /// <param name="columns">Список колонок, которые требуется загрузить</param>
  /// <returns>Таблица с составом или null</returns>
  DataTable LoadComposition(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    List<ColumnDescriptor> columns);

  /// <summary>
  /// Загрузить состав указанного объекта по определённым правилам фильтрации состава,
  /// выполнить группировку связей по их дочерним версиям объектов
  /// </summary>
  /// <param name="sessionID">Идентификатор сессии (Guid), в рамках которой выполняется работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации состава</param>
  /// <param name="contexts">Список контекстов, в рамках которых выполняется чтение состава</param>
  /// <param name="projID">Идентификатор версии объекта, состав которого будет загружен</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет загружен состав</param>
  /// <param name="advColumns">Список колонок, которые требуется загрузить</param>
  /// <param name="substitutes">Список всех групп допустимых замен в указанном составе</param>
  /// <param name="relationsIndex">Кэш для быстрого поиска строки с данными для указанной связи</param>
  /// <returns>Таблица с составом или null</returns>
  DataTable LoadComposition(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    List<ColumnDescriptor> advColumns,
    out SubstituteObjects substitutes,
    out Dictionary<long, DataRow> relationsIndex);

  /// <summary>
  /// Получить список групп допустимых замен для состава указанного объекта
  /// </summary>
  /// <param name="sessionID">Идентификатор сессии (Guid), в рамках которой выполняется работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации состава</param>
  /// <param name="contexts">Список контекстов, в рамках которых выполняется чтение состава</param>
  /// <param name="projID">Идентификатор версии объекта, состав которого будет загружен</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет загружен состав</param>
  /// <returns>Список всех групп допустимых замен в указанном составе</returns>
  SubstituteObjects LoadSubstitutes(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType);

  /// <summary>
  /// Получить список групп допустимых замен для состава указанного объекта, а также дополнительные атрибуты связей
  /// </summary>
  /// <param name="sessionID">Идентификатор сессии (Guid), в рамках которой выполняется работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации состава</param>
  /// <param name="contexts">Список контекстов, в рамках которых выполняется чтение состава</param>
  /// <param name="projID">Идентификатор версии объекта, состав которого будет загружен</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет загружен состав</param>
  /// <param name="attributes">Список дополнительных атрибутов связей</param>
  /// <param name="relAttributes">Значения дополнительных атрибутов связей</param>
  /// <returns>Список всех групп допустимых замен в указанном составе</returns>
  SubstituteObjects LoadSubstitutes(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    List<ColumnDescriptor> attributes,
    out RelationAttributesPackage relAttributes);

  /// <summary>
  /// Выполнить анализ исполнений, одним из которых является указанное articleID,
  /// определить в них общую и переменные части, вернуть в виде контейнера типа ArticlesPartsPackage.
  /// </summary>
  /// <param name="sessionID">Идентификатор сессии (Guid), в рамках которой выполняется работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации состава</param>
  /// <param name="articleID">Идентификатор версии одного из исполнений</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет загружен состав</param>
  /// <param name="spcForm">Форма спецификации (влияет на степень контроля идентичности связей)</param>
  /// <returns>Контейнер типа ArticlesPartsPackage</returns>
  ArticlesPartsPackage FindCommonAndVariableParts(
    Guid sessionID,
    string filtrationOwnerID,
    long articleID,
    int relationType,
    AVSSpecificationForm spcForm);

  /// <summary>
  /// Метод отыскивает другие исполнения указанной версии объекта и, если они найдены,
  /// выполняет поиск в их составах общих групп
  /// </summary>
  /// <param name="sessionID">Идентификатор сессии (Guid), в рамках которой выполняется работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации состава</param>
  /// <param name="contexts">Список контекстов, в рамках которых выполняется чтение состава</param>
  /// <param name="projID">Идентификатор версии объекта, состав которого будет загружен</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет загружен состав</param>
  /// <param name="advColumns">Список колонок, которые требуется загрузить, чтобы работал механизм сравнения связей</param>
  /// <param name="clientGroups">Список групп заменителей, как их настроил клиент</param>
  /// <param name="adminMode">true - в исполнениях разрешено создавать и удалять группы, иначе - только актуализировать заменители</param>
  /// <param name="spcForm">Форма спецификации (влияет на степень контроля идентичности связей)</param>
  /// <param name="newGroups">Коллекция идентификаторов остальных исполнений, а также варианты модификации их связей</param>
  /// <returns>true, если есть хотя бы одно исполнение, которое требуется исправлять</returns>
  bool FindCommonArticles(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    List<ColumnDescriptor> advColumns,
    SubstituteObjects clientGroups,
    bool adminMode,
    AVSSpecificationForm spcForm,
    out Dictionary<long, RelationAttributesPackage> newGroups);

  /// <summary>
  /// Выполнить запись информации о допустимых заменах в состав указанного родительского объекта
  /// </summary>
  /// <param name="sessionID  ">Уникальный идентификатор сессии, в рамках которой будет выполняться работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации для получения состава родительского объекта</param>
  /// <param name="contexts">Контексты, в рамках которых будет загружаться состав</param>
  /// <param name="projID">Идентификатор версии родительского объекта, состав которого будет модифицироваться</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет раскрыт состав</param>
  /// <param name="substitutes">Информация о группах заменителей и заменителях в группе</param>
  /// <param name="chRels">Список изменённых связей</param>
  /// <returns>Идентификатор объекта, в состав которого были внесены изменения</returns>
  long WriteSubstitutesInfo(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    SubstituteObjects substitutes,
    out List<long> chRels);

  /// <summary>
  /// Выполнить запись пакета атрибутов связей в состав указанного родительского объекта
  /// </summary>
  /// <param name="sessionID  ">Уникальный идентификатор сессии, в рамках которой будет выполняться работа с базой данных</param>
  /// <param name="package">Пакет атрибутов связей</param>
  /// <param name="chRels">Список изменённых связей</param>
  /// <returns>true, если информация была успешно сохранена в базе данных</returns>
  bool WriteRelationAttributesPackage(
    Guid sessionID,
    RelationAttributesPackage package,
    out List<long> chRels);

  /// <summary>
  /// Выполнить запись пакетов атрибутов связей в состав указанного родительского объекта
  /// </summary>
  /// <param name="sessionID  ">Уникальный идентификатор сессии, в рамках которой будет выполняться работа с базой данных</param>
  /// <param name="packages">Коллекция идентификаторов исполнений и пакетов их атрибутов</param>
  /// <param name="chRels">Список изменённых связей</param>
  /// <returns>true, если информация была успешно сохранена в базе данных</returns>
  bool WriteRelationAttributesPackages(
    Guid sessionID,
    Dictionary<long, RelationAttributesPackage> packages,
    out List<long> chRels);

  /// <summary>
  /// Задать условия применения объектов для указанной группы допустимых замен (конфигуратор составов)
  /// </summary>
  /// <param name="sessionID">Уникальный идентификатор сессии, в рамках которой будет выполняться работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации для получения состава родительского объекта</param>
  /// <param name="contexts">Контексты, в рамках которых будет загружаться состав</param>
  /// <param name="projID">Идентификатор версии родительского объекта, состав которого будет модифицироваться</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет раскрыт состав</param>
  /// <param name="groupNo">Номер группы заменителей</param>
  /// <param name="chRels">Список изменённых связей</param>
  /// <returns>true, если изменение прошло успешно</returns>
  bool CorrectConfiguratorApplicabilities(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    long groupNo,
    ref List<long> chRels);

  /// <summary>
  /// Задать условия применения объектов для указанного родительского объекта (конфигуратор составов)
  /// </summary>
  /// <param name="sessionID">Уникальный идентификатор сессии, в рамках которой будет выполняться работа с базой данных</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации для получения состава родительского объекта</param>
  /// <param name="contexts">Контексты, в рамках которых будет загружаться состав</param>
  /// <param name="projID">Идентификатор версии родительского объекта, состав которого будет модифицироваться</param>
  /// <param name="relationType">Идентификатор типа связи, по которой будет раскрыт состав</param>
  /// <param name="chRels">Список изменённых связей</param>
  /// <returns>true, если изменение прошло успешно</returns>
  bool CorrectConfiguratorApplicabilities(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    ref List<long> chRels);
}
