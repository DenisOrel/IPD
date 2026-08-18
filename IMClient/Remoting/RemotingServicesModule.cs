
// Type: IMClient.Remoting.RemotingServicesModule




using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Remoting.Sponsors;
using System;
using System.Diagnostics;
using System.Threading;


namespace IMClient.Remoting
{
    internal sealed class RemotingServicesModule : InitializerModule
    {
      private IMServerService imServerService;
      private IStartupService startupService;
      private Func<RemotingClientSponsorValidator> validatorFactory;
      private Func<IRemotingClientSponsorLogger> loggerFactory;
      private string savedClientSponsorMode;
      private IRemotingClientSponsorLogger savedClientSponsorLogger;
      private IRemotingClientSponsorFactory savedClientSponsorFactory;
      private const string OneShotSponsors = "oneshot";
      private const string ClassicSponsors = "classic";

      public RemotingServicesModule(
        IMServerService imServerService,
        IStartupService startupService,
        Func<RemotingClientSponsorValidator> validatorFactory,
        Func<IRemotingClientSponsorLogger> loggerFactory)
      {
        if (imServerService == null)
          throw new ArgumentNullException(nameof (imServerService));
        if (startupService == null)
          throw new ArgumentNullException(nameof (startupService));
        if (validatorFactory == null)
          throw new ArgumentNullException(nameof (validatorFactory));
        if (loggerFactory == null)
          throw new ArgumentNullException(nameof (loggerFactory));
        this.imServerService = imServerService;
        this.startupService = startupService;
        this.validatorFactory = validatorFactory;
        this.loggerFactory = loggerFactory;
      }

      protected override void DoInitialize()
      {
        base.DoInitialize();
        this.InstallRemotingClientSponsorLogger();
        this.InstallRemotingClientSponsorFactory();
        this.startupService.StartupComplete += new EventHandler(this.OnApplicationStarted);
      }

      protected override void DoShutdown()
      {
        this.startupService.StartupComplete += new EventHandler(this.OnApplicationStarted);
        this.RemoveClientSponsorFactory();
        this.RemoveRemotingClientSponsorLogger();
        base.DoShutdown();
      }

      private void OnApplicationStarted(object sender, EventArgs e)
      {
        if (string.IsNullOrEmpty(this.savedClientSponsorMode) || !(this.savedClientSponsorMode == "classic"))
          return;
        ThreadPool.QueueUserWorkItem((WaitCallback) (arg => this.ValidateRemotingClientSponsors()));
      }

      private void ValidateRemotingClientSponsors()
      {
        this.validatorFactory().CheckClientBackwardConnectivity();
      }

      private void InstallRemotingClientSponsorLogger()
      {
        if (this.imServerService.GetAppConfigurationService().GetTraceSwitch("Remoting.ClientSponsors") == TraceLevel.Off)
          return;
        IRemotingClientSponsorLogger clientSponsorLogger = this.loggerFactory();
        this.savedClientSponsorLogger = RemotingClientSponsorService.Default.Logger;
        RemotingClientSponsorService.Default.Logger = clientSponsorLogger;
      }

      private void RemoveRemotingClientSponsorLogger()
      {
        if (this.savedClientSponsorLogger == null)
          return;
        RemotingClientSponsorService.Default.Logger = this.savedClientSponsorLogger;
        this.savedClientSponsorLogger = (IRemotingClientSponsorLogger) null;
      }

      private void InstallRemotingClientSponsorFactory()
      {
        this.savedClientSponsorMode = this.ReadRemotingClientSponsorMode();
        if (!(this.savedClientSponsorMode == "oneshot"))
          return;
        this.savedClientSponsorFactory = RemotingClientSponsorService.Default.Factory;
        RemotingClientSponsorService.Default.Factory = (IRemotingClientSponsorFactory) new OneShotSponsorFactory(new Func<ILeaseRenewalService>(this.imServerService.GetLeaseRenewalService));
      }

      private void RemoveClientSponsorFactory()
      {
        if (this.savedClientSponsorFactory == null)
          return;
        RemotingClientSponsorService.Default.Factory = this.savedClientSponsorFactory;
        this.savedClientSponsorFactory = (IRemotingClientSponsorFactory) null;
      }

      private string ReadRemotingClientSponsorMode()
      {
        string str = this.imServerService.GetAppConfigurationService().GetConfigurationOption("Remoting.ClientSponsorMode");
        if (string.IsNullOrEmpty(str))
          str = "oneshot";
        return str.ToLower();
      }
    }
}
