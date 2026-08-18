// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.DBMailObject
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class DBMailObject(UserSession uSession, DataTable objectsTable) : 
  DBObject(uSession, objectsTable),
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public virtual void SetDeletionStatus(MailFolder folder, DeletionStatus status)
  {
    int attributeID = wfConsts.AttrRecipDeletionID;
    bool flag = false;
    switch (folder)
    {
      case MailFolder.Outbox:
        attributeID = wfConsts.AttrSenderDeletionID;
        break;
      case MailFolder.Deleted:
        if (status == DeletionStatus.Deleted)
          status = DeletionStatus.CompletelyDeleted;
        flag = true;
        break;
    }
    if (!flag)
    {
      if (attributeID == 0)
        return;
      this.GetAttributeByID(attributeID).AsInteger = (long) status;
    }
    else
    {
      long userId = this.Session.UserID;
      IDBAttribute attributeById1 = this.GetAttributeByID(wfConsts.AttrRecipID);
      if (attributeById1 != null && attributeById1.AsInteger == userId)
        this.GetAttributeByID(wfConsts.AttrRecipDeletionID).AsInteger = (long) status;
      IDBAttribute attributeById2 = this.GetAttributeByID(wfConsts.AttrSenderID);
      if (attributeById2 == null || attributeById2.AsInteger != userId)
        return;
      this.GetAttributeByID(wfConsts.AttrSenderDeletionID).AsInteger = (long) status;
    }
  }
}
