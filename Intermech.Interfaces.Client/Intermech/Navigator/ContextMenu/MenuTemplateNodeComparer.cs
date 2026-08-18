// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.MenuTemplateNodeComparer
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

internal class MenuTemplateNodeComparer : IComparer<MenuTemplateNode>
{
  public int Compare(MenuTemplateNode x, MenuTemplateNode y)
  {
    MenuTemplateNode menuTemplateNode1 = x;
    MenuTemplateNode menuTemplateNode2 = y;
    int num = menuTemplateNode1.GroupID.CompareTo(menuTemplateNode2.GroupID);
    if (num == 0)
    {
      num = menuTemplateNode1.OrderID.CompareTo(menuTemplateNode2.OrderID);
      if (num == 0)
        num = menuTemplateNode1.Text.CompareTo(menuTemplateNode2.Text);
    }
    return num;
  }
}
