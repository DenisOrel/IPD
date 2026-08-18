
// Type: Intermech.Interfaces.StringFormula
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Intermech.Interfaces
{
    public static class StringFormula
    {
      private static readonly string ClassifierSign = "{C}";

      /// <summary>Замена соответствующей части шаблона регистрационного номера на значение из классификатора.</summary>
      /// <param name="template">Шаблон регистрационного номера.</param>
      public static string ReplaceClassificatorPart(
        IUserSession session,
        string template,
        long classifierID,
        long objectID)
      {
        if (template.ToUpper().IndexOf(StringFormula.ClassifierSign, StringComparison.Ordinal) >= 0 && classifierID != 0L)
        {
          AttributeValues[] clasificatorAttributes = ((ISelectionsService) session.GetCustomService(typeof (ISelectionsService))).GetObjectClassificator((object) session.SessionGUID, classifierID).GetClasificatorAttributes(objectID);
          if (clasificatorAttributes != null && clasificatorAttributes.Length != 0 && clasificatorAttributes[0].Values != null && clasificatorAttributes[0].Values.Length != 0)
            template = template.Replace(StringFormula.ClassifierSign, Convert.ToString(clasificatorAttributes[0].Values[0], (IFormatProvider) CultureInfo.CurrentCulture));
        }
        return template;
      }

      public static string ReplaceObjectAttributePart(
        IUserSession session,
        string template,
        long objectID)
      {
        Match match = new Regex("\\{@(?<attr>[\\w\\s\\W][^}]{1,})\\}").Match(template);
        string attributeName = match.Groups["attr"].Value;
        if (attributeName != string.Empty)
        {
          object[] valuesByName = session.GetObject(objectID).GetValuesByName(attributeName, false);
          if (valuesByName != null && valuesByName.Length != 0)
            template = template.Replace(match.Value, Convert.ToString(valuesByName[0]));
        }
        return template;
      }

      /// <summary>Замена соответствующей части шаблона регистрационного номера на значение текущей даты.</summary>
      /// <param name="template">Шаблон регистрационного номера.</param>
      public static string ReplaceDatePart(IUserSession session, string template)
      {
        DateTime date = DateTime.UtcNow + session.TimeZoneOffset;
        Match match = new Regex("\\{(?<date>[dDmMyY_\\W\\s]{1,})\\}").Match(template);
        string source = match.Groups["date"].Value;
        if (source != string.Empty)
        {
          StringFormula.DateComponentReplace("[dD]{1,}", date, "d", ref source);
          StringFormula.DateComponentReplace("[mM]{1,}", date, "M", ref source);
          StringFormula.DateComponentReplace("[yY]{1,}", date, "y", ref source);
          template = template.Replace(match.Value, source);
        }
        return template;
      }

      public static void DateComponentReplace(
        string pattern,
        DateTime date,
        string substitute,
        ref string source)
      {
        Match match = new Regex(pattern).Match(source);
        if (!(match.Value != string.Empty))
          return;
        string empty = string.Empty;
        for (int index = 0; index < match.Value.Length; ++index)
          empty += substitute;
        source = source.Replace(match.Value, date.ToString(empty));
      }

      /// <summary>Получить шаблон формулы</summary>
      /// <param name="template">Шаблон регистрационного номера</param>
      /// <returns></returns>
      public static CounterTemplate GetNumberCounterTemplate(string template)
      {
        Match match = StringFormula.GetMatch(template);
        Group group1 = match.Groups["num"];
        Group group2 = match.Groups["start"];
        Group group3 = match.Groups["inc"];
        Group group4 = match.Groups["max"];
        return new CounterTemplate(group1.Value != string.Empty ? group1.Value.Replace('9', '0') : string.Empty, match.Value, group2.Value != string.Empty ? Convert.ToInt32(group2.Value) : 1, group3.Value != string.Empty ? Convert.ToInt32(group3.Value) : 1, group4.Value != string.Empty ? Convert.ToInt64(group4.Value) : long.MaxValue, match.Index, match.Index + match.Length - 1);
      }

      private static Match GetMatch(string template)
      {
        Match match = new Regex("\\{(?<num>9{1,}):?(?<start>\\d{0,}):?(?<inc>\\d{0,}):?(?<max>\\d{0,})\\}").Match(template);
        if (!match.Success)
          match = new Regex("\\%(?<num>9{1,}):?(?<start>\\d{0,}):?(?<inc>\\d{0,}):?(?<max>\\d{0,})\\%").Match(template);
        return match;
      }

      public static bool NumberCounterPresent(string template)
      {
        return StringFormula.GetMatch(template).Success;
      }
    }
}
