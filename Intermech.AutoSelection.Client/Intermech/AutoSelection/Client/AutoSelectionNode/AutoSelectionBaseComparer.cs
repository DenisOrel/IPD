// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionBaseComparer
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionBaseComparer : IComparer<AutoSelectionNodeBase>
{
  public int Compare(AutoSelectionNodeBase x, AutoSelectionNodeBase y)
  {
    if (x == y)
      return 0;
    if (y == null)
      return 1;
    return x == null ? -1 : x.Order.CompareTo(y.Order);
  }
}
