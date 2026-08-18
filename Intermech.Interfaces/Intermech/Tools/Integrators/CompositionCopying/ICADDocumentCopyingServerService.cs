
// Type: Intermech.Tools.Integrators.CompositionCopying.ICADDocumentCopyingServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Data;


namespace Intermech.Tools.Integrators.CompositionCopying
{
    /// <summary>
    /// Интерфейс серверного сервиса, обслуживающего задачи копирования 3D-моделей CAD-систем.
    /// </summary>
    public interface ICADDocumentCopyingServerService
    {
      /// <summary>
      /// Создает новый объект заданного типа путем клонирования существующего объекта IPS.
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="targetObjectTypeId">Тип создаваемого объекта IPS</param>
      /// <param name="sourceObjectId">Идентификатор версии существующего объекта IPS</param>
      /// <returns>Созданный объект-клон</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="session" /> содержит null</exception>
      /// <exception cref="T:System.ArgumentException">параметр <paramref name="targetObjectTypeId" /> содержит некорректное значение; параметр <paramref name="sourceObjectId" /> содержит некорректное значение</exception>
      IDBObject CloneObject(IUserSession session, int targetObjectTypeId, long sourceObjectId);

      /// <summary>
      /// Метод для загрузки чертежей или изделий по заданным параметрам
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="relationType">тип связи для получения коллекции</param>
      /// <param name="filtrationOwnerID">фильрация</param>
      /// <param name="dbRecordSetParams">параметры запроса</param>
      /// <returns></returns>
      DataTable LoadDrawingsOrArticles(
        IUserSession session,
        int relationType,
        string filtrationOwnerID,
        DBRecordSetParams dbRecordSetParams);
    }
}
