
// Type: Intermech.Navigator.Snapshots.SnapshotsCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls.Snapshots;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Snapshots;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Snapshots;

/// <summary>провайдер команд для итераций</summary>
public class SnapshotsCommandsProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    if (items.Count == 1)
    {
      groupCommands.Add("RestoreSnapshot", new CommandInfo(0, new ClickEventHandler(this.RestoreObjectShapshot)));
      groupCommands.Add("RenameSnapshot", new CommandInfo(0, new ClickEventHandler(SnapshotsCommandsProvider.RenameSnapshot)));
      groupCommands.Add("ViewSnapshot", new CommandInfo(0, new ClickEventHandler(SnapshotsCommandsProvider.ViewSnapshot)));
      groupCommands.Add("CompareSnapshot", new CommandInfo(0, new ClickEventHandler(SnapshotsCommandsProvider.CompareSnapshot)));
    }
    groupCommands.Add("DeleteSnapshot", new CommandInfo(0, new ClickEventHandler(this.DeleteObjectShapshot)));
    return groupCommands;
  }

  /// <summary>Переименовать итерацию</summary>
  /// <param name="items">The items.</param>
  /// <param name="viewservices">The viewservices.</param>
  /// <param name="additionalinfo">The additionalinfo.</param>
  private static void RenameSnapshot(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null || items.Count != 1 || !(items.GetItemData(0, typeof (SnapshotsNodeID)) is SnapshotsNodeID itemData))
      return;
    using (SnapshotRenameForm snapshotRenameForm = new SnapshotRenameForm(itemData.ID, itemData.SnapshotID))
    {
      int num = (int) snapshotRenameForm.ShowDialog();
    }
  }

  /// <summary>Восстановление итерации</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void RestoreObjectShapshot(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count != 1)
      return;
    SnapshotsNodeID itemData = items.GetItemData(0, typeof (SnapshotsNodeID)) as SnapshotsNodeID;
    IDBTypedObjectID parentData = items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (itemData == null || parentData == null)
      return;
    using (SessionKeeper sk = new SessionKeeper())
    {
      if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("RestoreShapshot"), (object) parentData.Caption), LocalizationHolder.rm.GetString("Restoring"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
        return;
      IDBObjectSnapshot snapshot = sk.Session.GetSnapshot(itemData.SnapshotID);
      if (snapshot == null)
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1409"), (object) itemData.SnapshotID), LocalizationHolder.rm.GetString("Restoring"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        List<long> readOnlyObjects = snapshot.GetReadOnlyObjects(parentData.ObjectID);
        if (readOnlyObjects.Count == 0)
        {
          snapshot.SaveToObject(parentData.ObjectID);
        }
        else
        {
          string onlyObjectsCaptions = SnapshotsCommandsProvider.GetReadOnlyObjectsCaptions(readOnlyObjects, sk);
          if (MessageBox.Show($"{LocalizationHolder.rm.GetString("Client.Core_1623")} {onlyObjectsCaptions} {LocalizationHolder.rm.GetString("Client.Core_1624")}", LocalizationHolder.rm.GetString("Restoring"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            return;
          sk.Session.GetSnapshot(itemData.SnapshotID).SaveToObject(parentData.ObjectID, false);
        }
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", parentData.ObjectID));
        if (!(viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service) || service.FocusedNode == null)
          return;
        NavigatorTreeNode focusedNode = service.FocusedNode;
        NodeID nodeId = focusedNode != null ? focusedNode.NodeID as NodeID : (NodeID) null;
        if (nodeId == null || nodeId.ObjectID != parentData.ObjectID)
          return;
        focusedNode.Tree.MakeNodeUnpopulated(focusedNode);
        focusedNode.Expanded = true;
      }
    }
  }

  /// <summary>
  /// Получает строку с наименованиями объектов недоступных для изменения.
  /// </summary>
  /// <param name="readOnlyObjects">Список ИД объектов недоступных для изменения.</param>
  /// <param name="sk">Хранитель сессии</param>
  /// <returns></returns>
  private static string GetReadOnlyObjectsCaptions(List<long> readOnlyObjects, SessionKeeper sk)
  {
    List<string> stringList = new List<string>();
    foreach (long readOnlyObject in readOnlyObjects)
    {
      long objectFId = sk.Session.GetObjectF_ID(readOnlyObject);
      IDBObject objectById = sk.Session.GetObjectByID(objectFId, false);
      if (!stringList.Contains(objectById.Caption))
        stringList.Add(objectById.Caption);
    }
    string str = stringList[0];
    for (int index = 1; index < stringList.Count; ++index)
      str = $"{str}, {stringList[index]}";
    return str + ".";
  }

  /// <summary>Удаление итерации</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void DeleteObjectShapshot(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || MessageBox.Show(items.Count > 1 ? LocalizationHolder.rm.GetString("DeleteShaphots") : LocalizationHolder.rm.GetString("DeleteShaphot"), LocalizationHolder.rm.GetString("Deletion"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int count = items.Count;
      for (int index = 0; index < count; ++index)
      {
        SnapshotsNodeID itemData = items.GetItemData(index, typeof (SnapshotsNodeID)) as SnapshotsNodeID;
        sessionKeeper.Session.GetSnapshot(itemData.SnapshotID).Delete(0L);
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("SnapshotsChanged", itemData.ID));
      }
    }
  }

  /// <summary>Получить идентификатор выделенной итерации</summary>
  private static long GetSelectedSnapshotID(ISelectedItems items)
  {
    return items == null || items.Count != 1 || !(items.GetItemData(0, typeof (SnapshotsNodeID)) is SnapshotsNodeID itemData) ? 0L : itemData.SnapshotID;
  }

  /// <summary>Открыть сохранённый в итерации состав в новом окне навигатора</summary>
  private static void ViewSnapshot(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    long selectedSnapshotId = SnapshotsCommandsProvider.GetSelectedSnapshotID(items);
    if (selectedSnapshotId == 0L)
      return;
    Utils.OpenNewWindow((IDescriptor) SnapshotDescriptor.Create(selectedSnapshotId, SnapshotAttributes.Default | SnapshotAttributes.ObjectsInSnapshot), (System.IServiceProvider) null, new GetSupportedColumnsEventHandler(SnapshotConsts.SnapshotTreeColumns), (NodeIDPath) null);
  }

  /// <summary>Сравнить сохранённый в итерации состав с актуальным в новом окне навигатора</summary>
  private static void CompareSnapshot(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    long selectedSnapshotId = SnapshotsCommandsProvider.GetSelectedSnapshotID(items);
    if (selectedSnapshotId == 0L)
      return;
    Utils.OpenNewWindow((IDescriptor) SnapshotDescriptor.Create(selectedSnapshotId, SnapshotAttributes.Default | SnapshotAttributes.ObjectsInSnapshot, SnapshotDescriptor.Content.CompareWithActual), (System.IServiceProvider) null, new GetSupportedColumnsEventHandler(SnapshotConsts.SnapshotTreeColumns), (NodeIDPath) null);
  }
}
