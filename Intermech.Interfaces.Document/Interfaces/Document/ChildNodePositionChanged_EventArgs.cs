// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ChildNodePositionChanged_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события ChildNodePositionChanged</summary>
public class ChildNodePositionChanged_EventArgs : EventArgs
{
  public DocumentTreeNode Node;
  /// <summary>Старый индекс дочернего узла</summary>
  public int OldIndex;
  /// <summary>Новый индекс дочернего узла</summary>
  public int NewIndex;
  public bool UpdateUI;

  /// <summary>Конструктор</summary>
  /// <param name="oldIndex">Индек первого узла</param>
  /// <param name="newIndex">Индек второго узла</param>
  public ChildNodePositionChanged_EventArgs(
    DocumentTreeNode node,
    int oldIndex,
    int newIndex,
    bool updateUI)
  {
    this.Node = node;
    this.OldIndex = oldIndex;
    this.NewIndex = newIndex;
    this.UpdateUI = updateUI;
  }
}
