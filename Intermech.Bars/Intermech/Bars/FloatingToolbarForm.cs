
// Type: Intermech.Bars.FloatingToolbarForm
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class FloatingToolbarForm : TopForm
    {
      private const int _a = 132;
      private const int _b = 0;
      private const int _c = 12;
      private const int _d = 13;
      private const int _e = 14;
      private const int _f = 15;
      private const int _g = 16 /*0x10*/;
      private const int _h = 17;
      private const int _i = 10;
      private const int _j = 11;
      private const int _k = 1;
      private const int _l = 20;
      private const int _m = 2;
      private const int _n = 70;
      private ToolBar _toolBar;
      private BarManager _barManager;
      private bool _hided;
      private bool r;
      private Rectangle _captionBounds;
      private ButtonItem _closeButton;
      private ButtonItem _actionsButton;
      private bool _closeDown;
      private ToolbarItemBase _highlightedButton;
      private int _resizeFlags;

      public FloatingToolbarForm(ToolBar toolbar, BarManager barManager, RightToLeft rightToLeft)
      {
        this._hided = false;
        this.r = true;
        this._resizeFlags = 0;
        this._toolBar = toolbar;
        this._barManager = barManager;
        this.RightToLeft = rightToLeft;
        this.ShowInTaskbar = false;
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.Manual;
        this._closeButton = new ButtonItem();
        this._closeButton.ToolTipText = BarLanguage.CloseMenuText;
        this._actionsButton = new ButtonItem();
        this._actionsButton.ToolTipText = BarLanguage.ToolbarOptionsText;
        this.i();
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this.Controls.Add((Control) toolbar);
      }

      private ToolbarItemBase GetHighlightedButton() => this._highlightedButton;

      protected override void WndProc(ref Message A_0)
      {
        if (A_0.Msg == 533)
        {
          if (this._toolBar._docker != null && !this._toolBar._docker.IsRedocking())
            this.g();
        }
        else
        {
          if (A_0.Msg == 132)
          {
            A_0.Result = new IntPtr(this.GetResizeFlags(new Point(A_0.LParam.ToInt32())));
            return;
          }
          if (A_0.Msg == 161)
            this._resizeFlags = this.GetResizeFlags(Cursor.Position);
          else if (A_0.Msg == 70)
          {
            FloatingToolbarForm.WINDOWPOS structure = (FloatingToolbarForm.WINDOWPOS) Marshal.PtrToStructure(A_0.LParam, typeof (FloatingToolbarForm.WINDOWPOS));
            this.DoResize(ref structure);
            Marshal.StructureToPtr((object) structure, A_0.LParam, false);
            A_0.Result = IntPtr.Zero;
            return;
          }
        }
        base.WndProc(ref A_0);
      }

      private void DoResize(ref FloatingToolbarForm.WINDOWPOS windowpos)
      {
        if (this._toolBar == null || windowpos.x == 0 && windowpos.y == 0 && windowpos.cx == 0 && windowpos.cy == 0 || this._toolBar is ContainerBar)
          return;
        bool wrapped;
        Size size = this.UpdateSize(this._toolBar.GetPreferredSizeWithExtent(this.GetFormRectangle(new Rectangle(0, 0, windowpos.cx, windowpos.cy)).Width, out wrapped));
        this.r = !wrapped;
        int num1 = size.Width - windowpos.cx;
        int num2 = size.Height - windowpos.cy;
        windowpos.cx += num1;
        windowpos.cy += num2;
        if (this._resizeFlags == 10 || this._resizeFlags == 16 /*0x10*/ || this._resizeFlags == 13)
        {
          windowpos.x -= num1;
        }
        else
        {
          if (this._resizeFlags != 13 && this._resizeFlags != 12 && this._resizeFlags != 14)
            return;
          windowpos.y -= num2;
        }
        Rectangle bounds = Screen.PrimaryScreen.Bounds;
        if (windowpos.x + windowpos.cx < 4)
          windowpos.x = 4;
        if (windowpos.x <= bounds.Right)
          return;
        windowpos.x = bounds.Width - windowpos.cx;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
          this._toolBar = (ToolBar) null;
        base.Dispose(disposing);
      }

      private int GetResizeFlags(Point A_0)
      {
        if (this._toolBar.Resizable)
        {
          A_0.Offset(-this.Left, -this.Top);
          Size frameBorderSize;
          if (A_0.X < 10)
          {
            int y = A_0.Y;
            frameBorderSize = SystemInformation.FrameBorderSize;
            int height = frameBorderSize.Height;
            if (y <= height)
              goto label_5;
          }
          if (A_0.Y < 10)
          {
            int x = A_0.X;
            frameBorderSize = SystemInformation.FrameBorderSize;
            int width = frameBorderSize.Width;
            if (x <= width)
              goto label_5;
          }
          if (A_0.X > this.Width - 10)
          {
            int y = A_0.Y;
            frameBorderSize = SystemInformation.FrameBorderSize;
            int height = frameBorderSize.Height;
            if (y <= height)
              goto label_10;
          }
          if (A_0.Y < 10)
          {
            int x = A_0.X;
            int width1 = this.Width;
            frameBorderSize = SystemInformation.FrameBorderSize;
            int width2 = frameBorderSize.Width;
            int num = width1 - width2;
            if (x >= num)
              goto label_10;
          }
          int y1 = A_0.Y;
          frameBorderSize = SystemInformation.FrameBorderSize;
          int height1 = frameBorderSize.Height;
          if (y1 <= height1)
            return 12;
          if (A_0.X < 10 && A_0.Y > this.Height - 10)
            return 16 /*0x10*/;
          if (A_0.X > this.Width - 10 && A_0.Y > this.Height - 10)
            return 17;
          int y2 = A_0.Y;
          int height2 = this.Height;
          frameBorderSize = SystemInformation.FrameBorderSize;
          int height3 = frameBorderSize.Height;
          int num1 = height2 - height3;
          if (y2 >= num1)
            return 15;
          int x1 = A_0.X;
          frameBorderSize = SystemInformation.FrameBorderSize;
          int width3 = frameBorderSize.Width;
          if (x1 <= width3)
            return 10;
          int x2 = A_0.X;
          int width4 = this.Width;
          frameBorderSize = SystemInformation.FrameBorderSize;
          int width5 = frameBorderSize.Width;
          int num2 = width4 - width5;
          if (x2 >= num2)
            return 11;
          goto label_23;
    label_10:
          return 14;
    label_5:
          return 13;
        }
    label_23:
        return 1;
      }

      private Rectangle GetFormRectangle(Rectangle A_0)
      {
        Rectangle formRectangle = A_0;
        ref Rectangle local1 = ref formRectangle;
        Size fixedFrameBorderSize = SystemInformation.FixedFrameBorderSize;
        int width = -fixedFrameBorderSize.Width;
        fixedFrameBorderSize = SystemInformation.FixedFrameBorderSize;
        int height1 = -fixedFrameBorderSize.Height;
        local1.Inflate(width, height1);
        if (!(this._toolBar is ContainerBar))
        {
          ref Rectangle local2 = ref formRectangle;
          int y = local2.Y;
          Size captionButtonSize = SystemInformation.ToolWindowCaptionButtonSize;
          int height2 = captionButtonSize.Height;
          local2.Y = y + height2;
          ref Rectangle local3 = ref formRectangle;
          int height3 = local3.Height;
          captionButtonSize = SystemInformation.ToolWindowCaptionButtonSize;
          int height4 = captionButtonSize.Height;
          local3.Height = height3 - height4;
        }
        return formRectangle;
      }

      private Size UpdateSize(Size size)
      {
        Size size1 = size;
        size1.Width += SystemInformation.FixedFrameBorderSize.Width * 2;
        size1.Height += SystemInformation.FixedFrameBorderSize.Height * 2;
        if (!(this._toolBar is ContainerBar))
          size1.Height += SystemInformation.ToolWindowCaptionHeight;
        return size1;
      }

      protected override void OnActivated(EventArgs A_0)
      {
        base.OnActivated(A_0);
        if (this._toolBar == null)
          return;
        this._toolBar.OnOwnerFormActivated();
      }

      internal void a(MouseEventArgs A_0)
      {
        this.Cursor = Cursors.SizeAll;
        this.Capture = true;
        this._toolBar._docker = new ToolBarDocker(this._toolBar, A_0);
      }

      protected override void OnPaint(PaintEventArgs pe)
      {
        this.BarManager.Renderer.StartToolBarRender(this._toolBar, false, this._toolBar.RightToLeft == RightToLeft.Yes && this._toolBar.AllowRightToLeft);
        this.BarManager.Renderer.DrawFloatingFormBackground(pe.Graphics, this.ClientRectangle);
        this.BarManager.Renderer.DrawFloatingFormText(this._toolBar.Text, pe.Graphics, this._toolBar.Font, this._captionBounds);
        this.BarManager.Renderer.FinishToolBarRender();
        if (this._toolBar.Closable)
        {
          DrawItemState state = DrawItemState.Default;
          if (this._highlightedButton == this._closeButton)
          {
            state |= DrawItemState.HotLight;
            if (this._closeDown)
              state |= DrawItemState.Selected;
          }
          this.BarManager.Renderer.DrawSystemButton(pe.Graphics, this._closeButton.ButtonBounds, ToolBarGlyphType.Close, state, true);
        }
        if (!this._toolBar.DrawActionsButton)
          return;
        DrawItemState state1 = DrawItemState.Default;
        if (this._highlightedButton == this._actionsButton)
        {
          state1 |= DrawItemState.HotLight;
          if (this._closeDown)
            state1 |= DrawItemState.Selected;
        }
        this.BarManager.Renderer.DrawSystemButton(pe.Graphics, this._actionsButton.ButtonBounds, ToolBarGlyphType.Actions, state1, true);
      }

      private void SetHighlightedButton(ToolbarItemBase A_0)
      {
        if (this._highlightedButton != null)
          this.RefreshButton(this._highlightedButton);
        this._highlightedButton = A_0;
        if (this._highlightedButton == null)
          return;
        this.RefreshButton(this._highlightedButton);
      }

      private void b()
      {
        Rectangle clientRectangle = this.ClientRectangle;
        ref Rectangle local = ref clientRectangle;
        Size fixedFrameBorderSize = SystemInformation.FixedFrameBorderSize;
        int width = -fixedFrameBorderSize.Width;
        fixedFrameBorderSize = SystemInformation.FixedFrameBorderSize;
        int height = -fixedFrameBorderSize.Height;
        local.Inflate(width, height);
        clientRectangle.Height = SystemInformation.ToolWindowCaptionButtonSize.Height;
        int num1 = SystemInformation.ToolWindowCaptionButtonSize.Width - 1;
        int num2 = clientRectangle.Right - 2;
        if (this._toolBar.Closable)
        {
          this._closeButton.ApplyLayout(new Rectangle(num2 - num1 + 1, clientRectangle.Top, num1, num1), (Graphics) null, false, false);
          num2 -= num1 + 1;
        }
        else
          this._closeButton.ApplyLayout(Rectangle.Empty, (Graphics) null, false, false);
        if (this._toolBar.DrawActionsButton)
        {
          this._actionsButton.ApplyLayout(new Rectangle(num2 - num1 + 1, clientRectangle.Top, num1, num1), (Graphics) null, false, false);
          num2 -= num1 + 1;
        }
        else
          this._actionsButton.ApplyLayout(Rectangle.Empty, (Graphics) null, false, false);
        clientRectangle.Width -= clientRectangle.Right - num2;
        this._captionBounds = clientRectangle;
      }

      public void b(bool A_0) => this._hided = A_0;

      private void CheckHighlightedButton(Point A_0)
      {
        ButtonItem A_0_1 = (ButtonItem) null;
        Rectangle buttonBounds = this._closeButton.ButtonBounds;
        if (this._toolBar.Closable && buttonBounds.Contains(A_0))
          A_0_1 = this._closeButton;
        if (this._actionsButton.ButtonBounds.Contains(A_0) && this._toolBar.DrawActionsButton)
          A_0_1 = this._actionsButton;
        if (this.GetHighlightedButton() == A_0_1)
          return;
        this.SetHighlightedButton((ToolbarItemBase) A_0_1);
      }

      public void SetSize(Size value)
      {
        Size size = this.UpdateSize(value);
        this.Size = size;
        Win32.SetWindowPos(this.Handle, 0, this.Left, this.Top, size.Width, size.Height, 16 /*0x10*/);
      }

      protected override void OnDeactivate(EventArgs e)
      {
        base.OnDeactivate(e);
        if (this._toolBar == null)
          return;
        this._toolBar.OnOwnerFormDeactivated();
      }

      private void RefreshButton(ToolbarItemBase A_0)
      {
        Rectangle buttonBounds = A_0.ButtonBounds;
        ++buttonBounds.Width;
        ++buttonBounds.Height;
        this.Invalidate(buttonBounds);
      }

      public BarManager BarManager => this._barManager;

      protected override void OnResize(EventArgs e)
      {
        base.OnResize(e);
        this.b();
      }

      public void d()
      {
        this._toolBar.WorkingRenderer.StartToolBarRender(this._toolBar, false, this._toolBar.RightToLeft == RightToLeft.Yes && this._toolBar.AllowRightToLeft);
        this._toolBar.WorkingRenderer.FinishToolBarRender();
        Size size = this.UpdateSize(this._toolBar.GetPreferredSizeWithExtent(this.r ? int.MaxValue : this._toolBar.Width));
        if (size != this.Size)
          this.Size = size;
        this.b();
        this.Invalidate();
      }

      public void e()
      {
        if (!this._hided)
          return;
        this.MakeVisible();
        this._hided = false;
      }

      public void f()
      {
        if (!this.Visible)
          return;
        this._hided = true;
        this.Hide();
      }

      internal void g()
      {
        this._toolBar._docker.Dispose();
        this._toolBar._docker = (ToolBarDocker) null;
        this.Cursor = Cursors.Default;
      }

      public bool h() => this._hided;

      internal void i()
      {
        if (this._toolBar.MinimumFloatingSize == Size.Empty)
          this.MinimumSize = Size.Empty;
        else
          this.MinimumSize = this.UpdateSize(this._toolBar.MinimumFloatingSize);
        if (this._toolBar.MaximumFloatingSize == Size.Empty)
          this.MaximumSize = Size.Empty;
        else
          this.MaximumSize = this.UpdateSize(this._toolBar.MaximumFloatingSize);
      }

      protected override void OnControlRemoved(ControlEventArgs e)
      {
        base.OnControlRemoved(e);
        this.Dispose();
      }

      protected override void OnDoubleClick(EventArgs e)
      {
        base.OnDoubleClick(e);
        if (this._toolBar.LastFixedContainer == null)
          return;
        this._toolBar._docker = (ToolBarDocker) null;
        this._toolBar.Redock((Control) this._toolBar.LastFixedContainer);
      }

      protected override void OnLayout(LayoutEventArgs levent)
      {
        Rectangle formRectangle = this.GetFormRectangle(this.ClientRectangle);
        this._toolBar.Bounds = formRectangle;
        if (this._toolBar is ContainerBar)
          this._toolBar.MinimumSize = formRectangle.Size;
        this._toolBar.WorkingRenderer.StartToolBarRender(this._toolBar, false, this._toolBar.RightToLeft == RightToLeft.Yes && this._toolBar.AllowRightToLeft);
        this._toolBar.WorkingRenderer.FinishToolBarRender();
        this._toolBar.CalculateLayoutInternal(this._toolBar.WorkingRenderer, false);
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
        base.OnMouseDown(e);
        if (e.Button != MouseButtons.Left)
          return;
        this.CheckHighlightedButton(new Point(e.X, e.Y));
        if (this._highlightedButton == this._closeButton && this._toolBar.Closable)
        {
          this._closeDown = true;
          this.RefreshButton((ToolbarItemBase) this._closeButton);
        }
        else if (this._highlightedButton == this._actionsButton && this._toolBar.DrawActionsButton)
        {
          TopLevelMenuItemBase actionsButton = this._toolBar.ActionsButton;
          Rectangle buttonBounds = this._actionsButton.ButtonBounds;
          int x = buttonBounds.X;
          buttonBounds = this._actionsButton.ButtonBounds;
          int bottom = buttonBounds.Bottom;
          Point position = new Point(x, bottom);
          actionsButton.Show((Control) this, position);
        }
        else
          this.a(e);
      }

      protected override void OnMouseLeave(EventArgs e)
      {
        base.OnMouseLeave(e);
        if (this.GetHighlightedButton() == null)
          return;
        this.SetHighlightedButton((ToolbarItemBase) null);
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        base.OnMouseMove(e);
        if (this._toolBar._docker != null)
          this._toolBar._docker.DoRedock(e);
        else
          this.CheckHighlightedButton(new Point(e.X, e.Y));
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
        base.OnMouseUp(e);
        if (this._toolBar == null)
          return;
        if (e.Button == MouseButtons.Right)
        {
          this._barManager.CustomizeToolbars(this._toolBar, (Control) this, new Point(e.X, e.Y));
        }
        else
        {
          if (!this._toolBar.Closable || !this._closeDown)
            return;
          this._closeDown = false;
          this.RefreshButton((ToolbarItemBase) this._closeButton);
          if (this._highlightedButton != this._closeButton)
            return;
          this._toolBar.OnCloseButtonPressed();
        }
      }

      private struct WINDOWPOS
      {
        public IntPtr hwnd;
        public IntPtr hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public int flags;
      }
    }
}
