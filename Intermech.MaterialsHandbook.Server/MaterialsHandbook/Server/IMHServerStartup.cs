// Decompiled with JetBrains decompiler
// Type: Intermech.MaterialsHandbook.Server.IMHServerStartup
// Assembly: Intermech.MaterialsHandbook.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 415584AC-BDF0-4945-B0B3-EBEC9DE4A5E1
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MaterialsHandbook.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.MaterialsHandbook;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.MaterialsHandbook.Server;

public class IMHServerStartup : IPackage, IConfigurable
{
  public void Load(IServiceProvider serviceProvider)
  {
    IUserSession session = (IUserSession) null;
    IDBTimedEvents service1 = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    ICustomServices service2 = (ICustomServices) serviceProvider.GetService(typeof (ICustomServices));
    try
    {
      if (service1 == null || service2 == null)
        return;
      session = service1.GetSystemSessionTemporaryClone("IMH.startup");
      if (session == null)
        return;
      service2.AddService(typeof (IIMHSystemSettingsService), (object) new IMHSystemSettingsService(session));
      service2.AddService(typeof (IIMHUserSettingsService), (object) new IMHUserSettingsService(session));
      service2.AddService(typeof (IIMHIndexingService), (object) new IMHIndexingService());
    }
    finally
    {
      session?.Logout("IMH.startup");
    }
  }

  public void Unload()
  {
  }

  public string Name => LocalizationHolder.rm.GetString("MaterialsHandbook.Server_1");

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
  }
}
