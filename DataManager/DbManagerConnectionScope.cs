// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbManagerConnectionScope
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Server.Data;

internal struct DbManagerConnectionScope(
  DbManager dbManager,
  bool needCloseConnection,
  bool isChecked) : IDisposable
{
  private readonly DbManager dbManager = dbManager;
  private DbManagerConnectionScope.InternalStateFlags flags = DbManagerConnectionScope.InternalStateFlags.None;

  public DbManager DbManager
  {
    [DebuggerStepThrough] get => this.dbManager;
  }

  public bool NeedCloseConnection
  {
    [DebuggerStepThrough] get
    {
      return this.IsFlagSet(DbManagerConnectionScope.InternalStateFlags.NeedCloseConnection);
    }
    private set
    {
      this.SetFlag(DbManagerConnectionScope.InternalStateFlags.NeedCloseConnection, value);
    }
  }

  public bool IsCheched
  {
    [DebuggerStepThrough] get
    {
      return this.IsFlagSet(DbManagerConnectionScope.InternalStateFlags.IsChecked);
    }
    private set => this.SetFlag(DbManagerConnectionScope.InternalStateFlags.IsChecked, value);
  }

  public bool IsDisposed
  {
    [DebuggerStepThrough] get
    {
      return (this.flags & DbManagerConnectionScope.InternalStateFlags.IsDisposed) != 0;
    }
    private set => this.SetFlag(DbManagerConnectionScope.InternalStateFlags.IsDisposed, value);
  }

  public void Dispose()
  {
    if (this.IsDisposed)
      return;
    this.IsDisposed = true;
    if (!this.NeedCloseConnection)
      return;
    if (this.IsCheched)
      this.dbManager.DisposeOpenConnectionScope();
    else
      this.dbManager.DisposeOpenConnectionScopeInternal();
  }

  private bool IsFlagSet(DbManagerConnectionScope.InternalStateFlags flag)
  {
    return (this.flags & flag) != 0;
  }

  private void SetFlag(DbManagerConnectionScope.InternalStateFlags flag, bool setValue)
  {
    if (setValue)
      this.flags |= flag;
    else
      this.flags &= ~flag;
  }

  [Flags]
  private enum InternalStateFlags
  {
    None = 0,
    IsChecked = 1,
    IsDisposed = 2,
    NeedCloseConnection = 4,
  }
}
