// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.IDComboItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;

#nullable disable
namespace Intermech.Workflow.Design;

[Serializable]
public class IDComboItem : ComboBoxExItem
{
  public long ID;
  public object Data;

  public IDComboItem(string name, long id, int imageindex)
    : base(name, imageindex)
  {
    this.ID = id;
  }
}
