
// Type: Intermech.Expressions.Variable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions
{
    /// <summary>Represents Variable.</summary>
    public class Variable : IVariable, ITypedIdentifier, IIdentifier
    {
      private AliasesCollection _aliases;
      private readonly Type _type;
      private readonly FieldTypes _fieldType;
      private string _name;

      internal event Variable.AliasAddEventHandler AliasAdd;

      internal event Variable.AliasRemoveEventHandler AliasRemove;

      public Variable(string name, Type type)
        : this(name, type, FieldTypes.ftUnknown)
      {
      }

      public Variable(string name, Type type, FieldTypes fieldType)
      {
        this._aliases = new AliasesCollection();
        this._aliases.AliasAdd += new AliasesCollection.AddEventHandler(this.OnAliasAdd);
        this._aliases.AliasRemove += new AliasesCollection.RemoveEventHandler(this.OnAliasRemove);
        this._type = type;
        this._name = name;
        this._fieldType = fieldType;
      }

      /// <summary>Destructor</summary>
      ~Variable()
      {
        this._aliases.AliasAdd -= new AliasesCollection.AddEventHandler(this.OnAliasAdd);
        this._aliases.AliasRemove -= new AliasesCollection.RemoveEventHandler(this.OnAliasRemove);
      }

      /// <summary>Creates a shallow copy of the current variable.</summary>
      /// <returns>A shallow copy of the current variable.</returns>
      internal Variable Clone() => (Variable) this.MemberwiseClone();

      /// <summary>Raises AliasAdd event.</summary>
      /// <param name="Alias">New alias.</param>
      private void OnAliasAdd(string Alias)
      {
        if (this.AliasAdd == null)
          return;
        this.AliasAdd(this.Name, Alias);
      }

      /// <summary>Raises AliasRemove event.</summary>
      /// <param name="Alias">Removed alias</param>
      private void OnAliasRemove(string Alias)
      {
        if (this.AliasRemove == null)
          return;
        this.AliasRemove(Alias);
      }

      /// <summary>Variable Aliases.</summary>
      /// <remarks>
      /// Each variable can have one or more aliases. In expressions, aliases can be used
      /// interchangeably with actual name of the variable.
      /// </remarks>
      public AliasesCollection Aliases => this._aliases;

      /// <summary>Name of the variable. Read-only.</summary>
      public string Name => this._name;

      public void ResetName(string name) => this._name = name;

      public Type Type => this._type;

      public FieldTypes FieldType => this._fieldType;

      public override string ToString() => this.Name;

      internal delegate void AliasAddEventHandler(string VariableName, string Alias);

      internal delegate void AliasRemoveEventHandler(string Alias);
    }
}
