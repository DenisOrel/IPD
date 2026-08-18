
// Type: Intermech.Files.WorkAreaSQLiteIndexFile
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data.SQLite;


namespace Intermech.Files;

internal sealed class WorkAreaSQLiteIndexFile : SQLiteDaoContextFactory
{
  public WorkAreaSQLiteIndexFile(string filePath, int cacheSizeInKBytes, bool asyncWrites)
  {
    this.DbFilePath = filePath;
    this.CacheSizeInKBytes = new int?(cacheSizeInKBytes);
    this.AsyncWritesMode = new bool?(asyncWrites);
    this.Freeze();
    this.InitializeAndRunMaintenance();
  }

  private void InitializeAndRunMaintenance() => this.CreateDbContext().OptionalClose();

  public WorkAreaSQLiteIndexDaoContext CreateDbContext()
  {
    this.RequireFrozen();
    WorkAreaSQLiteIndexDaoContext dbContext = new WorkAreaSQLiteIndexDaoContext(this.CreateSqlServices(), this.ConnectionString);
    dbContext.Open();
    return dbContext;
  }
}
