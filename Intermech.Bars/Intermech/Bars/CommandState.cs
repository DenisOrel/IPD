
// Type: Intermech.Bars.CommandState
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Collections;
using System.Diagnostics;


namespace Intermech.Bars
{
    internal class CommandState : ICommandState
    {
      private string _commandName;
      private ArrayList _items = new ArrayList();
      private ButtonItemBase _activeItem;
      private object _sender;

      public CommandState(string commandName) => this._commandName = commandName;

      internal void AddItem(ButtonItemBase item) => this._items.Add((object) item);

      internal void RemoveItem(ButtonItemBase item)
      {
        this._items.Remove((object) item);
        if (this._activeItem != item)
          return;
        if (this._items.Count > 0)
          this._activeItem = this._items[0] as ButtonItemBase;
        else
          this._activeItem = (ButtonItemBase) null;
      }

      internal ButtonItemBase ActiveItem
      {
        [DebuggerStepThrough] get => this._activeItem;
        set => this._activeItem = value;
      }

      internal bool CountainsItem(ButtonItemBase item) => this._items.Contains((object) item);

      internal IEnumerable Items
      {
        [DebuggerStepThrough] get => (IEnumerable) this._items;
      }

      public string CommandName
      {
        [DebuggerStepThrough] get => this._commandName;
        set => this._commandName = value;
      }

      public bool Visible
      {
        set
        {
          foreach (ToolbarItemBase toolbarItemBase in this._items)
            toolbarItemBase.Visible = value;
        }
        get => this.ActiveItem != null && this.ActiveItem.Visible;
      }

      public bool Enabled
      {
        set
        {
          foreach (ToolbarItemBase toolbarItemBase in this._items)
            toolbarItemBase.Enabled = value;
          if (this.ActiveItem == null)
            return;
          this.ActiveItem.Enabled = value;
        }
        get => this.ActiveItem != null && this.ActiveItem.Enabled;
      }

      public bool Checked
      {
        set
        {
          foreach (ButtonItemBase buttonItemBase in this._items)
            buttonItemBase.Checked = value;
        }
        [DebuggerStepThrough] get => this.ActiveItem.Checked;
      }

      public string Text
      {
        set
        {
          foreach (ToolbarItemBase toolbarItemBase in this._items)
            toolbarItemBase.Text = value;
        }
        [DebuggerStepThrough] get => this.ActiveItem.Text;
      }

      public int ImageIndex
      {
        set
        {
          foreach (ButtonItemBase buttonItemBase in this._items)
            buttonItemBase.ImageIndex = value;
        }
        [DebuggerStepThrough] get => this.ActiveItem.ImageIndex;
      }

      public string ToolTipText
      {
        set
        {
          foreach (ToolbarItemBase toolbarItemBase in this._items)
            toolbarItemBase.ToolTipText = value;
        }
        [DebuggerStepThrough] get => this.ActiveItem.ToolTipText;
      }

      public object Tag
      {
        set
        {
          foreach (ToolbarItemBase toolbarItemBase in this._items)
            toolbarItemBase.Tag = value;
        }
        [DebuggerStepThrough] get => this.ActiveItem.Tag;
      }

      public object Sender
      {
        [DebuggerStepThrough] get => this._sender;
        set => this._sender = value;
      }
    }
}
