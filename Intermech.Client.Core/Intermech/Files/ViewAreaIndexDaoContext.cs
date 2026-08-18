
// Type: Intermech.Files.ViewAreaIndexDaoContext
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.DaoModel;
using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Files;

internal sealed class ViewAreaIndexDaoContext : DaoContext
{
  private ViewAreaIndexDaoContext.FileStatesDaoService fileStatesService;

  public ViewAreaIndexDaoContext(ISqlProviderServices sqlServices, string connectionString)
    : base(sqlServices)
  {
    this.ConnectionString = connectionString != null ? connectionString : throw new ArgumentNullException(nameof (connectionString));
    this.fileStatesService = new ViewAreaIndexDaoContext.FileStatesDaoService();
    this.Services.Add((DaoService) this.fileStatesService);
  }

  public ViewAreaIndexDaoContext.FileStatesDaoService FileStates
  {
    [DebuggerStepThrough] get => this.fileStatesService;
  }

  internal sealed class FileStatesDaoService : DaoService
  {
    protected override void RunMaintenance(DbMaintenanceInfo info)
    {
      base.RunMaintenance(info);
      if (!info.NewDatabase)
        return;
      this.CreateMetadata();
    }

    private void CreateMetadata() => this.CreateTables();

    private void CreateTables()
    {
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "create table FileStates (FILE_KEY text primary key, FILE_PATH text not null, FILE_MT datetime not null, FIlE_LENGTH integer not null)";
          command.ExecuteNonQuery();
        }
      }
    }

    public void Append(FileState fileState)
    {
      if (fileState == null)
        throw new ArgumentNullException(nameof (fileState));
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "insert into FileStates (FILE_KEY, FILE_PATH, FILE_MT, FILE_LENGTH) values (@fkey, @fpath, @mt, @length)";
          SqlUtils.MakeParameter(command, "fkey", DbType.String).Value = (object) ViewAreaIndexDaoContext.FileStatesDaoService.ConvertToKey(fileState.FileName);
          SqlUtils.MakeParameter(command, "fpath", DbType.String).Value = (object) fileState.FileName;
          SqlUtils.MakeParameter(command, "mt", DbType.DateTime).Value = (object) fileState.LastWriteTimeUtc;
          SqlUtils.MakeParameter(command, "length", DbType.Int64).Value = (object) fileState.Length;
          command.ExecuteNonQuery();
        }
      }
    }

    public void RemoveAll()
    {
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "delete from FileStates";
          command.ExecuteNonQuery();
        }
      }
    }

    public void RemoveByPath(string filePath)
    {
      if (filePath == null)
        throw new ArgumentNullException(nameof (filePath));
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "delete from FileStates where FILE_KEY = @fkey";
          SqlUtils.MakeParameter(command, "fkey", DbType.String).Value = (object) ViewAreaIndexDaoContext.FileStatesDaoService.ConvertToKey(filePath);
          command.ExecuteNonQuery();
        }
      }
    }

    public FileState Find(string filePath)
    {
      if (filePath == null)
        throw new ArgumentNullException(nameof (filePath));
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "select FILE_PATH, FILE_MT, FILE_LENGTH from FileStates where FILE_KEY = @fkey";
          SqlUtils.MakeParameter(command, "fkey", DbType.String).Value = (object) ViewAreaIndexDaoContext.FileStatesDaoService.ConvertToKey(filePath);
          using (IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow))
            return dataReader.Read() ? new FileState(dataReader.GetString(0), dataReader.GetDateTime(1), dataReader.GetInt64(2)) : (FileState) null;
        }
      }
    }

    private static string ConvertToKey(string path) => path.ToLower();
  }
}
