
// Type: Intermech.ApplicationModel.NinjectIntegration.MainApplicationNinjectModule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Plugins;
using Ninject;
using Ninject.Modules;
using Ninject.Planning.Bindings.Resolvers;
using Ninject.Syntax;
using System;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Базовые привязки для IOC-контейнера, общие для всех основных приложений IPS.
    /// </summary>
    public sealed class MainApplicationNinjectModule : NinjectModule
    {
      /// <summary>Загружает модуль в IOC-контейнер.</summary>
      public override void Load()
      {
        this.Kernel.Components.Add<IMissingBindingResolver, MissingNinjectBindingResolver>();
        this.Bind<IApplicationServiceResolver>().To<MissingApplicationServiceResolver>().InSingletonScope();
        this.Bind<IInitializerModuleFactory>().To<NinjectInitializerModuleFactory>().InSingletonScope();
        this.Bind<ISharedLibraryInitializerService>().To<SharedLibraryInitializerService>().InSingletonScope();
        this.Bind<IPackageActivator>().To<NinjectPackageActivator>().InSingletonScope();
        this.Bind(new Type[1]{ typeof (IOptionalService<>) }).To(typeof (NinjectOptionalService<>));
        this.Bind<IKernel, IResolutionRoot>().ToConstant<IKernel>(this.Kernel).WhenInjectedInto(typeof (MissingApplicationServiceResolver), typeof (NinjectInitializerModuleFactory), typeof (SharedLibraryInitializerService), typeof (NinjectPackageActivator), typeof (IOCBasedPackage), typeof (IOCBasedPackageParameters), typeof (NinjectOptionalService<>)).InSingletonScope();
      }
    }
}
