// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ActiveElementChanged_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Аргументы события ActiveElementChanged</summary>
public class ActiveElementChanged_EventArgs : EventArgs
{
  /// <summary>Новый элемент</summary>
  public DocumentTreeNode Element;

  /// <summary>Конструктор</summary>
  /// <param name="element">Новый элемент</param>
  public ActiveElementChanged_EventArgs(DocumentTreeNode element) => this.Element = element;
}
