
// Type: Intermech.Interfaces.Contexts.IDBEditingContextsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Серверная служба, позволяющая получать классы, хранящие информацию контекстов редактирования
    /// </summary>
    public interface IDBEditingContextsService
    {
      /// <summary>
      /// Добавить версию объекта в указанный контекст редактирования
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии контекта</param>
      /// <param name="linkedContextNumber">Номер взаимосвязанного контекста</param>
      /// <param name="fID">Идентификатор добавляемого объекта</param>
      /// <param name="versionID">Идентификатор добавляемой версии объекта</param>
      /// <param name="writeModificationID">true - записывать номер группы изменений в обработчик объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <returns>true - добавление выполнено успешно</returns>
      bool AddToContext(
        object usrSession,
        long contextID,
        long linkedContextNumber,
        long fID,
        long versionID,
        bool writeModificationID,
        bool exceptIfFail);

      /// <summary>
      /// Заменить существующую версию объекта в указанном УПРОЩЁННОМ контексте редактирования на новую
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии УПРОЩЁННОГО контекта</param>
      /// <param name="linkedContextNumber">Номер взаимосвязанного контекста</param>
      /// <param name="fID">Идентификатор обновляемого объекта</param>
      /// <param name="newVersionID">Идентификатор новой версии объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <returns>true - замена выполнена успешно</returns>
      bool ReplaceInSimpleContext(
        object usrSession,
        long contextID,
        long linkedContextNumber,
        long fID,
        long newVersionID,
        bool exceptIfFail);

      /// <summary>
      /// Заменить существующие версии объектов в указанном УПРОЩЁННОМ контексте редактирования на новые
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии УПРОЩЁННОГО контекта</param>
      /// <param name="linkedContextNumber">Номер взаимосвязанного контекста</param>
      /// <param name="fIDs">Список идентификаторов обновляемых объектов</param>
      /// <param name="newVersionIDs">Идентификаторы новых версий объектов</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <returns>true - замена выполнена успешно</returns>
      bool ReplaceInSimpleContext(
        object usrSession,
        long contextID,
        long linkedContextNumber,
        IList<long> fIDs,
        IList<long> newVersionIDs,
        bool exceptIfFail);

      /// <summary>
      /// Добавить версии объектов в указанный контекст редактирования
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии контекта</param>
      /// <param name="linkedContextNumber">Номер взаимосвязанного контекста</param>
      /// <param name="fIDs">Список идентификаторов добавляемых объектов</param>
      /// <param name="versionIDs">Список идентификаторов добавляемых версий объектов</param>
      /// <param name="writeModificationID">true - записывать номер группы изменений в обработчик объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <returns>true - добавление выполнено успешно</returns>
      bool AddToContext(
        object usrSession,
        long contextID,
        long linkedContextNumber,
        IList<long> fIDs,
        IList<long> versionIDs,
        bool writeModificationID,
        bool exceptIfFail);

      /// <summary>
      /// Удалить версию объекта из указанного контекста редактирования
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии контекта</param>
      /// <param name="versionID">Идентификатор удаляемой версии объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <param name="clearModifiationID"></param>
      /// <returns>true - удаление выполнено успешно</returns>
      bool DeleteFromContext(
        object usrSession,
        long contextID,
        long versionID,
        bool exceptIfFail,
        bool clearModifiationID);

      /// <summary>
      /// Удалить версии объектов из указанного контекста редактирования
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии контекта</param>
      /// <param name="versionIDs">Идентификаторы удаляемых версий объектов</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <param name="clearModifiationID"></param>
      /// <returns>true - удаление выполнено успешно</returns>
      bool DeleteFromContext(
        object usrSession,
        long contextID,
        IList<long> versionIDs,
        bool exceptIfFail,
        bool clearModifiationID);

      /// <summary>Удалить объект из указанного контекста редактирования</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии контекта</param>
      /// <param name="fID">Идентификатор удаляемого объекта</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <param name="clearModifiationID"></param>
      /// <returns>true - удаление выполнено успешно</returns>
      bool DeleteObjectFromContext(
        object usrSession,
        long contextID,
        long fID,
        bool exceptIfFail,
        bool clearModifiationID);

      /// <summary>
      /// Удалить объекты из указанного контекста редактирования
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии контекта</param>
      /// <param name="fIDs">Идентификаторы удаляемых объектов</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <param name="clearModifiationID"></param>
      /// <returns>true - удаление выполнено успешно</returns>
      bool DeleteObjectsFromContext(
        object usrSession,
        long contextID,
        IList<long> fIDs,
        bool exceptIfFail,
        bool clearModifiationID);

      /// <summary>Проверить наличие версии в указанном контексте</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextID">Идентификтор версии контекта</param>
      /// <param name="versionID">Идентификатор искомой версии объекта</param>
      /// <returns>true - искомая версия объекта найдена в указанном контексте</returns>
      bool ExistsInContext(object usrSession, long contextID, long versionID);

      /// <summary>
      /// Проверить наличие версии в любом из контекстов с указанным идентификатором изменения
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="linkedContextNumber">Номер взаимосвязанного контекста</param>
      /// <param name="versionID">Идентификатор искомой версии объекта</param>
      /// <returns>Идентификатор версии объекта контекста, в котором найдена указанная версия,
      /// либо Intermech.Consts.UnknownObjectId</returns>
      long ExistsInContexts(object usrSession, long linkedContextNumber, long versionID);

      /// <summary>
      /// Получить список всех контекстов с указанным идентификатором изменения.
      /// Значение modificationID = Intermech.Consts.UnknownObjectId - получить список всех контекстов в системе.
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="linkedContextNumber">Номер взаимосвязанного контекста</param>
      /// <returns>Список идентификаторов контекстов</returns>
      List<long> GetLinkedContexts(object usrSession, long linkedContextNumber);

      /// <summary>
      /// Получить полный перечень связанных контекстов редактирования на основании указанного списка контекстов
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextObjectIDs">Начальный список идентификаторов версий контекстов редактирования</param>
      /// <returns>Все цепочки связанных контекстов редактирования, включая исходные версии контекстов</returns>
      List<long> GetAllLinkedContexts(object usrSession, List<long> contextObjectIDs);

      /// <summary>
      /// Получить контекст редактирования по идентификатору версии его объекта.
      /// В объект попадает информация о связанных контекстах (с таким же значением атрибута ModificationID)
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="ContextID"></param>
      /// <param name="withDescriptions">true - загружать описания каждой версии и контекстов, иначе только содержимое контекста</param>
      /// <param name="useCache">true - использовать кэширование, иначе - непосредственное чтение из базы данных</param>
      /// <returns>Контекст редактирования или null</returns>
      EditingContextsObjectContainer GetEditingContextsObject(
        object usrSession,
        long ContextID,
        bool withDescriptions,
        bool useCache);

      /// <summary>
      /// Записать в значение контекста содержимое указанного контейнера целиком, в рамках одной транзакции
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <param name="context">Контейнер с содержимым контекста редактирования</param>
      void SetEditingContextsObject(
        object usrSession,
        EditingContextsObjectContainer context,
        bool exceptIfFail);

      /// <summary>
      /// Записать в значение контекста содержимое указанного контейнера целиком, в рамках одной транзакции
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <param name="context">Контейнер с содержимым контекста редактирования</param>
      /// <param name="syncComposition">true - содержимое контекста перечитать из извещения</param>
      void SetEditingContextsObject(
        object usrSession,
        EditingContextsObjectContainer context,
        bool exceptIfFail,
        bool syncComposition,
        bool removeNotVersionedObjects = false);

      /// <summary>
      /// Отыскать контексты редактирования для указанных версий объектов
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="versionIDs">Список идентификаторов версий объектов, которые могут входить в контексты редактирования</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <returns>Список идентификаторов версий контекстов редактирования</returns>
      List<long> FindObjectsContexts(object usrSession, List<long> versionIDs, bool exceptIfFail);

      /// <summary>
      /// Очистить значение атрибута "Номер группы изменений" у указанных версий объектов,
      /// если оно указывает на несуществующий контекст редактирования
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="versionIDs">Список идентификаторов версий объектов, которые могут входить в контексты редактирования</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      /// <returns>Список идентификаторов версий объектов, у которых были сделаны изменения</returns>
      List<long> ClearModificationGroupID(object usrSession, List<long> versionIDs, bool exceptIfFail);

      /// <summary>
      /// Получить разницу в составах архивной и рабочей копии указанного извещения
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="ecoID">Идентификатор рабочей копии извещения</param>
      /// <returns>Список идентификаторов версий объектов, которых нет в составе архивной копии извещения</returns>
      List<long> GetDeltaECOComposiotions(object usrSession, long ecoID);

      /// <summary>
      /// Очистить значение атрибута "Номер группы изменений" у указанных версий объектов
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="versionIDs">Список идентификаторов версий объектов</param>
      /// <param name="exceptIfFail">true - генерировать исключение при возникновении ошибки</param>
      void ForceClearModificationGroupID(object usrSession, List<long> versionIDs, bool exceptIfFail);

      /// <summary>
      /// Проверить, можно ли связать два указанных контекста редактирования.
      /// Если необходимо, будет сгенерировано исключение KernelExceptionID(339).
      /// Если выполняется проверка на возможность связывания контекста с самим собой, будет возвращено
      /// значение false, но исключение генерироваться не будет.
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="contextMain">Основной контекст (должен содержать также все объекты своих связанных контекстов)</param>
      /// <param name="ctxToLink">Контекст, который требуется связать с основным (должен содержать также все объекты своих связанных контекстов)</param>
      /// <param name="exceptIfFail">Если контексты связать нельзя, и значение равно true, будет сгенерировано исключение KernelExceptionID(339)</param>
      /// <returns>true - контексты можно связывать, false - контексты нельзя связывать</returns>
      bool CanLinkContexts(
        object usrSession,
        EditingContextsObjectContainer contextMain,
        EditingContextsObjectContainer ctxToLink,
        bool exceptIfFail);
    }
}
