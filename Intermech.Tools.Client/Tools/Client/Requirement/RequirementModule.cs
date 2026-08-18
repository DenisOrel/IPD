// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Requirement.RequirementModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Services.Requirement;
using Intermech.Tools.Client.IntegratorsContextMenu;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Notifications;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Requirement;

internal sealed class RequirementModule : InitializerModule
{
  private IPropertyPagesService _propertyPagesService;
  private INotificationService _notificationService;
  private IRequirementsService _requirementsService;
  private IOutputView _outputView;
  private IFactory _navigatorFactory;
  private MenuTemplateNode _updateRequirementCommandNode;
  private RequirementMenuCommandProvider _commandsProviderFactory;

  public RequirementModule(
    IPropertyPagesService propertyPagesService,
    INotificationService notificationService,
    IRequirementsService requirementsService,
    IOutputView outputView,
    IFactory navigatorFactory,
    RequirementMenuCommandProvider commandsProviderFactory)
  {
    this._propertyPagesService = propertyPagesService;
    this._notificationService = notificationService;
    this._requirementsService = requirementsService;
    this._outputView = outputView;
    this._navigatorFactory = navigatorFactory;
    this._commandsProviderFactory = commandsProviderFactory;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this._notificationService.Subscribe(CaptureChangesEventArgs.CaptureChangesCompleted, new NotificationEventHandler(this.UpdateRequirementsSaveChanges));
    this._propertyPagesService.AddPage("Система\\Управление информацией об изделиях\\Технические требования", (IPropertyPage) new RequirementsSettingsPage());
    this.AddCommandItemsToContextMenuTemplate();
    this.AddCommandsProviderToNavigator();
  }

  private void AddCommandItemsToContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this._navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode menuTemplateNode = contextMenuTemplate[MenuConsts.IntegratorsMenuName];
      if (menuTemplateNode == null)
        return;
      this._updateRequirementCommandNode = new MenuTemplateNode(RequirementMenuConsts.GetRequirementsCommandName, RequirementMenuConsts.GetRequirementsDisplayName, -1, 27, 31 /*0x1F*/);
      menuTemplateNode.Nodes.Add(this._updateRequirementCommandNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private void AddCommandsProviderToNavigator()
  {
    this._navigatorFactory.AddCommandsProvider(1, (ICommandsProvider) this._commandsProviderFactory);
  }

  private void UpdateRequirementsSaveChanges(object sender, NotificationEventArgs e)
  {
    try
    {
      RequirementsSettings requirementsSettings = new RequirementsSettings();
      requirementsSettings.Load();
      if (!requirementsSettings.EnableRequirement || !requirementsSettings.EnableRequirementForCurrentUser || !(e is CaptureChangesEventArgs changesEventArgs) || !changesEventArgs.IsExtendedSave || changesEventArgs.Documents.Count == 0)
        return;
      ITechRequirementsService service = changesEventArgs.Integrator.GetService<ITechRequirementsService>(false);
      if (service == null)
        return;
      foreach (CaptureChangesDocumentInfo document in changesEventArgs.Documents)
      {
        if (document.IsInitialDocument)
          this._requirementsService.UpdateRequirements(document, changesEventArgs.Integrator, service);
      }
    }
    catch (Exception ex)
    {
      this._outputView.WriteString("Ошибки", ex.Message);
      int num = (int) MessageBox.Show("В процессе получения технических требований возникли ошибки. Подробности смотрите в окне 'Ошибки'.", "Внимание");
      this._outputView.Activate("Ошибки");
      this._outputView.ShowView();
    }
  }

  protected override void DoShutdown()
  {
    if (this._notificationService.HasSubscribers(CaptureChangesEventArgs.CaptureChangesCompleted))
      this._notificationService.Unsubscribe(CaptureChangesEventArgs.CaptureChangesCompleted, new NotificationEventHandler(this.UpdateRequirementsSaveChanges));
    this.RemoveCommandItemsFromContextMenuTemplate();
    base.DoShutdown();
  }

  private void RemoveCommandItemsFromContextMenuTemplate()
  {
    if (this._updateRequirementCommandNode == null)
      return;
    MenuTemplate contextMenuTemplate = this._navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate[MenuConsts.IntegratorsMenuName]?.Nodes.Remove(this._updateRequirementCommandNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    this._updateRequirementCommandNode = (MenuTemplateNode) null;
  }
}
