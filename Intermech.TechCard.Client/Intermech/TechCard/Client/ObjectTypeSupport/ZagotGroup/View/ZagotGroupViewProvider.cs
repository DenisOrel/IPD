// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ZagotGroup.View.ZagotGroupViewProvider
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
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ZagotGroup.View;

/// <summary>
/// Провайдер закладок для типа объекта "Групповая заготовка"
/// </summary>
internal class ZagotGroupViewProvider : IViewsProvider
{
  /// <summary>Конструктор</summary>
  static ZagotGroupViewProvider()
  {
    ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false).RegisterIconForObjectType(ZagotGroupView.IconImageName, TechCardConsts.ObjectTypes.ZagotID);
    AdjustableViewsHelper.RegisterView("ZagotGroupView", LocalizationHolder.rm.GetString("TechCard.Client_ZagotGroupView_Caption"), "", "Intermech.TechCard.Client", ZagotGroupView.IconImageName, true, 0);
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
    views.Add("ZagotGroupView", new ViewInfo(0, 1525, typeof (ZagotGroupView)));
    return views;
  }
}
