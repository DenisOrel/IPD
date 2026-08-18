// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.RemotingAttachment
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

public class RemotingAttachment : MarshalByRefObject, IAttachment
{
  private Attachment _att;
  private IUserSession _session;

  public RemotingAttachment(Attachment att, IUserSession session)
  {
    this._att = att;
    this._session = session;
  }

  public long ObjectID => this._att.ObjectID;

  public int ObjectType => this._att.TypeID;

  public IDBObject Object
  {
    get => this._session != null ? this._session.GetObject(this.ObjectID, false) : (IDBObject) null;
  }
}
