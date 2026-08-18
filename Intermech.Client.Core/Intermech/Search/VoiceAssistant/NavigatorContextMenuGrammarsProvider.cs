
// Type: Intermech.Search.VoiceAssistant.NavigatorContextMenuGrammarsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;


namespace Intermech.Search.VoiceAssistant;

public sealed class NavigatorContextMenuGrammarsProvider : IVoiceAssistantGrammarsProvider
{
  private LazyService<IFactory> _factory = new LazyService<IFactory>();

  public Grammar[] GetGrammars()
  {
    List<Grammar> grammarList = new List<Grammar>();
    List<Tuple<List<string>, string>> menuTemplateNodesInfo = new List<Tuple<List<string>, string>>();
    this.AddMenuTemplpateNodesInfoToList(this._factory.Value.ContextMenuTemplate.Nodes, menuTemplateNodesInfo, (List<string>) null);
    Choices choices = new Choices();
    foreach (Tuple<List<string>, string> tuple in menuTemplateNodesInfo)
    {
      GrammarBuilder builderFromPhrase = VoiceAssistantHelper.CreateGrammarBuilderFromPhrase(string.Join(" ", (IEnumerable<string>) tuple.Item1));
      if (builderFromPhrase != null)
        choices.Add((GrammarBuilder) new SemanticResultValue(builderFromPhrase, (object) tuple.Item2));
    }
    grammarList.Add(new Grammar(choices.ToGrammarBuilder())
    {
      Name = "NavigatorContextMenuGrammar"
    });
    return grammarList.ToArray();
  }

  private void AddMenuTemplpateNodesInfoToList(
    MenuTemplateNodeCollection menuTemplateNodes,
    List<Tuple<List<string>, string>> menuTemplateNodesInfo,
    List<string> parentMenuTemplateNodeText)
  {
    foreach (MenuTemplateNode menuTemplateNode in menuTemplateNodes)
    {
      List<string> parentMenuTemplateNodeText1 = new List<string>();
      if (parentMenuTemplateNodeText != null)
        parentMenuTemplateNodeText1.AddRange((IEnumerable<string>) parentMenuTemplateNodeText);
      parentMenuTemplateNodeText1.Add(menuTemplateNode.Text);
      menuTemplateNodesInfo.Add(new Tuple<List<string>, string>(parentMenuTemplateNodeText1, menuTemplateNode.Name));
      this.AddMenuTemplpateNodesInfoToList(menuTemplateNode.Nodes, menuTemplateNodesInfo, parentMenuTemplateNodeText1);
    }
  }
}
