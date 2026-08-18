// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views.ArtsCompositionApplicabilityViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views;

/// <summary>Провайдер для закладки применяемости ДСЕ в ТП</summary>
internal class ArtsCompositionApplicabilityViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null || items.Count != 1 || ServiceUtils.GetService<ArtsCompositionApplicabilityParams>((object) services, false) == null)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ArtsCompositionApplicabilityView", new ViewInfo(0, typeof (ArtsCompositionApplicabilityView)));
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
    List<int> intList = new List<int>((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes);
    ArtsCompositionApplicabilityViewProvider provider = new ArtsCompositionApplicabilityViewProvider();
    foreach (int typeID in intList)
      factory.AddViewsProvider(1, typeID, (IViewsProvider) provider);
  }
}
