
// Type: Intermech.Files.DownloadFileAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Files;

/// <summary>
/// Выполняет публикацию файла путем извлечения тела файла из файлового атрибута объекта.
/// </summary>
public sealed class DownloadFileAction : 
  FileAttributeActionBase,
  IFileAttributeActionInfo,
  IFileAttributeAction
{
  private readonly FileState publishedFileState;
  private readonly string publishedFilePath;

  /// <summary>Создает объект.</summary>
  /// <param name="publishedFileState">Ожидаемое состояние публикуемого файла</param>
  /// <param name="publishedFilePath">Абсолютный путь к публикуемому файлу</param>
  public DownloadFileAction(FileState publishedFileState, string publishedFilePath)
  {
    if (publishedFileState == null)
      throw new ArgumentNullException(nameof (publishedFileState));
    if (string.IsNullOrEmpty(publishedFilePath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(publishedFilePath))
      throw new ArgumentException();
    this.publishedFileState = publishedFileState;
    this.publishedFilePath = publishedFilePath;
  }

  /// <inheritdoc />
  protected override void DoPerform(IDBAttribute dbFileAttribute, List<string> initialFileNames)
  {
    int index = initialFileNames.FindIndex((Predicate<string>) (relativeName => PathUtils.IsSamePath(relativeName, this.publishedFileState.FileName)));
    if (index < 0)
      return;
    Directory.CreateDirectory(Path.GetDirectoryName(this.publishedFilePath));
    this.DownloadFile(dbFileAttribute, index);
    File.SetLastWriteTimeUtc(this.publishedFilePath, this.publishedFileState.LastWriteTimeUtc);
  }

  private void DownloadFile(IDBAttribute dbFileAttribute, int index)
  {
    try
    {
      using (Stream aDestStream = (Stream) new FileStream(this.publishedFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
        new BlobProcReader(dbFileAttribute.DBObjectID, AttributableElements.Object, dbFileAttribute.AttributeID, index, 0, aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
    }
    catch
    {
      if (File.Exists(this.publishedFilePath))
        this.DeleteFileOnException();
      throw;
    }
  }

  private void DeleteFileOnException()
  {
    try
    {
      File.Delete(this.publishedFilePath);
    }
    catch (Exception ex)
    {
      string currentMethodName = this.GetCurrentMethodName(nameof (DeleteFileOnException));
      SuppressedExceptions.TraceException(ex, currentMethodName);
    }
  }

  string IFileAttributeActionInfo.GetInfo() => $"Download a fresh file at {this.publishedFilePath}";
}
