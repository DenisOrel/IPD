// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.ExecuteSpStrategy
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System;
using System.Data;

#nullable disable
namespace Intermech.Server.Data;

public abstract class ExecuteSpStrategy(IDbManager dbManager) : DBManagerSqlStrategy(dbManager)
{
  public DbManagerCommandData CreateCommandData(
    string spName,
    params IDbDataParameter[] spParameters)
  {
    return spName != null ? this.DoCreateCommandData(spName, spParameters) : throw new ArgumentNullException(nameof (spName));
  }

  public void ProcessCommandResult(
    DbManagerCommandData commandData,
    DbManagerCommandResult commandResult)
  {
    if (commandData == null)
      throw new ArgumentNullException(nameof (commandData));
    if (commandResult == null)
      throw new ArgumentNullException(nameof (commandResult));
    this.DoProcessCommandResult(commandData, commandResult);
  }

  protected abstract DbManagerCommandData DoCreateCommandData(
    string spName,
    IDbDataParameter[] spParameters);

  protected abstract void DoProcessCommandResult(
    DbManagerCommandData commandData,
    DbManagerCommandResult commandResult);
}
