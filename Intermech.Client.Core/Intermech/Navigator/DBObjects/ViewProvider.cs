
// Type: Intermech.Navigator.DBObjects.ViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Snapshots;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер закладок</summary>
internal class ViewProvider : IViewsProvider
{
  /// <summary>Текущий пользователь и роль</summary>
  [NonSerialized]
  protected static ICurrentUserAndRole _userRole;

  /// <summary>Текущий пользователь и роль</summary>
  protected static ICurrentUserAndRole UserRole
  {
    get
    {
      if (ViewProvider._userRole == null)
        ViewProvider._userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      return ViewProvider._userRole;
    }
  }

  /// <summary>
  /// Возвращает контейнер со сведениями о закладках, которые должны быть
  /// выведены на экран в указанном контексте, а также о закладках других
  /// провайдеров, вывод которых должен быть подавлен.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="services">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    IDBRelationID itemData1 = items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID itemData2 = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    INodeID itemId = items.GetItemID(0);
    ViewsInfo views = new ViewsInfo();
    int typeId = items.GetItemID(0).TypeID;
    bool flag1 = true;
    IDBTypedObjectID itemData3 = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    if (true)
    {
      bool flag2 = true;
      int num = itemData2 != null ? itemData2.ObjectType : -1;
      if (num != -1)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(num);
        if ((objectType != null ? (objectType.AnyAttributes ? 1 : 0) : 0) == 0)
        {
          List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(num);
          for (int index = 0; index < attribute4ObjectTypeList.Count; ++index)
          {
            if (attribute4ObjectTypeList[index].RealFieldType == FieldTypes.ftFile)
            {
              flag2 = true;
              break;
            }
          }
        }
      }
      else if (num != -1)
      {
        IMSRelationType relationType = MetaDataHelper.GetRelationType(num);
        if ((relationType != null ? (relationType.AnyAttributes ? 1 : 0) : 0) == 0)
        {
          List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(num);
          for (int index = 0; index < relationTypeList.Count; ++index)
          {
            if (relationTypeList[index].RealFieldType == FieldTypes.ftFile)
            {
              flag2 = true;
              break;
            }
          }
        }
      }
      if (itemData2 != null & flag2)
        views.Add("ObjectFiles", new ViewInfo(0, 705, typeof (FilesView)));
      if (services.GetService(typeof (IViewState)) is IViewState service && (service.ViewState & ViewStateFlags.NoCompositionView) != ViewStateFlags.NoCompositionView)
      {
        List<int> applicabilityRelationTypesId = MetaDataHelper.GetApplicabilityRelationTypesID(typeId);
        if (applicabilityRelationTypesId != null && applicabilityRelationTypesId.Count > 0)
        {
          if (!(itemId is AdvObjectsListNodeID))
            views.Add("ChildrenView", new ViewInfo(0, 724, typeof (CompositionView)));
          else
            views.Add("ChildrenView", new ViewInfo(4, typeof (AdvObjectsViewBase)));
        }
        views.Add("ApplicabilityView", new ViewInfo(0, typeof (ApplicabilityView)));
      }
    }
    if (itemData2 != null)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(itemData2.ObjectType);
      if (objectType != null && objectType.Options.HasFlag((Enum) ObjectTypeOptions.CreateSnapshots))
        views.Add("SnapshotsView", new ViewInfo(0, 2575, typeof (SnapshotsView)));
    }
    if (itemData2 != null)
      views.Add("ObjectProperties", new ViewInfo(0, 697, typeof (PropertiesView)));
    if (itemData1 != null && itemData1.Value != -1L && itemData1.PartID != -1L && itemData1.Value != 0L && itemData1.PartID != 0L)
      views.Add("RelationProperties", new ViewInfo(0, 694, typeof (RelationPropertiesView)));
    if (Engine.LoadCompleted && items.GetItemData(0, typeof (IDBObjectID)) != null)
      views.Add("ObjectVisualizer", new ViewInfo(0, 706, typeof (Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.VisualizerView)));
    IViewState service1 = services.GetService(typeof (IViewState)) as IViewState;
    if (flag1 && service1 != null && (service1.ViewState & ViewStateFlags.NoEventsView) != ViewStateFlags.NoEventsView)
      views.Add("ObjectEvents", new ViewInfo(0, 711, typeof (ObjectEventsView)));
    if (itemData2 != null && itemData2.ObjectType == MetaDataHelper.GetObjectTypeID(new Guid("cad00002-306c-11d8-b4e9-00304f19f545")))
      views.Add("PerformanceOfDuities", new ViewInfo(0, typeof (PerformanceOfDuties)));
    if (MetaDataHelper.IsObjectTypeChildOf(typeId, MetaDataHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545")))
      views.Add("DocumentsThumbnailView", new ViewInfo(0, typeof (ThumbnailDocs)));
    return views;
  }
}
