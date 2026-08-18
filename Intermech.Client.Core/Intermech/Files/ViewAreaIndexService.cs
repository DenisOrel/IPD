
// Type: Intermech.Files.ViewAreaIndexService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Data;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class ViewAreaIndexService
{
  private ViewAreaIndexFile indexFile;

  public ViewAreaIndexService(ViewAreaIndexFile indexFile)
  {
    this.indexFile = indexFile != null ? indexFile : throw new ArgumentNullException(nameof (indexFile));
  }

  public void BatchAppend(ICollection<FileState> list)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (list.Count == 0)
      return;
    ViewAreaIndexDaoContext dbContext = this.indexFile.CreateDbContext();
    using (new DynamicScope())
    {
      DataScope.OpenConnection(dbContext.ConnectionPool);
      DataScope.BeginTransaction();
      foreach (FileState fileState in (IEnumerable<FileState>) list)
        dbContext.FileStates.Append(fileState);
      DataScope.Commit();
    }
  }

  public void BatchRemoveAll() => this.indexFile.CreateDbContext().FileStates.RemoveAll();

  public void BatchRemove(ICollection<string> list)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (list.Count == 0)
      return;
    ViewAreaIndexDaoContext dbContext = this.indexFile.CreateDbContext();
    using (new DynamicScope())
    {
      DataScope.OpenConnection(dbContext.ConnectionPool);
      DataScope.BeginTransaction();
      foreach (string filePath in (IEnumerable<string>) list)
        dbContext.FileStates.RemoveByPath(filePath);
      DataScope.Commit();
    }
  }

  public FileState Find(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    return this.indexFile.CreateDbContext().FileStates.Find(filePath);
  }
}
