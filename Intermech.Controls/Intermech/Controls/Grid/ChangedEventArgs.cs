
// Type: Intermech.Controls.Grid.ChangedEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.Grid;

/// <summary>Changed Event Args</summary>
public class ChangedEventArgs : EventArgs
{
  private ListColumn _column;
  private ListItem _item;
  private ListSubItem _subItem;
  private ChangedType _type;

  public ChangedEventArgs(
    ChangedType ctType,
    ListColumn column,
    ListItem item,
    ListSubItem subItem)
  {
    this._column = column;
    this._item = item;
    this._subItem = subItem;
    this._type = ctType;
  }

  public ListColumn Column
  {
    get => this._column;
    set => this._column = value;
  }

  public ListItem Item
  {
    get => this._item;
    set => this._item = value;
  }

  public ListSubItem SubItem
  {
    get => this._subItem;
    set => this._subItem = value;
  }

  public ChangedType ChangedType => this._type;
}
