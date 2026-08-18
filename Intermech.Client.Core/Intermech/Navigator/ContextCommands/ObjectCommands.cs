
// Type: Intermech.Navigator.ContextCommands.ObjectCommands
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.CacheServices;
using Intermech.Client.Core;
using Intermech.Client.Core.Commands;
using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Client.Core.Navigator.Controls.Windows;
using Intermech.Commands;
using Intermech.ControlFlow;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.ECO.Client;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Localization;
using Intermech.Interfaces.Projects;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Descriptos;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Snapshots;
using Intermech.PropertyEditors;
using Intermech.Search;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.ContextCommands;

/// <summary>
/// Класс содержит статические обработчики различных команд контекстного меню объектов
/// </summary>
public class ObjectCommands
{
  /// <summary>Коллекция созданных связей</summary>
  public static List<long> insertIncluded = new List<long>();
  /// <summary>Опции для окна "Удаление объектов" по умолчанию</summary>
  public static DeleteAnalyzerOptions DeleteOptions = DeleteAnalyzerOptions.None;

  private static void OnCreateNewObject(object sender, AfterObjectCreatedEventArgs e)
  {
    Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", e.ObjectID, e.ObjectTypeID));
  }

  /// <summary>Обработчик события "Сохранить на диск"</summary>
  /// <param name="items">Выделенные элементы</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительные параметры</param>
  public static void SaveToDisk(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<IDBTypedObjectID> dbTypedObjectIDs1 = new List<IDBTypedObjectID>();
    List<IDBTypedObjectID> dbTypedObjectIDs2 = new List<IDBTypedObjectID>();
    for (int index1 = 0; index1 < items.Count; ++index1)
    {
      if (!(items.GetItemData(index1, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
        return;
      dbTypedObjectIDs1.Add(itemData);
      List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(itemData.ObjectType);
      for (int index2 = 0; index2 < parentsIdReverse.Count; ++index2)
      {
        Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(parentsIdReverse[index2]);
        if (objectTypeGuid.Equals(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")) || objectTypeGuid.Equals(new Guid("cad0014e-306c-11d8-b4e9-00304f19f545")) || objectTypeGuid.Equals(new Guid("cad0014f-306c-11d8-b4e9-00304f19f545")) || objectTypeGuid.Equals(new Guid("cad00150-306c-11d8-b4e9-00304f19f545")))
        {
          dbTypedObjectIDs2.Add(itemData);
          break;
        }
      }
    }
    if (dbTypedObjectIDs2.Count == 0)
    {
      SaveToDiskForm saveToDiskForm = new SaveToDiskForm(items);
      if (saveToDiskForm.ShowDialog() != DialogResult.OK)
        return;
      bool filenamesWhenSave = AuthFilesHolder.GetAddObjectVersionToAuthFilenamesWhenSave();
      SaveToDiskClass task = new SaveToDiskClass(saveToDiskForm.Folder, saveToDiskForm.Format, saveToDiskForm.Relations, saveToDiskForm.IsExact, saveToDiskForm.Suffix, saveToDiskForm.ObjectTypesFiltr, saveToDiskForm.ObjectTypes, saveToDiskForm.CreateHierarchy, saveToDiskForm.LongPathSupport, saveToDiskForm.SaveCompatibleSigns, filenamesWhenSave, saveToDiskForm.SelectedAttributeID, saveToDiskForm.SaveToDiskProcessorList, dbTypedObjectIDs1);
      if (ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service)
        service.AddTask((IBackgroundTask) task);
      using (FixEditingContext fixEditingContext = new FixEditingContext())
        new Thread(fixEditingContext.SendEditingContextToThread(new ThreadStart(task.Saving)))
        {
          IsBackground = true
        }.Start();
    }
    else
    {
      SaveToDiskClassifierForm diskClassifierForm = new SaveToDiskClassifierForm();
      if (diskClassifierForm.ShowDialog() != DialogResult.OK)
        return;
      SaveToDiskClassifierClass task = new SaveToDiskClassifierClass(diskClassifierForm.Folder, diskClassifierForm.BaseVersions, dbTypedObjectIDs2);
      if (ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service)
        service.AddTask((IBackgroundTask) task);
      new Thread(new ThreadStart(task.Saving))
      {
        IsBackground = true
      }.Start();
    }
  }

  /// <summary>Обработчик события "Карточка"</summary>
  /// <param name="items">Выделенные элементы</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительные параметры</param>
  public static void ParametersCardCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count == 0)
      return;
    bool readOnly = false;
    if (viewServices != null)
      readOnly = ((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L;
    if (items.GetItemData(0, typeof (IDescriptor)) is Intermech.Navigator.DBObjects.Descriptor itemData && itemData.InvalidDescriptor)
    {
      Utils.NotifyWrongDbObjectDescriptor(itemData);
    }
    else
    {
      if (items.GetItemData(0, typeof (IDBObjectID)) == null)
        return;
      int num = (int) PropertiesWindow.Execute(items, readOnly: readOnly);
    }
  }

  /// <summary>
  /// Вызывает диалог создания нового объекта (отображает список всех типов и выделяет в дереве тип выделенного объекта)
  /// </summary>
  public static void CreateCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    try
    {
      if (!(items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData))
        return;
      AfterObjectCreatorDialogHandlers.Handle(service.CreateObjectByTypeDialog((int[]) null, itemData.Value), 0, items, viewServices, additionalInfo);
    }
    finally
    {
      service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  /// <summary>Вызывает диалог создания нового объекта</summary>
  public static void CreateCommandType(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData))
      return;
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    try
    {
      AfterObjectCreatorDialogHandlers.Handle(service.CreateObjectByTypeDialog(itemData.Value), 0, items, viewServices, additionalInfo);
    }
    finally
    {
      service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  /// <summary>
  /// Вызывает диалог создания нового объекта (по типу выделенного объекта)
  /// </summary>
  /// <param name="objectTypeID">Тип объекта</param>
  public static void CreateCommand(int objectTypeID)
  {
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    try
    {
      service.CreateObjectByTypeDialog(objectTypeID);
    }
    finally
    {
      service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  /// <summary>Вызывает диалог создания нового объекта по прототипу</summary>
  public static void CreatePrototypeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    try
    {
      long aTemplateObjectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
      AfterObjectCreatorDialogHandlers.Handle(service.CreateObjectByTemplateDialog(aTemplateObjectID), 0, items, viewServices, additionalInfo);
    }
    finally
    {
      service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  /// <summary>
  /// Вызывает диалог создания нового связанного контекста редактирования
  /// </summary>
  public static void CreateLinkedContextCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count != 1)
      return;
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    try
    {
      long objectByTypeDialog = service.CreateObjectByTypeDialog(MetaDataHelper.GetObjectTypeID("cad0146b-306c-11d8-b4e9-00304f19f545"));
      AfterObjectCreatorDialogHandlers.Handle(objectByTypeDialog, 0, items, viewServices, additionalInfo);
      if (objectByTypeDialog == 0L || objectByTypeDialog == -1L)
        return;
      long objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBEditingContextsObject editingContextsObject1 = sessionKeeper.Session.GetObject(objectByTypeDialog) as IDBEditingContextsObject;
        IDBEditingContextsObject editingContextsObject2 = sessionKeeper.Session.GetObject(objectID) as IDBEditingContextsObject;
        if (editingContextsObject1 == null)
          return;
        bool flag = false;
        if (editingContextsObject2 != null)
        {
          editingContextsObject1.LinkedContextNumber = Math.Abs(editingContextsObject2.LinkedContextNumber);
          flag = true;
        }
        if (!flag)
          return;
        DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChanged", objectByTypeDialog);
        Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
      }
    }
    finally
    {
      service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  /// <summary>Вызывает диалог создания нового объекта по прототипу</summary>
  public static void CreateVersionCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    CreateItemsVersionsCommand itemsVersionsCommand = new CreateItemsVersionsCommand();
    itemsVersionsCommand.Init(items, viewServices, additionalInfo);
    itemsVersionsCommand.Execute();
  }

  /// <summary>
  /// Вызывает диалог создания нового объекта и включает его в состав родительского узла текущего объекта nodeData
  /// </summary>
  public static void CreateIncludeInParentCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID))
      return;
    IDBTypedObjectID parentData = items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    ObjectCommands.InternalCreateIncludeCommand(items, viewServices, additionalInfo, parentData);
  }

  /// <summary>
  /// Вызывает диалог создания нового объекта и включает его в состав текущего объекта nodeData
  /// </summary>
  public static void CreateIncludeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ObjectCommands.InternalCreateIncludeCommand(items, viewServices, additionalInfo, (IDBTypedObjectID) null);
  }

  /// <summary>
  /// Вызывает диалог создания нового объекта и включает его в состав текущего объекта nodeData
  /// </summary>
  private static void InternalCreateIncludeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo,
    IDBTypedObjectID parentObject)
  {
    if (items == null || items.Count == 0)
      return;
    if (viewServices != null && viewServices.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service1)
      service1.CanRestoreFocusedNode = false;
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service2))
      return;
    service2.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    IDBTypedObjectID dbTypedObjectId = parentObject ?? items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (dbTypedObjectId == null)
      return;
    if ((!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service3) || service3.CachedEditingContextID == 0L ? 0 : (service3.IsECOEditingContext ? 1 : 0)) != 0 && MetaDataHelper.IsObjectTypeEditingContext(dbTypedObjectId.ObjectType) && Math.Abs(dbTypedObjectId.ObjectID) != Math.Abs(service3.CachedEditingContextID))
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1602"), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) != DialogResult.OK)
        return;
      service3.EditingContextID = dbTypedObjectId.ObjectID;
    }
    try
    {
      long objectByTypeDialog;
      if (additionalInfo is Tuple<int, int> tuple)
      {
        int aObjectTypeID = tuple.Item1;
        int aRelationTypeID = tuple.Item2;
        ObjectRelationLink objectRelationLink = new ObjectRelationLink(dbTypedObjectId.ObjectID, aRelationTypeID);
        objectByTypeDialog = service2.CreateObjectByTypeDialog(aObjectTypeID, new ObjectRelationLink[1]
        {
          objectRelationLink
        });
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          Hashtable allChildren = session.GetObjectType(parentObject == null ? items.GetItemID(0).TypeID : parentObject.ObjectType).GetAllChildren();
          if (allChildren.Count == 0)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_3406.ssp_imclient_3407()), (object) session.GetObjectType(parentObject == null ? items.GetItemID(0).TypeID : parentObject.ObjectType).ObjectTypeName));
          DataTable dataTable = session.GetObjectTypeCollection(-2, true).Select(string.Empty);
          Hashtable aObjectTypeIDRelationTypeIDs = new Hashtable();
          foreach (DictionaryEntry dictionaryEntry in allChildren)
          {
            if (dataTable.Rows.Find((object) (int) dictionaryEntry.Key) != null && (session.GetObjectType((int) dictionaryEntry.Key).Options & ObjectTypeOptions.DisableManualCreate) == ObjectTypeOptions.None)
              aObjectTypeIDRelationTypeIDs.Add(dictionaryEntry.Key, dictionaryEntry.Value);
          }
          if (aObjectTypeIDRelationTypeIDs.Count == 0)
            throw new Exception($"В составе объектов типа '{session.GetObjectType(parentObject == null ? items.GetItemID(0).TypeID : parentObject.ObjectType).ObjectTypeName}' нельзя создавать объекты командами Навигатора.");
          objectByTypeDialog = service2.CreateObjectByTypeDialog(aObjectTypeIDRelationTypeIDs, new long[1]
          {
            dbTypedObjectId.ObjectID
          });
        }
      }
      AfterObjectCreatorDialogHandlers.Handle(objectByTypeDialog, 0, items, viewServices, additionalInfo);
      if (objectByTypeDialog == 0L || objectByTypeDialog == -1L)
        return;
      ObjectCommands.OnCreateEntersInRelation(dbTypedObjectId.ObjectID, objectByTypeDialog, dbTypedObjectId.ObjectType);
    }
    finally
    {
      service2.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  /// <summary>Команда "Заменить объект в составе"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void ReplaceObject(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBObjectTypeID itemData1 = items.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID;
    IDBRelationID itemData2 = items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBObjectID dbObj = items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID;
    if (itemData1 == null || itemData2 == null || dbObj == null)
      return;
    List<long> relationIDs = new List<long>();
    INotificationService service1 = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(itemData2.ProjID, false);
      if (dbObject1 == null)
        return;
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_ObjectSelect"), string.Empty, itemData1.Value, viewServices, SelectionOptions.HideTree | SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
      if (numArray != null)
      {
        IDBRelation relation1 = sessionKeeper.Session.GetRelation(itemData2.Value, false);
        if (relation1 != null)
        {
          relation1.ReplacePartObject(numArray[0]);
          relationIDs.Add(relation1.RelationID);
        }
        if (dbObject1.ObjectType != MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545") && dbObject1.ObjectType != MetaDataHelper.GetObjectTypeID("cad0025f-306c-11d8-b4e9-00304f19f545"))
        {
          if (dbObject1.ObjectType != MetaDataHelper.GetObjectTypeID("cad0025e-306c-11d8-b4e9-00304f19f545"))
            goto label_43;
        }
        IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"));
        Guid conditionValue = attributeByGuid == null || !GuidHelper.IsGuid(attributeByGuid.AsString) ? Guid.Empty : new Guid(attributeByGuid.AsString);
        if (!conditionValue.Equals(Guid.Empty))
        {
          if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_InstanceDetected"), LocalizationHolder.rm.GetString("Client.Core_1467"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          {
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(dbObject1.ObjectType);
            ConditionStructure[] conditions = new ConditionStructure[2]
            {
              new ConditionStructure(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0),
              new ConditionStructure(-2, RelationalOperators.NotEqual, (object) dbObject1.ObjectID, LogicalOperators.NONE, 0, false)
            };
            ColumnDescriptor[] columns1 = new ColumnDescriptor[3]
            {
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
              new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
              new ColumnDescriptor((object) new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
            };
            DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns1);
            DataTable dataTable = objectCollection.Select(paramSet);
            ICompositionLoadService customService = sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
            IFiltrationService service2 = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
            StringBuilder stringBuilder = new StringBuilder();
            bool flag = false;
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              if (customService == null || service2 == null)
                return;
              long num1 = Convert.ToInt64(row.ItemArray[0]);
              IDBObject dbObject2 = (IDBObject) null;
              if (!num1.ToString().Contains("-"))
              {
                IDBObject dbObject3 = sessionKeeper.Session.GetObject(num1, false);
                if (dbObject3 != null)
                {
                  dbObject2 = dbObject3.CheckOut();
                  num1 = dbObject2.ObjectID;
                }
                else
                  continue;
              }
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num1);
              DataTable applicabilitiesList = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, objectInfo.ObjectTypeID);
              List<int> relList = new List<int>();
              foreach (int num2 in applicabilitiesList.Rows.Cast<DataRow>().Select<DataRow, int>((System.Func<DataRow, int>) (rows => Convert.ToInt32(rows.ItemArray[3]))).Where<int>((System.Func<int, bool>) (relID => !relList.Contains(relID))))
                relList.Add(num2);
              List<ColumnDescriptor> columns2 = new List<ColumnDescriptor>()
              {
                columns1[0],
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJ_GUID)
              };
              List<string> list = customService.LoadCompositions((object) sessionKeeper.Session.SessionGUID, num1, (IEnumerable<int>) relList, (IEnumerable<ColumnDescriptor>) columns2, service2.FiltrationServiceOwnerID).Rows.Cast<DataRow>().Where<DataRow>((System.Func<DataRow, bool>) (rows => rows.ItemArray[0].Equals((object) dbObj.Value))).Select<DataRow, string>((System.Func<DataRow, string>) (rows => rows.ItemArray[1].ToString())).ToList<string>();
              if (list.Count > 1)
              {
                stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Clien.Core_FindMoreOneObject"), row.ItemArray[1]));
                flag = true;
              }
              else if (list.Count == 1)
              {
                IDBRelation relation2 = sessionKeeper.Session.GetRelation(new Guid(list[0]), false);
                if (relation2 != null)
                {
                  relation2.ReplacePartObject(numArray[0]);
                  if (dbObject2 != null)
                  {
                    dbObject2.SaveChanges();
                    dbObject2.CheckIn();
                  }
                  relationIDs.Add(relation2.RelationID);
                }
              }
            }
            if (flag)
            {
              int num = (int) MessageBox.Show(stringBuilder.ToString(), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
          }
        }
      }
    }
label_43:
    if (service1 == null)
      return;
    DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsChanged", (IList<long>) relationIDs);
    service1.FireEvent((object) null, (NotificationEventArgs) e);
  }

  /// <summary>Команда "Заменить версию в составе"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void ReplaceObjectVersion(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBObjectTypeID itemData1 = items.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID;
    IDBRelationID itemData2 = items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBObjectID itemData3 = items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID;
    if (itemData1 == null || itemData2 == null || itemData3 == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(itemData2.Value, false);
      if (relation == null)
        return;
      if (relation.Attributes.FindByGUID(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545")) == null)
      {
        if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_VersionsNotEnable"), LocalizationHolder.rm.GetString("Client.Core_1467"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        relation.Attributes.AddAttribute(MetaDataHelper.GetAttributeID((object) "cad001c2-306c-11d8-b4e9-00304f19f545"), false);
        ObjectCommands.ReplaceObjectVersion(itemData3, relation);
      }
      else
        ObjectCommands.ReplaceObjectVersion(itemData3, relation);
    }
  }

  private static void ReplaceObjectVersion(IDBObjectID itemObj, IDBRelation rel)
  {
    long partObjectID = ObjectVersionSelection.SelectVersion(itemObj.ID, true, new List<long>(), new long[1]);
    if (partObjectID == 0L)
      return;
    rel.ReplacePartObject(partObjectID);
    INotificationService service1 = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    List<long> relationIDs = new List<long>();
    relationIDs.Add(rel.RelationID);
    if (service1 == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(rel.ProjID, false);
      if (dbObject1 == null)
        return;
      if (dbObject1.ObjectType != MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545") && dbObject1.ObjectType != MetaDataHelper.GetObjectTypeID("cad0025f-306c-11d8-b4e9-00304f19f545"))
      {
        if (dbObject1.ObjectType != MetaDataHelper.GetObjectTypeID("cad0025e-306c-11d8-b4e9-00304f19f545"))
          goto label_41;
      }
      IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"));
      Guid conditionValue = attributeByGuid == null || !GuidHelper.IsGuid(attributeByGuid.AsString) ? Guid.Empty : new Guid(attributeByGuid.AsString);
      if (!conditionValue.Equals(Guid.Empty))
      {
        if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_InstanceDetected"), LocalizationHolder.rm.GetString("Client.Core_1467"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(dbObject1.ObjectType);
          ConditionStructure[] conditions = new ConditionStructure[2]
          {
            new ConditionStructure(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0),
            new ConditionStructure(-2, RelationalOperators.NotEqual, (object) dbObject1.ObjectID, LogicalOperators.NONE, 0, false)
          };
          ColumnDescriptor[] columns1 = new ColumnDescriptor[3]
          {
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
            new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
            new ColumnDescriptor((object) new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
          };
          DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns1);
          DataTable dataTable = objectCollection.Select(paramSet);
          ICompositionLoadService customService = sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
          IFiltrationService service2 = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
          StringBuilder stringBuilder = new StringBuilder();
          bool flag = false;
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (customService == null || service2 == null)
              return;
            long num1 = Convert.ToInt64(row.ItemArray[0]);
            IDBObject dbObject2 = (IDBObject) null;
            if (!num1.ToString().Contains("-"))
            {
              IDBObject dbObject3 = sessionKeeper.Session.GetObject(num1, false);
              if (dbObject3 != null)
              {
                dbObject2 = dbObject3.CheckOut();
                num1 = dbObject2.ObjectID;
              }
              else
                continue;
            }
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num1);
            DataTable applicabilitiesList = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, objectInfo.ObjectTypeID);
            List<int> relList = new List<int>();
            foreach (int num2 in applicabilitiesList.Rows.Cast<DataRow>().Select<DataRow, int>((System.Func<DataRow, int>) (rows => Convert.ToInt32(rows.ItemArray[3]))).Where<int>((System.Func<int, bool>) (relID => !relList.Contains(relID))))
              relList.Add(num2);
            List<ColumnDescriptor> columns2 = new List<ColumnDescriptor>()
            {
              columns1[0],
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJ_GUID)
            };
            List<string> list = customService.LoadCompositions((object) sessionKeeper.Session.SessionGUID, num1, (IEnumerable<int>) relList, (IEnumerable<ColumnDescriptor>) columns2, service2.FiltrationServiceOwnerID).Rows.Cast<DataRow>().Where<DataRow>((System.Func<DataRow, bool>) (rows => rows.ItemArray[0].Equals((object) itemObj.Value))).Select<DataRow, string>((System.Func<DataRow, string>) (rows => rows.ItemArray[1].ToString())).ToList<string>();
            if (list.Count > 1)
            {
              stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Clien.Core_FindMoreOneObject"), row.ItemArray[1]));
              flag = true;
            }
            else if (list.Count == 1)
            {
              IDBRelation relation = sessionKeeper.Session.GetRelation(new Guid(list[0]), false);
              if (relation != null)
              {
                relation.ReplacePartObject(partObjectID);
                if (dbObject2 != null)
                {
                  dbObject2.SaveChanges();
                  dbObject2.CheckIn();
                }
                relationIDs.Add(relation.RelationID);
              }
            }
          }
          if (flag)
          {
            int num = (int) MessageBox.Show(stringBuilder.ToString(), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }
        }
      }
    }
label_41:
    DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsChanged", (IList<long>) relationIDs);
    service1.FireEvent((object) null, (NotificationEventArgs) e);
  }

  /// <summary>Команда "Исключить из состава"</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void ExcludeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    int projectTypeId = SelectedItemsHelper.GetProjectTypeID(items);
    if (ObjectTypeHelper.IsUnknownObjectTypeID(projectTypeId) || !(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData1))
      return;
    List<IDBRelationID> dbRelationIdList = new List<IDBRelationID>(items.Count);
    List<long> relationIDs = new List<long>(dbRelationIdList.Count);
    List<long> projIDs = new List<long>(dbRelationIdList.Count);
    List<int> relTypeIDs = new List<int>(dbRelationIdList.Count);
    List<long> objectIDs = new List<long>();
    HashSet<long> longSet = new HashSet<long>();
    string str = items.Count == 1 ? LocalizationHolder.rm.GetString("Client.Core_279") : LocalizationHolder.rm.GetString("Client.Core_280");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData2 && itemData2.Value != -1L && itemData2.Value != 0L)
        {
          dbRelationIdList.Add(itemData2);
          longSet.Add(itemData2.Value);
        }
      }
      str = string.Format(str, (object) sessionKeeper.Session.GetObject(itemData1.ProjID).NameInMessages);
    }
    if (MessageBox.Show(str, LocalizationHolder.rm.GetString("Client.Core_281"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    if (MetaDataHelper.IsObjectTypeChildOf(projectTypeId, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545")))
    {
      List<long> ecoRelations = new List<long>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService customService = sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
        List<ColumnDescriptor> columns = new List<ColumnDescriptor>(1);
        columns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
        int relationTypeId = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
        List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(projectTypeId, relationTypeId));
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(MetaDataHelper.GetAttributeTypeID(RevReqHelper.guidAttrDelWhenExcluded), RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true)
        };
        DataTable dataTable = customService.LoadComposition((object) sessionKeeper.Session.SessionGUID, itemData1.ProjID, relationTypeId, (IEnumerable<ColumnDescriptor>) columns, "cad005ac-306c-11d8-b4e9-00304f19f5455", (IEnumerable<ConditionStructure>) conditions, childrenIdRecursive.ToArray());
        if (dataTable != null)
        {
          if (dataTable.Rows.Count > 0)
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
              if (longSet.Contains(int64Value) && int64Value != 0L && ecoRelations.IndexOf(int64Value) < 0)
                ecoRelations.Add(int64Value);
            }
            dataTable.Dispose();
          }
        }
      }
      if (ecoRelations.Count > 0)
      {
        int selectedCount = 0;
        dbRelationIdList.ForEach((Action<IDBRelationID>) (item =>
        {
          if (ecoRelations.IndexOf(item.Value) < 0)
            return;
          ++selectedCount;
        }));
        if (MessageBox.Show(selectedCount > 1 ? LocalizationHolder.rm.GetString("Client.Core_1345") : LocalizationHolder.rm.GetString("Client.Core_1346"), LocalizationHolder.rm.GetString("Client.Core_281"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        sessionKeeper.Session.StartLogHistory();
        for (int index = 0; index < dbRelationIdList.Count; ++index)
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(dbRelationIdList[index].Value, false);
          if (relation != null)
          {
            relation.Delete(0L);
            relationIDs.Add(relation.RelationID);
            projIDs.Add(relation.ProjID);
            relTypeIDs.Add(relation.RelationType);
          }
        }
      }
      finally
      {
        foreach (CategoryValue modificationsHistory in sessionKeeper.Session.GetModificationsHistoryList())
        {
          if (modificationsHistory.CategoryType == 1 && (modificationsHistory.ActionID == ActionType.Delete || modificationsHistory.ActionID == ActionType.Purge) && objectIDs.IndexOf(modificationsHistory.CategoryID) < 0)
            objectIDs.Add(modificationsHistory.CategoryID);
        }
        sessionKeeper.Session.StopLogHistory();
        if (relationIDs.Count > 0)
        {
          DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs);
          Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
        }
        if (objectIDs.Count > 0)
        {
          DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs);
          Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
        }
      }
    }
  }

  /// <summary>
  /// Добавление в Clipboard объектов ClipboardObject при операциях Копировать и Вырезать
  /// </summary>
  private static void AddToClipboard(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo,
    bool IsCut)
  {
    ObjectCommands.AddToWindowsClipboard(items, viewServices, additionalInfo);
    ArrayList idList = new ArrayList(items.Count);
    IDBTypedObjectID parentData = items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    for (int index = 0; index < items.Count; ++index)
    {
      object itemData1 = items.GetItemData(index, typeof (IDBTypedObjectID));
      object itemData2 = items.GetItemData(index, typeof (IDBRelationID));
      if (itemData1 != null)
      {
        ClipboardObject clipboardObject = new ClipboardObject(itemData1 as IDBTypedObjectID, itemData2 as IDBRelationID);
        idList.Add((object) clipboardObject);
      }
    }
    IIOSource service = (IIOSource) (viewServices.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView);
    (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).SetDataObject((object) new ClipboardObjectsList(idList, IsCut, service, parentData));
  }

  /// <summary>
  /// Добавление в Clipboard текста из объекта ClipboardObject при выполнении операции Копировать текст
  /// </summary>
  private static void AddTextToClipboard(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo,
    bool IsCut)
  {
    ObjectCommands.AddTextToWindowsClipboard(items, viewServices, additionalInfo);
  }

  /// <summary>Добавление информации в буфер обмена Windows</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void AddToWindowsClipboard(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ChildrenView service1 = viewServices.GetService(typeof (ChildrenView)) as ChildrenView;
    NavigatorTreeView service2 = viewServices.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView;
    if (!(viewServices.GetService(typeof (ISelectedItemsText)) is ISelectedItemsText selectedItemsText1))
      selectedItemsText1 = (ISelectedItemsText) service1 ?? service2 as ISelectedItemsText;
    ISelectedItemsText selectedItemsText2 = selectedItemsText1;
    if (selectedItemsText2 == null)
      return;
    string selectedItemsText3 = selectedItemsText2.GetSelectedItemsText(SelectedItemsTextOptions.ColumnsCaptions, '\t'.ToString(), Environment.NewLine);
    if (string.IsNullOrEmpty(selectedItemsText3))
      return;
    try
    {
      Clipboard.SetText(selectedItemsText3, TextDataFormat.UnicodeText);
    }
    catch (Exception ex)
    {
    }
  }

  /// <summary>Добавление текста ячейки в буфер обмена Windows.</summary>
  /// <param name="items">Коллекция выделенных элементов.</param>
  /// <param name="viewServices">Контейнер сервисов.</param>
  /// <param name="additionalInfo">Дополнительная информация.</param>
  public static void AddTextToWindowsClipboard(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ISelectedItemsText service = (ISelectedItemsText) (viewServices.GetService(typeof (ChildrenView)) as ChildrenView);
    if (service == null)
      return;
    string selectedItemsText = service.GetSelectedItemsText(SelectedItemsTextOptions.None, string.Empty, string.Empty);
    if (!string.IsNullOrEmpty(selectedItemsText))
    {
      try
      {
        Clipboard.SetText(selectedItemsText, TextDataFormat.UnicodeText);
      }
      catch (Exception ex)
      {
      }
    }
    else
      Clipboard.Clear();
  }

  public static void CopyCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ObjectCommands.AddToClipboard(items, viewServices, additionalInfo, false);
  }

  public static void CopyTextCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ObjectCommands.AddTextToClipboard(items, viewServices, additionalInfo, false);
  }

  public static void CutCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ObjectCommands.AddToClipboard(items, viewServices, additionalInfo, true);
  }

  public static void AddFolderCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationType relationType = sessionKeeper.Session.GetRelationType(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"));
        long objectByTypeDialog = service.CreateObjectByTypeDialog(new Guid("cad00150-306c-11d8-b4e9-00304f19f545"), new ObjectRelationLink[1]
        {
          new ObjectRelationLink(itemData.ObjectID, relationType.RelationType)
        });
        AfterObjectCreatorDialogHandlers.Handle(objectByTypeDialog, 0, items, viewServices, additionalInfo);
        if (objectByTypeDialog == 0L || objectByTypeDialog == -1L || objectByTypeDialog == 0L)
          return;
        IDBRelation relation = sessionKeeper.Session.GetRelation(itemData.ObjectID, objectByTypeDialog, true);
        DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsCreated", relation.RelationID, relation.ProjID, relation.RelationType);
        Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
      }
    }
    finally
    {
      service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  /// <summary>
  /// Получить список допустимых типов объектов, которые можно включить в состав указанного родительского объекта
  /// </summary>
  /// <param name="projID">Идентификатор родительского типа объектов</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>Список, либо null, если у данного объекта не может быть состав, либо пустой список, если нет прав доступа добавлять что-то в состав</returns>
  public static List<int> GetObjectTypesForComposition(long projID, System.IServiceProvider services)
  {
    IObjectTypeNodeFilter service1 = services != null ? services.GetService(typeof (IObjectTypeNodeFilter)) as IObjectTypeNodeFilter : (IObjectTypeNodeFilter) null;
    List<int> typesForComposition = new List<int>(0);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      QuickObjectInfo objectInfo = session.GetObjectInfo(projID);
      Hashtable possibleChildren = session.GetObjectType(objectInfo.ObjectTypeID).GetPossibleChildren();
      if (possibleChildren.Count == 0)
        return (List<int>) null;
      IObjectTypeHierarchy service2 = (IObjectTypeHierarchy) ((ICacheServices) ServicesManager.GetService(typeof (ICacheServices))).GetService("ObjectTypeHierarchy");
      List<int> intList = new List<int>(0);
      foreach (int key in (IEnumerable) possibleChildren.Keys)
      {
        if (!intList.Contains(key) && service2.EnabledObjectType(key))
        {
          intList.Add(key);
          service1?.EnabledObjectTypes.Add(key);
          int parentType = service2.GetParentType(key);
          if (parentType != -1)
          {
            IDBObjectType objectType = session.GetObjectType(parentType);
            if (objectType.Versionable == ObjectVersionModes.Abstract)
            {
              intList.Add(objectType.ObjectType);
              service1?.EnabledObjectTypes.Add(objectType.ObjectType);
            }
          }
        }
      }
      for (int index1 = 0; index1 < intList.Count; ++index1)
      {
        int childTypeID = intList[index1];
        int[] parentTypes = service2.GetParentTypes(childTypeID);
        if (parentTypes == null || parentTypes.Length == 0)
        {
          if (!typesForComposition.Contains(childTypeID))
            typesForComposition.Add(childTypeID);
        }
        else
        {
          if (!typesForComposition.Contains(childTypeID))
            typesForComposition.Add(childTypeID);
          for (int index2 = 0; index2 < parentTypes.Length; ++index2)
          {
            if (intList.Contains(parentTypes[index2]))
            {
              typesForComposition.Remove(childTypeID);
              childTypeID = parentTypes[index2];
              if (!typesForComposition.Contains(childTypeID))
                typesForComposition.Add(childTypeID);
            }
            else if (!typesForComposition.Contains(childTypeID))
              typesForComposition.Add(childTypeID);
          }
        }
      }
      if (typesForComposition.Count == 0)
        return new List<int>();
    }
    return typesForComposition;
  }

  /// <summary>
  /// Отобразить стандартное окно "Добавить в состав" для указанного родительского объекта, вернуть список выбранных объектов.
  /// При ошибках будут сгенерированы соответствующие исключения
  /// </summary>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>Список выбранных объектов</returns>
  public static IDBTypedObjectID[] SelectObjectsForComposition(
    long projID,
    System.IServiceProvider services)
  {
    AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer();
    DescriptorCollection descriptorCollection = new DescriptorCollection();
    IObjectTypeNodeFilter serviceInstance = (IObjectTypeNodeFilter) new ObjectTypeNodeFilter();
    serviceContainer.AddService(typeof (IObjectTypeNodeFilter), (object) serviceInstance);
    serviceContainer.AdvancedProvider = services;
    string str = string.Empty;
    List<int> typesForComposition = ObjectCommands.GetObjectTypesForComposition(projID, (System.IServiceProvider) serviceContainer);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(projID);
      str = dbObject.NameInMessages;
      if (typesForComposition == null)
        throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString(sc_3406.ssp_imclient_3408()), (object) MetaDataHelper.GetObjectTypeName(dbObject.ObjectType)));
      if (typesForComposition.Count == 0)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_3406.ssp_imclient_3409()), (object) MetaDataHelper.GetObjectTypeName(dbObject.ObjectType)));
    }
    return Intermech.Navigator.SelectionWindow.Select(string.Format(LocalizationHolder.rm.GetString("Client.Core_282"), (object) str), (IDescriptor) new ObjectTypesDescriptor(typesForComposition.ToArray(), LocalizationHolder.rm.GetString("Client.Core_283")), typeof (IDBTypedObjectID), (System.IServiceProvider) serviceContainer, SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule) as IDBTypedObjectID[];
  }

  /// <summary>"Добавить в состав"</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void AddCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (viewServices != null && viewServices.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service1)
      service1.CanRestoreFocusedNode = false;
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IServiceContainer nodesContext = (IServiceContainer) new ServiceContainer();
    int[] allowablePartTypeIds = ObjectCommands.GetAllowablePartTypeIds(itemData.ObjectType);
    IObjectTypeNodeFilter serviceInstance = allowablePartTypeIds.Length != 0 ? (IObjectTypeNodeFilter) new ObjectTypeNodeFilter(allowablePartTypeIds) : throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_3406.ssp_imclient_3410()), (object) MetaDataHelper.GetObjectTypeName(itemData.ObjectType)));
    nodesContext.AddService(typeof (IObjectTypeNodeFilter), (object) serviceInstance);
    string str = string.Empty;
    Hashtable possibleChildren;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(itemData.ObjectID);
      possibleChildren = session.GetObjectType(itemData.ObjectType).GetPossibleChildren();
      if (possibleChildren.Count == 0)
        throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString(sc_3406.ssp_imclient_3411()), (object) session.GetObjectType(items.GetItemID(0).TypeID).ObjectTypeName));
      str = dbObject.NameInMessages;
    }
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add((IDescriptor) new ObjectTypesDescriptor(ObjectCommands.GetRootTypeIds(allowablePartTypeIds), LocalizationHolder.rm.GetString("Client.Core_283")));
    descriptors.Add((IDescriptor) new DesktopNodeDescriptor(DesktopObjectNode.DesktopObjectID));
    if (ServicesManager.GetService(typeof (IArchivesDescriptorService)) is IArchivesDescriptorService service2)
      descriptors.Add(service2.GetDescriptor());
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_1099"), descriptors);
    IDBTypedObjectID[] objectIDs = (IDBTypedObjectID[]) Intermech.Navigator.SelectionWindow.Select((string) null, string.Format(LocalizationHolder.rm.GetString("Client.Core_282"), (object) str), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), (DynamicSelectionEventHandler) null, (System.IServiceProvider) nodesContext, SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule, allowablePartTypeIds);
    if (objectIDs == null)
      return;
    ObjectCommands.DoInsertIntoObject(items.GetParentPath(0), itemData, objectIDs, (IDBRelationID[]) null, possibleChildren, viewServices, NavigatorRelationCommand.InsertIn);
  }

  private static int[] GetAllowablePartTypeIds(int objectTypeID)
  {
    List<int> source = new List<int>();
    foreach (IMSApplicability typeApplicability in MetaDataHelper.GetObjectTypeApplicabilities(objectTypeID))
    {
      source.Add(typeApplicability.ChildObjectTypeID);
      source.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(typeApplicability.ChildObjectTypeID));
    }
    return source.Distinct<int>().ToArray<int>();
  }

  private static int[] GetRootTypeIds(int[] objectTypeIds)
  {
    return ((IEnumerable<int>) objectTypeIds).Select<int, int>((System.Func<int, int>) (o => ObjectCommands.GetRootTypeID(o))).Distinct<int>().ToArray<int>();
  }

  private static int GetRootTypeID(int objectTypeID)
  {
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(objectTypeID);
    return objectTypeParentsId.Count <= 0 ? objectTypeID : objectTypeParentsId.Last<int>();
  }

  /// <summary>Очистить коллекции insert*</summary>
  internal static void ClearInsertCollections() => ObjectCommands.insertIncluded = new List<long>();

  /// <summary>
  /// Включает объекты objectIDs в состав объекта parentObject. linkTypes содержит типы связей для типов объектов
  /// (если linkTypes == null, то процедура сама получит эту инфу)
  /// </summary>
  /// <param name="parentPath"></param>
  /// <param name="parentObject"></param>
  /// <param name="objectIDs"></param>
  /// <param name="relationIDs"></param>
  /// <param name="linkTypes"></param>
  /// <param name="viewServices"></param>
  /// <param name="relCommand">Код выполняемой команды "Навигатора"</param>
  public static bool DoInsertIntoObject(
    NodeIDPath parentPath,
    IDBTypedObjectID parentObject,
    IDBTypedObjectID[] objectIDs,
    IDBRelationID[] relationIDs,
    Hashtable linkTypes,
    System.IServiceProvider viewServices,
    NavigatorRelationCommand relCommand)
  {
    return ObjectCommands.DoInsertIntoObject(parentPath, parentObject, objectIDs, relationIDs, linkTypes, false, viewServices, relCommand);
  }

  /// <summary>
  /// Включает объекты objectIDs в состав объекта parentObject. linkTypes содержит типы связей для типов объектов
  /// (если linkTypes == null, то процедура сама получит эту инфу)
  /// </summary>
  /// <param name="parentPath">Путь к родительскому узлу</param>
  /// <param name="parentObject">Родительский объект, в который происходит вставка</param>
  /// <param name="objectIDs">Список идентификаторов вставляемых объектов</param>
  /// <param name="relationIDs">Список связей-прототипов</param>
  /// <param name="linkTypes">Список типов связей</param>
  /// <param name="isCut">true - данные вставляются после команды "Вырезать"</param>
  /// <param name="viewServices">Контейнер сервисов для родительского узла</param>
  /// <param name="relCommand">Код выполняемой команды "Навигатора"</param>
  public static bool DoInsertIntoObject(
    NodeIDPath parentPath,
    IDBTypedObjectID parentObject,
    IDBTypedObjectID[] objectIDs,
    IDBRelationID[] relationIDs,
    Hashtable linkTypes,
    bool isCut,
    System.IServiceProvider viewServices,
    NavigatorRelationCommand relCommand)
  {
    ObjectCommands.ClearInsertCollections();
    if (objectIDs != null)
    {
      if (objectIDs.Length != 0)
      {
        try
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            sessionKeeper.Session.StartLogHistory();
            try
            {
              IUserSession session = sessionKeeper.Session;
              if (isCut)
              {
                relCommand = NavigatorRelationCommand.CutPaste;
                try
                {
                  foreach (ClipboardObject objectId in objectIDs)
                  {
                    if (objectId.Value != -1L)
                    {
                      IDBRelation relation = sessionKeeper.Session.GetRelation(objectId.Value, false);
                      if (relation != null)
                      {
                        relation.ProjID = parentObject.ObjectID;
                        ObjectCommands.insertIncluded.Add(relation.RelationID);
                      }
                    }
                  }
                  NotificationHelper.Notify((object) null, sessionKeeper.Session.GetModificationsHistoryList());
                  return true;
                }
                finally
                {
                  (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).RefreshImage();
                }
              }
              else
              {
                relCommand = NavigatorRelationCommand.CopyPaste;
                if (linkTypes == null)
                  linkTypes = session.GetObjectType(parentObject.ObjectType).GetPossibleChildren();
                for (int index = 0; index < objectIDs.Length; ++index)
                {
                  if (relationIDs != null && relationIDs.Length == objectIDs.Length)
                  {
                    IDBRelationID relationId = relationIDs[index];
                  }
                  int objectType = objectIDs[index].ObjectType;
                  object linkType = linkTypes[(object) objectType];
                  if (linkType == null)
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_3406.ssp_imclient_3412()), (object) session.GetObjectType(objectType).ObjectTypeName, (object) session.GetObjectType(parentObject.ObjectType).ObjectTypeName));
                  IDBRelationCollection relationCollection;
                  if (linkType is int relationType)
                  {
                    relationCollection = session.GetRelationCollection(relationType);
                    linkTypes[(object) objectType] = (object) relationCollection;
                  }
                  else
                    relationCollection = linkTypes[(object) objectType] as IDBRelationCollection;
                  NewRelationProperties properties = new NewRelationProperties(0L, parentObject.ObjectID, objectIDs[index].ID, DateTime.Now);
                  properties.PartObjectID = objectIDs[index].ObjectID;
                  CompositionContextsHolder service = viewServices != null ? viewServices.GetService(typeof (CompositionContextsHolder)) as CompositionContextsHolder : (CompositionContextsHolder) null;
                  bool flag = service == null;
                  CompositionContextsHolder compositionContextsHolder = service ?? ServicesManager.GetService(typeof (CompositionContextsHolder)) as CompositionContextsHolder;
                  if (compositionContextsHolder != null)
                  {
                    if (flag)
                      ServicesManager.RemoveService(typeof (CompositionContextsHolder));
                    properties.ValuesList = new AttributeValues[1]
                    {
                      new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00651-306c-11d8-b4e9-00304f19f545"), FieldTypes.ftInteger, MultiValueModes.MultiValuesFromList, compositionContextsHolder.Contexts.ConvertAll<object>((Converter<long, object>) (item => (object) item)).ToArray())
                    };
                  }
                  IDBRelation dbRelation = relationCollection.Create(properties);
                  ObjectCommands.insertIncluded.Add(dbRelation.RelationID);
                }
                NotificationHelper.Notify((object) null, sessionKeeper.Session.GetModificationsHistoryList(), relCommand);
              }
            }
            finally
            {
              sessionKeeper.Session.StopLogHistory();
            }
          }
        }
        finally
        {
          IClipboard service = (IClipboard) ServicesManager.GetService(typeof (IClipboard));
          if (service != null & isCut)
            service.RemoveCurrentDataObject();
        }
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Вставить объекты из буфера обмена в указанные выделенные элементы
  /// </summary>
  /// <param name="items">Коллекция выделенных элементов (приёмник вставки)</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void PasteCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (viewServices.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service)
      service.CanRestoreFocusedNode = false;
    object dataObject = (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).GetDataObject();
    if (dataObject == null || !(dataObject is IDBObjectTypedIDCollection))
      return;
    NodeIDPath parentPath = items.GetParentPath(0);
    bool isCut = dataObject is ICutCopy && (dataObject as ICutCopy).IsCut;
    bool flag1 = true;
    bool flag2 = false;
    bool flag3 = false;
    for (int index = 0; index < items.Count && !flag3; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      try
      {
        if (ObjectCommands.DoInsertIntoObject(parentPath, itemData, ((IDBObjectTypedIDCollection) dataObject).GetTypedObjects(), ((IDBObjectTypedIDCollection) dataObject).GetRelations(), (Hashtable) null, isCut, viewServices, NavigatorRelationCommand.Unknown) & isCut)
          (dataObject as ICutCopy).IsCut = false;
      }
      catch (Exception ex)
      {
        if (!flag2)
        {
          if (!flag1 || index == items.Count - 1)
            throw;
          List<IMMessageBoxButton> messageBoxButtonList = new List<IMMessageBoxButton>();
          messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1348"), DialogResult.Abort));
          messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_579"), DialogResult.Ignore));
          messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_580"), DialogResult.Retry));
          messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_581"), DialogResult.No));
          while (flag1)
          {
            switch (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_132"), LocalizationHolder.rm.GetString("Client.Core_1349"), messageBoxButtonList.ToArray(), IMMessageBoxImage.Question))
            {
              case DialogResult.Cancel:
              case DialogResult.Abort:
                flag3 = true;
                goto label_17;
              case DialogResult.Retry:
                flag2 = true;
                goto label_17;
              case DialogResult.Ignore:
                goto label_17;
              case DialogResult.No:
                ExceptionHelper.ExceptionService.ShowException(ex);
                flag1 = true;
                continue;
              default:
                continue;
            }
          }
        }
      }
label_17:
      if (isCut)
        break;
    }
  }

  /// <summary>
  /// Сделать выделенные в "Навигаторе" объекты базовыми версиями
  /// </summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void MakeBaseVersion(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> objectIDs = new List<long>(items.Count);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = 0; index < items.Count; ++index)
        {
          IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          if (objectIDs.IndexOf(itemData.ObjectID) < 0)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
            if (dbObject != null)
            {
              dbObject.MakeBaseVersion();
              objectIDs.AddRange((IEnumerable<long>) sessionKeeper.Session.GetObjectIDVersions(itemData.ObjectID));
            }
          }
        }
      }
    }
    finally
    {
      if (objectIDs.Count > 0)
      {
        DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs);
        Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
      }
    }
  }

  /// <summary>Удалить выделенные в "Навигаторе" объекты</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void DeleteCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    DeleteItemsCommand deleteItemsCommand = new DeleteItemsCommand();
    deleteItemsCommand.DeleteOptions = ObjectCommands.DeleteOptions;
    deleteItemsCommand.Init(items, viewServices, additionalInfo);
    ObjectCommands.DeleteOptions &= ~DeleteAnalyzerOptions.FindAllVersions;
    deleteItemsCommand.Execute();
  }

  public static void CheckoutCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    CheckoutItemsCommand checkoutItemsCommand = new CheckoutItemsCommand();
    checkoutItemsCommand.Init(items, viewServices, additionalInfo);
    checkoutItemsCommand.Execute();
  }

  public static void SaveChangesCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    SaveChangedItemsCommand changedItemsCommand = new SaveChangedItemsCommand();
    changedItemsCommand.Init(items, viewServices, additionalInfo);
    changedItemsCommand.Execute();
  }

  public static void CheckinCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    CheckinItemsCommand checkinItemsCommand = new CheckinItemsCommand();
    checkinItemsCommand.Init(items, viewServices, additionalInfo);
    checkinItemsCommand.Execute();
  }

  /// <summary>Отмена изменений в объектах</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void CancelCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    UndoChangedItemsCommand changedItemsCommand = new UndoChangedItemsCommand();
    changedItemsCommand.Init(items, viewServices, additionalInfo);
    changedItemsCommand.Execute();
  }

  public static void AdminCancelCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData1 = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(itemData1.ObjectID);
      if (MessageBox.Show(items.Count <= 1 ? string.Format(LocalizationHolder.rm.GetString("Client.Core_290"), (object) dbObject1.NameInMessages) : string.Format(LocalizationHolder.rm.GetString("Client.Core_289"), (object) items.Count), LocalizationHolder.rm.GetString("Client.Core_281"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      List<long> objectIDs = new List<long>();
      try
      {
        for (int index = 0; index < items.Count; ++index)
        {
          IDBTypedObjectID itemData2 = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(itemData2.ObjectID);
          if (dbObject2.CheckoutBy > 0L && dbObject2.CheckoutBy != sessionKeeper.Session.UserID)
            dbObject2 = sessionKeeper.Session.GetObject(-Math.Abs(itemData2.ObjectID));
          if (dbObject2 != null && dbObject2.ObjectID < 0L && dbObject2.CheckoutBy > 0L && !objectIDs.Contains(dbObject2.ObjectID) && !objectIDs.Contains(-dbObject2.ObjectID))
          {
            dbObject2.CancelChanges(true);
            objectIDs.Add(Math.Abs(itemData2.ObjectID));
            objectIDs.Add(-Math.Abs(itemData2.ObjectID));
            RecentObjectsNode.MRUObjects.Add(Math.Abs(itemData2.ObjectID), ObjectAction.CancelChanges, DateTime.UtcNow);
          }
        }
      }
      finally
      {
        if (objectIDs.Count > 0)
        {
          DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs);
          Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
        }
        else
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_291"), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
    }
  }

  public static void OpenCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> longList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBObjectID itemData = (IDBObjectID) items.GetItemData(index, typeof (IDBObjectID));
      if (itemData != null && !longList.Contains(itemData.Value))
      {
        longList.Add(itemData.Value);
        ObjectCommand openCommand = ObjectCommandFactory.CreateOpenCommand(false);
        if (openCommand != null)
        {
          openCommand.ObjectId = itemData.Value;
          openCommand.Execute();
          RecentObjectsNode.MRUObjects.Add(itemData.Value, ObjectAction.Open, DateTime.UtcNow);
        }
      }
    }
  }

  public static void EditCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> longList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBObjectID itemData = (IDBObjectID) items.GetItemData(index, typeof (IDBObjectID));
      if (itemData != null && !longList.Contains(itemData.Value))
      {
        longList.Add(itemData.Value);
        ObjectCommand editCommand = ObjectCommandFactory.CreateEditCommand(false);
        if (editCommand != null)
        {
          editCommand.ObjectId = itemData.Value;
          editCommand.Execute();
          RecentObjectsNode.MRUObjects.Add(itemData.Value, ObjectAction.Edit, DateTime.UtcNow);
        }
      }
    }
  }

  public static void ViewCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> longList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBObjectID itemData = (IDBObjectID) items.GetItemData(index, typeof (IDBObjectID));
      if (itemData != null && !longList.Contains(itemData.Value))
      {
        longList.Add(itemData.Value);
        ObjectCommand viewCommand = ObjectCommandFactory.CreateViewCommand(false);
        if (viewCommand != null)
        {
          viewCommand.ObjectId = itemData.Value;
          viewCommand.Execute();
          RecentObjectsNode.MRUObjects.Add(itemData.Value, ObjectAction.View, DateTime.UtcNow);
        }
      }
    }
  }

  public static void PrintCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> longList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBObjectID itemData = (IDBObjectID) items.GetItemData(index, typeof (IDBObjectID));
      if (itemData != null && !longList.Contains(itemData.Value))
      {
        longList.Add(itemData.Value);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          sessionKeeper.Session.GetObjectByID(itemData.ID, true).Print();
        ObjectCommand printCommand = ObjectCommandFactory.CreatePrintCommand(false);
        if (printCommand != null)
        {
          printCommand.ObjectId = itemData.Value;
          printCommand.Execute();
          RecentObjectsNode.MRUObjects.Add(itemData.Value, ObjectAction.Print, DateTime.UtcNow);
        }
      }
    }
  }

  public static void PrintPDFCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    SelectedItemsCommand printPdfCommand = (ApplicationServices.Container.GetService(typeof (IPrintPDFCommandFactory)) as IPrintPDFCommandFactory).GetPrintPDFCommand();
    printPdfCommand.Init(items, viewServices, additionalInfo);
    printPdfCommand.Execute();
  }

  public static void OpenWithCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> longList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBObjectID itemData = (IDBObjectID) items.GetItemData(index, typeof (IDBObjectID));
      if (itemData != null && !longList.Contains(itemData.Value))
      {
        longList.Add(itemData.Value);
        ObjectCommand openWithCommand = ObjectCommandFactory.CreateOpenWithCommand(false);
        if (openWithCommand != null)
        {
          openWithCommand.ObjectId = itemData.Value;
          openWithCommand.Execute();
        }
      }
    }
  }

  /// <summary>Вызывает диалог изменения шага ЖЦ объекта</summary>
  public static void SetLifecycleStepCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long[] objectIDs1 = new long[items.Count];
    List<int> stepsID = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        objectIDs1[index] = (items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
        int lcStep = sessionKeeper.Session.GetObject(objectIDs1[index]).LCStep;
        if (stepsID.IndexOf(lcStep) < 0)
          stepsID.Add(lcStep);
      }
      ObjectSteps[] objectsSteps = sessionKeeper.Session.GetLifecycleStepCollection(0).GetObjectsSteps(stepsID);
      if (objectsSteps == null)
      {
        if (items.Count > 1)
        {
          int num1 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_281"), LocalizationHolder.rm.GetString("Client.Core_292"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
        }
        else
        {
          int num2 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_281"), LocalizationHolder.rm.GetString("Client.Core_293"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
        }
      }
      else
      {
        SetObjectsLCStep setObjectsLcStep = new SetObjectsLCStep(objectsSteps);
        if (setObjectsLcStep.ShowDialog() != DialogResult.OK || setObjectsLcStep.StepSelected == -1)
          return;
        sessionKeeper.Session.StartLogHistory();
        try
        {
          sessionKeeper.Session.GetLifecycleStepCollection(0).SetObjectsLCStep(objectIDs1, setObjectsLcStep.StepSelected);
          IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(setObjectsLcStep.StepSelected, false);
          bool flag = MetaDataHelper.GetLCStepID(new Guid("cad003cc-306c-11d8-b4e9-00304f19f545")) == lifecycleStep.LCStep;
          List<long> longList = flag ? new List<long>() : (List<long>) null;
          if (flag)
          {
            List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00348-306c-11d8-b4e9-00304f19f545"));
            for (int index = 0; index < items.Count; ++index)
            {
              IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
              if (childrenIdRecursive.Contains(itemData.ObjectType))
                longList.Add(itemData.ObjectID);
            }
          }
          sessionKeeper.Session.GetLifecycleLevel(lifecycleStep.LevelID, false)?.GUID.Equals(new Guid("cad0000e-306c-11d8-b4e9-00304f19f545"));
          List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
          if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
            return;
          List<long> objectIDs2 = new List<long>();
          List<long> objectIDs3 = new List<long>();
          foreach (CategoryValue categoryValue in modificationsHistoryList)
          {
            if (categoryValue.CategoryType == 1 && (categoryValue.ActionID == ActionType.Delete || categoryValue.ActionID == ActionType.Purge))
              objectIDs2.Add(categoryValue.CategoryID);
            if (categoryValue.CategoryType == 1 && categoryValue.ActionID == ActionType.NextLCStep)
              objectIDs3.Add(categoryValue.CategoryID);
          }
          for (int index = objectIDs3.Count - 1; index >= 0; --index)
          {
            if (objectIDs2.Contains(objectIDs3[index]))
              objectIDs3.RemoveAt(index);
          }
          if (objectIDs2.Count > 0)
            service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs2));
          sessionKeeper.Session.ClearObjectSmartCache();
          if (objectIDs3.Count > 0)
            service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs3));
          if (!flag || longList.Count <= 0)
            return;
          if (objectIDs2.Count > 0)
          {
            for (int index = longList.Count - 1; index >= 0; --index)
            {
              if (objectIDs2.Contains(longList[index]))
                longList.RemoveAt(index);
            }
          }
          if (longList.Count <= 0)
            return;
          service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("RevisionsActualized", (IList<long>) objectIDs3));
        }
        finally
        {
          sessionKeeper.Session.StopLogHistory();
        }
      }
    }
  }

  /// <summary>
  /// Позволяет перевести на шаг ЖЦ объекты состава группирующего объекта
  /// </summary>
  public static void SetLifecycleStepChildsCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !MetaDataHelper.HasObjectTypeGroupingRelTypes(itemData.ObjectType))
      return;
    List<long> longList = new List<long>();
    ObjectSteps[] os = (ObjectSteps[]) null;
    int num1 = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = (DataTable) null;
      ColumnDescriptor[] columns = new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
      object[] objArray = new object[0];
      SortOrders[] sortOrdersArray = new SortOrders[0];
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-21, RelationalOperators.Equal, (object) itemData.ObjectID, LogicalOperators.NONE, 0, true)
      }, columns);
      int defaultRelationTypeId = MetaDataHelper.GetDefaultRelationTypeID(itemData.ObjectType);
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(defaultRelationTypeId);
      try
      {
        if (relationCollection != null)
          dataTable = relationCollection.Select(paramSet);
      }
      catch
      {
        dataTable = (DataTable) null;
      }
      if (dataTable == null || dataTable.Rows.Count == 0)
        return;
      List<int> stepsID = new List<int>();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        long result;
        if (long.TryParse(dataTable.Rows[index][0].ToString(), out result))
        {
          if (!longList.Contains(result))
            longList.Add(result);
          int int32 = Convert.ToInt32(dataTable.Rows[index][1]);
          if (!stepsID.Contains(int32))
            stepsID.Add(int32);
        }
      }
      os = sessionKeeper.Session.GetLifecycleStepCollection(0).GetObjectsSteps(stepsID);
    }
    if (os == null || os.Length == 0)
    {
      if (longList.Count > 1)
      {
        int num2 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_281"), LocalizationHolder.rm.GetString("Client.Core_294"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
      }
      else
      {
        int num3 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_281"), LocalizationHolder.rm.GetString("Client.Core_295"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
      }
    }
    else
    {
      using (SetObjectsLCStep setObjectsLcStep = new SetObjectsLCStep(os))
      {
        if (setObjectsLcStep.ShowDialog() != DialogResult.OK || setObjectsLcStep.StepSelected == -1)
          return;
        num1 = setObjectsLcStep.StepSelected;
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.StartLogHistory();
        try
        {
          sessionKeeper.Session.GetLifecycleStepCollection(0).SetObjectsLCStep(longList.ToArray(), num1);
          IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(num1, false);
          bool flag = false;
          IDBLifecycleLevelType lifecycleLevel = sessionKeeper.Session.GetLifecycleLevel(lifecycleStep.LevelID, false);
          if (lifecycleLevel != null)
            flag = lifecycleLevel.GUID.Equals(new Guid("cad0000e-306c-11d8-b4e9-00304f19f545"));
          List<long> objectIDs = new List<long>();
          List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
          if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
            return;
          foreach (CategoryValue categoryValue in modificationsHistoryList)
          {
            if (categoryValue.CategoryType == 1 && categoryValue.ActionID == ActionType.NextLCStep)
              objectIDs.Add(categoryValue.CategoryID);
          }
          DBObjectsEventArgs e = new DBObjectsEventArgs(flag ? "ObjectsRemoved" : "ObjectsChanged", (IList<long>) objectIDs);
          service.FireEvent((object) null, (NotificationEventArgs) e);
        }
        finally
        {
          sessionKeeper.Session.StopLogHistory();
        }
      }
    }
  }

  /// <summary>
  /// Устанавливает правило подбора версий из текущего группирующего объекта
  /// </summary>
  public static void SetVersionsRule(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    IMSAttribute4ObjectType attribute4ObjectType1 = MetaDataHelper.GetAttribute4ObjectType(itemData.ObjectType, MetaDataHelper.GetAttributeTypeID("cad00696-306c-11d8-b4e9-00304f19f545"));
    IMSAttribute4ObjectType attribute4ObjectType2 = MetaDataHelper.GetAttribute4ObjectType(itemData.ObjectType, MetaDataHelper.GetAttributeTypeID("cad001d2-306c-11d8-b4e9-00304f19f545"));
    if (attribute4ObjectType1 == null && attribute4ObjectType2 == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject RuleObject = sessionKeeper.Session.GetObject(itemData.ObjectID);
      VersionsRule versionsRule = new VersionsRule();
      versionsRule.Valid(sessionKeeper.Session);
      try
      {
        if (attribute4ObjectType2 != null)
        {
          versionsRule.LoadFromObject(sessionKeeper.Session, RuleObject);
        }
        else
        {
          IDBAttribute attributeByGuid = RuleObject.GetAttributeByGuid(new Guid("cad00696-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
          {
            versionsRule.LoadFromAttribute(sessionKeeper.Session, attributeByGuid);
            if (versionsRule.Criterions.Count == 0)
            {
              versionsRule = new VersionsRule();
              versionsRule.Valid(sessionKeeper.Session);
            }
          }
          versionsRule.RuleObjectCaption = RuleObject.Caption;
          versionsRule.RuleObjectGuid = RuleObject.ObjectGUID.ToString();
          versionsRule.RuleObjectModified = RuleObject.CreateDate;
          versionsRule.CurrentRuleType = VersionsRuleType.vrtSystemRule;
        }
      }
      catch
      {
      }
      (ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService).RuleClass = versionsRule;
    }
  }

  /// <summary>Создание аутентичных файлов</summary>
  public static void CreateAuthFilesCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(ServicesManager.GetService(typeof (IAuthFilesService)) is IAuthFilesService service))
      return;
    service.CheckAuthFiles(items, false);
  }

  /// <summary>Сохранение аутентичных файлов</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void SaveAuthFilesCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    string str = string.Empty;
    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
    {
      folderBrowserDialog.Description = "Укажите папку для сохранения аутентичных файлов";
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      str = folderBrowserDialog.SelectedPath;
    }
    if (!(ServicesManager.GetService(typeof (IAuthFilesService)) is IAuthFilesService service) || !service.CheckAuthFiles(items, true))
      return;
    if (!Directory.Exists(str))
      Directory.CreateDirectory(str);
    service.SaveAuthFiles(items, str, (AuthFileSaveNameResolveHandler) null);
    int num = (int) IMMessageBox.Show("Информация", "Операция сохранения аутентичных файлов выполнена", MessageBoxButtons.OK, IMMessageBoxImage.Information);
  }

  /// <summary>Просмотр аутентичных файлов</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void ViewAuthFilesCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count == 0 || !(ServicesManager.GetService(typeof (IAuthFilesService)) is IAuthFilesService service) || !service.CheckAuthFiles(items, true))
      return;
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID);
      if (dbObject == null)
        return;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid == null)
        return;
      for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
      {
        attributeByGuid.Index = index;
        if (attributeByGuid is IBlobReader blobReader)
        {
          BlobInformation blobInformation = blobReader.OpenBlob(-1);
          if (blobInformation.FileType == FileTypes.ftAuthentical)
          {
            using (new DynamicScope())
            {
              LaunchActionServiceVars.RootObjectMode.Declare(true);
              ClientContext.LaunchActions.LaunchByShell(new LaunchParams(LaunchType.View, itemData.ObjectID, itemData.ObjectType, VersionsRuleSources.GetCurrentWindowRule())
              {
                ObjectFileName = blobInformation.FileName
              });
              return;
            }
          }
        }
      }
    }
    int num = (int) IMMessageBox.Show(MessageDialogs.msgInformation, LocalizationHolder.rm.GetString("AuthFileMissing"), new IMMessageBoxButton[1]
    {
      new IMMessageBoxButton("OK", DialogResult.OK)
    }, IMMessageBoxImage.Warning);
  }

  /// <summary>Для работы с историей объекта</summary>
  public static void ObjectLCHistoryCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectId = (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
      int num = (int) new ObjectLCHistory(sessionKeeper.Session.GetObject(objectId), true).ShowDialog();
    }
  }

  /// <summary>Для работы с историей версии</summary>
  public static void VersionLCHistoryCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectId = (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
      int num = (int) new ObjectLCHistory(sessionKeeper.Session.GetObject(objectId), false).ShowDialog();
    }
  }

  /// <summary>Окно навигатора со списком версий объекта</summary>
  public static void ListVersions(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    TreeVersionsDescriptor descriptor = new TreeVersionsDescriptor(itemData.ObjectID, itemData.ID, itemData.ObjectType, itemData.Caption, DateTime.MaxValue);
    NavWindowBase.OverrideTreeViewClass = typeof (VersionsNavigatorTreeView);
    VersionsNavWindow versionsNavWindow = new VersionsNavWindow((IVersionsDescriptor) descriptor);
    versionsNavWindow.TreeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(Utils.GetNavigatorColumns);
    versionsNavWindow.Show(Holder.DockManager);
    versionsNavWindow.Activate();
  }

  /// <summary>Заглушка</summary>
  public static void Stub(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
  }

  /// <summary>Локализация выбранного объекта</summary>
  public static void LocalizationCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    LocalizationForm localizationForm = (LocalizationForm) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectId = (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
      if (localizationForm == null)
        localizationForm = new LocalizationForm();
      if (!(dbObject is IDBLocalizable dbLocalizable1))
        return;
      string languages = dbLocalizable1.Languages;
      if (localizationForm.ExecuteDialog(ref languages) != DialogResult.OK || !(dbObject is IDBLocalizable dbLocalizable2) || !(dbLocalizable2.Languages != languages))
        return;
      dbLocalizable2.Languages = languages;
    }
  }

  /// <summary>Назначение сиcтемного GUID</summary>
  public static void SetSystemGuid(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1223"), LocalizationHolder.rm.GetString("Client.Core_1222"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectId = (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(objectId);
      IGuidService customService = (IGuidService) sessionKeeper.Session.GetCustomService(typeof (IGuidService));
      string nameInMessages = dbObject1.NameInMessages;
      string empty = string.Empty;
      IDBObject dbObject2 = dbObject1.CheckoutBy != 0L ? dbObject1 : dbObject1.CheckOut(false);
      if (!SystemGUIDs.IsSystemGUID(dbObject1.GUID))
        dbObject1.GUID = customService.GenerateNextSystemGuid(2, nameInMessages, empty);
      if (!SystemGUIDs.IsSystemGUID(dbObject1.ObjectGUID))
        dbObject1.ObjectGUID = customService.GenerateNextSystemGuid(1, nameInMessages, empty);
      if (dbObject1.CheckoutBy != 0L)
        return;
      dbObject2.CheckIn();
    }
  }

  /// <summary>Создать в составе проекта на основе шаблона</summary>
  public static void BasedOnTemplate(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count != 1)
      return;
    long objectId1 = (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
    using (Intermech.Navigator.BasedOnTemplate basedOnTemplate = new Intermech.Navigator.BasedOnTemplate(objectId1))
    {
      if (basedOnTemplate.ShowDialog() != DialogResult.OK)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (KeyValuePair<int, List<CreatedProjectData>> addTemplateObject in (sessionKeeper.Session.GetObject(objectId1) as IDBProjectObject).AddTemplateObjects(basedOnTemplate.CreatedIDs, basedOnTemplate.TemplateID))
        {
          IntegratorObject iobj = IntegratorServices.Find(addTemplateObject.Key);
          if (iobj != null)
          {
            IIntegrator integrator = ClientContext.Integrators.GetIntegrator(iobj, false);
            if (integrator != null)
            {
              IEmbedAttributesService service = ServiceUtils.GetService<IEmbedAttributesService>((object) integrator, false);
              if (service != null)
              {
                foreach (CreatedProjectData createdProjectData in addTemplateObject.Value)
                {
                  long objectId2 = sessionKeeper.Session.CheckOutCommand(createdProjectData.ObjectID);
                  try
                  {
                    service.EmbedAttributeValues(objectId2, (IList<AttributeValues>) createdProjectData.AttributeValues);
                  }
                  finally
                  {
                    ObjectCopyCommand copyCommandByName = ObjectCommandFactory.CreateObjectCopyCommandByName("Checkin", true);
                    copyCommandByName.ObjectId = objectId2;
                    ServiceContainer serviceContainer = new ServiceContainer();
                    serviceContainer.AddService(typeof (ExtendedSaveOptions), (object) new ExtendedSaveOptions(SaveChangesMode.Checkin));
                    copyCommandByName.ContextServices = (System.IServiceProvider) serviceContainer;
                    copyCommandByName.Execute();
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  /// <summary>Создать итерацию.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CreateSnapshot(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SnapshotMasterForm snapshotMasterForm = new SnapshotMasterForm(items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID, nameof (CreateSnapshot)))
    {
      int num = (int) snapshotMasterForm.ShowDialog();
    }
  }

  /// <summary>Сохранить в итерацию.</summary>
  public static void SaveToSnapshot(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    using (SnapshotMasterForm snapshotMasterForm = new SnapshotMasterForm(items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID, nameof (SaveToSnapshot)))
    {
      int num = (int) snapshotMasterForm.ShowDialog();
    }
  }

  /// <summary>Объединить объекты</summary>
  public static void CombineObjectsCommand(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    List<long> objectIDs = new List<long>();
    List<MyElement> objectsInfo = new List<MyElement>();
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        objectIDs.Add(itemData.ObjectID);
        objectsInfo.Add(new MyElement((object) itemData.ObjectID, itemData.Caption, (object) null));
      }
    }
    using (ObjectToCombineInForm objectToCombineInForm = new ObjectToCombineInForm(objectsInfo))
    {
      if (objectToCombineInForm.ShowDialog() != DialogResult.OK)
        return;
      long objectToCombineInId = objectToCombineInForm.ObjectToCombineInID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
          return;
        objectIDs.Remove(objectToCombineInId);
        customService.CombineObjects(sessionKeeper.Session.SessionGUID, objectIDs.ToArray(), objectToCombineInId);
        if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
          return;
        service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs));
      }
    }
  }

  /// <summary>
  /// Восстановить объекты (из корзины).
  /// Восстановление в данном случае - перевод на шаг ЖЦ, который допускает схема ЖЦ.
  /// Если таких шагов несколько - предпочитать первый шаг ЖЦ.
  /// Если первого среди допустимых нет - тогда первый попавшийся шаг.
  /// </summary>
  /// <param name="items">The items.</param>
  /// <param name="viewservices">The viewservices.</param>
  /// <param name="additionalinfo">The additionalinfo.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public static void RestoreCommand(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    List<long> objectIDs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData1 = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        IDBLCStepID itemData2 = items.GetItemData(index, typeof (IDBLCStepID)) as IDBLCStepID;
        if (itemData1 != null && itemData2 != null)
        {
          objectIDs.Add(itemData1.ObjectID);
          IDBObject dbObject = sessionKeeper.Session.GetObject(itemData1.ObjectID);
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(itemData1.ObjectType);
          int[] nextSteps = sessionKeeper.Session.GetLifecycleStep(itemData2.LCStepID).GetNextSteps();
          int firstStep = sessionKeeper.Session.GetLCSchema(objectType.SchemaID).GetStepsCollection().GetFirstStep();
          dbObject.LCStep = !((IEnumerable<int>) nextSteps).Contains<int>(firstStep) ? nextSteps[0] : firstStep;
        }
      }
      if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
        return;
      service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs));
    }
  }

  public static void CreateVersionAnotherType(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    try
    {
      service.CreateVersionAnotherType(itemData.ObjectID, itemData.ObjectType);
    }
    finally
    {
      service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  /// <summary>
  /// Изменяет атрибут Гриф документа и переименовывает документ в соответствии с настройкой в параметрах ИПС
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  public static void ChangeDocumentsStamp(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      if (sk.Session.GetAttributeType(MetaDataHelper.GetAttributeID((object) "cadd9ac2-306c-11d8-b4e9-00304f19f545"), false) is IDBSecurity attributeType)
        attributeType.CheckAccess(ActionType.Write, true);
      int stampAttrValueIndex = ObjectCommands.GetStampAttrValueIndex();
      if (stampAttrValueIndex < 0)
        return;
      List<ObjInfoItem> docItems = new List<ObjInfoItem>();
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        docItems.Add(new ObjInfoItem(itemData.ObjectID, itemData.ObjectType));
      }
      Dictionary<long, List<long>> docsCompositionInfo = ObjectCommands.GetDocsCompositionInfo(docItems, sk);
      if (docsCompositionInfo.Count > 0)
      {
        DocLinksMonitorForm linksMonitorForm = new DocLinksMonitorForm();
        linksMonitorForm.Init(docsCompositionInfo);
        int num = (int) linksMonitorForm.ShowDialog();
        if (linksMonitorForm.DialogResult != DialogResult.OK)
          return;
      }
      string patternFromSettings = ObjectCommands.GetPatternFromSettings();
      List<string> attributesNamesInPattern = ObjectCommands.FindAttributesNamesInPattern(patternFromSettings);
      IDBTransactions customService = (IDBTransactions) sk.Session.GetCustomService(typeof (IDBTransactions));
      foreach (ObjInfoItem objInfoItem in docItems)
      {
        IDBObject docObj = sk.Session.GetObject(objInfoItem.ObjectID, false);
        if (docObj == null)
          break;
        try
        {
          customService.StartTransaction();
          try
          {
            ObjectCommands.SetAttrDocStampValue(docObj, stampAttrValueIndex);
            string pattern1 = patternFromSettings;
            string pattern2 = ObjectCommands.ReplaceAttrNamesWithValues(attributesNamesInPattern, docObj, pattern1);
            string newName = ObjectCommands.AddExtensionIfNeeded(attributesNamesInPattern, docObj, pattern2);
            ObjectCommands.RenameFirstFile(docObj, newName);
            customService.Commit();
          }
          catch
          {
            customService.Rollback();
            throw;
          }
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(new Exception($"Ошибка назначения грифа для документа {docObj.Caption} идентификационный номер {docObj.ObjectID}. ", ex));
        }
      }
    }
  }

  /// <summary>Переименовываем первый файл документа</summary>
  /// <param name="docObj">документ</param>
  /// <param name="newName">Имя для переименования</param>
  private static void RenameFirstFile(IDBObject docObj, string newName)
  {
    IDBAttribute attributeByGuid = docObj.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
    attributeByGuid.Index = 0;
    if (!(attributeByGuid is IBlobReader blobReader))
      return;
    BlobInformation blobInfo = blobReader.OpenBlob(-1);
    blobReader.CloseBlob();
    if (!(attributeByGuid is IBlobWriter blobWriter))
      return;
    blobInfo.FileName = newName;
    blobWriter.OpenBlob(blobInfo, true);
  }

  /// <summary>Назначаем атрибут Гриф документа</summary>
  /// <param name="docObj"></param>
  /// <param name="attrValueIndex"></param>
  private static void SetAttrDocStampValue(IDBObject docObj, int attrValueIndex)
  {
    docObj.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(MetaDataHelper.GetAttributeID((object) "cadd9ac2-306c-11d8-b4e9-00304f19f545"), (object) attrValueIndex)
    });
  }

  /// <summary>
  /// Получим индекс значения атрибута Гриф документа, которое надо назначить
  /// </summary>
  /// <returns></returns>
  private static int GetStampAttrValueIndex()
  {
    StampChangingForm stampChangingForm = new StampChangingForm();
    int num = (int) stampChangingForm.ShowDialog();
    return stampChangingForm.AttrValueIndex;
  }

  /// <summary>
  /// Собираем информацию о документах, для которых была вызвана команда.
  /// </summary>
  /// <param name="items"></param>
  /// <param name="sk"></param>
  /// <returns></returns>
  private static Dictionary<long, List<long>> GetDocsCompositionInfo(
    List<ObjInfoItem> docItems,
    SessionKeeper sk)
  {
    Dictionary<long, List<long>> docsCompositionInfo = new Dictionary<long, List<long>>();
    DBRecordSetParams dbRsp = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.ASC, 0)
    });
    DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) docItems, sk.Session, (IEnumerable<int>) new int[1]
    {
      MetaDataHelper.GetRelationTypeID("cad0057c-306c-11d8-b4e9-00304f19f545")
    }, 1, dbRsp, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545")));
    if (parentSostavData != null && parentSostavData.Rows.Count > 0)
    {
      long int64_1 = (long) parentSostavData.Rows[0]["F_PART_OBJ_ID"];
      List<long> collection = new List<long>();
      for (int index = 0; index < parentSostavData.Rows.Count; ++index)
      {
        int num;
        if (int64_1 == Convert.ToInt64(parentSostavData.Rows[index]["F_PART_OBJ_ID"]))
        {
          List<long> longList = collection;
          DataRow row = parentSostavData.Rows[index];
          num = -2;
          string columnName = num.ToString();
          long int64_2 = Convert.ToInt64(row[columnName]);
          longList.Add(int64_2);
        }
        else
        {
          DataRow row1 = parentSostavData.Rows[index];
          num = -50;
          string columnName1 = num.ToString();
          string str = (string) row1[columnName1];
          DataRow row2 = parentSostavData.Rows[index];
          num = -7;
          string columnName2 = num.ToString();
          Convert.ToInt32(row2[columnName2]);
          docsCompositionInfo.Add(int64_1, new List<long>((IEnumerable<long>) collection));
          collection.Clear();
          int64_1 = Convert.ToInt64(parentSostavData.Rows[index]["F_PART_OBJ_ID"]);
          List<long> longList = collection;
          DataRow row3 = parentSostavData.Rows[index];
          num = -2;
          string columnName3 = num.ToString();
          long int64_3 = Convert.ToInt64(row3[columnName3]);
          longList.Add(int64_3);
        }
        if (index == parentSostavData.Rows.Count - 1)
          docsCompositionInfo.Add(int64_1, collection);
      }
    }
    return docsCompositionInfo;
  }

  /// <summary>Получить шаблон из настроек</summary>
  /// <param name="sk"></param>
  /// <returns></returns>
  private static string GetPatternFromSettings()
  {
    return (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadString("CLIENT", "AUTHFILES", "FILENAMEWITHSECRECYSTAMP", "<Гриф документа> <Наименование>.<Расширение файла>", DBConfigMode.GlobalOnly);
  }

  /// <summary>Заменить в шаблоне имена атрибутов на их значения</summary>
  /// <param name="attrNames"></param>
  /// <param name="docObj"></param>
  /// <param name="pattern"></param>
  /// <returns></returns>
  private static string ReplaceAttrNamesWithValues(
    List<string> attrNames,
    IDBObject docObj,
    string pattern)
  {
    foreach (string attrName in attrNames)
    {
      if (attrName == "Расширение файла")
      {
        string newValue = ObjectCommands.ReadFileExtension(docObj);
        string oldValue = "<Расширение файла>";
        pattern = pattern.Replace(oldValue, newValue);
      }
      else
      {
        IDBAttribute attributeByName = docObj.GetAttributeByName(attrName, false);
        if (attributeByName != null)
        {
          string description = attributeByName.Description;
          string oldValue = $"<{attrName}>";
          pattern = pattern.Replace(oldValue, description);
        }
      }
    }
    return pattern;
  }

  private static string AddExtensionIfNeeded(
    List<string> attrNames,
    IDBObject docObj,
    string pattern)
  {
    if (!attrNames.Contains("Расширение файла"))
    {
      string str = ObjectCommands.ReadFileExtension(docObj);
      pattern = $"{pattern}.{str}";
    }
    return pattern;
  }

  /// <summary>Зачитаем расширение первого файла документа</summary>
  /// <param name="docObj"></param>
  /// <returns></returns>
  private static string ReadFileExtension(IDBObject docObj)
  {
    string str = string.Empty;
    IDBAttribute attributeByGuid = docObj.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid != null)
    {
      string description = attributeByGuid.Description;
      str = description.Substring(description.LastIndexOf('.') + 1);
    }
    return str;
  }

  /// <summary>Находим наименования атрибутов в строке шаблона</summary>
  /// <param name="pattern">Строка шаблона</param>
  /// <returns>Список имен атрибутов</returns>
  private static List<string> FindAttributesNamesInPattern(string pattern)
  {
    List<string> attributesNamesInPattern = new List<string>();
    string str1 = new string(pattern.ToCharArray());
    while (true)
    {
      int num1 = str1.IndexOf('<');
      int num2 = str1.IndexOf('>');
      if (num1 != -1 && num2 != -1)
      {
        string str2 = str1.Substring(num1 + 1, num2 - num1 - 1);
        attributesNamesInPattern.Add(str2);
        if (num2 + 1 < str1.Length)
          str1 = str1.Substring(num2 + 1);
        else
          break;
      }
      else
        break;
    }
    return attributesNamesInPattern;
  }

  internal static void CreateLinkedPrototypeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    try
    {
      long aTemplateObjectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
      IDBRelationID itemData = items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
      long byTemplateDialog = service.CreateObjectByTemplateDialog(aTemplateObjectID, new ObjectRelationLink[1]
      {
        new ObjectRelationLink(itemData.ProjID, itemData.RelationType, itemData.Value)
      });
      if (byTemplateDialog == -1L)
        return;
      AfterObjectCreatorDialogHandlers.Handle(byTemplateDialog, 0, items, viewServices, additionalInfo);
      ObjectCommands.OnCreateEntersInRelation(itemData.ProjID, byTemplateDialog);
    }
    finally
    {
      service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(ObjectCommands.OnCreateNewObject);
    }
  }

  private static void OnCreateEntersInRelation(
    long projectID,
    long newObjectID,
    int projectObjectTypeID = -1)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBRelation relation = session.GetRelation(projectID, newObjectID, true);
      if (projectObjectTypeID == -1)
        projectObjectTypeID = session.GetObjectInfo(projectID).ObjectTypeID;
      if (relation == null)
        return;
      DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsCreated", relation.RelationID, relation.ProjID, projectObjectTypeID, relation.RelationType, NavigatorRelationCommand.CreateIn);
      Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
    }
  }

  public static void CompareAuthFiles(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    (ServiceUtils.GetService<ICompareFilesService>((object) ApplicationServices.Container, false) ?? throw new KernelException(LocalizationHolder.rm.GetString("Client.Core_1700"))).CompareTwoObjectsFiles(items, FileTypes.ftAuthentical);
  }

  public static void CompareAuthFilesVersions(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    long versionForCompareId = VersionComparison.GetVersionForCompareId(viewServices, itemData);
    if (versionForCompareId == 0L)
      return;
    ISelectedItems items1;
    if (versionForCompareId == Math.Abs(itemData.Value))
      items1 = Intermech.Navigator.ContextMenu.Services.GetItems(itemData.Value);
    else
      items1 = Intermech.Navigator.ContextMenu.Services.GetItems(itemData.Value, versionForCompareId);
    ObjectCommands.CompareAuthFiles(items1, viewServices, additionalInfo);
  }

  /// <summary>Команда сравнения файлов для версий объекта</summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  public static void CompareFilesVersions(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (!(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    long versionForCompareId = VersionComparison.GetVersionForCompareId(viewservices, itemData);
    if (versionForCompareId == 0L)
      return;
    ISelectedItems items1;
    if (versionForCompareId == Math.Abs(itemData.Value))
      items1 = Intermech.Navigator.ContextMenu.Services.GetItems(itemData.Value);
    else
      items1 = Intermech.Navigator.ContextMenu.Services.GetItems(itemData.Value, versionForCompareId);
    ObjectCommands.CompareFiles(items1, viewservices, additionalinfo);
  }

  /// <summary>
  /// Команда сравнения файлов для двух документов
  /// (по тз - одинакового типа)
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  public static void CompareFiles(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    (ServiceUtils.GetService<ICompareFilesService>((object) ApplicationServices.Container, false) ?? throw new KernelException(LocalizationHolder.rm.GetString("Client.Core_1700"))).CompareTwoObjectsFiles(items, FileTypes.ftNormal);
  }

  /// <summary>Команда Активировать проект</summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  public static void ActivateProject(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (!(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObject(itemData.Value, false) is IDBProjectObject dbProjectObject))
        return;
      if (!dbProjectObject.IsProjectParticipant(sessionKeeper.Session.UserID))
      {
        int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1706"), LocalizationHolder.rm.GetString("Client.Core_281"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        int securityLevel = sessionKeeper.Session.SecurityLevel;
        IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(itemData.Value, new Guid("cad00816-306c-11d8-b4e9-00304f19f545"));
        if (objectAttributeByGuid == null)
          return;
        if ((long) securityLevel != objectAttributeByGuid.AsInteger)
        {
          int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1707"), LocalizationHolder.rm.GetString("Client.Core_281"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service))
            throw new KernelException("Не найден ICurrentUserAndRole сервис.");
          service.SetCurrentProject(itemData.Value, service.CachedProjectFiltrationMode);
        }
      }
    }
  }
}
