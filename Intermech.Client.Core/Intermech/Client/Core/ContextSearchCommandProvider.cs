
// Type: Intermech.Client.Core.ContextSearchCommandProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Провайдер команд для контекстного поиска</summary>
internal sealed class ContextSearchCommandProvider : ICommandsProvider
{
  private NavigatorContextSearchForm _navigatorContextSearchForm;

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if ((viewServices != null ? viewServices.GetService(typeof (INavigatorContextSearch)) as INavigatorContextSearch : (INavigatorContextSearch) null) == null)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("NavigatorContextSearch", new CommandInfo(0, new ClickEventHandler(this.NavigatorContextSearch)));
    if (this._navigatorContextSearchForm != null && !string.IsNullOrEmpty(this._navigatorContextSearchForm.FindWhat))
      groupCommands.Add("NavigatorContextSearchNext", new CommandInfo(0, new ClickEventHandler(this.NavigatorContextSearchNext)));
    return groupCommands;
  }

  private void NavigatorContextSearch(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (this._navigatorContextSearchForm != null)
      this._navigatorContextSearchForm.Close();
    INavigatorContextSearch service1 = viewServices != null ? viewServices.GetService(typeof (INavigatorContextSearch)) as INavigatorContextSearch : (INavigatorContextSearch) null;
    if (service1 == null)
      return;
    NavigatorContextSearchForm contextSearchForm = new NavigatorContextSearchForm();
    contextSearchForm.NavigatorContextSearch = service1;
    contextSearchForm.BringToFront();
    if (viewServices.GetService(typeof (ChildrenView)) is ChildrenView service2)
      contextSearchForm.Show((IWin32Window) service2);
    else
      contextSearchForm.Show();
    contextSearchForm.SetFocusToComboBox();
    this._navigatorContextSearchForm = contextSearchForm;
  }

  private void NavigatorContextSearchNext(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    INavigatorContextSearch service = viewServices != null ? viewServices.GetService(typeof (INavigatorContextSearch)) as INavigatorContextSearch : (INavigatorContextSearch) null;
    if (service == null)
      return;
    this._navigatorContextSearchForm.NavigatorContextSearch = service;
    this._navigatorContextSearchForm.FindNext();
  }
}
