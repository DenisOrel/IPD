
// Type: Intermech.Interfaces.ISelectionsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для работы с выборками и классификаторами</summary>
    public interface ISelectionsService
    {
      /// <summary>
      /// Получение массива условий выборки по указанному идентификатору
      /// (если есть в кэше - берется из кэша)
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionID">Идентификатор выборки</param>
      /// <returns>Массив элементов ConditionStructure</returns>
      ConditionStructure[] GetConditionStructures(object userSession, long selectionID);

      /// <summary>
      /// Получение массива условий выборки по указанному идентификатору c подстановкой
      /// в условия типа "входит в" и "состоит из" заданного идентификатора объекта
      /// (если есть в кэше - берется из кэша)
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionID">Идентификатор выборки</param>
      /// <param name="objectID">Идентификатор для подстановки в условия</param>
      /// <returns>Массив элементов ConditionStructure</returns>
      ConditionStructure[] GetConditionStructures(object userSession, long selectionID, long objectID);

      /// <summary>
      /// Установка нового массива условий для выборки по указанному идентификатору
      /// (в базе и в кэше)
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionID">Идентификатор выборки</param>
      /// <param name="conditionStructures">Массив элементов ConditionStructure</param>
      /// <returns>true если операция прошла успешно, иначе false</returns>
      bool SetConditionStructures(
        object userSession,
        long selectionID,
        ConditionStructure[] conditionStructures);

      /// <summary>Обновление всех имеющихся в кэше условий выборки</summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      void UpdateCashe(object userSession);

      /// <summary>
      /// Обновление в кэше условия для выборки с указанным идентификатором
      /// (если его нет в кэше, то будет туда добавлено)
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionID">Идентификатор выборки</param>
      void UpdateCashe(object userSession, long selectionID);

      /// <summary>Очистка кэша условий выборок</summary>
      void ClearCashe();

      /// <summary>
      /// Добавление обьектов к классификатору (ручной выборке). При неудаче возвращает ID объектов,
      /// которые не удалось классифицировать.
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionID">Идентификатор классификатора (ручной выборки)</param>
      /// <param name="objectIDs">Массив идентификаторов объектов, которые надо добавить</param>
      void IncludeObjects(object userSession, long selectionID, long[] objectIDs);

      /// <summary>
      /// Добавление обьектов к классификатору (ручной выборке). При неудаче возвращает ID объектов,
      /// которые не удалось классифицировать.
      /// </summary>
      /// <param name="userSessionGuid">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionGuid">Глобальный дентификатор классификатора (ручной выборки)</param>
      /// <param name="objectIDs">Массив идентификаторов объектов, которые надо добавить</param>
      void IncludeObjects(object userSessionGuid, Guid selectionGuid, long[] objectIDs);

      /// <summary>Получить интерфейс на классификатор объектов</summary>
      /// <param name="userSessionGuid">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="classifierID">Идентификатор версии объекта классификатора</param>
      /// <returns>Обработчик объекта-классификатора</returns>
      IObjectClassificator GetObjectClassificator(object userSessionGuid, long classifierID);

      /// <summary>
      /// Исключение объектов из классификатора (ручной выборки)
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionID">Идентификатор классификатора (ручной выборки)</param>
      /// <param name="objectIDs">Массив идентификаторов версий объектов, которые надо исключить</param>
      void ExcludeObjects(object userSession, long selectionID, long[] objectIDs);

      /// <summary>
      /// Исключение объектов из классификатора (ручной выборки) по ид объектов (не версий)
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionID">Идентификатор классификатора (ручной выборки)</param>
      /// <param name="IDs">Массив идентификаторов объектов, которые надо исключить</param>
      void ExcludeObjectsByID(object userSession, long selectionID, long[] IDs);

      /// <summary>
      /// Проверка наличия объекта в классификаторе (ручной выборке)
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="selectionID">Идентификатор классификатора (ручной выборки)</param>
      /// <param name="objectID">Идентификатор объекта, наличие которого проверяется</param>
      /// <returns>true - указанный объект найден в классификаторе (выборке)</returns>
      bool ExistsObject(object userSession, long selectionID, long objectID);

      /// <summary>
      /// Метод возвращает массив идентификаторов объектов из списка objectIDs, которые уже включены в выборку/классификатор folderID
      /// </summary>
      /// <param name="userSessionGuid">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="folderID">идентификатор папки классификатора или выборки</param>
      /// <param name="objectID">идентификаторы объектов, наличие которых надо проверить</param>
      /// <returns>Массив идентификаторов объектов, которые уже есть в папке/выборке</returns>
      long[] ExistsObjectsID(object userSession, long folderID, long[] objectIDs);

      /// <summary>Список включенных в выборку объектов</summary>
      /// <param name="userSession"></param>
      /// <param name="selectionID"></param>
      /// <returns></returns>
      Dictionary<int, List<long>> IncludedObjects(object userSession, long selectionID);

      /// <summary>
      /// Установка значения флага, определяющего нужно ли показывать состав вложенных
      /// папок классификаторов
      /// </summary>
      /// <param name="newValue">новое значение флага</param>
      void SetShowInternalFolders(bool newValue);

      /// <summary>
      /// Получение значения флага, определяющего нужно ли показывать состав вложенных
      /// папок классификаторов
      /// </summary>
      /// <returns>Значение флага</returns>
      bool GetShowInternalFolders();

      /// <summary>Загрузка данных в кэш</summary>
      void LoadClassifierToObjTypeCache();

      /// <summary>Удалить классификатор из кэша</summary>
      /// <param name="classifierID">Идентификатор версии удаляемого объекта-классификатора</param>
      void DeleteClassifierFromCache(long classifierID);

      /// <summary>Добавить классификатор в кэш</summary>
      /// <param name="session">Сессия</param>
      /// <param name="classifierID">Идентификатор версии объекта-классификатора</param>
      void AddClassifierToCache(IUserSession session, long classifierID);

      /// <summary>
      /// Получить из кэша массив с ID классификаторов, у которых атрибут "Глобальные идентификатор типов объектов" равен
      /// передаваемому типу объекта, кроме того все, кому не назначен.
      /// Если передать -1, возвратит все классификаторы.
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="objType">Тип объектов</param>
      /// <returns>массив с ID классификаторов</returns>
      long[] GetClassifierForObjType(object userSession, int objType);

      /// <summary>
      /// Получить ID папки классификатора в которую включен объект,	и -1, если никуда не включен
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="ID">ID объекта (F_ID)</param>
      /// <returns>ID папки классификатора (F_FOLDER_ID)</returns>
      long GetClassifierForObject(object userSession, long ID);

      /// <summary>
      /// Получить список идентификаторов типов объектов, которым назначен укаанный классификатор
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="classifierID">Идентификатор классификатора</param>
      /// <returns>Cписок идентификаторов типов объектов, которым назначен указанный классификатор</returns>
      int[] GetObjectTypesForClassifier(object userSession, long classifierID);

      /// <summary>
      /// Получить следующее доступное значение для ключа классификатора (каталога Imbase либо
      /// другого объекта корневого уровня с атрибутом "ключ папки классификатора")
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <returns>Сгенерированный ключ папки классификатора</returns>
      string GenerateNextTopLevelKey(object userSession);

      /// <summary>
      /// Сгенерировать следующий ключ классификатора верхнего уровня для указанного типа объекта
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="objType">Идентификатор типа объекта</param>
      /// <returns>Новый ключ классификатора верхнего уровня для указанного типа объекта</returns>
      string GenerateNextTopLevelKey(object userSession, int objType);

      /// <summary>
      /// Получить следующее доступное значение для ключа классификатора (каталога Imbase либо
      /// другого объекта с атрибутом "ключ папки классификатора")
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="parentTypeID">Идентификатор типа родительского объекта</param>
      /// <param name="parentKey">значение ключа родительского элемента классификатора</param>
      /// <param name="objType">тип объектов, для объекта которого генерится сей ключ, если
      /// передается Consts.UnknownObjectTypeId, будет генерить в разрезе всех объектов
      /// в базе данных, у которыых есть такой атрибут</param>
      /// <returns>Следующее доступное значение для ключа классификатора</returns>
      string GenerateNextClassifierKey(
        object userSession,
        int parentTypeID,
        string parentKey,
        int objType);

      /// <summary>
      /// Получить следующее доступное значение для ключа классификатора (каталога Imbase либо
      /// другого объекта с атрибутом "ключ папки классификатора")
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="id">Идентификатор объекта, для которого генерится ключ</param>
      /// <param name="objType">тип объектов, для объекта которого генерится сей ключ, если
      /// передается Consts.UnknownObjectTypeId, будет генерить в разрезе всех объектов
      /// в базе данных, у которыых есть такой атрибут</param>
      /// <returns>Следующее доступное значение для ключа классификатора</returns>
      string GenerateNextClassifierKey(object userSession, int objType, long id);

      /// <summary>
      /// Установить в кэше массив с индексами отключенных условий для выборки
      /// </summary>
      /// <param name="selectionID">ID выборки</param>
      /// <param name="conditionIndexes">Массив с индексами отключенных условий</param>
      void DisableConditionStructures(long selectionID, List<int> conditionIndexes);

      /// <summary>
      /// Установить в кэше массив с индексами условий и временными значениями для них
      /// </summary>
      /// <param name="selectionID">ID выборки</param>
      /// <param name="values">Временные значения</param>
      void SetTemporaryValues(long selectionID, List<object[]> values);

      /// <summary>
      /// Удалить из кэша массив с временными значениями для выборки
      /// </summary>
      /// <param name="selectionID">ID выборки</param>
      void RemoveTemporaryValues(long selectionID);

      /// <summary>Получить временные значения для выборки</summary>
      /// <param name="selectionID">ID выборки</param>
      /// <returns></returns>
      List<object[]> GetTemporaryValues(long selectionID);

      /// <summary>Флаг того, что выборке установлены временные значение</summary>
      /// <param name="selectionID">ID выборки</param>
      /// <returns></returns>
      bool IsTemporaryValuesPresent(long selectionID);

      /// <summary>
      /// Проверить, включено ли условие выборки с индексом conditionIndex
      /// </summary>
      /// <param name="selectionID">ID выборки</param>
      /// <param name="conditionIndex">Индекс условия в массиве условий выборки</param>
      /// <returns>true - указанное условие включено</returns>
      bool IsEnabledConditionStructure(long selectionID, int conditionIndex);

      /// <summary>
      /// Возвращает true, если СУБД поддерживает приведение memo-полей к верхнему регистру
      /// </summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <returns>true, если СУБД поддерживает приведение memo-полей к верхнему регистру</returns>
      bool CanUpperMemo(object userSession);

      /// <summary>Возвращает идентификатор рутового классификатора</summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="childClassifier">Идентификатор дочерней папки</param>
      /// <returns></returns>
      long GetRootClassifier(object userSession, long childClassifier);

      /// <summary>Возвращает идентификатор рутового классификатора</summary>
      /// <param name="userSession">Пользовательская сессия. В реализации службы выборок на
      /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
      /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
      /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
      /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
      /// <param name="childClassifier">Дочерняя папка</param>
      /// <returns></returns>
      long GetRootClassifier(object userSession, IDBObject childClassifier);

      /// <summary>
      /// Запустить процесс копирования структуры (развернутого состава) из указанного объекта в другой объект
      /// (классификатор или выборка)
      /// </summary>
      /// <param name="userSession">Пользовательская сессия</param>
      /// <param name="name">Название операции для отображения в логе</param>
      /// <param name="prototypeID">Идентификатор версии объекта-прототипа копируемой структуры</param>
      /// <param name="parentID">Идентификатор версии объекта в составе которого создается копия структуры</param>
      /// <returns>Глобальный идентификатор копирования</returns>
      Guid StartCopyStructure(object userSession, string name, long prototypeID, long parentID);

      /// <summary>Принудительная остановка копирования структуры</summary>
      /// <param name="copierGuid">Глобальный идентификатор копирования</param>
      void StopCopyStructure(Guid copierGuid);

      /// <summary>Получить информацию об статусе копирования структуры</summary>
      /// <param name="copierGuid">Глобальный идентификатор копирования</param>
      /// <returns></returns>
      StructureCopierStateInfo GetCopyStructureInfo(Guid copierGuid);
    }
}
