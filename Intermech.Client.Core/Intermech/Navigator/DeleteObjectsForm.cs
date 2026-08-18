
// Type: Intermech.Navigator.DeleteObjectsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Форма для выбора удаляемых объектов</summary>
public class DeleteObjectsForm : Form
{
  /// <summary>Кэш графических объектов "Навигатора"</summary>
  private INavGraphicsCache navGraphicsCache;
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService objtypesIcons;
  /// <summary>Информация о текущем пользователе и его роли</summary>
  private ICurrentUserAndRole userAndRole;
  /// <summary>
  /// Коллекция значков для типов объектов
  /// [(Int32)Идентификатор типа объекта] = [(Icon)Значок]
  /// </summary>
  private Dictionary<int, Icon> typesIcons = new Dictionary<int, Icon>();
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider services;
  /// <summary>Коллекция описаний удаляемых объектов</summary>
  private DeletingObjects items;
  /// <summary>Запрет на обработку событий от дерева</summary>
  private bool disableTreeEvents;
  /// <summary>Кэш имён пользователей</summary>
  private Dictionary<long, object> users = new Dictionary<long, object>();
  /// <summary>Кэш названий типов объектов</summary>
  private Dictionary<int, object> types = new Dictionary<int, object>();
  /// <summary>Кэш названий шагов ЖЦ</summary>
  private Dictionary<int, object> lcsteps = new Dictionary<int, object>();
  /// <summary>Словарик с настройками формы</summary>
  private Dictionary<int, object> settings = new Dictionary<int, object>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar tbComposition;
  private ButtonItem btnProperties;
  private Intermech.VirtualTreeView.VirtualTreeView treeDeletingObjects;
  private Panel panelBottom;
  private Button btnCancel;
  private Button btnDelete;
  private Column columnCheck;
  private CellEditor cellEditor;
  private CheckBox checkBox1;
  private Column columnCAPTION;
  private Column columnLCSTEP;
  private Column columnOWNER;
  private Column columnCHECKEDBY;
  private Column columnOBJECT_TYPE;
  private Column columnOBJECT_ID;
  private Column columnNote;
  private MenuBar menuTree;
  private ContextMenuBarItem contextMenuTree;
  private MenuButtonItem mnpProperties;
  private MenuButtonItem mnpSelectAll;
  private MenuButtonItem mnpDeselectAll;
  private Label labelOptions;
  private Button btnAnalyze;
  private CheckBox cbFindLinkedObjects;
  private CheckBox cbFindAllVersions;
  private ButtonItem btnSelectAll;
  private ButtonItem btnDeselectAll;

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="items">Коллекция описаний удаляемых объектов</param>
  /// <param name="options">Параметры</param>
  public DeleteObjectsForm(
    System.IServiceProvider services,
    DeletingObjects items,
    ref DeleteAnalyzerOptions options)
  {
    this.InitializeComponent();
    this.Init(services, items, ref options);
  }

  /// <summary>Инициализация данных</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="items">Коллекция описаний удаляемых объектов</param>
  /// <param name="options">Параметры</param>
  protected virtual void Init(
    System.IServiceProvider services,
    DeletingObjects items,
    ref DeleteAnalyzerOptions options)
  {
    this.services = services;
    this.items = items;
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service1)
    {
      service1.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service1, EventArgs.Empty);
    }
    this.LoadAttrValues();
    this.objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this.navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this.userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    INamedImageList service2 = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this.btnProperties.Image = service2.ImageList.Images[service2.ImageIndex("imgCard")];
    this.mnpProperties.Image = this.btnProperties.Image;
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 70, primaryWorkingArea.Height / 100 * 60);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.treeDeletingObjects.RowSelectedStyle.BackColor = SystemColors.Highlight;
    this.cbFindLinkedObjects.Checked = (options & DeleteAnalyzerOptions.FindLinkedObjects) > DeleteAnalyzerOptions.None;
    this.cbFindAllVersions.Checked = (options & DeleteAnalyzerOptions.FindAllVersions) > DeleteAnalyzerOptions.None;
    this.treeDeletingObjects.DataSource = (object) items;
    this.RebuildTree();
    this.treeDeletingObjects.RootRow.ExpandChildren(true);
    this.UpdateControls();
  }

  /// <summary>Вызвать форму "Настройка интерфейса пользователя"</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="items">Коллекция описаний удаляемых объектов</param>
  /// <param name="options">Параметры</param>
  public static DialogResult Execute(
    System.IServiceProvider services,
    DeletingObjects items,
    ref DeleteAnalyzerOptions options)
  {
    using (DeleteObjectsForm deleteObjectsForm = new DeleteObjectsForm(services, items, ref options))
    {
      int num = (int) deleteObjectsForm.ShowDialog();
      options &= ~DeleteAnalyzerOptions.FindLinkedObjects;
      options &= ~DeleteAnalyzerOptions.FindAllVersions;
      if (deleteObjectsForm.cbFindLinkedObjects.Checked)
        options |= DeleteAnalyzerOptions.FindLinkedObjects;
      if (deleteObjectsForm.cbFindAllVersions.Checked)
        options |= DeleteAnalyzerOptions.FindAllVersions;
      return (DialogResult) num;
    }
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public virtual void UpdateControls()
  {
    this.btnProperties.Enabled = (this.treeDeletingObjects.SelectedRow != null ? this.treeDeletingObjects.SelectedRow.Item as DeletingObject : (DeletingObject) null) != null;
    this.mnpProperties.Enabled = this.btnProperties.Enabled;
    this.btnSelectAll.Enabled = this.items != null && this.items.Count > 0;
    this.mnpSelectAll.Enabled = this.btnSelectAll.Enabled;
    this.btnDeselectAll.Enabled = this.btnSelectAll.Enabled;
    this.mnpDeselectAll.Enabled = this.btnSelectAll.Enabled;
  }

  /// <summary>Собрать у контролов настройки в коллекцию settings</summary>
  protected virtual void GetControlsState()
  {
    for (int index = 0; index < this.treeDeletingObjects.Columns.Count; ++index)
    {
      if (this.settings.ContainsKey(1000 + index))
        this.settings.Remove(1000 + index);
      this.settings.Add(1000 + index, (object) this.treeDeletingObjects.Columns[index].Width);
    }
  }

  /// <summary>Установить контролам настройки из коллекции settings</summary>
  protected virtual void SetControlsState()
  {
    for (int index = 0; index < this.treeDeletingObjects.Columns.Count; ++index)
    {
      if (this.settings.ContainsKey(1000 + index))
        this.treeDeletingObjects.Columns[index].Width = (int) this.settings[1000 + index];
    }
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DeleteObjectsForm_Load(object sender, EventArgs e)
  {
    this.settings.Clear();
    FormStorage.LoadLayout((Control) this, (IDictionary) this.settings);
    this.SetControlsState();
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DeleteObjectsForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.GetControlsState();
    FormStorage.SaveLayout((Control) this, (IDictionary) this.settings);
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Icon GetObjTypeIcon(int objTypeID)
  {
    objTypeID = Math.Max(objTypeID, -1);
    if (this.typesIcons.ContainsKey(objTypeID))
      return this.typesIcons[objTypeID];
    if (this.objtypesIcons.IndexOf(4, objTypeID) < 0)
      return (Icon) null;
    Icon objTypeIcon = ImagesResizeHelper.ResizeIconTo32x16(this.objtypesIcons.GetIcon(4, objTypeID), this.treeDeletingObjects.RowStyle.BackColor);
    this.typesIcons.Add(objTypeID, objTypeIcon);
    return objTypeIcon;
  }

  /// <summary>Загрузить расшифровки значений атрибутов в кэш</summary>
  protected virtual void LoadAttrValues()
  {
    if (this.items == null || this.items.Count == 0)
      return;
    List<DeletingObject> deletingObjects = this.items.ExtractDeletingObjects();
    IUserNamesCache userNamesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
    IObjectTypeNamesCache objectTypeNamesCache = CacheManager.Cache("ObjectTypeNamesCache") as IObjectTypeNamesCache;
    IObjectLCStepsCache objectLcStepsCache = CacheManager.Cache("ObjectLCStepsCache") as IObjectLCStepsCache;
    for (int index = 0; index < deletingObjects.Count; ++index)
    {
      DeletingObject deletingObject = deletingObjects[index];
      if (deletingObject.OwnerID > 0L && !this.users.ContainsKey(deletingObject.OwnerID))
        this.users.Add(deletingObject.OwnerID, (object) userNamesCache.GetUserName(deletingObject.OwnerID));
      if (deletingObject.ChkOutByID > 0L && !this.users.ContainsKey(deletingObject.ChkOutByID))
        this.users.Add(deletingObject.ChkOutByID, (object) userNamesCache.GetUserName(deletingObject.ChkOutByID));
      if (!this.types.ContainsKey(deletingObject.ObjectType))
        this.types.Add(deletingObject.ObjectType, (object) objectTypeNamesCache.GetTypeName(deletingObject.ObjectType));
      if (!this.lcsteps.ContainsKey(deletingObject.LCStepID))
        this.lcsteps.Add(deletingObject.LCStepID, (object) objectLcStepsCache.GetName(deletingObject.LCStepID));
    }
  }

  /// <summary>Перестроить дерево</summary>
  protected virtual void RebuildTree()
  {
    bool disableTreeEvents = this.disableTreeEvents;
    try
    {
      this.disableTreeEvents = true;
      this.treeDeletingObjects.UpdateRows(true);
    }
    finally
    {
      this.disableTreeEvents = disableTreeEvents;
    }
  }

  /// <summary>Получить данные для ячейки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeDeletingObjects_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (!(e.Row.Item is DeletingObject deletingObject))
      return;
    try
    {
      if (e.Column == this.columnCheck)
        e.CellData.Value = (object) deletingObject.RemoveObject;
      else if (e.Column == this.columnOBJECT_TYPE)
        e.CellData.Value = this.types.ContainsKey(deletingObject.ObjectType) ? this.types[deletingObject.ObjectType] : (object) "?";
      else if (e.Column == this.columnOBJECT_ID)
        e.CellData.Value = (object) deletingObject.ObjectID;
      else if (e.Column == this.columnCAPTION)
        e.CellData.Value = (object) CaptionTransform.GetCaption(deletingObject.Caption, deletingObject.VersionNo);
      else if (e.Column == this.columnLCSTEP)
        e.CellData.Value = this.lcsteps.ContainsKey(deletingObject.LCStepID) ? this.lcsteps[deletingObject.LCStepID] : (object) "?";
      else if (e.Column == this.columnOWNER)
        e.CellData.Value = this.users.ContainsKey(deletingObject.OwnerID) ? this.users[deletingObject.OwnerID] : (object) "?";
      else if (e.Column == this.columnCHECKEDBY)
      {
        e.CellData.Value = deletingObject.ChkOutByID <= 0L || !this.users.ContainsKey(deletingObject.ChkOutByID) ? (object) null : this.users[deletingObject.ChkOutByID];
      }
      else
      {
        if (e.Column != this.columnNote)
          return;
        e.CellData.Value = (object) deletingObject.RemoveNote;
      }
    }
    finally
    {
      if (deletingObject.ChkOutByID != 0L && e.Column != this.columnCheck)
      {
        Color color1;
        Color color2;
        LinearGradientMode linearGradientMode;
        Color color3;
        if (deletingObject.ChkOutByID == this.userAndRole.UserID)
        {
          color1 = this.navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor;
          color2 = (this.navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckOut) == GradientUsing.CheckOut ? this.navGraphicsCache.CurrentColorsScheme.CheckedOutBkEndColor : this.navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor;
          linearGradientMode = this.navGraphicsCache.CurrentColorsScheme.CheckedOutGradientMode;
          color3 = this.navGraphicsCache.CurrentColorsScheme.ForegroundCheckedOut;
        }
        else
        {
          color1 = this.navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor;
          color2 = (this.navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckedOutOther) == GradientUsing.CheckedOutOther ? this.navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkEndColor : this.navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor;
          linearGradientMode = this.navGraphicsCache.CurrentColorsScheme.CheckedOutOtherGradientMode;
          color3 = this.navGraphicsCache.CurrentColorsScheme.ForegroundCheckedOutOther;
        }
        StyleDelta delta1 = new StyleDelta();
        delta1.BackColor = color1;
        delta1.GradientColor = color2;
        delta1.GradientMode = linearGradientMode;
        delta1.ForeColor = color3;
        if (e.Column == this.columnOBJECT_ID)
          delta1.HorzAlignment = StringAlignment.Far;
        e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, delta1);
        StyleDelta delta2 = new StyleDelta();
        delta2.BackColor = color1;
        delta2.GradientColor = color2;
        delta2.GradientMode = linearGradientMode;
        delta2.ForeColor = color3;
        if (e.Column == this.columnOBJECT_ID)
          delta2.HorzAlignment = StringAlignment.Far;
        e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, delta2);
      }
    }
  }

  /// <summary>Получить информацию о дочерних элементах</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeDeletingObjects_GetChildren(object sender, GetChildrenEventArgs e)
  {
    DeletingObject deletingObject = e.Row.Item as DeletingObject;
    DeletingObjects deletingObjects = e.Row.Item as DeletingObjects;
    if (deletingObject == null && deletingObjects == null)
      return;
    if (deletingObjects != null)
      e.Children = (IList) deletingObjects;
    else
      e.Children = (IList) deletingObject.Items;
  }

  /// <summary>Получить данные для строки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeDeletingObjects_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is DeletingObject deletingObject))
      return;
    e.RowData.IconSize = 32 /*0x20*/;
    e.RowData.Icon = this.GetObjTypeIcon(deletingObject.ObjectType);
  }

  /// <summary>Установить данные для ячейки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeDeletingObjects_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (e.Column != this.columnCheck || !(e.Row.Item is DeletingObject deletingObject))
      return;
    deletingObject.RemoveObject = (bool) e.NewValue;
  }

  /// <summary>Изменилась колонка для сортировки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeDeletingObjects_SortColumnChanged(object sender, EventArgs e)
  {
    IComparer<DeletingObject> comparer = (IComparer<DeletingObject>) null;
    if (this.treeDeletingObjects.SortColumn == this.columnOBJECT_TYPE)
      comparer = (IComparer<DeletingObject>) new DeletingObjectComparerObjectType(this.columnOBJECT_TYPE.SortDirection == ListSortDirection.Ascending, this.users, this.types);
    if (this.treeDeletingObjects.SortColumn == this.columnOBJECT_ID)
      comparer = (IComparer<DeletingObject>) new DeletingObjectComparerObjectID(this.columnOBJECT_ID.SortDirection == ListSortDirection.Ascending);
    if (this.treeDeletingObjects.SortColumn == this.columnCAPTION)
      comparer = (IComparer<DeletingObject>) new DeletingObjectComparerCaption(this.columnCAPTION.SortDirection == ListSortDirection.Ascending);
    if (this.treeDeletingObjects.SortColumn == this.columnOWNER)
      comparer = (IComparer<DeletingObject>) new DeletingObjectComparerOwnerID(this.columnOWNER.SortDirection == ListSortDirection.Ascending, this.users);
    if (this.treeDeletingObjects.SortColumn == this.columnCHECKEDBY)
      comparer = (IComparer<DeletingObject>) new DeletingObjectComparerChkOutBy(this.columnCHECKEDBY.SortDirection == ListSortDirection.Ascending, this.users);
    if (this.treeDeletingObjects.SortColumn == this.columnLCSTEP)
      comparer = (IComparer<DeletingObject>) new DeletingObjectComparerLCStep(this.columnLCSTEP.SortDirection == ListSortDirection.Ascending, this.users, this.lcsteps);
    this.items.Sort(comparer);
    this.RebuildTree();
  }

  /// <summary>Изменилась выделенная строка в дереве</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeDeletingObjects_SelectionChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Отпущена клавиша в дереве удаляемых объектов</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeDeletingObjects_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyData != Keys.F4)
      return;
    this.btnProperties_Click(sender, (EventArgs) null);
  }

  /// <summary>Показать карточку указанного объекта</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void btnProperties_Click(object sender, EventArgs e)
  {
    DeletingObject deletingObject = this.treeDeletingObjects.SelectedRow != null ? this.treeDeletingObjects.SelectedRow.Item as DeletingObject : (DeletingObject) null;
    if (deletingObject == null)
      return;
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, deletingObject.ObjectID, true);
  }

  /// <summary>Нажата кнопка "Анализ"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoAnalyzeClick(object sender, EventArgs e) => this.DialogResult = DialogResult.No;

  /// <summary>Нажата кнопка "Удалить"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoDeleteClick(object sender, EventArgs e)
  {
    int num1 = this.items.SelectedCount();
    if (num1 == 0)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_576"), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_577"), (object) num1), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
        return;
      this.DialogResult = DialogResult.Yes;
    }
  }

  /// <summary>Нажата кнопка "Выделить все"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoSelectAll(object sender, EventArgs e)
  {
    List<DeletingObject> deletingObjects = this.items.ExtractDeletingObjects();
    for (int index = 0; index < deletingObjects.Count; ++index)
      deletingObjects[index].RemoveObject = true;
    this.RebuildTree();
    this.UpdateControls();
  }

  /// <summary>Нажата кнопка "Убрать отметки"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoDeselectAll(object sender, EventArgs e)
  {
    List<DeletingObject> deletingObjects = this.items.ExtractDeletingObjects();
    for (int index = 0; index < deletingObjects.Count; ++index)
      deletingObjects[index].RemoveObject = false;
    this.RebuildTree();
    this.UpdateControls();
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this.tbComposition.Renderer = renderer;
    this.menuTree.Renderer = renderer;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.tbComposition.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuTree.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DeleteObjectsForm));
    this.treeDeletingObjects = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnCheck = new Column();
    this.cellEditor = new CellEditor();
    this.checkBox1 = new CheckBox();
    this.columnCAPTION = new Column();
    this.columnOBJECT_ID = new Column();
    this.columnOBJECT_TYPE = new Column();
    this.columnLCSTEP = new Column();
    this.columnOWNER = new Column();
    this.columnCHECKEDBY = new Column();
    this.columnNote = new Column();
    this.panelBottom = new Panel();
    this.cbFindAllVersions = new CheckBox();
    this.cbFindLinkedObjects = new CheckBox();
    this.btnAnalyze = new Button();
    this.labelOptions = new Label();
    this.btnCancel = new Button();
    this.btnDelete = new Button();
    this.tbComposition = new Intermech.Bars.ToolBar();
    this.btnProperties = new ButtonItem();
    this.btnSelectAll = new ButtonItem();
    this.btnDeselectAll = new ButtonItem();
    this.menuTree = new MenuBar();
    this.contextMenuTree = new ContextMenuBarItem();
    this.mnpProperties = new MenuButtonItem();
    this.mnpSelectAll = new MenuButtonItem();
    this.mnpDeselectAll = new MenuButtonItem();
    this.treeDeletingObjects.BeginInit();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.treeDeletingObjects.AllowDrop = true;
    this.treeDeletingObjects.AllowMultiSelect = false;
    this.treeDeletingObjects.AllowUserPinnedColumns = false;
    this.treeDeletingObjects.BackColor = SystemColors.Control;
    this.treeDeletingObjects.Columns.Add(this.columnCheck);
    this.treeDeletingObjects.Columns.Add(this.columnCAPTION);
    this.treeDeletingObjects.Columns.Add(this.columnOBJECT_ID);
    this.treeDeletingObjects.Columns.Add(this.columnOBJECT_TYPE);
    this.treeDeletingObjects.Columns.Add(this.columnLCSTEP);
    this.treeDeletingObjects.Columns.Add(this.columnOWNER);
    this.treeDeletingObjects.Columns.Add(this.columnCHECKEDBY);
    this.treeDeletingObjects.Columns.Add(this.columnNote);
    this.treeDeletingObjects.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeDeletingObjects, "treeDeletingObjects");
    this.treeDeletingObjects.Editors.Add(this.cellEditor);
    this.treeDeletingObjects.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("treeDeletingObjects.HeaderStyle.HorzAlignment");
    this.treeDeletingObjects.ImageList = (ImageList) null;
    this.treeDeletingObjects.LineStyle = LineStyle.Dot;
    this.treeDeletingObjects.MainColumn = this.columnCAPTION;
    this.treeDeletingObjects.Name = "treeDeletingObjects";
    this.menuTree.SetPopupMenu((Control) this.treeDeletingObjects, (MenuBarItem) this.contextMenuTree);
    this.treeDeletingObjects.PrefixColumn = this.columnCheck;
    this.treeDeletingObjects.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("treeDeletingObjects.RowEvenStyle.WordWrap");
    this.treeDeletingObjects.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("treeDeletingObjects.RowOddStyle.WordWrap");
    this.treeDeletingObjects.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("treeDeletingObjects.RowSelectedStyle.WordWrap");
    this.treeDeletingObjects.RowStyle.BorderColor = SystemColors.Control;
    this.treeDeletingObjects.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.treeDeletingObjects.RowStyle.BorderWidth = 1;
    this.treeDeletingObjects.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("treeDeletingObjects.RowStyle.WordWrap");
    this.treeDeletingObjects.SelectBeforeEdit = true;
    this.treeDeletingObjects.ShowRootRow = false;
    this.treeDeletingObjects.SuppressErrorMessages = true;
    this.treeDeletingObjects.GetCellData += new GetCellDataHandler(this.treeDeletingObjects_GetCellData);
    this.treeDeletingObjects.GetChildren += new GetChildrenHandler(this.treeDeletingObjects_GetChildren);
    this.treeDeletingObjects.GetRowData += new GetRowDataHandler(this.treeDeletingObjects_GetRowData);
    this.treeDeletingObjects.SelectionChanged += new EventHandler(this.treeDeletingObjects_SelectionChanged);
    this.treeDeletingObjects.SetCellValue += new SetCellValueHandler(this.treeDeletingObjects_SetCellValue);
    this.treeDeletingObjects.SortColumnChanged += new EventHandler(this.treeDeletingObjects_SortColumnChanged);
    this.treeDeletingObjects.KeyUp += new KeyEventHandler(this.treeDeletingObjects_KeyUp);
    componentResourceManager.ApplyResources((object) this.columnCheck, "columnCheck");
    this.columnCheck.CellEditor = this.cellEditor;
    this.columnCheck.CellStyle.BorderWidth = 0;
    this.columnCheck.CellStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCheck.CellStyle.HorzAlignment");
    this.columnCheck.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnCheck.CellStyle.WordWrap");
    this.columnCheck.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCheck.HeaderStyle.HorzAlignment");
    this.columnCheck.Movable = false;
    this.columnCheck.Name = "columnCheck";
    this.columnCheck.Resizable = false;
    this.columnCheck.Sortable = false;
    this.cellEditor.CellAlignment = ContentAlignment.MiddleCenter;
    this.cellEditor.Control = (Control) this.checkBox1;
    this.cellEditor.DisplayMode = CellEditorDisplayMode.Always;
    this.cellEditor.UseCellHeight = false;
    this.cellEditor.UseCellWidth = false;
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    componentResourceManager.ApplyResources((object) this.columnCAPTION, "columnCAPTION");
    this.columnCAPTION.CellStyle.BorderWidth = 1;
    this.columnCAPTION.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnCAPTION.CellStyle.WordWrap");
    this.columnCAPTION.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCAPTION.HeaderStyle.HorzAlignment");
    this.columnCAPTION.Movable = false;
    this.columnCAPTION.Name = "columnCAPTION";
    componentResourceManager.ApplyResources((object) this.columnOBJECT_ID, "columnOBJECT_ID");
    this.columnOBJECT_ID.CellStyle.BorderWidth = 1;
    this.columnOBJECT_ID.CellStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnOBJECT_ID.CellStyle.HorzAlignment");
    this.columnOBJECT_ID.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnOBJECT_ID.CellStyle.WordWrap");
    this.columnOBJECT_ID.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnOBJECT_ID.HeaderStyle.HorzAlignment");
    this.columnOBJECT_ID.HeaderStyle.WordWrap = (bool) componentResourceManager.GetObject("columnOBJECT_ID.HeaderStyle.WordWrap");
    this.columnOBJECT_ID.Movable = false;
    this.columnOBJECT_ID.Name = "columnOBJECT_ID";
    componentResourceManager.ApplyResources((object) this.columnOBJECT_TYPE, "columnOBJECT_TYPE");
    this.columnOBJECT_TYPE.CellStyle.BorderWidth = 0;
    this.columnOBJECT_TYPE.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnOBJECT_TYPE.CellStyle.WordWrap");
    this.columnOBJECT_TYPE.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnOBJECT_TYPE.HeaderStyle.HorzAlignment");
    this.columnOBJECT_TYPE.Movable = false;
    this.columnOBJECT_TYPE.Name = "columnOBJECT_TYPE";
    componentResourceManager.ApplyResources((object) this.columnLCSTEP, "columnLCSTEP");
    this.columnLCSTEP.CellStyle.BorderWidth = 1;
    this.columnLCSTEP.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnLCSTEP.CellStyle.WordWrap");
    this.columnLCSTEP.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnLCSTEP.HeaderStyle.HorzAlignment");
    this.columnLCSTEP.Movable = false;
    this.columnLCSTEP.Name = "columnLCSTEP";
    componentResourceManager.ApplyResources((object) this.columnOWNER, "columnOWNER");
    this.columnOWNER.CellStyle.BorderWidth = 1;
    this.columnOWNER.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnOWNER.CellStyle.WordWrap");
    this.columnOWNER.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnOWNER.HeaderStyle.HorzAlignment");
    this.columnOWNER.Movable = false;
    this.columnOWNER.Name = "columnOWNER";
    componentResourceManager.ApplyResources((object) this.columnCHECKEDBY, "columnCHECKEDBY");
    this.columnCHECKEDBY.CellStyle.BorderWidth = 1;
    this.columnCHECKEDBY.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnCHECKEDBY.CellStyle.WordWrap");
    this.columnCHECKEDBY.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCHECKEDBY.HeaderStyle.HorzAlignment");
    this.columnCHECKEDBY.Movable = false;
    this.columnCHECKEDBY.Name = "columnCHECKEDBY";
    this.columnNote.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnNote, "columnNote");
    this.columnNote.CellStyle.BorderWidth = 1;
    this.columnNote.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnNote.CellStyle.WordWrap");
    this.columnNote.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnNote.HeaderStyle.HorzAlignment");
    this.columnNote.HeaderStyle.WordWrap = (bool) componentResourceManager.GetObject("columnNote.HeaderStyle.WordWrap");
    this.columnNote.Movable = false;
    this.columnNote.Name = "columnNote";
    this.columnNote.Sortable = false;
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.cbFindAllVersions);
    this.panelBottom.Controls.Add((Control) this.cbFindLinkedObjects);
    this.panelBottom.Controls.Add((Control) this.btnAnalyze);
    this.panelBottom.Controls.Add((Control) this.labelOptions);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnDelete);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.cbFindAllVersions, "cbFindAllVersions");
    this.cbFindAllVersions.Name = "cbFindAllVersions";
    this.cbFindAllVersions.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbFindLinkedObjects, "cbFindLinkedObjects");
    this.cbFindLinkedObjects.Name = "cbFindLinkedObjects";
    this.cbFindLinkedObjects.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnAnalyze, "btnAnalyze");
    this.btnAnalyze.Cursor = Cursors.Default;
    this.btnAnalyze.Name = "btnAnalyze";
    this.btnAnalyze.Click += new EventHandler(this.DoAnalyzeClick);
    componentResourceManager.ApplyResources((object) this.labelOptions, "labelOptions");
    this.labelOptions.Name = "labelOptions";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Cursor = Cursors.Default;
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Click += new EventHandler(this.DoDeleteClick);
    this.tbComposition.AllowVerticalDock = false;
    this.tbComposition.Closable = false;
    this.tbComposition.DockLine = 3;
    this.tbComposition.FullMenus = true;
    this.tbComposition.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.tbComposition.Hidden = false;
    this.tbComposition.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.btnProperties,
      (ToolbarItemBase) this.btnSelectAll,
      (ToolbarItemBase) this.btnDeselectAll
    });
    componentResourceManager.ApplyResources((object) this.tbComposition, "tbComposition");
    this.tbComposition.MinimumFloatingSize = new Size(250, 30);
    this.tbComposition.Movable = false;
    this.tbComposition.Name = "tbComposition";
    this.tbComposition.Overflow = ToolBarOverflow.Wrap;
    this.tbComposition.Stretch = true;
    this.btnProperties.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnProperties, "btnProperties");
    this.btnProperties.ImageIndex = 0;
    this.btnProperties.ShowText = true;
    this.btnProperties.Click += new EventHandler(this.btnProperties_Click);
    this.btnSelectAll.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnSelectAll, "btnSelectAll");
    this.btnSelectAll.ShowText = true;
    this.btnSelectAll.Click += new EventHandler(this.DoSelectAll);
    componentResourceManager.ApplyResources((object) this.btnDeselectAll, "btnDeselectAll");
    this.btnDeselectAll.ShowText = true;
    this.btnDeselectAll.Click += new EventHandler(this.DoDeselectAll);
    componentResourceManager.ApplyResources((object) this.menuTree, "menuTree");
    this.menuTree.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuTree.Hidden = false;
    this.menuTree.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuTree
    });
    this.menuTree.Name = "menuTree";
    this.menuTree.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuTree, "contextMenuTree");
    this.contextMenuTree.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.mnpProperties,
      (ToolbarItemBase) this.mnpSelectAll,
      (ToolbarItemBase) this.mnpDeselectAll
    });
    this.contextMenuTree.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpProperties, "mnpProperties");
    this.mnpProperties.ImageIndex = 0;
    this.mnpProperties.ShowText = true;
    this.mnpProperties.Click += new EventHandler(this.btnProperties_Click);
    this.mnpSelectAll.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpSelectAll, "mnpSelectAll");
    this.mnpSelectAll.ImageIndex = 2;
    this.mnpSelectAll.ShowText = true;
    this.mnpSelectAll.Click += new EventHandler(this.DoSelectAll);
    componentResourceManager.ApplyResources((object) this.mnpDeselectAll, "mnpDeselectAll");
    this.mnpDeselectAll.ShowText = true;
    this.mnpDeselectAll.Click += new EventHandler(this.DoDeselectAll);
    this.AcceptButton = (IButtonControl) this.btnDelete;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.treeDeletingObjects);
    this.Controls.Add((Control) this.menuTree);
    this.Controls.Add((Control) this.panelBottom);
    this.Controls.Add((Control) this.tbComposition);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DeleteObjectsForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.DeleteObjectsForm_FormClosed);
    this.Load += new EventHandler(this.DeleteObjectsForm_Load);
    this.treeDeletingObjects.EndInit();
    this.panelBottom.ResumeLayout(false);
    this.panelBottom.PerformLayout();
    this.ResumeLayout(false);
  }
}
