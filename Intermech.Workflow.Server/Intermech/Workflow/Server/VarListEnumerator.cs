// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.VarListEnumerator
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Server;

internal class VarListEnumerator : 
  MarshalByRefObject,
  IEnumerator<IVariable>,
  IDisposable,
  IEnumerator
{
  private IEnumerator<Variable> _enumerator;

  public VarListEnumerator(IEnumerator<Variable> en) => this._enumerator = en;

  public IVariable Current
  {
    get
    {
      Variable current = this._enumerator.Current;
      return current != null ? (IVariable) new RemotingVariable(current) : (IVariable) null;
    }
  }

  public void Dispose() => this._enumerator.Dispose();

  object IEnumerator.Current => (object) this.Current;

  public bool MoveNext() => this._enumerator.MoveNext();

  public void Reset() => this._enumerator.Reset();
}
