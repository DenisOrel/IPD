
// Type: IMClient.NavigateManager




using Intermech.Bars;
using Intermech.Interfaces.Client;
using System;


namespace IMClient
{
    public class NavigateManager : INavigateManager, ICommandTarget
    {
      private INavigate _navigator;
      private ICommandState _backCommand;
      private ICommandState _forwardCommand;
      private DropDownMenuItem _backItem;
      private DropDownMenuItem _forwardItem;

      public NavigateManager(
        ICommandManager commandManager,
        DropDownMenuItem backItem,
        DropDownMenuItem forwardItem)
      {
        this._navigator = (INavigate) null;
        this._backCommand = commandManager.FindCommand("NavigateBack");
        this._forwardCommand = commandManager.FindCommand("NavigateForward");
        this._backItem = backItem;
        this._forwardItem = forwardItem;
        this._backItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.BackItem_BeforePopup);
        this._forwardItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ForwardItem_BeforePopup);
      }

      private void Navigator_Changed(object sender, EventArgs e)
      {
        if (this._navigator == null)
        {
          this._backCommand.Enabled = false;
          this._backCommand.ToolTipText = LocalizationHolder.rm.GetString("IMClient_6");
          this._forwardCommand.Enabled = false;
          this._forwardCommand.ToolTipText = LocalizationHolder.rm.GetString("IMClient_7");
        }
        else
        {
          this._backCommand.Enabled = this._navigator.CanBack;
          this._backCommand.ToolTipText = this._navigator.BackName;
          this._forwardCommand.Enabled = this._navigator.CanForward;
          this._forwardCommand.ToolTipText = this._navigator.ForwardName;
        }
      }

      public void Attach(INavigate navigate)
      {
        if (this._navigator != null)
          this._navigator.Changed -= new EventHandler(this.Navigator_Changed);
        this._navigator = navigate;
        if (this._navigator != null)
          this._navigator.Changed += new EventHandler(this.Navigator_Changed);
        this.Navigator_Changed((object) this._navigator, (EventArgs) null);
      }

      public bool Execute(ICommandState commandState)
      {
        if (commandState.CommandName == "NavigateBack")
        {
          if (this._navigator != null && this._navigator.CanBack)
            this._navigator.Back();
          return true;
        }
        if (!(commandState.CommandName == "NavigateForward"))
          return false;
        if (this._navigator != null && this._navigator.CanForward)
          this._navigator.Forward();
        return true;
      }

      public bool QueryStatus(ICommandState commandState)
      {
        if (!(commandState.CommandName == "NavigateBack") && !(commandState.CommandName == "NavigateForward"))
          return false;
        this.Navigator_Changed((object) this._navigator, (EventArgs) null);
        return true;
      }

      private void BackItem_BeforePopup(object sender, MenuPopupEventArgs e)
      {
        if (this._navigator == null)
          return;
        string[] backNames = this._navigator.BackNames;
        this._backItem.DisposeChildren();
        if (backNames == null)
          return;
        for (int index = 0; index < backNames.Length; ++index)
        {
          MenuButtonItem menuButtonItem = new MenuButtonItem(backNames[index], new EventHandler(this.BackMenuItemClick));
          menuButtonItem.Tag = (object) (index + 1);
          this._backItem.Items.Add((ToolbarItemBase) menuButtonItem);
        }
      }

      private void BackMenuItemClick(object sender, EventArgs e)
      {
        if (!(sender is MenuButtonItem menuButtonItem) || this._navigator == null)
          return;
        this._navigator.Back((int) menuButtonItem.Tag);
      }

      private void ForwardMenuItemClick(object sender, EventArgs e)
      {
        if (!(sender is MenuButtonItem menuButtonItem) || this._navigator == null)
          return;
        this._navigator.Forward((int) menuButtonItem.Tag);
      }

      private void ForwardItem_BeforePopup(object sender, MenuPopupEventArgs e)
      {
        if (this._navigator == null)
          return;
        string[] forwardNames = this._navigator.ForwardNames;
        this._forwardItem.DisposeChildren();
        if (forwardNames == null)
          return;
        for (int index = 0; index < forwardNames.Length && forwardNames[index] != null; ++index)
        {
          MenuButtonItem menuButtonItem = new MenuButtonItem(forwardNames[index], new EventHandler(this.ForwardMenuItemClick));
          menuButtonItem.Tag = (object) (index + 1);
          this._forwardItem.Items.Add((ToolbarItemBase) menuButtonItem);
        }
      }
    }
}
