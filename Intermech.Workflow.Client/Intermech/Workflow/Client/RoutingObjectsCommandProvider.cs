// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.RoutingObjectsCommandProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Remoting.Sponsors;
using Intermech.Workflow.Design;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

internal class RoutingObjectsCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items.Count > 0)
    {
      mergedCommands.Add("LaunchProcess", new CommandInfo(0, new ClickEventHandler(this.LaunchProcessCommand)));
      if (items.GetItemID(0).TypeID == wfConsts.SchemesTypeID)
      {
        mergedCommands.Suppress("CreateLinkedProto", 0);
        mergedCommands.Suppress("CreateVersionAnotherType", 0);
        if (items.Count == 1)
        {
          mergedCommands.Add("CreateVersion", new CommandInfo(0, new ClickEventHandler(this.CreateVersionScheme)));
          mergedCommands.Add("CreateProto", new CommandInfo(0, new ClickEventHandler(this.CreateProto)));
          bool flag = true;
          if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
            flag = false;
          if (flag && itemData != null && (itemData.BaseVersion & 1L) == 0L)
            mergedCommands.Add("MakeBaseVersion", new CommandInfo(0, new ClickEventHandler(this.MakeBaseScheme)));
        }
        else if (items.Count > 1)
          mergedCommands.Suppress("MakeBaseVersion", 0);
      }
    }
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  private void MakeBaseScheme(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObject(itemData.Value) is IScheme scheme)
        {
          IDBAttribute dbAttribute = scheme.IsValid() ? scheme.GetAttributeByID(wfConsts.AttrIsDebugID) : throw new KernelException("Некорректный шаблон нельзя сделать базовым.");
          if (dbAttribute != null)
            dbAttribute.AsBoolean = false;
        }
        ConditionStructure[] conds = new ConditionStructure[1]
        {
          new ConditionStructure(wfConsts.AttrPrototypeID, RelationalOperators.Equal, (object) itemData.Value, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
        };
        DataTable dataTable = MiscFunx.SimpleSelect(sessionKeeper.Session, wfConsts.ProcessesTypeID, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }, conds, recordCount: -1);
        if (dataTable.Rows.Count > 0)
        {
          long[] processesID = new long[dataTable.Rows.Count];
          for (int index = 0; index < dataTable.Rows.Count; ++index)
            processesID[index] = (long) Convert.ToInt32(dataTable.Rows[index][0]);
          int num = (int) new WorkflowErrorFormWithProcessesViewAndDeleting("Внимание", "Удалить процессы, созданные по текущей версии шаблона?", processesID).ShowDialog();
        }
      }
    }
    ObjectCommands.MakeBaseVersion(items, viewServices, additionalInfo);
    if (itemData == null || itemData.Value >= 0L)
      return;
    ObjectCommands.CheckinCommand(items, viewServices, additionalInfo);
  }

  private void CreateVersionScheme(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    INotificationService service = ApplicationServices.Container.GetService(typeof (INotificationService)) as INotificationService;
    long objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (RemoteLock remoteLock = new RemoteLock())
      {
        IDBObject version = sessionKeeper.Session.GetObjectCollection(wfConsts.SchemesTypeID).CreateVersion(objectID);
        remoteLock.Add((object) version);
        version.CommitCreation(true, false);
        if (service != null)
        {
          DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", version.ObjectID);
          service.FireEvent((object) null, (NotificationEventArgs) e);
        }
        wfFunx.EditProcess(version.ObjectID);
      }
    }
  }

  private void CreateProto(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    INotificationService service = ApplicationServices.Container.GetService(typeof (INotificationService)) as INotificationService;
    long prototypeID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    using (SetSchemeName setSchemeName = new SetSchemeName(false))
    {
      if (setSchemeName.ShowDialog() != DialogResult.OK)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        using (RemoteLock remoteLock = new RemoteLock())
        {
          IDBObject objToLock = sessionKeeper.Session.GetObjectCollection(wfConsts.SchemesTypeID).Create(prototypeID);
          remoteLock.Add((object) objToLock);
          objToLock.Caption = setSchemeName.SchemeName;
          objToLock.CommitCreation(true, false);
          if (service != null)
          {
            DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", objToLock.ObjectID);
            service.FireEvent((object) null, (NotificationEventArgs) e);
          }
          wfFunx.EditProcess(objToLock.ObjectID);
        }
      }
    }
  }

  protected void LaunchProcessCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    wfFunx.CreateProcess(0L, (ISimpleSelectedItems) items);
  }
}
