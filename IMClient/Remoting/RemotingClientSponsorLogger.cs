
// Type: IMClient.Remoting.RemotingClientSponsorLogger




using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;


namespace IMClient.Remoting
{
    internal sealed class RemotingClientSponsorLogger : LongLifeObject, IRemotingClientSponsorLogger
    {
      private IServerEventLogService serverEventLog;
      private const string logFileName = "remoting_client_sponsors.log";

      public RemotingClientSponsorLogger(IServerEventLogService serverEventLog)
      {
        this.serverEventLog = serverEventLog != null ? serverEventLog : throw new ArgumentNullException(nameof (serverEventLog));
      }

      public void RegisterSponsor(MarshalByRefObject serverObject, string sponsorName)
      {
        this.serverEventLog.AddToTrace($"Register {sponsorName} for a single server-side object (uri = {RemotingServices.GetObjectUri(serverObject)})", "remoting_client_sponsors.log");
      }

      public void UnregisterSponsor(MarshalByRefObject serverObject, string sponsorName)
      {
        this.serverEventLog.AddToTrace($"Unregister {sponsorName} for a single server-side object (uri = {RemotingServices.GetObjectUri(serverObject)})", "remoting_client_sponsors.log");
      }

      public void Renewal(ICollection<MarshalByRefObject> serverObjects)
      {
        this.serverEventLog.AddToTrace(serverObjects.Count == 1 ? $"Renew the lifetime for a single server-side object (uri = {RemotingServices.GetObjectUri(CollectionUtils.GetFirstItem<MarshalByRefObject>((IEnumerable<MarshalByRefObject>) serverObjects))})" : $"Renew lifetimes for multiple server-side objects (total count = {serverObjects.Count})", "remoting_client_sponsors.log");
      }

      public void SponsorMessage(string message)
      {
        if (string.IsNullOrEmpty(message))
          return;
        this.serverEventLog.AddToTrace(message, "remoting_client_sponsors.log");
      }
    }
}
