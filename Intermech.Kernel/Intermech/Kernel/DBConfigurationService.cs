// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBConfigurationService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using System.Collections.Concurrent;
using System.Data;


namespace Intermech.Kernel;

public class DBConfigurationService : IDBConfigurationService
{
  private ConcurrentDictionary<ConfigSectKey, ConfigSectParams> _CommonConfigCache;
  private DataTable _ConfigTable;
  private IKernelCacheSynchronizer _KernelSynchronizer;

  public DBConfigurationService(IDbManager db) => this.ReloadCache(db);

  private IKernelCacheSynchronizer KernelSynchronizer
  {
    get
    {
      if (this._KernelSynchronizer == null)
        this._KernelSynchronizer = ServerServices.GetService(typeof (IKernelCacheSynchronizer)) as IKernelCacheSynchronizer;
      return this._KernelSynchronizer;
    }
  }

  private void ReloadCache(IDbManager db)
  {
    DataTable dataTable = db.ExecuteDataTable("SELECT * FROM IMS_CONFIGS WHERE F_USER_ID = 0 ORDER BY F_MODULE_NAME, F_SECTION_ID");
    ConcurrentDictionary<ConfigSectKey, ConfigSectParams> concurrentDictionary = new ConcurrentDictionary<ConfigSectKey, ConfigSectParams>();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    ConfigSectKey key = (ConfigSectKey) null;
    ConfigSectParams configSectParams = (ConfigSectParams) null;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (empty1 != dataTable.Rows[index]["F_MODULE_NAME"].ToString() || empty2 != dataTable.Rows[index]["F_SECTION_ID"].ToString())
      {
        if (key != null)
          concurrentDictionary[key] = configSectParams;
        key = new ConfigSectKey(dataTable.Rows[index]["F_MODULE_NAME"].ToString(), dataTable.Rows[index]["F_SECTION_ID"].ToString());
        configSectParams = new ConfigSectParams();
        empty1 = dataTable.Rows[index]["F_MODULE_NAME"].ToString();
        empty2 = dataTable.Rows[index]["F_SECTION_ID"].ToString();
      }
      configSectParams.AddValue(dataTable.Rows[index]["F_PARAM_NAME"].ToString(), dataTable.Rows[index]["F_VALUE"]);
    }
    concurrentDictionary[key] = configSectParams;
    this._CommonConfigCache = concurrentDictionary;
    this._ConfigTable = db.ExecuteDataTable("SELECT F_PARAM_NAME, F_VALUE FROM IMS_CONFIGS WHERE F_USER_ID = -100");
  }

  public IDBConfigurations GetDBConfigurations(IUserSession uSession)
  {
    return (IDBConfigurations) new DBConfigurations(uSession as UserSession, this);
  }

  public IDBConfigurations GetDBConfigurations(
    IUserSession uSession,
    IDBConfigurations parentConfigurations)
  {
    return (IDBConfigurations) new DBConfigurations(uSession as UserSession, (parentConfigurations as DBConfigurations).GetConfigCache(), this);
  }

  public object GetValue(
    string moduleName,
    string sectionName,
    string paramName,
    object defaultValue)
  {
    return this.GetValue(moduleName, sectionName, paramName) ?? defaultValue;
  }

  public object GetValue(string moduleName, string sectionName, string paramName)
  {
    ConfigSectParams configSectParams;
    return this._CommonConfigCache.TryGetValue(new ConfigSectKey(moduleName, sectionName), out configSectParams) ? configSectParams.GetValue(paramName) : (object) null;
  }

  public void SetValue(
    string moduleName,
    string sectionName,
    string paramName,
    object value,
    IDbManager db)
  {
    ConfigSectKey key = new ConfigSectKey(moduleName, sectionName);
    ConfigSectParams configSectParams1;
    if (this._CommonConfigCache.TryGetValue(key, out configSectParams1))
    {
      configSectParams1.AddValue(paramName, value);
    }
    else
    {
      ConfigSectParams configSectParams2 = new ConfigSectParams();
      configSectParams2.AddValue(paramName, value);
      this._CommonConfigCache[key] = configSectParams2;
    }
    if (db == null || this.KernelSynchronizer == null || moduleName.IndexOf(';') >= 0 || sectionName.IndexOf(';') >= 0 || paramName.IndexOf(';') >= 0)
      return;
    this.KernelSynchronizer.AddEvent($"1;{moduleName};{sectionName};{paramName}", db);
  }

  public void ReloadValue(string moduleName, string sectionName, string paramName, IDbManager db)
  {
    object obj = db.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = 0 AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", db.Parameter(nameof (moduleName), (object) moduleName), db.Parameter("sectID", (object) sectionName), db.Parameter("parName", (object) paramName));
    if (obj == null)
      return;
    this.SetValue(moduleName, sectionName, paramName, obj, (IDbManager) null);
  }

  public void WriteSection(string moduleName, string sectionName, DataTable table)
  {
    ConfigSectKey key = new ConfigSectKey(moduleName, sectionName);
    ConfigSectParams configSectParams = new ConfigSectParams();
    for (int index = 0; index < table.Rows.Count; ++index)
      configSectParams.AddValue(table.Rows[index][0].ToString(), table.Rows[index][1]);
    this._CommonConfigCache[key] = configSectParams;
  }

  public DataTable ReadSection(string moduleName, string sectionName)
  {
    DataTable tbl = this._ConfigTable.Clone();
    ConfigSectParams configSectParams;
    if (this._CommonConfigCache.TryGetValue(new ConfigSectKey(moduleName, sectionName), out configSectParams))
      configSectParams.FillDataTable(tbl);
    return tbl;
  }

  public void ClearSection(string moduleName, string sectionName)
  {
    ConfigSectParams configSectParams;
    if (!this._CommonConfigCache.TryGetValue(new ConfigSectKey(moduleName, sectionName), out configSectParams))
      return;
    configSectParams.Clear();
  }

  public void DeleteValue(string moduleName, string sectionName, string paramName)
  {
    ConfigSectParams configSectParams;
    if (!this._CommonConfigCache.TryGetValue(new ConfigSectKey(moduleName, sectionName), out configSectParams))
      return;
    configSectParams.DeleteValue(paramName);
  }
}
