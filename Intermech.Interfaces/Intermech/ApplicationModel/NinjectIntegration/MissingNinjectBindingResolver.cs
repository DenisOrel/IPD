
// Type: Intermech.ApplicationModel.NinjectIntegration.MissingNinjectBindingResolver
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Ninject.Activation;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Planning.Bindings;
using Ninject.Planning.Bindings.Resolvers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Реализует компонент, расширяющий возможности Ninject по поиску неявных привязок.
    /// Компонент выполняет поиск и создание привязок для отсутствующих зависимостей, используя локатор сервисов приложения.
    /// Реализация является thread safe.
    /// </summary>
    internal sealed class MissingNinjectBindingResolver : 
      NinjectComponent,
      IMissingBindingResolver,
      INinjectComponent,
      IDisposable
    {
      private ApplicationServiceContainer serviceLocator;

      /// <summary>Создает объект.</summary>
      public MissingNinjectBindingResolver() => this.serviceLocator = ApplicationServices.Container;

      /// <summary>Возвращает локатор сервисов приложения.</summary>
      private ApplicationServiceContainer ServiceLocator
      {
        [DebuggerStepThrough] get => this.serviceLocator;
      }

      public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, IRequest request)
      {
        Type service = request.Service;
        object serviceImpl = this.ServiceLocator.GetOrResolveService(service, false);
        if (serviceImpl == null)
          return Enumerable.Empty<IBinding>();
        return (IEnumerable<IBinding>) new Binding[1]
        {
          new Binding(service)
          {
            ProviderCallback = (Func<IContext, IProvider>) (ctx => (IProvider) new MissingNinjectBindingResolver.ServiceLocatorServiceProvider(serviceImpl))
          }
        };
      }

      private sealed class ServiceLocatorServiceProvider : IProvider
      {
        private object serviceImpl;

        public ServiceLocatorServiceProvider(object serviceImpl) => this.serviceImpl = serviceImpl;

        public object Create(IContext context) => this.serviceImpl;

        public Type Type => this.serviceImpl.GetType();
      }
    }
}
