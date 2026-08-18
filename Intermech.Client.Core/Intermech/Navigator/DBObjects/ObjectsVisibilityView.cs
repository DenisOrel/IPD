
// Type: Intermech.Navigator.DBObjects.ObjectsVisibilityView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.VirtualTreeView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Вьюшка для управления видимостью объектов</summary>
[ViewDescriptionProvider(typeof (ObjectsVisibilityView.ObjectsVisibilityViewDescriptionProvider))]
public class ObjectsVisibilityView : UserControl, IView
{
  /// <summary>Выполнена ли активация элемента управления</summary>
  internal bool _activated;
  /// <summary>Были ли изменения в закладке</summary>
  internal bool _isChanged;
  internal INamedImageList _images;
  /// <summary>Текущий пользователь</summary>
  internal ICurrentUserAndRole _userAndRole;
  /// <summary>Сервис значков для категорий и типов</summary>
  internal ICategoryTypeIconService _categoryImages;
  /// <summary>Служба уведомлений</summary>
  internal INotificationService _notifications;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  internal NotificationEventHandler _notifyHandler;
  /// <summary>Список объектов, с которыми работает закладка</summary>
  internal List<MyElementEx> _items;
  /// <summary>
  /// Контейнер сервисов (контекст) для выделенных элементов пространства навигации
  /// </summary>
  internal System.IServiceProvider _provider;
  /// <summary>Права доступа к объекту в закладке</summary>
  internal ObjectsVisibilityView.EditorMode _editorMode;
  /// <summary>Невидимый корневой элемент в дереве</summary>
  internal List<object> _rootItem = new List<object>();
  /// <summary>Временный класс для работы с настройками</summary>
  internal ObjectsVisibility _visibility = new ObjectsVisibility();
  /// <summary>
  /// Редактируемый список пользователей, групп, ролей и их настройки видимости
  /// </summary>
  internal List<MyObjectElement> _users = new List<MyObjectElement>();
  /// <summary>Индекс изображения "imgObjectVisibility"</summary>
  internal static int _imgObjectVisibility = -1;
  /// <summary>
  /// Коллекция значков для типов объектов
  /// [(Int32)Идентификатор типа объекта] = [(Icon)Значок]
  /// </summary>
  internal Dictionary<int, Icon> _typesIcons = new Dictionary<int, Icon>();
  private List<IDBTypedObjectID> _dbTypedObjectIds4ProcessedNodes = new List<IDBTypedObjectID>(0);
  /// <summary>Идентификатор группы "Все пользователи"</summary>
  private static long _allUsersGroupID = 0;
  /// <summary>
  /// Строка с настройками видимости для группы "Все пользователи"
  /// </summary>
  private static string _allUsersGroupVisible = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button btnCancel;
  private Button btnApply;
  private Panel panelControls;
  private Button btnDefaul;
  private Button btnDel;
  private Button btnAdd;
  private Intermech.VirtualTreeView.VirtualTreeView treeRights;
  private Column columnUsers;
  private Column columnShow;
  private CellEditor cellEditorShow;
  private CheckBox checkBoxShow;
  private Column columnHide;
  private CellEditor cellEditorHide;
  private CheckBox checkBoxHide;
  private Panel panelWarning;
  private Label labelWarning;

  /// <summary>Создать экземпляр класса</summary>
  public ObjectsVisibilityView()
  {
    this.InitializeComponent();
    this.InitViewResources();
  }

  /// <summary>Инициализация ресурсов закладки</summary>
  public void InitViewResources()
  {
    this._images = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this._userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this._categoryImages = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._notifications = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
  }

  /// <summary>Освобождение ресурсов закладки</summary>
  public void DisposeViewResources()
  {
    this._images = (INamedImageList) null;
    this._categoryImages = (ICategoryTypeIconService) null;
    this._notifications = (INotificationService) null;
    this._items = (List<MyElementEx>) null;
    this._provider = (System.IServiceProvider) null;
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._items = (List<MyElementEx>) null;
    this.FillDBTypedObjectIds4ProcessedNodes(items);
    this._provider = provider;
  }

  /// <summary>
  /// Активировать закладку (чтение из базы данных, загрузка информации и т.п.)
  /// </summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public void Activate(IView previousView)
  {
    if (this._notifications != null && this._notifyHandler == null)
    {
      this._notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
      this._notifications.Subscribe(this._notifyHandler);
    }
    this.LoadViewData();
    if (!(this._provider.GetService(typeof (IViewState)) is IViewState service))
      return;
    this.treeRights.Enabled = this.panelBottom.Visible = this.panelControls.Visible = (service.ViewState & ViewStateFlags.ReadOnly) != ViewStateFlags.ReadOnly;
  }

  /// <summary>Деактивировать закладку</summary>
  /// <param name="nextView">Следующая закладка</param>
  public void Deactivate(IView nextView)
  {
    if (this._notifications != null && this._notifyHandler != null)
    {
      this._notifications.Unsubscribe(this._notifyHandler);
      this._notifyHandler = (NotificationEventHandler) null;
    }
    if (this._isChanged && this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode && MessageBox.Show(LocalizationHolder.rm.GetString(sc_4238.ssp_imclient_4239()), LocalizationHolder.rm.GetString("Client.Core_1189"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
      this.SaveViewData();
    this._activated = false;
  }

  /// <summary>Заголовок закладки</summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_1183");

  /// <summary>Индекс изображения</summary>
  public int ImageIndex
  {
    [DebuggerStepThrough] get
    {
      if (ObjectsVisibilityView._imgObjectVisibility == -1)
        ObjectsVisibilityView._imgObjectVisibility = this._images.ImageIndex("imgObjectVisibility");
      return ObjectsVisibilityView._imgObjectVisibility;
    }
  }

  /// <summary>
  /// Порядковый номер закладки (прописан в файле Вьюшки.txt)
  /// </summary>
  public int OrderID
  {
    [DebuggerStepThrough] get => 65;
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
  }

  /// <summary>Выполнить очистку элементов управления в закладке</summary>
  internal void Clear()
  {
    this._visibility.Clear();
    this._users.Clear();
    this.treeRights.UpdateRows(true);
    this._activated = false;
    this.UpdateControls();
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="backColor"></param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Icon GetObjTypeIcon(int objTypeID, Color backColor)
  {
    if (!MetaDataHelper.ExistsObjectType(objTypeID))
      return (Icon) null;
    objTypeID = Math.Max(objTypeID, -1);
    if (this._typesIcons.ContainsKey(objTypeID))
      return this._typesIcons[objTypeID];
    if (this._categoryImages.IndexOf(4, objTypeID) < 0)
      return (Icon) null;
    Icon objTypeIcon = ImagesResizeHelper.ResizeIconTo32x16(this._categoryImages.GetIcon(4, objTypeID), backColor);
    this._typesIcons.Add(objTypeID, objTypeIcon);
    return objTypeIcon;
  }

  /// <summary>
  /// Заполнить элементы управления закладки данными, полученными в методе Initialize
  /// </summary>
  internal void LoadViewData()
  {
    this.Clear();
    this.UpdateControls();
    this._editorMode = ObjectsVisibilityView.EditorMode.EditorMode;
    this._activated = true;
    string source = string.Empty;
    bool flag = false;
    this._items = new List<MyElementEx>(this._dbTypedObjectIds4ProcessedNodes.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (ObjectsVisibilityView._allUsersGroupID == 0L)
      {
        ObjectsVisibilityView._allUsersGroupID = sessionKeeper.Session.IdentHelper.AllUsersGroupID;
        ObjectsVisibilityView._allUsersGroupVisible = ObjectsVisibilityHelper.AllUsersVisibility(ObjectsVisibilityView._allUsersGroupID);
      }
      for (int index = 0; index < this._dbTypedObjectIds4ProcessedNodes.Count; ++index)
      {
        IDBTypedObjectID ids4ProcessedNode = this._dbTypedObjectIds4ProcessedNodes[index];
        if (ids4ProcessedNode != null)
        {
          this._items.Add(new MyElementEx((object) ids4ProcessedNode.ID, ids4ProcessedNode.Caption, false, false, false, ids4ProcessedNode.ObjectID, ids4ProcessedNode.ObjectType, Guid.Empty, new object[1]
          {
            (object) ids4ProcessedNode.Owner
          }));
          IDBObject dbObject = sessionKeeper.Session.GetObject(ids4ProcessedNode.ObjectID, false);
          IDBAttribute attributeById = dbObject?.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545"));
          if (dbObject == null || attributeById == null)
          {
            if (dbObject != null && this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode && !(dbObject as IDBSecurity).CheckAccess(ActionType.SetAccess, false, false))
              this._editorMode = ObjectsVisibilityView.EditorMode.ReadOnly;
            if (dbObject != null && this._editorMode == ObjectsVisibilityView.EditorMode.ReadOnly && !(dbObject as IDBSecurity).CheckAccess(ActionType.GetAccess, false, false))
            {
              this._editorMode = ObjectsVisibilityView.EditorMode.None;
              break;
            }
            if (index != 0)
              flag = this._dbTypedObjectIds4ProcessedNodes.Count > 1 && attributeById != null;
            else
              continue;
          }
          if (this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode && !(dbObject as IDBSecurity).CheckAccess(ActionType.SetAccess, false, false))
            this._editorMode = ObjectsVisibilityView.EditorMode.ReadOnly;
          if (this._editorMode == ObjectsVisibilityView.EditorMode.ReadOnly && !(dbObject as IDBSecurity).CheckAccess(ActionType.GetAccess, false, false))
          {
            this._editorMode = ObjectsVisibilityView.EditorMode.None;
            break;
          }
          object empty = attributeById?.Value;
          if (empty == null || empty == DBNull.Value)
          {
            if (index != 0)
            {
              flag = this._dbTypedObjectIds4ProcessedNodes.Count > 1;
              empty = (object) string.Empty;
            }
            else
              continue;
          }
          if (index == 0)
            source = empty.ToString();
          else if (index > 0 && source != empty.ToString())
            flag = true;
        }
      }
      if (source == string.Empty && this._editorMode != ObjectsVisibilityView.EditorMode.None)
        source = ObjectsVisibilityView._allUsersGroupVisible;
      if (!flag)
      {
        this._visibility.Assign((object) source);
        this._users = this._visibility.GetObjects(sessionKeeper.Session);
      }
    }
    if (flag)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_4238.ssp_imclient_4240()), LocalizationHolder.rm.GetString("Client.Core_1191"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
    }
    this.checkBoxHide.AutoCheck = this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode;
    this.checkBoxShow.AutoCheck = this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode;
    this._isChanged = false;
    this._rootItem.Clear();
    if (this._editorMode != ObjectsVisibilityView.EditorMode.None)
      this._rootItem.Add((object) this._users);
    this.treeRights.DataSource = (object) this._rootItem;
    this.treeRights.RootRow.ExpandChildren(true);
    this.UpdateControls();
  }

  /// <summary>Сохранить информацию из закладки в выделенные объекты</summary>
  internal void SaveViewData()
  {
    if (!this._activated || this._items == null || this._items.Count == 0 || this._editorMode != ObjectsVisibilityView.EditorMode.EditorMode)
      return;
    this._visibility.SetObjects(this._users);
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545");
    string empty = this._visibility.ToString();
    if (empty == ObjectsVisibilityView._allUsersGroupVisible)
      empty = string.Empty;
    List<long> objectIDs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this._items.Count; ++index)
      {
        MyElementEx myElementEx = this._items[index];
        if (myElementEx != null)
        {
          IDBAttributeTypeInfo4 attributeById = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(myElementEx.ElementID32).Attributes.GetAttributeByID(attributeTypeId, false);
          IDBObject dbObject = sessionKeeper.Session.GetObject(myElementEx.ElementID64, false);
          IDBAttribute byId = dbObject?.Attributes.FindByID(attributeTypeId);
          if (attributeById.Required == RequiredModes.Manual)
          {
            if (empty == string.Empty)
            {
              if (byId != null)
              {
                byId.Delete(0L);
                if (!objectIDs.Contains(myElementEx.ElementID64))
                {
                  objectIDs.Add(myElementEx.ElementID64);
                  continue;
                }
                continue;
              }
              continue;
            }
            if (byId == null)
            {
              dbObject.Attributes.AddAttribute(attributeTypeId, true).Value = (object) empty;
              if (!objectIDs.Contains(myElementEx.ElementID64))
              {
                objectIDs.Add(myElementEx.ElementID64);
                continue;
              }
              continue;
            }
          }
          if (byId != null)
          {
            byId.Value = (object) empty;
            if (!objectIDs.Contains(myElementEx.ElementID64))
              objectIDs.Add(myElementEx.ElementID64);
          }
        }
      }
    }
    this._isChanged = false;
    this.UpdateControls();
    if (objectIDs.Count <= 0 || this._notifications == null)
      return;
    this._notifications.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs));
  }

  /// <summary>Отменить изменения в данных</summary>
  internal void CancelViewData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._users = this._visibility.GetObjects(sessionKeeper.Session);
    this._rootItem.Clear();
    this._rootItem.Add((object) this._users);
    this.treeRights.DataSource = (object) this._rootItem;
    this.treeRights.RootRow.ExpandChildren(true);
    this._isChanged = false;
    this.UpdateControls();
  }

  /// <summary>Управление контролами на закладке</summary>
  internal void UpdateControls()
  {
    this.btnAdd.Enabled = this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode;
    this.btnDel.Enabled = this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode && this._users.Count > 0 && this.treeRights.SelectedRows.Count > 0 && this.treeRights.SelectedRows.Count < this._users.Count;
    this.btnDefaul.Enabled = this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode && this._users.Count > 0;
    this.btnApply.Enabled = this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode && this._isChanged;
    this.btnApply.Visible = true;
    this.btnCancel.Enabled = this.btnApply.Enabled;
    this.btnCancel.Visible = true;
    this.panelBottom.Enabled = this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode;
    this.panelControls.Enabled = this._editorMode == ObjectsVisibilityView.EditorMode.EditorMode;
    this.panelWarning.Visible = this._editorMode == ObjectsVisibilityView.EditorMode.None;
  }

  /// <summary>Требуется информация о дочерних элементах в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRights_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Level != 0)
      return;
    e.Children = (IList) this._users;
  }

  /// <summary>Требуется информация о строке в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRights_GetRowData(object sender, GetRowDataEventArgs e)
  {
    MyObjectElement myObjectElement = (MyObjectElement) e.Row.Item;
    if (e.Row.Level != 1)
      return;
    e.RowData.IconSize = 32 /*0x20*/;
    e.RowData.Icon = this.GetObjTypeIcon(myObjectElement.ObjectType, Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue));
  }

  /// <summary>Требуется информация о ячейке в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRights_GetCellData(object sender, GetCellDataEventArgs e)
  {
    MyObjectElement myObjectElement = (MyObjectElement) e.Row.Item;
    if (e.Column == this.columnUsers)
      e.CellData.Value = (object) myObjectElement.Caption;
    else if (e.Column == this.columnShow)
    {
      ObjectsVisibilityFlags tag = (ObjectsVisibilityFlags) myObjectElement.Tag;
      e.CellData.Value = (object) ((tag & ObjectsVisibilityFlags.Visible) > ObjectsVisibilityFlags.None);
    }
    else
    {
      if (e.Column != this.columnHide)
        return;
      ObjectsVisibilityFlags tag = (ObjectsVisibilityFlags) myObjectElement.Tag;
      e.CellData.Value = (object) ((tag & ObjectsVisibilityFlags.Hidden) > ObjectsVisibilityFlags.None);
    }
  }

  /// <summary>Изменилась выделенная или сфокусированная строка</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRights_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Событие вызывается перед показом редактора в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRights_BeforeShowCellEdit(object sender, BeforeShowCellEditEventArgs e)
  {
  }

  /// <summary>Установить новое значение в ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRights_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (this._editorMode != ObjectsVisibilityView.EditorMode.EditorMode)
    {
      e.Cancel = true;
    }
    else
    {
      MyObjectElement myObjectElement = (MyObjectElement) e.Row.Item;
      ObjectsVisibilityFlags objectsVisibilityFlags1 = (ObjectsVisibilityFlags) myObjectElement.Tag;
      if (e.Column == this.columnShow)
      {
        if ((bool) e.NewValue)
          objectsVisibilityFlags1 = (objectsVisibilityFlags1 | ObjectsVisibilityFlags.Visible) & ~ObjectsVisibilityFlags.Hidden;
        else
          objectsVisibilityFlags1 &= ~ObjectsVisibilityFlags.Visible;
        myObjectElement.Tag = (object) objectsVisibilityFlags1;
      }
      if (e.Column == this.columnHide)
      {
        ObjectsVisibilityFlags objectsVisibilityFlags2 = !(bool) e.NewValue ? objectsVisibilityFlags1 & ~ObjectsVisibilityFlags.Hidden : (objectsVisibilityFlags1 | ObjectsVisibilityFlags.Hidden) & ~ObjectsVisibilityFlags.Visible;
        myObjectElement.Tag = (object) objectsVisibilityFlags2;
      }
      this._isChanged = true;
      this.treeRights.UpdateRowData(e.Row);
      this.UpdateControls();
    }
  }

  /// <summary>Нажата кнопка "Добавить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    this.UpdateControls();
    if (this._items == null || this._items.Count == 0 || this._editorMode != ObjectsVisibilityView.EditorMode.EditorMode)
      return;
    string caption1 = LocalizationHolder.rm.GetString("Client.Core_1184");
    string str = LocalizationHolder.rm.GetString("Client.Core_1185");
    string caption2 = LocalizationHolder.rm.GetString("Client.Core_1186");
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545")));
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545")));
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")));
    string description = str;
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(caption2, descriptors);
    long[] numArray = SelectionWindow.SelectObjects(caption1, description, (IDescriptor) rootDescriptor, SelectionOptions.Default | SelectionOptions.HideViewsToolbar | SelectionOptions.HideViewsGroupingBox | SelectionOptions.DisableObjectListFilter | SelectionOptions.DisableSelectAbstractTypes);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < numArray.Length; ++index)
      {
        long num = numArray[index];
        MyObjectElement myObjectElement = new MyObjectElement();
        myObjectElement.ObjectID = num;
        if (!this._users.Contains(myObjectElement))
        {
          myObjectElement.SyncObjectsData(sessionKeeper.Session);
          myObjectElement.Tag = (object) ObjectsVisibilityFlags.Visible;
          this._users.Add(myObjectElement);
        }
      }
    }
    this._users.Sort();
    this.treeRights.UpdateRows(true);
    this._isChanged = true;
    this.UpdateControls();
  }

  /// <summary>Нажата кнопка "Удалить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnDel_Click(object sender, EventArgs e)
  {
    this.UpdateControls();
    if (this._items == null || this._items.Count == 0 || this._editorMode != ObjectsVisibilityView.EditorMode.EditorMode || this.treeRights.SelectedRows.Count == 0)
      return;
    for (int index = 0; index < this.treeRights.SelectedRows.Count; ++index)
      this._users.Remove((MyObjectElement) this.treeRights.SelectedRows[index].Item);
    if (this._users.Count == 0)
    {
      this.btnDefault_Click(sender, e);
    }
    else
    {
      this.treeRights.UpdateRows(true);
      this._isChanged = true;
      this.UpdateControls();
    }
  }

  /// <summary>Нажата кнопка "Очистить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnDefault_Click(object sender, EventArgs e)
  {
    this.UpdateControls();
    if (this._items == null || this._items.Count == 0 || this._users.Count == 0 || this._editorMode != ObjectsVisibilityView.EditorMode.EditorMode)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._users = new ObjectsVisibility(ObjectsVisibilityHelper.AllUsersVisibility(ObjectsVisibilityView._allUsersGroupID)).GetObjects(sessionKeeper.Session);
    this._rootItem.Clear();
    this._rootItem.Add((object) this._users);
    this.treeRights.DataSource = (object) this._rootItem;
    this.treeRights.RootRow.ExpandChildren(true);
    this._isChanged = true;
    this.UpdateControls();
  }

  /// <summary>Нажата кнопка "Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    if (this._editorMode != ObjectsVisibilityView.EditorMode.EditorMode || !this._isChanged)
      return;
    this.SaveViewData();
  }

  /// <summary>Нажата кнопка "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (this._items == null || this._items.Count == 0 || this._editorMode != ObjectsVisibilityView.EditorMode.EditorMode || !this._isChanged || MessageBox.Show(LocalizationHolder.rm.GetString(sc_4238.ssp_imclient_4241()), LocalizationHolder.rm.GetString("Client.Core_1187"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
      return;
    this.CancelViewData();
  }

  private void FillDBTypedObjectIds4ProcessedNodes(ISelectedItems selectedItems)
  {
    List<IDBTypedObjectID> dbTypedObjectIdList = new List<IDBTypedObjectID>(selectedItems.Count);
    int index = 0;
    for (int count = selectedItems.Count; index < count; ++index)
    {
      IDBTypedObjectID itemData = selectedItems.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      dbTypedObjectIdList.Add(itemData);
    }
    this._dbTypedObjectIds4ProcessedNodes = dbTypedObjectIdList;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectsVisibilityView));
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.panelControls = new Panel();
    this.btnDefaul = new Button();
    this.btnDel = new Button();
    this.btnAdd = new Button();
    this.treeRights = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnUsers = new Column();
    this.columnShow = new Column();
    this.cellEditorShow = new CellEditor();
    this.checkBoxShow = new CheckBox();
    this.columnHide = new Column();
    this.cellEditorHide = new CellEditor();
    this.checkBoxHide = new CheckBox();
    this.panelWarning = new Panel();
    this.labelWarning = new Label();
    this.panelBottom.SuspendLayout();
    this.panelControls.SuspendLayout();
    this.treeRights.BeginInit();
    this.panelWarning.SuspendLayout();
    this.SuspendLayout();
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.panelControls.Controls.Add((Control) this.btnDefaul);
    this.panelControls.Controls.Add((Control) this.btnDel);
    this.panelControls.Controls.Add((Control) this.btnAdd);
    componentResourceManager.ApplyResources((object) this.panelControls, "panelControls");
    this.panelControls.Name = "panelControls";
    this.btnDefaul.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this.btnDefaul, "btnDefaul");
    this.btnDefaul.Name = "btnDefaul";
    this.btnDefaul.Click += new EventHandler(this.btnDefault_Click);
    this.btnDel.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this.btnDel, "btnDel");
    this.btnDel.Name = "btnDel";
    this.btnDel.Click += new EventHandler(this.btnDel_Click);
    this.btnAdd.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.treeRights.AllowDrop = true;
    this.treeRights.AllowUserPinnedColumns = false;
    this.treeRights.AutoFitColumns = true;
    this.treeRights.Columns.Add(this.columnUsers);
    this.treeRights.Columns.Add(this.columnShow);
    this.treeRights.Columns.Add(this.columnHide);
    this.treeRights.DisableHeaderContextMenu = false;
    componentResourceManager.ApplyResources((object) this.treeRights, "treeRights");
    this.treeRights.Editors.Add(this.cellEditorShow);
    this.treeRights.Editors.Add(this.cellEditorHide);
    this.treeRights.ImageList = (ImageList) null;
    this.treeRights.LineStyle = LineStyle.Dot;
    this.treeRights.MainColumn = this.columnUsers;
    this.treeRights.Name = "treeRights";
    this.treeRights.SelectBeforeEdit = true;
    this.treeRights.ShowRootRow = false;
    this.treeRights.SuppressErrorMessages = true;
    this.treeRights.BeforeShowCellEdit += new BeforeShowCellEditHandler(this.treeRights_BeforeShowCellEdit);
    this.treeRights.FocusRowChanged += new EventHandler(this.treeRights_SelectionChanged);
    this.treeRights.GetCellData += new GetCellDataHandler(this.treeRights_GetCellData);
    this.treeRights.GetChildren += new GetChildrenHandler(this.treeRights_GetChildren);
    this.treeRights.GetRowData += new GetRowDataHandler(this.treeRights_GetRowData);
    this.treeRights.SelectionChanged += new EventHandler(this.treeRights_SelectionChanged);
    this.treeRights.SetCellValue += new SetCellValueHandler(this.treeRights_SetCellValue);
    this.columnUsers.AutoSizePolicy = ColumnAutoSizePolicy.AutoIncrease;
    componentResourceManager.ApplyResources((object) this.columnUsers, "columnUsers");
    this.columnUsers.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnUsers.HeaderStyle.HorzAlignment");
    this.columnUsers.Movable = false;
    this.columnUsers.Name = "columnUsers";
    this.columnUsers.Sortable = false;
    componentResourceManager.ApplyResources((object) this.columnShow, "columnShow");
    this.columnShow.CellEditor = this.cellEditorShow;
    this.columnShow.Movable = false;
    this.columnShow.Name = "columnShow";
    this.columnShow.Resizable = false;
    this.columnShow.Sortable = false;
    this.cellEditorShow.CellAlignment = ContentAlignment.MiddleCenter;
    this.cellEditorShow.Control = (Control) this.checkBoxShow;
    this.cellEditorShow.DisplayMode = CellEditorDisplayMode.Always;
    this.cellEditorShow.UseCellHeight = false;
    this.cellEditorShow.UseCellWidth = false;
    componentResourceManager.ApplyResources((object) this.checkBoxShow, "checkBoxShow");
    this.checkBoxShow.Name = "checkBoxShow";
    componentResourceManager.ApplyResources((object) this.columnHide, "columnHide");
    this.columnHide.CellEditor = this.cellEditorHide;
    this.columnHide.Movable = false;
    this.columnHide.Name = "columnHide";
    this.columnHide.Resizable = false;
    this.columnHide.Sortable = false;
    this.cellEditorHide.CellAlignment = ContentAlignment.MiddleCenter;
    this.cellEditorHide.Control = (Control) this.checkBoxHide;
    this.cellEditorHide.DisplayMode = CellEditorDisplayMode.Always;
    this.cellEditorHide.UseCellHeight = false;
    this.cellEditorHide.UseCellWidth = false;
    componentResourceManager.ApplyResources((object) this.checkBoxHide, "checkBoxHide");
    this.checkBoxHide.Name = "checkBoxHide";
    componentResourceManager.ApplyResources((object) this.panelWarning, "panelWarning");
    this.panelWarning.BorderStyle = BorderStyle.Fixed3D;
    this.panelWarning.Controls.Add((Control) this.labelWarning);
    this.panelWarning.ForeColor = Color.DarkRed;
    this.panelWarning.Name = "panelWarning";
    componentResourceManager.ApplyResources((object) this.labelWarning, "labelWarning");
    this.labelWarning.Name = "labelWarning";
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.panelWarning);
    this.Controls.Add((Control) this.treeRights);
    this.Controls.Add((Control) this.panelControls);
    this.Controls.Add((Control) this.checkBoxShow);
    this.Controls.Add((Control) this.checkBoxHide);
    this.Controls.Add((Control) this.panelBottom);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.MinimumSize = new Size(350, 150);
    this.Name = nameof (ObjectsVisibilityView);
    this.panelBottom.ResumeLayout(false);
    this.panelControls.ResumeLayout(false);
    this.treeRights.EndInit();
    this.panelWarning.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Права доступа к объекту в закладке</summary>
  internal enum EditorMode
  {
    /// <summary>Объект некорректен, закладка пуста</summary>
    None,
    /// <summary>Только просмотр</summary>
    ReadOnly,
    /// <summary>Режим администратора</summary>
    EditorMode,
  }

  private sealed class ObjectsVisibilityViewDescriptionProvider : BaseViewDescriptionProvider
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
        Caption = LocalizationHolder.rm.GetString("Client.Core_1183"),
        ImageIndex = namedImageList.ImageIndex("imgObjectVisibility"),
        OrderID = 65,
        HelpTopicId = "1116"
      };
    }
  }
}
