// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IReadOnlyLocalFilesManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Интерфейс менеджера операций с атрибутом read-only для локальных файлов объектов IPS.
/// Реализация должна быть thread safe.
/// </summary>
public interface IReadOnlyLocalFilesManager
{
  /// <summary>
  /// Позволяет определить, можно ли управлять атрибутом read-only для указанного файла объекта IPS.
  /// </summary>
  /// <param name="dbObject">Описатель объекта IPS, которому принадлежит файл</param>
  /// <param name="dbObjectContext">Контекст объекта IPS, позволяющий связать обработку всех файлов объекта в единую операцию</param>
  /// <param name="relativeFilePath">Путь к файлу, как он записан в атрибуте объекта IPS</param>
  /// <param name="localFilePath">Путь к файлу объекта IPS на локальном диске</param>
  /// <returns>Признак возможности управления атрибутом read-only для локального файла объекта IPS</returns>
  /// <exception cref="T:ArgumentNullException">dbObject || dbObjectContext || relativeFilePath || localFilePath</exception>
  /// <exception cref="T:ArgumentException">relativeFilePath || localFilePath: не задан путь к файлу</exception>
  bool CanControlAttribute(
    DBObjectState dbObject,
    IDictionary<object, object> dbObjectContext,
    string relativeFilePath,
    string localFilePath);

  /// <summary>
  /// Определяет значение атрибута read-only для указанного файла объекта IPS с учетом возможного запрета на управление этим атрибутом файла.
  /// </summary>
  /// <param name="dbObject">Описатель объекта IPS, которому принадлежит файл</param>
  /// <param name="dbObjectContext">Контекст объекта IPS, позволяющий связать обработку всех файлов объекта в единую операцию</param>
  /// <param name="relativeFilePath">Путь к файлу, как он записан в атрибуте объекта IPS</param>
  /// <param name="localFilePath">Путь к файлу объекта IPS на локальном диске</param>
  /// <returns>Значение атрибута read-only для указанного файла</returns>
  /// <exception cref="T:ArgumentNullException">dbObject || dbObjectContext || relativeFilePath || localFilePath</exception>
  /// <exception cref="T:ArgumentException">relativeFilePath || localFilePath: не задан путь к файлу</exception>
  bool CalculateAttribute(
    DBObjectState dbObject,
    IDictionary<object, object> dbObjectContext,
    string relativeFilePath,
    string localFilePath);

  /// <summary>
  /// Событие для определения, можно ли управлять атрибутом read-only для указанного файла.
  /// </summary>
  event EventHandler<CanControlFileAttributeEventArgs> CanControlAttributeEvent;
}
