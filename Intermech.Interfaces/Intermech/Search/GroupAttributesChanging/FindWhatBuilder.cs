
// Type: Intermech.Search.GroupAttributesChanging.FindWhatBuilder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Intermech.Search.GroupAttributesChanging
{
    public sealed class FindWhatBuilder
    {
      private static readonly Dictionary<string, string> CirillicLatinSimilarityDictionary = new Dictionary<string, string>()
      {
        {
          "a",
          "(\\u0061|\\u0430)"
        },
        {
          "e",
          "(\\u0065|\\u0435)"
        },
        {
          "k",
          "(\\u006B|\\u043A)"
        },
        {
          "m",
          "(\\u006D|\\u043C)"
        },
        {
          "o",
          "(\\u006F|\\u043E)"
        },
        {
          "c",
          "(\\u0063|\\u0441)"
        },
        {
          "t",
          "(\\u0074|\\u0442)"
        },
        {
          "x",
          "(\\u0078|\\u0445)"
        },
        {
          "A",
          "(\\u0041|\\u0410)"
        },
        {
          "E",
          "(\\u0045|\\u0415)"
        },
        {
          "K",
          "(\\u004B|\\u041A)"
        },
        {
          "M",
          "(\\u004D|\\u041C)"
        },
        {
          "O",
          "(\\u004F|\\u041E)"
        },
        {
          "C",
          "(\\u0043|\\u0421)"
        },
        {
          "T",
          "(\\u0054|\\u0422)"
        },
        {
          "X",
          "(\\u0058|\\u0425)"
        },
        {
          "H",
          "(\\u0048|\\u041D)"
        },
        {
          "а",
          "(\\u0061|\\u0430)"
        },
        {
          "е",
          "(\\u0065|\\u0435)"
        },
        {
          "к",
          "(\\u006B|\\u043A)"
        },
        {
          "м",
          "(\\u006D|\\u043C)"
        },
        {
          "о",
          "(\\u006F|\\u043E)"
        },
        {
          "с",
          "(\\u0063|\\u0441)"
        },
        {
          "т",
          "(\\u0074|\\u0442)"
        },
        {
          "А",
          "(\\u0041|\\u0410)"
        },
        {
          "Е",
          "(\\u0045|\\u0415)"
        },
        {
          "К",
          "(\\u004B|\\u041A)"
        },
        {
          "М",
          "(\\u004D|\\u041C)"
        },
        {
          "О",
          "(\\u004F|\\u041E)"
        },
        {
          "С",
          "(\\u0043|\\u0421)"
        },
        {
          "Т",
          "(\\u0054|\\u0422)"
        },
        {
          "Х",
          "(\\u0058|\\u0425)"
        },
        {
          "Н",
          "(\\u0048|\\u041D)"
        }
      };

      public string Text { get; set; }

      public bool MatchCirillicLatinSimilarity { get; set; }

      public bool MatchCase { get; set; }

      public Regex GetResult()
      {
        string str1 = this.Text ?? string.Empty;
        if (str1 == SpecialCharacters.AnyNumber.Character)
          return new Regex("^.*$", RegexOptions.Multiline | RegexOptions.Compiled);
        if (string.IsNullOrEmpty(str1))
          return new Regex("^$", RegexOptions.Multiline | RegexOptions.Compiled);
        string pattern = str1.Replace("\\", "\\\\").Replace("[", "\\[").Replace("]", "\\]").Replace("{", "\\{").Replace("}", "\\}").Replace(".", "\\.").Replace("^", "\\^").Replace("+", "\\+").Replace("|", "\\|").Replace("(", "\\(").Replace(")", "\\)").Replace(SpecialCharacters.AnyNumber.Character, ".*").Replace(SpecialCharacters.Any.Character, ".").Replace(SpecialCharacters.AnyDigit.Character, "[0-9]{1}").Replace(SpecialCharacters.AnyLetter.Character, "[^0-9_\\s]{1}");
        if (this.MatchCirillicLatinSimilarity)
        {
          string str2 = string.Empty;
          foreach (char ch in pattern)
            str2 = !FindWhatBuilder.CirillicLatinSimilarityDictionary.ContainsKey(ch.ToString()) ? str2 + ch.ToString() : str2 + FindWhatBuilder.CirillicLatinSimilarityDictionary[ch.ToString()];
          pattern = str2;
        }
        return new Regex(pattern, (RegexOptions) ((!this.MatchCase ? 1 : 0) | 8));
      }
    }
}
