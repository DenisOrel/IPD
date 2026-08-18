// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IntegrationErrors.MenuCommandsProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Tools.Client.IntegrationErrors;

internal sealed class MenuCommandsProvider : ICommandsProvider
{
  private Func<IntegrationErrorsWindow> integrationErrorsWindowFactory;

  public MenuCommandsProvider(
    Func<IntegrationErrorsWindow> integrationErrorsWindowFactory)
  {
    this.integrationErrorsWindowFactory = integrationErrorsWindowFactory != null ? integrationErrorsWindowFactory : throw new ArgumentNullException(nameof (integrationErrorsWindowFactory));
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    if (items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add(MenuConsts.ShowIntegrationErrorsCommandName, new CommandInfo(0, new ClickEventHandler(this.ShowIntegrationErrorsHandler)));
    return groupCommands;
  }

  private void ShowIntegrationErrorsHandler(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBObjectID itemData = (IDBObjectID) items.GetItemData(0, typeof (IDBObjectID));
    using (IntegrationErrorsWindow integrationErrorsWindow = this.integrationErrorsWindowFactory())
    {
      integrationErrorsWindow.ObjectId = itemData.Value;
      int num = (int) integrationErrorsWindow.ShowDialog();
    }
  }
}
