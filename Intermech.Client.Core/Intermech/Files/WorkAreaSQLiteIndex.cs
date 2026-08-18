
// Type: Intermech.Files.WorkAreaSQLiteIndex
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Data;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class WorkAreaSQLiteIndex : IWorkAreaIndex
{
  private WorkAreaSQLiteIndexFile sqliteIndexFile;

  internal WorkAreaSQLiteIndex(WorkAreaSQLiteIndexFile sqliteIndexFile)
  {
    this.sqliteIndexFile = sqliteIndexFile != null ? sqliteIndexFile : throw new ArgumentNullException(nameof (sqliteIndexFile));
  }

  public void Append(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    this.sqliteIndexFile.CreateDbContext().ObjectStates.Append(objectState, DateTime.UtcNow);
  }

  public void Remove(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    this.sqliteIndexFile.CreateDbContext().ObjectStates.Remove(objectState);
  }

  public void Update(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    this.sqliteIndexFile.CreateDbContext().ObjectStates.Update(objectState, DateTime.UtcNow);
  }

  public void BatchAppend(ICollection<DBObjectState> list)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (list.Count == 0)
      return;
    WorkAreaSQLiteIndexDaoContext dbContext = this.sqliteIndexFile.CreateDbContext();
    using (new DynamicScope())
    {
      DataScope.OpenConnection(dbContext.ConnectionPool);
      DataScope.BeginTransaction();
      foreach (DBObjectState objectState in (IEnumerable<DBObjectState>) list)
        dbContext.ObjectStates.Append(objectState, DateTime.UtcNow);
      DataScope.Commit();
    }
  }

  public void BatchRemove(ICollection<DBObjectState> list)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (list.Count == 0)
      return;
    WorkAreaSQLiteIndexDaoContext dbContext = this.sqliteIndexFile.CreateDbContext();
    using (new DynamicScope())
    {
      DataScope.OpenConnection(dbContext.ConnectionPool);
      DataScope.BeginTransaction();
      foreach (DBObjectState objectState in (IEnumerable<DBObjectState>) list)
        dbContext.ObjectStates.Remove(objectState);
      DataScope.Commit();
    }
  }

  public void BatchUpdate(
    ICollection<DBObjectState> updateList,
    ICollection<DBObjectState> appendList)
  {
    if (updateList == null)
      throw new ArgumentNullException(nameof (updateList));
    if (appendList == null)
      throw new ArgumentNullException(nameof (appendList));
    if (updateList.Count == 0 && appendList.Count == 0)
      return;
    WorkAreaSQLiteIndexDaoContext dbContext = this.sqliteIndexFile.CreateDbContext();
    using (new DynamicScope())
    {
      DataScope.OpenConnection(dbContext.ConnectionPool);
      DataScope.BeginTransaction();
      foreach (DBObjectState update in (IEnumerable<DBObjectState>) updateList)
        dbContext.ObjectStates.Update(update, DateTime.UtcNow);
      foreach (DBObjectState append in (IEnumerable<DBObjectState>) appendList)
        dbContext.ObjectStates.Append(append, DateTime.UtcNow);
      DataScope.Commit();
    }
  }

  public bool Contains(long objectId)
  {
    return this.sqliteIndexFile.CreateDbContext().ObjectStates.Contains(objectId);
  }

  public DBObjectState Find(long id)
  {
    return this.sqliteIndexFile.CreateDbContext().ObjectStates.Find(id);
  }

  public DBObjectState FindByVersionId(long objectId)
  {
    return this.sqliteIndexFile.CreateDbContext().ObjectStates.FindByVersionId(objectId);
  }

  public DateTime? GetPublishTime(long objectId)
  {
    return this.sqliteIndexFile.CreateDbContext().ObjectStates.GetPublishTime(objectId);
  }

  public List<DBObjectState> Query()
  {
    return this.sqliteIndexFile.CreateDbContext().ObjectStates.QueryObjectStates();
  }

  public List<DBObjectState> QueryNotUsed(DateTime noUseSinceDate)
  {
    return this.sqliteIndexFile.CreateDbContext().ObjectStates.QueryNotUsedObjectStates(noUseSinceDate);
  }

  public void Flush()
  {
  }
}
