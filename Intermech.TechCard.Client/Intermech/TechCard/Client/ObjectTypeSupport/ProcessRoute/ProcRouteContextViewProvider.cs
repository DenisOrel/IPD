// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.ProcRouteContextViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.MRP2;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute;

/// <summary>Провайдер закладок для состава МО</summary>
internal class ProcRouteContextViewProvider : IViewsProvider
{
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
    IMSApplicability applicability = items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? MetaDataHelper.GetApplicability(TechCardConsts.ObjectTypes.ProcRoutingID, itemData.ObjectType, TechCardConsts.RelTypes.TechRelationID) : (IMSApplicability) null;
    IEnumerable<RelObjInfoItem> relObjInfoItems;
    if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled && TechcardClientControlsUtils.GetItemsApplicabilityInfo(items, services, out relObjInfoItems) && relObjInfoItems.Any<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.ProjInfo != (TypedInfoItem) null && MetaDataHelper.IsObjectTypeChildOf(item.ProjInfo.ObjTypeID, TechCardConsts.ObjectTypes.ArticleCopyBaseID))))
      views.Add("MRP2.ProductionCopyComplectNumbersView", new ViewInfo(0, typeof (ProductionCopyComplectNumbersView)));
    return views;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="factory"></param>
  public static void RegisterViewProvider([NotNull] IFactory factory)
  {
    factory.AddViewsProvider(1, (IViewsProvider) new ProcRouteContextViewProvider());
  }
}
