
// Type: Intermech.Remoting.Sponsors.NullLogger
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Remoting.Sponsors
{
    internal sealed class NullLogger : MarshalByRefObject, IRemotingClientSponsorLogger
    {
      public override object InitializeLifetimeService() => (object) null;

      public void RegisterSponsor(MarshalByRefObject serverObject, string sponsorName)
      {
      }

      public void UnregisterSponsor(MarshalByRefObject serverObject, string sponsorName)
      {
      }

      public void Renewal(ICollection<MarshalByRefObject> serverObjects)
      {
      }

      public void SponsorMessage(string message)
      {
      }
    }
}
