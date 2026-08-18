// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Rule.NumRuleViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.TcNumerationRules;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Rule;

/// <summary>Summary description for NumNodeViewProvider.</summary>
public class NumRuleViewProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _registeredView;

  /// <summary>GetViews</summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (!NumRuleViewProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("NumRuleView", LocalizationHolder.rm.GetString("TechCard.Client_229"), "", "TechCard", "", true, 0);
      NumRuleViewProvider._registeredView = true;
    }
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("NumRuleView", new ViewInfo(0, 1450, typeof (NumRuleView)));
    return views;
  }
}
