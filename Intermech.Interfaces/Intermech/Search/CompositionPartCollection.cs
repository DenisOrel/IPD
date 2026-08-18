
// Type: Intermech.Search.CompositionPartCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.ComponentModel;
using System;
using System.Linq;


namespace Intermech.Search
{
    [Serializable]
    public sealed class CompositionPartCollection : BindingListBase<CompositionPart>
    {
      public CompositionPartCollection(_Object owner)
      {
        this.Owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
      }

      public _Object Owner { get; private set; }

      protected override void ClearItems()
      {
        foreach (CompositionPart compositionPart in this.ToArray<CompositionPart>())
          compositionPart.Parent = (_Object) null;
        base.ClearItems();
      }

      protected override void InsertItem(int index, CompositionPart item)
      {
        if (item.Parent == this.Owner)
          return;
        item.Parent = this.Owner;
        base.InsertItem(index, item);
      }

      protected override void SetItem(int index, CompositionPart item)
      {
        if (item.Parent == this.Owner)
          return;
        item.Parent = this.Owner;
        base.SetItem(index, item);
      }

      protected override void RemoveItem(int index)
      {
        if (index < 0 || index >= this.Count || this[index] == null)
          return;
        this[index].Parent = (_Object) null;
        base.RemoveItem(index);
      }
    }
}
