
// Type: Intermech.PropertyEditors.MenuItemExt
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for MenuItemExt.</summary>
public class MenuItemExt : MenuItem
{
  private object tag;

  public new object Tag
  {
    get => this.tag;
    set => this.tag = value;
  }

  public MenuItemExt()
  {
  }

  public MenuItemExt(string text)
    : base(text)
  {
  }

  public MenuItemExt(string text, EventHandler onClick)
    : base(text, onClick)
  {
  }

  public MenuItemExt(string text, MenuItem[] items)
    : base(text, items)
  {
  }

  public MenuItemExt(string text, EventHandler onClick, Shortcut shortcut)
    : base(text, onClick, shortcut)
  {
  }

  public MenuItemExt(
    MenuMerge mergeType,
    int mergeOrder,
    Shortcut shortcut,
    string text,
    EventHandler onClick,
    EventHandler onPopup,
    EventHandler onSelect,
    MenuItem[] items)
    : base(mergeType, mergeOrder, shortcut, text, onClick, onPopup, onSelect, items)
  {
  }

  public MenuItemExt(string text, object aTag)
    : base(text)
  {
    this.tag = aTag;
  }

  public MenuItemExt(string text, EventHandler onClick, object aTag)
    : base(text, onClick)
  {
    this.tag = aTag;
  }
}
