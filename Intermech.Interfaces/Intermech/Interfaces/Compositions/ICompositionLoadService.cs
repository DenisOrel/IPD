
// Type: Intermech.Interfaces.Compositions.ICompositionLoadService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Серверная служба, позволяющая читать составы по указанным правилам подбора версий,правилам сортировки и отображения составов
    /// </summary>
    public interface ICompositionLoadService
    {
      /// <summary>Считать состав указанного родительского объекта</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Тип связи, по которому надо получить состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComposition(
        object usrSession,
        long projId,
        int relationTypeId,
        IEnumerable<ColumnDescriptor> columns,
        string filtrationOwnerId,
        params int[] childObjectTypes);

      /// <summary>Считать состав указанного родительского объекта</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Тип связи, по которому надо получить состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="dbParamTags">Дополнительные параметры</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComposition(
        object usrSession,
        long projId,
        int relationTypeId,
        IEnumerable<ColumnDescriptor> columns,
        string filtrationOwnerId,
        HybridDictionary dbParamTags,
        params int[] childObjectTypes);

      /// <summary>Считать состав указанного родительского объекта</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Тип связи, по которому надо получить состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="conditions">Дополнительные параметры запросов (или null)</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComposition(
        object usrSession,
        long projId,
        int relationTypeId,
        IEnumerable<ColumnDescriptor> columns,
        string filtrationOwnerId,
        IEnumerable<ConditionStructure> conditions,
        params int[] childObjectTypes);

      /// <summary>Считать состав указанного родительского объекта</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Тип связи, по которому надо получить состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComposition(
        object usrSession,
        long projId,
        int relationTypeId,
        IEnumerable<ColumnDescriptor> columns,
        VersionsRule rule,
        params int[] childObjectTypes);

      /// <summary>Считать состав указанного родительского объекта</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Тип связи, по которому надо получить состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="conditions">Дополнительные параметры запросов (или null)</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComposition(
        object usrSession,
        long projId,
        int relationTypeId,
        IEnumerable<ColumnDescriptor> columns,
        VersionsRule rule,
        IEnumerable<ConditionStructure> conditions,
        params int[] childObjectTypes);

      /// <summary>
      /// Считать состав указанного родительского объекта по всем видимым типам связей
      /// (по текущим настройкам отображения и сортировки составов)
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadCompositions(
        object usrSession,
        long projId,
        IEnumerable<ColumnDescriptor> columns,
        string filtrationOwnerId,
        params int[] childObjectTypes);

      /// <summary>
      /// Считать состав указанного родительского объекта по всем видимым типам связей
      /// (по текущим настройкам отображения и сортировки составов)
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadCompositions(
        object usrSession,
        long projId,
        IEnumerable<ColumnDescriptor> columns,
        VersionsRule rule,
        params int[] childObjectTypes);

      /// <summary>
      /// Считать состав указанного родительского объекта по указанным типам связей
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="visibleRelTypes">Список типов связей, по которым надо читать составы</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadCompositions(
        object usrSession,
        long projId,
        IEnumerable<int> visibleRelTypes,
        IEnumerable<ColumnDescriptor> columns,
        string filtrationOwnerId,
        params int[] childObjectTypes);

      /// <summary>
      /// Считать состав указанного родительского объекта по указанным типам связей
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="visibleRelTypes">Список типов связей, по которым надо читать составы</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadCompositions(
        object usrSession,
        long projId,
        IEnumerable<int> visibleRelTypes,
        IEnumerable<ColumnDescriptor> columns,
        VersionsRule rule,
        params int[] childObjectTypes);

      /// <summary>
      /// Считать составы указанных родительских объектов по указанному типу связи
      /// (все составы загружаются одним запросом в одну общую таблицу)
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projIDs">Список [F_OBJECT_ID] версий родительского объекта, для которых будет считываться состав</param>
      /// <param name="relationTypeId">Тип связи, по которой надо читать составы</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComplexCompositions(
        object usrSession,
        IEnumerable<long> projIDs,
        int relationTypeId,
        IEnumerable<ColumnDescriptor> columns,
        VersionsRule rule,
        params int[] childObjectTypes);

      /// <summary>
      /// Считать составы указанных родительских объектов по указанному типу связи
      /// (все составы загружаются одним запросом в одну общую таблицу)
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="projIDs">Список [F_OBJECT_ID] версий родительского объекта, для которых будет считываться состав</param>
      /// <param name="relationTypeId">Тип связи, по которой надо читать составы</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComplexCompositions(
        object usrSession,
        IEnumerable<long> projIDs,
        int relationTypeId,
        IEnumerable<ColumnDescriptor> columns,
        string filtrationOwnerId,
        params int[] childObjectTypes);

      /// <summary>
      /// Отыскать первый родительский объект, в состав которого входит указанный дочерний объект
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="partId">[F_OBJECT_ID] версии дочернего объекта, для которого ищется применяемость</param>
      /// <param name="relationTypeId">Тип связи, по которому надо получить применяемость</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <returns>[F_OBJECT_ID] Идентификатор версии первого родительского объекта, в состав которого входит указанный дочерний объект,
      /// либо 0, если родительский объект не найден</returns>
      long FindCompositionParentObject(
        object usrSession,
        long partId,
        int relationTypeId,
        string filtrationOwnerId);

      /// <summary>
      /// Отыскать первый родительский объект, в состав которого входит указанный дочерний объект
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="partId">[F_OBJECT_ID] версии дочернего объекта, для которого ищется применяемость</param>
      /// <param name="relationTypeId">Тип связи, по которому надо получить применяемость</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться применяемость</param>
      /// <returns>[F_OBJECT_ID] Идентификатор версии первого родительского объекта, в состав которого входит указанный дочерний объект,
      /// либо 0, если родительский объект не найден</returns>
      long FindCompositionParentObject(
        object usrSession,
        long partId,
        int relationTypeId,
        VersionsRule rule);

      /// <summary>
      /// Получить список версий дочерних объектов (без повторения), входящих в состав
      /// указанного родительского типа объектов указанным типом связи
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).</param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Идентификатор типа связи</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>Список версий дочерних объектов (может быть пустым)</returns>
      List<long> LoadCompositionObjects(
        object usrSession,
        long projId,
        int relationTypeId,
        string filtrationOwnerId,
        params int[] childObjectTypes);

      /// <summary>
      /// Получить список версий дочерних объектов (без повторения), входящих в состав
      /// указанного родительского типа объектов указанным типом связи
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).</param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Идентификатор типа связи</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>Список версий дочерних объектов (может быть пустым)</returns>
      List<long> LoadCompositionObjects(
        object usrSession,
        long projId,
        int relationTypeId,
        VersionsRule rule,
        params int[] childObjectTypes);

      /// <summary>
      /// Получить список версий дочерних объектов (без повторения), входящих в состав
      /// указанного родительского типа объектов указанным типом связи
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).</param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Идентификатор типа связи</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>Список версий дочерних объектов (может быть пустым)</returns>
      List<TypedObjectInfo> LoadCompositionTypedObjects(
        object usrSession,
        long projId,
        int relationTypeId,
        string filtrationOwnerId,
        params int[] childObjectTypes);

      /// <summary>
      /// Получить список версий дочерних объектов (без повторения), входящих в состав
      /// указанного родительского типа объектов указанным типом связи
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).</param>
      /// <param name="projId">[F_OBJECT_ID] версии родительского объекта, для которого будет считываться состав</param>
      /// <param name="relationTypeId">Идентификатор типа связи</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>Список версий дочерних объектов (может быть пустым)</returns>
      List<TypedObjectInfo> LoadCompositionTypedObjects(
        object usrSession,
        long projId,
        int relationTypeId,
        VersionsRule rule,
        params int[] childObjectTypes);

      /// <summary>Получить применяемость дочернего объекта</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="partId">[F_OBJECT_ID] версии дочернего объекта, для которого ищется применяемость</param>
      /// <param name="relationTypeId">Тип связи, по которому надо получить применяемость</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="childObjectTypes">Если требуется, можно дополнительно типизировать коллекцию связей указанными дочерними типами объектов</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadCompositionApplicability(
        object usrSession,
        long partId,
        int relationTypeId,
        IEnumerable<ColumnDescriptor> columns,
        string filtrationOwnerId,
        params int[] childObjectTypes);

      /// <summary>Получить состав/применяемость</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="objectId">[F_OBJECT_ID] версии дочернего/родительского объекта, для которого ищется применяемость/состав</param>
      /// <param name="objectType">тип дочернего/родительского объекта, для которого ищется применяемость/состав</param>
      /// <param name="searchRelationTypes">Типы связей по которым раскручивается состав/применяемость</param>
      /// <param name="searchObjectTypes">Типы искомых объектов</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="composition">состав/Применяемость</param>
      /// <param name="grouping">Группировка объектов в результирующей таблице</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="conditions">Условия для запроса</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
      /// <param name="tags">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
      /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.</param>
      /// <param name="loadLevels">Количество уровней, для получения рекурсивного состава -1</param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComposition(
        object usrSession,
        long objectId,
        int objectType,
        IEnumerable<int> searchRelationTypes,
        IEnumerable<int> searchObjectTypes,
        IEnumerable<ColumnDescriptor> columns,
        bool composition,
        bool grouping,
        VersionsRule rule,
        IEnumerable<ConditionStructure> conditions,
        string filtrationOwnerId,
        HybridDictionary tags,
        int loadLevels);

      /// <summary>Получить состав/применяемость</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="objectId">[F_OBJECT_ID] версии дочернего/родительского объекта, для которого ищется применяемость/состав</param>
      /// <param name="objectType">тип дочернего/родительского объекта, для которого ищется применяемость/состав</param>
      /// <param name="searchRelationTypes">Типы связей по которым раскручивается состав/применяемость</param>
      /// <param name="searchObjectTypes">Типы искомых объектов</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="composition">состав/Применяемость</param>
      /// <param name="grouping">Группировка объектов в результирующей таблице</param>
      /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="conditions">Условия для запроса</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="tags">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
      /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.</param>
      /// <param name="loadLevels">Количество уровней, для получения рекурсивного состава -1</param>
      /// <param name="expandObjectTypes">Если не null, указывает, состав объектов каких типов нужно разворачивать.
      /// Данное условие применяется только к объектам состава и не распространяется на объект objectID </param>
      /// <returns>DataTable с указанными столбцами (если нет состава - вернёт пустую DataTable) или null в случае ошибки</returns>
      DataTable LoadComposition(
        object usrSession,
        long objectId,
        int objectType,
        IEnumerable<int> searchRelationTypes,
        IEnumerable<int> searchObjectTypes,
        IEnumerable<ColumnDescriptor> columns,
        bool composition,
        bool grouping,
        VersionsRule rule,
        IEnumerable<ConditionStructure> conditions,
        string filtrationOwnerId,
        HybridDictionary tags,
        int loadLevels,
        IEnumerable<int> expandObjectTypes);

      /// <summary>Получить состав/применяемость</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="objects">Объекты, для которых ищется применяемость/состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="composition">состав/Применяемость</param>
      /// <param name="searchRelationTypes">Типы связей по которым раскручивается состав/применяемость</param>
      /// <param name="searchObjectTypes">Типы искомых объектов</param>
      /// <param name="grouping">Группировка объектов в результирующей таблице</param>
      /// <param name="versionsRule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="conditions">Условия для запроса</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="dbParams">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
      /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.</param>
      /// <param name="loadLevels">Количество уровней, для получения рекурсивного состава -1</param>
      /// <returns></returns>
      DataTable LoadComplexCompositions(
        object usrSession,
        IEnumerable<ObjInfoItem> objects,
        IEnumerable<int> searchRelationTypes,
        IEnumerable<int> searchObjectTypes,
        IEnumerable<ColumnDescriptor> columns,
        bool composition,
        bool grouping,
        VersionsRule versionsRule,
        IEnumerable<ConditionStructure> conditions,
        string filtrationOwnerId,
        Dictionary<long, HybridDictionary> dbParams,
        int loadLevels);

      /// <summary>Получить состав/применяемость</summary>
      /// <param name="userSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="objects">Объекты, для которых ищется применяемость/состав</param>
      /// <param name="columns">Коллекция столбцов для запроса состава из базы данных</param>
      /// <param name="composition">состав/Применяемость</param>
      /// <param name="searchRelationTypes">Типы связей по которым раскручивается состав/применяемость</param>
      /// <param name="searchObjectTypes">Типы искомых объектов</param>
      /// <param name="grouping">Группировка объектов в результирующей таблице</param>
      /// <param name="versionsRule">Правило подбора версий, по которому будет фильтроваться состав</param>
      /// <param name="conditions">Условия для запроса</param>
      /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
      /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.
      /// </param>
      /// <param name="dbParams">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
      /// Например, для включения режима актуализации состава, для работы в определённых контекстах состава, т.п.</param>
      /// <param name="loadLevels">Количество уровней, для получения рекурсивного состава -1</param>
      /// <param name="expandObjectTypes">Если не null, указывает, состав объектов каких типов нужно разворачивать.
      /// Данное условие применяется только к объектам состава и не распространяется на объекты objects</param>
      /// <returns></returns>
      DataTable LoadComplexCompositions(
        object userSession,
        IEnumerable<ObjInfoItem> objects,
        IEnumerable<int> searchRelationTypes,
        IEnumerable<int> searchObjectTypes,
        IEnumerable<ColumnDescriptor> columns,
        bool composition,
        bool grouping,
        VersionsRule versionsRule,
        IEnumerable<ConditionStructure> conditions,
        string filtrationOwnerId,
        Dictionary<long, HybridDictionary> dbParams,
        int loadLevels,
        IEnumerable<int> expandObjectTypes);

      /// <summary>Получить состав/применяемость</summary>
      /// <param name="userSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="loadingParams">Параметры загрузки применяемости /состава</param>
      DataTable LoadComplexCompositions(object userSession, CompositionLoadingParams loadingParams);

      /// <summary>
      /// Получить список типов реально входящих в состав либо родительских объектов для ids
      /// </summary>
      /// <param name="userSession">Пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="ids">Если смотрится состав - идентификаторы версий родительских объектов (IDBObject.ObjectID), если применяемость -  идентификаторы дочерних объектов (IDBObject.ID)</param>
      /// <param name="relationTypeId">Тип связи</param>
      /// <param name="composition">Состав=true, Применяемость=false</param>
      /// <returns></returns>
      List<int> GetPresentCompositionTypes(
        object userSession,
        IEnumerable<long> ids,
        int relationTypeId,
        bool composition);
    }
}
