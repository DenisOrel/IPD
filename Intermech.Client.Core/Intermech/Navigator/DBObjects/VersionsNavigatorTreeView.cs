
// Type: Intermech.Navigator.DBObjects.VersionsNavigatorTreeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Navigator.Controls;
using System;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

internal sealed class VersionsNavigatorTreeView : NavigatorTreeView
{
  /// <summary>Базовый конструктор</summary>
  public VersionsNavigatorTreeView()
  {
  }

  /// <summary>
  /// Создать дерево "Навигатора", задать ему определённый контекст (контейнер сервисов)
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  public VersionsNavigatorTreeView(IServiceProvider services)
    : base(services)
  {
  }

  protected override Image GetNodeImage(NavigatorTreeNode node)
  {
    return !(node.NodeID is VersionsHiveNodeID nodeId) ? base.GetNodeImage(node) : Images32x16_Cache.GetImage32x16(nodeId.CategoryID, (int) nodeId.Mode, node);
  }
}
