
// Type: Intermech.ApplicationModel.ApplicationStateService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Сервис событий изменения состояния приложения. Реализация является thread safe.
    /// </summary>
    public class ApplicationStateService : IApplicationStateEventsService
    {
      private object syncRoot;
      private volatile bool exitEventFired;
      private volatile bool emergencyExitEventFired;

      /// <summary>Создает объект.</summary>
      public ApplicationStateService() => this.syncRoot = new object();

      /// <summary>Рассылает событие о завершении работы приложения.</summary>
      public void RaiseExit()
      {
        lock (this.syncRoot)
        {
          if (this.exitEventFired)
            return;
          this.exitEventFired = true;
          EventHandler exit = this.Exit;
          if (exit == null)
            return;
          exit((object) null, EventArgs.Empty);
        }
      }

      /// <summary>
      /// Рассылает событие об аварийном завершении работы приложения.
      /// </summary>
      public void RaiseEmergencyExit()
      {
        lock (this.syncRoot)
        {
          if (this.emergencyExitEventFired)
            return;
          this.emergencyExitEventFired = true;
          EventHandler emergencyExit = this.EmergencyExit;
          if (emergencyExit == null)
            return;
          emergencyExit((object) null, EventArgs.Empty);
        }
      }

      /// <summary>Событие завершения работы приложения.</summary>
      public event EventHandler Exit;

      /// <summary>
      /// Событие аварийного завершения работы приложения. Метод обработчика должен выполнить только самые необходимые действия для
      /// очистки состояния приложения, так как у обработчика есть 2 секунды, чтобы обработать событие.
      /// </summary>
      public event EventHandler EmergencyExit;
    }
}
