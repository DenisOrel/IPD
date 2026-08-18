// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IFileImportSupport
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Files;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать интегратор, умеющий импортировать файлы в систему.
/// </summary>
public interface IFileImportSupport
{
  /// <summary>
  /// Позволяет определить, может ли интегратор импортировать этот файл.
  /// </summary>
  /// <param name="fileInfo">Сведения о файле</param>
  /// <param name="fileContent">Поток с содержимым файла</param>
  /// <returns>true, если интегратор поддерживает этот файл, false - если файл не знаком интегратору</returns>
  bool CanImportFile(FileInfo fileInfo, Stream fileContent);

  /// <summary>Возвращает флаги особенностей импорта файла.</summary>
  /// <returns>Флаги особенностей импорта файла</returns>
  ImportFileCapabilities GetImportFileCapabilities();

  /// <summary>Выполняет импорт файла в систему.</summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <returns>Результат импорта файла</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к имени импортируемого файла</exception>
  FileImportResult ImportFile(string fullPath);

  /// <summary>Выполняет импорт файла в систему.</summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <param name="importOptions">Опции импорта файла</param>
  /// <returns>Результат импорта файла</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к имени импортируемого файла</exception>
  FileImportResult ImportFile(string fullPath, FileImportOptions importOptions);
}
