
// Type: Intermech.Navigator.Notifications.UpdatePlan
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.Notifications;

internal class UpdatePlan : IUpdatePlan
{
  private ArrayList _updatedItems = new ArrayList();
  private ArrayList _replacedItems = new ArrayList();
  private NodeIDCollection _replacements = new NodeIDCollection();
  private ArrayList _removedItems = new ArrayList();
  private NodeIDCollection _appendedItems = new NodeIDCollection();
  private int _currentIndex = -1;

  public NodeIDCollection AppendedItems
  {
    get
    {
      NodeIDCollection appendedItems = new NodeIDCollection();
      appendedItems.AddRange((IEnumerable<INodeID>) this._appendedItems);
      return appendedItems;
    }
  }

  void IUpdatePlan.Append(INodeID partialNodeID) => this._appendedItems.Add(partialNodeID);

  void IUpdatePlan.Update() => this._updatedItems.Add((object) this._currentIndex);

  void IUpdatePlan.Replace(INodeID replacementNodeID)
  {
    this._replacedItems.Add((object) this._currentIndex);
    this._replacements.Add(replacementNodeID);
  }

  void IUpdatePlan.Remove() => this._removedItems.Add((object) this._currentIndex);

  public int CurrentIndex
  {
    get => this._currentIndex;
    set => this._currentIndex = value;
  }

  public void Execute(INodeView nodeView)
  {
    if (this._replacedItems.Count > 0)
      nodeView.Replace((IList) this._replacedItems, this._replacements);
    if (this._updatedItems.Count > 0)
      nodeView.Update((IList) this._updatedItems);
    if (this._removedItems.Count > 0)
      nodeView.Remove((IList) this._removedItems);
    if (this._appendedItems.Count <= 0)
      return;
    nodeView.Append(this._appendedItems);
  }
}
