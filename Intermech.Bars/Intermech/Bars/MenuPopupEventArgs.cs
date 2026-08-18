
// Type: Intermech.Bars.MenuPopupEventArgs
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class MenuPopupEventArgs : EventArgs
    {
      private MenuItemBase.MenuPopupMode _mode;
      private Control _control;
      private Point _position;

      internal MenuPopupEventArgs(MenuItemBase.MenuPopupMode A_0)
      {
        this._position = Point.Empty;
        this._mode = A_0;
      }

      internal MenuPopupEventArgs(MenuItemBase.MenuPopupMode A_0, Control A_1)
        : this(A_0)
      {
        this._control = A_1;
      }

      internal MenuPopupEventArgs(MenuItemBase.MenuPopupMode A_0, Control A_1, Point A_2)
        : this(A_0, A_1)
      {
        this._position = A_2;
      }

      public Control Control => this._control;

      public MenuItemBase.MenuPopupMode Mode => this._mode;

      public Point Position
      {
        get => this._position;
        set => this._position = value;
      }
    }
}
