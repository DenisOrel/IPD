
// Type: Intermech.Navigator.Controls.EditingContextsEventLogForm
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
using Intermech.Interfaces.Contexts;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Форма для отображения журнала событий контекста редактирования
/// </summary>
public class EditingContextsEventLogForm : Form
{
  /// <summary>Журнал событий</summary>
  private EditingContextsLog _log;
  /// <summary>Запрет на обработку событий от дерева</summary>
  private bool disableTreeEvents;
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
  /// <summary>Кэш имён пользователей</summary>
  private Dictionary<long, object> users = new Dictionary<long, object>();
  /// <summary>Кэш названий типов объектов</summary>
  private Dictionary<int, object> types = new Dictionary<int, object>();
  /// <summary>Кэш названий шагов ЖЦ</summary>
  private Dictionary<int, object> lcsteps = new Dictionary<int, object>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.VirtualTreeView.VirtualTreeView treeObjects;
  private Column columnCAPTION;
  private Column columnLCSTEP;
  private Column columnOWNER;
  private Column columnCHECKEDBY;
  private Column columnOBJECT_TYPE;
  private Column columnOBJECT_ID;
  private Column columnNote;
  private Panel panelBottom;
  private Button btnCancel;
  private Intermech.Bars.ToolBar tbMain;
  private ButtonItem btnProperties;

  /// <summary>Создать экземпляр формы</summary>
  public EditingContextsEventLogForm()
  {
    this.InitializeComponent();
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service1)
    {
      service1.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service1, EventArgs.Empty);
    }
    INamedImageList service2 = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this.btnProperties.Image = service2.ImageList.Images[service2.ImageIndex("imgCard")];
    this.objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this.navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this.userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this.treeObjects.RowSelectedStyle.BackColor = SystemColors.Highlight;
    this.tbMain.Visible = false;
  }

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="log">Журнал событий</param>
  public EditingContextsEventLogForm(EditingContextsLog log)
    : this()
  {
    this._log = log;
    this.LoadData();
  }

  /// <summary>Вызвать форму с указанными параметрами</summary>
  /// <param name="log">Журнал событий</param>
  [STAThread]
  public static void Execute(EditingContextsLog log)
  {
    using (EditingContextsEventLogForm contextsEventLogForm = new EditingContextsEventLogForm(log))
    {
      int num = (int) contextsEventLogForm.ShowDialog();
    }
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Параметры</param>
  private void EditingContextsEventLogForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Параметры</param>
  private void EditingContextsEventLogForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Очистить все внутренние структуры</summary>
  internal void Clear()
  {
  }

  /// <summary>Загрузить информацию в форму</summary>
  internal void LoadData()
  {
    this.Clear();
    this.LoadAttrValues();
    this.treeObjects.DataSource = (object) this;
    this.RebuildTree();
    this.treeObjects.RootRow.ExpandChildren(true);
    this.UpdateControls();
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public void UpdateControls() => this.btnCancel.Enabled = true;

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    this.tbMain.Renderer = (sender as BarManager).Renderer;
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
    Icon objTypeIcon = ImagesResizeHelper.ResizeIconTo32x16(this.objtypesIcons.GetIcon(4, objTypeID), this.treeObjects.RowStyle.BackColor);
    this.typesIcons.Add(objTypeID, objTypeIcon);
    return objTypeIcon;
  }

  /// <summary>Загрузить расшифровки значений атрибутов в кэш</summary>
  protected virtual void LoadAttrValues()
  {
    if (this._log == null || this._log.Count == 0)
      return;
    IUserNamesCache userNamesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
    IObjectTypeNamesCache objectTypeNamesCache = CacheManager.Cache("ObjectTypeNamesCache") as IObjectTypeNamesCache;
    IObjectLCStepsCache objectLcStepsCache = CacheManager.Cache("ObjectLCStepsCache") as IObjectLCStepsCache;
    List<long> versions = this._log.ExtractVersions();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<object> objectList = ObjectVersionDescriptionsHelper.LoadUnsortedDescriptions(sessionKeeper.Session, typeof (ObjectVersionDescription), (IList<long>) versions, -1);
      for (int index1 = 0; index1 < this._log.Count; ++index1)
      {
        EditingContextsLogEntry contextsLogEntry = this._log[index1];
        if (index1 < objectList.Count)
        {
          contextsLogEntry.Tag = objectList[index1];
        }
        else
        {
          for (int index2 = 0; index2 < objectList.Count; ++index2)
          {
            if (objectList[index2] is ObjectVersionDescription versionDescription && contextsLogEntry.ObjectID == versionDescription.F_OBJECT_ID)
            {
              contextsLogEntry.Tag = (object) versionDescription;
              break;
            }
          }
        }
        if (contextsLogEntry.Tag is ObjectVersionDescription tag)
        {
          if (tag.F_OWNER_ID > 0L && !this.users.ContainsKey(tag.F_OWNER_ID))
            this.users.Add(tag.F_OWNER_ID, (object) userNamesCache.GetUserName(tag.F_OWNER_ID));
          if (tag.F_CHKOUT_BY > 0L && !this.users.ContainsKey(tag.F_CHKOUT_BY))
            this.users.Add(tag.F_CHKOUT_BY, (object) userNamesCache.GetUserName(tag.F_CHKOUT_BY));
          if (!this.types.ContainsKey(tag.F_OBJECT_TYPE))
            this.types.Add(tag.F_OBJECT_TYPE, (object) objectTypeNamesCache.GetTypeName(tag.F_OBJECT_TYPE));
          if (!this.lcsteps.ContainsKey(tag.F_LCSTEP_ID))
            this.lcsteps.Add(tag.F_LCSTEP_ID, (object) objectLcStepsCache.GetName(tag.F_LCSTEP_ID));
        }
      }
    }
  }

  /// <summary>Перестроить дерево</summary>
  protected virtual void RebuildTree()
  {
    bool disableTreeEvents = this.disableTreeEvents;
    try
    {
      this.disableTreeEvents = true;
      this.treeObjects.UpdateRows(true);
    }
    finally
    {
      this.disableTreeEvents = disableTreeEvents;
    }
  }

  /// <summary>Получить информацию о дочернем узле</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeObjects_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item != this)
      return;
    e.Children = (IList) this._log;
  }

  /// <summary>Получить информацию о строке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeObjects_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is EditingContextsLogEntry contextsLogEntry) || !(contextsLogEntry.Tag is ObjectVersionDescription tag))
      return;
    e.RowData.IconSize = 32 /*0x20*/;
    e.RowData.Icon = this.GetObjTypeIcon(tag.F_OBJECT_TYPE);
  }

  /// <summary>Получить данные для ячейки дерева</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeObjects_GetCellData(object sender, GetCellDataEventArgs e)
  {
    ObjectVersionDescription tag = e.Row.Item is EditingContextsLogEntry entry ? entry.Tag as ObjectVersionDescription : (ObjectVersionDescription) null;
    if (tag == null)
      return;
    try
    {
      if (e.Column == this.columnOBJECT_TYPE)
        e.CellData.Value = this.types.ContainsKey(tag.F_OBJECT_TYPE) ? this.types[tag.F_OBJECT_TYPE] : (object) "?";
      else if (e.Column == this.columnOBJECT_ID)
        e.CellData.Value = (object) tag.F_OBJECT_ID;
      else if (e.Column == this.columnCAPTION)
        e.CellData.Value = (object) CaptionTransform.GetCaption(tag.CAPTION, tag.F_VERSION_ID);
      else if (e.Column == this.columnLCSTEP)
        e.CellData.Value = this.lcsteps.ContainsKey(tag.F_LCSTEP_ID) ? this.lcsteps[tag.F_LCSTEP_ID] : (object) "?";
      else if (e.Column == this.columnOWNER)
        e.CellData.Value = this.users.ContainsKey(tag.F_OWNER_ID) ? this.users[tag.F_OWNER_ID] : (object) "?";
      else if (e.Column == this.columnCHECKEDBY)
      {
        e.CellData.Value = tag.F_CHKOUT_BY <= 0L || !this.users.ContainsKey(tag.F_CHKOUT_BY) ? (object) null : this.users[tag.F_CHKOUT_BY];
      }
      else
      {
        if (e.Column != this.columnNote)
          return;
        e.CellData.Value = (object) EditingContextsLog.GetEntryText(entry);
      }
    }
    finally
    {
      if (tag.F_CHKOUT_BY != 0L)
      {
        Color color1;
        Color color2;
        LinearGradientMode linearGradientMode;
        Color color3;
        if (tag.F_CHKOUT_BY == this.userAndRole.UserID)
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

  /// <summary>Изменилась выделенная строка в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeObjects_SelectionChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.tbMain.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditingContextsEventLogForm));
    this.treeObjects = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnCAPTION = new Column();
    this.columnNote = new Column();
    this.columnOBJECT_ID = new Column();
    this.columnOBJECT_TYPE = new Column();
    this.columnLCSTEP = new Column();
    this.columnOWNER = new Column();
    this.columnCHECKEDBY = new Column();
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.tbMain = new Intermech.Bars.ToolBar();
    this.btnProperties = new ButtonItem();
    this.treeObjects.BeginInit();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.treeObjects, "treeObjects");
    this.treeObjects.AllowDrop = true;
    this.treeObjects.AllowMultiSelect = false;
    this.treeObjects.BackColor = SystemColors.Control;
    this.treeObjects.BackgroundImageMode = ImageDrawMode.Tile;
    this.treeObjects.BorderStyle = BorderStyle.Fixed3D;
    this.treeObjects.Columns.Add(this.columnCAPTION);
    this.treeObjects.Columns.Add(this.columnNote);
    this.treeObjects.Columns.Add(this.columnOBJECT_ID);
    this.treeObjects.Columns.Add(this.columnOBJECT_TYPE);
    this.treeObjects.Columns.Add(this.columnLCSTEP);
    this.treeObjects.Columns.Add(this.columnOWNER);
    this.treeObjects.Columns.Add(this.columnCHECKEDBY);
    this.treeObjects.DisableHeaderContextMenu = true;
    this.treeObjects.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("treeObjects.HeaderStyle.HorzAlignment");
    this.treeObjects.ImageList = (ImageList) null;
    this.treeObjects.LineStyle = LineStyle.Dot;
    this.treeObjects.MainColumn = this.columnCAPTION;
    this.treeObjects.Name = "treeObjects";
    this.treeObjects.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.treeObjects.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.treeObjects.RowStyle.BorderColor = SystemColors.Control;
    this.treeObjects.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.treeObjects.RowStyle.BorderWidth = 1;
    this.treeObjects.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("treeObjects.RowStyle.WordWrap");
    this.treeObjects.SelectBeforeEdit = true;
    this.treeObjects.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.treeObjects.ShowRootRow = false;
    this.treeObjects.SuppressErrorMessages = true;
    this.treeObjects.GetCellData += new GetCellDataHandler(this.treeObjects_GetCellData);
    this.treeObjects.GetChildren += new GetChildrenHandler(this.treeObjects_GetChildren);
    this.treeObjects.GetRowData += new GetRowDataHandler(this.treeObjects_GetRowData);
    this.treeObjects.SelectionChanged += new EventHandler(this.treeObjects_SelectionChanged);
    componentResourceManager.ApplyResources((object) this.columnCAPTION, "columnCAPTION");
    this.columnCAPTION.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    this.columnCAPTION.CellStyle.BorderWidth = 1;
    this.columnCAPTION.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnCAPTION.CellStyle.WordWrap");
    this.columnCAPTION.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCAPTION.HeaderStyle.HorzAlignment");
    this.columnCAPTION.Movable = false;
    this.columnCAPTION.Name = "columnCAPTION";
    this.columnCAPTION.Sortable = false;
    this.columnCAPTION.SortDirection = ListSortDirection.Ascending;
    componentResourceManager.ApplyResources((object) this.columnNote, "columnNote");
    this.columnNote.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnNote.CellStyle.BorderWidth = 1;
    this.columnNote.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnNote.CellStyle.WordWrap");
    this.columnNote.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnNote.HeaderStyle.HorzAlignment");
    this.columnNote.HeaderStyle.WordWrap = (bool) componentResourceManager.GetObject("columnNote.HeaderStyle.WordWrap");
    this.columnNote.Movable = false;
    this.columnNote.Name = "columnNote";
    this.columnNote.Sortable = false;
    this.columnNote.SortDirection = ListSortDirection.Ascending;
    componentResourceManager.ApplyResources((object) this.columnOBJECT_ID, "columnOBJECT_ID");
    this.columnOBJECT_ID.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    this.columnOBJECT_ID.CellStyle.BorderWidth = 1;
    this.columnOBJECT_ID.CellStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnOBJECT_ID.CellStyle.HorzAlignment");
    this.columnOBJECT_ID.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnOBJECT_ID.CellStyle.WordWrap");
    this.columnOBJECT_ID.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnOBJECT_ID.HeaderStyle.HorzAlignment");
    this.columnOBJECT_ID.HeaderStyle.WordWrap = (bool) componentResourceManager.GetObject("columnOBJECT_ID.HeaderStyle.WordWrap");
    this.columnOBJECT_ID.Movable = false;
    this.columnOBJECT_ID.Name = "columnOBJECT_ID";
    this.columnOBJECT_ID.Sortable = false;
    this.columnOBJECT_ID.SortDirection = ListSortDirection.Ascending;
    componentResourceManager.ApplyResources((object) this.columnOBJECT_TYPE, "columnOBJECT_TYPE");
    this.columnOBJECT_TYPE.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    this.columnOBJECT_TYPE.CellStyle.BorderWidth = 0;
    this.columnOBJECT_TYPE.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnOBJECT_TYPE.CellStyle.WordWrap");
    this.columnOBJECT_TYPE.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnOBJECT_TYPE.HeaderStyle.HorzAlignment");
    this.columnOBJECT_TYPE.Movable = false;
    this.columnOBJECT_TYPE.Name = "columnOBJECT_TYPE";
    this.columnOBJECT_TYPE.Sortable = false;
    this.columnOBJECT_TYPE.SortDirection = ListSortDirection.Ascending;
    componentResourceManager.ApplyResources((object) this.columnLCSTEP, "columnLCSTEP");
    this.columnLCSTEP.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    this.columnLCSTEP.CellStyle.BorderWidth = 1;
    this.columnLCSTEP.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnLCSTEP.CellStyle.WordWrap");
    this.columnLCSTEP.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnLCSTEP.HeaderStyle.HorzAlignment");
    this.columnLCSTEP.Movable = false;
    this.columnLCSTEP.Name = "columnLCSTEP";
    this.columnLCSTEP.Sortable = false;
    this.columnLCSTEP.SortDirection = ListSortDirection.Ascending;
    componentResourceManager.ApplyResources((object) this.columnOWNER, "columnOWNER");
    this.columnOWNER.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    this.columnOWNER.CellStyle.BorderWidth = 1;
    this.columnOWNER.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnOWNER.CellStyle.WordWrap");
    this.columnOWNER.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnOWNER.HeaderStyle.HorzAlignment");
    this.columnOWNER.Movable = false;
    this.columnOWNER.Name = "columnOWNER";
    this.columnOWNER.Sortable = false;
    this.columnOWNER.SortDirection = ListSortDirection.Ascending;
    componentResourceManager.ApplyResources((object) this.columnCHECKEDBY, "columnCHECKEDBY");
    this.columnCHECKEDBY.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    this.columnCHECKEDBY.CellStyle.BorderWidth = 1;
    this.columnCHECKEDBY.CellStyle.WordWrap = (bool) componentResourceManager.GetObject("columnCHECKEDBY.CellStyle.WordWrap");
    this.columnCHECKEDBY.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCHECKEDBY.HeaderStyle.HorzAlignment");
    this.columnCHECKEDBY.Movable = false;
    this.columnCHECKEDBY.Name = "columnCHECKEDBY";
    this.columnCHECKEDBY.Sortable = false;
    this.columnCHECKEDBY.SortDirection = ListSortDirection.Ascending;
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.tbMain, "tbMain");
    this.tbMain.AllowVerticalDock = false;
    this.tbMain.Closable = false;
    this.tbMain.DockLine = 3;
    this.tbMain.FullMenus = true;
    this.tbMain.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.tbMain.Hidden = false;
    this.tbMain.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.btnProperties
    });
    this.tbMain.MinimumFloatingSize = new Size(250, 30);
    this.tbMain.Movable = false;
    this.tbMain.Name = "tbMain";
    this.tbMain.Overflow = ToolBarOverflow.Wrap;
    this.tbMain.Stretch = true;
    this.btnProperties.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnProperties, "btnProperties");
    this.btnProperties.ImageIndex = 0;
    this.btnProperties.ShowText = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.treeObjects);
    this.Controls.Add((Control) this.tbMain);
    this.Controls.Add((Control) this.panelBottom);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditingContextsEventLogForm);
    this.SizeGripStyle = SizeGripStyle.Show;
    this.FormClosed += new FormClosedEventHandler(this.EditingContextsEventLogForm_FormClosed);
    this.Load += new EventHandler(this.EditingContextsEventLogForm_Load);
    this.treeObjects.EndInit();
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
