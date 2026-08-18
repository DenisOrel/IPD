
// Type: Intermech.Expressions.IdentifiersCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Expressions
{
    /// <summary>Contains a collection of identifiers.</summary>
    /// <remarks>This is a base collection class for specific collections in USPExpress</remarks>
    public class IdentifiersCollection : CollectionBase
    {
      private Hashtable _hashtable;

      protected internal IdentifiersCollection()
        : this((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
      {
      }

      protected internal IdentifiersCollection(IEqualityComparer comparer)
      {
        this._hashtable = new Hashtable(comparer);
      }

      protected void AddToHashtable(string name, int index)
      {
        this._hashtable.Add((object) name, (object) index);
      }

      /// <summary>
      /// Determines whether the collection contains a specific object.
      /// </summary>
      /// <param name="identifier">The identifier to locate in the collection.</param>
      /// <returns>True if the specified identifier is found in the collection; otherwise, false.</returns>
      public bool Contains(IIdentifier identifier) => this.InnerList.Contains((object) identifier);

      /// <summary>
      /// Determines the index of a specified Identifier in the collection.
      /// </summary>
      /// <param name="identifier">The Identifier to locate in the collection.</param>
      /// <returns>The zero-based index of the Identifier in the collection.</returns>
      public int IndexOf(IIdentifier identifier) => this.InnerList.IndexOf((object) identifier);

      protected override void OnClearComplete() => this._hashtable.Clear();

      /// <summary>
      /// Performs additional custom processes after inserting a new element into the collection
      /// </summary>
      /// <param name="index">The zero-based index at which to insert value.</param>
      /// <param name="value">The new value of the element at index.</param>
      protected override void OnInsertComplete(int index, object value)
      {
        base.OnInsertComplete(index, value);
        this.AddToHashtable(((IIdentifier) value).Name, index);
      }

      protected override void OnRemoveComplete(int index, object value)
      {
        base.OnRemoveComplete(index, value);
        this.RemoveFromHashtable(((IIdentifier) value).Name);
        this.RefreshIndices(index);
      }

      protected virtual void RefreshIndices(int start)
      {
        for (int index = start; index < this.Count; ++index)
          this._hashtable[(object) ((IIdentifier) this.List[index]).Name] = (object) (index - 1);
      }

      protected void RemoveFromHashtable(string name) => this._hashtable.Remove((object) name);

      protected void ReplaceInHashtable(string name, int index)
      {
        this._hashtable[(object) name] = (object) index;
      }

      /// <summary>
      /// Returns a zero-based index of the identifier with the specified name.
      /// </summary>
      /// <param name="name">Identifier name.</param>
      /// <returns>The zero-based index of the identifier in the collection. -1, if no identifier with that name found in the collection.</returns>
      public int IndexOf(string name)
      {
        object obj = this._hashtable[(object) name];
        return obj != null ? (int) obj : -1;
      }

      protected IIdentifier this[string name]
      {
        get
        {
          int index = this.IndexOf(name);
          return index != -1 ? (IIdentifier) this.List[index] : (IIdentifier) null;
        }
      }
    }
}
