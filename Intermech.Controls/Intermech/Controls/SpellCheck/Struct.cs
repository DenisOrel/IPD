
// Type: Intermech.Controls.SpellCheck.Struct
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Intermech.Controls.SpellCheck;

public sealed class Struct
{
  public static Regex _digitRegex = new Regex("^\\d", RegexOptions.Compiled);
  public static Regex _htmlRegex = new Regex("</[c-g\\d]+>|</[i-o\\d]+>|</[a\\d]+>|</[q-z\\d]+>|<[cg]+[^>]*>|<[i-o]+[^>]*>|<[q-z]+[^>]*>|<[a]+[^>]*>|<(\\[^\\]*\\|'[^']*'|[^'\\>])*>", RegexOptions.None);
  public static Regex _letterRegex = new Regex("\\D", RegexOptions.Compiled);
  public static Regex _spaceRegx = new Regex("[^\\s]+", RegexOptions.Compiled);
  public static Regex _upperRegex = new Regex("[^A-Z]", RegexOptions.Compiled);
  public static Regex _wordEx = new Regex("[A-Za-z0-9_'ёЁА-Яа-я]+", RegexOptions.Compiled);

  internal class AffixEntry
  {
    public string AddCharacters = "";
    public int[] Condition = new int[2001];
    public int ConditionCount;
    public string StripCharacters = "";
  }

  internal class AffixEntryCollection : List<Struct.AffixEntry>
  {
  }

  internal class AffixRule
  {
    public Struct.AffixEntryCollection AffixEntries = new Struct.AffixEntryCollection();
    public bool AllowCombine;
    public string Name = "";
  }

  internal class AffixRuleCollection : Dictionary<string, Struct.AffixRule>
  {
  }

  public class PhoneticRule
  {
    public bool BeginningOnly;
    public int[] Condition = new int[257];
    public int ConditionCount;
    public int ConsumeCount;
    public bool EndOnly;
    public int Priority;
    public bool ReplaceMode;
    public string ReplaceString;
  }

  internal class PhoneticRuleCollection : List<Struct.PhoneticRule>
  {
  }

  public enum TestResult
  {
    UnknownWord,
    WordInBaseDict,
    WordInUserDict,
    WordHasNoLetterSymbol,
    isHtmlTag,
    isMyTag,
  }

  public class Word : IComparable
  {
    public string AffixKeys = "";
    public int EditDistance;
    public int height;
    public int index;
    public string PhoneticCode = "";
    public string text = "";

    public int CompareTo(object obj)
    {
      return this.EditDistance.CompareTo(((Struct.Word) obj).EditDistance);
    }

    public override string ToString() => this.text;
  }
}
