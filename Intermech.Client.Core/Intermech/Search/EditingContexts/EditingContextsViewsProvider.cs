
// Type: Intermech.Search.EditingContexts.EditingContextsViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Search.EditingContexts;

public sealed class EditingContextsViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null)
      throw new ArgumentException();
    ViewsInfo views = new ViewsInfo();
    if (EditingContextEditorView.CheckParamsForInitializeView(items, services))
      views.Add("EditingContextsView", new ViewInfo(4, 765, typeof (EditingContextEditorView)));
    return views;
  }
}
