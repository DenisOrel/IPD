// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.AssemblyNinjectModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Caches.Metadata;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Kernel.Protection;
using Intermech.Kernel.Scripting;
using Intermech.Protection;
using Intermech.Tools.Integrators;
using Ninject;
using Ninject.Modules;


namespace Intermech.Kernel;

public sealed class AssemblyNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<IMetadataChangeMonitor>().To<MetadataChangeMonitor>().InSingletonScope();
    this.Bind<MetadataResolverFactory>().ToSelf().InSingletonScope();
    this.Kernel.Load((INinjectModule) new MetaDataHelperNinjectModule());
    this.Bind<IProtectionMessageService>().To<ProtectionMessageService>().InSingletonScope();
    this.Bind<IntegratorSettingsCacheManager>().ToSelf().InSingletonScope();
    this.Kernel.Load((INinjectModule) new CSharpScriptsNinjectModule());
  }
}
