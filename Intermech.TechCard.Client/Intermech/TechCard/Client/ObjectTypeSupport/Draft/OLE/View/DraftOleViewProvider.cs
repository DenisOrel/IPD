// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE.View.DraftOleViewProvider
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
using Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE.View;

/// <summary>
/// 
/// </summary>
internal class DraftOleViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  static DraftOleViewProvider()
  {
    ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false).RegisterIconForObjectType("imgDraftOle", TechCardConsts.ObjectTypes.DraftBaseID);
    AdjustableViewsHelper.RegisterView("DraftOLEView", LocalizationHolder.rm.GetString("TechCard.Client_181"), "", "Intermech.TechCard.Client", "imgDraftOle", true, 0);
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
    views.Add("DraftOLEView", new ViewInfo(0, 1414, typeof (DraftOleView)));
    return views;
  }
}
