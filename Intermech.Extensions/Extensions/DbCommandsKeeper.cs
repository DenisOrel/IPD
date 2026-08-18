// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DbCommandsKeeper
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Extensions;

public class DbCommandsKeeper : IDisposable
{
  [NotNull]
  private IDbConnection _dbConnection;
  [NotNull]
  private readonly List<IDbCommand> _dbCommandsList = new List<IDbCommand>();

  public DbCommandsKeeper([NotNull] IDbConnection dbConnection)
  {
    this._dbConnection = dbConnection;
  }

  public void Dispose()
  {
    foreach (IDisposable dbCommands in this._dbCommandsList)
      dbCommands.Dispose();
    this._dbConnection = (IDbConnection) null;
  }

  [NotNull]
  public IDbCommandEx Add([NotNull] string sql, bool autoCreateParams = true)
  {
    IDbCommandEx dbCommandEx = (IDbCommandEx) new DbCommandEx(this._dbConnection.CreateCommand());
    if (autoCreateParams)
    {
      foreach (Match match in Regex.Matches(sql, ":\\w+"))
      {
        IDbDataParameter parameter = dbCommandEx.CreateParameter();
        parameter.ParameterName = match.Value.Substring(1);
        dbCommandEx.Parameters.Add((object) parameter);
      }
    }
    dbCommandEx.CommandText = sql;
    this._dbCommandsList.Add((IDbCommand) dbCommandEx);
    return dbCommandEx;
  }
}
