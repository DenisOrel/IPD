
// Type: Intermech.Interfaces.ApplicationServices
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.ApplicationModel;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Предоставляет ссылку на общедоступный контейнер глобальных сервисов приложения. Реализация является thread safe.
    /// </summary>
    public static class ApplicationServices
    {
      private static volatile ApplicationServiceContainer containerInstance = new ApplicationServiceContainer();

      /// <summary>
      /// Возвращает или задает общедоступный контейнер глобальных сервисов приложения.
      /// </summary>
      public static ApplicationServiceContainer Container
      {
        [DebuggerStepThrough] get => ApplicationServices.containerInstance;
        [DebuggerStepThrough] set => ApplicationServices.containerInstance = value;
      }
    }
}
