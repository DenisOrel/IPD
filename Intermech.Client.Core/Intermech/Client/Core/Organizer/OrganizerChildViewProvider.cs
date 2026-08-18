
// Type: Intermech.Client.Core.Organizer.OrganizerChildViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class OrganizerChildViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    NodeIDPath parentPath = items.GetParentPath(0);
    if (parentPath == null)
      return new ViewsInfo();
    INodeID nodeId = parentPath[0];
    if (nodeId == null)
      return new ViewsInfo();
    if (!(ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service))
      return new ViewsInfo();
    ViewsInfo views = new ViewsInfo();
    Dictionary<string, ViewInfo> requiredViews = service.GetRequiredViews(nodeId.CategoryID);
    if (requiredViews != null)
    {
      foreach (KeyValuePair<string, ViewInfo> keyValuePair in requiredViews)
        views.Add(keyValuePair.Key, keyValuePair.Value);
    }
    List<string> superfluousViews = service.GetSuperfluousViews(nodeId.CategoryID);
    if (superfluousViews != null)
    {
      foreach (string viewName in superfluousViews)
        views.Add(viewName, new ViewInfo(0));
    }
    return views;
  }
}
