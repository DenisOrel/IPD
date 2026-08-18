// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTableCommands
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Класс для обработки команды "Редактировать"</summary>
public class ExpertTableCommands : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(ExpertTableCommands.EditDocument_ClickEvent)));
    return mergedCommands;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Редактировать таблиицу</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void EditDocument_ClickEvent(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    long objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject1 = session.GetObject(objectID);
      long checkoutBy = dbObject1.CheckoutBy;
      IDBObject dbObject2;
      if (!checkoutBy.Equals(0L))
      {
        checkoutBy = dbObject1.CheckoutBy;
        if (!checkoutBy.Equals(session.UserID))
          throw new ArgumentException(LocalizationHolder.rm.GetString("Expert.Editor_14"));
        dbObject2 = dbObject1;
      }
      else
      {
        dbObject2 = dbObject1.CheckOut();
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
        {
          dbObject1.ObjectID
        }, (IList<long>) new long[1]{ dbObject2.ObjectID }));
      }
      if (dbObject2 == null)
        return;
      Services.InvokeCommand("OpenInNewWindow", Services.GetCommandsTable(Services.GetItems(dbObject2.ObjectID), viewServices), viewServices);
    }
  }
}
