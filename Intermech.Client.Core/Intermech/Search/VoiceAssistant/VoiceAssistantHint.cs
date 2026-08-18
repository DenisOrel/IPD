
// Type: Intermech.Search.VoiceAssistant.VoiceAssistantHint
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Microsoft.Speech.Recognition;
using System;


namespace Intermech.Search.VoiceAssistant;

public sealed class VoiceAssistantHint
{
  public VoiceAssistantHint(string phrase, GrammarBuilder grammarBuilder)
  {
    if (phrase == null)
      throw new ArgumentNullException("text");
    if (grammarBuilder == null)
      throw new ArgumentNullException(nameof (grammarBuilder));
    this.Phrase = phrase;
    this.GrammarBuilder = grammarBuilder;
  }

  public string Phrase { get; private set; }

  public GrammarBuilder GrammarBuilder { get; private set; }
}
