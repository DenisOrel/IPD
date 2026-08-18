
// Type: Intermech.Interfaces.IClientVersionRulesCacheService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс клиентского кэша для ускоренного чтения правил подбора версий, а также для работы
    /// с коллекцией вариантов [переменных значений для сравнения] правил подбора
    /// </summary>
    public interface IClientVersionRulesCacheService
    {
      /// <summary>
      /// Получить количество правил подбора версий, загруженных в кэш
      /// </summary>
      int Count { get; }

      /// <summary>
      /// Получить экземпляр класса VersionsRule, в котором хранится
      /// правило подбора версий, по указанному индексу кэша
      /// </summary>
      VersionsRule this[int Index] { get; }

      /// <summary>
      /// Получить экземпляр класса VersionsRule, в котором хранится
      /// правило подбора версий, по указанному OBJECT_ID. Если объект с указанным
      /// OBJECT_ID не найден в кэше, метод вернёт null
      /// </summary>
      VersionsRule this[long Object_ID] { get; }

      /// <summary>
      /// Получить экземпляр класса VersionsRule, в котором хранится
      /// правило подбора версий, по указанному OIBJECT_ID. Если объект с указанным
      /// OBJECT_ID не найден в кэше, то объект будет запрошен из базы данных
      /// с помощью usrSession.
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Object_ID">OBJECT_ID объекта с правилом</param>
      /// <returns></returns>
      VersionsRule this[object usrSession, long Object_ID] { get; }

      /// <summary>Очистить кэш</summary>
      void Clear();

      /// <summary>
      /// Удалить из кэша ссылку на указанный объект с OBJECT_ID
      /// </summary>
      /// <param name="Object_ID">Уникальный ID версии объекта</param>
      void Delete(long Object_ID);

      /// <summary>
      /// Метод загружает в кэш все объекты из базы с типами данных
      /// "Общие/персональные правила подбора версий"
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <returns>Вернёт количество загруженных объектов с правилами подбора версий</returns>
      int Load(object usrSession);

      /// <summary>
      /// Метод загружает в кэш указанное правило подбора версий (обновляет или добавляет)
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Object_ID">Идентификатор версии объекта-правила</param>
      /// <returns>Вернёт количество загруженных объектов с правилами подбора версий</returns>
      int LoadRule(object usrSession, long Object_ID);

      /// <summary>
      /// Метод загружает в кэш указанное правило подбора версий (обновляет или добавляет)
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Object_ID">Идентификатор версии объекта-правила</param>
      /// <param name="actualDate">Задаётся дата подбора составов для правила</param>
      /// <returns>Вернёт количество загруженных объектов с правилами подбора версий</returns>
      int LoadRule(object usrSession, long Object_ID, DateTime actualDate);

      /// <summary>
      /// Проверить наличие объекта правила подбора версий в кэше.
      /// </summary>
      /// <param name="Object_ID">OBJECT_ID объекта с правилом</param>
      /// <returns>true, если объект с указанным OBJECT_ID найден в кэше</returns>
      bool RuleExists(long Object_ID);

      /// <summary>
      /// Проверить наличие объекта правила подбора версий в кэше.
      /// Если в кэше объект не найден, проверить наличие объекта в базе данных.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Object_ID">OBJECT_ID объекта с правилом</param>
      /// <returns>true, если объект с указанным OBJECT_ID найден в кэше</returns>
      bool RuleExists(object usrSession, long Object_ID);

      /// <summary>
      /// Найти тип указанного правила и вернуть его. Исключения вываливаться не будут.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).</param>
      /// <param name="Object_ID">OBJECT_ID объекта с правилом</param>
      /// <returns>Тип правила подбора версий. Если правило не найдено, вернётся тип "Пользовательское правило"</returns>
      VersionsRuleType RuleType(object usrSession, long Object_ID);

      /// <summary>
      /// Получить список правил подбора версий, предназначенных для редактирования составов
      /// </summary>
      /// <returns>Список правил подбора версий, предназначенных для редактирования составов</returns>
      List<VersionsRule> GetEditingRules();

      /// <summary>
      /// Проверяет, требуются ли указанному правилу подбора версий варианты значений переменных
      /// (если в правиле нет переменных, то варианты значений не нужны)
      /// </summary>
      /// <param name="Rule_Object_ID">OBJECT_ID объекта с правилом</param>
      /// <returns>true - правилу требуются варианты значений</returns>
      bool NeedRuleVars(long Rule_Object_ID);

      /// <summary>
      /// Вернуть вариант значений переменных с указанным индексом для указанного правила
      /// </summary>
      /// <param name="UserID">ID пользователя, для которого требуется вариант значений переменных</param>
      /// <param name="index">Индекс запрашиваемого варианта значений переменных</param>
      /// <param name="Rule_Object_ID">OBJECT_ID объекта с правилом</param>
      /// <returns>Вариант значений переменных или null, если ничего не найдено</returns>
      VersionsRule GetRuleVars(long UserID, int index, long Rule_Object_ID);

      /// <summary>
      /// Установить вариант значений переменных с указанным индексом для указанного правила.
      /// Метод проверяет, совместим ли указанный вариант с объектом и если нет, то добавление
      /// не происходит. UserID берётся из сессии usrSession.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Rule_Object_ID">OBJECT_ID объекта с правилом</param>
      /// <param name="index">Индекс запрашиваемого варианта значений переменных</param>
      /// <param name="value">Новый вариант значений переменных</param>
      /// <returns>Вариант значений переменных или null, если ничего не найдено</returns>
      bool SetRuleVars(object usrSession, long Rule_Object_ID, int index, VersionsRule value);

      /// <summary>
      /// Вернуть количество вариантов значений переменных для указанного правила
      /// </summary>
      /// <param name="UserID">ID пользователя, для которого ведётся подсчёт вариантов значений переменных</param>
      /// <param name="Rule_Object_ID">OBJECT_ID правила подбора версий</param>
      /// <returns>Количество вариантов значений переменных для указанного правила</returns>
      int RuleVarsCount(long UserID, long Rule_Object_ID);

      /// <summary>
      /// Вернуть коллекцию вариантов значений для указанного объекта
      /// </summary>
      /// <param name="UserID">ID пользователя, для которого получаются варианты значений переменных</param>
      /// <param name="Rule_Object_ID">OBJECT_ID правила подбора версий</param>
      /// <returns>Коллекция вариантов значений для указанного правила или null</returns>
      ArrayList RuleVarsList(long UserID, long Rule_Object_ID);

      /// <summary>
      /// Добавить для указанного правила (Rule_Object_ID) новый вариант значений переменных.
      /// UserID берётся из сессии usrSession.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Vars">Вариант значений переменных (клон Rule_Object_ID)</param>
      /// <param name="Rule_Object_ID">OBJECT_ID правила для сравнения</param>
      /// <returns>-1 при ошибке или индекс вновь добавленного варианта значений переменных</returns>
      int RuleVarsAdd(object usrSession, VersionsRule Vars, long Rule_Object_ID);

      /// <summary>
      /// Удалить указанный вариант значений переменных [index] для правила (Rule_Object_ID).
      /// Если в списке только 1 элемент, удаление не будет выполнено.
      /// UserID берётся из сессии usrSession.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Rule_Object_ID">OBJECT_ID правила для сравнения</param>
      /// <param name="index">Номер варианта значений переменных (от 0 до RuleVarsCount-1)</param>
      /// <returns>true, если удаление прошло успешно</returns>
      bool RuleVarsDel(object usrSession, long Rule_Object_ID, int index);

      /// <summary>
      /// Сохранить в Configurations сессии варианты значений переменных текущего пользователя
      /// UserID берётся из сессии usrSession.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      void SaveRuleVars(object usrSession);

      /// <summary>
      /// Загрузить из Configurations сессии варианты значений переменных текущего пользователя
      /// UserID берётся из сессии usrSession.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      void LoadRuleVars(object usrSession);

      /// <summary>
      /// Получить настройки фильтрации состава для пользователя, UserID которого будет взят из указанной сессии
      /// </summary>
      /// <param name="OwnerID">Уникальный ключ владельца вариантов значений переменных</param>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      FiltrationSettings GetFiltrationSettings(object usrSession, string OwnerID);

      /// <summary>
      /// Получить настройки фильтрации состава для пользователя, UserID которого будет взят из указанной сессии
      /// </summary>
      /// <param name="OwnerID">Уникальный ключ владельца вариантов значений переменных</param>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="GetDefaults">Если true, вернёт настройки фильтрации состава по умолчанию</param>
      FiltrationSettings GetFiltrationSettings(object usrSession, string OwnerID, bool GetDefaults);

      /// <summary>
      /// Установить настройки фильтрации состава для пользователя, UserID которого будет взят из указанной сессии.
      /// Если указать value = null, то будет удалены настройки владельца OwnerID
      /// </summary>
      /// <param name="OwnerID">Уникальный ключ владельца вариантов значений переменных</param>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="value">Настройки фильтрации состава</param>
      bool SetFiltrationSettings(object usrSession, string OwnerID, FiltrationSettings value);

      /// <summary>
      /// Загрузить из Configurations сессии коллекцию настроек фильтрации состава для текущего пользователя.
      /// UserID будет взят из указанной сессии.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      bool LoadFiltrationTuning(object usrSession);

      /// <summary>
      /// Сохранить в Configurations сессии коллекцию настроек фильтрации состава для текущего пользователя.
      /// UserID будет взят из указанной сессии.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      void SaveFiltrationTuning(object usrSession);

      /// <summary>
      /// Сохранить в Configurations сессии коллекцию настроек фильтрации состава для указанного пользователя.
      /// </summary>
      /// <param name="UserID">Идентификатор пользователя, для которого надо сохранить настройки фильтрации</param>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      void SaveFiltrationTuning(long UserID, object usrSession);

      /// <summary>
      /// Удалить настройки фильтрации для указанного правила для текущего пользователя.
      /// UserID будет взят из указанной сессии.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Rule_Object_ID">OBJECT_ID объекта с правилом</param>
      /// <returns>true, если настройки были найдены и удалены</returns>
      bool DeleteRuleTuning(object usrSession, long Rule_Object_ID);

      /// <summary>
      /// Удалить настройки фильтрации для указанного владельца.
      /// UserID будет взят из указанной сессии.
      /// </summary>
      /// <param name="usrSession">Пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="OwnerID">Уникальный ключ владельца вариантов значений переменных</param>
      /// <returns>true, если настройки были найдены и удалены</returns>
      bool DeleteRuleTuning(object usrSession, string OwnerID);

      /// <summary>
      /// Вернуть правило фильтрации состава, которое соответствует указанным настройкам фильтрации.
      /// Если правило невалидно или недоступно, будет возвращено правило "Последние версии объектов" (defaults = true) или null.
      /// При удачном поиске будет возвращена копия правила (клон), а не ссылка на объект правила из кэша.
      /// UserID будет взят из сессии.
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Filtration">Настройки фильтрации состава, в которых описано правило подбора и его переменные</param>
      /// <param name="defaults">true - при ошибке вернуть стандартное правило "Последние версии объектов", иначе - null</param>
      /// <returns>Если правило невалидно или недоступно, будет возвращено правило "Последние версии объектов" (defaults = true) или null.
      /// Если указан неверный индекс варианта значений переменных, будет возвращено родительское правило.</returns>
      VersionsRule GetFiltrationRule(object usrSession, IFiltrationSettings Filtration, bool defaults);

      /// <summary>
      /// Вернуть правило фильтрации состава, которое соответствует указанным настройкам фильтрации.
      /// Если правило невалидно или недоступно, будет возвращен null.
      /// При удачном поиске будет возвращена копия правила (клон), а не ссылка на объект правила из кэша.
      /// UserID будет взят из сессии.
      /// </summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="Filtration">Настройки фильтрации состава, в которых описано правило подбора и его переменные</param>
      /// <param name="RuleCompatible">Вернёт результат проверки совместимости возвращаемого правила с его родительским вариантом</param>
      /// <param name="RuleValid">Вернёт результат проверки правила на корректность его полей</param>
      /// <param name="VarsOutOfRange">Вернёт true, если вариант значений для правила вне диапазона доступных значений</param>
      /// <returns>Если правило невалидно или недоступно, будет возвращен null.
      /// Если указан неверный индекс варианта значений переменных, будет возвращено родительское правило.</returns>
      VersionsRule GetFiltrationRule(
        object usrSession,
        IFiltrationSettings Filtration,
        ref bool RuleCompatible,
        ref bool RuleValid,
        ref bool VarsOutOfRange);

      /// <summary>Назначить правило подбора версий по умолчанию</summary>
      /// <param name="usrSession">usrSession - пользовательская сессия.
      /// При обращении к кэшу со стороны сервера сюда можно передавать
      /// ссылку на интерфейс IUserSession или GUID сессии (как строку или System.Guid).
      /// При обращении к кэшу со стороны клиента сюда можно передавать только GUID сессии
      /// (как строку или System.Guid).
      /// </param>
      /// <param name="RuleClass">Новое правило по умолчанию</param>
      /// <returns>true - операция успешно выполнена</returns>
      bool SetDefaultVersionsRule(object usrSession, VersionsRule RuleClass);

      /// <summary>Правило подбора версий "Все версии объектов"</summary>
      VersionsRule AllVersionsRule { get; }

      /// <summary>
      /// Правило подбора версий "Все версии объектов с учётом конкретизации"
      /// </summary>
      VersionsRule AllConcreteVersionsRule { get; }

      /// <summary>Правило подбора версий "Последние версии объектов"</summary>
      VersionsRule LatestVersionsRule { get; }

      /// <summary>Правило подбора версий "Подбор базовых версий"</summary>
      VersionsRule BaseVersionsRule { get; }

      /// <summary>
      /// Правило подбора версий "Последовательное проведение изменений"
      /// </summary>
      VersionsRule SequentialModificationsRule { get; }

      /// <summary>
      /// Правило подбора версий по умолчанию ("Подбор базовых версий")
      /// </summary>
      [Obsolete("Use GetDefaultVersionRule", true)]
      VersionsRule DefaultVersionsRule { get; }

      VersionsRule GetDefaultVersionRule(Guid userSessionGuid);
    }
}
