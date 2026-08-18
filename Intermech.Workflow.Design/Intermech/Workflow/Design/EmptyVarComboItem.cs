// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.EmptyVarComboItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

#nullable disable
namespace Intermech.Workflow.Design;

internal class EmptyVarComboItem
{
  private string _itemName = string.Empty;

  public EmptyVarComboItem(string name) => this._itemName = name;

  public override string ToString() => this._itemName;
}
