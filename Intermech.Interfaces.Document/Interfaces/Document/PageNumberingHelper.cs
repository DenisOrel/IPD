// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageNumberingHelper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary> Класс для работы с номерами страниц в конструкторских документах </summary>
public sealed class PageNumberingHelper
{
  private const string DIGITS_ONLY_PATTERN = "^(\\d+)$";
  private const string DIGITS_AFTER_DOT_PATTERN = "^(\\d+)\\.(\\d+)$";
  private const string LETTER_AFTER_DIGIT_PATTERN = "^(\\d+)([a-zA-Zа-яА-Я])$";
  private const int CYRILLIC_HARD_SIGN_CODE = 1098;
  private const int CYRILLIC_SOFT_SIGN_CODE = 1100;
  public const int CYRILLIC_a_CODE = 1072;
  public const int CYRILLIC_ja_CODE = 1103;
  private static readonly Regex digitsOnlyRegex = new Regex("^(\\d+)$");

  /// <summary>Определить стиль нумерации страницы</summary>
  public static PageNumExtensionStyle GetNumberingStyle(string pageNumber)
  {
    Regex regex1 = new Regex("^(\\d+)\\.(\\d+)$");
    Regex regex2 = new Regex("^(\\d+)([a-zA-Zа-яА-Я])$");
    if (PageNumberingHelper.digitsOnlyRegex.Match(pageNumber).Success)
      return PageNumExtensionStyle.None;
    if (regex1.Match(pageNumber).Success)
      return PageNumExtensionStyle.DigitsAfterDot;
    return regex2.Match(pageNumber).Success ? PageNumExtensionStyle.Letter : PageNumExtensionStyle.Unknown;
  }

  /// <summary>Возвращает пул номеров для дополнительных страниц</summary>
  public static string[] GetAdditionalPageNumbers(
    string currentPageNumber,
    byte pageCount,
    PageNumExtensionStyle numStyle)
  {
    List<string> stringList = new List<string>();
    if (pageCount != (byte) 0)
    {
      PageNumBuilder pageNumBuilder = PageNumBuilder.Parse(currentPageNumber);
      int mainNumber = pageNumBuilder.MainPart;
      if (numStyle == PageNumExtensionStyle.DigitsAfterDot)
      {
        byte result;
        if (!byte.TryParse(pageNumBuilder.Extension, out result))
          result = (byte) 0;
        stringList.AddRange(Enumerable.Range((int) result + 1, (int) pageCount).Select<int, string>((Func<int, string>) (i => $"{mainNumber}.{i}")));
      }
      if (numStyle == PageNumExtensionStyle.Letter)
      {
        string str = pageNumBuilder.Extension;
        int start = string.IsNullOrWhiteSpace(str) ? 1072 : (int) str[0] + 1;
        int count = start + (int) pageCount - 1 <= 1103 ? (int) pageCount : 1103 - start + 1;
        stringList.AddRange(Enumerable.Range(start, count).Where<int>((Func<int, bool>) (c => c != 1098 && c != 1100)).Select<int, string>((Func<int, string>) (ch => $"{mainNumber}{(ValueType) (char) ch}")));
      }
    }
    return stringList.ToArray();
  }

  public static string ChangeNumberingStyle(string hierarchicalNumber)
  {
    PageNumBuilder pageNumBuilder = PageNumBuilder.Parse(hierarchicalNumber);
    PageNumExtensionStyle numberingStyle = PageNumberingHelper.GetNumberingStyle(hierarchicalNumber);
    switch (numberingStyle)
    {
      case PageNumExtensionStyle.None:
      case PageNumExtensionStyle.Unknown:
        return hierarchicalNumber;
      default:
        int mainPart = pageNumBuilder.MainPart;
        string s = pageNumBuilder.Extension;
        if (numberingStyle == PageNumExtensionStyle.DigitsAfterDot)
        {
          int num = 1072 + (int) byte.Parse(s) - 1;
          switch (num)
          {
            case 1098:
            case 1100:
              ++num;
              break;
          }
          pageNumBuilder.Extension = ((char) num).ToString();
        }
        if (numberingStyle == PageNumExtensionStyle.Letter)
        {
          int num = (int) s[0] - 1072 + 1;
          pageNumBuilder.Extension = num.ToString();
        }
        return pageNumBuilder.ToString();
    }
  }

  /// <summary>
  /// Проверяет, является ли строка допустимым расширением для номера доп. листа
  /// </summary>
  /// <param name="extension">строка расширения</param>
  public static bool IsValidExtension(string @extension)
  {
    if (PageNumberingHelper.IsNumericString(@extension) || @extension.Length == 0)
      return true;
    return @extension.Length == 1 && @extension[0] >= 'а' && @extension[0] <= 'я' && @extension[0] != 'ъ' && @extension[0] != 'ь';
  }

  /// <summary>Проверяет состоит ли строка из десятичных символов</summary>
  /// <param name="text">Строка символов</param>
  public static bool IsNumericString(string text)
  {
    return PageNumberingHelper.digitsOnlyRegex.Match(text).Success;
  }

  /// <summary>
  /// Конвертирует интервал страниц с учетом дополнительных в интервал обычных номеров страниц.
  /// </summary>
  /// <param name="document">Объект документа</param>
  /// <param name="hierarchicalPageNumsRange">интервал страниц</param>
  /// <returns>новый интервал страниц</returns>
  public static string GetPageNumbersForPrinting(
    ImDocumentData document,
    string hierarchicalPageNumsRange)
  {
    if (string.IsNullOrWhiteSpace(hierarchicalPageNumsRange) || document == null || !document.Nodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (n => n is PageData)))
      return "";
    Dictionary<string, string> dictionary = document.Nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (n => n is PageData)).ToDictionary<DocumentTreeNode, string, string>((Func<DocumentTreeNode, string>) (p => ((PageData) p).HierarchicalPageNumber), (Func<DocumentTreeNode, string>) (p => ((PageData) p).PageNumber.ToString()));
    hierarchicalPageNumsRange = $",{hierarchicalPageNumsRange},".Replace(" ", string.Empty);
    string str = hierarchicalPageNumsRange;
    char[] separator = new char[2]{ ',', '-' };
    foreach (string key in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      if (!dictionary.ContainsKey(key))
        return string.Empty;
      hierarchicalPageNumsRange = hierarchicalPageNumsRange.Replace($",{key},", $",!{dictionary[key]},").Replace($"-{key},", $"-!{dictionary[key]},").Replace($",{key}-", $",!{dictionary[key]}-");
    }
    return hierarchicalPageNumsRange.Substring(1, hierarchicalPageNumsRange.Length - 2).Replace("!", string.Empty);
  }
}
