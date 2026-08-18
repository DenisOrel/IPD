
// Type: Intermech.Search.ObjectGroups.ObjectGroupViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupViewProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    ViewsInfo views = new ViewsInfo();
    if (items.GetItemData(0, typeof (INodeID)) is ObjectGroupNodeID)
      views.Add("ObjectGroupView", new ViewInfo(0, typeof (ObjectGroupView)));
    return views;
  }
}
