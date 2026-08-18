// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.RemotingVarList
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Server;

public class RemotingVarList : MarshalByRefObject, IVariables, IEnumerable, IEnumerable<IVariable>
{
  private VarList _varlist;

  public RemotingVarList(VarList varlist) => this._varlist = varlist;

  public int Count => this._varlist.Count;

  public IVariable this[int index] => (IVariable) new RemotingVariable(this._varlist[index]);

  public IVariable Find(string name)
  {
    Variable variable = this._varlist.GetVariable(name);
    return variable != null ? (IVariable) new RemotingVariable(variable) : (IVariable) null;
  }

  public IVariable this[string name]
  {
    get => this.Find(name) ?? throw new Exception($"Переменная \"{name}\" не найдена!");
  }

  public IEnumerator<IVariable> GetEnumerator()
  {
    return (IEnumerator<IVariable>) new VarListEnumerator(this._varlist.GetEnumerator());
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new VarListEnumerator(this._varlist.GetEnumerator());
  }
}
