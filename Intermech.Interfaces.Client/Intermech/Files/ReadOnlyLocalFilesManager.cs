// Decompiled with JetBrains decompiler
// Type: Intermech.Files.ReadOnlyLocalFilesManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Реализует менеджер операций с атрибутом read-only для локальных файлов объектов IPS. Реализация является thread safe.
/// </summary>
public class ReadOnlyLocalFilesManager : IReadOnlyLocalFilesManager
{
  private object syncRoot;

  /// <summary>Создает объект.</summary>
  public ReadOnlyLocalFilesManager() => this.syncRoot = new object();

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
  public bool CanControlAttribute(
    DBObjectState dbObject,
    IDictionary<object, object> dbObjectContext,
    string relativeFilePath,
    string localFilePath)
  {
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    if (dbObjectContext == null)
      throw new ArgumentNullException(nameof (dbObjectContext));
    if (string.IsNullOrEmpty(relativeFilePath))
      throw new ArgumentException("Не указан путь к файлу объекта в базе данных.", nameof (relativeFilePath));
    if (string.IsNullOrEmpty(localFilePath))
      throw new ArgumentException("Не указан путь к файлу объекта на локальном диске.", nameof (localFilePath));
    lock (this.syncRoot)
    {
      EventHandler<CanControlFileAttributeEventArgs> controlAttributeEvent = this.CanControlAttributeEvent;
      if (controlAttributeEvent != null)
      {
        CanControlFileAttributeEventArgs e = new CanControlFileAttributeEventArgs(dbObject, dbObjectContext, relativeFilePath, localFilePath);
        controlAttributeEvent((object) this, e);
        return e.CanControl;
      }
    }
    return true;
  }

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
  public bool CalculateAttribute(
    DBObjectState dbObject,
    IDictionary<object, object> dbObjectContext,
    string relativeFilePath,
    string localFilePath)
  {
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    if (dbObjectContext == null)
      throw new ArgumentNullException(nameof (dbObjectContext));
    if (string.IsNullOrEmpty(relativeFilePath))
      throw new ArgumentException("Не указан путь к файлу объекта в базе данных.", nameof (relativeFilePath));
    if (string.IsNullOrEmpty(localFilePath))
      throw new ArgumentException("Не указан путь к файлу объекта на локальном диске.", nameof (localFilePath));
    return !dbObject.IsEditableState && this.CanControlAttribute(dbObject, dbObjectContext, relativeFilePath, localFilePath);
  }

  /// <summary>
  /// Событие для определения, можно ли управлять атрибутом read-only для указанного файла.
  /// </summary>
  public event EventHandler<CanControlFileAttributeEventArgs> CanControlAttributeEvent;
}
