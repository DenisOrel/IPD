// Decompiled with JetBrains decompiler
// Type: Intermech.Files.DBObjectFilesDifferenceRules
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Базовая реализация стратегии для определения изменений в локальных файлах объектов IPS.
/// Реализация стратегии использует сравнение дат модификации локального и удаленного (remote) файлов.
/// </summary>
public class DBObjectFilesDifferenceRules : IDBObjectFilesDifferenceRules
{
  private FileDifferenceCalculator fileDiffCalculator;

  /// <summary>Создает объект.</summary>
  public DBObjectFilesDifferenceRules() => this.fileDiffCalculator = new FileDifferenceCalculator();

  /// <summary>
  /// Возвращает объект для сравнения состояний как отдельных файлов, так и групп файлов.
  /// </summary>
  protected FileDifferenceCalculator FileDifferenceCalculator
  {
    [DebuggerStepThrough] get => this.fileDiffCalculator;
  }

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
  public FileDifferencePair CalculateDifference(
    DateTime utcNow,
    DBObjectState objectState,
    FileState localFileState,
    FileState remoteFileState)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    if (localFileState == null && remoteFileState == null)
      throw new ArgumentException("Аргументы localFileState и remoteFileState не могут быть одновременно равны null.");
    return this.DoCalculateDifference(utcNow, objectState, localFileState, remoteFileState);
  }

  /// <summary>
  /// Определяет наличие или отсутствие изменений в локальном файле объекта IPS, сравнивая локальное и удаленное (remote) состояние файла.
  /// </summary>
  /// <param name="utcNow">Текущее время в UTC</param>
  /// <param name="objectState">Версия объекта IPS, чей файл анализируется</param>
  /// <param name="localFileState">Состояние файла на локальном диске. Может быть null, если на диске файл отсутствует, но не одновременно с remoteFileState</param>
  /// <param name="remoteFileState">Состояние файла на базе данных. Может быть null, если в базе файл отсутствует, но не одновременно с localFileState</param>
  /// <returns>Результат сравнения состояний файлов</returns>
  protected virtual FileDifferencePair DoCalculateDifference(
    DateTime utcNow,
    DBObjectState objectState,
    FileState localFileState,
    FileState remoteFileState)
  {
    return this.FileDifferenceCalculator.Calculate(localFileState, remoteFileState);
  }
}
