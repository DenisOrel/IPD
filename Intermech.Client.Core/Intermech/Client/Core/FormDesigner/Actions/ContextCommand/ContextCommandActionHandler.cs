
// Type: Intermech.Client.Core.FormDesigner.Actions.ContextCommand.ContextCommandActionHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Commands.CommandCache;
using Intermech.Client.Core.FormDesigner.Actions.SaveChangesSupport;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.FormDesigner.Actions.ContextCommand;

/// <summary>
/// Обработки действий по нажатию на кнопку "комманды контекстного меню"
/// </summary>
internal class ContextCommandActionHandler : ActionSaveChangesHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="desForm"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  private ISelectedItems GetSelectedItems(DesForm desForm, IServiceProvider viewServices)
  {
    if (desForm == null)
      throw new ArgumentNullException(nameof (desForm));
    ISelectedItems selectedItems = ((desForm.ServiceProvider ?? viewServices) ?? throw new ArgumentNullException("desForm ServiceProvider")).GetService<ISelectedItems>(false);
    if (selectedItems != null)
      return selectedItems;
    IElementInfo info = desForm.Info;
    if (info == null)
      return selectedItems;
    switch (info.ElementKind)
    {
      case AttributableElements.Object:
        selectedItems = ObjectExtensions.GetItems(info.ElementIdentifier);
        break;
      case AttributableElements.Relation:
        selectedItems = RelationExtensions.GetItems(new Dictionary<long, List<long>>()
        {
          {
            0L,
            new List<long>() { info.ElementIdentifier }
          }
        });
        break;
    }
    return selectedItems;
  }

  /// <summary>Check button's state</summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  protected override bool DoButtonEnabled(object button, object form)
  {
    DesForm desForm = form as DesForm;
    AttrButton attrButton = button as AttrButton;
    if (desForm == null || attrButton == null || !(attrButton.FormDesignerActionParams is ContextCommandActionParams designerActionParams))
      return false;
    if (attrButton.Tag is bool && Convert.ToBoolean(attrButton.Tag).Equals(true))
      return attrButton.Enabled;
    if (designerActionParams.Method == null)
      return false;
    ICommandCacheService service = ServiceUtils.GetService<ICommandCacheService>((object) ServicesManager.ServiceContainer, true);
    IServiceProvider viewServices = desForm.ServiceProvider ?? (IServiceProvider) ServicesManager.ServiceContainer;
    ISelectedItems selectedItems = this.GetSelectedItems(desForm, viewServices);
    CommandsTable commandsTable = selectedItems != null ? service.GetCommandsTable(selectedItems, viewServices, false) : (CommandsTable) null;
    return commandsTable != null && commandsTable[designerActionParams.Method.CommandName] != null;
  }

  /// <summary>Implementation of button's press events</summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  protected override void DoButtonPressed(object button, object form)
  {
    DesForm desForm = form as DesForm;
    AttrButton attrButton = button as AttrButton;
    if (desForm == null || attrButton == null || !(attrButton.FormDesignerActionParams is ContextCommandActionParams designerActionParams))
      return;
    ICommandCacheService service1 = ServiceUtils.GetService<ICommandCacheService>((object) ServicesManager.ServiceContainer, true);
    IServiceProvider serviceProvider = desForm.ServiceProvider ?? (IServiceProvider) ServicesManager.ServiceContainer;
    ISelectedItems selectedItems = this.GetSelectedItems(desForm, serviceProvider);
    IServiceProvider viewServices = serviceProvider;
    CommandsTable commandsTable = service1.GetCommandsTable(selectedItems, viewServices, false);
    if (commandsTable == null || commandsTable[designerActionParams.Method.CommandName] == null)
      return;
    NavigatorTreeView service2 = ServiceUtils.GetService<NavigatorTreeView>((object) serviceProvider, false);
    bool flag = service2 == null || service2.DisableChangeSelectedNodeDuringNotificationProcessing;
    try
    {
      if (service2 != null)
        service2.DisableChangeSelectedNodeDuringNotificationProcessing = !designerActionParams.AllowObjectSelection;
      Intermech.Navigator.ContextMenu.Services.InvokeCommand(designerActionParams.Method.CommandName, commandsTable, serviceProvider);
    }
    finally
    {
      if (service2 != null)
        service2.DisableChangeSelectedNodeDuringNotificationProcessing = flag;
    }
  }

  /// <summary>Constructor</summary>
  public ContextCommandActionHandler() => this.InitData();

  /// <summary>Initialize object data</summary>
  internal void InitData()
  {
  }
}
