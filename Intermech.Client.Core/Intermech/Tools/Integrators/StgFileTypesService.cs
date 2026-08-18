
// Type: Intermech.Tools.Integrators.StgFileTypesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Runtime.ComInterop;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса, когда определение принадлежности файлов определенному приложению основано на чтении глобального идентификатора
/// контейнера Structured Storage.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public abstract class StgFileTypesService(IIntegrator owner) : ContentBasedFileTypesService(owner)
{
  private ICollection<Guid> fileGuids;

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.fileGuids = this.GetFileContentGuids();
  }

  /// <summary>
  /// Возвращает коллекцию идентификаторов типов контейнеров Structured Storage для файлов интегрируемого приложения.
  /// </summary>
  /// <returns>Коллекция идентификаторов для типов контейнеров</returns>
  protected virtual ICollection<Guid> GetFileContentGuids() => (ICollection<Guid>) new List<Guid>();

  /// <summary>
  /// Проверяет содержимое файла, действительно ли оно создано в интегрируемом приложении.
  /// </summary>
  /// <param name="fileInfo">Сведения о файле</param>
  /// <param name="fileContent">Поток с содержимым файла, указатель положения установлен в начало потока</param>
  /// <returns>true, если это файл приложения, с которым осуществляется интеграция</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  protected override bool VerifyFileContent(FileInfo fileInfo, Stream fileContent)
  {
    if (fileInfo == null)
      throw new ArgumentNullException(nameof (fileInfo));
    Guid guid = fileContent != null ? StgServices.GetStorageGuidEx(fileInfo, fileContent) : throw new ArgumentNullException(nameof (fileContent));
    return guid != Guid.Empty && this.fileGuids.Contains(guid);
  }
}
