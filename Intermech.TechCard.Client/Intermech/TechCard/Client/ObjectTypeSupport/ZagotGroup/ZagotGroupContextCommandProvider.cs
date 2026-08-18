// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ZagotGroup.ZagotGroupContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Commands.Edit;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ZagotGroup;

internal class ZagotGroupContextCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  public ZagotGroupContextCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "ApplyGroupAttributes", LocalizationHolder.rm.GetString("TechCard.Client_546"), -1, 10, 95);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private static void EditObjectCommand(
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
    SimpleEditCommand simpleEditCommand = new SimpleEditCommand();
    simpleEditCommand.Init(items, viewServices, additionalInfo);
    simpleEditCommand.Execute();
  }

  /// <summary>Передать атрибуты в единичные объекты</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void ApplyGroupAttributes(
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
    ApplyGroupAttributesFromObjectCommand fromObjectCommand = new ApplyGroupAttributesFromObjectCommand(nameof (ApplyGroupAttributes));
    fromObjectCommand.Init(items, viewServices, additionalInfo);
    fromObjectCommand.Execute();
  }

  /// <summary>GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = CommandsInfo.Empty;
    if (items == null || items.Count == 0)
      return mergedCommands;
    IViewState service = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    ViewStateFlags viewStateFlags = service != null ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
    {
      mergedCommands = new CommandsInfo();
      mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(ZagotGroupContextCommandProvider.EditObjectCommand)));
      mergedCommands.Add("ApplyGroupAttributes", new CommandInfo(0, new ClickEventHandler(ZagotGroupContextCommandProvider.ApplyGroupAttributes)));
    }
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
