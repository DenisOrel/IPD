// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech.DraftCadmContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech;

internal class DraftCadmContextCommandProvider : ICommandsProvider
{
  /// <summary>Опции для окна "Удаление объектов" по умолчанию</summary>
  internal static DeleteAnalyzerOptions _deleteOptions;

  public DraftCadmContextCommandProvider()
  {
    IFactory service = (IFactory) ServicesManager.GetService(typeof (IFactory));
    if (service == null)
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "ReplaceDimText", "Передать параметры в эскиз", -1, 13, 99);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>Реализация команды "Удалить"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void DeleteCommand(
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
    if (items.Count == sc_19499.ssp_techcard_19500(1378770156))
      return;
    DraftCadmDeleteItemsCommand deleteItemsCommand = new DraftCadmDeleteItemsCommand();
    deleteItemsCommand.Init(items, viewServices, additionalInfo);
    deleteItemsCommand.Execute();
  }

  private static void EditCommand(
    ISelectedItems items,
    IServiceProvider viewservices,
    object additionalinfo)
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
    if (items.Count == sc_19499.ssp_techcard_19501(215115301))
      return;
    DraftCadmEditCommand draftCadmEditCommand = new DraftCadmEditCommand();
    draftCadmEditCommand.Init(items, viewservices, additionalinfo);
    draftCadmEditCommand.Execute();
  }

  private static void ReplaceTextCommand(
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
    if (items.Count == sc_19499.ssp_techcard_19502(74072837))
      return;
    DraftCadmReplaceTextCommand replaceTextCommand = new DraftCadmReplaceTextCommand();
    replaceTextCommand.Init(items, viewServices, additionalInfo);
    replaceTextCommand.Execute();
  }

  /// <summary>GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (!(viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView))
      return CommandsInfo.Empty;
    ViewStateFlags viewStateFlags = !(viewServices.GetService(typeof (IViewState)) is IViewState service) ? ViewStateFlags.None : service.ViewState;
    CommandsInfo mergedCommands = new CommandsInfo();
    if ((viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None)
      return mergedCommands;
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
    {
      if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None)
        mergedCommands.Add("Delete", new CommandInfo(3, new ClickEventHandler(DraftCadmContextCommandProvider.DeleteCommand)));
      mergedCommands.Add("EditDocument", new CommandInfo(3, new ClickEventHandler(DraftCadmContextCommandProvider.EditCommand)));
      mergedCommands.Add("editObjectNode", new CommandInfo(3, new ClickEventHandler(DraftCadmContextCommandProvider.EditCommand)));
      if (items.GetParentData(0, typeof (IDBObjectID)) is IDBObjectID)
        mergedCommands.Add("ReplaceDimText", new CommandInfo(3, new ClickEventHandler(DraftCadmContextCommandProvider.ReplaceTextCommand)));
    }
    mergedCommands.Suppress("Cut", 0);
    mergedCommands.Suppress("Copy", 0);
    return mergedCommands;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }
}
