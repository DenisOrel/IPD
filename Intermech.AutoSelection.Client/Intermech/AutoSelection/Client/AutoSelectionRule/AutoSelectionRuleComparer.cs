// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRuleComparer
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionRule;

public class AutoSelectionRuleComparer : IComparer<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule>
{
  public int Compare(Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule x, Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule y)
  {
    if (x == y)
      return 0;
    if (y == null)
      return 1;
    return x == null ? -1 : x.Order - y.Order;
  }
}
