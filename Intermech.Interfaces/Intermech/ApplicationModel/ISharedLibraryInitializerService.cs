
// Type: Intermech.ApplicationModel.ISharedLibraryInitializerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Интерфейс сервиса инициализации общих библиотек приложения.
    /// </summary>
    public interface ISharedLibraryInitializerService
    {
      /// <summary>Возвращает фабрику модулей инициализации.</summary>
      IInitializerModuleFactory InitializerModuleFactory { get; }
    }
}
