
// Type: Intermech.Bars.ToolBarDocker
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Bars
{
    internal class ToolBarDocker : IDisposable
    {
      private const int _a = 5;
      private ToolBar _toolBar;
      private BarManager _barManager;
      private Point _mousePos;
      private bool _redocking;
      private Point _lastPos;

      public ToolBarDocker(ToolBar toolbar, MouseEventArgs mea)
      {
        this._redocking = false;
        this._toolBar = toolbar;
        if (toolbar.Parent is ToolBarContainer && ((ToolBarContainer) toolbar.Parent).Manager != null)
          this._barManager = ((ToolBarContainer) toolbar.Parent).Manager;
        else if (toolbar.Parent is FloatingToolbarForm)
          this._barManager = ((FloatingToolbarForm) toolbar.Parent).BarManager;
        this._mousePos = new Point(mea.X, mea.Y);
        this._lastPos = Point.Empty;
      }

      private void FloateToolbar()
      {
        Point mousePos = this._mousePos;
        this._redocking = true;
        this._toolBar.Capture = false;
        this._redocking = false;
        this._toolBar.MakeFloating(this._barManager, Cursor.Position, true);
        Size size = this._toolBar.f();
        if (mousePos.X > size.Width)
          mousePos.X = size.Width;
        if (mousePos.Y > size.Height)
          mousePos.Y = size.Height;
        Control parent = this._toolBar.Parent;
        Point position = Cursor.Position;
        int x = position.X - mousePos.X;
        position = Cursor.Position;
        int y = position.Y - mousePos.Y;
        Point point = new Point(x, y);
        parent.Location = point;
        ((TopForm) this._toolBar.Parent).MakeVisible();
        this._mousePos = mousePos;
        this._mousePos.Y += SystemInformation.FixedFrameBorderSize.Height;
        if (!(this._toolBar is ContainerBar))
          this._mousePos.Y += SystemInformation.ToolWindowCaptionHeight;
        this._toolBar.Parent.Capture = true;
      }

      private ToolBarContainer GetContainerAt(Point point)
      {
        foreach (ToolBarContainer container in this._barManager._containers)
        {
          if ((container.Dock == DockStyle.Left || container.Dock == DockStyle.Right ? (this._toolBar.AllowVerticalDock ? 1 : 0) : (this._toolBar.AllowHorizontalDock ? 1 : 0)) != 0 && this.IsGoodContainer(container, point))
            return container;
        }
        return (ToolBarContainer) null;
      }

      public void DoRedock(MouseEventArgs mea)
      {
        Point position = Cursor.Position;
        this._toolBar.Parent.Location = new Point(position.X - this._mousePos.X, position.Y - this._mousePos.Y);
        if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
          return;
        ToolBarContainer containerAt = this.GetContainerAt(position);
        if (containerAt == null)
          return;
        this.UpdateDockLine(containerAt, Cursor.Position);
        this._redocking = true;
        this._toolBar.Redock((Control) containerAt);
        this._redocking = false;
        this._toolBar.Capture = true;
      }

      private bool IsGoodContainer(ToolBarContainer container, Point point)
      {
        if (container == null || !container.Enabled)
          return false;
        Rectangle rectangle = new Rectangle(container.PointToScreen(new Point(0, 0)), container.ClientRectangle.Size);
        if (rectangle.Width == 0)
          rectangle.Inflate(10, 0);
        if (rectangle.Height == 0)
          rectangle.Inflate(0, 10);
        switch (container.Dock)
        {
          case DockStyle.Top:
          case DockStyle.Bottom:
            rectangle.Height += 5;
            break;
          case DockStyle.Left:
          case DockStyle.Right:
            rectangle.Width += 5;
            break;
        }
        return rectangle.Contains(point);
      }

      public bool IsRedocking() => this._redocking;

      public void OnMouseMove(MouseEventArgs e)
      {
        if (this._barManager == null)
          return;
        int width = SystemInformation.DragSize.Width;
        if (Math.Abs(e.X - this._lastPos.X) + Math.Abs(e.Y - this._lastPos.Y) < width)
          return;
        this._lastPos.X = e.X;
        this._lastPos.Y = e.Y;
        Point screen = this._toolBar.PointToScreen(new Point(e.X, e.Y));
        Rectangle rectangle = new Rectangle(this._toolBar.Parent.PointToScreen(new Point(0, 0)), this._toolBar.Parent.ClientRectangle.Size);
        rectangle.Inflate(22, 22);
        int num = !rectangle.Contains(screen) ? 1 : 0;
        ToolBarContainer containerAt = this.GetContainerAt(screen);
        if (num != 0)
        {
          if (containerAt == null)
          {
            if (!this._toolBar.Tearable)
              return;
            this.FloateToolbar();
          }
          else
          {
            this._redocking = true;
            this._toolBar.Redock((Control) containerAt);
            this._redocking = false;
            this._toolBar.Capture = true;
          }
        }
        else
          this.UpdateDockLine((ToolBarContainer) this._toolBar.Parent, screen);
      }

      private void UpdateDockLine(ToolBarContainer container, Point point)
      {
        Point client1 = container.PointToClient(point);
        Point client2 = this._toolBar.PointToClient(point);
        bool flag = container.Dock == DockStyle.Left || container.Dock == DockStyle.Right;
        int A_1_1 = 0;
        this._toolBar.DockOffset = !flag ? client1.X - this._mousePos.X : client1.Y - this._mousePos.Y;
        int toolbarsCountAtDockLine = container.GetToolbarsCountAtDockLine(this._toolBar.DockLine);
        if (toolbarsCountAtDockLine > 1)
        {
          if (flag)
          {
            if (client2.X >= 0 && client2.X < 3)
              A_1_1 = -1;
          }
          else if (client2.Y >= 0 && client2.Y < 3)
            A_1_1 = -1;
          if (A_1_1 != 0)
          {
            int num = this._toolBar.DockLine + A_1_1;
            container.b(this._toolBar.DockLine, A_1_1);
            this._toolBar.DockLine = num;
            return;
          }
        }
        int A_1_2 = 0;
        foreach (ToolBar control in (ArrangedElementCollection) container.Controls)
        {
          if ((!flag ? new Rectangle(0, control.Top, container.ClientRectangle.Width, control.Height) : new Rectangle(control.Left, 0, control.Width, container.ClientRectangle.Height)).Contains(client1) && control.DockLine != this._toolBar.DockLine)
          {
            if (toolbarsCountAtDockLine > 1)
            {
              if (flag)
              {
                if (client1.X >= this._toolBar.Bounds.Right && client1.X <= this._toolBar.Bounds.Right + 3)
                  A_1_2 = 1;
              }
              else if (client1.Y >= this._toolBar.Bounds.Bottom && client1.Y <= this._toolBar.Bounds.Bottom + 3)
                A_1_2 = 1;
              if (A_1_2 != 0)
              {
                int num = this._toolBar.DockLine + A_1_2;
                container.b(this._toolBar.DockLine, A_1_2);
                this._toolBar.DockLine = num;
                return;
              }
            }
            if (flag)
            {
              if (client1.X >= control.Left && client1.X <= control.Left + 3)
                A_1_2 = 1;
            }
            else if (client1.Y >= control.Top && client1.Y <= control.Top + 3)
              A_1_2 = 1;
            if (A_1_2 != 0)
            {
              int dockLine = control.DockLine;
              container.a(control.DockLine, 1);
              this._toolBar.DockLine = dockLine;
              return;
            }
            if (!(control is MenuBar) && !(this._toolBar is MenuBar))
            {
              this._toolBar.DockLine = control.DockLine;
              return;
            }
          }
        }
        switch (container.Dock)
        {
          case DockStyle.Top:
          case DockStyle.Bottom:
            if (client1.Y >= container.Height && client1.Y <= container.Height + 5)
            {
              A_1_2 = 1;
              break;
            }
            break;
          case DockStyle.Left:
          case DockStyle.Right:
            if (client1.X >= container.Width && client1.X <= container.Width + 5)
            {
              A_1_2 = 1;
              break;
            }
            break;
        }
        if (A_1_2 == 0 || toolbarsCountAtDockLine <= 1 && this._toolBar.DockLine == container.GetNextFreeDockLine() - 1 || A_1_2 != 1)
          return;
        this._toolBar.DockLine = container.GetNextFreeDockLine();
      }

      public void Dispose() => this._toolBar = (ToolBar) null;
    }
}
