
// Type: Intermech.Interfaces.EventlogSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура для передачи и хранения настроек регистрации событий
    /// </summary>
    [Serializable]
    /// <summary>
    /// Создать экземпляр структуры для передачи и хранения настроек регистрации событий
    /// </summary>
    /// <param name="logOn">Включена/выключена регистрация всех событий</param>
    /// <param name="notLoggedObjects"> Идентификаторы объектов, для которых не требуется проводить регистрацию событий</param>
    /// <param name="notLoggedTypes">Идентификаторы типов, для которых не проводить регистрацию событий</param>
    /// <param name="autoClear">Автоматическая очистка журнала</param>
    /// <param name="recordsKeepDays">Сколько дней сохранять записи журнала</param>
    public struct EventlogSettings(
      bool logOn,
      long[] notLoggedObjects,
      int[] notLoggedTypes,
      bool autoClear,
      int recordsKeepDays)
    {
      /// <summary>Включена/выключена регистрация всех событий</summary>
      public bool LogOn = logOn;
      /// <summary>
      /// Идентификаторы объектов, для которых не требуется проводить регистрацию событий
      /// </summary>
      public long[] NotLoggedObjects = notLoggedObjects;
      /// <summary>
      /// Идентификаторы типов, для которых не проводить регистрацию событий
      /// </summary>
      public int[] NotLoggedTypes = notLoggedTypes;
      /// <summary>Автоматическая очистка журнала</summary>
      public bool AutoClear = autoClear;
      /// <summary>Сколько дней сохранять записи журнала</summary>
      public int RecordsKeepDays = recordsKeepDays;
    }
}
