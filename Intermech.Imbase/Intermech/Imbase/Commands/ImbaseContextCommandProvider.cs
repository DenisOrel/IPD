// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Commands.ImbaseContextCommandProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.DataFormats;
using Intermech.Imbase.BackgroundTask;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Commands;

public class ImbaseContextCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    int objectTypeId = MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRecordTypeGUID);
    IDBTypedObjectID itemData1 = items.Count == 1 ? items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    if (items.Count == 1 && items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData2 && itemData2.Value == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      groupCommands.Add("ViewInTreeIMBASE", new CommandInfo(0, new ClickEventHandler(this.OnViewInTree_Click)));
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData3 && (itemData3.Value == objectTypeId || MetaDataHelper.GetObjectTypeParentID(itemData3.Value) != Intermech.Imbase.Consts.ImbaseRootObjectTypeID) && this.CanUseAttributeForObjectType(itemData3.Value, Intermech.Imbase.Consts.ImbaseObjectRefAttID))
      {
        groupCommands.Add("SynchObjects", new CommandInfo(0, new ClickEventHandler(this.SynchObjects)));
        groupCommands.Add("InverseSynchObjects", new CommandInfo(0, new ClickEventHandler(this.InverseSynchObjects)));
        if (itemData1 != null)
        {
          groupCommands.Add("GoToIMBASE", new CommandInfo(0, new ClickEventHandler(this.GoToIMBASE)));
          break;
        }
        break;
      }
    }
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID && items.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData4 && (itemData4.Value == objectTypeId || MetaDataHelper.GetObjectTypeParentID(itemData4.Value) != Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      {
        groupCommands.Add("RegistryInImbase", new CommandInfo(0, new ClickEventHandler(this.RegistryInImbase)));
        break;
      }
    }
    if (itemData1 != null)
    {
      int objectType = itemData1.ObjectType;
      if (objectType != -1)
      {
        if (this.CanUseAttributeForObjectType(objectType, Intermech.Imbase.Consts.ImbaseObjectRefAttID) && this.CanUseAttributeForObjectType(objectType, MetaDataHelper.GetAttributeID((object) new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"))))
          groupCommands.Add("LinkToImbase", new CommandInfo(0, new ClickEventHandler(this.LinkToIMBASE)));
        if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) == 0L && MetaDataHelper.GetObjectTypeApplicabilities(objectType).Count > 0)
          groupCommands.Add("AddFromImbase", new CommandInfo(0, new ClickEventHandler(ImbaseContextCommandProvider.OnAddFromImbase)));
      }
    }
    if (ImbaseHelper.IsAdmin)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData5 && (itemData5.Value == Intermech.Imbase.Consts.ImbaseTableRefTypeID || itemData5.Value == Intermech.Imbase.Consts.ImbaseTableTypeID))
        {
          groupCommands.Add("UpdateObjectsFromImbase", new CommandInfo(0, new ClickEventHandler(this.UpdateObjectsFromImbase)));
          break;
        }
      }
    }
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData6 && (itemData6.Value == Intermech.Imbase.Consts.ImbaseBLOBTypeID || itemData6.Value == Intermech.Imbase.Consts.ImbaseItemTypeID || itemData6.Value == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID || itemData6.Value == Intermech.Imbase.Consts.ImbaseTableRecordTypeID || itemData6.Value == Intermech.Imbase.Consts.ImbaseFolderTypeID || itemData6.Value == Intermech.Imbase.Consts.ImbaseFavoritesTypeID || itemData6.Value == Intermech.Imbase.Consts.ImbaseTableRefTypeID))
      {
        groupCommands.Suppress("Create", 3);
        groupCommands.Suppress("Add", 3);
      }
    }
    return groupCommands;
  }

  private void OnViewInTree_Click(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    ImbaseContextCommandProvider.OpenInImbaseTree(itemData.Value);
  }

  internal static void OpenInImbaseTree(long tableObjectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      NodeIDPath pathToImbaseObject = ImbaseClientHelper.CreatePathToImbaseObject(sessionKeeper.Session, tableObjectId);
      Utils.OpenNewWindow(pathToImbaseObject.RootDescriptor, (System.IServiceProvider) null, new GetSupportedColumnsEventHandler(Utils.DefaultSupportedColumnsObjects), pathToImbaseObject);
    }
  }

  private void GoToIMBASE(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long imbaseObjID = 0;
    long recID = -1;
    try
    {
      if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
        throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_SelectedObject_EmptyData"));
      NodeIDPath path = (NodeIDPath) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!ImbaseHelper.GetImbaseDataFromObject(sessionKeeper.Session, itemData.ObjectID, ref imbaseObjID, ref recID))
          throw new ImbaseContextCommandProvider.GoToImbaseException(LocalizationHolder.rm.GetString("Imbase_AttrImbaseObjectRef_Empty"));
        path = ImbaseClientHelper.CreatePathToImbaseObject(sessionKeeper.Session, imbaseObjID);
      }
      SelectedRecords.Add(imbaseObjID, new long[1]{ recID });
      SelectedRecords.Add(-imbaseObjID, new long[1]{ recID });
      Utils.OpenNewWindow(path.RootDescriptor, (System.IServiceProvider) null, new GetSupportedColumnsEventHandler(Utils.DefaultSupportedColumnsObjects), path);
    }
    catch (ApplicationException ex)
    {
      ExceptionHelper.ExceptionService.ShowException((Exception) ex);
    }
  }

  private static void OnAddFromImbase(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    long selObjID = (ServicesManager.GetService(typeof (IImbaseSelector)) as IImbaseSelector).SelectFromCatalog(LocalizationHolder.rm.GetString("Imbase.Client_5"), string.Empty, (object) new ImbaseRootNodeDescriptor(), false, true, (int[]) null, -1);
    if (selObjID == -1L)
      return;
    ImbaseContextCommandProvider.DoInsertIntoObject(items.GetParentPath(0), itemData, selObjID);
  }

  public static bool DoInsertIntoObject(
    NodeIDPath parentPath,
    IDBTypedObjectID parentObject,
    long selObjID)
  {
    if (selObjID == -1L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation dbRelation = (IDBRelation) null;
      try
      {
        IUserSession session = sessionKeeper.Session;
        Hashtable possibleChildren = session.GetObjectType(parentObject.ObjectType).GetPossibleChildren();
        IDBObject dbObject = session.GetObject(selObjID);
        if (dbObject == null)
          return false;
        int objectType = dbObject.ObjectType;
        object obj = possibleChildren[(object) objectType];
        if (obj == null)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_7671.ssp_imbase_7680()), (object) session.GetObjectType(objectType).ObjectTypeName, (object) session.GetObjectType(parentObject.ObjectType).ObjectTypeName));
        IDBRelationCollection relationCollection;
        if (obj is int)
        {
          relationCollection = session.GetRelationCollection(Convert.ToInt32(obj));
          possibleChildren[(object) objectType] = (object) relationCollection;
        }
        else
          relationCollection = possibleChildren[(object) objectType] as IDBRelationCollection;
        dbRelation = relationCollection.Create(parentObject.ObjectID, selObjID, DateTime.Now);
      }
      finally
      {
        if (dbRelation != null)
        {
          INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
          if (UISettings.DragDropNotofications)
            service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsManagedEventArgs("ManagedRelationsCreated", dbRelation.RelationID, true));
          else
            service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID, dbRelation.ProjID, dbRelation.RelationType));
          ((IClipboard) ServicesManager.GetService(typeof (IClipboard)))?.RemoveCurrentDataObject();
        }
      }
    }
    return true;
  }

  private void LinkToIMBASE(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      throw new Exception(LocalizationHolder.rm.GetString("Imbase_SelectedObject_EmptyData"));
    if (!(ServicesManager.GetService(typeof (IImbaseSelector)) is IImbaseSelector service1))
      throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_4"));
    Tuple<long, long> tuple = service1.SelectRecord(LocalizationHolder.rm.GetString("Imbase_LinkToImbase"), "", itemData.ObjectID);
    if (tuple == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        throw new Exception(LocalizationHolder.rm.GetString("Imbase.Server.ImbaseindexingService.NullImbaseServer"));
      customService.FillObjectAttributes(sessionKeeper.Session.SessionGUID, itemData.ObjectID, tuple.Item1, tuple.Item2, true);
    }
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2))
      return;
    service2.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", itemData.ObjectID));
  }

  private void RegistryInImbase(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    try
    {
      Dictionary<int, List<long>> dictionary = this.GroupObjectsByType(items);
      DataTable dt = dictionary.Count != 0 ? this.CheckLinkedObjects(dictionary) : throw new Exception(LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_NullSourceObjects"));
      bool flag = true;
      if (dt != null)
      {
        using (RegistryInImbaseLinkedObjectsDlg linkedObjectsDlg = new RegistryInImbaseLinkedObjectsDlg(dt))
        {
          if (linkedObjectsDlg.ShowDialog() == DialogResult.OK)
          {
            List<long> checkedIDs = linkedObjectsDlg.CheckedIDs;
            if (checkedIDs != null)
            {
              Dictionary<int, List<long>> dict = dictionary.ToDictionary<KeyValuePair<int, List<long>>, int, List<long>>((System.Func<KeyValuePair<int, List<long>>, int>) (x => x.Key), (System.Func<KeyValuePair<int, List<long>>, List<long>>) (y => y.Value.Except<long>((IEnumerable<long>) checkedIDs).ToList<long>()));
              dict.Where<KeyValuePair<int, List<long>>>((System.Func<KeyValuePair<int, List<long>>, bool>) (x => x.Value.Count == 0)).Select<KeyValuePair<int, List<long>>, int>((System.Func<KeyValuePair<int, List<long>>, int>) (x => x.Key)).ToList<int>().ForEach((Action<int>) (x => dict.Remove(x)));
              dictionary = dict.Count > 0 ? dict : (Dictionary<int, List<long>>) null;
            }
          }
          else
            flag = false;
        }
      }
      if (!flag)
        return;
      List<StringBuilder> messages = new List<StringBuilder>(3);
      if (dictionary != null)
      {
        IDescriptor rootDescriptor = (IDescriptor) new ImbaseRootNodeDescriptor();
        Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new RegistryInImbaseAnalyzer(new List<int>()
        {
          Intermech.Imbase.Consts.ImbaseFolderTypeID,
          Intermech.Imbase.Consts.ImbaseTableRefTypeID
        }), true);
        RegistryInImbaseSrv serviceInstance = new RegistryInImbaseSrv();
        AdvancedServiceContainer nodesContext = new AdvancedServiceContainer();
        nodesContext.AddService(typeof (IRegistryInImbase), (object) serviceInstance);
        long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase_SelectionWindow_Description"), "", rootDescriptor, (System.IServiceProvider) nodesContext, SelectionOptions.SelectObjects);
        if (numArray != null && numArray.Length != 0 && numArray[0] != 0L)
        {
          int num = -1;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(numArray[0]);
            num = !objectInfo.Empty ? objectInfo.ObjectTypeID : throw new Exception(LocalizationHolder.rm.GetString("Imbase_UnknownSelectedObject"));
          }
          long objID = 0;
          Dictionary<string, List<long>> notifications = new Dictionary<string, List<long>>(4);
          if (num == Intermech.Imbase.Consts.ImbaseFolderTypeID)
            objID = this.CreateCatalogRecordOrFolder(dictionary.SelectMany<KeyValuePair<int, List<long>>, long>((System.Func<KeyValuePair<int, List<long>>, IEnumerable<long>>) (x => (IEnumerable<long>) x.Value)).ToList<long>(), numArray[0], serviceInstance.DestionationObjTypeID, serviceInstance.DelSourceObj, notifications, messages);
          else if (num == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
            objID = this.CreateTableRefRow(dictionary, numArray[0], serviceInstance.DelSourceObj, notifications, messages);
          if (notifications.Count > 0 && ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          {
            foreach (KeyValuePair<string, List<long>> keyValuePair in notifications)
              service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs(keyValuePair.Key, (IList<long>) keyValuePair.Value));
          }
          if (objID != 0L)
            Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objID), viewServices);
        }
      }
      else
        messages.Add(new StringBuilder(LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_EmptyList")));
      if (messages.Count <= 0)
        return;
      IOutputView outputView = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
      if (outputView == null)
        return;
      string Name = LocalizationHolder.rm.GetString("Imbase_RegistryInImbase");
      outputView.ClearText(Name);
      messages.ForEach((Action<StringBuilder>) (x => outputView.WriteString(Name, x.ToString())));
      outputView.Activate(Name);
      outputView.ShowView();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void SynchObjects(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    synchSrv = (IImbaseSynchObjectsService) null;
    string empty = string.Empty;
    string text = items == null || items.Count == 0 ? LocalizationHolder.rm.GetString("Imbase_SynchObjects_NullSelectedObjects") : (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IImbaseSynchObjectsService)) is IImbaseSynchObjectsService synchSrv) ? LocalizationHolder.rm.GetString("Imbase_SynchService_Null") : string.Empty);
    if (string.IsNullOrEmpty(text))
    {
      using (SynchObjectsBaseForm synchObjectsBaseForm = new SynchObjectsBaseForm(items, synchSrv, viewServices))
      {
        int num = (int) synchObjectsBaseForm.ShowDialog();
      }
    }
    else
    {
      int num1 = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("Imbase_SynchObjects_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void InverseSynchObjects(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    string empty = string.Empty;
    string text = items == null || items.Count == 0 ? LocalizationHolder.rm.GetString("Imbase_SynchObjects_NullSelectedObjects") : (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IInverseImbaseSynchObjectsService)) is IInverseImbaseSynchObjectsService) ? LocalizationHolder.rm.GetString("Imbase_SynchService_Null") : string.Empty);
    if (string.IsNullOrEmpty(text))
    {
      using (InverseSynchObjectsForm synchObjectsForm = new InverseSynchObjectsForm(items, viewServices))
      {
        int num = (int) synchObjectsForm.ShowDialog();
      }
    }
    else
    {
      int num1 = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("Imbase_SynchObjects_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void UpdateObjectsFromImbase(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service))
      return;
    List<long> inputData = new List<long>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        inputData.Add(itemData.ObjectID);
    }
    if (inputData.Count <= 0)
      return;
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IUpdateObjectsFromImbaseService)) is IUpdateObjectsFromImbaseService customService)
    {
      UpdateObjectsFromImbaseBackgroundTask task = new UpdateObjectsFromImbaseBackgroundTask((IServiceForBackgroundTask) customService);
      service.AddTask((IBackgroundTask) task);
      task.StartTask((object) inputData);
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("Imbase_Message");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_UpdateObjectsFromImbase_Service_Null"), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private bool CanUseAttributeForObjectType(int objTypeID, int attrID)
  {
    bool flag = true;
    if (MetaDataHelper.GetAttribute4ObjectType(objTypeID, attrID) == null)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
      flag = objectType != null && objectType.AnyAttributes;
    }
    return flag;
  }

  private Dictionary<int, List<long>> GroupObjectsByType(ISelectedItems items)
  {
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        if (dictionary.ContainsKey(itemData.ObjectType))
          dictionary[itemData.ObjectType].Add(itemData.ObjectID);
        else
          dictionary.Add(itemData.ObjectType, new List<long>()
          {
            itemData.ObjectID
          });
      }
    }
    return dictionary;
  }

  private long CreateCatalogRecordOrFolder(
    List<long> sourceObjIDs,
    long selectedFolderID,
    int createdTypeID,
    bool needDeleteSource,
    Dictionary<string, List<long>> notifications,
    List<StringBuilder> messages)
  {
    Dictionary<long, long> source = new Dictionary<long, long>(sourceObjIDs.Count);
    List<long> longList1 = new List<long>(sourceObjIDs.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(createdTypeID);
      if (objectCollection == null)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(createdTypeID);
        if (objectType != null)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_NamedObjectCollection_Null"), (object) objectType.ObjectTypeName));
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_Null"), (object) createdTypeID));
      }
      int num = MetaDataHelper.GetDefaultRelationTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeID);
      if (num == -1)
        num = MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"));
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(num);
      if (relationCollection == null)
      {
        IMSRelationType relationType = MetaDataHelper.GetRelationType(num);
        if (relationType != null)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_NamedRelationCollection_Null"), (object) relationType.TypeName));
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_RelationCollection_Null"), (object) num));
      }
      StringBuilder stringBuilder1 = new StringBuilder();
      foreach (long sourceObjId in sourceObjIDs)
      {
        IDBObject dbObject = (IDBObject) null;
        try
        {
          dbObject = objectCollection.Create(sourceObjId);
          IDBRelation dbRelation = relationCollection.Create(selectedFolderID, dbObject.ObjectID);
          dbObject.CommitCreation(true);
          source.Add(sourceObjId, dbObject.ObjectID);
          longList1.Add(dbRelation.RelationID);
        }
        catch (Exception ex)
        {
          sessionKeeper.Session.GetObjectActualCopy(dbObject.ObjectID, false)?.Delete(0L);
          stringBuilder1.AppendLine($"\t\"{sessionKeeper.Session.GetObjectInfo(sourceObjId).Caption}\" (ID = {Convert.ToString(sourceObjId)})");
          stringBuilder1.AppendLine($"\t{ex.Message}");
        }
      }
      if (source.Count > 0)
      {
        notifications.Add("ObjectsCreated", source.Values.ToList<long>());
        notifications.Add("RelationsCreated", longList1);
      }
      if (stringBuilder1.Length > 0)
      {
        string str = $"{LocalizationHolder.rm.GetString("Imbase_CreateObjectsByPrototype_Fail")}{Environment.NewLine}";
        stringBuilder1.Insert(0, $"{str} {LocalizationHolder.rm.GetString("Imbase_SourceObjects")}{Environment.NewLine}");
        messages.Add(stringBuilder1);
      }
      if (needDeleteSource)
      {
        StringBuilder dontDeletedObjects = (StringBuilder) null;
        List<long> longList2 = this.DeleteObjects(sessionKeeper.Session, source.Keys.ToList<long>(), ref dontDeletedObjects);
        if (longList2.Count > 0)
          notifications.Add("ObjectsRemoved", longList2);
        if (dontDeletedObjects != null)
          messages.Add(dontDeletedObjects);
      }
      else
      {
        List<long> longList3 = new List<long>(sourceObjIDs.Count);
        StringBuilder stringBuilder2 = new StringBuilder();
        foreach (KeyValuePair<long, long> keyValuePair in source)
        {
          try
          {
            this.AddAttributes(sessionKeeper.Session, keyValuePair.Key, keyValuePair.Value);
            longList3.Add(keyValuePair.Key);
          }
          catch (Exception ex)
          {
            stringBuilder2.AppendLine($"\t\"{sessionKeeper.Session.GetObjectInfo(keyValuePair.Key).Caption}\" (ID = {Convert.ToString(keyValuePair.Key)})");
            stringBuilder2.AppendLine($"\t{ex.Message}");
          }
        }
        if (longList3.Count > 0)
          notifications.Add("ObjectsChanged", longList3);
        if (stringBuilder2.Length > 0)
        {
          stringBuilder2.Insert(0, $"{LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_AddAttributes_Error")}{Environment.NewLine}");
          messages.Add(stringBuilder2);
        }
      }
    }
    return source.Count != 1 ? 0L : source.ElementAt<KeyValuePair<long, long>>(0).Value;
  }

  private long CreateTableRefRow(
    Dictionary<int, List<long>> sourceObjIDs,
    long selectedLinkID,
    bool needDeleteSource,
    Dictionary<string, List<long>> notifications,
    List<StringBuilder> messages)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool needCheckIn = false;
      long tableReference = TableLoadHelper.GetTableReference(sessionKeeper.Session, selectedLinkID);
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(tableReference, false);
      if (objectActualCopy == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Table_Null"), (object) tableReference));
      DataSet tableData = this.GetTableData(sessionKeeper.Session, ref objectActualCopy, selectedLinkID, out needCheckIn);
      DataTable table1 = tableData.Tables["IMS_ATTR_TYPES"];
      DataTable table2 = tableData.Tables["IMS_DATA"];
      List<int> list = table1.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x["F_REQUIRED"]) == 2 && Convert.ToInt32(x["F_COMPUTED"]) == 0)).Select<DataRow, int>((System.Func<DataRow, int>) (x => MetaDataHelper.GetAttributeTypeID(Convert.ToString(x["F_ATTRIBUTE_GUID"])))).ToList<int>();
      if (list.Count == 0)
        throw new Exception(LocalizationHolder.rm.GetString("Imbase_Table_EmptyOrComputedColumnsOnly"));
      Dictionary<long, long> dictionary = new Dictionary<long, long>();
      StringBuilder messages1 = new StringBuilder();
      string empty = string.Empty;
      foreach (KeyValuePair<int, List<long>> sourceObjId in sourceObjIDs)
      {
        List<int> attributeForObjectType;
        try
        {
          attributeForObjectType = this.GetAttributeForObjectType(sourceObjId.Key, list);
        }
        catch (Exception ex)
        {
          messages1.AppendLine(ex.Message);
          string str = string.Format(LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_CantRegistryObjects"), (object) MetaDataHelper.GetObjectTypeName(sourceObjId.Key), (object) Convert.ToString(sourceObjId.Key));
          messages1.AppendLine($"\t{str}");
          using (List<long>.Enumerator enumerator = sourceObjId.Value.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              long current = enumerator.Current;
              messages1.AppendLine($"\t\"{sessionKeeper.Session.GetObjectInfo(current).Caption}\" (ID = {Convert.ToString(current)})");
            }
            continue;
          }
        }
        foreach (long num1 in sourceObjId.Value)
        {
          long num2 = this.RegisterObject(sessionKeeper.Session, num1, attributeForObjectType, table1, table2, ref messages1);
          if (num2 != -1L)
            dictionary.Add(num1, num2);
        }
      }
      List<long> longList1 = new List<long>(sourceObjIDs.Count + 1);
      if (dictionary.Count > 0)
      {
        table2.AcceptChanges();
        if (sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService && objectActualCopy.ObjectModifyMode == ObjectModifyModes.InBase)
          customService.CheckUniqueBeforeRegistryInImbase(sessionKeeper.Session.SessionGUID, tableReference, table1, table2, dictionary.Values.ToList<long>());
        TableLoadHelper.StoreData(sessionKeeper.Session, tableReference, table2.DataSet, sessionKeeper.Session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
        longList1.Add(tableReference);
        if (customService != null)
        {
          if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.InBase)
          {
            try
            {
              customService.UpdateAfterRegisteredInImbase(sessionKeeper.Session.SessionGUID, tableReference, table1, table2, dictionary.Values.ToList<long>());
            }
            catch (Exception ex)
            {
              messages1.AppendLine(LocalizationHolder.rm.GetString("Imbase_Indexing_Update"));
              messages1.AppendLine($"\t{ex.Message}");
            }
          }
        }
      }
      if (needDeleteSource)
      {
        StringBuilder dontDeletedObjects = (StringBuilder) null;
        List<long> longList2 = this.DeleteObjects(sessionKeeper.Session, dictionary.Keys.ToList<long>(), ref dontDeletedObjects);
        if (longList2.Count > 0)
          notifications.Add("ObjectsRemoved", longList2);
        if (dontDeletedObjects != null)
          messages.Add(dontDeletedObjects);
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (KeyValuePair<long, long> keyValuePair in dictionary)
        {
          try
          {
            this.AddAttributes(sessionKeeper.Session, keyValuePair.Key, selectedLinkID, keyValuePair.Value);
            longList1.Add(keyValuePair.Key);
          }
          catch (Exception ex)
          {
            stringBuilder.AppendLine($"\t\"{sessionKeeper.Session.GetObjectInfo(keyValuePair.Key).Caption}\" (ID = {Convert.ToString(keyValuePair.Key)})");
            stringBuilder.AppendLine($"\t{ex.Message}");
          }
        }
        if (stringBuilder.Length > 0)
        {
          stringBuilder.Insert(0, $"{LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_AddAttributes_Error")}{Environment.NewLine}");
          messages.Add(stringBuilder);
        }
      }
      if (needCheckIn)
      {
        try
        {
          objectActualCopy.CheckIn();
        }
        catch (Exception ex)
        {
          messages1.AppendLine($"{LocalizationHolder.rm.GetString("Imbase_Table_CheckIn_Fail")} \"{objectActualCopy.Caption}\" (ID = {Convert.ToString(objectActualCopy.ObjectID)}).");
          messages1.AppendLine(ex.Message);
        }
      }
      if (messages1.Length > 0)
        messages.Add(messages1);
      if (longList1.Count > 0)
        notifications.Add("ObjectsChanged", longList1);
      return selectedLinkID;
    }
  }

  internal static bool IsContentAttr(int objTypeID, int attrID)
  {
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objTypeID, attrID);
    bool flag;
    if (attribute4ObjectType == null)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
      flag = (attributeType.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase || attributeType.IsContent;
    }
    else
      flag = ImbaseContextCommandProvider.IsContentAttr(attribute4ObjectType);
    return flag;
  }

  internal static bool IsContentAttr(IMSAttribute4ObjectType imsAttr)
  {
    return (imsAttr.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase || imsAttr.IsContent;
  }

  private bool CheckInForImbaseLink(IDBObject sourceObj)
  {
    int objectType = sourceObj.ObjectType;
    return ImbaseContextCommandProvider.IsContentAttr(objectType, Intermech.Imbase.Consts.ImbaseObjectRefAttID) || ImbaseContextCommandProvider.IsContentAttr(objectType, Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
  }

  private void AddAttributes(
    IUserSession session,
    long sourceObjID,
    long tableRef,
    long recordRef = -1)
  {
    IDBObject sourceObj = session.GetObjectActualCopy(sourceObjID, false);
    bool flag = this.CheckInForImbaseLink(sourceObj);
    long objectId = sourceObj.ObjectID;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    try
    {
      if (flag)
        sourceObj = sourceObj.CheckOut(false);
      attributeValuesList.Add(new AttributeValues(Intermech.Imbase.Consts.ImbaseObjectRefAttID, (object) tableRef));
      if (recordRef != -1L)
        attributeValuesList.Add(new AttributeValues(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, (object) recordRef));
      sourceObj.SetAttributesValues(attributeValuesList.ToArray());
    }
    catch
    {
      if (flag)
      {
        sourceObj.CancelChanges();
        flag = false;
      }
      throw;
    }
    finally
    {
      if (flag)
        sourceObj.CheckIn();
    }
  }

  private List<long> DeleteObjects(
    IUserSession session,
    List<long> objIDs,
    ref StringBuilder dontDeletedObjects)
  {
    List<long> longList = new List<long>(objIDs.Count);
    dontDeletedObjects = new StringBuilder();
    foreach (long objId in objIDs)
    {
      IDBObject dbObject = session.GetObject(objId, false);
      if (dbObject != null)
      {
        try
        {
          dbObject.Delete(0L);
          longList.Add(objId);
        }
        catch (Exception ex)
        {
          dontDeletedObjects.AppendLine($"\t\"{dbObject.Caption.Trim()}\" (ID = {objId})");
          dontDeletedObjects.AppendLine($"\t{ex.Message}");
        }
      }
      else
        dontDeletedObjects.AppendLine($"\t{LocalizationHolder.rm.GetString("Imbase_NullObject")} (ID = {objId}).");
    }
    if (dontDeletedObjects.Length > 0)
      dontDeletedObjects.Insert(0, $"{LocalizationHolder.rm.GetString("Imbase_DeleteObjects_Fail")}:{Environment.NewLine}");
    else
      dontDeletedObjects = (StringBuilder) null;
    return longList;
  }

  private List<int> GetAttributeForObjectType(int objTypeID, List<int> tableAttrIDs)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
    if (objectType == null)
      throw new Exception($"{LocalizationHolder.rm.GetString("Imbase_UnknownObjectsType")} \"{MetaDataHelper.GetObjectTypeName(objTypeID)}\" (ID = {Convert.ToString(objTypeID)})");
    List<int> attributeForObjectType;
    if (objectType.AnyAttributes)
    {
      attributeForObjectType = tableAttrIDs;
    }
    else
    {
      List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(objTypeID);
      attributeForObjectType = tableAttrIDs.Intersect<int>(attribute4ObjectTypeList.Select<IMSAttribute4ObjectType, int>((System.Func<IMSAttribute4ObjectType, int>) (x => x.AttributeID))).ToList<int>();
      if (attributeForObjectType.Count == 0)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_HantObjectsAndTableIdenticalAttributes"), (object) MetaDataHelper.GetObjectTypeName(objTypeID), (object) Convert.ToString(objTypeID)));
    }
    return attributeForObjectType;
  }

  private Guid GetObjectGuid(IUserSession session, object value)
  {
    Guid objectGuid = Guid.Empty;
    string str = Convert.ToString(value);
    if (GuidHelper.IsGuid(str))
    {
      objectGuid = new Guid(str);
    }
    else
    {
      long result = 0;
      if (long.TryParse(str, out result))
      {
        QuickObjectInfo objectInfo = session.GetObjectInfo(result);
        objectGuid = !objectInfo.Empty ? objectInfo.VersionGuid : Guid.Empty;
      }
    }
    return objectGuid;
  }

  private DataSet GetTableData(
    IUserSession session,
    ref IDBObject tableObj,
    long linkID,
    out bool needCheckIn)
  {
    long objectId = tableObj.ObjectID;
    needCheckIn = false;
    try
    {
      if (tableObj.CheckoutBy == 0L)
      {
        if (tableObj.ObjectModifyMode == ObjectModifyModes.Checkout)
        {
          tableObj = tableObj.CheckOut(false);
          needCheckIn = objectId != tableObj.ObjectID;
          objectId = tableObj.ObjectID;
        }
      }
    }
    catch
    {
      throw;
    }
    DataSet tables = TableLoadHelper.GetTables(session, objectId, true);
    return tables != null && tables.Tables.Contains("IMS_DATA") && tables.Tables.Contains("IMS_ATTR_TYPES") ? tables : throw new Exception(LocalizationHolder.rm.GetString("Imbase_Table_ErrorDataSet"));
  }

  private long RegisterObject(
    IUserSession session,
    long objID,
    List<int> attrIDs,
    DataTable dtAttrs,
    DataTable dtData,
    ref StringBuilder messages)
  {
    long num = -1;
    try
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(objID, false);
      AttributeValues[] source = objectActualCopy != null ? objectActualCopy.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeCaption) : throw new Exception($"{LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_ObjectForRegistry_Null")} (ID = {Convert.ToString(objID)})");
      string str1 = string.Format(LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_Fail"), (object) objectActualCopy.Caption, (object) Convert.ToString(objID));
      if (source == null)
        throw new Exception($"{str1} {LocalizationHolder.rm.GetString("Imbase_Object_NullAttributes")}");
      AttributeValues[] array = ((IEnumerable<AttributeValues>) source).Where<AttributeValues>((System.Func<AttributeValues, bool>) (x => attrIDs.Contains(x.AttributeID))).ToArray<AttributeValues>();
      if (array.Length == 0)
        throw new Exception($"{str1} {LocalizationHolder.rm.GetString("Imbase_ObjectAndTableHantIdenticalAttributes")}");
      DataRow row = dtData.NewRow();
      row["F_GUID"] = (object) Guid.NewGuid();
      string columnName = string.Empty;
      foreach (AttributeValues attributeValues in array)
      {
        if (attributeValues.Values != null && attributeValues.Values.Length != 0 && attributeValues.Values[0] != null && attributeValues.Values[0] != DBNull.Value)
        {
          columnName = Convert.ToString((object) attributeValues.AttributeGuid);
          DataColumn column = dtData.Columns[columnName];
          ValuesArray valuesArray = (ValuesArray) null;
          if (column.ExtendedProperties.ContainsKey((object) "dataType"))
            valuesArray = new ValuesArray((Array) attributeValues.Values, column.ExtendedProperties[(object) "dataType"] as System.Type);
          if (attributeValues.AttributeType == FieldTypes.ftObjectLink)
          {
            Guid objectGuid = this.GetObjectGuid(session, attributeValues.Values[0]);
            if (!(objectGuid == Guid.Empty))
              row[columnName] = (object) objectGuid;
            else
              continue;
          }
          else if (attributeValues.AttributeType == FieldTypes.ftMeasured)
          {
            if (attributeValues.Values[0] is MeasuredValue mValue)
            {
              row[columnName] = (object) mValue.Value;
              string str2 = Convert.ToString(dtAttrs.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == columnName))["F_UNITS"]);
              if (GuidHelper.IsGuid(str2))
              {
                Guid objectGUID = new Guid(str2);
                QuickObjectInfo objectInfo = session.GetObjectInfo(objectGUID);
                if (!objectInfo.Empty)
                {
                  if (objectInfo.VersionGuid != Guid.Empty)
                  {
                    try
                    {
                      MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, objectInfo.ObjectID);
                      row[columnName] = (object) measuredValue.Value;
                    }
                    catch (Exception ex)
                    {
                      messages.AppendLine(LocalizationHolder.rm.GetString("Imbase_AttributeValue_ConvertToMeasureValue_Fail"));
                      messages.AppendLine($"\t{LocalizationHolder.rm.GetString("Imbase_Object")} \"{objectActualCopy.Caption}\" (ID = {Convert.ToString(objID)})");
                      messages.AppendLine($"\t{ex.Message}");
                      row[columnName] = (object) DBNull.Value;
                      continue;
                    }
                  }
                }
              }
            }
            else
              continue;
          }
          else
            row[columnName] = (object) valuesArray ?? attributeValues.Values[0];
          if (num <= -1L)
            num = Convert.ToInt64(row["F_KEY"]);
        }
      }
      if (num == -1L)
        throw new Exception($"{str1} {LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_Attributes_Empty")}");
      dtData.Rows.Add(row);
    }
    catch (Exception ex)
    {
      messages.AppendLine(ex.Message);
    }
    return num;
  }

  private DataTable CheckLinkedObjects(Dictionary<int, List<long>> objsGroupByType)
  {
    DataTable dataTable = (DataTable) null;
    ColumnDescriptor[] columns = new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseObjectRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams dbRSP = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseObjectRefAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
    }, columns);
    List<ObjInfoItem> objIDList = new List<ObjInfoItem>();
    foreach (KeyValuePair<int, List<long>> keyValuePair in objsGroupByType)
    {
      KeyValuePair<int, List<long>> pair = keyValuePair;
      objIDList.AddRange(pair.Value.Select<long, ObjInfoItem>((System.Func<long, ObjInfoItem>) (x => new ObjInfoItem(x, pair.Key))));
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dataTable = ImbaseHelper.SelectObjectsEx((IEnumerable<int>) new List<int>()
      {
        -1
      }, sessionKeeper.Session, dbRSP, (IEnumerable<ObjInfoItem>) objIDList);
    return dataTable == null || dataTable.Rows.Count <= 0 ? (DataTable) null : dataTable;
  }

  private class GoToImbaseException(string message) : ApplicationException(message)
  {
  }
}
