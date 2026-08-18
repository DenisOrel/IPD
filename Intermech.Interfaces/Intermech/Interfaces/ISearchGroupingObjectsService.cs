
// Type: Intermech.Interfaces.ISearchGroupingObjectsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс серверной службы по поиску группирующих объектов
    /// </summary>
    public interface ISearchGroupingObjectsService
    {
      /// <summary>
      /// Список анализаторов (по одному анализатору на каждый режим поиска)
      /// </summary>
      string[] AnalyzerNames { get; }

      /// <summary>
      /// Начать выполнение поиска группирующих объектов, при необходимости добавить в список
      /// дополнительные идентификаторы версий объектов, которые были найдены в процессе анализа.
      /// Служба опрашивает все зарегистрированные в ней анализаторы для выполнения данного анализа.
      /// Вся работа выполняется на сервере в отдельном потоке, в рамках задания по анализу.
      /// </summary>
      /// <param name="searchMode">Режим поиска группирующих объектов</param>
      /// <param name="sessionGuid">Идентификатор сессии, в рамках которой выполняется анализ</param>
      /// <param name="searchObjects">Первоначальный список объектов, среди которых начинается поиск</param>
      /// <returns>Уникальный идентификатор задания, в рамках которого выполняется анализ.
      /// Значение Guid.Empty означает невозможность начать анализ</returns>
      Guid Analyze(
        Guid userSessionGuid,
        string ananyzerName,
        SearchGroupingObjects searchGroupingObjects);

      /// <summary>
      /// Запросить статус указанного задания на сервере. Если задание успешно или ошибочно
      /// завершено, вернётся полный пакет со статусом задания, а само задание будет
      /// удалено на серверной стороне вместе со своим потоком
      /// </summary>
      /// <param name="jobID">Задание</param>
      /// <returns>Статус указанного задания на сервере</returns>
      SearchGroupingObjectJobStatus QueryJobStatus(Guid jobID);

      /// <summary>Прервать указанное задание</summary>
      /// <param name="jobID">Задание</param>
      /// <returns>true, если задание было найдено и остановлено</returns>
      bool CancelJob(Guid jobID);

      /// <summary>Выполнить регистрацию анализатора в службе</summary>
      /// <param name="analyzer">Анализатор</param>
      /// <returns>true, если регистрация выполнена успешно</returns>
      void RegisterAnalyzer(ISearchGroupingObjectAnalyzer analyzer);

      /// <summary>Выполнить удаление анализатора из службы</summary>
      /// <param name="analyzer">Анализатор</param>
      /// <returns>true, если удаление выполнено успешно</returns>
      void UnregisterAnalyzer(ISearchGroupingObjectAnalyzer analyzer);
    }
}
