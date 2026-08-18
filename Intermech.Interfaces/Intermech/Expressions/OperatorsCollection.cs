
// Type: Intermech.Expressions.OperatorsCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Expressions
{
    /// <summary>Collection of operators.</summary>
    public class OperatorsCollection : ReadOnlyCollectionBase
    {
      internal OperatorsCollection()
      {
      }

      internal OperatorsCollection(IList list) => this.InnerList.AddRange((ICollection) list);

      /// <summary>
      /// Determines whether the collection contains a specific <see cref="T:Intermech.Expressions.Operator" /> object.
      /// </summary>
      /// <param name="oper">The operator to locate in the collection.</param>
      /// <returns>True if the specified Operator is found in the collection; otherwise, false.</returns>
      public bool Contains(Operator oper) => this.InnerList.Contains((object) oper);

      /// <summary>
      /// Determines the index of a specified Operator in the collection.
      /// </summary>
      /// <param name="oper">The Operator to locate in the collection.</param>
      /// <returns>The zero-based index of the Operator in the collection.</returns>
      public int IndexOf(Operator oper) => this.InnerList.IndexOf((object) oper);

      /// <summary>Get the operator at the specified index.</summary>
      public Operator this[int index] => (Operator) this.InnerList[index];
    }
}
