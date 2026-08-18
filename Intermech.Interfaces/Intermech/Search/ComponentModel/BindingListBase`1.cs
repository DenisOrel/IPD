
// Type: Intermech.Search.ComponentModel.BindingListBase`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;


namespace Intermech.Search.ComponentModel
{
    [Serializable]
    public class BindingListBase<T> : BindingList<T>
    {
      public void AddRange(IEnumerable<T> items)
      {
        T[] array = items.ToArray<T>();
        if (array.Length >= this.Count / 2)
        {
          bool listChangedEvents = this.RaiseListChangedEvents;
          this.RaiseListChangedEvents = false;
          try
          {
            foreach (T obj in array)
              this.Add(obj);
          }
          finally
          {
            this.RaiseListChangedEvents = listChangedEvents;
            this.ResetBindings();
          }
        }
        else
        {
          foreach (T obj in array)
            this.Add(obj);
        }
      }

      public bool CanMoveTop(T item) => this.IndexOf(item) > 0;

      public bool CanMoveUp(T item) => this.IndexOf(item) > 0;

      public bool CanMoveDown(T item) => this.IndexOf(item) < this.Count - 1;

      public bool CanMoveBottom(T item) => this.IndexOf(item) < this.Count - 1;

      public void MoveTop(T item)
      {
        bool listChangedEvents = this.RaiseListChangedEvents;
        this.RaiseListChangedEvents = false;
        try
        {
          if (!this.CanMoveTop(item))
            return;
          this.Remove(item);
          this.Insert(0, item);
        }
        finally
        {
          this.RaiseListChangedEvents = listChangedEvents;
          this.ResetBindings();
        }
      }

      public void MoveUp(T item)
      {
        bool listChangedEvents = this.RaiseListChangedEvents;
        this.RaiseListChangedEvents = false;
        try
        {
          int num = this.IndexOf(item);
          if (num <= 0)
            return;
          this.Remove(item);
          this.Insert(num - 1, item);
        }
        finally
        {
          this.RaiseListChangedEvents = listChangedEvents;
          this.ResetBindings();
        }
      }

      public void MoveDown(T item)
      {
        bool listChangedEvents = this.RaiseListChangedEvents;
        this.RaiseListChangedEvents = false;
        try
        {
          int num = this.IndexOf(item);
          if (num >= this.Count - 1)
            return;
          this.Remove(item);
          this.Insert(num + 1, item);
        }
        finally
        {
          this.RaiseListChangedEvents = listChangedEvents;
          this.ResetBindings();
        }
      }

      public void MoveBottom(T item)
      {
        bool listChangedEvents = this.RaiseListChangedEvents;
        this.RaiseListChangedEvents = false;
        try
        {
          if (!this.CanMoveBottom(item))
            return;
          this.Remove(item);
          this.Add(item);
        }
        finally
        {
          this.RaiseListChangedEvents = listChangedEvents;
          this.ResetBindings();
        }
      }

      [System.Runtime.Serialization.OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
        bool listChangedEvents = this.RaiseListChangedEvents;
        this.RaiseListChangedEvents = false;
        try
        {
          T[] array = this.Items.ToArray<T>();
          this.ClearItems();
          foreach (T obj in array)
            this.Add(obj);
        }
        finally
        {
          this.RaiseListChangedEvents = listChangedEvents;
        }
      }
    }
}
