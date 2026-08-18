
// Type: Intermech.Expressions.AliasesCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Expressions
{
    public class AliasesCollection : CollectionBase
    {
      internal event AliasesCollection.AddEventHandler AliasAdd;

      internal event AliasesCollection.RemoveEventHandler AliasRemove;

      public AliasesCollection()
      {
      }

      public AliasesCollection(IList sourceList) => this.InnerList.AddRange((ICollection) sourceList);

      public int Add(string value) => this.List.Add((object) value);

      public bool Contains(string alias) => this.List.Contains((object) alias);

      public int IndexOf(string alias) => this.List.IndexOf((object) alias);

      protected override void OnInsertComplete(int index, object value)
      {
        this.RaiseAliasAddEvent((string) value);
      }

      protected override void OnRemoveComplete(int index, object value)
      {
        this.RaiseAliasRemoveEvent((string) value);
      }

      private void RaiseAliasAddEvent(string Alias)
      {
        if (this.AliasAdd == null)
          return;
        this.AliasAdd(Alias);
      }

      private void RaiseAliasRemoveEvent(string Alias)
      {
        if (this.AliasRemove == null)
          return;
        this.AliasRemove(Alias);
      }

      public void Remove(string value) => this.List.Remove((object) value);

      public string this[int index] => (string) this.List[index];

      internal delegate void AddEventHandler(string Alias);

      internal delegate void RemoveEventHandler(string Alias);
    }
}
