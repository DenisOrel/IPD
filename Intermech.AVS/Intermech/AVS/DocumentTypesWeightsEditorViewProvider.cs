// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocumentTypesWeightsEditorViewProvider
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>Провайдер закладки "Типы документов"</summary>
internal class DocumentTypesWeightsEditorViewProvider : IViewsProvider
{
  /// <summary>
  /// Возвращает контейнер со сведениями о закладках, которые должны быть
  /// выведены на экран в указанном контексте, а также о закладках других
  /// провайдеров, вывод которых должен быть подавлен.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    Guid guid = Guid.Empty;
    if (itemData != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        guid = sessionKeeper.Session.GetObjectInfo(itemData.ObjectID).VersionGuid;
    }
    ViewsInfo views = new ViewsInfo();
    if (guid == new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"))
    {
      views.Add("AVS.DocumentTypesWeights", new ViewInfo(4, 1519, typeof (DocumentTypesWeightsEditorView)));
      if (ImDocumentData.ShowDebugInfo)
        views.Add("AVS.RemarkAttributes", new ViewInfo(4, 1521, typeof (RemarkAttributesView)));
    }
    else if (guid == AvsIDCache.StdTemplateElementList && ImDocumentData.ShowDebugInfo)
      views.Add("AVS.RemarkAttributes", new ViewInfo(4, 1521, typeof (RemarkAttributesView)));
    return views;
  }
}
