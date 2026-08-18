
// Type: Intermech.Bars.ToolBarButtonsCustomizeMenu
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;


namespace Intermech.Bars
{
    internal class ToolBarButtonsCustomizeMenu : TopLevelMenuItemBase
    {
      private ToolBar _toolbar;
      private bool _addRemoveVisible;

      public ToolBarButtonsCustomizeMenu(ToolBar toolbar)
      {
        this._addRemoveVisible = true;
        this._toolbar = toolbar;
        this.SetToolBar(toolbar);
        this.ToolTipText = BarLanguage.ToolbarOptionsText;
      }

      private void CleanOldToolbarItems()
      {
        MenuButtonItem[] array = new MenuButtonItem[this.Items.Count];
        this.Items.CopyTo((ToolbarItemBase[]) array, 0);
        for (int index = 0; index < array.Length; ++index)
          array[index].Dispose();
      }

      public void SetAddRemoveVisible(bool visible) => this._addRemoveVisible = visible;

      private void AddToolbarItems(MenuButtonItem parentMenu)
      {
        foreach (ToolbarItemBase button in (CollectionBase) this._toolbar.Items)
        {
          if (!button.Locked)
          {
            ToolBarButtonsCustomizeMenu.MenuItemWithToolbarButton withToolbarButton = new ToolBarButtonsCustomizeMenu.MenuItemWithToolbarButton(button);
            withToolbarButton.Checked = button.Visible;
            withToolbarButton.Text = button.Text;
            if (withToolbarButton.Text.Length == 0)
              withToolbarButton.Text = button.ToolTipText;
            withToolbarButton.BeginGroup = button.BeginGroup;
            withToolbarButton.Click += new EventHandler(this.MenuItem_Click);
            if (button is ButtonItemBase)
            {
              ButtonItemBase buttonItemBase = (ButtonItemBase) button;
              withToolbarButton.ImageIndex = buttonItemBase.ImageIndex;
              withToolbarButton.Icon = buttonItemBase.Icon;
              withToolbarButton.IconSize = buttonItemBase.IconSize;
              withToolbarButton.Image = buttonItemBase.Image;
            }
            parentMenu.Items.Add((ToolbarItemBase) withToolbarButton);
          }
        }
      }

      protected internal override void OnBeforePopup(MenuPopupEventArgs mpe)
      {
        base.OnBeforePopup(mpe);
        this.CleanOldToolbarItems();
        if (this._toolbar.Overflow == ToolBarOverflow.Chevron)
        {
          for (int index = 0; index < this._toolbar.Items.Count; ++index)
          {
            if (this._toolbar.Items[index]._underChevron && this._toolbar.Items[index] is ButtonItemBase && this._toolbar.Items[index].IsVisible)
            {
              ButtonItemBase buttonItemBase = (ButtonItemBase) this._toolbar.Items[index];
              MenuButtonItem menuButtonItem = new MenuButtonItem();
              menuButtonItem.Text = buttonItemBase.Text;
              menuButtonItem.ImageIndex = buttonItemBase.ImageIndex;
              menuButtonItem.Icon = buttonItemBase.Icon;
              menuButtonItem.Image = buttonItemBase.Image;
              menuButtonItem.IconSize = buttonItemBase.IconSize;
              menuButtonItem.BeginGroup = buttonItemBase.BeginGroup;
              menuButtonItem.Checked = buttonItemBase.Checked;
              menuButtonItem.Enabled = buttonItemBase.Enabled;
              menuButtonItem.Font = buttonItemBase.Font;
              menuButtonItem.ForeColor = buttonItemBase.ForeColor;
              if (menuButtonItem.Text.Length == 0)
                menuButtonItem.Text = buttonItemBase.ToolTipText;
              if (buttonItemBase is MenuItemBase)
              {
                foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) ((MenuItemBase) buttonItemBase).Items)
                  menuButtonItem.Items.Add(toolbarItemBase.CloneItem());
              }
              menuButtonItem.Tag = (object) buttonItemBase;
              menuButtonItem.Click += new EventHandler(this.Chevroned_ItemClick);
              this.Items.Add((ToolbarItemBase) menuButtonItem);
            }
          }
        }
        if (this._addRemoveVisible)
        {
          MenuButtonItem parentMenu = (MenuButtonItem) new ToolBarButtonsCustomizeMenu.CustomizeMenuButtonItem(BarLanguage.AddRemoveButtonsText);
          parentMenu.BeginGroup = true;
          this.AddToolbarItems(parentMenu);
          parentMenu.Enabled = parentMenu.HasChildren;
          this.Items.Add((ToolbarItemBase) parentMenu);
        }
        this.ToolBar.OnCustomizeActionsButtonMenu(EventArgs.Empty);
      }

      private void Chevroned_ItemClick(object sender, EventArgs A_1)
      {
        MenuButtonItem menuButtonItem = (MenuButtonItem) sender;
        if (!(menuButtonItem.Tag is ButtonItemBase))
          return;
        ((ButtonItemBase) menuButtonItem.Tag).OnActivate();
      }

      public bool GetAddRemoveVisible() => this._addRemoveVisible;

      private void MenuItem_Click(object sender, EventArgs e)
      {
        ToolBarButtonsCustomizeMenu.MenuItemWithToolbarButton withToolbarButton = (ToolBarButtonsCustomizeMenu.MenuItemWithToolbarButton) sender;
        withToolbarButton.Checked = !withToolbarButton.Checked;
        withToolbarButton.ToolBarButton.SetUserVisible(withToolbarButton.Checked, withToolbarButton.Checked);
      }

      public void ShowMenu()
      {
        this.Show();
        this.CleanOldToolbarItems();
      }

      internal class CustomizeMenuButtonItem(string text) : MenuButtonItem(text)
      {
      }

      internal class MenuItemWithToolbarButton : MenuButtonItem
      {
        private ToolbarItemBase _button;

        public MenuItemWithToolbarButton(ToolbarItemBase button) => this._button = button;

        public ToolbarItemBase ToolBarButton => this._button;
      }
    }
}
