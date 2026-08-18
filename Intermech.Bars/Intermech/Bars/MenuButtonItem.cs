
// Type: Intermech.Bars.MenuButtonItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [DefaultEvent("Click")]
    [DebuggerDisplay("[{CommandName}] {Text} {FriendlyShortcut}")]
    public class MenuButtonItem : MenuItemBase
    {
      private Keys _primaryShortcut;
      private Keys _secondaryShortcut;
      private bool _shortcutActive;
      private string _friendlyShortcut;

      public event EventHandler Select;

      internal event EventHandler Update;

      public MenuButtonItem()
      {
        this._friendlyShortcut = string.Empty;
        this._shortcutActive = true;
      }

      public MenuButtonItem(string text)
        : base(text)
      {
        this._friendlyShortcut = string.Empty;
      }

      public MenuButtonItem(string text, int imageIndex)
        : this(text)
      {
        this.ImageIndex = imageIndex;
      }

      public MenuButtonItem(string text, EventHandler eventHandler)
        : this(text, eventHandler, -1)
      {
      }

      public MenuButtonItem(string text, EventHandler eventHandler, int imageIndex)
        : this(text)
      {
        this.Click += eventHandler;
        this.ImageIndex = imageIndex;
      }

      internal bool b()
      {
        if (this.Enabled)
        {
          MenuItemBase menuItemBase = (MenuItemBase) this;
          do
          {
            menuItemBase = menuItemBase.ParentMenu;
            if (menuItemBase == null)
              return true;
          }
          while (menuItemBase.Enabled);
        }
        return false;
      }

      public void IncreaseImportance()
      {
        for (MenuButtonItem menuButtonItem = this; menuButtonItem != null; menuButtonItem = menuButtonItem.ParentMenu as MenuButtonItem)
          menuButtonItem.Importance = ToolBarItemImportance.Medium;
      }

      public override ToolbarItemBase CloneItem()
      {
        MenuButtonItem menuButtonItem = (MenuButtonItem) base.CloneItem();
        menuButtonItem.Shortcut = this.Shortcut;
        menuButtonItem.Select = this.Select;
        return (ToolbarItemBase) menuButtonItem;
      }

      public override void Invalidate()
      {
        if (this.ParentMenu == null || this.ParentMenu.PopupMenu == null)
          return;
        Rectangle buttonBounds = this.ButtonBounds;
        buttonBounds.Inflate(2, 2);
        this.ParentMenu.PopupMenu.Invalidate(buttonBounds);
      }

      internal override void LayoutNeeded()
      {
        base.LayoutNeeded();
        if (this.ParentMenu == null || this.ParentMenu.PopupMenu == null)
          return;
        this.ParentMenu.PopupMenu.LayoutNeeded();
      }

      protected internal void OnSelect()
      {
        if (this.Select == null)
          return;
        this.Select((object) this, EventArgs.Empty);
      }

      public override bool Checked
      {
        get => base.Checked;
        set
        {
          base.Checked = value;
          if (this.Update == null)
            return;
          this.Update((object) this, EventArgs.Empty);
        }
      }

      public override bool Enabled
      {
        get => base.Enabled;
        set
        {
          base.Enabled = value;
          if (this.Update == null)
            return;
          this.Update((object) this, EventArgs.Empty);
        }
      }

      internal string FriendlyShortcut
      {
        get
        {
          if (this._friendlyShortcut.Length == 0 && this._primaryShortcut != Keys.None)
          {
            KeysConverter converter = (KeysConverter) TypeDescriptor.GetConverter(typeof (Keys));
            string str1 = converter.ConvertToString((object) this._primaryShortcut);
            if (str1.Contains(converter.ConvertToString((object) Keys.Multiply)))
            {
              int length = str1.IndexOf(converter.ConvertToString((object) Keys.Multiply));
              str1 = str1.Substring(0, length) + "*";
            }
            if (str1.Length >= 3 && str1.Substring(str1.Length - 3, 1) == "+" && str1.Substring(str1.Length - 2, 1) == "D")
              str1 = str1.Substring(0, str1.Length - 2) + str1.Substring(str1.Length - 1, 1);
            string str2 = string.Empty;
            if (this._secondaryShortcut != Keys.None)
            {
              str2 = converter.ConvertToString((object) this._secondaryShortcut);
              if (str2.Contains(converter.ConvertToString((object) Keys.Multiply)))
              {
                int length = str2.IndexOf(converter.ConvertToString((object) Keys.Multiply));
                str2 = str2.Substring(0, length) + "*";
              }
              if (str2.Length >= 3 && str2.Substring(str2.Length - 3, 1) == "+" && str2.Substring(str2.Length - 2, 1) == "D")
                str2 = str2.Substring(0, str2.Length - 2) + str2.Substring(str2.Length - 1, 1);
            }
            this._friendlyShortcut = str2.Length != 0 ? $"{str1}, {str2}" : str1;
          }
          return this._friendlyShortcut;
        }
      }

      public override ImageList ImageList
      {
        get
        {
          MenuItemBase menuItemBase = (MenuItemBase) this;
          while (menuItemBase.ParentMenu != null)
            menuItemBase = menuItemBase.ParentMenu;
          switch (menuItemBase)
          {
            case DropDownMenuItem _ when ((DropDownMenuItem) menuItemBase).MenuImageList != null:
              return ((DropDownMenuItem) menuItemBase).MenuImageList;
            case ContextMenuBarItem _ when ((ContextMenuBarItem) menuItemBase).MenuImageList != null:
              return ((ContextMenuBarItem) menuItemBase).MenuImageList;
            default:
              return menuItemBase.ToolBar.ImageList;
          }
        }
      }

      public override ToolBarItemImportance Importance
      {
        get => base.Importance;
        set
        {
          base.Importance = value == ToolBarItemImportance.Medium || value == ToolBarItemImportance.Low ? value : throw new ArgumentException("Only Medium and Low are acceptable values for menu items.");
        }
      }

      internal override IButtonsSite Owner => (IButtonsSite) this.ParentMenu;

      [Description("The combination of keys that will activate this item.")]
      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Category("Behavior")]
      public Keys PrimaryShortcut
      {
        get => this._primaryShortcut;
        set
        {
          this._primaryShortcut = value;
          this._friendlyShortcut = string.Empty;
          if (this.ParentMenu != null)
            this.ParentMenu.UpdateAcceleratorTable();
          this.LayoutNeeded();
        }
      }

      [DefaultValue(true)]
      public bool ShortcutActive
      {
        get => this._shortcutActive;
        set
        {
          if (this._shortcutActive == value)
            return;
          this._shortcutActive = value;
          if (this.ParentMenu != null)
            this.ParentMenu.UpdateAcceleratorTable();
          this.LayoutNeeded();
        }
      }

      [Category("Behavior")]
      [Description("The second key combination that will activate the item after the first is activated.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      public Keys SecondaryShortcut
      {
        get => this._secondaryShortcut;
        set
        {
          this._secondaryShortcut = value;
          this._friendlyShortcut = string.Empty;
          if (this.ParentMenu != null)
            this.ParentMenu.UpdateAcceleratorTable();
          this.LayoutNeeded();
        }
      }

      [Description("Indicates the key combination that will activate this menu item.")]
      [Category("Behavior")]
      [DefaultValue(typeof (Shortcut), "None")]
      public Shortcut Shortcut
      {
        get
        {
          try
          {
            return (Shortcut) this.PrimaryShortcut;
          }
          catch
          {
            throw new InvalidOperationException("An advanced key combination that cannot be represented by the Shortcut enumeration has been used.");
          }
        }
        set => this.PrimaryShortcut = (Keys) value;
      }

      [DefaultValue(typeof (Shortcut), "None")]
      [Category("Behavior")]
      [Description("The second key combination that will activate the item after the first is activated.")]
      public Shortcut Shortcut2
      {
        get
        {
          try
          {
            return (Shortcut) this.SecondaryShortcut;
          }
          catch
          {
            throw new InvalidOperationException("An advanced key combination that cannot be represented by the Shortcut enumeration has been used.");
          }
        }
        set => this.SecondaryShortcut = (Keys) value;
      }
    }
}
