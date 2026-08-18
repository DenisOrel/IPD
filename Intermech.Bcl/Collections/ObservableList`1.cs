
// Type: Intermech.Collections.ObservableList`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;


namespace Intermech.Collections
{
    [Serializable]
    public class ObservableList<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
      private readonly SimpleMonitor _monitor;
      private const string CountString = "Count";
      private const string IndexerName = "Item[]";

      public ObservableList(IList<T> list)
        : base(list)
      {
        this._monitor = new SimpleMonitor();
      }

      protected IDisposable BlockReentrancy()
      {
        this._monitor.Enter();
        return (IDisposable) this._monitor;
      }

      protected void CheckReentrancy()
      {
        if (this._monitor.Busy && this.CollectionChanged != null && this.CollectionChanged.GetInvocationList().Length > 1)
          throw new InvalidOperationException("The observable list doesn't allow reentrancy.");
      }

      protected override void ClearItems()
      {
        this.CheckReentrancy();
        base.ClearItems();
        this.OnPropertyChanged("Count");
        this.OnPropertyChanged("Item[]");
        this.OnCollectionReset();
      }

      protected override void InsertItem(int index, T item)
      {
        this.CheckReentrancy();
        base.InsertItem(index, item);
        this.OnPropertyChanged("Count");
        this.OnPropertyChanged("Item[]");
        this.OnCollectionChanged(NotifyCollectionChangedAction.Add, (object) item, index);
      }

      public void Move(int oldIndex, int newIndex) => this.MoveItem(oldIndex, newIndex);

      protected virtual void MoveItem(int oldIndex, int newIndex)
      {
        this.CheckReentrancy();
        T obj = this[oldIndex];
        base.RemoveItem(oldIndex);
        base.InsertItem(newIndex, obj);
        this.OnPropertyChanged("Item[]");
        this.OnCollectionChanged(NotifyCollectionChangedAction.Move, (object) obj, newIndex, oldIndex);
      }

      protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
      {
        if (this.CollectionChanged == null)
          return;
        using (this.BlockReentrancy())
          this.CollectionChanged((object) this, e);
      }

      private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
      {
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
      }

      private void OnCollectionChanged(
        NotifyCollectionChangedAction action,
        object item,
        int index,
        int oldIndex)
      {
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
      }

      private void OnCollectionChanged(
        NotifyCollectionChangedAction action,
        object oldItem,
        object newItem,
        int index)
      {
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
      }

      private void OnCollectionReset()
      {
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      }

      protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
      {
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, e);
      }

      private void OnPropertyChanged(string propertyName)
      {
        this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
      }

      protected override void RemoveItem(int index)
      {
        this.CheckReentrancy();
        T obj = this[index];
        base.RemoveItem(index);
        this.OnPropertyChanged("Count");
        this.OnPropertyChanged("Item[]");
        this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, (object) obj, index);
      }

      protected override void SetItem(int index, T item)
      {
        this.CheckReentrancy();
        T oldItem = this[index];
        base.SetItem(index, item);
        this.OnPropertyChanged("Item[]");
        this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, (object) oldItem, (object) item, index);
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public event NotifyCollectionChangedEventHandler CollectionChanged;

      [Serializable]
      private sealed class SimpleMonitor : IDisposable
      {
        private int _busyCount;

        public void Dispose() => --this._busyCount;

        public void Enter() => ++this._busyCount;

        public bool Busy => this._busyCount > 0;
      }
    }
}
