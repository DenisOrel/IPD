
// Type: Intermech.Remoting.Sponsors.OneShotSponsorFactory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Remoting.Sponsors
{
    /// <summary>
    /// Класс фабрики для создания объектов-спонсоров типа <see cref="T:Intermech.Remoting.Sponsors.OneShotSponsor" />.
    /// </summary>
    /// <remarks>
    /// Реализация является thread safe и long life, так как спонсоры пробрасываются в
    /// изолированные AppDomain, в которых выполняются C#-сценарии.
    /// </remarks>
    public sealed class OneShotSponsorFactory : MarshalByRefObject, IRemotingClientSponsorFactory
    {
      private Func<ILeaseRenewalService> leaseRenewalServiceProvider;

      /// <summary>Создает объект.</summary>
      /// <param name="serviceProvider">Провайдер сервиса для управления временем жизни серверных объектов</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="serviceProvider" /> содержит null</exception>
      public OneShotSponsorFactory(Func<ILeaseRenewalService> serviceProvider)
      {
        this.leaseRenewalServiceProvider = serviceProvider != null ? serviceProvider : throw new ArgumentNullException(nameof (serviceProvider));
      }

      /// <summary>
      /// Инициализирует сервис управления временем жизни текущего объекта.
      /// </summary>
      /// <returns>null, так как это long life object</returns>
      public override object InitializeLifetimeService() => (object) null;

      /// <summary>Создает и возвращает объект-спонсор.</summary>
      /// <returns>Объект-спонсор</returns>
      public IRemotingClientSponsor Create()
      {
        return (IRemotingClientSponsor) new OneShotSponsor(this.leaseRenewalServiceProvider());
      }
    }
}
