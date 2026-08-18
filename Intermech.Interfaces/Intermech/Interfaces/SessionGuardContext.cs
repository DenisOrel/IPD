
// Type: Intermech.Interfaces.SessionGuardContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting;
using System;


namespace Intermech.Interfaces
{
    public static class SessionGuardContext
    {
      private const string ActiveClientSessionProp = "X-IPS-SessionGuard-ActiveClientSession";
      private const string IsSuspendedProp = "X-IPS-SessionGuard-IsSuspended";
      private const string IsSuspendedValue = "true";

      internal static void Suspend()
      {
        RemotingCallContext.SetData("X-IPS-SessionGuard-IsSuspended", "true");
      }

      internal static void Resume()
      {
        RemotingCallContext.FreeNamedDataSlot("X-IPS-SessionGuard-IsSuspended");
      }

      internal static bool IsSuspended()
      {
        string data = RemotingCallContext.GetData("X-IPS-SessionGuard-IsSuspended");
        return data != null && data == "true";
      }

      internal static void SetActiveClientSession(Guid sessionGuid)
      {
        RemotingCallContext.SetData("X-IPS-SessionGuard-ActiveClientSession", sessionGuid.ToString("N"));
      }

      internal static void ResetActiveClientSession()
      {
        RemotingCallContext.FreeNamedDataSlot("X-IPS-SessionGuard-ActiveClientSession");
      }

      internal static Guid GetActiveClientSession()
      {
        string data = RemotingCallContext.GetData("X-IPS-SessionGuard-ActiveClientSession");
        return data != null ? new Guid(data) : Guid.Empty;
      }
    }
}
