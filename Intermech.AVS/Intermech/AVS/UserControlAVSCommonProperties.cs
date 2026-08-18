// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.UserControlAVSCommonProperties
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.AVS.AVSProperties;
using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса UserControlSetupSkipPositions </summary>
public class UserControlAVSCommonProperties : ExtUserControl
{
  private IContainer components;
  private ToolTipController _editModeToolTip;
  public Button _btnReset;
  private CheckEdit ceListChanges;
  private Label label2;
  protected SpinEdit _upDownChangesListCount;
  private CheckEdit cbShowBCh;
  private CheckEdit cbHideEqualNumbers;
  private CheckEdit cbMergeChapters;
  private Button btnSelectUserDocTypeName;
  private Button btnClearUserDocTypeName;
  private Label label1;
  private TextBox tbUserDocTypeName;
  private GroupBox groupBox1;
  private Label label3;
  private Button btnClearUserAttributeForName;
  private TextBox tbUserAttributeForName;
  private Button btnSelectUserAttributeForName;
  private CheckEdit cbUseUserAttributeForNameFieldForDocuments;
  private Label lbLimitAndNominalValueMode;
  private System.Windows.Forms.ComboBox cbLimitAndNominalValueMode;
  private TextBox tbNameDivider;
  private Label label4;
  private CheckEdit cbShowAdditionalComplect;
  private Label label5;
  private System.Windows.Forms.ComboBox cbNamePosition;
  private CheckEdit cbDisplayPartOnNewPage;
  private CheckEdit cbAutoGenTextLnkToMainDocInNameFld;
  private ToolTipController _readModeToolTip;
  public AVSCommonPropertiesSchema _AVSCommonPropertiesSchema;
  private bool _isSpecificationMode = true;

  public UserControlAVSCommonProperties()
  {
    this.InitializeComponent();
    this.cbLimitAndNominalValueMode.Items.Clear();
    foreach (LimitAndNominalValueMode nominalValueMode in Enum.GetValues(typeof (LimitAndNominalValueMode)))
      this.cbLimitAndNominalValueMode.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) nominalValueMode));
    this.cbNamePosition.Items.Clear();
    foreach (AttributeForNamePosition attributeForNamePosition in Enum.GetValues(typeof (AttributeForNamePosition)))
      this.cbNamePosition.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) attributeForNamePosition));
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      this._editModeToolTip?.Dispose();
      this._readModeToolTip?.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlAVSCommonProperties));
    this._editModeToolTip = new ToolTipController(this.components);
    this.btnSelectUserDocTypeName = new Button();
    this.btnClearUserDocTypeName = new Button();
    this.label1 = new Label();
    this.tbUserDocTypeName = new TextBox();
    this.label3 = new Label();
    this.btnClearUserAttributeForName = new Button();
    this.tbUserAttributeForName = new TextBox();
    this.btnSelectUserAttributeForName = new Button();
    this.lbLimitAndNominalValueMode = new Label();
    this._readModeToolTip = new ToolTipController(this.components);
    this._btnReset = new Button();
    this.ceListChanges = new CheckEdit();
    this.label2 = new Label();
    this._upDownChangesListCount = new SpinEdit();
    this.cbHideEqualNumbers = new CheckEdit();
    this.cbShowBCh = new CheckEdit();
    this.cbMergeChapters = new CheckEdit();
    this.groupBox1 = new GroupBox();
    this.cbAutoGenTextLnkToMainDocInNameFld = new CheckEdit();
    this.cbNamePosition = new System.Windows.Forms.ComboBox();
    this.tbNameDivider = new TextBox();
    this.cbUseUserAttributeForNameFieldForDocuments = new CheckEdit();
    this.label5 = new Label();
    this.label4 = new Label();
    this.cbLimitAndNominalValueMode = new System.Windows.Forms.ComboBox();
    this.cbShowAdditionalComplect = new CheckEdit();
    this.cbDisplayPartOnNewPage = new CheckEdit();
    this.ceListChanges.Properties.BeginInit();
    this._upDownChangesListCount.Properties.BeginInit();
    this.cbHideEqualNumbers.Properties.BeginInit();
    this.cbShowBCh.Properties.BeginInit();
    this.cbMergeChapters.Properties.BeginInit();
    this.groupBox1.SuspendLayout();
    this.cbAutoGenTextLnkToMainDocInNameFld.Properties.BeginInit();
    this.cbUseUserAttributeForNameFieldForDocuments.Properties.BeginInit();
    this.cbShowAdditionalComplect.Properties.BeginInit();
    this.cbDisplayPartOnNewPage.Properties.BeginInit();
    this.SuspendLayout();
    this._editModeToolTip.Active = false;
    this._editModeToolTip.Style = new ViewStyle("ToolTip style");
    this.btnSelectUserDocTypeName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnSelectUserDocTypeName.Location = new Point(543, 21);
    this.btnSelectUserDocTypeName.Name = "btnSelectUserDocTypeName";
    this.btnSelectUserDocTypeName.Size = new Size(27, 23);
    this.btnSelectUserDocTypeName.TabIndex = 31 /*0x1F*/;
    this.btnSelectUserDocTypeName.Text = "...";
    this._editModeToolTip.SetToolTip((Control) this.btnSelectUserDocTypeName, "Выбрать атрибут");
    this._readModeToolTip.SetToolTip((Control) this.btnSelectUserDocTypeName, "Выбрать атрибут");
    this.btnSelectUserDocTypeName.UseVisualStyleBackColor = true;
    this.btnSelectUserDocTypeName.Click += new EventHandler(this.btnSelectUserDocTypeName_Click);
    this.btnClearUserDocTypeName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClearUserDocTypeName.Image = (Image) componentResourceManager.GetObject("btnClearUserDocTypeName.Image");
    this.btnClearUserDocTypeName.Location = new Point(571, 21);
    this.btnClearUserDocTypeName.Name = "btnClearUserDocTypeName";
    this.btnClearUserDocTypeName.Size = new Size(24, 23);
    this.btnClearUserDocTypeName.TabIndex = 32 /*0x20*/;
    this._editModeToolTip.SetToolTip((Control) this.btnClearUserDocTypeName, "Очистить значение");
    this._readModeToolTip.SetToolTip((Control) this.btnClearUserDocTypeName, "Очистить значение");
    this.btnClearUserDocTypeName.UseVisualStyleBackColor = true;
    this.btnClearUserDocTypeName.Click += new EventHandler(this.btnClearUserDocTypeName_Click);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(13, 26);
    this.label1.Name = "label1";
    this.label1.Size = new Size(244, 13);
    this.label1.TabIndex = 30;
    this.label1.Text = "Атрибут для нестандартного типа документов:";
    this._editModeToolTip.SetToolTip((Control) this.label1, "Пользовательский атрибут объекта для замены наименования типа в графе \"Наименования\"");
    this._readModeToolTip.SetToolTip((Control) this.label1, "Пользовательский атрибут объекта для замены наименования типа в графе \"Наименования\"");
    this.tbUserDocTypeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbUserDocTypeName.Location = new Point(295, 23);
    this.tbUserDocTypeName.Name = "tbUserDocTypeName";
    this.tbUserDocTypeName.ReadOnly = true;
    this.tbUserDocTypeName.Size = new Size(244, 20);
    this.tbUserDocTypeName.TabIndex = 30;
    this._readModeToolTip.SetToolTip((Control) this.tbUserDocTypeName, "Пользовательский атрибут объекта для замены наименования типа в графе \"Наименования\"");
    this._editModeToolTip.SetToolTip((Control) this.tbUserDocTypeName, "Пользовательский атрибут объекта для замены наименования типа в графе \"Наименования\"");
    this.label3.AutoSize = true;
    this.label3.Location = new Point(13, 51);
    this.label3.Name = "label3";
    this.label3.Size = new Size(218, 13);
    this.label3.TabIndex = 34;
    this.label3.Text = "Атрибут - заменитель для наименования:";
    this._editModeToolTip.SetToolTip((Control) this.label3, "Пользовательский атрибут объекта для замены настроенного атрибута наименования в графе Наименования");
    this._readModeToolTip.SetToolTip((Control) this.label3, "Пользовательский атрибут объекта для замены настроенного атрибута наименования в графе Наименования");
    this.label3.Click += new EventHandler(this.label3_Click);
    this.btnClearUserAttributeForName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClearUserAttributeForName.Image = (Image) componentResourceManager.GetObject("btnClearUserAttributeForName.Image");
    this.btnClearUserAttributeForName.Location = new Point(571, 46);
    this.btnClearUserAttributeForName.Name = "btnClearUserAttributeForName";
    this.btnClearUserAttributeForName.Size = new Size(24, 23);
    this.btnClearUserAttributeForName.TabIndex = 37;
    this._editModeToolTip.SetToolTip((Control) this.btnClearUserAttributeForName, "Очистить значение");
    this._readModeToolTip.SetToolTip((Control) this.btnClearUserAttributeForName, "Очистить значение");
    this.btnClearUserAttributeForName.UseVisualStyleBackColor = true;
    this.btnClearUserAttributeForName.Click += new EventHandler(this.btnClearUserAttributeForName_Click);
    this.tbUserAttributeForName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbUserAttributeForName.Location = new Point(295, 48 /*0x30*/);
    this.tbUserAttributeForName.Name = "tbUserAttributeForName";
    this.tbUserAttributeForName.ReadOnly = true;
    this.tbUserAttributeForName.Size = new Size(244, 20);
    this.tbUserAttributeForName.TabIndex = 35;
    this._readModeToolTip.SetToolTip((Control) this.tbUserAttributeForName, "Пользовательский атрибут объекта для замены настроенного атрибута наименования в графе Наименования");
    this._editModeToolTip.SetToolTip((Control) this.tbUserAttributeForName, "Пользовательский атрибут объекта для замены настроенного атрибута наименования в графе Наименования");
    this.tbUserAttributeForName.TextChanged += new EventHandler(this.tbUserAttributeForName_TextChanged);
    this.btnSelectUserAttributeForName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnSelectUserAttributeForName.Location = new Point(543, 46);
    this.btnSelectUserAttributeForName.Name = "btnSelectUserAttributeForName";
    this.btnSelectUserAttributeForName.Size = new Size(27, 23);
    this.btnSelectUserAttributeForName.TabIndex = 36;
    this.btnSelectUserAttributeForName.Text = "...";
    this._editModeToolTip.SetToolTip((Control) this.btnSelectUserAttributeForName, "Выбрать атрибут");
    this._readModeToolTip.SetToolTip((Control) this.btnSelectUserAttributeForName, "Выбрать атрибут");
    this.btnSelectUserAttributeForName.UseVisualStyleBackColor = true;
    this.btnSelectUserAttributeForName.Click += new EventHandler(this.btnSelectUserAttributeForName_Click);
    this.lbLimitAndNominalValueMode.AutoSize = true;
    this.lbLimitAndNominalValueMode.Location = new Point(15, 171);
    this.lbLimitAndNominalValueMode.Name = "lbLimitAndNominalValueMode";
    this.lbLimitAndNominalValueMode.Size = new Size(510, 13);
    this.lbLimitAndNominalValueMode.TabIndex = 41;
    this.lbLimitAndNominalValueMode.Text = "Режим вывода \"Предельных значений\" и \"Значений номинала\" для подбора в графе Примечание:";
    this._readModeToolTip.Style = new ViewStyle("ToolTip style");
    this._btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._btnReset.Enabled = false;
    this._btnReset.FlatStyle = FlatStyle.System;
    this._btnReset.Location = new Point(16 /*0x10*/, 397);
    this._btnReset.Name = "_btnReset";
    this._btnReset.Size = new Size(121, 27);
    this._btnReset.TabIndex = 40;
    this._btnReset.Text = "По умолчанию";
    this._btnReset.Click += new EventHandler(this._btnReset_Click);
    this.ceListChanges.Location = new Point(13, 14);
    this.ceListChanges.Name = "ceListChanges";
    this.ceListChanges.Properties.Caption = "Вставлять лист регистрации изменений, начиная с";
    this.ceListChanges.Size = new Size(315, 19);
    this.ceListChanges.TabIndex = 21;
    this.ceListChanges.EditValueChanged += new EventHandler(this.ceListChanges_EditValueChanged);
    this.ceListChanges.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(384, 17);
    this.label2.Name = "label2";
    this.label2.Size = new Size(42, 13);
    this.label2.TabIndex = 23;
    this.label2.Text = "листов";
    this._upDownChangesListCount.EditValue = (object) 1;
    this._upDownChangesListCount.Location = new Point(334, 14);
    this._upDownChangesListCount.Name = "_upDownChangesListCount";
    this._upDownChangesListCount.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownChangesListCount.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownChangesListCount.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownChangesListCount.Properties.IsFloatValue = false;
    this._upDownChangesListCount.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownChangesListCount.Properties.UseCtrlIncrement = false;
    this._upDownChangesListCount.Properties.ValidateOnEnterKey = true;
    this._upDownChangesListCount.Size = new Size(47, 20);
    this._upDownChangesListCount.TabIndex = 22;
    this._upDownChangesListCount.ToolTip = "Сколько строк пропускать между изделиями с различным типом изделия";
    this._upDownChangesListCount.EditValueChanged += new EventHandler(this._upDownChangesListCount_EditValueChanged);
    this._upDownChangesListCount.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.cbHideEqualNumbers.Location = new Point(13, 39);
    this.cbHideEqualNumbers.Name = "cbHideEqualNumbers";
    this.cbHideEqualNumbers.Properties.Caption = "Скрывать одинаковые номера позиций у записей идущих подряд ";
    this.cbHideEqualNumbers.Size = new Size(437, 19);
    this.cbHideEqualNumbers.TabIndex = 26;
    this.cbHideEqualNumbers.CheckedChanged += new EventHandler(this.cbHideEqualNumbers_CheckedChanged);
    this.cbHideEqualNumbers.EditValueChanged += new EventHandler(this.cbHideEqualNumbers_EditValueChanged);
    this.cbHideEqualNumbers.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.cbShowBCh.Location = new Point(13, 65);
    this.cbShowBCh.Name = "cbShowBCh";
    this.cbShowBCh.Properties.Caption = "Включить отображение БЧ в графе Формат.";
    this.cbShowBCh.Size = new Size(437, 19);
    this.cbShowBCh.TabIndex = 27;
    this.cbShowBCh.EditValueChanged += new EventHandler(this.cbShowBCh_EditValueChanged);
    this.cbShowBCh.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.cbMergeChapters.Location = new Point(13, 90);
    this.cbMergeChapters.Name = "cbMergeChapters";
    this.cbMergeChapters.Properties.Caption = "Группировать исполнения в надписи 'Различия исполнений' ";
    this.cbMergeChapters.Size = new Size(437, 19);
    this.cbMergeChapters.TabIndex = 28;
    this.cbMergeChapters.EditValueChanged += new EventHandler(this.cbMergeChapters_EditValueChanged);
    this.cbMergeChapters.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.cbAutoGenTextLnkToMainDocInNameFld);
    this.groupBox1.Controls.Add((Control) this.cbNamePosition);
    this.groupBox1.Controls.Add((Control) this.tbNameDivider);
    this.groupBox1.Controls.Add((Control) this.cbUseUserAttributeForNameFieldForDocuments);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.btnClearUserAttributeForName);
    this.groupBox1.Controls.Add((Control) this.tbUserAttributeForName);
    this.groupBox1.Controls.Add((Control) this.btnSelectUserAttributeForName);
    this.groupBox1.Controls.Add((Control) this.label5);
    this.groupBox1.Controls.Add((Control) this.label4);
    this.groupBox1.Controls.Add((Control) this.label1);
    this.groupBox1.Controls.Add((Control) this.btnClearUserDocTypeName);
    this.groupBox1.Controls.Add((Control) this.tbUserDocTypeName);
    this.groupBox1.Controls.Add((Control) this.btnSelectUserDocTypeName);
    this.groupBox1.Location = new Point(13, 221);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(600, 170);
    this.groupBox1.TabIndex = 32 /*0x20*/;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Графа \"Наименование\"";
    this.cbAutoGenTextLnkToMainDocInNameFld.Location = new Point(14, 145);
    this.cbAutoGenTextLnkToMainDocInNameFld.Name = "cbAutoGenTextLnkToMainDocInNameFld";
    this.cbAutoGenTextLnkToMainDocInNameFld.Properties.Caption = "Автоматически добавлять \"Смотри\"";
    this.cbAutoGenTextLnkToMainDocInNameFld.Size = new Size(498, 19);
    this.cbAutoGenTextLnkToMainDocInNameFld.TabIndex = 41;
    this.cbAutoGenTextLnkToMainDocInNameFld.ToolTip = "Вставлять в графу \"Наименование\" текст с обозначением главного конструкторского документа, когда оно значительно отличается от обозначения изделия. Например: \"(см. 123.456.000)\"";
    this.cbAutoGenTextLnkToMainDocInNameFld.ToolTipController = this._editModeToolTip;
    this.cbAutoGenTextLnkToMainDocInNameFld.CheckedChanged += new EventHandler(this.cbAutoGenTextLnkToMainDocInNameFld_CheckedChanged);
    this.cbNamePosition.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbNamePosition.FormattingEnabled = true;
    this.cbNamePosition.Items.AddRange(new object[3]
    {
      (object) "Вместо",
      (object) "Перед",
      (object) "После"
    });
    this.cbNamePosition.Location = new Point(346, 97);
    this.cbNamePosition.Name = "cbNamePosition";
    this.cbNamePosition.Size = new Size(130, 21);
    this.cbNamePosition.TabIndex = 40;
    this.cbNamePosition.SelectedIndexChanged += new EventHandler(this.cbNamePosition_SelectedIndexChanged);
    this.tbNameDivider.Location = new Point(346, 121);
    this.tbNameDivider.Name = "tbNameDivider";
    this.tbNameDivider.Size = new Size(100, 20);
    this.tbNameDivider.TabIndex = 39;
    this.tbNameDivider.TextChanged += new EventHandler(this.tbNameDivider_TextChanged);
    this.cbUseUserAttributeForNameFieldForDocuments.Location = new Point(14, 73);
    this.cbUseUserAttributeForNameFieldForDocuments.Name = "cbUseUserAttributeForNameFieldForDocuments";
    this.cbUseUserAttributeForNameFieldForDocuments.Properties.Caption = "Использовать атрибут - заменитель наименования для документов";
    this.cbUseUserAttributeForNameFieldForDocuments.Size = new Size(498, 19);
    this.cbUseUserAttributeForNameFieldForDocuments.TabIndex = 38;
    this.cbUseUserAttributeForNameFieldForDocuments.ToolTip = "Использовать пользовательский атрибут объекта для замены настроенного атрибута наименования в графе \"Наименования\" для документов";
    this.cbUseUserAttributeForNameFieldForDocuments.ToolTipController = this._editModeToolTip;
    this.cbUseUserAttributeForNameFieldForDocuments.CheckedChanged += new EventHandler(this.cbUseUserAttributeForNameFieldForDocuments_CheckedChanged);
    this.label5.AutoSize = true;
    this.label5.Location = new Point(13, 100);
    this.label5.Name = "label5";
    this.label5.Size = new Size(246, 13);
    this.label5.TabIndex = 30;
    this.label5.Text = "Позиция атрибута - заменителя наименования";
    this.label4.AutoSize = true;
    this.label4.Location = new Point(13, 124);
    this.label4.Name = "label4";
    this.label4.Size = new Size(327, 13);
    this.label4.TabIndex = 30;
    this.label4.Text = "Символ разделения наименования и условного наименования";
    this.cbLimitAndNominalValueMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbLimitAndNominalValueMode.BackColor = SystemColors.Window;
    this.cbLimitAndNominalValueMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbLimitAndNominalValueMode.FlatStyle = FlatStyle.System;
    this.cbLimitAndNominalValueMode.FormattingEnabled = true;
    this.cbLimitAndNominalValueMode.Location = new Point(16 /*0x10*/, 185);
    this.cbLimitAndNominalValueMode.Name = "cbLimitAndNominalValueMode";
    this.cbLimitAndNominalValueMode.Size = new Size(600, 21);
    this.cbLimitAndNominalValueMode.TabIndex = 42;
    this.cbLimitAndNominalValueMode.SelectedIndexChanged += new EventHandler(this.cbLimitAndNominalValueMode_SelectedIndexChanged);
    this.cbShowAdditionalComplect.Location = new Point(13, 115);
    this.cbShowAdditionalComplect.Name = "cbShowAdditionalComplect";
    this.cbShowAdditionalComplect.Properties.Caption = "Отображать примечание по комплектам поставляемым отдельно";
    this.cbShowAdditionalComplect.Size = new Size(437, 19);
    this.cbShowAdditionalComplect.TabIndex = 43;
    this.cbShowAdditionalComplect.EditValueChanged += new EventHandler(this.cbShowAdditionalComplect_EditValueChanged);
    this.cbShowAdditionalComplect.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.cbDisplayPartOnNewPage.Location = new Point(13, 140);
    this.cbDisplayPartOnNewPage.Name = "cbDisplayPartOnNewPage";
    this.cbDisplayPartOnNewPage.Properties.Caption = "Выводить части спецификации с новой страницы";
    this.cbDisplayPartOnNewPage.Size = new Size(390, 19);
    this.cbDisplayPartOnNewPage.TabIndex = 44;
    this.cbDisplayPartOnNewPage.EditValueChanged += new EventHandler(this.cbDisplayPartOnNewPage_EditValueChanged);
    this.cbDisplayPartOnNewPage.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.Controls.Add((Control) this.cbDisplayPartOnNewPage);
    this.Controls.Add((Control) this.cbShowAdditionalComplect);
    this.Controls.Add((Control) this.cbLimitAndNominalValueMode);
    this.Controls.Add((Control) this.lbLimitAndNominalValueMode);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.cbHideEqualNumbers);
    this.Controls.Add((Control) this.cbMergeChapters);
    this.Controls.Add((Control) this.cbShowBCh);
    this.Controls.Add((Control) this._upDownChangesListCount);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.ceListChanges);
    this.Controls.Add((Control) this._btnReset);
    this.Location = new Point(535, 370);
    this.Name = nameof (UserControlAVSCommonProperties);
    this.Size = new Size(632, 425);
    this.ceListChanges.Properties.EndInit();
    this._upDownChangesListCount.Properties.EndInit();
    this.cbHideEqualNumbers.Properties.EndInit();
    this.cbShowBCh.Properties.EndInit();
    this.cbMergeChapters.Properties.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.cbAutoGenTextLnkToMainDocInNameFld.Properties.EndInit();
    this.cbUseUserAttributeForNameFieldForDocuments.Properties.EndInit();
    this.cbShowAdditionalComplect.Properties.EndInit();
    this.cbDisplayPartOnNewPage.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary> Схема общих настроек AVS </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AVSCommonPropertiesSchema AVSCommonPropertiesSchema
  {
    get => this._AVSCommonPropertiesSchema;
    set
    {
      this.LockControls();
      try
      {
        this._AVSCommonPropertiesSchema = value;
        this.Changed = false;
        this.RefreshReadOnly();
        this.UpdateControls(true);
        this.RaiseOnInitDataEvent((object) this._AVSCommonPropertiesSchema);
        if (value == null || value.Parent == null)
          this._btnReset.Text = "По умолчанию";
        else
          this._btnReset.Text = "Наследовать";
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  /// <summary>Показывать ли настройки для спецификаций</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsSpecificationMode
  {
    get => this._isSpecificationMode;
    internal set
    {
      if (this._isSpecificationMode == value)
        return;
      this._isSpecificationMode = value;
      if (this._isSpecificationMode)
        return;
      this.groupBox1.Height -= 15;
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    if (this._editModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this._editModeToolTip.Active)
        {
          this._editModeToolTip.Active = false;
          this._readModeToolTip.Active = true;
        }
      }
      else if (this._readModeToolTip.Active)
      {
        this._readModeToolTip.Active = false;
        this._editModeToolTip.Active = true;
      }
    }
    this.ceListChanges.Enabled = !this.ReadOnly;
    this.cbHideEqualNumbers.Enabled = !this.ReadOnly;
    this.cbShowBCh.Enabled = !this.ReadOnly;
    this.cbShowAdditionalComplect.Enabled = !this.ReadOnly;
    this.tbNameDivider.Enabled = !this.ReadOnly;
    this.cbMergeChapters.Enabled = !this.ReadOnly;
    this._upDownChangesListCount.Enabled = !this.ReadOnly;
    this.tbUserAttributeForName.Enabled = !this.ReadOnly;
    this.btnSelectUserAttributeForName.Enabled = !this.ReadOnly;
    this.btnClearUserAttributeForName.Enabled = !this.ReadOnly;
    this.tbUserDocTypeName.Enabled = !this.ReadOnly;
    this.btnSelectUserDocTypeName.Enabled = !this.ReadOnly;
    this.btnClearUserDocTypeName.Enabled = !this.ReadOnly;
    this.cbUseUserAttributeForNameFieldForDocuments.Enabled = !this.ReadOnly;
    this.cbAutoGenTextLnkToMainDocInNameFld.Enabled = !this.ReadOnly;
    this.cbAutoGenTextLnkToMainDocInNameFld.Visible = this.IsSpecificationMode;
    this.cbLimitAndNominalValueMode.Enabled = !this.ReadOnly;
    this.cbNamePosition.Enabled = !this.ReadOnly;
    this.cbDisplayPartOnNewPage.Enabled = !this.ReadOnly;
    if (this._AVSCommonPropertiesSchema == null)
    {
      this.ceListChanges.CheckState = CheckState.Indeterminate;
      this.cbHideEqualNumbers.CheckState = CheckState.Indeterminate;
      this.cbShowBCh.CheckState = CheckState.Indeterminate;
      this.cbShowAdditionalComplect.CheckState = CheckState.Indeterminate;
      this.cbMergeChapters.CheckState = CheckState.Indeterminate;
      this.tbNameDivider.Text = "";
      this.cbLimitAndNominalValueMode.SelectedIndex = -1;
      this.cbNamePosition.SelectedIndex = -1;
      this._upDownChangesListCount.Text = "";
      this._upDownChangesListCount.Value = 0M;
      this.cbDisplayPartOnNewPage.CheckState = CheckState.Indeterminate;
    }
    else
    {
      this.ceListChanges.Checked = this._AVSCommonPropertiesSchema.CreateChangesList;
      this.cbHideEqualNumbers.Checked = this._AVSCommonPropertiesSchema.HideEqualNumber;
      this.cbShowBCh.Checked = this._AVSCommonPropertiesSchema.ShowBCh;
      this.cbShowAdditionalComplect.Checked = this._AVSCommonPropertiesSchema.ShowAdditionalComplects;
      this.tbNameDivider.Text = this._AVSCommonPropertiesSchema.NameDivider;
      this.cbMergeChapters.Checked = this._AVSCommonPropertiesSchema.MergeVariableChapters;
      this._upDownChangesListCount.Value = (Decimal) this._AVSCommonPropertiesSchema.ChangesListCount;
      this.tbUserDocTypeName.Text = MetaDataHelper.GetAttributeTypeName(this._AVSCommonPropertiesSchema.UserAttributeForDocTypeName);
      this.tbUserAttributeForName.Text = MetaDataHelper.GetAttributeTypeName(this._AVSCommonPropertiesSchema.UserAttributeForNameField);
      this.cbUseUserAttributeForNameFieldForDocuments.Checked = this._AVSCommonPropertiesSchema.UseUserAttributeForNameFieldForDocuments;
      this.cbAutoGenTextLnkToMainDocInNameFld.Checked = this._AVSCommonPropertiesSchema.AutoGenerateTextLinkToMainDocumentInNameField;
      this.cbLimitAndNominalValueMode.SelectedIndex = this.cbLimitAndNominalValueMode.FindString(EnumDescConverter.GetEnumDescription((Enum) this._AVSCommonPropertiesSchema.LimitAndNominalValueModeForNote));
      this.cbNamePosition.SelectedIndex = this.cbNamePosition.FindString(EnumDescConverter.GetEnumDescription((Enum) this._AVSCommonPropertiesSchema.UserAttributeForNamePosition));
      this.cbDisplayPartOnNewPage.Checked = this._AVSCommonPropertiesSchema.DisplayPartOnNewPage;
    }
    this.RefreshBoldFontInControls();
    this._btnReset.Enabled = !this.ReadOnly;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly()
  {
    return this._AVSCommonPropertiesSchema == null || this._AVSCommonPropertiesSchema.ReadOnly;
  }

  /// <summary>Обновление параметра Bold у шрифта</summary>
  public void RefreshBoldFontInControls()
  {
    if (this._AVSCommonPropertiesSchema == null)
      return;
    this.ChangeUpDownFontBold((Control) this.ceListChanges, this._AVSCommonPropertiesSchema.CreateChangesListChanged);
    this.ChangeUpDownFontBold((Control) this._upDownChangesListCount, this._AVSCommonPropertiesSchema.ChangesListCountChanged);
    this.ChangeUpDownFontBold((Control) this.cbHideEqualNumbers, this._AVSCommonPropertiesSchema.HideEqualNumberChanged);
    this.ChangeUpDownFontBold((Control) this.cbShowBCh, this._AVSCommonPropertiesSchema.ShowBChChanged);
    this.ChangeUpDownFontBold((Control) this.cbShowAdditionalComplect, this._AVSCommonPropertiesSchema.ShowAdditionalComplectsChanged);
    this.ChangeUpDownFontBold((Control) this.tbNameDivider, this._AVSCommonPropertiesSchema.NameDividerChanged);
    this.ChangeUpDownFontBold((Control) this.cbMergeChapters, this._AVSCommonPropertiesSchema.MergeVariableChaptersChanged);
    this.ChangeUpDownFontBold((Control) this.tbUserDocTypeName, this._AVSCommonPropertiesSchema.UserAttributeForDocTypeNameChanged);
    this.ChangeUpDownFontBold((Control) this.tbUserAttributeForName, this._AVSCommonPropertiesSchema.UserAttributeForNameFieldChanged);
    this.ChangeUpDownFontBold((Control) this.cbUseUserAttributeForNameFieldForDocuments, this._AVSCommonPropertiesSchema.UseUserAttributeForNameFieldForDocumentsChanged);
    this.ChangeUpDownFontBold((Control) this.cbLimitAndNominalValueMode, this._AVSCommonPropertiesSchema.LimitAndNominalValueModeForNoteChanged);
    this.ChangeUpDownFontBold((Control) this.cbNamePosition, this._AVSCommonPropertiesSchema.UserAttributeForNamePositionChanged);
    this.ChangeUpDownFontBold((Control) this.cbDisplayPartOnNewPage, this._AVSCommonPropertiesSchema.DisplayPartOnNewPageChanged);
    this.ChangeUpDownFontBold((Control) this.cbAutoGenTextLnkToMainDocInNameFld, this._AVSCommonPropertiesSchema.AutoGenerateTextLinkToMainDocumentInNameFieldChanged);
  }

  private void ChangeUpDownFontBold(Control control, bool mustBeBold)
  {
    if (control.Font.Bold == mustBeBold)
      return;
    control.Font = new Font(control.Font.FontFamily, control.Font.SizeInPoints, mustBeBold ? FontStyle.Bold : FontStyle.Regular, control.Font.Unit, control.Font.GdiCharSet, control.Font.GdiVerticalFont);
  }

  /// <summary>!!!!!!!!!!!!!!!</summary>
  /// <param name="spinEdit"></param>
  /// <param name="e"></param>
  private void BeforeChangeUpDown(Control sender, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    if (this._AVSCommonPropertiesSchema == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated);
  }

  /// <summary>!!!!!!!!!!!!!!</summary>
  /// <returns></returns>
  private bool BeforeUpDownEdit()
  {
    if (this._AVSCommonPropertiesSchema == null || this.ControlsAreUpdating)
      return false;
    bool wasUpdated = false;
    return !(!this.CheckCanEdit(ref wasUpdated) | wasUpdated);
  }

  private void AfterUpDownEdit() => this.Changed = true;

  private void UpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    this.BeforeChangeUpDown(sender as Control, e);
  }

  private void _btnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || MessageBox.Show("Сбросить изменения в настройках к значениям по умолчанию?", "Настройки AVS", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockControls();
    try
    {
      this._AVSCommonPropertiesSchema.LoadDefaultParams();
      this.UpdateControls(true);
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void ceListChanges_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.CreateChangesList = ((CheckEdit) sender).Checked;
    this.AfterUpDownEdit();
    this.RefreshBoldFontInControls();
  }

  private void _upDownChangesListCount_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.ChangesListCount = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshBoldFontInControls();
  }

  private void cbHideEqualNumbers_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.HideEqualNumber = ((CheckEdit) sender).Checked;
    this.AfterUpDownEdit();
    this.RefreshBoldFontInControls();
  }

  private void cbShowBCh_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.ShowBCh = ((CheckEdit) sender).Checked;
    this.AfterUpDownEdit();
    this.RefreshBoldFontInControls();
  }

  private void cbMergeChapters_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.MergeVariableChapters = ((CheckEdit) sender).Checked;
    this.AfterUpDownEdit();
    this.RefreshBoldFontInControls();
  }

  private void btnSelectUserDocTypeName_Click(object sender, EventArgs e)
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false);
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesGuid.Count <= 0)
      return;
    this._AVSCommonPropertiesSchema.UserAttributeForDocTypeName = attributesSelectDlg.SelectedAttributesGuid[0];
    this.Changed = true;
    this.tbUserDocTypeName.Text = MetaDataHelper.GetAttributeTypeName(this._AVSCommonPropertiesSchema.UserAttributeForDocTypeName);
    this.RefreshBoldFontInControls();
  }

  private void btnClearUserDocTypeName_Click(object sender, EventArgs e)
  {
    this._AVSCommonPropertiesSchema.UserAttributeForDocTypeName = Guid.Empty;
    this.Changed = true;
    this.tbUserDocTypeName.Text = "";
    this.RefreshBoldFontInControls();
  }

  private void btnSelectUserAttributeForName_Click(object sender, EventArgs e)
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false);
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesGuid.Count <= 0)
      return;
    this._AVSCommonPropertiesSchema.UserAttributeForNameField = attributesSelectDlg.SelectedAttributesGuid[0];
    this.Changed = true;
    this.tbUserAttributeForName.Text = MetaDataHelper.GetAttributeTypeName(this._AVSCommonPropertiesSchema.UserAttributeForNameField);
    this.RefreshBoldFontInControls();
  }

  private void btnClearUserAttributeForName_Click(object sender, EventArgs e)
  {
    this._AVSCommonPropertiesSchema.UserAttributeForNameField = Guid.Empty;
    this.Changed = true;
    this.tbUserAttributeForName.Text = "";
    this.RefreshBoldFontInControls();
  }

  private void cbUseUserAttributeForNameFieldForDocuments_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.UseUserAttributeForNameFieldForDocuments = this.cbUseUserAttributeForNameFieldForDocuments.Checked;
    this.Changed = true;
    this.RefreshBoldFontInControls();
  }

  private void cbAutoGenTextLnkToMainDocInNameFld_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.AutoGenerateTextLinkToMainDocumentInNameField = this.cbAutoGenTextLnkToMainDocInNameFld.Checked;
    this.Changed = true;
    this.RefreshBoldFontInControls();
  }

  private void cbLimitAndNominalValueMode_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.LimitAndNominalValueModeForNote = (LimitAndNominalValueMode) EnumDescConverter.GetEnumValue(typeof (LimitAndNominalValueMode), this.cbLimitAndNominalValueMode.Text);
    this.Changed = true;
    this.RefreshBoldFontInControls();
  }

  private void label3_Click(object sender, EventArgs e)
  {
  }

  private void tbNameDivider_TextChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.NameDivider = this.tbNameDivider.Text;
    this.AfterUpDownEdit();
    this.RefreshBoldFontInControls();
  }

  private void tbUserAttributeForName_TextChanged(object sender, EventArgs e)
  {
  }

  private void cbHideEqualNumbers_CheckedChanged(object sender, EventArgs e)
  {
  }

  private void cbShowAdditionalComplect_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.ShowAdditionalComplects = ((CheckEdit) sender).Checked;
    this.AfterUpDownEdit();
    this.RefreshBoldFontInControls();
  }

  private void cbNamePosition_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.UserAttributeForNamePosition = (AttributeForNamePosition) EnumDescConverter.GetEnumValue(typeof (AttributeForNamePosition), this.cbNamePosition.Text);
    this.Changed = true;
    this.RefreshBoldFontInControls();
  }

  private void cbDisplayPartOnNewPage_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._AVSCommonPropertiesSchema.DisplayPartOnNewPage = ((CheckEdit) sender).Checked;
    this.AfterUpDownEdit();
    this.RefreshBoldFontInControls();
  }
}
