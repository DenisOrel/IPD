
// Type: Intermech.Interfaces.WebPortal.IPortalTasksQueue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Интерфейс на серверную службу - очередь заданий работы с порталом
    /// </summary>
    public interface IPortalTasksQueue
    {
      /// <summary>
      /// Добавить задание на публикацию. Задача начнется по очереди
      /// </summary>
      /// <param name="sessionGuid">GUID пользовательской сессии из которой создается задача</param>
      /// <param name="taskName">Имя задачи, если не задано сгенерирует автоматически</param>
      /// <param name="priority">Приоритет задачи</param>
      /// <param name="composition">Публикуемый состав</param>
      /// <param name="options">Опции публикации</param>
      long PublishObjects(
        Guid sessionGuid,
        string taskName,
        TaskPriority priority,
        PublishComposition composition,
        ExtendedPublishOptions options);

      /// <summary>
      /// Добавить задание с созданием пакета на публикацию. Задача начнется по очереди
      /// </summary>
      /// <param name="sessionGuid">GUID пользовательской сессии из которой создается задача</param>
      /// <param name="taskName">Имя задачи, если не задано сгенерирует автоматически</param>
      /// <param name="priority">Приоритет задачи</param>
      /// <param name="composition">Публикуемый состав</param>
      /// <param name="options">Опции публикации</param>
      /// <param name="packet">Информация о создаваемом пакете</param>
      /// <param name="createReceipt">Нужно ли создавать квитанцию</param>
      /// <returns></returns>
      long PublishObjects(
        Guid sessionGuid,
        string taskName,
        TaskPriority priority,
        PublishComposition composition,
        ExtendedPublishOptions options,
        Packet4Publish packet,
        bool createReceipt);

      /// <summary>Начать выполнение задачи</summary>
      /// <param name="taskID"></param>
      /// <returns></returns>
      bool StartTask(long taskID);

      /// <summary>Завершение владением</summary>
      /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
      /// <param name="objectIDs">Список идентификаторов в базе портала опубликованных объектов, владение которыми завершается</param>
      /// <param name="objectGuids">Список глобальных идентификаторов в базе узла опубликованных объектов, владение которыми завершается</param>
      /// <param name="ownerSites">Строка с кодами узлов с правами владения на эти объекты</param>
      /// <param name="withComposition">Завершить владение вместе с составом</param>
      /// <param name="autoUpdate">Получать обновления об изменениях у этих объектов</param>
      void OwnComplete(
        Guid sessionGuid,
        long[] objectIDs,
        Guid[] objectGuids,
        string ownerSites,
        bool withComposition,
        bool autoUpdate);

      /// <summary>
      /// Функция получения обновления из портала по его глобальному идентификатору
      /// </summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
      /// <param name="updateGuid">Глобальный идентификатор обновления/запроса импорта</param>
      void StartUpdate(Guid sessionGuid, string updateGuid, object tag);
    }
}
