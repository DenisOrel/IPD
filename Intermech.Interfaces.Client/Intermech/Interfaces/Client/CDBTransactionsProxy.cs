// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CDBTransactionsProxy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует клиентскую обертку для серверного сервиса IDBTransactions.
/// </summary>
/// <remarks>
/// Создание обертки стало возможно благодаря тому, что IDBTransactions не является реальным серверным сервисом,
/// а реализуется самой пользовательской сессией.
/// </remarks>
internal sealed class CDBTransactionsProxy : ClientSessionObjectProxy, IDBTransactions
{
  private IDBTransactions _serverObject;

  public CDBTransactionsProxy(ClientSession clientSession, IDBTransactions serverObject)
    : base(clientSession, (MarshalByRefObject) serverObject)
  {
    this._serverObject = serverObject;
  }

  /// <summary>Возвращает необернутый серверный объект.</summary>
  private IDBTransactions ServerObject
  {
    [DebuggerStepThrough] get => this._serverObject;
  }

  public void StartTransaction()
  {
    this.ClientSession.Guard.ValidateCall();
    this.ServerObject.StartTransaction();
  }

  public void Commit() => this.ServerObject.Commit();

  public void Rollback() => this.ServerObject.Rollback();

  public bool InTransaction => this.ServerObject.InTransaction;

  public void StartCreationLog()
  {
    this.ClientSession.Guard.ValidateCall();
    this.ServerObject.StartCreationLog();
  }

  public void CommitCreationLog()
  {
    this.ClientSession.Guard.ValidateCall();
    this.ServerObject.CommitCreationLog();
  }

  public void RollBackCreationLog()
  {
    this.ClientSession.Guard.ValidateCall();
    this.ServerObject.RollBackCreationLog();
  }

  public void RollBackCreationLog(long[] purgeList)
  {
    this.ClientSession.Guard.ValidateCall();
    this.ServerObject.RollBackCreationLog(purgeList);
  }

  public bool InCreationLogMode
  {
    get
    {
      this.ClientSession.Guard.ValidateCall();
      return this.ServerObject.InCreationLogMode;
    }
  }

  public void SuspendCreationLog()
  {
    this.ClientSession.Guard.ValidateCall();
    this.ServerObject.SuspendCreationLog();
  }

  public void ResumeCreationLog()
  {
    this.ClientSession.Guard.ValidateCall();
    this.ServerObject.ResumeCreationLog();
  }

  public CategoryValue[] GetCreationLog()
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.GetCreationLog();
  }

  public bool AutoRollback
  {
    get => this.ServerObject.AutoRollback;
    set
    {
      this.ClientSession.Guard.ValidateCall();
      this.ServerObject.AutoRollback = value;
    }
  }
}
