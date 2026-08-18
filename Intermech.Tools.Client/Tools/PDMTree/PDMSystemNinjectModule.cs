// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMSystemNinjectModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Diagnostics;
using Ninject.Extensions.Factory;
using Ninject.Modules;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMSystemNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<IEventLogWriter>().To<OutputViewLogWriter>().InSingletonScope().Named("StandardLibraryLog").WithConstructorArgument<string>("Библиотека стандартных CADMECH");
    this.Bind<IPDMSystemContext>().ToFactory<IPDMSystemContext>();
  }
}
