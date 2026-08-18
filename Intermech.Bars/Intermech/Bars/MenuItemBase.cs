
// Type: Intermech.Bars.MenuItemBase
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [Designer("Intermech.Bars.MenuItemDesigner")]
    [DefaultEvent("BeforePopup")]
    [DebuggerDisplay("[{CommandName}] {Text}")]
    public abstract class MenuItemBase : ButtonItemBase, IButtonsSite
    {
      protected MenuItemBase.MenuItemCollection _items;
      private MenuItemBase _parentMenu;
      private MenuButtonItem _highlightedItem;
      private PopupMenu _popupMenu;
      private MenuOffset _menuDirection;

      public event MenuItemBase.BeforePopupEventHandler BeforePopup;

      public event EventHandler AfterPopup;

      protected MenuItemBase()
      {
        this._highlightedItem = (MenuButtonItem) null;
        this._popupMenu = (PopupMenu) null;
        this._menuDirection = MenuOffset.Bottom;
        this.ShowText = true;
      }

      protected MenuItemBase(string text)
      {
        this._highlightedItem = (MenuButtonItem) null;
        this._popupMenu = (PopupMenu) null;
        this._menuDirection = MenuOffset.Bottom;
        this.ShowText = true;
        this.Text = text;
      }

      protected internal override void ApplyLayout(
        Rectangle buttonBounds,
        Graphics graphics,
        bool vertical,
        bool rightToLeft)
      {
        base.ApplyLayout(buttonBounds, graphics, vertical, rightToLeft);
        if (this._popupMenu == null)
          return;
        this._popupMenu.LayoutNeeded();
      }

      public override ToolbarItemBase CloneItem()
      {
        MenuItemBase menuItemBase1 = (MenuItemBase) base.CloneItem();
        menuItemBase1.BeforePopup = this.BeforePopup;
        menuItemBase1.AfterPopup = this.AfterPopup;
        foreach (MenuItemBase menuItemBase2 in (CollectionBase) this.Items)
          menuItemBase1.Items.Add(menuItemBase2.CloneItem());
        return (ToolbarItemBase) menuItemBase1;
      }

      protected internal virtual PopupMenu CreatePopupMenu(IPopupMenuHost host)
      {
        return new PopupMenu(this, host);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          if (this.ParentMenu != null && this.ParentMenu.Items.Contains((ToolbarItemBase) this))
            this.ParentMenu.Items.Remove((ToolbarItemBase) this);
          if (this.HasChildren)
          {
            MenuButtonItem[] array = new MenuButtonItem[this.Items.Count];
            this.Items.CopyTo((ToolbarItemBase[]) array, 0);
            this.Items.Clear();
            foreach (Component component in array)
              component.Dispose();
          }
        }
        base.Dispose(disposing);
      }

      internal MenuButtonItem GetFirstVisibleItem()
      {
        if (this.HasChildren)
        {
          for (int index = 0; index < this.Items.Count; ++index)
          {
            if (this.Items[index].Visible && !this.Items[index]._underChevron)
              return this.Items[index];
          }
        }
        return (MenuButtonItem) null;
      }

      internal void HidePopupMenu()
      {
        this.OnAfterPopup(EventArgs.Empty);
        this._popupMenu.Hide();
        this._popupMenu.Dispose();
        this.PopupMenu = (PopupMenu) null;
      }

      public bool HasVisibleSubitems()
      {
        if (this._items != null)
        {
          foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.Items)
          {
            if (toolbarItemBase.Visible)
              return true;
          }
        }
        return false;
      }

      protected internal virtual void OnAfterPopup(EventArgs e)
      {
        if (this.AfterPopup == null)
          return;
        this.AfterPopup((object) this, e);
      }

      protected internal virtual void OnBeforePopup(MenuPopupEventArgs e)
      {
        if (this.BeforePopup == null)
          return;
        this.BeforePopup((object) this, e);
      }

      void IButtonsSite.ChildItemsChanged()
      {
        if (this._popupMenu != null)
          this._popupMenu.LayoutNeeded();
        this.Invalidate();
        if (this.ParentMenu == null)
          return;
        this.ParentMenu.UpdateAcceleratorTable();
      }

      [Browsable(false)]
      public Control ControlHost => (Control) null;

      internal override Font DefaultFont
      {
        get => this.Parent != null ? this.Parent.Font : SystemInformation.MenuFont;
      }

      public override string CommandPath
      {
        get
        {
          string commandPath = this.CommandName;
          if (this._parentMenu != null)
            commandPath = $"{this._parentMenu.CommandPath}.{commandPath}";
          return commandPath;
        }
      }

      public MenuItemBase FindItem(string[] paths, int startIndex)
      {
        if (startIndex >= paths.Length)
          return (MenuItemBase) null;
        MenuItemBase menuItemBase = this.FindItem(paths[startIndex++]);
        if (menuItemBase == null)
          return (MenuItemBase) null;
        return startIndex == paths.Length ? menuItemBase : menuItemBase.FindItem(paths, startIndex);
      }

      public MenuItemBase FindItem(string commandName)
      {
        if (this._items == null)
          return (MenuItemBase) null;
        foreach (MenuItemBase menuItemBase in (CollectionBase) this._items)
        {
          if (menuItemBase.CommandName == commandName || menuItemBase.CommandName == string.Empty && menuItemBase.Text == commandName)
            return menuItemBase;
        }
        return (MenuItemBase) null;
      }

      ToolbarItemBaseCollection IButtonsSite.Items => (ToolbarItemBaseCollection) this.Items;

      internal virtual void UpdateAcceleratorTable()
      {
        if (this._parentMenu == null)
          return;
        this._parentMenu.UpdateAcceleratorTable();
      }

      protected internal virtual System.Type DefaultChildType => typeof (MenuButtonItem);

      [Browsable(false)]
      public bool HasChildren => this._items != null && this._items.Count != 0;

      internal MenuButtonItem HighlightedItem
      {
        get => this._highlightedItem;
        set
        {
          if (this._highlightedItem != value)
          {
            if (this._highlightedItem != null)
              this._highlightedItem.Invalidate();
            this._highlightedItem = value;
            if (this._highlightedItem != null)
              this._highlightedItem.Invalidate();
            if (this.ParentMenu != null && this.ParentMenu._popupMenu != null)
              this.ParentMenu.HighlightedItem = (MenuButtonItem) this;
          }
          if (value != null)
            return;
          foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.Items)
          {
            if (menuButtonItem._popupMenu != null)
            {
              menuButtonItem.HighlightedItem = (MenuButtonItem) null;
              break;
            }
          }
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public virtual MenuItemBase.MenuItemCollection Items
      {
        get
        {
          if (this._items == null)
            this._items = new MenuItemBase.MenuItemCollection((IButtonsSite) this);
          return this._items;
        }
      }

      [Browsable(false)]
      public MenuOffset MenuDirection => this._menuDirection;

      internal void SetMenuDirection(MenuOffset value) => this._menuDirection = value;

      [Browsable(false)]
      public MenuItemBase Parent => this._parentMenu;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Obsolete("Use the Items property instead.")]
      [Browsable(false)]
      public MenuItemBase.MenuItemCollection MenuItems => this.Items;

      [Browsable(false)]
      public MenuItemBase ParentMenu => this._parentMenu;

      internal void SetParentMenu(MenuItemBase value) => this._parentMenu = value;

      internal PopupMenu PopupMenu
      {
        get => this._popupMenu;
        set
        {
          this._popupMenu = value;
          if (this.ToolBar == null)
            return;
          this.ToolBar.Refresh();
        }
      }

      [Browsable(false)]
      public override string ToolTipText
      {
        get => base.ToolTipText;
        set => base.ToolTipText = value;
      }

      public delegate void BeforePopupEventHandler(object sender, MenuPopupEventArgs e);

      public class MenuItemCollection(IButtonsSite A_0) : ToolbarItemBaseCollection(A_0)
      {
        public int Add(string text) => this.Add((ToolbarItemBase) new MenuButtonItem(text));

        public int Add(string text, EventHandler eventHandler)
        {
          return this.Add((ToolbarItemBase) new MenuButtonItem(text, eventHandler));
        }

        internal override bool IsComponentSuitable(ToolbarItemBase item) => item is MenuButtonItem;

        internal override void SetOwner(ToolbarItemBase item, object owner)
        {
          MenuItemBase menuItemBase = owner as MenuItemBase;
          ((MenuItemBase) item).SetParentMenu(menuItemBase);
        }

        public MenuButtonItem this[int index] => (MenuButtonItem) base[index];
      }

      public enum MenuPopupMode
      {
        TopLevelMenu,
        ContextMenu,
        SubMenu,
      }
    }
}
