// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.CADDocumentCopyingNinjectModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.Tools.Client.CompositionCopying.Views;
using Ninject.Extensions.Factory;
using Ninject.Modules;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying;

internal sealed class CADDocumentCopyingNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<CADDocumentCopyingInitializerModule>().ToSelf().InSingletonScope();
    this.Bind<CADDocumentCopyingDispatcherHandler>().ToSelf().InSingletonScope();
    this.Bind<RootDocumentTypesCache>().ToSelf().InSingletonScope();
    this.Bind<ICopyingSessionServices>().ToFactory<ICopyingSessionServices>();
    this.Bind<DialogService>().ToSelf().InSingletonScope();
    this.Bind<ICompositionCopyingWizardServices>().ToFactory<ICompositionCopyingWizardServices>();
  }
}
