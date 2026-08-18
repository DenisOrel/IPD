// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbManagerTxData
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Server.Data;

internal sealed class DbManagerTxData
{
  private readonly IDbTransaction _transaction;
  private readonly DbManagerConnectionScope _connectionScope;
  private int _depth;

  public DbManagerTxData(IDbTransaction transaction, DbManagerConnectionScope connectionScope)
  {
    this._transaction = transaction;
    this._connectionScope = connectionScope;
    this._depth = 1;
  }

  public IDbTransaction Transaction
  {
    [DebuggerStepThrough] get => this._transaction;
  }

  public DbManagerConnectionScope ConnectionScope
  {
    [DebuggerStepThrough] get => this._connectionScope;
  }

  public int Depth
  {
    [DebuggerStepThrough] get => this._depth;
    set => this._depth = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof (value));
  }

  public void IncDepth() => ++this._depth;

  public void DecDepth()
  {
    if (this._depth <= 1)
      throw new InvalidOperationException();
    --this._depth;
  }
}
