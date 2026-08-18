// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.RowSelection_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.UI;

public class RowSelection_EventArgs : EventArgs
{
  private List<DocumentTreeNode> nodes;
  private bool? rowSelection;

  public List<DocumentTreeNode> Nodes
  {
    get => this.nodes;
    set => this.nodes = value;
  }

  public bool? RowSelection
  {
    get => this.rowSelection;
    set => this.rowSelection = value;
  }
}
