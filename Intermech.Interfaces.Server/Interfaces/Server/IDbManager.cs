// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDbManager
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDbManager : IDisposable
{
  [Obsolete("Do not use this method anymore.", true)]
  IDbManager AssignParameterValues(DataRow dataRow);

  IDbManager BeginTransaction();

  IDbManager BeginTransaction(IsolationLevel il);

  [Obsolete("Do not use this method anymore.", true)]
  void Close();

  [Obsolete("Do not use this method anymore.", true)]
  void Close(Guid sessionGuid);

  bool Commit();

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter[] CreateParameters(DataRow dataRow, params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  int Execute(CommandType commandType, string commandText, DataSet dataSet);

  [Obsolete("Do not use this method anymore.", true)]
  int Execute(CommandType commandType, string commandText, DataSet dataSet, string tableName);

  [Obsolete("Do not use this method anymore.", true)]
  int Execute(CommandType commandType, string commandText, DataTable table);

  [Obsolete("Do not use this method anymore.", true)]
  int Execute(string commandText, DataSet dataSet);

  [Obsolete("Do not use this method anymore.", true)]
  int Execute(string commandText, DataTable table);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(CommandType commandType, string commandText);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(DataSet dataSet, CommandType commandType, string commandText);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(
    DataSet dataSet,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  DataSet ExecuteDataSet(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName,
    string commandText,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSetArr(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName,
    string commandText,
    IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(DataSet dataSet, string commandText);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(
    DataSet dataSet,
    string tableName,
    CommandType commandType,
    string commandText);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(
    DataSet dataSet,
    string commandText,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(DataSet dataSet, string tableName, string commandText);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(
    DataSet dataSet,
    string tableName,
    string commandText,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(string commandText);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(string tableName, CommandType commandType, string commandText);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(
    string tableName,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(string commandText, params IDbDataParameter[] commandParameters);

  DataSet ExecuteDataSet(string tableName, string commandText);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteDataSet(
    string tableName,
    string commandText,
    params IDbDataParameter[] commandParameters);

  DataTable ExecuteDataTable(CommandType commandType, string commandText);

  DataTable ExecuteDataTable(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  DataTable ExecuteDataTable(DataTable dataTable, CommandType commandType, string commandText);

  DataTable ExecuteDataTable(
    DataTable dataTable,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  DataTable ExecuteDataTable(DataTable dataTable, string commandText);

  DataTable ExecuteDataTable(
    DataTable dataTable,
    string commandText,
    params IDbDataParameter[] commandParameters);

  DataTable ExecuteDataTable(string commandText);

  DataTable ExecuteDataTable(string commandText, params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataTable ExecuteDataTableArr(string commandText, IDbDataParameter[] commandParameters);

  int ExecuteNonQuery(CommandType commandType, string commandText);

  void SetAdminCommandTimeout();

  void SetNormalCommandTimeout();

  [Obsolete("Do not use this method anymore.", true)]
  int ExecuteNonQueryArr(string commandText, IDbDataParameter[] commandParameters);

  int ExecuteNonQuery(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  int ExecuteNonQuery(string commandText);

  int ExecuteNonQuery(string commandText, params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecutePreparedDataSet();

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecutePreparedDataSet(DataSet dataSet);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecutePreparedDataSet(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecutePreparedDataSet(string tableName);

  [Obsolete("Do not use this method anymore.", true)]
  DataTable ExecutePreparedDataTable();

  [Obsolete("Do not use this method anymore.", true)]
  DataTable ExecutePreparedDataTable(DataTable dataTable);

  [Obsolete("Do not use this method anymore.", true)]
  int ExecutePreparedNonQuery();

  [Obsolete("Do not use this method anymore.", true)]
  object ExecutePreparedScalar();

  void ExecuteReader(
    string commandText,
    ExecuteReaderDelegate readerDelegate,
    ExecuteReaderArgs args,
    params IDbDataParameter[] commandParameters);

  object ExecuteScalar(CommandType commandType, string commandText);

  object ExecuteScalar(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  object ExecuteScalar(string commandText);

  object ExecuteScalar(string commandText, params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteSpDataSet(
    DataSet dataSet,
    string spName,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteSpDataSet(
    DataSet dataSet,
    string tableName,
    string spName,
    params IDbDataParameter[] parameterValues);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteSpDataSet(string spName, params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataSet ExecuteSpDataSet(
    string tableName,
    string spName,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  DataTable ExecuteSpDataTable(
    DataTable dataTable,
    string spName,
    params IDbDataParameter[] parameterValues);

  [Obsolete("Do not use this method anymore.", true)]
  DataTable ExecuteSpDataTable(string spName, params IDbDataParameter[] commandParameters);

  int ExecuteSpNonQuery(string spName, params IDbDataParameter[] parameterValues);

  [Obsolete("Do not use this method anymore.", true)]
  object ExecuteSpScalar(string spName, params IDbDataParameter[] parameterValues);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter[] GetSpParameterSet(string spName, bool includeReturnValueParameter);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter InputOutputParameter(string parameterName, object value);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter InputParameter(string parameterName, object value);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter NullParameter(string parameterName, object value);

  IDbDataParameter OutputParameter(string parameterName, object value);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter ParamByName(IDbCommand command, string paramName);

  [Obsolete("Do not use this method anymore. Use GetOutputParameterValue(string) instead of this method.", true)]
  IDbDataParameter ParamByName(string paramName);

  object GetOutputParameterValue(string parameterName);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter Parameter(
    ParameterDirection parameterDirection,
    string parameterName,
    DbType dbType);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter Parameter(
    ParameterDirection parameterDirection,
    string parameterName,
    DbType dbType,
    int size);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter Parameter(
    ParameterDirection parameterDirection,
    string parameterName,
    object value);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter Parameter(string parameterName);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter TypedParameter(string parameterName, DbType dbType);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter TypedParameter(string parameterName, DbType dbType, int size);

  IDbDataParameter Parameter(string parameterName, object value);

  [Obsolete("Do not use this method anymore.", true)]
  IDbManager Prepare(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  IDbManager Prepare(string commandText, params IDbDataParameter[] commandParameters);

  [Obsolete("Do not use this method anymore.", true)]
  IDbDataParameter ReturnValue(string parameterName);

  void Rollback();

  [Obsolete("Do not use this method anymore.", true)]
  void AddParameter(IDbDataParameter parameter);

  [Obsolete("Do not use this property anymore.", true)]
  string ConnectionName { get; }

  [Obsolete("Do not use this property anymore.", true)]
  IDbConnection Connection { get; }

  IDbDataProvider DataProvider { get; }

  [Obsolete("Do not use this property anymore.", true)]
  IDbTransaction Transaction { get; }

  bool InTransaction { get; }

  int TransactionDepth { get; }

  [Obsolete("Do not use this property anymore.", true)]
  CommandBehavior CommandBehavior { get; set; }

  void AddBatchSQL(string commandText, DbCommandParam[] cmdParams);

  void ExecuteBatchSQL();

  DbCommandParam BatchParameter(string paramName, DbType dataType, object dataValue);

  IDisposable WithOpenConnection();
}
