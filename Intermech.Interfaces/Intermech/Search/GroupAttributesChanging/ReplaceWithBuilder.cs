
// Type: Intermech.Search.GroupAttributesChanging.ReplaceWithBuilder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Intermech.Search.GroupAttributesChanging
{
    public sealed class ReplaceWithBuilder
    {
      private static readonly Regex CounterRegex = new Regex("\\[(?<capacity>[0-9]{1,4}):(?<startValue>[0-9]{1,4}):(?<step>[0-9]{1,4}):(?<id>[0-9]{1,2})\\]", RegexOptions.Compiled);

      public string ReplaceWithTemplate { get; set; }

      public string CurrentAttributeValue { get; set; }

      public string ReplaceWithAttributeValue { get; set; }

      public CharacterCaseTransformation CharacterCaseTransformation { get; set; }

      public Dictionary<int, Counter> Counters { get; set; }

      public string GetResult()
      {
        string str = string.Empty;
        if (!string.IsNullOrEmpty(this.ReplaceWithTemplate))
        {
          str = this.SetCurrentAttributeValue(this.ReplaceWithTemplate);
          if (this.Counters != null)
            str = this.ApplyCounters(str, this.Counters);
        }
        else if (!string.IsNullOrEmpty(this.ReplaceWithAttributeValue))
          str = this.ReplaceWithAttributeValue;
        return this.ApplyCharacterCaseTransformation(str);
      }

      private string SetCurrentAttributeValue(string replaceWithTemplate)
      {
        return replaceWithTemplate.Replace(SpecialCharacters.CurrentAttributeValue.Character, this.CurrentAttributeValue ?? string.Empty);
      }

      private string ApplyCounters(string replaceWithTemplate, Dictionary<int, Counter> counters)
      {
        return ReplaceWithBuilder.CounterRegex.Replace(replaceWithTemplate, (MatchEvaluator) (o =>
        {
          Counter counterFromMatch = ReplaceWithBuilder.CreateCounterFromMatch(o);
          Counter counter = (Counter) null;
          if (!counters.TryGetValue(counterFromMatch.ID, out counter))
          {
            counters.Add(counterFromMatch.ID, counterFromMatch);
            counter = counterFromMatch;
          }
          string str = counter.ToString();
          counter.Increment();
          return str;
        }));
      }

      private static Counter CreateCounterFromMatch(Match match)
      {
        return new Counter(Convert.ToInt32(match.Groups["id"].Value), Convert.ToInt32(match.Groups["startValue"].Value))
        {
          Capacity = match.Groups["capacity"].Value.Length,
          Step = Convert.ToInt32(match.Groups["step"].Value)
        };
      }

      private string ApplyCharacterCaseTransformation(string replaceWith)
      {
        switch (this.CharacterCaseTransformation)
        {
          case CharacterCaseTransformation.None:
            return replaceWith;
          case CharacterCaseTransformation.LowerCase:
            return replaceWith.ToLowerInvariant();
          case CharacterCaseTransformation.UpperCase:
            return replaceWith.ToUpperInvariant();
          case CharacterCaseTransformation.StartWithCapital:
            if (replaceWith.Length <= 0)
              return replaceWith;
            return replaceWith.Length <= 1 ? replaceWith[0].ToString().ToUpperInvariant() : replaceWith[0].ToString().ToUpperInvariant() + replaceWith.Substring(1).ToLowerInvariant();
          default:
            throw new NotSupportedEnumException((Enum) this.CharacterCaseTransformation);
        }
      }
    }
}
