
// Type: Intermech.ApplicationModel.IInitializerModuleFactory
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.ApplicationModel
{
    /// <summary>Интерфейс фабрики модулей инициализации.</summary>
    public interface IInitializerModuleFactory
    {
      /// <summary>Создает модуль инициализации указанного типа.</summary>
      /// <typeparam name="TModule">Тип создаваемого модуля иницилизации</typeparam>
      /// <returns>Созданный модуль инициализации</returns>
      TModule Create<TModule>() where TModule : InitializerModule;

      /// <summary>Создает модуль инициализации указанного типа.</summary>
      /// <param name="moduleType">Тип модуля инициализации</param>
      /// <returns>Созданный модуль инициализации</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="moduleType" /> не должен быть равен null</exception>
      InitializerModule Create(Type moduleType);
    }
}
