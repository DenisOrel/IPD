
// Type: Intermech.Interfaces.IObjectsDeleteService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс серверной службы по удалению объектов</summary>
    public interface IObjectsDeleteService
    {
      /// <summary>
      /// Начать выполнение удаления объектов.
      /// Вся работа выполняется на сервере в отдельном потоке.
      /// </summary>
      /// <param name="sessionGuid">идентификатор сессии, в рамках которой выполняется удаление</param>
      /// <param name="deletingObjects">Список удаляемых объектов</param>
      /// <param name="mode">Режим удаления объектов</param>
      /// <returns>Уникальный идентификатор задания, в рамках которого выполняется удаление объектов.
      /// Значение Guid.Empty означает невозможность начать анализ</returns>
      Guid Delete(Guid sessionGuid, DeletingObjects deletingObjects, DeleteObjectsJobMode mode);

      /// <summary>
      /// Запросить статус указанного задания на сервере. Если задание успешно или ошибочно
      /// завершено, вернётся полный пакет со статусом задания, а само задание будет
      /// удалено на серверной стороне вместе со своим потоком
      /// </summary>
      /// <param name="jobID">Задание</param>
      /// <returns>Статус указанного задания на сервере</returns>
      DeleteObjectsJobStatus QueryJobStatus(Guid jobID);

      /// <summary>Прервать указанное задание</summary>
      /// <param name="jobID">Задание</param>
      /// <returns>Статус указанного задания на сервере</returns>
      DeleteObjectsJobStatus CancelJob(Guid jobID);

      /// <summary>Приостановить указанное задание</summary>
      /// <param name="jobID">Задание</param>
      /// <returns>Статус указанного задания на сервере</returns>
      DeleteObjectsJobStatus PauseJob(Guid jobID);

      /// <summary>Возобновить указанное задание в указанном режиме</summary>
      /// <param name="jobID">Задание</param>
      /// <param name="mode">Режим удаления объектов</param>
      /// <returns>Статус указанного задания на сервере</returns>
      DeleteObjectsJobStatus ResumeJob(Guid jobID, DeleteObjectsJobMode mode);
    }
}
