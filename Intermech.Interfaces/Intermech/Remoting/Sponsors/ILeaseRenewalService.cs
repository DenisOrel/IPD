
// Type: Intermech.Remoting.Sponsors.ILeaseRenewalService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Remoting.Lifetime;


namespace Intermech.Remoting.Sponsors
{
    /// <summary>
    /// Интерфейс сервиса, позволяющего увеличивать и уменьшать время жизни объектов remoting.
    /// Как правило, сервис предоставляется сервером remoting.
    /// Реализация должна быть thread safe.
    /// </summary>
    [ClientSideDisconnectionProtection(false)]
    public interface ILeaseRenewalService
    {
      /// <summary>
      /// Увеличивает или уменьшает время жизни объекта remoting.
      /// </summary>
      /// <param name="lease">Объект для управления временем жизни объекта</param>
      /// <param name="delta">Приращение для текущего значения время жизни для объекта. Значение параметра может быть отрицательным</param>
      /// <returns>Признак успешного/неуспешного изменения времени жизни объекта</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="lease" /> содержит null</exception>
      bool TryChangeLeaseTime(ILease lease, TimeSpan delta);
    }
}
