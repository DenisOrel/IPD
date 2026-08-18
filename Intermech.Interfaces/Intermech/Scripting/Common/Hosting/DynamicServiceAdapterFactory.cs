
// Type: Intermech.Scripting.Common.Hosting.DynamicServiceAdapterFactory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Castle.DynamicProxy;
using Intermech.ApplicationModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting;


namespace Intermech.Scripting.Common.Hosting
{
    /// <summary>
    /// Класс фабрики адаптеров обращений к сервисам приложения.
    /// С помощью таких адаптеров сценарии могут обращаться к сервисам приложения из изолированных AppDomain.
    /// Реализация интерфейса должна быть thread safe.
    /// </summary>
    public sealed class DynamicServiceAdapterFactory : IServiceAdapterFactory
    {
      private ApplicationServiceContainer applicationServices;
      private ProxyGenerator proxyGenerator;
      private DynamicServiceAdapterFactory.ReturnValueInterceptor returnValueInterceptor;
      private IInterceptor[] interceptorArray;

      public DynamicServiceAdapterFactory(ApplicationServiceContainer applicationServices)
      {
        this.applicationServices = applicationServices != null ? applicationServices : throw new ArgumentNullException(nameof (applicationServices));
        this.proxyGenerator = new ProxyGenerator();
        this.returnValueInterceptor = new DynamicServiceAdapterFactory.ReturnValueInterceptor(this);
        this.interceptorArray = new IInterceptor[1]
        {
          (IInterceptor) this.returnValueInterceptor
        };
      }

      /// <summary>
      /// Создает адаптер для указанного сервиса приложения, если это возможно.
      /// </summary>
      /// <param name="service">Тип сервиса</param>
      /// <param name="externalCache">Кэш, который можно использовать для хранения адаптеров</param>
      /// <returns>Адаптер сервиса или null</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="service" /> не должен быть равен null; параметр <paramref name="externalCache" /> не должен быть равен null</exception>
      public MarshalByRefObject TryCreateServiceAdapter(
        Type service,
        IDictionary<string, object> externalCache)
      {
        object service1 = this.applicationServices.GetService(service);
        if (service1 == null)
          throw new InvalidOperationException("No service found.");
        DynamicServiceAdapterFactory.ScriptData scriptData = this.GetOrCreateScriptData(externalCache);
        return (MarshalByRefObject) this.CreateAdapterInternal(service1, typeof (DynamicServiceAdapterFactory.DynamicScriptServiceObject), scriptData);
      }

      private DynamicServiceAdapterFactory.ScriptData GetOrCreateScriptData(
        IDictionary<string, object> externalCache)
      {
        object scriptData;
        if (!externalCache.TryGetValue("DynamicServiceAdapterFactory.ScriptData", out scriptData))
        {
          scriptData = (object) new DynamicServiceAdapterFactory.ScriptData();
          externalCache.Add("DynamicServiceAdapterFactory.ScriptData", scriptData);
        }
        return (DynamicServiceAdapterFactory.ScriptData) scriptData;
      }

      private DynamicServiceAdapterFactory.DynamicScriptServiceObject CreateAdapterInternal(
        object obj,
        Type classType,
        DynamicServiceAdapterFactory.ScriptData scriptData)
      {
        return scriptData.AdapterCache.GetOrAdd(obj, (Func<object, DynamicServiceAdapterFactory.DynamicScriptServiceObject>) (arg =>
        {
          List<Type> typeList = new List<Type>((IEnumerable<Type>) obj.GetType().GetInterfaces());
          typeList.RemoveAll((Predicate<Type>) (x => !x.IsPublic));
          return this.CreateAdapterInternal(obj, classType, typeList.ToArray(), scriptData);
        }));
      }

      private DynamicServiceAdapterFactory.DynamicScriptServiceObject CreateAdapterInternal(
        object obj,
        Type classType,
        Type[] interfaces,
        DynamicServiceAdapterFactory.ScriptData scriptData)
      {
        return scriptData.AdapterCache.GetOrAdd(obj, (Func<object, DynamicServiceAdapterFactory.DynamicScriptServiceObject>) (arg =>
        {
          ProxyGenerationOptions options = ProxyGenerationOptions.Default;
          options.BaseTypeForInterfaceProxy = classType;
          DynamicServiceAdapterFactory.DynamicScriptServiceObject interfaceProxyWithTarget = (DynamicServiceAdapterFactory.DynamicScriptServiceObject) this.proxyGenerator.CreateInterfaceProxyWithTarget(interfaces[0], interfaces, obj, options, this.interceptorArray);
          interfaceProxyWithTarget.ScriptData = scriptData;
          return interfaceProxyWithTarget;
        }));
      }

      internal sealed class ScriptData
      {
        public ScriptData()
        {
          this.AdapterCache = new ConcurrentDictionary<object, DynamicServiceAdapterFactory.DynamicScriptServiceObject>();
        }

        public ConcurrentDictionary<object, DynamicServiceAdapterFactory.DynamicScriptServiceObject> AdapterCache { get; private set; }
      }

      /// <summary>
      /// Базовый класс для генерируемых оберток. Не может быть private,
      /// так как в этом случае он не виден снаружи - от него нельзя унаследоваться.
      /// </summary>
      internal class DynamicScriptServiceObject : MarshalByRefObject
      {
        public DynamicServiceAdapterFactory.ScriptData ScriptData { get; internal set; }
      }

      internal sealed class ReturnValueInterceptor : IInterceptor
      {
        private DynamicServiceAdapterFactory factory;

        public ReturnValueInterceptor(DynamicServiceAdapterFactory factory) => this.factory = factory;

        public void Intercept(IInvocation invocation)
        {
          invocation.Proceed();
          if (invocation.ReturnValue == null || RemotingServices.IsTransparentProxy(invocation.ReturnValue) || invocation.ReturnValue.GetType().IsSerializable)
            return;
          if (invocation.Method.ReturnType.IsInterface)
          {
            Type[] interfaces = new Type[1]
            {
              invocation.Method.ReturnType
            };
            invocation.ReturnValue = (object) this.factory.CreateAdapterInternal(invocation.ReturnValue, typeof (DynamicServiceAdapterFactory.DynamicScriptServiceObject), interfaces, this.GetScriptData(invocation));
          }
          else
            invocation.ReturnValue = (object) this.factory.CreateAdapterInternal(invocation.ReturnValue, typeof (DynamicServiceAdapterFactory.DynamicScriptServiceObject), this.GetScriptData(invocation));
        }

        private DynamicServiceAdapterFactory.ScriptData GetScriptData(IInvocation invocation)
        {
          return ((DynamicServiceAdapterFactory.DynamicScriptServiceObject) invocation.Proxy).ScriptData;
        }
      }
    }
}
