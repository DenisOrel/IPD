
// Type: IMClient.UserConfigurationFromFileModule




using Intermech.ApplicationModel;
using Intermech.Configuration;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Runtime;
using System;
using System.IO;


namespace IMClient
{
    internal sealed class UserConfigurationFromFileModule : InitializerModule
    {
      private ILocalConfigurationManager configurationManager;
      private IApplicationEventLogService eventLogService;
      private IExceptionHandlerService exceptionService;
      private INotificationService notificationService;
      private string configFilePathForLoad;
      private string configFilePathForSave;

      public UserConfigurationFromFileModule(
        ILocalConfigurationManager configurationManager,
        IApplicationEventLogService eventLogService,
        IExceptionHandlerService exceptionService,
        INotificationService notificationService)
      {
        if (configurationManager == null)
          throw new ArgumentNullException(nameof (configurationManager));
        if (eventLogService == null)
          throw new ArgumentNullException(nameof (eventLogService));
        if (exceptionService == null)
          throw new ArgumentNullException(nameof (exceptionService));
        if (notificationService == null)
          throw new ArgumentNullException(nameof (notificationService));
        this.configurationManager = configurationManager;
        this.eventLogService = eventLogService;
        this.exceptionService = exceptionService;
        this.notificationService = notificationService;
      }

      protected override void DoInitialize()
      {
        base.DoInitialize();
        this.InitializeConfigFilePaths();
        this.LoadUserConfiguration();
        this.notificationService.Subscribe("ApplicationClosed", new NotificationEventHandler(this.OnApplicationClosed));
      }

      protected override void DoShutdown()
      {
        this.notificationService.Unsubscribe("ApplicationClosed", new NotificationEventHandler(this.OnApplicationClosed));
        base.DoShutdown();
      }

      private void OnApplicationClosed(object sender, NotificationEventArgs e)
      {
        SilentActionInvoker.Default.Invoke(new Action(this.SaveUserConfiguration));
      }

      private void InitializeConfigFilePaths()
      {
        this.configFilePathForLoad = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile), ProgramConsts.UserConfigurationFileName);
        this.configFilePathForSave = this.configFilePathForLoad;
        string str = AppSettingsHelper.GetString("LogPath", string.Empty);
        if (!string.IsNullOrEmpty(str))
          str = Environment.ExpandEnvironmentVariables(str);
        if (string.IsNullOrEmpty(str) || !Directory.Exists(str))
          return;
        string path = Path.Combine(str, ProgramConsts.UserConfigurationFileName);
        this.configFilePathForSave = path;
        if (!File.Exists(path))
          return;
        this.configFilePathForLoad = path;
      }

      private void LoadUserConfiguration()
      {
        try
        {
          this.LoadUserConfigurationInternal();
        }
        catch (Exception ex)
        {
          this.exceptionService.ShowException(ex);
        }
      }

      private void LoadUserConfigurationInternal()
      {
        if (!File.Exists(this.configFilePathForLoad))
          return;
        this.eventLogService.DefaultLog.Write($"Чтение конфигурации пользователя с локального диска (файл '{this.configFilePathForLoad}')");
        using (Stream stream = (Stream) File.OpenRead(this.configFilePathForLoad))
          ((IPersistableConfigurationManager) this.configurationManager).Load(stream);
      }

      private void SaveUserConfiguration()
      {
        using (Stream stream = (Stream) File.Create(this.configFilePathForSave))
          ((IPersistableConfigurationManager) this.configurationManager).Save(stream);
      }
    }
}
