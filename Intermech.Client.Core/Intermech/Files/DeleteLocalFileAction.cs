
// Type: Intermech.Files.DeleteLocalFileAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Files;

/// <summary>Выполняет удаление существующего локального файла.</summary>
public sealed class DeleteLocalFileAction : 
  FileAttributeActionBase,
  IFileAttributeActionInfo,
  IFileAttributeAction
{
  private readonly FileState fileState;
  private readonly string filePath;

  /// <summary>Создает объект.</summary>
  /// <param name="fileState">Актуальное состояние удаляемого файла на диске</param>
  /// <param name="filePath">Абсолютный путь к удаляемому файлу</param>
  public DeleteLocalFileAction(FileState fileState, string filePath)
  {
    if (fileState == null)
      throw new ArgumentNullException(nameof (fileState));
    if (string.IsNullOrEmpty(filePath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(filePath))
      throw new ArgumentException();
    this.fileState = fileState;
    this.filePath = filePath;
  }

  /// <inheritdoc />
  protected override void DoPerform(IDBAttribute dbFileAttribute, List<string> initialFileNames)
  {
    if (!File.Exists(this.filePath))
      return;
    if (File.GetLastWriteTimeUtc(this.filePath) != this.fileState.LastWriteTimeUtc)
      throw new FileOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1283"), (object) this.filePath));
    File.SetAttributes(this.filePath, FileAttributes.Normal);
    File.Delete(this.filePath);
  }

  string IFileAttributeActionInfo.GetInfo() => $"Delete a local file at {this.filePath}";
}
