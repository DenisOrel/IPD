// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbManagerCommandData
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Server.Data;

public class DbManagerCommandData
{
  public DbManagerCommandData(
    bool scalarMode,
    CommandType commandType,
    string commandText,
    IDbDataParameter[] commandParameters)
  {
    if (commandText == null)
      throw new ArgumentNullException(nameof (commandText));
    this.ScalarMode = scalarMode;
    this.CommandType = commandType;
    this.CommandText = commandText;
    this.CommandParameters = commandParameters;
  }

  public bool ScalarMode { get; }

  public CommandType CommandType { get; }

  public string CommandText { get; }

  public IDbDataParameter[] CommandParameters { get; }
}
