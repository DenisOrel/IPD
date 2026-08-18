
// Type: Intermech.Bars.ToolbarItemBaseCollection
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;


namespace Intermech.Bars
{
    public abstract class ToolbarItemBaseCollection : CollectionBase
    {
      private IButtonsSite _parent;
      private bool _rangeAdding;
      private bool _updating;

      internal ToolbarItemBaseCollection(IButtonsSite parent)
      {
        this._rangeAdding = false;
        this._updating = false;
        this._parent = parent;
      }

      public int Add(ToolbarItemBase item)
      {
        int count = this.Count;
        this.Insert(count, item);
        return count;
      }

      public void AddRange(ToolbarItemBase[] items)
      {
        this._rangeAdding = true;
        foreach (ToolbarItemBase toolbarItemBase in items)
          this.Add(toolbarItemBase);
        this._rangeAdding = false;
        this._parent.ChildItemsChanged();
      }

      public bool Contains(ToolbarItemBase item) => this.List.Contains((object) item);

      public void CopyTo(ToolbarItemBase[] array, int index) => this.List.CopyTo((Array) array, index);

      public int IndexOf(ToolbarItemBase item) => this.List.IndexOf((object) item);

      public virtual void Insert(int index, ToolbarItemBase item)
      {
        if (!this.IsComponentSuitable(item))
          throw new ArgumentException("This type of item is not suitable for adding to this parent.");
        if (item.Owner != null)
          item.Owner.Items.Remove(item);
        if (index < 0)
          index = 0;
        else if (index > this.List.Count)
          index = this.List.Count;
        this.List.Insert(index, (object) item);
      }

      internal abstract bool IsComponentSuitable(ToolbarItemBase item);

      protected override void OnClear()
      {
        base.OnClear();
        foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this)
        {
          this.SetOwner(toolbarItemBase, (object) null);
          if (toolbarItemBase is ControlContainerItem)
            this._parent.ControlHost.Controls.Remove(((ControlContainerItem) toolbarItemBase).ContainedControl);
        }
      }

      protected override void OnClearComplete()
      {
        base.OnClearComplete();
        this._parent.ChildItemsChanged();
      }

      protected override void OnInsertComplete(int index, object value)
      {
        if (this._updating)
          return;
        base.OnInsertComplete(index, value);
        ToolbarItemBase toolbarItemBase = (ToolbarItemBase) value;
        this.SetOwner(toolbarItemBase, (object) this._parent);
        if (toolbarItemBase is ControlContainerItem)
        {
          this._parent.ControlHost.Controls.Add(((ControlContainerItem) toolbarItemBase).ContainedControl);
          toolbarItemBase.Enabled = toolbarItemBase.Enabled;
        }
        if (this._rangeAdding)
          return;
        this._parent.ChildItemsChanged();
      }

      protected override void OnRemoveComplete(int index, object value)
      {
        if (this._updating)
          return;
        base.OnRemoveComplete(index, value);
        ToolbarItemBase toolbarItemBase = (ToolbarItemBase) value;
        this.SetOwner(toolbarItemBase, (object) null);
        if (toolbarItemBase is ControlContainerItem)
          this._parent.ControlHost.Controls.Remove(((ControlContainerItem) toolbarItemBase).ContainedControl);
        if (this._rangeAdding)
          return;
        this._parent.ChildItemsChanged();
      }

      public void Remove(ToolbarItemBase item) => this.List.Remove((object) item);

      internal void Move(ToolbarItemBase item, int newIndex)
      {
        this._updating = true;
        if (!this.List.Contains((object) item))
          return;
        try
        {
          this.List.Remove((object) item);
          if (newIndex < 0)
            newIndex = 0;
          if (newIndex > this.List.Count)
            newIndex = this.List.Count;
          this.List.Insert(newIndex, (object) item);
        }
        finally
        {
          this._updating = false;
          this._parent.ChildItemsChanged();
        }
      }

      internal abstract void SetOwner(ToolbarItemBase item, object owner);

      public ToolbarItemBase this[int index] => (ToolbarItemBase) this.List[index];
    }
}
