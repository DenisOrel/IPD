
// Type: Intermech.Remoting.Sponsors.IRemotingClientSponsorLogger
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Remoting.Sponsors
{
    /// <summary>
    /// Интерфейс объекта для журналирования обращений к спонсорам remoting-объектов.
    /// Реализация объекта должна быть thread safe.
    /// </summary>
    public interface IRemotingClientSponsorLogger
    {
      void RegisterSponsor(MarshalByRefObject serverObject, string sponsorName);

      void UnregisterSponsor(MarshalByRefObject serverObject, string sponsorName);

      void Renewal(ICollection<MarshalByRefObject> serverObjects);

      void SponsorMessage(string message);
    }
}
