
// Type: Intermech.Expressions.ExpressionTree
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using System;
using System.Collections;
using System.Xml;


namespace Intermech.Expressions
{
    /// <summary>Represents a parsed expression tree.</summary>
    public class ExpressionTree
    {
      internal ArrayList _polishNotation;
      /// <summary>
      /// Determines if exceptions are to be thrown when an expression is being evaluated, or if "NaN" and "Infinity" values are to be used instead.
      /// </summary>
      /// <remarks>
      /// If <c>true</c>, exceptions are thrown.
      /// If <c>false</c>, exceptions are not thrown and "NaN" and "Infinity" values are used.
      /// Default is <c>true</c>.
      /// </remarks>
      /// <example>
      /// Example 1:
      /// ThrowEvaluationExceptions: true
      /// Expression: 1/0
      /// Result: <see cref="T:Intermech.Expressions.Exceptions.DivisionByZeroException" /> exception is thrown
      /// Example 2:
      /// ThrowEvaluationExceptions: true
      /// Expression: 1/0
      /// Result: Infinity
      /// </example>
      private ExpressionVariablesCollection _variables;
      private VariableValuesCollection _variableValues;
      private static bool _ANSINulls;

      internal ExpressionTree(ExpressionVariablesCollection variables, ArrayList notation)
      {
        this._variables = variables;
        this._polishNotation = (ArrayList) notation.Clone();
      }

      /// <summary>Creates XmlDocument representing an expression tree.</summary>
      /// <returns>XmlDocument object.</returns>
      private XmlDocument BuildXMLTree()
      {
        XmlDocument xmlDocument = new XmlDocument();
        XmlElement element = xmlDocument.CreateElement("EXPRESSION");
        xmlDocument.AppendChild((XmlNode) element);
        if (this._polishNotation != null)
        {
          ArrayList arrayList = new ArrayList(0);
          for (int index1 = 0; index1 < this._polishNotation.Count; ++index1)
          {
            ItemInfo itemInfo = (ItemInfo) this._polishNotation[index1];
            switch (itemInfo.type)
            {
              case ItemType.Operand:
              case ItemType.Constant:
              case ItemType.Variable:
                XmlElement xmlElement1 = this.CreateXMLElement(ref element, itemInfo);
                arrayList.Add((object) xmlElement1);
                break;
              case ItemType.Function:
                XmlElement xmlElement2 = this.CreateXMLElement(ref element, itemInfo);
                if (itemInfo.paramCount > 0)
                {
                  for (int index2 = 0; index2 <= itemInfo.paramCount - 1; ++index2)
                  {
                    xmlElement2.AppendChild((XmlNode) arrayList[arrayList.Count - itemInfo.paramCount + index2]);
                    arrayList.RemoveAt(arrayList.Count - itemInfo.paramCount + index2);
                  }
                }
                arrayList.Add((object) xmlElement2);
                break;
              case ItemType.Operator:
                XmlElement xmlElement3 = this.CreateXMLElement(ref element, itemInfo);
                switch (itemInfo.AsOperator.OperandsSupported)
                {
                  case 1:
                    xmlElement3.AppendChild((XmlNode) arrayList[arrayList.Count - 1]);
                    arrayList[arrayList.Count - 1] = (object) xmlElement3;
                    continue;
                  case 2:
                    xmlElement3.AppendChild((XmlNode) arrayList[arrayList.Count - 2]);
                    xmlElement3.AppendChild((XmlNode) arrayList[arrayList.Count - 1]);
                    arrayList.RemoveAt(arrayList.Count - 1);
                    arrayList[arrayList.Count - 1] = (object) xmlElement3;
                    continue;
                  default:
                    continue;
                }
            }
          }
          element.AppendChild((XmlNode) arrayList[arrayList.Count - 1]);
        }
        return xmlDocument;
      }

      /// <summary>Creates XmlElement object representing given token.</summary>
      /// <param name="root">Root element of XmlDocument.</param>
      /// <param name="item">Token.</param>
      /// <returns>XmlElement object.</returns>
      private XmlElement CreateXMLElement(ref XmlElement root, ItemInfo item)
      {
        string str1 = "";
        string str2 = "";
        switch (item.type)
        {
          case ItemType.Operand:
            str1 = item.itemValue.ToString();
            str2 = "Value";
            break;
          case ItemType.Constant:
            str1 = item.AsConstant.Name;
            str2 = "Constant";
            break;
          case ItemType.Variable:
            str1 = this._variables[item.index].Name;
            str2 = "Variable";
            break;
          case ItemType.Function:
            str1 = item.AsFunction.Name;
            str2 = "Function";
            break;
          case ItemType.Operator:
            str1 = item.AsOperator.Name;
            str2 = "Operator";
            break;
        }
        XmlElement element = root.OwnerDocument.CreateElement("NODE");
        element.SetAttribute("TYPE", str2);
        element.SetAttribute("VALUE", str1);
        return element;
      }

      /// <summary>
      /// Evaluates the expression with case-insensitive string comparison enabled.
      /// </summary>
      /// <param name="values">Array of values of variables. The elements in this array must match variables in <see cref="P:Intermech.Expressions.ExpressionTree.Variables" /> collection.
      /// The number of variables passed to <see cref="M:Intermech.Expressions.ExpressionTree.Evaluate(Intermech.Expressions.VariableValuesCollection)" /> function must match the number of variables originally passed to <see cref="M:Intermech.Expressions.Parser.Parse(System.String)" /> function.
      /// </param>
      /// <returns>The calculated value.</returns>
      /// <exception cref="T:System.Exception"></exception>
      /// <exception cref="T:Intermech.Expressions.Exceptions.EvaluateException"></exception>
      public object Evaluate(object[] values) => this.Evaluate(values, false);

      /// <overloads>
      /// <summary>Evaluates the expression.</summary>
      /// <remarks>This method is thread-safe</remarks>
      /// </overloads>
      /// <summary>
      /// Evaluates the expression, given a collection of used variables with values, with case-insensitive string comparison enabled.
      /// </summary>
      /// <param name="values">Collection of actually used variables obtained with (see cref="Intermech.Expressions.ExpressionTree.GetUsedVariables"/), filled with the values.</param>
      /// <returns>The calculated value.</returns>
      public object Evaluate(VariableValuesCollection values) => this.Evaluate(values, false);

      /// <summary>
      /// Evaluates the expression, given a collection of used variables with values.
      /// </summary>
      /// <param name="variables">Collection of actually used variables obtained with (see cref="Intermech.Expressions.ExpressionTree.GetUsedVariables"/), filled with the values.</param>
      /// <param name="isCaseSensitive">Determines if string comparisons are case-sensitive.</param>
      /// <returns>The calculated value.</returns>
      public object Evaluate(VariableValuesCollection variables, bool isCaseSensitive)
      {
        object[] values = new object[this.Variables.Count];
        foreach (VariableValue variable in (ReadOnlyCollectionBase) variables)
          values[variable.Index] = variable.Value;
        return this.Evaluate(values, isCaseSensitive);
      }

      /// <summary>Evaluates the expression.</summary>
      /// <param name="values">Array of values of variables. The elements in this array must match variables in <see cref="P:Intermech.Expressions.ExpressionTree.Variables" /> collection.
      /// The number of variables passed to <see cref="M:Intermech.Expressions.ExpressionTree.Evaluate(Intermech.Expressions.VariableValuesCollection)" /> function must match the number of variables originally passed to <see cref="M:Intermech.Expressions.Parser.Parse(System.String)" /> function.
      /// </param>
      /// <param name="isCaseSensitive">Determines if string comparisons are case-sensitive.</param>
      /// <returns>The calculated value.</returns>
      public object Evaluate(object[] values, bool isCaseSensitive)
      {
        if (values.Length != this._variables.Count)
          throw new InvalidParameterCountException();
        ArrayList arrayList = new ArrayList(0);
        this.AnalizeValues(values);
        int count = this._polishNotation.Count;
        for (int index = 0; index < count; ++index)
        {
          ItemInfo itemInfo = (ItemInfo) this._polishNotation[index];
          switch (itemInfo.type)
          {
            case ItemType.Operand:
              arrayList.Add(itemInfo.itemValue);
              break;
            case ItemType.Constant:
              arrayList.Add(itemInfo.AsConstant.Value);
              break;
            case ItemType.Variable:
              arrayList.Add(values[itemInfo.index]);
              break;
            case ItemType.Function:
              Function asFunction = itemInfo.AsFunction;
              object obj1;
              if (itemInfo.paramCount <= 0)
              {
                obj1 = asFunction.Evaluate(new object[0], isCaseSensitive);
              }
              else
              {
                int paramCount = itemInfo.paramCount;
                Type[] typeArray = new Type[paramCount];
                object[] objArray = new object[paramCount];
                while (paramCount-- > 0)
                {
                  objArray[paramCount] = arrayList[arrayList.Count - 1];
                  typeArray[paramCount] = objArray[paramCount].GetType();
                  arrayList.RemoveAt(arrayList.Count - 1);
                }
                if (objArray.GetUpperBound(0) == 0 && objArray[0].GetType().IsArray)
                {
                  Array sourceArray = (Array) objArray[0];
                  objArray = new object[sourceArray.Length];
                  Array.Copy(sourceArray, (Array) objArray, sourceArray.Length);
                }
                obj1 = !asFunction.IsNullable(objArray) ? asFunction.Evaluate(objArray, isCaseSensitive) : (object) DBNull.Value;
              }
              arrayList.Add(obj1);
              break;
            case ItemType.Operator:
              Operator asOperator = itemInfo.AsOperator;
              object[] values1 = new object[(int) asOperator.OperandsSupported];
              if (asOperator.OperandsSupported == (byte) 1)
                values1[0] = arrayList[arrayList.Count - 1];
              else if (asOperator.OperandsSupported == (byte) 2)
              {
                values1[0] = arrayList[arrayList.Count - 2];
                values1[1] = arrayList[arrayList.Count - 1];
                arrayList.RemoveAt(arrayList.Count - 1);
              }
              object obj2 = !asOperator.IsNullable(values1) ? asOperator.Evaluate(values1, isCaseSensitive) : (object) DBNull.Value;
              arrayList[arrayList.Count - 1] = obj2;
              break;
          }
        }
        return arrayList[arrayList.Count - 1];
      }

      private void AnalizeValues(object[] values)
      {
        if (values == null)
          return;
        bool flag1 = false;
        bool flag2 = false;
        int length = values.Length;
        for (int index = 0; index < length; ++index)
        {
          object obj = values[index];
          if (Convert.IsDBNull(obj))
            flag1 = true;
          else if (obj != null)
          {
            if (obj.GetType() != typeof (string))
              return;
            flag2 = true;
          }
        }
        if (!(flag1 & flag2))
          return;
        for (int index = 0; index < length; ++index)
        {
          if (Convert.IsDBNull(values[index]))
            values[index] = (object) string.Empty;
        }
      }

      /// <summary>Returns the type which the expression returns.</summary>
      /// <returns>The Type object that represents the type which the expression returns.</returns>
      /// <remarks>This method is thread-safe</remarks>
      public Type ReturnType
      {
        get
        {
          ArrayList arrayList = new ArrayList(0);
          for (int index = 0; index < this._polishNotation.Count; ++index)
          {
            ItemInfo itemInfo = (ItemInfo) this._polishNotation[index];
            switch (itemInfo.type)
            {
              case ItemType.Operand:
                arrayList.Add((object) itemInfo.itemValue.GetType());
                break;
              case ItemType.Constant:
                arrayList.Add((object) itemInfo.AsConstant.Value.GetType());
                break;
              case ItemType.Variable:
                arrayList.Add((object) this._variables[itemInfo.index].Type);
                break;
              case ItemType.Function:
                Type returnType;
                if (itemInfo.paramCount > 0)
                {
                  int paramCount = itemInfo.paramCount;
                  Type[] types = new Type[paramCount];
                  while (paramCount-- > 0)
                  {
                    types[paramCount] = (Type) arrayList[arrayList.Count - 1];
                    arrayList.RemoveAt(arrayList.Count - 1);
                  }
                  returnType = itemInfo.AsFunction.GetReturnType(types);
                }
                else
                {
                  Type[] types = new Type[0];
                  returnType = itemInfo.AsFunction.GetReturnType(types);
                }
                arrayList.Add((object) returnType);
                break;
              case ItemType.Operator:
                if (itemInfo.AsOperator.OperandsSupported == (byte) 1)
                {
                  Type[] types = new Type[1]
                  {
                    (Type) arrayList[arrayList.Count - 1]
                  };
                  arrayList[arrayList.Count - 1] = (object) itemInfo.AsOperator.GetReturnType(types);
                  break;
                }
                if (itemInfo.AsOperator.OperandsSupported == (byte) 2)
                {
                  Type[] types = new Type[2]
                  {
                    (Type) arrayList[arrayList.Count - 2],
                    (Type) arrayList[arrayList.Count - 1]
                  };
                  arrayList[arrayList.Count - 1] = (object) itemInfo.AsOperator.GetReturnType(types);
                  break;
                }
                break;
            }
          }
          return (Type) arrayList[arrayList.Count - 1];
        }
      }

      /// <summary>
      /// Returns the collection of variables used in the expression. Read-only.
      /// </summary>
      /// <returns>Collection of used variables</returns>
      /// <remarks>
      /// <para>For each variable in the collection, you can specify a corresponding value to be used in evaluation.
      /// Then, you can pass this collection to the <see cref="M:Intermech.Expressions.ExpressionTree.Evaluate(Intermech.Expressions.VariableValuesCollection)" /> method.
      /// </para>
      /// <para>
      /// This method is thread-safe. Each time it is called, a new copy of the collection is created.
      /// </para>
      /// </remarks>
      public VariableValuesCollection UsedVariables
      {
        get
        {
          if (this._variableValues == null)
          {
            ArrayList arrayList1 = new ArrayList();
            ArrayList arrayList2 = new ArrayList();
            for (int index = 0; index < this._polishNotation.Count; ++index)
            {
              ItemInfo itemInfo = (ItemInfo) this._polishNotation[index];
              if (itemInfo.type == ItemType.Variable && !arrayList2.Contains((object) itemInfo.index))
              {
                arrayList1.Add((object) new VariableValue(itemInfo.index, this._variables[itemInfo.index]));
                arrayList2.Add((object) itemInfo.index);
              }
            }
            this._variableValues = new VariableValuesCollection((IList) arrayList1);
          }
          return this._variableValues.Clone();
        }
      }

      /// <summary>
      /// Checks if functions and operators in expression use valid arguments.
      /// </summary>
      /// <param name="errorPos">Position of invalid item in an expression.</param>
      /// <param name="invalidArgumentIndex">Index of invalid function/operator argument.</param>
      /// <returns>true if all the arguments are valid, false otherwise</returns>
      internal bool Validate(ref int errorPos, ref int invalidArgumentIndex)
      {
        ArrayList arrayList = new ArrayList(0);
        int invalidArgument = -1;
        bool flag = true;
        for (int index = 0; index < this._polishNotation.Count && flag; ++index)
        {
          ItemInfo itemInfo = (ItemInfo) this._polishNotation[index];
          switch (itemInfo.type)
          {
            case ItemType.Operand:
              arrayList.Add((object) itemInfo.itemValue.GetType());
              break;
            case ItemType.Constant:
              arrayList.Add((object) itemInfo.AsConstant.Value.GetType());
              break;
            case ItemType.Variable:
              arrayList.Add((object) this._variables[itemInfo.index].Type);
              break;
            case ItemType.Function:
              if (itemInfo.paramCount > 0)
              {
                int paramCount = itemInfo.paramCount;
                Type[] types = new Type[paramCount];
                while (paramCount-- > 0)
                {
                  types[paramCount] = (Type) arrayList[arrayList.Count - 1];
                  arrayList.RemoveAt(arrayList.Count - 1);
                }
                if (!itemInfo.AsFunction.Validate(types, ref invalidArgument))
                {
                  invalidArgumentIndex = invalidArgument;
                  errorPos = itemInfo.position;
                  flag = false;
                  break;
                }
                arrayList.Add((object) itemInfo.AsFunction.GetReturnType(types));
                break;
              }
              arrayList.Add((object) itemInfo.AsFunction.GetReturnType(new Type[0]));
              break;
            case ItemType.Operator:
              switch (itemInfo.AsOperator.OperandsSupported)
              {
                case 1:
                  Type[] types1 = new Type[1]
                  {
                    (Type) arrayList[arrayList.Count - 1]
                  };
                  if (!itemInfo.AsOperator.Validate(types1, ref invalidArgument))
                  {
                    invalidArgumentIndex = invalidArgument;
                    errorPos = itemInfo.position;
                    flag = false;
                  }
                  Type[] types2 = new Type[1]
                  {
                    (Type) arrayList[arrayList.Count - 1]
                  };
                  arrayList[arrayList.Count - 1] = (object) itemInfo.AsOperator.GetReturnType(types2);
                  continue;
                case 2:
                  Type[] types3 = new Type[2]
                  {
                    (Type) arrayList[arrayList.Count - 2],
                    (Type) arrayList[arrayList.Count - 1]
                  };
                  if (!itemInfo.AsOperator.Validate(types3, ref invalidArgument))
                  {
                    invalidArgumentIndex = invalidArgument;
                    errorPos = itemInfo.position;
                    flag = false;
                    continue;
                  }
                  Type[] types4 = new Type[2]
                  {
                    (Type) arrayList[arrayList.Count - 2],
                    (Type) arrayList[arrayList.Count - 1]
                  };
                  arrayList.RemoveAt(arrayList.Count - 1);
                  arrayList[arrayList.Count - 1] = (object) itemInfo.AsOperator.GetReturnType(types4);
                  continue;
                default:
                  continue;
              }
          }
        }
        return flag;
      }

      /// <summary>
      /// Controls comparisons against <c>System.DBNull</c> values.
      /// </summary>
      /// <remarks>
      /// <para>
      /// With <see cref="F:Intermech.Expressions.ExpressionTree._ANSINulls" /> set to <c>true</c>, the comparison operators EQUAL (=) and NOT EQUAL (&lt;&gt;) always return <c>DBNull</c> when one of its arguments is <c>DBNull</c>.
      /// With <see cref="F:Intermech.Expressions.ExpressionTree._ANSINulls" /> set to <c>false</c>, these operators return <c>TRUE</c> or <c>FALSE</c>, depending on whether both arguments are <c>DBNull</c>.
      /// </para>
      /// <para>
      /// Default is <c>true</c>.
      /// </para>
      /// </remarks>
      internal static bool ANSINulls
      {
        get => ExpressionTree._ANSINulls;
        set => ExpressionTree._ANSINulls = value;
      }

      public XmlDocument XML => this.BuildXMLTree();

      /// <summary>Gets the collection of variables. Read-only.</summary>
      public ExpressionVariablesCollection Variables => this._variables;
    }
}
