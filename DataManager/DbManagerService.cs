// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbManagerService
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Server.Data;

public sealed class DbManagerService : IDbManagerService
{
  private DbDataProviderCreatorService _dataProviderCreatorService;
  private string _defaultDataProviderName;
  private IConnectionStringService _connectionStringService;
  private FirstConnectionInfo _firstConnectionInfo;
  private ConcurrentDictionary<string, string> _connectionNameToDataProviderNameTable;
  private ConcurrentDictionary<DbDataProviderKey, IDbDataProvider> _connectionInfoToDataProviderTable;
  private List<Intermech.Server.Data.DbManager> _activeManagers;

  public DbManagerService(
    IConnectionStringService connectionStringService,
    string defaultDataProviderName,
    IEnumerable<string> dataProviderAssemblyFiles)
  {
    if (connectionStringService == null)
      throw new ArgumentNullException(nameof (connectionStringService));
    if (defaultDataProviderName == null)
      throw new ArgumentNullException(nameof (defaultDataProviderName));
    this._dataProviderCreatorService = dataProviderAssemblyFiles != null ? new DbDataProviderCreatorService(EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, ServerDiagnosticsConsts.EventLogSourceName), dataProviderAssemblyFiles) : throw new ArgumentNullException(nameof (dataProviderAssemblyFiles));
    this._defaultDataProviderName = this._dataProviderCreatorService.CanCreate(defaultDataProviderName) ? defaultDataProviderName : throw new ArgumentException($"Unknown data provider name '{defaultDataProviderName}'.", nameof (defaultDataProviderName));
    this._connectionStringService = connectionStringService;
    this._firstConnectionInfo = (FirstConnectionInfo) null;
    this._connectionNameToDataProviderNameTable = new ConcurrentDictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._connectionInfoToDataProviderTable = new ConcurrentDictionary<DbDataProviderKey, IDbDataProvider>();
    this._activeManagers = new List<Intermech.Server.Data.DbManager>(128 /*0x80*/);
  }

  public string ConnectionName
  {
    [DebuggerStepThrough] get => this._connectionStringService.DefaultConnectionName;
  }

  public string ConnectionString
  {
    [DebuggerStepThrough] get => this._connectionStringService.DefaultConnectionString;
  }

  [Obsolete("Use the method DbManagerService.CreateDbManager() instead of this.", true)]
  public IDbManager DbManager => throw new NotSupportedException();

  [Obsolete("Use the method CreateDbManager(string, string) instead of this.", true)]
  public IDbDataProvider GetDataProviderByName(string providerName)
  {
    throw new NotSupportedException();
  }

  private string GetDataProviderNameByConnectionName(string connectionName)
  {
    FirstConnectionInfo firstConnectionInfo = this._firstConnectionInfo;
    if (firstConnectionInfo != null && string.Equals(firstConnectionInfo.ConnectionName, connectionName, StringComparison.OrdinalIgnoreCase))
      return firstConnectionInfo.DataProviderName;
    string orAdd = this._connectionNameToDataProviderNameTable.GetOrAdd(connectionName, new System.Func<string, string>(this.GetDataProviderNameByConnectionNameSlow));
    if (firstConnectionInfo == null)
      Interlocked.CompareExchange<FirstConnectionInfo>(ref this._firstConnectionInfo, new FirstConnectionInfo(orAdd, connectionName), (FirstConnectionInfo) null);
    return orAdd;
  }

  private string GetDataProviderNameByConnectionNameSlow(string connectionName)
  {
    string[] strArray = connectionName.Split(new char[1]
    {
      '.'
    }, StringSplitOptions.RemoveEmptyEntries);
    int length = strArray.Length;
    for (int index = 0; index < length; ++index)
    {
      string dataProviderName = strArray[index];
      if (this._dataProviderCreatorService.CanCreate(dataProviderName))
        return dataProviderName;
    }
    return this._defaultDataProviderName;
  }

  public IDbManager CreateDbManager()
  {
    return this.CreateDbManager(this.GetDataProviderNameByConnectionName(this.ConnectionName), this._connectionStringService.GetConnectionString(this.ConnectionName));
  }

  [Obsolete("Use the method CreateDbManager(string, string) instead of this.", true)]
  public IDbManager CreateDbManager(IDbConnection dbConnection)
  {
    throw new NotSupportedException();
  }

  public IDbManager CreateDbManager(string dataProviderName, string connectionString)
  {
    if (dataProviderName == null)
      throw new ArgumentNullException(nameof (dataProviderName));
    IDbDataProvider dataProvider = connectionString != null ? this.GetOrCreateDataProvider(dataProviderName, connectionString) : throw new ArgumentNullException(nameof (connectionString));
    Intermech.Server.Data.DbManager dbManager = new Intermech.Server.Data.DbManager(dataProvider, dataProvider.CreateConnection(connectionString));
    this.AddToActiveManagers(dbManager);
    return (IDbManager) dbManager;
  }

  private IDbDataProvider GetOrCreateDataProvider(string dataProviderName, string connectionString)
  {
    DbDataProviderKey key = new DbDataProviderKey(dataProviderName, connectionString);
    IDbDataProvider orAdd;
    if (!this._connectionInfoToDataProviderTable.TryGetValue(key, out orAdd))
    {
      IDbDataProvider dbDataProvider = this._dataProviderCreatorService.Create(dataProviderName);
      orAdd = this._connectionInfoToDataProviderTable.GetOrAdd(key, dbDataProvider);
      if (dbDataProvider != orAdd)
        DisposeUtils.TryDispose((object) dbDataProvider);
    }
    return orAdd;
  }

  private void AddToActiveManagers(Intermech.Server.Data.DbManager dbManager)
  {
    lock (this._activeManagers)
      this._activeManagers.Add(dbManager);
    dbManager.Disposed += new EventHandler(this.RemoveFromActiveManagers);
  }

  private void RemoveFromActiveManagers(object sender, EventArgs e)
  {
    Intermech.Server.Data.DbManager dbManager = (Intermech.Server.Data.DbManager) sender;
    lock (this._activeManagers)
      this._activeManagers.Remove(dbManager);
    dbManager.Disposed -= new EventHandler(this.RemoveFromActiveManagers);
  }

  public ICollection<IDbManagerStatus> GetActiveDbManagers()
  {
    lock (this._activeManagers)
      return (ICollection<IDbManagerStatus>) this._activeManagers.ToArray();
  }
}
