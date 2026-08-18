
// Type: Intermech.Navigator.DBObjects.SecurityProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search;
using System;


namespace Intermech.Navigator.DBObjects;

internal class SecurityProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count == 0)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    IDBTypedObjectID[] typedObjectIds = (IDBTypedObjectID[]) null;
    SelectedItemsHelper.TryGetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIds(items, out typedObjectIds);
    if (typedObjectIds != null && typedObjectIds.Length != 0)
      views.Add("ObjectSecurity", new ViewInfo(0, 710, typeof (SecurityView)));
    return views;
  }
}
