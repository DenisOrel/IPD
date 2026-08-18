
// Type: Intermech.Client.Core.CompositionView.CompositionViewHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.NotificationService;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Статический класс-хелпер</summary>
public sealed class CompositionViewHelper
{
  /// <summary>Посылка сообщения об обновлении окна навигатора</summary>
  /// <param name="sender"></param>
  /// <param name="navWindow">Окно навигатора, которое требуется обновить</param>
  /// <param name="args">параметры</param>
  public static void UpdateSourceTreeView(
    object sender,
    object navWindow,
    NotificationEventArgs args)
  {
    bool flag = false;
    if (navWindow is INotificationWindowService notificationWindowService)
      flag = notificationWindowService.FireEvent(sender, args);
    if (flag || !(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.FireEvent(sender, args);
  }

  /// <summary>Получить выделенные items</summary>
  /// <param name="treeView">дерево</param>
  /// <param name="viewsManager">вьюшки</param>
  /// <returns>список items</returns>
  public static List<IDBTypedObjectID> GetSelectedItems(
    NavigatorTreeView treeView,
    IViewsManager viewsManager)
  {
    List<IDBTypedObjectID> selectedItems1 = new List<IDBTypedObjectID>();
    ISelectedItems selectedItems2 = (viewsManager.ActiveViewPage != null ? viewsManager.ActiveViewPage.Control as ISelectedItemsHost : (ISelectedItemsHost) null)?.SelectedItems;
    int categoryId;
    if ((selectedItems2 == null || selectedItems2.Count == 0) && treeView.FocusedNode != null)
    {
      categoryId = treeView.FocusedNode.NodeID.CategoryID;
      selectedItems2 = categoryId.Equals(1) ? treeView.SelectedItems : (ISelectedItems) null;
    }
    if (selectedItems2 != null && selectedItems2.Count > 0)
    {
      for (int index = 0; index < selectedItems2.Count; ++index)
      {
        categoryId = selectedItems2.GetItemID(index).CategoryID;
        if (categoryId.Equals(1) && selectedItems2.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
          selectedItems1.Add(itemData);
      }
    }
    return selectedItems1;
  }

  /// <summary>Возвращает список возможных связей с параметрами</summary>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <param name="withChildren">заносить в результат потомков</param>
  /// <returns></returns>
  public static Dictionary<int, List<cvRelationInfo>> GetPossibleRelations(
    int objectTypeID,
    bool withChildren)
  {
    Dictionary<int, List<cvRelationInfo>> possibleRelations = new Dictionary<int, List<cvRelationInfo>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IObjectTypesInheritanceCache cache = CacheManager.Cache("ObjectTypeInheritanceCache") as IObjectTypesInheritanceCache;
      IDBRelationsApplicabilityCollection applicabilityCollection = session.GetRelationsApplicabilityCollection();
      DataTable dataTable = (DataTable) null;
      if (objectTypeID > 0)
        dataTable = applicabilityCollection.GetApplicabilitiesList(-1, -1, objectTypeID);
      if (dataTable != null)
      {
        if (dataTable.Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            int int32_1 = Convert.ToInt32(row["F_RELATION_TYPE"]);
            int int32_2 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
            cvRelationInfo cvRelationInfo = new cvRelationInfo(int32_1, session.IdentHelper.IsSortedRelationType(int32_1));
            List<int> intList;
            if (withChildren)
              intList = CompositionViewHelper.GetChildrenObjectTypes(int32_2, cache);
            else
              intList = new List<int>((IEnumerable<int>) new int[1]
              {
                int32_2
              });
            foreach (int key in intList)
            {
              if (possibleRelations.ContainsKey(key))
              {
                List<cvRelationInfo> cvRelationInfoList = possibleRelations[key];
                if (!cvRelationInfoList.Contains(cvRelationInfo))
                  cvRelationInfoList.Add(cvRelationInfo);
              }
              else
              {
                List<cvRelationInfo> cvRelationInfoList = new List<cvRelationInfo>((IEnumerable<cvRelationInfo>) new cvRelationInfo[1]
                {
                  cvRelationInfo
                });
                possibleRelations[key] = cvRelationInfoList;
              }
            }
          }
        }
      }
    }
    return possibleRelations;
  }

  private static List<int> GetChildrenObjectTypes(
    int parentObjectType,
    IObjectTypesInheritanceCache cache)
  {
    List<int> childrenObjectTypes = new List<int>();
    childrenObjectTypes.Add(parentObjectType);
    int[] childrenTypes = cache.GetChildrenTypes(parentObjectType);
    if (childrenTypes != null && childrenTypes.Length != 0)
    {
      foreach (int parentObjectType1 in childrenTypes)
        childrenObjectTypes.AddRange((IEnumerable<int>) CompositionViewHelper.GetChildrenObjectTypes(parentObjectType1, cache));
    }
    return childrenObjectTypes;
  }

  /// <summary>Могут ли объекты быть добавлены в другой объект</summary>
  /// <param name="typedObjectIDs">список объектов для добавления</param>
  /// <param name="hash">информация о связях</param>
  /// <returns>true - если все объекты могут быть добавлены</returns>
  public static bool IsObjectsCanAddToObject(
    List<IDBTypedObjectID> typedObjectIDs,
    Dictionary<int, List<cvRelationInfo>> hash)
  {
    if (typedObjectIDs == null || hash == null || typedObjectIDs.Count.Equals(0))
      return false;
    foreach (IDBTypedObjectID typedObjectId in typedObjectIDs)
    {
      if (!hash.ContainsKey(typedObjectId.ObjectType))
        return false;
    }
    return true;
  }

  /// <summary>
  /// Проверка отображения связей "по-умолчанию" в дереве
  /// используется в команде "Добавить"
  /// </summary>
  /// <param name="typedObjectIDs">список объектов</param>
  /// <param name="parentTypeID">идентификатор типа родителя</param>
  /// <param name="hash">информация о связях</param>
  /// <returns>true  - если все связи "по-умолчанию" в объектах видимы</returns>
  public static bool IsRelationTypesInVisibleRelations(
    List<IDBTypedObjectID> typedObjectIDs,
    int parentTypeID,
    Dictionary<int, List<cvRelationInfo>> hash)
  {
    if (typedObjectIDs == null || hash == null || typedObjectIDs.Count.Equals(0))
      return false;
    List<int> visibleRelations = CompositionViewHolder.UserRole.Rule.GetObjectTypeVisibleRelations(parentTypeID, true);
    foreach (IDBTypedObjectID typedObjectId in typedObjectIDs)
    {
      cvRelationInfo cvRelationInfo = hash[typedObjectId.ObjectType][0];
      if (!visibleRelations.Contains(cvRelationInfo.RelationTypeID))
        return false;
    }
    return true;
  }
}
