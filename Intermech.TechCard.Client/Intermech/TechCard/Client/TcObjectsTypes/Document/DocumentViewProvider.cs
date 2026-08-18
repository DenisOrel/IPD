// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Document.DocumentViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Document;

/// <summary>Провайдер для закладки списка комплектов / документов</summary>
internal class DocumentViewProvider : IViewsProvider
{
  /// <summary>Конструктор</summary>
  static DocumentViewProvider()
  {
    AdjustableViewsHelper.RegisterView("DocumentView", LocalizationHolder.rm.GetString("TechCard.Client_168"), string.Empty, "Intermech.TechCard.Client", "imgDocumentList", true, 12);
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
    views.Add("DocumentView", new ViewInfo(0, typeof (DocumentView)));
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
    List<int> list = new List<int>((IEnumerable<int>) new int[2]
    {
      TechCardConsts.ObjectTypes.TechProcBaseID,
      TechCardConsts.ObjectTypes.OperaciyaID
    });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ComplectDocBaseID);
      GenericListHelper.MakeUnique<int>(childrenIdRecursive);
      DataTable applicabilitiesList = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(TechCardConsts.RelTypes.SortedRelationID, TechCardConsts.ObjectTypes.ComlectTechDocBaseID, -1);
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
    DocumentViewProvider provider = new DocumentViewProvider();
    foreach (int typeID in list)
      factory.AddViewsProvider(1, typeID, (IViewsProvider) provider);
  }
}
