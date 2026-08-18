
// Type: Intermech.Expressions.VariableValuesCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Expressions
{
    /// <summary>
    /// Contains a collection of <see cref="T:Intermech.Expressions.VariableValue" /> objects.
    /// </summary>
    public class VariableValuesCollection : ReadOnlyCollectionBase, ICloneable
    {
      private Hashtable _hashtable;

      internal VariableValuesCollection(IList list)
      {
        this._hashtable = new Hashtable(list.Count);
        foreach (VariableValue variableValue in (IEnumerable) list)
        {
          this._hashtable.Add((object) variableValue.Name, (object) variableValue);
          foreach (string alias in (CollectionBase) variableValue.Aliases)
            this._hashtable.Add((object) alias, (object) variableValue);
          this.InnerList.Add((object) variableValue);
        }
      }

      protected internal VariableValuesCollection Clone()
      {
        ArrayList arrayList = new ArrayList();
        foreach (VariableValue variableValue in (ReadOnlyCollectionBase) this)
          arrayList.Add(variableValue.Clone());
        return new VariableValuesCollection((IList) arrayList);
      }

      /// <summary>
      /// Determines whether the collection contains a specific <see cref="T:Intermech.Expressions.VariableValue" /> object.
      /// </summary>
      /// <param name="var">The variable to locate in the collection.</param>
      /// <returns>True if the specified variable is found in the collection; otherwise, false.</returns>
      public bool Contains(VariableValue var) => this.InnerList.Contains((object) var);

      /// <summary>
      /// Determines the index of a specified VariableValue in the collection.
      /// </summary>
      /// <param name="value">The VariableValue to locate in the collection.</param>
      /// <returns>The zero-based index of the VariableValue in the collection.</returns>
      public int IndexOf(VariableValue value) => this.InnerList.IndexOf((object) value);

      object ICloneable.Clone() => (object) this.Clone();

      /// <summary>
      /// Gets the variable associated with the specified name or alias.
      /// If the specified name is not found, attempting to get it returns a null reference.
      /// </summary>
      public VariableValue this[string name] => (VariableValue) this._hashtable[(object) name];

      /// <summary>Gets the variable at the specified index.</summary>
      public VariableValue this[int index] => (VariableValue) this.InnerList[index];
    }
}
