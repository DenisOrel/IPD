
// Type: Intermech.Search.VoiceAssistant.MessageBoxCommandsTarget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Microsoft.Speech.Recognition;
using System;
using System.Runtime.InteropServices;


namespace Intermech.Search.VoiceAssistant;

public sealed class MessageBoxCommandsTarget : IVoiceAssistantCommandsTarget
{
  private const string OKButtonCaption = "ОК";
  private const string CancelButtonCaption = "Отмена";
  private const string YesButtonCaption = "&Да";
  private const string NoButtonCaption = "&Нет";
  private const string AbortButtonCaption = "Пр&ервать";
  private const string RetryButtonCaption = "По&втор";
  private const string IgnoreButtonCaption = "&Пропустить";

  public bool Execute(RecognitionResult recognitionResult)
  {
    if (recognitionResult == null)
      throw new ArgumentNullException(nameof (recognitionResult));
    bool flag = false;
    foreach (IntPtr num in Intermech.Search.NativeMethods.FindAllWindowHandlesForCurrentProcess("#32770"))
    {
      if (num != IntPtr.Zero)
      {
        if (recognitionResult.Semantics.Value as string == "Close")
        {
          Intermech.Search.NativeMethods.SendMessage(new HandleRef((object) null, num), 16U /*0x10*/, IntPtr.Zero, IntPtr.Zero);
          flag = true;
        }
        else if (recognitionResult.Semantics.Value as string == "OK")
        {
          IntPtr windowEx = Intermech.Search.NativeMethods.FindWindowEx(num, IntPtr.Zero, "Button", "ОК");
          if (windowEx != IntPtr.Zero)
          {
            this.PerformButtonClick(windowEx);
            flag = true;
          }
        }
        else if (recognitionResult.Semantics.Value as string == "Cancel")
        {
          IntPtr windowEx = Intermech.Search.NativeMethods.FindWindowEx(num, IntPtr.Zero, "Button", "Отмена");
          if (windowEx != IntPtr.Zero)
          {
            this.PerformButtonClick(windowEx);
            flag = true;
          }
        }
        else if (recognitionResult.Semantics.Value as string == "Yes")
        {
          IntPtr windowEx = Intermech.Search.NativeMethods.FindWindowEx(num, IntPtr.Zero, "Button", "&Да");
          if (windowEx != IntPtr.Zero)
          {
            this.PerformButtonClick(windowEx);
            flag = true;
          }
        }
        else if (recognitionResult.Semantics.Value as string == "No")
        {
          IntPtr windowEx = Intermech.Search.NativeMethods.FindWindowEx(num, IntPtr.Zero, "Button", "&Нет");
          if (windowEx != IntPtr.Zero)
          {
            this.PerformButtonClick(windowEx);
            flag = true;
          }
        }
        else if (recognitionResult.Semantics.Value as string == "Abort")
        {
          IntPtr windowEx = Intermech.Search.NativeMethods.FindWindowEx(num, IntPtr.Zero, "Button", "Пр&ервать");
          if (windowEx != IntPtr.Zero)
          {
            this.PerformButtonClick(windowEx);
            flag = true;
          }
        }
        else if (recognitionResult.Semantics.Value as string == "Retry")
        {
          IntPtr windowEx = Intermech.Search.NativeMethods.FindWindowEx(num, IntPtr.Zero, "Button", "По&втор");
          if (windowEx != IntPtr.Zero)
          {
            this.PerformButtonClick(windowEx);
            flag = true;
          }
        }
        else if (recognitionResult.Semantics.Value as string == "Ignore")
        {
          IntPtr windowEx = Intermech.Search.NativeMethods.FindWindowEx(num, IntPtr.Zero, "Button", "&Пропустить");
          if (windowEx != IntPtr.Zero)
          {
            this.PerformButtonClick(windowEx);
            flag = true;
          }
        }
      }
    }
    return flag;
  }

  private void PerformButtonClick(IntPtr buttonHandle)
  {
    Intermech.Search.NativeMethods.SendMessage(new HandleRef((object) null, buttonHandle), 513U, new IntPtr(1), IntPtr.Zero);
    Intermech.Search.NativeMethods.SendMessage(new HandleRef((object) null, buttonHandle), 514U, new IntPtr(1), IntPtr.Zero);
  }
}
