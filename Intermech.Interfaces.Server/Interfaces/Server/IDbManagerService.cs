// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDbManagerService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDbManagerService
{
  [Obsolete("Use the method IDbManagerService.CreateDbManager() instead of this.", true)]
  IDbManager DbManager { get; }

  [Obsolete("Use the method CreateDbManager(string, string) instead of this.", true)]
  IDbDataProvider GetDataProviderByName(string providerName);

  IDbManager CreateDbManager();

  [Obsolete("Use the method CreateDbManager(string, string) instead of this.", true)]
  IDbManager CreateDbManager(IDbConnection dbConnection);

  IDbManager CreateDbManager(string providerName, string connectionString);

  string ConnectionName { get; }

  string ConnectionString { get; }

  ICollection<IDbManagerStatus> GetActiveDbManagers();
}
