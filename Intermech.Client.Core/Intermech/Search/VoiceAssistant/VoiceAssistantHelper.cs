
// Type: Intermech.Search.VoiceAssistant.VoiceAssistantHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Intermech.Search.VoiceAssistant;

public static class VoiceAssistantHelper
{
  private static readonly Regex NotLettersAndNotDigitsAndNotSpace = new Regex("[^\\w0-1\\s]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

  public static GrammarBuilder CreateGrammarBuilderFromPhrase(string text)
  {
    string[] source = !string.IsNullOrEmpty(text) ? VoiceAssistantHelper.NotLettersAndNotDigitsAndNotSpace.Replace(text, " ").Trim().ToLowerInvariant().Split(new string[1]
    {
      " "
    }, StringSplitOptions.RemoveEmptyEntries) : throw new ArgumentNullException(nameof (text));
    if (source.Length == 0 || ((IEnumerable<string>) source).Any<string>((Func<string, bool>) (o => o == string.Empty)))
      return (GrammarBuilder) null;
    GrammarBuilder builderFromPhrase = new GrammarBuilder();
    IVoiceAssistant voiceAssistant = ServiceLocator.Get<IVoiceAssistant>();
    foreach (string phrase in source)
    {
      VoiceAssistantHint hint = voiceAssistant.GetHint(phrase);
      if (hint == null)
        builderFromPhrase.Append(phrase);
      else
        builderFromPhrase.Append(hint.GrammarBuilder);
    }
    return builderFromPhrase;
  }
}
