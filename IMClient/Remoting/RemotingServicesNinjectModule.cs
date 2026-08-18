
// Type: IMClient.Remoting.RemotingServicesNinjectModule




using Intermech.Remoting.Sponsors;
using Ninject.Modules;


namespace IMClient.Remoting
{
    internal sealed class RemotingServicesNinjectModule : NinjectModule
    {
      public override void Load()
      {
        this.Bind<IRemotingClientSponsorLogger>().To<RemotingClientSponsorLogger>();
        this.Bind<RemotingClientSponsorValidator>().ToSelf();
        this.Bind<RemotingServicesModule>().ToSelf();
      }
    }
}
