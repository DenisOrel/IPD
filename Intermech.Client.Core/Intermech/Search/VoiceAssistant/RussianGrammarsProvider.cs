
// Type: Intermech.Search.VoiceAssistant.RussianGrammarsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Search.VoiceAssistant;

public sealed class RussianGrammarsProvider : IVoiceAssistantGrammarsProvider
{
  public Grammar[] GetGrammars()
  {
    return new List<Grammar>()
    {
      new Grammar(new SrgsDocument((XmlReader) new XmlTextReader(typeof (RussianGrammarsProvider).Assembly.GetManifestResourceStream("Intermech.Client.Core.Intermech.Search.VoiceAssistant.Grammars.ru.xml"))))
      {
        Name = "RussianGrammar",
        Priority = 3
      }
    }.ToArray();
  }
}
