
// Type: Intermech.ApplicationModel.NinjectIntegration.NinjectPackageActivator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Plugins;
using Ninject;
using System;
using System.Diagnostics;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Реализация создателя для объектов типа IPackage с поддержкой внедрения зависимостей (Dependency Injection).
    /// В качестве контейнера используется Ninject.
    /// Реализация является thread safe.
    /// </summary>
    internal sealed class NinjectPackageActivator : NinjectActivatorBase, IPackageActivator
    {
      private Type packageInterface;

      /// <summary>Создает объект.</summary>
      /// <param name="iocContainer">IOC-контейнер приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="iocContainer" /> не должен быть равен null</exception>
      public NinjectPackageActivator(IKernel iocContainer)
        : base(iocContainer)
      {
        this.packageInterface = typeof (IPackage);
      }

      private Type PackageInterface
      {
        [DebuggerStepThrough] get => this.packageInterface;
      }

      /// <summary>Создает объект, реализующий интерфейс IPackage.</summary>
      /// <param name="packageType">Тип объектов, реализующий интерфейс IPackage</param>
      /// <returns>Созданный объект</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="packageType" /> не должен быть равен null</exception>
      /// <exception cref="T:ArgumentException">Тип объектов должен быть реализовывать интерфейс IPackage</exception>
      public IPackage CreateInstance(Type packageType)
      {
        if (packageType == (Type) null)
          throw new ArgumentNullException(nameof (packageType));
        return this.PackageInterface.IsAssignableFrom(packageType) ? (IPackage) this.DoCreateInstance(packageType) : throw new ArgumentException($"Type '{packageType}' must implement the interface '{this.PackageInterface}'.", nameof (packageType));
      }
    }
}
