
// Type: Intermech.Tools.Integrators.AbstractFileTypesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса интегратора, позволяющего определять файлы, созданные в приложении, с который осуществляется интеграция.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public abstract class AbstractFileTypesService(IIntegrator owner) : 
  IntegratorService(owner),
  IApplicationFileTypes,
  IIntegratorService
{
  private PathCollection fileExtensions;

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.fileExtensions = this.GetFileExtensions();
    CollectionUtils.RemoveAll<string>((IList<string>) this.fileExtensions, new Predicate<string>(string.IsNullOrEmpty));
  }

  /// <summary>
  /// Возвращает коллекцию расширений для файлов интегрируемого приложения.
  /// </summary>
  /// <returns>Коллекция расширений файлов</returns>
  protected virtual PathCollection GetFileExtensions() => new PathCollection();

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
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException();
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
      return this.VerifyFileName(fileName);
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
    if (fileInfo == null)
      throw new ArgumentNullException(nameof (fileInfo));
    if (fileContent == null)
      throw new ArgumentNullException(nameof (fileContent));
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
      return this.VerifyFileName(fileInfo.FullName) && this.VerifyFileContent(fileInfo, fileContent);
  }

  /// <summary>
  /// Проверяет имя и расширение файла, действительно ли оно создано в интегрируемом приложении.
  /// </summary>
  /// <param name="fileName">Имя и путь к файлу, путь может быть относительным или отсутствовать</param>
  /// <returns>true, если это файл приложения, с которым осуществляется интеграция</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  protected virtual bool VerifyFileName(string fileName)
  {
    string str = fileName != null ? Path.GetExtension(fileName) : throw new ArgumentNullException(nameof (fileName));
    return !string.IsNullOrEmpty(str) && this.fileExtensions.Contains(str);
  }

  /// <summary>
  /// Проверяет содержимое файла, действительно ли оно создано в интегрируемом приложении.
  /// </summary>
  /// <param name="fileInfo">Сведения о файле</param>
  /// <param name="fileContent">Поток с содержимым файла, указатель положения установлен в начало потока</param>
  /// <returns>true, если это файл приложения, с которым осуществляется интеграция</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  protected abstract bool VerifyFileContent(FileInfo fileInfo, Stream fileContent);
}
