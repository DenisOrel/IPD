
// Type: Intermech.Bars.TopLevelMenuItemBase
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [Designer(typeof (TopLevelMenuItemDesigner))]
    public abstract class TopLevelMenuItemBase : MenuItemBase
    {
      protected TopLevelMenuItemBase()
      {
      }

      protected TopLevelMenuItemBase(string text)
        : base(text)
      {
      }

      public MenuButtonItem Show() => this.Show(false);

      public virtual MenuButtonItem Show(bool select)
      {
        MenuLooper menuLooper = this.ToolBar != null ? new MenuLooper((IPopupMenuHost) this.ToolBar, (Control) this.ToolBar, this.ToolBar.TopLevelMenuItems) : throw new InvalidOperationException("This menu item must belong to a toolbar to be shown in this way.");
        MenuButtonItem menuButtonItem = menuLooper.Select(this, select, true, Point.Empty);
        menuLooper.Dispose();
        return menuButtonItem;
      }

      public virtual MenuButtonItem Show(Control control, Point position)
      {
        if (this.ToolBar == null)
          throw new InvalidOperationException("This menu item must belong to a toolbar to be shown in this way.");
        TopLevelMenuItemBase[] availableMenus = new TopLevelMenuItemBase[1]
        {
          this
        };
        MenuLooper menuLooper = new MenuLooper((IPopupMenuHost) this.ToolBar, control, availableMenus);
        MenuButtonItem menuButtonItem = menuLooper.Select(this, false, false, control.PointToScreen(position));
        menuLooper.Dispose();
        return menuButtonItem;
      }

      public virtual MenuButtonItem Show(IPopupMenuHost host, Control control, Point position)
      {
        TopLevelMenuItemBase[] availableMenus = new TopLevelMenuItemBase[1]
        {
          this
        };
        MenuLooper menuLooper = new MenuLooper(host, control, availableMenus);
        MenuButtonItem menuButtonItem = menuLooper.Select(this, false, false, control.PointToScreen(position));
        menuLooper.Dispose();
        return menuButtonItem;
      }

      public virtual MenuButtonItem ShowIndependent(Point position)
      {
        using (Form form = new Form())
        {
          Win32.SetForegroundWindow(form.Handle);
          return this.Show((Control) form, form.PointToClient(position));
        }
      }

      internal override void UpdateAcceleratorTable()
      {
        if (!(this.ToolBar is MenuBar))
          return;
        ((MenuBar) this.ToolBar).ShortcutListener.UpdateAcceleratorTable(this.ToolBar);
      }

      internal bool DrawDroppedDown => this.PopupMenu != null && !this.PopupMenu.IsContextMenu;

      public override string Text
      {
        get => base.Text;
        set
        {
          base.Text = value;
          this.UpdateAcceleratorTable();
        }
      }
    }
}
