// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.Removed_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события Removed</summary>
public class Removed_EventArgs : EventArgs
{
  /// <summary>Удален для перемещения</summary>
  public bool RemovedByShift = true;
  /// <summary>Удаленный узел</summary>
  public DocumentTreeNode Node;
  /// <summary>Родительский узел из которого удалили</summary>
  public DocumentTreeNode ParentNode;

  /// <summary>Конструктор</summary>
  /// <param name="node">Удаленный узел</param>
  /// <param name="parentNode">Родительский узел из которого удалили</param>
  /// <param name="removedByShift">Удален для перемещения</param>
  public Removed_EventArgs(DocumentTreeNode node, DocumentTreeNode parentNode, bool removedByShift)
  {
    this.RemovedByShift = removedByShift;
    this.Node = node;
    this.ParentNode = parentNode;
  }
}
