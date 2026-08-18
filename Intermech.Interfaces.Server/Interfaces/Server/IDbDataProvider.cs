// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDbDataProvider
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDbDataProvider
{
  string GetValidateRDBMSVersionMessage();

  bool NoLockMode { get; set; }

  bool IsInitialized { get; }

  void Initialize(IDbManager firstDbManager);

  bool CanUseIndexTablespace { get; }

  string IndexTablespaceName { get; }

  string IndexTablespaceNameSQL { get; }

  void TrySetIndexTablespaceName(string indexTablespaceName);

  IDbConnection CreateConnection(string connectionString = null);

  IDbDataAdapter CreateDataAdapter(IDbConnection connection);

  IDbCommand CreateCommand(IDbConnection connection);

  Exception WrapDbException(Exception exception);

  string UpdateCommandText(CommandType commandType, string commandText);

  string UpdateParameterName(string parameterName);

  object UpdateParameterValue(string name, object val);

  void UpdateParameterTypeByValue(IDbDataParameter parameter, object paramValue);

  Type ConnectionType { get; }

  string Name { get; }

  string Now { get; }

  string Ln { get; }

  string Length(string param);

  string NVARCHARType(int len);

  string DATEType { get; }

  string FLOATType { get; }

  string INTEGERType { get; }

  string SMALLINTType { get; }

  string TEXTType { get; }

  string BLOBType { get; }

  string GetRoundSQL(string arg, int precision);

  string CreateGeneratorString(string generatorName, long startValue, int incrementValue);

  long NextGeneratorValue(string generatorName, IDbManager db);

  string InsertGeneratorValueString(string generatorName);

  long BeforeInsertID(string generatorName, IDbManager db);

  long AfterInsertID(IDbManager db);

  string GetTopRecordsInWhere(int recordsCount);

  string GetTopRecordsInSelect(int recordsCount);

  string GetTopString(int recordsCount);

  string GetRowNumString(int recordsCount);

  string GetFetchSQL(int PacketSize);

  string NullsOrder { get; }

  string GetUTCSelect(string fldName, TimeSpan timeOffset);

  void CreateIndex(string tableName, string fldName, IDbManager db, SortOrders order);

  void CreateFileStorage(string storageName, IDbManager db);

  string NVARCHARCast(string fldName, int len, string tablePrefix);

  int MaximumINOperands { get; }

  string GetTemporaryTableName(string tableName);

  void PrepareRelationsTempTable(IDbManager db);

  string GetEqualEmptyString(string fldName);

  void CreateObjectTypeView(
    string viewName,
    string addFields,
    IDbManager db,
    List<string> indexesList);

  void CreateRelationTypeView(
    string viewName,
    string addFields,
    IDbManager db,
    List<string> indexesList);

  string GetIndexSQL(string tableName, string fldName, SortOrders order);

  string GetIndexSQL(string tableName, string indxName, string fldName, SortOrders order);

  string GetDropIndexSQL(string tableName, string fldName, SortOrders order);

  string GetAddColumnsSQL(string tableName, string columns);

  string GetDropColumnsSQL(string tableName, string columnName);

  string GetModifyColumnSQL(string tableName, string fldName, string fldType);

  IsolationLevel DefaultIsolationLevel { get; }

  bool CanStoreEmptyString { get; }

  bool SpAutoRollback { get; }

  void WriteBlob(
    string tableName,
    string fieldName,
    string keyName,
    object keyValue,
    Stream blob,
    IDbManager db,
    long fileSize);

  string GetStorageType();

  bool AutoDDLCommit { get; }

  string NullBlobStr { get; }

  void RenameTable(string oldName, string newName, IDbManager db);

  bool CanUpperMemo { get; }

  void CreateObjectsTypeAttrView(string attributesTableName, IDbManager db);

  void CreateObjectsTypeAttrIndexes(
    string attributesTableName,
    IDbManager db,
    bool createValueIndex);

  string ConcatStringOperator { get; }

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter GetMemoParameter(string paramName);

  void DisableIndexes(IDbManager db, out string[] errorMessages);

  void EnableIndexes(IDbManager db, out string[] errorMessages);

  char[] LIKE_Symbols { get; }

  int RDBMSVersion { get; }

  string DatabaseName { get; }

  object ConvertScalarValue(object scalarValue);

  string FetchRowsSQL(int PacketSize);

  void CreateAttrValuesIndex(string attributesTableName, IDbManager db);

  void DropAttrValuesIndex(string attributesTableName, IDbManager db);

  DataTable PrepareDataTable(DataTable tbl);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter GetGuidDataParam(IDbManager db, string paramName, Guid paramValue);

  void InsertIntoTemporaryTable(
    string tableName,
    DbType d_type,
    long selectKeyValue,
    IDbManager db,
    Array vals);

  string GetSQL_TimestampField(string fldName, TimedEventKinds timeUnits);

  void CheckTableExists(string tableName, string fldName, IDbManager db);

  void DropTableIfExists(IDbManager db, string tableName);

  void ReadFileBody(
    IDbManager _dbManager,
    FileInfoStruct fileStruct,
    string storageName,
    string fname,
    int maxBufferSize,
    DataRow row);

  void InsertFileBody(IDbManager _dbManager, FileInfoStruct fileStruct, string storageName);

  void DeleteFileBody(IDbManager _dbManager, long fileID, string storageName);

  void CloneFile(
    IDbManager _dbManager,
    string storageName,
    long fromFile,
    long toFileID,
    string newFileName,
    long objectID,
    long userID,
    int attributeID);

  bool IsRecordsExists(
    IDbManager _dbManager,
    string tableName,
    string fldName,
    string whereStr,
    IDbDataParameter[] parameters);

  void DropIndexIfExists(string indexName, string tableName, IDbManager db);

  string GetEscapeSQL(string str_value);

  string GetCollateSQL();

  [Obsolete("Do not use this method anymore.", true)]
  void ExecuteBatchSQL(IDbCommand command, DbBatchCommandParameter[] parameters, int recCount);
}
