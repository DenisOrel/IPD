// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.IconicMenu
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using System;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Iconic Menu</summary>
public class IconicMenu : DropDownMenuItem
{
  private int _itemsPerLine = 4;

  /// <summary> Кол-во команд в одной строке </summary>
  public int ItemsPerLine => this._itemsPerLine;

  /// <summary>Конструктор</summary>
  public IconicMenu(int ItemsPerLine) => this._itemsPerLine = ItemsPerLine;

  /// <summary>Default Child Type</summary>
  protected override Type DefaultChildType => typeof (IconicMenuItem);

  /// <summary>Create PopupMenu</summary>
  /// <param name="host"></param>
  /// <returns></returns>
  protected override PopupMenu CreatePopupMenu(IPopupMenuHost host)
  {
    return (PopupMenu) new IconicMenuPopup(this, host)
    {
      ItemsPerLine = this.ItemsPerLine
    };
  }
}
