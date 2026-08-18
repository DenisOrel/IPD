// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADCommandsModule
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.Integrators.Electrical;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADCommandsModule : InitializerModule
{
  private readonly PluginContext pluginCtx;
  private IFactory navigatorFactorySvc;
  private ADCommandsProvider commandsProvider;

  public ADCommandsModule(PluginContext pluginCtx) => this.pluginCtx = pluginCtx;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.pluginCtx.IntegratorInstance == null)
      return;
    this.navigatorFactorySvc = (IFactory) ServicesManager.GetService(typeof (IFactory));
    this.commandsProvider = new ADCommandsProvider(this.pluginCtx.IntegratorInstance);
    this.commandsProvider.UpdateMenuTemplate();
    this.navigatorFactorySvc.AddCommandsProvider((ICommandsProvider) this.commandsProvider);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.commandsProvider != null)
    {
      this.navigatorFactorySvc.RemoveCommandsProvider((ICommandsProvider) this.commandsProvider);
      this.commandsProvider = (ADCommandsProvider) null;
    }
    this.navigatorFactorySvc = (IFactory) null;
  }
}
