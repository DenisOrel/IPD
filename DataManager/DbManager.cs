using Intermech.Extensions;
using Intermech.Interfaces.Server;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;


namespace Intermech.Server.Data;

public class DbManager : 
  IDbManager,
  IDisposable,
  IDbManagerStatus,
  IDbManagerTransactions,
  IDbManagerOwnerControl
{
  private static int _idGenerator = 0;
  private static ConcurrentDictionary<DbDataProviderKey, DbDataProviderInitializationInfo> _dataProviderInitTable = new ConcurrentDictionary<DbDataProviderKey, DbDataProviderInitializationInfo>();
  private readonly int _id;
  private readonly IDbDataProvider _dataProvider;
  private readonly IDbConnection _connection;
  private DbDataProviderKey _dataProviderInitKey;
  private DbDataProviderInitializationInfo _dataProviderInitInfo;
  private int _commandTimeout;
  private object _ownerObject;
  private Dictionary<string, DbBatchCommandParameter[]> _sqlCommandsBatchList;
  private IDbCommand _command;
  private DbManagerTxData _txData;
  private readonly DBManagerLoggers _globalLoggers;
  private bool _isDisposed;
  private readonly DBManagerCrossThreadAccessGuard _crossThreadAccessGuard;
  private readonly Action _beginTransactionRecoveryAction;
  private ExecuteSpStrategy _executeSpStrategy;
  private ExecuteBatchSqlStrategy _executeBatchSqlStrategy;

  public IDbManagerConnectionInfo GetConnectionInfo()
  {
    this.CheckInternalState();
    bool inTransaction = false;
    int transactionDepth = 0;
    if (this._txData != null)
    {
      inTransaction = true;
      transactionDepth = this._txData.Depth;
    }
    return (IDbManagerConnectionInfo) new DbManagerConnectionInfo(this._id, this._connection.ConnectionString, this._connection.State, inTransaction, transactionDepth);
  }

  void IDbManagerOwnerControl.SetOwner(object owner)
  {
    if (owner == null)
      throw new ArgumentNullException(nameof (owner));
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this._ownerObject = this._ownerObject == null ? owner : throw new InvalidOperationException("Объект-владелец уже был задан ранее.");
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  void IDbManagerOwnerControl.ResetOwner(object owner)
  {
    if (owner == null)
      throw new ArgumentNullException(nameof (owner));
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this._ownerObject = this._ownerObject == owner ? (object) null : throw new InvalidOperationException("Отменить привязку может только объект-владелец.");
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private void CheckNotOwnedBeforeStateChanging()
  {
    if (this._ownerObject != null)
    {
      string str = $"Неверное использование менеджера данных. Его состояние может изменить только объект-владелец '{this._ownerObject}'.";
      IEventLogHelper eventLogHelper = DbManagerConfiguration.EventLogHelper;
      if (eventLogHelper != null)
      {
        eventLogHelper.AddToTrace(str, Consts.traceAlways, "data_manager_errors.log");
        eventLogHelper.AddToTrace(Environment.StackTrace, Consts.traceAlways, "data_manager_errors.log");
      }
      throw new UserSessionProtectionException(str);
    }
  }

  public DbManager(IDbDataProvider dataProvider, IDbConnection connection)
  {
    if (dataProvider == null)
      throw new ArgumentNullException(nameof (dataProvider));
    if (connection == null)
      throw new ArgumentNullException(nameof (connection));
    if (connection.State != ConnectionState.Closed)
      throw new ArgumentException("Объект IDbConnection не должен быть открыт.", nameof (connection));
    this._id = Interlocked.Increment(ref DbManager._idGenerator);
    this._dataProvider = dataProvider;
    this._globalLoggers = DbManagerConfiguration.Loggers;
    this._connection = connection;
    this._commandTimeout = 300;
    this._sqlCommandsBatchList = new Dictionary<string, DbBatchCommandParameter[]>();
    this._crossThreadAccessGuard = new DBManagerCrossThreadAccessGuard();
    this._beginTransactionRecoveryAction = new Action(this.RollbackInternal);
    this.InitDataProviderLazily();
    this.SetCommandTimeoutInternal(DbManagerConfiguration.NormalCommandTimeout);
  }

  public IDbDataProvider DataProvider
  {
    [DebuggerStepThrough] get => this._dataProvider;
  }

  [Obsolete("Do not use this property anymore.", true)]
  public IDbConnection Connection => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public void AddParameter(IDbDataParameter parameter) => throw new NotSupportedException();

  private IDbCommand Command
  {
    [DebuggerStepThrough] get
    {
      if (this._command == null)
        this._command = this.CreateNewCommand();
      return this._command;
    }
  }

  private IDbCommand CreateNewCommand()
  {
    IDbCommand command = this._dataProvider.CreateCommand(this._connection);
    command.CommandTimeout = this._commandTimeout;
    if (this._txData != null)
      command.Transaction = this._txData.Transaction;
    return command;
  }

  private DataTable PrepareDateTimeColumns(DataTable tbl)
  {
    for (int index = 0; index < tbl.Columns.Count; ++index)
    {
      if (tbl.Columns[index].DataType == typeof (DateTime))
        tbl.Columns[index].DateTimeMode = DataSetDateTime.Unspecified;
    }
    return this.DataProvider.PrepareDataTable(tbl);
  }

  [Obsolete("Do not use this property anymore.", true)]
  public IDbTransaction Transaction => throw new NotSupportedException();

  public int TransactionDepth
  {
    [DebuggerStepThrough] get => this._txData == null ? 0 : this._txData.Depth;
  }

  public bool InTransaction
  {
    [DebuggerStepThrough] get => this._txData != null;
  }

  [Obsolete("Do not use this property anymore.", true)]
  public string ConnectionName => throw new NotSupportedException();

  public bool IsDisposed
  {
    [DebuggerStepThrough] get => this._isDisposed;
  }

  public void Dispose()
  {
    this.SetLock();
    try
    {
      this.DisposeInternal();
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private void DisposeInternal()
  {
    if (this._isDisposed)
      return;
    this.CheckNotOwnedBeforeStateChanging();
    bool flag = false;
    if (this._txData != null)
    {
      this.RollbackInternal();
      flag = true;
    }
    if (this._command != null)
    {
      this._command.Dispose();
      this._command = (IDbCommand) null;
    }
    this._connection.Dispose();
    this._isDisposed = true;
    EventHandler disposed = this.Disposed;
    if (disposed != null)
      disposed((object) this, EventArgs.Empty);
    if (!flag)
      return;
    this.ReportTransactionIsActive();
  }

  public event EventHandler Disposed;

  public T GetConnectionAs<T>() where T : DbConnection
  {
    this.CheckInternalState();
    return (T) this._connection;
  }

  public T GetTransactionAs<T>() where T : DbTransaction
  {
    this.CheckInternalState();
    return this._txData == null ? default (T) : (T) this._txData.Transaction;
  }

  [Obsolete("Do not use this method anymore.", true)]
  public void Close() => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public void Close(Guid sessionGuid) => throw new NotSupportedException();

  public IDisposable WithOpenConnection()
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return (IDisposable) this.CreateOpenConnectionScope(true);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private DbManagerConnectionScope CreateOpenConnectionScope(bool checkedScope = false)
  {
    bool needCloseConnection = this._connection.State == ConnectionState.Closed;
    if (needCloseConnection)
      this._connection.Open();
    return new DbManagerConnectionScope(this, needCloseConnection, checkedScope);
  }

  internal void DisposeOpenConnectionScope()
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      bool flag = false;
      if (this._txData != null)
      {
        this.RollbackInternal();
        flag = true;
      }
      this.DisposeOpenConnectionScopeInternal();
      if (!flag)
        return;
      this.ReportTransactionIsActive();
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  internal void DisposeOpenConnectionScopeInternal() => this._connection.Close();

  IDbManagerTransactionState IDbManagerTransactions.CaptureTransactionState()
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return (IDbManagerTransactionState) new DbManagerTxState(this, this._txData, this._txData != null ? this._txData.Depth : 0);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  internal void RestoreTransactionState(DbManagerTxState capturedState)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      if (this._txData == null || this._txData != capturedState.TransactionData)
        return;
      this._txData.Depth = capturedState.TransactionDepth;
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  public IDbManager BeginTransaction()
  {
    return this.BeginTransaction(this.DataProvider.DefaultIsolationLevel);
  }

  public IDbManager BeginTransaction(IsolationLevel il)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this.BeginTransactionInternal(il);
      return (IDbManager) this;
    }
    catch (Exception ex)
    {
      this.ReportAndThrowWithNoReturn(ex);
      return (IDbManager) null;
    }
    finally
    {
      this.ReleaseLock(this._beginTransactionRecoveryAction);
    }
  }

  private void BeginTransactionInternal(IsolationLevel il)
  {
    if (this._txData != null)
    {
      this._txData.IncDepth();
    }
    else
    {
      DbManagerConnectionScope connectionScope = new DbManagerConnectionScope();
      IDbTransaction dbTransaction = (IDbTransaction) null;
      IDbTransaction transaction;
      try
      {
        connectionScope = this.CreateOpenConnectionScope();
        transaction = this._connection.BeginTransaction(il);
      }
      catch
      {
        if (dbTransaction != null)
        {
          dbTransaction.Rollback();
          dbTransaction.Dispose();
        }
        if (connectionScope.DbManager == this)
          connectionScope.Dispose();
        throw;
      }
      this._txData = new DbManagerTxData(transaction, connectionScope);
      if (this._command == null)
        return;
      this._command.Transaction = this._txData.Transaction;
    }
  }

  public bool Commit()
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return this.CommitInternal();
    }
    catch (Exception ex)
    {
      this.ReportAndThrowWithNoReturn(ex);
      return false;
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private bool CommitInternal()
  {
    if (this._txData == null)
      return false;
    if (this._txData.Depth > 1)
    {
      this._txData.DecDepth();
      return false;
    }
    if (this._sqlCommandsBatchList.Count > 0)
      throw new KernelException("В методе завершения транзакции обнаружены невыполненные пакетные SQL-команды.");
    try
    {
      this._txData.Transaction.Commit();
    }
    finally
    {
      this._txData.Transaction.Dispose();
      this._txData.ConnectionScope.Dispose();
      this._txData = (DbManagerTxData) null;
      if (this._command != null && this._command.Transaction != null)
        this._command.Transaction = (IDbTransaction) null;
    }
    return true;
  }

  public void Rollback()
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this.RollbackInternal();
    }
    catch (Exception ex)
    {
      this.ReportAndThrowWithNoReturn(ex);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private void RollbackInternal()
  {
    if (this._txData == null)
      return;
    this._sqlCommandsBatchList.Clear();
    try
    {
      this._txData.Transaction.Rollback();
    }
    finally
    {
      this._txData.Transaction.Dispose();
      this._txData.ConnectionScope.Dispose();
      this._txData = (DbManagerTxData) null;
      if (this._command != null && this._command.Transaction != null)
        this._command.Transaction = (IDbTransaction) null;
    }
  }

  private string ReportTransactionIsActive()
  {
    string EventStr = "Неверное использование менеджера данных. Обнаружена незакрытая транзакция при изменении состояния менеджера.";
    IEventLogHelper eventLogHelper = DbManagerConfiguration.EventLogHelper;
    if (eventLogHelper != null)
    {
      eventLogHelper.AddToTrace(EventStr, Consts.traceAlways, "data_manager_errors.log");
      eventLogHelper.AddToTrace(Environment.StackTrace, Consts.traceAlways, "data_manager_errors.log");
    }
    return EventStr;
  }

  public void SetLock() => this._crossThreadAccessGuard.Enter(1000);

  public void ReleaseLock(Action recoveryAction = null)
  {
    this._crossThreadAccessGuard.Exit(recoveryAction);
  }

  private void CheckInternalState()
  {
    if (this._isDisposed)
      throw new ObjectDisposedException(this.GetType().FullName);
  }

  private bool IsNull(object value)
  {
    if (value == null || value is string && ((string) value).TrimEnd((char[]) null).Length == 0 || value is DateTime dateTime && dateTime == DateTime.MinValue || value is (short) 0 || value is 0)
      return true;
    return value is 0L;
  }

  private DbDataProviderInitializationInfo InitDataProviderOnClosedConnection(
    DbDataProviderKey dataProviderKey)
  {
    using (this.WithOpenConnection())
    {
      if (!this._dataProvider.IsInitialized)
        this._dataProvider.Initialize((IDbManager) this);
      return new DbDataProviderInitializationInfo();
    }
  }

  private void InitDataProviderIfNeed()
  {
    this._dataProviderInitKey = new DbDataProviderKey(this._dataProvider.Name, this._connection.ConnectionString);
    this._dataProviderInitInfo = DbManager._dataProviderInitTable.GetOrAdd(this._dataProviderInitKey, new System.Func<DbDataProviderKey, DbDataProviderInitializationInfo>(this.InitDataProviderOnClosedConnection));
  }

  private void InitDataProviderLazily()
  {
    try
    {
      this.InitDataProviderIfNeed();
    }
    catch
    {
      try
      {
        this.InitDataProviderIfNeed();
      }
      catch (Exception ex)
      {
        this.ReportAndThrowWithNoReturn(ex);
      }
    }
  }

  private void ReportExceptionToLogFile(Exception ex, params object[] extraInfo)
  {
    IEventLogHelper eventLogHelper = DbManagerConfiguration.EventLogHelper;
    if (eventLogHelper == null)
      return;
    string str = this._command == null ? string.Empty : $"Текст команды:{this._command.CommandText}{Environment.NewLine}";
    if (this._command != null && this._command.Parameters.Count != 0)
      str += $"Параметры:{this._globalLoggers.CommandParamsToString(this._command)}{Environment.NewLine}";
    if (extraInfo.Length != 0)
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
      {
        StringBuilder sb = objectPoolScope.Object;
        sb.Append(Environment.NewLine);
        ((IEnumerable<object>) extraInfo).InvokeForAll<object>((Action<object>) (item => sb.AppendLine(Convert.ToString(item))));
        str += $"Доп. информация: {sb} {Environment.NewLine}";
      }
    }
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.AppendLine("------------------------------------");
      stringBuilder.AppendLine($"Тип исключения  : {ex.GetType()}");
      stringBuilder.AppendLine("Текст исключения: " + ex.Message);
      stringBuilder.AppendLine("Стек :");
      stringBuilder.AppendLine(ex.StackTrace);
      Exception innerException = ex.InnerException;
      if (innerException != null)
      {
        stringBuilder.AppendLine("Internal Exception");
        stringBuilder.AppendLine($"Тип исключения  : {innerException.GetType()}");
        stringBuilder.AppendLine("Текст исключения: " + innerException.Message);
        stringBuilder.AppendLine("Стек :");
        stringBuilder.AppendLine(innerException.StackTrace);
      }
      stringBuilder.AppendLine("Стек вызова:");
      stringBuilder.AppendLine(Environment.StackTrace);
      stringBuilder.AppendLine(str);
      eventLogHelper.AddToTrace(stringBuilder.ToString(), Consts.traceAlways, "sql_errors.log");
    }
  }

  private void ReportAndThrowWithNoReturn(Exception ex, params object[] extraInfo)
  {
    this.ReportExceptionToLogFile(ex, extraInfo);
    if (this.CanWrapDbException(ex))
      throw this._dataProvider.WrapDbException(ex);
    ExceptionDispatchInfo.Capture(ex).Throw();
  }

  private bool CanWrapDbException(Exception ex)
  {
    switch (ex)
    {
      case ArgumentException _:
        return false;
      case InvalidOperationException _:
        return false;
      case NotSupportedException _:
        return false;
      case ObjectDisposedException _:
        return false;
      case KernelException _:
        return false;
      default:
        return true;
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter[] CreateParameters(
    DataRow dataRow,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter Parameter(string parameterName) => throw new NotSupportedException();

  public IDbDataParameter Parameter(string parameterName, object value)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return this.ParameterInternal(ParameterDirection.Input, parameterName, value);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter TypedParameter(string parameterName, DbType dbType)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter TypedParameter(string parameterName, DbType dbType, int size)
  {
    return this.Parameter(ParameterDirection.Input, parameterName, dbType, size);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter NullParameter(string parameterName, object value)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter InputParameter(string parameterName, object value)
  {
    return this.Parameter(ParameterDirection.Input, parameterName, value);
  }

  public IDbDataParameter OutputParameter(string parameterName, object value)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return this.ParameterInternal(ParameterDirection.Output, parameterName, value);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter InputOutputParameter(string parameterName, object value)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter ReturnValue(string parameterName) => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter Parameter(
    ParameterDirection parameterDirection,
    string parameterName,
    object value)
  {
    throw new NotSupportedException();
  }

  private IDbDataParameter ParameterInternal(
    ParameterDirection parameterDirection,
    string parameterName,
    object parameterValue)
  {
    try
    {
      IDbDataParameter parameter = this.Command.CreateParameter();
      parameter.ParameterName = this._dataProvider.UpdateParameterName(parameterName);
      parameter.Direction = parameterDirection;
      if (parameterValue is DbTypedValue dbTypedValue)
      {
        parameter.DbType = dbTypedValue.DBType;
        parameter.Value = this._dataProvider.UpdateParameterValue(parameter.ParameterName, dbTypedValue.Value);
      }
      else
      {
        parameter.Value = this._dataProvider.UpdateParameterValue(parameter.ParameterName, parameterValue);
        this._dataProvider.UpdateParameterTypeByValue(parameter, parameter.Value);
      }
      return parameter;
    }
    catch (Exception ex)
    {
      this.ReportAndThrowWithNoReturn(ex);
      return (IDbDataParameter) null;
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter Parameter(
    ParameterDirection parameterDirection,
    string parameterName,
    DbType dbType)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter Parameter(
    ParameterDirection parameterDirection,
    string parameterName,
    DbType dbType,
    int size)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore. Use GetOutputParameterValue(string) instead of this method.", true)]
  public IDbDataParameter ParamByName(string paramName) => throw new NotSupportedException();

  public object GetOutputParameterValue(string parameterName)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      IDataParameterCollection parameters = this.Command.Parameters;
      int index = parameters.IndexOf(this._dataProvider.UpdateParameterName(parameterName));
      if (index >= 0)
        return ((IDataParameter) parameters[index]).Value;
      throw new KernelException($"Параметр '{parameterName}' не найден в списке параметров текущей команды.");
    }
    catch (Exception ex)
    {
      this.ReportAndThrowWithNoReturn(ex);
      return (object) null;
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter ParamByName(IDbCommand command, string paramName)
  {
    throw new NotSupportedException();
  }

  private void SetupCurrentCommand(
    CommandType commandType,
    string commandText,
    IDbDataParameter[] commandParameters = null)
  {
    try
    {
      this.SetupCommand(this.Command, commandType, commandText, commandParameters);
    }
    catch (Exception ex)
    {
      this.ReportAndThrowWithNoReturn(ex);
    }
  }

  private void SetupCommand(
    IDbCommand command,
    CommandType commandType,
    string commandText,
    IDbDataParameter[] commandParameters = null)
  {
    command.CommandText = this._dataProvider.UpdateCommandText(commandType, commandText);
    command.CommandType = commandType;
    IDataParameterCollection parameters = command.Parameters;
    if (parameters.Count != 0)
      parameters.Clear();
    if (commandParameters == null)
      return;
    foreach (IDbDataParameter commandParameter in commandParameters)
      parameters.Add((object) commandParameter);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbDataParameter[] GetSpParameterSet(string spName, bool includeReturnValueParameter)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbManager AssignParameterValues(DataRow dataRow) => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public IDbManager Prepare(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public IDbManager Prepare(string commandText, params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public int Execute(string commandText, DataTable table) => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public int Execute(CommandType commandType, string commandText, DataTable table)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public int Execute(string commandText, DataSet dataSet) => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public int Execute(CommandType commandType, string commandText, DataSet dataSet)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public int Execute(
    CommandType commandType,
    string commandText,
    DataSet dataSet,
    string tableName)
  {
    throw new NotSupportedException();
  }

  public int ExecuteNonQuery(string commandText)
  {
    return this.ExecuteNonQuery(CommandType.Text, commandText, (IDbDataParameter[]) null);
  }

  public int ExecuteNonQuery(CommandType commandType, string commandText)
  {
    return this.ExecuteNonQuery(commandType, commandText, (IDbDataParameter[]) null);
  }

  public int ExecuteNonQuery(string commandText, params IDbDataParameter[] commandParameters)
  {
    return this.ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public int ExecuteNonQueryArr(string commandText, IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  public int ExecuteNonQuery(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return this.ExecuteNonQueryInternal(commandType, commandText, commandParameters);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private int ExecuteNonQueryInternal(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    this.SetupCurrentCommand(commandType, commandText, commandParameters);
    return this.ExecuteNonQueryInternal();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public int ExecutePreparedNonQuery() => throw new NotSupportedException();

  public void SetAdminCommandTimeout()
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this.SetCommandTimeoutInternal(172800);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  public void SetNormalCommandTimeout()
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this.SetCommandTimeoutInternal(DbManagerConfiguration.NormalCommandTimeout);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private void SetCommandTimeoutInternal(int newCommandTimeout)
  {
    if (this._commandTimeout == newCommandTimeout)
      return;
    this._commandTimeout = newCommandTimeout;
    if (this._command == null)
      return;
    this._command.CommandTimeout = newCommandTimeout;
  }

  private int ExecuteNonQueryInternal()
  {
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      int num;
      using (this.CreateOpenConnectionScope())
      {
        if (this._globalLoggers.Enabled)
          this._globalLoggers.Log(DbManagerLogType.ExecuteNonQuery, this, this.Command, DateTime.UtcNow - utcNow);
        num = this.Command.ExecuteNonQuery();
        if (this._globalLoggers.Enabled)
          this._globalLoggers.Log(DbManagerLogType.ElapsedTime, this, this.Command, DateTime.UtcNow - utcNow);
      }
      return num;
    }
    catch (Exception ex)
    {
      if (this._globalLoggers.Enabled)
        this._globalLoggers.Log(DbManagerLogType.ExecuteNonQuery, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
        {
          ex.Message
        });
      this.ReportAndThrowWithNoReturn(ex);
      return 0;
    }
  }

  public int ExecuteSpNonQuery(string spName, params IDbDataParameter[] parameterValues)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      if (this._executeSpStrategy == null)
        this._executeSpStrategy = ((CustomDataProvider) this._dataProvider).CreateExecuteSpStrategy(this);
      DbManagerCommandData commandData = this._executeSpStrategy.CreateCommandData(spName, parameterValues);
      DbManagerCommandResult commandResult = new DbManagerCommandResult(commandData.ScalarMode ? this.ExecuteScalarInternal(commandData.CommandType, commandData.CommandText, commandData.CommandParameters) : (object) this.ExecuteNonQueryInternal(commandData.CommandType, commandData.CommandText, commandData.CommandParameters));
      this._executeSpStrategy.ProcessCommandResult(commandData, commandResult);
      if (commandResult.ExtraOutputParameters.Count != 0)
      {
        IDataParameterCollection parameters = this.Command.Parameters;
        foreach (IDbDataParameter extraOutputParameter in (IEnumerable<IDbDataParameter>) commandResult.ExtraOutputParameters)
          parameters.Add((object) extraOutputParameter);
      }
      return -1;
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  public object ExecuteScalar(string commandText)
  {
    return this.ExecuteScalar(CommandType.Text, commandText, (IDbDataParameter[]) null);
  }

  public object ExecuteScalar(CommandType commandType, string commandText)
  {
    return this.ExecuteScalar(commandType, commandText, (IDbDataParameter[]) null);
  }

  public object ExecuteScalar(string commandText, params IDbDataParameter[] commandParameters)
  {
    return this.ExecuteScalar(CommandType.Text, commandText, commandParameters);
  }

  public object ExecuteScalar(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return this.ExecuteScalarInternal(commandType, commandText, commandParameters);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private object ExecuteScalarInternal(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    this.SetupCurrentCommand(commandType, commandText, commandParameters);
    return this.ExecuteScalarInternal();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public object ExecutePreparedScalar() => throw new NotSupportedException();

  private object ExecuteScalarInternal()
  {
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      object scalarValue;
      using (this.CreateOpenConnectionScope())
      {
        if (this._globalLoggers.Enabled)
          this._globalLoggers.Log(DbManagerLogType.ExecuteScalar, this, this.Command, DateTime.UtcNow - utcNow);
        scalarValue = this.Command.ExecuteScalar();
        scalarValue = this.DataProvider.ConvertScalarValue(scalarValue);
        if (this._globalLoggers.Enabled)
        {
          string str = string.Empty;
          if (scalarValue != null)
            str = $" Result[{scalarValue.GetType().ToString()}] = {scalarValue}";
          this._globalLoggers.Log(DbManagerLogType.ElapsedTime, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
          {
            str
          });
        }
      }
      return scalarValue;
    }
    catch (Exception ex)
    {
      if (this._globalLoggers.Enabled)
        this._globalLoggers.Log(DbManagerLogType.ExecuteScalar, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
        {
          ex.Message
        });
      this.ReportAndThrowWithNoReturn(ex);
      return (object) null;
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public object ExecuteSpScalar(string spName, params IDbDataParameter[] parameterValues)
  {
    throw new NotSupportedException();
  }

  public void ExecuteReader(
    string commandText,
    ExecuteReaderDelegate readerDelegate,
    ExecuteReaderArgs args,
    params IDbDataParameter[] commandParameters)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this.ExecuteReaderInternal(commandText, commandParameters, readerDelegate, args);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  [Obsolete("Do not use this property anymore.", true)]
  public CommandBehavior CommandBehavior
  {
    get => throw new NotSupportedException();
    set => throw new NotSupportedException();
  }

  private void ExecuteReaderInternal(
    string commandText,
    IDbDataParameter[] commandParameters,
    ExecuteReaderDelegate readerDelegate,
    ExecuteReaderArgs args)
  {
    this.SetupCurrentCommand(CommandType.Text, commandText, commandParameters);
    this.ExecuteReaderInternal(readerDelegate, args);
  }

  private void ExecuteReaderInternal(
    ExecuteReaderDelegate readerDelegate,
    ExecuteReaderArgs readerArgs)
  {
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      using (this.CreateOpenConnectionScope())
      {
        if (this._globalLoggers.Enabled)
          this._globalLoggers.Log(DbManagerLogType.ExecuteReader, this, this.Command, DateTime.UtcNow - utcNow);
        CommandBehavior behavior = CommandBehavior.Default;
        if (this.DataProvider.Name != "PostgreSQL")
          behavior = CommandBehavior.SequentialAccess;
        using (IDataReader reader = this.Command.ExecuteReader(behavior))
          readerDelegate(reader, readerArgs);
        if (!this._globalLoggers.Enabled)
          return;
        this._globalLoggers.Log(DbManagerLogType.ElapsedTime, this, this.Command, DateTime.UtcNow - utcNow);
      }
    }
    catch (Exception ex)
    {
      if (this._globalLoggers.Enabled)
        this._globalLoggers.Log(DbManagerLogType.ExecuteReader, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
        {
          ex.Message
        });
      this.ReportAndThrowWithNoReturn(ex);
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(string commandText) => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(CommandType commandType, string commandText)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(string commandText, params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecutePreparedDataSet() => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteSpDataSet(string spName, params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(DataSet dataSet, string commandText)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(DataSet dataSet, CommandType commandType, string commandText)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(
    DataSet dataSet,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(
    DataSet dataSet,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecutePreparedDataSet(DataSet dataSet) => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteSpDataSet(
    DataSet dataSet,
    string spName,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  public DataSet ExecuteDataSet(string tableName, string commandText)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return this.ExecuteDataSetInternal((DataSet) null, 0, 0, tableName, CommandType.Text, commandText, (IDbDataParameter[]) null);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(string tableName, CommandType commandType, string commandText)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(
    string tableName,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(
    string tableName,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecutePreparedDataSet(string tableName) => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteSpDataSet(
    string tableName,
    string spName,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(DataSet dataSet, string tableName, string commandText)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(
    DataSet dataSet,
    string tableName,
    CommandType commandType,
    string commandText)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(
    DataSet dataSet,
    string tableName,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  public DataSet ExecuteDataSet(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return this.ExecuteDataSetInternal(dataSet, startRecord, maxRecords, tableName, CommandType.Text, commandText, commandParameters);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSetArr(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName,
    string commandText,
    IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteDataSet(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  private DataSet ExecuteDataSetInternal(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    this.SetupCurrentCommand(commandType, commandText, commandParameters);
    return this.ExecuteDataSetInternal(dataSet, startRecord, maxRecords, tableName);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecutePreparedDataSet(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName)
  {
    throw new NotSupportedException();
  }

  private DataSet ExecuteDataSetInternal(
    DataSet dataSet,
    int startRecord,
    int maxRecords,
    string tableName)
  {
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      using (this.CreateOpenConnectionScope())
      {
        if (this._globalLoggers.Enabled)
          this._globalLoggers.Log(DbManagerLogType.ExecuteDataSet, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
          {
            tableName
          });
        if (dataSet == null)
        {
          dataSet = new DataSet();
          dataSet.RemotingFormat = SerializationFormat.Binary;
        }
        IDbDataAdapter dataAdapter = this._dataProvider.CreateDataAdapter(this._connection);
        dataAdapter.SelectCommand = this.Command;
        if (tableName == null)
          dataAdapter.Fill(dataSet);
        else if (maxRecords != 0)
          ((DbDataAdapter) dataAdapter).Fill(dataSet, startRecord, maxRecords, tableName);
        else
          ((DbDataAdapter) dataAdapter).Fill(dataSet, tableName);
        if (this._globalLoggers.Enabled)
          this._globalLoggers.Log(DbManagerLogType.ElapsedTime, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
          {
            tableName
          });
        return dataSet;
      }
    }
    catch (Exception ex)
    {
      if (this._globalLoggers.Enabled)
        this._globalLoggers.Log(DbManagerLogType.ExecuteDataSet, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
        {
          ex.Message
        });
      this.ReportAndThrowWithNoReturn(ex);
      return (DataSet) null;
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataSet ExecuteSpDataSet(
    DataSet dataSet,
    string tableName,
    string spName,
    params IDbDataParameter[] parameterValues)
  {
    throw new NotSupportedException();
  }

  public DataTable ExecuteDataTable(string commandText)
  {
    return this.ExecuteDataTable((DataTable) null, CommandType.Text, commandText, (IDbDataParameter[]) null);
  }

  public DataTable ExecuteDataTable(CommandType commandType, string commandText)
  {
    return this.ExecuteDataTable((DataTable) null, commandType, commandText, (IDbDataParameter[]) null);
  }

  public DataTable ExecuteDataTable(string commandText, params IDbDataParameter[] commandParameters)
  {
    return this.ExecuteDataTable((DataTable) null, CommandType.Text, commandText, commandParameters);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataTable ExecuteDataTableArr(string commandText, IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  public DataTable ExecuteDataTable(
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    return this.ExecuteDataTable((DataTable) null, commandType, commandText, commandParameters);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataTable ExecutePreparedDataTable() => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public DataTable ExecuteSpDataTable(string spName, params IDbDataParameter[] commandParameters)
  {
    throw new NotSupportedException();
  }

  public DataTable ExecuteDataTable(DataTable dataTable, string commandText)
  {
    return this.ExecuteDataTable(dataTable, CommandType.Text, commandText, (IDbDataParameter[]) null);
  }

  public DataTable ExecuteDataTable(
    DataTable dataTable,
    CommandType commandType,
    string commandText)
  {
    return this.ExecuteDataTable(dataTable, commandType, commandText, (IDbDataParameter[]) null);
  }

  public DataTable ExecuteDataTable(
    DataTable dataTable,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    return this.ExecuteDataTable(dataTable, CommandType.Text, commandText, commandParameters);
  }

  public DataTable ExecuteDataTable(
    DataTable dataTable,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return this.ExecuteDataTableInternal(dataTable, commandType, commandText, commandParameters);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private DataTable ExecuteDataTableInternal(
    DataTable dataTable,
    CommandType commandType,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    this.SetupCurrentCommand(commandType, commandText, commandParameters);
    return this.ExecuteDataTableInternal(dataTable);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataTable ExecutePreparedDataTable(DataTable dataTable)
  {
    throw new NotSupportedException();
  }

  private DataTable ExecuteDataTableInternal(DataTable dataTable)
  {
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      using (this.CreateOpenConnectionScope())
      {
        if (this._globalLoggers.Enabled)
          this._globalLoggers.Log(DbManagerLogType.ExecuteDataTable, this, this.Command, DateTime.UtcNow - utcNow);
        if (dataTable == null)
        {
          dataTable = new DataTable();
          dataTable.RemotingFormat = SerializationFormat.Binary;
        }
        IDbDataAdapter dataAdapter = this._dataProvider.CreateDataAdapter(this._connection);
        dataAdapter.SelectCommand = this.Command;
        ((DbDataAdapter) dataAdapter).Fill(dataTable);
        if (this._globalLoggers.Enabled)
          this._globalLoggers.Log(DbManagerLogType.ElapsedTime, this, this.Command, DateTime.UtcNow - utcNow);
        return this.PrepareDateTimeColumns(dataTable);
      }
    }
    catch (Exception ex)
    {
      if (this._globalLoggers.Enabled)
        this._globalLoggers.Log(DbManagerLogType.ExecuteDataTable, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
        {
          ex.Message
        });
      this.ReportAndThrowWithNoReturn(ex);
      return (DataTable) null;
    }
  }

  [Obsolete("Do not use this method anymore.", true)]
  public DataTable ExecuteSpDataTable(
    DataTable dataTable,
    string spName,
    params IDbDataParameter[] parameterValues)
  {
    throw new NotSupportedException();
  }

  public void AddBatchSQL(string commandText, DbCommandParam[] cmdParams)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this.AddBatchSQLInternal(commandText, cmdParams);
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private void AddBatchSQLInternal(string commandText, DbCommandParam[] cmdParams)
  {
    if (cmdParams.Length == 0)
      throw new KernelException($"Запрос {commandText} не является параметризованным, поэтому не может быть выполнен в пакетном режиме.");
    DbBatchCommandParameter[] commandParameterArray1;
    if (this._sqlCommandsBatchList.TryGetValue(commandText, out commandParameterArray1))
    {
      if (cmdParams.Length != commandParameterArray1.Length)
        throw new KernelException($"Не совпадает количеcтво параметров в пакетном запросе для команды {commandText}");
      for (int index1 = 0; index1 < cmdParams.Length; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < commandParameterArray1.Length; ++index2)
        {
          if (commandParameterArray1[index2].ParamName == cmdParams[index1].ParamName)
          {
            if (commandParameterArray1[index2].ParamType != cmdParams[index1].DataType)
              throw new KernelException($"Для параметра {cmdParams[index1].ParamName} в пакетном запросе {commandText} не совпадает тип данных.");
            commandParameterArray1[index2].ParamValues.Add(cmdParams[index1].DataValue);
            flag = true;
            break;
          }
        }
        if (!flag)
          throw new KernelException($"Не найден параметр {cmdParams[index1].ParamName} в пакетном запросе {commandText}.");
      }
    }
    else
    {
      DbBatchCommandParameter[] commandParameterArray2 = new DbBatchCommandParameter[cmdParams.Length];
      for (int index = 0; index < cmdParams.Length; ++index)
        commandParameterArray2[index] = new DbBatchCommandParameter(cmdParams[index].ParamName, cmdParams[index].DataType, cmdParams[index].DataValue);
      this._sqlCommandsBatchList.Add(commandText, commandParameterArray2);
    }
  }

  public void ExecuteBatchSQL()
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      this.ExecuteBatchSQLInternal();
    }
    finally
    {
      this.ReleaseLock();
    }
  }

  private void ExecuteBatchSQLInternal()
  {
    if (this._txData == null)
      throw new KernelException("Попытка выполнить метод ExecuteBatchSQL() вне транзакции!");
    if (this._executeBatchSqlStrategy == null)
      this._executeBatchSqlStrategy = ((CustomDataProvider) this._dataProvider).CreateExecuteBatchSqlStrategy(this);
    try
    {
      foreach (KeyValuePair<string, DbBatchCommandParameter[]> sqlCommandsBatch in this._sqlCommandsBatchList)
      {
        string key = sqlCommandsBatch.Key;
        DbBatchCommandParameter[] commandParameterArray = sqlCommandsBatch.Value;
        this.SetupCurrentCommand(CommandType.Text, key);
        DateTime utcNow = DateTime.UtcNow;
        string[] addData = (string[]) null;
        try
        {
          if (this._globalLoggers.Enabled)
            this._globalLoggers.Log(DbManagerLogType.ExecuteBatchSQL, this, this.Command, DateTime.UtcNow - utcNow);
          this._executeBatchSqlStrategy.Execute(this.Command, commandParameterArray, commandParameterArray[0].ParamValues.Count);
          if (this._globalLoggers.Enabled)
          {
            addData = this._globalLoggers.CommandBatchParamsToStrings(commandParameterArray);
            this._globalLoggers.Log(DbManagerLogType.ElapsedTime, this, this.Command, DateTime.UtcNow - utcNow, addData);
          }
        }
        catch (Exception ex)
        {
          if (this._globalLoggers.Enabled)
            this._globalLoggers.Log(DbManagerLogType.ExecuteBatchSQL, this, this.Command, DateTime.UtcNow - utcNow, new string[1]
            {
              ex.Message
            });
          string[] source = addData ?? this._globalLoggers.CommandBatchParamsToStrings(commandParameterArray);
          this.ReportAndThrowWithNoReturn(ex, ((IEnumerable<object>) source).ToArray<object>());
        }
      }
    }
    finally
    {
      this._sqlCommandsBatchList.Clear();
    }
  }

  public DbCommandParam BatchParameter(string paramName, DbType dataType, object dataValue)
  {
    this.CheckInternalState();
    this.SetLock();
    try
    {
      return new DbCommandParam(paramName, dataType, dataValue);
    }
    finally
    {
      this.ReleaseLock();
    }
  }
}
