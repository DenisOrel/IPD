
// Type: Intermech.Tools.Integrators.ContentBasedFileTypesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса интегратора, позволяющего определять файлы приложения, используя расширение файла и содержимое файла.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public abstract class ContentBasedFileTypesService(IIntegrator owner) : AbstractFileTypesService(owner)
{
}
