
// Type: Intermech.Navigator.DBObjects.EventLogPropertiesProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер для свойств евент лога</summary>
internal class EventLogPropertiesProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectID itemData = (IDBObjectID) items.GetItemData(0, typeof (IDBObjectID));
      if (itemData != null)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.Value, false);
        if (dbObject == null)
          return ViewsInfo.Empty;
        if (dbObject is IDBSecurity dbSecurity)
        {
          if (!dbSecurity.CheckAccess(ActionType.Edit, false, false))
            return ViewsInfo.Empty;
        }
      }
    }
    ViewsInfo views = new ViewsInfo();
    views.Add("EventLogPropertiesView", new ViewInfo(0, 1110, typeof (EventLogPropertiesView)));
    return views;
  }
}
