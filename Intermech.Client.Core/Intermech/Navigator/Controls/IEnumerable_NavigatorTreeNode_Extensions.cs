
// Type: Intermech.Navigator.Controls.IEnumerable_NavigatorTreeNode_Extensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.Controls;

public static class IEnumerable_NavigatorTreeNode_Extensions
{
  public static IEnumerable<NavigatorTreeNode> Recursive(
    [CanBeNull] this IEnumerable<NavigatorTreeNode> startNodes)
  {
    if (startNodes != null)
    {
      foreach (NavigatorTreeNode node in startNodes)
      {
        yield return node;
        if (node.HasChildren && node.Children != null)
        {
          foreach (NavigatorTreeNode navigatorTreeNode in node.Children.Recursive())
            yield return navigatorTreeNode;
        }
      }
    }
  }

  public static IEnumerable<NavigatorTreeNode> RecursiveChilds(
    [CanBeNull] this IEnumerable<NavigatorTreeNode> startNodes)
  {
    if (startNodes != null)
    {
      foreach (NavigatorTreeNode navigatorTreeNode in startNodes.SelectMany<NavigatorTreeNode, NavigatorTreeNode>((Func<NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>) (node => node.Children.Recursive())))
        yield return navigatorTreeNode;
    }
  }
}
