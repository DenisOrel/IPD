
// Type: Intermech.Bars.b
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
    internal class b : n
    {
      private MenuLooper _menuLooper;
      private Screen _screen;
      private static MenuItemBase _menuItem = (MenuItemBase) null;
      private Timer _showChevronedTimer;
      private ArrayList _shadows;
      private Timer _shadowTimer;

      public b(PopupMenu popupmenu, MenuLooper menuLooper, Screen screen)
        : base(popupmenu)
      {
        this._shadows = (ArrayList) null;
        this._shadowTimer = (Timer) null;
        this._menuLooper = menuLooper;
        this._screen = screen;
        popupmenu.Move += new EventHandler(this.Popup_Move);
        popupmenu.Resize += new EventHandler(this.Popup_Resize);
        popupmenu.MouseMove += new MouseEventHandler(this.Popup_MouseMove);
        popupmenu.MouseUp += new MouseEventHandler(this.Popup_MouseUp);
        popupmenu.MouseDown += new MouseEventHandler(this.Popup_MouseDown);
        popupmenu.MouseLeave += new EventHandler(this.Popup_MouseLeave);
        this._showChevronedTimer = new Timer();
        this._showChevronedTimer.Interval = menuLooper.GetMenuShowDelay();
        this._showChevronedTimer.Tick += new EventHandler(this.Timer1_Tick);
      }

      protected internal override Rectangle ConstraintArea() => this._screen.Bounds;

      protected internal override bool ShouldHighlightItem(MenuButtonItem item)
      {
        return this.PopupMenu.MenuItem.HighlightedItem == item;
      }

      private void Popup_MouseLeave(object sender, EventArgs e)
      {
        this.PopupMenu.MenuItem.HighlightedItem = (MenuButtonItem) null;
      }

      private void Popup_MouseDown(object sender, MouseEventArgs mea)
      {
        MenuButtonItem itemAt = this.PopupMenu.GetItemAt(new Point(mea.X, mea.Y));
        if (itemAt == null)
        {
          this._menuLooper.a(this.PopupMenu.MenuItem, false);
        }
        else
        {
          if (itemAt.PopupMenu != null || itemAt == this.PopupMenu.ChevronItem)
            return;
          this._menuLooper.a((MenuItemBase) itemAt, false);
        }
      }

      protected internal override void Show(ref int maximumMenuCount, MenuAnimation desiredAnimation)
      {
        if (desiredAnimation != MenuAnimation.None)
        {
          int num = 0;
          for (MenuItemBase menuItemBase = this.PopupMenu.MenuItem; menuItemBase != null; menuItemBase = menuItemBase.ParentMenu)
            ++num;
          MenuAnimation A_1 = MenuAnimation.None;
          if (num > maximumMenuCount)
          {
            maximumMenuCount = num;
            A_1 = desiredAnimation;
          }
          MenuAnimator.Animate(this.PopupMenu, A_1);
        }
        Win32.SetWindowPos(this.PopupMenu.Handle, 0, 0, 0, 0, 0, 87);
        if (!OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
          return;
        this._shadowTimer = new Timer();
        this._shadowTimer.Interval = 40;
        this._shadowTimer.Enabled = true;
        this._shadowTimer.Tick += new EventHandler(this.OnShadowTimerTick);
      }

      protected internal override bool AllowLowImportanceMenuItems() => this._menuLooper.b();

      private void Timer1_Tick(object sender, EventArgs e)
      {
        this._showChevronedTimer.Enabled = false;
        if (b._menuItem.PopupMenu == null)
          return;
        if (b._menuItem.HighlightedItem == b._menuItem.PopupMenu.ChevronItem)
          b._menuItem.PopupMenu.ExpandChevronedItems();
        else if (b._menuItem.HighlightedItem != null)
          this._menuLooper.a((MenuItemBase) b._menuItem.HighlightedItem, false);
        else
          this._menuLooper.a(b._menuItem, false);
      }

      private void Popup_MouseUp(object sender, MouseEventArgs mea)
      {
        MenuButtonItem itemAt = this.PopupMenu.GetItemAt(new Point(mea.X, mea.Y));
        if (itemAt == this.PopupMenu.ChevronItem)
        {
          this.PopupMenu.ExpandChevronedItems();
        }
        else
        {
          if (itemAt == null || itemAt.HasVisibleSubitems() || !itemAt.Enabled || !itemAt.Visible || itemAt._underChevron)
            return;
          this._menuLooper.SetMenuAction(new MenuAction(MenuAction.CommandType.Execute, (MenuItemBase) itemAt));
        }
      }

      public override void Dispose()
      {
        this.HideShadow();
        if (this._shadowTimer != null)
        {
          this._shadowTimer.Tick -= new EventHandler(this.OnShadowTimerTick);
          this._shadowTimer.Dispose();
          this._shadowTimer = (Timer) null;
        }
        if (this._showChevronedTimer != null)
        {
          this._showChevronedTimer.Tick -= new EventHandler(this.Timer1_Tick);
          this._showChevronedTimer.Dispose();
          this._showChevronedTimer = (Timer) null;
        }
        base.Dispose();
      }

      private void OnShadowTimerTick(object sender, EventArgs e)
      {
        this._shadowTimer.Enabled = false;
        this.UpdateShadows();
      }

      private void Popup_MouseMove(object sender, MouseEventArgs e)
      {
        b._menuItem = this.PopupMenu.MenuItem;
        this._showChevronedTimer.Enabled = false;
        this._showChevronedTimer.Enabled = true;
        MenuButtonItem menuButtonItem = this.PopupMenu.GetItemAt(new Point(e.X, e.Y));
        if (menuButtonItem != null && !menuButtonItem.Enabled)
          menuButtonItem = (MenuButtonItem) null;
        bool flag = this.PopupMenu.MenuItem.HighlightedItem == menuButtonItem;
        this.PopupMenu.MenuItem.HighlightedItem = menuButtonItem;
        if (menuButtonItem == null || flag)
          return;
        menuButtonItem.OnSelect();
      }

      protected internal override void LowImportanceItemsExpanded() => this._menuLooper.ShowFullMenu();

      private void Popup_Resize(object sender, EventArgs e)
      {
        if (this._shadows == null)
          return;
        this.UpdateShadows();
      }

      private void HideShadow()
      {
        if (this._shadows == null)
          return;
        foreach (Form shadow in this._shadows)
          shadow.Close();
        this._shadows.Clear();
        this._shadows = (ArrayList) null;
      }

      private void Popup_Move(object sender, EventArgs e)
      {
        if (this._shadows == null)
          return;
        this.UpdateShadows();
      }

      private void MakeShadows()
      {
        Rectangle bounds = this.PopupMenu.Bounds;
        ShadowForm shadowForm1 = new ShadowForm(this.PopupMenu.Host.Renderer.ShadowColor, false, true);
        shadowForm1.Locate(new Rectangle(bounds.Right, bounds.Top + 4, 4, bounds.Height));
        this._shadows.Add((object) shadowForm1);
        ShadowForm shadowForm2 = new ShadowForm(this.PopupMenu.Host.Renderer.ShadowColor, true, true);
        shadowForm2.Locate(new Rectangle(bounds.Left + 4, bounds.Bottom, bounds.Width - 4, 4));
        this._shadows.Add((object) shadowForm2);
      }

      private void MakeParentShadows()
      {
        if (this.PopupMenu.ParentBounds.Width == 0)
          return;
        ShadowForm shadowForm1 = new ShadowForm(this.PopupMenu.Host.Renderer.ShadowColor, false, true);
        Rectangle parentBounds1 = this.PopupMenu.ParentBounds;
        shadowForm1.Locate(new Rectangle(parentBounds1.Right + 1, parentBounds1.Top, 4, parentBounds1.Height + 4));
        this._shadows.Add((object) shadowForm1);
        ShadowForm shadowForm2 = new ShadowForm(this.PopupMenu.Host.Renderer.ShadowColor, true, true);
        Rectangle parentBounds2 = this.PopupMenu.ParentBounds;
        shadowForm2.Locate(new Rectangle(parentBounds2.Left + 4, parentBounds2.Bottom + 1, parentBounds2.Width - 3, 4));
        this._shadows.Add((object) shadowForm2);
      }

      private void UpdateShadows()
      {
        this.HideShadow();
        if (!Win32.IsWin2K())
          return;
        this._shadows = new ArrayList();
        if (this.PopupMenu.MenuItem is TopLevelMenuItemBase)
        {
          Rectangle parentBounds = this.PopupMenu.ParentBounds;
          switch (this.PopupMenu.MenuItem.MenuDirection)
          {
            case MenuOffset.Top:
              this.MakeShadows();
              this.MakeParentShadows();
              break;
            case MenuOffset.Bottom:
              this.MakeShadows();
              if (parentBounds.Width == 0)
                break;
              ShadowForm shadowForm1 = new ShadowForm(this.PopupMenu.Host.Renderer.ShadowColor, false, false);
              shadowForm1.Locate(new Rectangle(parentBounds.Right + 1, parentBounds.Top + 4, 4, parentBounds.Height - 4));
              this._shadows.Add((object) shadowForm1);
              break;
            case MenuOffset.Right:
              this.MakeShadows();
              if (parentBounds.Width == 0)
                break;
              ShadowForm shadowForm2 = new ShadowForm(this.PopupMenu.Host.Renderer.ShadowColor, false, true);
              shadowForm2.Locate(new Rectangle(parentBounds.Left + 4, parentBounds.Bottom + 1, parentBounds.Width - 3, 4));
              this._shadows.Add((object) shadowForm2);
              break;
          }
        }
        else
          this.MakeShadows();
      }
    }
}
