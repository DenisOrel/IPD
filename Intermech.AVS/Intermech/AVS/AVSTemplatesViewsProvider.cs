// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSTemplatesViewsProvider
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSViews;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.AVS;

/// <summary> Провайдер вьюшек для элемента спецификации </summary>
internal class AVSTemplatesViewsProvider : IViewsProvider
{
  private static AVSTemplatesViewsProvider instance;

  public AVSTemplatesViewsProvider()
  {
    if (AVSTemplatesViewsProvider.instance != null)
      return;
    AVSTemplatesViewsProvider.instance = this;
  }

  public static AVSTemplatesViewsProvider Instance => AVSTemplatesViewsProvider.instance;

  /// <summary> Получение списка вьюшек для отображения </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = ViewsInfo.Empty;
    if (services.GetService(typeof (IAVSTemplatesViewsService)) is IAVSTemplatesViewsService service && items != null && items.Count == 1)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(items.GetItemID(0).TypeID, AvsIDCache.ObjType_ConstructorDocumentTemplate) || service.ShowAll)
      {
        views = new ViewsInfo();
        views.Add("SetupSortingView", new ViewInfo(0, -1, typeof (SetupSortingView)));
        views.Add("SetupKeyWordsView", new ViewInfo(0, -1, typeof (SetupKeyWordsView)));
        if (MetaDataHelper.IsObjectTypeChildOf(items.GetItemID(0).TypeID, AvsIDCache.ObjType_Specification))
          views.Add("SetupNumberingView", new ViewInfo(0, -1, typeof (SetupNumberingView)));
        views.Add("SetupSkipLinesView", new ViewInfo(0, -1, typeof (SetupSkipLinesView)));
        views.Add("SetupAVSPropertiesView", new ViewInfo(0, -1, typeof (SetupAVSPropertiesView)));
        views.Add("SetupOutputView", new ViewInfo(0, -1, typeof (SetupOutputView)));
      }
      if (service.ShowCommonTemplate)
        views.Add("SetupObjectTypesView", new ViewInfo(0, -1, typeof (SetupObjectTypesView)));
    }
    return views;
  }
}
