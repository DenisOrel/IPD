// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DSettingsParamViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Провайдер для закладки настроек параметров атрибутов Cadmech
/// </summary>
internal class Cadmech3DSettingsParamViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null || items.Count != 1 || ServiceUtils.GetService<IIMCadSettingsService>((object) services, false) == null)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("Cadmech3DSettingsParamView", new ViewInfo(0, 1417, typeof (Cadmech3DSettingsParamView)));
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
    if (Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS == null)
      return;
    foreach (int typeID in Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS)
      factory.AddViewsProvider(1, typeID, (IViewsProvider) new Cadmech3DSettingsParamViewProvider());
  }
}
