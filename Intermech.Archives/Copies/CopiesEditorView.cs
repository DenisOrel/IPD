// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopiesEditorView
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
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
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Закладка для работы с копиями документа</summary>
public class CopiesEditorView : ChildrenView, IIODestination
{
  /// <summary>Все копии выделенных документов</summary>
  private List<CopyNodeInfo> _copies;
  /// <summary>
  /// Отображаемые копии выделенных документов. В зависимости от режима комбобокса.
  /// </summary>
  private List<CopyNodeInfo> _displayedCopies;
  /// <summary>
  /// Версия документа, для которого создаём копии (вью вызван для одного документа)
  /// </summary>
  private long _objectID;
  /// <summary>
  /// id документа, для которого создаём копии (вью вызван для одного документа)
  /// </summary>
  private long _id;
  /// <summary>id документов, с копиями которых работаем</summary>
  private List<long> _ids = new List<long>();
  /// <summary>тип документа, для которого создаём копии</summary>
  private int _typeID = -1;
  /// <summary>Листы рассылки для документов</summary>
  private List<long> _deliveryListIDs;
  /// <summary>
  /// Режим отображения копий в зависимости от выбора в комбобоксе.
  /// </summary>
  private string _copyDisplayingMode;
  /// <summary>Список именованных значков</summary>
  private readonly INamedImageList namedImageList;
  /// <summary>Документы, для которых вызван вью</summary>
  private ISelectedItems _items;
  /// <summary>Сервисы</summary>
  private System.IServiceProvider _serviceProvider;
  /// <summary>
  /// Запрет редактирования
  /// (если документ не поставлен на учёт и лист рассылки ещё не создан)
  /// </summary>
  private bool _readOnly = true;
  /// <summary>Вкладка открыта для одного выделенного документа</summary>
  private bool _isForOneItem;
  /// <summary>Диспетчер событий</summary>
  private readonly IODispatcher _IODispatcher;
  /// <summary>
  /// Сервис службы "горячих клавиш" и связанных с ними команд
  /// </summary>
  private readonly IHotKeysManager _hotKeysManager;
  /// <summary>
  /// 
  /// </summary>
  private MenuBarItem copiesMenu;
  /// <summary>создать копии</summary>
  private MenuButtonItem mbiCreateCopies;
  /// <summary>выслать...</summary>
  private MenuButtonItem mbiSendCopies;
  /// <summary>выслать</summary>
  private MenuButtonItem mbiSendCopiesFast;
  /// <summary>удалить копии</summary>
  private MenuButtonItem mbiDeleteCopies;
  /// <summary>вернуть копии</summary>
  private MenuButtonItem mbiReturnCopies;
  /// <summary>карточка</summary>
  private MenuButtonItem mbiParametersCard;
  /// <summary>настройки</summary>
  private MenuButtonItem mbiSettings;
  /// <summary>сборосить настройки отображения</summary>
  private MenuButtonItem mbiResetSettings;
  /// <summary>Атрибуты</summary>
  private MenuButtonItem mbiAttributes;
  /// <summary>История значений атрибута</summary>
  private MenuButtonItem mbiAttrHistory;
  /// <summary>Изменить значение атрибута</summary>
  private MenuButtonItem mbiChangeAttrValue;
  /// <summary>Добавить атрибут</summary>
  private MenuButtonItem mbiAddAttr;
  /// <summary>Добавить группу атрибутов</summary>
  private MenuButtonItem mbiAddAttrGroup;
  /// <summary>Удалить атрибут</summary>
  private MenuButtonItem mbiDeleteAttr;
  /// <summary>Удалить группу атрибутов</summary>
  private MenuButtonItem mbiDeleteAttrGroup;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ButtonItem btnCard;
  private ButtonItem btnCreate;
  private ButtonItem btnSendFast;
  private ButtonItem btnReturn;
  private ButtonItem btnDelete;
  private ComboBoxItem cbiFilter;
  private LabelItem liRegisterName;
  private ButtonItem btnRegister;
  private ButtonItem btnUnregister;
  private ButtonItem btnCreateByDeliveryList;
  private ButtonItem btnChangeByDeliveryList;
  private ButtonItem btnSend;

  /// <summary>
  /// Событие возникает, если в редакторе происходят изменения
  /// </summary>
  public event EventHandler OnChanged;

  /// <summary>Сгенерировать событие "OnChanged"</summary>
  protected virtual void RaiseOnChanged()
  {
    EventHandler onChanged = this.OnChanged;
    if (onChanged == null)
      return;
    onChanged((object) this, new EventArgs());
  }

  /// <summary>Версия документа, для которого создаём копии</summary>
  [Browsable(false)]
  public long ObjectID
  {
    get => this._objectID;
    set => this._objectID = value;
  }

  /// <summary>id документа, для которого создаём копии</summary>
  [Browsable(false)]
  public long Id
  {
    get => this._id;
    set => this._id = value;
  }

  /// <summary>Был нужен для нода</summary>
  [Browsable(false)]
  public int TypeID
  {
    get => this._typeID;
    set => this._typeID = value;
  }

  /// <summary>Листы рассылки для документов</summary>
  [Browsable(false)]
  public List<long> DeliveryListIDs
  {
    get => this._deliveryListIDs;
    set => this._deliveryListIDs = value;
  }

  /// <summary>Вкладка открыта для одного выделенного документа</summary>
  [Browsable(false)]
  public bool IsForOneItem
  {
    get => this._isForOneItem;
    set => this._isForOneItem = value;
  }

  /// <summary>
  /// Запрет редактирования
  /// (если документ не поставлен на учёт и лист рассылки ещё не создан)
  /// </summary>
  [Browsable(false)]
  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      this.btnRegister.Visible = this._readOnly;
      this.btnUnregister.Visible = !this._readOnly;
      this.liRegisterName.Visible = this.btnCard.Visible = this.cbiFilter.Visible = this.btnSend.Visible = this.btnSendFast.Visible = this.btnReturn.Visible = this.btnCreate.Visible = this.btnCreateByDeliveryList.Visible = this.btnChangeByDeliveryList.Visible = this.btnDelete.Visible = this.mbiCreateCopies.Enabled = !this._readOnly;
    }
  }

  /// <summary>зарегистрирован ли объект</summary>
  [Browsable(false)]
  public string InventoryNumber
  {
    set
    {
      this.btnRegister.Visible = this._readOnly;
      this.btnUnregister.Visible = !this._readOnly;
      this.liRegisterName.Text = value;
    }
  }

  /// <summary>
  /// Событие возникает в тот момент, когда грид может показать пользовательский фон в ячейке
  /// </summary>
  public event CustomCellBackgroundEventHandler ShowCellCustomBackground;

  /// <summary>Конструктор</summary>
  public CopiesEditorView()
  {
    this.InitializeComponent();
    this._services.RemoveService(typeof (IIODispatcher));
    this._IODispatcher = new IODispatcher();
    this._IODispatcher.RegisterDestination((IIODestination) this);
    this._services.AddService(typeof (IIODispatcher), (object) this._IODispatcher);
    this._hotKeysManager = ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager;
    if (!(ServicesManager.GetService(typeof (IGuidMapper)) is IGuidMapper))
      return;
    this.namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this.CreateMenu();
    this.btnCard.Image = this.namedImageList != null ? this.namedImageList.ImageList.Images[this.namedImageList.ImageIndex("imgCard")] : this.btnCard.Image;
    this.FillComboBox();
    this._services.AddService(typeof (CopiesConditionsProvider), (object) new CopiesConditionsProvider());
    this.cbiFilter.ComboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this.cbiFilter.ComboBox.SelectedIndex = 0;
    this.SelectedItemsChanged += new EventHandler(this.CopiesEditorView_SelectedItemsChanged);
    int num1 = 9;
    ButtonItem btnRegister = this.btnRegister;
    int num2 = num1;
    int num3 = num2 + 1;
    btnRegister.Index = num2;
    LabelItem liRegisterName = this.liRegisterName;
    int num4 = num3;
    int num5 = num4 + 1;
    liRegisterName.Index = num4;
    ButtonItem btnUnregister = this.btnUnregister;
    int num6 = num5;
    int num7 = num6 + 1;
    btnUnregister.Index = num6;
    ButtonItem btnCard = this.btnCard;
    int num8 = num7;
    int num9 = num8 + 1;
    btnCard.Index = num8;
    ButtonItem btnCreate = this.btnCreate;
    int num10 = num9;
    int num11 = num10 + 1;
    btnCreate.Index = num10;
    ButtonItem createByDeliveryList = this.btnCreateByDeliveryList;
    int num12 = num11;
    int num13 = num12 + 1;
    createByDeliveryList.Index = num12;
    ButtonItem changeByDeliveryList = this.btnChangeByDeliveryList;
    int num14 = num13;
    int num15 = num14 + 1;
    changeByDeliveryList.Index = num14;
    ButtonItem btnSend = this.btnSend;
    int num16 = num15;
    int num17 = num16 + 1;
    btnSend.Index = num16;
    ButtonItem btnSendFast = this.btnSendFast;
    int num18 = num17;
    int num19 = num18 + 1;
    btnSendFast.Index = num18;
    ButtonItem btnReturn = this.btnReturn;
    int num20 = num19;
    int num21 = num20 + 1;
    btnReturn.Index = num20;
    ButtonItem btnDelete = this.btnDelete;
    int num22 = num21;
    int num23 = num22 + 1;
    btnDelete.Index = num22;
    this.cbiFilter.Index = num23;
    this.ShowCustomContextMenu += new EventHandler<ContextMenuEventArgs>(this.CopiesEditorView_ShowCustomContextMenu);
    this._useInheritedNavViews = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this._items = items;
    this._serviceProvider = services;
    this._ids = this.GetDocumentsIDs();
    this._copies = this.GetDocumentsCopies();
    this.SetDisplayedCopies();
    if (this.IsForOneItem && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
    {
      this._objectID = itemData.ObjectID;
      this._id = itemData.ID;
    }
    this.Initialize(this.GetDisplayedCopiesDescriptor(), services);
  }

  /// <summary>Читаем сразу все копии</summary>
  protected override int FetchCount => -1;

  /// <summary>
  /// Категория для названия потока с сохранёнными настройками
  /// </summary>
  public override string StateStreamPrefix => "Copies_";

  /// <summary>Тип для названия потока с сохранёнными настройками</summary>
  protected override int StateStreamCategoryType => ConstsHolder.CopyOfDocumentID;

  /// <summary>
  /// попробуем выделить цветом тех пользвоателей, которые не в списке рассылки
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void CustomDrawCellBackground(object sender, iGCustomDrawCellEventArgs e)
  {
    if (this.ShowCellCustomBackground != null)
    {
      iGCell cell = this._grid.Rows[e.RowIndex].Cells[e.ColIndex];
      INodeID nodeIdForRow = this.GetNodeIDForRow(e.RowIndex);
      CustomCellBackgroundEventHandler customBackground = this.ShowCellCustomBackground;
      if (customBackground == null)
        return;
      customBackground((object) this, new CustomCellBackgroundEventArgs(e, this._grid, cell, nodeIdForRow));
    }
    else
      base.CustomDrawCellBackground(sender, e);
  }

  /// <summary>
  /// Поддерживаемые узлом колонки.
  /// Это шаманство связано с тем, что нынче вьюха показывает не содержимое (копии) DocumentNode, а объекты, переданные во вью,  как ListDescriptor.
  /// Что в свою очередь связано с необходимостью отображать объединенные копии для нескольких документов.
  /// </summary>
  /// <returns></returns>
  public override NodeColumnCollection GetSupportedColumns()
  {
    return new DocumentNode(this._typeID, this._objectID).GetSupportedColumns(this.ViewContentType, string.Empty);
  }

  /// <summary>Заполняем комбобокс условиями фильтрации документов</summary>
  private void FillComboBox()
  {
    this.cbiFilter.ComboBox.BeginUpdate();
    try
    {
      this.cbiFilter.ComboBox.Items.Clear();
      this.cbiFilter.ComboBox.Items.Add((object) new MyElement()
      {
        Caption = ServiceHolder.rm.GetString("Archives_100"),
        Value = (object) 0
      });
      this.cbiFilter.ComboBox.Items.Add((object) new MyElement()
      {
        Caption = ServiceHolder.rm.GetString("Archives_101"),
        Value = (object) 1
      });
      this.cbiFilter.ComboBox.Items.Add((object) new MyElement()
      {
        Caption = ServiceHolder.rm.GetString("Archives_102"),
        Value = (object) 2
      });
      this.cbiFilter.ComboBox.Items.Add((object) new MyElement()
      {
        Caption = ServiceHolder.rm.GetString("Archives_103"),
        Value = (object) 3
      });
    }
    finally
    {
      this.cbiFilter.ComboBox.EndUpdate();
    }
  }

  /// <summary>Создать указанное количество копий</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCreate_Click(object sender, EventArgs e)
  {
    using (CreateCopyForm createCopyForm = new CreateCopyForm())
    {
      if (createCopyForm.ShowDialog() != DialogResult.OK)
        return;
      using (SessionKeeper sk = new SessionKeeper())
      {
        if (!(sk.Session.GetCustomService(typeof (IDocumentCopyService)) is IDocumentCopyService customService))
          return;
        try
        {
          DocumentCommandsProvider.CheckUsersRightsForDocRegistration(sk, this._objectID);
          customService.CreateCopies(this._objectID, createCopyForm.copyCount, CopyKind.Hard, (object) sk.Session.SessionGUID);
          this.RefreshEditor();
        }
        catch (AccessDeniedException ex)
        {
          AccessDeniedExceptionForm.OnExceptionHandler((object) null, new ExceptionEventArgs((Exception) ex));
        }
      }
    }
  }

  /// <summary>Команда Выслать...</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnSend_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null || this.SelectedItems.Count == 0)
      return;
    long subscrID = 0;
    List<CopyNodeInfo> copiesForSending = new List<CopyNodeInfo>();
    for (int index = 0; index < this.SelectedItems.Count; ++index)
    {
      if (this.SelectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        CopyNodeInfo copyNode = this.GetCopyNode(itemData.ObjectID);
        if (copyNode != null)
        {
          if (index == 0)
            subscrID = copyNode.SubscriberID;
          if (copyNode.SubscriberID != subscrID)
          {
            int num = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_178"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
          }
          copiesForSending.Add(copyNode);
        }
      }
    }
    if (copiesForSending.Count == 0)
      return;
    if (this.IsForOneItem)
    {
      using (SendCopiesForm sendCopiesForm = new SendCopiesForm(this._copies, copiesForSending, subscrID, this._deliveryListIDs, this._id))
      {
        int num = (int) sendCopiesForm.ShowDialog();
      }
    }
    else
    {
      using (SendCopiesForm sendCopiesForm = new SendCopiesForm(this._copies, copiesForSending, subscrID, this._deliveryListIDs))
      {
        int num = (int) sendCopiesForm.ShowDialog();
      }
    }
    this.RefreshEditor();
    this.SendNotification();
  }

  /// <summary>Кнопка Выслать</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnSendFast_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null || this.SelectedItems.Count == 0)
      return;
    List<long> copiesIds = new List<long>();
    Exception exception;
    using (SessionKeeper sk = new SessionKeeper())
    {
      IDocumentCopyService customService = sk.Session.GetCustomService(typeof (IDocumentCopyService)) as IDocumentCopyService;
      for (int index = 0; index < this.SelectedItems.Count; ++index)
      {
        if (this.SelectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        {
          CopyNodeInfo copyNode = this.GetCopyNode(itemData.ObjectID);
          if (copyNode != null)
          {
            try
            {
              DocumentCommandsProvider.CheckUsersRightsForDocRegistration(sk, copyNode.DocObjectID);
            }
            catch (AccessDeniedException ex)
            {
              AccessDeniedExceptionForm.OnExceptionHandler((object) null, new ExceptionEventArgs((Exception) ex));
              continue;
            }
            copiesIds.Add(itemData.ObjectID);
          }
        }
      }
      exception = customService.CopiesFastSending((object) sk.Session.SessionGUID, copiesIds);
    }
    this.RefreshEditor();
    this.SendNotification();
    if (exception == null)
      return;
    ExceptionHelper.ExceptionService.ShowException(exception);
  }

  /// <summary>Вернуть выбранные копии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnReturn_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null || this.SelectedItems.Count == 0)
      return;
    List<long> copiesID = new List<long>();
    using (SessionKeeper sk = new SessionKeeper())
    {
      for (int index = 0; index < this.SelectedItems.Count; ++index)
      {
        IDBTypedObjectID typedObjectID = this.SelectedItems.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        if (typedObjectID != null)
        {
          CopyNodeInfo copyNodeInfo = this._displayedCopies.Where<CopyNodeInfo>((System.Func<CopyNodeInfo, bool>) (copy => copy.CopyObjectID == typedObjectID.ObjectID)).ToList<CopyNodeInfo>()[0];
          try
          {
            DocumentCommandsProvider.CheckUsersRightsForDocRegistration(sk, copyNodeInfo.DocObjectID);
            copiesID.Add(typedObjectID.ObjectID);
          }
          catch (AccessDeniedException ex)
          {
            AccessDeniedExceptionForm.OnExceptionHandler((object) null, new ExceptionEventArgs((Exception) ex));
          }
        }
      }
    }
    if (copiesID.Count == 0)
      return;
    long copyID = 0;
    if (this.SelectedItems.Count == 1 && this.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      copyID = itemData.ObjectID;
    using (ReturnCopiesForm returnCopiesForm = new ReturnCopiesForm())
    {
      returnCopiesForm.Init(copyID, 0L);
      if (returnCopiesForm.ShowDialog() != DialogResult.OK)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IDocumentCopyService)) is IDocumentCopyService customService)
          customService.ReturnCopies(copiesID, returnCopiesForm.RecID, returnCopiesForm.ReturnDate, (object) sessionKeeper.Session.SessionGUID);
      }
      this.RefreshEditor();
      this.SendNotification();
    }
  }

  /// <summary>Удалить выбранные копии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDelete_Click(object sender, EventArgs e) => this.DeleteCopies();

  /// <summary>Удалить копии.</summary>
  private void DeleteCopies()
  {
    if (this.SelectedItems == null || this.SelectedItems.Count == 0 || !this.CheckAccessRights())
      return;
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("Delete", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
    this.RefreshEditor();
    this.SendNotification();
  }

  /// <summary>Проверка прав доступа.</summary>
  /// <returns>True - если пройдена</returns>
  private bool CheckAccessRights()
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      for (int index = 0; index < this.SelectedItems.Count; ++index)
      {
        if (!(this.SelectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
          return false;
        CopyNodeInfo copyNode = this.GetCopyNode(itemData.ObjectID);
        if (copyNode == null)
          return false;
        try
        {
          DocumentCommandsProvider.CheckUsersRightsForDocRegistration(sk, copyNode.DocObjectID);
        }
        catch (AccessDeniedException ex)
        {
          AccessDeniedExceptionForm.OnExceptionHandler((object) null, new ExceptionEventArgs((Exception) ex));
          return false;
        }
      }
    }
    return true;
  }

  /// <summary>Изменился фильтр</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    MyElement myElement = (MyElement) null;
    if (this.cbiFilter.ComboBox.SelectedIndex >= 0)
      myElement = this.cbiFilter.ComboBox.Items[this.cbiFilter.ComboBox.SelectedIndex] as MyElement;
    if (myElement == null)
      return;
    switch (Convert.ToInt32(myElement.Value))
    {
      case 0:
        this._copyDisplayingMode = "All";
        break;
      case 1:
        this._copyDisplayingMode = "Unsend";
        break;
      case 2:
        this._copyDisplayingMode = "Send";
        break;
      case 3:
        this._copyDisplayingMode = "Returned";
        break;
    }
    this.RefreshEditor();
  }

  /// <summary>
  /// Перечитать содержимое редактора и обновить состояние кнопок
  /// </summary>
  public void RefreshEditor()
  {
    if (this._ids.Count != 0)
    {
      this.ReloadCopies();
      this.SetDisplayedCopies();
      this.Initialize(this.GetDisplayedCopiesDescriptor(), this._serviceProvider);
      this.Activate((IView) null);
    }
    this.ReloadItems();
    this.UpdateControlsStates();
  }

  /// <summary>
  /// Какие копии надо показывать в зависимости от режима комбобокса.
  /// </summary>
  private void SetDisplayedCopies()
  {
    switch (this._copyDisplayingMode)
    {
      case "All":
        this._displayedCopies = new List<CopyNodeInfo>((IEnumerable<CopyNodeInfo>) this._copies);
        break;
      case "Unsend":
        this._displayedCopies = this._copies.Where<CopyNodeInfo>((System.Func<CopyNodeInfo, bool>) (copy => copy.LСStepID == ConstsHolder.CreateLCStepID)).ToList<CopyNodeInfo>();
        break;
      case "Send":
        this._displayedCopies = this._copies.Where<CopyNodeInfo>((System.Func<CopyNodeInfo, bool>) (copy => copy.LСStepID == ConstsHolder.SendLCStepID)).ToList<CopyNodeInfo>();
        break;
      case "Returned":
        this._displayedCopies = this._copies.Where<CopyNodeInfo>((System.Func<CopyNodeInfo, bool>) (copy => copy.LСStepID == ConstsHolder.ReturnLCStepID)).ToList<CopyNodeInfo>();
        break;
    }
  }

  /// <summary>Получает дескриптор для списка копий.</summary>
  /// <returns>Дескриптор для списка копий</returns>
  private IDescriptor GetDisplayedCopiesDescriptor()
  {
    List<long> list = this._displayedCopies.Select<CopyNodeInfo, long>((System.Func<CopyNodeInfo, long>) (copy => copy.CopyObjectID)).ToList<long>();
    return (IDescriptor) new ListDescriptor(4, ConstsHolder.CopyOfDocumentID, string.Empty, (IList) list);
  }

  /// <summary>Получает список ИД документов</summary>
  private List<long> GetDocumentsIDs()
  {
    List<long> documentsIds = new List<long>();
    for (int index = 0; index < this._items.Count; ++index)
    {
      if (this._items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        long id = itemData.ID;
        if (!documentsIds.Contains(id))
          documentsIds.Add(id);
      }
    }
    return documentsIds;
  }

  /// <summary>Получает список копий выделенных в гриде документов.</summary>
  /// <returns>Список копий.</returns>
  private List<CopyNodeInfo> GetDocumentsCopies()
  {
    List<CopyNodeInfo> documentsCopies = new List<CopyNodeInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.In, (object) this._ids.ToArray(), LogicalOperators.AND, 0, false)
      }, new ColumnDescriptor[6]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ConstsHolder.OriginalObjectVersionID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ConstsHolder.OriginalObjectID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ConstsHolder.AlbumSubscriberID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 2)
      });
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.ObjectsSelect(ConstsHolder.CopyOfDocumentID, dbRecordSetParams).Rows)
      {
        long int64_1 = Convert.ToInt64(row[-2.ToString()]);
        long int64_2 = Convert.ToInt64(row[ConstsHolder.OriginalObjectID.ToString()]);
        long int64_3 = Convert.ToInt64(row[ConstsHolder.OriginalObjectVersionID.ToString()]);
        DataRow dataRow1 = row;
        int num = -50;
        string columnName1 = num.ToString();
        string caption = Convert.ToString(dataRow1[columnName1]);
        DataRow dataRow2 = row;
        num = -4;
        string columnName2 = num.ToString();
        int int32 = Convert.ToInt32(dataRow2[columnName2]);
        long subscriberID;
        switch (row[ConstsHolder.AlbumSubscriberID.ToString()])
        {
          case null:
          case DBNull _:
            subscriberID = 0L;
            break;
          default:
            subscriberID = Convert.ToInt64(row[ConstsHolder.AlbumSubscriberID.ToString()]);
            break;
        }
        documentsCopies.Add(new CopyNodeInfo(int64_1, int64_2, int64_3, subscriberID, int32, caption));
      }
    }
    return documentsCopies;
  }

  /// <summary>
  /// Перечитывает информацию о копиях выделенных документов.
  /// </summary>
  private void ReloadCopies() => this._copies = this.GetDocumentsCopies();

  /// <summary>Обновить доступность кнопок</summary>
  public void UpdateControlsStates()
  {
    this.btnRegister.Visible = this._readOnly && this.IsForOneItem;
    ButtonItem btnUnregister = this.btnUnregister;
    ButtonItem btnCreate = this.btnCreate;
    MenuButtonItem mbiCreateCopies = this.mbiCreateCopies;
    bool flag1;
    this.liRegisterName.Visible = flag1 = !this._readOnly && this.IsForOneItem;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    mbiCreateCopies.Enabled = num1 != 0;
    int num2;
    bool flag3 = (num2 = flag2 ? 1 : 0) != 0;
    btnCreate.Visible = num2 != 0;
    int num3 = flag3 ? 1 : 0;
    btnUnregister.Visible = num3 != 0;
    this.btnCard.Visible = this.cbiFilter.Visible = this.btnSendFast.Visible = this.btnSend.Visible = this.btnReturn.Visible = this.btnCreateByDeliveryList.Visible = this.btnChangeByDeliveryList.Visible = this.btnDelete.Visible = !this._readOnly;
    if (this.SelectedItems == null || this.SelectedItems.Count == 0)
    {
      this.btnDelete.Enabled = this.btnSendFast.Enabled = this.btnSend.Enabled = this.btnReturn.Enabled = this.btnCard.Enabled = false;
      this.mbiDeleteCopies.Visible = this.mbiSendCopies.Visible = this.mbiSendCopiesFast.Visible = this.mbiReturnCopies.Visible = this.mbiParametersCard.Visible = false;
    }
    else
    {
      bool flag4 = true;
      bool flag5 = true;
      Dictionary<long, long> dictionary = new Dictionary<long, long>();
      for (int index = 0; index < this.SelectedItems.Count; ++index)
      {
        if (this.SelectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        {
          CopyNodeInfo copyNode = this.GetCopyNode(itemData.ObjectID);
          if (copyNode != null)
          {
            if (dictionary.ContainsKey(copyNode.DocID) && !dictionary.ContainsValue(copyNode.DocObjectID))
            {
              flag5 = false;
            }
            else
            {
              if (!dictionary.ContainsValue(copyNode.DocObjectID))
                dictionary.Add(copyNode.DocID, copyNode.DocObjectID);
              if (flag5)
                flag5 = copyNode.LСStepID == ConstsHolder.CreateLCStepID;
            }
            if (flag4)
              flag4 = copyNode.LСStepID == ConstsHolder.SendLCStepID;
          }
        }
      }
      this.btnSendFast.Enabled = this.mbiSendCopies.Visible = this.btnSend.Enabled = this.mbiSendCopiesFast.Visible = flag5;
      this.btnReturn.Enabled = this.mbiReturnCopies.Visible = flag4;
      this.btnDelete.Enabled = this.mbiDeleteCopies.Visible = true;
      this.btnCard.Enabled = this.mbiParametersCard.Visible = this.SelectedItems.Count == 1;
    }
  }

  /// <summary>Возвращает из списка копий объект копии по ИД</summary>
  /// <param name="copyID">Объект копии</param>
  /// <returns></returns>
  public CopyNodeInfo GetCopyNode(long copyID)
  {
    CopyNodeInfo copyNode = (CopyNodeInfo) null;
    foreach (CopyNodeInfo copy in this._copies)
    {
      if (copy.CopyObjectID == copyID)
        copyNode = copy;
    }
    return copyNode;
  }

  /// <summary>Сообщить об изменениях с копиями</summary>
  public void SendNotification()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.FireEvent((object) null, new NotificationEventArgs("CopiesChanged"));
  }

  /// <summary>изменилась выделенная строка в гриде</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CopiesEditorView_SelectedItemsChanged(object sender, EventArgs e)
  {
    this.UpdateControlsStates();
  }

  /// <summary>Регистрация копии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnRegister_Click(object sender, EventArgs e)
  {
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    DocumentCommandsProvider.SetInventoryNumber(this._items, (System.IServiceProvider) viewServices, (object) null);
    this.RaiseOnChanged();
  }

  /// <summary>Нажатие кнопки "Снять документ с регистрации"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnUnregister_Click(object sender, EventArgs e)
  {
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("DeleteInventoryNumber", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this._items, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
    this.RaiseOnChanged();
  }

  /// <summary>Нажатие кнопки "Создать копии по листу рассылки"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnCreateByDeliveryList_Click(object sender, EventArgs e)
  {
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("CreateCopiesByDeliveryList", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this._items, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  /// <summary>
  /// Заменить по листу рассылки.
  /// Делает то же, что создать по листу рассылки, но не учитывает высланные копии при подсчете недостающего количества копий для абонента
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnChangeByDeliveryList_Click(object sender, EventArgs e)
  {
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("ChangeCopiesByDeliveryList", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this._items, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  /// <summary>Карточка копии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCard_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null || this.SelectedItems.Count != 1)
      return;
    this.InvokeParametersCard();
  }

  /// <summary>создание контекстного меню</summary>
  private void CreateMenu()
  {
    this.copiesMenu = ServiceHolder.BarManager.MenuBar.AddMenuBar(ServiceHolder.rm.GetString("Archives_99"));
    this.copiesMenu.Visible = false;
    this.mbiParametersCard = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_104"), new EventHandler(this.btnCard_Click));
    this.mbiParametersCard.ImageIndex = this.namedImageList.ImageIndex("imgCard");
    this.mbiParametersCard.Shortcut = Shortcut.F4;
    this.mbiCreateCopies = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_105"), new EventHandler(this.btnCreate_Click));
    this.mbiCreateCopies.BeginGroup = true;
    this.mbiSendCopies = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_106"), new EventHandler(this.btnSend_Click));
    this.mbiSendCopiesFast = new MenuButtonItem(ServiceHolder.rm.GetString("SendFast"), new EventHandler(this.btnSendFast_Click));
    this.mbiReturnCopies = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_107"), new EventHandler(this.btnReturn_Click));
    this.mbiDeleteCopies = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_108"), new EventHandler(this.btnDelete_Click));
    this.mbiDeleteCopies.Shortcut = Shortcut.CtrlDel;
    this.mbiSettings = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_109"), new EventHandler(this.SettingsClick));
    this.mbiSettings.BeginGroup = true;
    this.mbiSettings.ImageIndex = this.namedImageList.ImageIndex("imgViewSettings");
    this.mbiResetSettings = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_110"), new EventHandler(this.ResetSettingsClick));
    this.mbiAddAttr = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_193"), new EventHandler(this.btnAddAttr_Click));
    this.mbiAddAttrGroup = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_194"), new EventHandler(this.btnAddAttrGroup_Click));
    this.mbiAttrHistory = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_195"), new EventHandler(this.btnAttrHistory_Click));
    this.mbiChangeAttrValue = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_196"), new EventHandler(this.btnChangeAttrValue_Click));
    this.mbiDeleteAttr = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_197"), new EventHandler(this.btnDeleteAttr_Click));
    this.mbiDeleteAttrGroup = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_198"), new EventHandler(this.btnDeleteAttrGroupClick));
    this.mbiAttributes = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_192"));
    this.mbiAttributes.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[6]
    {
      this.mbiAddAttr,
      this.mbiAddAttrGroup,
      this.mbiAttrHistory,
      this.mbiChangeAttrValue,
      this.mbiDeleteAttr,
      this.mbiDeleteAttrGroup
    });
    this.copiesMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[9]
    {
      this.mbiParametersCard,
      this.mbiCreateCopies,
      this.mbiSendCopiesFast,
      this.mbiSendCopies,
      this.mbiReturnCopies,
      this.mbiDeleteCopies,
      this.mbiAttributes,
      this.mbiSettings,
      this.mbiResetSettings
    });
  }

  /// <summary>Удалить группу атрибутов.</summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void btnDeleteAttrGroupClick(object sender, EventArgs e)
  {
    if (this.SelectedItems == null)
      return;
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("DeleteAttributeGroup", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  /// <summary>Удалить атрибут.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void btnDeleteAttr_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null)
      return;
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("DeleteAttribute", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  /// <summary>Изменить значение атрибута.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void btnChangeAttrValue_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null)
      return;
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("EditAttributeValue", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  /// <summary>История значений атрибута.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void btnAttrHistory_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null)
      return;
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("ShowAttributeHistory", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  /// <summary>Добавить группу атрибутов</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void btnAddAttrGroup_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null)
      return;
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("AddAttributeGroupAddAttribute", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  /// <summary>Добавить атрибут.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnAddAttr_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems == null)
      return;
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("AddAttribute", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  /// <summary>показываем  меню</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CopiesEditorView_ShowCustomContextMenu(object sender, ContextMenuEventArgs e)
  {
    this.copiesMenu.Show((System.Windows.Forms.Control) this, e.Location);
  }

  /// <summary>Настройки отображения</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SettingsClick(object sender, EventArgs e)
  {
    this.ChangeGridColumnsMenuButtonItem_Click(sender, e);
  }

  /// <summary>Сбросить настройки отображения</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ResetSettingsClick(object sender, EventArgs e)
  {
    this.ResetColumnsCommand(sender, e);
  }

  /// <summary>Вызов стандартной команды Показать карточку.</summary>
  private void InvokeParametersCard()
  {
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("ParametersCard", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices), (System.IServiceProvider) viewServices);
  }

  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyDown | IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  public bool ProcessEvent(IIOEvent Event)
  {
    if (Event.EventType == IOEventType.evMouseDoubleClick)
    {
      this.InvokeParametersCard();
      return false;
    }
    List<IHotKeysCommand> hotKeysCommandList = this._hotKeysManager[((KeyEventArgs) Event.EventData).KeyCode | ((KeyEventArgs) Event.EventData).Modifiers];
    if (hotKeysCommandList != null && hotKeysCommandList.Count > 0)
    {
      foreach (IHotKeysCommand hotKeysCommand in hotKeysCommandList)
      {
        if (hotKeysCommand.Command == "ParametersCard")
        {
          ((KeyEventArgs) Event.EventData).Handled = true;
          this.InvokeParametersCard();
        }
        if (hotKeysCommand.Command == "Delete")
        {
          ((KeyEventArgs) Event.EventData).Handled = true;
          this.DeleteCopies();
        }
      }
    }
    return false;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.copiesMenu != null)
      {
        this.copiesMenu.Detach();
        this.copiesMenu.Dispose();
        this.copiesMenu = (MenuBarItem) null;
      }
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CopiesEditorView));
    this.btnCard = new ButtonItem();
    this.btnCreate = new ButtonItem();
    this.btnSendFast = new ButtonItem();
    this.btnReturn = new ButtonItem();
    this.btnDelete = new ButtonItem();
    this.cbiFilter = new ComboBoxItem();
    this.liRegisterName = new LabelItem();
    this.btnRegister = new ButtonItem();
    this.btnUnregister = new ButtonItem();
    this.btnCreateByDeliveryList = new ButtonItem();
    this.btnChangeByDeliveryList = new ButtonItem();
    this.btnSend = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[12]
    {
      (ToolbarItemBase) this.btnRegister,
      (ToolbarItemBase) this.btnUnregister,
      (ToolbarItemBase) this.liRegisterName,
      (ToolbarItemBase) this.btnCard,
      (ToolbarItemBase) this.btnCreate,
      (ToolbarItemBase) this.btnCreateByDeliveryList,
      (ToolbarItemBase) this.btnChangeByDeliveryList,
      (ToolbarItemBase) this.btnSend,
      (ToolbarItemBase) this.btnSendFast,
      (ToolbarItemBase) this.btnReturn,
      (ToolbarItemBase) this.btnDelete,
      (ToolbarItemBase) this.cbiFilter
    });
    componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
    this._toggleManualSortingButtonItem.Visible = false;
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("_grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("_grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "_pageViewsManager");
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this._manualSortingSetupButtonItem.Visible = false;
    this._refreshButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "_gridHeaderMenuBar");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this.btnCard.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnCard, "btnCard");
    this.btnCard.Enabled = false;
    this.btnCard.Click += new EventHandler(this.btnCard_Click);
    this.btnCreate.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnCreate, "btnCreate");
    this.btnCreate.Icon = (Icon) componentResourceManager.GetObject("btnCreate.Icon");
    this.btnCreate.Image = (Image) componentResourceManager.GetObject("btnCreate.Image");
    this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
    componentResourceManager.ApplyResources((object) this.btnSendFast, "btnSendFast");
    this.btnSendFast.Enabled = false;
    this.btnSendFast.Icon = (Icon) componentResourceManager.GetObject("btnSendFast.Icon");
    this.btnSendFast.Image = (Image) componentResourceManager.GetObject("btnSendFast.Image");
    this.btnSendFast.Click += new EventHandler(this.btnSendFast_Click);
    componentResourceManager.ApplyResources((object) this.btnReturn, "btnReturn");
    this.btnReturn.Enabled = false;
    this.btnReturn.Icon = (Icon) componentResourceManager.GetObject("btnReturn.Icon");
    this.btnReturn.Image = (Image) componentResourceManager.GetObject("btnReturn.Image");
    this.btnReturn.Click += new EventHandler(this.btnReturn_Click);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Enabled = false;
    this.btnDelete.Icon = (Icon) componentResourceManager.GetObject("btnDelete.Icon");
    this.btnDelete.Image = (Image) componentResourceManager.GetObject("btnDelete.Image");
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.cbiFilter.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.cbiFilter, "cbiFilter");
    this.cbiFilter.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbiFilter.MinimumControlWidth = 50;
    this.cbiFilter.Padding.Bottom = 0;
    this.cbiFilter.Padding.Left = 1;
    this.cbiFilter.Padding.Right = 1;
    this.cbiFilter.Padding.Top = 0;
    this.cbiFilter.Stretch = true;
    componentResourceManager.ApplyResources((object) this.liRegisterName, "liRegisterName");
    this.liRegisterName.Visible = false;
    this.btnRegister.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnRegister, "btnRegister");
    this.btnRegister.Icon = (Icon) componentResourceManager.GetObject("btnRegister.Icon");
    this.btnRegister.Image = (Image) componentResourceManager.GetObject("btnRegister.Image");
    this.btnRegister.Click += new EventHandler(this.btnRegister_Click);
    this.btnUnregister.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnUnregister, "btnUnregister");
    this.btnUnregister.Icon = (Icon) componentResourceManager.GetObject("btnUnregister.Icon");
    this.btnUnregister.Image = (Image) componentResourceManager.GetObject("btnUnregister.Image");
    this.btnUnregister.Click += new EventHandler(this.btnUnregister_Click);
    this.btnCreateByDeliveryList.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnCreateByDeliveryList, "btnCreateByDeliveryList");
    this.btnCreateByDeliveryList.Icon = (Icon) componentResourceManager.GetObject("btnCreateByDeliveryList.Icon");
    this.btnCreateByDeliveryList.Image = (Image) componentResourceManager.GetObject("btnCreateByDeliveryList.Image");
    this.btnCreateByDeliveryList.Click += new EventHandler(this.btnCreateByDeliveryList_Click);
    this.btnChangeByDeliveryList.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnChangeByDeliveryList, "btnChangeByDeliveryList");
    this.btnChangeByDeliveryList.Icon = (Icon) componentResourceManager.GetObject("btnChangeByDeliveryList.Icon");
    this.btnChangeByDeliveryList.Image = (Image) componentResourceManager.GetObject("btnChangeByDeliveryList.Image");
    this.btnChangeByDeliveryList.Click += new EventHandler(this.btnChangeByDeliveryList_Click);
    this.btnSend.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnSend, "btnSend");
    this.btnSend.Icon = (Icon) componentResourceManager.GetObject("btnSend.Icon");
    this.btnSend.Image = (Image) componentResourceManager.GetObject("btnSend.Image");
    this.btnSend.Click += new EventHandler(this.btnSend_Click);
    this.DisableCheckedOutColumn = true;
    this.DisableFiltration = true;
    this.DisableHeaderContextMenu = true;
    this.DisableIMContextMenu = true;
    this.DisableStatusBar = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (CopiesEditorView);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
