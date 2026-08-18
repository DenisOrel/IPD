// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.ConnectionStringService
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Configuration;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Server.Data;

public sealed class ConnectionStringService : IConnectionStringService
{
  private ConcurrentDictionary<string, string> nameToConnectionStringTable;
  private Func<string, string> getConnectionStringFunc;
  private Lazy<string> defaultConnectionNameCache;
  private Lazy<string> defaultConnectionStringCache;

  public ConnectionStringService()
  {
    this.nameToConnectionStringTable = new ConcurrentDictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.getConnectionStringFunc = new Func<string, string>(this.GetConnectionStringSlow);
    this.defaultConnectionNameCache = new Lazy<string>(new Func<string>(this.GetDefaultConnectionName), LazyThreadSafetyMode.PublicationOnly);
    this.defaultConnectionStringCache = new Lazy<string>(new Func<string>(this.GetDefaultConnectionString), LazyThreadSafetyMode.PublicationOnly);
  }

  public string DefaultConnectionName
  {
    [DebuggerStepThrough] get => this.defaultConnectionNameCache.Value;
  }

  public string DefaultConnectionString
  {
    [DebuggerStepThrough] get => this.defaultConnectionStringCache.Value;
  }

  public string GetConnectionString(string connectionName)
  {
    if (connectionName == null)
      throw new ArgumentNullException(nameof (connectionName));
    return this.nameToConnectionStringTable.GetOrAdd(connectionName, this.getConnectionStringFunc);
  }

  private string GetConnectionStringSlow(string connectionName)
  {
    string key = $"ConnectionString{(connectionName.Length == 0 ? (object) "" : (object) ".")}{connectionName}";
    string connectionStringSlow = AppSettingsHelper.GetString(key, (string) null);
    if (connectionStringSlow == null)
      throw new Exception($"The '{key}' key does not exist in the configuration file.");
    object obj = (object) ConfigurationManager.AppSettings.Get("UsePassword");
    if (obj != null && ((string) obj).ToLower() == "true")
    {
      string str1 = ConfigurationManager.AppSettings.Get("User ID");
      string str2 = Cryptor.Decrypt(ConfigurationManager.AppSettings.Get("Password"), "cad00016-306c-11d8-b4e9-00304f19f545");
      connectionStringSlow = $"{connectionStringSlow};User ID={str1};Password={str2}";
    }
    if (!connectionStringSlow.ToLower().Contains("enlist="))
      connectionStringSlow = $"{connectionStringSlow};Enlist=false";
    if (key.ToLower().Contains(".oracle") && !connectionStringSlow.ToLower().Contains("metadata pooling="))
      connectionStringSlow = $"{connectionStringSlow};Metadata Pooling=true";
    return connectionStringSlow;
  }

  private string GetDefaultConnectionName()
  {
    string str = ConfigurationManager.AppSettings.Get("ConnectionName");
    return string.IsNullOrEmpty(str) ? string.Empty : str.Trim();
  }

  private string GetDefaultConnectionString()
  {
    return this.GetConnectionString(this.DefaultConnectionName);
  }
}
