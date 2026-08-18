
// Type: Intermech.Interfaces.IObjectsChangingAnalyzerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс серверной службы анализаторов списка изменяемых объектов.
    /// </summary>
    public interface IObjectsChangingAnalyzerService
    {
      /// <summary>
      /// Начать выполнение анализа изменяемых объектов, при необходимости добавить в граф
      /// дополнительные идентификаторы версий объектов, которые тоже требуется изменить.
      /// На верхнем уровне - первоначальный список изменяемых версий объектов.
      /// Служба опрашивает все зарегистрированные в ней анализаторы для
      /// выполнения данного анализа.
      /// Вся работа выполняется на сервере в отдельном потоке, в рамках задания по анализу.
      /// </summary>
      /// <param name="action">Действие, выполняемое над объектами</param>
      /// <param name="sessionGuid">Идентификатор сессии, в рамках которой выполняется анализ</param>
      /// <param name="changingObjects">Список изменяемых версий объектов</param>
      /// <returns>Уникальный идентификатор задания, в рамках которого выполняется анализ.
      /// Значение Guid.Empty означает невозможность начать анализ</returns>
      Guid Analyze(ObjectChangingAction action, Guid sessionGuid, ChangingObjects changingObjects);

      /// <summary>
      /// Запросить статус указанного задания на сервере. Если задание успешно или ошибочно
      /// завершено, вернётся полный пакет со статусом задания, а само задание будет
      /// удалено на серверной стороне вместе со своим потоком
      /// </summary>
      /// <param name="jobID">Задание</param>
      /// <returns>Статус указанного задания на сервере</returns>
      ChangingAnalyzerJobStatus QueryJobStatus(Guid jobID);

      /// <summary>Прервать указанное задание</summary>
      /// <param name="jobID">Задание</param>
      /// <returns>true, если задание было найдено и остановлено</returns>
      bool CancelJob(Guid jobID);

      /// <summary>Выполнить регистрацию анализатора в службе</summary>
      /// <param name="analyzer">Анализатор</param>
      /// <returns>true, если регистрация выполнена успешно</returns>
      bool RegisterAnalyzer(IObjectsChangingAnalyzer analyzer);

      /// <summary>Выполнить удаление анализатора из службы</summary>
      /// <param name="analyzer">Анализатор</param>
      /// <returns>true, если удаление выполнено успешно</returns>
      bool UnregisterAnalyzer(IObjectsChangingAnalyzer analyzer);

      /// <summary>Выполнить удаление анализатора по его Guid из службы</summary>
      /// <param name="analyzerGuid">Guid анализатора</param>
      /// <returns>true, если удаление выполнено успешно</returns>
      bool UnregisterAnalyzer(Guid analyzerGuid);
    }
}
