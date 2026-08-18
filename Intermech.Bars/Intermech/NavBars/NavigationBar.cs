
// Type: Intermech.NavBars.NavigationBar
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Bars;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.NavBars
{
    [Designer(typeof (NavBarDesigner))]
    [DefaultEvent("SelectedPaneChanged")]
    [ToolboxBitmap(typeof (NavigationBar))]
    public class NavigationBar : Control, IPopupMenuHost, INavigationBar
    {
      private bool _disposed;
      private INavBarRenderer _renderer;
      internal ToolTip _toolTip;
      private NavigationPane _hoverPaneButton;
      private Font _headerFont;
      private Font _panelFont;
      private NavigationPane _hoverPane;
      private bool _mouseOnChevron;
      private bool _mouseDown;
      private bool _drawActionsButton;
      private Rectangle _headerRect;
      private Rectangle _j;
      private Rectangle _gripRect;
      private Rectangle _chevronRect;
      private int _showedPanes;
      private NavigationPane _selectedPane;
      private bool _resizing;
      private int _p;
      private string _moreButtonsText;
      private string _fewerButtonsText;
      private string _paneOptionsText;
      private string _addRemoveButtonsText;

      public event EventHandler SelectedPaneChanged;

      public event EventHandler ShowNavigationPaneOptions;

      public NavigationBar()
      {
        this._disposed = false;
        this._mouseOnChevron = false;
        this._mouseDown = false;
        this._drawActionsButton = true;
        this._showedPanes = 0;
        this._resizing = false;
        this._p = -1;
        this._moreButtonsText = LocalizationHolder.rm.GetString("Bars_12");
        this._fewerButtonsText = LocalizationHolder.rm.GetString("Bars_13");
        this._paneOptionsText = LocalizationHolder.rm.GetString("Bars_14");
        this._addRemoveButtonsText = LocalizationHolder.rm.GetString("Bars_15");
        this._headerFont = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point);
        this._panelFont = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
        base.Dock = DockStyle.Left;
        this._renderer = (INavBarRenderer) new NavBarRenderer();
        this._renderer.RedrawRequired += new EventHandler(this.RendererRedrawRequired);
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.ResizeRedraw, true);
      }

      private void RendererRedrawRequired(object sender, EventArgs renderer) => this.Invalidate(true);

      internal int GetPanesCount()
      {
        int panesCount = 0;
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          if (control is NavigationPane)
            ++panesCount;
        }
        return panesCount;
      }

      private void AddInvisibleButtonsMenu(ContextMenuBarItem A_0)
      {
        if (this._p == -1)
          return;
        bool flag = true;
        for (int p = this._p; p < this.Controls.Count; ++p)
        {
          if (this.Controls[p] is NavigationPane)
          {
            NavigationPane control = (NavigationPane) this.Controls[p];
            if (control.Listed && control._d.X < 0)
            {
              NavigationBar.PaneMenuItem paneMenuItem = new NavigationBar.PaneMenuItem();
              paneMenuItem.Text = control.Text;
              paneMenuItem._pane = control;
              paneMenuItem._b = true;
              paneMenuItem.Checked = control.Enabled;
              if (flag)
              {
                paneMenuItem.BeginGroup = true;
                flag = false;
              }
              A_0.Items.Add((ToolbarItemBase) paneMenuItem);
              if (control.SmallImage != null)
                paneMenuItem.Image = control.SmallImage;
              else if (control.LargeImage != null)
                paneMenuItem.Image = control.LargeImage;
            }
          }
        }
      }

      private void ShowCustomizeMenu(Point pos)
      {
        Rectangle chevronRect = this._chevronRect;
        chevronRect.Inflate(2, 2);
        this._mouseDown = true;
        this.Invalidate(chevronRect);
        Image image1 = (Image) null;
        using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NavBars.uparrow.gif"))
          image1 = Image.FromStream(manifestResourceStream);
        Image image2 = (Image) null;
        using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NavBars.downarrow.gif"))
          image2 = Image.FromStream(manifestResourceStream);
        ContextMenuBarItem A_0 = new ContextMenuBarItem();
        MenuButtonItem menuButtonItem1 = new MenuButtonItem(this._moreButtonsText);
        menuButtonItem1.Image = image1;
        menuButtonItem1.Enabled = this.ShowPanes < this.ListedCount();
        MenuButtonItem menuButtonItem2 = new MenuButtonItem(this._fewerButtonsText);
        menuButtonItem2.Image = image2;
        menuButtonItem2.Enabled = this.ShowPanes > 0;
        MenuButtonItem menuButtonItem3 = new MenuButtonItem(this._paneOptionsText);
        menuButtonItem3.Visible = this.ShowNavigationPaneOptions != null;
        MenuButtonItem menuButtonItem4 = new MenuButtonItem(this._addRemoveButtonsText);
        MenuButtonItem[] items = new MenuButtonItem[3]
        {
          menuButtonItem1,
          menuButtonItem2,
          menuButtonItem3
        };
        items[2] = menuButtonItem4;
        A_0.Items.AddRange((ToolbarItemBase[]) items);
        this.AddInvisibleButtonsMenu(A_0);
        for (int index = this.Controls.Count - 1; index >= 0; --index)
        {
          if (this.Controls[index] is NavigationPane)
          {
            NavigationPane control = (NavigationPane) this.Controls[index];
            NavigationBar.PaneMenuItem paneMenuItem = new NavigationBar.PaneMenuItem();
            paneMenuItem.Text = control.Text;
            paneMenuItem._pane = control;
            paneMenuItem.Checked = control.Listed;
            menuButtonItem4.Items.Add((ToolbarItemBase) paneMenuItem);
            if (control.SmallImage != null)
              paneMenuItem.Image = control.SmallImage;
            else if (control.LargeImage != null)
              paneMenuItem.Image = control.LargeImage;
          }
        }
        MenuButtonItem menuButtonItem5 = A_0.Show((IPopupMenuHost) this, (Control) this, pos);
        if (menuButtonItem5 is NavigationBar.PaneMenuItem)
        {
          NavigationBar.PaneMenuItem paneMenuItem = (NavigationBar.PaneMenuItem) menuButtonItem5;
          if (paneMenuItem._b)
            this.SelectedPane = paneMenuItem._pane;
          else
            paneMenuItem._pane.Listed = !paneMenuItem._pane.Listed;
        }
        else if (menuButtonItem5 == menuButtonItem1)
          ++this.ShowPanes;
        else if (menuButtonItem5 == menuButtonItem2)
          --this.ShowPanes;
        else if (menuButtonItem5 == menuButtonItem3 && this.ShowNavigationPaneOptions != null)
          this.ShowNavigationPaneOptions((object) this, EventArgs.Empty);
        image1.Dispose();
        image2.Dispose();
        this._mouseDown = false;
        this._mouseOnChevron = false;
        this.Invalidate(chevronRect);
      }

      internal void InvalidatePane(NavigationPane A_0)
      {
        Rectangle d = A_0._d;
        d.Inflate(2, 2);
        this.Invalidate(d);
      }

      internal void CalcLayout()
      {
        int num1 = this.ListedCount();
        this._p = -1;
        using (Graphics graphics = this.CreateGraphics())
        {
          try
          {
            Rectangle clientRectangle = this.ClientRectangle;
            clientRectangle.Inflate(-1, -1);
            this._headerRect = clientRectangle;
            this._headerRect.Height = (int) Math.Ceiling((double) graphics.MeasureString("X|L", this._headerFont).Height) + 3;
            int num2 = 16 /*0x10*/;
            int num3 = 16 /*0x10*/;
            foreach (Control control in (ArrangedElementCollection) this.Controls)
            {
              if (control is NavigationPane)
              {
                NavigationPane navigationPane = (NavigationPane) control;
                if (navigationPane.LargeImage != null && navigationPane.LargeImage.Height > num2)
                  num2 = navigationPane.LargeImage.Height;
                if (navigationPane.SmallImage != null && navigationPane.SmallImage.Width > num3)
                  num3 = navigationPane.SmallImage.Width;
              }
            }
            int num4 = num2 + 8;
            int num5 = num3 + 6;
            this._j = clientRectangle;
            this._j.Y = clientRectangle.Bottom - num4;
            this._j.Height = num4;
            this._chevronRect = this._j;
            ++this._chevronRect.Y;
            --this._chevronRect.Height;
            this._chevronRect.X = this._chevronRect.Right - 18;
            this._chevronRect.Width = 18;
            int num6 = this.ShowPanes + 1;
            int num7 = num1 - this.ShowPanes;
            int num8 = 1;
            for (int index = this.Controls.Count - 1; index >= 0; --index)
            {
              if (this.Controls[index] is NavigationPane)
              {
                NavigationPane control = (NavigationPane) this.Controls[index];
                if (control.Listed)
                {
                  Rectangle rectangle;
                  if (num8 <= this.ShowPanes)
                  {
                    rectangle = clientRectangle with
                    {
                      Y = clientRectangle.Bottom - num4 * num6,
                      Height = num4
                    };
                    control._hided = false;
                    --num6;
                  }
                  else
                  {
                    rectangle = this._j;
                    ++rectangle.Y;
                    --rectangle.Height;
                    rectangle.X = clientRectangle.Right - num5 * num7 - this._chevronRect.Width;
                    if (!this._drawActionsButton)
                      rectangle.X += this._chevronRect.Width;
                    rectangle.Width = num5;
                    control._hided = true;
                    --num7;
                    if (rectangle.Left < 0)
                      this._p = index;
                  }
                  control._d = rectangle;
                  ++num8;
                }
              }
            }
            this._gripRect = clientRectangle;
            this._gripRect.Y = clientRectangle.Bottom - num4 * (this.ShowPanes + 1) - 6;
            this._gripRect.Height = 6;
            Rectangle rectangle1 = clientRectangle with
            {
              Y = this._headerRect.Bottom,
              Height = this._gripRect.Top - this._headerRect.Bottom
            };
            if (rectangle1.Height < 0)
              rectangle1.Height = 0;
            foreach (Control control in (ArrangedElementCollection) this.Controls)
            {
              if (control is NavigationPane)
              {
                control.Bounds = rectangle1;
                control.Visible = control == this.SelectedPane;
              }
            }
          }
          catch (Exception ex)
          {
          }
        }
        this.Invalidate();
      }

      internal int ListedCount()
      {
        int num = 0;
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          if (control is NavigationPane && ((NavigationPane) control).Listed)
            ++num;
        }
        return num;
      }

      protected override Control.ControlCollection CreateControlsInstance()
      {
        return (Control.ControlCollection) new PaneCollection(this);
      }

      internal NavigationPane FirstPane()
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          if (control is NavigationPane)
            return (NavigationPane) control;
        }
        return (NavigationPane) null;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          this._disposed = true;
          if (this._toolTip != null)
            this._toolTip.Dispose();
        }
        base.Dispose(disposing);
      }

      public NavigationPane GetPaneAt(int x, int y)
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          if (control is NavigationPane)
          {
            NavigationPane paneAt = (NavigationPane) control;
            Rectangle d = paneAt._d;
            if (d.X >= 0 && paneAt.Listed && d.Contains(x, y))
              return paneAt;
          }
        }
        return (NavigationPane) null;
      }

      protected override void OnHandleCreated(EventArgs e)
      {
        if (!this.DesignMode && this._toolTip == null)
          this._toolTip = new ToolTip();
        base.OnHandleCreated(e);
      }

      protected override void OnHandleDestroyed(EventArgs e) => base.OnHandleDestroyed(e);

      protected override void OnLayout(LayoutEventArgs levent) => this.CalcLayout();

      protected override void OnMouseDown(MouseEventArgs e)
      {
        if (this._hoverPane != null)
        {
          this._mouseDown = true;
          this.InvalidatePane(this._hoverPane);
        }
        if (this._mouseOnChevron && this._chevronRect.Contains(e.X, e.Y))
        {
          this.ShowCustomizeMenu(new Point(this._chevronRect.Right, this._chevronRect.Y + this._chevronRect.Height / 2));
        }
        else
        {
          if (this._gripRect.Contains(e.X, e.Y))
            this._resizing = true;
          base.OnMouseDown(e);
        }
      }

      protected override void OnMouseLeave(EventArgs e)
      {
        base.Cursor = Cursors.Default;
        if (this._hoverPane != null)
        {
          this.InvalidatePane(this._hoverPane);
          this._hoverPane = (NavigationPane) null;
        }
        if (this._mouseOnChevron && !this._mouseDown)
        {
          this._mouseOnChevron = false;
          Rectangle chevronRect = this._chevronRect;
          chevronRect.Inflate(2, 2);
          this.Invalidate(chevronRect);
        }
        this._mouseDown = false;
        base.OnMouseLeave(e);
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        if (this._resizing)
        {
          float num1 = ((float) (this.ClientRectangle.Height - e.Y) - (float) this._j.Height) / (float) this._j.Height;
          int num2 = (double) num1 - (double) (int) num1 < 0.5 ? (int) (float) Math.Floor((double) num1) : (int) (float) Math.Ceiling((double) num1);
          if (num2 < 0)
            num2 = 0;
          if (num2 > this.ListedCount())
            num2 = this.ListedCount();
          this.ShowPanes = num2;
        }
        else
        {
          NavigationPane navigationPane = this.GetPaneAt(e.X, e.Y);
          if (navigationPane != null && navigationPane._hided)
          {
            if (navigationPane != this._hoverPaneButton)
            {
              this._hoverPaneButton = navigationPane;
              this._toolTip.SetToolTip((Control) this, navigationPane.Text);
            }
          }
          else
          {
            if (this._chevronRect.Contains(e.X, e.Y))
              this._toolTip.SetToolTip((Control) this, "Управление разделами");
            else
              this._toolTip.SetToolTip((Control) this, string.Empty);
            this._hoverPaneButton = (NavigationPane) null;
          }
          if (navigationPane != null && !navigationPane.Enabled)
            navigationPane = (NavigationPane) null;
          if (navigationPane != this._hoverPane)
          {
            if (this._hoverPane != null)
              this.InvalidatePane(this._hoverPane);
            this._hoverPane = navigationPane;
            if (this._hoverPane != null)
              this.InvalidatePane(this._hoverPane);
          }
          if (this._drawActionsButton)
          {
            bool flag = this._chevronRect.Contains(e.X, e.Y);
            if (flag != this._mouseOnChevron)
            {
              this._mouseOnChevron = flag;
              Rectangle chevronRect = this._chevronRect;
              chevronRect.Inflate(2, 2);
              this.Invalidate(chevronRect);
            }
          }
          if (this._gripRect.Contains(e.X, e.Y))
            base.Cursor = Cursors.SizeNS;
          else if (this._hoverPane != null || this._mouseOnChevron)
            base.Cursor = Cursors.Hand;
          else
            base.Cursor = Cursors.Default;
          base.OnMouseMove(e);
        }
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
        this._resizing = false;
        if (this._mouseDown && this._hoverPane != null)
        {
          this._mouseDown = false;
          this.SelectedPane = this._hoverPane;
        }
        base.OnMouseUp(e);
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        this._renderer.DrawBackground(e.Graphics, this.ClientRectangle, this.BackColor);
        if (this._selectedPane != null && this._headerRect != Rectangle.Empty)
          this._renderer.DrawHeader(e.Graphics, this._headerRect, this._selectedPane.Text, this._headerFont, (Image) null);
        if (this._j != Rectangle.Empty)
          this._renderer.DrawContentPaneBackground(e.Graphics, this._j, DrawItemState.None);
        if (this._drawActionsButton)
        {
          DrawItemState state = DrawItemState.None;
          if (this._mouseOnChevron)
          {
            state |= DrawItemState.HotLight;
            if (this._mouseDown)
              state |= DrawItemState.Selected;
          }
          if (state != DrawItemState.None)
            this._renderer.DrawFooterPaneBackground(e.Graphics, this._chevronRect, state);
        }
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          if (control is NavigationPane)
          {
            NavigationPane pane = (NavigationPane) control;
            if (pane.Listed && !(pane._d == Rectangle.Empty) && pane._d.X >= 0)
            {
              DrawItemState state = DrawItemState.None;
              if (this._hoverPane == pane)
                state |= DrawItemState.HotLight;
              if (this._mouseDown && this._hoverPane == pane)
                state |= DrawItemState.Selected;
              if (this._selectedPane == pane)
                state |= DrawItemState.Checked;
              if (!pane.Enabled)
                state |= DrawItemState.Disabled;
              if (!pane._hided)
              {
                this._renderer.DrawContentPaneBackground(e.Graphics, pane._d, state);
                this._renderer.DrawContentPane(e.Graphics, pane._d, state, pane, this._panelFont);
              }
              else
              {
                this._renderer.DrawFooterPaneBackground(e.Graphics, pane._d, state);
                this._renderer.DrawFooterPane(e.Graphics, pane._d, state, pane, this._panelFont);
              }
            }
          }
        }
        this._renderer.DrawGripper(e.Graphics, this._gripRect);
        if (!this._drawActionsButton)
          return;
        this._renderer.DrawChevron(e.Graphics, this._chevronRect);
      }

      protected override void OnParentChanged(EventArgs e)
      {
        base.OnParentChanged(e);
        if (this._disposed)
          return;
        this.CalcLayout();
      }

      protected override void OnResize(EventArgs e)
      {
        base.OnResize(e);
        this.CalcLayout();
      }

      public void SetActiveRenderer(INavBarRenderer renderer)
      {
        if (renderer == null)
          throw new ArgumentException();
        if (this._renderer != null)
          this._renderer.RedrawRequired -= new EventHandler(this.RendererRedrawRequired);
        this._renderer = renderer;
        if (this._renderer != null)
          this._renderer.RedrawRequired += new EventHandler(this.RendererRedrawRequired);
        this.Invalidate();
      }

      MenuAnimation IPopupMenuHost.MenuAnimation => MenuAnimation.System;

      ImageList IPopupMenuHost.MenuImageList => (ImageList) null;

      IMenuRenderer IPopupMenuHost.Renderer
      {
        get
        {
          return this.Renderer is IMenuRenderer ? (IMenuRenderer) this.Renderer : throw new InvalidOperationException("Eyefinder renderer does not implement IMenuRenderer.");
        }
      }

      bool IPopupMenuHost.FullMenus => true;

      bool IPopupMenuHost.RightToLeft => this.RightToLeft == RightToLeft.Yes;

      bool IPopupMenuHost.RightAlignMenus => SystemInformation.RightAlignedMenus;

      public Screen Screen => Screen.FromPoint(this.PointToScreen(new Point(0, 0)));

      Intermech.Bars.ToolBar IPopupMenuHost.ToolBar => (Intermech.Bars.ToolBar) null;

      public bool Vertical => false;

      public ToolBarLayout Flow => ToolBarLayout.Vertical;

      protected override void WndProc(ref Message m) => base.WndProc(ref m);

      [Browsable(false)]
      public string AddRemoveButtonsText
      {
        get => this._addRemoveButtonsText;
        set => this._addRemoveButtonsText = value;
      }

      private bool ShouldSerializeAddRemoveButtonsText() => false;

      [Browsable(false)]
      public override Image BackgroundImage
      {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
      }

      [Browsable(false)]
      public override Cursor Cursor
      {
        get => base.Cursor;
        set => base.Cursor = value;
      }

      [DefaultValue(DockStyle.Left)]
      public override DockStyle Dock
      {
        get => base.Dock;
        set => base.Dock = value;
      }

      [Description("Indicates whether the actions button is drawn.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public bool DrawActionsButton
      {
        get => this._drawActionsButton;
        set
        {
          this._drawActionsButton = value;
          this.CalcLayout();
        }
      }

      [Browsable(false)]
      public string FewerButtonsText
      {
        get => this._fewerButtonsText;
        set => this._fewerButtonsText = value;
      }

      private bool ShouldSerializeFewerButtonsText() => false;

      [Browsable(false)]
      public override Color ForeColor
      {
        get => base.ForeColor;
        set => base.ForeColor = value;
      }

      [Browsable(false)]
      public string MoreButtonsText
      {
        get => this._moreButtonsText;
        set => this._moreButtonsText = value;
      }

      private bool ShouldSerializeMoreButtonsText() => false;

      [Browsable(false)]
      public string PaneOptionsText
      {
        get => this._paneOptionsText;
        set => this._paneOptionsText = value;
      }

      private bool ShouldSerializePaneOptionsText() => false;

      [Browsable(false)]
      public INavBarRenderer Renderer => this._renderer;

      [Browsable(false)]
      public NavigationPane SelectedPane
      {
        get => this._selectedPane;
        set
        {
          this._selectedPane = value == null || this.Controls.Contains((Control) value) ? value : throw new ArgumentException("Specified pane is not present.");
          this.CalcLayout();
          if (this.SelectedPaneChanged == null)
            return;
          this.SelectedPaneChanged((object) this, EventArgs.Empty);
        }
      }

      [DefaultValue(0)]
      [Description("Indicates how many panes to show in the main area of the control.")]
      [Category("Appearance")]
      public int ShowPanes
      {
        get => this._showedPanes;
        set
        {
          this._showedPanes = value >= 0 && value <= this.ListedCount() ? value : throw new ArgumentException();
          this.PerformLayout();
        }
      }

      public INavigationPane CreatePane(string name)
      {
        NavigationPane pane = new NavigationPane();
        pane.Name = name;
        this.Controls.Add((Control) pane);
        return (INavigationPane) pane;
      }

      public INavigationPane FindPane(string name)
      {
        foreach (NavigationPane control in (ArrangedElementCollection) this.Controls)
        {
          if (control.Name == name)
            return (INavigationPane) control;
        }
        return (INavigationPane) null;
      }

      public IAppPane CeateAppPane(string name)
      {
        AppPane appPane = new AppPane();
        appPane.Name = name;
        this.Controls.Add((Control) appPane);
        return (IAppPane) appPane;
      }

      [Browsable(false)]
      public INavigationPane[] Panes
      {
        get
        {
          ArrayList arrayList = new ArrayList();
          foreach (Control control in (ArrangedElementCollection) this.Controls)
          {
            if (control is NavigationPane)
              arrayList.Add((object) control);
          }
          return (INavigationPane[]) arrayList.ToArray(typeof (INavigationPane));
        }
      }

      private class PaneMenuItem : MenuButtonItem
      {
        public NavigationPane _pane;
        public bool _b;
      }
    }
}
