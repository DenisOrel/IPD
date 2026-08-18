// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SameDesignationsSetupForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Mask;
using Intermech.Controls.Grid;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса SameDesignationsSetupForm </summary>
public class SameDesignationsSetupForm : ExtForm
{
  private IContainer components;
  private Button _BtnOK;
  private Button _BtnCancel;
  private ToolTipController _EditModeToolTip;
  private Label label2;
  private Label label6;
  private Label label7;
  private GroupBox _GroupBoxStartSubstr;
  private Label _label3;
  private Label _label4;
  private Label _label5;
  protected SpinEdit _upDownSubstrStartNumber;
  private ImageComboBoxEdit _comboBoxSubstrStartFrom;
  private Label _label1;
  private ListGrid _ListGridSubStr;
  private GroupBox _GroupBoxFinishSubstr;
  protected SpinEdit _upDownSubstrFinishNumber;
  private Button _btnAddSubStr;
  private Button _btnDelSubStr;
  private ImageComboBoxEdit _comboBoxSubstrFinishAt;
  private ComboBoxEdit _comboBoxSubstrStartSymbol;
  private ComboBoxEdit _comboBoxSubstrFinishSymbol;
  public Button _BtnReset;
  private ImageComboBoxEdit _comboBoxListSource;
  private Label label3;
  private Button _buttonTest;
  private ToolTipController _ReadModeToolTip;
  private CompareDesignationSchema _compareDesignationSchema;
  private CompareDesignationSchema _oldCompareDesignationSchema;
  private static char[] _predefinedSymbols = new char[5];
  private Control _changedControl;
  private IStructualControlSupport _iStructualControlSupport;
  private InitDataEventHandler _onInitDataEventDelegateThis;

  static SameDesignationsSetupForm()
  {
    SameDesignationsSetupForm._predefinedSymbols.SetValue((object) Convert.ToChar("."), 0);
    SameDesignationsSetupForm._predefinedSymbols.SetValue((object) Convert.ToChar("-"), 1);
    SameDesignationsSetupForm._predefinedSymbols.SetValue((object) Convert.ToChar("*"), 2);
    SameDesignationsSetupForm._predefinedSymbols.SetValue((object) Convert.ToChar(","), 3);
    SameDesignationsSetupForm._predefinedSymbols.SetValue((object) Convert.ToChar(" "), 4);
  }

  public SameDesignationsSetupForm()
  {
    this.InitializeComponent();
    this.Init((CompareDesignationSchema) null);
  }

  public SameDesignationsSetupForm(CompareDesignationSchema compareDesignationSchema)
  {
    this.InitializeComponent();
    this.Init(compareDesignationSchema);
  }

  public SameDesignationsSetupForm(
    Control owner,
    CompareDesignationSchema compareDesignationSchema,
    IStructualControlSupport iStructualControlSupport)
    : base(owner)
  {
    this.InitializeComponent();
    this._onInitDataEventDelegateThis = new InitDataEventHandler(this.OnInitData);
    this._iStructualControlSupport = iStructualControlSupport;
    iStructualControlSupport.OnInitDataEvent += this._onInitDataEventDelegateThis;
    this.Init(compareDesignationSchema);
  }

  private void Init(CompareDesignationSchema compareDesignationSchema)
  {
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1522);
    this._oldCompareDesignationSchema = compareDesignationSchema;
    this.CompareDesignationSchema = compareDesignationSchema.Clone();
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    if (this._iStructualControlSupport != null && this._onInitDataEventDelegateThis != null)
      this._iStructualControlSupport.OnInitDataEvent -= this._onInitDataEventDelegateThis;
    this._onInitDataEventDelegateThis = (InitDataEventHandler) null;
    this._iStructualControlSupport = (IStructualControlSupport) null;
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ListColumn listColumn1 = new ListColumn();
    ListColumn listColumn2 = new ListColumn();
    Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem();
    ListSubItem listSubItem1 = new ListSubItem();
    ListSubItem listSubItem2 = new ListSubItem();
    this._EditModeToolTip = new ToolTipController(this.components);
    this._BtnOK = new Button();
    this._BtnCancel = new Button();
    this._ListGridSubStr = new ListGrid();
    this._btnAddSubStr = new Button();
    this._btnDelSubStr = new Button();
    this._BtnReset = new Button();
    this._buttonTest = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this._GroupBoxStartSubstr = new GroupBox();
    this._comboBoxSubstrStartSymbol = new ComboBoxEdit();
    this._upDownSubstrStartNumber = new SpinEdit();
    this._label5 = new Label();
    this._label4 = new Label();
    this._label3 = new Label();
    this._comboBoxSubstrStartFrom = new ImageComboBoxEdit();
    this._label1 = new Label();
    this._GroupBoxFinishSubstr = new GroupBox();
    this._comboBoxSubstrFinishSymbol = new ComboBoxEdit();
    this._upDownSubstrFinishNumber = new SpinEdit();
    this.label2 = new Label();
    this.label6 = new Label();
    this.label7 = new Label();
    this._comboBoxSubstrFinishAt = new ImageComboBoxEdit();
    this._comboBoxListSource = new ImageComboBoxEdit();
    this.label3 = new Label();
    this._GroupBoxStartSubstr.SuspendLayout();
    this._comboBoxSubstrStartSymbol.Properties.BeginInit();
    this._upDownSubstrStartNumber.Properties.BeginInit();
    this._comboBoxSubstrStartFrom.Properties.BeginInit();
    this._GroupBoxFinishSubstr.SuspendLayout();
    this._comboBoxSubstrFinishSymbol.Properties.BeginInit();
    this._upDownSubstrFinishNumber.Properties.BeginInit();
    this._comboBoxSubstrFinishAt.Properties.BeginInit();
    this._comboBoxListSource.Properties.BeginInit();
    this.SuspendLayout();
    this._EditModeToolTip.Active = false;
    this._EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(433, 307);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(75, 23);
    this._BtnOK.TabIndex = 8;
    this._BtnOK.Text = "ОК";
    this._EditModeToolTip.SetToolTip((Control) this._BtnOK, "Сохранить изменения и закрыть диалог");
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(513, 307);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(75, 23);
    this._BtnCancel.TabIndex = 9;
    this._BtnCancel.Text = "Отмена";
    this._ReadModeToolTip.SetToolTip((Control) this._BtnCancel, "Закрыть диалог");
    this._EditModeToolTip.SetToolTip((Control) this._BtnCancel, "Отменить изменения и закрыть диалог");
    this._ListGridSubStr.AllowColumnResize = false;
    this._ListGridSubStr.AlternateBackground = Color.DarkGreen;
    this._ListGridSubStr.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._ListGridSubStr.AutoHeight = false;
    this._ListGridSubStr.BackColor = SystemColors.Window;
    listColumn1.Name = "Column1";
    listColumn1.Text = "От";
    listColumn1.TextAlignment = ContentAlignment.MiddleCenter;
    listColumn1.Width = 290;
    listColumn2.Name = "Column2";
    listColumn2.Text = "До";
    listColumn2.TextAlignment = ContentAlignment.MiddleCenter;
    listColumn2.Width = 290;
    this._ListGridSubStr.Columns.AddRange(new ListColumn[2]
    {
      listColumn1,
      listColumn2
    });
    this._ListGridSubStr.GridColor = Color.Gray;
    this._ListGridSubStr.GridLines = GridLines.Horizontal;
    this._ListGridSubStr.GridLineStyle = GridLineStyle.Dashed;
    this._ListGridSubStr.HeaderHeight = 18;
    this._ListGridSubStr.HeaderStyle = HeaderStyle.XP;
    this._ListGridSubStr.HotTrackingColor = Color.LightGray;
    this._ListGridSubStr.ImageList = (ImageList) null;
    this._ListGridSubStr.ItemHeight = 17;
    listItem.BackColor = Color.White;
    listItem.ForeColor = Color.Black;
    listItem.RowBorderColor = Color.Black;
    listSubItem1.BackColor = Color.Empty;
    listSubItem1.ForeColor = Color.Black;
    listSubItem1.Text = "начала обозначения";
    listSubItem2.BackColor = Color.Empty;
    listSubItem2.ForeColor = Color.Black;
    listSubItem2.Text = "количества символов = 12";
    listItem.SubItems.AddRange(new ListSubItem[2]
    {
      listSubItem1,
      listSubItem2
    });
    listItem.Text = "начала обозначения";
    this._ListGridSubStr.Items.AddRange(new Intermech.Controls.Grid.ListItem[1]
    {
      listItem
    });
    this._ListGridSubStr.Location = new Point(6, 26);
    this._ListGridSubStr.Name = "_ListGridSubStr";
    this._ListGridSubStr.SelectedTextColor = Color.White;
    this._ListGridSubStr.SelectionColor = Color.DarkBlue;
    this._ListGridSubStr.ShowFocusRect = true;
    this._ListGridSubStr.Size = new Size(584, 99);
    this._ListGridSubStr.SortType = SortType.None;
    this._ListGridSubStr.SuperFlatHeaderColor = Color.White;
    this._ListGridSubStr.TabIndex = 0;
    this._ReadModeToolTip.SetToolTip((Control) this._ListGridSubStr, "Список правил, по которым из обозначения вырезаются подстроки для дальнейшего сравнения с целью определения \"похожести\" обозначений");
    this._EditModeToolTip.SetToolTip((Control) this._ListGridSubStr, "Список правил, по которым из обозначения вырезаются подстроки для дальнейшего сравнения с целью определения \"похожести\" обозначений");
    this._ListGridSubStr.SelectedIndexChanged += new ListGrid.ClickedEventHandler(this._ListGridSubStr_SelectedIndexChanged);
    this._btnAddSubStr.Anchor = AnchorStyles.Top;
    this._btnAddSubStr.FlatStyle = FlatStyle.System;
    this._btnAddSubStr.Location = new Point(223, 135);
    this._btnAddSubStr.Name = "_btnAddSubStr";
    this._btnAddSubStr.Size = new Size(75, 23);
    this._btnAddSubStr.TabIndex = 1;
    this._btnAddSubStr.Text = "Добавить";
    this._EditModeToolTip.SetToolTip((Control) this._btnAddSubStr, "Добавить в список новое правило, по которому будет вырезаться подстрока для дальнейшего сравнения с целью определения \"похожести\" обозначений");
    this._btnAddSubStr.Click += new EventHandler(this._btnAddSubStr_Click);
    this._btnDelSubStr.Anchor = AnchorStyles.Top;
    this._btnDelSubStr.FlatStyle = FlatStyle.System;
    this._btnDelSubStr.Location = new Point(303, 135);
    this._btnDelSubStr.Name = "_btnDelSubStr";
    this._btnDelSubStr.Size = new Size(75, 23);
    this._btnDelSubStr.TabIndex = 2;
    this._btnDelSubStr.Text = "Удалить";
    this._EditModeToolTip.SetToolTip((Control) this._btnDelSubStr, "Удалить из списка выделеное правило для выделения подстроки из обозначения");
    this._btnDelSubStr.Click += new EventHandler(this._btnDelSubStr_Click);
    this._BtnReset.Enabled = false;
    this._BtnReset.FlatStyle = FlatStyle.System;
    this._BtnReset.Location = new Point(11, 307);
    this._BtnReset.Name = "_BtnReset";
    this._BtnReset.Size = new Size(81, 23);
    this._BtnReset.TabIndex = 5;
    this._BtnReset.Text = "По умолчанию";
    this._EditModeToolTip.SetToolTip((Control) this._BtnReset, "Вернуть список к значению по умолчанию");
    this._BtnReset.Visible = false;
    this._BtnReset.Click += new EventHandler(this._BtnReset_Click);
    this._buttonTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonTest.FlatStyle = FlatStyle.System;
    this._buttonTest.Location = new Point(334, 307);
    this._buttonTest.Name = "_buttonTest";
    this._buttonTest.Size = new Size(75, 23);
    this._buttonTest.TabIndex = 7;
    this._buttonTest.Text = "Тест";
    this._EditModeToolTip.SetToolTip((Control) this._buttonTest, "Протестировать правила определения \"похожести\" обозначений");
    this._ReadModeToolTip.SetToolTip((Control) this._buttonTest, "Протестировать правила определения \"похожести\" обозначений");
    this._buttonTest.Click += new EventHandler(this._buttonTest_Click);
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this._GroupBoxStartSubstr.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._GroupBoxStartSubstr.Controls.Add((Control) this._comboBoxSubstrStartSymbol);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._upDownSubstrStartNumber);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._label5);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._label4);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._label3);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._comboBoxSubstrStartFrom);
    this._GroupBoxStartSubstr.FlatStyle = FlatStyle.System;
    this._GroupBoxStartSubstr.Location = new Point(6, 190);
    this._GroupBoxStartSubstr.Name = "_GroupBoxStartSubstr";
    this._GroupBoxStartSubstr.Size = new Size(288, 97);
    this._GroupBoxStartSubstr.TabIndex = 3;
    this._GroupBoxStartSubstr.TabStop = false;
    this._GroupBoxStartSubstr.Text = "Начало подстроки";
    this._comboBoxSubstrStartSymbol.EditValue = (object) "";
    this._comboBoxSubstrStartSymbol.Location = new Point(86, 70);
    this._comboBoxSubstrStartSymbol.Name = "_comboBoxSubstrStartSymbol";
    this._comboBoxSubstrStartSymbol.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxSubstrStartSymbol.Properties.Items.AddRange(new object[5]
    {
      (object) ". (точка)",
      (object) "- (минус)",
      (object) "* (звездочка)",
      (object) ", (запятая)",
      (object) "(пробел)"
    });
    this._comboBoxSubstrStartSymbol.Properties.MaskData.EditMask = "C";
    this._comboBoxSubstrStartSymbol.Properties.MaskData.MaskType = MaskType.Simple;
    this._comboBoxSubstrStartSymbol.Properties.PopupSizeable = true;
    this._comboBoxSubstrStartSymbol.Properties.ReadOnly = true;
    this._comboBoxSubstrStartSymbol.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this._comboBoxSubstrStartSymbol.Size = new Size(196, 20);
    this._comboBoxSubstrStartSymbol.TabIndex = 3;
    this._comboBoxSubstrStartSymbol.ToolTip = "Cимвол, с которого должно начаться выделение подстроки из обозначения";
    this._comboBoxSubstrStartSymbol.EditValueChanged += new EventHandler(this._comboBoxSubstrStartSymbol_EditValueChanged);
    this._comboBoxSubstrStartSymbol.SelectedIndexChanged += new EventHandler(this._comboBoxSubstrStartSymbol_SelectedIndexChanged);
    this._comboBoxSubstrStartSymbol.EditValueChanging += new ChangingEventHandler(this._comboBoxSubstrStartSymbol_EditValueChanging);
    this._comboBoxSubstrStartSymbol.Leave += new EventHandler(this._comboBoxSubstrStartSymbol_Leave);
    this._upDownSubstrStartNumber.EditValue = (object) 1;
    this._upDownSubstrStartNumber.Location = new Point(86, 44);
    this._upDownSubstrStartNumber.Name = "_upDownSubstrStartNumber";
    this._upDownSubstrStartNumber.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownSubstrStartNumber.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownSubstrStartNumber.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownSubstrStartNumber.Properties.IsFloatValue = false;
    this._upDownSubstrStartNumber.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownSubstrStartNumber.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._upDownSubstrStartNumber.Properties.ReadOnly = true;
    this._upDownSubstrStartNumber.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this._upDownSubstrStartNumber.Properties.UseCtrlIncrement = false;
    this._upDownSubstrStartNumber.Properties.ValidateOnEnterKey = true;
    this._upDownSubstrStartNumber.Size = new Size(196, 20);
    this._upDownSubstrStartNumber.TabIndex = 1;
    this._upDownSubstrStartNumber.ToolTip = "Номер буквы или символа, с которого должно начаться выделение подстроки из обозначения";
    this._upDownSubstrStartNumber.EditValueChanged += new EventHandler(this._upDownSubstrStartNumber_EditValueChanged);
    this._upDownSubstrStartNumber.EditValueChanging += new ChangingEventHandler(this._upDownSubstrStartNumber_EditValueChanging);
    this._label5.FlatStyle = FlatStyle.System;
    this._label5.Location = new Point(8, 71);
    this._label5.Name = "_label5";
    this._label5.Size = new Size(69, 22);
    this._label5.TabIndex = 2;
    this._label5.Text = "Символ:";
    this._label4.FlatStyle = FlatStyle.System;
    this._label4.Location = new Point(8, 45);
    this._label4.Name = "_label4";
    this._label4.Size = new Size(69, 22);
    this._label4.TabIndex = 1;
    this._label4.Text = "Номер:";
    this._label3.FlatStyle = FlatStyle.System;
    this._label3.Location = new Point(8, 19);
    this._label3.Name = "_label3";
    this._label3.Size = new Size(69, 22);
    this._label3.TabIndex = 0;
    this._label3.Text = "От:";
    this._comboBoxSubstrStartFrom.EditValue = (object) 3;
    this._comboBoxSubstrStartFrom.Location = new Point(86, 17);
    this._comboBoxSubstrStartFrom.Name = "_comboBoxSubstrStartFrom";
    this._comboBoxSubstrStartFrom.Properties.AutoComplete = false;
    this._comboBoxSubstrStartFrom.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxSubstrStartFrom.Properties.Items.AddRange(new ImageComboBoxItem[4]
    {
      new ImageComboBoxItem("Начала обозначения", (object) 3, -1),
      new ImageComboBoxItem("Буквы номер", (object) 0, -1),
      new ImageComboBoxItem("Символа номер", (object) 1, -1),
      new ImageComboBoxItem("Символа номер (считая с конца обозначения)", (object) 2, -1)
    });
    this._comboBoxSubstrStartFrom.Properties.PopupSizeable = true;
    this._comboBoxSubstrStartFrom.Size = new Size(196, 20);
    this._comboBoxSubstrStartFrom.TabIndex = 0;
    this._comboBoxSubstrStartFrom.ToolTip = "Выбор начала подстроки в обозначении";
    this._comboBoxSubstrStartFrom.CloseUp += new CloseUpEventHandler(this._comboBoxSubstrStartFrom_CloseUp);
    this._comboBoxSubstrStartFrom.SelectedIndexChanged += new EventHandler(this._comboBoxSubstrStartFrom_SelectedIndexChanged);
    this._label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._label1.FlatStyle = FlatStyle.System;
    this._label1.Location = new Point(8, 8);
    this._label1.Name = "_label1";
    this._label1.Size = new Size(587, 14);
    this._label1.TabIndex = 5;
    this._label1.Text = "При сравнении обозначений берутся подстроки по следующим правилам:";
    this._GroupBoxFinishSubstr.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._GroupBoxFinishSubstr.Controls.Add((Control) this._comboBoxSubstrFinishSymbol);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this._upDownSubstrFinishNumber);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this.label2);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this.label6);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this.label7);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this._comboBoxSubstrFinishAt);
    this._GroupBoxFinishSubstr.FlatStyle = FlatStyle.System;
    this._GroupBoxFinishSubstr.Location = new Point(306, 190);
    this._GroupBoxFinishSubstr.Name = "_GroupBoxFinishSubstr";
    this._GroupBoxFinishSubstr.Size = new Size(288, 97);
    this._GroupBoxFinishSubstr.TabIndex = 4;
    this._GroupBoxFinishSubstr.TabStop = false;
    this._GroupBoxFinishSubstr.Text = "Окончание подстроки";
    this._comboBoxSubstrFinishSymbol.Location = new Point(86, 70);
    this._comboBoxSubstrFinishSymbol.Name = "_comboBoxSubstrFinishSymbol";
    this._comboBoxSubstrFinishSymbol.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxSubstrFinishSymbol.Properties.Items.AddRange(new object[5]
    {
      (object) ". (точка)",
      (object) "- (минус)",
      (object) "* (звездочка)",
      (object) ", (запятая)",
      (object) "(пробел)"
    });
    this._comboBoxSubstrFinishSymbol.Properties.MaskData.EditMask = "C";
    this._comboBoxSubstrFinishSymbol.Properties.MaskData.MaskType = MaskType.Simple;
    this._comboBoxSubstrFinishSymbol.Properties.PopupSizeable = true;
    this._comboBoxSubstrFinishSymbol.Properties.ReadOnly = true;
    this._comboBoxSubstrFinishSymbol.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this._comboBoxSubstrFinishSymbol.Size = new Size(196, 20);
    this._comboBoxSubstrFinishSymbol.TabIndex = 4;
    this._comboBoxSubstrFinishSymbol.ToolTip = "Выбор окончания подстроки из обозначения";
    this._comboBoxSubstrFinishSymbol.EditValueChanged += new EventHandler(this._comboBoxSubstrFinishSymbol_EditValueChanged);
    this._comboBoxSubstrFinishSymbol.SelectedIndexChanged += new EventHandler(this._comboBoxSubstrFinishSymbol_SelectedIndexChanged);
    this._comboBoxSubstrFinishSymbol.EditValueChanging += new ChangingEventHandler(this._comboBoxSubstrFinishSymbol_EditValueChanging);
    this._comboBoxSubstrFinishSymbol.Leave += new EventHandler(this._comboBoxSubstrFinishSymbol_Leave);
    this._upDownSubstrFinishNumber.EditValue = (object) 12;
    this._upDownSubstrFinishNumber.Location = new Point(86, 44);
    this._upDownSubstrFinishNumber.Name = "_upDownSubstrFinishNumber";
    this._upDownSubstrFinishNumber.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownSubstrFinishNumber.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownSubstrFinishNumber.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownSubstrFinishNumber.Properties.IsFloatValue = false;
    this._upDownSubstrFinishNumber.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownSubstrFinishNumber.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._upDownSubstrFinishNumber.Properties.UseCtrlIncrement = false;
    this._upDownSubstrFinishNumber.Properties.ValidateOnEnterKey = true;
    this._upDownSubstrFinishNumber.Size = new Size(196, 20);
    this._upDownSubstrFinishNumber.TabIndex = 1;
    this._upDownSubstrFinishNumber.ToolTip = "Номер буквы или символа, на котором должно закончиться выделение подстроки из обозначения";
    this._upDownSubstrFinishNumber.EditValueChanged += new EventHandler(this._upDownSubstrFinishNumber_EditValueChanged);
    this._upDownSubstrFinishNumber.EditValueChanging += new ChangingEventHandler(this._upDownSubstrFinishNumber_EditValueChanging);
    this.label2.FlatStyle = FlatStyle.System;
    this.label2.Location = new Point(8, 71);
    this.label2.Name = "label2";
    this.label2.Size = new Size(69, 22);
    this.label2.TabIndex = 2;
    this.label2.Text = "Символ:";
    this.label6.FlatStyle = FlatStyle.System;
    this.label6.Location = new Point(8, 45);
    this.label6.Name = "label6";
    this.label6.Size = new Size(69, 22);
    this.label6.TabIndex = 1;
    this.label6.Text = "Номер:";
    this.label7.FlatStyle = FlatStyle.System;
    this.label7.Location = new Point(8, 19);
    this.label7.Name = "label7";
    this.label7.Size = new Size(69, 22);
    this.label7.TabIndex = 0;
    this.label7.Text = "До:";
    this._comboBoxSubstrFinishAt.EditValue = (object) 1;
    this._comboBoxSubstrFinishAt.Location = new Point(86, 17);
    this._comboBoxSubstrFinishAt.Name = "_comboBoxSubstrFinishAt";
    this._comboBoxSubstrFinishAt.Properties.AutoComplete = false;
    this._comboBoxSubstrFinishAt.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxSubstrFinishAt.Properties.Items.AddRange(new ImageComboBoxItem[4]
    {
      new ImageComboBoxItem("Конца обозначения", (object) 0, -1),
      new ImageComboBoxItem("Количества символов", (object) 1, -1),
      new ImageComboBoxItem("Символ номер", (object) 3, -1),
      new ImageComboBoxItem("Символ номер (считая с конца обозначения)", (object) 2, -1)
    });
    this._comboBoxSubstrFinishAt.Properties.PopupSizeable = true;
    this._comboBoxSubstrFinishAt.Size = new Size(196, 20);
    this._comboBoxSubstrFinishAt.TabIndex = 0;
    this._comboBoxSubstrFinishAt.ToolTip = "Где должна заканчиваться выделяемая из обозначения подстрока";
    this._comboBoxSubstrFinishAt.CloseUp += new CloseUpEventHandler(this._groupBoxSubstrFinishAt_CloseUp);
    this._comboBoxSubstrFinishAt.SelectedIndexChanged += new EventHandler(this._groupBoxSubstrFinishAt_SelectedIndexChanged);
    this._comboBoxListSource.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._comboBoxListSource.EditValue = (object) false;
    this._comboBoxListSource.Location = new Point(100, 308);
    this._comboBoxListSource.Name = "_comboBoxListSource";
    this._comboBoxListSource.Properties.AutoComplete = false;
    this._comboBoxListSource.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxListSource.Properties.Items.AddRange(new ImageComboBoxItem[2]
    {
      new ImageComboBoxItem("Унаследован", (object) false, -1),
      new ImageComboBoxItem("Собственный", (object) true, -1)
    });
    this._comboBoxListSource.Size = new Size(143, 20);
    this._comboBoxListSource.TabIndex = 6;
    this._comboBoxListSource.ToolTip = "Выбор, откуда брать список";
    this._comboBoxListSource.SelectedIndexChanged += new EventHandler(this._comboBoxListSource_SelectedIndexChanged);
    this.label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label3.Location = new Point(32 /*0x20*/, 311);
    this.label3.Name = "label3";
    this.label3.Size = new Size(62, 13);
    this.label3.TabIndex = 17;
    this.label3.Text = "Список:";
    this.label3.TextAlign = ContentAlignment.MiddleRight;
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(601, 341);
    this.Controls.Add((Control) this._buttonTest);
    this.Controls.Add((Control) this._comboBoxListSource);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this._btnDelSubStr);
    this.Controls.Add((Control) this._btnAddSubStr);
    this.Controls.Add((Control) this._GroupBoxFinishSubstr);
    this.Controls.Add((Control) this._ListGridSubStr);
    this.Controls.Add((Control) this._GroupBoxStartSubstr);
    this.Controls.Add((Control) this._label1);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.Controls.Add((Control) this._BtnReset);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SameDesignationsSetupForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Настройка правил сравнения обозначений";
    this.Load += new EventHandler(this.SameDesignationsSetupForm_Load);
    this.Closed += new EventHandler(this.SameDesignationsSetupForm_Closed);
    this.Closing += new CancelEventHandler(this.SameDesignationsSetupForm_Closing);
    this._GroupBoxStartSubstr.ResumeLayout(false);
    this._comboBoxSubstrStartSymbol.Properties.EndInit();
    this._upDownSubstrStartNumber.Properties.EndInit();
    this._comboBoxSubstrStartFrom.Properties.EndInit();
    this._GroupBoxFinishSubstr.ResumeLayout(false);
    this._comboBoxSubstrFinishSymbol.Properties.EndInit();
    this._upDownSubstrFinishNumber.Properties.EndInit();
    this._comboBoxSubstrFinishAt.Properties.EndInit();
    this._comboBoxListSource.Properties.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Схема выдирания подстрок из обозначения для их дальнейшего сравнения </summary>
  public CompareDesignationSchema CompareDesignationSchema
  {
    get => this._compareDesignationSchema;
    set
    {
      this.LockControls();
      try
      {
        this._compareDesignationSchema = value;
        this.ReloadVisualList();
        this.RefreshReadOnly();
        this.UpdateControls(true);
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  /// <summary> Сфокусированое правило выдёргивания подстроки. Присвоение значения обновляет панель редактирования </summary>
  private CompareDesignationSubStr _focusedCompareDesignationSubStr
  {
    get
    {
      return this._ListGridSubStr.FocusedItem != null ? (CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag : (CompareDesignationSubStr) null;
    }
    set => this.UpdateControls(false);
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      Size size = this.Size;
      int width1 = size.Width;
      int x = this._BtnCancel.Location.X;
      size = this._BtnCancel.Size;
      int width2 = size.Width;
      int num = x + width2;
      return width1 - num;
    }
  }

  /// <summary> Обработчик события "данные были обновленны" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void OnInitData(object sender, InitDataEventArgs e)
  {
    if (e.Tag == null)
      this.Close();
    this.LockControls();
    try
    {
      Guid guid = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      this._oldCompareDesignationSchema = (CompareDesignationSchema) null;
      if (e.Tag is SpecifNumberingFull)
      {
        this._oldCompareDesignationSchema = ((SpecifNumberingFull) e.Tag).CompareDesignationSchema;
      }
      else
      {
        if (!(e.Tag is SkipLinesSchema))
          return;
        this._oldCompareDesignationSchema = ((SkipLinesSchema) e.Tag).CompareDesignationSchema;
      }
      this.CompareDesignationSchema = this._oldCompareDesignationSchema.Clone();
      if (guid != Guid.Empty)
      {
        foreach (Intermech.Controls.Grid.ListItem listItem in (CollectionBase) this._ListGridSubStr.Items)
        {
          if (listItem.Tag != null && ((CompareDesignationSubStr) listItem.Tag).StrGuid == guid)
          {
            this._ListGridSubStr.FocusedItem = listItem;
            if (!listItem.Selected)
            {
              listItem.Selected = true;
              break;
            }
            break;
          }
        }
      }
      this.RefreshReadOnly();
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    this._BtnCancel.Text = this.ReadOnly ? "Закрыть" : "Отмена";
    this._BtnOK.Enabled = !this.ReadOnly;
    if (this._EditModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this._EditModeToolTip.Active)
        {
          this._EditModeToolTip.Active = false;
          this._ReadModeToolTip.Active = true;
        }
      }
      else if (this._ReadModeToolTip.Active)
      {
        this._ReadModeToolTip.Active = false;
        this._EditModeToolTip.Active = true;
      }
    }
    this._comboBoxListSource.Visible = this._compareDesignationSchema != null && this._compareDesignationSchema.SpecifNumberingFull != null && this._compareDesignationSchema.SpecifNumberingFull.ParentLevel != null;
    if (this._comboBoxListSource.Visible)
    {
      this._comboBoxListSource.Properties.ReadOnly = this.ReadOnly;
      this._comboBoxListSource.BackColor = this._comboBoxListSource.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
      this._comboBoxListSource.Properties.Buttons[0].Visible = !this._comboBoxListSource.Properties.ReadOnly;
      this._comboBoxListSource.SelectedIndex = this._compareDesignationSchema.Changed ? 1 : 0;
    }
    else
      this._comboBoxListSource.SelectedIndex = 1;
    this.label3.Visible = this._comboBoxListSource.Visible;
    this._ListGridSubStr.BackColor = this._comboBoxListSource.SelectedIndex != 1 || this.ReadOnly ? Color.WhiteSmoke : SystemColors.Window;
    this._BtnReset.Visible = !this._comboBoxListSource.Visible;
    this._BtnReset.Enabled = !this.ReadOnly && this._BtnReset.Visible;
    this._btnAddSubStr.Enabled = !this.ReadOnly && this._comboBoxListSource.SelectedIndex == 1;
    this._btnDelSubStr.Enabled = !this.ReadOnly && this._ListGridSubStr.FocusedItem != null && this._ListGridSubStr.Items.Count > 1 && this._comboBoxListSource.SelectedIndex == 1;
    CompareDesignationSubStr designationSubStr = this._focusedCompareDesignationSubStr;
    this._comboBoxSubstrStartFrom.Properties.ReadOnly = this.ReadOnly || designationSubStr == null || this._comboBoxListSource.SelectedIndex != 1;
    this._upDownSubstrStartNumber.Properties.ReadOnly = this.ReadOnly || designationSubStr == null || designationSubStr.StartFindWhat == CompareDesignationSubStr.FindWhat.Unknow || designationSubStr.StartFindWhat == CompareDesignationSubStr.FindWhat.StartEndString || this._comboBoxListSource.SelectedIndex != 1;
    this._comboBoxSubstrStartSymbol.Properties.ReadOnly = this.ReadOnly || designationSubStr == null || designationSubStr.StartFindWhat != CompareDesignationSubStr.FindWhat.SymbolNumber && designationSubStr.StartFindWhat != CompareDesignationSubStr.FindWhat.SymbolNumberFromEnd || this._comboBoxListSource.SelectedIndex != 1;
    this._comboBoxSubstrFinishAt.Properties.ReadOnly = this.ReadOnly || designationSubStr == null || this._comboBoxListSource.SelectedIndex != 1;
    this._upDownSubstrFinishNumber.Properties.ReadOnly = this.ReadOnly || designationSubStr == null || designationSubStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.Unknow || designationSubStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.StartEndString || this._comboBoxListSource.SelectedIndex != 1;
    this._comboBoxSubstrFinishSymbol.Properties.ReadOnly = this.ReadOnly || designationSubStr == null || designationSubStr.FinishFindWhat != CompareDesignationSubStr.FindWhat.SymbolNumber && designationSubStr.FinishFindWhat != CompareDesignationSubStr.FindWhat.SymbolNumberFromEnd || this._comboBoxListSource.SelectedIndex != 1;
    if (designationSubStr != null)
    {
      if (this._changedControl != this._comboBoxSubstrStartFrom)
      {
        if (designationSubStr.StartFindWhat == CompareDesignationSubStr.FindWhat.Unknow)
          this._comboBoxSubstrStartFrom.Text = "???";
        else
          this._comboBoxSubstrStartFrom.SelectedIndex = (int) (designationSubStr.StartFindWhat - 2);
      }
      if (this._changedControl != this._upDownSubstrStartNumber)
      {
        if (designationSubStr.StartFindWhat == CompareDesignationSubStr.FindWhat.Unknow || designationSubStr.StartFindWhat == CompareDesignationSubStr.FindWhat.StartEndString)
          this._upDownSubstrStartNumber.Text = "";
        else
          this._upDownSubstrStartNumber.Value = (Decimal) designationSubStr.StartNumber;
      }
      if (this._changedControl != this._comboBoxSubstrStartSymbol)
      {
        if (designationSubStr.StartFindWhat == CompareDesignationSubStr.FindWhat.SymbolNumber || designationSubStr.StartFindWhat == CompareDesignationSubStr.FindWhat.SymbolNumberFromEnd)
        {
          int symbolElementIndex = SameDesignationsSetupForm.GetComboBoxSymbolElementIndex(designationSubStr.StartSymbol);
          if (symbolElementIndex != -1)
            this._comboBoxSubstrStartSymbol.SelectedIndex = symbolElementIndex;
          else
            this._comboBoxSubstrStartSymbol.EditValue = (object) designationSubStr.StartSymbol.ToString();
        }
        else
          this._comboBoxSubstrStartSymbol.EditValue = (object) "";
      }
      if (this._changedControl != this._comboBoxSubstrFinishAt)
      {
        if (designationSubStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.Unknow)
          this._comboBoxSubstrFinishAt.Text = "???";
        else
          this._comboBoxSubstrFinishAt.SelectedIndex = (int) (designationSubStr.FinishFindWhat - 2);
      }
      if (this._changedControl != this._upDownSubstrFinishNumber)
      {
        if (designationSubStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.Unknow || designationSubStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.StartEndString)
          this._upDownSubstrFinishNumber.Text = "";
        else
          this._upDownSubstrFinishNumber.Value = (Decimal) designationSubStr.FinishNumber;
      }
      if (this._changedControl != this._comboBoxSubstrFinishSymbol)
      {
        if (designationSubStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.SymbolNumber || designationSubStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.SymbolNumberFromEnd)
        {
          int symbolElementIndex = SameDesignationsSetupForm.GetComboBoxSymbolElementIndex(designationSubStr.FinishSymbol);
          if (symbolElementIndex != -1)
            this._comboBoxSubstrFinishSymbol.SelectedIndex = symbolElementIndex;
          else
            this._comboBoxSubstrFinishSymbol.EditValue = (object) designationSubStr.FinishSymbol.ToString();
        }
        else
          this._comboBoxSubstrFinishSymbol.EditValue = (object) "";
      }
    }
    else
    {
      this._comboBoxSubstrStartFrom.Text = "";
      this._upDownSubstrStartNumber.Text = "";
      this._comboBoxSubstrStartSymbol.EditValue = (object) "";
      this._comboBoxSubstrFinishAt.Text = "";
      this._upDownSubstrFinishNumber.Text = "";
      this._comboBoxSubstrFinishSymbol.EditValue = (object) "";
    }
    this._comboBoxSubstrStartFrom.BackColor = this._comboBoxSubstrStartFrom.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownSubstrStartNumber.BackColor = this._upDownSubstrStartNumber.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxSubstrStartSymbol.BackColor = this._comboBoxSubstrStartSymbol.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxSubstrFinishAt.BackColor = this._comboBoxSubstrFinishAt.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownSubstrFinishNumber.BackColor = this._upDownSubstrFinishNumber.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxSubstrFinishSymbol.BackColor = this._comboBoxSubstrFinishSymbol.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxSubstrStartFrom.Properties.Buttons[0].Visible = !this._comboBoxSubstrStartFrom.Properties.ReadOnly;
    this._upDownSubstrStartNumber.Properties.Buttons[0].Visible = !this._upDownSubstrStartNumber.Properties.ReadOnly;
    this._comboBoxSubstrStartSymbol.Properties.Buttons[0].Visible = !this._comboBoxSubstrStartSymbol.Properties.ReadOnly;
    this._comboBoxSubstrFinishAt.Properties.Buttons[0].Visible = !this._comboBoxSubstrFinishAt.Properties.ReadOnly;
    this._upDownSubstrFinishNumber.Properties.Buttons[0].Visible = !this._upDownSubstrFinishNumber.Properties.ReadOnly;
    this._comboBoxSubstrFinishSymbol.Properties.Buttons[0].Visible = !this._comboBoxSubstrFinishSymbol.Properties.ReadOnly;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected override bool GetIsReadOnly()
  {
    return this._compareDesignationSchema != null && this._compareDesignationSchema.ReadOnly;
  }

  /// <summary> Обновление отображения списка правил выдёргивания подстрок из обозначения </summary>
  private void ReloadVisualList()
  {
    this._ListGridSubStr.BeginUpdate();
    try
    {
      this._ListGridSubStr.Items.Clear();
      if (this._compareDesignationSchema != null)
      {
        foreach (CompareDesignationSubStr subStr in this._compareDesignationSchema.SubStrs)
        {
          Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem();
          ListSubItem listSubItem1 = new ListSubItem();
          ListSubItem listSubItem2 = new ListSubItem();
          listItem.SubItems.AddRange(new ListSubItem[2]
          {
            listSubItem1,
            listSubItem2
          });
          this.RefreshListItem(listItem, subStr);
          this._ListGridSubStr.Items.Add(listItem);
        }
      }
      this.CheckAnySelected();
    }
    finally
    {
      this._ListGridSubStr.EndUpdate();
    }
  }

  /// <summary> Обновление сфокусированого визуального отображения правила выдирания подстроки в списке всех правил (в ListGrid-е) </summary>
  private void RefreshFocusedListItem() => this.RefreshListItem(this._ListGridSubStr.FocusedItem);

  /// <summary> Обновление визуального отображения правила выдирания подстроки в списке всех правил (в ListGrid-е) </summary>
  /// <param name="listItem"> ListItem - визуальное отображение </param>
  private void RefreshListItem(Intermech.Controls.Grid.ListItem listItem)
  {
    if (listItem == null)
      return;
    this.RefreshListItem(listItem, (CompareDesignationSubStr) listItem.Tag);
  }

  /// <summary> Обновление визуального отображения правила выдирания подстроки в списке всех правил (в ListGrid-е) </summary>
  /// <param name="listItem"> ListItem - визуальное отображение </param>
  /// <param name="compareDesignationSubStr"> правило выдёргивания подстроки из обозначения </param>
  private void RefreshListItem(Intermech.Controls.Grid.ListItem listItem, CompareDesignationSubStr compareDesignationSubStr)
  {
    if (listItem == null || compareDesignationSubStr == null)
      return;
    listItem.SubItems[0].Text = compareDesignationSubStr.StartAsText;
    listItem.SubItems[1].Text = compareDesignationSubStr.FinishAsText;
    listItem.Text = listItem.SubItems[0].Text;
    listItem.Tag = (object) compareDesignationSubStr;
  }

  /// <summary> Контроль того факта, что хотя бы одно правило выдирания подстроки выбрано </summary>
  private void CheckAnySelected()
  {
    if (this._ListGridSubStr.Items.Count <= 0)
      return;
    if (this._ListGridSubStr.FocusedItem == null)
      this._ListGridSubStr.FocusedItem = this._ListGridSubStr.Items[0];
    if (this._ListGridSubStr.FocusedItem.Selected)
      return;
    this._ListGridSubStr.FocusedItem.Selected = true;
  }

  /// <summary> Определение индекса текстого представления символа в ComboBox-е </summary>
  /// <param name="character"> Символ </param>
  /// <returns> индекс </returns>
  private static int GetComboBoxSymbolElementIndex(char character)
  {
    return Array.IndexOf<char>(SameDesignationsSetupForm._predefinedSymbols, character);
  }

  /// <summary> Преобразует строку в символ </summary>
  /// <param name="str"> строка </param>
  /// <returns> символ </returns>
  private static char StringToChar(string str)
  {
    if (str == "(пробел)")
      return char.Parse(" ");
    char[] charArray = str.ToCharArray();
    return charArray.Length == 0 ? char.Parse(" ") : charArray[0];
  }

  /// <summary> Сохранить изменения </summary>
  protected virtual void SaveChanges()
  {
    this._oldCompareDesignationSchema.CopyParamsFrom(this._compareDesignationSchema);
    this._oldCompareDesignationSchema.Changed = this._compareDesignationSchema.Changed;
  }

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SameDesignationsSetupForm_Load(object sender, EventArgs e)
  {
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SameDesignationsSetupForm_Closed(object sender, EventArgs e)
  {
    if (this.ReadOnly || this.DialogResult != DialogResult.OK || !this.Changed)
      return;
    this.SaveChanges();
  }

  /// <summary> Было выбрано другое правило выдирания подстроки </summary>
  /// <param name="source"></param>
  /// <param name="e"></param>
  private void _ListGridSubStr_SelectedIndexChanged(object source, ClickEventArgs e)
  {
    this.UpdateControls(false);
  }

  /// <summary> Был закрыт список выбора вариантов поиска начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartFrom_CloseUp(object sender, CloseUpEventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
    {
      e.AcceptValue = true;
    }
    else
    {
      Guid guid = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      int selectedIndex = this._comboBoxSubstrStartFrom.SelectedIndex;
      if (this._compareDesignationSchema != null && this._focusedCompareDesignationSubStr != null && !this.ControlsAreUpdating && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated))
      {
        if (wasUpdated)
        {
          if (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || selectedIndex != this._comboBoxSubstrStartFrom.SelectedIndex || this._comboBoxSubstrStartFrom.Properties.ReadOnly)
          {
            e.AcceptValue = false;
            return;
          }
          if ((this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid) != guid)
          {
            e.AcceptValue = false;
            return;
          }
        }
        e.AcceptValue = true;
      }
      else
        e.AcceptValue = false;
    }
  }

  /// <summary> Было изменено правило выбора начала подстроки выдираемой из обозначения </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartFrom_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null)
      return;
    this._changedControl = (Control) this._comboBoxSubstrStartFrom;
    try
    {
      this._focusedCompareDesignationSubStr.StartFindWhat = (CompareDesignationSubStr.FindWhat) (this._comboBoxSubstrStartFrom.SelectedIndex + 2);
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedListItem();
  }

  /// <summary> Был закрыт список выбора вариантов поиска окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _groupBoxSubstrFinishAt_CloseUp(object sender, CloseUpEventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
    {
      e.AcceptValue = true;
    }
    else
    {
      Guid guid = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      int selectedIndex = this._comboBoxSubstrFinishAt.SelectedIndex;
      if (this._compareDesignationSchema != null && this._focusedCompareDesignationSubStr != null && !this.ControlsAreUpdating && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated))
      {
        if (wasUpdated)
        {
          if (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || selectedIndex != this._comboBoxSubstrFinishAt.SelectedIndex || this._comboBoxSubstrFinishAt.Properties.ReadOnly)
          {
            e.AcceptValue = false;
            return;
          }
          if ((this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid) != guid)
          {
            e.AcceptValue = false;
            return;
          }
        }
        e.AcceptValue = true;
      }
      else
        e.AcceptValue = false;
    }
  }

  /// <summary> Было изменено правило выбора окончания подстроки выдираемой из обозначения </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _groupBoxSubstrFinishAt_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null)
      return;
    this._changedControl = (Control) this._comboBoxSubstrFinishAt;
    try
    {
      this._focusedCompareDesignationSubStr.FinishFindWhat = (CompareDesignationSubStr.FindWhat) (this._comboBoxSubstrFinishAt.SelectedIndex + 2);
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedListItem();
  }

  /// <summary> Вызывается при попытке изменения номера буквы/символа, который должен быть найден для определения начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _upDownSubstrStartNumber_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
    {
      e.Cancel = false;
    }
    else
    {
      Guid guid = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
      if (this._compareDesignationSchema != null && this._focusedCompareDesignationSubStr != null && !this.ControlsAreUpdating && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated))
      {
        if (wasUpdated)
        {
          if (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || oldValue != Decimal.ToInt32(this._upDownSubstrStartNumber.Value) || this._upDownSubstrStartNumber.Properties.ReadOnly)
          {
            e.Cancel = true;
            return;
          }
          if ((this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid) != guid)
          {
            e.Cancel = true;
            return;
          }
        }
        e.Cancel = false;
      }
      else
        e.Cancel = true;
    }
  }

  /// <summary> Вызывается после изменения номера буквы/символа, который должен быть найден для определения начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _upDownSubstrStartNumber_EditValueChanged(object sender, EventArgs e)
  {
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null)
      return;
    this._changedControl = (Control) this._upDownSubstrStartNumber;
    try
    {
      this._focusedCompareDesignationSubStr.StartNumber = (int) this._upDownSubstrStartNumber.Value;
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedListItem();
  }

  /// <summary> Вызывается при попытке изменения номера буквы/символа, который должен быть найден для определения окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _upDownSubstrFinishNumber_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
    {
      e.Cancel = false;
    }
    else
    {
      Guid guid = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
      if (this._compareDesignationSchema != null && this._focusedCompareDesignationSubStr != null && !this.ControlsAreUpdating && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated))
      {
        if (wasUpdated)
        {
          if (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || oldValue != Decimal.ToInt32(this._upDownSubstrFinishNumber.Value) || this._upDownSubstrFinishNumber.Properties.ReadOnly)
          {
            e.Cancel = true;
            return;
          }
          if ((this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid) != guid)
          {
            e.Cancel = true;
            return;
          }
        }
        e.Cancel = false;
      }
      else
        e.Cancel = true;
    }
  }

  /// <summary> Вызывается после изменения номера буквы/символа, который должен быть найден для определения окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _upDownSubstrFinishNumber_EditValueChanged(object sender, EventArgs e)
  {
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null)
      return;
    this._changedControl = (Control) this._upDownSubstrFinishNumber;
    try
    {
      this._focusedCompareDesignationSubStr.FinishNumber = (int) this._upDownSubstrFinishNumber.Value;
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedListItem();
  }

  /// <summary> Вызывается при попытке изменения символа, который должен быть найден для определения начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartSymbol_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
    {
      e.Cancel = false;
    }
    else
    {
      Guid guid = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      char ch = e.OldValue == null || e.OldValue.GetType() != typeof (string) ? char.MinValue : SameDesignationsSetupForm.StringToChar((string) e.OldValue);
      if (this._compareDesignationSchema != null && this._focusedCompareDesignationSubStr != null && !this.ControlsAreUpdating && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated))
      {
        if (wasUpdated)
        {
          if (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || (int) ch != (int) SameDesignationsSetupForm.StringToChar(this._comboBoxSubstrStartSymbol.Text) || this._comboBoxSubstrStartSymbol.Properties.ReadOnly)
          {
            e.Cancel = true;
            return;
          }
          if ((this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid) != guid)
          {
            e.Cancel = true;
            return;
          }
        }
        e.Cancel = false;
      }
      else
        e.Cancel = true;
    }
  }

  /// <summary> Вызывается после изменения символа, который должен быть найден для определения начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartSymbol_EditValueChanged(object sender, EventArgs e)
  {
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null)
      return;
    this._changedControl = (Control) this._comboBoxSubstrStartSymbol;
    try
    {
      this._focusedCompareDesignationSubStr.StartSymbol = SameDesignationsSetupForm.StringToChar(this._comboBoxSubstrStartSymbol.Text);
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedListItem();
  }

  /// <summary> Вызывается после изменения с помощью выпадающего списка символа, который должен быть найден для определения начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartSymbol_SelectedIndexChanged(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return;
    Guid guid1 = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null || this._comboBoxListSource.SelectedIndex != 1 || !this.CheckCanEdit(ref wasUpdated))
      return;
    char startSymbol = this._focusedCompareDesignationSubStr.StartSymbol;
    if (wasUpdated)
    {
      Guid guid2 = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      if (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || (int) startSymbol != (int) this._focusedCompareDesignationSubStr.StartSymbol || guid2 != guid1 || this._comboBoxSubstrStartSymbol.Properties.ReadOnly)
        return;
    }
    this._changedControl = (Control) this._comboBoxSubstrStartSymbol;
    try
    {
      if (this._comboBoxSubstrStartSymbol.SelectedIndex != -1)
        this._focusedCompareDesignationSubStr.StartSymbol = SameDesignationsSetupForm._predefinedSymbols[this._comboBoxSubstrStartSymbol.SelectedIndex];
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedListItem();
  }

  /// <summary> Вызывается после потери фокуса контролом, в котором выбирается символ, который должен быть найден для определения начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartSymbol_Leave(object sender, EventArgs e)
  {
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null)
      return;
    int symbolElementIndex = SameDesignationsSetupForm.GetComboBoxSymbolElementIndex(SameDesignationsSetupForm.StringToChar(this._comboBoxSubstrStartSymbol.Text));
    if (symbolElementIndex == -1)
      return;
    this._comboBoxSubstrStartSymbol.SelectedIndex = symbolElementIndex;
  }

  /// <summary> Вызывается при попытке изменения символа, который должен быть найден для определения окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishSymbol_EditValueChanging(object sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
    {
      e.Cancel = false;
    }
    else
    {
      Guid guid = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      char ch = e.OldValue == null || e.OldValue.GetType() != typeof (string) ? char.MinValue : SameDesignationsSetupForm.StringToChar((string) e.OldValue);
      if (this._compareDesignationSchema != null && this._focusedCompareDesignationSubStr != null && !this.ControlsAreUpdating && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated))
      {
        if (wasUpdated)
        {
          if (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || (int) ch != (int) SameDesignationsSetupForm.StringToChar(this._comboBoxSubstrFinishSymbol.Text) || this._comboBoxSubstrFinishSymbol.Properties.ReadOnly)
          {
            e.Cancel = true;
            return;
          }
          if ((this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid) != guid)
          {
            e.Cancel = true;
            return;
          }
        }
        e.Cancel = false;
      }
      else
        e.Cancel = true;
    }
  }

  /// <summary> Вызывается после изменения символа, который должен быть найден для определения окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishSymbol_EditValueChanged(object sender, EventArgs e)
  {
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null)
      return;
    this._changedControl = (Control) this._comboBoxSubstrFinishSymbol;
    try
    {
      this._focusedCompareDesignationSubStr.FinishSymbol = SameDesignationsSetupForm.StringToChar(this._comboBoxSubstrFinishSymbol.Text);
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedListItem();
  }

  /// <summary> Вызывается после изменения с помощью выпадающего списка символа, который должен быть найден для определения окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishSymbol_SelectedIndexChanged(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return;
    Guid guid1 = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null || this._comboBoxListSource.SelectedIndex != 1 || !this.CheckCanEdit(ref wasUpdated))
      return;
    char finishSymbol = this._focusedCompareDesignationSubStr.FinishSymbol;
    if (wasUpdated)
    {
      Guid guid2 = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
      if (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || (int) finishSymbol != (int) this._focusedCompareDesignationSubStr.FinishSymbol || guid2 != guid1 || this._comboBoxSubstrFinishSymbol.Properties.ReadOnly)
        return;
    }
    this._changedControl = (Control) this._comboBoxSubstrFinishSymbol;
    try
    {
      if (this._comboBoxSubstrFinishSymbol.SelectedIndex != -1)
        this._focusedCompareDesignationSubStr.FinishSymbol = SameDesignationsSetupForm._predefinedSymbols[this._comboBoxSubstrFinishSymbol.SelectedIndex];
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedListItem();
  }

  /// <summary> Вызывается после потери фокуса контролом, в котором выбирается символ, который должен быть найден для определения окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishSymbol_Leave(object sender, EventArgs e)
  {
    int symbolElementIndex = SameDesignationsSetupForm.GetComboBoxSymbolElementIndex(SameDesignationsSetupForm.StringToChar(this._comboBoxSubstrFinishSymbol.Text));
    if (symbolElementIndex == -1)
      return;
    this._comboBoxSubstrFinishSymbol.SelectedIndex = symbolElementIndex;
  }

  /// <summary> Была нажата кнопка "Добавить" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnAddSubStr_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating || this._compareDesignationSchema == null || this.ControlsAreUpdating || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || !this._btnAddSubStr.Enabled))
      return;
    this.LockControls();
    this._ListGridSubStr.BeginUpdate();
    try
    {
      Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem();
      ListSubItem listSubItem1 = new ListSubItem();
      ListSubItem listSubItem2 = new ListSubItem();
      listItem.SubItems.AddRange(new ListSubItem[2]
      {
        listSubItem1,
        listSubItem2
      });
      this.RefreshListItem(listItem, this._compareDesignationSchema.AddEmptyStr());
      this._ListGridSubStr.Items.Add(listItem);
      this._ListGridSubStr.FocusedItem = listItem;
      if (!listItem.Selected)
        listItem.Selected = true;
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._ListGridSubStr.EndUpdate();
      this.UnlockControls();
    }
  }

  /// <summary> Была нажата кнопка "Удалить" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnDelSubStr_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return;
    Guid guid = this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid;
    if (this._compareDesignationSchema == null || this.ControlsAreUpdating || this._focusedCompareDesignationSubStr == null || !this.CheckCanEdit(ref wasUpdated) || this._compareDesignationSchema.SubStrs.Length <= 1 || wasUpdated && (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || !this._btnDelSubStr.Enabled || (this._ListGridSubStr.FocusedItem == null || this._ListGridSubStr.FocusedItem.Tag == null ? Guid.Empty : ((CompareDesignationSubStr) this._ListGridSubStr.FocusedItem.Tag).StrGuid) != guid) || MessageBox.Show("Удалить выбранное правило определения подстроки?", "Удаление правила определения подстроки", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockControls();
    this._ListGridSubStr.BeginUpdate();
    try
    {
      this._compareDesignationSchema.Remove(this._focusedCompareDesignationSubStr);
      int itemIndex = this._ListGridSubStr.Items.FindItemIndex(this._ListGridSubStr.FocusedItem);
      this._ListGridSubStr.Items.Remove(this._ListGridSubStr.FocusedItem);
      if (itemIndex >= this._ListGridSubStr.Items.Count && itemIndex > 0)
        this._ListGridSubStr.FocusedItem = this._ListGridSubStr.Items[this._ListGridSubStr.Items.Count - 1];
      if (itemIndex < this._ListGridSubStr.Items.Count && itemIndex > 0)
        this._ListGridSubStr.FocusedItem = this._ListGridSubStr.Items[itemIndex];
      if (this._ListGridSubStr.FocusedItem != null && !this._ListGridSubStr.FocusedItem.Selected)
        this._ListGridSubStr.FocusedItem.Selected = true;
      this.UpdateControls(false);
      this._compareDesignationSchema.Changed = true;
      this.Changed = true;
    }
    finally
    {
      this._ListGridSubStr.EndUpdate();
      this.UnlockControls();
    }
  }

  /// <summary> Вызывается при попытке закрыть форму </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SameDesignationsSetupForm_Closing(object sender, CancelEventArgs e)
  {
    if (this.ReadOnly || this.DialogResult != DialogResult.OK || !this.Changed)
      return;
    bool flag = false;
    foreach (CompareDesignationSubStr subStr in this._compareDesignationSchema.SubStrs)
    {
      if (subStr.StartFindWhat == CompareDesignationSubStr.FindWhat.Unknow || subStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.Unknow)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    if (MessageBox.Show("Не все правила определения подстрок были определены до конца, не определенные до конца правила будут удалены. Продолжить?", "Настройки сравнения обозначений", MessageBoxButtons.OKCancel) != DialogResult.OK)
    {
      e.Cancel = true;
    }
    else
    {
      foreach (CompareDesignationSubStr subStr in this._compareDesignationSchema.SubStrs)
      {
        if (subStr.StartFindWhat == CompareDesignationSubStr.FindWhat.Unknow || subStr.FinishFindWhat == CompareDesignationSubStr.FindWhat.Unknow)
          this._compareDesignationSchema.Remove(subStr);
      }
    }
  }

  /// <summary> Был изменён источник данных </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxListSource_SelectedIndexChanged(object sender, EventArgs e)
  {
    int selectedIndex = this._comboBoxListSource.SelectedIndex;
    bool wasUpdated = false;
    if (this.ControlsAreUpdating || this.ControlsAreUpdating || this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this.ReadOnly)
      return;
    if ((selectedIndex == 1 || !this._compareDesignationSchema.Changed ? 6 : (int) MessageBox.Show("Сбросить изменения в настройках сравнения обозначений?", "Настройки сравнения обозначений", MessageBoxButtons.YesNo)) == 6)
    {
      this._compareDesignationSchema.Changed = selectedIndex == 1;
      this.Changed = true;
      if (!this._compareDesignationSchema.Changed)
      {
        this.LockControls();
        try
        {
          this._compareDesignationSchema.LoadDefaultSchema();
          this.CompareDesignationSchema = this._compareDesignationSchema;
          this.UpdateControls(true);
          this.Changed = true;
        }
        finally
        {
          this.UnlockControls();
        }
      }
      else
        this.UpdateControls(true);
    }
    else
      this.UpdateControls(true);
  }

  /// <summary> Была нажата кнопка "По умолчанию" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _BtnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating || this.ControlsAreUpdating || this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this.ReadOnly || MessageBox.Show("Сбросить изменения в настройках сравнения обозначений?", "Настройки сравнения обозначений", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this._compareDesignationSchema.Changed = true;
    this.Changed = true;
    this.LockControls();
    try
    {
      this._compareDesignationSchema.LoadDefaultSchema();
      this.CompareDesignationSchema = this._compareDesignationSchema;
      this.UpdateControls(true);
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Была нажата кнопка "Тест" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _buttonTest_Click(object sender, EventArgs e)
  {
    int num = (int) new TestSameDesignation(this.CompareDesignationSchema).ShowDialog();
  }
}
