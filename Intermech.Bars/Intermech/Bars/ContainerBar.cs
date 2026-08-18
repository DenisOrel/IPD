
// Type: Intermech.Bars.ContainerBar
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [Designer(typeof (ContainerBarDesigner))]
    public class ContainerBar : ToolBar
    {
      private Size _minimumSize;
      private Rectangle c;
      private Rectangle d;
      private Rectangle e;
      private ContainerBarClientPanel _clientPanel;
      private Rectangle g;
      private ButtonItem _closeButton;
      private ContainerBarResizer _resizer;
      private bool j;

      public ContainerBar()
      {
        this._minimumSize = new Size(200, 284);
        this._clientPanel = (ContainerBarClientPanel) null;
        this._closeButton = (ButtonItem) null;
        this._resizer = (ContainerBarResizer) null;
        this.j = false;
        this.Stretch = true;
        this.Text = "Container";
        this.Dock = DockStyle.Right;
        this.Overflow = ToolBarOverflow.Wrap;
        this._closeButton = (ButtonItem) new SystemButton(ToolBarGlyphType.Close);
        this._closeButton.SetToolBar((ToolBar) this);
        this._closeButton.ToolTipText = "Close";
      }

      private Rectangle a()
      {
        if (this.Situation == ToolBarSituation.Contained)
        {
          switch (this.Parent.Dock)
          {
            case DockStyle.Top:
              Rectangle clientRectangle1 = this.ClientRectangle;
              int x1 = clientRectangle1.X;
              clientRectangle1 = this.ClientRectangle;
              int y1 = clientRectangle1.Bottom - 2;
              clientRectangle1 = this.ClientRectangle;
              int width1 = clientRectangle1.Width;
              return new Rectangle(x1, y1, width1, 2);
            case DockStyle.Bottom:
              Rectangle clientRectangle2 = this.ClientRectangle;
              int x2 = clientRectangle2.X;
              clientRectangle2 = this.ClientRectangle;
              int y2 = clientRectangle2.Y;
              clientRectangle2 = this.ClientRectangle;
              int width2 = clientRectangle2.Width;
              return new Rectangle(x2, y2, width2, 2);
            case DockStyle.Left:
              Rectangle clientRectangle3 = this.ClientRectangle;
              int x3 = clientRectangle3.Right - 2;
              clientRectangle3 = this.ClientRectangle;
              int y3 = clientRectangle3.Y;
              clientRectangle3 = this.ClientRectangle;
              int height1 = clientRectangle3.Height;
              return new Rectangle(x3, y3, 2, height1);
            case DockStyle.Right:
              Rectangle clientRectangle4 = this.ClientRectangle;
              int x4 = clientRectangle4.X;
              clientRectangle4 = this.ClientRectangle;
              int y4 = clientRectangle4.Y;
              clientRectangle4 = this.ClientRectangle;
              int height2 = clientRectangle4.Height;
              return new Rectangle(x4, y4, 2, height2);
          }
        }
        return Rectangle.Empty;
      }

      private Size GetPreferredSizeWithExtent(IToolBarRenderer renderer, bool vertical)
      {
        using (Graphics graphics = this.CreateGraphics())
          return ToolBarMeasure.GetPreferredSizeWithExtent((ToolBar) this, graphics, renderer, vertical, this.Width - 4, out bool _);
      }

      internal override void CalculateLayoutInternal(IToolBarRenderer renderer, bool vertical)
      {
        if (this.IgnoreLayoutRequests)
          return;
        this.IgnoreLayoutRequests = true;
        bool rightToLeft = this.RightToLeft == RightToLeft.Yes && this.AllowRightToLeft;
        renderer.StartToolBarRender((ToolBar) this, false, rightToLeft);
        renderer.FinishToolBarRender();
        Size size = this.Items.Count == 0 ? Size.Empty : this.GetPreferredSizeWithExtent(renderer, false);
        Size toolbarSize = size;
        if (size.Height != 0 && size.Width != 0)
          toolbarSize.Width += 8;
        else
          toolbarSize = Size.Empty;
        Rectangle toolbarBounds;
        Rectangle gripperBounds;
        renderer.LayoutContainerBar(this.ClientRectangle, toolbarSize, out this.c, out toolbarBounds, out this.d, out gripperBounds);
        this.g = toolbarBounds;
        this._grabHandleBounds = gripperBounds;
        Rectangle A_2 = toolbarBounds;
        A_2.Offset(4, 0);
        using (Graphics graphics = this.CreateGraphics())
          ToolBarMeasure.a((ToolBar) this, graphics, A_2, renderer, false, false, false);
        int num1 = SystemInformation.ToolWindowCaptionButtonSize.Width - 1;
        int x = this.c.Right - num1 - 3;
        int num2 = this.c.Top + this.c.Height / 2;
        if (this.Closable)
        {
          this._closeButton.ApplyLayout(new Rectangle(x, num2 - num1 / 2, num1, num1), (Graphics) null, false, false);
          x -= num1 + 1;
        }
        else
          this._closeButton.ApplyLayout(Rectangle.Empty, (Graphics) null, false, false);
        if (this.DrawActionsButton)
        {
          this.ActionsButton.ApplyLayout(new Rectangle(x, num2 - num1 / 2, num1, num1), (Graphics) null, false, false);
          x -= num1 + 1;
        }
        else
          this.ActionsButton.ApplyLayout(Rectangle.Empty, (Graphics) null, false, false);
        this.e = this.c;
        this.e.Width = x - this.e.X + num1;
        if (gripperBounds.Width != 0)
        {
          this.e.X += gripperBounds.Width + 2;
          this.e.Width -= gripperBounds.Width + 2;
        }
        if (this._clientPanel != null)
          this._clientPanel.Bounds = this.d;
        this.Invalidate();
        this.IgnoreLayoutRequests = false;
      }

      internal override Size GetPreferredSizeWithExtent(int extent, out bool wrapped)
      {
        wrapped = false;
        if (extent > (int) short.MaxValue)
          extent = this._minimumSize.Width;
        return new Size(Math.Max(extent, this._minimumSize.Width), this._minimumSize.Height);
      }

      protected override void OnControlAdded(ControlEventArgs e)
      {
        base.OnControlAdded(e);
        if (this._clientPanel != null || !(e.Control is ContainerBarClientPanel))
          return;
        this._clientPanel = (ContainerBarClientPanel) e.Control;
      }

      protected override void OnControlRemoved(ControlEventArgs e)
      {
        base.OnControlRemoved(e);
        if (e.Control != this._clientPanel)
          return;
        this._clientPanel = (ContainerBarClientPanel) null;
      }

      protected override void OnEnter(EventArgs e)
      {
        base.OnEnter(e);
        this.Invalidate(this.c);
      }

      protected override void OnItemRelease(ToolbarItemBase item, Point position)
      {
        if (item == this._closeButton)
          this.OnCloseButtonPressed();
        else
          base.OnItemRelease(item, position);
      }

      protected override void OnLeave(EventArgs e)
      {
        base.OnLeave(e);
        this.Invalidate(this.c);
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
        if (this.Resizable && this.Situation != ToolBarSituation.Floating && this.a().Contains(e.X, e.Y))
          this._resizer = new ContainerBarResizer(this, new Point(e.X, e.Y));
        else
          base.OnMouseDown(e);
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        if (this._resizer != null)
          this._resizer.b(new Point(e.X, e.Y));
        else if (this.Resizable && this.Situation != ToolBarSituation.Floating && this.a().Contains(e.X, e.Y))
        {
          if (this.Parent.Dock == DockStyle.Left || this.Parent.Dock == DockStyle.Right)
            this.Cursor = Cursors.SizeWE;
          else
            this.Cursor = Cursors.SizeNS;
        }
        else
          base.OnMouseMove(e);
      }

      internal override void OnOwnerFormActivated()
      {
        base.OnOwnerFormActivated();
        if (!this.ContainsFocus)
          return;
        this.Invalidate(this.c);
      }

      internal override void OnOwnerFormDeactivated()
      {
        base.OnOwnerFormDeactivated();
        if (!this.j)
          return;
        this.Invalidate(this.c);
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        base.OnPaint(e);
        this.WorkingRenderer.DrawContainerBarText(this.Text, e.Graphics, this.Font, this.e);
        if (!this.DrawActionsButton)
          return;
        DrawItemState state = DrawItemState.Default;
        if (this.ActionsButton == this.HighlightedItem)
        {
          state |= DrawItemState.HotLight;
          if (this._itemPushed || this.ActionsButton.DrawDroppedDown)
            state |= DrawItemState.Selected;
        }
        this.WorkingRenderer.DrawSystemButton(e.Graphics, this.ActionsButton.ButtonBounds, ToolBarGlyphType.Actions, state, false);
      }

      protected override void OnPaintBackground(PaintEventArgs pevent)
      {
        bool containsFocus = this.ContainsFocus;
        this.WorkingRenderer.DrawContainerBarBackground(this, pevent.Graphics, this.ClientRectangle, this.d);
        this.WorkingRenderer.DrawContainerBarTitleBarBackground(pevent.Graphics, this.c, containsFocus);
        Rectangle g = this.g;
        if (g.Height > 0 && g.Width > 0)
          this.WorkingRenderer.DrawContainerBarToolBarBackground(pevent.Graphics, g);
        this.j = containsFocus;
      }

      protected internal override void OnRendererChanged()
      {
        base.OnRendererChanged();
        this.DoLayout();
      }

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == 533 && this._resizer != null)
          this._resizer = (ContainerBarResizer) null;
        if (m.Msg == 123)
          m.Msg = m.Msg;
        base.WndProc(ref m);
      }

      [Browsable(false)]
      public ContainerBarClientPanel ClientPanel => this._clientPanel;

      public override bool Closable
      {
        get => base.Closable;
        set
        {
          base.Closable = value;
          this.DoLayout();
        }
      }

      [DefaultValue(typeof (DockStyle), "Right")]
      public override DockStyle Dock
      {
        get => base.Dock;
        set => base.Dock = value;
      }

      internal override ToolbarItemBase[] ExtraButtons
      {
        get
        {
          return new ToolbarItemBase[2]
          {
            (ToolbarItemBase) this.ActionsButton,
            (ToolbarItemBase) this._closeButton
          };
        }
      }

      public override ToolBarLayout Flow
      {
        get => ToolBarLayout.Horizontal;
        set
        {
        }
      }

      [Obsolete("Use the MinimumFloatingSize property instead.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      [DefaultValue(500)]
      [Category("Layout")]
      [Description("Indicates the maximum desired depth of the container.")]
      public int MaximumDepth
      {
        get => this.MaximumFloatingSize.Width;
        set => this.MaximumFloatingSize = new Size(value, value);
      }

      [Description("Indicates the minimum desired depth of the container.")]
      [Obsolete("Use the MinimumFloatingSize property instead.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      [DefaultValue(50)]
      [Category("Layout")]
      public int MinimumDepth
      {
        get => this.MinimumFloatingSize.Width;
        set => this.MinimumFloatingSize = new Size(value, value);
      }

      [Description("Indicates the minimum desired size of this container.")]
      [Category("Layout")]
      [DefaultValue(typeof (Size), "200, 284")]
      public override Size MinimumSize
      {
        get => this._minimumSize;
        set
        {
          this._minimumSize = value;
          if (!(this.Parent is ToolBarContainer))
            return;
          ((ToolBarContainer) this.Parent).ForceLayout();
        }
      }

      [DefaultValue(typeof (ToolBarOverflow), "Wrap")]
      public override ToolBarOverflow Overflow
      {
        get => base.Overflow;
        set => base.Overflow = value;
      }

      [DefaultValue(true)]
      public override bool Stretch
      {
        get => base.Stretch;
        set => base.Stretch = value;
      }

      [DefaultValue("Container")]
      public override string Text
      {
        get => base.Text;
        set
        {
          base.Text = value;
          this.Invalidate(this.c);
        }
      }
    }
}
