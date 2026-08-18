// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.ProcRouteCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>
/// 
/// </summary>
internal class ProcRouteCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  public ProcRouteCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "throughCreateNode", LocalizationHolder.rm.GetString("TechCard.Client_206"), -1, 13, 90);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "throughAddOperNode", LocalizationHolder.rm.GetString("TechCard.Client_207"), -1, 13, 98);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void ThroughCreateCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    ProcRouteThroughCreateCommand throughCreateCommand = new ProcRouteThroughCreateCommand();
    throughCreateCommand.Init(items, viewServices, (object) null);
    throughCreateCommand.Execute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void ThroughAddOperCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    ProcRouteThroughAddOperCommand throughAddOperCommand = new ProcRouteThroughAddOperCommand();
    throughAddOperCommand.Init(items, viewServices, (object) null);
    throughAddOperCommand.Execute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void AddCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    ProcRouteAddItemsCommand routeAddItemsCommand = new ProcRouteAddItemsCommand();
    routeAddItemsCommand.Init(items, viewServices, additionalInfo);
    routeAddItemsCommand.Execute();
  }

  /// <summary>Скрывать ли команду Вставить для выделенных объектов</summary>
  /// <param name="commandsInfo"></param>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  private void IsPasteCommandAllow(
    CommandsInfo commandsInfo,
    ISelectedItems items,
    IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0)
      return;
    if (Intermech.TechCard.Client.Commands.PasteCommand.AllowCommand(items, viewServices))
      commandsInfo.Add("Paste", new CommandInfo(3, new ClickEventHandler(ProcRouteCommandProvider.PasteCommand)));
    else
      commandsInfo.Suppress("Paste", 0);
  }

  /// <summary>Реализация команды "Вставить"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void PasteCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    ProcRoutePasteItemsCommand pasteItemsCommand = new ProcRoutePasteItemsCommand();
    pasteItemsCommand.Init(items, viewServices, additionalInfo);
    pasteItemsCommand.Execute();
  }

  /// <summary>GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
      mergedCommands.Add("Add", new CommandInfo(0, new ClickEventHandler(ProcRouteCommandProvider.AddCommand)));
    if ((viewStateFlags & ViewStateFlags.ReadOnly) != ViewStateFlags.None || (viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None || items.Count != 1)
      return mergedCommands;
    mergedCommands.Add("throughCreateNode", new CommandInfo(0, new ClickEventHandler(ProcRouteCommandProvider.ThroughCreateCommand)));
    mergedCommands.Add("throughAddOperNode", new CommandInfo(0, new ClickEventHandler(ProcRouteCommandProvider.ThroughAddOperCommand)));
    return mergedCommands;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo commandsInfo = new CommandsInfo();
    if (((!(viewServices.GetService(typeof (IViewState)) is IViewState service1) ? 0L : (long) service1.ViewState) & 2L) != 2L)
    {
      IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        object dataObject = service.GetDataObject();
        if (dataObject != null)
        {
          if (!(dataObject is IDBObjectTypedIDCollection))
            commandsInfo.Suppress("Paste", 0);
          else
            this.IsPasteCommandAllow(commandsInfo, items, viewServices);
        }
      }
    }
    return commandsInfo;
  }
}
