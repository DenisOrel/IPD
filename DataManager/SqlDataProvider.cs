// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.SqlDataProvider
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Server.Data;

public sealed class SqlDataProvider : CustomDataProvider
{
  private readonly bool _snapshotIsolationMode;
  private readonly string _transisolationLevelSQL;
  private readonly IsolationLevel _defaultIsolationLevel;
  private static readonly char[] _ReplacementList = new char[3]
  {
    '[',
    '_',
    '%'
  };

  public SqlDataProvider()
  {
    this.NoLockMode = true;
    this._snapshotIsolationMode = string.Equals(ConfigurationManager.AppSettings.Get("IsolationSnapshots"), "1");
    if (this._snapshotIsolationMode)
    {
      this._transisolationLevelSQL = "SET TRANSACTION ISOLATION LEVEL SNAPSHOT";
      this._defaultIsolationLevel = IsolationLevel.Snapshot;
    }
    else
    {
      this._transisolationLevelSQL = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";
      this._defaultIsolationLevel = IsolationLevel.ReadUncommitted;
    }
  }

  public override Exception WrapDbException(Exception exception)
  {
    return exception == null ? (Exception) null : new Exception(exception.Message, exception);
  }

  public override void InitRDBMSVersion(IDbManager firstDbManager)
  {
    DataTable dataTable = firstDbManager.ExecuteDataTable("SELECT SERVERPROPERTY('productversion')");
    if (dataTable.Rows.Count != 0)
    {
      string str = Convert.ToString(dataTable.Rows[0][0]);
      this.RDBMSVersion = Convert.ToInt32(str.Substring(0, str.IndexOf('.')));
    }
    this.DatabaseName = ((DbManager) firstDbManager).GetConnectionAs<SqlConnection>().Database;
    object obj = firstDbManager.ExecuteScalar($"SELECT DATABASEPROPERTYEX('{this.DatabaseName}', 'Collation')");
    if (obj != null && obj != DBNull.Value)
      this.DatabaseCollate = Convert.ToString(obj);
    base.InitRDBMSVersion(firstDbManager);
  }

  protected override int MinimalRDBMSVersion
  {
    [DebuggerStepThrough] get => 13;
  }

  public override bool CanUseIndexTablespace
  {
    [DebuggerStepThrough] get => false;
  }

  protected override void DoSetIndexTablespaceName(string tablespaceName)
  {
    throw new NotSupportedException();
  }

  public override IDbConnection CreateConnection(string connectionString)
  {
    SqlConnection connection = new SqlConnection();
    if (connectionString != null)
      connection.ConnectionString = this.UpdateConnectionString(connectionString);
    connection.StateChange += new StateChangeEventHandler(this.OnDbConnectionStateChange);
    return (IDbConnection) connection;
  }

  public override IDbDataAdapter CreateDataAdapter(IDbConnection connection)
  {
    if (connection == null)
      throw new ArgumentNullException(nameof (connection));
    return (IDbDataAdapter) new SqlDataAdapter();
  }

  public override IDbCommand CreateCommand(IDbConnection connection)
  {
    return connection != null ? connection.CreateCommand() : throw new ArgumentNullException(nameof (connection));
  }

  public override string UpdateCommandText(CommandType commandType, string commandText)
  {
    if (commandType != CommandType.Text)
      return commandText;
    string str = this.UpdateUnicodeLiterals(this.GetUnlockedSelect(commandText));
    if (str.IndexOf(':') == -1)
      return str;
    if (str.IndexOf('\'') == -1)
      return str.Replace(':', '@');
    bool flag = false;
    int length = str.Length;
    char[] charArray = str.ToCharArray();
    for (int index = 0; index < length; ++index)
    {
      char ch = charArray[index];
      if (ch == '\'')
        flag = !flag;
      else if (!flag && ch == ':')
        charArray[index] = '@';
    }
    return new string(charArray);
  }

  private string GetUnlockedSelect(string commandText, bool noQueryMode = false)
  {
    if (this.DefaultIsolationLevel != IsolationLevel.ReadUncommitted || !this.NoLockMode)
      return commandText;
    int startIndex1 = 0;
    bool flag;
    if (noQueryMode)
    {
      flag = commandText.StartsWith("SELECT") || commandText.StartsWith("select");
    }
    else
    {
      startIndex1 = commandText.IndexOf("SELECT ");
      if (startIndex1 < 0)
        startIndex1 = commandText.IndexOf("select ");
      flag = startIndex1 > -1;
    }
    if (flag)
    {
      int startIndex2 = commandText.ToUpper().IndexOf(" WHERE ", startIndex1);
      if (startIndex2 > -1)
      {
        commandText = commandText.Insert(startIndex2, " WITH(NOLOCK)");
      }
      else
      {
        int startIndex3 = commandText.ToUpper().IndexOf(" ORDER BY ", startIndex1);
        if (startIndex3 > -1)
        {
          commandText = commandText.Insert(startIndex3, " WITH(NOLOCK)");
        }
        else
        {
          int startIndex4 = commandText.ToUpper().IndexOf(" GROUP BY ", startIndex1);
          if (startIndex4 > -1)
            commandText = commandText.Insert(startIndex4, " WITH(NOLOCK)");
          else if (commandText.ToUpper().IndexOf(" FROM ", startIndex1) > -1)
            commandText += " WITH(NOLOCK)";
        }
      }
    }
    return commandText;
  }

  public override void UpdateParameterTypeByValue(IDbDataParameter parameter, object paramValue)
  {
    if (!(paramValue is Guid))
      return;
    parameter.DbType = DbType.Guid;
  }

  public override string UpdateParameterName(string parameterName)
  {
    if (parameterName.Length > 0 && parameterName[0] != '@')
      parameterName = parameterName[0] != ':' ? "@" + parameterName : parameterName.Replace(':', '@');
    return parameterName;
  }

  public override object UpdateParameterValue(string name, object val)
  {
    if (val == null)
      return (object) DBNull.Value;
    return val is byte[] numArray && numArray.Length == 0 ? (object) DBNull.Value : val;
  }

  public override Type ConnectionType => typeof (SqlConnection);

  public override string Name => "Sql";

  public override string Now => "GETUTCDATE()";

  public override string Ln => "LOG";

  public override string Length(string param) => $"LEN({param})";

  public override string NVARCHARType(int len) => $"NVARCHAR({len})";

  public override string DATEType => "DATETIME";

  public override string FLOATType => "FLOAT";

  public override string INTEGERType => "BIGINT";

  public override string SMALLINTType => "SMALLINT";

  public override string TEXTType => "TEXT";

  public override string BLOBType => "IMAGE";

  public override string CreateGeneratorString(
    string generatorName,
    long startValue,
    int incrementValue)
  {
    return $"CREATE TABLE {generatorName} (F_KEY INTEGER NOT NULL IDENTITY ({startValue}, {incrementValue}), F_CREATED DATETIME)";
  }

  public override long NextGeneratorValue(string generatorName, IDbManager db)
  {
    using (db.WithOpenConnection())
    {
      db.ExecuteNonQuery($"INSERT INTO {generatorName} (F_CREATED) VALUES ({db.DataProvider.Now})");
      return Convert.ToInt64(db.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
    }
  }

  public override long BeforeInsertID(string generatorName, IDbManager db) => 0;

  public override long AfterInsertID(IDbManager db)
  {
    return Convert.ToInt64(db.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
  }

  public override string InsertGeneratorValueString(string generatorName) => string.Empty;

  public override string GetTopRecordsInSelect(int recordsCount) => $"TOP {recordsCount} ";

  public override string GetTopRecordsInWhere(int recordsCount) => "";

  public override string GetTopString(int recordsCount) => this.GetTopRecordsInSelect(recordsCount);

  public override string GetRowNumString(int recordsCount) => string.Empty;

  public override string GetFetchSQL(int PacketSize) => string.Empty;

  public override string FetchRowsSQL(int PacketSize) => this.GetFetchSQL(PacketSize);

  public override string NullsOrder => "";

  public override string GetUTCSelect(string fldName, TimeSpan timeOffset)
  {
    return $"DATEADD(minute, {Convert.ToInt32(timeOffset.TotalMinutes)}, {fldName})";
  }

  public override void CreateObjectTypeView(
    string viewName,
    string addFields,
    IDbManager db,
    List<string> indexesList)
  {
    db.ExecuteNonQuery($"CREATE TABLE {viewName} (F_OBJECT_ID          BigNumber_DEF NOT NULL,F_ID                 BigNumber_DEF NOT NULL,F_LC_STEP            int NOT NULL,F_VERSION_ID         int NOT NULL,F_CHKOUT_BY          BigNumber_DEF NOT NULL,F_OBJECT_VER_TYPE    int NOT NULL,F_OBJECT_TYPE        int NOT NULL,F_OWNER_ID           BigNumber_DEF NOT NULL,F_LEVEL_ID           int NOT NULL,F_GUID               GUID_DEF NULL,CAPTION              String850_DEF,F_OBJ_CREATE        datetime NOT NULL{addFields},F_PROJECT_ID         BigNumber_DEF NOT NULL,F_MODIFICATION_ID    BigNumber_DEF DEFAULT 0 NOT NULL,F_BASE_VERSION       BigNumber_DEF DEFAULT 0 NOT NULL,F_SITE_ID            VARCHAR(10) NULL,F_ACCESS             SmallNumber_DEF DEFAULT 0 NOT NULL,F_CREATOR_ID BigNumber_DEF DEFAULT 0 NOT NULL)");
    db.ExecuteNonQuery($"ALTER TABLE {viewName} ADD PRIMARY KEY CLUSTERED (F_OBJECT_ID)");
    db.ExecuteNonQuery($"ALTER TABLE {viewName} SET(LOCK_ESCALATION = DISABLE)");
    base.CreateObjectTypeView(viewName, addFields, db, indexesList);
  }

  public override void CreateRelationTypeView(
    string viewName,
    string addFields,
    IDbManager db,
    List<string> indexesList)
  {
    db.ExecuteNonQuery($"CREATE TABLE {viewName} (F_PRJLINK_ID         BigNumber_DEF NOT NULL,F_PROJ_ID            BigNumber_DEF NOT NULL,F_PART_ID            BigNumber_DEF NOT NULL,F_RELATION_TYPE      int NOT NULL,F_CREATE_DATE        datetime NOT NULL,F_PRJ_GUID GUID_DEF,F_REL_CREATOR BigNumber_DEF DEFAULT 0 NOT NULL {addFields})");
    db.ExecuteNonQuery($"ALTER TABLE {viewName} ADD PRIMARY KEY CLUSTERED (F_PRJLINK_ID)");
    db.ExecuteNonQuery($"ALTER TABLE {viewName} SET(LOCK_ESCALATION = DISABLE)");
    indexesList.Add(this.GetIndexSQL(viewName, "F_PROJ_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_PART_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_RELATION_TYPE", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_CREATE_DATE", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_PRJ_GUID", SortOrders.ASC));
  }

  public override void CreateObjectsTypeAttrView(string attributesTableName, IDbManager db)
  {
    db.ExecuteNonQuery($"CREATE TABLE {attributesTableName} (F_OBJECT_ID     BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID  int NOT NULL,F_INLIST_ID     int NOT NULL,F_INTEGER_VALUE BigNumber_DEF NULL,F_STRING_VALUE  String850_DEF,F_DOUBLE_VALUE  float NULL,F_DATE_VALUE    datetime NULL)");
    db.ExecuteNonQuery($"ALTER TABLE {attributesTableName} ADD PRIMARY KEY CLUSTERED (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID)");
    db.ExecuteNonQuery($"ALTER TABLE {attributesTableName} SET(LOCK_ESCALATION = DISABLE)");
  }

  public override void CreateObjectsTypeAttrIndexes(
    string attributesTableName,
    IDbManager db,
    bool createValueIndex)
  {
    db.ExecuteNonQuery($"ALTER TABLE {attributesTableName} ADD FOREIGN KEY (F_ATTRIBUTE_ID) REFERENCES IMS_ATTRIBUTES");
    this.CreateIndex(attributesTableName, "F_ATTRIBUTE_ID", db, SortOrders.ASC);
    if (!createValueIndex)
      return;
    this.CreateAttrValuesIndex(attributesTableName, db);
  }

  public override void CreateAttrValuesIndex(string attributesTableName, IDbManager db)
  {
    this.CreateIndex(attributesTableName, "F_INTEGER_VALUE", db, SortOrders.ASC, true);
    this.CreateIndex(attributesTableName, "F_STRING_VALUE", db, SortOrders.ASC, true);
    this.CreateIndex(attributesTableName, "F_DOUBLE_VALUE", db, SortOrders.ASC, true);
    this.CreateIndex(attributesTableName, "F_DATE_VALUE", db, SortOrders.ASC, true);
  }

  public override string GetModifyColumnSQL(string tableName, string fldName, string fldType)
  {
    return $"ALTER TABLE {tableName} ALTER COLUMN {fldName} {fldType}";
  }

  private string GetIndexLastName(string fldName, SortOrders order)
  {
    if (order == SortOrders.DESC)
      return $"_{fldName.ToString()}_DESC";
    if (order == SortOrders.ASC)
      return "_" + fldName.ToString();
    throw new KernelExceptionID(214);
  }

  public override string GetIndexSQL(string tableName, string fldName, SortOrders order)
  {
    return this.GetIndexSQL(tableName, fldName, order, false);
  }

  public string GetIndexSQL(string tableName, string fldName, SortOrders order, bool noNulls)
  {
    string str = !noNulls ? (fldName == "F_VERSION_ID" || fldName == "F_CHKOUT_BY" || fldName == "F_OBJECT_VER_TYPE" || fldName == "F_PROJECT_ID" || fldName == "F_MODIFICATION_ID" ? $" WHERE {fldName} <> 0" : string.Empty) : $" WHERE {fldName} IS NOT NULL";
    return string.Format("CREATE INDEX {0}{3} ON {0} ({1} {2}){4}", (object) tableName, (object) fldName, (object) order.ToString(), (object) this.GetIndexLastName(fldName, order), (object) str);
  }

  public override void CreateIndex(
    string tableName,
    string fldName,
    IDbManager db,
    SortOrders order)
  {
    this.CreateIndex(tableName, fldName, db, order, false);
  }

  public void CreateIndex(
    string tableName,
    string fldName,
    IDbManager db,
    SortOrders order,
    bool noNulls)
  {
    db.ExecuteNonQuery(this.GetIndexSQL(tableName, fldName, order, noNulls));
  }

  public override string GetDropIndexSQL(string tableName, string fldName, SortOrders order)
  {
    return string.Format("DROP INDEX {0}.{0}{1}", (object) tableName, (object) this.GetIndexLastName(fldName, order));
  }

  public override string GetAddColumnsSQL(string tableName, string columns)
  {
    return $"ALTER TABLE {tableName} ADD {columns}";
  }

  public override string GetDropColumnsSQL(string tableName, string columnName)
  {
    return $"ALTER TABLE {tableName} DROP COLUMN {columnName}";
  }

  public override void CreateFileStorage(string storageName, IDbManager db)
  {
    db.ExecuteNonQuery($"CREATE TABLE {storageName} (F_FILE_ID BIGINT NOT NULL, F_FILENAME NVARCHAR(255) NULL, F_FILEBODY IMAGE NULL, F_FILESIZE BIGINT DEFAULT 0 NOT NULL, F_FILEDATE DATETIME NULL, F_ARC_METHOD SMALLINT DEFAULT 0 NOT NULL,F_OBJECTLINK_ID BIGINT NULL, F_ZIPSIZE BIGINT DEFAULT 0 NOT NULL, F_NOTE NVARCHAR(450) NULL, F_ATTRIBUTE_ID INT DEFAULT 0 NOT NULL,F_LINKTYPE INTEGER NOT NULL DEFAULT 0, F_AUTHOR BIGINT NOT NULL DEFAULT 0)");
    db.ExecuteNonQuery($"ALTER TABLE {storageName} ADD PRIMARY KEY CLUSTERED (F_FILE_ID)");
    db.ExecuteNonQuery($"ALTER TABLE {storageName} SET(LOCK_ESCALATION = DISABLE)");
  }

  public override void DropTableIfExists(IDbManager db, string tableName)
  {
    db.ExecuteNonQuery(string.Format("IF OBJECT_ID (N'{0}', N'U') IS NOT NULL DROP TABLE {0}", (object) tableName));
  }

  public override string NVARCHARCast(string fldName, int len, string tablePrefix)
  {
    return string.Format("CAST({2}.{0} AS NVARCHAR({1})) {0}", (object) fldName, (object) len, (object) tablePrefix);
  }

  public override int MaximumINOperands => 2000;

  public override string GetTemporaryTableName(string tableName) => "#" + tableName;

  public override void PrepareRelationsTempTable(IDbManager db)
  {
    try
    {
      db.ExecuteScalar("SELECT TOP 1 F_PRJLINK_ID FROM #TMP_RELATIONS");
    }
    catch
    {
      db.ExecuteNonQuery("CREATE TABLE #TMP_RELATIONS (F_PRJLINK_ID bigint NOT NULL PRIMARY KEY, F_PROJ_ID bigint NOT NULL, F_PART_ID bigint NULL, F_RELATION_TYPE INT NOT NULL, F_CREATE_DATE DATETIME NULL, F_TREE_LEVEL INT NOT NULL)");
    }
  }

  public override string GetEqualEmptyString(string fldName)
  {
    return string.Format("{0} = '' OR {0} IS NULL", (object) fldName);
  }

  private void OnDbConnectionStateChange(object sender, StateChangeEventArgs e)
  {
    if (e.CurrentState != ConnectionState.Open)
      return;
    this.SetTransactionIsolationLevel((IDbConnection) sender);
  }

  private void SetTransactionIsolationLevel(IDbConnection connection)
  {
    try
    {
      using (IDbCommand command = connection.CreateCommand())
      {
        command.CommandText = this.TransisolationLevelSQL;
        command.ExecuteNonQuery();
      }
    }
    catch (Exception ex)
    {
      IEventLogHelper eventLogHelper = DbManagerConfiguration.EventLogHelper;
      if (eventLogHelper == null)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("------------------------------------");
      stringBuilder.AppendLine("Не удалось задать Isolation Level, действующий по умолчанию для SQL-подключения.");
      stringBuilder.AppendFormat("Тип исключения:{0}", (object) ex.GetType()).AppendLine();
      stringBuilder.AppendFormat("Текст исключения:{0}", (object) ex.Message).AppendLine();
      stringBuilder.AppendFormat("Стек:{0}", (object) ex.StackTrace);
      eventLogHelper.AddToTrace(stringBuilder.ToString(), Consts.traceAlways, "sql_errors.log");
    }
  }

  private string TransisolationLevelSQL => this._transisolationLevelSQL;

  public override IsolationLevel DefaultIsolationLevel => this._defaultIsolationLevel;

  public override bool CanStoreEmptyString => true;

  public string[] GetInitCommands()
  {
    return new string[1]{ "SET XACT_ABORT OFF" };
  }

  public override bool SpAutoRollback => true;

  public override void WriteBlob(
    string tableName,
    string fieldName,
    string keyName,
    object keyValue,
    Stream blob,
    IDbManager db,
    long fileSize)
  {
    DbManager dbManager = (DbManager) db;
    object obj = db.ExecuteScalar($"SELECT TEXTPTR({fieldName}) FROM {tableName} WHERE {keyName} = @id", db.Parameter("id", keyValue));
    byte[] numArray1 = obj != null && obj != DBNull.Value ? (byte[]) obj : throw new KernelException("DataProvider.WriteBlob error: record not found.");
    int num1 = fileSize >= (long) Consts.BlobTransferBufferLength ? Consts.BlobTransferBufferLength : (int) fileSize;
    SqlCommand sqlCommand = new SqlCommand($"UPDATETEXT {tableName}.{fieldName} @Pointer @Offset 0 @Bytes", dbManager.GetConnectionAs<SqlConnection>(), dbManager.GetTransactionAs<SqlTransaction>());
    sqlCommand.CommandTimeout = DbManagerConfiguration.NormalCommandTimeout;
    sqlCommand.Parameters.Add("@Pointer", SqlDbType.Binary, 16 /*0x10*/).Value = (object) numArray1;
    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@Bytes", SqlDbType.Image, num1);
    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@Offset", SqlDbType.Int);
    sqlParameter2.Value = (object) 0;
    blob.Position = 0L;
    using (BinaryReader binaryReader = new BinaryReader(blob))
    {
      byte[] numArray2 = binaryReader.ReadBytes(num1);
      long num2 = 0;
      for (; numArray2.Length != 0; numArray2 = binaryReader.ReadBytes(num1))
      {
        sqlParameter1.Value = (object) numArray2;
        sqlCommand.ExecuteNonQuery();
        num2 += (long) num1;
        sqlParameter2.Value = (object) num2;
      }
      binaryReader.Close();
    }
  }

  public override string GetStorageType() => "MS SQL Server";

  public override bool AutoDDLCommit => false;

  public override string NullBlobStr => "NULL";

  public override char[] LIKE_Symbols => SqlDataProvider._ReplacementList;

  public override void RenameTable(string oldName, string newName, IDbManager db)
  {
    db.ExecuteSpNonQuery("sp_rename", db.Parameter("objname", (object) oldName), db.Parameter("newname", (object) newName));
  }

  public override bool CanUpperMemo => false;

  public override string ConcatStringOperator => "+";

  public override string GetIndexSQL(
    string tableName,
    string indxName,
    string fldName,
    SortOrders order)
  {
    return string.Format("CREATE INDEX {3} ON {0} ({1} {2})", (object) tableName, (object) fldName, (object) order.ToString(), (object) indxName);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public override IDbDataParameter GetMemoParameter(string paramName)
  {
    throw new NotSupportedException();
  }

  private void SetIndexes(IDbManager db, bool enable, out string[] errorMessages)
  {
    string[] strArray = new string[3]
    {
      "IMS_",
      "IMV_",
      "STORAGE_"
    };
    List<string> stringList = new List<string>();
    DataTable dataTable = db.ExecuteDataTable("select a.name as index_name, b.name as table_name from sys.indexes a, sys.objects b where a.object_id = b.object_id and b.name like 'IM%' and a.is_primary_key = 0");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      string str = Convert.ToString(dataTable.Rows[index]["index_name"]);
      if (!(str == string.Empty))
      {
        try
        {
          db.ExecuteNonQuery($"alter index {str} on {Convert.ToString(dataTable.Rows[index]["table_name"])} {(enable ? (object) "rebuild" : (object) "disable")}");
        }
        catch (Exception ex)
        {
          stringList.Add(ex.Message);
        }
      }
    }
    errorMessages = stringList.Count > 0 ? stringList.ToArray() : (string[]) null;
  }

  public override void DisableIndexes(IDbManager db, out string[] errorMessages)
  {
    if (this.GetServerVersion(db) < 9)
    {
      errorMessages = new string[1]
      {
        "Текущая версия MSSQL не поддерживается."
      };
    }
    else
    {
      errorMessages = (string[]) null;
      this.SetIndexes(db, false, out errorMessages);
    }
  }

  public override void EnableIndexes(IDbManager db, out string[] errorMessages)
  {
    if (this.GetServerVersion(db) < 9)
    {
      errorMessages = new string[1]
      {
        "Текущая версия MSSQL не поддерживается."
      };
    }
    else
    {
      errorMessages = (string[]) null;
      this.SetIndexes(db, true, out errorMessages);
    }
  }

  private int GetServerVersion(IDbManager db)
  {
    string str = Convert.ToString(db.ExecuteScalar("select serverproperty('productversion')"));
    return Convert.ToInt32(str.Substring(0, str.IndexOf('.')));
  }

  public override void InsertIntoTemporaryTable(
    string tableName,
    DbType d_type,
    long selectKeyValue,
    IDbManager db,
    Array vals)
  {
    if (tableName == "IMS_TMP_INTEGER")
    {
      SqlParameter sqlParameter = new SqlParameter();
      sqlParameter.ParameterName = "@ImportTable";
      sqlParameter.TypeName = "dbo.IMS_TMP_INTEGER_STRUCT";
      sqlParameter.SqlDbType = SqlDbType.Structured;
      sqlParameter.Value = (object) new SqlDataProvider.ImsTmpInteger_StreamingDataRecord(selectKeyValue, (IEnumerable) vals);
      db.ExecuteSpNonQuery("IMS_TMP_INTEGER_TVP", (IDbDataParameter) sqlParameter);
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < vals.Length; ++index)
      {
        string str = d_type != DbType.String ? vals.GetValue(index).ToString() : DataSetProcessor.QString(vals.GetValue(index).ToString());
        stringBuilder.AppendFormat("INSERT INTO {0} VALUES ({1}, {2}) ", (object) tableName, (object) selectKeyValue, (object) str);
      }
      --stringBuilder.Length;
      db.ExecuteNonQuery(stringBuilder.ToString());
    }
  }

  public override void DropIndexIfExists(string indexName, string tableName, IDbManager db)
  {
    try
    {
      string str = this.RDBMSVersion >= 14 ? " IF EXISTS " : string.Empty;
      db.ExecuteNonQuery($"DROP INDEX {str}{indexName} ON {tableName}");
    }
    catch
    {
    }
  }

  public override ExecuteBatchSqlStrategy CreateExecuteBatchSqlStrategy(DbManager dbManager)
  {
    return (ExecuteBatchSqlStrategy) new SqlExecuteBatchSqlStrategy((IDbManager) dbManager);
  }

  [Obsolete("Do not use this method anymore.", true)]
  protected override void ExecuteBatchSQLInternal(
    IDbCommand command,
    DbBatchCommandParameter[] parameters,
    int recCount)
  {
    throw new NotSupportedException();
  }

  private sealed class ImsTmpInteger_StreamingDataRecord : IEnumerable<SqlDataRecord>, IEnumerable
  {
    private long _accessKey;
    private IEnumerable _values;

    public ImsTmpInteger_StreamingDataRecord(long accessKey, IEnumerable values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      this._accessKey = accessKey;
      this._values = values;
    }

    public IEnumerator<SqlDataRecord> GetEnumerator()
    {
      SqlMetaData[] columnStructure = new SqlMetaData[2]
      {
        new SqlMetaData("F_KEY", SqlDbType.BigInt),
        new SqlMetaData("F_VALUE", SqlDbType.BigInt)
      };
      foreach (object obj in this._values)
      {
        SqlDataRecord sqlDataRecord = new SqlDataRecord(columnStructure);
        sqlDataRecord.SetInt64(0, this._accessKey);
        sqlDataRecord.SetInt64(1, Convert.ToInt64(obj));
        yield return sqlDataRecord;
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
