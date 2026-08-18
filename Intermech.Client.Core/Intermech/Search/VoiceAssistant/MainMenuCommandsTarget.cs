
// Type: Intermech.Search.VoiceAssistant.MainMenuCommandsTarget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Microsoft.Speech.Recognition;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Search.VoiceAssistant;

public sealed class MainMenuCommandsTarget : IVoiceAssistantCommandsTarget
{
  private LazyService<IMainMenuService> _mainMenuService = new LazyService<IMainMenuService>();

  public bool Execute(RecognitionResult recognitionResult)
  {
    if (recognitionResult.Grammar.Name == "MainMenuGrammar")
    {
      foreach (MenuItemBase menuItem in (CollectionBase) this._mainMenuService.Value.MenuBar.Items)
      {
        MenuItemBase menuItemWithText = this.FindMenuItemWithText(menuItem, recognitionResult.Semantics.Value as string);
        if (menuItemWithText != null)
        {
          if (menuItemWithText is MenuBarItem)
          {
            if (Form.ActiveForm != null)
            {
              foreach (Form ownedForm in Form.ActiveForm.OwnedForms)
              {
                if (ownedForm is PopupMenu)
                {
                  Intermech.Search.NativeMethods.SendMessage(new HandleRef((object) null, ownedForm.Handle), 513U, (IntPtr) 1, Intermech.Search.NativeMethods.CreateLParamForMouseEvent((short) -10, (short) -10));
                  Intermech.Search.NativeMethods.SendMessage(new HandleRef((object) null, ownedForm.Handle), 675U, IntPtr.Zero, IntPtr.Zero);
                }
              }
            }
            ((TopLevelMenuItemBase) menuItemWithText).Show(true);
            return true;
          }
          menuItemWithText.PerformClick();
          return true;
        }
      }
    }
    return false;
  }

  private MenuItemBase FindMenuItemWithText(MenuItemBase menuItem, string text)
  {
    if (menuItem.Text == text)
      return menuItem;
    foreach (MenuItemBase menuItem1 in (CollectionBase) menuItem.Items)
    {
      MenuItemBase menuItemWithText = this.FindMenuItemWithText(menuItem1, text);
      if (menuItemWithText != null)
        return menuItemWithText;
    }
    return (MenuItemBase) null;
  }
}
