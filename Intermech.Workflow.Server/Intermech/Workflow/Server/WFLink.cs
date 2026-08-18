// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WFLink
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Workflow.Server.Activities;
using System;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class WFLink : DBObject
{
  private WFActivity _from;
  private WFActivity _to;
  public LinkKind Kind;
  private long _fromID = -1;
  private long _toID = -1;
  private bool _allowDeletion;
  internal int Index = -1;
  internal bool _inherited;
  public long OldObjectID;

  public long FromID
  {
    get => this._fromID;
    set => this._fromID = value;
  }

  public long ToID
  {
    get => this._toID;
    set => this._toID = value;
  }

  public WFActivity From
  {
    get
    {
      if (this._from == null && this._fromID != -1L)
        this._from = this.GetActivity(this._fromID);
      return this._from;
    }
  }

  public WFActivity To
  {
    get
    {
      if (this._to == null && this._toID != -1L)
        this._to = this.GetActivity(this._toID);
      return this._to;
    }
  }

  public WFLink(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  public WFLink(UserSession uSession)
    : base(uSession)
  {
  }

  private WFActivity GetActivity(long objectID)
  {
    return this.UserSession.GetObject(objectID) as WFActivity;
  }

  public long ProcessID
  {
    get => this.Attributes.FindByID(wfConsts.AttrProcessID).AsInteger;
    set => this.Attributes.FindByID(wfConsts.AttrProcessID).AsInteger = value;
  }

  protected override void DoDelete()
  {
    if (this._allowDeletion)
      return;
    long processId = this.ProcessID;
    if (processId == 0L)
      return;
    int objectLevel = this.UserSession.GetObjectLevel(processId);
    if (objectLevel != -1 && objectLevel != this.UserSession.IdentHelper.DeletedID)
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Server_14"));
  }

  internal void InternalDelete(bool allowDeletion)
  {
    if (this.Inherited)
      return;
    this._allowDeletion = allowDeletion;
    this.Delete(0L);
  }

  public bool Inherited => this._inherited;

  public override string ToString() => $"{base.ToString()} /{this.ObjectID.ToString()}";

  protected internal void Copied()
  {
    if (this.OldObjectID > 0L && this.From is Case from)
    {
      if (from.ExpertConditions == null || from.ExpertConditions.IsEmpty)
      {
        if (from.ExpressionConditions != null && from.ExpressionConditions.ReplaceLink(this.OldObjectID, this.ObjectID))
          from.SaveExpressionConditions();
      }
      else if (from.ExpertConditions.ReplaceLink(this.OldObjectID, this.ObjectID))
        from.SaveExpertConditions();
    }
    if (!(this.To is Timer to))
      return;
    to.ReplaceLink(this.OldObjectID, this.ObjectID);
  }

  internal bool IsDirect => this.Kind != LinkKind.ParallelBlock;

  protected override void DoBeforeCommitCreation()
  {
    if (this._fromID != -1L)
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrFromActivityID);
      if (attributeById != null)
        attributeById.AsInteger = Math.Abs(this._fromID);
    }
    if (this._toID != -1L)
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrToActivityID);
      if (attributeById != null)
        attributeById.AsInteger = Math.Abs(this._toID);
    }
    base.DoBeforeCommitCreation();
  }
}
