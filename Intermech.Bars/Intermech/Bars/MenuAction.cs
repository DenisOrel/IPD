
// Type: Intermech.Bars.MenuAction
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class MenuAction
    {
      public MenuAction.CommandType _commandType;
      public MenuItemBase _menu;
      public bool _selectTopItem;
      public Control _control;
      public bool _e;

      public MenuAction(MenuAction.CommandType commandType)
      {
        this._e = true;
        this._commandType = commandType;
      }

      public MenuAction(MenuAction.CommandType commandType, Control control)
        : this(commandType)
      {
        this._control = control;
      }

      public MenuAction(MenuAction.CommandType commandType, MenuItemBase menu)
        : this(commandType)
      {
        this._menu = menu;
      }

      public MenuAction(MenuAction.CommandType commandType, MenuItemBase A_1, bool selectTopItem)
        : this(commandType, A_1)
      {
        this._selectTopItem = selectTopItem;
      }

      public enum CommandType
      {
        Show,
        Cancel,
        Execute,
      }
    }
}
