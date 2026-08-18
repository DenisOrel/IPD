
// Type: Intermech.Navigator.Controls.VisibleNodesComparer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

/// <summary>Сравнить положение двух узлов в дереве "Навигатора"</summary>
internal class VisibleNodesComparer : IComparer<NavigatorTreeNode>
{
  /// <summary>Сравнить положение двух узлов в дереве "Навигатора"</summary>
  /// <param name="x">Первый узел</param>
  /// <param name="y">Второй узел</param>
  /// <returns>Сравнение положение двух узлов в дереве "Навигатора"</returns>
  public int Compare(NavigatorTreeNode x, NavigatorTreeNode y)
  {
    NavigatorTreeNode navigatorTreeNode1 = x;
    NavigatorTreeNode navigatorTreeNode2 = y;
    return navigatorTreeNode1 == null || navigatorTreeNode1.Handle == null || navigatorTreeNode2 == null || navigatorTreeNode2.Handle == null || navigatorTreeNode1.Parent != navigatorTreeNode2.Parent ? 0 : navigatorTreeNode1.Id.CompareTo(navigatorTreeNode2.Id);
  }
}
