
// Type: IMClient.UserSessions.IMClientSessionPool




using IMClient.Tests;
using Intermech.ApplicationModel;
using Intermech.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Protection;
using System;
using System.Diagnostics;


namespace IMClient.UserSessions
{
    internal sealed class IMClientSessionPool : ClientSessionPoolBase
    {
      private IApplicationEventLogService eventLogService;
      private ILocalConfigurationManager localConfigurationManager;
      private IOptionalService<IOutputView> outputView;
      private SessionLoginWithPasswordInfo loginWithPasswordInfo;
      private SessionPoolThreadKey uiThreadKey;

      public IMClientSessionPool(
        IApplicationEventLogService eventLogService,
        ILocalConfigurationManager localConfigurationManager,
        IOptionalService<IOutputView> outputView,
        IMServerService imserverService,
        IClientCache clientCacheService)
        : base(imserverService, clientCacheService)
      {
        if (eventLogService == null)
          throw new ArgumentNullException(nameof (eventLogService));
        if (localConfigurationManager == null)
          throw new ArgumentNullException(nameof (localConfigurationManager));
        if (outputView == null)
          throw new ArgumentNullException(nameof (outputView));
        this.eventLogService = eventLogService;
        this.localConfigurationManager = localConfigurationManager;
        this.outputView = outputView;
        this.loginWithPasswordInfo = new SessionLoginWithPasswordInfo();
        this.loginWithPasswordInfo.UserName = "SYSDBA";
        this.uiThreadKey = this.CreateCurrentThreadKey();
      }

      protected override Tuple<IUserSession, UserSessionLoginInfo> CreateAndLoginMainSession()
      {
        IUserSession session = this.IMServerService.ServerObject.CreateSession();
        string s = System.Configuration.ConfigurationManager.AppSettings.Get("MaxAccessLevel");
        int result;
        if (s != null && s != string.Empty && int.TryParse(s, out result))
          session.SetClientAccessLevel(result, Environment.MachineName);
        this.RaiseInitializeLoginInfo(session);
        this.LoginMainSession(session);
        return Tuple.Create<IUserSession, UserSessionLoginInfo>(session, this.CopyLoginInfo((UserSessionLoginInfo) this.loginWithPasswordInfo));
      }

      private void LoginMainSession(IUserSession newMainSession)
      {
        if (newMainSession.UserID > 0L)
          return;
        if (new LoginHelper(this.eventLogService, (IConfigurationManager) this.localConfigurationManager).GetPassword(this.loginWithPasswordInfo, new string[1]
        {
          this.IMServerService.ServerUrl
        }, this.IMServerService.ServerObject, newMainSession))
          return;
        ProtectionService.Stop();
        Process.GetCurrentProcess().Kill();
      }

      private void RaiseInitializeLoginInfo(IUserSession newMainSession)
      {
        if (!AutoLoginHelper.IsTestMode)
          return;
        EventHandler<InitializeLoginInfoEventArgs> initializeLoginInfo = AutoLoginHelper.InitializeLoginInfo;
        if (initializeLoginInfo == null)
          return;
        UserSessionLoginInfo sessionLoginInfo = this.CopyLoginInfo((UserSessionLoginInfo) this.loginWithPasswordInfo);
        InitializeLoginInfoEventArgs e = new InitializeLoginInfoEventArgs(this.IMServerService.ServerObject, newMainSession, sessionLoginInfo);
        initializeLoginInfo((object) this, e);
        this.loginWithPasswordInfo.Assign(sessionLoginInfo);
        this.loginWithPasswordInfo.SetPassword(e.Password);
        this.loginWithPasswordInfo.IsValid = true;
      }

      protected override bool IsSessionPinningRequired(SessionPoolThreadKey threadKey)
      {
        return threadKey.Equals(this.uiThreadKey);
      }

      public void UpdateCachedLoginPassword(string newPassword)
      {
        lock (this.SyncRoot)
          this.loginWithPasswordInfo.SetPassword(newPassword);
      }
    }
}
