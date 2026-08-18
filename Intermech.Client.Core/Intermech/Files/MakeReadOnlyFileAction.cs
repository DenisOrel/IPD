
// Type: Intermech.Files.MakeReadOnlyFileAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Files;

/// <summary>
/// Выполняет установку/снятие атрибута read-only у существующего файла в файловой области.
/// </summary>
internal sealed class MakeReadOnlyFileAction : 
  FileAttributeActionBase,
  IFileAttributeActionInfo,
  IFileAttributeAction
{
  private readonly string filePath;
  private readonly bool readOnly;
  private IOpenFilesService openFilesService;

  /// <summary>Создает объект.</summary>
  /// <param name="filePath">Абсолютный путь к файлу</param>
  /// <param name="readOnly">Требуемое значение атрибута read-only</param>
  /// <param name="openFilesService">Сервис файлов, открытых во внешних приложениях</param>
  public MakeReadOnlyFileAction(string filePath, bool readOnly, IOpenFilesService openFilesService)
  {
    if (string.IsNullOrEmpty(filePath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(filePath))
      throw new ArgumentException();
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    this.filePath = filePath;
    this.readOnly = readOnly;
    this.openFilesService = openFilesService;
  }

  /// <inheritdoc />
  protected override void DoPerform(IDBAttribute dbFileAttribute, List<string> initialFileNames)
  {
    FileUtils.SetReadOnlyAttribute(this.filePath, this.readOnly);
    this.openFilesService.SetReadOnlyFlag(this.filePath, this.readOnly);
  }

  string IFileAttributeActionInfo.GetInfo()
  {
    return $"Set the readonly attribute at {this.filePath} to the value {this.readOnly}";
  }
}
