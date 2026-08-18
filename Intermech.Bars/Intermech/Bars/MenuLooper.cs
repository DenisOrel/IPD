
// Type: Intermech.Bars.MenuLooper
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class MenuLooper : IDisposable
    {
      private IPopupMenuHost _popupMenuHost;
      private Control _control;
      private Screen _screen;
      private Form _activeForm;
      private ArrayList _e;
      private MenuAction _menuAction;
      private bool _active;
      private int maximumMenuCount;
      private TopLevelMenuItemBase[] _availableMenus;
      private bool j;
      private bool k;
      private static MenuLooper _worker;
      private bool _collapseMenu;

      public MenuLooper(
        IPopupMenuHost popupHost,
        Control control,
        TopLevelMenuItemBase[] availableMenus)
      {
        this._activeForm = (Form) null;
        this._e = (ArrayList) null;
        this._menuAction = (MenuAction) null;
        this.maximumMenuCount = 0;
        this.j = false;
        this._collapseMenu = !popupHost.FullMenus;
        this._control = control;
        this._popupMenuHost = popupHost;
        this._availableMenus = availableMenus;
        if (this._availableMenus.Length == 0 && popupHost.ToolBar != null)
          this._availableMenus = new TopLevelMenuItemBase[1]
          {
            popupHost.ToolBar.ActionsButton
          };
        if (popupHost == null)
          throw new ArgumentNullException();
      }

      public void Dispose()
      {
        this._popupMenuHost = (IPopupMenuHost) null;
        this._control = (Control) null;
        this._screen = (Screen) null;
      }

      public static void a()
      {
        if (MenuLooper._worker == null)
          return;
        MenuLooper._worker.f();
      }

      public void a(bool A_0) => this.k = A_0;

      private MenuAction GetActionForMnemonic(char mnemonicChar)
      {
        foreach (ToolbarItemBase A_1 in (CollectionBase) this._popupMenuHost.ToolBar.Items)
        {
          if (A_1.Enabled && A_1.Visible && A_1 is TopLevelMenuItemBase && Control.IsMnemonic(mnemonicChar, A_1.Text))
            return new MenuAction(MenuAction.CommandType.Show, (MenuItemBase) A_1, true);
        }
        return (MenuAction) null;
      }

      private bool a(Control A_0)
      {
        int num = A_0 != this._popupMenuHost.ToolBar ? 1 : (this.j ? 1 : 0);
        bool flag = !(A_0 is FloatingToolbarForm);
        return num != 0 && flag;
      }

      internal void SetMenuAction(MenuAction action) => this._menuAction = action;

      private void HideItemPopupMenu(MenuItemBase item)
      {
        if (item.PopupMenu == null)
          return;
        item.HidePopupMenu();
      }

      private MenuAction a(TopLevelMenuItemBase A_0)
      {
        TopLevelMenuItemBase A_1 = this.a(A_0, 1);
        return A_1 == A_0 ? (MenuAction) null : new MenuAction(MenuAction.CommandType.Show, (MenuItemBase) A_1, true);
      }

      private MenuItemBase a(ICollection A_0, Point A_1)
      {
        foreach (MenuItemBase menuItemBase in (IEnumerable) A_0)
        {
          if (menuItemBase.PopupMenu != null && menuItemBase.PopupMenu.Bounds.Contains(A_1))
            return menuItemBase;
        }
        return (MenuItemBase) null;
      }

      private void a(IList list, TopLevelMenuItemBase A_1)
      {
        if (this._popupMenuHost.ToolBar != null)
          this._popupMenuHost.ToolBar.HighlightedItem = (ToolbarItemBase) null;
        foreach (MenuItemBase menuItemBase in (IEnumerable) list)
          this.HideItemPopupMenu(menuItemBase);
        list.Clear();
      }

      private Point ClientToScreen(IntPtr hwnd, IntPtr lParam)
      {
        Win32.POINT A_1 = new Win32.POINT();
        A_1.X = Win32.LoWorld(lParam.ToInt32());
        A_1.Y = Win32.HiWord(lParam.ToInt32());
        Win32.ClientToScreen(hwnd, out A_1);
        return new Point(A_1.X, A_1.Y);
      }

      private void ActiveForm_Deactivate(object seneder, EventArgs e)
      {
        if (!this._active)
          return;
        this.f();
      }

      public static void ExitMenuLoop()
      {
        if (MenuLooper._worker == null)
          return;
        MenuLooper._worker.CancelMenu();
      }

      public void CancelMenu()
      {
        if (!this._active)
          return;
        this._active = false;
        MenuLooper.PostMessage(this._control.Handle, 31 /*0x1F*/, IntPtr.Zero, IntPtr.Zero);
      }

      internal void a(MenuItemBase A_0, bool A_1)
      {
        while (this._e.Count != 0 && this._e[0] != A_0 && this._e[0] != A_0.ParentMenu)
        {
          this.HideItemPopupMenu((MenuItemBase) this._e[0]);
          this._e.RemoveAt(0);
        }
        if (A_0.ParentMenu == null || A_0.ParentMenu.PopupMenu == null || A_0.PopupMenu != null || !A_0.Enabled || !A_0.Visible || A_0._underChevron || !A_0.HasVisibleSubitems())
          return;
        this.a(A_0, A_0.ParentMenu, A_1);
        this._e.Insert(0, (object) A_0);
      }

      private MenuAction GetItemAt(MenuItemBase parentItem, Point pos)
      {
        if (!this.j && this._popupMenuHost.ToolBar.Items.Contains((ToolbarItemBase) parentItem))
        {
          Point client = this._popupMenuHost.ToolBar.PointToClient(pos);
          if (this._popupMenuHost.ToolBar.ClientRectangle.Contains(client))
          {
            ToolbarItemBase itemAt = this._popupMenuHost.ToolBar.GetItemAt(client);
            if (itemAt != parentItem && itemAt is TopLevelMenuItemBase && itemAt.Enabled)
              return new MenuAction(MenuAction.CommandType.Show, (MenuItemBase) itemAt);
          }
        }
        return (MenuAction) null;
      }

      private TopLevelMenuItemBase a(TopLevelMenuItemBase A_0, int A_1)
      {
        int index = Array.IndexOf<TopLevelMenuItemBase>(this._availableMenus, A_0);
        do
        {
          index += A_1;
          if (index < 0)
            index = this._availableMenus.Length - 1;
          if (index == this._availableMenus.Length)
            index = 0;
          if (this._availableMenus[index] == A_0)
            return A_0;
        }
        while (!this._availableMenus[index].Visible || !this._availableMenus[index].Enabled);
        return this._availableMenus[index];
      }

      private MenuAction a(MenuItemBase A_0, char A_1, ArrayList A_2)
      {
        MenuButtonItem menuButtonItem1 = (MenuButtonItem) null;
        int num = 0;
        int index1 = 0;
        int index2 = -1;
        foreach (MenuButtonItem menuButtonItem2 in (CollectionBase) A_0.Items)
        {
          if (menuButtonItem2.Visible && menuButtonItem2.Enabled && !menuButtonItem2._underChevron && Control.IsMnemonic(A_1, menuButtonItem2.Text))
          {
            ++num;
            if (num == 1)
              index1 = A_0.Items.IndexOf((ToolbarItemBase) menuButtonItem2);
            if (A_0.HighlightedItem != null && A_0.Items.IndexOf((ToolbarItemBase) menuButtonItem2) > A_0.Items.IndexOf((ToolbarItemBase) A_0.HighlightedItem) && index2 == -1)
              index2 = A_0.Items.IndexOf((ToolbarItemBase) menuButtonItem2);
            menuButtonItem1 = menuButtonItem2;
          }
        }
        switch (num)
        {
          case 0:
            return (MenuAction) null;
          case 1:
            if (!menuButtonItem1.HasVisibleSubitems())
              return new MenuAction(MenuAction.CommandType.Execute, (MenuItemBase) menuButtonItem1);
            this.a((MenuItemBase) menuButtonItem1, true);
            return (MenuAction) null;
          default:
            A_0.HighlightedItem = index2 != -1 ? A_0.Items[index2] : A_0.Items[index1];
            A_0.HighlightedItem.OnSelect();
            goto case 0;
        }
      }

      private void a(MenuItemBase A_0, MenuItemBase A_1, bool skip)
      {
        A_0.OnBeforePopup(new MenuPopupEventArgs(MenuItemBase.MenuPopupMode.SubMenu));
        PopupMenu popupMenu = A_0.CreatePopupMenu(this._popupMenuHost);
        popupMenu.a(this, this._screen);
        if (this._activeForm != null)
          this._activeForm.AddOwnedForm((Form) popupMenu);
        A_0.PopupMenu = popupMenu;
        popupMenu.CalcMenuSize(true);
        A_0.HighlightedItem = !skip ? (MenuButtonItem) null : A_0.GetFirstVisibleItem();
        popupMenu.ShowMenu(ref this.maximumMenuCount, MenuAnimator.SystemAnimation(this._popupMenuHost.MenuAnimation, skip));
        if (A_0.HighlightedItem == null)
          return;
        A_0.HighlightedItem.OnSelect();
      }

      private bool a(TopLevelMenuItemBase topLevelMenu, bool skip, bool isTopLevel, Point position)
      {
        MenuPopupEventArgs e;
        if (isTopLevel)
        {
          e = new MenuPopupEventArgs(MenuItemBase.MenuPopupMode.TopLevelMenu, this._control);
          topLevelMenu.OnBeforePopup(e);
        }
        else
        {
          e = new MenuPopupEventArgs(MenuItemBase.MenuPopupMode.ContextMenu, this._control, position);
          topLevelMenu.OnBeforePopup(e);
          position = e.Position;
        }
        if (!topLevelMenu.HasVisibleSubitems())
        {
          topLevelMenu.OnAfterPopup((EventArgs) e);
          return false;
        }
        this._screen = !this.j ? Screen.FromPoint(this._popupMenuHost.ToolBar.PointToScreen(topLevelMenu.ButtonBounds.Location)) : Screen.FromPoint(position);
        if (isTopLevel && this._popupMenuHost.ToolBar != null)
          this._popupMenuHost.ToolBar.HighlightedItem = (ToolbarItemBase) topLevelMenu;
        PopupMenu popupMenu = topLevelMenu.CreatePopupMenu(this._popupMenuHost);
        popupMenu.a(this, this._screen);
        popupMenu.IsContextMenu = !isTopLevel;
        if (this._activeForm != null)
          this._activeForm.AddOwnedForm((Form) popupMenu);
        topLevelMenu.PopupMenu = popupMenu;
        if (isTopLevel)
          popupMenu.CalcMenuSize(false);
        else
          popupMenu.CalcMenuSize(false, position);
        if (skip)
          topLevelMenu.HighlightedItem = topLevelMenu.GetFirstVisibleItem();
        else
          topLevelMenu.HighlightedItem = (MenuButtonItem) null;
        popupMenu.ShowMenu(ref this.maximumMenuCount, MenuAnimator.SystemAnimation(this._popupMenuHost.MenuAnimation, skip));
        if (topLevelMenu.HighlightedItem != null)
          topLevelMenu.HighlightedItem.OnSelect();
        return true;
      }

      public bool b() => this._collapseMenu;

      private MenuAction b(TopLevelMenuItemBase A_0)
      {
        TopLevelMenuItemBase A_1 = this.a(A_0, -1);
        return A_1 == A_0 ? (MenuAction) null : new MenuAction(MenuAction.CommandType.Show, (MenuItemBase) A_1, true);
      }

      public MenuButtonItem Select(
        TopLevelMenuItemBase parentItem,
        bool select,
        bool isTopLevel,
        Point position)
      {
        this.j = !isTopLevel;
        if (MenuLooper._worker != null)
          MenuLooper._worker.f();
        MenuLooper._worker = this;
        if (this._popupMenuHost.ToolBar != null)
          this._popupMenuHost.ToolBar.OnEnterMenuLoop();
        this._activeForm = Form.ActiveForm;
        if (this._activeForm != null)
          this._activeForm.Deactivate += new EventHandler(this.ActiveForm_Deactivate);
        MenuButtonItem menuButtonItem;
        try
        {
          if (!this.e())
          {
            if (!this.a(parentItem, select, isTopLevel, position))
              return (MenuButtonItem) null;
          }
          else
            this._popupMenuHost.ToolBar.HighlightedItem = (ToolbarItemBase) parentItem;
          Win32.ReleaseCapture();
          menuButtonItem = this.c(parentItem);
        }
        finally
        {
          if (this._activeForm != null)
            this._activeForm.Deactivate -= new EventHandler(this.ActiveForm_Deactivate);
          if (this._popupMenuHost.ToolBar != null)
            this._popupMenuHost.ToolBar.OnExitMenuLoop();
        }
        MenuLooper._worker = (MenuLooper) null;
        if (menuButtonItem != null)
        {
          Application.DoEvents();
          menuButtonItem.OnActivate();
        }
        return menuButtonItem;
      }

      internal MenuAction GetMenuAction() => this._menuAction;

      private MenuButtonItem c(TopLevelMenuItemBase parentItem)
      {
        Win32.MSG A_0_1 = new Win32.MSG();
        this._e = new ArrayList();
        MenuButtonItem menuButtonItem = (MenuButtonItem) null;
        bool flag1 = false;
        MenuAction menuAction = (MenuAction) null;
        IntPtr num = IntPtr.Zero;
        bool flag2 = false;
        Control A_0_2 = (Control) null;
        Win32.HideCaret(IntPtr.Zero);
        Cursor.Current = Cursors.Default;
        if (this.e())
          flag1 = true;
        this._e.Insert(0, (object) parentItem);
        this._active = true;
        while (this._active)
        {
          switch (Win32.GetMessageA(out A_0_1, IntPtr.Zero, 0, 0))
          {
            case -1:
            case 0:
              this._active = false;
              continue;
            default:
              Win32.TranslateMessage(out A_0_1);
              this._menuAction = (MenuAction) null;
              MenuItemBase menuItemBase1;
              if (A_0_1.message == 161 || A_0_1.message == 164)
                menuAction = new MenuAction(MenuAction.CommandType.Cancel);
              else if (A_0_1.message == 123)
                this._active = true;
              else if (A_0_1.message >= 512 /*0x0200*/ && A_0_1.message <= 521)
              {
                Point screen = this.ClientToScreen(A_0_1.hwnd, A_0_1.lParam);
                menuItemBase1 = this.a((ICollection) this._e, screen);
                Control control = Control.FromHandle(A_0_1.hwnd);
                switch (A_0_1.message)
                {
                  case 512 /*0x0200*/:
                    if (!(num == A_0_1.lParam))
                    {
                      if (control is PopupMenu)
                        Win32.DispatchMessageA(ref A_0_1);
                      else if (control == this._popupMenuHost.ToolBar && control != null)
                        menuAction = this.GetItemAt((MenuItemBase) parentItem, screen);
                      num = A_0_1.lParam;
                      break;
                    }
                    break;
                  case 513:
                  case 515:
                  case 516:
                  case 517:
                  case 518:
                    if (control is PopupMenu)
                    {
                      Win32.DispatchMessageA(ref A_0_1);
                      break;
                    }
                    menuAction = new MenuAction(MenuAction.CommandType.Cancel, control);
                    break;
                  case 514:
                    if (control is PopupMenu)
                    {
                      Win32.DispatchMessageA(ref A_0_1);
                      break;
                    }
                    if (control != this._popupMenuHost.ToolBar && control != this._control)
                    {
                      menuAction = new MenuAction(MenuAction.CommandType.Cancel, control);
                      break;
                    }
                    break;
                }
              }
              else if (A_0_1.message >= 256 /*0x0100*/ && A_0_1.message <= 264)
              {
                MenuItemBase menuItemBase2 = (MenuItemBase) this._e[0];
                switch (A_0_1.message)
                {
                  case 256 /*0x0100*/:
                  case 260:
                    if (!this._popupMenuHost.RightToLeft && A_0_1.wParam.ToInt32() == 37 || this._popupMenuHost.RightToLeft && A_0_1.wParam.ToInt32() == 39)
                    {
                      if (this._e.Count >= 2)
                      {
                        this.HideItemPopupMenu(menuItemBase2);
                        this._e.Remove((object) menuItemBase2);
                        MenuItemBase menuItemBase3 = (MenuItemBase) this._e[0];
                        if (menuItemBase3.HighlightedItem != null)
                        {
                          menuItemBase3.HighlightedItem.OnSelect();
                          break;
                        }
                        break;
                      }
                      menuAction = this.b(parentItem);
                      break;
                    }
                    if (!this._popupMenuHost.RightToLeft && A_0_1.wParam.ToInt32() == 39 || this._popupMenuHost.RightToLeft && A_0_1.wParam.ToInt32() == 37)
                    {
                      if (menuItemBase2.PopupMenu != null && menuItemBase2.HighlightedItem != null && menuItemBase2.HighlightedItem.HasVisibleSubitems() && menuItemBase2.HighlightedItem.Enabled)
                      {
                        this.a((MenuItemBase) menuItemBase2.HighlightedItem, menuItemBase2, true);
                        this._e.Insert(0, (object) menuItemBase2.HighlightedItem);
                        break;
                      }
                      menuAction = this.a(parentItem);
                      break;
                    }
                    switch (A_0_1.wParam.ToInt32())
                    {
                      case 18:
                        menuAction = new MenuAction(MenuAction.CommandType.Cancel);
                        menuAction._e = false;
                        break;
                      case 27:
                        if (menuItemBase2.PopupMenu == null)
                        {
                          menuAction = new MenuAction(MenuAction.CommandType.Cancel);
                          break;
                        }
                        this.HideItemPopupMenu(menuItemBase2);
                        if (this._e.Count > 1)
                          this._e.Remove((object) menuItemBase2);
                        if (this.j)
                        {
                          menuAction = new MenuAction(MenuAction.CommandType.Cancel);
                          break;
                        }
                        if (this._e.Count == 1 && ((MenuItemBase) this._e[0]).PopupMenu == null)
                        {
                          menuItemBase1 = (MenuItemBase) this._e[0];
                          flag1 = true;
                          break;
                        }
                        break;
                      case 38:
                        if (menuItemBase2.HasVisibleSubitems())
                        {
                          if (flag1)
                          {
                            flag1 = false;
                            this.a(parentItem, true, true, Point.Empty);
                            break;
                          }
                          menuItemBase2.PopupMenu.a(-1);
                          break;
                        }
                        break;
                      case 40:
                        if (menuItemBase2.HasVisibleSubitems())
                        {
                          if (flag1)
                          {
                            flag1 = false;
                            this.a(parentItem, true, true, Point.Empty);
                            break;
                          }
                          menuItemBase2.PopupMenu.a(1);
                          break;
                        }
                        break;
                      default:
                        if (A_0_1.message == 260)
                        {
                          if (menuItemBase2.PopupMenu != null)
                          {
                            menuAction = this.a(menuItemBase2, char.ToUpper(Convert.ToChar(A_0_1.wParam.ToInt32())), this._e);
                            break;
                          }
                          menuAction = new MenuAction(MenuAction.CommandType.Cancel);
                          menuAction._e = false;
                          break;
                        }
                        break;
                    }
                    break;
                  case 258:
                    if (A_0_1.wParam.ToInt32() == 13)
                    {
                      if (flag1)
                      {
                        flag1 = false;
                        this.a(parentItem, true, true, Point.Empty);
                        break;
                      }
                      if (menuItemBase2.HighlightedItem != null && menuItemBase2.HighlightedItem.Enabled)
                      {
                        if (menuItemBase2.HighlightedItem.HasVisibleSubitems())
                        {
                          this.a((MenuItemBase) menuItemBase2.HighlightedItem, true);
                          break;
                        }
                        menuAction = new MenuAction(MenuAction.CommandType.Execute, (MenuItemBase) menuItemBase2.HighlightedItem);
                        break;
                      }
                      break;
                    }
                    if (menuItemBase2.PopupMenu == null)
                    {
                      menuAction = this.GetActionForMnemonic(char.ToUpper(Convert.ToChar(A_0_1.wParam.ToInt32())));
                      if (menuAction != null)
                      {
                        flag1 = false;
                        break;
                      }
                      break;
                    }
                    menuAction = this.a(menuItemBase2, char.ToUpper(Convert.ToChar(A_0_1.wParam.ToInt32())), this._e);
                    break;
                }
              }
              else
                Win32.DispatchMessageA(ref A_0_1);
              if (this._menuAction != null)
              {
                menuAction = this._menuAction;
                this._menuAction = (MenuAction) null;
              }
              if (menuAction != null)
              {
                switch (menuAction._commandType)
                {
                  case MenuAction.CommandType.Show:
                    this.a((IList) this._e, parentItem);
                    this._popupMenuHost.ToolBar.HighlightedItem = (ToolbarItemBase) menuAction._menu;
                    if (!flag1)
                      this.a((TopLevelMenuItemBase) menuAction._menu, menuAction._selectTopItem, true, Point.Empty);
                    this._e.Add((object) menuAction._menu);
                    parentItem = (TopLevelMenuItemBase) menuAction._menu;
                    break;
                  case MenuAction.CommandType.Cancel:
                    this._active = false;
                    flag2 = menuAction._e;
                    A_0_2 = menuAction._control;
                    break;
                  case MenuAction.CommandType.Execute:
                    if (this._e[0] is ToolBarButtonsCustomizeMenu.CustomizeMenuButtonItem)
                    {
                      menuAction._menu.OnActivate();
                      ((MenuItemBase) this._e[0]).PopupMenu.Invalidate(menuAction._menu.ButtonBounds);
                      break;
                    }
                    menuButtonItem = (MenuButtonItem) menuAction._menu;
                    this._active = false;
                    break;
                }
                menuAction = (MenuAction) null;
                continue;
              }
              continue;
          }
        }
        this.a((IList) this._e, parentItem);
        Win32.ShowCaret(IntPtr.Zero);
        this._e.Clear();
        if (flag2 && this.a(A_0_2))
          MenuLooper.PostMessage(A_0_1.hwnd, A_0_1.message, A_0_1.wParam, A_0_1.lParam);
        return menuButtonItem;
      }

      internal int GetMenuShowDelay()
      {
        int A_2 = 1;
        Win32.SystemParametersInfo(106, 0, ref A_2, 0);
        if (A_2 < 1)
          A_2 = 1;
        return A_2;
      }

      public bool e() => this.k;

      public void f()
      {
        if (!this._active)
          return;
        this._active = false;
        if (this._popupMenuHost.ToolBar == null)
          return;
        this._popupMenuHost.ToolBar.Refresh();
      }

      public void ShowFullMenu() => this._collapseMenu = false;

      [DllImport("user32.dll", SetLastError = true)]
      private static extern bool PostMessage(IntPtr A_0, int A_1, IntPtr A_2, IntPtr A_3);
    }
}
