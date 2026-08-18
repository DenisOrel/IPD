
// Type: Intermech.Interfaces.ServiceUtils
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Interfaces
{
    public static class ServiceUtils
    {
      public static object GetService(object serviceProvider, Type serviceType, bool throwIfNotFound)
      {
        object service = ServiceUtils.InternalGetService(serviceProvider, serviceType);
        return !(service == null & throwIfNotFound) ? service : throw ServiceUtils.ServiceNotAvailable(serviceType);
      }

      public static T GetService<T>(object serviceProvider, bool throwIfNotFound) where T : class
      {
        return (T) ServiceUtils.GetService(serviceProvider, typeof (T), throwIfNotFound);
      }

      public static bool IsServiceAvailable(object serviceProvider, Type serviceType)
      {
        return ServiceUtils.InternalGetService(serviceProvider, serviceType) != null;
      }

      public static void CheckServiceAvailable(object serviceProvider, Type serviceType)
      {
        if (!ServiceUtils.IsServiceAvailable(serviceProvider, serviceType))
          throw ServiceUtils.ServiceNotAvailable(serviceType);
      }

      private static Exception ServiceNotAvailable(Type serviceType)
      {
        return new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_755"), (object) serviceType));
      }

      private static object InternalGetService(object serviceProvider, Type serviceType)
      {
        switch (serviceProvider)
        {
          case null:
            return (object) null;
          case IUserSession _:
            return ((IUserSession) serviceProvider).GetCustomService(serviceType);
          case IServiceProvider _:
            return ((IServiceProvider) serviceProvider).GetService(serviceType);
          default:
            throw new NotSupportedException($"The object of type '{serviceProvider.GetType()}' is not a service provider.");
        }
      }
    }
}
