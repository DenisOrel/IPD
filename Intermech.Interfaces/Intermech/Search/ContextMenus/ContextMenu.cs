
// Type: Intermech.Search.ContextMenus.ContextMenu
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;


namespace Intermech.Search.ContextMenus
{
    [Serializable]
    public sealed class ContextMenu : INotifyPropertyChanged, IContextMenuItemContainer, ICloneable
    {
      private ContextMenuItemCollection _items;

      public ContextMenu()
      {
        this._items = new ContextMenuItemCollection((IContextMenuItemContainer) this);
        this.Items.ListChanged += new ListChangedEventHandler(this.Items_ListChanged);
      }

      public ContextMenu Clone()
      {
        ContextMenu contextMenu = new ContextMenu();
        foreach (ContextMenuItem contextMenuItem in (Collection<ContextMenuItem>) this.Items)
          contextMenu.Items.Add(contextMenuItem.Clone());
        return contextMenu;
      }

      public IEnumerable<ContextMenuItem> GetDescendants()
      {
        foreach (ContextMenuItem item in (Collection<ContextMenuItem>) this.Items)
        {
          yield return item;
          foreach (ContextMenuItem descendant in item.GetDescendants())
            yield return descendant;
        }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public ContextMenuItemCollection Items => this._items;

      object ICloneable.Clone() => (object) this.Clone();

      [System.Runtime.Serialization.OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
        this._items.ListChanged += new ListChangedEventHandler(this.Items_ListChanged);
      }

      private void Items_ListChanged(object sender, ListChangedEventArgs e)
      {
        this.OnPropertyChanged<ContextMenuItemCollection>((Expression<Func<ContextMenuItemCollection>>) (() => this.Items));
      }

      private void OnPropertyChanged<T>(Expression<Func<T>> expression)
      {
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged == null)
          return;
        propertyChanged((object) this, new PropertyChangedEventArgs(((MemberExpression) expression.Body).Member.Name));
      }
    }
}
