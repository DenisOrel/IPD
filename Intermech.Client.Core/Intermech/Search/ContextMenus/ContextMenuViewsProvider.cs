
// Type: Intermech.Search.ContextMenus.ContextMenuViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenuViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    ViewsInfo views = new ViewsInfo();
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (ContextMenuEditorView.TryGetSuitableSingleTypedObjectID(items, out typedObjectID))
      views.Add(typeof (ContextMenuEditorView).Name, new ViewInfo(-1, typeof (ContextMenuEditorView)));
    if (ContextMenusForObjectEditorView.TryGetSuitableSingleTypedObjectID(items, out typedObjectID))
      views.Add(typeof (ContextMenusForObjectEditorView).Name, new ViewInfo(-1, typeof (ContextMenusForObjectEditorView)));
    return views;
  }
}
