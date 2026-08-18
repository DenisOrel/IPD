
// Type: Intermech.ApplicationModel.IOCBasedPackageParameters
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject;
using System;
using System.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Параметры создания объектов типа <see cref="T:IOCBasedPackage" />
    /// </summary>
    public sealed class IOCBasedPackageParameters
    {
      private IKernel iocContainer;

      /// <summary>Создает объект.</summary>
      /// <param name="iocContainer">IOC-контейнер основного приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="iocContainer" /> не должен быть равен null</exception>
      public IOCBasedPackageParameters(IKernel iocContainer)
      {
        this.iocContainer = iocContainer != null ? iocContainer : throw new ArgumentNullException(nameof (iocContainer));
      }

      /// <summary>Возвращает IOC-контейнер основного приложения.</summary>
      public IKernel IOCContainer
      {
        [DebuggerStepThrough] get => this.iocContainer;
      }
    }
}
