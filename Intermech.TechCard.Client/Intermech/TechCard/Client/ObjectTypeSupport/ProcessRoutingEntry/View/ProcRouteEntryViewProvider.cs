// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.View.ProcRouteEntryViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Extensions;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.View;

/// <summary>
/// 
/// </summary>
internal class ProcRouteEntryViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  static ProcRouteEntryViewProvider()
  {
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    service.RegisterIconForObjectType("imgProcRouteEntry", MetaDataHelper.GetObjectTypeID("cadd9a56-306c-11d8-b4e9-00304f19f545"));
    service.RegisterIconForObjectType("imgProcRouteEntryArticle", MetaDataHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545"));
    AdjustableViewsHelper.RegisterView("ProcessRouteEntryView", LocalizationHolder.rm.GetString("TechCard.Client_ProcRouteEntryView_Name"), "", "Intermech.TechCard.Client", "imgProcRouteEntry", true, 0);
    AdjustableViewsHelper.RegisterView("ProcRouteEntryArticleView", LocalizationHolder.rm.GetString("TechCard.Client_ProcRouteEntryArticleView_Name"), "", "Intermech.TechCard.Client", "imgProcRouteEntryArticle", true, 0);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null || items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    bool flag1 = true;
    bool flag2 = true;
    if (ServiceUtils.GetService<IProductionListReportService>((object) ServicesManager.ServiceContainer, false) == null)
    {
      flag1 = false;
    }
    else
    {
      IEnumerable<RelObjInfoItem> relObjInfoItems;
      TechcardClientControlsUtils.GetItemsApplicabilityInfo(items, services, out relObjInfoItems);
      if (relObjInfoItems != null && relObjInfoItems.Any<RelObjInfoItem>())
      {
        List<int> objTypesProduction = MetaDataHelper.GetObjectTypeChildrenIDRecursive(MRP2Consts.objtypeIdProductionObjects);
        List<int> objTypesArticle = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new int[2]
        {
          TechCardConsts.ObjectTypes.ArticleBaseID,
          TechCardConsts.ObjectTypes.MaterialBaseID
        });
        if (relObjInfoItems.All<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (a => !objTypesProduction.Contains(a.ProjInfo.ObjTypeID))))
          flag1 = false;
        if (relObjInfoItems.All<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (a => !objTypesArticle.Contains(a.ProjInfo.ObjTypeID))))
          flag2 = false;
      }
    }
    if (!flag2 && !flag1)
    {
      flag2 = true;
      flag1 = true;
    }
    if (flag1)
      views.Add("ProcessRouteEntryView", new ViewInfo(0, 1414, typeof (ProcRouteEntryView)));
    if (flag2)
      views.Add("ProcRouteEntryArticleView", new ViewInfo(0, 1415, typeof (ProcRouteEntryArticleView)));
    views.Suppress("DocumentView", 0);
    return views;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="factory"></param>
  public static void RegisterViewProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    factory.AddViewsProvider(1, TechCardConsts.ObjectTypes.ProcRoutingEntryID, (IViewsProvider) new ProcRouteEntryViewProvider());
  }
}
