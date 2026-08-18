
// Type: Intermech.Expressions.Parser
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Constants;
using Intermech.Expressions.Exceptions;
using Intermech.Expressions.Functions;
using Intermech.Expressions.Operators;
using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace Intermech.Expressions
{
    /// <summary>Parses an expression string</summary>
    public class Parser : IDisposable
    {
      internal const char DateIdentifier = '#';
      private VariablesCollection _variables;
      private ArrayList _notation;
      private ExpressionVariablesCollection _variablesCache;
      private bool _autoDetectVars;
      private bool _validate;
      private bool _useCache;
      private object _context;
      private const string OperatorBeginLetters = "AaOoNn";
      internal const char StringIdentifier = '\'';
      private static ConstantsCollection _constants;
      private static FunctionsCollection _functions;
      private static OperatorsCollection _operators;
      private static NumberFormatInfo _nfi;
      private static Hashtable _expressionsCache;
      private static Regex _regex = new Regex("^[\\p{L}\\p{Pc}][\\w\\p{Zs}]*$");

      static Parser()
      {
        Parser.AddFunctionsCollection();
        Parser.AddOperatorsCollection();
        Parser.AddConstantsCollection();
        Parser._expressionsCache = new Hashtable();
        Parser._nfi = new NumberFormatInfo();
        Parser._nfi.NumberDecimalSeparator = ".";
      }

      /// <summary>Public constructor</summary>
      public Parser()
      {
        this.AddVariablesCollection();
        this._autoDetectVars = false;
        this._validate = true;
        this._useCache = true;
      }

      /// <summary>Destructor</summary>
      ~Parser() => this.Dispose();

      /// <summary>Очищает внутренний кэш вычисленных выражений</summary>
      public static void ClearCache() => Parser._expressionsCache.Clear();

      private static void AddConstantsCollection()
      {
        Parser._constants = new ConstantsCollection();
        Parser._constants.Add((Constant) new PiConstant());
        Parser._constants.Add((Constant) new EConstant());
        Parser._constants.Add((Constant) new TrueConstant());
        Parser._constants.Add((Constant) new FalseConstant());
        Parser._constants.Add((Constant) new YearConstant());
        Parser._constants.Add((Constant) new QuarterConstant());
        Parser._constants.Add((Constant) new MonthConstant());
        Parser._constants.Add((Constant) new DayOfYearConstant());
        Parser._constants.Add((Constant) new DayConstant());
        Parser._constants.Add((Constant) new WeekOfYearConstant());
        Parser._constants.Add((Constant) new WeekdayConstant());
        Parser._constants.Add((Constant) new HourConstant());
        Parser._constants.Add((Constant) new MinuteConstant());
        Parser._constants.Add((Constant) new SecondConstant());
        Parser._constants.Add((Constant) new SystemConstant());
        Parser._constants.Add((Constant) new SundayConstant());
        Parser._constants.Add((Constant) new MondayConstant());
        Parser._constants.Add((Constant) new TuesdayConstant());
        Parser._constants.Add((Constant) new WednesdayConstant());
        Parser._constants.Add((Constant) new ThursdayConstant());
        Parser._constants.Add((Constant) new FridayConstant());
        Parser._constants.Add((Constant) new SaturdayConstant());
        Parser._constants.Add((Constant) new Jan1Constant());
        Parser._constants.Add((Constant) new FirstFourDaysConstant());
        Parser._constants.Add((Constant) new FirstFullWeekConstant());
        Parser._constants.ConstantAdd += new ConstantsCollection.AddEventHandler(Parser.CheckGlobalName);
      }

      private static void AddFunctionsCollection()
      {
        Parser._functions = new FunctionsCollection();
        Parser._functions.Add((Function) new AbsFunction());
        Parser._functions.Add((Function) new ArcCosFunction());
        Parser._functions.Add((Function) new _ArcCosFunction());
        Parser._functions.Add((Function) new ArcCotFunction());
        Parser._functions.Add((Function) new ArcCscFunction());
        Parser._functions.Add((Function) new ArcSecFunction());
        Parser._functions.Add((Function) new ArcSinFunction());
        Parser._functions.Add((Function) new _ArcSinFunction());
        Parser._functions.Add((Function) new ArcTanFunction());
        Parser._functions.Add((Function) new _ArcTanFunction());
        Parser._functions.Add((Function) new CaptionFunction());
        Parser._functions.Add((Function) new CeilingFunction());
        Parser._functions.Add((Function) new CosFunction());
        Parser._functions.Add((Function) new _CosFunction());
        Parser._functions.Add((Function) new HCosFunction());
        Parser._functions.Add((Function) new CotFunction());
        Parser._functions.Add((Function) new HCotFunction());
        Parser._functions.Add((Function) new CscFunction());
        Parser._functions.Add((Function) new HCscFunction());
        Parser._functions.Add((Function) new ExpFunction());
        Parser._functions.Add((Function) new FloorFunction());
        Parser._functions.Add((Function) new IntFunction());
        Parser._functions.Add((Function) new DblFunction());
        Parser._functions.Add((Function) new IIFFunction());
        Parser._functions.Add((Function) new IFFunction());
        Parser._functions.Add((Function) new AndFunction());
        Parser._functions.Add((Function) new OrFunction());
        Parser._functions.Add((Function) new LnFunction());
        Parser._functions.Add((Function) new LogFunction());
        Parser._functions.Add((Function) new Log10Function());
        Parser._functions.Add((Function) new NegFunction());
        Parser._functions.Add((Function) new PowerFunction());
        Parser._functions.Add((Function) new RandomFunction());
        Parser._functions.Add((Function) new SecFunction());
        Parser._functions.Add((Function) new HSecFunction());
        Parser._functions.Add((Function) new SinFunction());
        Parser._functions.Add((Function) new _SinFunction());
        Parser._functions.Add((Function) new HSinFunction());
        Parser._functions.Add((Function) new SqrtFunction());
        Parser._functions.Add((Function) new SqrFunction());
        Parser._functions.Add((Function) new SumFunction());
        Parser._functions.Add((Function) new TanFunction());
        Parser._functions.Add((Function) new _TanFunction());
        Parser._functions.Add((Function) new HTanFunction());
        Parser._functions.Add((Function) new MinFunction());
        Parser._functions.Add((Function) new MaxFunction());
        Parser._functions.Add((Function) new RoundFunction());
        Parser._functions.Add((Function) new CountFunction());
        Parser._functions.Add((Function) new AverageFunction());
        Parser._functions.Add((Function) new NowFunction());
        Parser._functions.Add((Function) new TodayFunction());
        Parser._functions.Add((Function) new YearFunction());
        Parser._functions.Add((Function) new MonthFunction());
        Parser._functions.Add((Function) new DayFunction());
        Parser._functions.Add((Function) new WeekDayFunction());
        Parser._functions.Add((Function) new HourFunction());
        Parser._functions.Add((Function) new MinuteFunction());
        Parser._functions.Add((Function) new SecondFunction());
        Parser._functions.Add((Function) new DateAddFunction());
        Parser._functions.Add((Function) new DateDiffFunction());
        Parser._functions.Add((Function) new DatePartFunction());
        Parser._functions.Add((Function) new DateFunction());
        Parser._functions.Add((Function) new FormatFunction());
        Parser._functions.Add((Function) new StrFunction());
        Parser._functions.Add((Function) new PosFunction());
        Parser._functions.Add((Function) new LenFunction());
        Parser._functions.Add((Function) new LeftFunction());
        Parser._functions.Add((Function) new RightFunction());
        Parser._functions.Add((Function) new MidFunction());
        Parser._functions.Add((Function) new SubstituteFunction());
        Parser._functions.Add((Function) new LowerFunction());
        Parser._functions.Add((Function) new UpperFunction());
        Parser._functions.Add((Function) new TrimFunction());
        Parser._functions.Add((Function) new InFunction());
        Parser._functions.Add((Function) new FindFunction());
        Parser._functions.Add((Function) new IsDBNullFunction());
        Parser._functions.Add((Function) new IsNullFunction());
        Parser._functions.Add((Function) new ValFunction());
        Parser._functions.FunctionAdd += new FunctionsCollection.AddEventHandler(Parser.CheckGlobalName);
      }

      private static void AddOperatorsCollection()
      {
        Parser._operators = new OperatorsCollection((IList) new ArrayList(32 /*0x20*/)
        {
          (object) new PlusModifier(),
          (object) new MinusModifier(),
          (object) new PlusOperator(),
          (object) new MinusOperator(),
          (object) new MultiplyOperator(),
          (object) new DivideOperator(),
          (object) new ModulusOperator(),
          (object) new PowerOperator(),
          (object) new IsLessThanOperator(),
          (object) new IsGreaterThanOperator(),
          (object) new IsEqualToOperator(),
          (object) new IsBasicEqualToOperator(),
          (object) new IsNotEqualToOperator(),
          (object) new IsBasicNotEqualToOperator(),
          (object) new IsLessThanOrEqualToOperator(),
          (object) new IsGreaterThanOrEqualToOperator(),
          (object) new AndOperator(),
          (object) new OrOperator(),
          (object) new NotOperator(),
          (object) new AndBasicOperator(),
          (object) new OrBasicOperator(),
          (object) new NotBasicOperator(),
          (object) new BitwiseAndOperator(),
          (object) new BitwiseInclusiveOrOperator(),
          (object) new BitwiseExclusiveOrOperator(),
          (object) new BitwiseCompliment(),
          (object) new ShiftLeftOperator(),
          (object) new ShiftRightOperator()
        });
      }

      /// <summary>Add item to Polish Notation array.</summary>
      /// <param name="itemType">Item type.</param>
      /// <param name="notation">Polish Notation array.</param>
      /// <param name="stack">Auxiliary stack of functions. Is used to build a Polish Notation array.</param>
      /// <remarks>Overloaded version, is used to add parentheses, parentheses and commas.</remarks>
      private void AddToPolish(ItemType itemType, ArrayList notation, ArrayList stack)
      {
        ItemInfo itemInfo1 = new ItemInfo();
        ItemInfo itemInfo2 = new ItemInfo();
        switch (itemType)
        {
          case ItemType.OpeningParen:
            itemInfo1.type = ItemType.OpeningParen;
            stack.Add((object) itemInfo1);
            break;
          case ItemType.ClosingParen:
            ItemInfo itemInfo3 = (ItemInfo) stack[stack.Count - 1];
            if (stack.Count > 0 && itemInfo3.type != ItemType.OpeningParen)
            {
              do
              {
                notation.Add((object) itemInfo3);
                stack.RemoveAt(stack.Count - 1);
                itemInfo3 = (ItemInfo) stack[stack.Count - 1];
              }
              while (itemInfo3.type != ItemType.OpeningParen);
            }
            stack.RemoveAt(stack.Count - 1);
            break;
          case ItemType.Comma:
            ItemInfo itemInfo4 = (ItemInfo) stack[stack.Count - 1];
            if (stack.Count <= 0 || itemInfo4.type == ItemType.OpeningParen)
              break;
            do
            {
              notation.Add((object) itemInfo4);
              stack.RemoveAt(stack.Count - 1);
              itemInfo4 = (ItemInfo) stack[stack.Count - 1];
            }
            while (itemInfo4.type != ItemType.OpeningParen);
            break;
        }
      }

      /// <summary>Add item to Polish Notation array.</summary>
      /// <param name="itemType">Item type.</param>
      /// <param name="value">Item value.</param>
      /// <param name="position">Position in an expression string being parsed.</param>
      /// <param name="notation">Polish Notation array.</param>
      /// <remarks>Overloaded version, is used to add numbers, dates and strings.</remarks>
      private void AddToPolish(ItemType itemType, object value, int position, ArrayList notation)
      {
        ItemInfo itemInfo = new ItemInfo();
        if (itemType != ItemType.Operand)
          return;
        itemInfo.type = itemType;
        itemInfo.itemValue = value;
        itemInfo.position = position;
        notation.Add((object) itemInfo);
      }

      /// <summary>Adds item to Polish Notation array.</summary>
      /// <param name="itemType">Item type.</param>
      /// <param name="index">Item index in corresponding collection.</param>
      /// <param name="position">Position in an expression string.</param>
      /// <param name="notation">Polish Notation array.</param>
      /// <param name="stack">Auxiliary stack of functions. Is used to build a Polish Notation array.</param>
      /// <remarks>Overloaded version, is used to add variables, functions and operators.</remarks>
      private void AddToPolish(
        ItemType itemType,
        int index,
        int position,
        ArrayList notation,
        ArrayList stack)
      {
        ItemInfo itemInfo1 = new ItemInfo();
        ItemInfo itemInfo2 = new ItemInfo();
        switch (itemType)
        {
          case ItemType.Constant:
            itemInfo1.type = itemType;
            itemInfo1.index = -1;
            itemInfo1.position = position;
            itemInfo1.itemValue = (object) Parser._constants[index];
            notation.Add((object) itemInfo1);
            break;
          case ItemType.Variable:
            itemInfo1.type = itemType;
            itemInfo1.index = index;
            itemInfo1.position = position;
            itemInfo1.itemValue = (object) this._variables[index];
            notation.Add((object) itemInfo1);
            break;
          case ItemType.Function:
            itemInfo1.type = itemType;
            itemInfo1.index = -1;
            itemInfo1.position = position;
            itemInfo1.itemValue = (object) Parser._functions[index];
            stack.Add((object) itemInfo1);
            break;
          case ItemType.Operator:
            if (Parser._operators[index].OperandsSupported > (byte) 1)
            {
              bool flag = false;
              while (!flag)
              {
                if (stack.Count == 0)
                {
                  flag = true;
                }
                else
                {
                  ItemInfo itemInfo3 = (ItemInfo) stack[stack.Count - 1];
                  switch (itemInfo3.type)
                  {
                    case ItemType.Function:
                      notation.Add((object) itemInfo3);
                      stack.RemoveAt(stack.Count - 1);
                      continue;
                    case ItemType.Operator:
                      if (itemInfo3.AsOperator.GetPriority() < Parser._operators[index].GetPriority())
                      {
                        flag = true;
                        continue;
                      }
                      notation.Add((object) itemInfo3);
                      stack.RemoveAt(stack.Count - 1);
                      continue;
                    default:
                      flag = true;
                      continue;
                  }
                }
              }
            }
            itemInfo1.type = itemType;
            itemInfo1.index = -1;
            itemInfo1.position = position;
            itemInfo1.itemValue = (object) Parser._operators[index];
            stack.Add((object) itemInfo1);
            break;
        }
      }

      private void AddVariablesCollection()
      {
        this._variables = new VariablesCollection();
        this._variables.VariableAdd += new VariablesCollection.AddEventHandler(this.CheckName);
        this._variables.Changed += new VariablesCollection.ChangedEventHandler(this.DeleteVariablesCache);
      }

      /// <summary>
      /// Returns a value indicating whether the occurrence of the given token is valid so that it could be pushed to the Polish Notation stack.
      /// </summary>
      /// <param name="newType">Type of the new token.</param>
      /// <param name="operandsSupported">Number of operands supported.</param>
      /// <param name="lastType">Type of the preceding token.</param>
      /// <returns>True, if the occurrence is valid; otherwise, false.</returns>
      /// <remarks>Overloaded version to check if operator is valid.</remarks>
      private bool CanAdd(ItemType newType, int operandsSupported, ref ItemType lastType)
      {
        if (operandsSupported == 1)
        {
          if (lastType != ItemType.OpeningParen && lastType != ItemType.Comma && lastType != ItemType.Operator)
            return false;
          lastType = ItemType.Operator;
          return true;
        }
        if (lastType != ItemType.Variable && lastType != ItemType.ClosingParen && lastType != ItemType.Constant && lastType != ItemType.Operand)
          return false;
        lastType = ItemType.Operator;
        return true;
      }

      /// <summary>
      /// Returns a value indicating whether the occurrence of the given token is valid so that it could be pushed to the Polish Notation stack.
      /// </summary>
      /// <param name="newType">Type of the new token.</param>
      /// <param name="lastType">Type of the preceding token.</param>
      /// <param name="nPosition">Current position in an expression text.</param>
      /// <param name="bImplicitMultiplication">Value indicating whether implicit multiplication is supported.</param>
      /// <param name="notation">Polish Notation stack.</param>
      /// <param name="stack">Auxiliary stack of functions. Is used to build a Polish Notation array.</param>
      /// <returns>True, if new item can be added; otherwise, false.</returns>
      private bool CanAdd(
        ItemType newType,
        ref ItemType lastType,
        int nPosition,
        bool bImplicitMultiplication,
        ArrayList notation,
        ArrayList stack)
      {
        switch (newType)
        {
          case ItemType.OpeningParen:
            if (lastType != ItemType.OpeningParen && lastType != ItemType.Comma && lastType != ItemType.Operator && lastType != ItemType.Function)
            {
              if (bImplicitMultiplication && (lastType == ItemType.Operand || lastType == ItemType.Variable || lastType == ItemType.Constant || lastType == ItemType.ClosingParen))
              {
                this.AddToPolish(ItemType.Operator, 4, nPosition, notation, stack);
                lastType = newType;
                return true;
              }
              break;
            }
            lastType = newType;
            return true;
          case ItemType.ClosingParen:
          case ItemType.Comma:
            if (lastType == ItemType.ClosingParen || lastType == ItemType.Variable || lastType == ItemType.Constant || lastType == ItemType.Operand)
            {
              lastType = newType;
              return true;
            }
            break;
          case ItemType.Operand:
            if (lastType != ItemType.OpeningParen && lastType != ItemType.Comma && lastType != ItemType.Operator)
            {
              if (bImplicitMultiplication && lastType == ItemType.ClosingParen)
              {
                this.AddToPolish(ItemType.Operator, 4, nPosition, notation, stack);
                lastType = newType;
                return true;
              }
              break;
            }
            lastType = newType;
            return true;
          case ItemType.Constant:
          case ItemType.Variable:
          case ItemType.Function:
            if (lastType != ItemType.OpeningParen && lastType != ItemType.Comma && lastType != ItemType.Operator)
            {
              if (bImplicitMultiplication && (lastType == ItemType.Operand || lastType == ItemType.ClosingParen))
              {
                this.AddToPolish(ItemType.Operator, 4, nPosition, notation, stack);
                lastType = newType;
                return true;
              }
              break;
            }
            lastType = newType;
            return true;
        }
        return false;
      }

      /// <summary>
      /// Checks if the specified identifier is valid and is unique among defined Variables, Constants, Functions and Operators.
      /// </summary>
      /// <param name="newName">New identifier.</param>
      /// <remarks>
      /// Is called when adding new variables, aliases, functions or constants.
      /// </remarks>
      private void CheckName(string newName)
      {
        Parser.CheckGlobalName(newName);
        if (newName == null || newName.Length == 0)
          throw new InvalidIdentifierException(newName);
        if (this._variables.IndexOf(newName) > -1)
          throw new DuplicateIdentifierException();
      }

      private static void CheckGlobalName(string newName)
      {
        if (newName == null || newName.Length == 0)
          throw new InvalidIdentifierException(newName);
        bool flag = true;
        if (Parser._functions.IndexOf(newName) > -1)
          flag = false;
        else if (Parser._constants.IndexOf(newName) > -1)
        {
          flag = false;
        }
        else
        {
          for (int index = 0; index < Parser._operators.Count; ++index)
          {
            if (string.Compare(newName, Parser._operators[index].Name, true) == 0)
            {
              flag = false;
              break;
            }
          }
        }
        if (!flag)
          throw new DuplicateIdentifierException();
      }

      /// <summary>Deletes current ExpressionVariables collection.</summary>
      private void DeleteVariablesCache()
      {
        this._variablesCache = (ExpressionVariablesCollection) null;
      }

      /// <summary>
      /// Checks if there is an operator at current position in an expression string.
      /// If found, advances to the next token position.
      /// </summary>
      /// <param name="sText">Expression string.</param>
      /// <param name="sItem">Name of the operator, if found.</param>
      /// <param name="nPos">Current position in an expression string.</param>
      /// <param name="nIndex">Index in Operators collection.</param>
      /// <param name="lastItem">Type of the preceding token.</param>
      /// <returns>True, if item name is found in Operators collection; otherwise, false.</returns>
      private bool FindOperator(
        string sText,
        ref string sItem,
        ref int nPos,
        ref int nIndex,
        ItemType lastItem)
      {
        OperatorType index = OperatorType.noOperator;
        switch (sText[nPos])
        {
          case '!':
            index = OperatorType.notOperator;
            if (nPos < sText.Length - 1)
            {
              switch (sText[nPos + 1])
              {
                case '&':
                  index = OperatorType.bitwiseExclusiveOrOperator;
                  break;
                case '=':
                  index = OperatorType.isNotEqualToOperator;
                  break;
              }
            }
            else
              break;
            break;
          case '%':
            index = OperatorType.modulusOperator;
            break;
          case '&':
            index = OperatorType.bitwiseAndOperator;
            if (nPos < sText.Length - 1 && sText[nPos + 1] == '&')
            {
              index = OperatorType.andOperator;
              break;
            }
            break;
          case '*':
            index = OperatorType.multiplyOperator;
            break;
          case '+':
            index = this.IsSignModifier(nPos, lastItem) ? OperatorType.plusModifier : OperatorType.plusOperator;
            break;
          case '-':
            index = this.IsSignModifier(nPos, lastItem) ? OperatorType.minusModifier : OperatorType.minusOperator;
            break;
          case '/':
            index = OperatorType.divideOperator;
            break;
          case '<':
            index = OperatorType.isLessThanOperator;
            if (nPos < sText.Length - 1)
            {
              switch (sText[nPos + 1])
              {
                case '<':
                  index = OperatorType.shiftLeftOperator;
                  break;
                case '=':
                  index = OperatorType.isLessThanOrEqualToOperator;
                  break;
                case '>':
                  index = OperatorType.isBasicNotEqualToOperator;
                  break;
              }
            }
            else
              break;
            break;
          case '=':
            index = OperatorType.isBasicEqualToOperator;
            if (nPos < sText.Length - 1 && sText[nPos + 1] == '=')
            {
              index = OperatorType.isEqualToOperator;
              break;
            }
            break;
          case '>':
            index = OperatorType.isGreaterThanOperator;
            if (nPos < sText.Length - 1)
            {
              switch (sText[nPos + 1])
              {
                case '=':
                  index = OperatorType.isGreaterThanOrEqualToOperator;
                  break;
                case '>':
                  index = OperatorType.shiftRightOperator;
                  break;
              }
            }
            else
              break;
            break;
          case 'A':
          case 'a':
            if (nPos <= sText.Length - 3 && (nPos < sText.Length - 3 ? (!this.IsIdentifierCharacter(sText[nPos + 3]) ? 1 : 0) : 1) != 0 && string.Compare(sText.Substring(nPos, 3), "and", true) == 0)
            {
              index = OperatorType.andBasicOperator;
              break;
            }
            break;
          case 'N':
          case 'n':
            if (nPos <= sText.Length - 3 && (nPos < sText.Length - 3 ? (!this.IsIdentifierCharacter(sText[nPos + 3]) ? 1 : 0) : 1) != 0 && string.Compare(sText.Substring(nPos, 3), "not", true) == 0)
            {
              index = OperatorType.notBasicOperator;
              break;
            }
            break;
          case 'O':
          case 'o':
            if (nPos <= sText.Length - 2 && (nPos < sText.Length - 2 ? (!this.IsIdentifierCharacter(sText[nPos + 2]) ? 1 : 0) : 1) != 0 && string.Compare(sText.Substring(nPos, 2), "or", true) == 0)
            {
              index = OperatorType.orBasicOperator;
              break;
            }
            break;
          case '^':
            index = OperatorType.powerOperator;
            break;
          case '|':
            index = OperatorType.bitwiseInclusiveOrOperator;
            if (nPos < sText.Length - 1 && sText[nPos + 1] == '|')
            {
              index = OperatorType.orOperator;
              break;
            }
            break;
          case '~':
            index = OperatorType.bitwiseCompliment;
            break;
        }
        if (index != OperatorType.noOperator)
        {
          sItem = Parser._operators[(int) index].Name;
          nPos += sItem.Length - 1;
        }
        nIndex = (int) index;
        return index != OperatorType.noOperator;
      }

      /// <summary>
      /// Returns a value indicating whether the specified string can be converted to DateTime type.
      /// </summary>
      /// <param name="sItem">A string containing a number to convert.</param>
      /// <param name="dtDate">Converted date.</param>
      /// <returns>True, if a specified string can be converted to DateTime type; otherwise false.</returns>
      private bool IsDate(string sItem, ref DateTime dtDate)
      {
        bool flag = true;
        try
        {
          dtDate = Convert.ToDateTime(sItem);
        }
        catch (InvalidCastException ex)
        {
          flag = false;
        }
        return flag;
      }

      /// <summary>
      /// Returns a value indicating whether the specified item name is found in the <see cref="P:Intermech.Expressions.Parser.Functions" /> collection.
      /// </summary>
      /// <param name="sItem">Item name.</param>
      /// <param name="funcIndex">Index in <see cref="P:Intermech.Expressions.Parser.Functions" /> collection.</param>
      /// <param name="itemType">ItemType (Function).</param>
      /// <returns>True, if item name is found in Functions collection; otherwise, false.</returns>
      private bool IsFunction(string sItem, ref int funcIndex, ref ItemType itemType)
      {
        funcIndex = Parser._functions.IndexOf(sItem);
        if (funcIndex <= -1)
          return false;
        itemType = ItemType.Function;
        return true;
      }

      /// <summary>
      /// Returns a value indicating whether the specified symbol can be used in a variable, constant or function names
      /// </summary>
      /// <param name="ch">A Unicode character.</param>
      /// <returns>true, if symbol can be used in variable, constant or function names; otherwise, false.</returns>
      private bool IsIdentifierCharacter(char ch) => this.IsLetter(ch) || char.IsDigit(ch);

      /// <summary>
      /// Returns a value indicating whether the specified symbol is categorized as an alphabetic letter or '_'.
      /// </summary>
      /// <param name="ch">A Unicode character.</param>
      /// <returns>true, if symbol  is categorized as an alphabetic letter or '_'; otherwise, false.</returns>
      private bool IsLetter(char ch) => char.IsLetter(ch) || ch == '_';

      /// <summary>
      /// Returns a value indicating whether the specified string can be converted to Double type.
      /// </summary>
      /// <param name="sItem">A string containing a number to convert.</param>
      /// <param name="dNumber">Converted number.</param>
      /// <returns>True, if a specified string can be converted to Double type; otherwise false.</returns>
      private bool IsNumber(string sItem, out double dNumber)
      {
        return double.TryParse(sItem, NumberStyles.Float, (IFormatProvider) Parser._nfi, out dNumber);
      }

      /// <summary>
      /// Returns a value indicating if occurrence of '+' or '-' character belongs to a number in scientific notation.
      /// </summary>
      /// <param name="sText">Expression string.</param>
      /// <param name="nPos">Current position in an Expression string.</param>
      /// <returns>True, if scientific notation is used; otherwise, false.</returns>
      private bool IsScientificNotation(string sText, int nPos)
      {
        return (nPos < 2 || nPos >= sText.Length - 1 ? 0 : (sText[nPos - 1] != 'e' && sText[nPos - 1] != 'E' || !char.IsDigit(sText[nPos]) && sText[nPos] != '-' && sText[nPos] != '+' ? 0 : (char.IsDigit(sText[nPos - 2]) ? 1 : 0))) != 0;
      }

      /// <summary>
      /// Returns a value indicating whether '+' or '-' is used as unary sign Modifier.
      /// </summary>
      /// <param name="nPos">Current position in an expression string.</param>
      /// <param name="lastItem">Type of the last parsed item.</param>
      /// <returns>True, if '+' or '-' is used as sign Modifier; otherwise, false.</returns>
      private bool IsSignModifier(int nPos, ItemType lastItem)
      {
        return lastItem == ItemType.OpeningParen || lastItem == ItemType.Comma || lastItem == ItemType.Operator;
      }

      /// <summary>
      /// Returns a value indicating whether the specified item name is found in the Variables collection or Constants collection.
      /// </summary>
      /// <param name="name">Item name.</param>
      /// <param name="index">Index in Variables collection or Constants collection.</param>
      /// <param name="itemType">ItemType (Variable or Constant).</param>
      /// <returns>True, if item name is found in Variables collection or Constants collection; otherwise, false.</returns>
      private bool IsVariable(string name, ref int index, ref ItemType itemType)
      {
        if (!this._autoDetectVars)
        {
          index = this._variables.IndexOf(name);
          if (index > -1)
          {
            itemType = ItemType.Variable;
            return true;
          }
        }
        index = Parser._constants.IndexOf(name);
        if (index <= -1)
          return false;
        itemType = ItemType.Constant;
        return true;
      }

      /// <summary>Parses the expression.</summary>
      /// <param name="text">Expression to be parsed.</param>
      /// <returns>ExpressionTree object.</returns>
      /// <exception cref="T:System.Exception"></exception>
      /// <exception cref="T:Intermech.Expressions.Exceptions.ParseException"></exception>
      /// <remarks>This method is thread-safe</remarks>
      public ExpressionTree Parse(string text)
      {
        if (this._autoDetectVars)
          this._variables.Clear();
        return this.Parse(text, false);
      }

      /// <summary>
      /// Parses the expression with "Implicit Multiplication" option.
      /// </summary>
      /// <param name="text">Expression to be parsed.</param>
      /// <param name="implicitMultiplication">Indicates whether implicit multiplication is supported.</param>
      /// <returns>ExpressionTree object.</returns>
      /// <exception cref="T:System.Exception"></exception>
      /// <exception cref="T:Intermech.Expressions.Exceptions.ParseException"></exception>
      private ExpressionTree Parse(string text, bool implicitMultiplication)
      {
        if (text.Trim() == string.Empty)
          return (ExpressionTree) null;
        if (this._useCache && Parser._expressionsCache[(object) text] is ExpressionTree expressionTree1)
          return expressionTree1;
        int nPos = 0;
        int pos = 0;
        string empty = string.Empty;
        int index = 0;
        ItemType itemType = ItemType.Operand;
        ItemType lastType = ItemType.OpeningParen;
        double dNumber = 0.0;
        DateTime dtDate = new DateTime();
        int num = 0;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        this._notation = new ArrayList(0);
        ArrayList stack = new ArrayList(0);
        ArrayList arrayList = new ArrayList(0);
        string str = text.Trim();
        if (str.Length > 0 && str[0] == '@')
        {
          str = this.ParseOldFormatExpression(str);
          if (str.Trim() == string.Empty)
            return (ExpressionTree) null;
        }
        lock (this)
        {
          if (this._variablesCache == null)
            this.RecreateVariablesCache();
        }
        ItemInfo itemInfo;
        do
        {
          char ch = str[nPos];
          if (flag1 && ch != '#')
            empty += ch.ToString();
          else if (flag3 && (ch != ']' || empty.IndexOf('[') != -1 && empty.IndexOf(']') == -1))
            empty += ch.ToString();
          else if (flag2 && (ch == '\'' ? (str[nPos - 1] == '\u0001' ? 1 : 0) : 1) != 0)
          {
            switch (ch)
            {
              case '\t':
              case '\n':
              case '\v':
              case '\f':
              case '\r':
                throw new InvalidCharacterInStringException(nPos, ch);
              default:
                empty += ch.ToString();
                break;
            }
          }
          else if (flag2 && ch == '\'' && nPos < str.Length - 1 && str[nPos + 1] == '\'')
          {
            ++nPos;
            empty += ch.ToString();
          }
          else if (!this.IsLetter(ch))
          {
            if (char.IsDigit(ch))
            {
              empty += ch.ToString();
            }
            else
            {
              FunctionInfo functionInfo;
              switch (ch)
              {
                case '\t':
                case '\n':
                case '\v':
                case '\f':
                case '\r':
                case ' ':
                  if (empty != string.Empty)
                  {
                    if (this.IsVariable(empty, ref index, ref itemType))
                    {
                      if (!this.CanAdd(itemType, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(itemType, index, nPos - empty.Length, this._notation, stack);
                      empty = string.Empty;
                      goto label_145;
                    }
                    if (this.IsNumber(empty, out dNumber))
                    {
                      if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(ItemType.Operand, (object) dNumber, nPos - empty.Length, this._notation);
                      empty = string.Empty;
                      goto label_145;
                    }
                    this.TreatUnknown(empty, ref lastType, nPos, this._notation, stack, implicitMultiplication);
                    empty = string.Empty;
                    goto label_145;
                  }
                  goto label_145;
                case '!':
                case '%':
                case '&':
                case '*':
                case '+':
                case '-':
                case '/':
                case '<':
                case '=':
                case '>':
                case '^':
                case '|':
                case '~':
                  if (empty != string.Empty)
                  {
                    if (this.IsVariable(empty, ref index, ref itemType))
                    {
                      if (!this.CanAdd(itemType, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(itemType, index, nPos - empty.Length, this._notation, stack);
                      empty = string.Empty;
                    }
                    else if (this.IsNumber(empty, out dNumber))
                    {
                      if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(ItemType.Operand, (object) dNumber, nPos - empty.Length, this._notation);
                      empty = string.Empty;
                    }
                    else
                    {
                      if ((ch == '+' || ch == '-') && this.IsScientificNotation(str, nPos))
                      {
                        empty += ch.ToString();
                        goto label_145;
                      }
                      this.TreatUnknown(empty, ref lastType, nPos, this._notation, stack, implicitMultiplication);
                      empty = string.Empty;
                    }
                  }
                  if (this.FindOperator(str, ref empty, ref nPos, ref index, lastType))
                  {
                    if (!this.CanAdd(ItemType.Operator, (int) Parser._operators[index].OperandsSupported, ref lastType))
                      throw new InvalidOperatorLocationException(nPos - empty.Length + 1, empty);
                    this.AddToPolish(ItemType.Operator, index, nPos - empty.Length + 1, this._notation, stack);
                    empty = string.Empty;
                    goto label_145;
                  }
                  goto label_145;
                case '#':
                  if (flag1)
                  {
                    if (!this.IsDate(empty, ref dtDate))
                      throw new InvalidDateException(nPos - empty.Length - 1, empty);
                    if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                      throw new InvalidTermLocationException(nPos - empty.Length - 1, empty);
                    this.AddToPolish(ItemType.Operand, (object) dtDate, nPos - empty.Length - 1, this._notation);
                    empty = string.Empty;
                    flag1 = false;
                    goto label_145;
                  }
                  flag1 = true;
                  goto label_145;
                case '\'':
                  if (flag2)
                  {
                    if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                      throw new InvalidTermLocationException(nPos - empty.Length - 1, empty);
                    this.AddToPolish(ItemType.Operand, (object) empty, nPos - empty.Length - 1, this._notation);
                    empty = string.Empty;
                    flag2 = false;
                    goto label_145;
                  }
                  flag2 = true;
                  goto label_145;
                case '(':
                  ++num;
                  if (empty != string.Empty)
                  {
                    if (this.IsFunction(empty, ref index, ref itemType))
                    {
                      if (!this.CanAdd(itemType, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidFunctionLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(itemType, index, nPos - empty.Length, this._notation, stack);
                      empty = string.Empty;
                      functionInfo.parenCount = num - 1;
                      functionInfo.stackIndex = stack.Count - 1;
                      functionInfo.paramCount = 1;
                      arrayList.Add((object) functionInfo);
                    }
                    else
                    {
                      if (!implicitMultiplication)
                        throw new UnknownFunctionException(nPos - empty.Length, empty);
                      if (this.IsVariable(empty, ref index, ref itemType))
                      {
                        if (!this.CanAdd(itemType, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                          throw new InvalidTermLocationException(nPos - empty.Length, empty);
                        this.AddToPolish(itemType, index, nPos - empty.Length, this._notation, stack);
                        empty = string.Empty;
                      }
                      else
                      {
                        if (!this.IsNumber(empty, out dNumber))
                          throw new UnknownFunctionException(nPos - empty.Length, empty);
                        if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                          throw new InvalidTermLocationException(nPos - empty.Length, empty);
                        this.AddToPolish(ItemType.Operand, (object) dNumber, nPos - empty.Length, this._notation);
                        empty = string.Empty;
                      }
                    }
                  }
                  if (!this.CanAdd(ItemType.OpeningParen, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                    throw new InvalidParenthesisLocationException(nPos - empty.Length);
                  this.AddToPolish(ItemType.OpeningParen, this._notation, stack);
                  pos = nPos + 1;
                  goto label_145;
                case ')':
                  if (empty != string.Empty)
                  {
                    if (this.IsVariable(empty, ref index, ref itemType))
                    {
                      if (!this.CanAdd(itemType, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(itemType, index, nPos - empty.Length, this._notation, stack);
                      empty = string.Empty;
                    }
                    else if (this.IsNumber(empty, out dNumber))
                    {
                      if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(ItemType.Operand, (object) dNumber, nPos - empty.Length, this._notation);
                      empty = string.Empty;
                    }
                    else
                    {
                      this.TreatUnknown(empty, ref lastType, nPos, this._notation, stack, implicitMultiplication);
                      empty = string.Empty;
                    }
                  }
                  if (num == 0)
                    throw new UnbalancedParenthesesException(nPos - empty.Length);
                  --num;
                  if (arrayList.Count > 0)
                  {
                    functionInfo = (FunctionInfo) arrayList[arrayList.Count - 1];
                    if (functionInfo.parenCount == num)
                    {
                      itemInfo = (ItemInfo) stack[functionInfo.stackIndex] with
                      {
                        paramCount = str[nPos - 1] != '(' ? functionInfo.paramCount : 0
                      };
                      if (itemInfo.type == ItemType.Function && !itemInfo.AsFunction.MultArgsSupported(itemInfo.paramCount))
                        throw new WrongArgumentsNumberException(pos, itemInfo.paramCount);
                      stack[functionInfo.stackIndex] = (object) itemInfo;
                      arrayList.RemoveAt(arrayList.Count - 1);
                      if (itemInfo.paramCount == 0)
                      {
                        lastType = ItemType.ClosingParen;
                        this.AddToPolish(ItemType.ClosingParen, this._notation, stack);
                        goto label_145;
                      }
                    }
                  }
                  if (!this.CanAdd(ItemType.ClosingParen, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                    throw new InvalidParenthesisLocationException(nPos - empty.Length);
                  this.AddToPolish(ItemType.ClosingParen, this._notation, stack);
                  goto label_145;
                case ',':
                  if (empty != string.Empty)
                  {
                    if (this.IsVariable(empty, ref index, ref itemType))
                    {
                      if (!this.CanAdd(itemType, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(itemType, index, nPos - empty.Length, this._notation, stack);
                      empty = string.Empty;
                    }
                    else if (this.IsNumber(empty, out dNumber))
                    {
                      if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(ItemType.Operand, (object) dNumber, nPos - empty.Length, this._notation);
                      empty = string.Empty;
                    }
                    else
                    {
                      this.TreatUnknown(empty, ref lastType, nPos, this._notation, stack, implicitMultiplication);
                      empty = string.Empty;
                    }
                  }
                  functionInfo = arrayList.Count > 0 ? (FunctionInfo) arrayList[arrayList.Count - 1] : throw new UnexpectedSymbolException(nPos, ch);
                  ++functionInfo.paramCount;
                  arrayList[arrayList.Count - 1] = (object) functionInfo;
                  if (!this.CanAdd(ItemType.Comma, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                    throw new InvalidCommaLocationException(nPos);
                  this.AddToPolish(ItemType.Comma, this._notation, stack);
                  goto label_145;
                case '.':
                  if (nPos <= 0 || nPos >= str.Length - 1 || !char.IsDigit(str[nPos - 1]) || !char.IsDigit(str[nPos + 1]))
                    throw new UnexpectedSymbolException(nPos, ch);
                  empty += ch.ToString();
                  goto label_145;
                case '[':
                  if (!flag3)
                  {
                    flag3 = true;
                    goto label_145;
                  }
                  break;
                case ']':
                  if (empty != string.Empty)
                  {
                    if (this.IsVariable(empty, ref index, ref itemType))
                    {
                      if (!this.CanAdd(itemType, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                        throw new InvalidTermLocationException(nPos - empty.Length, empty);
                      this.AddToPolish(itemType, index, nPos - empty.Length, this._notation, stack);
                      empty = string.Empty;
                    }
                    else
                    {
                      this.TreatUnknown(empty, ref lastType, nPos, this._notation, stack, implicitMultiplication);
                      empty = string.Empty;
                    }
                    flag3 = false;
                    goto label_145;
                  }
                  break;
              }
              throw new UnexpectedSymbolException(nPos, ch);
            }
          }
          else if (empty != string.Empty)
          {
            if (this.IsNumber(empty, out dNumber) && !this.IsScientificNotation(str, nPos + 1))
            {
              if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
                throw new InvalidTermLocationException(nPos - empty.Length, empty);
              this.AddToPolish(ItemType.Operand, (object) dNumber, nPos - empty.Length, this._notation);
              empty = ch.ToString();
            }
            else
              empty += ch.ToString();
          }
          else if ("AaOoNn".IndexOf(ch) <= -1 || !this.FindOperator(str, ref empty, ref nPos, ref index, lastType))
            empty += ch.ToString();
          else if (!this.CanAdd(ItemType.Operator, (int) Parser._operators[index].OperandsSupported, ref lastType))
          {
            if (this.Functions.IndexOf(empty) == -1)
              throw new InvalidOperatorLocationException(nPos - empty.Length + 1, empty);
          }
          else
          {
            this.AddToPolish(ItemType.Operator, index, nPos - empty.Length + 1, this._notation, stack);
            empty = string.Empty;
          }
    label_145:
          ++nPos;
        }
        while (nPos < str.Length);
        if (flag1)
          throw new MissingSymbolException('#');
        if (flag2)
          throw new MissingSymbolException('\'');
        if (flag3)
          throw new MissingSymbolException(']');
        if (empty != string.Empty)
        {
          if (this.IsVariable(empty, ref index, ref itemType))
          {
            if (!this.CanAdd(itemType, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
              throw new InvalidTermLocationException(nPos - empty.Length, empty);
            this.AddToPolish(itemType, index, nPos - empty.Length, this._notation, stack);
            empty = string.Empty;
          }
          else if (this.IsNumber(empty, out dNumber))
          {
            if (!this.CanAdd(ItemType.Operand, ref lastType, nPos - empty.Length, implicitMultiplication, this._notation, stack))
              throw new InvalidTermLocationException(nPos - empty.Length, empty);
            this.AddToPolish(ItemType.Operand, (object) dNumber, nPos - empty.Length, this._notation);
            empty = string.Empty;
          }
          else
          {
            this.TreatUnknown(empty, ref lastType, nPos, this._notation, stack, implicitMultiplication);
            empty = string.Empty;
          }
        }
        if (num > 0)
          throw new UnbalancedParenthesesException(nPos - empty.Length);
        if (lastType != ItemType.Variable && lastType != ItemType.ClosingParen && lastType != ItemType.Constant && lastType != ItemType.Operand)
          throw new InvalidExpressionException(nPos - empty.Length, empty);
        if (stack.Count > 0)
        {
          do
          {
            itemInfo = (ItemInfo) stack[stack.Count - 1];
            this._notation.Add((object) itemInfo);
            stack.Remove((object) itemInfo);
          }
          while (stack.Count > 0);
        }
        if (this._autoDetectVars)
          this.RecreateVariablesCache();
        ExpressionTree expressionTree2 = new ExpressionTree(this._variablesCache, this._notation);
        int errorPos = -1;
        int invalidArgumentIndex = -1;
        if (this._validate && !expressionTree2.Validate(ref errorPos, ref invalidArgumentIndex))
          throw new InvalidArgumentTypeException(errorPos, invalidArgumentIndex + 1);
        if (this._useCache)
        {
          lock (Parser._expressionsCache)
            Parser._expressionsCache[(object) text] = (object) expressionTree2;
        }
        return expressionTree2;
      }

      private string ParseOldFormatExpression(string input)
      {
        if (string.IsNullOrEmpty(input))
          return input;
        StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
        char ch1 = '\'';
        int num = 1;
        int index = -1;
        ItemType itemType = ItemType.ClosingParen;
        bool flag1 = false;
        bool flag2 = false;
        string empty = string.Empty;
        char[] charArray = input.ToCharArray();
        for (int pos = 1; pos < charArray.Length; ++pos)
        {
          char ch2 = charArray[pos];
          switch (ch2)
          {
            case '{':
              flag2 = !flag2 ? true : throw new InvalidExpressionException(pos, string.Empty);
              num = pos;
              break;
            case '}':
              if (!flag2)
                throw new InvalidExpressionException(pos, string.Empty);
              string prefix;
              string suffix;
              string prefux = this.ExtractPrefux(input.Substring(num + 1, pos - num - 1), out prefix, out suffix);
              if (stringBuilder.Length > 0)
              {
                if (flag1)
                {
                  stringBuilder.Append(ch1);
                  flag1 = false;
                }
                stringBuilder.Append('+');
              }
              if (!this.IsVariable(prefux, ref index, ref itemType))
                throw new UnknownVariableException(num + prefix.Length, prefux);
              string str = !(this._variables[index].Type != typeof (string)) ? $"[{prefux}]" : $"STR([{prefux}])";
              if (prefix.Length == 0 && suffix.Length == 0)
                stringBuilder.Append(str);
              else if (suffix.Length > 0)
                stringBuilder.Append($"VAL('{prefix}',{str},'{suffix}')");
              else
                stringBuilder.Append($"VAL('{prefix}',{str})");
              flag2 = false;
              break;
            default:
              if (!flag2)
              {
                if (!flag1)
                {
                  if (stringBuilder.Length > 0)
                    stringBuilder.Append('+');
                  stringBuilder.Append(ch1);
                  flag1 = true;
                }
                stringBuilder.Append(ch2);
                if ((int) ch2 == (int) ch1)
                {
                  stringBuilder.Append(ch1);
                  break;
                }
                break;
              }
              break;
          }
        }
        if (flag1)
          stringBuilder.Append(ch1);
        return stringBuilder.ToString();
      }

      private string ExtractPrefux(string varName, out string prefix, out string suffix)
      {
        prefix = string.Empty;
        suffix = string.Empty;
        int length = varName.IndexOf('[');
        int num = varName.IndexOf(']');
        if (length != -1 && num != -1)
        {
          if (length != -1)
            prefix = varName.Substring(0, length).Replace("'", "''");
          if (num != -1)
            suffix = varName.Substring(num + 1, varName.Length - num - 1).Replace("'", "''");
          varName = varName.Substring(length + 1, num - length - 1);
        }
        return varName;
      }

      private void TreatUnknown(
        string token,
        ref ItemType lastType,
        int inPos,
        ArrayList notation,
        ArrayList stack,
        bool implicitMultiplication)
      {
        if (!this.CanAdd(ItemType.Variable, ref lastType, inPos - token.Length, implicitMultiplication, notation, stack))
          throw new InvalidTermLocationException(inPos - token.Length, token);
        if (!this._autoDetectVars)
          throw new UnknownVariableException(inPos - token.Length, token);
        int index = this._variables.IndexOf(token);
        if (index == -1)
          index = this._variables.Add(this.OnCreateVariable(token) ?? throw new UnknownVariableException(inPos - token.Length, token));
        this.AddToPolish(ItemType.Variable, index, inPos - token.Length, notation, stack);
      }

      private Variable OnCreateVariable(string name)
      {
        Variable var = new Variable(name, typeof (double));
        if (this.CreateVariable != null)
        {
          VariableEventArgs vea = new VariableEventArgs(name, var);
          this.CreateVariable((object) this, vea);
          var = vea.Variable;
        }
        return var;
      }

      /// <summary>
      /// Creates a "copy snapshot" of Parser.Variables collection.
      /// Stores it in ExpressionVariables collection.
      /// </summary>
      /// <remarks>
      /// A reference to ExpressionVariables collection is then passed to the ExpressionTree object.
      /// </remarks>
      private void RecreateVariablesCache()
      {
        ArrayList arrayList = new ArrayList(this._variables.Count);
        for (int index = 0; index < this._variables.Count; ++index)
          arrayList.Add((object) this._variables[index].Clone());
        this._variablesCache = new ExpressionVariablesCollection((IList) arrayList);
      }

      /// <summary>Returns the collection of constants. Read-only.</summary>
      public ConstantsCollection Constants => Parser._constants;

      /// <summary>Returns the collection of functions. Read-only.</summary>
      public FunctionsCollection Functions => Parser._functions;

      /// <summary>
      /// Returns the collection of built-in operators. Read-only.
      /// </summary>
      public OperatorsCollection Operators => Parser._operators;

      /// <summary>Returns the collection of variables. Read-only.</summary>
      public VariablesCollection Variables => this._variables;

      /// <summary>Использовать автоматическое определение переменных</summary>
      public bool AutoDetectVariables
      {
        get => this._autoDetectVars;
        set => this._autoDetectVars = value;
      }

      public bool Validate
      {
        get => this._validate;
        set => this._validate = value;
      }

      /// <summary>Контекст вычисления. Используется кто как хочет.</summary>
      public object Context
      {
        get => this._context;
        set => this._context = value;
      }

      public bool UseCache
      {
        get => this._useCache;
        set => this._useCache = value;
      }

      public event CreateVariableEventHandler CreateVariable;

      public void Dispose()
      {
        if (this._variables == null)
          return;
        this._variables.Clear();
        this._variables.VariableAdd -= new VariablesCollection.AddEventHandler(this.CheckName);
        this._variables.Changed -= new VariablesCollection.ChangedEventHandler(this.DeleteVariablesCache);
        this._variables = (VariablesCollection) null;
      }
    }
}
