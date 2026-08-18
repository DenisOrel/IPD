// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.DelimiterMapping
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.AVS.Output;

/// <summary>Класс модели данных схемы вывода значения атрибута</summary>
public class DelimiterMapping : OutputMappingBase
{
  private static readonly List<DelimiterMapping> _predefined;
  public static readonly DelimiterMapping EmptyStub = new DelimiterMapping("", "");
  private const char NonBreakSpace = ' ';
  private const char WordJoiner = '\u2060';
  public const string NewLine = "\r\n";
  private const string NonBreakDash = "\\_";
  private string _rtf;
  private string _xml;
  public static readonly DelimiterMapping DelimiterNewLine = new DelimiterMapping("\r\n", "(принудительный перенос)");
  public static readonly DelimiterMapping DelimiterSpace = new DelimiterMapping(" ", "(пробел)");

  static DelimiterMapping()
  {
    List<DelimiterMapping> delimiterMappingList = new List<DelimiterMapping>();
    delimiterMappingList.Add(DelimiterMapping.DelimiterSpace);
    char ch1 = ' ';
    string text1 = ch1.ToString();
    ch1 = '\u000E';
    string rtf1 = ch1.ToString();
    delimiterMappingList.Add(new DelimiterMapping(text1, rtf1, "&#0e;", "(неразрывный пробел)"));
    char ch2 = '\u2060';
    string text2 = ch2.ToString();
    ch2 = '\u001C';
    string rtf2 = ch2.ToString();
    delimiterMappingList.Add(new DelimiterMapping(text2, rtf2, "&#1c;", "(без пробела)"));
    delimiterMappingList.Add(DelimiterMapping.DelimiterNewLine);
    delimiterMappingList.Add(new DelimiterMapping(".", ". (точка)"));
    delimiterMappingList.Add(new DelimiterMapping(",", ", (запятая)"));
    delimiterMappingList.Add(new DelimiterMapping("*", "* (звездочка)"));
    delimiterMappingList.Add(new DelimiterMapping("-", "- (минус)"));
    delimiterMappingList.Add(new DelimiterMapping("\\_", '\u0017'.ToString(), "&#17;", "(неразрывный дефис)"));
    DelimiterMapping._predefined = delimiterMappingList;
  }

  public DelimiterMapping(string text, string description)
  {
    this.Delimiter = text;
    this.Description = description;
  }

  public DelimiterMapping(string text)
  {
    this.Delimiter = text;
    this.Description = DelimiterMapping.GetDefaultDescription(text);
  }

  public DelimiterMapping(string text, string rtf, string xml, string description)
  {
    this.Delimiter = text;
    this.Description = description;
    this._rtf = rtf;
    this._xml = xml;
  }

  internal static string GetDefaultDescription(string text)
  {
    return DelimiterMapping.PredefinedDelimiters.FirstOrDefault<DelimiterMapping>(new Func<DelimiterMapping, bool>(matches))?.Description ?? text;

    bool matches(DelimiterMapping d)
    {
      return d.Delimiter.Equals(text, StringComparison.CurrentCultureIgnoreCase) || d.DelimiterRTF.Equals(text, StringComparison.CurrentCultureIgnoreCase) || d.DelimiterXML.Equals(text, StringComparison.CurrentCultureIgnoreCase);
    }
  }

  public static DelimiterMapping Default => new DelimiterMapping(" ", "(пробел)");

  /// <summary>Возвращает список предопределенных разделителей</summary>
  public static List<DelimiterMapping> PredefinedDelimiters => DelimiterMapping._predefined;

  public string Delimiter { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public bool IsEmptyStub => string.IsNullOrEmpty(this.Delimiter + this.Description);

  public string DelimiterRTF
  {
    get => this._rtf ?? this.Delimiter;
    set => this._rtf = value;
  }

  public string DelimiterXML
  {
    get => this._xml ?? this.Delimiter;
    set => this._xml = value;
  }

  /// <summary>
  /// Выдать из списка предопределенных разделителей или создать новый
  /// </summary>
  internal static DelimiterMapping Create(string delimiter)
  {
    return DelimiterMapping._predefined.FirstOrDefault<DelimiterMapping>(new Func<DelimiterMapping, bool>(matches))?.Clone() ?? new DelimiterMapping(delimiter);

    bool matches(DelimiterMapping d)
    {
      return d.Delimiter.Equals(delimiter, StringComparison.CurrentCultureIgnoreCase) || d.DelimiterRTF.Equals(delimiter, StringComparison.CurrentCultureIgnoreCase) || d.DelimiterXML.Equals(delimiter, StringComparison.CurrentCultureIgnoreCase) || d.Description.Equals(delimiter, StringComparison.CurrentCultureIgnoreCase);
    }
  }

  internal DelimiterMapping Clone()
  {
    return new DelimiterMapping(this.Delimiter, this._rtf, this._xml, this.Description);
  }

  public override string ToString() => this.Delimiter;

  internal override XElement ToXML()
  {
    return new XElement((XName) "Mapping", new object[2]
    {
      (object) new XAttribute((XName) "ID", (object) this.DelimiterXML),
      (object) new XAttribute((XName) "Order", (object) this.Order.ToString())
    });
  }

  public override bool Equals(object obj)
  {
    return obj is DelimiterMapping delimiterMapping && this.Delimiter == delimiterMapping.Delimiter;
  }

  public override int GetHashCode()
  {
    return EqualityComparer<string>.Default.GetHashCode(this.Delimiter) - 1941289302;
  }
}
