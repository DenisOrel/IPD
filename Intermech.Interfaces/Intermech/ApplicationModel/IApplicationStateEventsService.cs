
// Type: Intermech.ApplicationModel.IApplicationStateEventsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Интерфейс сервиса событий изменения состояния приложения.
    /// </summary>
    public interface IApplicationStateEventsService
    {
      /// <summary>Событие завершения работы приложения.</summary>
      event EventHandler Exit;

      /// <summary>
      /// Событие аварийного завершения работы приложения. Метод обработчика должен выполнить только самые необходимые действия для
      /// очистки состояния приложения, так как у обработчика есть 2 секунды, чтобы обработать событие.
      /// </summary>
      event EventHandler EmergencyExit;
    }
}
