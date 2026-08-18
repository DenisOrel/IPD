// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.BaseObjectsInfoProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Imbase;

internal class BaseObjectsInfoProvider : IViewsProvider
{
  static BaseObjectsInfoProvider()
  {
    AdjustableViewsHelper.RegisterView("BaseObjectsInfoView", LocalizationHolder.rm.GetString("Imbase_BaseObjectsInfoView_Caption"), LocalizationHolder.rm.GetString("Imbase_BaseObjectsInfoView_Caption"), "Imbase", "imgProp", true, 10300);
  }

  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return ViewsInfo.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(itemData.ObjectID, false);
      if (objectActualCopy == null)
        return ViewsInfo.Empty;
      IDBAttribute attributeById = objectActualCopy.GetAttributeByID(Consts.ImbaseObjectRefAttID);
      if (attributeById == null || attributeById.Values[0] == null || attributeById.Values[0] == DBNull.Value || attributeById.AsInteger <= -1L)
        return ViewsInfo.Empty;
      views.Add("BaseObjectsInfoView", new ViewInfo(3, 697, typeof (BaseObjectsInfoView)));
    }
    return views;
  }
}
