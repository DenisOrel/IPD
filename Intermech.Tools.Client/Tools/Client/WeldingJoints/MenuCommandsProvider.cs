// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.WeldingJoints.MenuCommandsProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Services.WeldingJoints;
using Intermech.Tools.Integrators;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.WeldingJoints;

internal sealed class MenuCommandsProvider : ICommandsProvider
{
  private IIntegratorRegistry integratorRegistry;
  private IWeldingJointsService weldingJointsService;

  public MenuCommandsProvider(
    IIntegratorRegistry integratorRegistry,
    IWeldingJointsService weldingJointsService)
  {
    this.integratorRegistry = integratorRegistry;
    this.weldingJointsService = weldingJointsService;
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
      if (itemData != null && this.weldingJointsService.CanUpdateWeldingSeams(itemData.ObjectType))
      {
        CommandsInfo groupCommands = new CommandsInfo();
        groupCommands.Add(MenuConsts.UpdateWeldingSeamsCommandName, new CommandInfo(0, new ClickEventHandler(this.UpdateWeldingSeamsCommandHandler)));
        return groupCommands;
      }
    }
    return CommandsInfo.Empty;
  }

  private void UpdateWeldingSeamsCommandHandler(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    UpdateWeldingSeamsResult weldingSeamsResult = this.weldingJointsService.UpdateWeldingSeams(itemData);
    if (weldingSeamsResult.IsSuccessful || weldingSeamsResult.DocumentsWithoutArticles.Count == 0)
      return;
    long documentsWithoutArticle = weldingSeamsResult.DocumentsWithoutArticles[0];
    int num = (int) MessageBox.Show($"Не удалось обновить сварные швы в базе данных IPS, так как у документа '{(documentsWithoutArticle == itemData.ObjectID ? (object) itemData.Caption : (object) DBHelper.GetObjectCaption(documentsWithoutArticle))}' (ид. версии {documentsWithoutArticle}) отсутствуют выпускаемые по нему изделия. Сначала воспользуйтесь командой 'Расширенное сохранение', а затем повторите текущую операцию.", MenuConsts.UpdateWeldingSeamsDisplayName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }
}
