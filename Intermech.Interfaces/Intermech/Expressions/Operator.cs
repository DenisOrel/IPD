
// Type: Intermech.Expressions.Operator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions
{
    /// <summary>A base abstract class for any operator.</summary>
    public abstract class Operator : IIdentifier
    {
      internal static short[] kPriorityArray = new short[31 /*0x1F*/]
      {
        (short) 0,
        (short) 14,
        (short) 14,
        (short) 10,
        (short) 10,
        (short) 11,
        (short) 11,
        (short) 11,
        (short) 12,
        (short) 9,
        (short) 9,
        (short) 8,
        (short) 8,
        (short) 8,
        (short) 8,
        (short) 9,
        (short) 9,
        (short) 4,
        (short) 3,
        (short) 13,
        (short) 4,
        (short) 3,
        (short) 13,
        (short) 7,
        (short) 5,
        (short) 6,
        (short) 13,
        (short) 11,
        (short) 11,
        (short) 2,
        (short) 1
      };

      /// <overloads>Returns a value calculated by the operator.</overloads>
      /// <summary>
      /// Returns a value calculated by the operator. Case-insensitive string comparison is enabled.
      /// </summary>
      /// <param name="values">Array of input values.</param>
      /// <returns>Calculated value.</returns>
      /// <remarks>If you do not want to support IsCaseSensitive parameter you must override this method; otherwise you must override <c>Evaluate(object[] Values, bool IsCaseSensitive)</c> method.</remarks>
      public virtual object Evaluate(object[] values) => this.Evaluate(values, false);

      /// <summary>Returns a value calculated by the operator.</summary>
      /// <param name="values">Array of values.</param>
      /// <param name="isCaseSensitive">Value indicating a case-sensitive or insensitive string comparison.</param>
      /// <returns>Calculated value.</returns>
      /// <remarks>If you want to support IsCaseSensitive parameter you must override this method; otherwise you must override <c>Evaluate(object[] Values)</c> method.</remarks>
      public virtual object Evaluate(object[] values, bool isCaseSensitive) => this.Evaluate(values);

      /// <summary>Returns operator type.</summary>
      internal abstract OperatorType GetOperatorType();

      /// <summary>Returns operator priority.</summary>
      public virtual int GetPriority()
      {
        return (int) Operator.kPriorityArray[(int) (this.GetOperatorType() + 1)];
      }

      /// <summary>
      /// Returns a type of the return value depending on types of operands. Read-only.
      /// </summary>
      /// <param name="types">Types of operands.</param>
      /// <returns>Type of the return value.</returns>
      /// <remarks>Default implementation returns System.Double. If you want to return value of another type you must override this method.</remarks>
      public virtual Type GetReturnType(Type[] types) => typeof (double);

      /// <summary>
      /// Returns a value indicating whether the operator supports the specified type of an operand.
      /// </summary>
      /// <param name="index">Operand index.</param>
      /// <param name="type">Operand type.</param>
      /// <returns>True, if the operator supports the specified Type of an operand with the specified Index; otherwise, false.</returns>
      /// <remarks>Default implementation returns true if operand type is System.Double. If you want to support operands of another type you must override this method.</remarks>
      protected virtual bool InputTypeSupported(Type type, int index)
      {
        return ExpTypeConverter.CanConvert(type, typeof (double)) || ExpTypeConverter.CanConvert(type, typeof (long));
      }

      /// <summary>
      /// Determines if the operator returns <c>DBNull</c> when one of its input parameters is <c>DBNull</c>.
      /// </summary>
      /// <param name="values">Array of input values.</param>
      /// <returns><c>true</c>, if the operator returns <c>DBNull</c>, otherwise <c>false</c>.</returns>
      /// <remarks>
      /// <para>
      /// Default implementation returns <c>DBNull</c> if at least one of the operator's input arguments is <c>DBNull</c>.
      /// </para>
      /// <para>
      /// You should override this method only if you would like to implement different <c>DBNull</c> processing logic.
      /// For example, the following built-in USPExpress operators override this method:
      /// <list type="bullet">
      /// <item>
      /// Logical operators: AND, OR
      /// </item>
      /// </list>
      /// </para>
      /// </remarks>
      public virtual bool IsNullable(object[] values)
      {
        bool flag = false;
        int length = values.Length;
        for (int index = 0; index < length; ++index)
        {
          if (Convert.IsDBNull(values[index]))
          {
            flag = true;
            break;
          }
        }
        return flag;
      }

      /// <summary>
      /// Returns a value indicating whether the operator supports specified operands Types.
      /// </summary>
      /// <param name="types">Types of operands.</param>
      /// <param name="invalidArgument">Invalid operand index.</param>
      /// <returns>True, if the operator supports specified Types of operands; otherwise, false.</returns>
      /// <remarks>Default implementation calls <see cref="M:Intermech.Expressions.Operator.InputTypeSupported(System.Type,System.Int32)" /> method iteratively for each of the input parameters. If the types of your input parameters depend on each other, you must override this method.</remarks>
      public virtual bool Validate(Type[] types, ref int invalidArgument)
      {
        for (int index = 0; index < types.Length; ++index)
        {
          if (!this.InputTypeSupported(types[index], index))
          {
            invalidArgument = index;
            return false;
          }
        }
        return true;
      }

      /// <summary>Returns a string representation of the operator. Read-only.</summary>
      public abstract string Name { get; }

      /// <summary>
      /// Returns the number of operands supported by the operator.
      /// </summary>
      /// <remarks>Default implementation returns 2.</remarks>
      public virtual byte OperandsSupported => 2;

      public override string ToString() => this.Name;
    }
}
