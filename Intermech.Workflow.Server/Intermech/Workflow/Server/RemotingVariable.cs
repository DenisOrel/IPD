// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.RemotingVariable
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces.Workflow;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

public class RemotingVariable : MarshalByRefObject, IVariable
{
  private Variable _var;

  public RemotingVariable(Variable var) => this._var = var;

  public string Name => this._var.Name;

  public VarType Type => this._var.VarType;

  public string Value
  {
    get => this._var.Value;
    set => this._var.Value = value;
  }

  public object TypedValue
  {
    get => this._var.TypedValue;
    set => this._var.TypedValue = value;
  }

  public int AttributeID => this._var.AttrTypeID;
}
