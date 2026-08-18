
// Type: IMClient.AutoUpdater.Module




using Intermech.ApplicationModel;
using Intermech.AutoUpdater;
using Intermech.Diagnostics;
using Intermech.Interfaces.Client;
using IPSAutoUpdater.Interfaces;
using System;


namespace IMClient.AutoUpdater
{
    internal sealed class Module : InitializerModule
    {
      private IApplicationEventLogService eventLogService;
      private IStartupService startupService;
      private Lazy<IMainFormUpdate> mainFormService;
      private AutoUpdaterClientSettings clientSettings;
      private AutoUpdaterClient client;
      private bool isClientRegistered;
      private AutoUpdaterServerConnection serverConnection;
      private AutoUpdaterServerGuardian serverGuardian;

      public Module(
        IApplicationEventLogService eventLogService,
        IStartupService startupService,
        Lazy<IMainFormUpdate> mainFormService)
      {
        if (eventLogService == null)
          throw new ArgumentNullException(nameof (eventLogService));
        if (startupService == null)
          throw new ArgumentNullException(nameof (startupService));
        if (mainFormService == null)
          throw new ArgumentNullException(nameof (mainFormService));
        this.eventLogService = eventLogService;
        this.startupService = startupService;
        this.mainFormService = mainFormService;
        this.clientSettings = new AutoUpdaterClientSettings();
      }

      protected override void DoInitialize()
      {
        base.DoInitialize();
        if (!this.clientSettings.AllowAutoUpdate)
          return;
        this.startupService.StartupComplete += new EventHandler(this.ConnectToAutoUpdateServer);
      }

      private void ConnectToAutoUpdateServer(object sender, EventArgs e)
      {
        this.client = new AutoUpdaterClient(this.eventLogService, this.mainFormService);
        this.serverConnection = new AutoUpdaterServerConnection(AutoUpdaterConsts.RemotingServerAddress);
        this.serverConnection.Connected += new EventHandler(this.OnAutoUpdateServerConnected);
        this.serverConnection.ConnectionLost += new EventHandler(this.OnAutoUpdateServerDisconnected);
        this.serverGuardian = new AutoUpdaterServerGuardian(this.serverConnection, this.clientSettings.ServerCheckPeriod);
        this.serverGuardian.Enabled = true;
      }

      private void OnAutoUpdateServerConnected(object sender, EventArgs e)
      {
        this.eventLogService.DefaultLog.Write("Установлено подключение к службе обновления ПО ИНТЕРМЕХ.");
        this.RegisterClientSilently();
      }

      private void OnAutoUpdateServerDisconnected(object sender, EventArgs e)
      {
        this.eventLogService.DefaultLog.Write("Потеряно подключение к службе обновления ПО ИНТЕРМЕХ.", EventLogItemType.Warning);
        this.isClientRegistered = false;
      }

      private void RegisterClientSilently()
      {
        try
        {
          this.serverConnection.ServerObject.Register(this.client);
          this.isClientRegistered = true;
          this.eventLogService.DefaultLog.Write("Выполнена регистрация приложения в службе обновления ПО ИНТЕРМЕХ.");
        }
        catch (Exception ex)
        {
          this.eventLogService.DefaultLog.Write(ExceptionServices.GetExtendedExceptionText(ex, "При регистрации приложения в службе обновления ПО ИНТЕРМЕХ произошла ошибка."), EventLogItemType.Error);
        }
      }

      private void UnregisterClientSilently()
      {
        try
        {
          this.serverConnection.ServerObject.Unregister(this.client);
          this.eventLogService.DefaultLog.Write("Отменена регистрация приложения в службе обновления ПО ИНТЕРМЕХ.");
        }
        catch (Exception ex)
        {
          this.eventLogService.DefaultLog.Write(ExceptionServices.GetExtendedExceptionText(ex, "При отмене регистрации приложения в службе обновления ПО ИНТЕРМЕХ произошла ошибка."), EventLogItemType.Error);
        }
        this.isClientRegistered = false;
      }

      protected override void DoShutdown()
      {
        if (this.serverGuardian != null)
        {
          this.serverGuardian.Enabled = false;
          this.serverGuardian = (AutoUpdaterServerGuardian) null;
        }
        if (this.serverConnection != null)
        {
          if (this.isClientRegistered && this.serverConnection.TestConnection())
            this.UnregisterClientSilently();
          this.serverConnection = (AutoUpdaterServerConnection) null;
          this.isClientRegistered = false;
        }
        this.startupService.StartupComplete -= new EventHandler(this.ConnectToAutoUpdateServer);
        this.client = (AutoUpdaterClient) null;
        base.DoShutdown();
      }
    }
}
