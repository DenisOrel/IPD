// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbManagerConnectionInfo
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System.Data;

#nullable disable
namespace Intermech.Server.Data;

internal sealed class DbManagerConnectionInfo : IDbManagerConnectionInfo
{
  public DbManagerConnectionInfo(
    int id,
    string connectionString,
    ConnectionState connectionState,
    bool inTransaction,
    int transactionDepth)
  {
    this.ID = id;
    this.ConnectionString = connectionString;
    this.ConnectionState = connectionState;
    this.InTransaction = inTransaction;
    this.TransactionDepth = transactionDepth;
  }

  public int ID { get; private set; }

  public string ConnectionString { get; private set; }

  public ConnectionState ConnectionState { get; private set; }

  public bool InTransaction { get; private set; }

  public int TransactionDepth { get; private set; }
}
