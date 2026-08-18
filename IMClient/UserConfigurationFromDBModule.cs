
// Type: IMClient.UserConfigurationFromDBModule




using Intermech;
using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Runtime;
using System;
using System.IO;


namespace IMClient
{
    internal sealed class UserConfigurationFromDBModule : InitializerModule
    {
      private IConfigurationManager configurationManager;
      private IApplicationEventLogService eventLogService;
      private IExceptionHandlerService exceptionService;
      private IPackedStream packService;
      private INotificationService notificationService;
      private IMServerService imserverService;
      private bool canSaveConfiguration;

      public UserConfigurationFromDBModule(
        IConfigurationManager configurationManager,
        IApplicationEventLogService eventLogService,
        IExceptionHandlerService exceptionService,
        IPackedStream packService,
        INotificationService notificationService,
        IMServerService imserverService)
      {
        if (configurationManager == null)
          throw new ArgumentNullException(nameof (configurationManager));
        if (eventLogService == null)
          throw new ArgumentNullException(nameof (eventLogService));
        if (exceptionService == null)
          throw new ArgumentNullException(nameof (exceptionService));
        if (packService == null)
          throw new ArgumentNullException(nameof (packService));
        if (notificationService == null)
          throw new ArgumentNullException(nameof (notificationService));
        if (imserverService == null)
          throw new ArgumentNullException(nameof (imserverService));
        this.configurationManager = configurationManager;
        this.eventLogService = eventLogService;
        this.exceptionService = exceptionService;
        this.packService = packService;
        this.notificationService = notificationService;
        this.imserverService = imserverService;
        this.canSaveConfiguration = true;
      }

      protected override void DoInitialize()
      {
        base.DoInitialize();
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
        if (!this.canSaveConfiguration)
          return;
        SilentActionInvoker.Default.Invoke(new Action(this.SaveUserConfiguration));
      }

      private void LoadUserConfiguration()
      {
        if (this.imserverService.ConnectionErrorStrategy is IMServerInteractiveConnectionErrorStrategy connectionErrorStrategy)
          connectionErrorStrategy.CanAbortConnection = false;
        try
        {
          this.LoadUserConfigurationInternal();
        }
        catch (Exception ex)
        {
          if (ex is DomainOnlyLoginException)
            throw;
          this.exceptionService.ShowException(ex);
        }
        finally
        {
          if (connectionErrorStrategy != null)
            connectionErrorStrategy.CanAbortConnection = true;
        }
      }

      private void LoadUserConfigurationInternal()
      {
        BlobInformation config_info;
        byte[] config_file;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          sessionKeeper.Session.Configurations.LoadConfigData(ProgramConsts.UserConfigurationFileName, out config_info, out config_file);
          this.canSaveConfiguration = !sessionKeeper.Session.EtalonBase;
        }
        if (config_file == null || config_file.Length == 0)
          return;
        this.eventLogService.DefaultLog.Write($"Чтение конфигурации пользователя из базы данных IPS (файл '{ProgramConsts.UserConfigurationFileName}')");
        using (MemoryStream configurationStream = this.GetConfigurationStream(config_info, config_file))
          ((IPersistableConfigurationManager) this.configurationManager).Load((Stream) configurationStream);
      }

      private MemoryStream GetConfigurationStream(BlobInformation configInfo, byte[] configData)
      {
        if (configInfo.ArcMethod == ArcMethods.NotPacked)
          return new MemoryStream(configData);
        MemoryStream outStream = new MemoryStream((int) configInfo.RealFileSize);
        using (MemoryStream inStream = new MemoryStream(configData))
        {
          try
          {
            this.packService.UnpackStream((Stream) outStream, (Stream) inStream);
            outStream.Position = 0L;
          }
          catch
          {
            outStream.SetLength(0L);
          }
        }
        return outStream;
      }

      private void SaveUserConfiguration()
      {
        BlobInformation config_info;
        byte[] array;
        using (MemoryStream inStream = new MemoryStream())
        {
          ((IPersistableConfigurationManager) this.configurationManager).Save((Stream) inStream);
          inStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream((int) (inStream.Length / 2L)))
          {
            this.packService.PackStream((Stream) outStream, (Stream) inStream, 9);
            config_info = new BlobInformation(inStream.Length, outStream.Length, DateTime.Now, ProgramConsts.UserConfigurationFileName, ArcMethods.ZLibPacked, string.Empty);
            array = outStream.ToArray();
          }
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          sessionKeeper.Session.Configurations.WriteConfigData(config_info, array);
      }
    }
}
