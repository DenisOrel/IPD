// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE.DraftOleContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE;
using System;
using System.IO;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;

/// <summary>
/// 
/// </summary>
internal class DraftOleContextCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) != ViewStateFlags.None || (viewStateFlags & ViewStateFlags.ReadOnly) != ViewStateFlags.None || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(DraftOleContextCommandProvider.EditObjectCommand)));
    mergedCommands.Add("ViewDocument", new CommandInfo(0, new ClickEventHandler(DraftOleContextCommandProvider.ViewObjectCommand)));
    mergedCommands.Suppress("ViewWithOptions", 0);
    mergedCommands.Suppress("PrintDocument", 0);
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public static void EditObjectCommand(
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
    DraftOleEditCommand draftOleEditCommand = new DraftOleEditCommand();
    draftOleEditCommand.Init(items, viewServices, additionalInfo);
    draftOleEditCommand.Execute();
  }

  /// <summary>Просмотр документа</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void ViewObjectCommand(
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
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    DraftOleClass draftOleClass = new DraftOleClass(itemData.Value);
    if (!draftOleClass.LoadData())
      return;
    Stream dataStream = draftOleClass.DataStream;
    DraftOleEditDialog.ShowModal(ref dataStream, itemData.Caption, true, true);
  }
}
