// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPIntermediateTaskResult
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.MRP.Server;

internal sealed class MRPIntermediateTaskResult : IAssignable, ICloneable
{
  private Guid actionsID;
  public volatile Exception Exception;
  public volatile LinkedList<IMRPAction> Actions = new LinkedList<IMRPAction>();

  public Guid ActionsID
  {
    [DebuggerStepThrough] get => this.actionsID;
  }

  public MRPIntermediateTaskResult() => this.actionsID = Guid.NewGuid();

  public MRPIntermediateTaskResult(object source) => this.Assign(source);

  public MRPIntermediateTaskResult(Guid actionsID) => this.actionsID = actionsID;

  public MRPIntermediateTaskResult(Guid actionsID, Exception e)
  {
    this.actionsID = actionsID;
    this.Exception = e;
  }

  public void Clear()
  {
    lock (this.Actions)
      this.Actions.Clear();
    this.Exception = (Exception) null;
  }

  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is MRPIntermediateTaskResult intermediateTaskResult))
      return;
    this.MergeWith(intermediateTaskResult.Actions);
    this.actionsID = intermediateTaskResult.ActionsID;
    this.Exception = intermediateTaskResult.Exception;
  }

  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  public void MergeWith(IMRPCompositionTask source)
  {
    if (source == null)
      return;
    this.MergeWith(source.Actions);
  }

  public void MergeWith(LinkedList<IMRPAction> source)
  {
    if (source == null || source.Count == 0)
      return;
    lock (this.Actions)
    {
      foreach (IMRPAction mrpAction in source)
        this.Actions.AddLast(mrpAction);
    }
  }

  public void SetResult(IServerSession serverSession)
  {
    if (serverSession == null)
      throw new ArgumentNullException(nameof (serverSession));
    serverSession.SetSessionPluginsData((object) this.ActionsID, this.Clone());
  }

  public static MRPIntermediateTaskResult GetResult(
    IServerSession serverSession,
    Guid actionsID,
    bool autoCreate)
  {
    if (serverSession == null)
      throw new ArgumentNullException(nameof (serverSession));
    if (serverSession.GetSessionPluginsData((object) actionsID) is MRPIntermediateTaskResult sessionPluginsData || !autoCreate)
      return sessionPluginsData;
    MRPIntermediateTaskResult result = new MRPIntermediateTaskResult(actionsID);
    serverSession.SetSessionPluginsData((object) result.ActionsID, (object) result);
    return result;
  }

  public static void RemoveResult(IServerSession serverSession, Guid actionsID)
  {
    if (serverSession == null)
      throw new ArgumentNullException(nameof (serverSession));
    serverSession.RemoveSessionPluginsData((object) actionsID);
  }
}
