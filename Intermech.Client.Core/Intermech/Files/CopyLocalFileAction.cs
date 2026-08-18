
// Type: Intermech.Files.CopyLocalFileAction
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
/// Выполняет публикацию файла путем копирования подходящего локального файла.
/// </summary>
internal sealed class CopyLocalFileAction : 
  FileAttributeActionBase,
  IFileAttributeActionInfo,
  IFileAttributeAction
{
  private readonly FileState publishedState;
  private readonly string copyFromPath;
  private readonly string publishedFilePath;

  /// <summary>Создает объект.</summary>
  /// <param name="publishedState">Ожидаемое состояние публикуемого файла</param>
  /// <param name="publishedFilePath">Абсолютный путь к публикуемому файлу</param>
  /// <param name="copyFromPath">Абсолютный путь к копируемому локальному файлу</param>
  public CopyLocalFileAction(
    FileState publishedState,
    string publishedFilePath,
    string copyFromPath)
  {
    if (publishedState == null)
      throw new ArgumentNullException(nameof (publishedState));
    if (string.IsNullOrEmpty(publishedFilePath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(publishedFilePath))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(copyFromPath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(copyFromPath))
      throw new ArgumentException();
    this.publishedState = publishedState;
    this.publishedFilePath = publishedFilePath;
    this.copyFromPath = copyFromPath;
  }

  /// <inheritdoc />
  protected override void DoPerform(IDBAttribute dbFileAttribute, List<string> initialFileNames)
  {
    if (!initialFileNames.Exists((Predicate<string>) (fileName => PathUtils.IsSamePath(fileName, this.publishedState.FileName))))
      return;
    Directory.CreateDirectory(Path.GetDirectoryName(this.publishedFilePath));
    File.Copy(this.copyFromPath, this.publishedFilePath, false);
  }

  string IFileAttributeActionInfo.GetInfo()
  {
    return $"Duplicate a local file at {this.publishedFilePath} from {this.copyFromPath}";
  }
}
