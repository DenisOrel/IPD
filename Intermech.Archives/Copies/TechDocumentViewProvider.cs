// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.TechDocumentViewProvider
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Провайдер ОТД</summary>
public class TechDocumentViewProvider : IViewsProvider
{
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
    ViewsInfo views = new ViewsInfo();
    views.Add("TechDocumView", new ViewInfo(0, 2651, typeof (TechDocumView)));
    int typeId1 = items.GetItemID(0).TypeID;
    if (MetaDataHelper.IsObjectTypeChildOf(typeId1, MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545")))
    {
      NodeIDPath parentPath = items.GetParentPath(0);
      if (parentPath == null)
      {
        views.Remove("TechDocumView");
        return views;
      }
      if (parentPath.Length == 0)
        views.Remove("TechDocumView");
      else if (parentPath.LastID.TypeID == MetaDataHelper.GetObjectTypeID("cad0004a-306c-11d8-b4e9-00304f19f545"))
        views.Remove("TechDocumView");
      else if (parentPath.Length == 1 && !MetaDataHelper.IsObjectTypeChildOf(parentPath.FirstID.TypeID, MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545")))
      {
        views.Remove("TechDocumView");
      }
      else
      {
        parentPath.RemoveLast();
        int typeId2 = parentPath.LastID.TypeID;
        bool flag1 = MetaDataHelper.IsObjectTypeChildOf(typeId2, MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545"));
        bool flag2 = MetaDataHelper.IsObjectTypeChildOf(typeId2, MetaDataHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545"));
        bool flag3 = parentPath.LastID.CategoryID == Intermech.Archives.Consts.CategoryArchivesNode;
        if (!flag1 && !flag2 && !flag3)
          views.Remove("TechDocumView");
      }
    }
    if (MetaDataHelper.IsObjectTypeChildOf(typeId1, MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545")))
    {
      NodeIDPath parentPath = items.GetParentPath(0);
      if (parentPath == null)
      {
        views.Remove("TechDocumView");
        return views;
      }
      if (parentPath.Length == 0)
        views.Remove("TechDocumView");
      else if (parentPath.LastID.TypeID == MetaDataHelper.GetObjectTypeID("cad0004a-306c-11d8-b4e9-00304f19f545"))
        views.Remove("TechDocumView");
      else if (parentPath.Length == 1 && !MetaDataHelper.IsObjectTypeChildOf(parentPath.FirstID.TypeID, MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545")))
        views.Remove("TechDocumView");
      else if (!MetaDataHelper.IsObjectTypeChildOf(TechDocumentViewProvider.GetRootTypeID(parentPath), MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545")))
        views.Remove("TechDocumView");
    }
    return views;
  }

  /// <summary>
  /// Рекурсивно получает корневой тип для классификатора (т.к. может быть много вложений, папок и т.д.)
  /// </summary>
  /// <param name="path">Путь для выделенного узла.</param>
  /// <returns></returns>
  private static int GetRootTypeID(NodeIDPath path)
  {
    path.RemoveLast();
    int childType = path.LastID.TypeID;
    if (MetaDataHelper.IsObjectTypeChildOf(childType, MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545")))
      childType = TechDocumentViewProvider.GetRootTypeID(path);
    return childType;
  }
}
