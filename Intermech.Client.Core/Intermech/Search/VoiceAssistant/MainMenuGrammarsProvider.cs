
// Type: Intermech.Search.VoiceAssistant.MainMenuGrammarsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Microsoft.Speech.Recognition;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Search.VoiceAssistant;

public sealed class MainMenuGrammarsProvider : IVoiceAssistantGrammarsProvider
{
  private LazyService<IMainMenuService> _mainMenuService = new LazyService<IMainMenuService>();

  public Grammar[] GetGrammars()
  {
    List<Grammar> grammarList = new List<Grammar>();
    List<string> menuItemsText = new List<string>();
    foreach (MenuItemBase menuItem in (CollectionBase) this._mainMenuService.Value.MenuBar.Items)
      this.AddChildrenMenuItemsTextToList(menuItem, menuItemsText);
    Choices choices = new Choices();
    foreach (string text in menuItemsText)
    {
      if (!string.IsNullOrEmpty(text))
      {
        GrammarBuilder builderFromPhrase = VoiceAssistantHelper.CreateGrammarBuilderFromPhrase(text);
        if (builderFromPhrase != null)
          choices.Add((GrammarBuilder) new SemanticResultValue(builderFromPhrase, (object) text));
      }
    }
    grammarList.Add(new Grammar(choices.ToGrammarBuilder())
    {
      Name = "MainMenuGrammar"
    });
    return grammarList.ToArray();
  }

  private void AddChildrenMenuItemsTextToList(MenuItemBase menuItem, List<string> menuItemsText)
  {
    if (!menuItem.Visible)
      return;
    if (!string.IsNullOrEmpty(menuItem.Text) && !menuItemsText.Contains(menuItem.Text))
      menuItemsText.Add(menuItem.Text);
    foreach (MenuItemBase menuItem1 in (CollectionBase) menuItem.Items)
      this.AddChildrenMenuItemsTextToList(menuItem1, menuItemsText);
  }
}
