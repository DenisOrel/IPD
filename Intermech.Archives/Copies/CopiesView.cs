// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopiesView
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Archives.Copies.Subscribers;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Copies;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Закладка Копии документа</summary>
[ViewDescriptionProvider(typeof (CopiesView.CopiesViewDescriptionProvider))]
public class CopiesView : UserControl, IView
{
  /// <summary>Выделенные итемы в навигаторе</summary>
  private ISelectedItems _items;
  private System.IServiceProvider _serviceProvider;
  /// <summary>
  /// id выбранной версии документа
  /// нужен для работы с листом рассылки. заполняется только при одном выделенном документе.
  /// </summary>
  private long _objectID;
  /// <summary>Список ИД выбранных документов (не версий).</summary>
  private readonly List<long> _ids = new List<long>();
  /// <summary>
  /// Список листов рассылки для выбранных документов
  /// Для работы с вкладкой Лист рассылки всегда используется _deliveryListIDs[0], т.к. она работает только для одного документа
  /// </summary>
  private readonly List<long> _deliveryListIDs = new List<long>();
  /// <summary>Список абонентов в листе рассылки</summary>
  private readonly List<long> _subscribersID = new List<long>();
  /// <summary>Иконка для закладки</summary>
  private int _imageIndex = -1;
  /// <summary>Сервис уведомлений</summary>
  private readonly INotificationService _notifyService;
  /// <summary>Отображается ли в данный момент вьюшка на экране</summary>
  private bool _isViewActivated;
  /// <summary>Вьюшка  отображается для одного итема(документа)</summary>
  private bool _isViewForOneItem;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PageControl pcDocumentInformation;
  private Intermech.Docking.TabPage tpSubscribers;
  private Button btnCancel;
  private Button btnOK;
  private AddSubscriberControl addSubscribers;
  private ImageList ilCopies;
  private Intermech.Docking.TabPage tpCopies;
  private CopiesEditorView copiesEditorView1;
  private Panel panel1;

  /// <summary>Обработка события создания объекта</summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:Intermech.Interfaces.Client.NotificationEventArgs" /> instance containing the event data.</param>
  private void ObjectsCreatedEventHandler(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs))
      return;
    for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
    {
      if (objectsEventArgs.ObjectTypeIDs[index] == ConstsHolder.DeliveryListID)
      {
        long objectId = objectsEventArgs.ObjectIDs[index];
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (this._ids.Contains(Convert.ToInt64(sessionKeeper.Session.GetObjectAttributeByID(objectId, ConstsHolder.OriginalObjectID).Value)))
            this.LoadInformation();
        }
      }
    }
  }

  /// <summary>Обработка события изменения объекта.</summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:Intermech.Interfaces.Client.NotificationEventArgs" /> instance containing the event data.</param>
  private void ObjectsChangedEventHandler(object sender, NotificationEventArgs e)
  {
    if (!this._isViewActivated || !(e is DBObjectsExtendedEventArgs extendedEventArgs))
      return;
    if (extendedEventArgs.ObjectIDs.Contains(this._objectID))
    {
      foreach (AttributeValues attributeValues in extendedEventArgs.AttributeValuesArray)
      {
        if (attributeValues.AttributeID == ConstsHolder.InventoryNumberID && (attributeValues.Values[0] == null || attributeValues.Values[0].ToString() == string.Empty))
          this.LoadInformation();
      }
    }
    if (!extendedEventArgs.ObjectIDs.Contains(this._deliveryListIDs[0]) || sender is AddSubscriberControl subscriberControl && subscriberControl.OwnerType == OwnerType.Control || !this._isViewForOneItem)
      return;
    this.addSubscribers.RefreshEditor();
  }

  /// <summary>Обработчик события изменения вьюшки "Список копий"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void copiesEditorView1_OnChanged(object sender, EventArgs e)
  {
    if (!this._isViewActivated)
      return;
    this.LoadInformation();
  }

  /// <summary>изменения на закладке Лист рассылки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void addSubscribers_OnChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Сохранить изменения на закладке Лист рассылки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOK_Click(object sender, EventArgs e) => this.addSubscribers.Save();

  /// <summary>Отменить изменения на закладке Лист рассылки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e) => this.addSubscribers.Cancel();

  /// <summary>изменяется страница</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void pcDocumentInformation_SelectedPageChanging(
    object sender,
    PageControlCancelEventArgs e)
  {
    if (e.TabIndex == 1)
    {
      if (this.addSubscribers.IsChanged)
      {
        switch (IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), ServiceHolder.rm.GetString("Archives_112"), MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question))
        {
          case DialogResult.Yes:
            this.addSubscribers.Save();
            return;
          case DialogResult.No:
            this.addSubscribers.Cancel();
            break;
          default:
            e.Cancel = true;
            return;
        }
      }
      this.copiesEditorView1.RefreshEditor();
    }
    if (e.TabIndex != 0 || !this._isViewForOneItem)
      return;
    this.addSubscribers.RefreshEditor();
  }

  /// <summary>
  ///  выделение фоном для копий, которые высланы пользователям не из листа рассылки
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void copiesEditorView1_ShowCellCustomBackground(
    object sender,
    CustomCellBackgroundEventArgs e)
  {
    if (!this.copiesEditorView1.IsForOneItem || e.NodeID == null || !(e.NodeID is NodeID nodeId))
      return;
    CopyNodeInfo copyNode = this.copiesEditorView1.GetCopyNode(nodeId.ObjectID);
    if (copyNode == null || copyNode.LСStepID != ConstsHolder.SendLCStepID || this._subscribersID.Contains(copyNode.SubscriberID))
      return;
    Color aquamarine = Color.Aquamarine;
    Color lightBlue = Color.LightBlue;
    Rectangle bounds = e.Cell.Bounds;
    LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, aquamarine, lightBlue, LinearGradientMode.Vertical);
    try
    {
      e.DrawArgs.Graphics.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    finally
    {
      linearGradientBrush.Dispose();
    }
  }

  /// <summary>Конструктор</summary>
  public CopiesView()
  {
    this.copiesEditorView1 = new CopiesEditorView();
    this.InitializeComponent();
    this._notifyService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
  }

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._items = items;
    this._serviceProvider = provider;
    this._isViewForOneItem = items.Count == 1;
    if (this._isViewForOneItem && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1)
      this._objectID = itemData1.ObjectID;
    this._ids.Clear();
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2)
      {
        long id = itemData2.ID;
        if (!this._ids.Contains(id))
          this._ids.Add(id);
      }
    }
    this.copiesEditorView1.OnChanged -= new EventHandler(this.copiesEditorView1_OnChanged);
    this.copiesEditorView1.OnChanged += new EventHandler(this.copiesEditorView1_OnChanged);
    if (!(ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    this._imageIndex = service.ImageIndex("imgDocCopies");
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">
  /// Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.
  /// </param>
  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    if (this._notifyService != null)
    {
      this._notifyService.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsChangedEventHandler));
      this._notifyService.Subscribe("CopiesChanged", new NotificationEventHandler(this.CopiesChanged));
      this._notifyService.Subscribe("ObjectsCreated", new NotificationEventHandler(this.ObjectsCreatedEventHandler));
    }
    this.LoadInformation();
    this._isViewActivated = true;
  }

  private void CopiesChanged(object sender, NotificationEventArgs e)
  {
    if (!this._isViewActivated)
      return;
    this.LoadInformation();
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">
  /// Закладка, на которую осуществляется переключение. Может быть null, если выполняется
  /// не переключение, а удаление закладок.
  /// </param>
  public void Deactivate(IView nextView)
  {
    if (this.addSubscribers.IsChanged)
    {
      if (IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), ServiceHolder.rm.GetString("Archives_112"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
        this.addSubscribers.Save();
      else
        this.addSubscribers.Cancel();
    }
    this.copiesEditorView1.Deactivate(nextView);
    if (this._notifyService != null)
    {
      this._notifyService.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsChangedEventHandler));
      this._notifyService.Unsubscribe("ObjectsCreated", new NotificationEventHandler(this.ObjectsCreatedEventHandler));
      this._notifyService.Unsubscribe("CopiesChanged", new NotificationEventHandler(this.CopiesChanged));
    }
    this._isViewActivated = false;
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  public string Caption => ServiceHolder.rm.GetString("Archives_99");

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  public int ImageIndex => this._imageIndex;

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  public int OrderID => 27;

  /// <summary>Загружаем информацию</summary>
  public void LoadInformation()
  {
    this.copiesEditorView1.IsForOneItem = this._isViewForOneItem;
    this.copiesEditorView1.Initialize(this._items, this._serviceProvider);
    this.addSubscribers.Initialize(this._items, this._serviceProvider);
    this.addSubscribers.Visible = this._isViewForOneItem;
    this.addSubscribers.Enabled = this._isViewForOneItem;
    this._deliveryListIDs.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
        return;
      foreach (long id in this._ids)
        this._deliveryListIDs.Add(customService.GetDeliveryListID(sessionKeeper.Session.SessionGUID, id));
    }
    if (this._isViewForOneItem)
    {
      this.ShowInventoryNumber();
      if (ConstsHolder.DeliveryListID != 0 && ConstsHolder.OriginalObjectID != -10000)
      {
        this.addSubscribers.ObjectID = this._objectID;
        this.addSubscribers.ID = this._ids[0];
        this.addSubscribers.LoadSubscribers(new List<long>()
        {
          this._deliveryListIDs[0]
        }, OwnerType.Control, false);
      }
    }
    else
      this.copiesEditorView1.ReadOnly = false;
    this.copiesEditorView1.DeliveryListIDs = this._deliveryListIDs;
    this.copiesEditorView1.Activate((IView) null);
    this.UpdateControls();
  }

  /// <summary>
  /// Показывает инвентарный номер выделенного документа на вкладке Копии.
  /// </summary>
  private void ShowInventoryNumber()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._objectID, ConstsHolder.InventoryNumberID);
      if (objectAttributeById == null || objectAttributeById.Value == DBNull.Value || objectAttributeById.AsString == string.Empty)
      {
        this.addSubscribers.ReadOnly = this.copiesEditorView1.ReadOnly = true;
      }
      else
      {
        this.addSubscribers.ReadOnly = this.copiesEditorView1.ReadOnly = false;
        this.copiesEditorView1.InventoryNumber = objectAttributeById.AsString;
      }
    }
  }

  /// <summary>Перечитать список абонентов в листе рассылки</summary>
  private void SubscribersListReread()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._subscribersID.Clear();
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._deliveryListIDs[0], ConstsHolder.SubscribersID);
      if (objectAttributeById == null)
        return;
      foreach (object obj in objectAttributeById.Values)
      {
        if (obj != DBNull.Value)
        {
          long int64 = Convert.ToInt64(obj);
          if (!this._subscribersID.Contains(int64))
            this._subscribersID.Add(int64);
        }
      }
    }
  }

  /// <summary>Обновить контролы</summary>
  private void UpdateControls()
  {
    this.btnOK.Enabled = this.addSubscribers.IsChanged;
    this.btnCancel.Enabled = this.addSubscribers.IsChanged;
    this.copiesEditorView1.RefreshEditor();
    if (!this._isViewForOneItem)
      return;
    this.SubscribersListReread();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (this._notifyService != null)
    {
      this._notifyService.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsChangedEventHandler));
      this._notifyService.Unsubscribe("ObjectsCreated", new NotificationEventHandler(this.ObjectsCreatedEventHandler));
    }
    if (this.copiesEditorView1 != null)
      this.copiesEditorView1.OnChanged -= new EventHandler(this.copiesEditorView1_OnChanged);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CopiesView));
    this.pcDocumentInformation = new PageControl();
    this.tpCopies = new Intermech.Docking.TabPage();
    this.tpSubscribers = new Intermech.Docking.TabPage();
    this.addSubscribers = new AddSubscriberControl();
    this.panel1 = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.ilCopies = new ImageList(this.components);
    this.pcDocumentInformation.SuspendLayout();
    this.tpCopies.SuspendLayout();
    this.tpSubscribers.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.pcDocumentInformation.AccessibleDescription = (string) null;
    this.pcDocumentInformation.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.pcDocumentInformation, "pcDocumentInformation");
    this.pcDocumentInformation.BackgroundImage = (Image) null;
    this.pcDocumentInformation.Controls.Add((Control) this.tpCopies);
    this.pcDocumentInformation.Controls.Add((Control) this.tpSubscribers);
    this.pcDocumentInformation.Font = (Font) null;
    this.pcDocumentInformation.ImageList = this.ilCopies;
    this.pcDocumentInformation.Name = "pcDocumentInformation";
    this.pcDocumentInformation.SelectedPageChanging += new PageControlCancelEventHandler(this.pcDocumentInformation_SelectedPageChanging);
    this.tpCopies.AccessibleDescription = (string) null;
    this.tpCopies.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.tpCopies, "tpCopies");
    this.tpCopies.BackgroundImage = (Image) null;
    this.tpCopies.Controls.Add((Control) this.copiesEditorView1);
    this.tpCopies.Font = (Font) null;
    this.tpCopies.Index = 0;
    this.tpCopies.Name = "tpCopies";
    this.tpCopies.TabImage = (Image) componentResourceManager.GetObject("tpCopies.TabImage");
    this.copiesEditorView1.AccessibleDescription = (string) null;
    this.copiesEditorView1.AccessibleName = (string) null;
    this.copiesEditorView1.AllowCustomGroupValues = true;
    componentResourceManager.ApplyResources((object) this.copiesEditorView1, "copiesEditorView1");
    this.copiesEditorView1.BackgroundImage = (Image) null;
    this.copiesEditorView1.Control = (object) this.copiesEditorView1;
    this.copiesEditorView1.DisableCheckedOutColumn = true;
    this.copiesEditorView1.DisableFiltration = true;
    this.copiesEditorView1.DisableHeaderContextMenu = true;
    this.copiesEditorView1.DisableIMContextMenu = true;
    this.copiesEditorView1.DisableKeyDownEvents = false;
    this.copiesEditorView1.DisableStatusBar = true;
    this.copiesEditorView1.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.copiesEditorView1.Id = 0L;
    this.copiesEditorView1.Name = "copiesEditorView1";
    this.copiesEditorView1.ObjectID = 0L;
    this.copiesEditorView1.ReadOnly = true;
    this.copiesEditorView1.TypeID = -1;
    this.copiesEditorView1.ShowCellCustomBackground += new CustomCellBackgroundEventHandler(this.copiesEditorView1_ShowCellCustomBackground);
    this.tpSubscribers.AccessibleDescription = (string) null;
    this.tpSubscribers.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.tpSubscribers, "tpSubscribers");
    this.tpSubscribers.BackgroundImage = (Image) null;
    this.tpSubscribers.Controls.Add((Control) this.addSubscribers);
    this.tpSubscribers.Controls.Add((Control) this.panel1);
    this.tpSubscribers.Font = (Font) null;
    this.tpSubscribers.Index = 1;
    this.tpSubscribers.Name = "tpSubscribers";
    this.tpSubscribers.TabImage = (Image) componentResourceManager.GetObject("tpSubscribers.TabImage");
    this.addSubscribers.AccessibleDescription = (string) null;
    this.addSubscribers.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.addSubscribers, "addSubscribers");
    this.addSubscribers.BackgroundImage = (Image) null;
    this.addSubscribers.Font = (Font) null;
    this.addSubscribers.ID = 0L;
    this.addSubscribers.IsChanged = false;
    this.addSubscribers.Name = "addSubscribers";
    this.addSubscribers.ObjectID = 0L;
    this.addSubscribers.OnChanged += new AddSubscriberControl.ObjectOptionsChangedEventHandler(this.addSubscribers_OnChanged);
    this.panel1.AccessibleDescription = (string) null;
    this.panel1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.BackgroundImage = (Image) null;
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Font = (Font) null;
    this.panel1.Name = "panel1";
    this.btnOK.AccessibleDescription = (string) null;
    this.btnOK.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.BackgroundImage = (Image) null;
    this.btnOK.Font = (Font) null;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.AccessibleDescription = (string) null;
    this.btnCancel.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.BackgroundImage = (Image) null;
    this.btnCancel.Font = (Font) null;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.ilCopies.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilCopies.ImageStream");
    this.ilCopies.TransparentColor = Color.Transparent;
    this.ilCopies.Images.SetKeyName(0, "uvedomlenija_2.ico");
    this.ilCopies.Images.SetKeyName(1, "docs_copy1.ico");
    this.ilCopies.Images.SetKeyName(2, "docs_copy1_alb.ico");
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.pcDocumentInformation);
    this.Name = nameof (CopiesView);
    this.pcDocumentInformation.ResumeLayout(false);
    this.tpCopies.ResumeLayout(false);
    this.tpSubscribers.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class CopiesViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
        service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      INamedImageList namedImageList = service;
      return new ViewDescription()
      {
        Caption = ServiceHolder.rm.GetString("Archives_99"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgDocCopies") : -1,
        OrderID = 27
      };
    }
  }
}
