// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelNodeList
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelNodeList : List<AutoSelectionNodeCommon>
{
  public object Clone()
  {
    AutoSelNodeList autoSelNodeList = new AutoSelNodeList();
    foreach (AutoSelectionNodeCommon selectionNodeCommon in (List<AutoSelectionNodeCommon>) this)
      autoSelNodeList.Add(selectionNodeCommon.Clone() as AutoSelectionNodeCommon);
    return (object) autoSelNodeList;
  }

  public new void Add(AutoSelectionNodeCommon selNode)
  {
    if (selNode == null)
      return;
    if (selNode.Order == -1)
    {
      int val1 = -1;
      foreach (AutoSelectionNodeCommon selectionNodeCommon in (List<AutoSelectionNodeCommon>) this)
        val1 = Math.Max(val1, selectionNodeCommon.Order);
      int num;
      selNode.Order = num = val1 + 1;
    }
    base.Add(selNode);
  }

  public void Remove(AutoSelectionNodeCommon selNode, bool updateOrder)
  {
    if (selNode == null)
      return;
    if (!updateOrder)
    {
      this.Remove(selNode);
    }
    else
    {
      int index1 = this.IndexOf(selNode);
      if (index1 == -1)
        return;
      this.RemoveAt(index1);
      for (int index2 = index1; index2 < this.Count; ++index2)
        this[index2].Order = index2;
    }
  }
}
