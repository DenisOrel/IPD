// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BeforeRemoveChildNode_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события BeforeRemoveChildNode</summary>
public class BeforeRemoveChildNode_EventArgs : EventArgs
{
  /// <summary>Удаляемый узел</summary>
  public DocumentTreeNode Child;
  /// <summary>Удаляется для перемещения</summary>
  public bool RemoveByShift = true;
  /// <summary>Отменить удаление</summary>
  public bool Cancel;

  /// <summary>Конструктор</summary>
  /// <param name="child">Удаляемый узел</param>
  /// <param name="removeByShift">Удаляется для перемещения</param>
  public BeforeRemoveChildNode_EventArgs(DocumentTreeNode child, bool removeByShift)
  {
    this.Child = child;
    this.RemoveByShift = removeByShift;
  }
}
