// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IDBObjectFilesDifferenceRules
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Интерфейс стратегии для определения изменений в локальных файлах объектов IPS.
/// </summary>
public interface IDBObjectFilesDifferenceRules
{
  /// <summary>
  /// Определяет наличие или отсутствие изменений в локальном файле объекта IPS, сравнивая локальное и удаленное (remote) состояние файла.
  /// </summary>
  /// <param name="utcNow">Текущее время в UTC</param>
  /// <param name="objectState">Версия объекта IPS, чей файл анализируется</param>
  /// <param name="localFileState">Состояние файла на локальном диске. Может быть null, если на диске файл отсутствует, но не одновременно с remoteFileState</param>
  /// <param name="remoteFileState">Состояние файла на базе данных. Может быть null, если в базе файл отсутствует, но не одновременно с localFileState</param>
  /// <returns>Результат сравнения состояний файлов</returns>
  /// <exception cref="T:ArgumentNullException">objectState</exception>
  /// <exception cref="T:ArgumentException">аргументы localFileState и remoteFileState одновременно не могут быть равны null</exception>
  FileDifferencePair CalculateDifference(
    DateTime utcNow,
    DBObjectState objectState,
    FileState localFileState,
    FileState remoteFileState);
}
