
// Type: Intermech.Search.VoiceAssistant.NavigatorContextMenuCommandsTarget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Microsoft.Speech.Recognition;
using System;


namespace Intermech.Search.VoiceAssistant;

public sealed class NavigatorContextMenuCommandsTarget : IVoiceAssistantCommandsTarget
{
  public bool Execute(RecognitionResult recognitionResult)
  {
    if (recognitionResult == null)
      throw new ArgumentNullException(nameof (recognitionResult));
    if (recognitionResult.Grammar.Name == "NavigatorContextMenuGrammar")
    {
      string commandName = recognitionResult.Semantics.Value as string;
      if (!string.IsNullOrEmpty(commandName))
      {
        NavigatorTreeView navigatorTreeView = ServicesManager.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView;
        if (navigatorTreeView != null)
        {
          navigatorTreeView.Invoke((Delegate) (() => navigatorTreeView.Execute(commandName)));
          return true;
        }
        ChildrenView childrenView = ServicesManager.GetService(typeof (ChildrenView)) as ChildrenView;
        if (childrenView != null)
        {
          childrenView.Invoke((Delegate) (() => childrenView.Execute(commandName)));
          return true;
        }
      }
    }
    return false;
  }
}
