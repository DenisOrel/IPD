// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.CommandsNinjectModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Ninject.Extensions.Factory;
using Ninject.Modules;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class CommandsNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<ExtendedSaveHelper>().ToSelf().InSingletonScope();
    this.Bind<ExtendedSaveCommandProvider>().ToSelf();
    this.Bind<ArticleAttributesLockHandler>().ToSelf();
    this.Bind<CheckinCommandOptionsProvider>().ToSelf();
    this.Bind<ICommandsModuleFactory>().ToFactory<ICommandsModuleFactory>();
    this.Bind<CommandsModule>().ToSelf();
  }
}
