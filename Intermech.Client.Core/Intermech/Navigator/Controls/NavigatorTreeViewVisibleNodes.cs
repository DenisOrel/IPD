
// Type: Intermech.Navigator.Controls.NavigatorTreeViewVisibleNodes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Содержит информацию о видимых узлах дерева "Навигатора"
/// </summary>
internal class NavigatorTreeViewVisibleNodes : NavigatorTreeViewVisibleNodesContainer
{
  /// <summary>Группы видимых узлов</summary>
  private NavigatorTreeViewVisibleNodesGroup[] _groups;

  /// <summary>Конструктор</summary>
  /// <param name="nodes">Список узлов</param>
  public NavigatorTreeViewVisibleNodes(IList nodes)
    : base(nodes)
  {
    this._groups = (NavigatorTreeViewVisibleNodesGroup[]) null;
  }

  /// <summary>Группы видимых узлов</summary>
  public NavigatorTreeViewVisibleNodesGroup[] Groups
  {
    get
    {
      if (this._groups == null)
        this.CreateGroups();
      return this._groups;
    }
  }

  /// <summary>Создать коллекцию групп</summary>
  private void CreateGroups()
  {
    if (this.Count == 0)
    {
      this._groups = new NavigatorTreeViewVisibleNodesGroup[0];
    }
    else
    {
      IDictionary dictionary = (IDictionary) new HybridDictionary();
      bool flag = this[0].Parent == null;
      for (int index = flag ? 1 : 0; index < this.Count; ++index)
      {
        NavigatorTreeNode navigatorTreeNode = this[index];
        if (navigatorTreeNode.Parent != null)
        {
          if (!(dictionary[(object) navigatorTreeNode.Parent] is IList list))
          {
            list = (IList) new ArrayList();
            dictionary[(object) navigatorTreeNode.Parent] = (object) list;
          }
          list.Add((object) navigatorTreeNode);
        }
      }
      this._groups = new NavigatorTreeViewVisibleNodesGroup[flag ? dictionary.Count + 1 : dictionary.Count];
      int num = 0;
      if (flag)
        this._groups[num++] = new NavigatorTreeViewVisibleNodesGroup(this[0], (NavigatorTreeNode) null);
      foreach (DictionaryEntry dictionaryEntry in dictionary)
        this._groups[num++] = new NavigatorTreeViewVisibleNodesGroup((IList) dictionaryEntry.Value, (NavigatorTreeNode) dictionaryEntry.Key);
    }
  }
}
