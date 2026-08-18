
// Type: Intermech.Search.EditingContexts.EditingContextItemCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.ComponentModel;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace Intermech.Search.EditingContexts
{
    [Serializable]
    public sealed class EditingContextItemCollection : BindingListBase<EditingContextItem>
    {
      public EditingContextItemCollection(EditingContext owner)
      {
        this.Owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
      }

      public EditingContext Owner { get; private set; }

      public void Sort(int sortAttributeTypeID, ListSortDirection listSortDirection)
      {
        if (AttributeTypeHelper.IsUnknownAttributeTypeID(sortAttributeTypeID))
          throw new ArgumentException();
        EditingContextItem[] items = listSortDirection != ListSortDirection.Ascending ? this.OrderByDescending<EditingContextItem, object>((Func<EditingContextItem, object>) (o => o.Object.Attributes.GetAttributeValue(sortAttributeTypeID))).ToArray<EditingContextItem>() : this.OrderBy<EditingContextItem, object>((Func<EditingContextItem, object>) (o => o.Object.Attributes.GetAttributeValue(sortAttributeTypeID))).ToArray<EditingContextItem>();
        bool listChangedEvents = this.RaiseListChangedEvents;
        this.RaiseListChangedEvents = false;
        try
        {
          this.Clear();
          this.AddRange((IEnumerable<EditingContextItem>) items);
        }
        finally
        {
          this.RaiseListChangedEvents = listChangedEvents;
          this.ResetBindings();
        }
      }

      protected override void ClearItems()
      {
        foreach (EditingContextItem editingContextItem in this.ToArray<EditingContextItem>())
          this.Remove(editingContextItem);
      }

      protected override void InsertItem(int index, EditingContextItem item)
      {
        if (item.EditingContext == this.Owner)
          return;
        item.EditingContext = this.Owner;
        base.InsertItem(index, item);
      }

      protected override void RemoveItem(int index)
      {
        EditingContextItem editingContextItem = this[index];
        if (editingContextItem.EditingContext == null)
          return;
        editingContextItem.EditingContext = (EditingContext) null;
        base.RemoveItem(index);
      }

      protected override void SetItem(int index, EditingContextItem item)
      {
        if (item.EditingContext == this.Owner)
          return;
        item.EditingContext = this.Owner;
        base.SetItem(index, item);
      }
    }
}
