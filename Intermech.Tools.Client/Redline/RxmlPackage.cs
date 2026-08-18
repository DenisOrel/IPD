// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.RxmlPackage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Ninject;
using Ninject.Activation;
using System;

#nullable disable
namespace Intermech.Redline;

internal sealed class RxmlPackage(IOCBasedPackageParameters createParameters) : IOCBasedPackage(createParameters, "Интегратор с редактором замечаний ИНТЕРМЕХ (RXML)")
{
  protected override void DoInitializeIOCContainer()
  {
    base.DoInitializeIOCContainer();
    this.GlobalIOCContainer.Bind<IClientRxmlService>().To<ClientRxmlService>().InSingletonScope();
    this.IOCContainer.Bind<RedliningIdCache>().ToSelf().InSingletonScope();
    this.IOCContainer.Bind<LastViewedDocumentsService>().ToSelf().InSingletonScope();
    this.IOCContainer.Bind<RedliningComObjectServiceLink>().ToSelf().InSingletonScope();
    this.IOCContainer.Bind<RedliningComObject>().ToSelf().OnActivation((Action<IContext, RedliningComObject>) ((context, obj) => obj.Initialize(context.Kernel.Get<RedliningComObjectServiceLink>())));
    this.IOCContainer.Bind<RxmlCommandsModule>().ToSelf();
    this.IOCContainer.Bind<RxmlFileServicesModule>().ToSelf();
    this.IOCContainer.Bind<RedliningComObjectModule>().ToSelf();
  }

  protected override void CreateSubModules(LazyInitializerModuleGroup subModules)
  {
    base.CreateSubModules(subModules);
    subModules.Add<RxmlCommandsModule>();
    subModules.Add<RxmlFileServicesModule>();
    subModules.Add<RedliningComObjectModule>();
  }
}
