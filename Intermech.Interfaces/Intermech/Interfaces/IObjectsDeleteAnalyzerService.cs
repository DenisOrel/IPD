
// Type: Intermech.Interfaces.IObjectsDeleteAnalyzerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс серверной службы анализаторов списка удаляемых объектов.
    /// </summary>
    public interface IObjectsDeleteAnalyzerService
    {
      /// <summary>Заполнить описания в указанных объектах</summary>
      /// <param name="sessionGuid">идентификатор сессии, в рамках которой выполняется анализ</param>
      /// <param name="deletingObjects">Список удаляемых версий объектов</param>
      /// <returns>Список удаляемых объектов, у которых заполнены все поля</returns>
      DeletingObjects LoadDescriptions(Guid sessionGuid, DeletingObjects deletingObjects);

      /// <summary>
      /// Начать выполнение анализа удаляемых объектов, при необходимости добавить в граф
      /// дополнительные идентификаторы версий объектов, которые тоже требуется удалить.
      /// На верхнем уровне - первоначальный список удаляемых версий объектов.
      /// Служба опрашивает все зарегистрированные в ней анализаторы для
      /// выполнения данного анализа.
      /// Вся работа выполняется на сервере в отдельном потоке, в рамках задания по анализу.
      /// </summary>
      /// <param name="sessionGuid">идентификатор сессии, в рамках которой выполняется анализ</param>
      /// <param name="deletingObjects">Список удаляемых версий объектов</param>
      /// <param name="options">Параметры</param>
      /// <returns>Уникальный идентификатор задания, в рамках которого выполняется анализ.
      /// Значение Guid.Empty означает невозможность начать анализ</returns>
      Guid Analyze(Guid sessionGuid, DeletingObjects deletingObjects, DeleteAnalyzerOptions options);

      /// <summary>
      /// Запросить статус указанного задания на сервере. Если задание успешно или ошибочно
      /// завершено, вернётся полный пакет со статусом задания, а само задание будет
      /// удалено на серверной стороне вместе со своим потоком
      /// </summary>
      /// <param name="jobID">Задание</param>
      /// <returns>Статус указанного задания на сервере</returns>
      DeleteAnalyzerJobStatus QueryJobStatus(Guid jobID);

      /// <summary>Прервать указанное задание</summary>
      /// <param name="jobID">Задание</param>
      /// <returns>true, если задание было найдено и остановлено</returns>
      bool CancelJob(Guid jobID);

      /// <summary>Выполнить регистрацию анализатора в службе</summary>
      /// <param name="analyzer">Анализатор</param>
      /// <returns>true, если регистрация выполнена успешно</returns>
      bool RegisterAnalyzer(IObjectsDeleteAnalyzer analyzer);

      /// <summary>Выполнить удаление анализатора из службы</summary>
      /// <param name="analyzer">Анализатор</param>
      /// <returns>true, если удаление выполнено успешно</returns>
      bool UnregisterAnalyzer(IObjectsDeleteAnalyzer analyzer);

      /// <summary>Выполнить удаление анализатора по его Guid из службы</summary>
      /// <param name="analyzerGuid">Guid анализатора</param>
      /// <returns>true, если удаление выполнено успешно</returns>
      bool UnregisterAnalyzer(Guid analyzerGuid);
    }
}
