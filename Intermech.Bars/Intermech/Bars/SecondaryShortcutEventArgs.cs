
// Type: Intermech.Bars.SecondaryShortcutEventArgs
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class SecondaryShortcutEventArgs : EventArgs
    {
      private Keys _primaryShortcut;
      private Keys _secondaryShortcut;
      private MenuButtonItem _item;

      internal SecondaryShortcutEventArgs(Keys keys)
      {
        this._primaryShortcut = keys;
        this._secondaryShortcut = Keys.None;
        this._item = (MenuButtonItem) null;
      }

      internal SecondaryShortcutEventArgs(
        Keys primaryKeys,
        Keys secondaryKeys,
        MenuButtonItem menuItem)
      {
        this._primaryShortcut = primaryKeys;
        this._secondaryShortcut = secondaryKeys;
        this._item = menuItem;
      }

      public MenuButtonItem Item => this._item;

      public bool Primary => this._secondaryShortcut == Keys.None;

      public Keys PrimaryShortcut => this._primaryShortcut;

      public Keys SecondaryShortcut => this._secondaryShortcut;
    }
}
