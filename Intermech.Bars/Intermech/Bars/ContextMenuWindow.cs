
// Type: Intermech.Bars.ContextMenuWindow
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class ContextMenuWindow : NativeWindow, IDisposable
    {
      private MenuBar _menuBar;
      private Control _control;
      private bool _isRichTextBox;

      public ContextMenuWindow(MenuBar menubar, Control control)
      {
        this._isRichTextBox = false;
        this._menuBar = menubar;
        this._control = control;
        control.HandleCreated += new EventHandler(this.Control_HandleCreated);
        control.HandleDestroyed += new EventHandler(this.Control_HandleDestroyed);
        if (control.IsHandleCreated)
          this.AssignHandle(control.Handle);
        if (!(control is RichTextBox))
          return;
        this._isRichTextBox = true;
      }

      public void Dispose()
      {
        if (this.Handle != IntPtr.Zero)
          this.ReleaseHandle();
        this._control.HandleCreated -= new EventHandler(this.Control_HandleCreated);
        this._control.HandleDestroyed -= new EventHandler(this.Control_HandleDestroyed);
      }

      private void ShowContextMenu(int mousePos)
      {
        Point empty = Point.Empty with
        {
          X = Win32.LoWorld(mousePos),
          Y = Win32.HiWord(mousePos)
        };
        Point client;
        if (empty.X == -1 || empty.X == (int) ushort.MaxValue)
        {
          empty.X = Control.MousePosition.X;
          empty.Y = Control.MousePosition.Y;
          client = this._control.PointToClient(empty);
        }
        else
          client = this._control.PointToClient(empty);
        this._control.BeginInvoke((Delegate) new ContextMenuWindow.ShowContextMenuInvoker(this._menuBar.ShowContextMenu), (object) this._control, (object) client);
      }

      protected override void WndProc(ref Message A_0)
      {
        if (this._isRichTextBox && A_0.Msg == 517)
          this.b(A_0.LParam.ToInt32());
        else if (!this._isRichTextBox && A_0.Msg == 123)
          this.ShowContextMenu(A_0.LParam.ToInt32());
        else
          base.WndProc(ref A_0);
      }

      private void Control_HandleDestroyed(object A_0, EventArgs A_1) => this.ReleaseHandle();

      private void b(int A_0)
      {
        this._control.BeginInvoke((Delegate) new ContextMenuWindow.ShowContextMenuInvoker(this._menuBar.ShowContextMenu), (object) this._control, (object) (Point.Empty with
        {
          X = Win32.LoWorld(A_0),
          Y = Win32.HiWord(A_0)
        }));
      }

      private void Control_HandleCreated(object A_0, EventArgs A_1)
      {
        this.AssignHandle(this._control.Handle);
      }

      private delegate void ShowContextMenuInvoker(Control control, Point pos);
    }
}
