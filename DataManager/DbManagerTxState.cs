// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbManagerTxState
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Server.Data;

internal sealed class DbManagerTxState : IDbManagerTransactionState
{
  private readonly DbManager dbManager;
  private readonly WeakReference<DbManagerTxData> txDataRef;
  private readonly int txDepth;
  private bool isRestored;

  public DbManagerTxState(DbManager dbManager, DbManagerTxData txData, int txDepth)
  {
    this.dbManager = dbManager;
    this.txDataRef = new WeakReference<DbManagerTxData>(txData);
    this.txDepth = txDepth;
  }

  public DbManager DbManager
  {
    [DebuggerStepThrough] get => this.dbManager;
  }

  public DbManagerTxData TransactionData
  {
    [DebuggerStepThrough] get
    {
      DbManagerTxData target;
      return !this.txDataRef.TryGetTarget(out target) ? (DbManagerTxData) null : target;
    }
  }

  public int TransactionDepth
  {
    [DebuggerStepThrough] get => this.txDepth;
  }

  public void Restore()
  {
    if (this.isRestored)
      return;
    this.isRestored = true;
    this.dbManager.RestoreTransactionState(this);
  }
}
