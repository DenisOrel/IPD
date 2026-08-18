// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardBaseCheckInOutCommandsProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>Override base command provider</summary>
internal class TechCardBaseCheckInOutCommandsProvider : CheckInOutCommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="commandsInfo"></param>
  public override void Postprocess(CommandsInfo commandsInfo)
  {
    if (this.AllowCheckOut)
      commandsInfo.Add("CheckOut", new CommandInfo(0, new ClickEventHandler(TechCardBaseCheckInOutCommandsProvider.CheckOutCommand)));
    if (this.AllowCheckIn)
      commandsInfo.Add("CheckIn", new CommandInfo(0, new ClickEventHandler(TechCardBaseCheckInOutCommandsProvider.CheckinCommand)));
    if (this.AllowSave)
      commandsInfo.Add("SaveChanges", new CommandInfo(0, new ClickEventHandler(TechCardBaseCheckInOutCommandsProvider.SaveChangesCommand)));
    if (this.AllowAdminCancel)
      commandsInfo.Add("AdminCancelChanges", new CommandInfo(0, new ClickEventHandler(TechCardBaseCheckInOutCommandsProvider.AdminCancelCommand)));
    if (!this.AllowCancel)
      return;
    commandsInfo.Add("CancelChanges", new CommandInfo(0, new ClickEventHandler(TechCardBaseCheckInOutCommandsProvider.CancelCommand)));
  }

  /// <summary>Реализация команды "Взять на редактирование"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CheckOutCommand(
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
    Intermech.TechCard.Client.Commands.CheckOutCommand checkOutCommand = new Intermech.TechCard.Client.Commands.CheckOutCommand();
    checkOutCommand.Init(items, viewServices, additionalInfo);
    checkOutCommand.Execute();
  }

  /// <summary>Реализация команды "Завершить редактирование"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CheckinCommand(
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
    CheckInCommand checkInCommand = new CheckInCommand();
    checkInCommand.Init(items, viewServices, additionalInfo);
    checkInCommand.Execute();
  }

  /// <summary>Отмена изменений в объектах</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void CancelCommand(
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
    UndoChangedCommand undoChangedCommand = new UndoChangedCommand();
    undoChangedCommand.Init(items, viewServices, additionalInfo);
    undoChangedCommand.Execute();
  }

  /// <summary>Отмена чужих изменений в объектах</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void AdminCancelCommand(
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
    ObjectCommands.AdminCancelCommand(items, viewServices, additionalInfo);
    TechCardSelectedItemsCommand.ClearCheckedItems(viewServices);
  }

  /// <summary>Сохранение изменений в объектах</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void SaveChangesCommand(
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
    SaveChangedCommand saveChangedCommand = new SaveChangedCommand();
    saveChangedCommand.Init(items, viewServices, additionalInfo);
    saveChangedCommand.Execute();
  }
}
