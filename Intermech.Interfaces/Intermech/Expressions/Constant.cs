
// Type: Intermech.Expressions.Constant
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions
{
    /// <summary>
    /// A base abstract class for any constant.
    /// You must inherit from this class in order to add a user-defined constant to the Constants collection.
    /// </summary>
    public abstract class Constant : ITypedIdentifier, IIdentifier
    {
      /// <summary>Returns the name of the constant. Read-only.</summary>
      public abstract string Name { get; }

      public Type Type => this.Value.GetType();

      /// <summary>Returns the value of the constant. Read-only.</summary>
      public abstract object Value { get; }

      public FieldTypes FieldType => FieldTypes.ftUnknown;
    }
}
