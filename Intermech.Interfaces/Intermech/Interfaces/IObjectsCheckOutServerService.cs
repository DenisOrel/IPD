
// Type: Intermech.Interfaces.IObjectsCheckOutServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Серверный сервис, позволяющий брать на изменение группы объектов
    /// </summary>
    public interface IObjectsCheckOutServerService
    {
      /// <summary>Получить список версий объектов для редактирования.</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к сервису со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="versions">Список версий объектов, которые требуется взять на изменение</param>
      /// <param name="throwException">true - при ошибке сгенерировать исключение</param>
      /// <returns>Список описаний версий объектов для редактирования. Если возникла ошибка, будет возвращено значение null</returns>
      ObjectCheckedOutVersionsHolder CheckOut(
        object usrSession,
        IList<long> versions,
        bool throwException);

      /// <summary>Загрузить описания указанных версий объектов</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к сервису со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="versions">Список идентификаторов версий объектов</param>
      /// <param name="throwException">true - при ошибке сгенерировать исключение</param>
      /// <returns>Список описаний версий объектов. Если возникла ошибка, будет возвращено значение null</returns>
      List<ObjectCheckOutVersionDescription> LoadDescriptions(
        object usrSession,
        IList<long> versions,
        bool throwException);

      /// <summary>
      /// Выполнить откат произведённых изменений по списку-результату метода CheckOut
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к сервису со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к сервису со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="rollback">Список редактируемых версий объектов, полученных в методе CheckOut</param>
      /// <param name="throwException">true - при ошибке сгенерировать исключение</param>
      void Rollback(object usrSession, ObjectCheckedOutVersionsHolder rollback, bool throwException);
    }
}
