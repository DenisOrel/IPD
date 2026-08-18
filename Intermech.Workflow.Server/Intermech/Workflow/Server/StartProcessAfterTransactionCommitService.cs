// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.StartProcessAfterTransactionCommitService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Server;

internal class StartProcessAfterTransactionCommitService : LongLifeObject, IDelayProcessStarter
{
  private Dictionary<Guid, HashSet<long>> _processQueue = new Dictionary<Guid, HashSet<long>>();
  private IEventLogHelper _eventLogHelper;

  public StartProcessAfterTransactionCommitService(IEventLogHelper eventLogHelper)
  {
    this._eventLogHelper = eventLogHelper ?? throw new ArgumentNullException();
    this._eventLogHelper.AfterCommitCreationObjectEvent += new ObjectEventHandler(this.EventLogHelper_AfterCommitCreationObjectEvent);
    this._eventLogHelper.RollbackEvent += new TransactionHandler(this.EventLogHelper_RollbackEvent);
  }

  public void AddProcessToQueue(Guid userSessionGuid, long processID)
  {
    if (this._processQueue.ContainsKey(userSessionGuid))
      this._processQueue[userSessionGuid].Add(processID);
    else
      this._processQueue.Add(userSessionGuid, new HashSet<long>()
      {
        processID
      });
  }

  private void EventLogHelper_RollbackEvent(IUserSession session)
  {
    if (!this._processQueue.ContainsKey(session.SessionGUID))
      return;
    this._processQueue.Remove(session.SessionGUID);
  }

  private void EventLogHelper_AfterCommitCreationObjectEvent(IDBObject sender, IUserSession session)
  {
    if (!this._processQueue.ContainsKey(session.SessionGUID))
      return;
    foreach (long objectID in this._processQueue[session.SessionGUID])
    {
      if (session.GetObject(objectID, false) is WFProcess wfProcess)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual (wfProcess.StartProcess());
      }
    }
    this._processQueue.Remove(session.SessionGUID);
  }
}
