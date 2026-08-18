// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.AutoSelectionCache.AutoSelectionRuleCacheComparer
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Server.AutoSelectionCache;

internal class AutoSelectionRuleCacheComparer : IComparer<AutoSelectionRuleCache>
{
  public int Compare(AutoSelectionRuleCache x, AutoSelectionRuleCache y)
  {
    AutoSelectionRuleCache selectionRuleCache1 = x;
    AutoSelectionRuleCache selectionRuleCache2 = y;
    int num = selectionRuleCache1.TypeLinked.CompareTo(selectionRuleCache2.TypeLinked);
    if (num == 0)
      num = selectionRuleCache1.OrderID.CompareTo(selectionRuleCache2.OrderID);
    return num;
  }
}
