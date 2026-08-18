// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Register
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Archives.Server;
using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class Register : SystemActivity
{
  private ArchiveService archiveService;
  private string archiveError = string.Empty;
  private string _err = string.Empty;
  private List<long> _succeeded = new List<long>();

  public Register(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this.archiveService = ApplicationServices.Container.GetService(typeof (IArchiveService)) as ArchiveService;
  }

  public override ActivityKind Kind => ActivityKind.Register;

  private Guid GetArchiveGuid(int attrID)
  {
    IDBAttribute attributeById = this.GetAttributeByID(attrID);
    if (attributeById != null)
    {
      string asString = attributeById.AsString;
      if (!string.IsNullOrEmpty(asString))
      {
        Guid archiveGuid = new Guid(asString);
        if (this.UserSession.GetObject(archiveGuid, false) != null)
          return archiveGuid;
        IDBAttributeType attributeType = this.UserSession.GetAttributeType(archiveGuid, false);
        if (attributeType != null)
        {
          Variable variable = this.VariableList.GetVariable(attributeType.AttributeID);
          if (variable != null)
            return variable.AsGuid;
        }
      }
    }
    return Guid.Empty;
  }

  private Guid DocArchiveGuid => this.GetArchiveGuid(wfConsts.AttrDocArchiveID);

  private Guid RevArchiveGuid => this.GetArchiveGuid(wfConsts.AttrRevArchiveID);

  private bool SetArchiveID(IDBObject obj, long arcID)
  {
    if (arcID == 0L)
      return true;
    if (MetaDataHelper.GetAttribute4ObjectType(obj.ObjectType, wfConsts.AttrArchiveID) == null || this.archiveService == null || !this.archiveService.CanPlaceToArchive(this.UserSession.GetObject(arcID, false), obj, out this.archiveError))
      return false;
    return obj.Attributes.AddAttribute(wfConsts.AttrArchiveID, false, new object[1]
    {
      (object) arcID
    }) != null;
  }

  protected void ProcessAttachments(AttachmentList attachs, long docArcID, long revArcID)
  {
    foreach (Attachment attach in (List<Attachment>) attachs)
    {
      IDBObject dbObject = this.UserSession.GetObject(attach.ObjectID, false);
      if (dbObject != null)
      {
        try
        {
          int num = wfConsts.IsECO(dbObject.ObjectType) ? 1 : 0;
          long arcID = num != 0 ? revArcID : docArcID;
          if (this.SetArchiveID(dbObject, arcID))
            this._succeeded.Add(attach.ObjectID);
          if (!string.IsNullOrEmpty(this.archiveError))
          {
            this._err += "\r\n";
            this._err = $"{this._err}{dbObject.NameInMessages} : {this.archiveError}";
            this.archiveError = string.Empty;
          }
          if (num != 0)
          {
            if (attach.InnerList != null)
            {
              if (attach.InnerList.Count > 0)
                this.ProcessAttachments(attach.InnerList, docArcID, revArcID);
            }
          }
        }
        catch (Exception ex)
        {
          if (!string.IsNullOrEmpty(this._err))
            this._err += "\r\n";
          this._err += "\r\n";
          this._err = $"{this._err}{dbObject.NameInMessages} : {ex.Message}";
        }
      }
    }
  }

  internal override void PrepareActivity()
  {
    base.PrepareActivity();
    IDBObject dbObject1 = this.UserSession.GetObject(this.DocArchiveGuid, false);
    IDBObject dbObject2 = this.UserSession.GetObject(this.RevArchiveGuid, false);
    long objectId1 = dbObject1 != null ? dbObject1.ObjectID : 0L;
    long objectId2 = dbObject2 != null ? dbObject2.ObjectID : 0L;
    this._succeeded.Clear();
    this.ProcessAttachments(MiscFunx.ExpandAttachments((IUserSession) this.UserSession, this.Attachments), objectId1, objectId2);
    if (MiscFunx.IsFlagSet((IDBObject) this, ActivityFlags.DetachRegisteredObjects) && this._succeeded.Count > 0)
    {
      for (int index = this.Attachments.Count - 1; index >= 0; --index)
      {
        if (this._succeeded.Contains(this.Attachments[index].ObjectID))
          this.Attachments.RemoveAt(index);
      }
    }
    if (!string.IsNullOrEmpty(this._err))
      throw new WorkflowException(this._err);
    if (!MiscFunx.IsFlagSet((IDBObject) this, ActivityFlags.DetachRegisteredObjects) || this._succeeded.Count <= 0)
      return;
    this.Attachments.Save((IDBObject) this);
  }
}
