
// Type: Intermech.Files.WorkAreaFileTracker
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.IO;


namespace Intermech.Files;

/// <summary>
/// Реализует трекер файлов для рабочей области файлового хранилища. Является thread-safe.
/// </summary>
internal sealed class WorkAreaFileTracker : FileTracker
{
  private object syncRoot;

  public WorkAreaFileTracker() => this.Initialize();

  public WorkAreaFileTracker(int capacity)
    : base(capacity)
  {
    this.Initialize();
  }

  private void Initialize() => this.syncRoot = new object();

  protected override void DoSaveFileState(
    long objectId,
    string fileName,
    DateTime lastWriteTimeUtc)
  {
    lock (this.syncRoot)
      base.DoSaveFileState(objectId, fileName, lastWriteTimeUtc);
  }

  protected override void DoRemoveFileState(string fileName)
  {
    lock (this.syncRoot)
      base.DoRemoveFileState(fileName);
  }

  protected override DateTime? DoGetLastWriteTime(long objectId, string fileName)
  {
    lock (this.syncRoot)
      return base.DoGetLastWriteTime(objectId, fileName);
  }

  protected override void ValidateFileName(string fileName)
  {
    base.ValidateFileName(fileName);
    if (string.IsNullOrEmpty(fileName))
      throw new InvalidOperationException("Имя файла не должно быть пустым.");
    if (Path.IsPathRooted(fileName))
      throw new InvalidOperationException($"Путь к файлу '{fileName}' должен быть указан в относительной форме, вычисленной от корня рабочей области.");
  }
}
