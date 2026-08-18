// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.CommandsProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.EventLog;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search.EventLog;
using System;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

internal class CommandsProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    long viewState = viewServices.GetService(typeof (IViewState)) is IViewState service1 ? (long) service1.ViewState : 0L;
    CommandsInfo mergedCommands = new CommandsInfo();
    if ((viewState & 2L) != 0L)
      return CommandsInfo.Empty;
    if (items.GetItemID(0).CategoryID == 10 && !EventsViewConsts.IsFile)
    {
      mergedCommands.Add("DeleteEventLogRecord", new CommandInfo(1, new ClickEventHandler(CommandsProvider.DeleteEventLogRecord)));
      mergedCommands.Add("ClearEventLog", new CommandInfo(1, new ClickEventHandler(CommandsProvider.ClearEventLog)));
    }
    if (items.Count == 1 && items.GetItemData(0, typeof (ICanOpenInNewWindow)) is ICanOpenInNewWindow)
      mergedCommands.Add("OpenInNewWindow", new CommandInfo(1, new ClickEventHandler(CommandsProvider.OpenInNewWindow)));
    if (items.Count != 0 && !EventsViewConsts.IsFile)
    {
      ISimpleExcelReports service2 = ServicesManager.GetService(typeof (ISimpleExcelReports)) as ISimpleExcelReports;
      ChildrenView service3 = viewServices.GetService(typeof (ChildrenView)) as ChildrenView;
      if (service2 != null && service3 != null)
        mergedCommands.Add("EventLogExcelReport", new CommandInfo(1, new ClickEventHandler(CommandsProvider.EventLogExcelReport)));
      mergedCommands.Add("Copy", new CommandInfo(0, new ClickEventHandler(ObjectCommands.AddToWindowsClipboard)));
    }
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items.Count != 1 || ((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    if (items.GetItemID(0).CategoryID == DatabaseConfiguratorConsts.EventLogCategoryID && !EventsViewConsts.IsFile)
    {
      groupCommands.Add("CreateFilter", new CommandInfo(1, new ClickEventHandler(CommandsProvider.CreateFilter)));
      groupCommands.Add("ClearEventLog", new CommandInfo(1, new ClickEventHandler(CommandsProvider.ClearEventLog)));
    }
    if (items.GetItemID(0).CategoryID == DatabaseConfiguratorConsts.EventFilterCategoryID && !EventsViewConsts.IsFile)
      groupCommands.Add("Delete", new CommandInfo(1, new ClickEventHandler(CommandsProvider.DeleteFilter)));
    return groupCommands;
  }

  private static void OpenInNewWindow(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(items.GetItemData(0, typeof (IDescriptor)) is IDescriptor itemData))
      return;
    if (itemData is Intermech.Navigator.DBObjects.Descriptor descriptor)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(descriptor.ObjectID);
        int objectType = dbObject.ObjectType;
        int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545");
        int objectTypeId2 = MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545");
        if (MetaDataHelper.IsObjectTypeChildOf(objectType, objectTypeId1) || MetaDataHelper.IsObjectTypeChildOf(objectType, objectTypeId2))
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Can_not_open"), LocalizationHolder.rm.GetString("Open_in_new window"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
        if (MetaDataHelper.GetLCLevelGuid(MetaDataHelper.GetLCStep(dbObject.LCStep).LevelID).ToString() == "cad00049-306c-11d8-b4e9-00304f19f545")
        {
          if (!sessionKeeper.Session.IsAdmin)
            throw new AccessDeniedException(sessionKeeper.Session);
        }
      }
    }
    Utils.OpenNewWindow(itemData, viewServices);
  }

  private static void CreateFilter(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (viewServices != null && viewServices.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service1)
      service1.CanRestoreFocusedNode = false;
    Filter filter = new Filter(Guid.NewGuid());
    FiltersManager.Filters.Add(filter);
    FiltersManager.Flush();
    Intermech.DatabaseConfigurator.Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new FilterEventArgs("FilterCreated", true, filter.Guid));
    if (!(viewServices.GetService(typeof (IViewsManager)) is IViewsManager service2) || service2.ViewPages.Count <= 0 || service2.ActiveViewPage != null && service2.ActiveViewPage.View.Caption == FilterConfigView.FilterConfigViewName)
      return;
    IView view = service2.ActiveViewPage.View;
    for (int index = 0; index < service2.ViewPages.Count; ++index)
    {
      if (service2.ViewPages[index].View.Caption == FilterConfigView.FilterConfigViewName)
      {
        service2.ActiveViewPage = service2.ViewPages[index];
        break;
      }
    }
  }

  private static void DeleteFilter(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    Filter filter = FiltersManager.Filters.FindFilter((items.GetItemData(0, typeof (IFilterGuid)) as IFilterGuid).Value);
    if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_90"), (object) filter.Name), LocalizationHolder.rm.GetString("DatabaseConfigurator_91"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    FiltersManager.Filters.Remove(filter);
    FiltersManager.Flush();
    Intermech.DatabaseConfigurator.Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new FilterEventArgs("FilterRemoved", false, filter.Guid));
  }

  private static void DeleteEventLogRecord(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ArrayList eventIDs = new ArrayList();
    for (int index = 0; index < items.Count; ++index)
    {
      IEventID itemData = (IEventID) items.GetItemData(index, typeof (IEventID));
      if (itemData != null)
        eventIDs.Add((object) itemData.Value);
    }
    if (MessageBox.Show(MessageDialogs.msgReallyDelete, MessageDialogs.msgQuery, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (viewServices.GetService(typeof (IEventLogProvider)) is IEventLogProvider service && service.EventLog == EventLogs.Archival ? sessionKeeper.Session.EventLogArchive : sessionKeeper.Session.EventLog)?.DeleteEvents((long[]) eventIDs.ToArray(typeof (long)));
    Intermech.DatabaseConfigurator.Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new RecordEventArgs("RecordRemoved", (IList) eventIDs));
  }

  private static void ClearEventLog(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    bool byDate = true;
    DateTime date = DateTime.Now;
    if (new EventLogDeleteForm().Execute(ref byDate, ref date) != DialogResult.OK || MessageBox.Show(MessageDialogs.msgReallyDelete, MessageDialogs.msgQuery, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IEventLog eventLog = viewServices.GetService(typeof (IEventLogProvider)) is IEventLogProvider service && service.EventLog == EventLogs.Archival ? sessionKeeper.Session.EventLogArchive : sessionKeeper.Session.EventLog;
      if (eventLog == null)
        return;
      if (!byDate)
        date = DateTime.Now + TimeSpan.FromDays(1.0);
      eventLog.ClearEvents(date);
    }
    Intermech.DatabaseConfigurator.Holder.NotificationService.FireEvent((object) null, new NotificationEventArgs("RefreshEventLog"));
  }

  private static void EventLogExcelReport(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ChildrenView service = viewServices != null ? viewServices.GetService(typeof (ChildrenView)) as ChildrenView : (ChildrenView) null;
    if (service == null)
      return;
    EventsView.EventLogExcelReport("IPS Event Log", service.Grid);
  }
}
