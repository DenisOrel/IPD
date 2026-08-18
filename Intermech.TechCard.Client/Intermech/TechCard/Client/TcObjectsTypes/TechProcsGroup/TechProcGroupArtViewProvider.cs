// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcsGroup.TechProcGroupArtViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Extensions;
using Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcsGroup.View;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcsGroup;

/// <summary>Summary description for TechProcGroupArtViewProvider.</summary>
public class TechProcGroupArtViewProvider : IViewsProvider
{
  /// <summary>Конструктор</summary>
  static TechProcGroupArtViewProvider()
  {
    ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false).RegisterIconForObjectType(TechProcGroupArtView.IconImageName, TechCardConsts.ObjectTypes.ArticleBaseID);
    AdjustableViewsHelper.RegisterView("TechProcGroupArtView", LocalizationHolder.rm.GetString("TechCard.Client_280"), "", "Intermech.TechCard.Client", TechProcGroupArtView.IconImageName, true, 0);
  }

  /// <summary>GetViews</summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null || items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("TechProcGroupArtView", new ViewInfo(0, 1425, typeof (TechProcGroupArtView)));
    return views;
  }
}
