
// Type: Intermech.Files.AlteredFilesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Files;

/// <summary>
/// Сервис для регистрации файлов, чье состояние на локальном диске соответствует состоянию в базе данных, но содержимое изменено.
/// Как правило, это файлы для просмотра с внедренными подписями, контрольными суммами и т.д.
/// Такие файлы не могут использоваться повторно, файловый сервис перезаписывает их каждый раз.
/// </summary>
public class AlteredFilesService
{
  private static readonly AlteredFilesService defaultInstance = new AlteredFilesService();
  private PathDictionary<bool> alteredFilesTable;
  private object syncRoot;

  /// <summary>Создает объект.</summary>
  public AlteredFilesService()
  {
    this.alteredFilesTable = new PathDictionary<bool>();
    this.syncRoot = new object();
  }

  public void ReportAlteredFile(string path)
  {
    if (path == null)
      throw new ArgumentNullException(nameof (path));
    lock (this.syncRoot)
      this.alteredFilesTable[path] = true;
  }

  public void ReportAlteredFiles(ICollection<string> pathList)
  {
    if (pathList == null)
      throw new ArgumentNullException(nameof (pathList));
    if (pathList.Count == 0)
      return;
    lock (this.syncRoot)
    {
      foreach (string path in (IEnumerable<string>) pathList)
      {
        if (string.IsNullOrEmpty(path))
          throw new InvalidOperationException("The file path must not be empty or null.");
        this.alteredFilesTable[path] = true;
      }
    }
  }

  public void RemoveFile(string path)
  {
    if (path == null)
      throw new ArgumentNullException(nameof (path));
    lock (this.syncRoot)
      this.alteredFilesTable.Remove(path);
  }

  public bool IsFileAltered(string path)
  {
    if (path == null)
      throw new ArgumentNullException(nameof (path));
    lock (this.syncRoot)
    {
      bool flag;
      if (this.alteredFilesTable.TryGetValue(path, out flag))
        return flag;
    }
    return false;
  }

  internal static AlteredFilesService Default
  {
    [DebuggerStepThrough] get => AlteredFilesService.defaultInstance;
  }
}
