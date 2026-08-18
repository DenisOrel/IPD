// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureView.ArchiveStructureView
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Bars;
using Intermech.DatabaseConfigurator;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Archives.StructureView;

/// <summary>закладка  Структура архива</summary>
[ViewDescriptionProvider(typeof (ArchiveStructureView.ArchiveStructureViewDescriptionProvider))]
public class ArchiveStructureView : ChildrenView
{
  /// <summary>Индекс значка закладки</summary>
  private static int _imageIndex = -1;
  /// <summary>идентификатор выделенного архива</summary>
  private static long _archiveID = -1;
  private MenuBarItem _menu;
  private MenuButtonItem _miCreateNew;
  private MenuButtonItem _miAdd;
  private MenuButtonItem _miDelete;
  private MenuButtonItem _miSetup;
  private MenuButtonItem _miReset;
  private MenuButtonItem _miResetAttrToDefaultValue;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ButtonItem btnAddAttributeType;
  private ButtonItem btnDeleteAttributeType;
  private ButtonItem btnCreate;

  /// <summary>Название закладки</summary>
  public override string Caption => ServiceHolder.rm.GetString("Archives_74");

  /// <summary>Индекс значка закладки</summary>
  public override int ImageIndex
  {
    get
    {
      if (ArchiveStructureView._imageIndex >= 0)
        return ArchiveStructureView._imageIndex;
      ArchiveStructureView._imageIndex = ChildrenView._namedImageList.ImageIndex("imgListView");
      return ArchiveStructureView._imageIndex;
    }
  }

  /// <summary>индекс закладки</summary>
  public override int OrderID => 23;

  /// <summary>
  /// Категория для названия потока с сохранёнными настройками
  /// </summary>
  protected override int StateStreamCategoryID => 3;

  /// <summary>закладка для отображения стрктуры архива</summary>
  public ArchiveStructureView()
  {
    this.InitializeComponent();
    this.SelectedItemsChanged += new EventHandler(this.ArchiveStructureView_SelectedItemsChanged);
    this.ShowCustomContextMenu += new EventHandler<ContextMenuEventArgs>(this.ArchiveStructureView_ShowCustomContextMenu);
  }

  /// <summary>
  /// Создает или получает извне элемент навигации, чье содержимое отображается в гриде.
  /// </summary>
  /// <returns></returns>
  protected override INode GetNode()
  {
    return (INode) new ArchiveStructureNode(ArchiveStructureView._archiveID);
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="provider">Контейнер сервисов</param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.InitMenu();
    ArchiveStructureView._archiveID = ((IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID))).ObjectID;
    base.Initialize(items, provider);
  }

  /// <summary>Activates the specified previous view.</summary>
  /// <param name="previousView">The previous view.</param>
  public override void Activate(IView previousView)
  {
    this._categoryTypeIconService.FindIcon += new FindIconEventHandler(this.IconService_FindIcon);
    base.Activate(previousView);
    this.SetBehaviorForCells();
    this.CheckButtonsEnable();
  }

  /// <summary>Deactivates the specified next view.</summary>
  /// <param name="nextView">The next view.</param>
  public override void Deactivate(IView nextView)
  {
    base.Deactivate(nextView);
    if (this._categoryTypeIconService == null)
      return;
    this._categoryTypeIconService.FindIcon -= new FindIconEventHandler(this.IconService_FindIcon);
  }

  /// <summary>
  /// Создает колонки в гриде по коллекции колонок навигатора.
  /// </summary>
  /// <param name="columns">Коллекция колонок навигатора</param>
  /// <param name="reloadData">
  /// Признак необходимости перечитать данные в гриде, если новая
  /// коллекция колонок не соответствует отображаемым данным</param>
  protected override void GridSetColumns(NodeColumnCollection columns, bool reloadData)
  {
    base.GridSetColumns(columns, reloadData);
    this.SetBehaviorForCells();
  }

  /// <summary>Перечитывает содержимое грида.</summary>
  public override void ReloadItems(int? count = null)
  {
    base.ReloadItems(count);
    this.SetBehaviorForCells();
  }

  /// <summary>
  /// Запрос на редактирование
  /// Редактируем ручками только строковые атрибуты.
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void GridRequestEdit(object sender, iGRequestEditEventArgs e)
  {
    IMSAttributeType attributeTypeInfo = this.GetAppointedToRowAttributeTypeInfo(e.RowIndex);
    if (attributeTypeInfo == null)
      base.GridRequestEdit(sender, e);
    else if (attributeTypeInfo.FieldType == FieldTypes.ftString && attributeTypeInfo.MultiValueMode == MultiValueModes.SingleValue)
      e.DoDefault = true;
    else
      e.DoDefault = false;
  }

  /// <summary>проверить доступность кнопок</summary>
  private void CheckButtonsEnable()
  {
    if (this.SelectedItems == null || this.SelectedItems.Count == 0)
      this.btnDeleteAttributeType.Enabled = this._miDelete.Enabled = this._miResetAttrToDefaultValue.Enabled = false;
    else
      this.btnDeleteAttributeType.Enabled = this._miDelete.Enabled = this._miResetAttrToDefaultValue.Enabled = true;
  }

  private void InvokeAttrProcessorAndSetNewValueByDefault(object sender, int rowIndex)
  {
    if (this.SelectedItems.Count != 1)
      return;
    int typeId = (this.SelectedItems.GetItemData(0, typeof (ArchiveStructureNodeID)) as ArchiveStructureNodeID).TypeID;
    if (!(sender is iGrid iGrid))
      return;
    AttributeProcessor attributeProcessor = new AttributeProcessor(ArchiveStructureView._archiveID, AttributableElements.Object);
    attributeProcessor.Load(ArchiveStructureView._archiveID, AttributableElements.Object, GetAttributeValuesModes.None, false);
    object initValue = attributeProcessor.EditValue(new AttributeValues(typeId, (object) null), editorControl: sender as System.Windows.Forms.Control, controlBounds: new Rectangle?(iGrid.CurCell.Bounds));
    if (initValue == null)
      return;
    string viewValue = attributeProcessor.GetViewValue(new AttributeValues(typeId, initValue));
    string newTextValue = initValue.ToString();
    if (this._grid.CurCell.Text.Equals(viewValue) || this.TryWriteNewValueToAttribute(rowIndex, newTextValue) != iGEditResult.Commit)
      return;
    this._grid.CurCell.Value = (object) viewValue;
  }

  /// <summary>
  /// для назначени иконки для типа атрибута  на закладке
  /// т.к. в childrenview пытается получить иконку для категории Атрибут
  /// а нам надо для типа данных строковый, числовой, и т.д.
  /// </summary>
  /// <param name="category"></param>
  /// <param name="type"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  private Icon IconService_FindIcon(int category, int type, object data)
  {
    if (category == 3)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(type);
      if (attributeType != null)
        return this._categoryTypeIconService.GetIcon(3, -1, (object) attributeType.FieldType);
    }
    return (Icon) null;
  }

  /// <summary>
  /// показываем своё контекстное меню (DisableIMContextMenu - true)
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void ArchiveStructureView_ShowCustomContextMenu(object sender, ContextMenuEventArgs e)
  {
    this._menu.Show((System.Windows.Forms.Control) this, e.Location);
  }

  /// <summary>изменилась выделенная строка</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ArchiveStructureView_SelectedItemsChanged(object sender, EventArgs e)
  {
    this.CheckButtonsEnable();
  }

  /// <summary>
  /// Handles the BeforeCommitEdit event of the _grid control.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:TenTec.Windows.iGridLib.iGBeforeCommitEditEventArgs" /> instance containing the event data.</param>
  private void _grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
  {
    if (!this._grid.CurCell.Text.Equals(e.NewText))
      e.Result = this.TryWriteNewValueToAttribute(e.RowIndex, e.NewText);
    else
      e.Result = iGEditResult.Cancel;
  }

  private void OnGridEllipsisBtnClick(object sender, iGEllipsisBtnClickEventArgs e)
  {
    if (!this.isEllipsisBtnClickForDefaultValueColumnCell(e.ColIndex))
      return;
    this.InvokeAttrProcessorAndSetNewValueByDefault(sender, e.RowIndex);
  }

  /// <summary>добавить тип атрибута в структуру архива</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnBtnAddAttributeType_Click(object sender, EventArgs e)
  {
    ArchiveStructureCommands.AddAttributeType(this.SelectedItems, (System.IServiceProvider) this._services, (object) null);
    this.ReloadView();
  }

  /// <summary>удалить тип атрибута из стурктуры архива</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnBtnDeleteAttributeType_Click(object sender, EventArgs e)
  {
    ArchiveStructureCommands.DeleteAttributeType(this.SelectedItems, (System.IServiceProvider) this._services, (object) null);
    this.ReloadView();
  }

  private void OnSetupClick(object sender, EventArgs e)
  {
    this.ChangeGridColumnsMenuButtonItem_Click(sender, e);
  }

  private void OnResetClick(object sender, EventArgs e) => this.ResetColumnsCommand(sender, e);

  /// <summary>Нажатие пункта меню Сброс значения по умолчанию.</summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void OnResetAttrToDefaultValueClick(object sender, EventArgs e)
  {
    this.RemoveSelectedAttrFromDefaultAttrValuesAttribute();
    this.ReloadView();
  }

  /// <summary>
  /// создать новый тип атрибута и добавить его
  /// в структуру архива
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnBtnCreate_Click(object sender, EventArgs e)
  {
    ArchiveStructureCommands.CreateAttributeType(this.SelectedItems, (System.IServiceProvider) this._services, (object) null);
    this.ReloadView();
  }

  /// <summary>
  /// Если пользователь согласится - пишет новое значение в атрибуте.
  /// </summary>
  /// <param name="rowIndex">Индекс строки.</param>
  /// <param name="newTextValue">Новое текстовое значение</param>
  /// <returns>iGEditResult.Commit - если юзер согласился менять значение, iGEditResult.Cancel - если отказался</returns>
  private iGEditResult TryWriteNewValueToAttribute(int rowIndex, string newTextValue)
  {
    IMSAttributeType attributeTypeInfo = this.GetAppointedToRowAttributeTypeInfo(rowIndex);
    if (this.NeedChangeDefaultValueForArchiveStructure(attributeTypeInfo) == DialogResult.Cancel)
      return iGEditResult.Cancel;
    this.WriteNewValueByDefaultToArchiveStructureAttr(attributeTypeInfo.AttributeGuid, newTextValue);
    return iGEditResult.Commit;
  }

  /// <summary>
  /// Удалить выбранные атрибуты из атрибутов Значения по умолчанию атрибутов структуры архивов.
  /// </summary>
  private void RemoveSelectedAttrFromDefaultAttrValuesAttribute()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this.SelectedItems.Count; ++index)
      {
        if (this.SelectedItems.GetItemData(index, typeof (ArchiveStructureNodeID)) is ArchiveStructureNodeID itemData)
        {
          int typeId = itemData.TypeID;
          if (sessionKeeper.Session.GetCustomService(typeof (IArchiveService)) is IArchiveService customService)
          {
            long archiveId = ArchiveStructureView._archiveID;
            List<int> attrTypeIDsForDeleting = new List<int>();
            attrTypeIDsForDeleting.Add(typeId);
            Guid sessionGuid = sessionKeeper.Session.SessionGUID;
            customService.DeleteAttributesFromDefaultAttrValuesAttribute(archiveId, attrTypeIDsForDeleting, sessionGuid);
          }
        }
      }
    }
  }

  /// <summary>
  /// Получить информацию об атрибуте, с которым соотносится указанная строка.
  /// </summary>
  /// <param name="rowIndex">Индекс строки.</param>
  /// <returns>Информация об атрибуте</returns>
  private IMSAttributeType GetAppointedToRowAttributeTypeInfo(int rowIndex)
  {
    IMSAttributeType attributeTypeInfo = (IMSAttributeType) null;
    if (this.Grid.Rows[rowIndex].Tag is ChildrenViewRowData tag && tag.NodeID is ArchiveStructureNodeID nodeId)
      attributeTypeInfo = MetaDataHelper.GetAttributeType(nodeId.TypeID);
    return attributeTypeInfo;
  }

  /// <summary>Устанавливаем поведение ячеек</summary>
  private void SetBehaviorForCells()
  {
    this._grid.BeginUpdate();
    foreach (iGCol col in (IEnumerable) this._grid.Cols)
    {
      if (col.Tag is NodeColumn tag && tag.Key.Contains("F_DEFAULT_VALUE"))
        this.SetEditBehaviorForDefaultColumnCells(col);
    }
    this._grid.EndUpdate();
  }

  /// <summary>
  /// Устанавливает режим редактирования ячеек колонки Значение по умолчанию.
  /// </summary>
  /// <param name="col">Колонка Значения по умолчанию.</param>
  private void SetEditBehaviorForDefaultColumnCells(iGCol col)
  {
    foreach (iGCell cell in (IEnumerable) col.Cells)
      this.SetEditBehaviorForDefaultColumnCell(cell);
  }

  /// <summary>
  /// <summary>
  /// Устанавливает режим редактирования ячейки колонки Значение по умолчанию.
  /// Курсор внутри ячейки для строкового атрибута и EllipsesBtn для атрибутов других типов, кроме многозначных.
  /// Доступ для редактирования непосредственно внутри ячейки регулируется в методе GridRequestEdit().
  /// </summary>
  /// </summary>
  /// <param name="cell">Ячейка колонки Значение по умолчанию.</param>
  private void SetEditBehaviorForDefaultColumnCell(iGCell cell)
  {
    IMSAttributeType attributeTypeInfo = this.GetAppointedToRowAttributeTypeInfo(cell.RowIndex);
    if (attributeTypeInfo == null)
      return;
    int num1 = attributeTypeInfo.MultiValueMode == MultiValueModes.MultiValues ? 1 : (attributeTypeInfo.MultiValueMode == MultiValueModes.MultiValuesFromList ? 1 : 0);
    bool flag1 = attributeTypeInfo.FieldType == FieldTypes.ftAutoInc || attributeTypeInfo.FieldType == FieldTypes.ftBlob || attributeTypeInfo.FieldType == FieldTypes.ftExternalLink || attributeTypeInfo.FieldType == FieldTypes.ftMemo || attributeTypeInfo.FieldType == FieldTypes.ftShortBlob || attributeTypeInfo.FieldType == FieldTypes.ftUnknown || attributeTypeInfo.FieldType == FieldTypes.ftPassword;
    bool flag2 = (attributeTypeInfo.Options & AttributeOptions.DisableManualEdit) != 0;
    int num2 = num1 != 0 ? 1 : (attributeTypeInfo.FieldType != FieldTypes.ftString ? 0 : (attributeTypeInfo.MultiValueMode == MultiValueModes.SingleValue ? 1 : 0));
    int num3 = flag1 ? 1 : 0;
    cell.TypeFlags = (num2 | num3 | (flag2 ? 1 : 0)) == 0 ? iGCellTypeFlags.HasEllipsisBtn : iGCellTypeFlags.None;
    cell.ReadOnly = iGBool.False;
    cell.Enabled = iGBool.True;
  }

  private void InitMenu()
  {
    this._menu = ServiceHolder.BarManager.MenuBar.AddMenuBar(ServiceHolder.rm.GetString("Archives_5"));
    this._menu.Visible = false;
    if (ServicesManager.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService)
    {
      this._miCreateNew = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_6"), new EventHandler(this.OnBtnCreate_Click));
      this._miCreateNew.BeginGroup = true;
      this.btnCreate.Visible = true;
    }
    this._miAdd = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_7"), new EventHandler(this.OnBtnAddAttributeType_Click));
    this._miDelete = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_8"), new EventHandler(this.OnBtnDeleteAttributeType_Click));
    this._miSetup = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_10"), new EventHandler(this.OnSetupClick));
    this._miReset = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_98"), new EventHandler(this.OnResetClick));
    this._miResetAttrToDefaultValue = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_201"), new EventHandler(this.OnResetAttrToDefaultValueClick));
    this._miResetAttrToDefaultValue.BeginGroup = true;
    this._miSetup.BeginGroup = true;
    this._miSetup.ImageIndex = ChildrenView._namedImageList.ImageIndex("imgViewSettings");
    this._menu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[5]
    {
      this._miAdd,
      this._miDelete,
      this._miResetAttrToDefaultValue,
      this._miSetup,
      this._miReset
    });
    if (this._miCreateNew == null)
      return;
    this._menu.Items.Insert(2, (ToolbarItemBase) this._miCreateNew);
  }

  /// <summary>
  /// Нажата EllipsisBtn в ячейке колонки со Значением атрибута по умолчанию или нет
  /// </summary>
  /// <param name="colIndex">Индекс колонки, в которой произошло нажатие.</param>
  /// <returns>True - если нажата в колонке Значение по умолчанию.</returns>
  private bool isEllipsisBtnClickForDefaultValueColumnCell(int colIndex)
  {
    return this._grid.Cols[colIndex].Tag is NodeColumn tag && tag.Key.Contains("F_DEFAULT_VALUE");
  }

  /// <summary>
  /// Задает вопрос, надо ли менять значение в атрибуте Значения по умолчанию структуры архива.
  /// </summary>
  /// <param name="attrTypeInfo">Информация об атрибуте.</param>
  /// <returns></returns>
  private DialogResult NeedChangeDefaultValueForArchiveStructure(IMSAttributeType attrTypeInfo)
  {
    return attrTypeInfo == null ? DialogResult.Cancel : MessageBox.Show(string.Format(ServiceHolder.rm.GetString("Archives_200"), (object) attrTypeInfo.Name), ServiceHolder.rm.GetString("Archives_14"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
  }

  /// <summary>Записать новое значение по умолчанию.</summary>
  /// <param name="newAttrValueByDefault">Новое значение атрибута по умолчанию.</param>
  /// <param name="attributeGuid">Гуид атрибута.</param>
  private void WriteNewValueByDefaultToArchiveStructureAttr(
    Guid attributeGuid,
    string newAttrValueByDefault)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject archiveObject = sessionKeeper.Session.GetObject(ArchiveStructureView._archiveID, false);
      if (archiveObject == null)
        return;
      this.AddOrReplaceValueByDefaultForAttribute(archiveObject, attributeGuid, newAttrValueByDefault);
    }
  }

  private void AddOrReplaceValueByDefaultForAttribute(
    IDBObject archiveObject,
    Guid attributeGuid,
    string attrValueByDefault)
  {
    string guidAndValue = attributeGuid.ToString() + ConstsHolder.Separator + attrValueByDefault;
    IDBAttribute attributeById = archiveObject.GetAttributeByID(ConstsHolder.ArchiveStructureAttrValuesByDefaultAttrID);
    if ((attributeById == null ? 1 : (attributeById.ValuesCount != 1 ? 0 : (attributeById.IsNull ? 1 : 0))) != 0)
    {
      ArchiveStructureView.AddValueToAttribute(archiveObject, (object) guidAndValue);
    }
    else
    {
      object[] objArray = this.FormNewDefaultValuesForAttr(attributeById, attributeGuid, guidAndValue);
      ArchiveStructureView.AddValueToAttribute(archiveObject, (object) objArray);
    }
  }

  /// <summary>
  /// Сформировать новые значения атрибута Значения по умолчанию атрибутов структуры архива.
  /// </summary>
  /// <param name="defaultValueStructureAttr">Атрибут Значения по умолчанию атрибутов структуры архива.</param>
  /// <param name="attributeGuid">Гуид редактируемого атрибута.</param>
  /// <param name="guidAndValue">Гуид+значение редактируемого атрибута.</param>
  /// <returns>Массив новых значений</returns>
  private object[] FormNewDefaultValuesForAttr(
    IDBAttribute defaultValueStructureAttr,
    Guid attributeGuid,
    string guidAndValue)
  {
    object[] values = defaultValueStructureAttr.Values;
    bool flag = false;
    for (int index = 0; index < values.Length; ++index)
    {
      if (values[index].ToString().Contains(attributeGuid.ToString()))
      {
        values[index] = (object) guidAndValue;
        flag = true;
        break;
      }
    }
    if (flag)
      return values;
    List<object> list = ((IEnumerable<object>) values).ToList<object>();
    list.Add((object) guidAndValue);
    return list.ToArray();
  }

  private static void AddValueToAttribute(IDBObject archiveObject, object value)
  {
    AttributeValues[] valuesList = new AttributeValues[1]
    {
      new AttributeValues(ConstsHolder.ArchiveStructureAttrValuesByDefaultAttrID, value)
    };
    archiveObject.SetAttributesValues(valuesList);
  }

  /// <summary>Обновить вьюшку (грид и кнопки).</summary>
  private void ReloadView()
  {
    this.ReloadItems(new int?());
    this.CheckButtonsEnable();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (this._categoryTypeIconService != null)
      this._categoryTypeIconService.FindIcon -= new FindIconEventHandler(this.IconService_FindIcon);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ArchiveStructureView));
    this.btnAddAttributeType = new ButtonItem();
    this.btnDeleteAttributeType = new ButtonItem();
    this.btnCreate = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.btnAddAttributeType,
      (ToolbarItemBase) this.btnDeleteAttributeType,
      (ToolbarItemBase) this.btnCreate
    });
    this._embeddedViewsDropDownMenuItem.Visible = false;
    this._toggleManualSortingButtonItem.Visible = false;
    this._grid.DefaultAutoGroupRow.Height = 21;
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
    this._grid.EllipsisBtnClick += new iGEllipsisBtnClickEventHandler(this.OnGridEllipsisBtnClick);
    this._grid.BeforeCommitEdit += new iGBeforeCommitEditEventHandler(this._grid_BeforeCommitEdit);
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this._manualSortingSetupButtonItem.Visible = false;
    this._editingModeButtonItem.Visible = false;
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this.buttonHeightSet.Visible = false;
    this.btnAddAttributeType.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnAddAttributeType, "btnAddAttributeType");
    this.btnAddAttributeType.Icon = (Icon) componentResourceManager.GetObject("btnAddAttributeType.Icon");
    this.btnAddAttributeType.Click += new EventHandler(this.OnBtnAddAttributeType_Click);
    componentResourceManager.ApplyResources((object) this.btnDeleteAttributeType, "btnDeleteAttributeType");
    this.btnDeleteAttributeType.Icon = (Icon) componentResourceManager.GetObject("btnDeleteAttributeType.Icon");
    this.btnDeleteAttributeType.ImageIndex = 2;
    this.btnDeleteAttributeType.Click += new EventHandler(this.OnBtnDeleteAttributeType_Click);
    this.btnCreate.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnCreate, "btnCreate");
    this.btnCreate.Icon = (Icon) componentResourceManager.GetObject("btnCreate.Icon");
    this.btnCreate.Visible = false;
    this.btnCreate.Click += new EventHandler(this.OnBtnCreate_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.DisableCheckedOutColumn = true;
    this.DisableIMContextMenu = true;
    this.Name = nameof (ArchiveStructureView);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._gridHeaderMenuBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._pictureBox, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._toolBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._grid, 0);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class ArchiveStructureViewDescriptionProvider : BaseViewDescriptionProvider
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
        Caption = ServiceHolder.rm.GetString("Archives_74"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgListView") : -1,
        OrderID = 23
      };
    }
  }
}
