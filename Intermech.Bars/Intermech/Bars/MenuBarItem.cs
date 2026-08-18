
// Type: Intermech.Bars.MenuBarItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class MenuBarItem : TopLevelMenuItemBase
    {
      private bool _mdiWindowList;
      private bool _showIconsOnMdiWindowList;
      private ArrayList _mdiWindowItems;

      public MenuBarItem()
      {
        this._mdiWindowList = false;
        this._showIconsOnMdiWindowList = false;
        this._mdiWindowItems = (ArrayList) null;
      }

      public MenuBarItem(string text)
        : base(text)
      {
        this._mdiWindowList = false;
        this._showIconsOnMdiWindowList = false;
        this._mdiWindowItems = (ArrayList) null;
      }

      private void DisposeFormsItems()
      {
        if (this._mdiWindowItems == null)
          return;
        foreach (MenuButtonItem mdiWindowItem in this._mdiWindowItems)
        {
          mdiWindowItem.Click -= new EventHandler(this.FormItem_Click);
          mdiWindowItem.Dispose();
        }
        this._mdiWindowItems.Clear();
      }

      private void FormItem_Click(object A_0, EventArgs A_1)
      {
        ((Form) ((ToolbarItemBase) A_0).Tag).Activate();
      }

      public override ToolbarItemBase CloneItem()
      {
        MenuBarItem menuBarItem = (MenuBarItem) base.CloneItem();
        menuBarItem.MdiWindowList = this.MdiWindowList;
        menuBarItem.ShowIconsOnMdiWindowList = this.ShowIconsOnMdiWindowList;
        return (ToolbarItemBase) menuBarItem;
      }

      protected internal override void OnAfterPopup(EventArgs e)
      {
        base.OnAfterPopup(e);
        this.DisposeFormsItems();
      }

      protected internal override void OnBeforePopup(MenuPopupEventArgs e)
      {
        if (this._mdiWindowList)
        {
          if (!(this.ToolBar is MenuBar))
            return;
          MenuBar toolBar = (MenuBar) this.ToolBar;
          if (toolBar.OwnerForm == null || !toolBar.OwnerForm.IsMdiContainer)
            return;
          this.DisposeFormsItems();
          foreach (Form mdiChild in toolBar.OwnerForm.MdiChildren)
          {
            MenuBarItem.FormMenuItem formMenuItem = new MenuBarItem.FormMenuItem();
            formMenuItem._form = mdiChild;
            formMenuItem.Text = mdiChild.Text;
            if (this._showIconsOnMdiWindowList)
            {
              formMenuItem.Icon = new Icon(mdiChild.Icon, 16 /*0x10*/, 16 /*0x10*/);
              formMenuItem.IconSize = new Size(16 /*0x10*/, 16 /*0x10*/);
            }
            this._mdiWindowItems.Add((object) formMenuItem);
            this.Items.Add((ToolbarItemBase) formMenuItem);
            if (toolBar.OwnerForm.ActiveMdiChild == mdiChild)
              formMenuItem.Checked = true;
            formMenuItem.Click += new EventHandler(this.FormItem_Click);
          }
          if (this._mdiWindowItems.Count != 0)
            ((ToolbarItemBase) this._mdiWindowItems[0]).BeginGroup = true;
        }
        base.OnBeforePopup(e);
      }

      [Browsable(false)]
      public override bool BeginGroup
      {
        get => false;
        set
        {
        }
      }

      [Browsable(false)]
      public override bool Checked
      {
        get => false;
        set
        {
        }
      }

      [Browsable(false)]
      public override Icon Icon
      {
        get => (Icon) null;
        set
        {
        }
      }

      [Browsable(false)]
      public override Size IconSize
      {
        get => base.IconSize;
        set => base.IconSize = value;
      }

      [Browsable(false)]
      public override Image Image
      {
        get => base.Image;
        set => base.Image = value;
      }

      [Browsable(false)]
      public override int ImageIndex
      {
        get => -1;
        set
        {
        }
      }

      [DefaultValue(false)]
      [Description("Indicates whether this item will show a list of mdi children.")]
      [Category("Behavior")]
      public virtual bool MdiWindowList
      {
        get => this._mdiWindowList;
        set
        {
          this._mdiWindowList = value;
          if (!this._mdiWindowList || this._mdiWindowItems != null)
            return;
          this._mdiWindowItems = new ArrayList();
        }
      }

      [Description("Indicates whether form icons should be shown in the mdi window list.")]
      [DefaultValue(false)]
      [Category("Behavior")]
      public virtual bool ShowIconsOnMdiWindowList
      {
        get => this._showIconsOnMdiWindowList;
        set => this._showIconsOnMdiWindowList = value;
      }

      private class FormMenuItem : MenuButtonItem
      {
        public Form _form;

        protected internal override void OnActivate()
        {
          base.OnActivate();
          if (this._form == null)
            return;
          this._form.Activate();
        }
      }
    }
}
