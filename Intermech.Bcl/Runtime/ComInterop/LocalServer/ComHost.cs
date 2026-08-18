
// Type: Intermech.Runtime.ComInterop.LocalServer.ComHost
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Содержит глобальные COM-сервисы для приложения. Все методы и свойства класса являются thread safe.
    /// </summary>
    /// <remarks>
    /// Этот тип оставлен только для обратной совместимости с уже существующим кодом.
    /// Во всех остальных случаях следует использовать сервис типа <see cref="T:Intermech.Runtime.ComInterop.LocalServer.ComServer" />.
    /// </remarks>
    public static class ComHost
    {
      private static ComServer comServerInstance;
      private static readonly ServiceRef<ComServer> comServerRef = new ServiceRef<ComServer>();
      private static readonly ServiceRef<ComHostConfiguration> comServerConfigurationRef = new ServiceRef<ComHostConfiguration>();

      /// <summary>Возвращает конфигурацию COM-сервера.</summary>
      public static ComHostConfiguration Configuration
      {
        [DebuggerStepThrough] get => ComHost.comServerConfigurationRef.Value;
      }

      /// <summary>
      /// Создает и добавляет фабрику для указанного COM-объекта в список активных фабрик приложения.
      /// </summary>
      /// <param name="comObjectType">Тип COM-объекта</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comObjectType" /> не должен быть равен null</exception>
      public static void ActivateClassFactory(Type comObjectType)
      {
        if (comObjectType == (Type) null)
          throw new ArgumentNullException(nameof (comObjectType));
        ComServer comServer = ComHost.comServerRef.Value;
        if (comServer.IsComClassActive(comObjectType))
          return;
        comServer.ActivateComClass(comObjectType);
      }

      /// <summary>
      /// Удаляет фабрику для указанного COM-объекта из списка активных фабрик приложения.
      /// </summary>
      /// <param name="comObjectType">Тип COM-объекта</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comObjectType" /> не должен быть равен null</exception>
      public static void DeactivateClassFactory(Type comObjectType)
      {
        if (comObjectType == (Type) null)
          throw new ArgumentNullException(nameof (comObjectType));
        ComHost.comServerRef.Value.DeactivateComClass(comObjectType);
      }

      /// <summary>
      /// Возвращает или задает глобально доступный экземпляр COM-сервера.
      /// </summary>
      public static ComServer Instance
      {
        [DebuggerStepThrough] get => ComHost.comServerInstance;
        set
        {
          if (ComHost.comServerInstance == value)
            return;
          ComHost.comServerInstance = value;
          ComHost.comServerRef.Value = ComHost.comServerInstance;
          ComHost.comServerConfigurationRef.Value = new ComHostConfiguration(ComHost.comServerInstance);
        }
      }
    }
}
