// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.Subscribers.AddSubscriberControl
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Archives.Common;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Interfaces.Copies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies.Subscribers;

/// <summary>Контрол для изменения списка абонентов</summary>
public class AddSubscriberControl : UserControl
{
  /// <summary>изменения в редакторе</summary>
  private bool _isChanged;
  /// <summary>Выделенные в гриде документы</summary>
  private ISelectedItems _items;
  /// <summary>
  /// _id документа,  для которой показывается лист рассылки
  /// (при размещении на закладке навигатора)
  /// </summary>
  private long _id;
  /// <summary>
  /// _id версии документа,  для которой показывается лист рассылки
  /// (при размещении на закладке навигатора)
  /// </summary>
  private long _objectID;
  /// <summary>Список листов рассылки</summary>
  private List<long> _deliveryListIDs = new List<long>();
  /// <summary>Изменённые листы рассылки</summary>
  private readonly List<long> _changedDeliveryList = new List<long>();
  /// <summary>ИД текущего пользователя</summary>
  private long _userID;
  /// <summary>Имя текущего пользователя</summary>
  private string _userName = string.Empty;
  /// <summary>
  /// Показываем  контрол в отдельной форме или на закладке
  /// </summary>
  private OwnerType _type = OwnerType.Form;
  /// <summary>Форма была вызвана из графы Разослать извещения</summary>
  private bool _isCallFromEcoEditor;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeList tlSubscribers;
  private TreeListColumn tlSubscriber;
  private TreeListColumn tlNumber;
  private TreeListColumn tlDate;
  private TreeListColumn tlOwner;
  private TreeListColumn tlSub_ID;
  private TreeListColumn tlOwnerID;
  private TreeListColumn tlListID;
  private TreeListColumn tlActualCopyID;
  private Intermech.Bars.ToolBar tbSubscribers;
  private ButtonItem btnAddForAll;
  private ButtonItem btnAdd;
  private ButtonItem btnDelete;
  private ButtonItem btnSend;
  private TreeListColumn tlActualCopy;
  private TreeListColumn tlCanSend;
  private RepositoryItemSpinEdit countEditor;
  private RepositoryItemSpinEdit repositoryItemSpinEdit1;
  private ButtonItem btnCopyFromDoc;
  private ButtonItem btnCreateDeliveryList;
  private ButtonItem btnAddByRoute;
  private ButtonItem btnDeleteFromAll;
  private ButtonItem btnReturn;
  private RepositoryItemDateEdit repositoryItemDateEdit1;
  private TreeListColumn tlNote;

  /// <summary>
  /// Событие возникает, если в редакторе происходят изменения
  /// </summary>
  public event AddSubscriberControl.ObjectOptionsChangedEventHandler OnChanged;

  /// <summary>
  /// Запрет редактирования
  /// (если документ не поставлен на учёт и лист рассылки ещё не создан)
  /// </summary>
  public bool ReadOnly { get; set; }

  /// <summary>
  /// _id версии документа,  для которой показывается лист рассылки
  /// (при размещении на закладке навигатора)
  /// </summary>
  public long ObjectID
  {
    get => this._objectID;
    set => this._objectID = value;
  }

  /// <summary>
  /// _id документа,  для которой показывается лист рассылки
  /// (при размещении на закладке навигатора)
  /// </summary>
  public long ID
  {
    get => this._id;
    set => this._id = value;
  }

  /// <summary>Список абонентов был изменён</summary>
  public virtual bool IsChanged
  {
    get => this._isChanged;
    set
    {
      this._isChanged = value;
      this.RaiseOnChanged();
    }
  }

  /// <summary>Контрол вызван в отдельной форме или на закладке</summary>
  public OwnerType OwnerType => this._type;

  /// <summary>Конструктор</summary>
  public AddSubscriberControl()
  {
    this.InitializeComponent();
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.tbSubscribers.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged += new EventHandler(this.barManager_RendererChanged);
      this.barManager_RendererChanged((object) service, EventArgs.Empty);
    }
    if (ServicesManager.GetService(typeof (IGuidMapper)) is IGuidMapper)
    {
      this.tlSubscribers.SelectImageList = Statics.IconSrv.ImageList;
      this.tlSubscriber.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly;
      this.tlDate.Options = ColumnOptions.CanSorted | ColumnOptions.ReadOnly | ColumnOptions.CanFocused;
      this.tlOwner.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly;
      this.tlNumber.Options = ColumnOptions.CanSorted | ColumnOptions.ReadOnly | ColumnOptions.CanFocused;
      this.tlActualCopy.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly;
      this.tlNote.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly | ColumnOptions.CanFocused;
    }
    this.tlSubscribers.HorzScrollVisibility = ScrollVisibility.Auto;
  }

  /// <summary>Инициализация вкладки</summary>
  /// <param name="items">The items.</param>
  /// <param name="provider">The provider.</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._items = items;
  }

  /// <summary>Сгенерировать событие "OnChanged"</summary>
  protected virtual void RaiseOnChanged()
  {
    AddSubscriberControl.ObjectOptionsChangedEventHandler onChanged = this.OnChanged;
    if (onChanged == null)
      return;
    onChanged((object) this, new EventArgs());
  }

  /// <summary>Удаление выделенных абонентов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDelete_Click(object sender, EventArgs e)
  {
    TreeListNode focusedNode = this.tlSubscribers.FocusedNode;
    if (focusedNode == null)
      return;
    if (!this._changedDeliveryList.Contains(Convert.ToInt64(focusedNode[(object) "LIST_ID"])))
      this._changedDeliveryList.Add(Convert.ToInt64(focusedNode[(object) "LIST_ID"]));
    this.tlSubscribers.Nodes.Remove(focusedNode);
    this.IsChanged = true;
  }

  /// <summary>Добавить абонентов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    object[] objArray = this.AddSubscribers();
    TreeListNode focusedNode = this.tlSubscribers.FocusedNode;
    if (focusedNode == null)
      return;
    long int64 = Convert.ToInt64(focusedNode[(object) "LIST_ID"]);
    TreeListNode listNode = (TreeListNode) null;
    foreach (TreeListNode node in this.tlSubscribers.Nodes)
    {
      if (Convert.ToInt64(node[(object) "LIST_ID"]) == int64)
      {
        listNode = node;
        break;
      }
    }
    if (objArray == null || objArray.Length == 0)
      return;
    foreach (object subObject in objArray)
      this.AddSubscriberNode(subObject as IDBTypedObjectID, listNode);
    this.IsChanged = true;
  }

  /// <summary>Добавить во все списки рассылки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAddForAll_Click(object sender, EventArgs e)
  {
    object[] objArray = this.AddSubscribers();
    if (objArray == null || objArray.Length == 0)
      return;
    foreach (object obj in objArray)
    {
      IDBTypedObjectID subObject = obj as IDBTypedObjectID;
      foreach (TreeListNode node in this.tlSubscribers.Nodes)
        this.AddSubscriberNode(subObject, node);
    }
    this.IsChanged = true;
  }

  /// <summary>Удалить абонента из всех списков рассылки.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnDeleteFromAll_Click(object sender, EventArgs e)
  {
    TreeListNode focusedNode = this.tlSubscribers.FocusedNode;
    if (focusedNode == null)
      return;
    this.DeleteSubscriberFromAllNodes(Convert.ToInt64(focusedNode[(object) "SUB_ID"]));
  }

  /// <summary>Кнопка Скопировать у документа.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnCopyFromDoc_Click(object sender, EventArgs e)
  {
    if (this.IsChanged)
    {
      if (MessageBox.Show(string.Format(ServiceHolder.rm.GetString("Archives_168")), string.Format(ServiceHolder.rm.GetString("Archives_111")), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
        return;
      this.Save();
    }
    if (this._isCallFromEcoEditor)
    {
      long num = 0;
      long deliveryListId = this._deliveryListIDs[0];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IECOServer customService1 = sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer;
        ICopiesService customService2 = sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
        if (customService1 != null)
        {
          if (customService2 != null)
          {
            IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(deliveryListId, ConstsHolder.OriginalObjectID);
            if (objectAttributeById == null || objectAttributeById.Value == null)
              return;
            long asInteger = objectAttributeById.AsInteger;
            IDBObject objectById = sessionKeeper.Session.GetObjectByID(asInteger, false);
            List<long> list = customService1.GetDocsIDsInfoFromECOComposition(objectById.ObjectID, sessionKeeper.Session.SessionGUID).Keys.ToList<long>();
            IDescriptor rootDescriptor = (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, -1, string.Format(ServiceHolder.rm.GetString("Archives_190")), (IList) list);
            object[] objArray = Intermech.Navigator.SelectionWindow.Select(string.Format(ServiceHolder.rm.GetString("Archives_191")), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.HideTree | SelectionOptions.HideViewsToolbar | SelectionOptions.HideViewsGroupingBox | SelectionOptions.SelectObjects | SelectionOptions.DisableObjectListFilter | SelectionOptions.DisableMultiselect | SelectionOptions.ForceRebuildNavTree);
            if (objArray != null)
            {
              if (objArray.Length == 1)
              {
                if (objArray[0] is IDBTypedObjectID dbTypedObjectId)
                  num = customService2.GetDeliveryListID(sessionKeeper.Session.SessionGUID, dbTypedObjectId.ID);
              }
            }
          }
        }
      }
      if (num != 0L && ServicesManager.GetService(typeof (ICopiesClientService)) is ICopiesClientService service)
      {
        long copiedDeliveryListID = num;
        service.CopyDeliveryList(copiedDeliveryListID, new List<long>()
        {
          deliveryListId
        });
      }
    }
    else
    {
      ServiceContainer viewServices = new ServiceContainer();
      viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
      Intermech.Navigator.ContextMenu.Services.InvokeCommand("CopyDeliveryListFromDoc", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this._items, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
    }
    this.RefreshEditor();
  }

  /// <summary>Добавить абонентов по расцеховочному маршруту.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnAddByRoute_Click(object sender, EventArgs e)
  {
    if (this.IsChanged)
    {
      if (MessageBox.Show(string.Format(ServiceHolder.rm.GetString("Archives_168")), string.Format(ServiceHolder.rm.GetString("Archives_111")), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
        return;
      this.Save();
    }
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("AddSubscribersByRoute", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this._items, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
    this.RefreshEditor();
  }

  /// <summary>Нажатие кнопки Создать лист рассылки.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnCreateDeliveryList_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
        return;
      this._deliveryListIDs = new List<long>()
      {
        customService.CreateDeliveryList(sessionKeeper.Session.SessionGUID, this._objectID)
      };
      this.RefreshEditor();
    }
  }

  /// <summary>Выслать копию</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnSend_Click(object sender, EventArgs e)
  {
    if (this.tlSubscribers.FocusedNode == null || ConstsHolder.CopyOfDocumentID == -1)
      return;
    TreeListNode focusedNode = this.tlSubscribers.FocusedNode;
    using (SendCopiesForm sendCopiesForm = new SendCopiesForm(this._deliveryListIDs[0], Convert.ToInt64(focusedNode[(object) "SUB_ID"]), this._id, Convert.ToInt32(focusedNode[(object) "NUMBER"])))
    {
      if (sendCopiesForm.ShowDialog() == DialogResult.OK)
      {
        focusedNode[(object) "ACTUAL_COPY_ID"] = (object) sendCopiesForm.ActualCopyID;
        focusedNode[(object) "ACTUAL_COPY"] = (object) sendCopiesForm.ActualCopyCaption;
      }
    }
    this.UpdateControls();
  }

  /// <summary>
  /// Вернуть копию
  /// Работает только для одного выбранного абонента
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnReturn_Click(object sender, EventArgs e)
  {
    if (this.tlSubscribers.FocusedNode == null)
      return;
    List<MyElement> choosedSubscriber = this.GetCopiesForChoosedSubscriber(this._id, Convert.ToInt64(this.tlSubscribers.FocusedNode[(object) "SUB_ID"]));
    if (choosedSubscriber.Count == 0)
    {
      int num = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_212"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      using (ReturnCopiesFromSubscriberViewForm subscriberViewForm = new ReturnCopiesFromSubscriberViewForm())
      {
        subscriberViewForm.Init(choosedSubscriber);
        if (subscriberViewForm.ShowDialog() != DialogResult.OK)
          return;
        List<long> copiesID = new List<long>();
        using (SessionKeeper sk = new SessionKeeper())
        {
          foreach (long docObjectID in subscriberViewForm.CopiesToReturn)
          {
            try
            {
              DocumentCommandsProvider.CheckUsersRightsForDocRegistration(sk, docObjectID);
              copiesID.Add(docObjectID);
            }
            catch (AccessDeniedException ex)
            {
              AccessDeniedExceptionForm.OnExceptionHandler((object) null, new Intermech.Interfaces.ExceptionEventArgs((Exception) ex));
            }
          }
          if (copiesID.Count == 0)
            return;
          if (sk.Session.GetCustomService(typeof (IDocumentCopyService)) is IDocumentCopyService customService)
            customService.ReturnCopies(copiesID, subscriberViewForm.WhoReturnsCopies, subscriberViewForm.ReturnDate, (object) sk.Session.SessionGUID);
        }
        this.RefreshEditor();
        this.UpdateControls();
      }
    }
  }

  /// <summary>
  /// Получить список копий для абонента, на котором была вызвана форма
  /// </summary>
  /// <returns></returns>
  private List<MyElement> GetCopiesForChoosedSubscriber(long docId, long subscriberId)
  {
    List<MyElement> choosedSubscriber = new List<MyElement>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[3]
      {
        new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) docId, LogicalOperators.AND, 0, false),
        new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) subscriberId, LogicalOperators.AND, 0, false),
        new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.SendLCStepID, LogicalOperators.AND, 0, false)
      }, new ColumnDescriptor[4]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 1),
        new ColumnDescriptor((object) ConstsHolder.RecipientID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ConstsHolder.RecipientID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      });
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(ConstsHolder.CopyOfDocumentID, dbRecordSetParams);
      if (dataTable == null)
        return choosedSubscriber;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64_1 = Convert.ToInt64(row[0]);
        string caption1 = Convert.ToString(row[1]);
        long int64_2 = Convert.ToInt64(row[2]);
        string caption2 = Convert.ToString(row[3]);
        choosedSubscriber.Add(new MyElement((object) int64_1, caption1, (object) new MyElement((object) int64_2, caption2, (object) null)));
      }
    }
    return choosedSubscriber;
  }

  /// <summary>Сохранить сделанные изменения</summary>
  public void Save()
  {
    if (this._changedDeliveryList.Count <= 0)
      return;
    List<DeliveryList> deliveryLists = new List<DeliveryList>();
    foreach (TreeListNode node1 in this.tlSubscribers.Nodes)
    {
      long int64 = Convert.ToInt64(node1[(object) 6]);
      if (this._changedDeliveryList.Contains(int64))
      {
        DeliveryList deliveryList = new DeliveryList()
        {
          ID = int64
        };
        List<Subscriber> subscriberList = new List<Subscriber>();
        for (int index = 0; index < node1.Nodes.Count; ++index)
        {
          TreeListNode node2 = node1.Nodes[index];
          Subscriber subscriber = new Subscriber()
          {
            ID = Convert.ToInt64(node2[(object) "SUB_ID"]),
            CopyNumber = Convert.ToInt32(node2[(object) "NUMBER"]),
            OwnerId = Convert.ToInt64(node2[(object) "OWNER_ID"]),
            SignDate = Convert.ToDateTime(node2[(object) "DATE"]),
            ActualCopyId = Convert.ToInt64(node2[(object) "ACTUAL_COPY_ID"]),
            Note = Convert.ToString(node2[(object) "NOTE"])
          };
          subscriberList.Add(subscriber);
        }
        deliveryList.Subscribers = subscriberList;
        deliveryLists.Add(deliveryList);
      }
    }
    if (deliveryLists.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
          throw new KernelException("Не найден сервис для работы с копиями и листами рассылки ICopiesService.");
        customService.SaveDeliveryLists(sessionKeeper.Session.SessionGUID, deliveryLists);
      }
    }
    this.IsChanged = false;
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    foreach (DeliveryList deliveryList in deliveryLists)
      service?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", deliveryList.ID, ConstsHolder.DeliveryListID, new AttributeValues[0], new AttributeValues[0]));
  }

  /// <summary>Отменить сделанные изменения</summary>
  public void Cancel()
  {
    this.LoadSubscribers(this._deliveryListIDs, this._type, this._isCallFromEcoEditor);
    this.IsChanged = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlSubscribers_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlSubscribers_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
  {
    if (this.tlSubscribers.FocusedNode == null || e.Column == null || e.Column != this.tlNumber && e.Column != this.tlDate && e.Column != this.tlNote)
      return;
    e.Column.Options = this.tlSubscribers.FocusedNode.Level == 0 ? this.tlNumber.Options & ~ColumnOptions.CanFocused | ColumnOptions.ReadOnly : this.tlNumber.Options & ~ColumnOptions.ReadOnly | ColumnOptions.CanFocused;
  }

  /// <summary>изменение значения ячейки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlSubscribers_CellValueChanged(object sender, CellValueChangedEventArgs e)
  {
    if (e.Column != this.tlNumber && e.Column != this.tlDate && e.Column != this.tlNote)
      return;
    if (!this._changedDeliveryList.Contains(Convert.ToInt64(e.Node[(object) "LIST_ID"])))
      this._changedDeliveryList.Add(Convert.ToInt64(e.Node[(object) "LIST_ID"]));
    this.IsChanged = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlSubscribers_CompareNodeValues(object sender, CompareNodeValuesEventArgs e)
  {
    if (e.Column != this.tlNumber)
      return;
    try
    {
      int int32_1 = Convert.ToInt32(e.NodeValue1);
      int int32_2 = Convert.ToInt32(e.NodeValue2);
      e.Result = int32_1.CompareTo(int32_2);
    }
    catch
    {
    }
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void barManager_RendererChanged(object sender, EventArgs e)
  {
    if (!(sender is BarManager barManager))
      return;
    this.tbSubscribers.Renderer = barManager.Renderer;
  }

  /// <summary>Удаляет абонента из всех узлов.</summary>
  /// <param name="subscrID">ID абонента</param>
  private void DeleteSubscriberFromAllNodes(long subscrID)
  {
    foreach (TreeListNode node1 in this.tlSubscribers.Nodes)
    {
      for (int index = 0; index < node1.Nodes.Count; ++index)
      {
        TreeListNode node2 = node1.Nodes[index];
        if (Convert.ToInt64(node2[(object) "SUB_ID"]) == subscrID)
        {
          node1.Nodes.Remove(node2);
          if (!this._changedDeliveryList.Contains(Convert.ToInt64(node1[(object) "LIST_ID"])))
          {
            this._changedDeliveryList.Add(Convert.ToInt64(node1[(object) "LIST_ID"]));
            break;
          }
          break;
        }
      }
    }
  }

  /// <summary>Добавить узел для абонента в узел листа рассылки</summary>
  /// <param name="subObject">Интерфейс объекта абонента</param>
  /// <param name="listNode">Узел дерева (лист рассылки), в который будем добавлять абонента</param>
  /// <returns></returns>
  private void AddSubscriberNode(IDBTypedObjectID subObject, TreeListNode listNode)
  {
    if (listNode == null)
      return;
    DateTime now = DateTime.Now;
    TreeListNode treeListNode1 = (TreeListNode) null;
    if (!this._changedDeliveryList.Contains(Convert.ToInt64(listNode[(object) "LIST_ID"])))
      this._changedDeliveryList.Add(Convert.ToInt64(listNode[(object) "LIST_ID"]));
    foreach (TreeListNode node in listNode.Nodes)
    {
      if (Convert.ToInt64(node[(object) 4]) == subObject.ObjectID)
      {
        treeListNode1 = node;
        break;
      }
    }
    TreeListNode treeListNode2 = treeListNode1 ?? this.tlSubscribers.AppendNode((object) new object[0], listNode);
    treeListNode2[(object) "SUB_CAPTION"] = (object) subObject.Caption;
    treeListNode2.ImageIndex = treeListNode2.SelectImageIndex = Statics.IconSrv.IndexOf(4, subObject.ObjectType);
    treeListNode2[(object) "NUMBER"] = (object) "1";
    treeListNode2[(object) "DATE"] = (object) now;
    treeListNode2[(object) "OWNER_CAPTION"] = (object) this._userName;
    treeListNode2[(object) "SUB_ID"] = (object) subObject.ObjectID;
    treeListNode2[(object) "OWNER_ID"] = (object) this._userID;
    treeListNode2[(object) "LIST_ID"] = listNode[(object) "LIST_ID"];
    treeListNode2[(object) "ACTUAL_COPY_ID"] = (object) 0L;
    treeListNode2[(object) "NOTE"] = (object) string.Empty;
    if (this._type != OwnerType.Control)
      return;
    treeListNode2[(object) "CAN_SEND"] = (object) this.CanSendCopy(subObject.ObjectID);
  }

  /// <summary>
  ///  Проверить можно ли высылать указанному абоненту копии
  /// </summary>
  /// <param name="subscrID">id пользователя</param>
  /// <returns></returns>
  private bool CanSendCopy(long subscrID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[4]
      {
        new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.SendLCStepID, LogicalOperators.AND, 0, false),
        new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) subscrID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) this._id, LogicalOperators.AND, 0, false),
        new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.NotEqual, (object) Math.Abs(this._objectID), LogicalOperators.AND, 0, false)
      }, new object[1]{ (object) -2 });
      return sessionKeeper.Session.ObjectsSelect(ConstsHolder.CopyOfDocumentID, dbRecordSetParams).Rows.Count == 0;
    }
  }

  /// <summary>выбор абонентов</summary>
  /// <returns></returns>
  private object[] AddSubscribers()
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")));
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545")));
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cadd9235-306c-11d8-b4e9-00304f19f545")));
    int objectTypeId = MetaDataHelper.GetObjectTypeID(PortalConsts.objtypeSites);
    if (objectTypeId != -1)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objectTypeId));
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(ServiceHolder.rm.GetString("Archives_120"), descriptors);
    return Intermech.Navigator.SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_123"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree);
  }

  /// <summary>Видимость кнопок</summary>
  private void UpdateControls()
  {
    this.btnCreateDeliveryList.Visible = this._type == OwnerType.Control;
    this.btnCreateDeliveryList.Enabled = this._deliveryListIDs[0] == 0L;
    TreeListNode focusedNode = this.tlSubscribers.FocusedNode;
    this.btnAddForAll.Visible = this._deliveryListIDs.Count > 1;
    this.btnDeleteFromAll.Visible = this._deliveryListIDs.Count > 1;
    this.btnAdd.Enabled = !this.btnCreateDeliveryList.Enabled && focusedNode != null;
    this.btnCopyFromDoc.Enabled = this.btnAdd.Enabled;
    this.btnCopyFromDoc.Visible = this._type == OwnerType.Control || this._isCallFromEcoEditor;
    this.btnAddByRoute.Enabled = this.btnAdd.Enabled;
    this.btnAddByRoute.Visible = this._type == OwnerType.Control;
    this.btnDelete.Enabled = !this.btnCreateDeliveryList.Enabled && focusedNode != null && focusedNode.Level != 0;
    this.btnDeleteFromAll.Enabled = focusedNode != null && focusedNode.Level != 0;
    this.tlNumber.Options = focusedNode == null || focusedNode.Level != 1 ? this.tlNumber.Options & ~ColumnOptions.CanFocused | ColumnOptions.ReadOnly : this.tlNumber.Options & ~ColumnOptions.ReadOnly | ColumnOptions.CanFocused;
    this.tlDate.Options = focusedNode == null || focusedNode.Level != 1 ? this.tlDate.Options & ~ColumnOptions.CanFocused | ColumnOptions.ReadOnly : this.tlDate.Options & ~ColumnOptions.ReadOnly | ColumnOptions.CanFocused;
    this.tlNote.Options = focusedNode == null || focusedNode.Level != 1 ? this.tlNote.Options & ~ColumnOptions.CanFocused | ColumnOptions.ReadOnly : this.tlNote.Options & ~ColumnOptions.ReadOnly | ColumnOptions.CanFocused;
    this.btnSend.Visible = this._type == OwnerType.Control;
    this.btnSend.Enabled = !this.ReadOnly && focusedNode != null && focusedNode.Level != 0 && Convert.ToBoolean(focusedNode[(object) "CAN_SEND"]);
    this.btnReturn.Visible = this._type == OwnerType.Control;
    this.btnReturn.Enabled = !this.ReadOnly && focusedNode != null && focusedNode.Level != 0;
  }

  /// <summary>
  /// Загрузить существующих  абонентов из указанных листов рассылки
  /// </summary>
  /// <param name="deliveryList">список листов рассылки, в которые будут добавлены абоненты</param>
  /// <param name="type">показываем  контрол в отдельной форме или на закладке</param>
  /// <param name="isCallFromEco">форма вызвалась из графы "Разослать" извещения</param>
  public void LoadSubscribers(List<long> deliveryList, OwnerType type, bool isCallFromEco)
  {
    this._deliveryListIDs = deliveryList;
    this._type = type;
    this._isCallFromEcoEditor = isCallFromEco;
    this.tlSubscribers.Nodes.Clear();
    this.tlActualCopy.VisibleIndex = type == OwnerType.Form ? -1 : 4;
    if (deliveryList.Count == 0 || deliveryList[0] == 0L)
    {
      this.UpdateControls();
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICurrentUserAndRole service = ApplicationServices.Container.GetService<ICurrentUserAndRole>(false);
        this._userID = service != null ? service.UserID : throw new KernelException("Не найден ICurrentUserAndRole сервис.");
        this._userName = service.UserName;
        if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
          throw new KernelException("Не найден сервис для работы с копиями и листами рассылки ICopiesService.");
        foreach (DeliveryList deliveryList1 in customService.GetDeliveryLists(sessionKeeper.Session.SessionGUID, deliveryList))
        {
          TreeListNode parentNode = this.tlSubscribers.AppendNode((object) new object[0], (TreeListNode) null);
          parentNode[(object) "SUB_CAPTION"] = (object) deliveryList1.NameInMessages;
          parentNode[(object) "LIST_ID"] = (object) deliveryList1.ID;
          int num = Statics.IconSrv.IndexOf(4, ConstsHolder.DeliveryListID);
          parentNode.SelectImageIndex = num;
          parentNode.ImageIndex = parentNode.SelectImageIndex;
          foreach (Subscriber subscriber in deliveryList1.Subscribers)
          {
            TreeListNode treeListNode = this.tlSubscribers.AppendNode((object) new object[0], parentNode);
            treeListNode[(object) "SUB_CAPTION"] = (object) subscriber.Caption;
            treeListNode.ImageIndex = treeListNode.SelectImageIndex = Statics.IconSrv.IndexOf(4, subscriber.ObjectType);
            treeListNode.SetValue((object) "NUMBER", (object) subscriber.CopyNumber);
            treeListNode[(object) "DATE"] = (object) subscriber.SignDate;
            treeListNode[(object) "OWNER_CAPTION"] = (object) subscriber.OwnerName;
            treeListNode[(object) "SUB_ID"] = (object) subscriber.ID;
            treeListNode[(object) "OWNER_ID"] = (object) subscriber.OwnerId;
            treeListNode[(object) "LIST_ID"] = (object) deliveryList1.ID;
            treeListNode[(object) "ACTUAL_COPY_ID"] = (object) subscriber.ActualCopyId;
            treeListNode[(object) "ACTUAL_COPY"] = (object) subscriber.ActualCopyName;
            treeListNode[(object) "NOTE"] = (object) subscriber.Note;
            if (type == OwnerType.Control)
              treeListNode[(object) "CAN_SEND"] = (object) this.CanSendCopy(subscriber.ID);
          }
          parentNode.Expanded = true;
        }
      }
      this.tlSubscribers.Columns["ACTUAL_COPY"].BestFit();
      this.UpdateControls();
    }
  }

  /// <summary>перечитать информацию на закладке</summary>
  public void RefreshEditor()
  {
    this.LoadSubscribers(this._deliveryListIDs, this._type, this._isCallFromEcoEditor);
  }

  /// <summary>Сохранить физические размеры контрола и колонок</summary>
  public void SaveLayout()
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, int>()
    {
      {
        "ColumnSubscriberWidth",
        this.tlSubscribers.Columns["SUB_CAPTION"].Width
      },
      {
        "ColumnCopyNumberWidth",
        this.tlSubscribers.Columns["NUMBER"].Width
      },
      {
        "ColumnDataWidth",
        this.tlSubscribers.Columns["DATE"].Width
      },
      {
        "ColumnOwnerWidth",
        this.tlSubscribers.Columns["OWNER_CAPTION"].Width
      },
      {
        "ColumnActualCopyWidth",
        this.tlSubscribers.Columns["ACTUAL_COPY"].Width
      },
      {
        "ColumnNoteWidth",
        this.tlSubscribers.Columns["NOTE"].Width
      }
    });
  }

  /// <summary>Загрузить физические размеры контрола и колонок</summary>
  public void LoadLayout()
  {
    Dictionary<string, int> dictionary = new Dictionary<string, int>()
    {
      {
        "ColumnSubscriberWidth",
        this.tlSubscribers.Columns["SUB_CAPTION"].Width
      },
      {
        "ColumnCopyNumberWidth",
        this.tlSubscribers.Columns["NUMBER"].Width
      },
      {
        "ColumnDataWidth",
        this.tlSubscribers.Columns["DATE"].Width
      },
      {
        "ColumnOwnerWidth",
        this.tlSubscribers.Columns["OWNER_CAPTION"].Width
      },
      {
        "ColumnActualCopyWidth",
        this.tlSubscribers.Columns["ACTUAL_COPY"].Width
      },
      {
        "ColumnNoteWidth",
        this.tlSubscribers.Columns["NOTE"].Width
      }
    };
    FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    this.tlSubscribers.Columns["SUB_CAPTION"].Width = dictionary["ColumnSubscriberWidth"];
    this.tlSubscribers.Columns["NUMBER"].Width = dictionary["ColumnCopyNumberWidth"];
    this.tlSubscribers.Columns["DATE"].Width = dictionary["ColumnDataWidth"];
    this.tlSubscribers.Columns["OWNER_CAPTION"].Width = dictionary["ColumnOwnerWidth"];
    this.tlSubscribers.Columns["ACTUAL_COPY"].Width = dictionary["ColumnActualCopyWidth"];
    this.tlSubscribers.Columns["NOTE"].Width = dictionary["ColumnNoteWidth"];
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.tbSubscribers.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.barManager_RendererChanged);
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddSubscriberControl));
    this.tlSubscribers = new TreeList();
    this.tlSubscriber = new TreeListColumn();
    this.tlNumber = new TreeListColumn();
    this.repositoryItemSpinEdit1 = new RepositoryItemSpinEdit();
    this.tlDate = new TreeListColumn();
    this.repositoryItemDateEdit1 = new RepositoryItemDateEdit();
    this.tlOwner = new TreeListColumn();
    this.tlSub_ID = new TreeListColumn();
    this.tlOwnerID = new TreeListColumn();
    this.tlListID = new TreeListColumn();
    this.tlActualCopyID = new TreeListColumn();
    this.tlActualCopy = new TreeListColumn();
    this.tlCanSend = new TreeListColumn();
    this.countEditor = new RepositoryItemSpinEdit();
    this.tbSubscribers = new Intermech.Bars.ToolBar();
    this.btnAddForAll = new ButtonItem();
    this.btnDeleteFromAll = new ButtonItem();
    this.btnCreateDeliveryList = new ButtonItem();
    this.btnAdd = new ButtonItem();
    this.btnCopyFromDoc = new ButtonItem();
    this.btnAddByRoute = new ButtonItem();
    this.btnDelete = new ButtonItem();
    this.btnSend = new ButtonItem();
    this.btnReturn = new ButtonItem();
    this.tlNote = new TreeListColumn();
    this.tlSubscribers.BeginInit();
    this.repositoryItemSpinEdit1.BeginInit();
    this.repositoryItemDateEdit1.BeginInit();
    this.countEditor.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tlSubscribers, "tlSubscribers");
    this.tlSubscribers.Columns.AddRange(new TreeListColumn[11]
    {
      this.tlSubscriber,
      this.tlNumber,
      this.tlDate,
      this.tlOwner,
      this.tlSub_ID,
      this.tlOwnerID,
      this.tlListID,
      this.tlActualCopyID,
      this.tlActualCopy,
      this.tlCanSend,
      this.tlNote
    });
    this.tlSubscribers.Name = "tlSubscribers";
    this.tlSubscribers.RepositoryItems.AddRange(new RepositoryItem[2]
    {
      (RepositoryItem) this.countEditor,
      (RepositoryItem) this.repositoryItemDateEdit1
    });
    this.tlSubscribers.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseFont | StyleOptions.UseForeColor, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.tlSubscribers.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.tlSubscribers_FocusedNodeChanged);
    this.tlSubscribers.FocusedColumnChanged += new FocusedColumnChangedEventHandler(this.tlSubscribers_FocusedColumnChanged);
    this.tlSubscribers.CompareNodeValues += new CompareNodeValuesEventHandler(this.tlSubscribers_CompareNodeValues);
    this.tlSubscribers.CellValueChanged += new CellValueChangedEventHandler(this.tlSubscribers_CellValueChanged);
    componentResourceManager.ApplyResources((object) this.tlSubscriber, "tlSubscriber");
    this.tlSubscriber.Name = "tlSubscriber";
    componentResourceManager.ApplyResources((object) this.tlNumber, "tlNumber");
    this.tlNumber.ColumnEdit = (RepositoryItem) this.repositoryItemSpinEdit1;
    this.tlNumber.Format.FormatString = "g";
    this.tlNumber.Format.FormatType = FormatType.Numeric;
    this.tlNumber.Name = "tlNumber";
    this.repositoryItemSpinEdit1.AutoHeight = false;
    this.repositoryItemSpinEdit1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.repositoryItemSpinEdit1.ButtonsStyle = BorderStyles.Office2003;
    this.repositoryItemSpinEdit1.DisplayFormat.FormatString = "g";
    this.repositoryItemSpinEdit1.DisplayFormat.FormatType = FormatType.Numeric;
    this.repositoryItemSpinEdit1.EditFormat.FormatString = "g";
    this.repositoryItemSpinEdit1.EditFormat.FormatType = FormatType.Numeric;
    this.repositoryItemSpinEdit1.IsFloatValue = false;
    this.repositoryItemSpinEdit1.MaxValue = new Decimal(new int[4]
    {
      10000,
      0,
      0,
      0
    });
    this.repositoryItemSpinEdit1.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
    this.repositoryItemSpinEdit1.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemSpinEdit1.UseCtrlIncrement = true;
    componentResourceManager.ApplyResources((object) this.tlDate, "tlDate");
    this.tlDate.ColumnEdit = (RepositoryItem) this.repositoryItemDateEdit1;
    this.tlDate.Name = "tlDate";
    this.repositoryItemDateEdit1.AutoHeight = false;
    this.repositoryItemDateEdit1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemDateEdit1.DisplayFormat.FormatString = "G";
    this.repositoryItemDateEdit1.DisplayFormat.FormatType = FormatType.DateTime;
    this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
    componentResourceManager.ApplyResources((object) this.tlOwner, "tlOwner");
    this.tlOwner.Name = "tlOwner";
    componentResourceManager.ApplyResources((object) this.tlSub_ID, "tlSub_ID");
    this.tlSub_ID.Name = "tlSub_ID";
    componentResourceManager.ApplyResources((object) this.tlOwnerID, "tlOwnerID");
    this.tlOwnerID.Name = "tlOwnerID";
    componentResourceManager.ApplyResources((object) this.tlListID, "tlListID");
    this.tlListID.Name = "tlListID";
    componentResourceManager.ApplyResources((object) this.tlActualCopyID, "tlActualCopyID");
    this.tlActualCopyID.Name = "tlActualCopyID";
    componentResourceManager.ApplyResources((object) this.tlActualCopy, "tlActualCopy");
    this.tlActualCopy.Name = "tlActualCopy";
    componentResourceManager.ApplyResources((object) this.tlCanSend, "tlCanSend");
    this.tlCanSend.Name = "tlCanSend";
    this.countEditor.AutoHeight = false;
    this.countEditor.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.countEditor.DisplayFormat.FormatString = "g";
    this.countEditor.DisplayFormat.FormatType = FormatType.Numeric;
    this.countEditor.EditFormat.FormatString = "g";
    this.countEditor.EditFormat.FormatType = FormatType.Numeric;
    this.countEditor.IsFloatValue = false;
    this.countEditor.MaxValue = new Decimal(new int[4]
    {
      10000,
      0,
      0,
      0
    });
    this.countEditor.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.countEditor.Name = "countEditor";
    this.countEditor.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.countEditor.UseCtrlIncrement = true;
    this.tbSubscribers.FullMenus = true;
    this.tbSubscribers.Guid = new Guid("37056402-c6d1-47d4-be0f-e941c1a06e55");
    this.tbSubscribers.Hidden = false;
    this.tbSubscribers.Items.AddRange(new ToolbarItemBase[9]
    {
      (ToolbarItemBase) this.btnAddForAll,
      (ToolbarItemBase) this.btnDeleteFromAll,
      (ToolbarItemBase) this.btnCreateDeliveryList,
      (ToolbarItemBase) this.btnAdd,
      (ToolbarItemBase) this.btnCopyFromDoc,
      (ToolbarItemBase) this.btnAddByRoute,
      (ToolbarItemBase) this.btnDelete,
      (ToolbarItemBase) this.btnSend,
      (ToolbarItemBase) this.btnReturn
    });
    componentResourceManager.ApplyResources((object) this.tbSubscribers, "tbSubscribers");
    this.tbSubscribers.Name = "tbSubscribers";
    this.tbSubscribers.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.btnAddForAll, "btnAddForAll");
    this.btnAddForAll.Image = (Image) componentResourceManager.GetObject("btnAddForAll.Image");
    this.btnAddForAll.Click += new EventHandler(this.btnAddForAll_Click);
    componentResourceManager.ApplyResources((object) this.btnDeleteFromAll, "btnDeleteFromAll");
    this.btnDeleteFromAll.Image = (Image) componentResourceManager.GetObject("btnDeleteFromAll.Image");
    this.btnDeleteFromAll.Click += new EventHandler(this.btnDeleteFromAll_Click);
    this.btnCreateDeliveryList.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnCreateDeliveryList, "btnCreateDeliveryList");
    this.btnCreateDeliveryList.Enabled = false;
    this.btnCreateDeliveryList.Icon = (Icon) componentResourceManager.GetObject("btnCreateDeliveryList.Icon");
    this.btnCreateDeliveryList.Image = (Image) componentResourceManager.GetObject("btnCreateDeliveryList.Image");
    this.btnCreateDeliveryList.Visible = false;
    this.btnCreateDeliveryList.Click += new EventHandler(this.btnCreateDeliveryList_Click);
    this.btnAdd.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Enabled = false;
    this.btnAdd.Icon = (Icon) componentResourceManager.GetObject("btnAdd.Icon");
    this.btnAdd.Image = (Image) componentResourceManager.GetObject("btnAdd.Image");
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    componentResourceManager.ApplyResources((object) this.btnCopyFromDoc, "btnCopyFromDoc");
    this.btnCopyFromDoc.Icon = (Icon) componentResourceManager.GetObject("btnCopyFromDoc.Icon");
    this.btnCopyFromDoc.Image = (Image) componentResourceManager.GetObject("btnCopyFromDoc.Image");
    this.btnCopyFromDoc.Click += new EventHandler(this.btnCopyFromDoc_Click);
    componentResourceManager.ApplyResources((object) this.btnAddByRoute, "btnAddByRoute");
    this.btnAddByRoute.Icon = (Icon) componentResourceManager.GetObject("btnAddByRoute.Icon");
    this.btnAddByRoute.Image = (Image) componentResourceManager.GetObject("btnAddByRoute.Image");
    this.btnAddByRoute.Click += new EventHandler(this.btnAddByRoute_Click);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Enabled = false;
    this.btnDelete.Icon = (Icon) componentResourceManager.GetObject("btnDelete.Icon");
    this.btnDelete.Image = (Image) componentResourceManager.GetObject("btnDelete.Image");
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.btnSend.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnSend, "btnSend");
    this.btnSend.Enabled = false;
    this.btnSend.Icon = (Icon) componentResourceManager.GetObject("btnSend.Icon");
    this.btnSend.Image = (Image) componentResourceManager.GetObject("btnSend.Image");
    this.btnSend.Click += new EventHandler(this.btnSend_Click);
    componentResourceManager.ApplyResources((object) this.btnReturn, "btnReturn");
    this.btnReturn.Enabled = false;
    this.btnReturn.Icon = (Icon) componentResourceManager.GetObject("btnReturn.Icon");
    this.btnReturn.Image = (Image) componentResourceManager.GetObject("btnReturn.Image");
    this.btnReturn.Click += new EventHandler(this.btnReturn_Click);
    componentResourceManager.ApplyResources((object) this.tlNote, "tlNote");
    this.tlNote.Name = "tlNote";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlSubscribers);
    this.Controls.Add((Control) this.tbSubscribers);
    this.Name = nameof (AddSubscriberControl);
    this.tlSubscribers.EndInit();
    this.repositoryItemSpinEdit1.EndInit();
    this.repositoryItemDateEdit1.EndInit();
    this.countEditor.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Делегат события об изменении в редакторе</summary>
  /// <param name="sender">Контрол (редактор опций объекта)</param>
  /// <param name="e">Аргументы события</param>
  public delegate void ObjectOptionsChangedEventHandler(object sender, EventArgs e);
}
