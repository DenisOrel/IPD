
// Type: IMClient.AutoUpdater.AutoUpdaterServerProxy




using IPSAutoUpdater.Interfaces;
using System;
using System.Diagnostics;


namespace IMClient.AutoUpdater
{
    internal sealed class AutoUpdaterServerProxy
    {
      private IAutoUpdaterServer rawServerObject;

      public AutoUpdaterServerProxy(IAutoUpdaterServer serverObject)
      {
        this.rawServerObject = serverObject != null ? serverObject : throw new ArgumentNullException(nameof (serverObject));
      }

      internal IAutoUpdaterServer RawServerObject
      {
        [DebuggerStepThrough] get => this.rawServerObject;
      }

      public void Register(AutoUpdaterClient client)
      {
        if (client == null)
          throw new ArgumentNullException(nameof (client));
        if (!this.rawServerObject.RegisterClient((IAutoUpdaterClient) client))
          throw new Exception("Служба обновления ПО ИНТЕРМЕХ отказала в регистрации по неизвестной причине.");
      }

      public void Unregister(AutoUpdaterClient client)
      {
        if (client == null)
          throw new ArgumentNullException(nameof (client));
        this.rawServerObject.UnregisterClient((IAutoUpdaterClient) client);
      }
    }
}
