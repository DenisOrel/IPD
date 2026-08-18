
// Type: Intermech.Interfaces.GuidHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text.RegularExpressions;


namespace Intermech.Interfaces
{
    public class GuidHelper
    {
      private static Regex _guidRegex;

      public static string GuidRegexString
      {
        get => "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
      }

      public static Regex GuidRegex
      {
        get
        {
          if (GuidHelper._guidRegex == null)
            GuidHelper._guidRegex = new Regex(GuidHelper.GuidRegexString, RegexOptions.Compiled);
          return GuidHelper._guidRegex;
        }
      }

      public static bool IsGuid(string text)
      {
        return text != null && text.Length != 0 && Guid.TryParse(text, out Guid _);
      }
    }
}
