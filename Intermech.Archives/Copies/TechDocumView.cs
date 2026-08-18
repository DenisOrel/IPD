// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.TechDocumView
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Закладка ОТД</summary>
public class TechDocumView : ObjectsViewBase
{
  /// <summary>сервис для фильтрации документов</summary>
  private readonly TechDocumentFilter techDocumFilter;
  /// <summary>служба уведомлений</summary>
  private readonly INotificationService notifyService;
  /// <summary>текущие настройки фильтрации</summary>
  private List<ConditionStructure> currentFilter = new List<ConditionStructure>();
  /// <summary>текущие настройки фильтрации по инвентатному номеру</summary>
  private ConditionStructure currentInventoryFilter = ConditionStructure.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBoxItem cbDocumsFiltration;
  private ButtonItem btnFilter;

  /// <summary>
  /// Никто не знает, хотела тут Валентина что-то делать или нет.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void NotifyEvent(object sender, NotificationEventArgs e)
  {
  }

  /// <summary>
  /// Обработка события изменения объектов
  /// Нужна для того, чтобы вкладка обновилась, когда документ зарегистрировался или снялся с регистрации
  /// </summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:Intermech.Interfaces.Client.NotificationEventArgs" /> instance containing the event data.</param>
  private void ObjectsChangedEventHandler(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsExtendedEventArgs extendedEventArgs) || extendedEventArgs.AttributeValuesArray == null)
      return;
    AttributeValues[] attributeValuesArray = extendedEventArgs.AttributeValuesArray;
    for (int index = 0; index < attributeValuesArray.Length; ++index)
    {
      if (attributeValuesArray[index] != null && attributeValuesArray[index].AttributeID == ConstsHolder.InventoryNumberID)
        this.ReloadItems();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public TechDocumView()
  {
    this.InitializeComponent();
    this.FillComboBox();
    this.techDocumFilter = new TechDocumentFilter();
    this._services.AddService(typeof (TechDocumentFilter), (object) this.techDocumFilter);
    this.cbDocumsFiltration.ComboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this.cbDocumsFiltration.ComboBox.SelectedIndex = 0;
    this.notifyService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this.notifyService != null)
      this.notifyService.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsChangedEventHandler));
    this.btnFilter.Checked = !this.currentInventoryFilter.Equals((object) ConditionStructure.Empty);
    this._useInheritedNavViews = false;
  }

  /// <summary>Заполянем комбобокс условиями фильтрации документов</summary>
  private void FillComboBox()
  {
    this.cbDocumsFiltration.ComboBox.BeginUpdate();
    try
    {
      this.cbDocumsFiltration.ComboBox.Items.Clear();
      List<ConditionStructure> conditionStructureList1 = new List<ConditionStructure>();
      MyElement myElement1 = new MyElement();
      myElement1.Caption = ServiceHolder.rm.GetString("Archives_135");
      conditionStructureList1.Add(new ConditionStructure(ConstsHolder.InventoryNumberID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, false));
      myElement1.Value = (object) conditionStructureList1;
      this.cbDocumsFiltration.ComboBox.Items.Add((object) myElement1);
      List<ConditionStructure> conditionStructureList2 = new List<ConditionStructure>();
      MyElement myElement2 = new MyElement();
      myElement2.Caption = ServiceHolder.rm.GetString("Archives_136");
      conditionStructureList2.Add(new ConditionStructure(ConstsHolder.InventoryNumberID, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.AND, 0, false));
      myElement2.Value = (object) conditionStructureList2;
      this.cbDocumsFiltration.ComboBox.Items.Add((object) myElement2);
      List<ConditionStructure> conditionStructureList3 = new List<ConditionStructure>();
      MyElement myElement3 = new MyElement();
      myElement3.Caption = ServiceHolder.rm.GetString("Archives_137");
      conditionStructureList3.Add(new ConditionStructure(ConstsHolder.InventoryNumberID, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.AND, 0, false));
      conditionStructureList3.Add(new ConditionStructure(-9, RelationalOperators.Equal, (object) MetaDataHelper.GetLCLevelID("cad00011-306c-11d8-b4e9-00304f19f545"), LogicalOperators.AND, 0, false));
      myElement3.Value = (object) conditionStructureList3;
      this.cbDocumsFiltration.ComboBox.Items.Add((object) myElement3);
      List<ConditionStructure> conditionStructureList4 = new List<ConditionStructure>();
      MyElement myElement4 = new MyElement();
      myElement4.Caption = ServiceHolder.rm.GetString("Archives_138");
      conditionStructureList4.Add(new ConditionStructure(ConstsHolder.InventoryNumberID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, false));
      conditionStructureList4.Add(new ConditionStructure(-9, RelationalOperators.NotEqual, (object) MetaDataHelper.GetLCLevelID("cad00012-306c-11d8-b4e9-00304f19f545"), LogicalOperators.AND, 0, false));
      conditionStructureList4.Add(new ConditionStructure(-9, RelationalOperators.NotEqual, (object) MetaDataHelper.GetLCLevelID("cad009de-306c-11d8-b4e9-00304f19f545"), LogicalOperators.AND, 0, false));
      myElement4.Value = (object) conditionStructureList4;
      this.cbDocumsFiltration.ComboBox.Items.Add((object) myElement4);
      List<ConditionStructure> conditionStructureList5 = new List<ConditionStructure>();
      this.cbDocumsFiltration.ComboBox.Items.Add((object) new MyElement()
      {
        Caption = ServiceHolder.rm.GetString("Archives_139"),
        Value = (object) conditionStructureList5
      });
    }
    finally
    {
      this.cbDocumsFiltration.ComboBox.EndUpdate();
    }
  }

  /// <summary>Изменился фильтр документов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    MyElement myElement = (MyElement) null;
    if (this.cbDocumsFiltration.ComboBox.SelectedIndex >= 0)
      myElement = this.cbDocumsFiltration.ComboBox.Items[this.cbDocumsFiltration.ComboBox.SelectedIndex] as MyElement;
    if (myElement == null)
      return;
    this.currentFilter = new List<ConditionStructure>();
    (myElement.Value as List<ConditionStructure>).ForEach((Action<ConditionStructure>) (cs => this.currentFilter.Add(cs)));
    if (this.cbDocumsFiltration.ComboBox.SelectedIndex == 3)
      this.NotSendCopies(this.currentFilter);
    if (this.cbDocumsFiltration.ComboBox.SelectedIndex == 4)
      this.NotReturnCopies(this.currentFilter);
    List<ConditionStructure> addCS = new List<ConditionStructure>();
    if (!this.currentInventoryFilter.Equals((object) ConditionStructure.Empty))
      addCS.Add(this.currentInventoryFilter);
    addCS.AddRange((IEnumerable<ConditionStructure>) this.currentFilter);
    this.techDocumFilter.SetConditions(addCS);
    this.ReloadItems();
  }

  /// <summary>
  /// формируем условие для поиска Невысланых актуальных копий документа
  /// </summary>
  /// <param name="conditions"></param>
  private void NotSendCopies(List<ConditionStructure> conditions)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(new Guid("cadd9365-306c-11d8-b4e9-00304f19f545"));
      ConditionStructure conditionStructure = new ConditionStructure(0, RelationalOperators.Equal, (object) new ConditionFormula(string.Format("EXISTS(SELECT {0}.F_OBJECT_ID FROM {0} WHERE [cadd9365-306c-11d8-b4e9-00304f19f545:cadd935a-306c-11d8-b4e9-00304f19f545] = SystemTableAlias.F_ID AND EXISTS(SELECT {1}.F_OBJECT_ID FROM {1} WHERE {1}.F_OBJECT_ID = {0}.F_OBJECT_ID AND F_ATTRIBUTE_ID = {2} AND F_INTEGER_VALUE IS NULL))", (object) objectType.ViewName, (object) objectType.AttributesTableName, (object) MetaDataHelper.GetAttributeTypeID(new Guid("cadd9352-306c-11d8-b4e9-00304f19f545"))), Array.Empty<DBDataParam>()), LogicalOperators.NONE, 0, true);
      conditions.Add(conditionStructure);
    }
  }

  /// <summary>
  /// формируем условие для поиска Не возвращеных устаревших копий документа
  /// </summary>
  /// <param name="conditions"></param>
  private void NotReturnCopies(List<ConditionStructure> conditions)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ConditionStructure conditionStructure1 = new ConditionStructure(ConstsHolder.InventoryNumberID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, false);
      ConditionStructure conditionStructure2 = new ConditionStructure(-9, RelationalOperators.Equal, (object) MetaDataHelper.GetLCLevelID("cad00011-306c-11d8-b4e9-00304f19f545"), LogicalOperators.OR, 1, false);
      ConditionStructure conditionStructure3 = new ConditionStructure(-16, RelationalOperators.Equal, (object) true, LogicalOperators.AND, -1, false);
      string viewName = sessionKeeper.Session.GetObjectType(new Guid("cadd9364-306c-11d8-b4e9-00304f19f545")).ViewName;
      int lcStepId = MetaDataHelper.GetLCStepID(new Guid("cadd936d-306c-11d8-b4e9-00304f19f545"));
      ConditionStructure conditionStructure4 = new ConditionStructure(0, RelationalOperators.Equal, (object) new ConditionFormula(string.Format("EXISTS(SELECT {0}.F_OBJECT_ID FROM {0} WHERE [cadd9364-306c-11d8-b4e9-00304f19f545:cadd935a-306c-11d8-b4e9-00304f19f545] = SystemTableAlias.F_ID AND [cadd9364-306c-11d8-b4e9-00304f19f545:cadd9359-306c-11d8-b4e9-00304f19f545] <> ABS(SystemTableAlias.F_OBJECT_ID) AND {0}.F_LC_STEP = :lcStepID1)", (object) viewName), new DBDataParam[1]
      {
        new DBDataParam("lcStepID1", (object) lcStepId)
      }), LogicalOperators.NONE, 0, true);
      conditions.AddRange((IEnumerable<ConditionStructure>) new ConditionStructure[4]
      {
        conditionStructure1,
        conditionStructure2,
        conditionStructure3,
        conditionStructure4
      });
    }
  }

  /// <summary>Название закладки</summary>
  public override string Caption => ServiceHolder.rm.GetString("Archives_140");

  /// <summary>Положение закладки</summary>
  public override int OrderID => 24;

  /// <summary>
  /// Категория для названия потока с сохранёнными настройками
  /// </summary>
  public override string StateStreamPrefix => "TechDocum_";

  /// <summary>
  /// Возвращает тип элементов навигации, которые зачитываются и отображаются в гриде.
  /// </summary>
  public override ContentType ViewContentType => ContentType.NonFolders;

  /// <summary>Подменяем узел</summary>
  /// <returns></returns>
  protected override INode GetNode()
  {
    INode node1 = base.GetNode();
    switch (node1)
    {
      case ArchiveNode _:
      case ArchivesNode _:
      case ObjectTypeNode _:
      case SelectionNode _:
        INode node2;
        switch (node1)
        {
          case ArchiveNode _:
            ArchiveNode archiveNode = node1 as ArchiveNode;
            node2 = (INode) new TechArchiveNode(archiveNode.ArcTypeID, archiveNode.ArcID);
            break;
          case ArchivesNode _:
            node2 = (INode) new TechArchivesNode();
            break;
          case SelectionNode _:
            SelectionNode selectionNode = node1 as SelectionNode;
            node2 = (INode) new TechSelectionNode(selectionNode.SelTypeID, selectionNode.SelID, selectionNode.Binding, selectionNode.ExternalConditions);
            break;
          default:
            node2 = (INode) new TechDocumentsTypeNode((node1 as ObjectTypeNode).ObjTypeID, AccessRights.Enabled);
            break;
        }
        IContextAware contextAware = node2 as IContextAware;
        IContextAware parentNode = this._parentNode as IContextAware;
        if (contextAware != null)
        {
          AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer((System.IServiceProvider) this._services);
          if (parentNode != null)
            serviceContainer.AdvancedProvider = parentNode.Services;
          contextAware.Services = (System.IServiceProvider) serviceContainer;
        }
        return node2;
      default:
        return node1;
    }
  }

  /// <summary>задать условия фильтрации</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnFilter_Click(object sender, EventArgs e)
  {
    using (FilterDialog filterDialog = new FilterDialog(this.currentInventoryFilter))
    {
      if (filterDialog.ShowDialog() == DialogResult.OK)
      {
        List<ConditionStructure> addCS = new List<ConditionStructure>();
        addCS.AddRange((IEnumerable<ConditionStructure>) this.currentFilter);
        this.currentInventoryFilter = filterDialog.Condition;
        if (!this.currentInventoryFilter.Equals((object) ConditionStructure.Empty))
          addCS.Add(this.currentInventoryFilter);
        this.techDocumFilter.SetConditions(addCS);
        this.ReloadItems();
      }
      this.btnFilter.Checked = !this.currentInventoryFilter.Equals((object) ConditionStructure.Empty);
    }
  }

  /// <summary>Освободить ресурсы закладки.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    this.notifyService.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsChangedEventHandler));
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechDocumView));
    this.cbDocumsFiltration = new ComboBoxItem();
    this.btnFilter = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnFilter,
      (ToolbarItemBase) this.cbDocumsFiltration
    });
    componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
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
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this.cbDocumsFiltration.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.cbDocumsFiltration, "cbDocumsFiltration");
    this.cbDocumsFiltration.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbDocumsFiltration.Importance = ToolBarItemImportance.High;
    this.cbDocumsFiltration.MinimumControlWidth = 100;
    this.cbDocumsFiltration.Padding.Bottom = 0;
    this.cbDocumsFiltration.Padding.Left = 1;
    this.cbDocumsFiltration.Padding.Right = 1;
    this.cbDocumsFiltration.Padding.Top = 0;
    this.cbDocumsFiltration.Stretch = true;
    this.btnFilter.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnFilter, "btnFilter");
    this.btnFilter.Icon = (Icon) componentResourceManager.GetObject("btnFilter.Icon");
    this.btnFilter.Click += new EventHandler(this.btnFilter_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (TechDocumView);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._gridHeaderMenuBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._pictureBox, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._toolBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._grid, 0);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
