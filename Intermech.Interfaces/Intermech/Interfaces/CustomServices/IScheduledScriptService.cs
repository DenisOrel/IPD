
// Type: Intermech.Interfaces.CustomServices.IScheduledScriptService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.CustomServices
{
    /// <summary>Сервис скриптов планировщике</summary>
    public interface IScheduledScriptService
    {
      /// <summary>Зарегистрировать скрипт в сервисе</summary>
      /// <param name="sessionGuid">Guid пользовательской сессии</param>
      /// <param name="scriptInfo">Описание скрипта планировщика</param>
      void RegisterScript(Guid sessionGuid, ScheduledScriptInfo scriptInfo);

      /// <summary>Удалить скрипт из сервисе</summary>
      /// <param name="sessionGuid">Guid пользовательской сессии</param>
      /// <param name="scriptInfo">Описание скрипта планировщика</param>
      /// <param name="exceptionOnError">Генерировать исключение в случае ошибки</param>
      void RemoveScript(Guid sessionGuid, ScheduledScriptInfo scriptInfo, bool exceptionOnError = true);

      /// <summary>Обновить параметры скрипта в сервисе</summary>
      /// <param name="sessionGuid">Guid пользовательской сессии</param>
      /// <param name="scriptInfo">Описание скрипта планировщика</param>
      void UpdateScript(Guid sessionGuid, ScheduledScriptInfo scriptInfo);

      /// <summary>Выполнить скрипт "в ручном" режиме</summary>
      /// <param name="sessionGuid">Guid пользовательской сессии</param>
      /// <param name="scriptInfo">Описание скрипта планировщика</param>
      void ExecuteScript(Guid sessionGuid, ScheduledScriptInfo scriptInfo);
    }
}
