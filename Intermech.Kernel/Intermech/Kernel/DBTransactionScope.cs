// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBTransactionScope
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel;

public sealed class DBTransactionScope : IDisposable
{
  private UserSession _Session;
  private bool _InTransaction;
  private DBTransactionState _State;
  private bool _TurnOffAutoRollback;
  private string _TransactionLabel;

  public DBTransactionScope(IUserSession session, string transactionLabel)
  {
    this._Session = session as UserSession;
    this._InTransaction = this._Session.InTransaction;
    this._Session.StartTransaction();
    this._State = DBTransactionState.Started;
    this._TransactionLabel = transactionLabel;
  }

  public void Commit()
  {
    this._Session.Commit();
    this._State = DBTransactionState.Commited;
  }

  public void Rollback()
  {
    this._Session.Rollback();
    this._State = DBTransactionState.RolledBack;
  }

  public bool InTransaction => this._Session.InTransaction;

  public bool AutoRollback
  {
    get => !this._Session.RollbackOff;
    set
    {
      this._TurnOffAutoRollback = !value;
      this._Session.RollbackOff = !value;
    }
  }

  public void Dispose()
  {
    if (this._TurnOffAutoRollback)
      throw new KernelException("Внимание! В контексте данной транзакции был выключен режим автоматического отката транзакций.");
    if (this._State != DBTransactionState.Started || !this._Session.InTransaction)
      return;
    this._Session.Rollback();
  }
}
