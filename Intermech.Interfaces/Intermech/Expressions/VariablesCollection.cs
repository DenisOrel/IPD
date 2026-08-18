
// Type: Intermech.Expressions.VariablesCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Expressions
{
    public class VariablesCollection : IdentifiersCollection
    {
      internal event VariablesCollection.ChangedEventHandler Changed;

      internal event VariablesCollection.AddEventHandler VariableAdd;

      internal VariablesCollection()
        : base((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
      {
      }

      public int Add(Variable variable) => this.List.Add((object) variable);

      public void AddRange(ICollection variables)
      {
        if (variables == null)
          return;
        foreach (Variable variable in (IEnumerable) variables)
        {
          try
          {
            this.Add(variable);
          }
          catch (Exception ex)
          {
          }
        }
      }

      private void OnAliasAdd(string variableName, string alias)
      {
        this.OnVariableAdd(alias);
        this.AddToHashtable(alias, this.IndexOf(variableName));
        this.OnChanged();
      }

      private void OnAliasRemove(string alias)
      {
        this.RemoveFromHashtable(alias);
        this.OnChanged();
      }

      protected override void OnInsert(int index, object value)
      {
        this.OnVariableAdd(((Variable) value).Name);
      }

      protected override void OnInsertComplete(int index, object value)
      {
        base.OnInsertComplete(index, value);
        Variable variable = (Variable) value;
        for (int index1 = 0; index1 < variable.Aliases.Count; ++index1)
        {
          this.OnVariableAdd(variable.Aliases[index1]);
          this.AddToHashtable(variable.Aliases[index1], index);
        }
        variable.AliasAdd += new Variable.AliasAddEventHandler(this.OnAliasAdd);
        variable.AliasRemove += new Variable.AliasRemoveEventHandler(this.OnAliasRemove);
        this.OnChanged();
      }

      protected override void OnRemoveComplete(int index, object value)
      {
        Variable variable = (Variable) value;
        variable.AliasAdd -= new Variable.AliasAddEventHandler(this.OnAliasAdd);
        variable.AliasRemove -= new Variable.AliasRemoveEventHandler(this.OnAliasRemove);
        for (int index1 = 0; index1 < variable.Aliases.Count; ++index1)
          this.RemoveFromHashtable(variable.Aliases[index1]);
        base.OnRemoveComplete(index, value);
        this.OnChanged();
      }

      private void OnChanged()
      {
        if (this.Changed == null)
          return;
        this.Changed();
      }

      private void OnVariableAdd(string Name)
      {
        if (this.VariableAdd == null)
          return;
        this.VariableAdd(Name);
      }

      protected override void RefreshIndices(int start)
      {
        for (int index1 = start; index1 < this.Count; ++index1)
        {
          Variable variable = this[index1];
          this.ReplaceInHashtable(variable.Name, index1 - 1);
          for (int index2 = 0; index2 < variable.Aliases.Count; ++index2)
            this.ReplaceInHashtable(variable.Aliases[index2], index1 - 1);
        }
      }

      public void Remove(Variable variable) => this.List.Remove((object) variable);

      public Variable this[int index] => (Variable) this.List[index];

      internal delegate void AddEventHandler(string Name);

      internal delegate void ChangedEventHandler();
    }
}
