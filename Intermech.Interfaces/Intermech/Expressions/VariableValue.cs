
// Type: Intermech.Expressions.VariableValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Expressions
{
    /// <summary>Contains information about a variable and its value.</summary>
    /// <remarks>
    /// Allows one to specify a value of the variable to be used in evaluation.
    /// See also (see cref="Intermech.Expressions.ExpressionTree.GetUsedVariables"/) and (see cref="Intermech.Expressions.ExpressionTree.Evaluate(Intermech.Expressions.VariableValuesCollection)"/) methods.
    /// </remarks>
    [DebuggerDisplay("{_variable.Name}= {_value}")]
    public class VariableValue : ICloneable, IVariable, ITypedIdentifier, IIdentifier
    {
      private int _index;
      private object _value;
      private Variable _variable;

      internal VariableValue(int index, Variable variable)
      {
        this._index = index;
        this._value = (object) null;
        this._variable = variable;
      }

      /// <summary>Creates a shallow copy of the object.</summary>
      /// <returns>A shallow copy of the object.</returns>
      public object Clone() => (object) new VariableValue(this._index, this._variable);

      /// <summary>Variable Aliases.</summary>
      public AliasesCollection Aliases => this._variable.Aliases;

      /// <summary>Index of the variable in the <see cref="T:Intermech.Expressions.VariablesCollection" /> collection</summary>
      internal int Index => this._index;

      /// <summary>Name of the variable.</summary>
      public string Name => this._variable.Name;

      public Type Type => this._variable.Type;

      public FieldTypes FieldType => this._variable.FieldType;

      /// <summary>Value of the variable.</summary>
      public object Value
      {
        get => this._value;
        set => this._value = value;
      }
    }
}
