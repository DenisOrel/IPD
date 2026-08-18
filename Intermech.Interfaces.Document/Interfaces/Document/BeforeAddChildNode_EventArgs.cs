// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BeforeAddChildNode_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события BeforeAddChildNode</summary>
public class BeforeAddChildNode_EventArgs : EventArgs
{
  /// <summary>Добавляемый узел</summary>
  public DocumentTreeNode Child;
  /// <summary>Отменить добавление</summary>
  public bool Cancel;

  /// <summary>Конструктор</summary>
  /// <param name="child">Добавляемый узел</param>
  public BeforeAddChildNode_EventArgs(DocumentTreeNode child) => this.Child = child;
}
