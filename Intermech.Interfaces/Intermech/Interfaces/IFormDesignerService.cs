
// Type: Intermech.Interfaces.IFormDesignerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс серверной части дизайнера форм.</summary>
    public interface IFormDesignerService
    {
      /// <summary>Добавить/обновить информацию в кэше.</summary>
      /// <param name="iDBObj">Форма</param>
      void AddToCache(IDBObject iDBObj);

      /// <summary>Добавить/обновить информацию в кэше.</summary>
      /// <param name="iDBObj">Форма</param>
      /// <param name="iDBAttr">Атрибут</param>
      void AddToCache(IDBObject iDBObj, IDBAttribute iDBAttr);

      /// <summary>Изменение информации о наименовании формы</summary>
      /// <param name="iDBObj">Форма</param>
      void ChangeFormsCaption(IDBObject iDBObj);

      /// <summary>Корректировка кэша.</summary>
      /// <param name="iDBObj">Объект</param>
      /// <param name="hasFormula">Задано ли условие</param>
      /// &gt;
      void ChangeFormsCondition(IDBObject iDBObj, bool hasFormula);

      /// <summary>Корректировка кэша.</summary>
      /// <param name="iDBObj">Объект</param>
      /// <param name="formula">Условие</param>
      void ChangeFormsCondition(IDBObject iDBObj, object formula);

      /// <summary>Корректировка кэша.</summary>
      /// <param name="iDBObj">Объект</param>
      void ChangeFormsVisible(IDBObject iDBObj);

      /// <summary>Корректировка кэша.</summary>
      /// <param name="iDBObj">Объект</param>
      /// <param name="typeID">Идентификатор типа объектов</param>
      /// <param name="bValue">Значение</param>
      void ChangeFormsVisibleForUserCache(IDBObject iDBObj, int typeID, bool bValue);

      /// <summary>Изменение информации о владельце формы.</summary>
      /// <param name="iDBObj">Форма</param>
      void CheckInForm(IDBObject iDBObj);

      /// <summary>Изменение информации о владельце формы.</summary>
      /// <param name="iDBObj">Форма</param>
      void CheckOutForm(IDBObject iDBObj);

      /// <summary>
      /// Пометить форму, как удаленную. Полностью удаляется из кэша при чекине формы.
      /// </summary>
      /// <param name="iDBObj">Форма</param>
      /// <param name="typesID">Идентификатор типа объектов/связей</param>
      /// <param name="iDBAttr">Атрибут</param>
      void MarkAsRemoved(IDBObject iDBObj, int typesID, IDBAttribute iDBAttr);

      /// <summary>Удаление из кэша.</summary>
      /// <param name="iDBObj">Форма</param>
      void RemoveFromCache(IDBObject iDBObj);

      /// <summary>
      /// Удаление формы из локального кэша для текущего пользователя.
      /// </summary>
      /// <param name="iDBObj">Форма</param>
      /// <param name="typesID">Тип объекта/связи. Если -1, то удаляется форма для всех типов объектов/связей.</param>
      void RemoveFormFromUserCacheToCurrUser(IDBObject iDBObj, int typesID);

      /// <summary>Удаление из кэша типа объектов/связей.</summary>
      /// <param name="typesID">Идентификатор типа объектов/связей</param>
      /// <param name="kind">Тип элемента</param>
      void RemoveTypeFromCache(int typesID, AttributableElements kind);

      /// <summary>Отмена изменений.</summary>
      /// <param name="iDBObj">Форма</param>
      void UndoCheckOutForm(IDBObject iDBObj);

      /// <summary>
      /// Очистка кеша версий форм редактированиядля всех пользователей
      /// </summary>
      void ClearUserVersionCache();

      /// <summary>Очистка кеша версий форм редактирования пользователя</summary>
      /// <param name="userID"></param>
      void ClearUserVersionCache(long userID);

      /// <summary>
      /// Возвращает коллекцию форм для заданных идентификаторов
      /// </summary>
      /// <param name="objectIDs">IDs объектов</param>
      /// <param name="kind"></param>
      /// <param name="sessionID">GUID сессии пользователя</param>
      /// <param name="checkVisibility">Фильтрация форм с учетом видимости для пользователя / роли</param>
      /// <returns>Коллекция форм</returns>
      ICollection<FormInformation> GetForms(
        long[] objectIDs,
        AttributableElements kind,
        Guid sessionID,
        bool checkVisibility = false);

      /// <summary>
      /// Возвращает коллекцию форм для заданного объекта и пользователя.
      /// </summary>
      /// <param name="objectID">ID объекта</param>
      /// <param name="sessionID">GUID сессии пользователя</param>
      /// <returns>Коллекция форм</returns>
      ICollection<FormInformation> GetFormsForObject(long objectID, Guid sessionID);

      /// <summary>
      /// Возвращает коллекцию форм для заданного объекта и пользователя.
      /// </summary>
      /// <param name="objectID">ID объекта</param>
      /// <param name="relationID">ID связи от объекта к родителю</param>
      /// <param name="sessionID">GUID сессии пользователя</param>
      /// <returns>Коллекция форм</returns>
      ICollection<FormInformation> GetFormsForObject(long objectID, long relationID, Guid sessionID);

      /// <summary>Взять формы для типа объектов/связей.</summary>
      /// <param name="typesID">Идентификатор типа объектов/связей</param>
      /// <param name="kind">Тип элемента</param>
      /// <returns>Список форм</returns>
      Dictionary<FormInformation, bool[]> GetFormsForObjectsType(int typesID, AttributableElements kind);

      /// <summary>Возвращает коллекцию форм для заданного типа объектов</summary>
      /// <param name="objectTypeID">ID типа объекта</param>
      /// <param name="sessionID">ID сессии пользователя</param>
      /// <returns>Коллекция форм</returns>
      ICollection<FormInformation> GetFormsForObjectType(int objectTypeID, Guid sessionID);

      /// <summary>Возвращает коллекцию форм для заданного типа связей.</summary>
      /// <param name="relationID">ID связи</param>
      /// <param name="sessionID">GUID сессии пользователя</param>
      /// <returns>Коллекция форм</returns>
      ICollection<FormInformation> GetFormsForRelation(long relationID, Guid sessionID);

      /// <summary>Возвращает коллекцию форм для заданного типа объектов</summary>
      /// <param name="relationTypeID">ID типа связи</param>
      /// <param name="sessionID">ID сессии пользователя</param>
      /// <returns>Коллекция форм</returns>
      ICollection<FormInformation> GetFormsForRelationType(int relationTypeID, Guid sessionID);

      /// <summary>
      /// Получение списка объектов (папок / каталогов) Imbase, вверх по иерархии,
      /// содержащих формы редактирования
      /// </summary>
      /// <param name="imbaseObjID">Ид. версии объекта Imbase</param>
      /// <param name="sessionGuid">Сессия подключения к базе</param>
      /// <returns></returns>
      DataTable GetImbaseObjectsWithForms(long imbaseObjID, Guid sessionGuid);

      /// <summary>
      /// Получения списка форм по ид. версии объекта Imbase c проверкой родительских узлов
      /// </summary>
      /// <param name="imbaseObjID">Ид. версии объекта Imbase</param>
      /// <param name="objectTypeIDs">Ид. типов объекта (к которым привязаны формы - НЕ ОБЪЕКТЫ IMBASE) (могут быть не заданы) для дополнительной фильтрации </param>
      /// <param name="objTable">Перечень объектов Imbase с формами редактирования (полученный через GetImbaseObjectsWithForms)</param>
      /// <param name="sessionGuid">Сессия подключения к базе</param>
      /// <returns></returns>
      List<long> GetFormsByImbaseObject(
        long imbaseObjID,
        int[] objectTypeIDs,
        DataTable objTable,
        Guid sessionGuid);

      /// <summary>
      /// Добавление информации об индексах отображения форм редактирования для типа объектов/связи.
      /// </summary>
      /// <param name="typeGuid">Глобальный идентификатор типа объектов/связи</param>
      /// <param name="dict">Список глобальных идентификаторов форм редактирования и соответствующих им индексов отображения</param>
      /// <remarks>Данный метод совмещает уже имеющийся список с новым списком. Для уже существующих элементов информация обновляется</remarks>
      void AddFormDisplayOrderForType(Guid typeGuid, Dictionary<Guid, int> dict);

      /// <summary>
      /// Удаление информации об индексах отображения форм редактирования для типа объектов/связи.
      /// </summary>
      /// <param name="typeGuid">Глобальный идентификатор типа объектов/связи</param>
      void ClearFormDisplayOrderForType(Guid typeGuid);

      /// <summary>
      /// Получение информации об индексах отображения форм редактирования для типа объектов/связи.
      /// </summary>
      /// <param name="typeGuid">Глобальный идентификатор типа объектов/связи</param>
      /// <returns>Информация об индексах отображения форм редактирования</returns>
      Dictionary<Guid, int> GetFormDisplayOrderForType(Guid typeGuid);

      /// <summary>
      /// Удаление информации об индексах отображения форм редактирования, при удалении формы у типа объектов/связи, для типа объектов/связи.
      /// </summary>
      /// <param name="typeGuid">Глобальный идентификатор типа объектов/связи</param>
      /// <param name="guids">Список глобальных идентификаторов форм редактирования</param>
      void RemoveFormDisplayOrderForType(Guid typeGuid, List<Guid> guids);

      /// <summary>
      /// Замена информации об индексах отображения форм редактирования для типа объектов/связи.
      /// </summary>
      /// <param name="typeGuid">Глобальный идентификатор типа объектов/связи</param>
      /// <param name="dict">Список глобальных идентификаторов форм редактирования и соответствующих им индексов отображения</param>
      /// <remarks>Имеющийся список заменяется новым списком</remarks>
      void SetFormDisplayOrderForType(Guid typeGuid, Dictionary<Guid, int> dict);

      /// <summary>Очистка кэша с переинициализацией</summary>
      void FlushCache();
    }
}
