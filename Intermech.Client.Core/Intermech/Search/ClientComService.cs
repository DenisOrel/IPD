
// Type: Intermech.Search.ClientComService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Runtime.ComInterop.LocalServer;
using System.Runtime.InteropServices;


namespace Intermech.Search;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
[Guid("2C3EF02E-E844-4629-A718-97E86226F6A4")]
[ProgId("Intermech.Search.ClientComService")]
public sealed class ClientComService : SingleThreadedObject
{
  public bool IsStartupCompleted
  {
    get
    {
      try
      {
        IStartupService startupService = ServiceLocator.Get<IStartupService>();
        IPluginManager pluginManager = ServiceLocator.Get<IPluginManager>();
        IMainFormUpdate mainFormUpdate = ServiceLocator.Get<IMainFormUpdate>();
        return startupService.IsStartupCompleted && pluginManager.IsLoadComplete && mainFormUpdate.MainForm.IsHandleCreated && !ServiceLocator.IsRegistered<ISplashService>();
      }
      catch
      {
        return false;
      }
    }
  }
}
