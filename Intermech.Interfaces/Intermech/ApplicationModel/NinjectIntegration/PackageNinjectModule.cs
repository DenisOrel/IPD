
// Type: Intermech.ApplicationModel.NinjectIntegration.PackageNinjectModule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject;
using Ninject.Modules;
using Ninject.Syntax;
using System;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Базовые привязки для IOC-контейнера модулей расширения IPS.
    /// </summary>
    internal sealed class PackageNinjectModule : NinjectModule
    {
      /// <summary>Загружает модуль в IOC-контейнер.</summary>
      public override void Load()
      {
        this.Bind<IInitializerModuleFactory>().To<NinjectInitializerModuleFactory>().InSingletonScope();
        this.Bind(new Type[1]{ typeof (IOptionalService<>) }).To(typeof (NinjectOptionalService<>));
        this.Bind<IKernel, IResolutionRoot>().ToConstant<IKernel>(this.Kernel).WhenInjectedInto(typeof (NinjectInitializerModuleFactory), typeof (IOCBasedPackage), typeof (IOCBasedPackageParameters), typeof (IOptionalService<>)).InSingletonScope();
      }
    }
}
