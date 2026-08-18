
// Type: Intermech.Search.VoiceAssistant.IVoiceAssistant
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Search.VoiceAssistant;

public interface IVoiceAssistant
{
  void AddGrammarsProvider(IVoiceAssistantGrammarsProvider grammarsProvider);

  void RemoveGrammarsProvider(IVoiceAssistantGrammarsProvider grammarsProvider);

  IVoiceAssistantCommandsTarget ActiveCommandsTarget { get; set; }

  void AddCommandsTarget(IVoiceAssistantCommandsTarget commandsTarget);

  void RemoveCommandsTarget(IVoiceAssistantCommandsTarget commandsTarget);

  void AddHint(VoiceAssistantHint hint);

  VoiceAssistantHint GetHint(string phrase);

  void RemoveHint(string phrase);

  event EventHandler StateChanged;

  VoiceAssistantState State { get; }

  void Start();

  void Stop();
}
