
// Type: Intermech.Search.VoiceAssistant.FormCommandsTarget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Microsoft.Speech.Recognition;
using System;
using System.Windows.Forms;


namespace Intermech.Search.VoiceAssistant;

public sealed class FormCommandsTarget : IVoiceAssistantCommandsTarget
{
  public bool Execute(RecognitionResult recognitionResult)
  {
    if (recognitionResult == null)
      throw new ArgumentNullException(nameof (recognitionResult));
    Form activeForm = Form.ActiveForm;
    if (activeForm != null)
    {
      if (recognitionResult.Semantics.Value as string == "Close")
      {
        activeForm.Close();
        return true;
      }
      if (recognitionResult.Semantics.Value as string == "OK" && activeForm.AcceptButton != null)
      {
        activeForm.AcceptButton.PerformClick();
        return true;
      }
      if (recognitionResult.Semantics.Value as string == "Cancel" && activeForm.CancelButton != null)
      {
        activeForm.CancelButton.PerformClick();
        return true;
      }
    }
    return false;
  }
}
