
// Type: Intermech.Tools.Integrators.CompositeFileTypesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.IO;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует сервиса интегратора по определению файлов интегрируемого приложения, который позволяет задействовать для этого несколько более простых сервисов,
/// вызываемых последовательно. Этот сервис используется в тех случаях, когда у приложения имеется несколько версий форматов файлов, для каждого из которых
/// реализован отдельный сервис-определитель. Реализация класса является thread-safe.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public abstract class CompositeFileTypesService(IIntegrator owner) : 
  IntegratorService(owner),
  IApplicationFileTypes,
  IIntegratorService
{
  private ICollection<IApplicationFileTypes> subServices;

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.subServices = this.GetSubServices();
    foreach (IApplicationFileTypes subService in (IEnumerable<IApplicationFileTypes>) this.subServices)
    {
      if (subService is IntegratorService integratorService)
      {
        integratorService.OutputService = this.OutputService;
        integratorService.LicenseService = this.LicenseService;
        integratorService.Initialize();
      }
    }
  }

  /// <summary>
  /// Возвращает коллекцию более простых сервисов, каждый из которых умеет определять только один определенный формат файлов приложения.
  /// </summary>
  /// <returns>Коллекция более простых сервисов для определения файлов приложения</returns>
  protected virtual ICollection<IApplicationFileTypes> GetSubServices()
  {
    return (ICollection<IApplicationFileTypes>) new List<IApplicationFileTypes>();
  }

  /// <summary>
  /// Позволяет определить по имени файла, является ли он документом приложения.
  /// </summary>
  /// <remarks>
  /// Как правило, другие сервисы интегратора вызывают этот метод перед открытием документа из базы IPS в приложении, чтобы проверить,
  /// является ли мастер-файл документом приложения. Такая проверка нужна из-за того, что пользователь вручную может изменить список
  /// файлов любого документа в базе IPS.
  /// </remarks>
  /// <param name="fileName">Имя и путь к файлу, путь может быть относительным или отсутствовать</param>
  /// <returns>true, если это файл приложения, с которым осуществляется интеграция</returns>
  public bool IsApplicationFile(string fileName)
  {
    this.RequireReadyState();
    foreach (IApplicationFileTypes subService in (IEnumerable<IApplicationFileTypes>) this.subServices)
    {
      if (subService.IsApplicationFile(fileName))
        return true;
    }
    return false;
  }

  /// <summary>
  /// Позволяет определить по имени и содержимому файла, является ли он документом приложения. Сначала выполняется проверка по имени, и, если она
  /// успешно пройдена, то выполняется проверка содержимого файла.
  /// </summary>
  /// <remarks>
  /// Как правило, этот метод используется тогда, когда нужно найти интегратор, ответственный за обработку еще не зарегистрированного в IPS файла.
  /// Использовать расширение файла нельзя, так как файлы разных приложений могут использовать одинаковые расширения.
  /// </remarks>
  /// <param name="fileInfo">Сведения о файле</param>
  /// <param name="fileContent">Поток с содержимым файла, указатель положения установлен в начало потока</param>
  /// <returns>true, если файл приложения, с которым осуществляется интеграция</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  public bool IsApplicationFile(FileInfo fileInfo, Stream fileContent)
  {
    this.RequireReadyState();
    foreach (IApplicationFileTypes subService in (IEnumerable<IApplicationFileTypes>) this.subServices)
    {
      if (subService.IsApplicationFile(fileInfo, fileContent))
        return true;
      fileContent.Seek(0L, SeekOrigin.Begin);
    }
    return false;
  }
}
