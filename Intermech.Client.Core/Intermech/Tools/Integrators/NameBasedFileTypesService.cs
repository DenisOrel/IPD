
// Type: Intermech.Tools.Integrators.NameBasedFileTypesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.IO;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса интегратора, позволяющего определять файлы приложения, используя только расширение файла.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public abstract class NameBasedFileTypesService(IIntegrator owner) : AbstractFileTypesService(owner)
{
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
    if (fileContent == null)
      throw new ArgumentNullException(nameof (fileContent));
    return true;
  }
}
