// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.RouteElementsViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Route_Element;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element;

/// <summary>
/// Провайдер для закладки со списком элементов расцеховки
/// </summary>
internal class RouteElementsViewProvider : IViewsProvider
{
  /// <summary>Конструктор</summary>
  static RouteElementsViewProvider()
  {
    AdjustableViewsHelper.RegisterView("RouteElementsView", LocalizationHolder.rm.GetString("TechCard.Client_541"), "", "Intermech.TechCard.Client", "", true, 0);
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
    views.Add("RouteTemplateView", new ViewInfo(0, 1417, typeof (RouteElementsView)));
    return views;
  }

  /// <summary>Регистрация провайдера закладок</summary>
  /// <param name="factory"></param>
  internal static void RegisterViewProvider([NotNull] IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    List<int> list = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ElemRouteID);
      GenericListHelper.MakeUnique<int>(childrenIdRecursive);
      DataTable applicabilitiesList = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(TechCardConsts.RelTypes.TechRelationID, TechCardConsts.ObjectTypes.ElemRouteID, -1);
      if (applicabilitiesList != null)
      {
        int columnIndex1 = applicabilitiesList.Columns.IndexOf("F_OBJECT_TYPE");
        int columnIndex2 = applicabilitiesList.Columns.IndexOf("F_INOBJECT_TYPE");
        foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
        {
          int int32 = Convert.ToInt32(row[columnIndex1]);
          if (childrenIdRecursive.BinarySearch(int32) >= 0 && TechCardConsts.Utils.IsTechcardObjectType((object) Convert.ToInt32(row[columnIndex2])))
            list.Add(Convert.ToInt32(row[columnIndex2]));
        }
        GenericListHelper.MakeUnique<int>(list);
      }
    }
    if (list.Count == 0)
      return;
    RouteElementsViewProvider provider = new RouteElementsViewProvider();
    foreach (int typeID in list)
      factory.AddViewsProvider(1, typeID, (IViewsProvider) provider);
  }
}
