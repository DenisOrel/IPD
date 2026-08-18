// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Security.GetAccessReportProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.DatabaseConfigurator.Security;

internal class GetAccessReportProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (GetAccessReportProvider.CanShowGetAccessReportCommand(items))
      mergedCommands.Add("GetAccessReport", new CommandInfo(0, new ClickEventHandler(GetAccessReportProvider.GetReport)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("GetAccessReportForObjects", new CommandInfo(0, new ClickEventHandler(this.GetAccessReportForObjects)));
    return groupCommands;
  }

  public static void GetReport(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
      IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
      string category = LocalizationHolder.rm.GetString("GetAccessReport");
      service.ClearText(category);
      for (int index = 0; index < items.Count; ++index)
      {
        long userID = (items.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID).Value;
        foreach (string text in customService.GetAccessReport(sessionKeeper.Session.SessionGUID, userID))
          service.WriteString(category, text);
        service.WriteString(category, string.Empty);
      }
      service.Activate(category);
      service.ShowView();
    }
  }

  private static bool CanShowGetAccessReportCommand(ISelectedItems items)
  {
    for (int index = 0; index < items.Count; ++index)
    {
      if (!(items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectType != MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545") && itemData.ObjectType != MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545") && itemData.ObjectType != MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"))
        return false;
    }
    return true;
  }

  private void GetAccessReportForObjects(
    ISelectedItems items,
    IServiceProvider viewservices,
    object additionalinfo)
  {
    List<long> usersId = this.GetUsersId();
    if (!usersId.Any<long>())
      return;
    List<long> longList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      longList.Add(itemData.ObjectID);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string[] accessReport = (sessionKeeper.Session.GetCustomService(typeof (IInternalUserSessions)) as IInternalUserSessions).GetAccessReport(sessionKeeper.Session.SessionGUID, usersId.ToArray(), longList.ToArray());
      IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
      string category = LocalizationHolder.rm.GetString(nameof (GetAccessReportForObjects));
      service.ClearText(category);
      for (int index = 0; index < accessReport.Length; ++index)
        service.WriteString(category, accessReport[index]);
      service.WriteString(category, string.Empty);
      service.Activate(category);
      service.ShowView();
    }
  }

  private List<long> GetUsersId()
  {
    IDBTypedObjectID[] dbTypedObjectIdArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("DatabaseConfigurator_268"), (IDescriptor) new UsersGroupsDescriptor(), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects, new int[1]
    {
      MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")
    }) as IDBTypedObjectID[];
    List<long> collection = new List<long>();
    if (dbTypedObjectIdArray != null && dbTypedObjectIdArray.Length != 0)
    {
      foreach (IDBTypedObjectID dbTypedObjectId in dbTypedObjectIdArray)
        collection.SafeAdd<long>(dbTypedObjectId.ObjectID);
    }
    return collection;
  }
}
