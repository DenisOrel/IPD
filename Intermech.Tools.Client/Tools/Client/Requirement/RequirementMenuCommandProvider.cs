// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Requirement.RequirementMenuCommandProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Services.Requirement;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Notifications;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Requirement;

internal sealed class RequirementMenuCommandProvider : ICommandsProvider
{
  private IRequirementsService _requirementsService;
  private IFileVault _fileVaultService;

  public RequirementMenuCommandProvider(
    IRequirementsService requirementsService,
    IFileVault fileVaultService)
  {
    this._requirementsService = requirementsService;
    this._fileVaultService = fileVaultService;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    if (items.Count == 1)
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
      if (itemData != null)
      {
        RequirementsSettings requirementsSettings = new RequirementsSettings();
        requirementsSettings.Load();
        if (requirementsSettings.EnableRequirement && requirementsSettings.EnableRequirementForCurrentUser)
        {
          IntegratorObject iobj = IntegratorServices.Find(itemData.ObjectType);
          if (iobj == null)
            return CommandsInfo.Empty;
          IIntegrator integrator = ClientContext.Integrators.GetIntegrator(iobj, false);
          ITechRequirementsService service = integrator != null ? integrator.GetService<ITechRequirementsService>(false) : (ITechRequirementsService) null;
          if (service != null && service.CanGetTechRequirements(itemData.ObjectType))
          {
            CommandsInfo groupCommands = new CommandsInfo();
            groupCommands.Add(RequirementMenuConsts.GetRequirementsCommandName, new CommandInfo(0, new ClickEventHandler(this.UpdateRequirementsCommandHandler)));
            return groupCommands;
          }
        }
      }
    }
    return CommandsInfo.Empty;
  }

  private void UpdateRequirementsCommandHandler(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    if (!PDMHelper.IsDocumentWithArticles(itemData.ObjectType))
      return;
    DataTable documentArticles = DBDocumentHelper.FindDocumentArticles(itemData.ObjectID, VersionsRuleSources.GetEditorRule(), false);
    if (documentArticles == null || documentArticles.Rows.Count == 0)
    {
      int num = (int) MessageBox.Show("Невозможно получить технические требования т.к. у документа отсутствует изделие. Для начала выполните расширенное сохранение.", RequirementMenuConsts.GetRequirementsDisplayName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      IntegratorObject iobj = IntegratorServices.Find(itemData.ObjectType);
      if (iobj == null)
        return;
      IIntegrator integrator = ClientContext.Integrators.GetIntegrator(iobj, false);
      ITechRequirementsService service = integrator != null ? integrator.GetService<ITechRequirementsService>(false) : (ITechRequirementsService) null;
      if (service == null)
        return;
      string filePath = this._fileVaultService.PublishTree(itemData.ObjectID, true, VersionsRuleSources.GetEditorRule(), (IFileArea) this._fileVaultService.WorkArea);
      this._requirementsService.UpdateRequirements(new CaptureChangesDocumentInfo(itemData.ObjectID, itemData.ObjectType, filePath, true, true), integrator, service);
    }
  }
}
