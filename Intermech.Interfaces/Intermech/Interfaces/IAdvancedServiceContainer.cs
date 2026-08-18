
// Type: Intermech.Interfaces.IAdvancedServiceContainer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel.Design;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Контейнер сервисов, позволяющий получать отсутствующие в своём составе сервисы у дополнительного контейнера сервисов
    /// </summary>
    public interface IAdvancedServiceContainer : IServiceContainer, IServiceProvider
    {
      /// <summary>Дополнительный контейнер сервисов</summary>
      IServiceProvider AdvancedProvider { get; set; }
    }
}
