// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Editor.wfCommandProvider
// Assembly: Intermech.Workflow.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 48E18BC1-AABA-4AA1-97DA-4BBD788BE326
// Assembly location: D:\IPS\Client\Intermech.Workflow.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Editor.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Editor;

/// <summary>Summary description for wfCommandProvider.</summary>
public class wfCommandProvider : ICommandsProvider
{
  private WorkflowPlugin _plugin;
  private StepwiseProviderManager _checkInOutManager;

  public wfCommandProvider(WorkflowPlugin plugin) => this._plugin = plugin;

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo commandsInfo = new CommandsInfo();
    commandsInfo.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(this.EditSchemeCommand)));
    if (this._checkInOutManager == null)
    {
      this._checkInOutManager = new StepwiseProviderManager();
      this._checkInOutManager.Providers.Add((IStepwiseCommandsProvider) new wfCheckInOutCommandsProvider());
    }
    this._checkInOutManager.CollectCommands(items, viewServices, commandsInfo);
    return commandsInfo;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void EditSchemeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    Form service = viewServices.GetService(typeof (Form)) as Form;
    wfFunx.OpenProcess(items, true, service != null && service.Modal);
  }
}
