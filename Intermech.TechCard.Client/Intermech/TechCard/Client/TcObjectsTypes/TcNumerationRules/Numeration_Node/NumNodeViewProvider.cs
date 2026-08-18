// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node.NumNodeViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node;

/// <summary>Summary description for NumNodeViewProvider.</summary>
public class NumNodeViewProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _registeredView;

  /// <summary>GetViews</summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (!NumNodeViewProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("NumNodeView", LocalizationHolder.rm.GetString("TechCard.Client_222"), "", "TechCard", "", true, 0);
      NumNodeViewProvider._registeredView = true;
    }
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("NumNodeView", new ViewInfo(0, 1450, typeof (NumNodeView)));
    return views;
  }
}
