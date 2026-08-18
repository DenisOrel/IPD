
// Type: Intermech.Bars.ToolBar
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [Designer(typeof (ToolBarDesigner))]
    [DefaultEvent("ButtonClick")]
    public class ToolBar : Control, IPopupMenuHost, IButtonsSite
    {
      internal const int _a = 3;
      private bool _stretch;
      private Guid _guid;
      private bool _allowHorizontalDock;
      private bool _allowVerticalDock;
      private WeakReference _lastFixedContainer;
      private Size _minimumFloatingSize;
      private Size _maximumFloatingSize;
      private bool _resizable;
      private bool _drawActionsButton;
      private IToolBarRenderer _renderer;
      private bool _rendererNeedDispose;
      private ImageList _imageList;
      private bool _inMenuLoop;
      internal bool _contained;
      internal Size an;
      internal bool ao;
      internal int ap;
      internal ToolBarSituation aq;
      internal bool _wrapped;
      internal Rectangle _grabHandleBounds;
      private bool _allowMerge;
      private ToolBar _mergedToolBar;
      private ToolbarStructure _originalStructure;
      private const int _b = 5;
      internal const int _c = 7;
      private const int _d = 13;
      protected ToolbarItemBaseCollection _items;
      private ToolbarItemBase _highlightedItem;
      private bool _flipLastItem;
      private ToolBarOverflow _overflow;
      private ToolBarLayout _flow;
      private bool _ignoreLayoutRequests;
      private ToolBarTextAlign _textAlign;
      private ToolbarItemBase _stretchItem;
      private bool _allowRightToLeft;
      private ToolBarButtonsCustomizeMenu _actionsButton;
      internal bool _itemPushed;
      internal TopLevelMenuItemBase p;
      private MenuAnimation _menuAnimation;
      private bool _showShortcutsInToolTips;
      private ToolTips _toolTips;
      private ToolBarSituation _situation;
      internal int _dockLine;
      private int _dockOffset;
      internal ToolBarDocker _docker;
      private bool _movable;
      private bool _tearable;
      private bool _closable;
      private int _updateCount;
      private bool _fullMenus = true;

      public event ToolBar.ButtonClickEventHandler ButtonClick;

      public event EventHandler EnterMenuLoop;

      public event EventHandler ExitMenuLoop;

      public event EventHandler CustomizeActionsButtonMenu;

      public ToolBar()
      {
        this._flipLastItem = false;
        this._overflow = ToolBarOverflow.Chevron;
        this._flow = ToolBarLayout.Horizontal;
        this._ignoreLayoutRequests = false;
        this._textAlign = ToolBarTextAlign.Side;
        this._stretchItem = (ToolbarItemBase) null;
        this._allowRightToLeft = false;
        this._menuAnimation = MenuAnimation.System;
        this._showShortcutsInToolTips = false;
        this._toolTips = (ToolTips) null;
        this._situation = ToolBarSituation.Standalone;
        this._dockLine = 0;
        this._dockOffset = 0;
        this._docker = (ToolBarDocker) null;
        this._movable = true;
        this._tearable = true;
        this._closable = true;
        this._stretch = false;
        this._allowHorizontalDock = true;
        this._allowVerticalDock = true;
        this._minimumFloatingSize = new Size(60, 30);
        this._maximumFloatingSize = Size.Empty;
        this._resizable = true;
        this._drawActionsButton = true;
        this._inMenuLoop = false;
        this._contained = false;
        this.an = Size.Empty;
        this.ao = false;
        this.ap = 0;
        this.aq = ToolBarSituation.Contained;
        this._wrapped = false;
        this._allowMerge = false;
        this._mergedToolBar = (ToolBar) null;
        this._originalStructure = (ToolbarStructure) null;
        this.Initialize();
      }

      private void Initialize()
      {
        this.Text = "Tool Bar";
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.Selectable, false);
        this.Dock = DockStyle.Top;
        this._items = (ToolbarItemBaseCollection) new ToolBar.ToolBarItemCollection((IButtonsSite) this);
        this._renderer = (IToolBarRenderer) new Office2003Renderer();
        this._renderer.RedrawRequired += new EventHandler(this.Renderer_RedrawRequired);
        this._rendererNeedDispose = true;
        this._actionsButton = new ToolBarButtonsCustomizeMenu(this);
        this._toolTips = new ToolTips((Control) this);
        this._toolTips.DropShadow = false;
        this._toolTips.GetToolTipText += new ToolTips.GetToolTipTextEventHandler(this.GetTooTipText);
        this._guid = Guid.NewGuid();
      }

      private string GetTooTipText(Point pos)
      {
        ToolbarItemBase itemAt = this.GetItemAt(pos);
        if (itemAt == null)
          return string.Empty;
        string tooTipText = itemAt.ToolTipText;
        if (itemAt is ButtonItem && this.ShowShortcutsInToolTips)
        {
          ButtonItem buttonItem = (ButtonItem) itemAt;
          if (buttonItem.BuddyMenu != null && buttonItem.BuddyMenu.Shortcut != Shortcut.None)
            tooTipText = $"{tooTipText} ({buttonItem.BuddyMenu.FriendlyShortcut})";
        }
        return tooTipText;
      }

      internal Size GetPreferredSizeWithExtent(int extent)
      {
        return this.GetPreferredSizeWithExtent(extent, out bool _);
      }

      internal object GetServiceInternal(System.Type type) => this.GetService(type);

      internal void SetMergedToolBar(ToolBar toolbar) => this._mergedToolBar = toolbar;

      private void Renderer_RedrawRequired(object sender, EventArgs e)
      {
        if (this.Situation == ToolBarSituation.Floating)
        {
          if (this.Parent == null)
            return;
          this.Parent.Invalidate(true);
        }
        else
          this.Invalidate(true);
      }

      private void DoPaint(PaintEventArgs pea, IToolBarRenderer renderer)
      {
        bool chevron = false;
        bool vertical = this.Flow == ToolBarLayout.Vertical;
        bool flag = this.RightToLeft == RightToLeft.Yes && this.AllowRightToLeft;
        ISelectionService selectionService = (ISelectionService) null;
        if (this.DesignMode)
          selectionService = (ISelectionService) this.GetService(typeof (ISelectionService));
        renderer.StartToolBarRender(this, vertical, flag);
        if ((this.Situation == ToolBarSituation.Contained || this is ContainerBar) && this._movable)
          renderer.DrawToolBarGrabHandle(pea.Graphics, this._grabHandleBounds, vertical);
        foreach (ToolbarItemBase component in (CollectionBase) this.Items)
        {
          if (component.Visible)
          {
            if (component._underChevron)
              chevron = true;
            else
              this.DrawButton(renderer, pea.Graphics, component, vertical, flag, this.DesignMode && selectionService.GetComponentSelected((object) component));
          }
        }
        foreach (ToolbarItemBase extraButton in this.ExtraButtons)
        {
          if (extraButton != this._actionsButton)
            this.DrawButton(renderer, pea.Graphics, extraButton, vertical, flag, false);
        }
        if (this.DrawActionsButton && this.Situation == ToolBarSituation.Contained && !(this is ContainerBar))
        {
          DrawItemState state = DrawItemState.Default;
          if (this.HighlightedItem == this._actionsButton)
          {
            state |= DrawItemState.HotLight;
            if (this._itemPushed)
              state |= DrawItemState.Selected;
          }
          if (this._actionsButton.DrawDroppedDown)
            state |= DrawItemState.HotLight | DrawItemState.Selected;
          renderer.DrawToolBarActionsButton(pea.Graphics, this._actionsButton.ButtonBounds, vertical, chevron, state, this.DesignMode);
        }
        renderer.FinishToolBarRender();
      }

      internal void MakeFloating(BarManager barManager, Point A_1, bool A_2)
      {
        if (barManager == null)
          throw new ArgumentNullException();
        if (!(this.Parent is FloatingToolbarForm))
        {
          Font font = new Font(this.Font, this.Font.Style);
          RightToLeft rightToLeft = this.RightToLeft;
          if (this.Parent != null)
            this.Parent.Controls.Remove((Control) this);
          FloatingToolbarForm ownedForm = new FloatingToolbarForm(this, barManager, rightToLeft);
          ownedForm.Font = font;
          ownedForm.RightToLeft = rightToLeft;
          Size size = this.f();
          ownedForm.SetSize(size);
          if (barManager.OwnerForm != null)
            barManager.OwnerForm.AddOwnedForm((Form) ownedForm);
        }
        this.Parent.Location = A_1;
        if (A_2)
          return;
        ((TopForm) this.Parent).MakeVisible();
      }

      private void DrawButton(
        IToolBarRenderer renderer,
        Graphics g,
        ToolbarItemBase item,
        bool vertical,
        bool A_4,
        bool A_5)
      {
        if (item._drawSeparator)
          renderer.DrawToolBarSeparator(g, item._separatorBounds, vertical);
        DrawItemState drawItemState = DrawItemState.Default;
        if (item == this.HighlightedItem | A_5)
          drawItemState |= DrawItemState.HotLight;
        if ((drawItemState & DrawItemState.HotLight) == DrawItemState.HotLight && this._itemPushed)
          drawItemState |= DrawItemState.Selected;
        if (item is ButtonItemBase && ((ButtonItemBase) item).Checked)
          drawItemState |= DrawItemState.Checked;
        if (!item.Enabled || !this.Enabled)
          drawItemState |= DrawItemState.Disabled;
        renderer.DrawToolBarItem(item, g, this.Font, vertical, drawItemState, this._textAlign);
        if (!(item is SystemButonBase))
          return;
        (item as SystemButonBase).Paint(g, drawItemState);
      }

      private void b(Point A_0)
      {
        ToolbarItemBase toolbarItemBase = this.GetItemAt(new Point(A_0.X, A_0.Y));
        if (toolbarItemBase == null)
        {
          foreach (ToolbarItemBase extraButton in this.ExtraButtons)
          {
            Rectangle buttonBounds = extraButton.ButtonBounds;
            ++buttonBounds.Width;
            ++buttonBounds.Height;
            if (buttonBounds.Contains(A_0.X, A_0.Y))
              toolbarItemBase = extraButton;
          }
        }
        if (this.HighlightedItem == toolbarItemBase)
          return;
        if (toolbarItemBase != null && !toolbarItemBase.Enabled)
          toolbarItemBase = (ToolbarItemBase) null;
        if (this.HighlightedItem == toolbarItemBase)
          return;
        this.HighlightedItem = toolbarItemBase;
      }

      internal void b(ToolbarItemBase A_0)
      {
        if (this.HighlightedItem != A_0)
          return;
        this.HighlightedItem = (ToolbarItemBase) null;
      }

      private void ImageList_Disposed(object A_0, EventArgs A_1) => this.ImageList = (ImageList) null;

      internal virtual Rectangle ButtonStripBoundsFromToolBarBounds(Rectangle toolbarBounds)
      {
        int num1 = 3 + this.LeftPadding;
        if (this.Situation == ToolBarSituation.Contained && this.Movable)
          num1 += 5;
        int num2 = 3 + this.RightPadding;
        if (this.Situation == ToolBarSituation.Contained && this.DrawActionsButton)
          num2 += 13;
        int num3 = 1;
        int num4 = 1;
        if (this.Situation == ToolBarSituation.Contained)
        {
          ++num3;
          ++num4;
        }
        if (this.Flow == ToolBarLayout.Horizontal)
        {
          toolbarBounds.Offset(num1, num3);
          toolbarBounds.Width -= num1 + num2;
          toolbarBounds.Height -= num3 + num4;
          return toolbarBounds;
        }
        toolbarBounds.Offset(num3, num1);
        toolbarBounds.Width -= num3 + num4;
        toolbarBounds.Height -= num1 + num2;
        return toolbarBounds;
      }

      private void ImageList_RecreateHandle(object A_0, EventArgs A_1) => this.DoLayout();

      internal virtual void CalculateActionsButtonBounds()
      {
        if (!this.DrawActionsButton || this.Situation != ToolBarSituation.Contained)
          return;
        this._actionsButton.ApplyLayout(this.Flow != ToolBarLayout.Vertical ? new Rectangle(this.ClientRectangle.Width - 13, 0, 13, this.ClientRectangle.Height) : new Rectangle(1, this.ClientRectangle.Height - 12, this.ClientRectangle.Width - 2, 13), (Graphics) null, false, false);
      }

      internal virtual void CalculateGripperBounds()
      {
        if (this.Situation == ToolBarSituation.Contained && this.Movable)
        {
          if (this.Flow == ToolBarLayout.Vertical)
            this._grabHandleBounds = new Rectangle(5, 1, this.ClientRectangle.Width - 8, 6);
          else
            this._grabHandleBounds = new Rectangle(1, 5, 6, this.ClientRectangle.Height - 8);
        }
        else
          this._grabHandleBounds = Rectangle.Empty;
      }

      internal virtual void CalculateLayoutInternal(IToolBarRenderer renderer, bool vertical)
      {
        if (this._ignoreLayoutRequests || !this.IsHandleCreated)
          return;
        this._ignoreLayoutRequests = true;
        if (this.Situation == ToolBarSituation.Standalone)
        {
          Size preferredSizeWithExtent = this.GetPreferredSizeWithExtent(vertical ? this.Height : this.Width);
          if (vertical && this.Width != preferredSizeWithExtent.Width)
            this.Width = preferredSizeWithExtent.Width;
          else if (!vertical && this.Height != preferredSizeWithExtent.Height)
            this.Height = preferredSizeWithExtent.Height;
        }
        Rectangle A_2 = this.ButtonStripBoundsFromToolBarBounds(this.ClientRectangle);
        this.CalculateGripperBounds();
        this.CalculateActionsButtonBounds();
        bool rightToLeft = this.RightToLeft == RightToLeft.Yes && this.AllowRightToLeft;
        using (Graphics graphics = this.CreateGraphics())
          ToolBarMeasure.a(this, graphics, A_2, renderer, vertical, rightToLeft, this.FlipLastItem);
        this.Invalidate();
        this._ignoreLayoutRequests = false;
      }

      [Obsolete("Use the Visible property instead.")]
      public void Close() => this.Visible = false;

      internal void DoLayout()
      {
        if (this._ignoreLayoutRequests || this.Parent == null)
          return;
        this.an = Size.Empty;
        bool rightToLeft = this.RightToLeft == RightToLeft.Yes && this.AllowRightToLeft;
        switch (this.Situation)
        {
          case ToolBarSituation.Standalone:
            this.WorkingRenderer.StartToolBarRender(this, this._flow == ToolBarLayout.Vertical, rightToLeft);
            this.WorkingRenderer.FinishToolBarRender();
            this.CalculateLayoutInternal(this.WorkingRenderer, this._flow == ToolBarLayout.Vertical);
            break;
          case ToolBarSituation.Contained:
            this._contained = true;
            ((ToolBarContainer) this.Parent).DoLayout();
            break;
          case ToolBarSituation.Floating:
            ((FloatingToolbarForm) this.Parent).d();
            break;
        }
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          ToolbarItemBase[] array = new ToolbarItemBase[this.Items.Count];
          this.Items.CopyTo(array, 0);
          this.Items.Clear();
          foreach (Component component in array)
            component.Dispose();
          this._renderer.RedrawRequired -= new EventHandler(this.Renderer_RedrawRequired);
          if (this._rendererNeedDispose)
          {
            this._renderer.Dispose();
            this._rendererNeedDispose = false;
          }
          this._actionsButton.Dispose();
          this._toolTips.GetToolTipText -= new ToolTips.GetToolTipTextEventHandler(this.GetTooTipText);
          this._toolTips.Dispose();
          this.ImageList = (ImageList) null;
        }
        base.Dispose(disposing);
      }

      internal Size f() => this.GetPreferredSizeWithExtent(int.MaxValue);

      public void Float(BarManager manager, Point desktopLocation)
      {
        this.MakeFloating(manager, desktopLocation, false);
      }

      public void BeginUpdate() => this.BeginUpdateInternal();

      public void EndUpdate() => this.EndUpdateInternal();

      internal new void BeginUpdateInternal()
      {
        if (!this.IsHandleCreated)
          return;
        if (this._updateCount == 0)
          this.SendMessage(11, 0, 0);
        ++this._updateCount;
      }

      internal new bool EndUpdateInternal() => this.EndUpdateInternal(true);

      internal new bool EndUpdateInternal(bool invalidate)
      {
        if (this._updateCount <= 0)
          return false;
        this._updateCount = (int) (short) (this._updateCount - 1);
        if (this._updateCount == 0)
        {
          this.SendMessage(11, -1, 0);
          if (invalidate)
          {
            this._ignoreLayoutRequests = false;
            this.DoLayout();
            this.Invalidate(true);
          }
        }
        return true;
      }

      internal new IntPtr SendMessage(int msg, int wparam, int lparam)
      {
        return Win32.SendMessage(this.Handle, msg, wparam, lparam);
      }

      public ToolbarItemBase GetItemAt(Point position)
      {
        if (this.HighlightedItem != null)
        {
          Rectangle buttonBounds = this.HighlightedItem.ButtonBounds;
          ++buttonBounds.Width;
          ++buttonBounds.Height;
          if (buttonBounds.Contains(position))
            return this.HighlightedItem;
        }
        foreach (ToolbarItemBase itemAt in (CollectionBase) this.Items)
        {
          if (itemAt.Visible && !itemAt._underChevron)
          {
            Rectangle buttonBounds = itemAt.ButtonBounds;
            ++buttonBounds.Width;
            ++buttonBounds.Height;
            if (buttonBounds.Contains(position))
              return itemAt;
          }
        }
        return (ToolbarItemBase) null;
      }

      internal virtual Size GetPreferredSizeWithExtent(int extent, out bool wrapped)
      {
        Size buttonStripSize = new Size(100, 100);
        Size size = this.ToolBarSizeFromButtonStripSize(buttonStripSize);
        extent -= this.Flow == ToolBarLayout.Horizontal ? size.Width - buttonStripSize.Width : size.Height - buttonStripSize.Height;
        Size preferredSizeWithExtent;
        using (Graphics graphics = this.CreateGraphics())
          preferredSizeWithExtent = ToolBarMeasure.GetPreferredSizeWithExtent(this, graphics, this.WorkingRenderer, this.Flow == ToolBarLayout.Vertical, extent, out wrapped);
        return this.ToolBarSizeFromButtonStripSize(preferredSizeWithExtent);
      }

      protected internal virtual void OnButtonClick(ToolBarItemEventArgs e)
      {
        if (this.ButtonClick == null)
          return;
        this.ButtonClick((object) this, e);
      }

      protected override void OnChangeUICues(UICuesEventArgs e)
      {
        base.OnChangeUICues(e);
        this.Invalidate();
      }

      protected internal virtual void OnCloseButtonPressed() => this.Hide();

      protected internal virtual void OnCustomizeActionsButtonMenu(EventArgs e)
      {
        if (this.CustomizeActionsButtonMenu == null)
          return;
        this.CustomizeActionsButtonMenu((object) this, e);
      }

      protected internal void OnEnterMenuLoop()
      {
        if (this.EnterMenuLoop != null)
          this.EnterMenuLoop((object) this, EventArgs.Empty);
        this._inMenuLoop = true;
      }

      protected internal void OnExitMenuLoop()
      {
        if (this.ExitMenuLoop != null)
          this.ExitMenuLoop((object) this, EventArgs.Empty);
        this._inMenuLoop = false;
      }

      protected override void OnFontChanged(EventArgs e)
      {
        base.OnFontChanged(e);
        this.DoLayout();
      }

      protected override void OnHandleCreated(EventArgs e)
      {
        base.OnHandleCreated(e);
        this.DoLayout();
      }

      protected virtual void OnItemPush(ToolbarItemBase item, Point position)
      {
        if (item == this._actionsButton)
        {
          this._actionsButton.ShowMenu();
        }
        else
        {
          if (item is TopLevelMenuItemBase)
          {
            if (item is DropDownMenuItem)
            {
              Rectangle buttonBounds = item.ButtonBounds;
              if (position.X > buttonBounds.Right - 11)
              {
                ((TopLevelMenuItemBase) item).Show();
                return;
              }
            }
            else
            {
              ((TopLevelMenuItemBase) item).Show();
              return;
            }
          }
          this._itemPushed = true;
          item.Invalidate();
        }
      }

      protected virtual void OnItemRelease(ToolbarItemBase item, Point position)
      {
        if (!this._itemPushed || !(item is ButtonItemBase))
          return;
        ((ButtonItemBase) item).OnActivate();
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
        base.OnMouseDown(e);
        if (e.Button != MouseButtons.Left)
          return;
        this.b(new Point(e.X, e.Y));
        if (this.HighlightedItem != null)
        {
          if (!(this.HighlightedItem is ButtonItemBase))
            return;
          this.OnItemPush(this.HighlightedItem, new Point(e.X, e.Y));
        }
        else
        {
          if (this.HighlightedItem != null || !this._movable)
            return;
          if (this.Situation == ToolBarSituation.Contained)
          {
            this.Cursor = Cursors.SizeAll;
            this._docker = new ToolBarDocker(this, e);
            this.Capture = true;
          }
          else
          {
            if (this.Situation != ToolBarSituation.Floating)
              return;
            Point client = this.Parent.PointToClient(this.PointToScreen(new Point(e.X, e.Y)));
            ((FloatingToolbarForm) this.Parent).a(new MouseEventArgs(MouseButtons.None, 0, client.X, client.Y, 0));
          }
        }
      }

      protected override void OnMouseLeave(EventArgs e)
      {
        if (this.HighlightedItem != null && !this._inMenuLoop)
          this.HighlightedItem = (ToolbarItemBase) null;
        this.Cursor = Cursors.Default;
        base.OnMouseLeave(e);
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        base.OnMouseMove(e);
        if (this._docker != null)
        {
          this._docker.OnMouseMove(e);
        }
        else
        {
          if (e.Button == MouseButtons.None)
          {
            if (this._movable && this._grabHandleBounds.Contains(e.X, e.Y))
              this.Cursor = Cursors.SizeAll;
            else
              this.Cursor = Cursors.Default;
          }
          if (this._itemPushed)
            return;
          this.b(new Point(e.X, e.Y));
        }
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
        if (e.Button == MouseButtons.Left)
        {
          if (this.HighlightedItem != null)
          {
            if (this.HighlightedItem.ButtonBounds.Contains(e.X, e.Y))
              this.OnItemRelease(this.HighlightedItem, new Point(e.X, e.Y));
            if (this.HighlightedItem != null)
              this.HighlightedItem.Invalidate();
          }
          this._itemPushed = false;
        }
        if (e.Button == MouseButtons.Right && this.Parent is ToolBarContainer)
          ((ToolBarContainer) this.Parent).Manager.CustomizeToolbars(this, (Control) this, new Point(e.X, e.Y));
        base.OnMouseUp(e);
      }

      internal virtual void OnOwnerFormActivated()
      {
      }

      internal virtual void OnOwnerFormDeactivated()
      {
      }

      protected override void OnPaint(PaintEventArgs e) => this.DoPaint(e, this.WorkingRenderer);

      protected override void OnPaintBackground(PaintEventArgs pevent)
      {
        this.WorkingRenderer.DrawToolBarBackground(this, pevent.Graphics, this.ClientRectangle, this.Flow == ToolBarLayout.Vertical);
      }

      protected override void OnParentChanged(EventArgs e)
      {
        base.OnParentChanged(e);
        if (this.Parent is ToolBarContainer)
        {
          this._situation = ToolBarSituation.Contained;
          if (this._docker == null)
            this._lastFixedContainer = new WeakReference((object) (ToolBarContainer) this.Parent);
        }
        else
          this._situation = !(this.Parent is FloatingToolbarForm) ? ToolBarSituation.Standalone : ToolBarSituation.Floating;
        this.DoLayout();
      }

      protected internal virtual void OnRendererChanged()
      {
        this.Renderer_RedrawRequired((object) null, (EventArgs) null);
      }

      protected override void OnResize(EventArgs e)
      {
        base.OnResize(e);
        if (this.Situation != ToolBarSituation.Standalone)
          return;
        this.DoLayout();
      }

      [Obsolete("Use the Visible property instead.")]
      public void Open() => this.Visible = true;

      protected override bool ProcessMnemonic(char charCode)
      {
        if ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)
          return false;
        foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this._items)
        {
          if (this.Enabled && this.Visible && toolbarItemBase.Visible && toolbarItemBase.Enabled && Control.IsMnemonic(charCode, toolbarItemBase.Text))
          {
            switch (toolbarItemBase)
            {
              case TopLevelMenuItemBase _:
                ((TopLevelMenuItemBase) toolbarItemBase).Show(true);
                break;
              case ButtonItemBase _:
                ((ButtonItemBase) toolbarItemBase).OnActivate();
                break;
            }
            return true;
          }
        }
        return base.ProcessMnemonic(charCode);
      }

      public void Redock(Control container) => this.Parent = container;

      protected override void SetBoundsCore(
        int x,
        int y,
        int width,
        int height,
        BoundsSpecified specified)
      {
        if (this.Situation == ToolBarSituation.Standalone && this.IsHandleCreated)
        {
          bool flag = this.Flow == ToolBarLayout.Vertical;
          Size preferredSizeWithExtent = this.GetPreferredSizeWithExtent(flag ? height : width);
          if (flag)
            width = preferredSizeWithExtent.Width;
          else
            height = preferredSizeWithExtent.Height;
        }
        base.SetBoundsCore(x, y, width, height, specified);
      }

      protected override void SetVisibleCore(bool value)
      {
        if (this.Situation == ToolBarSituation.Floating)
        {
          base.SetVisibleCore(value);
          FloatingToolbarForm parent = (FloatingToolbarForm) this.Parent;
          if (!parent.BarManager.FormHasFocus)
          {
            parent.Hide();
            parent.b(value);
          }
          else if (value)
            parent.MakeVisible();
          else
            parent.Hide();
        }
        else
          base.SetVisibleCore(value);
      }

      private bool ShouldSerializeFlow()
      {
        return this.Situation == ToolBarSituation.Standalone && this._flow != 0;
      }

      private bool ShouldSerializeRenderer() => this.Renderer.GetType() != typeof (Office2003Renderer);

      ImageList IPopupMenuHost.MenuImageList
      {
        get
        {
          if (this.HighlightedItem is DropDownMenuItem && ((DropDownMenuItem) this.HighlightedItem).MenuImageList != null)
            return ((DropDownMenuItem) this.HighlightedItem).MenuImageList;
          return this.p != null && this.p is DropDownMenuItem && ((DropDownMenuItem) this.p).MenuImageList != null ? ((DropDownMenuItem) this.p).MenuImageList : this.ImageList;
        }
      }

      IMenuRenderer IPopupMenuHost.Renderer => (IMenuRenderer) this.WorkingRenderer;

      bool IPopupMenuHost.RightAlignMenus => SystemInformation.RightAlignedMenus;

      bool IPopupMenuHost.RightToLeft => this.RightToLeft == RightToLeft.Yes;

      ToolBar IPopupMenuHost.ToolBar => this;

      void IButtonsSite.ChildItemsChanged()
      {
        this.DoLayout();
        if (!(this is MenuBar))
          return;
        ((MenuBar) this).ShortcutListener.UpdateAcceleratorTable(this);
      }

      [Browsable(false)]
      Control IButtonsSite.ControlHost => (Control) this;

      internal virtual Size ToolBarSizeFromButtonStripSize(Size buttonStripSize)
      {
        int num1 = 0 + 6 + (this.LeftPadding + this.RightPadding);
        if (this.Situation == ToolBarSituation.Contained && this.Movable)
          num1 += 5;
        if (this.DrawActionsButton && this.Situation == ToolBarSituation.Contained)
          num1 += 13;
        int num2 = num1 + (this.Flow == ToolBarLayout.Horizontal ? buttonStripSize.Width : buttonStripSize.Height);
        if (num2 < 18)
          num2 = 18;
        int num3 = 2 + (this.Flow == ToolBarLayout.Horizontal ? buttonStripSize.Height : buttonStripSize.Width);
        if (this.Situation == ToolBarSituation.Contained)
          num3 += 2;
        if (num3 < 18)
          num3 = 18;
        return this.Flow != ToolBarLayout.Horizontal ? new Size(num3, num2) : new Size(num2, num3);
      }

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == 533 && this._docker != null && !this._docker.IsRedocking())
        {
          this._docker.Dispose();
          this._docker = (ToolBarDocker) null;
          if (this.Situation == ToolBarSituation.Contained)
            this._lastFixedContainer = new WeakReference((object) (ToolBarContainer) this.Parent);
        }
        base.WndProc(ref m);
      }

      [Browsable(false)]
      public TopLevelMenuItemBase ActionsButton => (TopLevelMenuItemBase) this._actionsButton;

      [Description("Indicates whether the Add/Remove buttons option will be visible in the actions menu.")]
      [DefaultValue(true)]
      [Category("Appearance")]
      public bool AddRemoveButtonsVisible
      {
        get => this._actionsButton.GetAddRemoveVisible();
        set => this._actionsButton.SetAddRemoveVisible(value);
      }

      [DefaultValue(true)]
      [Category("Docking")]
      [Description("Indicates whether the user wil be able to dock this toolbar at the top or bottom of the form.")]
      public bool AllowHorizontalDock
      {
        get => this._allowHorizontalDock;
        set => this._allowHorizontalDock = value;
      }

      [DefaultValue(false)]
      [Description("Indicates whether the MenuBar will allow itself to be merged or allow another MenuBar to merge with it.")]
      [Category("Merging")]
      public bool AllowMerge
      {
        get => this._allowMerge;
        set => this._allowMerge = value;
      }

      [DefaultValue(false)]
      [Description("Indicates whether right to left layout of items in the toolbar is permitted.")]
      [Category("Item Layout")]
      public virtual bool AllowRightToLeft
      {
        get => this._allowRightToLeft;
        set
        {
          this._allowRightToLeft = true;
          this.DoLayout();
        }
      }

      [DefaultValue(true)]
      [Description("Indicates whether the user wil be able to dock this toolbar at the left or right of the form.")]
      [Category("Docking")]
      public bool AllowVerticalDock
      {
        get => this._allowVerticalDock;
        set => this._allowVerticalDock = value;
      }

      [Browsable(false)]
      public override AnchorStyles Anchor
      {
        get => base.Anchor;
        set => base.Anchor = value;
      }

      [Browsable(false)]
      public override Color BackColor
      {
        get => base.BackColor;
        set => base.BackColor = value;
      }

      [Browsable(false)]
      public override Image BackgroundImage
      {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Obsolete("Use the Items property instead.")]
      public ToolbarItemBaseCollection Buttons => this._items;

      [Description("Indicates whether, when floating, the toolbar will display a close button.")]
      [DefaultValue(true)]
      [Category("Docking")]
      public virtual bool Closable
      {
        get => this._closable;
        set
        {
          this._closable = value;
          if (this.Situation != ToolBarSituation.Floating)
            return;
          this.DoLayout();
        }
      }

      [Browsable(false)]
      public override Cursor Cursor
      {
        get => base.Cursor;
        set => base.Cursor = value;
      }

      [DefaultValue(typeof (DockStyle), "Top")]
      public override DockStyle Dock
      {
        get => base.Dock;
        set => base.Dock = value;
      }

      [DefaultValue(0)]
      [Category("Docking")]
      [Description("Indicates the line of toolbars in the container that this toolbar will be on.")]
      public virtual int DockLine
      {
        get => this._dockLine;
        set
        {
          this._dockLine = value;
          if (this.Situation != ToolBarSituation.Contained)
            return;
          ((ToolBarContainer) this.Parent).ForceLayout();
        }
      }

      [DefaultValue(0)]
      [Description("Indicates the offset, in pixels, of this toolbar in the line of toolbars it belongs to.")]
      [Category("Docking")]
      public virtual int DockOffset
      {
        get => this._dockOffset;
        set
        {
          this._dockOffset = value;
          if (this.Situation != ToolBarSituation.Contained)
            return;
          ((ToolBarContainer) this.Parent).ForceLayout();
        }
      }

      [Category("Appearance")]
      [Description("Indicates whether an extra, thin button is drawn on the end of the toolbar.")]
      [DefaultValue(true)]
      public virtual bool DrawActionsButton
      {
        get => this._drawActionsButton;
        set
        {
          this._drawActionsButton = value;
          this.DoLayout();
        }
      }

      internal virtual ToolbarItemBase[] ExtraButtons
      {
        get
        {
          return new ToolbarItemBase[1]
          {
            (ToolbarItemBase) this._actionsButton
          };
        }
      }

      [DefaultValue(false)]
      [Category("Item Layout")]
      [Description("Indicates whether the last item on the toolbar is flipped to the far side of the button space.")]
      public bool FlipLastItem
      {
        get => this._flipLastItem;
        set
        {
          this._flipLastItem = value;
          this.DoLayout();
        }
      }

      [Category("Item Layout")]
      [Description("Indicates how items are laid out within the toolbar.")]
      public virtual ToolBarLayout Flow
      {
        get
        {
          return this.Situation == ToolBarSituation.Contained ? (this.Parent.Dock != DockStyle.Left && this.Parent.Dock != DockStyle.Right ? ToolBarLayout.Horizontal : ToolBarLayout.Vertical) : (this.Situation == ToolBarSituation.Floating ? ToolBarLayout.Horizontal : this._flow);
        }
        set
        {
          this._flow = value;
          if (this.Situation == ToolBarSituation.Standalone)
          {
            if (this._flow == ToolBarLayout.Vertical && (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom))
            {
              this.Dock = DockStyle.Left;
            }
            else
            {
              if (this._flow != ToolBarLayout.Horizontal || this.Dock != DockStyle.Left && this.Dock != DockStyle.Right)
                return;
              this.Dock = DockStyle.Top;
            }
          }
          else
            this.DoLayout();
        }
      }

      [Browsable(false)]
      public override Color ForeColor
      {
        get => base.ForeColor;
        set => base.ForeColor = value;
      }

      internal bool FriendDesignMode => this.DesignMode;

      [Browsable(false)]
      public Guid Guid
      {
        get => this._guid;
        set => this._guid = value;
      }

      internal ToolbarItemBase HighlightedItem
      {
        get => this._highlightedItem;
        set
        {
          if (this._highlightedItem == value)
            return;
          if (this._highlightedItem != null)
            this._highlightedItem.Invalidate();
          this._highlightedItem = value;
          if (this._highlightedItem == null)
            return;
          this._highlightedItem.Invalidate();
        }
      }

      internal bool IgnoreLayoutRequests
      {
        get => this._ignoreLayoutRequests;
        set => this._ignoreLayoutRequests = value;
      }

      [DefaultValue(typeof (ImageList), null)]
      [Category("Appearance")]
      public ImageList ImageList
      {
        get => this._imageList;
        set
        {
          if (this._imageList != null)
          {
            this._imageList.RecreateHandle -= new EventHandler(this.ImageList_RecreateHandle);
            this._imageList.Disposed -= new EventHandler(this.ImageList_Disposed);
          }
          this._imageList = value;
          if (this._imageList != null)
          {
            this._imageList.RecreateHandle += new EventHandler(this.ImageList_RecreateHandle);
            this._imageList.Disposed += new EventHandler(this.ImageList_Disposed);
          }
          this.DoLayout();
        }
      }

      [Browsable(false)]
      [Obsolete("Use the Situation property instead.")]
      public bool IsFloating => this.Situation == ToolBarSituation.Floating;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      public bool IsOpen
      {
        get
        {
          if (this.Situation != ToolBarSituation.Floating)
            return this.Visible;
          return this.Parent.Visible || ((FloatingToolbarForm) this.Parent).h();
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public ToolbarItemBaseCollection Items => this._items;

      [Browsable(false)]
      public ToolBarContainer LastFixedContainer
      {
        get
        {
          return this._lastFixedContainer != null && this._lastFixedContainer.IsAlive ? (ToolBarContainer) this._lastFixedContainer.Target : (ToolBarContainer) null;
        }
      }

      protected internal virtual int LeftPadding => 0;

      [Description("The maximum desired size of the toolbar when floating.")]
      [DefaultValue(typeof (Size), "0,0")]
      [Category("Docking")]
      public virtual Size MaximumFloatingSize
      {
        get => this._maximumFloatingSize;
        set
        {
          this._maximumFloatingSize = value;
          if (this.Situation != ToolBarSituation.Floating)
            return;
          ((FloatingToolbarForm) this.Parent).i();
        }
      }

      [Description("Indicates the animation performed on menu items as they are displayed.")]
      [Category("Behavior")]
      [DefaultValue(typeof (MenuAnimation), "System")]
      public MenuAnimation MenuAnimation
      {
        get => this._menuAnimation;
        set => this._menuAnimation = value;
      }

      [Browsable(false)]
      public virtual bool FullMenus
      {
        get => this._fullMenus;
        set => this._fullMenus = value;
      }

      [Browsable(false)]
      public ToolBar MergedToolBar => this._mergedToolBar;

      [Category("Docking")]
      [DefaultValue(typeof (Size), "60,30")]
      [Description("The minimum desired size of the toolbar when floating.")]
      public virtual Size MinimumFloatingSize
      {
        get => this._minimumFloatingSize;
        set
        {
          this._minimumFloatingSize = value;
          if (this.Situation != ToolBarSituation.Floating)
            return;
          ((FloatingToolbarForm) this.Parent).i();
        }
      }

      [Description("Indicates whether the toolbar will display a grab handle and let the user move it within its container.")]
      [DefaultValue(true)]
      [Category("Docking")]
      public virtual bool Movable
      {
        get => this._movable;
        set
        {
          this._movable = value;
          this.DoLayout();
        }
      }

      internal ToolbarStructure OriginalStructure
      {
        get => this._originalStructure;
        set => this._originalStructure = value;
      }

      [DefaultValue(typeof (ToolBarOverflow), "Chevron")]
      [Description("Indicates how toolbar items that flow off the toolbar's normal width are treated.")]
      [Category("Item Layout")]
      public virtual ToolBarOverflow Overflow
      {
        get => this.Situation == ToolBarSituation.Floating ? ToolBarOverflow.Wrap : this._overflow;
        set
        {
          this._overflow = value;
          this.DoLayout();
        }
      }

      [TypeConverter(typeof (RendererConverter))]
      [Category("Appearance")]
      [Browsable(true)]
      [Description("The renderer used by the toolbar when in a standalone state.")]
      public IToolBarRenderer Renderer
      {
        get => this._renderer;
        set
        {
          if (value == null)
            throw new ArgumentNullException();
          if (this._renderer != null)
          {
            this._renderer.RedrawRequired -= new EventHandler(this.Renderer_RedrawRequired);
            if (this._rendererNeedDispose)
            {
              this._renderer.Dispose();
              this._rendererNeedDispose = false;
            }
          }
          this._renderer = value;
          if (this._renderer != null)
            this._renderer.RedrawRequired += new EventHandler(this.Renderer_RedrawRequired);
          this.Invalidate(true);
          this.OnRendererChanged();
        }
      }

      [Description("Indicates whether the ToolBar is resizable by the user.")]
      [DefaultValue(true)]
      [Category("Docking")]
      public bool Resizable
      {
        get => this._resizable;
        set => this._resizable = value;
      }

      protected internal virtual int RightPadding => 0;

      internal bool ShowKeyboardMnemonics => this.ShowKeyboardCues;

      [DefaultValue(false)]
      [Category("Appearance")]
      [Description("Indicates whether keyboard shortcuts are shown in tooltips. Keyboard shortcuts are retreived from the menu associated with a button.")]
      public bool ShowShortcutsInToolTips
      {
        get => this._showShortcutsInToolTips;
        set => this._showShortcutsInToolTips = value;
      }

      [Browsable(false)]
      public ToolBarSituation Situation => this._situation;

      [Category("Docking")]
      [Description("Indicates whether the toolbar will take up the full extent of its row, where possible.")]
      [DefaultValue(false)]
      public virtual bool Stretch
      {
        get => this._stretch;
        set
        {
          this._stretch = value;
          if (this.Situation != ToolBarSituation.Contained)
            return;
          ((ToolBarContainer) this.Parent).ForceLayout();
        }
      }

      [Category("Item Layout")]
      [DefaultValue(typeof (ToolbarItemBase), null)]
      [Description("Designates one toolbar item to be stretched to occupy all available space.")]
      public ToolbarItemBase StretchItem
      {
        get => this._stretchItem;
        set
        {
          if (value != null && !this.Items.Contains(value))
            throw new ArgumentException();
          if (this._stretchItem != null)
            this._stretchItem.Stretch = false;
          this._stretchItem = value;
          if (this._stretchItem == null)
            return;
          this._stretchItem.Stretch = true;
        }
      }

      [Category("Docking")]
      [Description("Indicates whether the toolbar will allow the user to tear it out of its container in to a floating state.")]
      [DefaultValue(true)]
      public virtual bool Tearable
      {
        get => this._tearable;
        set => this._tearable = value;
      }

      [DefaultValue("Tool Bar")]
      public override string Text
      {
        get => base.Text;
        set => base.Text = value;
      }

      [DefaultValue(typeof (ToolBarTextAlign), "Side")]
      [Description("Controls how the text is positioned relative to the image in each button.")]
      [Category("Item Layout")]
      public virtual ToolBarTextAlign TextAlign
      {
        get => this._textAlign;
        set
        {
          this._textAlign = value;
          this.DoLayout();
        }
      }

      [Browsable(false)]
      internal TopLevelMenuItemBase[] TopLevelMenuItems
      {
        get
        {
          ArrayList arrayList = new ArrayList();
          foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.Items)
          {
            if (toolbarItemBase is TopLevelMenuItemBase)
              arrayList.Add((object) toolbarItemBase);
          }
          TopLevelMenuItemBase[] topLevelMenuItems = new TopLevelMenuItemBase[arrayList.Count];
          arrayList.CopyTo((Array) topLevelMenuItems);
          return topLevelMenuItems;
        }
      }

      [Browsable(false)]
      [Obsolete("Use the Flow property instead.")]
      public bool Vertical => this.Flow == ToolBarLayout.Vertical;

      [Browsable(false)]
      public IToolBarRenderer WorkingRenderer
      {
        get
        {
          if (this.Situation == ToolBarSituation.Contained)
            return ((ToolBarContainer) this.Parent).Manager.Renderer;
          return this.Situation == ToolBarSituation.Floating ? ((FloatingToolbarForm) this.Parent).BarManager.Renderer : this._renderer;
        }
      }

      public ButtonItemBase FindItemByCommandName(string itemCommandPath)
      {
        string[] paths = itemCommandPath.Split('/');
        if (paths.Length == 0)
          return (ButtonItemBase) null;
        ButtonItemBase barItem = this.FindBarItem(paths[0]);
        if (barItem == null)
          return (ButtonItemBase) null;
        if (barItem is MenuItemBase menuItemBase)
          barItem = (ButtonItemBase) menuItemBase.FindItem(paths, 1);
        return barItem;
      }

      private ButtonItemBase FindBarItem(string commandName)
      {
        foreach (ToolbarItemBase barItem in (CollectionBase) this._items)
        {
          if (barItem.CommandName == commandName || barItem.CommandName == string.Empty && barItem.Text == commandName)
            return barItem as ButtonItemBase;
        }
        return (ButtonItemBase) null;
      }

      [Browsable(false)]
      public bool Hidden
      {
        get => !this.Visible;
        set => this.Visible = !value;
      }

      public delegate void ButtonClickEventHandler(object sender, ToolBarItemEventArgs e);

      public class ToolBarItemCollection : ToolbarItemBaseCollection
      {
        internal ToolBarItemCollection(IButtonsSite site)
          : base(site)
        {
        }

        internal override bool IsComponentSuitable(ToolbarItemBase item)
        {
          return ToolBar.ToolBarItemCollection.IsComponentSuitableForToolBar(item);
        }

        public static bool IsComponentSuitableForToolBar(ToolbarItemBase item)
        {
          return !(item is MenuItemBase) || item is TopLevelMenuItemBase;
        }

        internal override void SetOwner(ToolbarItemBase item, object owner)
        {
          item.SetToolBar((ToolBar) owner);
        }
      }
    }
}
