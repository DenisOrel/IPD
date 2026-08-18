
// Type: Intermech.Interfaces.Plugins.DefaultPackageActivator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Plugins
{
    /// <summary>
    /// Реализация создателя для объектов типа IPackage, используемая по умолчанию.
    /// </summary>
    internal sealed class DefaultPackageActivator : IPackageActivator
    {
      /// <summary>Создает объект, реализующий интерфейс IPackage.</summary>
      /// <param name="packageType">Тип объектов, реализующий интерфейс IPackage</param>
      /// <returns>Созданный объект</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="packageType" /> не должен быть равен null</exception>
      /// <exception cref="T:ArgumentException">Тип объектов должен быть реализовывать интерфейс IPackage</exception>
      public IPackage CreateInstance(Type packageType)
      {
        return !(packageType == (Type) null) ? (IPackage) Activator.CreateInstance(packageType) : throw new ArgumentNullException(nameof (packageType));
      }
    }
}
