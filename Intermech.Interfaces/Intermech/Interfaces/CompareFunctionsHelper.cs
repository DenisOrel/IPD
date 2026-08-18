
// Type: Intermech.Interfaces.CompareFunctionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Класс со списком функций сравнения</summary>
    [Serializable]
    public sealed class CompareFunctionsHelper
    {
      /// <summary>Равно</summary>
      public const string fnEQUALS = "EQUALS";
      /// <summary>Больше</summary>
      public const string fnGREATER = "GREATER";
      /// <summary>Больше или равно</summary>
      public const string fnEQUALS_GREATER = "EQUALS_GREATER";
      /// <summary>Меньше</summary>
      public const string fnLESS = "LESS";
      /// <summary>Меньше или равно</summary>
      public const string fnEQUALS_LESS = "EQUALS_LESS";
      /// <summary>Максимальное значение</summary>
      public const string fnMAX = "MAX";
      /// <summary>Минимальное значение</summary>
      public const string fnMIN = "MIN";
      /// <summary>В указанном диапазоне</summary>
      public const string fnIN_BOUNDS = "IN_BOUNDS";
      /// <summary>В указанном диапазоне, включая границы диапазона</summary>
      public const string fnIN_BOUNDS_INC = "IN_BOUNDS_INC";
      /// <summary>В списке значений</summary>
      public const string fnIN_LIST = "IN_LIST";
      /// <summary>Содержит подстроку</summary>
      public const string fnCONTAINS = "CONTAINS";
      /// <summary>Значение задано</summary>
      public const string fnNOTNULL = "NOTNULL";
      /// <summary>Базовая версия объекта</summary>
      public const string fnBASEVERSION = "BASEVERSION";
      /// <summary>
      /// Максимальное количество значений для сравнения в списке для "IN_LIST" = 100
      /// </summary>
      public int limMaxItems = 100;
      /// <summary>Имя функции по умолчанию - NOTNULL</summary>
      public string DefaultFunction = "NOTNULL";
      /// <summary>
      /// Имя функции по умолчанию для правила по умолчанию - NOTNULLs
      /// </summary>
      public string DefaultRuleFunction = "NOTNULL";
      /// <summary>Имя агрегатной функции по умолчанию - MAX</summary>
      public string DefaultAggFunction = "MAX";
      /// <summary>
      /// Имя агрегатной функции по умолчанию для правила по умолчанию - MAX
      /// </summary>
      public string DefaultRuleAggFunction = "MAX";
      /// <summary>Универсальная функция - NOTNULL</summary>
      public string UniversalFunction = "NOTNULL";
      /// <summary>Список пар "Имя функции" = "Индекс"</summary>
      public SortedList Functions = new SortedList();
      /// <summary>Список пар "Имя функции" = "Текстовое описание"</summary>
      public SortedList Names = new SortedList();
      /// <summary>
      /// Список пар "Имя функции" = "Минимальное количество аргументов"
      /// </summary>
      public SortedList MinArguments = new SortedList();
      /// <summary>
      /// Список пар "Имя функции" = "Максимальное количество аргументов"
      /// </summary>
      public SortedList MaxArguments = new SortedList();
      /// <summary>Список пар "Имя функции" = "Оператор запроса к ядру"</summary>
      public SortedList RelationalOperator = new SortedList();
      /// <summary>Список непустых обязательных атрибутов объектов</summary>
      private static List<int> notEmptyAttributes = new List<int>((IEnumerable<int>) new int[14]
      {
        -5,
        -58,
        -16,
        -12,
        -3,
        -4,
        -9,
        -15,
        -10,
        -13,
        -18,
        -2,
        -7,
        -8
      });

      /// <summary>
      /// Создать и инициализировать экземпляр класса с функциями сравнения
      /// </summary>
      public CompareFunctionsHelper()
      {
        this.Functions[(object) "BASEVERSION"] = (object) -3;
        this.Functions[(object) "MAX"] = (object) -2;
        this.Functions[(object) "MIN"] = (object) -1;
        this.Functions[(object) "EQUALS"] = (object) 0;
        this.Functions[(object) "GREATER"] = (object) 2;
        this.Functions[(object) "EQUALS_GREATER"] = (object) 3;
        this.Functions[(object) "LESS"] = (object) 4;
        this.Functions[(object) "EQUALS_LESS"] = (object) 5;
        this.Functions[(object) "IN_BOUNDS"] = (object) 100;
        this.Functions[(object) "IN_BOUNDS_INC"] = (object) 101;
        this.Functions[(object) "IN_LIST"] = (object) 200;
        this.Functions[(object) "CONTAINS"] = (object) 300;
        this.Functions[(object) "NOTNULL"] = (object) 10000;
        this.Names[(object) "EQUALS"] = (object) LocalizationHolder.rm.GetString("Interfaces_529");
        this.Names[(object) "GREATER"] = (object) LocalizationHolder.rm.GetString("Interfaces_530");
        this.Names[(object) "EQUALS_GREATER"] = (object) LocalizationHolder.rm.GetString("Interfaces_531");
        this.Names[(object) "LESS"] = (object) LocalizationHolder.rm.GetString("Interfaces_532");
        this.Names[(object) "EQUALS_LESS"] = (object) LocalizationHolder.rm.GetString("Interfaces_533");
        this.Names[(object) "BASEVERSION"] = (object) LocalizationHolder.rm.GetString("Interfaces_612");
        this.Names[(object) "MAX"] = (object) LocalizationHolder.rm.GetString("Interfaces_534");
        this.Names[(object) "MIN"] = (object) LocalizationHolder.rm.GetString("Interfaces_535");
        this.Names[(object) "IN_BOUNDS"] = (object) LocalizationHolder.rm.GetString("Interfaces_536");
        this.Names[(object) "IN_BOUNDS_INC"] = (object) LocalizationHolder.rm.GetString("Interfaces_537");
        this.Names[(object) "IN_LIST"] = (object) LocalizationHolder.rm.GetString("Interfaces_538");
        this.Names[(object) "CONTAINS"] = (object) LocalizationHolder.rm.GetString("Interfaces_539");
        this.Names[(object) "NOTNULL"] = (object) LocalizationHolder.rm.GetString("Interfaces_540");
        this.MinArguments[(object) "EQUALS"] = (object) 1;
        this.MinArguments[(object) "GREATER"] = (object) 1;
        this.MinArguments[(object) "EQUALS_GREATER"] = (object) 1;
        this.MinArguments[(object) "LESS"] = (object) 1;
        this.MinArguments[(object) "EQUALS_LESS"] = (object) 1;
        this.MinArguments[(object) "MAX"] = (object) 0;
        this.MinArguments[(object) "MIN"] = (object) 0;
        this.MinArguments[(object) "BASEVERSION"] = (object) 0;
        this.MinArguments[(object) "IN_BOUNDS"] = (object) 2;
        this.MinArguments[(object) "IN_BOUNDS_INC"] = (object) 2;
        this.MinArguments[(object) "IN_LIST"] = (object) 1;
        this.MinArguments[(object) "CONTAINS"] = (object) 1;
        this.MinArguments[(object) "NOTNULL"] = (object) 0;
        this.MaxArguments[(object) "EQUALS"] = (object) 1;
        this.MaxArguments[(object) "GREATER"] = (object) 1;
        this.MaxArguments[(object) "EQUALS_GREATER"] = (object) 1;
        this.MaxArguments[(object) "LESS"] = (object) 1;
        this.MaxArguments[(object) "EQUALS_LESS"] = (object) 1;
        this.MaxArguments[(object) "MAX"] = (object) 0;
        this.MaxArguments[(object) "MIN"] = (object) 0;
        this.MaxArguments[(object) "BASEVERSION"] = (object) 0;
        this.MaxArguments[(object) "IN_BOUNDS"] = (object) 2;
        this.MaxArguments[(object) "IN_BOUNDS_INC"] = (object) 2;
        this.MaxArguments[(object) "IN_LIST"] = (object) this.limMaxItems;
        this.MaxArguments[(object) "CONTAINS"] = (object) 1;
        this.MaxArguments[(object) "NOTNULL"] = (object) 0;
        this.RelationalOperator[(object) "EQUALS"] = (object) RelationalOperators.Equal;
        this.RelationalOperator[(object) "GREATER"] = (object) RelationalOperators.Greater;
        this.RelationalOperator[(object) "EQUALS_GREATER"] = (object) RelationalOperators.GreaterOrEqual;
        this.RelationalOperator[(object) "LESS"] = (object) RelationalOperators.Less;
        this.RelationalOperator[(object) "EQUALS_LESS"] = (object) RelationalOperators.LessOrEqual;
        this.RelationalOperator[(object) "BASEVERSION"] = (object) RelationalOperators.Equal;
        this.RelationalOperator[(object) "MAX"] = (object) RelationalOperators.None;
        this.RelationalOperator[(object) "MIN"] = (object) RelationalOperators.None;
        this.RelationalOperator[(object) "IN_BOUNDS"] = (object) RelationalOperators.None;
        this.RelationalOperator[(object) "IN_BOUNDS_INC"] = (object) RelationalOperators.Between;
        this.RelationalOperator[(object) "IN_LIST"] = (object) RelationalOperators.In;
        this.RelationalOperator[(object) "CONTAINS"] = (object) RelationalOperators.Substring;
        this.RelationalOperator[(object) "NOTNULL"] = (object) RelationalOperators.NotEmpty;
        this.RelationalOperator[(object) "!EQUALS"] = (object) RelationalOperators.NotEqual;
        this.RelationalOperator[(object) "!GREATER"] = (object) RelationalOperators.LessOrEqual;
        this.RelationalOperator[(object) "!EQUALS_GREATER"] = (object) RelationalOperators.Less;
        this.RelationalOperator[(object) "!LESS"] = (object) RelationalOperators.GreaterOrEqual;
        this.RelationalOperator[(object) "!EQUALS_LESS"] = (object) RelationalOperators.Greater;
        this.RelationalOperator[(object) "!BASEVERSION"] = (object) RelationalOperators.NotEqual;
        this.RelationalOperator[(object) "!MAX"] = (object) RelationalOperators.None;
        this.RelationalOperator[(object) "!MIN"] = (object) RelationalOperators.None;
        this.RelationalOperator[(object) "!IN_BOUNDS"] = (object) RelationalOperators.None;
        this.RelationalOperator[(object) "!IN_BOUNDS_INC"] = (object) RelationalOperators.NotBetween;
        this.RelationalOperator[(object) "!IN_LIST"] = (object) RelationalOperators.NotIn;
        this.RelationalOperator[(object) "!CONTAINS"] = (object) RelationalOperators.NotSubstring;
        this.RelationalOperator[(object) "!NOTNULL"] = (object) RelationalOperators.NotExistsOrEmpty;
      }

      /// <summary>По описанию функции получить её имя</summary>
      /// <param name="FuncDescr">Описание функции</param>
      /// <returns>Имя функции</returns>
      public string GetFunctionName(string FuncDescr)
      {
        return !this.Names.ContainsValue((object) FuncDescr) ? string.Empty : this.Names.GetKey(this.Names.IndexOfValue((object) FuncDescr)).ToString();
      }

      /// <summary>
      /// Проверить, совместима ли  функция FunctionName с указанным типом данных DataType
      /// </summary>
      /// <param name="FunctionName">Функция сравнения</param>
      /// <param name="DataType">Тип данных</param>
      /// <param name="IsListValue">Является ли исследуемый атрибут списковым</param>
      /// <returns></returns>
      public bool IsCompatible(string FunctionName, FieldTypes DataType, bool IsListValue)
      {
        if (DataType == FieldTypes.ftUnknown)
          return false;
        object function = this.Functions[(object) FunctionName];
        if (function == null)
          return false;
        try
        {
          int int32 = Convert.ToInt32(function);
          if (int32 >= 10000)
            return true;
          if (DataType == FieldTypes.ftBoolean)
            return int32 == 0;
          if (IsListValue)
            return int32 == 0 || int32 == 200;
          return int32 == 300 ? DataType == FieldTypes.ftString : MyAttributeHelper.IsSimpleType(DataType) || int32 >= 10000;
        }
        catch
        {
          return false;
        }
      }

      /// <summary>
      /// Метод возвращает массив со всеми именами или описаниями функций
      /// </summary>
      /// <param name="CopyNames">true - копировать в массив имена функций, false - их описания</param>
      /// <param name="includeAggregate">true - добавлять в список агрегатные функции</param>
      /// <returns>Массив со всеми именами функций</returns>
      public object[] GetMembers(bool CopyNames, bool includeAggregate)
      {
        if (this.Functions.Count <= 0 || this.Names.Count <= 0 || this.MinArguments.Count <= 0 || this.MaxArguments.Count <= 0)
          return (object[]) null;
        List<object> objectList = new List<object>();
        for (int index = 0; index < this.Functions.Count; ++index)
        {
          string key = this.Functions.GetKey(index).ToString();
          if (includeAggregate || !this.IsAggregate(key))
          {
            if (CopyNames)
              objectList.Add((object) key);
            else
              objectList.Add((object) this.Names[(object) key].ToString());
          }
        }
        return objectList.ToArray();
      }

      /// <summary>
      /// Метод возвращает массив со всеми именами или описаниями функций для определённого типа данных
      /// </summary>
      /// <param name="CopyNames">true - копировать в массив имена функций, false - их описания</param>
      /// <param name="DataType">Определённый тип данных</param>
      /// <param name="IsListValue">Является ли исследуемый атрибут списковым</param>
      /// <param name="includeAggregate">true - добавлять в список агрегатные функции</param>
      /// <returns>Массив со всеми именами функций</returns>
      public object[] GetMembers(
        bool CopyNames,
        FieldTypes DataType,
        bool IsListValue,
        bool includeAggregate)
      {
        if (this.Functions.Count <= 0 || this.Names.Count <= 0 || this.MinArguments.Count <= 0 || this.MaxArguments.Count <= 0)
          return (object[]) null;
        SortedList sortedList = new SortedList();
        if (CopyNames)
        {
          for (int index = 0; index < this.Names.Count; ++index)
          {
            if ((includeAggregate || !this.IsAggregate(this.Names.GetKey(index).ToString())) && this.IsCompatible(this.Names.GetKey(index).ToString(), DataType, IsListValue))
              sortedList.Add(this.Names.GetKey(index), (object) index);
          }
        }
        else
        {
          for (int index = 0; index < this.Names.Count; ++index)
          {
            if ((includeAggregate || !this.IsAggregate(this.Names.GetKey(index).ToString())) && this.IsCompatible(this.Names.GetKey(index).ToString(), DataType, IsListValue))
              sortedList.Add(this.Names.GetByIndex(index), (object) index);
          }
        }
        object[] members = new object[sortedList.Count];
        sortedList.Keys.CopyTo((Array) members, 0);
        return members;
      }

      /// <summary>Проверить наличие значения в списке функций сравнения</summary>
      /// <param name="value">Функция сравнения</param>
      /// <returns>true, если значение принадлежит списку функций</returns>
      public bool IsMember(string value) => this.Functions.ContainsKey((object) value);

      /// <summary>Получить агрегатные функции</summary>
      /// <param name="CopyNames">true - копировать в массив имена функций, false - их описания</param>
      /// <param name="onlyBaseVersion">true - в список попадёт только "Базовая версия"</param>
      /// <returns>Список функций или их описаний</returns>
      public object[] GetAggregateFunctions(bool CopyNames, bool onlyBaseVersion)
      {
        if (this.Functions.Count <= 0 || this.Names.Count <= 0 || this.MinArguments.Count <= 0 || this.MaxArguments.Count <= 0)
          return (object[]) null;
        List<object> objectList = new List<object>();
        for (int index = 0; index < this.Functions.Count; ++index)
        {
          string key = this.Functions.GetKey(index).ToString();
          if (this.IsAggregate(key) && (!onlyBaseVersion || !(key != "BASEVERSION")))
          {
            if (CopyNames)
              objectList.Add((object) key);
            else
              objectList.Add((object) this.Names[(object) key].ToString());
          }
        }
        return objectList.ToArray();
      }

      /// <summary>
      /// Получить список типов данных, которые совместимы со списком агрегатных функций
      /// </summary>
      /// <returns>Список типов данных, которые совместимы со списком агрегатных функций</returns>
      public List<FieldTypes> GetAggregateFieldTypes()
      {
        List<FieldTypes> aggregateFieldTypes = new List<FieldTypes>();
        object[] aggregateFunctions = this.GetAggregateFunctions(true, false);
        Array values = Enum.GetValues(typeof (FieldTypes));
        for (int index1 = 0; index1 < aggregateFunctions.Length; ++index1)
        {
          for (int index2 = 0; index2 < values.Length; ++index2)
          {
            FieldTypes DataType = (FieldTypes) values.GetValue(index2);
            switch (DataType)
            {
              case FieldTypes.ftUnknown:
              case FieldTypes.ftSystem:
                continue;
              default:
                if (this.IsCompatible(aggregateFunctions[index1].ToString(), DataType, false) && aggregateFieldTypes.IndexOf((FieldTypes) values.GetValue(index2)) < 0)
                {
                  aggregateFieldTypes.Add((FieldTypes) values.GetValue(index2));
                  continue;
                }
                continue;
            }
          }
        }
        return aggregateFieldTypes;
      }

      /// <summary>Проверить, является ли функция агретагной</summary>
      /// <param name="value">Имя функции сравнения</param>
      /// <returns>true, если указанная функция сравнения является агрегатной</returns>
      public bool IsAggregate(string value) => Convert.ToInt32(this.Functions[(object) value]) < 0;

      /// <summary>Проверить, годна ли функция для логических величин</summary>
      /// <param name="value">Имя функции сравнения</param>
      /// <returns>true, если указанная функция сравнения является агрегатной</returns>
      public bool IsBool(string value)
      {
        return Convert.ToInt32(this.Functions[(object) value]) == 0 || Convert.ToInt32(this.Functions[(object) value]) >= 10000;
      }

      /// <summary>
      /// Проверяет, можно ли применить к результату функции логическую операцию отрицания
      /// </summary>
      /// <param name="value">Имя функции сравнения</param>
      /// <returns>true, если можно к функции применять логическую операцию отрицания</returns>
      public bool CanBeNegative(string value) => !this.IsAggregate(value);

      /// <summary>
      /// Вернуть минимально допустимое количество аргументов для указанной функции сравнения
      /// </summary>
      /// <param name="value">Имя функции сравнения</param>
      /// <returns></returns>
      public int MinComparableValues(string value)
      {
        object minArgument = this.MinArguments[(object) value];
        return minArgument == null ? 0 : Convert.ToInt32(minArgument);
      }

      /// <summary>
      /// Вернуть максимально допустимое количество аргументов для указанной функции сравнения
      /// </summary>
      /// <param name="value">Имя функции сравнения</param>
      /// <returns></returns>
      public int MaxComparableValues(string value)
      {
        object maxArgument = this.MaxArguments[(object) value];
        return maxArgument == null ? 0 : Convert.ToInt32(maxArgument);
      }

      /// <summary>
      /// Функция сравнения "EQUALS"
      /// Выполнить проверку равенства двух объектов
      /// </summary>
      /// <param name="Value1">Значение первое</param>
      /// <param name="Value2">Значение второе</param>
      /// <param name="type1">Тип данных первой величины</param>
      /// <param name="type2">Тип данных второй величины</param>
      /// <returns>true, если объекты равны</returns>
      public static bool ObjValues_EQUALS(
        ref object Value1,
        ref object Value2,
        FieldTypes type1,
        FieldTypes type2)
      {
        if (Value1 == null && Value2 == null)
          return true;
        if (Value1 != null && Value2 == null || Value1 == null && Value2 != null)
          return false;
        if (type1 == FieldTypes.ftMeasured)
        {
          if (type2 == FieldTypes.ftMeasured)
          {
            try
            {
              return MeasureHelper.Compare(MeasureHelper.ConvertToMeasuredValue(Value1.ToString()), MeasureHelper.ConvertToMeasuredValue(Value2.ToString())) == CompareResult.Equal;
            }
            catch
            {
              return false;
            }
          }
        }
        DateTime result1;
        DateTime result2;
        if (Value1.GetType() == typeof (DateTime) && Value2.GetType() == typeof (DateTime) && DateTime.TryParse(Value1.ToString(), out result1) && DateTime.TryParse(Value2.ToString(), out result2))
          return result1.Date == result2.Date;
        if (Value1.GetType() == typeof (string) && Value2.GetType() == typeof (string))
          return Value1.ToString().Trim().Equals(Value2.ToString().Trim(), StringComparison.OrdinalIgnoreCase);
        long result3;
        long result4;
        return type1 == FieldTypes.ftInteger && type2 == FieldTypes.ftInteger && long.TryParse(Value1.ToString(), out result3) && long.TryParse(Value2.ToString(), out result4) ? result3.Equals(result4) : Value1.Equals(Value2);
      }

      /// <summary>
      /// Проверить, содержит ли указанный объект в своём составе другой объект (как подстроку)
      /// Если одно из значений равно null, вернёт false.
      /// </summary>
      /// <param name="Value1">Значение первое</param>
      /// <param name="Value2">Значение второе</param>
      /// <returns>true, если Value2 входит в Value1 (как подстрока). Если одно из значений равно null, вернёт 0.</returns>
      public static bool ObjValues_CONTAINS(ref object Value1, ref object Value2)
      {
        return Value1 != null && Value2 != null && Convert.ToString(Value1).Trim().ToUpper().Contains(Convert.ToString(Value2).Trim().ToUpper());
      }

      /// <summary>
      /// Сравнение двух объектов
      /// Вернёт -1, если Value1 меньше Value2, 0, если Value1 = Value2, 1, если Value1 больше Value2.
      /// Если одно из значений равно null, вернёт 0.
      /// </summary>
      /// <param name="Value1">Значение первое</param>
      /// <param name="Value2">Значение второе</param>
      /// <param name="type1">Тип данных первой величины</param>
      /// <param name="type2">Тип данных второй величины</param>
      /// <returns>Вернёт одно из значений CompareResult. Если одно из значений равно null, вернёт CompareResult.NotCompatible.</returns>
      public static CompareResult ObjValues_COMPARE(
        ref object Value1,
        ref object Value2,
        FieldTypes type1,
        FieldTypes type2)
      {
        if (Value1 == null || Value2 == null)
          return CompareResult.NotCompatible;
        string s1 = Convert.ToString(Value1).Trim();
        string s2 = Convert.ToString(Value2).Trim();
        if (type1 == FieldTypes.ftMeasured)
        {
          if (type2 == FieldTypes.ftMeasured)
          {
            try
            {
              return MeasureHelper.Compare(MeasureHelper.ConvertToMeasuredValue(Value1.ToString()), MeasureHelper.ConvertToMeasuredValue(Value2.ToString()));
            }
            catch
            {
            }
            return CompareResult.NotCompatible;
          }
        }
        if (Value1.GetType() == typeof (string) && Value1.GetType() == typeof (string))
          return MeasureHelper.IntToCompareResult(s1.ToUpper().CompareTo(s2.ToUpper()));
        long result1;
        long result2;
        if (type1 == FieldTypes.ftInteger && type2 == FieldTypes.ftInteger && long.TryParse(s1, out result1) && long.TryParse(s2, out result2))
        {
          if (result1 > result2)
            return CompareResult.More;
          return result1 < result2 ? CompareResult.Less : CompareResult.Equal;
        }
        DateTime result3;
        DateTime result4;
        if (Value1.GetType() == typeof (DateTime) && Value2.GetType() == typeof (DateTime) && DateTime.TryParse(s1, out result3) && DateTime.TryParse(s2, out result4))
        {
          if (result3.Date > result4.Date)
            return CompareResult.More;
          return result3.Date < result4.Date ? CompareResult.Less : CompareResult.Equal;
        }
        double result5;
        double result6;
        if (Value1.GetType() == typeof (double) && Value2.GetType() == typeof (double) && double.TryParse(s1, out result5) && double.TryParse(s2, out result6))
        {
          if (result5 > result6)
            return CompareResult.More;
          return result5 < result6 ? CompareResult.Less : CompareResult.Equal;
        }
        float result7;
        float result8;
        if (Value1.GetType() == typeof (float) && Value2.GetType() == typeof (float) && float.TryParse(s1, out result7) && float.TryParse(s2, out result8))
        {
          if ((double) result7 > (double) result8)
            return CompareResult.More;
          return (double) result7 < (double) result8 ? CompareResult.Less : CompareResult.Equal;
        }
        Decimal result9;
        Decimal result10;
        if (!(Value1.GetType() == typeof (Decimal)) || !(Value2.GetType() == typeof (Decimal)) || !Decimal.TryParse(s1, out result9) || !Decimal.TryParse(s2, out result10))
          return MeasureHelper.IntToCompareResult(s1.ToUpper().CompareTo(s2.ToUpper()));
        if (result9 > result10)
          return CompareResult.More;
        return result9 < result10 ? CompareResult.Less : CompareResult.Equal;
      }

      /// <summary>
      /// Преобразовать функцию сравнения в соответсвующий оператор запроса к ядру
      /// </summary>
      /// <param name="compareFunction">Функция сравнения</param>
      /// <returns>Соответствующий оператор запроса к ядру</returns>
      public RelationalOperators FunctionToRelationalOperator(string compareFunction)
      {
        return string.IsNullOrEmpty(compareFunction) || this.RelationalOperator.IndexOfKey((object) compareFunction) < 0 ? RelationalOperators.None : (RelationalOperators) this.RelationalOperator[(object) compareFunction];
      }

      /// <summary>
      /// Преобразовать критерий сравнения в условия запроса к ядру
      /// </summary>
      /// <param name="criterion">Критерий сравнения версий объектов</param>
      /// <returns>Условия запроса к ядру</returns>
      public ConditionStructure CriterionToConditionStructure(VersionsRuleCriterion criterion)
      {
        if (criterion == null)
          return ConditionStructure.Empty;
        string compareFunction = criterion.CompareFunction;
        if (criterion.Negation)
          compareFunction = "!" + compareFunction;
        RelationalOperators relationalOperator = this.FunctionToRelationalOperator(compareFunction);
        if (relationalOperator == RelationalOperators.None)
          return ConditionStructure.Empty;
        int attributeID = criterion.MainAttribute.Attribute.AttrID;
        AttributeSourceTypes attributeSource = AttributeSourceTypes.Object;
        object conditionValue = (object) null;
        if (criterion.ComparableValues.Count > 0 && criterion.ComparableValues[0].ValueType != "ATTRIBUTE")
          conditionValue = criterion.ComparableValues[0].Value;
        object conditionValue2 = (object) null;
        if (criterion.ComparableValues.Count > 1 && criterion.ComparableValues[criterion.ComparableValues.Count - 1].ValueType != "ATTRIBUTE")
          conditionValue2 = criterion.ComparableValues[criterion.ComparableValues.Count - 1].Value;
        List<object> objectList = new List<object>();
        if (criterion.ComparableValues.Count > 0)
        {
          for (int index = 0; index < criterion.ComparableValues.Count; ++index)
          {
            if (criterion.ComparableValues[index].ValueType != "ATTRIBUTE")
              objectList.Add(criterion.ComparableValues[index].Value);
          }
        }
        if (compareFunction == "BASEVERSION")
        {
          conditionValue = (object) 1L;
          attributeID = -16;
        }
        if (objectList.Count > 0 && conditionValue == null || objectList.Count != criterion.ComparableValues.Count)
          return ConditionStructure.Empty;
        LogicalOperators logicalOperator = LogicalOperators.OR;
        if (criterion.BoolFunction == "AND")
          logicalOperator = LogicalOperators.AND;
        if (criterion.BoolFunction == "NOP")
          logicalOperator = LogicalOperators.NONE;
        int groupID = 0;
        bool caseSensitive = true;
        switch (relationalOperator)
        {
          case RelationalOperators.NotEmpty:
            return CompareFunctionsHelper.notEmptyAttributes.IndexOf(attributeID) >= 0 ? ConditionStructure.Empty : new ConditionStructure(attributeID, relationalOperator, conditionValue, conditionValue2, logicalOperator, groupID, caseSensitive, attributeSource, ColumnContents.Text);
          case RelationalOperators.Equal:
          case RelationalOperators.Greater:
          case RelationalOperators.GreaterOrEqual:
          case RelationalOperators.Less:
          case RelationalOperators.LessOrEqual:
          case RelationalOperators.Substring:
          case RelationalOperators.Between:
            return new ConditionStructure(attributeID, relationalOperator, conditionValue, conditionValue2, logicalOperator, groupID, caseSensitive, attributeSource, ColumnContents.Text);
          case RelationalOperators.In:
            return new ConditionStructure(attributeID, relationalOperator, (object) objectList.ToArray(), (object) null, logicalOperator, groupID, caseSensitive, attributeSource, ColumnContents.Text);
          default:
            return ConditionStructure.Empty;
        }
      }
    }
}
