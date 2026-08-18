
// Type: Intermech.Interfaces.IIMViewerServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс серверного сервиса интеграции с IMViewer.</summary>
    public interface IIMViewerServerService
    {
      /// <summary>
      /// Возвращает глобальные настройки интеграции с IMViewer.
      /// </summary>
      IMViewerSystemSettings Settings { get; }

      /// <summary>Обновляет глобальные настройки интеграции с IMViewer.</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="settings">Объект с настройками</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="session" /> содержит null; параметр <paramref name="settings" /> содержит null</exception>
      /// <exception cref="T:System.InvalidOperationException">значение параметра <paramref name="settings" /> должно быть заморожено</exception>
      void UpdateSettings(IUserSession session, IMViewerSystemSettings settings);

      /// <summary>
      /// Возвращает коллекцию идентификаторов типов документов, у которых могут быть связанные объекты IMViewer.
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>Коллекция для чтения идентификаторов типов документов</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="session" /> содержит null</exception>
      ICollection<int> GetSourceDocumentTypes(IUserSession session);

      /// <summary>Возвращает идентификатор типа объектов IMViewer.</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>Идентификатор типа объектов IMViewer</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="session" /> содержит null</exception>
      int GetViewerObjectType(IUserSession session);

      /// <summary>
      /// Проверяет, может ли у документа указанного типа быть связанный с ним объект IMViewer.
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="documentTypeId">Идентификатор типа документа</param>
      /// <returns>Результат проверки</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="session" /> содержит null</exception>
      /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
      bool CanHaveViewerObject(IUserSession session, int documentTypeId);

      /// <summary>
      /// Проверяет, имеется ли у указанного документа связанный с ним объект IMViewer.
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="documentId">Идентификатор версии документа</param>
      /// <param name="documentTypeId">Идентификатор типа документа</param>
      /// <returns>Результат проверки</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="session" /> содержит null</exception>
      /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
      bool HasViewerObject(IUserSession session, long documentId, int documentTypeId);

      /// <summary>
      /// Находит для указанного документа связанный с ним объект IMViewer.
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="documentId">Идентификатор версии документа</param>
      /// <param name="documentTypeId">Идентификатор типа документа</param>
      /// <returns>Идентификатор версии объекта IMViewer</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="session" /> содержит null</exception>
      /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
      long FindViewerObjectId(IUserSession session, long documentId, int documentTypeId);
    }
}
