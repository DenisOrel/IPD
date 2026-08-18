
// Type: Intermech.Client.Core.FormDesigner.Navigator.FormDesignerViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.FormDesigner.Navigator;

/// <summary>
/// Класс для регистрирования View'шки редактирования атрибутов объекта.
/// </summary>
internal class FormDesignerViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = ViewsInfo.Empty;
    if (items.Count == 1)
    {
      string str = "FormDesignerObject = ";
      string empty = string.Empty;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IFormDesignerService)) is IFormDesignerService customService)
        {
          ViewsInfo viewsInfo = new ViewsInfo();
          IDBTypedObjectID itemData1 = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          IDBRelationID itemData2 = items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
          if (itemData1 != null)
          {
            foreach (FormInformation formInformation in itemData2 != null ? (IEnumerable<FormInformation>) customService.GetFormsForObject(itemData1.Value, itemData2.Value, sessionKeeper.Session.SessionGUID) : (IEnumerable<FormInformation>) customService.GetFormsForObject(itemData1.Value, sessionKeeper.Session.SessionGUID))
            {
              string viewName = $"{str}[{itemData1.ObjectType},{formInformation.OrderIndex}],{formInformation.ToString(true)},Object";
              viewsInfo.Add(viewName, new ViewInfo(4, 696, typeof (FormDesignerViewObject)));
            }
          }
          if (itemData2 != null)
          {
            ICollection<FormInformation> formsForRelation = customService.GetFormsForRelation(itemData2.Value, sessionKeeper.Session.SessionGUID);
            if (formsForRelation != null && formsForRelation.Count > 0)
            {
              foreach (FormInformation formInformation in (IEnumerable<FormInformation>) formsForRelation)
              {
                string viewName = $"{str}[{itemData2.RelationType},{formInformation.OrderIndex}],{formInformation.ToString(true)},Relation";
                viewsInfo.Add(viewName, new ViewInfo(4, typeof (FormDesignerViewRelation)));
              }
            }
          }
          views = viewsInfo.ViewNames == null || viewsInfo.ViewNames.Length == 0 ? ViewsInfo.Empty : viewsInfo;
        }
      }
    }
    return views;
  }
}
