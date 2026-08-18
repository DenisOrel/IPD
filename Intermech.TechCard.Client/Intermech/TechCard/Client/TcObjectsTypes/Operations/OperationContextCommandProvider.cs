// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Operations.OperationContextCommandProvider
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
using Intermech.TechCard.Client.Commands;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Operations;

/// <summary>
/// Класс реализующий команды контекстного меню для объектов типа "Операция"
/// </summary>
internal class OperationContextCommandProvider : ICommandsProvider
{
  /// <summary>"Добавить в состав"</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  private static void AddCommandTpNode(
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
    AddObjectCommand addObjectCommand = new AddObjectCommand(true);
    addObjectCommand.Init(items, viewServices, additionalInfo);
    addObjectCommand.Execute();
  }

  /// <summary>Конструктор</summary>
  public OperationContextCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (viewServices == null)
      return CommandsInfo.Empty;
    long viewState = !(viewServices.GetService(typeof (IViewState)) is IViewState service) ? 0L : (long) service.ViewState;
    CommandsInfo mergedCommands = new CommandsInfo();
    if ((viewState & 2L) == 0L)
      mergedCommands.Add("Add", new CommandInfo(0, new ClickEventHandler(OperationContextCommandProvider.AddCommandTpNode)));
    return mergedCommands;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    bool flag = ((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) == 2L;
    if (items.Count == 1)
    {
      IMSObjectType objectType = items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? MetaDataHelper.GetObjectType(itemData.ObjectType) : (IMSObjectType) null;
      if (!flag && objectType != null && MetaDataHelper.GetObjectTypeApplicabilities(objectType.ObjectTypeID).Count != 0)
        groupCommands.Add("Add", new CommandInfo(0, new ClickEventHandler(OperationContextCommandProvider.AddCommandTpNode)));
    }
    return groupCommands;
  }
}
