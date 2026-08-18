// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.IMViewerNinjectModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Ninject.Modules;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal sealed class IMViewerNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<MenuModule>().ToSelf().InSingletonScope();
    this.Bind<MenuCommandsProvider>().ToSelf().WhenInjectedInto<MenuModule>();
    this.Bind<MenuCommandsFlags>().ToSelf().InSingletonScope();
    this.Bind<SystemSettingsInitializerModule>().ToSelf().InSingletonScope();
    this.Bind<SystemSettingsEditorModel>().ToSelf();
    this.Bind<ModelFileReplaceModule>().ToSelf().InSingletonScope();
  }
}
