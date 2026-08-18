// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DataEditor
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>
/// Реализует редактор настроек инструмента типа "Редактор документов".
/// </summary>
internal sealed class DataEditor : DataEditorControl, ISelectorFilter, INodeSelectorFilter
{
  /// <summary>Редактируемые настройки</summary>
  private IMDocEditorToolSettings toolSettings;
  /// <summary>
  /// Коллекция значков для типов объектов
  /// [(Int32)Идентификатор типа объекта] = [(Icon)Значок]
  /// </summary>
  private Dictionary<int, Icon> typesIcons = new Dictionary<int, Icon>();
  /// <summary>Сервис именованных изображений</summary>
  private INamedImageList images;
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService objtypesIcons;
  /// <summary>Кэш графических объектов "Навигатора"</summary>
  private INavGraphicsCache navGraphicsCache;
  /// <summary>[неизвестный тип объекта]</summary>
  private readonly string Message_0 = LocalizationHolder.rm.GetString("Document.Client_119");
  /// <summary>[шаблон не задан]</summary>
  private readonly string Message_1 = LocalizationHolder.rm.GetString("Document.Client_120");
  /// <summary>[выберите объект-шаблон]</summary>
  private readonly string Message_2 = LocalizationHolder.rm.GetString("Document.Client_121");
  /// <summary>Обработчик</summary>
  private DevExpress.IM.XtraEditors.Controls.ButtonPressedEventHandler handler;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel;
  private PictureBox pictureHint;
  private Label labelHint;
  private Panel panelMain;
  private Intermech.VirtualTreeView.VirtualTreeView treeParentObjects;
  private Column columnObjectType;
  private Panel panelRight;
  private Column columnTemplate;
  private Button btnAdd;
  private Button btnClear;
  private Button btnDelete;
  private Button btnDefaults;
  private CellEditor cellEditor1;
  private ButtonEdit buttonEdit1;

  public DataEditor() => this.InitializeComponent();

  private void DataEditor_Load(object sender, EventArgs e)
  {
  }

  /// <summary>Передает редактору объект с настройками.</summary>
  /// <param name="data">Настройки</param>
  /// <param name="readOnly">Признак режима отображения настроек без возможности редактирования</param>
  public override void SetData(XmlDocument data, bool readOnly)
  {
    base.SetData(data, readOnly);
    this.Init();
    this.toolSettings = IMDocEditorToolSettingsCodec.Decode(data);
    this.FillObjectTypesTree(true);
    this.UpdateControls();
  }

  /// <summary>
  /// Редактор возвращает новый объект настроек, содержащий все сделанные пользователем изменения.
  /// </summary>
  /// <returns>Объект с настройками</returns>
  public override XmlDocument GetData()
  {
    XmlDocument data = new XmlDocument();
    IMDocEditorToolSettingsCodec.Encode(data, this.toolSettings);
    return data;
  }

  /// <summary>Обновить статусы контролов</summary>
  private void UpdateControls()
  {
    if (this.toolSettings != null)
    {
      this.btnAdd.Enabled = !this.ReadOnly;
      this.btnDelete.Enabled = this.treeParentObjects.SelectedRows.Count > 0 && !this.ReadOnly;
      this.btnClear.Enabled = this.toolSettings.Count > 0 && !this.ReadOnly;
    }
    else
    {
      this.btnAdd.Enabled = false;
      this.btnClear.Enabled = false;
      this.btnDelete.Enabled = false;
    }
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  private Icon GetObjTypeIcon(int objTypeID, Color backColor)
  {
    if (!MetaDataHelper.ExistsObjectType(objTypeID))
      return (Icon) null;
    objTypeID = Math.Max(objTypeID, -1);
    if (this.typesIcons.ContainsKey(objTypeID))
      return this.typesIcons[objTypeID];
    if (this.objtypesIcons.IndexOf(4, objTypeID) < 0)
      return (Icon) null;
    Icon objTypeIcon = ImagesResizeHelper.ResizeIconTo32x16(this.objtypesIcons.GetIcon(4, objTypeID), backColor);
    this.typesIcons.Add(objTypeID, objTypeIcon);
    return objTypeIcon;
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  private int GetObjTypeIconIndex(int objTypeID)
  {
    if (!MetaDataHelper.ExistsObjectType(objTypeID))
      return -1;
    objTypeID = Math.Max(objTypeID, -1);
    return this.objtypesIcons.IndexOf(4, objTypeID);
  }

  /// <summary>
  /// Выполнить инициализацию полей редактора, получить ссылки на службы
  /// </summary>
  private void Init()
  {
    this.images = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this.objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this.navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this.FillObjectTypesTree(true);
    this.UpdateControls();
  }

  /// <summary>Заполнить дерево типов объектов</summary>
  /// <param name="resetDatasource">Переназначать источник данных</param>
  private void FillObjectTypesTree(bool resetDatasource)
  {
    if (resetDatasource || this.treeParentObjects.DataSource == null)
      this.treeParentObjects.DataSource = (object) this.toolSettings;
    this.treeParentObjects.UpdateRows(true);
    this.UpdateControls();
  }

  /// <summary>Обработка события "Добавить"</summary>
  private void Add()
  {
    if (this.toolSettings == null)
      return;
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Document.Client_122"), typeof (ObjectTypeFolder), true);
    selectorForm.SelectorFilter = (ISelectorFilter) this;
    selectorForm.NodeSelectorFilter = (INodeSelectorFilter) this;
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    foreach (int id in selectorForm.IDList)
    {
      if (MetaDataHelper.ExistsObjectType(id) && this.toolSettings[id] == null)
        this.toolSettings[id] = new IMDocObjectTypeSettings(id, Guid.Empty);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.toolSettings.SyncMetaData();
      this.toolSettings.SyncObjectsData(sessionKeeper.Session);
    }
    this.FillObjectTypesTree(false);
    this.RaiseDataChanged();
  }

  /// <summary>Обработка события "Удалить"</summary>
  private void Remove()
  {
    if (this.toolSettings == null || this.treeParentObjects.SelectedRows.Count == 0)
      return;
    List<int> intList = new List<int>();
    for (int index = 0; index < this.treeParentObjects.SelectedRows.Count; ++index)
      intList.Add((int) this.treeParentObjects.SelectedRows[index].Item);
    for (int index = 0; index < intList.Count; ++index)
      this.toolSettings[intList[index]] = (IMDocObjectTypeSettings) null;
    this.FillObjectTypesTree(false);
    this.RaiseDataChanged();
  }

  /// <summary>Обработка события - "По умолчанию"</summary>
  private void Defaults()
  {
    if (this.toolSettings == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.toolSettings.ResetToDefaults(sessionKeeper.Session);
    this.FillObjectTypesTree(false);
    this.RaiseDataChanged();
  }

  /// <summary>Обработка события "Очистить"</summary>
  private void Clear()
  {
    if (this.toolSettings == null || this.toolSettings.Count == 0)
      return;
    this.toolSettings.Clear();
    this.FillObjectTypesTree(false);
    this.RaiseDataChanged();
  }

  /// <summary>Нажата кнопка "Добавить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoAdd(object sender, EventArgs e) => this.Add();

  /// <summary>Нажата кнопка "Удалить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoRemove(object sender, EventArgs e) => this.Remove();

  /// <summary>Нажата кнопка "Очистить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoClear(object sender, EventArgs e) => this.Clear();

  /// <summary>Нажата кнопка "По умолчанию"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoDefaults(object sender, EventArgs e) => this.Defaults();

  /// <summary>Получить дочерние узлы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (!(e.Row.Item is IMDocEditorToolSettings))
      return;
    e.Children = (IList) (e.Row.Item as IMDocEditorToolSettings).SupportedTypeIDs;
  }

  /// <summary>Получить данные для указанной строки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is int))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType((int) e.Row.Item);
    if (objectType == null)
      return;
    e.RowData.Icon = this.GetObjTypeIcon(objectType.ObjectTypeID, Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue));
    e.RowData.IconSize = e.RowData.Icon.Width;
  }

  /// <summary>Получить данные для указанной ячейки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (!(e.Row.Item is int objTypeID))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
    IMDocObjectTypeSettings toolSetting = objectType != null ? this.toolSettings[objectType.ObjectTypeID] : (IMDocObjectTypeSettings) null;
    if (e.Column == this.columnObjectType)
      e.CellData.Value = objectType != null ? (object) objectType.ObjectTypeName : (object) this.Message_0;
    if (e.Column != this.columnTemplate)
      return;
    e.CellData.Value = toolSetting == null || !(toolSetting.TemplateGuid != Guid.Empty) ? (object) this.Message_1 : (object) toolSetting.TemplateCaption;
    Color color = Color.FromArgb(0, 0, (int) byte.MaxValue);
    e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, new StyleDelta()
    {
      ForeColor = color
    });
    e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, new StyleDelta()
    {
      ForeColor = color
    });
  }

  /// <summary>Установить значение в указанной ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_SetCellValue(object sender, SetCellValueEventArgs e)
  {
  }

  /// <summary>Показать контекстное меню в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_ShowContextMenu(object sender, MouseEventArgs e)
  {
  }

  /// <summary>Изменилась сфокусированная ячейка в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoUpdateControls(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>
  /// Проверить, можно ли отображать указанный объект в дереве окна по выбору типов объектов
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="id">Объект</param>
  /// <returns>true, если нельзя отображать</returns>
  public bool IsInFilter(int category, object id)
  {
    return category == 4 && (int) id != MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>Можно ли выбирать указанный узел</summary>
  /// <param name="category">Категория</param>
  /// <param name="id">Идентификатор</param>
  /// <param name="errorMessage">Если значение не равно String.Empty, то оно будет отображено в статусной строке окна</param>
  /// <returns>true, если выбор узла разрешён</returns>
  public bool CanSelectNode(int category, object id, out string errorMessage)
  {
    errorMessage = string.Empty;
    if (category != 4)
      return false;
    int objTypeID = (int) id;
    errorMessage = this.toolSettings[objTypeID] == null ? string.Empty : LocalizationHolder.rm.GetString("Document.Client_123");
    return errorMessage == string.Empty;
  }

  /// <summary>Установить значение редактора</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void cellEditor1_SetControlValue(object sender, CellEditorSetValueEventArgs e)
  {
    IMDocObjectTypeSettings toolSetting = this.toolSettings[(int) e.CellWidget.Row.Item];
    if (toolSetting == null)
      return;
    e.Control.Tag = toolSetting.Clone();
    e.Control.Text = toolSetting.TemplateGuid != Guid.Empty ? toolSetting.TemplateCaption : this.Message_2;
  }

  /// <summary>Получить значение редактора</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void cellEditor1_GetControlValue(object sender, CellEditorGetValueEventArgs e)
  {
    IMDocObjectTypeSettings toolSetting = this.toolSettings[(int) e.CellWidget.Row.Item];
    if (toolSetting == null)
      return;
    toolSetting.Assign(e.Control.Tag);
    e.Value = toolSetting.TemplateGuid != Guid.Empty ? (object) toolSetting.TemplateCaption : (object) this.Message_2;
    e.Control.Text = e.Value.ToString();
    this.RaiseDataChanged();
  }

  /// <summary>Инициализировать редактор</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void cellEditor1_InitializeControl(object sender, CellEditorInitializeEventArgs e)
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DataEditor));
    ((ButtonEdit) e.Control).Properties.Buttons.Clear();
    ((ButtonEdit) e.Control).Properties.Buttons.AddRange(new EditorButton[2]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("buttonEdit1.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), LocalizationHolder.rm.GetString("Document.Client_124"), (object) 0),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("buttonEdit1.Properties.Buttons1"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), LocalizationHolder.rm.GetString("Document.Client_125"), (object) 1)
    });
    ((ButtonEdit) e.Control).Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    if (this.handler == null)
      this.handler = new DevExpress.IM.XtraEditors.Controls.ButtonPressedEventHandler(this.ButtonPressedEventHandler);
    ((ButtonEdit) e.Control).ButtonClick -= this.handler;
    ((ButtonEdit) e.Control).ButtonClick += this.handler;
    componentResourceManager.ReleaseAllResources();
    this.UpdateControls();
  }

  /// <summary>Обработчик событий от кнопки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ButtonPressedEventHandler(object sender, ButtonPressedEventArgs e)
  {
    int num = (sender as ButtonEdit).Properties.Buttons.IndexOf(e.Button);
    IMDocObjectTypeSettings tag = (sender as ButtonEdit).Tag as IMDocObjectTypeSettings;
    switch (num)
    {
      case 0:
        tag.TemplateGuid = Guid.Empty;
        tag.TemplateCaption = string.Empty;
        tag.TemplateID = 0L;
        tag.TemplateTypeID = -1;
        (sender as ButtonEdit).Text = this.Message_2;
        break;
      case 1:
        object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Document.Client_126"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(DocIDCache.ObjType_ImDocTemplate), typeof (IDBTypedObjectID), SelectionOptions.Default | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect);
        if (objArray == null)
          break;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          long objectId = (objArray[0] as IDBTypedObjectID).ObjectID;
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
          tag.TemplateCaption = dbObject.Caption;
          tag.TemplateGuid = dbObject.ObjectGUID;
          tag.TemplateID = dbObject.ObjectID;
          tag.TemplateTypeID = dbObject.ObjectType;
        }
        (sender as ButtonEdit).Text = tag.TemplateCaption;
        break;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DataEditor));
    this.tableLayoutPanel = new TableLayoutPanel();
    this.pictureHint = new PictureBox();
    this.panelMain = new Panel();
    this.treeParentObjects = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnObjectType = new Column();
    this.columnTemplate = new Column();
    this.cellEditor1 = new CellEditor();
    this.buttonEdit1 = new ButtonEdit();
    this.panelRight = new Panel();
    this.btnDefaults = new Button();
    this.btnClear = new Button();
    this.btnDelete = new Button();
    this.btnAdd = new Button();
    this.labelHint = new Label();
    this.tableLayoutPanel.SuspendLayout();
    ((ISupportInitialize) this.pictureHint).BeginInit();
    this.panelMain.SuspendLayout();
    this.treeParentObjects.BeginInit();
    this.buttonEdit1.Properties.BeginInit();
    this.panelRight.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
    this.tableLayoutPanel.Controls.Add((Control) this.pictureHint, 0, 0);
    this.tableLayoutPanel.Controls.Add((Control) this.panelMain, 0, 1);
    this.tableLayoutPanel.Controls.Add((Control) this.labelHint, 1, 0);
    this.tableLayoutPanel.Name = "tableLayoutPanel";
    componentResourceManager.ApplyResources((object) this.pictureHint, "pictureHint");
    this.pictureHint.Name = "pictureHint";
    this.pictureHint.TabStop = false;
    this.tableLayoutPanel.SetColumnSpan((Control) this.panelMain, 2);
    this.panelMain.Controls.Add((Control) this.treeParentObjects);
    this.panelMain.Controls.Add((Control) this.panelRight);
    componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
    this.panelMain.Name = "panelMain";
    this.treeParentObjects.AllowDrop = true;
    this.treeParentObjects.AllowIndividualRowResize = false;
    this.treeParentObjects.AllowRowResize = false;
    this.treeParentObjects.AllowUserPinnedColumns = false;
    this.treeParentObjects.AutoFitColumns = true;
    this.treeParentObjects.BackgroundImageMode = ImageDrawMode.Tile;
    this.treeParentObjects.BorderStyle = BorderStyle.Fixed3D;
    this.treeParentObjects.Columns.Add(this.columnObjectType);
    this.treeParentObjects.Columns.Add(this.columnTemplate);
    this.treeParentObjects.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeParentObjects, "treeParentObjects");
    this.treeParentObjects.Editors.Add(this.cellEditor1);
    this.treeParentObjects.ImageList = (ImageList) null;
    this.treeParentObjects.LineStyle = LineStyle.Dot;
    this.treeParentObjects.MainColumn = this.columnObjectType;
    this.treeParentObjects.Name = "treeParentObjects";
    this.treeParentObjects.RowEvenStyle.BorderWidth = 1;
    this.treeParentObjects.RowOddStyle.BorderWidth = 1;
    this.treeParentObjects.RowSelectedStyle.BorderWidth = 1;
    this.treeParentObjects.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.treeParentObjects.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.treeParentObjects.RowStyle.BorderWidth = 1;
    this.treeParentObjects.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.treeParentObjects.ShowRootRow = false;
    this.treeParentObjects.SuppressErrorMessages = true;
    this.treeParentObjects.GetRowData += new GetRowDataHandler(this.treeParentObjects_GetRowData);
    this.treeParentObjects.GetChildren += new GetChildrenHandler(this.treeParentObjects_GetChildren);
    this.treeParentObjects.GetCellData += new GetCellDataHandler(this.treeParentObjects_GetCellData);
    this.treeParentObjects.SetCellValue += new SetCellValueHandler(this.treeParentObjects_SetCellValue);
    this.treeParentObjects.FocusRowChanged += new EventHandler(this.DoUpdateControls);
    this.treeParentObjects.SelectionChanged += new EventHandler(this.DoUpdateControls);
    this.treeParentObjects.ShowContextMenu += new MouseEventHandler(this.treeParentObjects_ShowContextMenu);
    this.columnObjectType.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    componentResourceManager.ApplyResources((object) this.columnObjectType, "columnObjectType");
    this.columnObjectType.CellStyle.BorderStyle = Border3DStyle.Flat;
    this.columnObjectType.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnObjectType.HeaderStyle.HorzAlignment");
    this.columnObjectType.Movable = false;
    this.columnObjectType.Name = "columnObjectType";
    this.columnObjectType.Sortable = false;
    this.columnObjectType.SortDirection = ListSortDirection.Ascending;
    this.columnTemplate.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    componentResourceManager.ApplyResources((object) this.columnTemplate, "columnTemplate");
    this.columnTemplate.CellEditor = this.cellEditor1;
    this.columnTemplate.CellStyle.BorderStyle = Border3DStyle.Flat;
    this.columnTemplate.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnTemplate.HeaderStyle.HorzAlignment");
    this.columnTemplate.Movable = false;
    this.columnTemplate.Name = "columnTemplate";
    this.columnTemplate.Sortable = false;
    this.columnTemplate.SortDirection = ListSortDirection.Ascending;
    this.cellEditor1.CellAlignment = ContentAlignment.MiddleLeft;
    this.cellEditor1.Control = (Control) this.buttonEdit1;
    this.cellEditor1.DisplayMode = CellEditorDisplayMode.OnEdit;
    this.cellEditor1.GetControlValue += new CellEditorGetValueHandler(this.cellEditor1_GetControlValue);
    this.cellEditor1.SetControlValue += new CellEditorSetValueHandler(this.cellEditor1_SetControlValue);
    this.cellEditor1.InitializeControl += new CellEditorInitializeHandler(this.cellEditor1_InitializeControl);
    componentResourceManager.ApplyResources((object) this.buttonEdit1, "buttonEdit1");
    this.buttonEdit1.Name = "buttonEdit1";
    this.buttonEdit1.Properties.Buttons.AddRange(new EditorButton[2]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("buttonEdit1.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Убрать ссылку на объект-шаблон", (object) 0),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("buttonEdit1.Properties.Buttons1"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выбрать объект-шаблон", (object) 1)
    });
    this.panelRight.Controls.Add((Control) this.btnDefaults);
    this.panelRight.Controls.Add((Control) this.btnClear);
    this.panelRight.Controls.Add((Control) this.btnDelete);
    this.panelRight.Controls.Add((Control) this.btnAdd);
    componentResourceManager.ApplyResources((object) this.panelRight, "panelRight");
    this.panelRight.MinimumSize = new Size(100, 105);
    this.panelRight.Name = "panelRight";
    componentResourceManager.ApplyResources((object) this.btnDefaults, "btnDefaults");
    this.btnDefaults.Name = "btnDefaults";
    this.btnDefaults.UseVisualStyleBackColor = true;
    this.btnDefaults.Click += new EventHandler(this.DoDefaults);
    componentResourceManager.ApplyResources((object) this.btnClear, "btnClear");
    this.btnClear.Name = "btnClear";
    this.btnClear.UseVisualStyleBackColor = true;
    this.btnClear.Click += new EventHandler(this.DoClear);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.DoRemove);
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.DoAdd);
    componentResourceManager.ApplyResources((object) this.labelHint, "labelHint");
    this.labelHint.Name = "labelHint";
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.tableLayoutPanel);
    this.MinimumSize = new Size(250, 250);
    this.Name = nameof (DataEditor);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Load += new EventHandler(this.DataEditor_Load);
    this.tableLayoutPanel.ResumeLayout(false);
    ((ISupportInitialize) this.pictureHint).EndInit();
    this.panelMain.ResumeLayout(false);
    this.treeParentObjects.EndInit();
    this.buttonEdit1.Properties.EndInit();
    this.panelRight.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
