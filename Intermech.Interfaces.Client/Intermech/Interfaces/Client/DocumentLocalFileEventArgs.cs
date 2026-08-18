// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DocumentLocalFileEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы события для локально извлеченных файлов документов IPS.
/// </summary>
public sealed class DocumentLocalFileEventArgs : EventArgs
{
  /// <summary>Создает объект.</summary>
  /// <param name="objectId">Идентификатор версии объекта IPS</param>
  /// <param name="objectTypeId">Идентификатор типа объекта IPS</param>
  /// <param name="fileName">Имя файла, как оно записано в файловом атрибуте объекта IPS</param>
  /// <param name="filePath">Абсолютный путь к файлу документа на локальном диске</param>
  public DocumentLocalFileEventArgs(
    long objectId,
    int objectTypeId,
    string fileName,
    string filePath)
  {
    this.ObjectId = objectId;
    this.ObjectTypeId = objectTypeId;
    this.FileName = fileName;
    this.FilePath = filePath;
  }

  /// <summary>Идентификатор версии объекта IPS</summary>
  public long ObjectId { get; }

  /// <summary>Идентификатор типа объекта IPS</summary>
  public int ObjectTypeId { get; }

  /// <summary>
  /// Имя файла, как оно записано в файловом атрибуте объекта IPS
  /// </summary>
  public string FileName { get; }

  /// <summary>Абсолютный путь к файлу документа на локальном диске</summary>
  public string FilePath { get; }
}
