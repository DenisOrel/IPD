// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views.ArtsCompositionApplicabilityCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.ContextMenu.Extensions;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views;

/// <summary>
/// // Провайдер команд локального контекстного меню для закладки "Применяемость в ТП"
/// </summary>
internal class ArtsCompositionApplicabilityCommandProvider : 
  ILocalCommandsProvider,
  ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  private MenuTemplateNode _findTechObjectMenuTemplateNode;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void FindTechObjectHandler(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      return;
    ArtsCompositionApplicabilityParams service = viewServices != null ? viewServices.GetService<ArtsCompositionApplicabilityParams>(false) : (ArtsCompositionApplicabilityParams) null;
    NavigatorTreeView navigatorTreeView1;
    if (service == null)
    {
      navigatorTreeView1 = (NavigatorTreeView) null;
    }
    else
    {
      System.IServiceProvider serviceProvider = service.ServiceProvider;
      navigatorTreeView1 = serviceProvider != null ? serviceProvider.GetService<NavigatorTreeView>(false) : (NavigatorTreeView) null;
    }
    NavigatorTreeView navigatorTreeView2 = navigatorTreeView1;
    if (navigatorTreeView2 == null)
      return;
    ArtsCompositionApplicabilityFindObjectCommand findObjectCommand = new ArtsCompositionApplicabilityFindObjectCommand(navigatorTreeView2.RootNode);
    findObjectCommand.Init(items, viewServices, additionalInfo);
    findObjectCommand.Execute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  CommandsInfo ICommandsProvider.GetGroupCommands(
    ISelectedItems items,
    System.IServiceProvider viewServices)
  {
    if (items == null)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    if (items.Count == 1 && items.GetItemData<IDBObjectID>(0, false) != null && (viewServices != null ? viewServices.GetService<ArtsCompositionApplicabilityParams>(false) : (ArtsCompositionApplicabilityParams) null) != null)
      groupCommands.Add("FindTechObject", new CommandInfo(0, new ClickEventHandler(this.FindTechObjectHandler)));
    return groupCommands;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  CommandsInfo ICommandsProvider.GetMergedCommands(
    ISelectedItems items,
    System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="contextMenuTemplate"></param>
  void ILocalCommandsProvider.InitCommandTemplates(MenuTemplate contextMenuTemplate)
  {
    if (this._findTechObjectMenuTemplateNode != null)
      return;
    this._findTechObjectMenuTemplateNode = new MenuTemplateNode("FindTechObject", LocalizationHolder.rm.GetString("TechCard.Client_537"), -1, 0, 0, Keys.None);
    contextMenuTemplate.Nodes.Add(this._findTechObjectMenuTemplateNode);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="contextMenuTemplate"></param>
  void ILocalCommandsProvider.DisposeCommandTemplates(MenuTemplate contextMenuTemplate)
  {
    if (this._findTechObjectMenuTemplateNode == null)
      return;
    ApplicationServices.Container.GetService<IHotKeysManager>(false)?.UnregisterCommand(this._findTechObjectMenuTemplateNode.Name);
    contextMenuTemplate.Nodes.Remove(this._findTechObjectMenuTemplateNode);
    this._findTechObjectMenuTemplateNode = (MenuTemplateNode) null;
  }
}
