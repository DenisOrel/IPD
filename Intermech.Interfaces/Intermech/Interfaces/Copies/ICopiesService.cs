
// Type: Intermech.Interfaces.Copies.ICopiesService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Copies
{
    /// <summary>
    /// Сервис для работы с копиями документов и листом рассылки
    /// </summary>
    public interface ICopiesService
    {
      /// <summary>Проверка, есть ли у документа любые копии</summary>
      /// <param name="objectId">Версия обьекта</param>
      /// <returns>true - если есть копии</returns>
      bool DocumentHasCopies(long objectId, object sessionID);

      /// <summary>Получить список копий объекта</summary>
      /// <param name="objectId">Версия обьекта</param>
      /// <returns>true - если есть копии</returns>
      List<long> GetDocumentsCopies(long objectId, object sessionID);

      /// <summary>
      /// Записывает значения атрибутов, связанных с инвентарными номерами.
      /// </summary>
      /// <param name="objectId">Id объекта.</param>
      /// <param name="inventoryNumber">Инвентарный номер.</param>
      /// <param name="registrationDateTime">Дата регистрации в ОТД.</param>
      /// <param name="invNumberAttrValues">Значение атрибута Инвентарный номер, которое нужно для рассылки уведомлений.</param>
      void SetInventoryNumberAttributes(
        Guid sessionGuid,
        long objectId,
        string inventoryNumber,
        DateTime registrationDateTime,
        out AttributeValues invNumberAttrValues);

      /// <summary>Создает лист рассылки для документа.</summary>
      /// <param name="sessionGuid">GUID сессии.</param>
      /// <param name="objectID">ID версии объекта.</param>
      /// <returns>ИД листа расслыки документа</returns>
      long CreateDeliveryList(Guid sessionGuid, long objectID);

      /// <summary>
      /// Получает ИД листа рассылки документа.
      /// Возвращает Intermech.Consts.UnknownObjectId, если листа рассылки нет.
      /// Принимает id документа, а не версии.
      /// </summary>
      /// <param name="sessionGuid">Сессия</param>
      /// <param name="id">ИД документа (не версии).</param>
      /// <returns>ИД листа рассылки документа</returns>
      long GetDeliveryListID(Guid sessionGuid, long id);

      /// <summary>
      /// Получает ИД листа рассылки документа.
      /// Возвращает Intermech.Consts.UnknownObjectId, если листа рассылки нет.
      /// </summary>
      /// <param name="sessionGuid">Сессия</param>
      /// <param name="id">ИД версии документа.</param>
      /// <returns>ИД листа рассылки документа</returns>
      long GetObjectVersionDeliveryListID(Guid sessionGuid, long objectId);

      /// <summary>Создать копии по листу рассылки</summary>
      /// <param name="sessionGuid">Гуид сесии</param>
      /// <param name="docObjectId">Документ, для которого создаем копии</param>
      /// <param name="mindSendedCopies">Учитывать ли количество уже высланных абоненту копий при создании</param>
      void CreateCopiesByDeliveryList(Guid sessionGuid, long docObjectId, bool mindSendedCopies);

      /// <summary>
      /// получить лист рассылки для указанного типа объектов
      /// (id абонента - количество копий)
      /// </summary>
      /// <param name="objTypeID"></param>
      /// <returns></returns>
      Dictionary<long, int> GetSubscribers(int objTypeID);

      /// <summary>
      /// добавить абонентов в лист рассылки для указанного типа документов
      /// </summary>
      /// <param name="objTypeID">тип документов </param>
      /// <param name="list">(id абонента - количество копий)</param>
      /// <param name="sessionID"></param>
      void ChangeSubscribers(int objTypeID, Dictionary<long, int> list, object sessionID);

      /// <summary>
      /// Добавляет подписчиков в листы рассылки.
      /// Это дубляж метода из DocumentCommandsProvider, не учитывающий рассылку сообщений.
      /// </summary>
      /// <param name="sessionGuid">Гуид сессии</param>
      /// <param name="copiedDeliveryListID">Копируемый лист рассылки</param>
      /// <param name="deliveryLists">Листы рассылки, в которые происходит добавление</param>
      void AddSubcribersToDeliveryLists(
        Guid sessionGuid,
        long copiedDeliveryListID,
        List<long> deliveryLists);

      /// <summary>Заменить абонентов в листах рассылки.</summary>
      /// <param name="sessionGuid">Гуид сессии</param>
      /// <param name="copiedDeliveryListID">Копируемый лист рассылки.</param>
      /// <param name="deliveryLists">Листы рассылки, в которых происходит замена абонентов.</param>
      void ReplaceSubscribersInDeliveryLists(
        Guid sessionGuid,
        long copiedDeliveryListID,
        List<long> deliveryLists);

      /// <summary>Получить листы рассылки со всеми данными</summary>
      /// <param name="sessionGuid"></param>
      /// <param name="deliveryListsIds"></param>
      /// <returns></returns>
      List<DeliveryList> GetDeliveryLists(Guid sessionGuid, List<long> deliveryListsIds);

      /// <summary>Сохранить информацию о листах рассылки</summary>
      /// <param name="sessionGuid">Сессия</param>
      /// <param name="deliveryLists"></param>
      /// <returns></returns>
      void SaveDeliveryLists(Guid sessionGuid, List<DeliveryList> deliveryLists);

      /// <summary>
      /// Добавить абонентов из листа рассылки извещения в лист рассылки документа.
      /// При отсутствии ЛР документа он создается
      /// </summary>
      /// <param name="ecoDeliveryListID">ИД листа рассылки извещения</param>
      /// <param name="docID">ИД документа.</param>
      /// <param name="docObjID">ИД версии документа</param>
      /// <param name="sessionGuid">Сессия</param>
      void AddSubscrsFromEcoToDoc(long ecoDeliveryListID, long docID, long docObjID, Guid sessionGuid);

      /// <summary>
      /// Вернуть  формулу для типа объекта (если формулы нет - null)
      /// </summary>
      /// <param name="objTypeID"></param>
      /// <returns></returns>
      object GetFormula(int objTypeID);

      /// <summary>
      /// Вернуть  формулу для типа объекта
      /// (если формулы нет - рекурсивного искать у родителького)
      /// </summary>
      /// <param name="objTypeID"></param>
      /// <returns></returns>
      string GetFormulaRecursive(int objTypeID);

      /// <summary>Изменить формулы для типов объектов</summary>
      /// <param name="formulas"></param>
      /// <param name="sessionID"></param>
      void ChangeFormula(Dictionary<int, string> formulas, object sessionID);

      /// <summary>
      /// Вернуть список классификаторов для вычисления номера ОТД
      /// </summary>
      List<long> Classifiers { get; }

      /// <summary>
      /// изменить список id-ков классификаторов для генерации номера ОТД
      /// </summary>
      /// <param name="classifiersID">новый список</param>
      /// <param name="sessionID">сессия</param>
      void ChangeClassifiers(List<long> classifiersID, object sessionID);

      /// <summary>
      /// Удалить объект из кэша подписчиков по умолчанию на тип объекта
      /// </summary>
      /// <param name="objectId">Ид объекта</param>
      /// <param name="sessionGuid"></param>
      void RemoveObjectFromSubscribersDictionary(long objectId, Guid sessionGuid);

      string GetWarningAboutExceededCopies(
        Dictionary<long, int> copiesForDocsCount,
        long subsriberId,
        Guid sessionGuid);

      /// <summary>
      /// Получает соответствие атрибута Инвентарный номер для родительской версии объектов из поданного списка
      /// </summary>
      /// <param name="sessionGuid">Сессия</param>
      /// <param name="Ids">Версии объектов, для которых надо получить данные</param>
      /// <returns>Соответствие атрибута Инвентарный номер для родительской версии объектов из поданного списка</returns>
      List<(long Id, string NameInMessage, long ParentId, string ParentInventoryNumber)> GetObjectsParentsInventoryNumbers(
        Guid sessionGuid,
        List<long> Ids);
    }
}
