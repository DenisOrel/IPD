// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MultiCAD.JTCommandsModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Tools.Client.MultiCAD;

internal sealed class JTCommandsModule : InitializerModule
{
  private IFactory navigatorFactory;
  private JTCommandsProvider jtCommandsProvider;

  public JTCommandsModule(IFactory navigatorFactory, JTCommandsProvider jtCommandsProvider)
  {
    this.navigatorFactory = navigatorFactory;
    this.jtCommandsProvider = jtCommandsProvider;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.jtCommandsProvider.AddCommandsToMenuTemplate(this.navigatorFactory.ContextMenuTemplate);
    this.navigatorFactory.AddCommandsProvider(1, (ICommandsProvider) this.jtCommandsProvider);
  }

  protected override void DoShutdown()
  {
    this.navigatorFactory.RemoveCommandsProvider(1, (ICommandsProvider) this.jtCommandsProvider);
    this.jtCommandsProvider.RemoveCommandsFromMenuTemplate(this.navigatorFactory.ContextMenuTemplate);
    base.DoShutdown();
  }
}
