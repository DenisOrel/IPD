// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileProbeEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.IO;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Аргументы события, используемого для поиска метода импорта файла в базу IPS.
/// </summary>
public sealed class FileProbeEventArgs : EventArgs
{
  private FileInfo fileInfo;
  private Stream fileContent;
  private ImportFileHandler importHandler;
  private ImportFileCapabilities importCapabilities;

  /// <summary>Создает объект.</summary>
  /// <param name="fileInfo">Описание файла</param>
  /// <param name="fileContent">Содержимое файла</param>
  public FileProbeEventArgs(FileInfo fileInfo, Stream fileContent)
  {
    if (fileInfo == null)
      throw new ArgumentNullException();
    if (fileContent == null)
      throw new ArgumentNullException();
    this.fileInfo = fileInfo;
    this.fileContent = fileContent;
  }

  /// <summary>Возвращает описание импортируемого файла.</summary>
  public FileInfo FileInfo => this.fileInfo;

  /// <summary>Возвращает содержимое импортируемого файла.</summary>
  public Stream FileContent => this.fileContent;

  /// <summary>Возвращает или задает метод импорта файла в базу IPS.</summary>
  public ImportFileHandler ImportHandler
  {
    get => this.importHandler;
    set => this.importHandler = value;
  }

  /// <summary>
  /// Возвращает или задает флаги особых возможностей у метода импорта файла.
  /// </summary>
  public ImportFileCapabilities ImportCapabilities
  {
    get => this.importCapabilities;
    set => this.importCapabilities = value;
  }
}
