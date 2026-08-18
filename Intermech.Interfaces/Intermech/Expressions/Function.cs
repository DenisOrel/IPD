
// Type: Intermech.Expressions.Function
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Functions;
using System;


namespace Intermech.Expressions
{
    /// <summary>
    /// A base abstract class for any function.
    /// You must inherit from this class in order to add a user-defined function to the <see cref="P:Intermech.Expressions.Parser.Functions" /> collection.
    /// </summary>
    public abstract class Function : IIdentifier
    {
      public const double M_TO_RAD = 0.017453292519943295;
      public const double M_TO_GRAD = 57.295779513082323;

      /// <overloads>Returns a value calculated by the function.
      /// When deriving from the <see cref="T:Intermech.Expressions.Function" /> class, you must override one of the overloaded versions of <c>Evaluate</c> method.
      /// </overloads>
      /// <summary>Returns a value calculated by the function.</summary>
      /// <param name="values">Array of input values.</param>
      /// <returns>Calculated value.</returns>
      /// <remarks>You should override this method if you do not need to account for case-sensitivity; otherwise you must override <c>Evaluate(object[] Values, bool IsCaseSensitive)</c> method.</remarks>
      public virtual object Evaluate(object[] values) => this.Evaluate(values, false);

      /// <summary>
      /// Returns a value calculated by the function. Allows to account for case-sensitivity.
      /// </summary>
      /// <param name="values">Array of input values.</param>
      /// <param name="isCaseSensitive">Determines if string comparisons are case-sensitive.</param>
      /// <returns>Calculated value.</returns>
      /// <remarks>If you need to account for case-sensitivity you must override this method; otherwise you should override <c>Evaluate(object[] Values)</c> method.</remarks>
      public virtual object Evaluate(object[] values, bool isCaseSensitive) => this.Evaluate(values);

      /// <summary>Returns a type of the return value. Read-only.</summary>
      /// <param name="types">Types of input parameters.</param>
      /// <returns>Type of the return value.</returns>
      /// <remarks>Default implementation returns System.Double. If you want to return value of different type you must override this method.</remarks>
      public virtual Type GetReturnType(Type[] types) => typeof (double);

      /// <summary>
      /// Returns a value indicating whether the function supports the specified type of input parameter.
      /// </summary>
      /// <param name="type">Input parameter type.</param>
      /// <param name="index">Input parameter index.</param>
      /// <returns>True, if the function supports specified type of the parameter with the specified Index; otherwise, false.</returns>
      /// <remarks>Default implementation returns true for System.Double type. If you want to support parameters of different type you must override this method.</remarks>
      protected virtual bool InputTypeSupported(Type type, int index)
      {
        return ExpTypeConverter.CanConvert(type, typeof (double));
      }

      /// <summary>
      /// Determines if the function returns <c>DBNull</c> when one of its input parameters is <c>DBNull</c>.
      /// </summary>
      /// <param name="values">Array of input values.</param>
      /// <returns><c>true</c>, if function returns <c>DBNull</c>, otherwise <c>false</c>.</returns>
      /// <remarks>
      /// <para>
      /// Default implementation returns <c>DBNull</c> if at least one of the function's input arguments is <c>DBNull</c>.
      /// </para>
      /// <para>
      /// You should override this method only if you would like to implement different <c>DBNull</c> processing logic.
      /// For example, the following built-in USPExpress functions override this method:
      /// <list type="bullet">
      /// <item>
      /// Logical functions: AND, OR
      /// </item>
      /// <item>
      /// Aggregate functions: Count, Min, Max, Sum
      /// </item>
      /// <item>
      /// IIF funciton
      /// </item>
      /// </list>
      /// </para>
      /// </remarks>
      public virtual bool IsNullable(object[] values)
      {
        bool flag = false;
        for (int index = 0; index < values.Length; ++index)
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
      /// Returns a value indicating whether the function supports a specified number of input parameters.
      /// </summary>
      /// <param name="count">Number of input parameters.</param>
      /// <returns>True, if the function supports a specified number of input parameters; otherwise, false.</returns>
      /// <remarks>Default implementation returns true if number of input parameters is equal to 1. If you want to support different number of input parameters you must override this method.</remarks>
      public virtual bool MultArgsSupported(int count) => count == 1;

      /// <summary>
      /// Returns a value indicating whether the function supports specified types of input parameters.
      /// </summary>
      /// <param name="types">Array of types of input parameters.</param>
      /// <param name="invalidArgument">Invalid argument index.</param>
      /// <returns>True, if the function supports specified types; otherwise, false.</returns>
      /// <remarks>Default implementation calls <see cref="M:Intermech.Expressions.Function.InputTypeSupported(System.Type,System.Int32)" /> method iteratively for each of the input parameters. You must override this method only if the types of your input parameters depend on each other.</remarks>
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

      public override string ToString()
      {
        return this.Description.Length > 0 ? $"{this.Name}:{this.Description}" : this.Name;
      }

      /// <summary>Returns the name of the function. Read-only.</summary>
      public abstract string Name { get; }

      public virtual string Description => string.Empty;

      public virtual FunctionCategory Category => FunctionCategory.None;
    }
}
