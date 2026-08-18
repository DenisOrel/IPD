// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackingServerPlugin
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using System;

#nullable disable
namespace Intermech.CompositionTracking.Server;

public class CompositionTrackingServerPlugin : IPackage, IConfigurable
{
  private IPluginManager _manager;
  private static int _refCount;
  private static CompositionTrackingSubscriber _trackingSubscriber;

  public static string GetPluginName()
  {
    return LocalizationHolder.rm.GetString("CompositionTracking.Server_1");
  }

  public static void LoadPlugin(IServiceProvider serviceProvider)
  {
    if (CompositionTrackingServerPlugin._refCount == 0)
    {
      CompositionTrackingServerHolder.serviceProvider = serviceProvider;
      CompositionTrackingServerHolder.dbTimedEvents = serviceProvider.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
      CompositionTrackingServerHolder.eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
      IUserSession sessionTemporaryClone = CompositionTrackingServerHolder.DbTimedEvents.GetSystemSessionTemporaryClone("CompositionTracking.LoadPlugin");
      try
      {
        CompositionTrackingServerHolder.trackingService = new CompositionTrackingService();
        CompositionTrackingServerHolder.trackingService.Settings.LoadConfigData(sessionTemporaryClone.SessionGUID);
        CompositionTrackingServerHolder.TrackingService.RegisterService(serviceProvider);
        CompositionTrackingServerPlugin._trackingSubscriber = new CompositionTrackingSubscriber();
        CompositionTrackingServerPlugin._trackingSubscriber.Activate();
      }
      finally
      {
        sessionTemporaryClone?.Logout("CompositionTracking.LoadPlugin");
      }
    }
    ++CompositionTrackingServerPlugin._refCount;
  }

  public static void UnloadPlugin()
  {
    --CompositionTrackingServerPlugin._refCount;
    if (CompositionTrackingServerPlugin._refCount != 0)
      return;
    if (CompositionTrackingServerPlugin._trackingSubscriber != null)
    {
      CompositionTrackingServerPlugin._trackingSubscriber.Deactivate();
      CompositionTrackingServerPlugin._trackingSubscriber = (CompositionTrackingSubscriber) null;
    }
    if (CompositionTrackingServerHolder.TrackingService == null)
      return;
    CompositionTrackingServerHolder.TrackingService.UnRegisterService(CompositionTrackingServerHolder.ServiceProvider);
    CompositionTrackingServerHolder.trackingService = (CompositionTrackingService) null;
  }

  public string Name => CompositionTrackingServerPlugin.GetPluginName();

  public void Load(IServiceProvider serviceProvider)
  {
    CompositionTrackingServerHolder.serviceProvider = serviceProvider;
    CompositionTrackingServerHolder.dbTimedEvents = serviceProvider.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    CompositionTrackingServerHolder.eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this._manager = serviceProvider.GetService(typeof (IPluginManager)) as IPluginManager;
    if (this._manager == null)
      return;
    this._manager.LoadComplete += new EventHandler(this.manager_LoadComplete);
  }

  public void Unload()
  {
    if (this._manager != null)
      this._manager.LoadComplete -= new EventHandler(this.manager_LoadComplete);
    this._manager_Unload();
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
  }

  private void manager_LoadComplete(object sender, EventArgs e)
  {
    CompositionTrackingServerPlugin.LoadPlugin(CompositionTrackingServerHolder.ServiceProvider);
  }

  private void _manager_Unload() => CompositionTrackingServerPlugin.UnloadPlugin();
}
