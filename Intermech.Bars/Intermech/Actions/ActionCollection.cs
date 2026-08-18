
// Type: Intermech.Actions.ActionCollection
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.Actions
{
    [Editor(typeof (ActionCollectionEditor), typeof (UITypeEditor))]
    public class ActionCollection : CollectionBase
    {
      private ActionList _owner;
      private Action _null = new Action();

      public ActionCollection(ActionList owner)
      {
        this._owner = owner;
        this._null._owner = this._owner;
      }

      public ActionCollection(ActionCollection value) => this.AddRange(value);

      public ActionCollection(Action[] value) => this.AddRange(value);

      public ActionList Parent => this._owner;

      internal Action Null => this._null;

      public Action this[int index]
      {
        get => (Action) this.List[index];
        set => this.List[index] = (object) value;
      }

      public int Add(Action value) => this.List.Add((object) value);

      public void AddRange(Action[] value)
      {
        foreach (Action action in value)
          this.Add(action);
      }

      public void AddRange(ActionCollection value)
      {
        foreach (Action action in value)
          this.Add(action);
      }

      public bool Contains(Action value) => this.List.Contains((object) value);

      public void CopyTo(Action[] array, int index) => this.List.CopyTo((Array) array, index);

      public int IndexOf(Action value) => this.List.IndexOf((object) value);

      public void Insert(int index, Action value) => this.List.Insert(index, (object) value);

      public ActionCollection.ActionEnumerator GetEnumerator()
      {
        return new ActionCollection.ActionEnumerator(this);
      }

      public void Remove(Action value) => this.List.Remove((object) value);

      protected override void OnSet(int index, object oldValue, object newValue)
      {
        if (oldValue != null)
          ((Action) oldValue)._owner = (ActionList) null;
        if (newValue == null)
          return;
        ((Action) newValue)._owner = this._owner;
      }

      protected override void OnInsert(int index, object value)
      {
        if (value == null)
          return;
        ((Action) value)._owner = this._owner;
      }

      protected override void OnClear()
      {
      }

      protected override void OnRemove(int index, object value)
      {
      }

      protected override void OnValidate(object value)
      {
      }

      public class ActionEnumerator : IEnumerator
      {
        private IEnumerator _baseEnumerator;
        private IEnumerable _temp;

        public ActionEnumerator(ActionCollection mappings)
        {
          this._temp = (IEnumerable) mappings;
          this._baseEnumerator = this._temp.GetEnumerator();
        }

        public Action Current => (Action) this._baseEnumerator.Current;

        object IEnumerator.Current => this._baseEnumerator.Current;

        public bool MoveNext() => this._baseEnumerator.MoveNext();

        bool IEnumerator.MoveNext() => this._baseEnumerator.MoveNext();

        public void Reset() => this._baseEnumerator.Reset();

        void IEnumerator.Reset() => this._baseEnumerator.Reset();
      }
    }
}
