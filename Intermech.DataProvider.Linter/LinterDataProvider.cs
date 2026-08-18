// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.LinterDataProvider
// Assembly: Intermech.DataProvider.Linter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5976CE7B-8000-4C30-A078-1BBCAD6EB006
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.DataProvider.Linter.dll

using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.LinterClient;
using System.IO;

#nullable disable
namespace Intermech.Server.Data;

public sealed class LinterDataProvider : CustomDataProvider
{
  private static readonly char[] _ReplacementList = new char[2]
  {
    '_',
    '%'
  };

  public override char[] LIKE_Symbols => LinterDataProvider._ReplacementList;

  public override void InsertIntoTemporaryTable(
    string tableName,
    DbType d_type,
    long selectKeyValue,
    IDbManager db,
    Array vals)
  {
    throw new NotImplementedException();
  }

  public override IDbConnection CreateConnection(string connectionString)
  {
    LinterDbConnection connection = new LinterDbConnection();
    if (connectionString != null)
      connection.ConnectionString = this.UpdateConnectionString(connectionString);
    return (IDbConnection) connection;
  }

  public override IDbDataAdapter CreateDataAdapter(IDbConnection connection)
  {
    if (connection == null)
      throw new ArgumentNullException(nameof (connection));
    return (IDbDataAdapter) new LinterDbDataAdapter();
  }

  public override IDbCommand CreateCommand(IDbConnection connection)
  {
    return connection != null ? connection.CreateCommand() : throw new ArgumentNullException(nameof (connection));
  }

  public override void DropIndexIfExists(string indexName, string tableName, IDbManager db)
  {
    try
    {
      db.ExecuteNonQuery($"DROP INDEX {indexName}");
    }
    catch
    {
    }
  }

  public override string UpdateCommandText(CommandType commandType, string commandText)
  {
    return commandType != CommandType.Text ? commandText : this.UpdateUnicodeLiterals(commandText);
  }

  public override object ConvertScalarValue(object scalarValue)
  {
    return scalarValue is bool && !Convert.ToBoolean(scalarValue) ? (object) DBNull.Value : scalarValue;
  }

  public override void UpdateParameterTypeByValue(IDbDataParameter parameter, object paramValue)
  {
    if (!(paramValue is string))
      return;
    (parameter as LinterDbParameter).LinterDbType = ELinterDbType.NVarChar;
  }

  public override string UpdateParameterName(string parameterName) => parameterName;

  public override object UpdateParameterValue(string name, object val)
  {
    switch (val)
    {
      case null:
        return (object) DBNull.Value;
      case byte[] numArray when numArray.Length == 0:
        return (object) DBNull.Value;
      case bool flag:
        return (object) (flag ? 1 : 0);
      case Guid _:
        return (object) val.ToString();
      case string _ when val.ToString() == string.Empty:
        return (object) DBNull.Value;
      default:
        return val;
    }
  }

  public override Type ConnectionType => typeof (LinterDbConnection);

  public override string Name => "Linter";

  public override string Now => "SYSDATE";

  public override string Ln => "LN";

  public override string Length(string param) => $"LENGTH({param})";

  public override string NVARCHARType(int len) => $"NVARCHAR({len})";

  public override string DATEType => "DATE";

  public override string FLOATType => "FLOAT";

  public override string INTEGERType => "BIGINT";

  public override string SMALLINTType => "SMALLINT";

  public override string TEXTType => "TEXT";

  public override string BLOBType => "BLOB";

  public override string CreateGeneratorString(
    string generatorName,
    long startValue,
    int incrementValue)
  {
    return $"CREATE SEQUENCE {generatorName} START WITH {startValue} INCREMENT BY {incrementValue} NOMAXVALUE";
  }

  public override long NextGeneratorValue(string generatorName, IDbManager db)
  {
    return Convert.ToInt64(db.ExecuteScalar($"SELECT {generatorName}.NEXTVAL"));
  }

  public override string InsertGeneratorValueString(string generatorName)
  {
    return generatorName + ".NEXTVAL";
  }

  public override long BeforeInsertID(string generatorName, IDbManager db)
  {
    return this.NextGeneratorValue(generatorName, db);
  }

  public override long AfterInsertID(IDbManager db) => 0;

  public override string GetTopRecordsInWhere(int recordsCount) => string.Empty;

  public override string GetTopRecordsInSelect(int recordsCount) => string.Empty;

  public override string GetTopString(int recordsCount) => string.Empty;

  public override string GetRowNumString(int recordsCount) => string.Empty;

  public override string NullsOrder => "NULLS FIRST";

  public override string GetFetchSQL(int PacketSize) => $" LIMIT {PacketSize}";

  public override string FetchRowsSQL(int PacketSize) => this.GetFetchSQL(PacketSize);

  public override string GetUTCSelect(string fldName, TimeSpan timeOffset)
  {
    string str = timeOffset.TotalMinutes >= 0.0 ? "+" : "-";
    return $"{fldName} {str} {Convert.ToInt32(timeOffset.Duration().TotalMinutes)}/1440";
  }

  public override void CreateObjectTypeView(
    string viewName,
    string addFields,
    IDbManager db,
    List<string> indexesList)
  {
    db.ExecuteNonQuery($"CREATE TABLE {viewName} (F_OBJECT_ID          BIGINT NOT NULL,F_ID                 BIGINT NOT NULL,F_LC_STEP            INTEGER NOT NULL,F_VERSION_ID         INTEGER NOT NULL,F_CHKOUT_BY          BIGINT NOT NULL,F_OBJECT_VER_TYPE    INTEGER NOT NULL,F_OBJECT_TYPE        INTEGER NOT NULL,F_OWNER_ID           BIGINT NOT NULL,F_LEVEL_ID           INTEGER NOT NULL,F_GUID               VARCHAR(40) NULL,CAPTION              NVARCHAR(450),F_OBJ_CREATE         DATE NOT NULL{addFields},F_PROJECT_ID         BIGINT NOT NULL,F_MODIFICATION_ID    BIGINT DEFAULT 0 NOT NULL,F_BASE_VERSION       INTEGER DEFAULT 0 NOT NULL,F_SITE_ID            VARCHAR(10) NULL,F_ACCESS             SMALLINT DEFAULT 0 NOT NULL)");
    db.ExecuteNonQuery($"ALTER TABLE {viewName} ADD PRIMARY KEY (F_OBJECT_ID)");
    base.CreateObjectTypeView(viewName, addFields, db, indexesList);
  }

  public override void CreateRelationTypeView(
    string viewName,
    string addFields,
    IDbManager db,
    List<string> indexesList)
  {
    db.ExecuteNonQuery($"CREATE TABLE {viewName} (F_PRJLINK_ID         BIGINT NOT NULL,F_PROJ_ID            BIGINT NOT NULL,F_PART_ID            BIGINT NOT NULL,F_RELATION_TYPE      INTEGER NOT NULL,F_CREATE_DATE        DATE NOT NULL,F_PRJ_GUID VARCHAR(40) NOT NULL{addFields})");
    db.ExecuteNonQuery($"ALTER TABLE {viewName} ADD PRIMARY KEY (F_PRJLINK_ID)");
    base.CreateRelationTypeView(viewName, addFields, db, indexesList);
  }

  public override void CreateObjectsTypeAttrView(string attributesTableName, IDbManager db)
  {
    db.ExecuteNonQuery($"CREATE TABLE {attributesTableName} (F_OBJECT_ID     BIGINT NOT NULL,F_ATTRIBUTE_ID  INTEGER NOT NULL,F_INLIST_ID     INTEGER NOT NULL,F_INTEGER_VALUE BIGINT NULL,F_STRING_VALUE  NVARCHAR(450) NULL,F_DOUBLE_VALUE  float NULL,F_DATE_VALUE    date NULL)");
    db.ExecuteNonQuery($"ALTER TABLE {attributesTableName} ADD  PRIMARY KEY (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID) ");
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

  public override string GetModifyColumnSQL(string tableName, string fldName, string fldType)
  {
    throw new KernelException("Not supported!");
  }

  private string GetIndexLastName(string fldName, SortOrders order, int tableLen)
  {
    string indexLastName;
    if (order == SortOrders.DESC)
    {
      indexLastName = $"_{fldName.ToString()}_D";
    }
    else
    {
      if (order != SortOrders.ASC)
        throw new KernelExceptionID(214);
      indexLastName = "_" + fldName.ToString();
    }
    if (indexLastName.Length + tableLen > 30)
      indexLastName = indexLastName.Substring(0, 30 - tableLen);
    return indexLastName;
  }

  public override string GetIndexSQL(string tableName, string fldName, SortOrders order)
  {
    return string.Format("CREATE INDEX {0}{2} ON {0} ({1})", (object) tableName, (object) fldName, (object) this.GetIndexLastName(fldName, order, tableName.Length));
  }

  public override string GetDropIndexSQL(string tableName, string fldName, SortOrders order)
  {
    return $"DROP INDEX {this.GetIndexLastName(fldName, order, tableName.Length)} on {tableName}";
  }

  public override string GetAddColumnsSQL(string tableName, string columns)
  {
    return $"ALTER TABLE {tableName} ADD COLUMN {columns}";
  }

  public override string GetDropColumnsSQL(string tableName, string columnName)
  {
    return $"ALTER TABLE {tableName} DROP COLUMN {columnName}";
  }

  public override void CreateIndex(
    string tableName,
    string fldName,
    IDbManager db,
    SortOrders order)
  {
    db.ExecuteNonQuery(this.GetIndexSQL(tableName, fldName, order));
  }

  public override void CreateFileStorage(string storageName, IDbManager db)
  {
    db.ExecuteNonQuery($"CREATE TABLE {storageName} (F_FILE_ID BIGINT NOT NULL, F_FILENAME NVARCHAR(255) NULL, F_FILEBODY BLOB NULL, F_FILESIZE BIGINT DEFAULT 0 NOT NULL, F_FILEDATE DATE NULL, F_ARC_METHOD SMALLINT DEFAULT 0 NOT NULL,F_OBJECTLINK_ID BIGINT NULL, F_ZIPSIZE BIGINT DEFAULT 0 NOT NULL, F_NOTE NVARCHAR(450) NULL, F_ATTRIBUTE_ID INTEGER DEFAULT 0 NOT NULL, F_LINKTYPE INTEGER DEFAULT 0 NOT NULL, F_AUTHOR BIGINT DEFAULT 0 NOT NULL)");
    db.ExecuteNonQuery($"ALTER TABLE {storageName} ADD PRIMARY KEY (F_FILE_ID)");
  }

  public override string NVARCHARCast(string fldName, int len, string tablePrefix)
  {
    return string.Format("CAST {2}.{0} AS NVARCHAR({1})", (object) fldName, (object) len, (object) tablePrefix);
  }

  public override int MaximumINOperands => 500;

  public override string GetTemporaryTableName(string tableName) => tableName;

  public override void PrepareRelationsTempTable(IDbManager db)
  {
  }

  public override string GetEqualEmptyString(string fldName) => fldName + " IS NULL";

  public override bool CanStoreEmptyString => false;

  public override bool SpAutoRollback => false;

  public override string NullBlobStr => "NULL";

  public override void WriteBlob(
    string tableName,
    string fieldName,
    string keyName,
    object keyValue,
    Stream blob1,
    IDbManager db,
    long fileSize)
  {
    DbManager dbManager = (DbManager) db;
    LinterDbCommand linterDbCommand = new LinterDbCommand(string.Format("SELECT {0}, {2} FROM {1} WHERE {2} = :id FOR UPDATE", (object) fieldName, (object) tableName, (object) keyName), dbManager.GetConnectionAs<LinterDbConnection>() ?? throw new KernelException("linter_connect == null"), dbManager.GetTransactionAs<LinterDbTransaction>());
    linterDbCommand.Parameters.Add((object) db.Parameter("id", keyValue));
    LinterDbDataReader linterDbDataReader = linterDbCommand.ExecuteReader();
    using (linterDbDataReader)
    {
      if (!linterDbDataReader.Read())
        return;
      LinterBlob linterBlobForUpdate = linterDbDataReader.GetLinterBlobForUpdate(0);
      linterBlobForUpdate.Clear();
      blob1.Position = 0L;
      using (BinaryReader binaryReader = new BinaryReader(blob1))
      {
        for (byte[] buffer = binaryReader.ReadBytes(Consts.BlobTransferBufferLength); buffer.Length != 0; buffer = binaryReader.ReadBytes(Consts.BlobTransferBufferLength))
          linterBlobForUpdate.Append(buffer, 0, buffer.Length);
        binaryReader.Close();
      }
    }
  }

  public override string GetStorageType() => "Linter";

  public override bool AutoDDLCommit => true;

  public override void RenameTable(string oldName, string newName, IDbManager db)
  {
    db.ExecuteNonQuery($"ALTER TABLE {oldName} RENAME TO {newName}");
  }

  public override bool CanUpperMemo => true;

  public override string ConcatStringOperator => "||";

  public override string GetIndexSQL(
    string tableName,
    string indxName,
    string fldName,
    SortOrders order)
  {
    return string.Format("CREATE INDEX {2} ON {0} ({1})", (object) tableName, (object) fldName, (object) indxName);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public override IDbDataParameter GetMemoParameter(string paramName)
  {
    throw new NotSupportedException();
  }

  private void SetIndexes(IDbManager db, bool enable, out string[] errorMessages)
  {
    List<string> stringList = new List<string>();
    DataTable dataTable = db.ExecuteDataTable("select t.index_name, t.table_name from sys.user_indexes t where table_name like 'IM%' and t.uniqueness like 'NONUNIQUE' and t.temporary = 'N'");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      string str = Convert.ToString(dataTable.Rows[index]["index_name"]);
      try
      {
        db.ExecuteNonQuery($"alter index {str} {(enable ? (object) "rebuild" : (object) "unusable")}");
      }
      catch (Exception ex)
      {
        stringList.Add($"Ошибка при {(enable ? (object) "включении" : (object) "отключении")} индекса {str} таблицы {Convert.ToString(dataTable.Rows[index]["table_name"])}: {ex.Message}");
      }
    }
    try
    {
      db.ExecuteNonQuery(enable ? "CREATE UNIQUE INDEX IMS_GUID_GUID ON IMS_GUID (F_GUID)" : "DROP INDEX IMS_GUID_GUID");
    }
    catch (Exception ex)
    {
      stringList.Add(ex.Message);
    }
    if (enable)
    {
      try
      {
        db.ExecuteNonQuery("ALTER TABLE IMS_GUID_RESOLVE ADD CONSTRAINT IMS_GUID_RESOLVE_PK PRIMARY KEY (F_GUID)");
      }
      catch (Exception ex)
      {
        stringList.Add(ex.Message);
      }
    }
    else
    {
      object obj = db.ExecuteScalar("select t.index_name from sys.user_indexes t where table_name like 'IMS_GUID_RESOLVE' and t.uniqueness like 'UNIQUE'");
      if (obj != null)
      {
        if (obj != DBNull.Value)
        {
          try
          {
            db.ExecuteNonQuery($"ALTER TABLE IMS_GUID_RESOLVE DROP CONSTRAINT {obj}");
          }
          catch (Exception ex)
          {
            stringList.Add(ex.Message);
          }
        }
      }
    }
    errorMessages = stringList.Count > 0 ? stringList.ToArray() : (string[]) null;
  }

  public override void DisableIndexes(IDbManager db, out string[] errorMessages)
  {
    errorMessages = (string[]) null;
    this.SetIndexes(db, false, out errorMessages);
  }

  public override void EnableIndexes(IDbManager db, out string[] errorMessages)
  {
    errorMessages = (string[]) null;
    this.SetIndexes(db, true, out errorMessages);
  }

  protected class UniqueIndex
  {
    public string Name;
    public string Table;
    public string Field;

    public UniqueIndex(string name, string table, string field)
    {
      this.Name = name;
      this.Table = table;
      this.Field = field;
    }
  }
}
