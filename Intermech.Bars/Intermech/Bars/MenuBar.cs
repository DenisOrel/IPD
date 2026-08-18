
// Type: Intermech.Bars.MenuBar
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Bars
{
    [ProvideProperty("PopupMenu", typeof (Control))]
    [Designer("Intermech.Bars.MenuBarDesigner")]
    public class MenuBar : ToolBar, IExtenderProvider
    {
      private bool _alwaysShowMnemonics;
      private Hashtable _contextMenus;
      private Hashtable _contextMenuWindows;
      private MenuBar.FormSysCommandListener _sysCommandListener;
      private ShortcutListener _shortcutListener;
      private Form _ownerForm;
      private bool _maximizedWindows;
      private MenuBar.MdiButtonDisplayMode _mdiButtonDisplay;
      private ButtonItem _minimizeButton;
      private ButtonItem _restoreButton;
      private ButtonItem _closeButton;
      private MenuBarItem _mdiFormMenu;
      private MdiHelper _mdiHelper;
      private bool _showMdiSystemMenu;
      private bool _fullMenu;

      public MenuBar()
      {
        this._alwaysShowMnemonics = true;
        this._fullMenu = false;
        this._maximizedWindows = false;
        this._mdiButtonDisplay = MenuBar.MdiButtonDisplayMode.All;
        this._showMdiSystemMenu = true;
        this._contextMenus = new Hashtable();
        this._contextMenuWindows = new Hashtable();
        this._shortcutListener = new ShortcutListener();
        this.Text = "Menu Bar";
        this.Closable = false;
        this.Stretch = true;
        this.Overflow = ToolBarOverflow.Wrap;
        this.AllowRightToLeft = true;
        this._mdiHelper = new MdiHelper(this);
        this._mdiHelper.ControlChanged += new EventHandler(this.MdiHelper_ControlChanged);
      }

      public MenuBarItem AddMenuBar(string text) => this.AddMenuBar(text, -1);

      public MenuBarItem AddMenuBar(string text, int imageIndex)
      {
        MenuBarItem menuBarItem = new MenuBarItem(text);
        menuBarItem.ImageIndex = imageIndex;
        this._items.Add((ToolbarItemBase) menuBarItem);
        return menuBarItem;
      }

      public MenuBarItem AddMenuBar(string text, Image image)
      {
        MenuBarItem menuBarItem = new MenuBarItem(text);
        menuBarItem.Image = image;
        this._items.Add((ToolbarItemBase) menuBarItem);
        return menuBarItem;
      }

      private void ShowMdiMenu()
      {
        MenuButtonItem menuButtonItem1 = new MenuButtonItem(BarLanguage.RestoreMenuText);
        using (Stream manifestResourceStream = typeof (MenuBar).Assembly.GetManifestResourceStream("Resources.restore.gif"))
          menuButtonItem1.Image = Image.FromStream(manifestResourceStream);
        menuButtonItem1.Enabled = this.MdiButtonDisplay == MenuBar.MdiButtonDisplayMode.All;
        MenuButtonItem menuButtonItem2 = new MenuButtonItem(BarLanguage.MoveMenuText);
        menuButtonItem2.Enabled = false;
        MenuButtonItem menuButtonItem3 = new MenuButtonItem(BarLanguage.SizeMenuText);
        menuButtonItem3.Enabled = false;
        MenuButtonItem menuButtonItem4 = new MenuButtonItem(BarLanguage.MinimizeMenuText);
        using (Stream manifestResourceStream = typeof (MenuBar).Assembly.GetManifestResourceStream("Resources.minimize.gif"))
          menuButtonItem4.Image = Image.FromStream(manifestResourceStream);
        menuButtonItem4.Enabled = this.MdiButtonDisplay == MenuBar.MdiButtonDisplayMode.All;
        MenuButtonItem menuButtonItem5 = new MenuButtonItem(BarLanguage.MaximizeMenuText);
        menuButtonItem5.Enabled = false;
        using (Stream manifestResourceStream = typeof (MenuBar).Assembly.GetManifestResourceStream("Resources.maximize.gif"))
          menuButtonItem5.Image = Image.FromStream(manifestResourceStream);
        MenuButtonItem menuButtonItem6 = new MenuButtonItem(BarLanguage.CloseMenuText);
        using (Stream manifestResourceStream = typeof (MenuBar).Assembly.GetManifestResourceStream("Resources.close.gif"))
          menuButtonItem6.Image = Image.FromStream(manifestResourceStream);
        menuButtonItem6.Enabled = this.MdiButtonDisplay != 0;
        this._mdiFormMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[6]
        {
          menuButtonItem1,
          menuButtonItem2,
          menuButtonItem3,
          menuButtonItem4,
          menuButtonItem5,
          menuButtonItem6
        });
        menuButtonItem6.BeginGroup = true;
        MenuBarItem mdiFormMenu = this._mdiFormMenu;
        Rectangle buttonBounds = this._mdiFormMenu.ButtonBounds;
        int left = buttonBounds.Left;
        buttonBounds = this._mdiFormMenu.ButtonBounds;
        int bottom = buttonBounds.Bottom;
        Point position = new Point(left, bottom);
        MenuButtonItem menuButtonItem7 = mdiFormMenu.Show((Control) this, position);
        menuButtonItem1.Dispose();
        menuButtonItem2.Dispose();
        menuButtonItem3.Dispose();
        menuButtonItem4.Dispose();
        menuButtonItem5.Dispose();
        menuButtonItem6.Dispose();
        if (this.OwnerForm.ActiveMdiChild == null)
          return;
        if (menuButtonItem7 == menuButtonItem1)
          this.OwnerForm.ActiveMdiChild.WindowState = FormWindowState.Normal;
        else if (menuButtonItem7 == menuButtonItem4)
        {
          this.OwnerForm.ActiveMdiChild.WindowState = FormWindowState.Minimized;
        }
        else
        {
          if (menuButtonItem7 != menuButtonItem6)
            return;
          this.OwnerForm.ActiveMdiChild.Close();
        }
      }

      private void PaintMdiIcon(Graphics g)
      {
        if (this.OwnerForm.ActiveMdiChild == null)
          return;
        try
        {
          using (Icon icon = new Icon(this.OwnerForm.ActiveMdiChild.Icon, new Size(16 /*0x10*/, 16 /*0x10*/)))
            g.DrawIcon(icon, this._mdiFormMenu.ButtonBounds);
        }
        catch
        {
        }
      }

      private bool a(Form A_0)
      {
        if (A_0.MdiChildren.Length != 0)
        {
          foreach (Control mdiChild in A_0.MdiChildren)
          {
            if (mdiChild.Visible)
              return true;
          }
        }
        return false;
      }

      private void MdiHelper_ControlChanged(object A_0, EventArgs A_1)
      {
        bool flag = this.a(this.OwnerForm);
        if (flag)
        {
          flag = false;
          foreach (Form mdiChild in this.OwnerForm.MdiChildren)
          {
            if (mdiChild.WindowState == FormWindowState.Maximized)
            {
              flag = true;
              break;
            }
          }
        }
        if (flag == this._maximizedWindows)
          return;
        this._maximizedWindows = flag;
        if (this._maximizedWindows && this._mdiFormMenu == null)
        {
          this._mdiFormMenu = new MenuBarItem();
          this._mdiFormMenu.SetToolBar((ToolBar) this);
          this._minimizeButton = (ButtonItem) new SystemButton(ToolBarGlyphType.Minimize);
          this._minimizeButton.SetToolBar((ToolBar) this);
          this._minimizeButton.ToolTipText = BarLanguage.MinimizeWindowText;
          this._restoreButton = (ButtonItem) new SystemButton(ToolBarGlyphType.Restore);
          this._restoreButton.SetToolBar((ToolBar) this);
          this._restoreButton.ToolTipText = BarLanguage.RestoreWindowText;
          this._closeButton = (ButtonItem) new SystemButton(ToolBarGlyphType.Close);
          this._closeButton.SetToolBar((ToolBar) this);
          this._closeButton.ToolTipText = BarLanguage.CloseWindowText;
        }
        this.DoLayout();
      }

      internal void ShowContextMenu(Control control, Point pos)
      {
        ((TopLevelMenuItemBase) this._contextMenus[(object) control]).Show((IPopupMenuHost) this, control, pos);
      }

      public MenuBarItem FindMenuBar(string commandName)
      {
        foreach (MenuBarItem menuBar in (CollectionBase) this._items)
        {
          if (menuBar.CommandName == commandName)
            return menuBar;
        }
        return (MenuBarItem) null;
      }

      public MenuItemBase FindMenuItem(string menuCommandPath)
      {
        string[] paths = menuCommandPath.Split('.');
        if (paths.Length == 0)
          return (MenuItemBase) null;
        return this.FindMenuBar(paths[0])?.FindItem(paths, 1);
      }

      private void b()
      {
        MenuBarItem parentItem = (MenuBarItem) null;
        foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.Items)
        {
          if (toolbarItemBase is MenuBarItem && toolbarItemBase.Enabled && toolbarItemBase.Visible)
          {
            parentItem = (MenuBarItem) toolbarItemBase;
            break;
          }
        }
        if (parentItem == null)
          return;
        MenuLooper menuLooper = new MenuLooper((IPopupMenuHost) this, (Control) this, this.TopLevelMenuItems);
        menuLooper.a(true);
        menuLooper.Select((TopLevelMenuItemBase) parentItem, false, true, Point.Empty);
        menuLooper.Dispose();
      }

      private void MdiChild_Activate(object A_0, EventArgs A_1)
      {
        if (this._maximizedWindows && this._mdiFormMenu != null)
          this._mdiFormMenu.Invalidate();
        BarManager.UndoMerge((ToolBar) this);
        if (!this.AllowMerge || this.OwnerForm == null || this.OwnerForm.ActiveMdiChild == null)
          return;
        MenuBar source = (MenuBar) null;
        foreach (Control control in (ArrangedElementCollection) this.OwnerForm.ActiveMdiChild.Controls)
        {
          if (control is MenuBar && ((ToolBar) control).AllowMerge)
          {
            source = (MenuBar) control;
            break;
          }
        }
        if (source == null)
          return;
        BarManager.Merge((ToolBar) source, (ToolBar) this);
      }

      internal override void CalculateLayoutInternal(IToolBarRenderer renderer, bool vertical)
      {
        base.CalculateLayoutInternal(renderer, vertical);
        if (!this._maximizedWindows)
          return;
        int num1 = SystemInformation.ToolWindowCaptionButtonSize.Width - 1;
        int num2 = num1 + 1;
        int y1 = this.Situation != ToolBarSituation.Contained || !this.Movable ? 6 : 12;
        Rectangle clientRectangle;
        if (vertical)
        {
          this._mdiFormMenu.ApplyLayout(new Rectangle(this.ClientRectangle.Width / 2 - 8, y1, 16 /*0x10*/, 16 /*0x10*/), (Graphics) null, false, false);
        }
        else
        {
          MenuBarItem mdiFormMenu = this._mdiFormMenu;
          int x = y1;
          clientRectangle = this.ClientRectangle;
          int y2 = clientRectangle.Height / 2 - 8;
          Rectangle buttonBounds = new Rectangle(x, y2, 16 /*0x10*/, 16 /*0x10*/);
          mdiFormMenu.ApplyLayout(buttonBounds, (Graphics) null, false, false);
        }
        int num3;
        if (vertical)
        {
          clientRectangle = this.ClientRectangle;
          num3 = clientRectangle.Width / 2 - num1 / 2;
        }
        else
        {
          clientRectangle = this.ClientRectangle;
          num3 = clientRectangle.Height / 2 - num1 / 2;
        }
        if (this._mdiButtonDisplay == MenuBar.MdiButtonDisplayMode.All)
        {
          if (vertical)
          {
            clientRectangle = this.ClientRectangle;
            int y3 = clientRectangle.Height - 3 - num2 * 3;
            this._minimizeButton.ApplyLayout(new Rectangle(num3, y3, num1, num1), (Graphics) null, false, false);
          }
          else
          {
            clientRectangle = this.ClientRectangle;
            this._minimizeButton.ApplyLayout(new Rectangle(clientRectangle.Width - 3 - num2 * 3, num3, num1, num1), (Graphics) null, false, false);
          }
          if (vertical)
          {
            clientRectangle = this.ClientRectangle;
            int y4 = clientRectangle.Height - 3 - num2 * 2;
            this._restoreButton.ApplyLayout(new Rectangle(num3, y4, num1, num1), (Graphics) null, false, false);
          }
          else
          {
            clientRectangle = this.ClientRectangle;
            this._restoreButton.ApplyLayout(new Rectangle(clientRectangle.Width - 3 - num2 * 2, num3, num1, num1), (Graphics) null, false, false);
          }
        }
        if (this._mdiButtonDisplay != MenuBar.MdiButtonDisplayMode.All && this._mdiButtonDisplay != MenuBar.MdiButtonDisplayMode.CloseOnly)
          return;
        if (vertical)
        {
          clientRectangle = this.ClientRectangle;
          int y5 = clientRectangle.Height - 3 - num2;
          this._closeButton.ApplyLayout(new Rectangle(num3, y5, num1, num1), (Graphics) null, false, false);
        }
        else
        {
          clientRectangle = this.ClientRectangle;
          this._closeButton.ApplyLayout(new Rectangle(clientRectangle.Width - 3 - num2, num3, num1, num1), (Graphics) null, false, false);
        }
      }

      public override Font Font => SystemFonts.MenuFont;

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          this._shortcutListener.Dispose();
          this._mdiHelper.AttachForm((Form) null);
          if (this._mdiFormMenu != null)
          {
            this._mdiFormMenu.Dispose();
            this._closeButton.Dispose();
            this._minimizeButton.Dispose();
            this._restoreButton.Dispose();
          }
        }
        base.Dispose(disposing);
      }

      [DefaultValue(typeof (MenuBarItem), null)]
      [Category("Behavior")]
      public MenuBarItem GetPopupMenu(Control control)
      {
        return this._contextMenus.Contains((object) control) ? (MenuBarItem) this._contextMenus[(object) control] : (MenuBarItem) null;
      }

      protected override void OnDoubleClick(EventArgs e)
      {
        Point client = this.PointToClient(Cursor.Position);
        if (this._maximizedWindows && this.ShowMdiSystemMenu && this._mdiFormMenu.ButtonBounds.Contains(client) && this.OwnerForm.ActiveMdiChild != null)
          this.OwnerForm.ActiveMdiChild.Close();
        else
          base.OnDoubleClick(e);
      }

      protected override void OnHandleCreated(EventArgs e)
      {
        base.OnHandleCreated(e);
        if (this.DesignMode)
          return;
        this._shortcutListener.Listening = true;
      }

      protected override void OnItemRelease(ToolbarItemBase item, Point position)
      {
        if (item == this._minimizeButton && this._maximizedWindows)
          this.OwnerForm.ActiveMdiChild.WindowState = FormWindowState.Minimized;
        else if (item == this._restoreButton && this._maximizedWindows)
          this.OwnerForm.ActiveMdiChild.WindowState = FormWindowState.Normal;
        else if (item == this._closeButton && this._maximizedWindows)
          this.OwnerForm.ActiveMdiChild.Close();
        else
          base.OnItemRelease(item, position);
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
        if (this._maximizedWindows && this._mdiFormMenu.ButtonBounds.Contains(e.X, e.Y))
          this.ShowMdiMenu();
        else
          base.OnMouseDown(e);
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        base.OnPaint(e);
        if (!this._maximizedWindows || !this.ShowMdiSystemMenu)
          return;
        this.PaintMdiIcon(e.Graphics);
      }

      protected override void OnPaintBackground(PaintEventArgs pevent)
      {
        this.WorkingRenderer.DrawMenuBarBackground(this, pevent.Graphics, this.ClientRectangle, this.Flow == ToolBarLayout.Vertical);
      }

      public void SetPopupMenu(Control control, MenuBarItem value)
      {
        if (this._contextMenuWindows.Contains((object) control) && value == null)
        {
          ((ContextMenuWindow) this._contextMenuWindows[(object) control]).Dispose();
          this._contextMenuWindows.Remove((object) control);
        }
        this._contextMenus[(object) control] = (object) value;
        if (value == null)
          this._contextMenus.Remove((object) control);
        if (value == null || this.DesignMode || this._contextMenuWindows.Contains((object) control))
          return;
        ContextMenuWindow contextMenuWindow = new ContextMenuWindow(this, control);
        this._contextMenuWindows.Add((object) control, (object) contextMenuWindow);
      }

      bool IExtenderProvider.CanExtend(object extendee) => extendee is Control;

      [DefaultValue(true)]
      public override bool AllowRightToLeft
      {
        get => base.AllowRightToLeft;
        set => base.AllowRightToLeft = value;
      }

      [Category("Appearance")]
      [DefaultValue(true)]
      [Description("Indicates whether keyboard mnemonics are always shown on the menu bar.")]
      public bool AlwaysShowMnemonics
      {
        get => this._alwaysShowMnemonics;
        set
        {
          this._alwaysShowMnemonics = value;
          this.Invalidate();
        }
      }

      [DefaultValue(false)]
      public override bool Closable
      {
        get => base.Closable;
        set => base.Closable = value;
      }

      [Browsable(false)]
      [DefaultValue(false)]
      public override bool DrawActionsButton
      {
        get => false;
        set
        {
        }
      }

      internal override ToolbarItemBase[] ExtraButtons
      {
        get
        {
          if (!this._maximizedWindows)
            return base.ExtraButtons;
          if (this.MdiButtonDisplay == MenuBar.MdiButtonDisplayMode.All)
            return new ToolbarItemBase[3]
            {
              (ToolbarItemBase) this._minimizeButton,
              (ToolbarItemBase) this._restoreButton,
              (ToolbarItemBase) this._closeButton
            };
          if (this.MdiButtonDisplay != MenuBar.MdiButtonDisplayMode.CloseOnly)
            return new ToolbarItemBase[0];
          return new ToolbarItemBase[1]
          {
            (ToolbarItemBase) this._closeButton
          };
        }
      }

      protected internal override int LeftPadding
      {
        get => this._maximizedWindows && this.ShowMdiSystemMenu ? 24 : 0;
      }

      [Description("Indicates which mdi buttons will be displayed when an mdi child form is maximized.")]
      [Category("Appearance")]
      [DefaultValue(typeof (MenuBar.MdiButtonDisplayMode), "All")]
      public MenuBar.MdiButtonDisplayMode MdiButtonDisplay
      {
        get => this._mdiButtonDisplay;
        set
        {
          this._mdiButtonDisplay = value;
          if (!this._maximizedWindows)
            return;
          this.DoLayout();
        }
      }

      [DefaultValue(false)]
      [Browsable(true)]
      public override bool FullMenus
      {
        get => this._fullMenu;
        set => this._fullMenu = value;
      }

      [DefaultValue(typeof (ToolBarOverflow), "Wrap")]
      public override ToolBarOverflow Overflow
      {
        get => base.Overflow;
        set => base.Overflow = value;
      }

      [Browsable(false)]
      public Form OwnerForm
      {
        get => this._ownerForm;
        set
        {
          if (value == this._ownerForm)
            return;
          if (this._ownerForm != null && !this.DesignMode)
          {
            this._ownerForm.MdiChildActivate += new EventHandler(this.MdiChild_Activate);
            this._sysCommandListener.Dispose();
          }
          this._ownerForm = value;
          this._mdiHelper.AttachForm(value);
          this._shortcutListener.OwnerForm = value;
          if (this._ownerForm == null || this.DesignMode)
            return;
          this._ownerForm.MdiChildActivate += new EventHandler(this.MdiChild_Activate);
          this._sysCommandListener = new MenuBar.FormSysCommandListener(this._ownerForm, this);
        }
      }

      protected internal override int RightPadding
      {
        get
        {
          if (this._maximizedWindows)
          {
            switch (this._mdiButtonDisplay)
            {
              case MenuBar.MdiButtonDisplayMode.All:
                return SystemInformation.ToolWindowCaptionButtonSize.Width * 3 + 3;
              case MenuBar.MdiButtonDisplayMode.CloseOnly:
                return SystemInformation.ToolWindowCaptionButtonSize.Width + 3;
            }
          }
          return 0;
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public ShortcutListener ShortcutListener
      {
        get => this._shortcutListener;
        set
        {
          if (value == null)
            throw new ArgumentNullException();
          this._shortcutListener.Dispose();
          this._shortcutListener = value;
          this._shortcutListener.UpdateAcceleratorTable((ToolBar) this);
        }
      }

      [Category("Appearance")]
      [Description("Indicates whether the MDI system menu will be shown for maximized MDI children.")]
      [DefaultValue(true)]
      public bool ShowMdiSystemMenu
      {
        get => this._showMdiSystemMenu;
        set
        {
          this._showMdiSystemMenu = value;
          if (!this._maximizedWindows)
            return;
          this.DoLayout();
        }
      }

      public override ISite Site
      {
        get => base.Site;
        set
        {
          base.Site = value;
          if (value == null)
            return;
          IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
          if (service == null || !(service.RootComponent is Form))
            return;
          this.OwnerForm = (Form) service.RootComponent;
        }
      }

      [DefaultValue(true)]
      public override bool Stretch
      {
        get => base.Stretch;
        set => base.Stretch = value;
      }

      [DefaultValue("Menu Bar")]
      public override string Text
      {
        get => base.Text;
        set => base.Text = value;
      }

      private class FormSysCommandListener : NativeWindow, IDisposable
      {
        private Form _form;
        private MenuBar _menuBar;

        public FormSysCommandListener(Form form, MenuBar menuBar)
        {
          this._form = form;
          this._menuBar = menuBar;
          form.HandleCreated += new EventHandler(this.Form_HandleCreated);
          form.HandleDestroyed += new EventHandler(this.Form_HandleDestroyed);
          if (!form.IsHandleCreated)
            return;
          this.AssignHandle(form.Handle);
        }

        public void Dispose()
        {
          if (!(this.Handle != IntPtr.Zero))
            return;
          this.ReleaseHandle();
        }

        protected override void WndProc(ref Message msg)
        {
          if (msg.Msg == 274)
          {
            IntPtr num = msg.WParam;
            if (num.ToInt32() == 61696)
            {
              num = msg.LParam;
              if (num.ToInt32() == 0 && this._menuBar.Enabled)
              {
                this._menuBar.b();
                msg.Result = IntPtr.Zero;
                return;
              }
            }
          }
          base.WndProc(ref msg);
        }

        private void Form_HandleDestroyed(object A_0, EventArgs A_1) => this.ReleaseHandle();

        private void Form_HandleCreated(object A_0, EventArgs A_1)
        {
          this.AssignHandle(this._form.Handle);
        }
      }

      public enum MdiButtonDisplayMode
      {
        None,
        All,
        CloseOnly,
      }
    }
}
