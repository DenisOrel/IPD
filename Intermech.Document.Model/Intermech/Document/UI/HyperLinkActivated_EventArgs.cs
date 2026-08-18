// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.HyperLinkActivated_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Аргументы события ActiveElementChanged</summary>
public class HyperLinkActivated_EventArgs : EventArgs
{
  /// <summary>Новый элемент</summary>
  public DocumentTreeNode Element;
  public string LinkId;
  public bool RightClick;

  /// <summary>Конструктор</summary>
  /// <param name="element">Новый элемент</param>
  public HyperLinkActivated_EventArgs(DocumentTreeNode element, string linkId, bool rightButton)
  {
    this.Element = element;
    this.LinkId = linkId;
    this.RightClick = rightButton;
  }
}
