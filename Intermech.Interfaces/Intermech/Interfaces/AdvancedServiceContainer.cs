
// Type: Intermech.Interfaces.AdvancedServiceContainer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel.Design;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Контейнер сервисов, позволяющий получать отсутствующие в своём составе сервисы у родительского контейнера сервисов
    /// </summary>
    public class AdvancedServiceContainer : 
      ServiceContainer,
      IAdvancedServiceContainer,
      IServiceContainer,
      IServiceProvider
    {
      /// <summary>Дополнительный контейнер сервисов</summary>
      internal IServiceProvider _advancedProvider;

      /// <summary>Конструктор</summary>
      public AdvancedServiceContainer()
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="parentProvider">Родительский контейнер сервисов</param>
      public AdvancedServiceContainer(IServiceProvider parentProvider)
        : base(parentProvider)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="parentProvider">Родительский контейнер сервисов</param>
      /// <param name="advancedContainer">Дополнительный контейнер сервисов</param>
      public AdvancedServiceContainer(
        IServiceProvider parentProvider,
        IServiceProvider advancedContainer)
        : base(parentProvider)
      {
        this.AdvancedProvider = advancedContainer;
      }

      /// <summary>Дополнительный контейнер сервисов</summary>
      public IServiceProvider AdvancedProvider
      {
        get => this._advancedProvider;
        set
        {
          this._advancedProvider = !this.Equals((object) value) ? value : throw new Exception(LocalizationHolder.rm.GetString("Interfaces_797"));
        }
      }

      /// <summary>Получить ссылку на сервис указанного типа</summary>
      /// <param name="serviceType">Тип запрашиваемого сервиса</param>
      /// <returns>Сервис запрошенного типа или null, если сервис не найден</returns>
      public override object GetService(Type serviceType)
      {
        object service = base.GetService(serviceType);
        return service != null || this._advancedProvider == null ? service : this._advancedProvider.GetService(serviceType);
      }
    }
}
