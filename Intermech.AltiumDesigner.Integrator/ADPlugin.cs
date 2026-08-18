// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADPlugin
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Plugins;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.Tools.Integrators.Electrical;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADPlugin : ClientModularPackage
{
  public ADPlugin()
    : base(ADConsts.IntegratorName)
  {
  }

  protected override void CreateSubModules(InitializerModuleGroup subModules)
  {
    base.CreateSubModules(subModules);
    PluginContext pluginCtx = new PluginContext();
    subModules.Add((InitializerModule) new ADIntegratorModule(pluginCtx));
    subModules.Add((InitializerModule) new ADDBAutoSetupModule(pluginCtx));
    subModules.Add((InitializerModule) new ADCommandsModule(pluginCtx));
  }

  public override void Load(IServiceProvider serviceProvider)
  {
    base.Load(serviceProvider);
    if (!ComHost.Configuration.ComSupportActive)
      return;
    ComHost.ActivateClassFactory(typeof (ADIntegratorAPI));
  }

  public override void Unload()
  {
    base.Unload();
    if (!ComHost.Configuration.ComSupportActive)
      return;
    ComHost.DeactivateClassFactory(typeof (ADIntegratorAPI));
  }
}
