
// Type: Intermech.Expressions.VariableEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions
{
    public class VariableEventArgs : EventArgs
    {
      private Variable _variable;
      private string _name;

      public VariableEventArgs(string name, Variable var)
      {
        this._variable = var;
        this._name = name;
      }

      /// <summary>Переменная, созданная по умолчанию с типом double</summary>
      public Variable Variable
      {
        get => this._variable;
        set => this._variable = value;
      }

      /// <summary>Имя переменной</summary>
      public string Name => this._name;
    }
}
