
// Type: Intermech.Search.ContextMenus.ContextMenuItemCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.ComponentModel;
using System;
using System.Linq;


namespace Intermech.Search.ContextMenus
{
    [Serializable]
    public sealed class ContextMenuItemCollection : BindingListBase<ContextMenuItem>
    {
      public ContextMenuItemCollection(IContextMenuItemContainer owner) => this.Owner = owner;

      public IContextMenuItemContainer Owner { get; private set; }

      protected override void ClearItems()
      {
        foreach (ContextMenuItem contextMenuItem in this.ToArray<ContextMenuItem>())
          this.Remove(contextMenuItem);
      }

      protected override void InsertItem(int index, ContextMenuItem item)
      {
        if (item.Parent == this.Owner)
          return;
        item.Parent = this.Owner;
        base.InsertItem(index, item);
      }

      protected override void RemoveItem(int index)
      {
        ContextMenuItem contextMenuItem = this[index];
        if (contextMenuItem.Parent == null)
          return;
        contextMenuItem.Parent = (IContextMenuItemContainer) null;
        base.RemoveItem(index);
      }

      protected override void SetItem(int index, ContextMenuItem item)
      {
        if (item.Parent == this.Owner)
          return;
        item.Parent = this.Owner;
        base.SetItem(index, item);
      }
    }
}
