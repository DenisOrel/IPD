
// Type: Intermech.Navigator.ChangingObjectsForm
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

/// <summary>Форма для выбора изменяемых объектов</summary>
public class ChangingObjectsForm : Form
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
  /// <summary>Действия, выполняемые над объектами</summary>
  private ObjectChangingAction action;
  /// <summary>Коллекция описаний изменяемых объектов</summary>
  private ChangingObjects items;
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
  private Intermech.VirtualTreeView.VirtualTreeView treeChangingObjects;
  private Panel panelBottom;
  private Button btnCancel;
  private Button btnAction;
  private Column columnCheck;
  private Column columnCAPTION;
  private Column columnLCSTEP;
  private Column columnOWNER;
  private Column columnCHECKEDBY;
  private Column columnOBJECT_TYPE;
  private Column columnOBJECT_ID;
  private Column columnNote;
  private Button btnDeselectAll;
  private Button btnSelectAll;
  private CellEditor cellEditor1;
  private CheckBox checkBox1;

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="action">Действия, выполняемые над объектами</param>
  /// <param name="items">Коллекция описаний удаляемых объектов</param>
  public ChangingObjectsForm(
    System.IServiceProvider services,
    ObjectChangingAction action,
    ChangingObjects items)
  {
    this.InitializeComponent();
    this.Init(services, action, items);
  }

  /// <summary>Инициализация данных</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="action">Действия, выполняемые над объектами</param>
  /// <param name="items">Коллекция описаний изменяемых объектов</param>
  protected virtual void Init(
    System.IServiceProvider services,
    ObjectChangingAction action,
    ChangingObjects items)
  {
    this.services = services;
    this.action = action;
    this.items = items;
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service1)
    {
      service1.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service1, EventArgs.Empty);
    }
    switch (this.action)
    {
      case ObjectChangingAction.CheckOut:
        this.Text = LocalizationHolder.rm.GetString("Client.Core_556");
        break;
      case ObjectChangingAction.CheckIn:
        this.Text = LocalizationHolder.rm.GetString("Client.Core_555");
        break;
      case ObjectChangingAction.SaveChanges:
        this.Text = LocalizationHolder.rm.GetString("Client.Core_557");
        break;
      case ObjectChangingAction.CancelChanges:
        this.Text = LocalizationHolder.rm.GetString("Client.Core_554");
        break;
    }
    this.LoadAttrValues();
    this.objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this.navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this.userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    INamedImageList service2 = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this.btnProperties.Image = service2.ImageList.Images[service2.ImageIndex("imgCard")];
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 70, primaryWorkingArea.Height / 100 * 60);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.treeChangingObjects.RowSelectedStyle.BackColor = SystemColors.Highlight;
    this.treeChangingObjects.DataSource = (object) items;
    this.RebuildTree();
    this.treeChangingObjects.RootRow.ExpandChildren(true);
    this.UpdateControls();
  }

  /// <summary>Вызвать форму "Настройка интерфейса пользователя"</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="action">Действия, выполняемые над объектами</param>
  /// <param name="items">Коллекция описаний изменяемых объектов</param>
  public static DialogResult Execute(
    System.IServiceProvider services,
    ObjectChangingAction action,
    ChangingObjects items)
  {
    using (ChangingObjectsForm changingObjectsForm = new ChangingObjectsForm(services, action, items))
      return changingObjectsForm.ShowDialog();
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public virtual void UpdateControls()
  {
    this.btnProperties.Enabled = (this.treeChangingObjects.SelectedRow != null ? this.treeChangingObjects.SelectedRow.Item as ChangingObject : (ChangingObject) null) != null;
  }

  /// <summary>Собрать у контролов настройки в коллекцию settings</summary>
  protected virtual void GetControlsState()
  {
    for (int index = 0; index < this.treeChangingObjects.Columns.Count; ++index)
    {
      if (this.settings.ContainsKey(1000 + index))
        this.settings.Remove(1000 + index);
      this.settings.Add(1000 + index, (object) this.treeChangingObjects.Columns[index].Width);
    }
  }

  /// <summary>Установить контролам настройки из коллекции settings</summary>
  protected virtual void SetControlsState()
  {
    for (int index = 0; index < this.treeChangingObjects.Columns.Count; ++index)
    {
      if (this.settings.ContainsKey(1000 + index))
        this.treeChangingObjects.Columns[index].Width = (int) this.settings[1000 + index];
    }
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ChangingObjectsForm_Load(object sender, EventArgs e)
  {
    this.settings.Clear();
    FormStorage.LoadLayout((Control) this, (IDictionary) this.settings);
    this.SetControlsState();
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ChangingObjectsForm_FormClosed(object sender, FormClosedEventArgs e)
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
    Icon objTypeIcon = ImagesResizeHelper.ResizeIconTo32x16(this.objtypesIcons.GetIcon(4, objTypeID), this.treeChangingObjects.RowStyle.BackColor);
    this.typesIcons.Add(objTypeID, objTypeIcon);
    return objTypeIcon;
  }

  /// <summary>Загрузить расшифровки значений атрибутов в кэш</summary>
  protected virtual void LoadAttrValues()
  {
    if (this.items == null || this.items.Count == 0)
      return;
    List<ChangingObject> changingObjects = this.items.ExtractChangingObjects();
    IUserNamesCache userNamesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
    IObjectTypeNamesCache objectTypeNamesCache = CacheManager.Cache("ObjectTypeNamesCache") as IObjectTypeNamesCache;
    IObjectLCStepsCache objectLcStepsCache = CacheManager.Cache("ObjectLCStepsCache") as IObjectLCStepsCache;
    for (int index = 0; index < changingObjects.Count; ++index)
    {
      ChangingObject changingObject = changingObjects[index];
      if (changingObject.OwnerID > 0L && !this.users.ContainsKey(changingObject.OwnerID))
        this.users.Add(changingObject.OwnerID, (object) userNamesCache.GetUserName(changingObject.OwnerID));
      if (changingObject.ChkOutByID > 0L && !this.users.ContainsKey(changingObject.ChkOutByID))
        this.users.Add(changingObject.ChkOutByID, (object) userNamesCache.GetUserName(changingObject.ChkOutByID));
      if (!this.types.ContainsKey(changingObject.ObjectType))
        this.types.Add(changingObject.ObjectType, (object) objectTypeNamesCache.GetTypeName(changingObject.ObjectType));
      if (!this.lcsteps.ContainsKey(changingObject.LCStepID))
        this.lcsteps.Add(changingObject.LCStepID, (object) objectLcStepsCache.GetName(changingObject.LCStepID));
    }
  }

  /// <summary>Перестроить дерево</summary>
  protected virtual void RebuildTree()
  {
    bool disableTreeEvents = this.disableTreeEvents;
    try
    {
      this.disableTreeEvents = true;
      this.treeChangingObjects.UpdateRows(true);
    }
    finally
    {
      this.disableTreeEvents = disableTreeEvents;
    }
  }

  /// <summary>Получить данные для ячейки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeChangingObjects_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (!(e.Row.Item is ChangingObject changingObject))
      return;
    try
    {
      if (e.Column == this.columnCheck)
        e.CellData.Value = (object) changingObject.ApplyChanges;
      else if (e.Column == this.columnOBJECT_TYPE)
        e.CellData.Value = this.types.ContainsKey(changingObject.ObjectType) ? this.types[changingObject.ObjectType] : (object) "?";
      else if (e.Column == this.columnOBJECT_ID)
        e.CellData.Value = (object) changingObject.ObjectID;
      else if (e.Column == this.columnCAPTION)
        e.CellData.Value = (object) CaptionTransform.GetCaption(changingObject.Caption, changingObject.VersionID);
      else if (e.Column == this.columnLCSTEP)
        e.CellData.Value = this.lcsteps.ContainsKey(changingObject.LCStepID) ? this.lcsteps[changingObject.LCStepID] : (object) "?";
      else if (e.Column == this.columnOWNER)
        e.CellData.Value = this.users.ContainsKey(changingObject.OwnerID) ? this.users[changingObject.OwnerID] : (object) "?";
      else if (e.Column == this.columnCHECKEDBY)
      {
        e.CellData.Value = changingObject.ChkOutByID <= 0L || !this.users.ContainsKey(changingObject.ChkOutByID) ? (object) null : this.users[changingObject.ChkOutByID];
      }
      else
      {
        if (e.Column != this.columnNote)
          return;
        e.CellData.Value = (object) changingObject.ChangingNote;
      }
    }
    finally
    {
      if (changingObject.ChkOutByID != 0L && e.Column != this.columnCheck)
      {
        Color color1;
        Color color2;
        LinearGradientMode linearGradientMode;
        Color color3;
        if (changingObject.ChkOutByID == this.userAndRole.UserID)
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
  private void treeChangingObjects_GetChildren(object sender, GetChildrenEventArgs e)
  {
    ChangingObject changingObject = e.Row.Item as ChangingObject;
    ChangingObjects changingObjects = e.Row.Item as ChangingObjects;
    if (changingObject == null && changingObjects == null)
      return;
    if (changingObjects != null)
      e.Children = (IList) changingObjects;
    else
      e.Children = (IList) changingObject.Items;
  }

  /// <summary>Получить данные для строки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeChangingObjects_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is ChangingObject changingObject))
      return;
    e.RowData.IconSize = 32 /*0x20*/;
    e.RowData.Icon = this.GetObjTypeIcon(changingObject.ObjectType);
  }

  /// <summary>Установить данные для ячейки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeChangingObjects_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (e.Column != this.columnCheck || !(e.Row.Item is ChangingObject changingObject) || changingObject.FixApplyChanges)
      return;
    changingObject.ApplyChanges = (bool) e.NewValue;
  }

  /// <summary>Изменилась колонка для сортировки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeChangingObjects_SortColumnChanged(object sender, EventArgs e)
  {
    IComparer<ChangingObject> comparer = (IComparer<ChangingObject>) null;
    if (this.treeChangingObjects.SortColumn == this.columnOBJECT_TYPE)
      comparer = (IComparer<ChangingObject>) new ChangingObjectComparerObjectType(this.columnOBJECT_TYPE.SortDirection == ListSortDirection.Ascending, this.users, this.types);
    if (this.treeChangingObjects.SortColumn == this.columnOBJECT_ID)
      comparer = (IComparer<ChangingObject>) new ChangingObjectComparerObjectID(this.columnOBJECT_ID.SortDirection == ListSortDirection.Ascending);
    if (this.treeChangingObjects.SortColumn == this.columnCAPTION)
      comparer = (IComparer<ChangingObject>) new ChangingObjectComparerCaption(this.columnCAPTION.SortDirection == ListSortDirection.Ascending);
    if (this.treeChangingObjects.SortColumn == this.columnOWNER)
      comparer = (IComparer<ChangingObject>) new ChangingObjectComparerOwnerID(this.columnOWNER.SortDirection == ListSortDirection.Ascending, this.users);
    if (this.treeChangingObjects.SortColumn == this.columnCHECKEDBY)
      comparer = (IComparer<ChangingObject>) new ChangingObjectComparerChkOutBy(this.columnCHECKEDBY.SortDirection == ListSortDirection.Ascending, this.users);
    if (this.treeChangingObjects.SortColumn == this.columnLCSTEP)
      comparer = (IComparer<ChangingObject>) new ChangingObjectComparerLCStep(this.columnLCSTEP.SortDirection == ListSortDirection.Ascending, this.users, this.lcsteps);
    this.items.Sort(comparer);
    this.RebuildTree();
  }

  /// <summary>Изменилась выделенная строка в дереве</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeChangingObjects_SelectionChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Отпущена клавиша в дереве изменяемых объектов</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void treeChangingObjects_KeyUp(object sender, KeyEventArgs e)
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
    ChangingObject changingObject = this.treeChangingObjects.SelectedRow != null ? this.treeChangingObjects.SelectedRow.Item as ChangingObject : (ChangingObject) null;
    if (changingObject == null)
      return;
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, changingObject.ObjectID, true);
  }

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoActionClick(object sender, EventArgs e)
  {
    int num1 = this.items.SelectedCount();
    if (num1 == 0)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_558"), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      if (this.action == ObjectChangingAction.CancelChanges && MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_559"), (object) num1), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
        return;
      this.DialogResult = DialogResult.Yes;
    }
  }

  /// <summary>Нажата кнопка "Выделить все"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoSelectAll(object sender, EventArgs e)
  {
    List<ChangingObject> changingObjects = this.items.ExtractChangingObjects();
    for (int index = 0; index < changingObjects.Count; ++index)
      changingObjects[index].ApplyChanges = true;
    this.RebuildTree();
    this.UpdateControls();
  }

  /// <summary>Нажата кнопка "Убрать отметки"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoDeselectAll(object sender, EventArgs e)
  {
    List<ChangingObject> changingObjects = this.items.ExtractChangingObjects();
    for (int index = 0; index < changingObjects.Count; ++index)
      changingObjects[index].ApplyChanges = false;
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
    this.tbComposition.Renderer = (sender as BarManager).Renderer;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.tbComposition.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChangingObjectsForm));
    this.treeChangingObjects = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnCheck = new Column();
    this.cellEditor1 = new CellEditor();
    this.checkBox1 = new CheckBox();
    this.columnCAPTION = new Column();
    this.columnOBJECT_ID = new Column();
    this.columnOBJECT_TYPE = new Column();
    this.columnLCSTEP = new Column();
    this.columnOWNER = new Column();
    this.columnCHECKEDBY = new Column();
    this.columnNote = new Column();
    this.panelBottom = new Panel();
    this.btnDeselectAll = new Button();
    this.btnSelectAll = new Button();
    this.btnCancel = new Button();
    this.btnAction = new Button();
    this.tbComposition = new Intermech.Bars.ToolBar();
    this.btnProperties = new ButtonItem();
    this.treeChangingObjects.BeginInit();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.treeChangingObjects.AllowDrop = true;
    this.treeChangingObjects.AllowMultiSelect = false;
    this.treeChangingObjects.AllowUserPinnedColumns = false;
    this.treeChangingObjects.BackColor = SystemColors.Control;
    this.treeChangingObjects.Columns.Add(this.columnCheck);
    this.treeChangingObjects.Columns.Add(this.columnCAPTION);
    this.treeChangingObjects.Columns.Add(this.columnOBJECT_ID);
    this.treeChangingObjects.Columns.Add(this.columnOBJECT_TYPE);
    this.treeChangingObjects.Columns.Add(this.columnLCSTEP);
    this.treeChangingObjects.Columns.Add(this.columnOWNER);
    this.treeChangingObjects.Columns.Add(this.columnCHECKEDBY);
    this.treeChangingObjects.Columns.Add(this.columnNote);
    this.treeChangingObjects.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeChangingObjects, "treeChangingObjects");
    this.treeChangingObjects.Editors.Add(this.cellEditor1);
    this.treeChangingObjects.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("treeChangingObjects.HeaderStyle.HorzAlignment");
    this.treeChangingObjects.ImageList = (ImageList) null;
    this.treeChangingObjects.LineStyle = LineStyle.Dot;
    this.treeChangingObjects.MainColumn = this.columnCAPTION;
    this.treeChangingObjects.Name = "treeChangingObjects";
    this.treeChangingObjects.PrefixColumn = this.columnCheck;
    this.treeChangingObjects.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("treeChangingObjects.RowEvenStyle.WordWrap");
    this.treeChangingObjects.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("treeChangingObjects.RowOddStyle.WordWrap");
    this.treeChangingObjects.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("treeChangingObjects.RowSelectedStyle.WordWrap");
    this.treeChangingObjects.RowStyle.BorderColor = SystemColors.Control;
    this.treeChangingObjects.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.treeChangingObjects.RowStyle.BorderWidth = 1;
    this.treeChangingObjects.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("treeChangingObjects.RowStyle.WordWrap");
    this.treeChangingObjects.SelectBeforeEdit = true;
    this.treeChangingObjects.ShowRootRow = false;
    this.treeChangingObjects.SuppressErrorMessages = true;
    this.treeChangingObjects.GetCellData += new GetCellDataHandler(this.treeChangingObjects_GetCellData);
    this.treeChangingObjects.GetChildren += new GetChildrenHandler(this.treeChangingObjects_GetChildren);
    this.treeChangingObjects.GetRowData += new GetRowDataHandler(this.treeChangingObjects_GetRowData);
    this.treeChangingObjects.SelectionChanged += new EventHandler(this.treeChangingObjects_SelectionChanged);
    this.treeChangingObjects.SetCellValue += new SetCellValueHandler(this.treeChangingObjects_SetCellValue);
    this.treeChangingObjects.SortColumnChanged += new EventHandler(this.treeChangingObjects_SortColumnChanged);
    this.treeChangingObjects.KeyUp += new KeyEventHandler(this.treeChangingObjects_KeyUp);
    componentResourceManager.ApplyResources((object) this.columnCheck, "columnCheck");
    this.columnCheck.CellEditor = this.cellEditor1;
    this.columnCheck.CellStyle.BorderWidth = 0;
    this.columnCheck.CellStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCheck.CellStyle.HorzAlignment");
    this.columnCheck.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnCheck.CellStyle.WordWrap");
    this.columnCheck.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCheck.HeaderStyle.HorzAlignment");
    this.columnCheck.Movable = false;
    this.columnCheck.Name = "columnCheck";
    this.columnCheck.Resizable = false;
    this.columnCheck.Sortable = false;
    this.cellEditor1.CellAlignment = ContentAlignment.MiddleCenter;
    this.cellEditor1.Control = (Control) this.checkBox1;
    this.cellEditor1.DisplayMode = CellEditorDisplayMode.Always;
    this.cellEditor1.UseCellHeight = false;
    this.cellEditor1.UseCellWidth = false;
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
    this.panelBottom.Controls.Add((Control) this.btnDeselectAll);
    this.panelBottom.Controls.Add((Control) this.btnSelectAll);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnAction);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnDeselectAll, "btnDeselectAll");
    this.btnDeselectAll.Cursor = Cursors.Default;
    this.btnDeselectAll.Name = "btnDeselectAll";
    this.btnDeselectAll.Click += new EventHandler(this.DoDeselectAll);
    componentResourceManager.ApplyResources((object) this.btnSelectAll, "btnSelectAll");
    this.btnSelectAll.Cursor = Cursors.Default;
    this.btnSelectAll.Name = "btnSelectAll";
    this.btnSelectAll.Click += new EventHandler(this.DoSelectAll);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnAction, "btnAction");
    this.btnAction.Cursor = Cursors.Default;
    this.btnAction.Name = "btnAction";
    this.btnAction.Click += new EventHandler(this.DoActionClick);
    this.tbComposition.AllowVerticalDock = false;
    this.tbComposition.Closable = false;
    this.tbComposition.DockLine = 3;
    this.tbComposition.FullMenus = true;
    this.tbComposition.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.tbComposition.Hidden = false;
    this.tbComposition.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.btnProperties
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
    this.AcceptButton = (IButtonControl) this.btnAction;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.treeChangingObjects);
    this.Controls.Add((Control) this.panelBottom);
    this.Controls.Add((Control) this.tbComposition);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ChangingObjectsForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.ChangingObjectsForm_FormClosed);
    this.Load += new EventHandler(this.ChangingObjectsForm_Load);
    this.treeChangingObjects.EndInit();
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
