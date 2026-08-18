// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.SelectObjectFromProductionCopyWizardProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Navigator.VirtualNodes;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

internal class SelectObjectFromProductionCopyWizardProvider : 
  BaseViewDescriptionProvider,
  IViewsProvider
{
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
    if (items.GetItemData<IDescriptor>(0, false) is HiveDescriptor)
      views.Add("SelectObjectFromProductionCopyWizard", new ViewInfo(0, typeof (SelectObjectFromProductionCopyWizardControl)));
    return views;
  }

  public override ViewDescription DoGetViewDescription(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider)
  {
    return new ViewDescription()
    {
      Caption = LocalizationHolder.rm.GetString("TechCard.Client_548"),
      ImageIndex = -1,
      OrderID = 0
    };
  }

  /// <param name="factory"></param>
  public static void RegisterViewProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    factory.AddViewsProvider(SelectObjectFromProductionCopyWizardControl.RootCategoryNodeId, (IViewsProvider) new SelectObjectFromProductionCopyWizardProvider());
  }
}
