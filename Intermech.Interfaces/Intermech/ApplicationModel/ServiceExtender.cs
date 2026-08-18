
// Type: Intermech.ApplicationModel.ServiceExtender
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Базовый класс для создания расширений для сервисов приложения, которые изменяют поведение сервисов через события сервисов.
    /// Реализация не является thread safe.
    /// </summary>
    public abstract class ServiceExtender
    {
      private bool enabled;

      /// <summary>Активирует и деактивирует это расширение сервиса.</summary>
      public bool Enabled
      {
        [DebuggerStepThrough] get => this.enabled;
        set
        {
          if (this.enabled == value)
            return;
          this.EnabledChanging(value);
          this.enabled = value;
        }
      }

      private void EnabledChanging(bool newValue)
      {
        if (newValue)
          this.DoEnable();
        else
          this.DoDisable();
      }

      /// <summary>Активирует это расширение сервиса.</summary>
      protected virtual void DoEnable()
      {
      }

      /// <summary>Деактивирует это расширение сервиса.</summary>
      protected virtual void DoDisable()
      {
      }
    }
}
