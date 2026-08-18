
// Type: Intermech.Files.UploadFileAction
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
/// Сохраняет измененный локальный файл в файловом атрибуте объекта.
/// </summary>
public sealed class UploadFileAction : 
  FileAttributeActionBase,
  IObjectFilesUploadResult,
  IFileAttributeActionInfo,
  IFileAttributeAction
{
  private readonly FileState fileState;
  private readonly string filePath;
  private bool allowNewFiles;
  private FileTypes fileType;
  private List<FileState> uploadedFileStates;

  /// <summary>Создает объект.</summary>
  /// <param name="fileState">Актуальное состояние файла на диске</param>
  /// <param name="filePath">Абсолютный путь к файлу на диске</param>
  public UploadFileAction(FileState fileState, string filePath)
  {
    if (fileState == null)
      throw new ArgumentNullException(nameof (fileState));
    if (string.IsNullOrEmpty(filePath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(filePath))
      throw new ArgumentException();
    this.fileState = fileState;
    this.filePath = filePath;
    this.fileType = FileTypes.ftNormal;
  }

  /// <summary>
  /// Включает и выключает возможность загрузки в объект новых файлов. По умолчанию такая возможность выключена.
  /// </summary>
  public bool AllowNewFiles
  {
    get => this.allowNewFiles;
    set => this.allowNewFiles = value;
  }

  /// <summary>
  /// Возвращает или задает тип сохраняемого в объект файла.
  /// </summary>
  public FileTypes FileType
  {
    get => this.fileType;
    set => this.fileType = value;
  }

  /// <summary>
  /// Возвращает коллекцию состояний файлов непосредственно после записи в базу IPS. Значение свойства может быть null,
  /// если запись файлов еще не была выполнена.
  /// </summary>
  public ICollection<FileState> UploadedFileStates
  {
    get => (ICollection<FileState>) this.uploadedFileStates;
  }

  /// <inheritdoc />
  protected override void DoPerform(IDBAttribute dbFileAttribute, List<string> initialFileNames)
  {
    int aIndex = initialFileNames.FindIndex((Predicate<string>) (relativeName => PathUtils.IsSamePath(relativeName, this.fileState.FileName)));
    if (aIndex < 0 && this.allowNewFiles)
    {
      if (initialFileNames.Count == 1 && dbFileAttribute.IsNull)
      {
        aIndex = 0;
        initialFileNames[0] = this.fileState.FileName;
      }
      else
      {
        dbFileAttribute.AddValue((object) this.fileType);
        aIndex = initialFileNames.Count;
        initialFileNames.Add(this.fileState.FileName);
      }
    }
    if (aIndex < 0)
      return;
    DateTime modifyDate = this.fileState.LastWriteTimeUtc.TruncateToSecond() + dbFileAttribute.Session.TimeZoneOffset;
    BlobInformation aBlobInformation = new BlobInformation(this.fileState.Length, 0L, modifyDate, this.fileState.FileName, ArcMethods.ZLibPacked, string.Empty);
    dbFileAttribute.Index = aIndex;
    aBlobInformation.BlobID = dbFileAttribute.AsInteger;
    aBlobInformation.FileType = this.fileType;
    using (Stream aSourceStream = (Stream) new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      new BlobProcWriter(dbFileAttribute.DBObjectID, AttributableElements.Object, dbFileAttribute.AttributeID, aIndex, 0, aBlobInformation, aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
    this.uploadedFileStates = new List<FileState>();
    this.uploadedFileStates.Add(new FileState(this.fileState.FileName, modifyDate - dbFileAttribute.Session.TimeZoneOffset, this.fileState.Length));
  }

  string IFileAttributeActionInfo.GetInfo()
  {
    return $"Upload to server an updated file at {this.filePath}";
  }
}
