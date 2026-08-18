// Decompiled with JetBrains decompiler
// Type: Intermech.Files.CanControlFileAttributeEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Аргументы события, позволяющего определить, можно ли управлять атрибутом read-only у локального файла документа.
/// </summary>
/// <remarks>
/// Не для всех файлов возможно свободное управление атрибутом read-only, так как это может приводить к поломке приложения, работающего с этими файлами.
/// Например, нельзя выставлять атрибут read-only на файлы красного карандаша - это приводит к невозможности создания/редактирования замечаний к просматриваемым архивным копиям документов.
/// Другим примером могут служить файлы параметрических семейств Autodesk Inventor, эти файлы обновляются CAD-системой в отложенном режиме по мере необходимости. Установка на
/// них атрибутов read-only приводит к падению CAD-системы.
/// </remarks>
[Serializable]
public class CanControlFileAttributeEventArgs : EventArgs
{
  private DBObjectState dbObject;
  private IDictionary<object, object> dbObjectContext;
  private string relativeFilePath;
  private string localFilePath;
  private bool canControl;

  /// <summary>Создает объект.</summary>
  /// <param name="dbObject">Описатель объекта IPS, которому принадлежит файл</param>
  /// <param name="dbObjectContext">Контекст объекта IPS, позволяющий связать несколько событий в единую операцию</param>
  /// <param name="relativeFilePath">Путь к файлу, как он записан в атрибуте объекта IPS</param>
  /// <param name="localFilePath">Путь к файлу объекта IPS на локальном диске</param>
  /// <exception cref="T:ArgumentNullException">dbObject || dbObjectContext || relativeFilePath || localFilePath</exception>
  public CanControlFileAttributeEventArgs(
    DBObjectState dbObject,
    IDictionary<object, object> dbObjectContext,
    string relativeFilePath,
    string localFilePath)
  {
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    if (dbObjectContext == null)
      throw new ArgumentNullException(nameof (dbObjectContext));
    if (relativeFilePath == null)
      throw new ArgumentNullException(nameof (relativeFilePath));
    if (localFilePath == null)
      throw new ArgumentNullException(nameof (localFilePath));
    this.dbObject = dbObject;
    this.dbObjectContext = dbObjectContext;
    this.relativeFilePath = relativeFilePath;
    this.localFilePath = localFilePath;
    this.canControl = true;
  }

  /// <summary>Описатель объекта IPS, которому принадлежит файл.</summary>
  public DBObjectState DBObject
  {
    [DebuggerStepThrough] get => this.dbObject;
  }

  /// <summary>
  /// Возвращает контекст объекта IPS, позволяющий связать несколько событий в единую операцию.
  /// Все события, составляющие единую операцию, получают один и тот же объект контекста.
  /// </summary>
  public IDictionary<object, object> DBObjectContext
  {
    [DebuggerStepThrough] get => this.dbObjectContext;
  }

  /// <summary>Путь к файлу, как он записан в атрибуте объекта IPS.</summary>
  public string RelativeFilePath
  {
    [DebuggerStepThrough] get => this.relativeFilePath;
  }

  /// <summary>Путь к файлу объекта IPS на локальном диске.</summary>
  public string LocalFilePath
  {
    [DebuggerStepThrough] get => this.localFilePath;
  }

  /// <summary>
  /// Возвращает или задает признак, разрешающий управление атрибутом read-only для локального файла объекта IPS.
  /// </summary>
  public bool CanControl
  {
    [DebuggerStepThrough] get => this.canControl;
    [DebuggerStepThrough] set => this.canControl = value;
  }
}
