// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.Evaluator
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class Evaluator
{
  internal static bool NoneEval(object value, string check) => true;

  internal static bool EqualEval(object value, string check)
  {
    switch (value)
    {
      case null:
        return false;
      case long num1:
        return num1 == Convert.ToInt64(check);
      case int num2:
        return num2 == Convert.ToInt32(check);
      case double num3:
        return num3 == Convert.ToDouble(check);
      case Decimal num4:
        return num4 == Convert.ToDecimal(check);
      default:
        return value.ToString().Equals(check);
    }
  }

  internal static bool GreaterEval(object value, string check)
  {
    switch (value)
    {
      case null:
        return false;
      case long num1:
        return num1 > Convert.ToInt64(check);
      case int num2:
        return num2 > Convert.ToInt32(check);
      case double num3:
        return num3 > Convert.ToDouble(check);
      case Decimal num4:
        return num4 > Convert.ToDecimal(check);
      default:
        return value.ToString().CompareTo(check) > 0;
    }
  }

  internal static bool NotEqualEval(object value, string check)
  {
    return !Evaluator.EqualEval(value, check);
  }

  internal static bool SubstringEval(object value, string check)
  {
    return value != null && value.ToString().Contains(check);
  }

  internal static bool NotSubstringEval(object value, string check)
  {
    return !Evaluator.SubstringEval(value, check);
  }

  internal static bool BetweenEval(object value, string check)
  {
    string[] strArray = check.Split(';');
    return strArray.Length == 2 && !Evaluator.LessEval(value, strArray[0]) && !Evaluator.GreaterEval(value, strArray[1]);
  }

  internal static bool NotBetweenEval(object value, string check)
  {
    string[] strArray = check.Split(';');
    return strArray.Length == 2 && (Evaluator.LessEval(value, strArray[0]) || Evaluator.GreaterEval(value, strArray[1]));
  }

  internal static bool LessOrEqualEval(object value, string check)
  {
    switch (value)
    {
      case null:
        return false;
      case long num1:
        return num1 <= Convert.ToInt64(check);
      case int num2:
        return num2 <= Convert.ToInt32(check);
      case double num3:
        return num3 <= Convert.ToDouble(check);
      case Decimal num4:
        return num4 <= Convert.ToDecimal(check);
      default:
        return value.ToString().CompareTo(check) <= 0;
    }
  }

  internal static bool LessEval(object value, string check)
  {
    switch (value)
    {
      case null:
        return false;
      case long num1:
        return num1 < Convert.ToInt64(check);
      case int num2:
        return num2 < Convert.ToInt32(check);
      case double num3:
        return num3 < Convert.ToDouble(check);
      case Decimal num4:
        return num4 < Convert.ToDecimal(check);
      default:
        return value.ToString().CompareTo(check) < 0;
    }
  }

  internal static bool GreaterOrEqualEval(object value, string check)
  {
    switch (value)
    {
      case null:
        return false;
      case long num1:
        return num1 >= Convert.ToInt64(check);
      case int num2:
        return num2 >= Convert.ToInt32(check);
      case double num3:
        return num3 >= Convert.ToDouble(check);
      case Decimal num4:
        return num4 >= Convert.ToDecimal(check);
      default:
        return value.ToString().CompareTo(check) >= 0;
    }
  }

  internal static bool NotInListEval(object value, string check)
  {
    string str = check;
    char[] chArray = new char[1]{ ';' };
    foreach (string check1 in str.Split(chArray))
    {
      if (Evaluator.EqualEval(value, check1))
        return false;
    }
    return true;
  }

  internal static bool InListEval(object value, string check)
  {
    string str = check;
    char[] chArray = new char[1]{ ';' };
    foreach (string check1 in str.Split(chArray))
    {
      if (Evaluator.EqualEval(value, check1))
        return true;
    }
    return false;
  }
}
