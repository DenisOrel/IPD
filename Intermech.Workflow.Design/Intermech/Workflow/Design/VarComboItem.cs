// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.VarComboItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;

#nullable disable
namespace Intermech.Workflow.Design;

internal class VarComboItem : IDComboItem
{
  public VarType Type;
  public VarKind VarKind;

  public VarComboItem(string name, long id, int imageindex, bool isGlobalVar)
    : base(name, id, imageindex)
  {
    this.VarKind = isGlobalVar ? VarKind.Global : VarKind.User;
  }

  public override bool Equals(object obj)
  {
    return obj is string ? this.Text.Equals(obj.ToString(), StringComparison.InvariantCultureIgnoreCase) : base.Equals(obj);
  }

  public override int GetHashCode() => base.GetHashCode();
}
