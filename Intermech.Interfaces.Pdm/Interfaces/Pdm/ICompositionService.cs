// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ICompositionService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Интерфейс для получения составов/применяемости объектов БД
/// </summary>
public interface ICompositionService : ICustomCompositionService
{
  /// <summary>
  /// Получить состав/применяемость
  /// Если обрабатывается в CompositionService ОБЯЗАТЕЛЬНО !!!
  /// 1) ColumnDescriptor должны в ColumnName = ColumnNameMapping.ID;
  /// 2) в columns должны присутствовать ObligatoryObjectAttributes.F_OBJECT_TYPE и ObligatoryObjectAttributes.F_OBJECT_ID
  /// </summary>
  /// <param name="userSessionGuid">GUID сессии</param>
  /// <param name="objectID">ID объекта</param>
  /// <param name="schemeID">ID схемы поиска</param>
  /// <param name="filterConditions">Условия по которым необходимо отфильтровать результаты, если заданы, то условия выборки в схеме поиска игнорируются</param>
  /// <param name="columns">Колонки</param>
  /// <param name="selectGUID">GUID, по которому клиентская программа сможет обращаться к серверному потоку, разворачивающему состав</param>
  /// <param name="filtrationOwnerID">Идентификатор настроек фильтрации, по которым будет выполняться разворачивание состава</param>
  /// <param name="tags">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
  /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.</param>
  /// <returns>Для начала ничего толком не возвращается. Надо юзать метод GetInfo и брать результаты оттуда</returns>
  DataTable Select(
    Guid userSessionGuid,
    long objectID,
    long schemeID,
    List<ConditionStructure> filterConditions,
    List<ColumnDescriptor> columns,
    Guid selectGUID,
    string filtrationOwnerID,
    [NotNull] HybridDictionary tags);

  /// <summary>
  /// Получить состав/применяемость
  /// Если обрабатывается в CompositionService ОБЯЗАТЕЛЬНО !!!
  /// 1) ColumnDescriptor должны в ColumnName = ColumnNameMapping.ID;
  /// 2) в columns должны присутствовать ObligatoryObjectAttributes.F_OBJECT_TYPE и ObligatoryObjectAttributes.F_OBJECT_ID
  /// </summary>
  /// <param name="userSessionGuid">GUID сессии</param>
  /// <param name="objectID">ID объекта</param>
  /// <param name="schemeID">ID схемы поиска</param>
  /// <param name="columns">Колонки</param>
  /// <param name="selectGUID">GUID, по которому клиентская программа сможет обращаться к серверному потоку, разворачивающему состав</param>
  /// <param name="FiltrationOwnerID">Идентификатор настроек фильтрации, по которым будет выполняться разворачивание состава</param>
  /// <param name="Tags">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
  /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.</param>
  /// <returns>Для начала ничего толком не возвращается. Надо юзать метод GetInfo и брать результаты оттуда</returns>
  DataTable Select(
    Guid userSessionGuid,
    long objectID,
    long schemeID,
    List<ColumnDescriptor> columns,
    Guid selectGUID,
    string FiltrationOwnerID,
    [NotNull] HybridDictionary Tags);

  /// <summary>
  /// Получить состав/применяемость, используя виртуальную схему поиска
  /// Если обрабатывается в CompositionService ОБЯЗАТЕЛЬНО !!!
  /// 1) ColumnDescriptor должны в ColumnName = ColumnNameMapping.ID;
  /// 2) в columns должны присутствовать ObligatoryObjectAttributes.F_OBJECT_TYPE и ObligatoryObjectAttributes.F_OBJECT_ID
  /// </summary>
  /// <param name="userSessionGuid">GUID сессии</param>
  /// <param name="objectID">ID объекта</param>
  /// <param name="scheme">Виртуальная схема поиска</param>
  /// <param name="columns">Колонки</param>
  /// <param name="selectGUID">GUID, по которому клиентская программа сможет обращаться к серверному потоку, разворачивающему состав</param>
  /// <param name="FiltrationOwnerID">Идентификатор настроек фильтрации, по которым будет выполняться разворачивание состава</param>
  /// <param name="Tags">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
  /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.</param>
  /// <returns>Для начала ничего толком не возвращается. Надо юзать метод GetInfo и брать результаты оттуда</returns>
  DataTable Select(
    Guid userSessionGuid,
    long objectID,
    RuntimeSearchScheme scheme,
    List<ColumnDescriptor> columns,
    Guid selectGUID,
    string FiltrationOwnerID,
    [NotNull] HybridDictionary Tags);

  /// <summary>Получить схемы поиска для заданных типов связей</summary>
  /// <param name="userSessionGuid"></param>
  /// <param name="relationTypes">Типы связей</param>
  /// <returns>Список идентификаторов схем</returns>
  [Obsolete("Будет удалено в IPS 7. Используйте функцию GetSchemesForRelationTypesEx")]
  List<long> GetSchemesForRelationTypes(Guid userSessionGuid, List<Guid> relationTypes);

  /// <summary>
  /// Получить схемы поиска для заданных типов связей и дополнительно в конкретную сторону
  /// (для состава либо для применяемости)
  /// </summary>
  /// <param name="userSessionGuid">GUID сессии</param>
  /// <param name="relationTypes">Типы связей</param>
  /// <param name="mode">Направление поиска</param>
  /// <returns>Список идентификаторов схем</returns>
  [Obsolete("Будет удалено в IPS 7. Используйте функцию GetSchemesForRelationTypesEx")]
  List<long> GetSchemesForRelationTypes(
    Guid userSessionGuid,
    List<Guid> relationTypes,
    ContainsMode mode);

  /// <summary>
  /// Расширенная информация по схемам, включая их наименование и список допустимых ролей
  /// </summary>
  /// <param name="userSessionGuid">GUID сессии</param>
  /// <param name="relationTypes">Типы связей</param>
  /// <param name="mode">Направление поиска</param>
  /// <param name="roleFiltration">Отфильтровать по текущей роли</param>
  List<SearchSchemaInfo> GetSchemesForRelationTypesEx(
    Guid userSessionGuid,
    List<Guid> relationTypes,
    ContainsMode mode,
    bool roleFiltration);
}
