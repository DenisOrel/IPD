// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProjectCommands
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Project.Client;

public class ProjectCommands : ICommandsProvider
{
  private StepwiseProviderManager _checkInOutManager;

  public ProjectCommands([NotNull] ProjectClientPlugin plugin)
  {
  }

  [NotNull]
  public CommandsInfo GetMergedCommands([NotNull] ISelectedItems items, [CanBeNull] IServiceProvider viewServices)
  {
    CommandsInfo commandsInfo = new CommandsInfo();
    int lcStep = 0;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ISitesCacheService customService = sessionKeeper.Session.GetCustomService<ISitesCacheService>(false);
      for (int index = 0; index < items.Count; ++index)
      {
        IDBLCStepID itemData1 = items.GetItemData<IDBLCStepID>(index, false);
        if (itemData1 != null)
        {
          if (lcStep == 0)
            lcStep = itemData1.LCStepID;
          else if (lcStep != itemData1.LCStepID)
          {
            lcStep = 0;
            break;
          }
        }
        if (!flag && customService?.Info != null)
        {
          IDBTypedObjectID itemData2 = items.GetItemData<IDBTypedObjectID>(index, false);
          if (itemData2 != null && SiteIDHelper.IsForeign(customService, itemData2.SiteID))
            flag = true;
        }
      }
    }
    commandsInfo.Add("EditDocument", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.EditProjectCommand)));
    commandsInfo.Add("ViewDocument", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.ViewProjectCommand)));
    commandsInfo.Add("View", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.ViewProjectCommand)));
    commandsInfo.Add("Edit", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.EditProjectCommand)));
    if (!flag && lcStep != 0)
    {
      Intermech.Project.TaskStatus taskStatus = Intermech.Project.Helper.LCStepToTaskStatus(lcStep);
      if (taskStatus == Intermech.Project.TaskStatus.NotStarted || taskStatus == Intermech.Project.TaskStatus.Terminated)
        commandsInfo.Add("StartProject", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.StartProjectCommand)));
      if (taskStatus == Intermech.Project.TaskStatus.Sent || taskStatus == Intermech.Project.TaskStatus.Executed)
        commandsInfo.Add("AbortProject", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.AbortProjectCommand)));
    }
    if (this._checkInOutManager == null)
    {
      this._checkInOutManager = new StepwiseProviderManager();
      this._checkInOutManager.Providers.Add((IStepwiseCommandsProvider) new ProjectCheckInOutCommandsProvider());
    }
    this._checkInOutManager.CollectCommands(items, viewServices, commandsInfo);
    return commandsInfo;
  }

  [NotNull]
  public CommandsInfo GetGroupCommands([CanBeNull] ISelectedItems items, [CanBeNull] IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void ViewProjectCommand(
    [NotNull] ISelectedItems items,
    [CanBeNull] IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    IMProject.OpenProject(items, false);
  }

  public void EditProjectCommand(
    [NotNull] ISelectedItems items,
    [CanBeNull] IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    IMProject.OpenProject(items, true);
  }

  public void StartProjectCommand(
    [NotNull] ISelectedItems items,
    [CanBeNull] IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    IMProject.StartProject(items);
  }

  public void AbortProjectCommand(
    [NotNull] ISelectedItems items,
    [CanBeNull] IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    IMProject.AbortProject(items);
  }
}
