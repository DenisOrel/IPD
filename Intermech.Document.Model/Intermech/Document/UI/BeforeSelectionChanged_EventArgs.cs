// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.BeforeSelectionChanged_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Аргументы события BeforeAddChildNode</summary>
public class BeforeSelectionChanged_EventArgs : EventArgs
{
  /// <summary>Добавляемый узел</summary>
  public List<DocumentTreeNode> Selection;

  /// <summary>Конструктор</summary>
  /// <param name="child">Добавляемый узел</param>
  public BeforeSelectionChanged_EventArgs(List<DocumentTreeNode> Selection)
  {
    this.Selection = Selection;
  }
}
