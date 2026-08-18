
// Type: Intermech.Search._GuidHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Intermech.Search
{
    public static class _GuidHelper
    {
      private const string HexNumberRegexAsString = "[0-9a-fA-F]";
      private static readonly Regex[] ValidPrefixesRegexes = _GuidHelper.CreateValidPrefixesRegexes();

      public static bool IsValidPrefix(string prefix)
      {
        return prefix.Length > 0 && prefix.Length <= _GuidHelper.ValidPrefixesRegexes.Length && _GuidHelper.ValidPrefixesRegexes[prefix.Length - 1].IsMatch(prefix);
      }

      private static Regex[] CreateValidPrefixesRegexes()
      {
        List<string> source = new List<string>();
        for (int count = 1; count <= 8; ++count)
          source.Add(_GuidHelper.Repeat("[0-9a-fA-F]", count));
        for (int count = 0; count <= 4; ++count)
          source.Add($"{source[7]}-{_GuidHelper.Repeat("[0-9a-fA-F]", count)}");
        for (int count = 0; count <= 4; ++count)
          source.Add($"{source[12]}-{_GuidHelper.Repeat("[0-9a-fA-F]", count)}");
        for (int count = 0; count <= 4; ++count)
          source.Add($"{source[17]}-{_GuidHelper.Repeat("[0-9a-fA-F]", count)}");
        for (int count = 0; count <= 12; ++count)
          source.Add($"{source[22]}-{_GuidHelper.Repeat("[0-9a-fA-F]", count)}");
        return source.Select<string, Regex>((Func<string, Regex>) (o => new Regex(o, RegexOptions.Compiled))).ToArray<Regex>();
      }

      private static string Repeat(string text, int count)
      {
        string empty = string.Empty;
        for (int index = 0; index < count; ++index)
          empty += text;
        return empty;
      }
    }
}
