
// Type: Intermech.Navigator.DBObjects.EditingContextsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Вьюшка для просмотра и управления контекстом редактирования
/// </summary>
public class EditingContextsView : UserControl, IView
{
  internal INamedImageList _images;
  /// <summary>Текущий пользователь</summary>
  internal ICurrentUserAndRole _userAndRole;
  /// <summary>Сервис значков для категорий и типов</summary>
  internal ICategoryTypeIconService _categoryImages;
  /// <summary>Индекс изображения "imgEditingContext"</summary>
  internal static int _imgEditingContext = -1;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  internal NotificationEventHandler _notifyHandler;
  /// <summary>
  /// Коллекция выделенных элементов пространства навигации, на основании данных которых работает закладка
  /// </summary>
  internal ISelectedItems _items;
  /// <summary>Служба уведомлений</summary>
  private INotificationService _notifications;
  /// <summary>
  /// Контейнер сервисов (контекст) для выделенных элементов пространства навигации
  /// </summary>
  internal System.IServiceProvider _services;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolTip toolTip;
  private ImageList imageList;
  private Panel panelBottom;
  private Button btnCancel;
  private Button btnApply;
  private EditingContextsEditor editor;
  private Label labelPicture;
  private Label labelWarning;

  /// <summary>Создать экземпляр класса</summary>
  public EditingContextsView()
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
    EditingContextsView._imgEditingContext = EditingContextsView._imgEditingContext < 0 ? this._images.ImageIndex("imgObjectsFilter") : EditingContextsView._imgEditingContext;
  }

  /// <summary>Освобождение ресурсов закладки</summary>
  public void DisposeViewResources()
  {
    this._images = (INamedImageList) null;
    this._categoryImages = (ICategoryTypeIconService) null;
    this._items = (ISelectedItems) null;
    this._services = (System.IServiceProvider) null;
    this._notifications = (INotificationService) null;
  }

  /// <summary>Заголовок закладки</summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_1225");

  /// <summary>Индекс изображения</summary>
  public int ImageIndex => EditingContextsView._imgEditingContext;

  /// <summary>
  /// Порядковый номер закладки (прописан в файле Вьюшки.txt)
  /// </summary>
  public int OrderID => 0;

  /// <summary>Инициализировать закладку</summary>
  /// <param name="items">Коллекция выделенных элементов пространства навигации</param>
  /// <param name="provider">Контейнер сервисов</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._items = items;
    this._services = provider;
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
    IViewState service = this._services != null ? this._services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    if (service != null)
    {
      long viewState = (long) service.ViewState;
    }
    this.editor.ReadOnly = false;
    this.panelBottom.Visible = !this.editor.ReadOnly;
    this.LoadViewData();
    this.editor.SelectedItemsChanged += new EventHandler(this.Editor_SelectedItemsChanged);
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
    if (!this.editor.IsChanged || MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1471"), LocalizationHolder.rm.GetString("Client.Core_1472"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
      return;
    this.DoApply((object) this, (EventArgs) null);
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (this.IsDisposed)
      return;
    bool flag1 = false;
    if (e.EventName == "RelationsCreated")
    {
      if (this.editor.IsChanged || !(e is DBRelationsEventArgs relationsEventArgs) || relationsEventArgs.RelationIDs == null || relationsEventArgs.RelationIDs.Count <= 0)
        return;
      EditingContextsObjectContainer internalContext = this.editor.InternalContext;
      if (internalContext == null || relationsEventArgs.ProjIDs == null || relationsEventArgs.ProjIDs.IndexOf(internalContext.ContextID) < 0 || !relationsEventArgs.Exists(internalContext.ContextID, MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545")))
        return;
      this.editor.CurrentContextObjectID = this.editor.CurrentContextObjectID;
    }
    else if (e.EventName == "RelationsRemoved")
    {
      if (!(e is DBRelationsEventArgs relationsEventArgs) || relationsEventArgs.RelationIDs == null || relationsEventArgs.RelationIDs.Count <= 0)
        return;
      EditingContextsObjectContainer internalContext = this.editor.InternalContext;
      if (internalContext == null || relationsEventArgs.ProjIDs == null || relationsEventArgs.ProjIDs.IndexOf(internalContext.ContextID) < 0 || !relationsEventArgs.Exists(internalContext.ContextID, MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545")))
        return;
      for (int index1 = internalContext.Descriptions.Count - 1; index1 >= 0; --index1)
      {
        ObjectVersionDescription description = internalContext.Descriptions[index1];
        if (description.Tag is List<long>)
        {
          List<long> tag = (List<long>) description.Tag;
          bool flag2 = false;
          for (int index2 = 0; index2 < tag.Count; ++index2)
          {
            flag2 = relationsEventArgs.RelationIDs.IndexOf(tag[index2]) >= 0;
            if (flag2)
            {
              tag.RemoveAt(index2);
              break;
            }
          }
          if (flag2)
          {
            internalContext.DeleteVersion(description.F_OBJECT_ID);
            flag1 = true;
          }
        }
      }
      if (!flag1)
        return;
      internalContext.ClearCacheTables();
      this.editor.FillEditor(false);
    }
    else if (e.EventName == "ObjectsRemoved")
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0)
        return;
      EditingContextsObjectContainer internalContext = this.editor.InternalContext;
      if (internalContext == null)
        return;
      for (int index = internalContext.Descriptions.Count - 1; index >= 0; --index)
      {
        ObjectVersionDescription description = internalContext.Descriptions[index];
        if (objectsEventArgs.ObjectIDs.Contains(description.F_OBJECT_ID) || objectsEventArgs.ObjectIDs.Contains(-description.F_OBJECT_ID))
        {
          internalContext.DeleteVersion(description.F_OBJECT_ID);
          flag1 = true;
        }
      }
      if (!flag1)
        return;
      internalContext.ClearCacheTables();
      this.editor.FillEditor(false);
    }
    else if (e.EventName == "ObjectsChanged" || e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsChangesCancelled")
    {
      if (!(e is DBObjectsEventArgs e1) || e1.ObjectIDs == null)
        return;
      EditingContextsObjectContainer internalContext = this.editor.InternalContext;
      if (internalContext == null)
        return;
      for (int index = internalContext.Descriptions.Count - 1; index >= 0; --index)
      {
        ObjectVersionDescription description = internalContext.Descriptions[index];
        if (e1.ObjectIDs.Contains(description.F_OBJECT_ID) || e1.ObjectIDs.Contains(-description.F_OBJECT_ID))
        {
          flag1 = true;
          break;
        }
      }
      if (!flag1)
        return;
      this.editor.DoRefresh((object) this, (EventArgs) e1);
    }
    else
    {
      if (!(e.EventName == "ObjectsCheckedOut") || !(e is DBObjectsCheckOutEventArgs e2) || e2.ObjectIDs == null)
        return;
      EditingContextsObjectContainer internalContext = this.editor.InternalContext;
      if (internalContext == null)
        return;
      for (int index = internalContext.Descriptions.Count - 1; index >= 0; --index)
      {
        ObjectVersionDescription description = internalContext.Descriptions[index];
        if (e2.ObjectIDs.Contains(description.F_OBJECT_ID) || e2.ObjectIDs.Contains(-description.F_OBJECT_ID))
        {
          flag1 = true;
          break;
        }
      }
      if (!flag1)
        return;
      this.editor.DoRefresh((object) this, (EventArgs) e2);
    }
  }

  /// <summary>Выполнить очистку элементов управления в закладке</summary>
  internal void Clear() => this.UpdateControls();

  /// <summary>
  /// Заполнить элементы управления закладки данными, полученными в методе Initialize
  /// </summary>
  internal void LoadViewData()
  {
    this.Clear();
    if (this._items == null || this._items.Count == 0)
      return;
    IDBTypedObjectID itemData = this._items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (!MetaDataHelper.IsObjectTypeEditingContext(itemData.ObjectType))
      return;
    this.editor.Services = this._services;
    this.editor.CurrentContextObjectID = itemData.ObjectID;
    this.UpdateControls();
  }

  /// <summary>Управление контролами на закладке</summary>
  internal void UpdateControls()
  {
    this.labelPicture.Visible = this.editor.AccessRights != EditingContextsAccessRights.FullAccess;
    this.labelWarning.Visible = this.labelPicture.Visible;
    this.btnApply.Enabled = this.editor.IsChanged;
    this.btnCancel.Enabled = this.editor.IsChanged;
  }

  /// <summary>В редакторе контекста есть изменения</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editor_OnChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Нажата кнопка "Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoApply(object sender, EventArgs e)
  {
    if (!this.editor.IsChanged)
      return;
    if (this.editor.AccessRights != EditingContextsAccessRights.FullAccess)
      return;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService)
          customService.SetEditingContextsObject((object) sessionKeeper.Session.SessionGUID, this.editor.Context.SimpleClone(), true);
      }
      this.editor.Fix();
    }
    catch (Exception ex)
    {
      throw ex;
    }
    (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.editor.CurrentContextObjectID));
  }

  /// <summary>Нажата кнопка "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCancel(object sender, EventArgs e)
  {
    if (!this.editor.IsChanged || this.editor.AccessRights != EditingContextsAccessRights.FullAccess)
      return;
    this.editor.Undo();
  }

  private void Editor_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
      ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
    ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) this.editor.SelectedItems);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditingContextsView));
    this.toolTip = new ToolTip(this.components);
    this.panelBottom = new Panel();
    this.labelPicture = new Label();
    this.imageList = new ImageList(this.components);
    this.labelWarning = new Label();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.editor = new EditingContextsEditor();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    this.panelBottom.Controls.Add((Control) this.labelPicture);
    this.panelBottom.Controls.Add((Control) this.labelWarning);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.labelPicture, "labelPicture");
    this.labelPicture.ImageList = this.imageList;
    this.labelPicture.MaximumSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.labelPicture.MinimumSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.labelPicture.Name = "labelPicture";
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "warning.png");
    componentResourceManager.ApplyResources((object) this.labelWarning, "labelWarning");
    this.labelWarning.Name = "labelWarning";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.DoCancel);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.DoApply);
    this.editor.DisableHeader = true;
    componentResourceManager.ApplyResources((object) this.editor, "editor");
    this.editor.IsChanged = false;
    this.editor.MinimumSize = new Size(200, 100);
    this.editor.Name = "editor";
    this.editor.OnChanged += new EditingContextsEditor.EditingContextsChangedEventHandler(this.editor_OnChanged);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.editor);
    this.Controls.Add((Control) this.panelBottom);
    this.MinimumSize = new Size(330, 140);
    this.Name = nameof (EditingContextsView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.panelBottom.ResumeLayout(false);
    this.panelBottom.PerformLayout();
    this.ResumeLayout(false);
  }
}
