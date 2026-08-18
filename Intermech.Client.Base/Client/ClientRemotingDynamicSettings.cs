
// Type: Intermech.Client.ClientRemotingDynamicSettings
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Remoting.Optimized;
using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Client
{
    /// <summary>
    /// Сервис динамических настроект Remoting для клиента IPS.
    /// Реализация является thread safe.
    /// </summary>
    /// <remarks>
    /// Сервис используется канальными приемниками клиента IPS для получения настроек,
    /// которые невозможно или сложно передать через свойства приемника в app.config.
    /// </remarks>
    public sealed class ClientRemotingDynamicSettings
    {
      private volatile Func<IClientFormatterSinkInterceptor> formatterSinkInterceptorFactory;
      private static readonly ClientRemotingDynamicSettings instance = new ClientRemotingDynamicSettings();

      /// <summary>
      /// Возвращает или задает фабрику перехватчиков для синхронных вызовов через remoting.
      /// Значение свойства может быть не задано и равно null. Значение свойства можно
      /// изменять в процессе работы приложения.
      /// </summary>
      public Func<IClientFormatterSinkInterceptor> FormatterSinkInterceptorFactory
      {
        [DebuggerStepThrough] get => this.formatterSinkInterceptorFactory;
        [DebuggerStepThrough] set
        {
          Interlocked.Exchange<Func<IClientFormatterSinkInterceptor>>(ref this.formatterSinkInterceptorFactory, value);
        }
      }

      /// <summary>Возвращает глобальный экземпляр сервиса.</summary>
      public static ClientRemotingDynamicSettings Instance
      {
        [DebuggerStepThrough] get => ClientRemotingDynamicSettings.instance;
      }
    }
}
