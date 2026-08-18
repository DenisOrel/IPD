// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.ModelDrawings.AncillaryDrawingsNinjectModule
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using Intermech.ApplicationModel;
using Ninject.Modules;

#nullable disable
namespace Intermech.Tools.CADExtensions.ModelDrawings;

internal sealed class AncillaryDrawingsNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<InitializerModule>().To<AncillaryDrawingsCommandsInitializer>().InSingletonScope().WithMetadata(InitializerModuleTags.StartupComplete, (object) true).BindingConfiguration.IsImplicit = true;
  }
}
