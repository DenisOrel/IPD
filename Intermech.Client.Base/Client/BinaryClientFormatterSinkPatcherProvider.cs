
// Type: Intermech.Client.BinaryClientFormatterSinkPatcherProvider
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Remoting;
using Intermech.Remoting.Optimized;
using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Threading;


namespace Intermech.Client
{
    /// <summary>
    /// Создает обертку над BinaryClientFormatterSink, необходимую для исправления ошибок в реализации remoting.
    /// </summary>
    internal sealed class BinaryClientFormatterSinkPatcherProvider : ClientFormatterSinkWrapperProvider
    {
      public BinaryClientFormatterSinkPatcherProvider(IDictionary properties, ICollection providerData)
        : base(properties, providerData)
      {
        BinaryClientFormatterSinkPatcherProvider.DynamicFormatterSinkInterceptorProvider interceptorProvider = new BinaryClientFormatterSinkPatcherProvider.DynamicFormatterSinkInterceptorProvider();
        properties[(object) "interceptors"] = (object) new Func<IClientFormatterSinkInterceptor>(interceptorProvider.TryGet);
      }

      protected override IClientFormatterSinkProvider CreateNativeProvider()
      {
        return (IClientFormatterSinkProvider) new OptimizedBinaryClientFormatterSinkProvider(this.Properties, this.ProviderData);
      }

      protected override IClientFormatterSink CreateNativeSinkWrapper(
        IChannelSender channel,
        string url,
        object remoteChannelData,
        IClientFormatterSink nativeSink)
      {
        return (IClientFormatterSink) new BinaryClientFormatterSinkPatcher(nativeSink);
      }

      /// <summary>
      /// Динамический провайдер объектов типа IClientFormatterSinkInterceptor.
      /// Реализация является thread safe, один экземпляр объекта используется всеми канальными приемниками.
      /// </summary>
      private sealed class DynamicFormatterSinkInterceptorProvider
      {
        private ThreadLocal<IClientFormatterSinkInterceptor> threadBoundInterceptor;

        /// <summary>Создает объект.</summary>
        public DynamicFormatterSinkInterceptorProvider()
        {
          this.threadBoundInterceptor = new ThreadLocal<IClientFormatterSinkInterceptor>();
        }

        /// <summary>Предоставляет значение</summary>
        /// <returns>Значение или null</returns>
        public IClientFormatterSinkInterceptor TryGet()
        {
          if (this.threadBoundInterceptor.Value == null)
          {
            Func<IClientFormatterSinkInterceptor> interceptorFactory = ClientRemotingDynamicSettings.Instance.FormatterSinkInterceptorFactory;
            if (interceptorFactory != null)
            {
              IClientFormatterSinkInterceptor formatterSinkInterceptor = interceptorFactory();
              if (formatterSinkInterceptor != null)
                this.threadBoundInterceptor.Value = formatterSinkInterceptor;
            }
          }
          return this.threadBoundInterceptor.Value;
        }
      }
    }
}
