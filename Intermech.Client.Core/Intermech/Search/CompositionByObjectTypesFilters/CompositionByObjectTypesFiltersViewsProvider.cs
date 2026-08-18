
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFiltersViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (CompositionByObjectTypesFilterEditorView.TryGetSuitableSingleTypedObjectID(items, out typedObjectID))
      views.Add(typeof (CompositionByObjectTypesFilterEditorView).Name, new ViewInfo(0, typeof (CompositionByObjectTypesFilterEditorView)));
    if (CompositionByObjectTypesFiltersEditorView.TryGetSuitableSingleTypedObjectID(items, out typedObjectID))
      views.Add(typeof (CompositionByObjectTypesFiltersEditorControl).Name, new ViewInfo(0, typeof (CompositionByObjectTypesFiltersEditorView)));
    return views;
  }
}
