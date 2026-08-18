
// Type: Intermech.Scripting.Common.Debugging.ClientTokenProvider
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Scripting.Common.Debugging
{
    public sealed class ClientTokenProvider
    {
      private Lazy<int> clientTokenCache;
      private static readonly ClientTokenProvider defaultInstance = new ClientTokenProvider();

      private ClientTokenProvider()
      {
        this.clientTokenCache = new Lazy<int>(new Func<int>(this.CreateClientToken));
      }

      private int CreateClientToken() => Process.GetCurrentProcess().Id;

      public int GetClientToken() => this.clientTokenCache.Value;

      public static ClientTokenProvider Default
      {
        [DebuggerStepThrough] get => ClientTokenProvider.defaultInstance;
      }
    }
}
