// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IFileImportService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Интерфейс службы импорта файлов в базу IPS. Все методы, свойства и события интерфейса являются thread-safe.
/// </summary>
public interface IFileImportService
{
  /// <summary>
  /// Выполняет импорт указанного файла в базу IPS. В результате импорта в базе создается новых объект.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <returns>Идентификатор версии объекта, созданного в результате импорта файла</returns>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="fullPath" /> должен быть непустой строкой. Параметр <paramref name="fullPath" /> должен содержать путь в абсолютной форме.</exception>
  /// <exception cref="T:System.Exception">Ошибка в процессе импорта файла</exception>
  long ImportFile(string fullPath);

  /// <summary>
  /// Выполняет импорт указанного файла в базу IPS. В результате импорта в базе создается новых объект.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <param name="importOptions">Опции импорта файла</param>
  /// <returns>Результат импорта файла</returns>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="fullPath" /> должен быть непустой строкой. Параметр <paramref name="fullPath" /> должен содержать путь в абсолютной форме.</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="importOptions" /> не должен быть равен null</exception>
  FileImportResult ImportFile(string fullPath, FileImportOptions importOptions);

  /// <summary>Выполняет пакетный импорт файлов в базу IPS.</summary>
  /// <param name="files">Список абсолютных путей к импортируемым файлам</param>
  /// <param name="postProcess">Метод для пост-обработки каждого импортированного объекта. Может быть null</param>
  /// <exception cref="T:System.ArgumentException">Ошибка в списке путей к импортируемым файлам</exception>
  /// <exception cref="T:Intermech.FaultException">Файл не может быть импортирован</exception>
  void BatchImport(ICollection<string> files, Action<long> postProcess);

  /// <summary>Выполняет пакетный импорт файлов в базу IPS.</summary>
  /// <param name="importTitle">Заголовок окна для выбора импортируемых файлов</param>
  /// <param name="initialDirectory">Начальный каталог для окна выбора импортируемых файлов</param>
  /// <param name="postProcess">Метод для пост-обработки каждого импортированного объекта. Может быть null</param>
  /// <exception cref="T:System.ArgumentException">Не задан заголовок или начальный каталог для окна выбора файлов</exception>
  /// <exception cref="T:System.IO.DirectoryNotFoundException">Начальный каталог для окна выбора файлов не найден на диске</exception>
  /// <exception cref="T:Intermech.FaultException">Файл не может быть импортирован</exception>
  void BatchImport(string importTitle, string initialDirectory, Action<long> postProcess);

  /// <summary>Выполняет пакетный импорт файлов в базу IPS.</summary>
  /// <param name="files">Список абсолютных путей к импортируемым файлам</param>
  /// <param name="batchImportOptions">Опции импорта файлов</param>
  /// <returns>Коллекция с результатами импорта файлов</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="files" /> не должен быть равен null. Параметр <paramref name="batchImportOptions" /> не должен быть равен null.</exception>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="files" /> содержит недопустимые значения</exception>
  /// <exception cref="T:System.Exception">Ошибка в процессе импорта файла</exception>
  List<FileImportResult> ImportFiles(
    ICollection<string> files,
    BatchFileImportOptions batchImportOptions);

  /// <summary>
  /// Событие для подключения специализированных методов импорта файлов. Оно вызывается для каждого импортируемого файла.
  /// </summary>
  event EventHandler<FileProbeEventArgs> FileProbe;

  /// <summary>
  /// Событие для подключения методов импорта файлов, используемых при отсутствии специализированных методов импорта.
  /// </summary>
  event EventHandler<FileProbeEventArgs> FallbackProbe;
}
