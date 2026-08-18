
// Type: Intermech.Remoting.Sponsors.RemotingClientSponsorService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Remoting.Sponsors
{
    public sealed class RemotingClientSponsorService
    {
      private object syncRoot;
      private IRemotingClientSponsorLogger logger;
      private IRemotingClientSponsorFactory factory;
      private static readonly RemotingClientSponsorService defaultInstance = new RemotingClientSponsorService();

      public RemotingClientSponsorService()
      {
        this.syncRoot = new object();
        this.logger = (IRemotingClientSponsorLogger) new NullLogger();
        this.factory = (IRemotingClientSponsorFactory) new DefaultRemotingClientSponsorFactory();
      }

      public IRemotingClientSponsorLogger Logger
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
            return this.logger;
        }
        [DebuggerStepThrough] set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (value));
          lock (this.syncRoot)
            this.logger = value;
        }
      }

      public IRemotingClientSponsorFactory Factory
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
            return this.factory;
        }
        [DebuggerStepThrough] set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (value));
          lock (this.syncRoot)
            this.factory = value;
        }
      }

      public static RemotingClientSponsorService Default
      {
        get => RemotingClientSponsorService.defaultInstance;
      }
    }
}
