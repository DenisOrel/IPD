// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WFAttachmentRelationCollection
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

public class WFAttachmentRelationCollection : 
  DBRelationCollection,
  IWFAttachmentRelationCollection,
  IDBRelationCollection,
  IDBRecords,
  IDBSessionable,
  IDBAttributableCollection
{
  private bool _checkAttachment = true;

  public WFAttachmentRelationCollection(UserSession userSession, int relationTypeID)
    : base(userSession, relationTypeID)
  {
  }

  public WFAttachmentRelationCollection(
    UserSession userSession,
    int relationTypeID,
    string filtrationOwnerID)
    : base(userSession, relationTypeID, filtrationOwnerID)
  {
  }

  public override IDBRelation Create(
    DateTime beginDate,
    long projectID,
    long partID,
    long prjlinkID,
    long partObjectID,
    IDBRelation prototype,
    Guid relationGUID,
    AttributeValues[] vals = null)
  {
    if (!this.CheckAttachment || !(this.UserSession.GetObject(projectID, false) is WFActivity wfActivity) || !wfActivity.Flags.HasFlag((Enum) ActivityFlags.DenyAttach))
      return base.Create(beginDate, projectID, partID, prjlinkID, partObjectID, prototype, relationGUID, vals);
    if (wfActivity.Flags.HasFlag((Enum) ActivityFlags.AllowAdminAttach) && this.UserSession.IsAdmin)
      return base.Create(beginDate, projectID, partID, prjlinkID, partObjectID, prototype, relationGUID, vals);
    if (wfActivity.Flags.HasFlag((Enum) ActivityFlags.AllowSystemAttach) && this.UserSession.IsSystemSession)
      return base.Create(beginDate, projectID, partID, prjlinkID, partObjectID, prototype, relationGUID, vals);
    throw new KernelException("Добавление вложений запрещено настройками маршрутизатора!");
  }

  public IDBRelation Create(long projectID, long partObjectID, bool checkAttachment)
  {
    this.CheckAttachment = checkAttachment;
    return this.Create(projectID, partObjectID);
  }

  public bool CheckAttachment
  {
    get => this._checkAttachment;
    set => this._checkAttachment = value;
  }
}
