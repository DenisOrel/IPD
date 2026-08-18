
// Type: Intermech.Search.ButtonBars.ButtonBarButtonCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.ComponentModel;
using System;
using System.Linq;


namespace Intermech.Search.ButtonBars
{
    [Serializable]
    public sealed class ButtonBarButtonCollection : BindingListBase<ButtonBarButton>
    {
      public ButtonBarButtonCollection(IButtonBarButtonCollectionOwner owner)
      {
        this.Owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
      }

      public IButtonBarButtonCollectionOwner Owner { get; private set; }

      protected override void ClearItems()
      {
        foreach (ButtonBarButton buttonBarButton in this.ToArray<ButtonBarButton>())
          this.Remove(buttonBarButton);
      }

      protected override void InsertItem(int index, ButtonBarButton item)
      {
        if (item.Parent == this.Owner)
          return;
        item.Parent = this.Owner;
        base.InsertItem(index, item);
      }

      protected override void RemoveItem(int index)
      {
        ButtonBarButton buttonBarButton = this[index];
        if (buttonBarButton.Parent == null)
          return;
        buttonBarButton.Parent = (IButtonBarButtonCollectionOwner) null;
        base.RemoveItem(index);
      }

      protected override void SetItem(int index, ButtonBarButton item)
      {
        if (item.Parent == this.Owner)
          return;
        item.Parent = this.Owner;
        base.SetItem(index, item);
      }
    }
}
