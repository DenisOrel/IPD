
// Type: Intermech.PropertyEditors.VersionRulesEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.LookAndFeel;
using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.LifeCycle;
using Intermech.Search.UI;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// Редактор настроек фильтрации (для работы совместно с тулбаром "Фильтрация состава" главной формы)
/// </summary>
public sealed class VersionRulesEditorForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList imagesTree;
  private ToolTip toolTip;
  private Panel panelMain;
  private TreeListColumn treeCriterionsCommon;
  private TreeListColumn treeCriterionsAdd;
  private TreeListColumn treeCriterionsValue;
  private TreeListColumn treeCriterionsBool;
  private RepositoryItemComboBox comboFunction;
  private RepositoryItemComboBox comboCompareType;
  private RepositoryItemComboBox comboReadOnly;
  private RepositoryItemButtonEdit buttonAttribute;
  private RepositoryItemTextEdit editorString;
  private RepositoryItemComboBox comboNegation;
  private RepositoryItemDateEdit editorDate;
  private RepositoryItemSpinEdit editorInteger;
  private RepositoryItemCalcEdit editorFloat;
  private RepositoryItemRadioGroup editorBoolean;
  private RepositoryItemComboBox editorAttribute;
  private RepositoryItemComboBox comboSystemAttr;
  private RepositoryItemComboBox comboOperators;
  private ImageList _toolStripImageList;
  private Label labelAdvCriterion;
  private Label labelActualDate;
  private DateTimePicker _actualDateDateTimePicker;
  public CheckBox _expandAllCheckBox;
  private LabelItem lbHint;
  private Button _cancelButton;
  private Button _acceptButton;
  public CheckBox _editingRuleCheckBox;
  private TreeList CriterionsTree;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private TableLayoutPanel tableLayoutPanel2;
  private Panel panel1;
  private ToolStrip toolStrip1;
  private ToolStripButton _addCriterionToolStripButton;
  private ToolStripButton _removeCriterionToolStripButton;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton _addValueToolStripButton;
  private ToolStripButton _removeValueToolStripButton;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton _moveUpToolStripButton;
  private ToolStripButton _moveDownToolStripButton;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem _addCriterionToolStripMenuItem;
  private ToolStripMenuItem _removeCriterionToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripMenuItem _addValueToolStripMenuItem;
  private ToolStripMenuItem _removeValueToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator4;
  private ToolStripMenuItem _moveUpToolStripMenuItem;
  private ToolStripMenuItem _moveDownToolStripMenuItem;
  private FlowLayoutPanel flowLayoutPanel3;
  private ComboBox _additionalCriterionComboBox;
  private FlowLayoutPanel flowLayoutPanel2;
  private LinkLabel _additionalCriterionAttributeLinkLabel;
  private MessageControl _messageControl;
  private Panel panel2;
  private CheckBox _ignoreUserConcretizationCheckBox;
  private CheckBox _addToDropdownListCheckBox;
  public const string BaseVersionsAdditionalCriterion = "Базовые версии объектов";
  public const string MinValueAdditionalCriterion = "Имеет максимальное значение";
  public const string MaxValueAdditionalCriterion = "Имеет минимальное значение";
  /// <summary>
  /// Варианты заголовка формы (для режимов редактирования и просмотра)
  /// </summary>
  private string[] _captions = new string[3]
  {
    LocalizationHolder.rm.GetString("Client.Core_266"),
    LocalizationHolder.rm.GetString("Client.Core_804"),
    LocalizationHolder.rm.GetString("Client.Core_805")
  };
  /// <summary>
  /// Варианты заголовка (для режимов редактирования и просмотра)
  /// </summary>
  private string[] _headers = new string[3]
  {
    LocalizationHolder.rm.GetString("Client.Core_806"),
    LocalizationHolder.rm.GetString("Client.Core_807"),
    LocalizationHolder.rm.GetString("Client.Core_808")
  };
  /// <summary>Были ли изменения в правиле отбора</summary>
  private bool _changed;
  /// <summary>ID атрибута "Атрибуты ядра системы\Правила"</summary>
  private int _xmlAttrID;
  /// <summary>Название этого атрибута</summary>
  private string _xmlAttrName = "";
  /// <summary>Является ли атрибут системным типом (ftSystem)</summary>
  private bool _xmlIsSystemType;
  /// <summary>Его тип</summary>
  private FieldTypes _xmlAttrType;
  /// <summary>
  /// Выполняется ли работа внутри обработчиков событий, меняющих структуру дерева
  /// </summary>
  private bool _inEditor;
  /// <summary>Выполняется ли обработка событий</summary>
  private bool _inEventHandlers;
  /// <summary>Указатель на редактор для ячеек с функцией сравнения</summary>
  private RepositoryItemComboBox _funcEditor;
  /// <summary>Сугубо для внутреннего применения</summary>
  private CompareFunctionsHelper _fcFunc = new CompareFunctionsHelper();
  /// <summary>Сугубо для внутреннего применения</summary>
  private CompareTypesHelper _fcTypes = new CompareTypesHelper();
  /// <summary>
  /// Список типов значений для сравнения, за исключением типа "Значение пользователя"
  /// </summary>
  private CompareTypesHelper _fcTypesNoParams = new CompareTypesHelper(new int[1]
  {
    2
  });
  /// <summary>Сугубо для внутреннего применения</summary>
  private CompareOperatorsHelper _fcOperators = new CompareOperatorsHelper();
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService _fObjtypesIcons;
  /// <summary>Сервис для регистрации своих категорий</summary>
  private IGuidMapper _guidMapper;
  /// <summary>ID категории своих значков</summary>
  private static int _iconsCategory;
  /// <summary>Индексы своих значков</summary>
  private static int[] _icons;
  /// <summary>
  /// Запретить тип "Значение пользователя" в аргументах критериев
  /// (разрешить только непараметризованное правило)
  /// </summary>
  private bool _disableVariableValues;
  /// <summary>Ссылка на сервис ICurrentUserAndRole</summary>
  private ICurrentUserAndRole FUserAndRole;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VersionRulesEditorForm));
    RadioGroupItem radioGroupItem1 = new RadioGroupItem();
    RadioGroupItem radioGroupItem2 = new RadioGroupItem();
    this.imagesTree = new ImageList(this.components);
    this.toolTip = new ToolTip(this.components);
    this.labelActualDate = new Label();
    this._actualDateDateTimePicker = new DateTimePicker();
    this._expandAllCheckBox = new CheckBox();
    this._editingRuleCheckBox = new CheckBox();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this.labelAdvCriterion = new Label();
    this.panelMain = new Panel();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this._addCriterionToolStripMenuItem = new ToolStripMenuItem();
    this._removeCriterionToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this._addValueToolStripMenuItem = new ToolStripMenuItem();
    this._removeValueToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator4 = new ToolStripSeparator();
    this._moveUpToolStripMenuItem = new ToolStripMenuItem();
    this._moveDownToolStripMenuItem = new ToolStripMenuItem();
    this.CriterionsTree = new TreeList();
    this.treeCriterionsCommon = new TreeListColumn();
    this.treeCriterionsAdd = new TreeListColumn();
    this.treeCriterionsValue = new TreeListColumn();
    this.treeCriterionsBool = new TreeListColumn();
    this.comboFunction = new RepositoryItemComboBox();
    this.comboCompareType = new RepositoryItemComboBox();
    this.comboReadOnly = new RepositoryItemComboBox();
    this.buttonAttribute = new RepositoryItemButtonEdit();
    this.editorString = new RepositoryItemTextEdit();
    this.comboNegation = new RepositoryItemComboBox();
    this.editorDate = new RepositoryItemDateEdit();
    this.editorInteger = new RepositoryItemSpinEdit();
    this.editorFloat = new RepositoryItemCalcEdit();
    this.editorBoolean = new RepositoryItemRadioGroup();
    this.editorAttribute = new RepositoryItemComboBox();
    this.comboSystemAttr = new RepositoryItemComboBox();
    this.comboOperators = new RepositoryItemComboBox();
    this._toolStripImageList = new ImageList(this.components);
    this.lbHint = new LabelItem();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.panel2 = new Panel();
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.panel1 = new Panel();
    this.toolStrip1 = new ToolStrip();
    this._addCriterionToolStripButton = new ToolStripButton();
    this._removeCriterionToolStripButton = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._addValueToolStripButton = new ToolStripButton();
    this._removeValueToolStripButton = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this._moveUpToolStripButton = new ToolStripButton();
    this._moveDownToolStripButton = new ToolStripButton();
    this.flowLayoutPanel3 = new FlowLayoutPanel();
    this._additionalCriterionComboBox = new ComboBox();
    this._additionalCriterionAttributeLinkLabel = new LinkLabel();
    this.flowLayoutPanel2 = new FlowLayoutPanel();
    this._ignoreUserConcretizationCheckBox = new CheckBox();
    this._messageControl = new MessageControl();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._addToDropdownListCheckBox = new CheckBox();
    this.panelMain.SuspendLayout();
    this.contextMenuStrip1.SuspendLayout();
    this.CriterionsTree.BeginInit();
    this.comboFunction.BeginInit();
    this.comboCompareType.BeginInit();
    this.comboReadOnly.BeginInit();
    this.buttonAttribute.BeginInit();
    this.editorString.BeginInit();
    this.comboNegation.BeginInit();
    this.editorDate.BeginInit();
    this.editorInteger.BeginInit();
    this.editorFloat.BeginInit();
    this.editorBoolean.BeginInit();
    this.editorAttribute.BeginInit();
    this.comboSystemAttr.BeginInit();
    this.comboOperators.BeginInit();
    this.tableLayoutPanel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.tableLayoutPanel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this.flowLayoutPanel3.SuspendLayout();
    this.flowLayoutPanel2.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.imagesTree.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesTree.ImageStream");
    this.imagesTree.TransparentColor = Color.Transparent;
    this.imagesTree.Images.SetKeyName(0, "");
    this.imagesTree.Images.SetKeyName(1, "");
    this.imagesTree.Images.SetKeyName(2, "");
    this.imagesTree.Images.SetKeyName(3, "");
    this.imagesTree.Images.SetKeyName(4, "");
    this.imagesTree.Images.SetKeyName(5, "ftSystem_UserID.ico");
    this.imagesTree.Images.SetKeyName(6, "ftSystem_LevelID.ico");
    this.toolTip.ShowAlways = true;
    componentResourceManager.ApplyResources((object) this.labelActualDate, "labelActualDate");
    this.labelActualDate.Name = "labelActualDate";
    this.toolTip.SetToolTip((Control) this.labelActualDate, componentResourceManager.GetString("labelActualDate.ToolTip"));
    this._actualDateDateTimePicker.Checked = false;
    componentResourceManager.ApplyResources((object) this._actualDateDateTimePicker, "_actualDateDateTimePicker");
    this._actualDateDateTimePicker.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
    this._actualDateDateTimePicker.Name = "_actualDateDateTimePicker";
    this._actualDateDateTimePicker.ShowCheckBox = true;
    this.toolTip.SetToolTip((Control) this._actualDateDateTimePicker, componentResourceManager.GetString("_actualDateDateTimePicker.ToolTip"));
    this._actualDateDateTimePicker.ValueChanged += new EventHandler(this.ActualDateDateTimePicker_ValueChanged);
    this._actualDateDateTimePicker.EnabledChanged += new EventHandler(this.ActualDateDateTimePicker_EnabledChanged);
    componentResourceManager.ApplyResources((object) this._expandAllCheckBox, "_expandAllCheckBox");
    this._expandAllCheckBox.Checked = true;
    this._expandAllCheckBox.CheckState = CheckState.Checked;
    this._expandAllCheckBox.Cursor = Cursors.Hand;
    this._expandAllCheckBox.Name = "_expandAllCheckBox";
    this.toolTip.SetToolTip((Control) this._expandAllCheckBox, componentResourceManager.GetString("_expandAllCheckBox.ToolTip"));
    this._expandAllCheckBox.CheckedChanged += new EventHandler(this.ExpandAllCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._editingRuleCheckBox, "_editingRuleCheckBox");
    this._editingRuleCheckBox.Cursor = Cursors.Default;
    this._editingRuleCheckBox.Name = "_editingRuleCheckBox";
    this.toolTip.SetToolTip((Control) this._editingRuleCheckBox, componentResourceManager.GetString("_editingRuleCheckBox.ToolTip"));
    this._editingRuleCheckBox.CheckedChanged += new EventHandler(this.EditingRuleCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.Cursor = Cursors.Hand;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    componentResourceManager.ApplyResources((object) this._acceptButton, "_acceptButton");
    this._acceptButton.Cursor = Cursors.Hand;
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Click += new EventHandler(this.AcceptButton_Click);
    componentResourceManager.ApplyResources((object) this.labelAdvCriterion, "labelAdvCriterion");
    this.labelAdvCriterion.Name = "labelAdvCriterion";
    componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
    this.panelMain.ContextMenuStrip = this.contextMenuStrip1;
    this.panelMain.Controls.Add((Control) this.CriterionsTree);
    this.panelMain.Name = "panelMain";
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this._addCriterionToolStripMenuItem,
      (ToolStripItem) this._removeCriterionToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this._addValueToolStripMenuItem,
      (ToolStripItem) this._removeValueToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator4,
      (ToolStripItem) this._moveUpToolStripMenuItem,
      (ToolStripItem) this._moveDownToolStripMenuItem
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
    this._addCriterionToolStripMenuItem.Name = "_addCriterionToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this._addCriterionToolStripMenuItem, "_addCriterionToolStripMenuItem");
    this._addCriterionToolStripMenuItem.Click += new EventHandler(this.AddCriterionToolStripMenuItem_Click);
    this._removeCriterionToolStripMenuItem.Name = "_removeCriterionToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this._removeCriterionToolStripMenuItem, "_removeCriterionToolStripMenuItem");
    this._removeCriterionToolStripMenuItem.Click += new EventHandler(this.RemoveCriterionToolStripMenuItem_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator3, "toolStripSeparator3");
    this._addValueToolStripMenuItem.Name = "_addValueToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this._addValueToolStripMenuItem, "_addValueToolStripMenuItem");
    this._addValueToolStripMenuItem.Click += new EventHandler(this.AddValueToolStripMenuItem_Click);
    this._removeValueToolStripMenuItem.Name = "_removeValueToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this._removeValueToolStripMenuItem, "_removeValueToolStripMenuItem");
    this._removeValueToolStripMenuItem.Click += new EventHandler(this.RemoveValueToolStripMenuItem_Click);
    this.toolStripSeparator4.Name = "toolStripSeparator4";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator4, "toolStripSeparator4");
    this._moveUpToolStripMenuItem.Image = (Image) Intermech.Client.Core.Properties.Resources.arrow_up_blue;
    this._moveUpToolStripMenuItem.Name = "_moveUpToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this._moveUpToolStripMenuItem, "_moveUpToolStripMenuItem");
    this._moveUpToolStripMenuItem.Click += new EventHandler(this.MoveUpToolStripMenuItem_Click);
    this._moveDownToolStripMenuItem.Image = (Image) Intermech.Client.Core.Properties.Resources.arrow_down_blue;
    this._moveDownToolStripMenuItem.Name = "_moveDownToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this._moveDownToolStripMenuItem, "_moveDownToolStripMenuItem");
    this._moveDownToolStripMenuItem.Click += new EventHandler(this.MoveDownToolStripMenuItem_Click);
    this.CriterionsTree.Columns.AddRange(new TreeListColumn[4]
    {
      this.treeCriterionsCommon,
      this.treeCriterionsAdd,
      this.treeCriterionsValue,
      this.treeCriterionsBool
    });
    componentResourceManager.ApplyResources((object) this.CriterionsTree, "CriterionsTree");
    this.CriterionsTree.Name = "CriterionsTree";
    this.CriterionsTree.RepositoryItems.AddRange(new RepositoryItem[13]
    {
      (RepositoryItem) this.comboFunction,
      (RepositoryItem) this.comboCompareType,
      (RepositoryItem) this.comboReadOnly,
      (RepositoryItem) this.buttonAttribute,
      (RepositoryItem) this.editorString,
      (RepositoryItem) this.comboNegation,
      (RepositoryItem) this.editorDate,
      (RepositoryItem) this.editorInteger,
      (RepositoryItem) this.editorFloat,
      (RepositoryItem) this.editorBoolean,
      (RepositoryItem) this.editorAttribute,
      (RepositoryItem) this.comboSystemAttr,
      (RepositoryItem) this.comboOperators
    });
    this.CriterionsTree.SelectImageList = this.imagesTree;
    this.CriterionsTree.Styles.AddReplace("HideSelectionRow", (object) new ViewStyle("HideSelectionRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this.CriterionsTree.Styles.AddReplace("RedFontStyle", (object) new ViewStyle("RedFontStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, Color.Red));
    this.CriterionsTree.Styles.AddReplace("BoolNOPStyle", (object) new ViewStyle("BoolNOPStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.Window));
    this.CriterionsTree.Styles.AddReplace("OddRow", (object) new ViewStyle("OddRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.None, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGreen, SystemColors.WindowText));
    this.CriterionsTree.Styles.AddReplace("CommonSelectedCell", (object) new ViewStyle("CommonSelectedCell", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.InactiveCaptionText, SystemColors.WindowText));
    this.CriterionsTree.Styles.AddReplace("MainArgStyle", (object) new ViewStyle("MainArgStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.CriterionsTree.Styles.AddReplace("BoolStyle", (object) new ViewStyle("BoolStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, Color.Blue));
    this.CriterionsTree.Styles.AddReplace("NegFontStyle", (object) new ViewStyle("NegFontStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Far, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.CriterionsTree.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.Azure, SystemColors.WindowText));
    this.CriterionsTree.Styles.AddReplace("DefaultFontStyle", (object) new ViewStyle("DefaultFontStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.CriterionsTree.Styles.AddReplace("PinkFontStyle", (object) new ViewStyle("PinkFontStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, Color.ForestGreen));
    this.CriterionsTree.Styles.AddReplace("DefFuncStyle", (object) new ViewStyle("DefFuncStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.CriterionsTree.Styles.AddReplace("AggFuncStyle", (object) new ViewStyle("AggFuncStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.FromArgb(216, (int) byte.MaxValue, 216), Color.Black));
    this.CriterionsTree.Styles.AddReplace("ValueFontStyle", (object) new ViewStyle("ValueFontStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.CriterionsTree.Styles.AddReplace("UrlFontBkStyle", (object) new ViewStyle("UrlFontBkStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseFont | StyleOptions.UseForeColor, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LemonChiffon, Color.Blue));
    this.CriterionsTree.Styles.AddReplace("UrlFontStyle", (object) new ViewStyle("UrlFontStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, Color.Blue));
    this.CriterionsTree.GetCustomNodeCellEdit += new GetCustomNodeCellEditEventHandler(this.CriterionsTree_GetCustomNodeCellEdit);
    this.CriterionsTree.GetCustomNodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.CriterionsTree_GetCustomNodeCellStyle);
    this.CriterionsTree.GetNodeDisplayValue += new GetNodeDisplayValueEventHandler(this.CriterionsTree_GetNodeDisplayValue);
    this.CriterionsTree.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.CriterionsTree_FocusedNodeChanged);
    this.CriterionsTree.SelectionChanged += new EventHandler(this.CriterionsTree_SelectionChanged);
    this.CriterionsTree.CellValueChanged += new CellValueChangedEventHandler(this.CriterionsTree_CellValueChanged);
    this.CriterionsTree.ShowingEditor += new CancelEventHandler(this.CriterionsTree_ShowingEditor);
    componentResourceManager.ApplyResources((object) this.treeCriterionsCommon, "treeCriterionsCommon");
    this.treeCriterionsCommon.Name = "treeCriterionsCommon";
    componentResourceManager.ApplyResources((object) this.treeCriterionsAdd, "treeCriterionsAdd");
    this.treeCriterionsAdd.Name = "treeCriterionsAdd";
    componentResourceManager.ApplyResources((object) this.treeCriterionsValue, "treeCriterionsValue");
    this.treeCriterionsValue.Name = "treeCriterionsValue";
    componentResourceManager.ApplyResources((object) this.treeCriterionsBool, "treeCriterionsBool");
    this.treeCriterionsBool.Name = "treeCriterionsBool";
    this.comboFunction.AutoHeight = false;
    this.comboFunction.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", 14, true, true, false, HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выберите функцию сравнения")
    });
    this.comboFunction.DropDownRows = 15;
    this.comboFunction.HotTrackDropDownItems = false;
    this.comboFunction.Name = "comboFunction";
    this.comboFunction.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.comboCompareType.AutoHeight = false;
    this.comboCompareType.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", 14, true, true, false, HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выберите вид значения для сравнения")
    });
    this.comboCompareType.DropDownRows = 15;
    this.comboCompareType.Name = "comboCompareType";
    this.comboCompareType.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.comboReadOnly.AutoHeight = false;
    this.comboReadOnly.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", 14, true, false, false, HorzAlignment.Center, (Image) null)
    });
    this.comboReadOnly.Name = "comboReadOnly";
    this.comboReadOnly.ReadOnly = true;
    this.comboReadOnly.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.buttonAttribute.AutoHeight = false;
    this.buttonAttribute.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 12, true, true, false, HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Нажмите кнопку чтобы открыть окно выбора атрибутов")
    });
    this.buttonAttribute.Name = "buttonAttribute";
    this.buttonAttribute.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.buttonAttribute.ButtonClick += new ButtonPressedEventHandler(this.buttonAttribute_ButtonClick);
    this.editorString.AutoHeight = false;
    this.editorString.Name = "editorString";
    this.comboNegation.AutoHeight = false;
    this.comboNegation.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", 14, true, true, false, HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выберите, применять ли логическое отрицание к функции сравнения")
    });
    this.comboNegation.DropDownRows = 2;
    this.comboNegation.Name = "comboNegation";
    this.comboNegation.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.editorDate.AutoHeight = false;
    this.editorDate.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", 14, true, true, false, HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Укажите дату")
    });
    this.editorDate.CharacterCasing = CharacterCasing.Upper;
    this.editorDate.DisplayFormat.FormatString = "dd.MM.yyyy";
    this.editorDate.DisplayFormat.FormatType = FormatType.Custom;
    this.editorDate.EditFormat.FormatString = "dd.MM.yyyy";
    this.editorDate.EditFormat.FormatType = FormatType.Custom;
    this.editorDate.LookAndFeel.Style = LookAndFeelStyle.Office2003;
    this.editorDate.Name = "editorDate";
    this.editorDate.ShowClear = false;
    this.editorDate.ShowToday = false;
    this.editorDate.ShowWeekNumbers = true;
    this.editorInteger.AutoHeight = false;
    this.editorInteger.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 14, true, true, false, HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Укажите целое число")
    });
    this.editorInteger.IsFloatValue = false;
    this.editorInteger.Name = "editorInteger";
    this.editorInteger.UseCtrlIncrement = true;
    this.editorFloat.AutoHeight = false;
    this.editorFloat.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", 14, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("editorFloat.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Подсчитать новое значение на калькуляторе")
    });
    this.editorFloat.DisplayFormat.FormatString = "g";
    this.editorFloat.DisplayFormat.FormatType = FormatType.Numeric;
    this.editorFloat.EditFormat.FormatString = "g";
    this.editorFloat.EditFormat.FormatType = FormatType.Numeric;
    this.editorFloat.Name = "editorFloat";
    this.editorFloat.Precision = 10;
    componentResourceManager.ApplyResources((object) radioGroupItem1, "radioGroupItem1");
    radioGroupItem1.Value = (object) false;
    componentResourceManager.ApplyResources((object) radioGroupItem2, "radioGroupItem2");
    radioGroupItem2.Value = (object) true;
    this.editorBoolean.Items.AddRange(new RadioGroupItem[2]
    {
      radioGroupItem1,
      radioGroupItem2
    });
    this.editorBoolean.Name = "editorBoolean";
    this.editorAttribute.AutoHeight = false;
    this.editorAttribute.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", 14, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.editorAttribute.DropDownRows = 11;
    this.editorAttribute.Name = "editorAttribute";
    this.comboSystemAttr.AutoHeight = false;
    this.comboSystemAttr.Buttons.AddRange(new EditorButton[4]
    {
      new EditorButton(ButtonPredefines.Combo, "", 14, true, true, false, HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выберите одно из допустимых значений", (object) "0"),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("comboSystemAttr.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выберите пользователя", (object) "1"),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("comboSystemAttr.Buttons1"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Укажите число, выраженное в единицах измерения", (object) 2),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("comboSystemAttr.Buttons2"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выберите шаг жизненного цикла", (object) 3)
    });
    this.comboSystemAttr.DropDownRows = 15;
    this.comboSystemAttr.Name = "comboSystemAttr";
    this.comboSystemAttr.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.comboSystemAttr.ButtonClick += new ButtonPressedEventHandler(this.comboSystemAttr_ButtonClick);
    this.comboOperators.AutoHeight = false;
    this.comboOperators.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", 14, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.comboOperators.DropDownRows = 2;
    this.comboOperators.Name = "comboOperators";
    this.comboOperators.TextEditStyle = TextEditStyles.DisableTextEditor;
    this._toolStripImageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_toolStripImageList.ImageStream");
    this._toolStripImageList.TransparentColor = Color.Transparent;
    this._toolStripImageList.Images.SetKeyName(0, "AddCriterion");
    this._toolStripImageList.Images.SetKeyName(1, "RemoveCriterion");
    this._toolStripImageList.Images.SetKeyName(2, "AddValue");
    this._toolStripImageList.Images.SetKeyName(3, "RemoveValue");
    this._toolStripImageList.Images.SetKeyName(4, "Image00005.png");
    this._toolStripImageList.Images.SetKeyName(5, "Image00006.png");
    this._toolStripImageList.Images.SetKeyName(6, "star.ico");
    componentResourceManager.ApplyResources((object) this.lbHint, "lbHint");
    this.lbHint.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
    this.lbHint.Importance = ToolBarItemImportance.High;
    this.lbHint.MinimumSize = 200;
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.panel2, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.panel2.Controls.Add((Control) this.tableLayoutPanel2);
    this.panel2.Controls.Add((Control) this._messageControl);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel2, "tableLayoutPanel2");
    this.tableLayoutPanel2.Controls.Add((Control) this._expandAllCheckBox, 0, 1);
    this.tableLayoutPanel2.Controls.Add((Control) this.panel1, 0, 0);
    this.tableLayoutPanel2.Controls.Add((Control) this.flowLayoutPanel3, 0, 2);
    this.tableLayoutPanel2.Controls.Add((Control) this.flowLayoutPanel2, 0, 3);
    this.tableLayoutPanel2.Controls.Add((Control) this._editingRuleCheckBox, 0, 4);
    this.tableLayoutPanel2.Controls.Add((Control) this._ignoreUserConcretizationCheckBox, 0, 5);
    this.tableLayoutPanel2.Controls.Add((Control) this._addToDropdownListCheckBox, 0, 6);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.panel1.Controls.Add((Control) this.toolStrip1);
    this.panel1.Controls.Add((Control) this.panelMain);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.toolStrip1.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this._addCriterionToolStripButton,
      (ToolStripItem) this._removeCriterionToolStripButton,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._addValueToolStripButton,
      (ToolStripItem) this._removeValueToolStripButton,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this._moveUpToolStripButton,
      (ToolStripItem) this._moveDownToolStripButton
    });
    componentResourceManager.ApplyResources((object) this.toolStrip1, "toolStrip1");
    this.toolStrip1.Name = "toolStrip1";
    this._addCriterionToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._addCriterionToolStripButton, "_addCriterionToolStripButton");
    this._addCriterionToolStripButton.Name = "_addCriterionToolStripButton";
    this._addCriterionToolStripButton.Click += new EventHandler(this.AddCriterionToolStripButton_Click);
    this._removeCriterionToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._removeCriterionToolStripButton, "_removeCriterionToolStripButton");
    this._removeCriterionToolStripButton.Name = "_removeCriterionToolStripButton";
    this._removeCriterionToolStripButton.Click += new EventHandler(this.RemoveCriterionToolStripButton_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this._addValueToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._addValueToolStripButton, "_addValueToolStripButton");
    this._addValueToolStripButton.Name = "_addValueToolStripButton";
    this._addValueToolStripButton.Click += new EventHandler(this.AddValueToolStripButton_Click);
    this._removeValueToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._removeValueToolStripButton, "_removeValueToolStripButton");
    this._removeValueToolStripButton.Name = "_removeValueToolStripButton";
    this._removeValueToolStripButton.Click += new EventHandler(this.RemoveValueToolStripButton_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator2, "toolStripSeparator2");
    this._moveUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveUpToolStripButton.Image = (Image) Intermech.Client.Core.Properties.Resources.arrow_up_blue;
    componentResourceManager.ApplyResources((object) this._moveUpToolStripButton, "_moveUpToolStripButton");
    this._moveUpToolStripButton.Name = "_moveUpToolStripButton";
    this._moveUpToolStripButton.Click += new EventHandler(this.MoveUpToolStripButton_Click);
    this._moveDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveDownToolStripButton.Image = (Image) Intermech.Client.Core.Properties.Resources.arrow_down_blue;
    componentResourceManager.ApplyResources((object) this._moveDownToolStripButton, "_moveDownToolStripButton");
    this._moveDownToolStripButton.Name = "_moveDownToolStripButton";
    this._moveDownToolStripButton.Click += new EventHandler(this.MoveDownToolStripButton_Click);
    componentResourceManager.ApplyResources((object) this.flowLayoutPanel3, "flowLayoutPanel3");
    this.flowLayoutPanel3.Controls.Add((Control) this.labelAdvCriterion);
    this.flowLayoutPanel3.Controls.Add((Control) this._additionalCriterionComboBox);
    this.flowLayoutPanel3.Controls.Add((Control) this._additionalCriterionAttributeLinkLabel);
    this.flowLayoutPanel3.Name = "flowLayoutPanel3";
    this._additionalCriterionComboBox.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._additionalCriterionComboBox, "_additionalCriterionComboBox");
    this._additionalCriterionComboBox.Name = "_additionalCriterionComboBox";
    this._additionalCriterionComboBox.SelectedIndexChanged += new EventHandler(this.AdditionalCriterionComboBox_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._additionalCriterionAttributeLinkLabel, "_additionalCriterionAttributeLinkLabel");
    this._additionalCriterionAttributeLinkLabel.Name = "_additionalCriterionAttributeLinkLabel";
    this._additionalCriterionAttributeLinkLabel.TabStop = true;
    this._additionalCriterionAttributeLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.AdditionalCriterionAttributeLinkLabel_LinkClicked);
    componentResourceManager.ApplyResources((object) this.flowLayoutPanel2, "flowLayoutPanel2");
    this.flowLayoutPanel2.Controls.Add((Control) this.labelActualDate);
    this.flowLayoutPanel2.Controls.Add((Control) this._actualDateDateTimePicker);
    this.flowLayoutPanel2.Name = "flowLayoutPanel2";
    componentResourceManager.ApplyResources((object) this._ignoreUserConcretizationCheckBox, "_ignoreUserConcretizationCheckBox");
    this._ignoreUserConcretizationCheckBox.Name = "_ignoreUserConcretizationCheckBox";
    this._ignoreUserConcretizationCheckBox.UseVisualStyleBackColor = true;
    this._ignoreUserConcretizationCheckBox.CheckedChanged += new EventHandler(this.IgnoreUserConcretizationCheckBox_CheckedChanged);
    this._messageControl.BackColor = Color.LightBlue;
    this._messageControl.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this._messageControl, "_messageControl");
    this._messageControl.Name = "_messageControl";
    componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
    this.flowLayoutPanel1.Controls.Add((Control) this._cancelButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._acceptButton);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    componentResourceManager.ApplyResources((object) this._addToDropdownListCheckBox, "_addToDropdownListCheckBox");
    this._addToDropdownListCheckBox.Name = "_addToDropdownListCheckBox";
    this._addToDropdownListCheckBox.UseVisualStyleBackColor = true;
    this._addToDropdownListCheckBox.CheckedChanged += new EventHandler(this.AddToDropdownListCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (VersionRulesEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Closed += new EventHandler(this.VersionRulesEditorForm_Closed);
    this.Load += new EventHandler(this.VersionRulesEditorForm_Load);
    this.panelMain.ResumeLayout(false);
    this.contextMenuStrip1.ResumeLayout(false);
    this.CriterionsTree.EndInit();
    this.comboFunction.EndInit();
    this.comboCompareType.EndInit();
    this.comboReadOnly.EndInit();
    this.buttonAttribute.EndInit();
    this.editorString.EndInit();
    this.comboNegation.EndInit();
    this.editorDate.EndInit();
    this.editorInteger.EndInit();
    this.editorFloat.EndInit();
    this.editorBoolean.EndInit();
    this.editorAttribute.EndInit();
    this.comboSystemAttr.EndInit();
    this.comboOperators.EndInit();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.tableLayoutPanel2.ResumeLayout(false);
    this.tableLayoutPanel2.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.flowLayoutPanel3.ResumeLayout(false);
    this.flowLayoutPanel3.PerformLayout();
    this.flowLayoutPanel2.ResumeLayout(false);
    this.flowLayoutPanel2.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Создать экземпляр формы-редактора правила отбора версий
  /// </summary>
  public VersionRulesEditorForm()
  {
    this.InitializeComponent();
    this._addCriterionToolStripButton.Image = this._addCriterionToolStripMenuItem.Image = this._toolStripImageList.Images["AddCriterion"];
    this._removeCriterionToolStripButton.Image = this._removeCriterionToolStripMenuItem.Image = this._toolStripImageList.Images["RemoveCriterion"];
    this._addValueToolStripButton.Image = this._addValueToolStripMenuItem.Image = this._toolStripImageList.Images["AddValue"];
    this._removeValueToolStripButton.Image = this._removeValueToolStripMenuItem.Image = this._toolStripImageList.Images["RemoveValue"];
    this.RuleObjectName = string.Empty;
    if (this.DesignMode)
      return;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1023 /*0x03FF*/);
    this.editorString.MaxLength = Intermech.Consts.MaxStringSize;
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 80 /*0x50*/, workingArea.Height / 100 * 70);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this._fObjtypesIcons = Statics.IconSrv;
    this.FUserAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this._guidMapper = ServicesManager.GetService(typeof (IGuidMapper)) as IGuidMapper;
    if (VersionRulesEditorForm._iconsCategory == 0 && this._guidMapper != null)
    {
      VersionRulesEditorForm._iconsCategory = this._guidMapper.Register(Guid.NewGuid());
      VersionRulesEditorForm._icons = new int[this.imagesTree.Images.Count];
      for (int index = 0; index < this.imagesTree.Images.Count; ++index)
      {
        if (this._fObjtypesIcons != null)
        {
          using (Icon icon = Intermech.Interfaces.ImageHelper.BitmapToIcon(this.imagesTree.Images[index] as Bitmap))
            VersionRulesEditorForm._icons[index] = this._fObjtypesIcons.AddIcon(icon, VersionRulesEditorForm._iconsCategory, index);
        }
        else
          VersionRulesEditorForm._icons[index] = -1;
      }
    }
    this.CriterionsTree.SelectImageList = this._fObjtypesIcons != null ? this._fObjtypesIcons.ImageList : (ImageList) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      MyAttributeHelper.GetAttrInfo("cad001d2-306c-11d8-b4e9-00304f19f545", ref this._xmlAttrName, ref this._xmlAttrID, ref this._xmlAttrType, ref this._xmlIsSystemType);
      this.RuleClass = new VersionsRule();
      this.RuleClass.Clear();
      this.RuleClass.Valid(session);
    }
    this.BuildRuleNodes();
    this.RuntimeFillControls();
    this.UpdateControls();
  }

  /// <summary>
  /// Ссылка на интерфейс IFiltrationClass окна-владельца, для того, чтобы получать настройки фильтрации состава
  /// </summary>
  public IFiltrationClass FiltrationClass { get; set; }

  /// <summary>
  /// Режим работы формы
  /// 0 -	полноценный редактор правила отбора версий объектов (admin режим)
  /// 1 - просмотр правила отбора версий объектов (read-only режим)
  /// 2 - заполнение недостающих значений для сравнения в критериях (user режим)
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int EditorMode { get; set; }

  /// <summary>
  /// Где размещена наша форма
  /// 0 - самостоятельная форма (по дефолту)
  /// 1 - на форме-создателе новых объектов
  /// 2 - на вьюшке "Навигатора"
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ParentMode { get; set; }

  /// <summary>
  /// Для особых случаев надо запретить и спрятать кнопки "Применить" и "Отмена"
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool HideApplyCancel { get; set; }

  /// <summary>ID выделенного объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long RuleObjectID { get; set; }

  /// <summary>Название выделенного объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string RuleObjectName { get; set; }

  /// <summary>
  /// Экземпляр класса, инкапсулирующий в себя правило отбора версий указанного объекта
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public VersionsRule RuleClass { get; set; }

  /// <summary>
  /// Были ли изменения в редактируемом правиле подбора версий
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsChanged
  {
    [DebuggerStepThrough] get => this._changed;
    set
    {
      if (this._changed == value)
        return;
      this._changed = value;
      this.UpdateControls();
    }
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public void UpdateControls()
  {
    bool inEventHandlers = this._inEventHandlers;
    try
    {
      this._inEventHandlers = true;
      this.treeCriterionsBool.VisibleIndex = this.EditorMode >= 2 ? -1 : 3;
      TreeListNode treeListNode1 = (TreeListNode) null;
      TreeListNode treeListNode2 = this.CriterionsTree.Selection[0];
      if (treeListNode2 != null)
        treeListNode1 = treeListNode2.RootNode;
      VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
      if (treeListNode1 != null)
        versionsRuleCriterion = (VersionsRuleCriterion) treeListNode1.Tag;
      ComparableValue comparableValue = (ComparableValue) null;
      if (treeListNode2 != null && treeListNode2.Level == 1)
        comparableValue = (ComparableValue) treeListNode2.Tag;
      string str1 = "";
      if (this._additionalCriterionAttributeLinkLabel.Tag is VersionsRuleCriterion tag)
        str1 = tag.CompareFunction;
      string str2 = "";
      if (versionsRuleCriterion != null)
        str2 = versionsRuleCriterion.CompareFunction;
      if (comparableValue != null)
      {
        string valueType = comparableValue.ValueType;
      }
      int editorMode = this.EditorMode;
      this._acceptButton.Visible = this.EditorMode < 2 && !this.HideApplyCancel;
      this._cancelButton.Visible = !this.HideApplyCancel;
      if (this.EditorMode == 0 && this._acceptButton.Text != VersionRulesEditorForm.RulesEditorConsts.ApplyText1)
        this._acceptButton.Text = VersionRulesEditorForm.RulesEditorConsts.ApplyText1;
      if (this.EditorMode > 0 && this._acceptButton.Text != VersionRulesEditorForm.RulesEditorConsts.ApplyText2)
        this._acceptButton.Text = VersionRulesEditorForm.RulesEditorConsts.ApplyText2;
      if (this.EditorMode != 2 && this._cancelButton.Text != VersionRulesEditorForm.RulesEditorConsts.CancelText1)
        this._cancelButton.Text = VersionRulesEditorForm.RulesEditorConsts.CancelText1;
      if (this.EditorMode == 2 && this._cancelButton.Text != VersionRulesEditorForm.RulesEditorConsts.CancelText2)
        this._cancelButton.Text = VersionRulesEditorForm.RulesEditorConsts.CancelText2;
      bool flag = this.RuleClass != null && this.RuleClass.CurrentRuleType == VersionsRuleType.vrtStandardRule;
      this.RuleClass.HasVariableValues();
      this._messageControl.Visible = !flag;
      this._addCriterionToolStripButton.Enabled = this._addCriterionToolStripMenuItem.Enabled = flag && !this._inEditor && this.EditorMode == 0;
      this._removeCriterionToolStripButton.Enabled = this._removeCriterionToolStripMenuItem.Enabled = this._addCriterionToolStripButton.Enabled && treeListNode1 != null && versionsRuleCriterion != null;
      this._addValueToolStripButton.Enabled = this._addValueToolStripMenuItem.Enabled = this._addCriterionToolStripButton.Enabled && versionsRuleCriterion != null && str2 != "" && !this._fcFunc.IsAggregate(str2) && versionsRuleCriterion.CanAddValue();
      this._removeValueToolStripButton.Enabled = this._removeValueToolStripMenuItem.Enabled = this._addCriterionToolStripButton.Enabled && versionsRuleCriterion != null && treeListNode2 != null && treeListNode2.Level == 1 && str2 != "" && versionsRuleCriterion.CanDeleteValue() && comparableValue != null;
      this._moveUpToolStripButton.Enabled = this._moveUpToolStripMenuItem.Enabled = flag && this.EditorMode == 0 && treeListNode2 != null && treeListNode2.Level == 0 && this.RuleClass != null && this.RuleClass.Criterions.IndexOf(versionsRuleCriterion) > 0;
      this._moveDownToolStripButton.Enabled = this._moveDownToolStripMenuItem.Enabled = flag && this.EditorMode == 0 && treeListNode2 != null && treeListNode2.Level == 0 && this.RuleClass != null && this.RuleClass.Criterions.IndexOf(versionsRuleCriterion) < this.RuleClass.Criterions.Count - 2;
      this.CriterionsTree.Enabled = flag;
      this._additionalCriterionAttributeLinkLabel.Enabled = flag && this.EditorMode == 0 && str1 != "BASEVERSION";
      this._additionalCriterionAttributeLinkLabel.Visible = str1 != "BASEVERSION";
      this._additionalCriterionComboBox.Enabled = this.labelAdvCriterion.Enabled = flag && this.EditorMode == 0;
      this._editingRuleCheckBox.Enabled = this.EditorMode != 2 && this.FUserAndRole.IsAdmin && this.RuleClass != null && !SystemGUIDs.IsSystemGUID(this.RuleClass.RuleObjectGuid);
      this._acceptButton.Enabled = this.IsChanged || this.EditorMode == 1;
      this._cancelButton.Enabled = this.IsChanged || this.EditorMode > 0;
      this._actualDateDateTimePicker.Value = this.RuleClass == null || !(this.RuleClass.ActualDate > DateTime.MinValue) ? DateTime.Now.Date : this.RuleClass.ActualDate;
      this._actualDateDateTimePicker.Enabled = flag && !this._editingRuleCheckBox.Checked;
      this._actualDateDateTimePicker.Checked = this.RuleClass != null && this.RuleClass.ActualDate > DateTime.MinValue && this._actualDateDateTimePicker.Enabled;
      this._actualDateDateTimePicker.Visible = true;
      this.labelActualDate.Enabled = this._actualDateDateTimePicker.Enabled;
      this.labelActualDate.Visible = true;
      this.labelActualDate.ForeColor = !this._actualDateDateTimePicker.Checked ? SystemColors.ControlText : Color.Red;
      this._ignoreUserConcretizationCheckBox.Enabled = this.RuleClass != null & flag && this.EditorMode == 0 && !this._editingRuleCheckBox.Checked;
      this._addToDropdownListCheckBox.Enabled = this.RuleClass != null;
      this._addToDropdownListCheckBox.Checked = this.RuleClass != null && this.RuleClass.AddToDropDownList;
    }
    finally
    {
      this._inEventHandlers = inEventHandlers;
    }
  }

  /// <summary>
  /// Загрузить правило отбора версий в форму, включить определённый режим редактирования
  /// </summary>
  /// <param name="ARuleClass">Исходный класс с правилом отбора версий</param>
  /// <param name="AEditorMode">Режим редактирования (0 - полноценный редактор правила отбора версий объектов (admin режим), 1 - заполнение недостающих значений для сравнения в критериях (user режим), 2 - просмотр правила отбора версий объектов (read-only режим))</param>
  public void LoadObjectData(VersionsRule ARuleClass, int AEditorMode)
  {
    this.RuleObjectID = 0L;
    this.EditorMode = AEditorMode;
    if (this.EditorMode < 0)
      this.EditorMode = 1;
    if (this.EditorMode >= this._headers.Length)
      this.EditorMode = 1;
    if (ARuleClass != null)
      this.RuleClass = ARuleClass.Clone() as VersionsRule;
    else
      this.ClearQuick();
    this.BuildRuleNodes();
    this.RuntimeFillControls();
    this.Text = this._captions[this.EditorMode];
    if (this.RuleObjectName.Length > 0)
      this.Text = this._captions[this.EditorMode] + this.RuleObjectName;
    this.UpdateControls();
  }

  /// <summary>Загрузить данные объекта с ID = RuleObjectID в форму</summary>
  /// <param name="AEditorMode">Режим редактирования (0 - полноценный редактор правила отбора версий объектов (admin режим), 1 - заполнение недостающих значений для сравнения в критериях (user режим), 2 - просмотр правила отбора версий объектов (read-only режим))</param>
  public void LoadObjectData(int AEditorMode)
  {
    this.EditorMode = AEditorMode;
    if (this.EditorMode < 0)
      this.EditorMode = 1;
    if (this.EditorMode >= this._headers.Length)
      this.EditorMode = 1;
    this.ClearQuick();
    this.IsChanged = false;
    if (this.RuleObjectID == 0L)
    {
      this.RuntimeFillControls();
      this.Clear();
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IVersionRulesCacheService rulesCacheService;
        try
        {
          rulesCacheService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        }
        catch
        {
          rulesCacheService = (IVersionRulesCacheService) null;
        }
        if (rulesCacheService != null)
        {
          this.RuleClass = rulesCacheService[(object) sessionKeeper.Session.SessionGUID, this.RuleObjectID];
          if (this.RuleClass == null)
          {
            rulesCacheService.LoadRule((object) sessionKeeper.Session.SessionGUID, this.RuleObjectID);
            this.RuleClass = rulesCacheService[(object) sessionKeeper.Session.SessionGUID, this.RuleObjectID];
          }
        }
      }
      try
      {
        this._inEventHandlers = true;
        this.BuildRuleNodes();
        this.RuntimeFillControls();
      }
      finally
      {
        this._inEventHandlers = false;
      }
      this.UpdateControls();
    }
  }

  /// <summary>Сохранить данные в объект с ID = RuleObjectID</summary>
  public void SaveObjectData()
  {
    if (this.RuleObjectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.RuleClass.Valid(sessionKeeper.Session);
      this.IsChanged = !this.RuleClass.SaveToObject(sessionKeeper.Session, this.RuleObjectID);
      if (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService)
        customService.LoadRule((object) sessionKeeper.Session.SessionGUID, this.RuleObjectID, this.RuleClass.ActualDate);
    }
    this.BuildRuleNodes();
    this.UpdateControls();
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.RuleClass.RuleObjectID, this.RuleClass.RuleObjectType));
  }

  /// <summary>Загрузить данные объекта с ID = RuleObjectID в форму</summary>
  /// <param name="AEditorMode">Режим редактирования (0 - полноценный редактор правила отбора версий объектов (admin режим), 1 - заполнение недостающих значений для сравнения в критериях (user режим), 2 - просмотр правила отбора версий объектов (read-only режим))</param>
  /// <param name="TemplateObjectID">ID объекта-прототипа</param>
  public void LoadTemplateData(int AEditorMode, long TemplateObjectID)
  {
    this.EditorMode = AEditorMode;
    if (this.EditorMode < 0)
      this.EditorMode = 1;
    if (this.EditorMode >= this._headers.Length)
      this.EditorMode = 1;
    this.ClearQuick();
    this.IsChanged = false;
    if (TemplateObjectID == 0L)
    {
      this.RuntimeFillControls();
      this.Clear();
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IVersionRulesCacheService rulesCacheService;
        try
        {
          rulesCacheService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        }
        catch
        {
          rulesCacheService = (IVersionRulesCacheService) null;
        }
        if (rulesCacheService != null)
          this.RuleClass = rulesCacheService[(object) sessionKeeper.Session.SessionGUID, TemplateObjectID];
      }
      this.RuleClass.CurrentRuleType = VersionsRuleType.vrtStandardRule;
      this.BuildRuleNodes();
      this.RuntimeFillControls();
      this.UpdateControls();
    }
  }

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
    this.UpdateControls();
  }

  /// <summary>
  /// Построить в дереве InTree список узлов для правила отбора версий
  /// </summary>
  /// <param name="InTree">Дерево, в который надо добавить узлы</param>
  /// <returns></returns>
  public void BuildRuleNodes()
  {
    TreeList criterionsTree = this.CriterionsTree;
    if (criterionsTree == null)
      return;
    criterionsTree.BeginUpdate();
    bool inEventHandlers = this._inEventHandlers;
    try
    {
      this._inEventHandlers = true;
      criterionsTree.ClearNodes();
      if (this.RuleClass == null || this.RuleClass.Criterions.Count <= 0)
        return;
      for (int index = 0; index < this.RuleClass.Criterions.Count; ++index)
      {
        VersionsRuleCriterion criterion = this.RuleClass.Criterions[index];
        if (criterion != null)
        {
          if (this._fcFunc.IsAggregate(criterion.CompareFunction))
          {
            this._additionalCriterionAttributeLinkLabel.Text = criterion.MainAttribute.Attribute.AttrName;
            this._additionalCriterionAttributeLinkLabel.Tag = (object) criterion;
            this._additionalCriterionComboBox.SelectedIndex = this._additionalCriterionComboBox.Items.IndexOf(this._fcFunc.Names[(object) criterion.CompareFunction]);
          }
          else
          {
            TreeListNode Node = criterionsTree.AppendNode((object) new object[4]
            {
              (object) criterion.MainAttribute.Attribute.AttrName,
              (object) fncnConsts.GetNegationValue(criterion.Negation),
              this._fcFunc.Names[(object) criterion.CompareFunction],
              this._fcOperators.Names[(object) criterion.BoolFunction]
            }, (TreeListNode) null);
            Node.Tag = (object) criterion;
            if (Node != null)
              this.UpdateNode(criterionsTree, Node, false);
          }
        }
      }
    }
    finally
    {
      this._ignoreUserConcretizationCheckBox.Checked = this.RuleClass != null && this.RuleClass.IgnoreSoftConcretization;
      this._editingRuleCheckBox.Checked = this.RuleClass != null && this.RuleClass.EditingRule;
      this._inEventHandlers = inEventHandlers;
      criterionsTree.EndUpdate();
      criterionsTree.Refresh();
    }
  }

  private void VersionRulesEditorForm_Closed(object sender, EventArgs e)
  {
    if (this.ParentMode != 0)
      return;
    FormStorage.SaveLayout((Control) this);
  }

  private void VersionRulesEditorForm_Load(object sender, EventArgs e)
  {
    if (this.ParentMode != 0)
      return;
    FormStorage.LoadLayout((Control) this);
  }

  private void AddCriterionToolStripButton_Click(object sender, EventArgs e) => this.AddCriterion();

  private void RemoveCriterionToolStripButton_Click(object sender, EventArgs e)
  {
    this.RemoveCriterion();
  }

  private void AddValueToolStripButton_Click(object sender, EventArgs e) => this.AddValue();

  private void RemoveValueToolStripButton_Click(object sender, EventArgs e) => this.RemoveValue();

  private void MoveUpToolStripButton_Click(object sender, EventArgs e) => this.MoveUp();

  private void MoveDownToolStripButton_Click(object sender, EventArgs e) => this.MoveDown();

  private void CriterionsTree_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
  {
    if (e.Node.Level != 0 || e.Column != this.treeCriterionsBool)
      return;
    VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.RootNode.Tag;
    if (tag == null)
      return;
    int num = this.RuleClass.Criterions.IndexOf(tag);
    VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
    if (num > 0)
    {
      VersionsRuleCriterion criterion = this.RuleClass.Criterions[num - 1];
    }
    if (num >= 0 && num < this.RuleClass.Criterions.Count - 1)
      versionsRuleCriterion = this.RuleClass.Criterions[num + 1];
    if (num == 0 && versionsRuleCriterion != null && versionsRuleCriterion.CFunc.IsAggregate(versionsRuleCriterion.CompareFunction))
      e.Value = (object) string.Empty;
    else if (num == this.RuleClass.Criterions.Count - 1 || tag.CFunc.IsAggregate(tag.CompareFunction))
    {
      e.Value = (object) string.Empty;
    }
    else
    {
      if (versionsRuleCriterion == null || !versionsRuleCriterion.CFunc.IsAggregate(versionsRuleCriterion.CompareFunction))
        return;
      e.Value = (object) string.Empty;
    }
  }

  private void CriterionsTree_GetCustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
  {
    if (this.RuleClass == null || this.RuleClass.CurrentRuleType != VersionsRuleType.vrtStandardRule || this.RuleClass.Criterions.Count <= 0 || e.Node == null || e.Column == null)
      return;
    e.RepositoryItem = (RepositoryItem) this.comboReadOnly;
    if (this.EditorMode == 2)
      return;
    if (e.Node.Level == 0 && e.Column == this.treeCriterionsCommon)
    {
      if (this.EditorMode != 0)
        return;
      e.RepositoryItem = (RepositoryItem) this.buttonAttribute;
    }
    else if (e.Node.Level == 1 && e.Column == this.treeCriterionsCommon)
    {
      if (this.EditorMode != 0)
        return;
      e.RepositoryItem = (RepositoryItem) this.comboCompareType;
    }
    else if (e.Node.Level == 0 && e.Column == this.treeCriterionsAdd)
    {
      if (this.EditorMode != 0)
        return;
      VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.RootNode.Tag;
      if (tag == null || this._fcFunc.IsAggregate(tag.CompareFunction) || tag.MainAttribute.AttrType == FieldTypes.ftBoolean)
        return;
      e.RepositoryItem = (RepositoryItem) this.comboNegation;
    }
    else if (e.Node.Level == 0 && e.Column == this.treeCriterionsValue)
    {
      if (this.EditorMode != 0)
        return;
      e.RepositoryItem = (RepositoryItem) this._funcEditor;
      if (this._funcEditor != null)
        return;
      e.RepositoryItem = (RepositoryItem) this.comboFunction;
    }
    else if (e.Node.Level == 1 && e.Column == this.treeCriterionsValue)
    {
      VersionsRuleCriterion tag1 = (VersionsRuleCriterion) e.Node.RootNode.Tag;
      if (tag1 == null)
        return;
      ComparableValue tag2 = (ComparableValue) e.Node.Tag;
      if (tag2 == null)
        return;
      string valueType = tag2.ValueType;
      e.RepositoryItem = (RepositoryItem) this.comboReadOnly;
      if (this.EditorMode == 0 && valueType == "ATTRIBUTE")
        e.RepositoryItem = (RepositoryItem) this.buttonAttribute;
      if (!(valueType == "CONST") && (this.EditorMode == 2 || !(valueType == "VARIABLE")))
        return;
      if (tag1.MainAttribute.Attribute.IsAttrList)
      {
        e.RepositoryItem = (RepositoryItem) this.comboSystemAttr;
        if (tag1.MainAttribute.AttrType != FieldTypes.ftBoolean)
          return;
        e.RepositoryItem = (RepositoryItem) this.editorBoolean;
      }
      else if (tag1.MainAttribute.Attribute.AttrID == -4)
        e.RepositoryItem = (RepositoryItem) this.comboSystemAttr;
      else if (tag1.MainAttribute.AttrType == FieldTypes.ftBoolean)
        e.RepositoryItem = (RepositoryItem) this.editorBoolean;
      else if (tag1.MainAttribute.AttrType == FieldTypes.ftInteger || tag1.MainAttribute.AttrType == FieldTypes.ftAutoInc)
        e.RepositoryItem = (RepositoryItem) this.editorFloat;
      else if (tag1.MainAttribute.AttrType == FieldTypes.ftDouble)
        e.RepositoryItem = (RepositoryItem) this.editorFloat;
      else if (tag1.MainAttribute.AttrType == FieldTypes.ftDateTime)
        e.RepositoryItem = (RepositoryItem) this.editorDate;
      else if (tag1.MainAttribute.AttrType == FieldTypes.ftMeasured)
        e.RepositoryItem = (RepositoryItem) this.comboSystemAttr;
      else
        e.RepositoryItem = (RepositoryItem) this.editorString;
    }
    else
    {
      if (e.Node.Level != 0 || e.Column != this.treeCriterionsBool || this.EditorMode != 0)
        return;
      VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.RootNode.Tag;
      if (tag == null)
        return;
      int num = this.RuleClass.Criterions.IndexOf(tag);
      VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
      if (num > 0)
      {
        VersionsRuleCriterion criterion = this.RuleClass.Criterions[num - 1];
      }
      if (num >= 0 && num < this.RuleClass.Criterions.Count - 1)
        versionsRuleCriterion = this.RuleClass.Criterions[num + 1];
      if (num == this.RuleClass.Criterions.Count - 1 || tag.CFunc.IsAggregate(tag.CompareFunction) || versionsRuleCriterion != null && versionsRuleCriterion.CFunc.IsAggregate(versionsRuleCriterion.CompareFunction))
        return;
      e.RepositoryItem = (RepositoryItem) this.comboOperators;
    }
  }

  private void CriterionsTree_GetCustomNodeCellStyle(
    object sender,
    GetCustomNodeCellStyleEventArgs e)
  {
    if (this._inEventHandlers || this.RuleClass == null || e.Node == null || e.Style == null || e.Column == null || e.Node.TreeList.Selection.IndexOf(e.Node) >= 0)
      return;
    if (e.Node.Level == 0 && e.Column == this.treeCriterionsCommon)
    {
      VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.RootNode.Tag;
      if (tag == null)
        return;
      int num = this.RuleClass.Criterions.IndexOf(tag);
      VersionsRuleCriterion versionsRuleCriterion1 = (VersionsRuleCriterion) null;
      VersionsRuleCriterion versionsRuleCriterion2 = (VersionsRuleCriterion) null;
      if (num > 0)
        versionsRuleCriterion1 = this.RuleClass.Criterions[num - 1];
      if (num >= 0 && num < this.RuleClass.Criterions.Count - 1)
        versionsRuleCriterion2 = this.RuleClass.Criterions[num + 1];
      if (num == 0 && versionsRuleCriterion2 != null && versionsRuleCriterion2.CFunc.IsAggregate(versionsRuleCriterion2.CompareFunction))
      {
        e.Style = e.Node.TreeList.Styles["UrlFontStyle"];
        return;
      }
      if (tag != null && !tag.CFunc.IsAggregate(tag.CompareFunction) && tag.BoolFunction == "AND" && versionsRuleCriterion2 != null && !versionsRuleCriterion2.CFunc.IsAggregate(versionsRuleCriterion2.CompareFunction))
      {
        e.Style = e.Node.TreeList.Styles["UrlFontBkStyle"];
        return;
      }
      if (versionsRuleCriterion1 != null && versionsRuleCriterion1.BoolFunction == "AND" && !versionsRuleCriterion1.CFunc.IsAggregate(versionsRuleCriterion1.CompareFunction) && !tag.CFunc.IsAggregate(tag.CompareFunction))
      {
        e.Style = e.Node.TreeList.Styles["UrlFontBkStyle"];
        return;
      }
      e.Style = e.Node.TreeList.Styles["UrlFontStyle"];
    }
    if (e.Node.Level == 0 && e.Column == this.treeCriterionsBool)
    {
      VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.RootNode.Tag;
      if (tag == null)
        return;
      int num = this.RuleClass.Criterions.IndexOf(tag);
      VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
      if (num > 0)
      {
        VersionsRuleCriterion criterion = this.RuleClass.Criterions[num - 1];
      }
      if (num >= 0 && num < this.RuleClass.Criterions.Count - 1)
        versionsRuleCriterion = this.RuleClass.Criterions[num + 1];
      if (num == 0 && versionsRuleCriterion != null && versionsRuleCriterion.CFunc.IsAggregate(versionsRuleCriterion.CompareFunction))
        e.Style = e.Node.TreeList.Styles["BoolNOPStyle"];
      else if (num == this.RuleClass.Criterions.Count - 1 || tag.CFunc.IsAggregate(tag.CompareFunction))
        e.Style = e.Node.TreeList.Styles["BoolNOPStyle"];
      else if (versionsRuleCriterion != null && versionsRuleCriterion.CFunc.IsAggregate(versionsRuleCriterion.CompareFunction))
        e.Style = e.Node.TreeList.Styles["BoolNOPStyle"];
      else
        e.Style = e.Node.TreeList.Styles["BoolStyle"];
    }
    else if (e.Node.Level == 0 && e.Column == this.treeCriterionsValue)
    {
      VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.RootNode.Tag;
      if (tag == null)
        return;
      if (this._fcFunc.IsAggregate(tag.CompareFunction))
        e.Style = e.Node.TreeList.Styles["AggFuncStyle"];
      else
        e.Style = e.Node.TreeList.Styles["DefFuncStyle"];
    }
    else if (e.Node.Level == 0 && e.Column == this.treeCriterionsAdd)
    {
      e.Style = e.Node.TreeList.Styles["NegFontStyle"];
    }
    else
    {
      if (e.Node.Level != 1 || e.Column != this.treeCriterionsValue)
        return;
      ComparableValue tag = (ComparableValue) e.Node.Tag;
      if (tag == null)
        return;
      string valueType = tag.ValueType;
      if (valueType == "ATTRIBUTE")
        e.Style = e.Node.TreeList.Styles["UrlFontStyle"];
      if (valueType == "CONST")
        e.Style = e.Node.TreeList.Styles["ValueFontStyle"];
      if (!(valueType == "VARIABLE"))
        return;
      if (this.EditorMode != 1)
        e.Style = e.Node.TreeList.Styles["PinkFontStyle"];
      else
        e.Style = e.Node.TreeList.Styles["ValueFontStyle"];
    }
  }

  private void CriterionsTree_CellValueChanged(object sender, CellValueChangedEventArgs e)
  {
    if (e.Node == null || e.Column == null || this._inEditor)
      return;
    this._inEditor = true;
    try
    {
      object AValue = (object) null;
      if (e.Value != null)
        AValue = e.Value;
      TreeListNode node = e.Node;
      if (node == null)
        return;
      TreeListNode rootNode = node.RootNode;
      if (rootNode == null)
        return;
      if (e.Column == this.treeCriterionsValue && e.Node.Level == 0)
      {
        VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.Tag;
        if (tag == null)
          return;
        string FunctionName = this._fcFunc.Names.GetKey(this._fcFunc.Names.IndexOfValue(AValue)).ToString();
        if (!this._fcFunc.IsCompatible(FunctionName, tag.MainAttribute.AttrType, tag.MainAttribute.Attribute.IsAttrList))
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          tag.SetCompareFunction(sessionKeeper.Session, FunctionName);
        this.IsChanged = true;
        this.UpdateNode(this.CriterionsTree, rootNode, true);
        this.CriterionsTree.FullExpandNode(rootNode);
        this.CriterionsTree.InvalidateNode(rootNode);
      }
      if (e.Column == this.treeCriterionsCommon && e.Node.Level == 1)
      {
        ComparableValue tag = (ComparableValue) e.Node.Tag;
        if (tag == null)
          return;
        int index = this._fcTypes.Names.IndexOfValue(AValue);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          tag.SetType(sessionKeeper.Session, this._fcTypes.Names.GetKey(index).ToString());
          if (tag.ValueType != "ATTRIBUTE")
          {
            if (tag.Attribute.AttrID == 0)
              tag.Attribute.Assign(tag.Criterion.MainAttribute.Attribute);
          }
        }
        this.IsChanged = true;
        this.UpdateCompareTypeNode(this.CriterionsTree, e.Node);
        this.CriterionsTree.InvalidateNode(rootNode);
      }
      if (e.Column == this.treeCriterionsAdd && e.Node.Level == 0)
      {
        VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.Tag;
        if (tag == null)
          return;
        tag.Negation = AValue.ToString() == fncnConsts.fncnNegation;
        this.IsChanged = true;
        this.UpdateNode(this.CriterionsTree, rootNode, true);
        this.CriterionsTree.FullExpandNode(rootNode);
        this.CriterionsTree.InvalidateNode(rootNode);
      }
      if (e.Column == this.treeCriterionsValue && e.Node.Level == 1)
      {
        VersionsRuleCriterion tag1 = (VersionsRuleCriterion) e.Node.RootNode.Tag;
        if (tag1 == null)
          return;
        ComparableValue tag2 = (ComparableValue) e.Node.Tag;
        if (tag2 == null)
          return;
        MyElement myElement = (MyElement) null;
        if (tag2.AttrType == FieldTypes.ftInteger)
        {
          if (!tag1.MainAttribute.Attribute.IsAttrList)
          {
            try
            {
              AValue = (object) Convert.ToInt64(e.Value);
            }
            catch
            {
              AValue = tag2.Value;
            }
          }
        }
        if (tag2.AttrType == FieldTypes.ftDouble)
        {
          try
          {
            AValue = (object) (double) (Decimal) e.Value;
          }
          catch
          {
            AValue = (object) e.Value.ToString();
          }
        }
        if (tag2.AttrType == FieldTypes.ftDateTime)
        {
          try
          {
            AValue = (object) ((DateTime) e.Value).Date;
          }
          catch
          {
            AValue = (object) e.Value.ToString();
          }
        }
        if (tag1.MainAttribute.Attribute.IsAttrList)
        {
          if (tag1.MainAttribute.Attribute.AttrType == FieldTypes.ftBoolean)
          {
            try
            {
              AValue = e.Value;
            }
            catch
            {
              AValue = (object) false;
            }
          }
          else
            AValue = e.Value;
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (myElement == null)
          {
            tag2.SetValue(sessionKeeper.Session, AValue);
          }
          else
          {
            try
            {
              tag2.SetValue(sessionKeeper.Session, myElement.Value);
            }
            catch
            {
              tag2.SetValue(sessionKeeper.Session, (object) myElement.ToString());
            }
          }
        }
        this.IsChanged = true;
        this.UpdateCompareTypeNode(this.CriterionsTree, e.Node);
        this.CriterionsTree.InvalidateNode(rootNode);
      }
      if (e.Column != this.treeCriterionsBool || e.Node.Level != 0)
        return;
      VersionsRuleCriterion tag3 = (VersionsRuleCriterion) e.Node.RootNode.Tag;
      if (tag3 == null)
        return;
      string str = this._fcOperators.Names.GetKey(this._fcOperators.Names.IndexOfValue(AValue)).ToString();
      tag3.BoolFunction = str;
      this.IsChanged = true;
      this.UpdateNode(this.CriterionsTree, rootNode, true);
      this.CriterionsTree.FullExpandNode(rootNode);
      this.CriterionsTree.InvalidateNode(rootNode);
    }
    finally
    {
      this._inEditor = false;
      this.UpdateControls();
    }
  }

  private void buttonAttribute_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    TreeListNode treeListNode = this.CriterionsTree.Selection[0];
    if (treeListNode == null)
      return;
    TreeListNode rootNode = treeListNode.RootNode;
    if (rootNode == null)
      return;
    VersionsRuleCriterion tag = (VersionsRuleCriterion) rootNode.Tag;
    FieldTypes fieldTypes = tag.MainAttribute.AttrType;
    bool isAttrSystem = tag.MainAttribute.Attribute.IsAttrSystem;
    ComparableValue comparableValue = (ComparableValue) null;
    string attrDialog2 = VersionRulesEditorForm.RulesEditorConsts.AttrDialog2;
    object[] ExcludeAttrs = new object[1]
    {
      (object) tag.MainAttribute.Attribute.AttrID
    };
    if (treeListNode.Level == 0)
    {
      comparableValue = tag.MainAttribute;
      string attrDialog3 = VersionRulesEditorForm.RulesEditorConsts.AttrDialog3;
      fieldTypes = FieldTypes.ftUnknown;
    }
    if (treeListNode.Level == 1)
      comparableValue = (ComparableValue) treeListNode.Tag;
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) new MyAttributeFilter(new List<FieldTypes>((IEnumerable<FieldTypes>) new FieldTypes[1]
      {
        fieldTypes
      }), isAttrSystem, ExcludeAttrs)
      {
        IsUserAttr = MyAttributeHelper.IsUserIDType(tag.MainAttribute.Attribute.AttrID),
        UseAttrType = (fieldTypes != 0)
      };
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      string AttrName = "";
      string AttrGUID = "";
      FieldTypes AttrType = FieldTypes.ftUnknown;
      bool IsSystemType = false;
      int AttrID = attributesSelectDlg.SelectedAttributesID[0];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        MyAttributeHelper.GetAttrInfo(AttrID, ref AttrName, ref AttrGUID, ref AttrType, ref IsSystemType);
        comparableValue.SetValueType(session, "ATTRIBUTE", (object) AttrGUID);
        if (!this._fcFunc.IsCompatible(tag.CompareFunction, comparableValue.Attribute.AttrType, comparableValue.Attribute.IsAttrList))
        {
          tag.SetCompareFunction(session, this._fcFunc.UniversalFunction);
          tag.Valid();
        }
      }
      tag.CorrectValuesType();
      this.IsChanged = true;
      this.UpdateNode(this.CriterionsTree, rootNode, true);
      this.CriterionsTree.FullExpandNode(rootNode);
      this.CriterionsTree.InvalidateNode(rootNode);
      this.UpdateControls();
    }
  }

  private void comboSystemAttr_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int int32 = Convert.ToInt32(e.Button.Tag);
    switch (int32)
    {
      case 1:
      case 2:
      case 3:
        TreeListNode treeListNode = this.CriterionsTree.Selection[0];
        if (treeListNode == null || treeListNode.Level != 1)
          break;
        TreeListNode rootNode = treeListNode.RootNode;
        if (rootNode == null)
          break;
        VersionsRuleCriterion tag1 = (VersionsRuleCriterion) rootNode.Tag;
        if (int32 == 3)
        {
          if (tag1.MainAttribute.Attribute.AttrID != -4)
            break;
          ComparableValue tag2 = (ComparableValue) treeListNode.Tag;
          if (tag2 == null || this.EditorMode == 0 && tag2.ValueType == "ATTRIBUTE" || this.EditorMode == 1 && tag2.ValueType == "ATTRIBUTE" || this.EditorMode == 2)
            break;
          object[] objArray = Intermech.Navigator.SelectionWindow.Select("Выберите шаг ЖЦ", "Выберите шаг жизненного цикла", (IDescriptor) new LifeCycleSchemesDescriptor(), typeof (IDBLCStepID), SelectionOptions.HideViewsToolbar | SelectionOptions.HideViewsGroupingBox | SelectionOptions.SelectOtherNodes | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
          if (objArray == null)
            break;
          int lcStepId = (objArray[0] as IDBLCStepID).LCStepID;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            tag2.Attribute.SetByID(tag1.MainAttribute.Attribute.AttrID);
            tag2.SetValue(sessionKeeper.Session, (object) lcStepId);
          }
          tag1.CorrectValuesType();
          this.IsChanged = true;
          this.UpdateNode(this.CriterionsTree, treeListNode.RootNode, false);
          this.UpdateControls();
        }
        if (int32 == 1)
        {
          if (!MyAttributeHelper.IsUserIDType(tag1.MainAttribute.Attribute.AttrID))
            break;
          ComparableValue tag3 = (ComparableValue) treeListNode.Tag;
          if (tag3 == null || this.EditorMode == 0 && tag3.ValueType == "ATTRIBUTE" || this.EditorMode == 1 && tag3.ValueType == "ATTRIBUTE" || this.EditorMode == 2)
            break;
          Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"), true), true);
          object[] objArray = Intermech.Navigator.SelectionWindow.Select(VersionRulesEditorForm.RulesEditorConsts.AttrDialog10, VersionRulesEditorForm.RulesEditorConsts.AttrDialog11, (IDescriptor) new UsersGroupsDescriptor(), typeof (IDBTypedObjectID), SelectionOptions.Default | SelectionOptions.HideViews | SelectionOptions.DisableSelectFromViews);
          if (objArray == null)
            break;
          if ((objArray[0] as IDBTypedObjectID).ObjectType != MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"))
          {
            int num = (int) MessageBox.Show(VersionRulesEditorForm.RulesEditorConsts.AttrDialog17, VersionRulesEditorForm.RulesEditorConsts.AttrDialog16, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            break;
          }
          long objectId = (objArray[0] as IDBTypedObjectID).ObjectID;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            tag3.SetValue(sessionKeeper.Session, (object) objectId);
          tag1.CorrectValuesType();
          this.IsChanged = true;
          this.UpdateNode(this.CriterionsTree, treeListNode.RootNode, false);
          this.UpdateControls();
        }
        if (int32 != 2 || MeasureHelper.Measures == null || MeasureHelper.Measures.Length == 0 || tag1.MainAttribute.Attribute.AttrType != FieldTypes.ftMeasured)
          break;
        ComparableValue tag4 = (ComparableValue) treeListNode.Tag;
        if (tag4 == null || this.EditorMode == 0 && tag4.ValueType == "ATTRIBUTE" || this.EditorMode == 1 && tag4.ValueType == "ATTRIBUTE" || this.EditorMode == 2)
          break;
        MeasureDescriptor measure = MeasureHelper.Measures[0];
        MeasuredValue aMeasureValue;
        try
        {
          aMeasureValue = MeasureHelper.ConvertToMeasuredValue(tag4.Value.ToString());
        }
        catch
        {
          aMeasureValue = new MeasuredValue(0.0, measure.MeasureID);
        }
        if (new MeasureForm().ExecuteDialog(ref aMeasureValue, MeasureHelper.Measures) != DialogResult.OK)
          break;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          tag4.SetValue(sessionKeeper.Session, (object) MeasureHelper.ConvertToString(aMeasureValue.Value, aMeasureValue.MeasureID, false));
        tag1.CorrectValuesType();
        this.IsChanged = true;
        this.UpdateNode(this.CriterionsTree, treeListNode.RootNode, false);
        this.UpdateControls();
        break;
    }
  }

  private void CriterionsTree_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  private void CriterionsTree_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    if (this._inEventHandlers)
      return;
    this.UpdateControls();
    if (e.Node == null)
      return;
    VersionsRuleCriterion tag = (VersionsRuleCriterion) e.Node.RootNode.Tag;
    if (tag == null)
      return;
    this.CorrectEditors(tag.MainAttribute.AttrType, tag.MainAttribute.Attribute.IsAttrList);
    this.CorrectEditors(tag, tag.MainAttribute.Attribute.AttrPossibleValues);
  }

  private void CriterionsTree_ShowingEditor(object sender, CancelEventArgs e)
  {
    TreeListNode treeListNode1 = (TreeListNode) null;
    TreeListNode treeListNode2 = this.CriterionsTree.Selection[0];
    if (treeListNode2 != null)
      treeListNode1 = treeListNode2.RootNode;
    VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
    if (treeListNode1 != null)
      versionsRuleCriterion = (VersionsRuleCriterion) treeListNode1.Tag;
    if (versionsRuleCriterion == null)
      return;
    this.CorrectEditors(versionsRuleCriterion.MainAttribute.AttrType, versionsRuleCriterion.MainAttribute.Attribute.IsAttrList);
  }

  private void AddCriterionToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddCriterion();
  }

  private void RemoveCriterionToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.RemoveCriterion();
  }

  private void AddValueToolStripMenuItem_Click(object sender, EventArgs e) => this.AddValue();

  private void RemoveValueToolStripMenuItem_Click(object sender, EventArgs e) => this.RemoveValue();

  private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveUp();

  private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveDown();

  private void AdditionalCriterionComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._inEventHandlers || !(this._additionalCriterionAttributeLinkLabel.Tag is VersionsRuleCriterion tag))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      tag.SetCompareFunction(sessionKeeper.Session, this._fcFunc.GetFunctionName(this._additionalCriterionComboBox.Items[this._additionalCriterionComboBox.SelectedIndex].ToString()));
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void AdditionalCriterionAttributeLinkLabel_LinkClicked(
    object sender,
    LinkLabelLinkClickedEventArgs e)
  {
    if (this._inEventHandlers || !(this._additionalCriterionAttributeLinkLabel.Tag is VersionsRuleCriterion tag))
      return;
    bool isAttrSystem = tag.MainAttribute.Attribute.IsAttrSystem;
    ComparableValue mainAttribute = tag.MainAttribute;
    string attrDialog3 = VersionRulesEditorForm.RulesEditorConsts.AttrDialog3;
    object[] ExcludeAttrs = new object[1]
    {
      (object) tag.MainAttribute.Attribute.AttrID
    };
    SelectorForm selectorForm = new SelectorForm(typeof (AttributesFolder), attrDialog3, typeof (AttributeFolder), false);
    selectorForm.SelectorFilter = (ISelectorFilter) new MyAttributeFilter(this._fcFunc.GetAggregateFieldTypes(), isAttrSystem, ExcludeAttrs)
    {
      IsUserAttr = MyAttributeHelper.IsUserIDType(tag.MainAttribute.Attribute.AttrID)
    };
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    string AttrName = "";
    string AttrGUID = "";
    FieldTypes AttrType = FieldTypes.ftUnknown;
    bool IsSystemType = false;
    int int32 = Convert.ToInt32(selectorForm.IDList[0]);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      MyAttributeHelper.GetAttrInfo(int32, ref AttrName, ref AttrGUID, ref AttrType, ref IsSystemType);
      mainAttribute.SetValueType(session, "ATTRIBUTE", (object) AttrGUID);
      if (!this._fcFunc.IsCompatible(tag.CompareFunction, mainAttribute.Attribute.AttrType, mainAttribute.Attribute.IsAttrList))
      {
        tag.SetCompareFunction(session, this._fcFunc.UniversalFunction);
        tag.Valid();
      }
    }
    tag.CorrectValuesType();
    this.IsChanged = true;
    this._additionalCriterionAttributeLinkLabel.Text = tag.MainAttribute.Attribute.AttrName;
    this.UpdateControls();
  }

  private void ActualDateDateTimePicker_EnabledChanged(object sender, EventArgs e)
  {
    int num = this._inEventHandlers ? 1 : 0;
  }

  private void ActualDateDateTimePicker_ValueChanged(object sender, EventArgs e)
  {
    if (this._inEventHandlers || this.RuleClass == null)
      return;
    if (this.EditorMode == 2)
      return;
    try
    {
      this.RuleClass.ActualDate = this._actualDateDateTimePicker.Checked ? this._actualDateDateTimePicker.Value.Date : DateTime.MinValue;
      if (this._actualDateDateTimePicker.Checked)
        this._editingRuleCheckBox.Checked = false;
      this.IsChanged = true;
      if (!this._actualDateDateTimePicker.Checked || !this.CorrectDateTimeRule())
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.RuleClass.ConvertToActualDateRule(sessionKeeper.Session);
        this.BuildRuleNodes();
      }
    }
    finally
    {
      this.UpdateControls();
    }
  }

  private void ExpandAllCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (this._expandAllCheckBox.Checked)
      this.CriterionsTree.FullExpand();
    else
      this.CriterionsTree.FullCollapse();
  }

  private void EditingRuleCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (this._inEventHandlers || this.RuleClass == null || this.EditorMode == 2)
      return;
    this.RuleClass.EditingRule = this._editingRuleCheckBox.Checked;
    if (this._editingRuleCheckBox.Checked)
      this._actualDateDateTimePicker.Checked = false;
    this.RuntimeFillControls();
    if (this._editingRuleCheckBox.Checked)
      this._ignoreUserConcretizationCheckBox.Checked = false;
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void IgnoreUserConcretizationCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (this._inEventHandlers || this.RuleClass == null || this.EditorMode == 2)
      return;
    if (!this.RuleClass.EditingRule)
      this.RuleClass.IgnoreSoftConcretization = this._ignoreUserConcretizationCheckBox.Checked;
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void AcceptButton_Click(object sender, EventArgs e)
  {
    if (this.HideApplyCancel)
      return;
    if (this.EditorMode != 0)
    {
      this.DialogResult = DialogResult.OK;
    }
    else
    {
      if (!this.IsChanged)
        return;
      this.SaveObjectData();
    }
  }

  private void CancelButton_Click(object sender, EventArgs e)
  {
    if (this.HideApplyCancel)
      return;
    if (this.EditorMode == 2)
      this.DialogResult = DialogResult.Cancel;
    else if (this.EditorMode == 1)
    {
      if (!this.IsChanged)
      {
        this.DialogResult = DialogResult.Cancel;
      }
      else
      {
        if (MessageBox.Show(VersionRulesEditorForm.RulesEditorConsts.AttrDialog8, VersionRulesEditorForm.RulesEditorConsts.AttrDialog9, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
          return;
        this.DialogResult = DialogResult.Cancel;
      }
    }
    else
    {
      this.DialogResult = DialogResult.None;
      if (!this.IsChanged || MessageBox.Show(VersionRulesEditorForm.RulesEditorConsts.AttrDialog6, VersionRulesEditorForm.RulesEditorConsts.AttrDialog7, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
        return;
      this.LoadObjectData(this.EditorMode);
    }
  }

  private void AddToDropdownListCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (this._inEventHandlers || this.RuleClass == null || this.EditorMode == 2)
      return;
    this.RuleClass.AddToDropDownList = this._addToDropdownListCheckBox.Checked;
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void AddCriterion()
  {
    this.UpdateControls();
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      MyAttributeFilter myAttributeFilter = new MyAttributeFilter(new List<FieldTypes>(), false, (object[]) null);
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) myAttributeFilter;
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      string AttrName = "";
      string AttrGUID = "";
      FieldTypes AttrType = FieldTypes.ftUnknown;
      bool IsSystemType = false;
      MyAttributeHelper.GetAttrInfo(Convert.ToInt32(attributesSelectDlg.SelectedAttributesID[0]), ref AttrName, ref AttrGUID, ref AttrType, ref IsSystemType);
      this.BuildNodes(this.CriterionsTree, AttrGUID, this._fcFunc.UniversalFunction, CompareOperatorsHelper.ctDefaultFunction);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.RuleClass.Valid(sessionKeeper.Session);
      this.IsChanged = true;
      this.UpdateControls();
    }
  }

  private void RemoveCriterion()
  {
    TreeListNode node = (TreeListNode) null;
    TreeListNode treeListNode = this.CriterionsTree.Selection[0];
    if (treeListNode != null)
      node = treeListNode.RootNode;
    VersionsRuleCriterion Criterion = (VersionsRuleCriterion) null;
    if (node != null)
      Criterion = (VersionsRuleCriterion) node.Tag;
    if (this._inEditor || node == null || Criterion == null)
      return;
    int StandardCriterions;
    int AdvancedCriterions;
    this.RuleClass.CriterionsCount(out StandardCriterions, out AdvancedCriterions);
    if (Criterion.CFunc.IsAggregate(Criterion.CompareFunction) && AdvancedCriterions <= 1)
    {
      int num1 = (int) MessageBox.Show(VersionRulesEditorForm.RulesEditorConsts.AttrDialog15, VersionRulesEditorForm.RulesEditorConsts.AttrDialog14, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else if (!Criterion.CFunc.IsAggregate(Criterion.CompareFunction) && StandardCriterions <= 1)
    {
      int num2 = (int) MessageBox.Show(VersionRulesEditorForm.RulesEditorConsts.AttrDialog13, VersionRulesEditorForm.RulesEditorConsts.AttrDialog12, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      if (MessageBox.Show($"{VersionRulesEditorForm.RulesEditorConsts.AttrDialog4}{sc_4599.ssp_imclient_4600()}{Criterion.MainAttribute.Attribute.AttrName}\"", VersionRulesEditorForm.RulesEditorConsts.AttrDialog5, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
        return;
      this.CriterionsTree.DeleteNode(node);
      this.RuleClass.Remove(Criterion);
      this.IsChanged = true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.RuleClass.Valid(sessionKeeper.Session);
      if (this._actualDateDateTimePicker.Checked)
      {
        if (!this.CorrectDateTimeRule())
        {
          this.BuildRuleNodes();
          this.UpdateControls();
          return;
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this.RuleClass.ConvertToActualDateRule(sessionKeeper.Session);
          this.BuildRuleNodes();
        }
      }
      this.UpdateControls();
    }
  }

  private void AddValue()
  {
    TreeListNode Node = (TreeListNode) null;
    TreeListNode treeListNode = this.CriterionsTree.Selection[0];
    if (treeListNode != null)
      Node = treeListNode.RootNode;
    VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
    if (Node != null)
      versionsRuleCriterion = (VersionsRuleCriterion) Node.Tag;
    string str = "";
    if (versionsRuleCriterion != null)
      str = versionsRuleCriterion.CompareFunction;
    if (this._inEditor || versionsRuleCriterion == null || str == "" || !versionsRuleCriterion.CanAddValue())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      versionsRuleCriterion.Add(session, "CONST", cvConsts.cvConst);
    }
    this.UpdateNode(this.CriterionsTree, Node, true);
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void RemoveValue()
  {
    TreeListNode Node = (TreeListNode) null;
    TreeListNode treeListNode = this.CriterionsTree.Selection[0];
    if (treeListNode != null)
      Node = treeListNode.RootNode;
    VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
    if (Node != null)
      versionsRuleCriterion = (VersionsRuleCriterion) Node.Tag;
    ComparableValue comparableValue = (ComparableValue) null;
    if (treeListNode != null && treeListNode.Level == 1)
      comparableValue = (ComparableValue) treeListNode.Tag;
    if (treeListNode == null || versionsRuleCriterion == null || !versionsRuleCriterion.CanDeleteValue() || comparableValue == null)
      return;
    versionsRuleCriterion.Remove(comparableValue);
    this.UpdateNode(this.CriterionsTree, Node, true);
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void MoveUp()
  {
    TreeListNode treeListNode = (TreeListNode) null;
    TreeListNode node1 = this.CriterionsTree.Selection[0];
    if (node1 != null)
      treeListNode = node1.RootNode;
    VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
    if (treeListNode != null)
      versionsRuleCriterion = (VersionsRuleCriterion) treeListNode.Tag;
    if (node1 == null || node1.Level != 0 || versionsRuleCriterion == null)
      return;
    int Index1 = this.RuleClass.Criterions.IndexOf(versionsRuleCriterion);
    if (Index1 <= 0)
      return;
    int num = node1.TreeList.Nodes.IndexOf(node1);
    if (num <= 0)
      return;
    TreeListNode node2 = node1.TreeList.Nodes[num - 1];
    if (node2.Level != node1.Level || node2.ParentNode != node1.ParentNode)
      return;
    VersionsRuleCriterion criterion = this.RuleClass.Criterions[Index1 - 1];
    node1.TreeList.SetNodeIndex(node1, num - 1);
    this.RuleClass.Exchange(Index1, Index1 - 1);
    node1.TreeList.SetFocusedNode(node1);
    node1.TreeList.TopVisibleNodeIndex = num - 1;
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void MoveDown()
  {
    TreeListNode treeListNode = (TreeListNode) null;
    TreeListNode node1 = this.CriterionsTree.Selection[0];
    if (node1 != null)
      treeListNode = node1.RootNode;
    VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
    if (treeListNode != null)
      versionsRuleCriterion = (VersionsRuleCriterion) treeListNode.Tag;
    if (node1 == null || node1.Level != 0 || versionsRuleCriterion == null)
      return;
    int Index1 = this.RuleClass.Criterions.IndexOf(versionsRuleCriterion);
    if (Index1 >= this.RuleClass.Criterions.Count - 2)
      return;
    int num = node1.TreeList.Nodes.IndexOf(node1);
    if (num < 0 || num >= this.RuleClass.Criterions.Count - 2)
      return;
    TreeListNode node2 = node1.TreeList.Nodes[num + 1];
    if (node2.Level != node1.Level || node2.ParentNode != node1.ParentNode)
      return;
    VersionsRuleCriterion criterion = this.RuleClass.Criterions[Index1 + 1];
    node1.TreeList.SetNodeIndex(node1, num + 1);
    this.RuleClass.Exchange(Index1, Index1 + 1);
    node1.TreeList.SetFocusedNode(node1);
    node1.TreeList.TopVisibleNodeIndex = num + 1;
    this.IsChanged = true;
    this.UpdateControls();
  }

  /// <summary>
  /// Заполнить комбо-боксы и прочие контролы нашими данными в рантайме
  /// </summary>
  private void RuntimeFillControls()
  {
    this.comboFunction.Items.Clear();
    this.comboFunction.Items.AddRange(this._fcFunc.GetMembers(false, false));
    this.comboCompareType.Items.Clear();
    if (this._disableVariableValues)
      this.comboCompareType.Items.AddRange(this._fcTypesNoParams.GetMembers(false));
    else
      this.comboCompareType.Items.AddRange(this._fcTypes.GetMembers(false));
    this.comboNegation.Items.Clear();
    this.comboNegation.Items.AddRange(fncnConsts.GetMembers());
    this.comboOperators.Items.Clear();
    this.comboOperators.Items.AddRange(this._fcOperators.GetMembers(false));
    bool onlyBaseVersion = false;
    if (this.RuleClass != null)
      onlyBaseVersion = this.RuleClass.EditingRule;
    VersionsRuleCriterion tag = this._additionalCriterionAttributeLinkLabel.Tag as VersionsRuleCriterion;
    this._additionalCriterionComboBox.BeginUpdate();
    try
    {
      this._additionalCriterionComboBox.Items.Clear();
      this._additionalCriterionComboBox.Items.AddRange(this._fcFunc.GetAggregateFunctions(false, onlyBaseVersion));
    }
    finally
    {
      this._additionalCriterionComboBox.EndUpdate();
    }
    for (int index = 0; index < this._additionalCriterionComboBox.Items.Count; ++index)
    {
      if (this._additionalCriterionComboBox.Items[index].ToString() == this._fcFunc.Names[(object) tag.CompareFunction].ToString())
      {
        this._additionalCriterionComboBox.SelectedIndex = index;
        break;
      }
    }
    if (this._additionalCriterionComboBox.SelectedIndex >= 0)
      return;
    this._additionalCriterionComboBox.SelectedIndex = 0;
  }

  /// <summary>Очистка внутренних структур</summary>
  private void Clear()
  {
    this.RuleClass.Clear();
    this.BuildRuleNodes();
    this.RuntimeFillControls();
    this.UpdateControls();
  }

  /// <summary>Быстрая очистка внутренних структур</summary>
  private void ClearQuick()
  {
    if (this.RuleClass == null)
      return;
    this.RuleClass.Clear();
  }

  /// <summary>
  /// Согласно указанной функции CompareTo создать (или исправить) структуру дочерних узлов в Node
  /// </summary>
  /// <param name="InTree">Дерево, в котором ведутся все изменения</param>
  /// <param name="Node">Корневой узел в дереве InTree</param>
  private void BuildChilds(TreeList InTree, TreeListNode Node)
  {
    if (InTree == null || Node == null)
      return;
    Node.Nodes.Clear();
    VersionsRuleCriterion tag = (VersionsRuleCriterion) Node.Tag;
    if (tag == null || this._fcFunc.MaxComparableValues(tag.CompareFunction) <= 0)
      return;
    int count = tag.ComparableValues.Count;
    if (count <= 0)
      return;
    for (int index = 0; index < count; ++index)
    {
      ComparableValue comparableValue = tag[index];
      if (comparableValue != null && (this.EditorMode != 1 || !(comparableValue.ValueType != "VARIABLE")))
      {
        string str = this._fcTypes.Names[(object) comparableValue.ValueType].ToString();
        if (comparableValue.Attribute.AttrID == 0 && comparableValue.ValueType != "ATTRIBUTE")
          comparableValue.Attribute.Assign(tag.MainAttribute.Attribute);
        object displayValue = comparableValue.GetDisplayValue(this.EditorMode);
        TreeListNode treeListNode = InTree.AppendNode((object) new object[3]
        {
          (object) str,
          (object) "",
          displayValue
        }, Node);
        treeListNode.Tag = (object) comparableValue;
        int icon = VersionRulesEditorForm._icons[2];
        if (comparableValue.ValueType == "VARIABLE")
          icon = VersionRulesEditorForm._icons[3];
        if (comparableValue.ValueType == "ATTRIBUTE")
          icon = VersionRulesEditorForm._icons[4];
        treeListNode.ImageIndex = icon;
        treeListNode.SelectImageIndex = treeListNode.ImageIndex;
        treeListNode.StateImageIndex = -1;
      }
    }
  }

  /// <summary>Обновить указанный коревой узел</summary>
  /// <param name="InTree">Дерево, в который надо добавить узлы</param>
  /// <param name="Node">Корневой узел</param>
  /// <param name="LockTree">Блокировать ли прорисовку дерева во время его обновления</param>
  private void UpdateNode(TreeList InTree, TreeListNode Node, bool LockTree)
  {
    if (InTree == null || Node == null)
      return;
    Node = Node.RootNode;
    if (Node == null)
      return;
    VersionsRuleCriterion tag = (VersionsRuleCriterion) Node.Tag;
    if (tag == null)
      return;
    string compareFunction = tag.CompareFunction;
    if (LockTree)
      InTree.BeginUpdate();
    try
    {
      Node.ImageIndex = this.GetTypeImageIndex(tag);
      Node.SelectImageIndex = Node.ImageIndex;
      Node.StateImageIndex = -1;
      Node.SetValue((object) this.treeCriterionsCommon, (object) tag.MainAttribute.Attribute.AttrName);
      Node.SetValue((object) this.treeCriterionsAdd, (object) fncnConsts.GetNegationValue(tag.Negation));
      Node.SetValue((object) this.treeCriterionsValue, this._fcFunc.Names[(object) tag.CompareFunction]);
      Node.SetValue((object) this.treeCriterionsBool, this._fcOperators.Names[(object) tag.BoolFunction]);
      this.BuildChilds(InTree, Node);
      if (!this._expandAllCheckBox.Checked)
        return;
      Node.Expanded = true;
    }
    finally
    {
      if (LockTree)
        InTree.EndUpdate();
    }
  }

  /// <summary>
  /// Обновить указанный узел с типом значения для сравнения
  /// </summary>
  /// <param name="InTree">Дерево, в который надо добавить узлы</param>
  /// <param name="Node">Узел с типом значения для сравнения</param>
  private void UpdateCompareTypeNode(TreeList InTree, TreeListNode Node)
  {
    if (InTree == null || Node == null || Node.Level != 1 || (VersionsRuleCriterion) Node.RootNode.Tag == null)
      return;
    ComparableValue tag = (ComparableValue) Node.Tag;
    if (tag == null)
      return;
    Node[(object) this.treeCriterionsValue] = tag.GetDisplayValue(this.EditorMode);
    int icon = VersionRulesEditorForm._icons[2];
    if (tag.ValueType == "VARIABLE")
      icon = VersionRulesEditorForm._icons[3];
    if (tag.ValueType == "ATTRIBUTE")
      icon = VersionRulesEditorForm._icons[4];
    Node.ImageIndex = icon;
    Node.SelectImageIndex = Node.ImageIndex;
    InTree.InvalidateNode(Node);
  }

  /// <summary>
  /// Построить в дереве InTree список узлов для указанного атрибута и по указанному правилу
  /// </summary>
  /// <param name="InTree">Дерево, в который надо добавить узлы</param>
  /// <param name="AttributeGUID">GUID атрибута, по которому будет проводиться поиск</param>
  /// <param name="CompareFunction">Функция сравнения</param>
  /// <param name="BoolFunction">Логическая функция для сравнения со следующим критерием</param>
  /// <returns></returns>
  private TreeListNode BuildNodes(
    TreeList InTree,
    string AttributeGUID,
    string CompareFunction,
    string BoolFunction)
  {
    if (InTree == null || AttributeGUID.Length <= 0 || CompareFunction.Length <= 0)
      return (TreeListNode) null;
    VersionsRuleCriterion Criterion = (VersionsRuleCriterion) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      Criterion = this.RuleClass.Add(sessionKeeper.Session, AttributeGUID, CompareFunction, BoolFunction, (ArrayList) null, (ArrayList) null);
    if (Criterion == null)
      return (TreeListNode) null;
    InTree.BeginUpdate();
    try
    {
      TreeListNode treeListNode = InTree.AppendNode((object) new object[4]
      {
        (object) Criterion.MainAttribute.Attribute.AttrName,
        (object) fncnConsts.GetNegationValue(Criterion.Negation),
        this._fcFunc.Names[(object) Criterion.CompareFunction],
        this._fcOperators.Names[(object) Criterion.BoolFunction]
      }, (TreeListNode) null);
      treeListNode.Tag = (object) Criterion;
      if (treeListNode == null)
        return (TreeListNode) null;
      treeListNode.ImageIndex = this.GetTypeImageIndex(Criterion);
      treeListNode.SelectImageIndex = treeListNode.ImageIndex;
      treeListNode.StateImageIndex = -1;
      this.BuildChilds(InTree, treeListNode);
      InTree.FullExpandNode(treeListNode);
      InTree.InvalidateNode(treeListNode);
      return treeListNode;
    }
    finally
    {
      InTree.EndUpdate();
      InTree.Refresh();
    }
  }

  /// <summary>
  /// Выполнить корректировку списка выдаваемых функций сравнения
  /// </summary>
  /// <param name="DataType">Тип данных главного атрибута критерия отбора</param>
  /// <param name="IsListValue">Является ли исследуемый атрибут списковым</param>
  private void CorrectEditors(FieldTypes DataType, bool IsListValue)
  {
    if (this._funcEditor == null)
      this._funcEditor = this.comboFunction;
    this._funcEditor.Items.Clear();
    this._funcEditor.Items.AddRange(this._fcFunc.GetMembers(false, DataType, IsListValue, false));
  }

  /// <summary>Выполнить корректировку списка допустмых значений</summary>
  /// <param name="Criterion">Критерий подбора</param>
  /// <param name="PosValues">Список допустимых значений (коллекция MyElement)</param>
  private void CorrectEditors(VersionsRuleCriterion Criterion, ArrayList PosValues)
  {
    this.comboSystemAttr.Items.Clear();
    if (PosValues != null && PosValues.Count > 0)
    {
      foreach (MyElement posValue in PosValues)
        this.comboSystemAttr.Items.Add((object) posValue);
    }
    if (Criterion == null)
      return;
    this.comboSystemAttr.Buttons[3].Visible = Criterion.MainAttribute.Attribute.AttrID == -4;
    this.comboSystemAttr.Buttons[1].Visible = MyAttributeHelper.IsUserIDType(Criterion.MainAttribute.Attribute.AttrID);
    this.comboSystemAttr.Buttons[2].Visible = Criterion.MainAttribute.Attribute.AttrType == FieldTypes.ftMeasured && !this.comboSystemAttr.Buttons[3].Visible;
    this.comboSystemAttr.Buttons[0].Visible = PosValues.Count > 0 && this.EditorMode != 1;
  }

  /// <summary>Вернуть номер значка для указанного типа данных</summary>
  /// <param name="Criterion">Критерий подбора, для значений сравнения которого подбираются значки</param>
  /// <returns>Номер значка для указанного типа</returns>
  private int GetTypeImageIndex(VersionsRuleCriterion Criterion)
  {
    if (Criterion == null)
      return -1;
    if (MyAttributeHelper.IsUserIDType(Criterion.MainAttribute.Attribute.AttrID) || MyAttributeHelper.IsUserIDType(Criterion.MainAttribute.Attribute.AttrID))
      return VersionRulesEditorForm._icons[5];
    if (Criterion.MainAttribute.Attribute.AttrID == -9)
      return VersionRulesEditorForm._icons[6];
    return this._fObjtypesIcons == null ? -1 : this._fObjtypesIcons.IndexOf(3, -1, (object) Criterion.MainAttribute.AttrType);
  }

  /// <summary>Корректно ли правило по подбору составов на дату</summary>
  /// <returns>true - правило корректно или было откорректировано</returns>
  private bool CorrectDateTimeRule()
  {
    if (this._actualDateDateTimePicker.Checked)
    {
      bool flag = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        flag = this.RuleClass.IsCorrectActualDateRule(sessionKeeper.Session);
      if (!flag)
      {
        if (IMMessageBox.Show("Внимание", "Для корректного подбора составов на дату в правиле подбора должен быть критерий,\nсвязанный с уровнем продвижения или шагом жизненного цикла.\n\nДобавить такой критерий в правило?\n", new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("Добавить", DialogResult.OK),
          new IMMessageBoxButton("Отмена", DialogResult.Cancel)
        }, IMMessageBoxImage.Information) != DialogResult.OK)
        {
          this._actualDateDateTimePicker.Checked = false;
          this.RuleClass.ActualDate = this._actualDateDateTimePicker.Checked ? this._actualDateDateTimePicker.Value.Date : DateTime.MinValue;
          if (this._actualDateDateTimePicker.Checked)
            this._editingRuleCheckBox.Checked = false;
          return false;
        }
      }
    }
    return true;
  }

  /// <summary>
  /// Делегат события об изменении в редакторе правила подбора версий
  /// </summary>
  /// <param name="sender">Контрол (редактор правила подбора версий)</param>
  /// <param name="e">Аргументы события</param>
  public delegate void VersionRulesEditorChangedEventHandler(object sender, EventArgs e);

  /// <summary>
  /// Номера изображений в списке для узлов с критериями правила отбора версий
  /// </summary>
  private static class RulesEditorImages
  {
    public const int imgStandardCriterion = 0;
    public const int imgAdditionalCriterion = 1;
    public const int imgValueConst = 2;
    public const int imgValueVariable = 3;
    public const int imgValueAttribute = 4;
    public const int img_ftUserID = 5;
    public const int img_ftLevelID = 6;
  }

  /// <summary>
  /// Свалка констант для формы-редактора правил отбора версий
  /// </summary>
  private static class RulesEditorConsts
  {
    /// <summary>Применить</summary>
    public static readonly string ApplyText1 = LocalizationHolder.rm.GetString("Client.Core_167");
    /// <summary>ОК</summary>
    public static readonly string ApplyText2 = LocalizationHolder.rm.GetString("Client.Core_218");
    /// <summary>Отмена</summary>
    public static readonly string CancelText1 = LocalizationHolder.rm.GetString("Client.Core_166");
    /// <summary>Закрыть</summary>
    public static readonly string CancelText2 = LocalizationHolder.rm.GetString("Client.Core_217");
    /// <summary>
    /// Данное правило является системным. Его нельзя изменить или удалить.
    /// </summary>
    public static readonly string PanelHint1 = LocalizationHolder.rm.GetString("Client.Core_809");
    /// <summary>Выберите атрибут для нового критерия подбора версий</summary>
    public static readonly string AttrDialog1 = LocalizationHolder.rm.GetString("Client.Core_810");
    /// <summary>Выберите атрибут текущего значения для сравнения</summary>
    public static readonly string AttrDialog2 = LocalizationHolder.rm.GetString("Client.Core_811");
    /// <summary>Выберите атрибут критерия подбора версий объектов</summary>
    public static readonly string AttrDialog3 = LocalizationHolder.rm.GetString("Client.Core_812");
    /// <summary>
    /// Вы действительно хотите удалить указанный критерий ?\n\nКритерий:
    /// </summary>
    public static readonly string AttrDialog4 = LocalizationHolder.rm.GetString("Client.Core_813");
    /// <summary>Удаление критерия подбора из текущего правила</summary>
    public static readonly string AttrDialog5 = LocalizationHolder.rm.GetString("Client.Core_814");
    /// <summary>
    /// Вы действительно хотите отменить все изменения\nв данном правиле подбора версий?
    /// </summary>
    public static readonly string AttrDialog6 = LocalizationHolder.rm.GetString("Client.Core_815");
    /// <summary>Отмена изменений в правиле подбора версий</summary>
    public static readonly string AttrDialog7 = LocalizationHolder.rm.GetString("Client.Core_816");
    /// <summary>
    /// Вы действительно хотите отменить все изменения\nв указанных критериях?
    /// </summary>
    public static readonly string AttrDialog8 = LocalizationHolder.rm.GetString("Client.Core_817");
    /// <summary>Отмена изменений в критериях правила подбора версий</summary>
    public static readonly string AttrDialog9 = LocalizationHolder.rm.GetString("Client.Core_818");
    /// <summary>[Неверный номер функции]</summary>
    public static readonly string ErrorCell1 = LocalizationHolder.rm.GetString("Client.Core_819");
    /// <summary>Выбор пользователя</summary>
    public static readonly string AttrDialog10 = LocalizationHolder.rm.GetString("Client.Core_820");
    /// <summary>Выберите имя пользователя</summary>
    public static readonly string AttrDialog11 = LocalizationHolder.rm.GetString("Client.Core_821");
    /// <summary>Удаление основного критерия подбора</summary>
    public static readonly string AttrDialog12 = LocalizationHolder.rm.GetString("Client.Core_822");
    /// <summary>
    /// В правиле должен быть как минимум один основной критерий подбора версий
    /// </summary>
    public static readonly string AttrDialog13 = LocalizationHolder.rm.GetString("Client.Core_823");
    /// <summary>Удаление дополнительного критерия подбора</summary>
    public static readonly string AttrDialog14 = LocalizationHolder.rm.GetString("Client.Core_824");
    /// <summary>
    /// В правиле должен быть один дополнительный критерий подбора версий. Его удаление запрещено
    /// </summary>
    public static readonly string AttrDialog15 = LocalizationHolder.rm.GetString("Client.Core_825");
    /// <summary>Выбор пользователя</summary>
    public static readonly string AttrDialog16 = LocalizationHolder.rm.GetString("Client.Core_820");
    /// <summary>
    /// Вы должны выбрать в качестве значения для сравнения\nучётную запись пользователя, а не группу пользователей
    /// </summary>
    public static readonly string AttrDialog17 = LocalizationHolder.rm.GetString("Client.Core_826");
    /// <summary>Очень длинный текст :-)</summary>
    public static readonly string AttrDialog18 = LocalizationHolder.rm.GetString("Client.Core_827") + LocalizationHolder.rm.GetString("Client.Core_828") + LocalizationHolder.rm.GetString("Client.Core_829") + LocalizationHolder.rm.GetString("Client.Core_830");
    /// <summary>Преобразование стандартного правила в системное</summary>
    public static readonly string AttrDialog19 = LocalizationHolder.rm.GetString("Client.Core_831");
  }
}
