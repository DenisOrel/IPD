// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PageCursorPositionChanged_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Аргументы события PageCursorPositionChanged</summary>
public class PageCursorPositionChanged_EventArgs : EventArgs
{
  /// <summary>Страница</summary>
  public Page Page;
  /// <summary>Положение курсора в мм</summary>
  public PointF Position;

  /// <summary>Конструктор</summary>
  /// <param name="page">Страница</param>
  /// <param name="position">Положение курсора в мм</param>
  public PageCursorPositionChanged_EventArgs(Page page, PointF position)
  {
    this.Page = page;
    this.Position = position;
  }
}
