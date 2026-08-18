// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.DocumentCommandsProvider
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.ECO;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Провайдер команд для документов</summary>
public class DocumentCommandsProvider : ICommandsProvider
{
  /// <summary>
  /// Метод вызывается для получения допустимых и подавляемых команд контекстного меню для
  /// выделенных элементов навигации одной категории и типа.
  /// Например, если в «Навигаторе» выделены элементы навигации нескольких разных категорий и типов,
  /// то данная команда будет вызываться для каждой из подгрупп этих элементов, сгруппированных
  /// по их категориям и типам. Наиболее применяемый метод даного интерфейса.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми мо
  /// гут пользоваться команды.</param>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0 || MetaDataHelper.GetAttributeType(MetaDataHelper.GetAttributeTypeID(ConstsHolder.InventoryNumberGuid)) == null)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("InventoryNumber", new CommandInfo(0, new ClickEventHandler(DocumentCommandsProvider.SetInventoryNumber)));
    mergedCommands.Add("DeleteInventoryNumber", new CommandInfo(0, new ClickEventHandler(DocumentCommandsProvider.DeleteInventoryNumber)));
    mergedCommands.Add("CopyDeliveryListFromDoc", new CommandInfo(0, new ClickEventHandler(this.CopyDeliveryListFromDoc)));
    mergedCommands.Add("AddSubscriber", new CommandInfo(0, new ClickEventHandler(DocumentCommandsProvider.AddSubscriber)));
    mergedCommands.Add("CreateCopiesByDeliveryList", new CommandInfo(0, new ClickEventHandler(this.CreateCopiesByDeliveryList)));
    mergedCommands.Add("ChangeCopiesByDeliveryList", new CommandInfo(0, new ClickEventHandler(this.ChangeCopiesByDeliveryList)));
    if (items.Count == 1)
    {
      if (ConstsHolder.CehRouteID != -1)
        mergedCommands.Add("AddSubscribersByRoute", new CommandInfo(0, new ClickEventHandler(this.AddSubscribersByRoute)));
      if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && MetaDataHelper.GetObjectTypeParentID(itemData.ObjectType) == MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"))
        mergedCommands.Add("CopyDeliveryListFromECOToDoc", new CommandInfo(0, new ClickEventHandler(this.CopyDeliveryListFromECOToDoc)));
    }
    return mergedCommands;
  }

  /// <summary>
  /// Метод вызывается для получения допустимых и подавляемых команд контекстного меню для всей группы выделенных
  /// элементов навигации. Особенности данного метода:
  /// 1. Если команда зарегистрирована на все категории, то метод вызывается один раз и получает в качестве параметра
  /// items все выделенные в «Навигаторе» элементы навигации;
  /// 2. Если команда зарегистрирована на конкретную категорию, то метод будет вызван один раз для всех выделенных
  /// элементов навигации только в том случае, если все они принадлежат одной категории; для всех выделенных
  /// элементов навигации только в том случае, если все они принадлежат указанной категории;
  /// 3. Если команда зарегистрирована на конкретные категорию и тип, то метод будет вызван один раз для всех
  /// выделенных элементов навигации только в том случае, если все они принадлежат указанной категории и типу.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>
  /// Снять выделенные документы с регистрации.
  /// Метод удаляет значения в атрибутах "Дата регистрации в ОТД", "Зарегистрировал в ОТД, "Инвентарный номер (ОТД)"
  /// </summary>
  /// <param name="items">Итемы</param>
  /// <param name="viewservices">Сервисы</param>
  /// <param name="additionalinfo">Доп. инфо</param>
  private static void DeleteInventoryNumber(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    string empty = string.Empty;
    if (MessageBox.Show(items.Count != 1 ? ServiceHolder.rm.GetString("Archives_206") : ServiceHolder.rm.GetString("Archives_205"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
        throw new KernelException("Не найден сервис ICopiesService");
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        {
          if (customService.DocumentHasCopies(itemData.ObjectID, (object) sessionKeeper.Session.SessionGUID))
          {
            int num = (int) MessageBox.Show($"Документ {itemData.Caption} не может быть снят с регистрации, т.к. у него есть копии.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          else
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID);
            AttributeValues attributeValues = new AttributeValues(ConstsHolder.InventoryNumberID, (object) null);
            dbObject.SetAttributesValues(new AttributeValues[1]
            {
              attributeValues
            });
            if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
            {
              DBObjectsExtendedEventArgs e = new DBObjectsExtendedEventArgs("ObjectsChanged", dbObject.ObjectID, dbObject.ObjectType, new AttributeValues[0], new AttributeValues[1]
              {
                attributeValues
              });
              service.FireEvent((object) null, (NotificationEventArgs) e);
            }
          }
        }
      }
    }
  }

  /// <summary>Зарегистрировать выбранные объекты в ОТД</summary>
  /// <param name="items">Итемы</param>
  /// <param name="viewServices">Сервисы</param>
  /// <param name="additionalInfo">Доп. инфо</param>
  public static void SetInventoryNumber(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    List<IDBTypedObjectID> dbTypedObjectIdList1 = new List<IDBTypedObjectID>();
    bool flag1 = false;
    bool flag2 = false;
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        dbTypedObjectIdList1.Add(itemData);
    }
    List<(long, string, long, string)> valueTupleList1 = new List<(long, string, long, string)>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
        throw new KernelException("Не найден сервис ICopiesService");
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      List<long> Ids = dbTypedObjectIdList1.Select<IDBTypedObjectID, long>((System.Func<IDBTypedObjectID, long>) (x => x.ObjectID)).AsList<long>();
      valueTupleList1 = customService.GetObjectsParentsInventoryNumbers(sessionGuid, Ids);
    }
    List<(long, string)> valueTupleList2 = new List<(long, string)>();
    foreach ((_, _, _, _) in valueTupleList1)
    {
      (long, string, long, string) parentsInfo;
      if (parentsInfo.Item3 != 0L && parentsInfo.Item4 != null && !string.IsNullOrEmpty(parentsInfo.Item4))
      {
        DialogResult dialogResult = DialogResult.None;
        if (!flag1 && !flag2)
        {
          using (ReplaceNumberForm replaceNumberForm = new ReplaceNumberForm(parentsInfo.Item2, parentsInfo.Item4))
          {
            dialogResult = replaceNumberForm.ShowDialog();
            flag1 = dialogResult == DialogResult.OK;
            flag2 = dialogResult == DialogResult.Ignore;
          }
        }
        if (!flag1)
        {
          switch (dialogResult)
          {
            case DialogResult.Cancel:
              return;
            case DialogResult.Yes:
              break;
            default:
              continue;
          }
        }
        valueTupleList2.Add((parentsInfo.Item1, parentsInfo.Item4));
        dbTypedObjectIdList1.Remove(dbTypedObjectIdList1.First<IDBTypedObjectID>((System.Func<IDBTypedObjectID, bool>) (x => x.ObjectID == parentsInfo.Item1)));
      }
    }
    using (SessionKeeper sk = new SessionKeeper())
    {
      foreach ((long, string) valueTuple in valueTupleList2)
      {
        AttributeValues attributeValues = new AttributeValues(ConstsHolder.InventoryNumberID, (object) valueTuple.Item2);
        sk.Session.SetObjectAttributesValues(valueTuple.Item1, false, new AttributeValues[1]
        {
          attributeValues
        });
        if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
        {
          DBObjectsExtendedEventArgs e = new DBObjectsExtendedEventArgs("ObjectsChanged", valueTuple.Item1, -1, new AttributeValues[0], new AttributeValues[1]
          {
            attributeValues
          });
          service.FireEvent((object) null, (NotificationEventArgs) e);
        }
      }
      dbTypedObjectIdList1 = DocumentCommandsProvider.GetAvailableItemsForRegistration(dbTypedObjectIdList1, sk);
    }
    if (dbTypedObjectIdList1.Count <= 0)
      return;
    List<IDBTypedObjectID> items1 = new List<IDBTypedObjectID>();
    List<IDBTypedObjectID> dbTypedObjectIdList2 = new List<IDBTypedObjectID>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) dbTypedObjectIdList1.Select<IDBTypedObjectID, long>((System.Func<IDBTypedObjectID, long>) (x => x.ObjectID)).AsArray<long>(), LogicalOperators.NONE, 0, false)
      }, new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ConstsHolder.InventoryNumberID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      })
      {
        Tags = new HybridDictionary()
      };
      dbRecordSetParams.Tags[(object) "ShowAllModifications"] = (object) true;
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.ObjectsSelect(-1, dbRecordSetParams).Rows)
      {
        long objId = Convert.ToInt64(row[0]);
        if (row[1].ToString() == string.Empty)
          items1.Add(dbTypedObjectIdList1.First<IDBTypedObjectID>((System.Func<IDBTypedObjectID, bool>) (x => x.ObjectID == objId)));
        else
          dbTypedObjectIdList2.Add(dbTypedObjectIdList1.First<IDBTypedObjectID>((System.Func<IDBTypedObjectID, bool>) (x => x.ObjectID == objId)));
      }
    }
    if (dbTypedObjectIdList2.Count > 0)
    {
      if (dbTypedObjectIdList2.Count == 1)
      {
        if (items.Count == 1)
        {
          int num = (int) IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), string.Format(ServiceHolder.rm.GetString("Archives_162"), (object) dbTypedObjectIdList2[0].Caption), MessageBoxButtonsAdv.OK, IMMessageBoxImage.Warning);
          return;
        }
        if (IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), string.Format(ServiceHolder.rm.GetString("Archives_127"), (object) dbTypedObjectIdList2[0].Caption), MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Warning) == DialogResultAdv.Cancel)
          return;
      }
      else
      {
        foreach (IDBTypedObjectID dbTypedObjectId in dbTypedObjectIdList2)
        {
          if (IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), string.Format(ServiceHolder.rm.GetString("Archives_127"), (object) dbTypedObjectId.Caption), MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Warning) == DialogResultAdv.Cancel)
            return;
        }
      }
    }
    if (items1.Count == 0)
      return;
    DialogResult dialogResult1 = new InventoryNumberForm(items1).ShowDialog();
    if (items1.Count <= 1 || dialogResult1 != DialogResult.Retry)
      return;
    foreach (IDBTypedObjectID dbTypedObjectId in items1)
    {
      InventoryNumberPerItem inventoryNumberPerItem = new InventoryNumberPerItem();
      inventoryNumberPerItem.Init(dbTypedObjectId);
      int num = (int) inventoryNumberPerItem.ShowDialog();
    }
  }

  /// <summary>
  /// Получает документы, которые пользователь имеет право зарегистрировать в отд.
  /// </summary>
  /// <param name="workItems">Документы для регистрации.</param>
  /// <param name="sk">Сессия.</param>
  /// <returns></returns>
  private static List<IDBTypedObjectID> GetAvailableItemsForRegistration(
    List<IDBTypedObjectID> workItems,
    SessionKeeper sk)
  {
    List<IDBTypedObjectID> itemsForRegistration = new List<IDBTypedObjectID>();
    foreach (IDBTypedObjectID workItem in workItems)
    {
      try
      {
        DocumentCommandsProvider.CheckUsersRightsForDocRegistration(sk, workItem.ObjectID);
        itemsForRegistration.Add(workItem);
      }
      catch (AccessDeniedException ex)
      {
        AccessDeniedExceptionForm.OnExceptionHandler((object) null, new ExceptionEventArgs((Exception) ex));
      }
    }
    return itemsForRegistration;
  }

  /// <summary>
  /// Добавляет абонентов в лист рассылки по расцеховке изделия.
  /// </summary>
  /// <param name="items">Итемы</param>
  /// <param name="viewServices">Сервисы</param>
  /// <param name="additionalInfo">Доп. инфо</param>
  private void AddSubscribersByRoute(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    long cehRouteID = 0;
    Dictionary<int, List<long>> productsTypedIds = DocumentCommandsProvider.GetProductsTypedIDs(itemData.ObjectID);
    if (productsTypedIds.Count > 0)
    {
      cehRouteID = DocumentCommandsProvider.CehRouteChoice(productsTypedIds);
    }
    else
    {
      if (!MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, new Guid("cad00185-306c-11d8-b4e9-00304f19f545")))
      {
        int num = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_172"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return;
      }
      List<long> routesIds = DocumentCommandsProvider.GetRoutesIDs(itemData.ObjectID);
      ObjectsSelectionOptionsHolder serviceInstance = new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.LocalTypesMode);
      AdvancedServiceContainer nodesContext = new AdvancedServiceContainer();
      nodesContext.AddService(typeof (ObjectsSelectionOptionsHolder), (object) serviceInstance);
      SelectObjectsDescriptor rootDescriptor = new SelectObjectsDescriptor(ServiceHolder.rm.GetString("Archives_207"), routesIds);
      object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_209"), ServiceHolder.rm.GetString("Archives_208"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), (System.IServiceProvider) nodesContext, SelectionOptions.HideTree | SelectionOptions.HideViewsGroupingBox | SelectionOptions.SelectObjects | SelectionOptions.DisableObjectListFilter | SelectionOptions.DisableMultiselect | SelectionOptions.ForceRebuildNavTree);
      if (objArray != null && objArray.Length != 0 && objArray[0] is IDBTypedObjectID dbTypedObjectId)
        cehRouteID = dbTypedObjectId.ObjectID;
    }
    using (SessionKeeper sk = new SessionKeeper())
    {
      if (cehRouteID == 0L || !(sk.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
        return;
      long num1 = customService.GetDeliveryListID(sk.Session.SessionGUID, itemData.ID);
      if (num1 == 0L)
        num1 = customService.CreateDeliveryList(sk.Session.SessionGUID, itemData.ObjectID);
      if (service != null)
      {
        DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", num1, ConstsHolder.DeliveryListID);
        service.FireEvent((object) null, (NotificationEventArgs) e);
      }
      if (num1 == 0L)
      {
        int num2 = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_167"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        Dictionary<long, int> subscribersAndCopiesNumber = DocumentCommandsProvider.GetNewSubscribersAndCopiesNumber(sk, num1, cehRouteID, customService, itemData);
        this.AddNewSubscribersToDeliveryList(num1, subscribersAndCopiesNumber, sk, service);
      }
    }
  }

  /// <summary>Добавить абонентов в лист рассылки</summary>
  /// <param name="deliveryListId">Лист рассылки</param>
  /// <param name="newSubscribers">Словарь абонентов/количества копий</param>
  /// <param name="sk">Сессия</param>
  /// <param name="notificationService">Сервис уведомлений</param>
  private void AddNewSubscribersToDeliveryList(
    long deliveryListId,
    Dictionary<long, int> newSubscribers,
    SessionKeeper sk,
    INotificationService notificationService)
  {
    long num = ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service ? service.UserID : throw new KernelException("Не найден сервис ICurrentUserAndRole.");
    int[] attributesID = new int[6]
    {
      ConstsHolder.SubscribersID,
      ConstsHolder.NumberOfCopiesID,
      ConstsHolder.ListOwnerID,
      ConstsHolder.SubscribersDateID,
      ConstsHolder.ActualCopyID,
      ConstsHolder.NotesForSubscribersID
    };
    AttributeValues[] attributesValues = sk.Session.GetObjectAttributesValues(deliveryListId, attributesID, GetAttributeValuesModes.None, false);
    List<object> objectList1 = new List<object>();
    List<object> objectList2 = new List<object>();
    List<object> objectList3 = new List<object>();
    List<object> objectList4 = new List<object>();
    List<object> objectList5 = new List<object>();
    List<object> objectList6 = new List<object>();
    if (attributesValues[0].Values.Length >= 1 && attributesValues[0].Value != DBNull.Value && attributesValues[0].Value != null)
    {
      objectList1.AddRange((IEnumerable<object>) attributesValues[0].Values);
      objectList2.AddRange((IEnumerable<object>) attributesValues[1].Values);
      objectList4.AddRange((IEnumerable<object>) attributesValues[2].Values);
      objectList3.AddRange((IEnumerable<object>) attributesValues[3].Values);
      objectList5.AddRange((IEnumerable<object>) attributesValues[4].Values);
    }
    foreach (KeyValuePair<long, int> newSubscriber in newSubscribers)
    {
      objectList1.Add((object) newSubscriber.Key);
      objectList2.Add((object) newSubscriber.Value);
      objectList4.Add((object) num);
      objectList3.Add((object) DateTime.Now);
      objectList5.Add((object) 0L);
    }
    AttributeValues[] attributeValuesArray = new AttributeValues[5]
    {
      new AttributeValues(ConstsHolder.SubscribersID, (object) objectList1.ToArray()),
      new AttributeValues(ConstsHolder.NumberOfCopiesID, (object) objectList2.ToArray()),
      new AttributeValues(ConstsHolder.ListOwnerID, (object) objectList4.ToArray()),
      new AttributeValues(ConstsHolder.SubscribersDateID, (object) objectList3.ToArray()),
      new AttributeValues(ConstsHolder.ActualCopyID, (object) objectList5.ToArray())
    };
    sk.Session.SetObjectAttributesValues(deliveryListId, false, attributeValuesArray);
    notificationService?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", deliveryListId, ConstsHolder.DeliveryListID, attributesValues, attributeValuesArray));
  }

  /// <summary>
  /// Получить словарь с новыми абонентами для добавления в ЛР и количеством копий для них.
  /// </summary>
  /// <param name="deliveryListId">ИД листа рассылки</param>
  /// <param name="cehRouteID">ИД расцеховочного маршрута</param>
  /// <param name="copiesService">Сервис копий</param>
  /// <param name="currTypedObjectID">Выделенный документ</param>
  /// <returns>Словарь с ИД абонентов и количеством копий</returns>
  private static Dictionary<long, int> GetNewSubscribersAndCopiesNumber(
    SessionKeeper sk,
    long deliveryListId,
    long cehRouteID,
    ICopiesService copiesService,
    IDBTypedObjectID currTypedObjectID)
  {
    IDBAttribute objectAttributeById = sk.Session.GetObjectAttributeByID(deliveryListId, ConstsHolder.SubscribersID);
    List<long> second = new List<long>();
    for (int index = 0; index < objectAttributeById.ValuesCount; ++index)
    {
      if (objectAttributeById.Values[index] != DBNull.Value)
      {
        long int64 = Convert.ToInt64(objectAttributeById.Values[index]);
        second.Add(int64);
      }
    }
    List<long> list = DocumentCommandsProvider.GetUniqueСehIDs(DocumentCommandsProvider.GetSendCopiesByCehRouteAttrTable(sk, cehRouteID)).Except<long>((IEnumerable<long>) second).ToList<long>();
    Dictionary<long, int> subscribers = copiesService.GetSubscribers(currTypedObjectID.ObjectType);
    Dictionary<long, int> subscribersAndCopiesNumber = new Dictionary<long, int>();
    foreach (long key in list)
    {
      int num = 1;
      if (subscribers.ContainsKey(key))
        subscribers.TryGetValue(key, out num);
      subscribersAndCopiesNumber.Add(key, num);
    }
    return subscribersAndCopiesNumber;
  }

  /// <summary>Выбор расцеховочного маршрута</summary>
  /// <param name="typedIDs">Типизированный словарь объектов, из маршрутов которых надо произвести выбор</param>
  /// <returns>ИД расцеховочного маршрута. Intermech.Consts.UnknownObjectID - если не выбрано</returns>
  private static long CehRouteChoice(Dictionary<int, List<long>> typedIDs)
  {
    using (CehRouteChoiceForm cehRouteChoiceForm = new CehRouteChoiceForm(typedIDs))
    {
      int num = (int) cehRouteChoiceForm.ShowDialog();
      return cehRouteChoiceForm.CehRouteID;
    }
  }

  /// <summary>
  /// Получает типизированный словарь объектов, в которые входит документ связью "Документация на изделие"
  /// </summary>
  /// <param name="docObjectID">ID версии документа</param>
  /// <returns></returns>
  private static Dictionary<int, List<long>> GetProductsTypedIDs(long docObjectID)
  {
    Dictionary<int, List<long>> productsTypedIds = new Dictionary<int, List<long>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"), string.Empty).EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
      }), docObjectID).Rows)
      {
        int parentObjectTypeId = MetaDataHelper.GetTopParentObjectTypeID(Convert.ToInt32(row[-7.ToString()]));
        long int64 = Convert.ToInt64(row[-2.ToString()]);
        List<long> longList = (List<long>) null;
        if (!productsTypedIds.TryGetValue(parentObjectTypeId, out longList))
        {
          longList = new List<long>();
          productsTypedIds.Add(parentObjectTypeId, longList);
        }
        longList.Add(int64);
      }
    }
    return productsTypedIds;
  }

  /// <summary>
  /// Список маршрутов обработки, в которые входит техпроцесс
  /// </summary>
  /// <param name="techProcObjectID">ID версии техпроцесса</param>
  /// <returns></returns>
  private static List<long> GetRoutesIDs(long techProcObjectID)
  {
    List<long> collection = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"), string.Empty);
      relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545");
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
      });
      DataTable dataTable = relationCollection.EntersInVersion(paramSet, techProcObjectID);
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[-2.ToString()]);
          collection.SafeAdd<long>(int64);
        }
      }
    }
    return collection;
  }

  /// <summary>
  /// Получить ИД уникальных подразделений, на которые ссылаются расцеховочные элементы
  /// </summary>
  /// <param name="table">Таблица расцеховочных элементов и их атрибутов "Рассылка копий по РМ"</param>
  private static List<long> GetUniqueСehIDs(DataTable table)
  {
    List<long> uniqueСehIds = new List<long>();
    if (table == null)
      return uniqueСehIds;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      object obj = row[ConstsHolder.SendCopyByCehRouteAttrID.ToString()];
      switch (obj)
      {
        case null:
        case DBNull _:
          string str = Convert.ToString(row[-50.ToString()]);
          int num = (int) MessageBox.Show(string.Format(ServiceHolder.rm.GetString("Archives_173"), (object) str), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          continue;
        default:
          long int64 = Convert.ToInt64(obj);
          if (!uniqueСehIds.Contains(int64))
          {
            uniqueСehIds.Add(int64);
            continue;
          }
          continue;
      }
    }
    return uniqueСehIds;
  }

  /// <summary>
  /// Получает таблицу аргументов "Рассылка копий по РМ" для расцеховочных элементов расцеховочного маршрута
  /// </summary>
  /// <param name="cehRouteID">ID расцеховочного элементе.</param>
  private static DataTable GetSendCopiesByCehRouteAttrTable(SessionKeeper sk, long cehRouteID)
  {
    DBRecordSetParams dbRsp = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ConstsHolder.SendCopyByCehRouteAttrID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    return DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      new ObjInfoItem(cehRouteID)
    }, sk.Session, (IEnumerable<int>) new int[1]
    {
      MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545")
    }, -1, dbRsp, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) new List<int>()
    {
      ConstsHolder.ElemRouteID
    });
  }

  /// <summary>Добавить абонентов для выбранных документов</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void AddSubscriber(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    List<long> itemsDeliveryLists1 = DocumentCommandsProvider.GetItemsDeliveryLists(items, false);
    if (!DocumentCommandsProvider.CreateMissingDeliveryLists(items, itemsDeliveryLists1))
      return;
    List<long> itemsDeliveryLists2 = DocumentCommandsProvider.GetItemsDeliveryLists(items, true);
    if (itemsDeliveryLists2.Count == 0)
    {
      int num1 = (int) IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_124"), ServiceHolder.rm.GetString("Archives_125"), MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
    }
    else
    {
      using (AddSubscriberForm addSubscriberForm = new AddSubscriberForm(itemsDeliveryLists2, false))
      {
        int num2 = (int) addSubscriberForm.ShowDialog();
      }
    }
  }

  /// <summary>
  /// Получает список листов рассылки выделенных документов.
  /// </summary>
  /// <param name="items">Выделенные документы</param>
  /// <param name="getExistedListsOnly">Определяет, указывать ли в списке только существующие листы рассылки, без повторов.
  /// False - список содержит значения ИД для каждого итема, включая Intermech.Consts.UnknownObjectId (если листа рассылки нет)
  /// и повторы ИД листа рассылки (если в итемах есть версии одного документа).
  /// </param>
  /// <returns>Список ID листов рассылки выделенных документов.</returns>
  private static List<long> GetItemsDeliveryLists(ISelectedItems items, bool getExistedListsOnly)
  {
    List<long> itemsDeliveryLists = new List<long>();
    if (ConstsHolder.DeliveryListID == 0 || ConstsHolder.OriginalObjectID == -10000)
      return itemsDeliveryLists;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICopiesService customService = sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && customService != null)
        {
          long deliveryListId = customService.GetDeliveryListID(sessionKeeper.Session.SessionGUID, itemData.ID);
          if (getExistedListsOnly)
          {
            if (deliveryListId != 0L && !itemsDeliveryLists.Contains(deliveryListId))
              itemsDeliveryLists.Add(deliveryListId);
          }
          else
            itemsDeliveryLists.Add(deliveryListId);
        }
      }
    }
    return itemsDeliveryLists;
  }

  /// <summary>Скопировать лист рассылки у документа.</summary>
  /// <param name="items">Итемы.</param>
  /// <param name="viewservices">Сервисы.</param>
  /// <param name="additionalinfo">Доп. инфо.</param>
  private void CopyDeliveryListFromDoc(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null || items.Count == 0)
      return;
    List<long> itemsDeliveryLists1 = DocumentCommandsProvider.GetItemsDeliveryLists(items, false);
    if (!DocumentCommandsProvider.CreateMissingDeliveryLists(items, itemsDeliveryLists1))
      return;
    List<long> itemsDeliveryLists2 = DocumentCommandsProvider.GetItemsDeliveryLists(items, true);
    long copiedDeliveryListId = DocumentCommandsProvider.GetCopiedDeliveryListID();
    switch (copiedDeliveryListId)
    {
      case -1:
        break;
      case 0:
        int num = (int) IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), ServiceHolder.rm.GetString("Archives_175"), MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        break;
      default:
        if (!(ServicesManager.GetService(typeof (ICopiesClientService)) is ICopiesClientService service))
          break;
        service.CopyDeliveryList(copiedDeliveryListId, itemsDeliveryLists2);
        break;
    }
  }

  /// <summary>
  /// Создает листы рассылки для тех документов, у которых их нет.
  /// </summary>
  /// <param name="items">The items.</param>
  /// <param name="deliveryLists">Список листов рассылки, соответствующий выделенным итемам.
  /// Для отсутствующего лр в списке используется Intermech.Consts.UnknownObjectId</param>
  /// <returns>True - если пользователь согласен создать лр. False - если не согласен</returns>
  private static bool CreateMissingDeliveryLists(ISelectedItems items, List<long> deliveryLists)
  {
    List<long> longList1 = new List<long>();
    string str = string.Empty;
    for (int index = 0; index < items.Count; ++index)
    {
      if (deliveryLists[index] == 0L && items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        longList1.Add(itemData.ObjectID);
        str = !string.IsNullOrEmpty(str) ? $"{str}, {itemData.Caption}" : str + itemData.Caption;
      }
    }
    if (longList1.Count == 1)
    {
      if (MessageBox.Show(string.Format(ServiceHolder.rm.GetString("Archives_169"), (object) str), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        return false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService)
        {
          long deliveryList = customService.CreateDeliveryList(sessionKeeper.Session.SessionGUID, longList1[0]);
          if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          {
            DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", deliveryList, ConstsHolder.DeliveryListID);
            service.FireEvent((object) null, (NotificationEventArgs) e);
          }
        }
      }
    }
    if (longList1.Count > 1)
    {
      if (MessageBox.Show(string.Format(ServiceHolder.rm.GetString("Archives_170"), (object) str), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        return false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService)
        {
          List<long> longList2 = new List<long>();
          List<long> longList3 = new List<long>();
          IObjectsInfoCache service1 = ApplicationServices.Container.GetService<IObjectsInfoCache>();
          foreach (long objectID in longList1)
          {
            QuickObjectInfo objectInfo = service1.GetObjectInfo(objectID);
            if (!longList3.Contains(objectInfo.ID))
            {
              longList2.Add(objectInfo.ObjectID);
              longList3.Add(objectInfo.ID);
            }
          }
          List<long> objectIDs = new List<long>();
          List<int> objectTypeIDs = new List<int>();
          foreach (long objectID in longList2)
          {
            long deliveryList = customService.CreateDeliveryList(sessionKeeper.Session.SessionGUID, objectID);
            objectIDs.Add(deliveryList);
            objectTypeIDs.Add(ConstsHolder.DeliveryListID);
          }
          if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2)
          {
            DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", (IList<long>) objectIDs, (IList<int>) objectTypeIDs);
            service2.FireEvent((object) null, (NotificationEventArgs) e);
          }
        }
      }
    }
    return true;
  }

  /// <summary>
  /// Получает ИД копируемого листа рассылки.
  /// /// Возвращает -1 - если выбор в селекшн виндоу отменен, Intermech.Consts.UnknownObjectId (0)- если у выбранного документа отсутствует лист рассылки
  /// </summary>
  /// <returns>ИД копируемого листа рассылки. </returns>
  private static long GetCopiedDeliveryListID()
  {
    long copiedDeliveryListId = -1;
    IDescriptor rootDescriptor = new DescriptorCollection()
    {
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545"))
    }[0];
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_164"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length != 1 || !(objArray[0] is IDBTypedObjectID))
      return copiedDeliveryListId;
    long id = (objArray[0] as IDBTypedObjectID).ID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return !(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService) ? copiedDeliveryListId : customService.GetDeliveryListID(sessionKeeper.Session.SessionGUID, id);
  }

  /// <summary>Создать копии по листу рассылки.</summary>
  /// <param name="items">The items.</param>
  /// <param name="viewservices">The viewservices.</param>
  /// <param name="additionalinfo">The additionalinfo.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void CreateCopiesByDeliveryList(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    this.CreateCopiesByDeliveryList(items, true);
  }

  private void CreateCopiesByDeliveryList(ISelectedItems items, bool mindSendedCopies)
  {
    List<long> longList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      if (longList.Contains(itemData.ID))
      {
        int num = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_177"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return;
      }
      longList.Add(itemData.ID);
    }
    using (SessionKeeper sk = new SessionKeeper())
    {
      if (!(sk.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
        throw new KernelException("Не найден ICopiesService");
      INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        {
          try
          {
            DocumentCommandsProvider.CheckUsersRightsForDocRegistration(sk, itemData.ObjectID);
          }
          catch (AccessDeniedException ex)
          {
            AccessDeniedExceptionForm.OnExceptionHandler((object) null, new ExceptionEventArgs((Exception) ex));
            continue;
          }
          IDBAttribute objectAttributeById = sk.Session.GetObjectAttributeByID(itemData.ObjectID, ConstsHolder.InventoryNumberID);
          if (objectAttributeById == null || objectAttributeById.Value == DBNull.Value || objectAttributeById.AsString == string.Empty)
          {
            int num = (int) MessageBox.Show(string.Format(ServiceHolder.rm.GetString("Archives_179"), (object) itemData.Caption), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }
          else
            customService.CreateCopiesByDeliveryList(sk.Session.SessionGUID, itemData.ObjectID, mindSendedCopies);
        }
      }
      service?.FireEvent((object) null, new NotificationEventArgs("CopiesChanged"));
    }
  }

  /// <summary>Заменить копии по листу рассылки</summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  private void ChangeCopiesByDeliveryList(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    this.CreateCopiesByDeliveryList(items, false);
  }

  /// <summary>Копировать лист рассылки в док.</summary>
  /// <param name="items">The items.</param>
  /// <param name="viewservices">The viewservices.</param>
  /// <param name="additionalinfo">The additionalinfo.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void CopyDeliveryListFromECOToDoc(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || ConstsHolder.DeliveryListID == 0 || ConstsHolder.OriginalObjectID == -10000)
      return;
    long objectId = itemData.ObjectID;
    long id = itemData.ID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICopiesService customService1 = sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
      IECOServer customService2 = sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer;
      if (customService1 == null || customService2 == null)
        return;
      long deliveryListId = customService1.GetDeliveryListID(sessionKeeper.Session.SessionGUID, id);
      if (deliveryListId == 0L)
      {
        int num = (int) MessageBox.Show(string.Format(ServiceHolder.rm.GetString("Archives_183"), (object) itemData.Caption), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        Dictionary<long, long> fromEcoComposition = customService2.GetDocsIDsInfoFromECOComposition(objectId, sessionKeeper.Session.SessionGUID);
        if (fromEcoComposition.Count == 0)
          return;
        List<long> ecoDeliveryLists = this.GetDocsFromECODeliveryLists(fromEcoComposition);
        if (!(ServicesManager.GetService(typeof (ICopiesClientService)) is ICopiesClientService service))
          return;
        service.CopyDeliveryList(deliveryListId, ecoDeliveryLists);
      }
    }
  }

  /// <summary>
  /// Получает список ИД листов рассылок для документов.
  /// Если у документа нет листа рассылки - лист рассылки создается и его ИД добавляется в итоговый список.
  /// </summary>
  /// <param name="docsIDsInfo">Словарь (ObjectID, ИД) документов</param>
  /// <returns>Список ИД листов рассылок для документов</returns>
  private List<long> GetDocsFromECODeliveryLists(Dictionary<long, long> docsIDsInfo)
  {
    List<long> ecoDeliveryLists = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.In, (object) docsIDsInfo.Values.ToArray<long>(), LogicalOperators.NONE, 0, false)
      }, new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ConstsHolder.OriginalObjectID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      });
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(ConstsHolder.DeliveryListID, dbRecordSetParams);
      if (dataTable == null)
        return ecoDeliveryLists;
      List<long> longList = new List<long>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64_1 = Convert.ToInt64(row[-2.ToString()]);
        long int64_2 = Convert.ToInt64(row[ConstsHolder.OriginalObjectID.ToString()]);
        ecoDeliveryLists.Add(int64_1);
        longList.Add(int64_2);
      }
      foreach (KeyValuePair<long, long> keyValuePair in docsIDsInfo)
      {
        if (!longList.Contains(keyValuePair.Value))
        {
          long deliveryList = this.CreateDeliveryList(keyValuePair.Key);
          if (deliveryList != 0L)
            ecoDeliveryLists.Add(deliveryList);
        }
      }
    }
    return ecoDeliveryLists;
  }

  /// <summary>
  /// Создает лист рассылки для документа и уведомляет об этом
  /// </summary>
  /// <param name="docObjectID">ObjectID документа</param>
  /// <returns>ObjectID свежесозданного листа рассылки или Intermech.Consts.UnknownObjectId, если что-то пошло не так.</returns>
  private long CreateDeliveryList(long docObjectID)
  {
    long objectID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
      if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService) || service == null)
        return objectID;
      objectID = customService.CreateDeliveryList(sessionKeeper.Session.SessionGUID, docObjectID);
      DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", objectID, ConstsHolder.DeliveryListID);
      service.FireEvent((object) null, (NotificationEventArgs) e);
    }
    return objectID;
  }

  /// <summary>
  /// Проверяет на наличие права у текущего юзера на выполнение команд связанных с регистрацией и работой с копиями документов
  /// Выбрасывает AccessDeniedException, если проверка не пройдена
  /// </summary>
  /// <param name="sk">Сессия</param>
  /// <param name="docObjectID">ИД версии документа, доступность которого проверяем</param>
  /// <returns>Выбрасывает AccessDeniedException, если проверка не пройдена</returns>
  public static void CheckUsersRightsForDocRegistration(SessionKeeper sk, long docObjectID)
  {
    IDBAttribute objectAttributeById = sk.Session.GetObjectAttributeByID(docObjectID, ConstsHolder.ArchiveAttrID);
    bool flag;
    if (objectAttributeById == null || objectAttributeById.Value == DBNull.Value)
    {
      flag = true;
    }
    else
    {
      if (!(sk.Session.GetObject(docObjectID) is IDBSecurity dbSecurity))
        return;
      flag = dbSecurity.CheckAccess(ActionType.DocRegistry, false, false);
    }
    if (!flag)
      throw new AccessDeniedException(sk.Session);
  }
}
