
// Type: Intermech.Bars.ButtonItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;


namespace Intermech.Bars
{
    public class ButtonItem : ButtonItemBase
    {
      private MenuButtonItem _buddyMenu;

      public ButtonItem()
      {
        this._buddyMenu = (MenuButtonItem) null;
        this._showText = false;
      }

      private void BuddyMenu_Update(object sender, EventArgs e)
      {
        this.Checked = this._buddyMenu.Checked;
        this.Enabled = this._buddyMenu.Enabled;
      }

      protected internal override void OnActivate()
      {
        if (this.BuddyMenu != null)
          this.BuddyMenu.OnActivate();
        else
          base.OnActivate();
      }

      [Category("Behavior")]
      [DefaultValue(typeof (MenuButtonItem), null)]
      [Description("The MenuItem to invoke when the user clicks this button.")]
      public MenuButtonItem BuddyMenu
      {
        get => this._buddyMenu;
        set
        {
          if (this._buddyMenu != null)
            this._buddyMenu.Update -= new EventHandler(this.BuddyMenu_Update);
          this._buddyMenu = value;
          if (this._buddyMenu != null && this.DesignMode)
          {
            this.Checked = value.Checked;
            this.Enabled = value.Enabled;
            if (this.ImageIndex == -1)
              this.ImageIndex = value.ImageIndex;
            if (this.ToolTipText.Length == 0)
              this.ToolTipText = value.Text.Replace("&", string.Empty);
          }
          if (this._buddyMenu == null)
            return;
          this._buddyMenu.Update += new EventHandler(this.BuddyMenu_Update);
        }
      }

      public override ToolbarItemBase CloneItem()
      {
        ButtonItem buttonItem = (ButtonItem) base.CloneItem();
        buttonItem.BuddyMenu = this.BuddyMenu;
        buttonItem.ShowText = this.ShowText;
        return (ToolbarItemBase) buttonItem;
      }
    }
}
