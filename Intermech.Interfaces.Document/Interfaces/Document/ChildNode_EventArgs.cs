// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ChildNode_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события ChildNode</summary>
public class ChildNode_EventArgs : EventArgs
{
  /// <summary>Родительский элемент (при удалении, узел из которого был удалён дочерний узел)</summary>
  public DocumentTreeNode Parent;
  /// <summary>Дочерний узел</summary>
  public DocumentTreeNode Child;
  /// <summary>Индекс дочернего узла</summary>
  public int Index = -1;
  /// <summary>Перемещение узла</summary>
  public bool ByShift;
  /// <summary>Обновить элементы управления</summary>
  public bool UpdateUI;
  /// <summary>Обновить разбивку</summary>
  public bool UpdateLayout;

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский элемент (нужен при удалении)</param>
  /// <param name="child">Дочерний узел</param>
  /// <param name="index">Индекс дочернего узла</param>
  /// <param name="byShift">Перемещение узла</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public ChildNode_EventArgs(
    DocumentTreeNode parent,
    DocumentTreeNode child,
    int index,
    bool byShift,
    bool updateUI,
    bool updateLayout)
  {
    this.Parent = parent;
    this.Index = index;
    this.Child = child;
    this.ByShift = byShift;
    this.UpdateUI = updateUI;
    this.UpdateLayout = updateLayout;
  }
}
