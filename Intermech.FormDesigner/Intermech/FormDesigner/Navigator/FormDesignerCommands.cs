// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Navigator.FormDesignerCommands
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.FormDesigner.Navigator;

/// <summary>
/// 
/// </summary>
internal class FormDesignerCommands : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = CommandsInfo.Empty;
    if (items.Count == 1)
    {
      mergedCommands = new CommandsInfo();
      mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(FormDesignerCommands.EditDocument_ClickEvent)));
    }
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

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void EditDocument_ClickEvent(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
      IDBObject dbObject = session.GetObject(objectID);
      switch (dbObject.ObjectModifyMode)
      {
        case ObjectModifyModes.Checkout:
        case ObjectModifyModes.CreateVersion:
          if (dbObject.CheckoutBy == 0L)
          {
            dbObject = dbObject.CheckOut();
            INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
            if (service != null)
            {
              DBObjectsEventArgs e = (DBObjectsEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
              {
                objectID
              }, (IList<long>) new long[1]
              {
                dbObject.ObjectID
              });
              service.FireEvent((object) null, (NotificationEventArgs) e);
              break;
            }
            break;
          }
          if (dbObject.CheckoutBy != session.UserID)
            throw new ArgumentException(LocalizationHolder.rm.GetString(sc_7169.ssp_imclient_7170()));
          break;
        case ObjectModifyModes.CantModify:
          throw new ArgumentException(LocalizationHolder.rm.GetString("FormDesigner_15"));
      }
      if (dbObject == null)
        return;
      ISelectedItems items1 = Services.GetItems(dbObject.ObjectID);
      if (items1.Count == 0)
      {
        ISelectedItems items2 = Services.GetItems(-dbObject.ObjectID);
        if (items2.Count > 0)
          items1 = items2;
      }
      Services.InvokeCommand("OpenInNewWindow", Services.GetCommandsTable(items1, viewServices, false), viewServices);
    }
  }
}
