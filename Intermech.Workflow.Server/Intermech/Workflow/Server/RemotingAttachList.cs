// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.RemotingAttachList
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Server;

internal class RemotingAttachList : 
  MarshalByRefObject,
  IAttachments,
  IEnumerable<IAttachment>,
  IEnumerable
{
  private AttachmentList _attlist;
  private IUserSession _session;
  private AttachEventHandler _onChange;

  public RemotingAttachList(
    AttachmentList attlist,
    IUserSession session,
    AttachEventHandler onChange)
  {
    this._attlist = attlist;
    this._session = session;
    this._onChange = onChange;
  }

  public int Count => this._attlist.Count;

  public IAttachment this[int index]
  {
    get => (IAttachment) new RemotingAttachment(this._attlist[index], this._session);
  }

  public IAttachment Find(long objectid)
  {
    foreach (Attachment att in (List<Attachment>) this._attlist)
    {
      if (att.ObjectID == objectid)
        return (IAttachment) new RemotingAttachment(att, this._session);
    }
    return (IAttachment) null;
  }

  public int Add(long objectid)
  {
    Attachment attach = this._attlist.AddAttachment(objectid);
    if (this._onChange != null)
      this._onChange((object) this, attach);
    return this._attlist.Count - 1;
  }

  public void RemoveAt(int index)
  {
    this._attlist.RemoveAt(index);
    if (this._onChange == null)
      return;
    this._onChange((object) this, (Attachment) null);
  }

  public IEnumerator<IAttachment> GetEnumerator()
  {
    return (IEnumerator<IAttachment>) new AttachListEnumerator((IEnumerator<Attachment>) this._attlist.GetEnumerator(), this._session);
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new AttachListEnumerator((IEnumerator<Attachment>) this._attlist.GetEnumerator(), this._session);
  }
}
