
// Type: Intermech.Bars.PopupMenu
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class PopupMenu : Form
    {
      private const int _a = 18;
      private const int _b = 10;
      private MenuItemBase _menuItem;
      private IPopupMenuHost _popupMenuHost;
      private bool _subMenu;
      private bool _isContextMenu;
      private Point _menuPosition;
      internal bool _animating;
      private int _breakOffset;
      private int _breakSize;
      private int _marginWidth;
      private bool _chevronedItems;
      private MenuButtonItem _chevronMenuItem;
      private ToolTips _toolTips;
      private n o;
      private bool _designMode;
      private bool _scrollable;
      private int r;
      private int _scrollOffset;
      private Rectangle _upScrollMenuBounds;
      private Rectangle _downScrollMenuBounds;
      private Timer _scrollTimer;

      protected internal PopupMenu(MenuItemBase menuItem, IPopupMenuHost host)
      {
        this._animating = false;
        this._chevronedItems = false;
        this._chevronMenuItem = (MenuButtonItem) null;
        this._toolTips = (ToolTips) null;
        this._designMode = false;
        this._scrollable = false;
        this._scrollTimer = (Timer) null;
        this._menuItem = menuItem;
        this._popupMenuHost = host;
        this.FormBorderStyle = FormBorderStyle.None;
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.Manual;
        this.SetStyle(ControlStyles.Selectable, false);
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this._scrollTimer = new Timer();
        this._scrollTimer.Interval = 20;
        this._scrollTimer.Tick += new EventHandler(this.Timer_Tick);
        this._chevronMenuItem = new MenuButtonItem();
        this._chevronMenuItem.SetParentMenu(menuItem);
      }

      private void a()
      {
        Size desiredSize = this.DesiredSize;
        this._scrollable = this.Height < desiredSize.Height;
        if (this._scrollable)
        {
          this.r = desiredSize.Height - this.Height + 20;
          this._upScrollMenuBounds = this.ClientRectangle;
          --this._upScrollMenuBounds.Width;
          this._upScrollMenuBounds.Inflate(-1, -1);
          this._upScrollMenuBounds.Height = 10;
          this._downScrollMenuBounds = this._upScrollMenuBounds;
          this._downScrollMenuBounds.Y = this.ClientRectangle.Height - 10 - 1;
        }
        using (Graphics graphics = this.CreateGraphics())
        {
          this.LayoutChildItems(graphics, this.ItemDisplayArea);
          if (this.ChevronedItems)
          {
            Rectangle itemDisplayArea = this.ItemDisplayArea;
            itemDisplayArea.Y = itemDisplayArea.Bottom - 18 + 1;
            itemDisplayArea.Height = 18;
            this._chevronMenuItem.ApplyLayout(itemDisplayArea, graphics, this.Host.Flow == ToolBarLayout.Vertical, this.Host.RightToLeft);
          }
        }
        this.Invalidate();
      }

      private string GetToolTipText(Point pos)
      {
        if (!this._designMode)
        {
          MenuButtonItem itemAt = this.GetItemAt(pos);
          if (itemAt != null)
            return itemAt.ToolTipText;
        }
        return string.Empty;
      }

      internal void a(int A_0)
      {
        int index = this._menuItem.Items.IndexOf((ToolbarItemBase) this._menuItem.HighlightedItem);
        do
        {
          index += A_0;
          if (index == this._menuItem.Items.Count && this.ChevronedItems)
          {
            index = 0;
            this.ExpandChevronedItems();
          }
          if (index == this._menuItem.Items.Count)
            index = 0;
          if (index < 0)
            index = this._menuItem.Items.Count - 1;
        }
        while (!this._menuItem.Items[index].Visible || this._menuItem.Items[index]._underChevron);
        this._menuItem.HighlightedItem = this._menuItem.Items[index];
        this._menuItem.HighlightedItem.OnSelect();
        if (!this._scrollable)
          return;
        Rectangle buttonBounds;
        if (this._menuItem.HighlightedItem.ButtonBounds.Y <= this._upScrollMenuBounds.Bottom)
        {
          int scrollOffset = this._scrollOffset;
          int bottom = this._upScrollMenuBounds.Bottom;
          buttonBounds = this._menuItem.HighlightedItem.ButtonBounds;
          int y = buttonBounds.Y;
          int num = bottom - y + 1;
          this._scrollOffset = scrollOffset - num;
          this.a();
        }
        buttonBounds = this._menuItem.HighlightedItem.ButtonBounds;
        if (buttonBounds.Bottom <= this._downScrollMenuBounds.Y)
          return;
        int scrollOffset1 = this._scrollOffset;
        buttonBounds = this._menuItem.HighlightedItem.ButtonBounds;
        int num1 = buttonBounds.Bottom - this._downScrollMenuBounds.Y;
        this._scrollOffset = scrollOffset1 + num1;
        this.a();
      }

      internal void a(Control A_0)
      {
        this.o = (n) new w(this, A_0);
        this._designMode = true;
        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        Win32.SetParent(this.Handle, A_0.Handle);
        this.UpdateChevronedItems();
      }

      internal void ShowMenu(MenuAnimation animation)
      {
        int maximumMenuCount = 0;
        this.ShowMenu(ref maximumMenuCount, animation);
      }

      internal void CalcMenuSize(bool subMenu, Point position)
      {
        this._menuPosition = position;
        this.CalcMenuSize(subMenu);
      }

      internal void ShowMenu(ref int maximumMenuCount, MenuAnimation desiredAnimation)
      {
        this.o.Show(ref maximumMenuCount, desiredAnimation);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        this.Visible = true;
      }

      private void Timer_Tick(object sender, EventArgs e)
      {
        bool flag1 = this._upScrollMenuBounds.Contains(this.PointToClient(Cursor.Position));
        bool flag2 = this._downScrollMenuBounds.Contains(this.PointToClient(Cursor.Position));
        if (flag1 | flag2)
        {
          int scrollOffset = this._scrollOffset;
          if (flag1)
            this._scrollOffset -= 3;
          else
            this._scrollOffset += 3;
          if (this._scrollOffset < 0)
            this._scrollOffset = 0;
          if (this._scrollOffset > this.r)
            this._scrollOffset = this.r;
          if (this._scrollOffset == scrollOffset)
            return;
          this.a();
        }
        else
          this._scrollTimer.Enabled = false;
      }

      internal void a(MenuLooper worker, Screen screen)
      {
        this.o = (n) new b(this, worker, screen);
        this.UpdateChevronedItems();
      }

      private void calcSubMenuSize(
        Rectangle parentBounds,
        Rectangle maxBounds,
        out Point startPosition,
        out Size menuSize)
      {
        startPosition = !this._popupMenuHost.RightToLeft ? new Point(parentBounds.Right, parentBounds.Y) : new Point(parentBounds.X, parentBounds.Y);
        menuSize = this.DesiredSize;
        if (maxBounds.Bottom - startPosition.Y < menuSize.Height)
        {
          startPosition.Y = maxBounds.Bottom - menuSize.Height;
          if (startPosition.Y < maxBounds.Y)
          {
            startPosition.Y = maxBounds.Y;
            menuSize.Height = maxBounds.Height;
          }
        }
        if (menuSize.Width > maxBounds.Width)
          menuSize.Width = maxBounds.Width;
        if (this._popupMenuHost.RightToLeft)
        {
          startPosition.X -= menuSize.Width;
          this._menuItem.SetMenuDirection(MenuOffset.Left);
          if (startPosition.X >= maxBounds.Left)
            return;
          startPosition.X = maxBounds.Left;
          this._menuItem.SetMenuDirection(MenuOffset.Right);
        }
        else
        {
          this._menuItem.SetMenuDirection(MenuOffset.Right);
          if (startPosition.X + menuSize.Width <= maxBounds.Right)
            return;
          startPosition.X = parentBounds.X - menuSize.Width;
          this._menuItem.SetMenuDirection(MenuOffset.Left);
        }
      }

      private void DrawScrollItem(Graphics g, int A_1, int A_2, int A_3, Color color)
      {
        using (Pen pen = new Pen(color))
        {
          g.DrawLine(pen, A_1, A_2, A_1 + 4, A_2);
          g.DrawLine(pen, A_1 + 1, A_2 + A_3, A_1 + 3, A_2 + A_3);
          g.DrawLine(pen, A_1 + 2, A_2 + A_3 * 2, A_1 + 2, A_2);
        }
      }

      internal void CalcMenuSize(bool subMenu)
      {
        this._subMenu = subMenu;
        Point location;
        Size menuSize;
        if (subMenu)
          this.calcSubMenuSize(this.ParentBounds, this.o.ConstraintArea(), out location, out menuSize);
        else if (this._popupMenuHost.Flow == ToolBarLayout.Vertical)
          this.c(this.ParentBounds, this.o.ConstraintArea(), out location, out menuSize);
        else
          this.CalcMenuSize(this.ParentBounds, this.o.ConstraintArea(), out location, out menuSize);
        this.Bounds = new Rectangle(location, menuSize);
      }

      private void CalcContextMenuSize(
        Rectangle parentBounds,
        Rectangle maxBounds,
        out Point startPosition,
        out Size menuSize)
      {
        startPosition = !this._popupMenuHost.RightToLeft ? new Point(parentBounds.X, parentBounds.Bottom) : new Point(parentBounds.Right + 1, parentBounds.Bottom);
        menuSize = this.DesiredSize;
        int num1 = maxBounds.Bottom - startPosition.Y;
        int num2 = startPosition.Y - parentBounds.Height - maxBounds.Y;
        this._breakSize = parentBounds.Width;
        if (num1 >= menuSize.Height)
          this._menuItem.SetMenuDirection(MenuOffset.Bottom);
        else if (num2 > num1)
        {
          startPosition.Y -= parentBounds.Height;
          if (menuSize.Height > num2)
            startPosition.Y = 0;
          startPosition.Y -= menuSize.Height;
          if (startPosition.Y < 0)
          {
            startPosition.Y = maxBounds.Bottom - menuSize.Height;
            if (startPosition.Y < maxBounds.Y)
            {
              startPosition.Y = maxBounds.Y;
              menuSize.Height = maxBounds.Height;
            }
          }
          this._menuItem.SetMenuDirection(MenuOffset.Top);
        }
        else
        {
          if (menuSize.Height > num1)
          {
            menuSize.Height = Math.Min(menuSize.Height, maxBounds.Height - 8);
            int num3 = menuSize.Height - num1;
            startPosition.Y -= num3 + 8;
          }
          this._menuItem.SetMenuDirection(MenuOffset.Bottom);
        }
        if (menuSize.Width > maxBounds.Width)
          menuSize.Width = maxBounds.Width;
        if (this._popupMenuHost.RightToLeft)
        {
          startPosition.X -= menuSize.Width;
          if (startPosition.X >= maxBounds.Left)
            return;
          this._breakOffset = maxBounds.Left - startPosition.X;
          startPosition.X = maxBounds.Left;
        }
        else
        {
          if (startPosition.X + menuSize.Width <= maxBounds.Right)
            return;
          this._breakOffset = startPosition.X - (maxBounds.Right - menuSize.Width);
          startPosition.X = maxBounds.Right - menuSize.Width;
        }
      }

      private void CalcMenuSize(
        Rectangle parentBounds,
        Rectangle maxBounds,
        out Point startPosition,
        out Size menuSize)
      {
        if (this._isContextMenu)
        {
          this.CalcContextMenuSize(parentBounds, maxBounds, out startPosition, out menuSize);
        }
        else
        {
          startPosition = this._popupMenuHost.RightToLeft || this._popupMenuHost.RightAlignMenus ? new Point(parentBounds.Right + 1, parentBounds.Bottom) : new Point(parentBounds.X, parentBounds.Bottom);
          menuSize = this.DesiredSize;
          int num1 = maxBounds.Bottom - startPosition.Y;
          int num2 = startPosition.Y - parentBounds.Height - maxBounds.Y;
          this._breakSize = parentBounds.Width;
          if (num1 >= menuSize.Height)
            this._menuItem.SetMenuDirection(MenuOffset.Bottom);
          else if (num2 > num1)
          {
            startPosition.Y -= parentBounds.Height;
            if (menuSize.Height > num2)
              menuSize.Height = num2;
            startPosition.Y -= menuSize.Height;
            if (this.IsContextMenu)
              ++startPosition.Y;
            this._menuItem.SetMenuDirection(MenuOffset.Top);
          }
          else
          {
            if (menuSize.Height > num1)
              menuSize.Height = num1;
            this._menuItem.SetMenuDirection(MenuOffset.Bottom);
          }
          if (menuSize.Width > maxBounds.Width)
            menuSize.Width = maxBounds.Width;
          if (this._popupMenuHost.RightToLeft || this._popupMenuHost.RightAlignMenus)
          {
            startPosition.X -= menuSize.Width;
            if (startPosition.X >= maxBounds.Left)
              return;
            this._breakOffset = maxBounds.Left - startPosition.X;
            startPosition.X = maxBounds.Left;
          }
          else
          {
            if (startPosition.X + menuSize.Width <= maxBounds.Right)
              return;
            this._breakOffset = startPosition.X - (maxBounds.Right - menuSize.Width);
            startPosition.X = maxBounds.Right - menuSize.Width;
          }
        }
      }

      private void c(
        Rectangle parentBounds,
        Rectangle maxBounds,
        out Point menuPos,
        out Size menuSize)
      {
        menuPos = !this._popupMenuHost.RightToLeft ? new Point(parentBounds.Right, parentBounds.Y) : new Point(parentBounds.X, parentBounds.Y);
        Size desiredSize = this.DesiredSize;
        menuSize = desiredSize;
        int num1 = parentBounds.Left - maxBounds.Left;
        int num2 = maxBounds.Right - parentBounds.Right;
        this._breakSize = parentBounds.Height;
        if (this._popupMenuHost.RightToLeft && (num1 >= desiredSize.Width || num1 > num2) || !this._popupMenuHost.RightToLeft && num2 < desiredSize.Width && num1 > num2)
        {
          menuPos = new Point(parentBounds.X - desiredSize.Width, parentBounds.Y);
          if (menuPos.X < maxBounds.X)
          {
            menuSize.Width -= maxBounds.X - menuPos.X;
            menuPos.X = maxBounds.X;
          }
          this._menuItem.SetMenuDirection(MenuOffset.Left);
        }
        else
        {
          menuPos = new Point(parentBounds.Right, parentBounds.Y);
          if (menuPos.X + menuSize.Width > maxBounds.Right)
            menuSize.Width = maxBounds.Right - menuPos.X;
          this._menuItem.SetMenuDirection(MenuOffset.Right);
        }
        if (menuSize.Height > maxBounds.Height)
          menuSize.Height = maxBounds.Height;
        if (menuPos.Y + menuSize.Height <= maxBounds.Bottom)
          return;
        this._breakOffset = menuPos.Y - (maxBounds.Bottom - menuSize.Height);
        menuPos.Y = maxBounds.Bottom - menuSize.Height;
      }

      private void UpdateChevronedItems()
      {
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) this._menuItem.Items)
        {
          menuButtonItem._underChevron = menuButtonItem.Importance == ToolBarItemImportance.Low && this.o.AllowLowImportanceMenuItems();
          this._chevronedItems = this._chevronedItems || menuButtonItem._underChevron;
        }
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          if (this.o != null)
          {
            this.o.Dispose();
            this.o = (n) null;
          }
          if (this._toolTips != null)
          {
            this._toolTips.GetToolTipText -= new ToolTips.GetToolTipTextEventHandler(this.GetToolTipText);
            this._toolTips.Dispose();
            this._toolTips = (ToolTips) null;
          }
          if (this._scrollTimer != null)
          {
            this._scrollTimer.Tick -= new EventHandler(this.Timer_Tick);
            this._scrollTimer.Dispose();
            this._scrollTimer = (Timer) null;
          }
          if (this._chevronMenuItem != null)
          {
            this._chevronMenuItem.Dispose();
            this._chevronMenuItem = (MenuButtonItem) null;
          }
        }
        base.Dispose(disposing);
      }

      internal void ExpandChevronedItems()
      {
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) this._menuItem.Items)
        {
          if (menuButtonItem.Importance == ToolBarItemImportance.Low)
            menuButtonItem._underChevron = false;
        }
        this._chevronedItems = false;
        this.o.LowImportanceItemsExpanded();
        this.LayoutNeeded();
      }

      protected void EnableToolTips()
      {
        this._toolTips = new ToolTips((Control) this);
        this._toolTips.DropShadow = false;
        this._toolTips.GetToolTipText += new ToolTips.GetToolTipTextEventHandler(this.GetToolTipText);
      }

      internal void LayoutNeeded()
      {
        this.CalcMenuSize(this._subMenu);
        this.a();
        this.Invalidate();
      }

      public virtual MenuButtonItem GetItemAt(Point position)
      {
        foreach (MenuButtonItem itemAt in (CollectionBase) this._menuItem.Items)
        {
          if (itemAt.Visible && !itemAt._underChevron && itemAt.ButtonBounds.Contains(position))
            return itemAt;
        }
        return this.ChevronedItems && this._chevronMenuItem.ButtonBounds.Contains(position) ? this._chevronMenuItem : (MenuButtonItem) null;
      }

      protected virtual void LayoutChildItems(Graphics graphics, Rectangle itemDisplayArea)
      {
        bool flag = true;
        int num = itemDisplayArea.Top - this._scrollOffset;
        foreach (MenuButtonItem menuItem in (CollectionBase) this._menuItem.Items)
        {
          if (menuItem.Visible && !menuItem._underChevron)
          {
            menuItem._drawSeparator = menuItem.BeginGroup && !flag;
            if (menuItem._drawSeparator)
              num += 3;
            flag = false;
            Size size = MenuMeasure.MenuItemSize(graphics, menuItem, this._popupMenuHost.MenuImageList, this._popupMenuHost);
            Rectangle buttonBounds = itemDisplayArea with
            {
              Y = num,
              Height = size.Height + 1
            };
            menuItem.ApplyLayout(buttonBounds, graphics, this.Host.Flow == ToolBarLayout.Vertical, this.Host.RightToLeft);
            num += buttonBounds.Height;
          }
        }
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        if (this._scrollable && (this._upScrollMenuBounds.Contains(e.X, e.Y) || this._downScrollMenuBounds.Contains(e.X, e.Y)))
        {
          this.Cursor = Cursors.Default;
          this._scrollTimer.Enabled = true;
        }
        else
          base.OnMouseMove(e);
      }

      protected sealed override void OnPaintBackground(PaintEventArgs pevent)
      {
        Rectangle clientRectangle = this.ClientRectangle;
        --clientRectangle.Width;
        --clientRectangle.Height;
        MenuOffset menuDirection = MenuOffset.Bottom;
        if (this._menuItem is TopLevelMenuItemBase)
          menuDirection = this._menuItem.MenuDirection;
        this._popupMenuHost.Renderer.DrawMenuBackground(pevent.Graphics, clientRectangle, this._marginWidth, this._breakOffset, this._breakSize, menuDirection, this._popupMenuHost.RightToLeft);
        Region region = (Region) null;
        if (this._scrollable)
        {
          region = pevent.Graphics.Clip;
          pevent.Graphics.SetClip(this.ItemDisplayArea);
        }
        this.PaintChildItems(pevent);
        if (this._scrollable)
          pevent.Graphics.Clip = region;
        if (this.ChevronedItems)
        {
          DrawItemState state = DrawItemState.Default;
          if (this._menuItem.HighlightedItem == this._chevronMenuItem)
            state |= DrawItemState.HotLight;
          this._popupMenuHost.Renderer.DrawMenuItem(pevent.Graphics, this._chevronMenuItem, this._popupMenuHost, this._marginWidth, state, false);
          Rectangle buttonBounds = this._chevronMenuItem.ButtonBounds;
          --buttonBounds.Y;
          this._popupMenuHost.Renderer.DrawMenuActionsButton(pevent.Graphics, buttonBounds, this._marginWidth, state, this._designMode);
        }
        if (!this._scrollable)
          return;
        if (this._scrollOffset > 0)
          this.DrawScrollItem(pevent.Graphics, this.ClientRectangle.Width / 2 - 2, this._upScrollMenuBounds.Top + 7, -1, SystemColors.ControlText);
        else
          this.DrawScrollItem(pevent.Graphics, this.ClientRectangle.Width / 2 - 2, this._upScrollMenuBounds.Top + 7, -1, SystemColors.ControlDark);
        if (this._scrollOffset < this.r)
          this.DrawScrollItem(pevent.Graphics, this.ClientRectangle.Width / 2 - 2, this._downScrollMenuBounds.Top + 3, 1, SystemColors.ControlText);
        else
          this.DrawScrollItem(pevent.Graphics, this.ClientRectangle.Width / 2 - 2, this._downScrollMenuBounds.Top + 3, 1, SystemColors.ControlDark);
      }

      protected override void OnResize(EventArgs e)
      {
        if (this._animating)
          return;
        this.a();
        base.OnResize(e);
      }

      protected virtual void PaintChildItems(PaintEventArgs e)
      {
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) this._menuItem.Items)
        {
          if (menuButtonItem.Visible && !menuButtonItem._underChevron)
          {
            if (menuButtonItem._drawSeparator)
            {
              Rectangle buttonBounds = menuButtonItem.ButtonBounds;
              buttonBounds.Y -= 3;
              this._popupMenuHost.Renderer.DrawMenuSeparator(e.Graphics, buttonBounds, this._marginWidth, this._popupMenuHost.RightToLeft);
            }
            DrawItemState state = DrawItemState.Default;
            if (this.ShouldHighlightItem(menuButtonItem))
              state |= DrawItemState.HotLight;
            if (!menuButtonItem.Enabled)
              state |= DrawItemState.Disabled;
            this._popupMenuHost.Renderer.DrawMenuItem(e.Graphics, menuButtonItem, this._popupMenuHost, this._marginWidth, state, this._menuItem is ToolBarButtonsCustomizeMenu.CustomizeMenuButtonItem);
          }
        }
      }

      protected bool ShouldHighlightItem(MenuButtonItem item) => this.o.ShouldHighlightItem(item);

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == 33)
          m.Result = new IntPtr(3);
        else
          base.WndProc(ref m);
      }

      private bool ChevronedItems => this._chevronedItems || this._designMode;

      internal MenuButtonItem ChevronItem => this._chevronMenuItem;

      protected override CreateParams CreateParams
      {
        get
        {
          CreateParams createParams = base.CreateParams;
          if (this._designMode)
          {
            createParams.Style |= 1073741824 /*0x40000000*/;
            return createParams;
          }
          createParams.Style |= int.MinValue;
          createParams.ExStyle |= 8;
          return createParams;
        }
      }

      protected virtual Size DesiredClientSize
      {
        get
        {
          Size empty = Size.Empty;
          Size size = Size.Empty;
          using (Graphics graphics = this.CreateGraphics())
          {
            bool flag = true;
            foreach (MenuButtonItem menuItem in (CollectionBase) this._menuItem.Items)
            {
              if (menuItem.Visible && !menuItem._underChevron)
              {
                size = MenuMeasure.MenuItemSize(graphics, menuItem, this._popupMenuHost.MenuImageList, this._popupMenuHost);
                if (size.Width > empty.Width)
                  empty.Width = size.Width;
                if (menuItem.BeginGroup && !flag)
                  empty.Height += 3;
                flag = false;
                empty.Height += size.Height + 1;
              }
            }
            if (size != Size.Empty)
              --empty.Height;
          }
          this._marginWidth = MenuMeasure.MaxImageWidth((ICollection) this._menuItem.Items, this._popupMenuHost.MenuImageList);
          if (this._menuItem is ToolBarButtonsCustomizeMenu.CustomizeMenuButtonItem)
            this._marginWidth += 22;
          empty.Width += this._marginWidth;
          return empty;
        }
      }

      private Size DesiredSize
      {
        get
        {
          Size desiredClientSize = this.DesiredClientSize;
          desiredClientSize.Width += 2;
          desiredClientSize.Height += 4;
          if (this.ChevronedItems)
            desiredClientSize.Height += 18;
          if (this._designMode && desiredClientSize.Width < 100)
            desiredClientSize.Width = 100;
          return desiredClientSize;
        }
      }

      protected internal IPopupMenuHost Host => this._popupMenuHost;

      internal bool IsContextMenu
      {
        get => this._isContextMenu;
        set => this._isContextMenu = value;
      }

      protected Rectangle ItemDisplayArea
      {
        get
        {
          Rectangle clientRectangle = this.ClientRectangle;
          clientRectangle.Inflate(-1, -2);
          if (this._scrollable)
            clientRectangle.Inflate(0, -10);
          return clientRectangle;
        }
      }

      protected internal MenuItemBase MenuItem => this._menuItem;

      internal Rectangle ParentBounds
      {
        get
        {
          if (this._isContextMenu)
            return this.o.ModifyParentBounds(new Rectangle(this._menuPosition, Size.Empty));
          if (this._menuItem.ToolBar != null)
            return this.o.ModifyParentBounds(new Rectangle(this._menuItem.ToolBar.PointToScreen(this._menuItem.ButtonBounds.Location), this._menuItem.ButtonBounds.Size));
          n o = this.o;
          PopupMenu popupMenu = this._menuItem.ParentMenu.PopupMenu;
          Rectangle buttonBounds = this._menuItem.ButtonBounds;
          Point location = buttonBounds.Location;
          Point screen = popupMenu.PointToScreen(location);
          buttonBounds = this._menuItem.ButtonBounds;
          Size size = buttonBounds.Size;
          Rectangle parentBounds = new Rectangle(screen, size);
          return o.ModifyParentBounds(parentBounds);
        }
      }

      protected int ScrollOffset => this._scrollOffset;
    }
}
