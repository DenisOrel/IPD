
// Type: Intermech.Files.WorkAreaSQLiteIndexDaoContext
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.DaoModel;
using Intermech.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Files;

internal sealed class WorkAreaSQLiteIndexDaoContext : DaoContext
{
  private WorkAreaSQLiteIndexDaoContext.FormatVersionDaoService fmtService;
  private WorkAreaSQLiteIndexDaoContext.ObjectStatesDaoService objectStatesService;

  public WorkAreaSQLiteIndexDaoContext(ISqlProviderServices sqlServices, string connectionString)
    : base(sqlServices)
  {
    this.ConnectionString = connectionString != null ? connectionString : throw new ArgumentNullException(nameof (connectionString));
    PropertiesDaoService propService = new PropertiesDaoService("StorageProps");
    this.Services.Add((DaoService) propService);
    this.fmtService = new WorkAreaSQLiteIndexDaoContext.FormatVersionDaoService((IPropertiesService) propService);
    this.Services.Add((DaoService) this.fmtService);
    this.objectStatesService = new WorkAreaSQLiteIndexDaoContext.ObjectStatesDaoService(this.fmtService);
    this.Services.Add((DaoService) this.objectStatesService);
  }

  protected override void DoRunMaintenance(DbMaintenanceInfo info)
  {
    base.DoRunMaintenance(info);
    if (info.NewDatabase)
      return;
    this.ApplyPatches();
  }

  public WorkAreaSQLiteIndexDaoContext.ObjectStatesDaoService ObjectStates
  {
    get => this.objectStatesService;
  }

  private void ApplyPatches()
  {
    Action[] actionArray = new Action[1]
    {
      new Action(this.PatchToVersion1)
    };
    int formatVersion = this.fmtService.GetFormatVersion();
    if (formatVersion >= actionArray.Length)
      return;
    for (int index = formatVersion; index < actionArray.Length; ++index)
      actionArray[index]();
    this.CompressStorage();
  }

  private void PatchToVersion1()
  {
    using (new DynamicScope())
    {
      DataScope.OpenConnection(this.ConnectionPool);
      DataScope.BeginTransaction();
      this.objectStatesService.PatchToVersion1();
      this.fmtService.SetFormatVersion(1);
      DataScope.Commit();
    }
  }

  private void PatchToVersion2()
  {
  }

  private void CompressStorage()
  {
    using (new DynamicScope())
    {
      DataScope.OpenConnection(this.ConnectionPool);
      using (IDbCommand command = DataScope.CreateCommand())
      {
        command.CommandText = "vacuum";
        command.ExecuteNonQuery();
      }
    }
  }

  internal sealed class FormatVersionDaoService : DaoService
  {
    private IPropertiesService propService;
    private const string formatVersionProp = "version";

    internal FormatVersionDaoService(IPropertiesService propService)
    {
      this.propService = propService;
    }

    public int GetFormatVersion() => int.Parse(this.propService.ReadProperty("version", "0"));

    public void SetFormatVersion(int version)
    {
      this.propService.WriteProperty(nameof (version), version.ToString());
    }
  }

  internal sealed class ObjectStatesDaoService : DaoService
  {
    private WorkAreaSQLiteIndexDaoContext.FormatVersionDaoService fmtService;

    internal ObjectStatesDaoService(
      WorkAreaSQLiteIndexDaoContext.FormatVersionDaoService fmtService)
    {
      this.fmtService = fmtService;
    }

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
      this.CreateObjectStatesTable();
      this.fmtService.SetFormatVersion(1);
    }

    private void CreateObjectStatesTable()
    {
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "create table ObjectStates (ID integer primary key, OBJECT_ID integer not null, MODIFY_MODE integer not null, CAPTION text, LAST_USED datetime not null)";
          command.ExecuteNonQuery();
          command.CommandText = "create unique index IX_ObjectStates_OBJECT_ID on ObjectStates (OBJECT_ID asc)";
          command.ExecuteNonQuery();
        }
      }
    }

    internal void PatchToVersion1()
    {
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "alter table ObjectStates rename to ObjectStates_old";
          command.ExecuteNonQuery();
          this.CreateObjectStatesTable();
          command.CommandText = "insert into ObjectStates select * from ObjectStates_old";
          command.ExecuteNonQuery();
          command.CommandText = "drop table ObjectStates_old";
          command.ExecuteNonQuery();
        }
      }
    }

    public void Append(DBObjectState objectState, DateTime lastUsedTime)
    {
      if (objectState == null)
        throw new ArgumentNullException(nameof (objectState));
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "insert into ObjectStates (ID, OBJECT_ID, MODIFY_MODE, CAPTION, LAST_USED) values (@id, @oid, @mmode, @caption, @lastused)";
          SqlUtils.MakeParameter(command, "id", DbType.Int64).Value = (object) objectState.Id;
          SqlUtils.MakeParameter(command, "oid", DbType.Int64).Value = (object) objectState.ObjectId;
          SqlUtils.MakeParameter(command, "mmode", DbType.Int32).Value = (object) objectState.ModifyMode;
          SqlUtils.MakeParameter(command, "caption", DbType.String).Value = (object) objectState.Caption;
          SqlUtils.MakeParameter(command, "lastused", DbType.DateTime).Value = (object) lastUsedTime;
          command.ExecuteNonQuery();
        }
      }
    }

    public void Append(WorkAreaIndexDBObjectRecord record)
    {
      if (record == null)
        throw new ArgumentNullException(nameof (record));
      this.Append(record.ObjectState, record.LastUsedTime);
    }

    public void Remove(DBObjectState objectState)
    {
      if (objectState == null)
        throw new ArgumentNullException(nameof (objectState));
      this.RequireStarted();
      this.RemoveInternal(objectState.Id);
    }

    public void Remove(WorkAreaIndexDBObjectRecord record)
    {
      if (record == null)
        throw new ArgumentNullException(nameof (record));
      this.RequireStarted();
      this.RemoveInternal(record.ObjectState.Id);
    }

    private void RemoveInternal(long id)
    {
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "delete from ObjectStates where ID = @id";
          SqlUtils.MakeParameter(command, nameof (id), DbType.Int64).Value = (object) id;
          command.ExecuteNonQuery();
        }
      }
    }

    public void Update(DBObjectState objectState, DateTime lastUsedTime)
    {
      if (objectState == null)
        throw new ArgumentNullException(nameof (objectState));
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "update ObjectStates set OBJECT_ID = @oid, MODIFY_MODE = @mmode, CAPTION = @caption, LAST_USED = @lastused where ID = @id";
          SqlUtils.MakeParameter(command, "id", DbType.Int64).Value = (object) objectState.Id;
          SqlUtils.MakeParameter(command, "oid", DbType.Int64).Value = (object) objectState.ObjectId;
          SqlUtils.MakeParameter(command, "mmode", DbType.Int32).Value = (object) objectState.ModifyMode;
          SqlUtils.MakeParameter(command, "caption", DbType.String).Value = (object) objectState.Caption;
          SqlUtils.MakeParameter(command, "lastused", DbType.DateTime).Value = (object) lastUsedTime;
          command.ExecuteNonQuery();
        }
      }
    }

    public void Update(WorkAreaIndexDBObjectRecord record)
    {
      if (record == null)
        throw new ArgumentNullException(nameof (record));
      this.Update(record.ObjectState, record.LastUsedTime);
    }

    public bool Contains(long objectId)
    {
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "select ID from ObjectStates where OBJECT_ID = @oid";
          SqlUtils.MakeParameter(command, "oid", DbType.Int64).Value = (object) objectId;
          return command.ExecuteScalar() != null;
        }
      }
    }

    public DBObjectState Find(long id)
    {
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "select OBJECT_ID, MODIFY_MODE, CAPTION from ObjectStates where ID = @id";
          SqlUtils.MakeParameter(command, nameof (id), DbType.Int64).Value = (object) id;
          using (IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow))
            return dataReader.Read() ? new DBObjectState(id, dataReader.GetInt64(0), (ObjectModifyModes) dataReader.GetInt32(1), dataReader.GetString(2)) : (DBObjectState) null;
        }
      }
    }

    public DBObjectState FindByVersionId(long objectId)
    {
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "select ID, MODIFY_MODE, CAPTION from ObjectStates where OBJECT_ID = @oid";
          SqlUtils.MakeParameter(command, "oid", DbType.Int64).Value = (object) objectId;
          using (IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow))
            return dataReader.Read() ? new DBObjectState(dataReader.GetInt64(0), objectId, (ObjectModifyModes) dataReader.GetInt32(1), dataReader.GetString(2)) : (DBObjectState) null;
        }
      }
    }

    public DateTime? GetPublishTime(long objectId)
    {
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "select LAST_USED from ObjectStates where OBJECT_ID = @oid";
          SqlUtils.MakeParameter(command, "oid", DbType.Int64).Value = (object) objectId;
          object obj = command.ExecuteScalar();
          return obj != null ? new DateTime?(Convert.ToDateTime(obj)) : new DateTime?();
        }
      }
    }

    public List<DBObjectState> QueryObjectStates()
    {
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "select count(*) from ObjectStates";
          List<DBObjectState> dbObjectStateList = new List<DBObjectState>(Convert.ToInt32(command.ExecuteScalar()));
          command.CommandText = "select ID, OBJECT_ID, MODIFY_MODE, CAPTION from ObjectStates";
          using (IDataReader dataReader = command.ExecuteReader())
          {
            while (dataReader.Read())
              dbObjectStateList.Add(new DBObjectState(dataReader.GetInt64(0), dataReader.GetInt64(1), (ObjectModifyModes) dataReader.GetInt32(2), dataReader.GetString(3)));
          }
          return dbObjectStateList;
        }
      }
    }

    public List<DBObjectState> QueryNotUsedObjectStates(DateTime noUseSinceDate)
    {
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "select count(*) from ObjectStates";
          List<DBObjectState> dbObjectStateList = new List<DBObjectState>(Convert.ToInt32(command.ExecuteScalar()));
          command.CommandText = "select ID, OBJECT_ID, MODIFY_MODE, CAPTION from ObjectStates where LAST_USED < @nousedate";
          SqlUtils.MakeParameter(command, "nousedate", DbType.DateTime).Value = (object) noUseSinceDate.Date;
          using (IDataReader dataReader = command.ExecuteReader())
          {
            while (dataReader.Read())
              dbObjectStateList.Add(new DBObjectState(dataReader.GetInt64(0), dataReader.GetInt64(1), (ObjectModifyModes) dataReader.GetInt32(2), dataReader.GetString(3)));
          }
          return dbObjectStateList;
        }
      }
    }

    public void ScanRecords(Action<WorkAreaIndexDBObjectRecord> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      this.RequireStarted();
      using (new DynamicScope())
      {
        DataScope.OpenConnection(this.ConnectionPool);
        using (IDbCommand command = DataScope.CreateCommand())
        {
          command.CommandText = "select ID, OBJECT_ID, MODIFY_MODE, CAPTION, LAST_USED from ObjectStates";
          using (IDataReader dataReader = command.ExecuteReader())
          {
            while (dataReader.Read())
            {
              DBObjectState objectState = new DBObjectState(dataReader.GetInt64(0), dataReader.GetInt64(1), (ObjectModifyModes) dataReader.GetInt32(2), dataReader.GetString(3));
              DateTime dateTime = dataReader.GetDateTime(4);
              action(new WorkAreaIndexDBObjectRecord(objectState, dateTime));
            }
          }
        }
      }
    }
  }
}
