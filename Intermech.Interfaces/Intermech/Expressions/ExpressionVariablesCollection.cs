
// Type: Intermech.Expressions.ExpressionVariablesCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Expressions
{
    /// <summary>
    /// Stores information on variables in an expression tree.
    /// </summary>
    public class ExpressionVariablesCollection : ReadOnlyCollectionBase
    {
      internal ExpressionVariablesCollection()
      {
      }

      internal ExpressionVariablesCollection(IList list) => this.InnerList.AddRange((ICollection) list);

      /// <summary>
      /// Determines whether the collection contains a specific <see cref="T:Intermech.Expressions.Variable" /> object.
      /// </summary>
      /// <param name="variable">The variable to locate in the collection.</param>
      /// <returns>True if the specified Variable is found in the collection; otherwise, false.</returns>
      public bool Contains(Variable variable) => this.InnerList.Contains((object) variable);

      /// <summary>
      /// Determines the index of a specific Variable in the collection.
      /// </summary>
      /// <param name="variable">The Variable to locate in the collection.</param>
      /// <returns>The zero-based index of the Variable in the collection.</returns>
      public int IndexOf(Variable variable) => this.InnerList.IndexOf((object) variable);

      /// <summary>Get the variable at the specified index.</summary>
      public Variable this[int index] => (Variable) this.InnerList[index];
    }
}
