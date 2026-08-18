// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.StringExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace Intermech.Extensions;

public static class StringExtensions
{
  [NotNull]
  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty([CanBeNull] this string value, [NotNull, NotEmpty] string other)
  {
    return Intermech.Diagnostics.Check.Result.NotNullOrEmpty(string.IsNullOrEmpty(value) ? other : value);
  }

  [NotNull]
  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty([CanBeNull] this string value, [NotNull, NotEmpty] Func<string> getOther)
  {
    return Intermech.Diagnostics.Check.Result.NotNullOrEmpty(string.IsNullOrEmpty(value) ? getOther() : value);
  }

  [NotNull]
  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty([CanBeNull] this string value, [CanBeNull] string other1, [NotNull, NotEmpty] string other2)
  {
    return Intermech.Diagnostics.Check.Result.NotNullOrEmpty(string.IsNullOrEmpty(value) ? (string.IsNullOrEmpty(other1) ? other2 : other1) : value);
  }

  [NotNull]
  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull] this string value,
    [CanBeNull] string other1,
    [CanBeNull] string other2,
    [NotNull, NotEmpty] string other3)
  {
    return Intermech.Diagnostics.Check.Result.NotNullOrEmpty(string.IsNullOrEmpty(value) ? (string.IsNullOrEmpty(other1) ? (string.IsNullOrEmpty(other2) ? other3 : other2) : other1) : value);
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace([CanBeNull] this string value, [NotNull, NotWhitespace] string other)
  {
    return Intermech.Diagnostics.Check.Result.NotNullOrWhitespace(string.IsNullOrWhiteSpace(value) ? other : value);
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace([CanBeNull] this string value, [NotNull, NotWhitespace] Func<string> getOther)
  {
    return Intermech.Diagnostics.Check.Result.NotNullOrWhitespace(string.IsNullOrWhiteSpace(value) ? getOther() : value);
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace([CanBeNull] this string value, [CanBeNull] string other1, [NotNull, NotWhitespace] string other2)
  {
    return Intermech.Diagnostics.Check.Result.NotNullOrWhitespace(string.IsNullOrWhiteSpace(value) ? (string.IsNullOrWhiteSpace(other1) ? other2 : other1) : value);
  }

  [NotNull]
  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull] this string value,
    [CanBeNull] string other1,
    [CanBeNull] string other2,
    [NotNull, NotWhitespace] string other3)
  {
    return Intermech.Diagnostics.Check.Result.NotNullOrWhitespace(string.IsNullOrWhiteSpace(value) ? (string.IsNullOrWhiteSpace(other1) ? (string.IsNullOrWhiteSpace(other2) ? other3 : other2) : other1) : value);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string TrimStart(
    [NotNull] this string input,
    [NotNull] string prefixToRemove,
    StringComparison comparisonType = StringComparison.CurrentCulture)
  {
    return !input.StartsWith(prefixToRemove, comparisonType) ? input : input.Substring(prefixToRemove.Length, input.Length - prefixToRemove.Length);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string TrimEnd(
    [NotNull] this string input,
    [NotNull] string suffixToRemove,
    StringComparison comparisonType = StringComparison.CurrentCulture)
  {
    return !input.EndsWith(suffixToRemove, comparisonType) ? input : input.Substring(0, input.Length - suffixToRemove.Length);
  }

  public static int IndexOfAny([NotNull] this string str, [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr)
  {
    return ((IEnumerable<string>) findStr).Select<string, int>((Func<string, int>) (s => str.IndexOf(s))).Min<int>(-1);
  }

  public static int IndexOfAny([NotNull] this string str, [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr, [CanBeNull] out string foundStr)
  {
    int num1 = -1;
    foundStr = (string) null;
    foreach (string str1 in findStr)
    {
      int num2 = str.IndexOf(str1);
      if (num1 == -1 || num2 < num1)
      {
        num1 = num2;
        foundStr = str1;
      }
    }
    return num1;
  }

  public static int IndexOfAny([NotNull] this string str, [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr, int startIndex)
  {
    return ((IEnumerable<string>) findStr).Select<string, int>((Func<string, int>) (s => str.IndexOf(s, startIndex))).Min<int>(-1);
  }

  public static int IndexOfAny(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr,
    int startIndex,
    [CanBeNull] out string foundStr)
  {
    int num1 = -1;
    foundStr = (string) null;
    foreach (string str1 in findStr)
    {
      int num2 = str.IndexOf(str1, startIndex);
      if (num1 == -1 || num2 < num1)
      {
        num1 = num2;
        foundStr = str1;
      }
    }
    return num1;
  }

  public static int IndexOfAny([NotNull] this string str, [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr, int startIndex, int count)
  {
    return ((IEnumerable<string>) findStr).Select<string, int>((Func<string, int>) (s => str.IndexOf(s, startIndex, count))).Min<int>(-1);
  }

  public static int IndexOfAny(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr,
    int startIndex,
    int count,
    [CanBeNull] out string foundStr)
  {
    int num1 = -1;
    foundStr = (string) null;
    foreach (string str1 in findStr)
    {
      int num2 = str.IndexOf(str1, startIndex, count);
      if (num1 == -1 || num2 < num1)
      {
        num1 = num2;
        foundStr = str1;
      }
    }
    return num1;
  }

  public static int IndexOfAny([NotNull] this string str, [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr, StringComparison comparisonType)
  {
    return ((IEnumerable<string>) findStr).Select<string, int>((Func<string, int>) (s => str.IndexOf(s, comparisonType))).Min<int>(-1);
  }

  public static int IndexOfAny(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr,
    StringComparison comparisonType,
    [CanBeNull] out string foundStr)
  {
    int num1 = -1;
    foundStr = (string) null;
    foreach (string str1 in findStr)
    {
      int num2 = str.IndexOf(str1, comparisonType);
      if (num1 == -1 || num2 < num1)
      {
        num1 = num2;
        foundStr = str1;
      }
    }
    return num1;
  }

  public static int IndexOfAny(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr,
    int startIndex,
    StringComparison comparisonType)
  {
    return ((IEnumerable<string>) findStr).Select<string, int>((Func<string, int>) (s => str.IndexOf(s, startIndex, comparisonType))).Min<int>(-1);
  }

  public static int IndexOfAny(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr,
    int startIndex,
    StringComparison comparisonType,
    [CanBeNull] out string foundStr)
  {
    int num1 = -1;
    foundStr = (string) null;
    foreach (string str1 in findStr)
    {
      int num2 = str.IndexOf(str1, startIndex, comparisonType);
      if (num1 == -1 || num2 < num1)
      {
        num1 = num2;
        foundStr = str1;
      }
    }
    return num1;
  }

  public static int IndexOfAny(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr,
    int startIndex,
    int count,
    StringComparison comparisonType)
  {
    return ((IEnumerable<string>) findStr).Select<string, int>((Func<string, int>) (s => str.IndexOf(s, startIndex, count, comparisonType))).Min<int>(-1);
  }

  public static int IndexOfAny(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] findStr,
    int startIndex,
    int count,
    StringComparison comparisonType,
    [CanBeNull] out string foundStr)
  {
    int num1 = -1;
    foundStr = (string) null;
    foreach (string str1 in findStr)
    {
      int num2 = str.IndexOf(str1, startIndex, count, comparisonType);
      if (num1 == -1 || num2 < num1)
      {
        num1 = num2;
        foundStr = str1;
      }
    }
    return num1;
  }

  [NotNull]
  public static IEnumerable<(string Name, string Value)> ToNameValuesEnumeration(
    [NotNull] this string str,
    [CanBeNull, NotEmpty] string lineDelimiter,
    char nameValueDelimiter = '=',
    StringComparison comparisonType = StringComparison.CurrentCulture)
  {
    lineDelimiter = lineDelimiter ?? Environment.NewLine;
    int startIndex1 = 0;
    int foundIndex;
    for (int length = str.Length; startIndex1 < length && (foundIndex = str.IndexOf(nameValueDelimiter, startIndex1)) >= 0; startIndex1 = foundIndex + lineDelimiter.Length)
    {
      string str1 = str.Substring(startIndex1, foundIndex - startIndex1);
      int startIndex2 = foundIndex + 1;
      foundIndex = str.IndexOf(lineDelimiter, startIndex2, comparisonType);
      string str2 = foundIndex > 0 ? str.Substring(startIndex2, foundIndex - startIndex2) : str.Substring(startIndex2, length - startIndex2);
      yield return (str1, str2);
      if (foundIndex < 0)
        break;
    }
  }

  [NotNull]
  public static IEnumerable<(string Name, string Value)> ToNameValuesEnumeration(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] lineDelimiters,
    char nameValueDelimiter = '=',
    StringComparison comparisonType = StringComparison.CurrentCulture)
  {
    int startIndex1 = 0;
    int length = str.Length;
    int foundIndex;
    while (startIndex1 < length && (foundIndex = str.IndexOf(nameValueDelimiter, startIndex1)) >= 0)
    {
      string str1 = str.Substring(startIndex1, foundIndex - startIndex1);
      int startIndex2 = foundIndex + 1;
      string foundStr;
      foundIndex = str.IndexOfAny(lineDelimiters, startIndex2, comparisonType, out foundStr);
      string str2 = foundIndex > 0 ? str.Substring(startIndex2, foundIndex - startIndex2) : str.Substring(startIndex2, length - startIndex2);
      yield return (str1, str2);
      if (foundIndex < 0)
        break;
      startIndex1 = foundIndex + foundStr.Length;
      foundStr = (string) null;
    }
  }

  [NotNull]
  public static IEnumerable<(string Name, string Value)> ToNameValuesEnumeration(
    [NotNull] this string str,
    [NotNull] char[] lineDelimiters,
    char nameValueDelimiter = '=')
  {
    int startIndex1 = 0;
    int foundIndex;
    for (int length = str.Length; startIndex1 < length && (foundIndex = str.IndexOf(nameValueDelimiter, startIndex1)) >= 0; startIndex1 = foundIndex + 1)
    {
      string str1 = str.Substring(startIndex1, foundIndex - startIndex1);
      int startIndex2 = foundIndex + 1;
      foundIndex = str.IndexOfAny(lineDelimiters, startIndex2);
      string str2 = foundIndex > 0 ? str.Substring(startIndex2, foundIndex - startIndex2) : str.Substring(startIndex2, length - startIndex2);
      yield return (str1, str2);
      if (foundIndex < 0)
        break;
    }
  }

  [NotNull]
  public static IEnumerable<(string Name, string Value)> ToNameValuesEnumeration(
    [NotNull] this string str,
    char lineDelimiter,
    char nameValueDelimiter = '=')
  {
    int startIndex1 = 0;
    int foundIndex;
    for (int length = str.Length; startIndex1 < length && (foundIndex = str.IndexOf(nameValueDelimiter, startIndex1)) >= 0; startIndex1 = foundIndex + 1)
    {
      string str1 = str.Substring(startIndex1, foundIndex - startIndex1);
      int startIndex2 = foundIndex + 1;
      foundIndex = str.IndexOf(lineDelimiter, startIndex2);
      string str2 = foundIndex > 0 ? str.Substring(startIndex2, foundIndex - startIndex2) : str.Substring(startIndex2, length - startIndex2);
      yield return (str1, str2);
      if (foundIndex < 0)
        break;
    }
  }

  [NotNull]
  public static Dictionary<string, string> ToNameValuesDictionary(
    [NotNull] this string str,
    [CanBeNull, NotEmpty] string lineDelimiter = null,
    char nameValueDelimiter = '=',
    StringComparison comparisonType = StringComparison.CurrentCulture)
  {
    lineDelimiter = lineDelimiter ?? Environment.NewLine;
    Dictionary<string, string> valuesDictionary = new Dictionary<string, string>();
    int startIndex1 = 0;
    int num1;
    int num2;
    for (int length = str.Length; startIndex1 < length && (num1 = str.IndexOf(nameValueDelimiter, startIndex1)) >= 0; startIndex1 = num2 + lineDelimiter.Length)
    {
      string key = str.Substring(startIndex1, num1 - startIndex1);
      int startIndex2 = num1 + 1;
      num2 = str.IndexOf(lineDelimiter, startIndex2, comparisonType);
      string str1 = num2 > 0 ? str.Substring(startIndex2, num2 - startIndex2) : str.Substring(startIndex2, length - startIndex2);
      valuesDictionary.Add(key, str1);
      if (num2 < 0)
        break;
    }
    return valuesDictionary;
  }

  [NotNull]
  public static Dictionary<string, string> ToNameValuesDictionary(
    [NotNull] this string str,
    [NotNull, ItemNotNull, ItemNotEmpty, NotEmpty] string[] lineDelimiters,
    char nameValueDelimiter = '=',
    StringComparison comparisonType = StringComparison.CurrentCulture)
  {
    Dictionary<string, string> valuesDictionary = new Dictionary<string, string>();
    int startIndex1 = 0;
    int num1;
    string foundStr;
    int num2;
    for (int length = str.Length; startIndex1 < length && (num1 = str.IndexOf(nameValueDelimiter, startIndex1)) >= 0; startIndex1 = num2 + foundStr.Length)
    {
      string key = str.Substring(startIndex1, num1 - startIndex1);
      int startIndex2 = num1 + 1;
      num2 = str.IndexOfAny(lineDelimiters, startIndex2, comparisonType, out foundStr);
      string str1 = num2 > 0 ? str.Substring(startIndex2, num2 - startIndex2) : str.Substring(startIndex2, length - startIndex2);
      valuesDictionary.Add(key, str1);
      if (num2 < 0)
        break;
    }
    return valuesDictionary;
  }

  [NotNull]
  public static Dictionary<string, string> ToNameValuesDictionary(
    [NotNull] this string str,
    [NotNull] char[] lineDelimiters,
    char nameValueDelimiter = '=')
  {
    Dictionary<string, string> valuesDictionary = new Dictionary<string, string>();
    int startIndex1 = 0;
    int num1;
    int num2;
    for (int length = str.Length; startIndex1 < length && (num1 = str.IndexOf(nameValueDelimiter, startIndex1)) >= 0; startIndex1 = num2 + 1)
    {
      string key = str.Substring(startIndex1, num1 - startIndex1);
      int startIndex2 = num1 + 1;
      num2 = str.IndexOfAny(lineDelimiters, startIndex2);
      string str1 = num2 > 0 ? str.Substring(startIndex2, num2 - startIndex2) : str.Substring(startIndex2, length - startIndex2);
      valuesDictionary.Add(key, str1);
      if (num2 < 0)
        break;
    }
    return valuesDictionary;
  }

  [NotNull]
  public static Dictionary<string, string> ToNameValuesDictionary(
    [NotNull] this string str,
    char lineDelimiter,
    char nameValueDelimiter = '=')
  {
    Dictionary<string, string> valuesDictionary = new Dictionary<string, string>();
    int startIndex1 = 0;
    int num1;
    int num2;
    for (int length = str.Length; startIndex1 < length && (num1 = str.IndexOf(nameValueDelimiter, startIndex1)) >= 0; startIndex1 = num2 + 1)
    {
      string key = str.Substring(startIndex1, num1 - startIndex1);
      int startIndex2 = num1 + 1;
      num2 = str.IndexOf(lineDelimiter, startIndex2);
      string str1 = num2 > 0 ? str.Substring(startIndex2, num2 - startIndex2) : str.Substring(startIndex2, length - startIndex2);
      valuesDictionary.Add(key, str1);
      if (num2 < 0)
        break;
    }
    return valuesDictionary;
  }

  [NotNull]
  public static string ReduceWhitespace([NotNull] this string value)
  {
    if (value.Length == 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder(value.Length);
    bool flag = false;
    for (int index = 0; index < value.Length; ++index)
    {
      if (char.IsWhiteSpace(value[index]))
      {
        if (!flag)
          flag = true;
        else
          continue;
      }
      else
        flag = false;
      stringBuilder.Append(value[index]);
    }
    return stringBuilder.ToString();
  }

  [NotNull]
  [NotEmpty]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string IfNullOrEmpty([CanBeNull] this string value, [NotNull, NotEmpty] string other)
  {
    return string.IsNullOrEmpty(value) ? other : value;
  }

  [NotNull]
  [NotWhitespace]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string IfNullOrWhiteSpace([CanBeNull] this string value, [NotNull, NotWhitespace] string other)
  {
    return string.IsNullOrWhiteSpace(value) ? other : value;
  }

  [CanBeNull]
  [NotWhitespace]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string LimitLength_DeleteRedundantAtStart([CanBeNull] this string value, [ZeroOrPositiveNumber] int lengthLimit)
  {
    if (string.IsNullOrEmpty(value))
      return value;
    int length = value.Length;
    return length > lengthLimit ? value.Substring(length - lengthLimit) : value;
  }
}
