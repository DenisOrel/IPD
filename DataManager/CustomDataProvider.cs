// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.CustomDataProvider
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Threading;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Server.Data;

public abstract class CustomDataProvider : IDbDataProvider
{
  private AtomicInt32 _RDBMSVersion = new AtomicInt32(0);
  private AtomicRef<string> _DatabaseName = new AtomicRef<string>(string.Empty);
  private AtomicRef<string> _DatabaseCollate = new AtomicRef<string>(string.Empty);
  private AtomicRef<string> _IndexTablespaceName = new AtomicRef<string>(string.Empty);
  private AtomicBoolean _NoLockMode = new AtomicBoolean(false);
  private AtomicBoolean _IsInitialized = new AtomicBoolean(false);

  public abstract string Name { get; }

  public abstract Type ConnectionType { get; }

  public abstract IDbConnection CreateConnection(string connectionString = null);

  public abstract IDbDataAdapter CreateDataAdapter(IDbConnection connection);

  public abstract IDbCommand CreateCommand(IDbConnection connection);

  public bool IsInitialized
  {
    [DebuggerStepThrough] get => this._IsInitialized.Value;
    [DebuggerStepThrough] private set => this._IsInitialized.Value = value;
  }

  public void Initialize(IDbManager firstDbManager)
  {
    if (firstDbManager == null)
      throw new ArgumentNullException(nameof (firstDbManager));
    lock (this)
    {
      if (this.IsInitialized)
        return;
      this.InitAll(firstDbManager);
      this.IsInitialized = true;
    }
  }

  protected virtual void InitAll(IDbManager firstDbManager)
  {
    this.InitRDBMSVersion(firstDbManager);
  }

  public abstract string Now { get; }

  public abstract string Ln { get; }

  public virtual string GetCollateSQL()
  {
    return this.DatabaseCollate != string.Empty ? "collate " + this.DatabaseCollate : string.Empty;
  }

  public virtual string GetEscapeSQL(string str_value) => string.Empty;

  public virtual bool CanUseIndexTablespace
  {
    [DebuggerStepThrough] get => true;
  }

  public string IndexTablespaceName
  {
    [DebuggerStepThrough] get => this._IndexTablespaceName.Value;
    [DebuggerStepThrough] protected set
    {
      this._IndexTablespaceName.Value = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public virtual string IndexTablespaceNameSQL
  {
    get
    {
      return !this.CanUseIndexTablespace || !(this.IndexTablespaceName != string.Empty) ? string.Empty : " TABLESPACE " + this.IndexTablespaceName;
    }
  }

  public virtual Exception WrapDbException(Exception exception)
  {
    return exception == null ? (Exception) null : new Exception(exception.Message);
  }

  public bool NoLockMode
  {
    [DebuggerStepThrough] get => this._NoLockMode.Value;
    [DebuggerStepThrough] set => this._NoLockMode.Value = value;
  }

  public virtual void InitRDBMSVersion(IDbManager firstDbManager)
  {
  }

  public virtual string GetValidateRDBMSVersionMessage()
  {
    string rdbmsVersionMessage = string.Empty;
    if (this.MinimalRDBMSVersion != 0 && this.RDBMSVersion != 0 && this.RDBMSVersion < this.MinimalRDBMSVersion)
      rdbmsVersionMessage = $"Используемая версия базы данных {this.Name} устарела, поэтому система может функционировать некорректно.{$" Рекомендуем обновить СУБД. Текущая версия {this.RDBMSVersion}, требуется версия не ниже {this.MinimalRDBMSVersion}."}";
    return rdbmsVersionMessage;
  }

  protected virtual int MinimalRDBMSVersion
  {
    [DebuggerStepThrough] get => 0;
  }

  public void TrySetIndexTablespaceName(string tablespaceName)
  {
    if (string.IsNullOrEmpty(tablespaceName))
      throw new ArgumentException("The index tablespace name is null or empty.", nameof (tablespaceName));
    if (!this.CanUseIndexTablespace)
      return;
    this.DoSetIndexTablespaceName(tablespaceName);
  }

  protected virtual void DoSetIndexTablespaceName(string tablespaceName)
  {
    this.IndexTablespaceName = tablespaceName;
  }

  public abstract void InsertIntoTemporaryTable(
    string tableName,
    DbType d_type,
    long selectKeyValue,
    IDbManager db,
    Array vals);

  [Obsolete("Do not use this method anymore.", true)]
  public void ExecuteBatchSQL(
    IDbCommand command,
    DbBatchCommandParameter[] parameters,
    int recCount)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  protected virtual void ExecuteBatchSQLInternal(
    IDbCommand command,
    DbBatchCommandParameter[] parameters,
    int recCount)
  {
    throw new NotSupportedException();
  }

  public virtual string UpdateConnectionString(string connectionString) => connectionString;

  public virtual string UpdateCommandText(CommandType commandType, string commandText)
  {
    return commandText;
  }

  public abstract void UpdateParameterTypeByValue(IDbDataParameter parameter, object paramValue);

  public abstract string UpdateParameterName(string parameterName);

  public abstract object UpdateParameterValue(string name, object val);

  public virtual void CreateAttrValuesIndex(string attributesTableName, IDbManager db)
  {
    this.CreateIndex(attributesTableName, "F_INTEGER_VALUE", db, SortOrders.ASC);
    this.CreateIndex(attributesTableName, "F_STRING_VALUE", db, SortOrders.ASC);
    this.CreateIndex(attributesTableName, "F_DOUBLE_VALUE", db, SortOrders.ASC);
    this.CreateIndex(attributesTableName, "F_DATE_VALUE", db, SortOrders.ASC);
  }

  public virtual void DropAttrValuesIndex(string attributesTableName, IDbManager db)
  {
    try
    {
      db.ExecuteNonQuery(this.GetDropIndexSQL(attributesTableName, "F_INTEGER_VALUE", SortOrders.ASC));
    }
    catch
    {
    }
    try
    {
      db.ExecuteNonQuery(this.GetDropIndexSQL(attributesTableName, "F_STRING_VALUE", SortOrders.ASC));
    }
    catch
    {
    }
    try
    {
      db.ExecuteNonQuery(this.GetDropIndexSQL(attributesTableName, "F_DOUBLE_VALUE", SortOrders.ASC));
    }
    catch
    {
    }
    try
    {
      db.ExecuteNonQuery(this.GetDropIndexSQL(attributesTableName, "F_DATE_VALUE", SortOrders.ASC));
    }
    catch
    {
    }
  }

  public abstract void CreateIndex(
    string tableName,
    string fldName,
    IDbManager db,
    SortOrders order);

  public abstract string GetDropIndexSQL(string tableName, string fldName, SortOrders order);

  public abstract string GetIndexSQL(string tableName, string fldName, SortOrders order);

  public virtual string GetRoundSQL(string arg, int precision) => $"ROUND({arg}, {precision})";

  public int RDBMSVersion
  {
    [DebuggerStepThrough] get => this._RDBMSVersion.Value;
    [DebuggerStepThrough] protected set => this._RDBMSVersion.Value = value;
  }

  public string DatabaseName
  {
    [DebuggerStepThrough] get => this._DatabaseName.Value;
    [DebuggerStepThrough] protected set => this._DatabaseName.Value = value;
  }

  public string DatabaseCollate
  {
    [DebuggerStepThrough] get => this._DatabaseCollate.Value;
    [DebuggerStepThrough] protected set => this._DatabaseCollate.Value = value;
  }

  public abstract string DATEType { get; }

  public abstract string FLOATType { get; }

  public abstract string INTEGERType { get; }

  public abstract string SMALLINTType { get; }

  public abstract string TEXTType { get; }

  public abstract string BLOBType { get; }

  public abstract string NullsOrder { get; }

  public abstract int MaximumINOperands { get; }

  public virtual IsolationLevel DefaultIsolationLevel => IsolationLevel.ReadCommitted;

  public abstract bool CanStoreEmptyString { get; }

  public abstract bool SpAutoRollback { get; }

  public abstract bool AutoDDLCommit { get; }

  public abstract string NullBlobStr { get; }

  public abstract bool CanUpperMemo { get; }

  public abstract string ConcatStringOperator { get; }

  public abstract char[] LIKE_Symbols { get; }

  public virtual object ConvertScalarValue(object scalarValue) => scalarValue;

  protected string UpdateUnicodeLiterals(string commandText)
  {
    return CustomDataProvider.StringUpdater.Update(commandText);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public virtual IDbDataParameter GetGuidDataParam(
    IDbManager db,
    string paramName,
    Guid paramValue)
  {
    throw new NotSupportedException();
  }

  public virtual string GetSQL_TimestampField(string fldName, TimedEventKinds timeUnits) => fldName;

  public virtual DataTable PrepareDataTable(DataTable tbl) => tbl;

  public virtual ExecuteSpStrategy CreateExecuteSpStrategy(DbManager dbManager)
  {
    return (ExecuteSpStrategy) new DefaultExecuteSpStrategy((IDbManager) dbManager);
  }

  public virtual ExecuteBatchSqlStrategy CreateExecuteBatchSqlStrategy(DbManager dbManager)
  {
    return (ExecuteBatchSqlStrategy) new DefaultExecuteBatchSqlStrategy((IDbManager) dbManager);
  }

  public virtual void CheckTableExists(string tableName, string fldName, IDbManager db)
  {
    db.ExecuteDataTable(string.Format("SELECT {0} FROM {1} WHERE {0} = -1", (object) fldName, (object) tableName));
  }

  public virtual void DropTableIfExists(IDbManager db, string tableName)
  {
    try
    {
      db.ExecuteNonQuery("DROP TABLE " + tableName);
    }
    catch
    {
    }
  }

  protected string InitFileBody(FileInfoStruct fileStruct, string fname)
  {
    try
    {
      fileStruct.FileBody = (Stream) new FileStream(fname, File.Exists(fname) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read);
    }
    catch
    {
      IAppServerFilesCache service = ServerServices.GetService(typeof (IAppServerFilesCache)) as IAppServerFilesCache;
      fname = fileStruct.GetIsolatedFileName(service.FStorage, true);
      fileStruct.FileBody = (Stream) new FileStream(fname, File.Exists(fname) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read);
    }
    return fname;
  }

  public abstract void WriteBlob(
    string tableName,
    string fieldName,
    string keyName,
    object keyValue,
    Stream blob,
    IDbManager db,
    long fileSize);

  public virtual void DeleteFileBody(IDbManager _dbManager, long fileID, string storageName)
  {
  }

  public virtual void InsertFileBody(
    IDbManager _dbManager,
    FileInfoStruct fileStruct,
    string storageName)
  {
    _dbManager.ExecuteNonQuery($"INSERT INTO {storageName} (F_FILE_ID, F_FILENAME, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, F_OBJECTLINK_ID, F_NOTE, F_ATTRIBUTE_ID, F_AUTHOR, F_LINKTYPE, F_FILEBODY) VALUES (:fID, :fname, :fsize, :fdate, :arc, :zipsize, :objID, :notes, :attrID, :authr, :linktype, {_dbManager.DataProvider.NullBlobStr})", _dbManager.Parameter("fID", (object) fileStruct.FileID), _dbManager.Parameter("fname", (object) fileStruct.FileName), _dbManager.Parameter("fsize", (object) fileStruct.RealFileSize), _dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), _dbManager.Parameter("arc", (object) Convert.ToInt32((object) fileStruct.ArcMethod)), _dbManager.Parameter("zipsize", (object) fileStruct.PacketFileSize), _dbManager.Parameter("objID", (object) fileStruct.ObjectLinkID), _dbManager.Parameter("notes", (object) fileStruct.Note), _dbManager.Parameter("attrID", (object) fileStruct.AttributeID), _dbManager.Parameter("authr", (object) fileStruct.Author), _dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
    _dbManager.ExecuteNonQuery($"UPDATE {storageName} SET F_FILEBODY = {_dbManager.DataProvider.NullBlobStr} WHERE F_FILE_ID = :fID", _dbManager.Parameter("fID", (object) fileStruct.FileID));
    this.WriteBlob(storageName, "F_FILEBODY", "F_FILE_ID", (object) fileStruct.FileID, fileStruct.FileBody, _dbManager, fileStruct.PacketFileSize);
  }

  public virtual void CloneFile(
    IDbManager _dbManager,
    string storageName,
    long fromFile,
    long toFileID,
    string newFileName,
    long objectID,
    long userID,
    int attributeID)
  {
    _dbManager.ExecuteNonQuery(string.Format("INSERT INTO {0} (F_FILE_ID, F_FILENAME, F_FILEBODY, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, F_OBJECTLINK_ID, F_NOTE, F_ATTRIBUTE_ID, F_AUTHOR, F_LINKTYPE) SELECT :toFileID, :newFileName, F_FILEBODY, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, :objID, F_NOTE, :attrID, :userID1, F_LINKTYPE FROM {0} WHERE F_FILE_ID = :fromFileID", (object) storageName), _dbManager.Parameter(nameof (toFileID), (object) toFileID), _dbManager.Parameter("fromFileID", (object) fromFile), _dbManager.Parameter(nameof (newFileName), (object) newFileName), _dbManager.Parameter("objID", (object) objectID), _dbManager.Parameter("userID1", (object) userID), _dbManager.Parameter("attrID", (object) attributeID));
  }

  private void ReadFileBodyFunc(IDataReader reader, ExecuteReaderArgs args)
  {
    Tuple<FileInfoStruct, string, int> inputParam = args.InputParam as Tuple<FileInfoStruct, string, int>;
    IDataRecord dataRecord = (IDataRecord) reader;
    try
    {
      (dataRecord as IDataReader).Read();
      if ((dataRecord as IDataReader).IsDBNull(0))
        throw new KernelExceptionID(219, (object) inputParam.Item1.FileID, (object) inputParam.Item1.ObjectLinkID.ToString());
      string path = this.InitFileBody(inputParam.Item1, inputParam.Item2);
      BinaryWriter binaryWriter = new BinaryWriter(inputParam.Item1.FileBody);
      try
      {
        long fieldOffset = 0;
        int packetFileSize = inputParam.Item3;
        if (inputParam.Item1.PacketFileSize < (long) inputParam.Item3)
          packetFileSize = (int) inputParam.Item1.PacketFileSize;
        byte[] buffer = new byte[packetFileSize];
        long bytes;
        for (bytes = dataRecord.GetBytes(0, fieldOffset, buffer, 0, packetFileSize); bytes == (long) packetFileSize; bytes = dataRecord.GetBytes(0, fieldOffset, buffer, 0, packetFileSize))
        {
          binaryWriter.Write(buffer);
          binaryWriter.Flush();
          fieldOffset += (long) packetFileSize;
        }
        if (bytes > 0L)
        {
          binaryWriter.Write(buffer, 0, (int) bytes);
          binaryWriter.Flush();
        }
      }
      finally
      {
        binaryWriter.Close();
      }
      inputParam.Item1.FileBody = (Stream) new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
    }
    finally
    {
      (dataRecord as IDataReader).Close();
    }
  }

  public virtual void ReadFileBody(
    IDbManager _dbManager,
    FileInfoStruct fileStruct,
    string storageName,
    string fname,
    int maxBufferSize,
    DataRow row)
  {
    ExecuteReaderArgs args = new ExecuteReaderArgs((object) new Tuple<FileInfoStruct, string, int>(fileStruct, fname, maxBufferSize));
    _dbManager.ExecuteReader($"SELECT F_FILEBODY FROM {storageName} WHERE F_FILE_ID = :fID", new ExecuteReaderDelegate(this.ReadFileBodyFunc), args, _dbManager.Parameter("fID", (object) fileStruct.FileID));
  }

  public abstract string GetTopString(int recordsCount);

  public abstract string GetFetchSQL(int PacketSize);

  public virtual bool IsRecordsExists(
    IDbManager _dbManager,
    string tableName,
    string fldName,
    string whereStr,
    IDbDataParameter[] parameters)
  {
    return _dbManager.ExecuteDataTable($"SELECT {this.GetTopString(1)} {fldName} FROM {tableName} WHERE {whereStr} {this.GetFetchSQL(1)}", parameters).Rows.Count > 0;
  }

  public virtual void CreateObjectTypeView(
    string viewName,
    string addFields,
    IDbManager db,
    List<string> indexesList)
  {
    indexesList.Add(this.GetIndexSQL(viewName, "F_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_LC_STEP", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_VERSION_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_CHKOUT_BY", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_OBJECT_VER_TYPE", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_OBJECT_TYPE", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_OWNER_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_LEVEL_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "CAPTION", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_GUID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_OBJ_CREATE", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_PROJECT_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_MODIFICATION_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_BASE_VERSION", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_ACCESS", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_CREATOR_ID", SortOrders.ASC));
  }

  public virtual void CreateRelationTypeView(
    string viewName,
    string addFields,
    IDbManager db,
    List<string> indexesList)
  {
    indexesList.Add(this.GetIndexSQL(viewName, "F_PROJ_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_PART_ID", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_RELATION_TYPE", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_CREATE_DATE", SortOrders.ASC));
    indexesList.Add(this.GetIndexSQL(viewName, "F_PRJ_GUID", SortOrders.ASC));
  }

  public abstract void DropIndexIfExists(string indexName, string tableName, IDbManager db);

  public abstract string Length(string param);

  public abstract string NVARCHARType(int len);

  public abstract string CreateGeneratorString(
    string generatorName,
    long startValue,
    int incrementValue);

  public abstract long NextGeneratorValue(string generatorName, IDbManager db);

  public abstract string InsertGeneratorValueString(string generatorName);

  public abstract long BeforeInsertID(string generatorName, IDbManager db);

  public abstract long AfterInsertID(IDbManager db);

  public abstract string GetTopRecordsInWhere(int recordsCount);

  public abstract string GetTopRecordsInSelect(int recordsCount);

  public abstract string GetRowNumString(int recordsCount);

  public abstract string GetUTCSelect(string fldName, TimeSpan timeOffset);

  public abstract void CreateFileStorage(string storageName, IDbManager db);

  public abstract string NVARCHARCast(string fldName, int len, string tablePrefix);

  public abstract string GetTemporaryTableName(string tableName);

  public abstract void PrepareRelationsTempTable(IDbManager db);

  public abstract string GetEqualEmptyString(string fldName);

  public abstract string GetIndexSQL(
    string tableName,
    string indxName,
    string fldName,
    SortOrders order);

  public abstract string GetAddColumnsSQL(string tableName, string columns);

  public abstract string GetDropColumnsSQL(string tableName, string columnName);

  public abstract string GetModifyColumnSQL(string tableName, string fldName, string fldType);

  public abstract string GetStorageType();

  public abstract void RenameTable(string oldName, string newName, IDbManager db);

  public abstract void CreateObjectsTypeAttrView(string attributesTableName, IDbManager db);

  public abstract void CreateObjectsTypeAttrIndexes(
    string attributesTableName,
    IDbManager db,
    bool createValueIndex);

  [Obsolete("Do not use this method anymore.", true)]
  public abstract IDbDataParameter GetMemoParameter(string paramName);

  public abstract void DisableIndexes(IDbManager db, out string[] errorMessages);

  public abstract void EnableIndexes(IDbManager db, out string[] errorMessages);

  public abstract string FetchRowsSQL(int PacketSize);

  private sealed class StringUpdater
  {
    private int _pos;
    private int _start;
    private char[] _text;
    private StringBuilder _sb;
    private const char QUOTE = '\'';

    public static string Update(string text)
    {
      return text == null || text.IndexOf('\'') == -1 ? text : new CustomDataProvider.StringUpdater(text).Text;
    }

    public StringUpdater(string text)
    {
      this._pos = 0;
      this._start = 0;
      this._text = text.ToCharArray();
      this._sb = new StringBuilder(this._text.Length);
      this.Scan();
    }

    private void Scan()
    {
      while (this._pos < this._text.Length)
      {
        this._start = this._pos;
        char ch = this._text[this._pos++];
        if (ch == '\'')
        {
          this.ScanString('\'');
          string str = new string(this._text, this._start + 1, this._pos - this._start - 2);
          if (this.LastChar > char.MinValue && this.LastChar != 'N')
            this._sb.Append("N");
          this._sb.Append('\'');
          this._sb.Append(str);
          this._sb.Append('\'');
        }
        else
          this._sb.Append(ch);
      }
    }

    private void ScanString(char escape)
    {
      char[] text = this._text;
      while (this._pos < text.Length)
      {
        char ch = text[this._pos++];
        if ((int) ch == (int) escape && this._pos < text.Length && (int) text[this._pos] == (int) escape)
          ++this._pos;
        else if ((int) ch == (int) escape)
          break;
      }
      if (this._pos > text.Length)
        throw new Exception($"Invalid string {new string(text, this._start, this._pos - 1 - this._start)}.");
    }

    internal char LastChar => this._sb.Length > 0 ? this._sb[this._sb.Length - 1] : char.MinValue;

    public string Text => this._sb.ToString();
  }
}
