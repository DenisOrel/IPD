// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.AttachListEnumerator
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

internal class AttachListEnumerator : 
  MarshalByRefObject,
  IEnumerator<IAttachment>,
  IDisposable,
  IEnumerator
{
  private IEnumerator<Attachment> _enumerator;
  private IUserSession _session;

  public AttachListEnumerator(IEnumerator<Attachment> en, IUserSession session)
  {
    this._enumerator = en;
    this._session = session;
  }

  public IAttachment Current
  {
    get
    {
      Attachment current = this._enumerator.Current;
      return current != null ? (IAttachment) new RemotingAttachment(current, this._session) : (IAttachment) null;
    }
  }

  public void Dispose() => this._enumerator.Dispose();

  object IEnumerator.Current => (object) this.Current;

  public bool MoveNext() => this._enumerator.MoveNext();

  public void Reset() => this._enumerator.Reset();
}
