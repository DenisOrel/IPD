
// Type: Intermech.Interfaces.IVersionRulesCacheService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс кэша для ускоренного чтения правил подбора версий, а также для работы
    /// с коллекцией вариантов [переменных значений для сравнения] правил подбора
    /// </summary>
    public interface IVersionRulesCacheService : IClientVersionRulesCacheService
    {
      /// <summary>
      /// Метод загружает в кэш правило подбора версий (добавляет, обновляет или удаляет) для указанного уровня продвижения
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="LevelID">Идентификатор уровня продвижения, для которого надо обновить правило</param>
      void UpdateLifecycleRule(object usrSession, int LevelID);

      /// <summary>
      /// Загрузить из Configurations сессии коллекцию дополнительных настроек текущего пользователя.
      /// UserID будет взят из указанной сессии.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <returns>true, если загрузка прошла успешно</returns>
      bool LoadUserSettings(object usrSession);

      /// <summary>
      /// Сохранить дополнительные настройки текущего пользователя в базу данных
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      bool SaveUserSettings(object usrSession);

      /// <summary>
      /// Сохранить все настройки текущего пользователя в базу данных
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      bool Save(object usrSession);

      /// <summary>
      /// Сбросить настройки даты в правилах подбора для текущего пользователя, если это требуется
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      void ResetDateTime(object usrSession);

      /// <summary>Настроечный объект пользователя UserID по ключу Key</summary>
      /// <param name="UserID">ID пользователя</param>
      /// <param name="Key">Ключ для настроечного объекта</param>
      /// <returns>Настроечный объект (допускается null)</returns>
      object this[long UserID, object Key] { get; set; }

      /// <summary>
      /// Загрузить из базы данных коллекцию дополнительных настроек ролей
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <returns>true, если загрузка прошла успешно</returns>
      bool LoadRolesSettings(object usrSession);

      /// <summary>Сохранить дополнительные настройки роли в базу данных</summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="RoleID">ID роли, для которой надо сохранить настройки</param>
      bool SaveRolesSettings(object usrSession, long RoleID);

      /// <summary>
      /// Получить указанный настроечный объект для роли по ключу Key
      /// </summary>
      /// <param name="RoleID">ID роли</param>
      /// <param name="Key">Объект-ключ (сериализуемый)</param>
      /// <returns>Настроечный объект (сериализуемый) или null</returns>
      object GetRoleSettingsObject(long RoleID, object Key);

      /// <summary>
      /// Записать указанный настроечный объект для роли по ключу Key в кэш
      /// </summary>
      /// <param name="RoleID">ID роли</param>
      /// <param name="Key">Объект-ключ (сериализуемый)</param>
      /// <param name="value">Настроечный объект (сериализуемый) или null</param>
      void SetRoleSettingsObject(long RoleID, object Key, object value);

      /// <summary>
      /// Возвращает все идентификаторы владельцев настроек фильтрации.
      /// </summary>
      /// <param name="userSession">Идентификатор пользовательской сессии.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).</param>
      /// <returns>Массив идентификаторов</returns>
      string[] GetFiltrationSettingsList(object userSession);
    }
}
