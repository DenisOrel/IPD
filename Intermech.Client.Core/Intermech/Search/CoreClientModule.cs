
// Type: Intermech.Search.CoreClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Search.ButtonBars;
using Intermech.Search.ButtonBars.MainMenuCommands;
using Intermech.Search.Configuration;
using Intermech.Search.UI;
using Intermech.Search.UI.Commands;
using Intermech.Search.Versions;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;


namespace Intermech.Search;

public sealed class CoreClientModule
{
  private ClientComService _clientComServer = new ClientComService();
  private RegistrationServices _registrationServices = new RegistrationServices();
  private int _clientComServiceCookie;

  public void Initialize()
  {
    ServiceLocator.Register<INavigatorClientService>((INavigatorClientService) new NavigatorClientService(ServiceLocator.Get<ICurrentUserAndRole>(), ServiceLocator.Get<INavGraphicsCache>(), ServiceLocator.Get<ICategoryTypeIconService>(), ServiceLocator.Get<IColumnSchemes>()));
    if (ServiceLocator.IsRegistered<IConfigurationOptionRepository>())
      ServiceLocator.Unregister<IConfigurationOptionRepository>();
    ServiceLocator.Register<IConfigurationOptionRepository>((IConfigurationOptionRepository) new CachedConfigurationOptionRepository((IConfigurationOptionRepository) new ConfigurationOptionRepository()));
    ServiceLocator.Get<IConfigurationOptionInfoProvider>().RegisterEditor(ConfigurationOptionKeys.Versions_DefaultVersionRule, typeof (VersionRuleEditor));
    ServiceLocator.Register<IRoleConfigurationManager>((IRoleConfigurationManager) new RoleConfigurationManager());
    ConfigurationPageHelper.CreateAndRegisterPages();
    ServiceLocator.Register<ICommandStatisticsRepository>((ICommandStatisticsRepository) new CommandStatisticsRepository());
    ServiceLocator.Get<INotificationService>().Subscribe("ApplicationClosing", new NotificationEventHandler(this.Application_Closing));
    ServiceLocator.Get<ICommandStatisticsRepository>().Load();
    INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
    namedImageList.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Intermech.Client.Core.Intermech.Search.Icons.Plus_32x32.ico")), "Intermech.Search.Icons.Plus_32x32.ico");
    namedImageList.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Intermech.Client.Core.Intermech.Search.Icons.Minus_32x32.ico")), "Intermech.Search.Icons.Minus_32x32.ico");
    ServiceLocator.Register<IButtonBarClientService>((IButtonBarClientService) new ButtonBarClientService());
    ServiceLocator.Register<ICommandClientService>((ICommandClientService) new CommandClientService());
    new MainMenuCommandsClientModule().Load();
    this.ResetShowInvalidConcreteVersions();
    ServiceLocator.Get<IPluginManager>().LoadComplete += (EventHandler) ((sender, e) =>
    {
      try
      {
        this._clientComServiceCookie = this._registrationServices.RegisterTypeForComClients(typeof (ClientComService), RegistrationClassContext.LocalServer, RegistrationConnectionType.MultipleUse);
      }
      catch
      {
      }
    });
  }

  private void Application_Closing(object sender, NotificationEventArgs e)
  {
    ServiceLocator.Get<ICommandStatisticsRepository>().Save();
    this.ResetShowInvalidConcreteVersions();
    try
    {
      this._registrationServices.UnregisterTypeForComClients(this._clientComServiceCookie);
    }
    catch
    {
    }
  }

  private void ResetShowInvalidConcreteVersions()
  {
    ServiceLocator.Get<IConfigurationOptionRepository>().AddOrUpdate(ConfigurationOptionKeys.Versions_ShowInvalidConcreteVersions, (object) false);
  }
}
