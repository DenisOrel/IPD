
// Type: Intermech.Interfaces.Compositions.CompositionService.CompositionLoadingParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Interfaces.Compositions.CompositionService
{
    /// <summary>Параметры загрузки состава / применяемости</summary>
    [Serializable]
    public class CompositionLoadingParams
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="objects"></param>
      /// <param name="searchObjectTypes">Типы искомых объектов</param>
      /// <param name="searchRelationTypes">Типы связей по которым раскручивается состав/применяемость</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="conditions">Условия для запроса</param>
      /// <param name="composition">Состав/Применяемость</param>
      /// <param name="grouping"> Группировка объектов в результирующей таблице</param>
      public CompositionLoadingParams(
        IEnumerable<ObjInfoItem> objects,
        IEnumerable<int> searchObjectTypes,
        IEnumerable<int> searchRelationTypes,
        IEnumerable<ColumnDescriptor> columns,
        IEnumerable<ConditionStructure> conditions,
        bool composition,
        bool grouping)
      {
        this.Objects = objects;
        this.SearchObjectTypes = searchObjectTypes;
        this.SearchRelationTypes = searchRelationTypes;
        this.Columns = columns;
        this.Conditions = conditions;
        this.Composition = composition;
        this.Grouping = grouping;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objects"></param>
      /// <param name="searchObjectTypes">Типы искомых объектов</param>
      /// <param name="expandObjectTypes">Если не null, указывает, состав объектов каких типов нужно разворачивать.
      /// Данное условие применяется только к объектам состава и не распространяется на объекты objects</param>
      /// <param name="searchRelationTypes">Типы связей по которым раскручивается состав/применяемость</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="conditions">Условия для запроса</param>
      /// <param name="composition">Состав/Применяемость</param>
      /// <param name="grouping"> Группировка объектов в результирующей таблице</param>
      /// <param name="loadLevels">Количество уровней, для получения рекурсивного состава -1</param>
      /// <param name="versionsRule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
      /// <param name="dbParams">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
      /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.</param>
      public CompositionLoadingParams(
        IEnumerable<ObjInfoItem> objects,
        IEnumerable<int> searchObjectTypes,
        IEnumerable<int> expandObjectTypes,
        IEnumerable<int> searchRelationTypes,
        IEnumerable<ColumnDescriptor> columns,
        IEnumerable<ConditionStructure> conditions,
        bool composition,
        bool grouping,
        int loadLevels,
        VersionsRule versionsRule,
        string filtrationOwnerId,
        IDictionary<long, HybridDictionary> dbParams = null)
      {
        this.Objects = objects;
        this.SearchObjectTypes = searchObjectTypes;
        this.SearchRelationTypes = searchRelationTypes;
        this.ExpandObjectTypes = expandObjectTypes;
        this.Columns = columns;
        this.Conditions = conditions;
        this.Composition = composition;
        this.Grouping = grouping;
        this.LoadLevels = loadLevels;
        this.VersionsRule = versionsRule;
        this.FiltrationOwnerId = filtrationOwnerId;
        this.DbParams = dbParams;
      }

      /// <summary>Объекты, для которых ищется применяемость/состав</summary>
      public IEnumerable<ObjInfoItem> Objects { get; set; }

      /// <summary>Типы искомых объектов</summary>
      public IEnumerable<int> SearchObjectTypes { get; set; }

      /// <summary>
      /// Если не null, указывает, состав объектов каких типов нужно разворачивать.
      /// Данное условие применяется только к объектам состава и не распространяется на объекты objects
      /// </summary>
      public IEnumerable<int> ExpandObjectTypes { get; set; }

      /// <summary>
      /// Типы связей по которым раскручивается состав / применяемость
      /// </summary>
      public IEnumerable<int> SearchRelationTypes { get; set; }

      /// <summary>Условия для запроса</summary>
      public IEnumerable<ConditionStructure> Conditions { get; set; }

      /// <summary>
      /// Коллекция столбцов для запроса состава из базы данных
      /// </summary>
      public IEnumerable<ColumnDescriptor> Columns { get; set; }

      /// <summary>true = Состав / false = Применяемость</summary>
      public bool Composition { get; set; }

      /// <summary>Группировка объектов в результирующей таблице</summary>
      public bool Grouping { get; set; }

      /// <summary>
      /// Количество уровней, для получения рекурсивного состава -1
      /// </summary>
      public int LoadLevels { get; set; } = -1;

      /// <summary>
      /// Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </summary>
      public string FiltrationOwnerId { get; set; } = "cad001e0-306c-11d8-b4e9-00304f19f545";

      /// <summary>
      /// Правило подбора версий, по которому будет фильтроваться состав
      /// </summary>
      public VersionsRule VersionsRule { get; set; }

      /// <summary>
      /// Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
      /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.
      /// </summary>
      public IDictionary<long, HybridDictionary> DbParams { get; set; }

      /// <summary>Интерфейс фильтрации результирующего DataTable</summary>
      public ICompositionDataFilter DataFilter { get; set; }
    }
}
