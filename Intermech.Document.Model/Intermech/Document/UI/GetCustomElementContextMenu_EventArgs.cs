// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.GetCustomElementContextMenu_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Аргументы события GetCustomElementContextMenu</summary>
public class GetCustomElementContextMenu_EventArgs : EventArgs
{
  /// <summary>Контекст</summary>
  public DocumentTreeNode[] Context;
  /// <summary>Список элементов контекстного меню</summary>
  public List<MenuButtonItem> ContextMenuItems;

  /// <summary>Конструктор</summary>
  /// <param name="context">Контекст</param>
  /// <param name="contextMenuItems">Список элементов контекстного меню</param>
  public GetCustomElementContextMenu_EventArgs(
    DocumentTreeNode[] context,
    List<MenuButtonItem> contextMenuItems)
  {
    this.Context = context;
    this.ContextMenuItems = contextMenuItems;
  }
}
