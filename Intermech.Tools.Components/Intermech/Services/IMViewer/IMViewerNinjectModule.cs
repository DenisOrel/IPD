// Decompiled with JetBrains decompiler
// Type: Intermech.Services.IMViewer.IMViewerNinjectModule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data.SidecarObjects;
using Ninject.Modules;

#nullable disable
namespace Intermech.Services.IMViewer;

/// <summary>
/// Модуль Ninject для клиентской части интеграции с IMViewer.
/// </summary>
internal sealed class IMViewerNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<IIMViewerClientService, IIMViewerObjectCreatorService>().To<IMViewerClientService>().InSingletonScope();
    this.Bind<IMViewerObjectsIDCache>().ToSelf().InSingletonScope();
    this.Bind<IMViewerLaunchHandler>().ToSelf().InSingletonScope();
    this.Bind<IMViewerInitializerModule>().ToSelf().InSingletonScope();
  }
}
