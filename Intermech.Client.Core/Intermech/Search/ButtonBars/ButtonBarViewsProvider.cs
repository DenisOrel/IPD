
// Type: Intermech.Search.ButtonBars.ButtonBarViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Search.ButtonBars;

public sealed class ButtonBarViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (services == null)
      throw new ArgumentNullException(nameof (services));
    ViewsInfo views = new ViewsInfo();
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (ButtonBarsEditorView.CheckParams(items, services, out typedObjectID))
      views.Add("ButtonBarsEditorView", new ViewInfo(100, typeof (ButtonBarsEditorView)));
    return views;
  }
}
