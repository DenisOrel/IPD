
// Type: IMClient.UINotificationsItemsCollection




using System.Collections.ObjectModel;


namespace IMClient
{
    internal sealed class UINotificationsItemsCollection : ObservableCollection<UINotificationsItemVM>
    {
      private readonly UINotificationsVM parent;

      internal UINotificationsItemsCollection(UINotificationsVM parent) => this.parent = parent;

      protected override void InsertItem(int index, UINotificationsItemVM item)
      {
        base.InsertItem(index, item);
        item.SetParent(this.parent);
      }

      protected override void SetItem(int index, UINotificationsItemVM item)
      {
        UINotificationsItemVM notificationsItemVm = this.Items[index];
        base.SetItem(index, item);
        notificationsItemVm.SetParent((UINotificationsVM) null);
        item.SetParent(this.parent);
      }

      protected override void RemoveItem(int index)
      {
        UINotificationsItemVM notificationsItemVm = this.Items[index];
        base.RemoveItem(index);
        notificationsItemVm.SetParent((UINotificationsVM) null);
      }

      protected override void ClearItems()
      {
        for (int index = 0; index < this.Items.Count; ++index)
          this.Items[index].SetParent((UINotificationsVM) null);
        base.ClearItems();
      }
    }
}
