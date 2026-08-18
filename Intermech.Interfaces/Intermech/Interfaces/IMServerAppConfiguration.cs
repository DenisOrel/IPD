
// Type: Intermech.Interfaces.IMServerAppConfiguration
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для получения конфигурации сервера приложений из app.config, которая используется для единообразной настройки клиентов этого сервера приложений.
    /// По соображениям безопасности сервис возвращает не всю конфигурацию сервера приложений, а только некоторое подмножество.
    /// </summary>
    [ClientSideDisconnectionProtection(false)]
    public interface IMServerAppConfiguration
    {
      /// <summary>
      /// Возвращает значение указанной опции из app.config сервера приложений из секции appSettings.
      /// </summary>
      /// <param name="optionName">Имя опции</param>
      /// <returns>Значение опции или null, если указанная опция не найдена, либо ее чтение запрещено по соображениям безопасности</returns>
      /// <exception cref="T:ArgumentNullException">optionName</exception>
      string GetConfigurationOption(string optionName);

      /// <summary>
      /// Возвращает значение ключа трассировки из app.config сервера приложений из секции system.diagnostics.
      /// </summary>
      /// <param name="switchName">Имя ключа трассировки</param>
      /// <returns>Значение ключа трассировки</returns>
      /// <exception cref="T:ArgumentNullException">switchName</exception>
      TraceLevel GetTraceSwitch(string switchName);

      /// <summary>
      /// Возвращает все значения опций и ключей трассировки из app.config сервера приложений.
      /// </summary>
      /// <returns>Кортеж из двух словарей: значений опций и значений ключей трассировки</returns>
      Tuple<Dictionary<string, string>, Dictionary<string, TraceLevel>> GetAll();
    }
}
