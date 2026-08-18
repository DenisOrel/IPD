// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DefaultExecuteSpStrategy
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System.Data;

#nullable disable
namespace Intermech.Server.Data;

public class DefaultExecuteSpStrategy(IDbManager dbManager) : ExecuteSpStrategy(dbManager)
{
  protected override DbManagerCommandData DoCreateCommandData(
    string spName,
    IDbDataParameter[] spParameters)
  {
    return new DbManagerCommandData(false, CommandType.StoredProcedure, spName, spParameters == null || spParameters.Length == 0 ? (IDbDataParameter[]) null : spParameters);
  }

  protected override void DoProcessCommandResult(
    DbManagerCommandData commandData,
    DbManagerCommandResult commandResult)
  {
  }
}
