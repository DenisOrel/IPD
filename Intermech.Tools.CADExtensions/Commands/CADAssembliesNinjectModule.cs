// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.Commands.CADAssembliesNinjectModule
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using Intermech.ApplicationModel;
using Ninject.Modules;

#nullable disable
namespace Intermech.Tools.CADExtensions.Commands;

internal sealed class CADAssembliesNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<InitializerModule>().To<CADAssembliesCommandsModule>().InSingletonScope().WithMetadata(InitializerModuleTags.StartupComplete, (object) true).BindingConfiguration.IsImplicit = true;
  }
}
