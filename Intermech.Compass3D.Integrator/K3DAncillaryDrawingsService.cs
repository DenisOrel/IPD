// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DAncillaryDrawingsService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Interfaces.Plugins;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DAncillaryDrawingsService : IntegratorService
{
  private IPluginManager pluginManager;
  private Lazy<bool> isProcessingEnabled;

  public K3DAncillaryDrawingsService(IIntegrator owner)
    : base(owner)
  {
    this.isProcessingEnabled = new Lazy<bool>(new Func<bool>(this.TestProcessingEnabled), LazyThreadSafetyMode.PublicationOnly);
  }

  public IPluginManager PluginManager
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.pluginManager;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.pluginManager = value;
      }
    }
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.PluginManager == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "PluginManager");
  }

  public bool IsProcessingEnabled
  {
    [DebuggerStepThrough] get => this.isProcessingEnabled.Value;
  }

  private bool TestProcessingEnabled()
  {
    List<IPlugin> pluginList = new List<IPlugin>(this.PluginManager.Plugins.Count);
    foreach (IPlugin plugin in (IEnumerable<IPlugin>) this.PluginManager.Plugins)
      pluginList.Add(plugin);
    return pluginList.Find((Predicate<IPlugin>) (plugin => plugin.Name == "Intermech.Tools.CADExtensions")) != null;
  }
}
