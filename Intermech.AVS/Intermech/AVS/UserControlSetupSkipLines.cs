// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.UserControlSetupSkipLines
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces.AVS;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса UserControlSetupSkipPositions </summary>
public class UserControlSetupSkipLines : ExtUserControl
{
  private IContainer components;
  private ToolTipController _editModeToolTip;
  protected Label _label1;
  private SpinEdit _upDownBetweenDifferentDesignations;
  protected Label _label2;
  private SpinEdit _upDownBetweenSameDesignations;
  private Button _btnSameDesiognationSetup;
  protected Label _label3;
  protected Label _label4;
  private SpinEdit _upDownBetweenDifferentObjTypes;
  private SpinEdit _upDownBetweenSameObjTypes;
  protected Label _label5;
  protected Label _label6;
  protected Label _label7;
  protected Label _label8;
  protected Label _label9;
  protected Label _label10;
  protected Label _label11;
  protected Label _label12;
  protected Label _label13;
  protected Label _label14;
  protected Label _label15;
  protected Label _label16;
  private SpinEdit _upDownAfterVariableData;
  private SpinEdit _upDownBeforeVariableData;
  private SpinEdit _upDownAfterVariantNumber;
  private SpinEdit _upDownBeforeVariantNumber;
  private SpinEdit _upDownBetweenArtVariants;
  private SpinEdit _upDownBeforeAdd2;
  private SpinEdit _upDownBeforeAdd1;
  private SpinEdit _upDownAfterNote;
  private SpinEdit _upDownBeforeNote;
  private SpinEdit _upDownAfterSectionName;
  private SpinEdit _upDownBeforeSectionName;
  public Button _btnReset;
  private SpinEdit _upDownAfterAdd1;
  protected Label label1;
  private SpinEdit _upDownAfterAdd2;
  private Button bIspoln;
  private CheckBox cbNonSkipAtStartPage;
  private SpinEdit _upDownBeforeAdditionalChapter;
  protected Label label2;
  protected Label label3;
  private SpinEdit _upDownAfterAdditionalChapter;
  private System.Windows.Forms.ComboBox cbNumberingPositions;
  protected Label label4;
  private SpinEdit _upDownAfterDynamicGroup;
  protected Label label5;
  protected Label label6;
  private SpinEdit _upDownBeforeDynamicGroup;
  private ToolTipController _readModeToolTip;
  public SkipLinesSchema _skipLinesSchema;
  public SkipLinesSchema rootSkipLinesSchema;

  public UserControlSetupSkipLines()
  {
    this.InitializeComponent();
    this.Init();
  }

  /// <summary> Инциализация формы </summary>
  protected void Init()
  {
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._editModeToolTip = new ToolTipController(this.components);
    this._btnSameDesiognationSetup = new Button();
    this.bIspoln = new Button();
    this._readModeToolTip = new ToolTipController(this.components);
    this._label1 = new Label();
    this._upDownBetweenDifferentDesignations = new SpinEdit();
    this._label2 = new Label();
    this._upDownBetweenSameDesignations = new SpinEdit();
    this._label3 = new Label();
    this._upDownBetweenSameObjTypes = new SpinEdit();
    this._label4 = new Label();
    this._upDownBetweenDifferentObjTypes = new SpinEdit();
    this._label5 = new Label();
    this._upDownAfterVariableData = new SpinEdit();
    this._label6 = new Label();
    this._upDownBeforeVariableData = new SpinEdit();
    this._label7 = new Label();
    this._upDownAfterVariantNumber = new SpinEdit();
    this._label8 = new Label();
    this._upDownBeforeVariantNumber = new SpinEdit();
    this._label9 = new Label();
    this._upDownBetweenArtVariants = new SpinEdit();
    this._label10 = new Label();
    this._upDownBeforeAdd2 = new SpinEdit();
    this._label11 = new Label();
    this._label12 = new Label();
    this._upDownBeforeAdd1 = new SpinEdit();
    this._label13 = new Label();
    this._upDownAfterNote = new SpinEdit();
    this._label14 = new Label();
    this._upDownBeforeNote = new SpinEdit();
    this._label15 = new Label();
    this._upDownAfterSectionName = new SpinEdit();
    this._label16 = new Label();
    this._upDownBeforeSectionName = new SpinEdit();
    this._btnReset = new Button();
    this._upDownAfterAdd1 = new SpinEdit();
    this.label1 = new Label();
    this._upDownAfterAdd2 = new SpinEdit();
    this.cbNonSkipAtStartPage = new CheckBox();
    this._upDownBeforeAdditionalChapter = new SpinEdit();
    this.label2 = new Label();
    this.label3 = new Label();
    this._upDownAfterAdditionalChapter = new SpinEdit();
    this.cbNumberingPositions = new System.Windows.Forms.ComboBox();
    this.label4 = new Label();
    this._upDownAfterDynamicGroup = new SpinEdit();
    this.label5 = new Label();
    this.label6 = new Label();
    this._upDownBeforeDynamicGroup = new SpinEdit();
    this._upDownBetweenDifferentDesignations.Properties.BeginInit();
    this._upDownBetweenSameDesignations.Properties.BeginInit();
    this._upDownBetweenSameObjTypes.Properties.BeginInit();
    this._upDownBetweenDifferentObjTypes.Properties.BeginInit();
    this._upDownAfterVariableData.Properties.BeginInit();
    this._upDownBeforeVariableData.Properties.BeginInit();
    this._upDownAfterVariantNumber.Properties.BeginInit();
    this._upDownBeforeVariantNumber.Properties.BeginInit();
    this._upDownBetweenArtVariants.Properties.BeginInit();
    this._upDownBeforeAdd2.Properties.BeginInit();
    this._upDownBeforeAdd1.Properties.BeginInit();
    this._upDownAfterNote.Properties.BeginInit();
    this._upDownBeforeNote.Properties.BeginInit();
    this._upDownAfterSectionName.Properties.BeginInit();
    this._upDownBeforeSectionName.Properties.BeginInit();
    this._upDownAfterAdd1.Properties.BeginInit();
    this._upDownAfterAdd2.Properties.BeginInit();
    this._upDownBeforeAdditionalChapter.Properties.BeginInit();
    this._upDownAfterAdditionalChapter.Properties.BeginInit();
    this._upDownAfterDynamicGroup.Properties.BeginInit();
    this._upDownBeforeDynamicGroup.Properties.BeginInit();
    this.SuspendLayout();
    this._editModeToolTip.Active = false;
    this._editModeToolTip.Style = new ViewStyle("ToolTip style");
    this._btnSameDesiognationSetup.FlatStyle = FlatStyle.System;
    this._btnSameDesiognationSetup.Location = new Point(493, 35);
    this._btnSameDesiognationSetup.Name = "_btnSameDesiognationSetup";
    this._btnSameDesiognationSetup.Size = new Size(121, 27);
    this._btnSameDesiognationSetup.TabIndex = 2;
    this._btnSameDesiognationSetup.Text = "Сходство...";
    this._editModeToolTip.SetToolTip((Control) this._btnSameDesiognationSetup, "Определить критерии \"похожести\" обозначений");
    this._readModeToolTip.SetToolTip((Control) this._btnSameDesiognationSetup, "Просмотреть критерии \"похожести\" обозначений");
    this._btnSameDesiognationSetup.Click += new EventHandler(this._btnSameDesiognationSetup_Click);
    this.bIspoln.FlatStyle = FlatStyle.System;
    this.bIspoln.Location = new Point(493, 63 /*0x3F*/);
    this.bIspoln.Name = "bIspoln";
    this.bIspoln.Size = new Size(121, 27);
    this.bIspoln.TabIndex = 4;
    this.bIspoln.Text = "Исполнения...";
    this._editModeToolTip.SetToolTip((Control) this.bIspoln, "Настройка исполнений");
    this._readModeToolTip.SetToolTip((Control) this.bIspoln, "Настройка исполнений");
    this.bIspoln.Click += new EventHandler(this.bIspoln_Click);
    this._readModeToolTip.Style = new ViewStyle("ToolTip style");
    this._label1.Location = new Point(13, 40);
    this._label1.Name = "_label1";
    this._label1.Size = new Size(181, 13);
    this._label1.TabIndex = 2;
    this._label1.Text = "При различных обозначениях";
    this._label1.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBetweenDifferentDesignations.EditValue = (object) 1;
    this._upDownBetweenDifferentDesignations.Location = new Point(199, 37);
    this._upDownBetweenDifferentDesignations.Name = "_upDownBetweenDifferentDesignations";
    this._upDownBetweenDifferentDesignations.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBetweenDifferentDesignations.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenDifferentDesignations.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenDifferentDesignations.Properties.IsFloatValue = false;
    this._upDownBetweenDifferentDesignations.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBetweenDifferentDesignations.Properties.UseCtrlIncrement = false;
    this._upDownBetweenDifferentDesignations.Properties.ValidateOnEnterKey = true;
    this._upDownBetweenDifferentDesignations.Size = new Size(45, 20);
    this._upDownBetweenDifferentDesignations.TabIndex = 0;
    this._upDownBetweenDifferentDesignations.ToolTip = "Сколько строк пропускать между изделиями с различными обозначениями";
    this._upDownBetweenDifferentDesignations.EditValueChanged += new EventHandler(this._upDownBetweenDifferentDesignations_EditValueChanged);
    this._upDownBetweenDifferentDesignations.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label2.Location = new Point(258, 40);
    this._label2.Name = "_label2";
    this._label2.Size = new Size(182, 13);
    this._label2.TabIndex = 5;
    this._label2.Text = "При похожих обозначениях";
    this._label2.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBetweenSameDesignations.EditValue = (object) 1;
    this._upDownBetweenSameDesignations.Location = new Point(445, 37);
    this._upDownBetweenSameDesignations.Name = "_upDownBetweenSameDesignations";
    this._upDownBetweenSameDesignations.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBetweenSameDesignations.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenSameDesignations.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenSameDesignations.Properties.IsFloatValue = false;
    this._upDownBetweenSameDesignations.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBetweenSameDesignations.Properties.UseCtrlIncrement = false;
    this._upDownBetweenSameDesignations.Properties.ValidateOnEnterKey = true;
    this._upDownBetweenSameDesignations.Size = new Size(45, 20);
    this._upDownBetweenSameDesignations.TabIndex = 1;
    this._upDownBetweenSameDesignations.ToolTip = "Сколько строк пропускать между изделиями с похожими обозначениями";
    this._upDownBetweenSameDesignations.EditValueChanged += new EventHandler(this._upDownBetweenSameDesignations_EditValueChanged);
    this._upDownBetweenSameDesignations.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label3.Location = new Point(253, 90);
    this._label3.Name = "_label3";
    this._label3.Size = new Size(187, 29);
    this._label3.TabIndex = 10;
    this._label3.Text = "При одинаковых  классах \r\nстандартных изделий\r\n";
    this._label3.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBetweenSameObjTypes.EditValue = (object) 1;
    this._upDownBetweenSameObjTypes.Location = new Point(445, 95);
    this._upDownBetweenSameObjTypes.Name = "_upDownBetweenSameObjTypes";
    this._upDownBetweenSameObjTypes.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBetweenSameObjTypes.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenSameObjTypes.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenSameObjTypes.Properties.IsFloatValue = false;
    this._upDownBetweenSameObjTypes.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBetweenSameObjTypes.Properties.UseCtrlIncrement = false;
    this._upDownBetweenSameObjTypes.Properties.ValidateOnEnterKey = true;
    this._upDownBetweenSameObjTypes.Size = new Size(45, 20);
    this._upDownBetweenSameObjTypes.TabIndex = 6;
    this._upDownBetweenSameObjTypes.ToolTip = "Сколько строк пропускать между изделиями с одинаковым типом изделия";
    this._upDownBetweenSameObjTypes.EditValueChanged += new EventHandler(this._upDownBetweenSameObjTypes_EditValueChanged);
    this._upDownBetweenSameObjTypes.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label4.Location = new Point(11, 90);
    this._label4.Name = "_label4";
    this._label4.Size = new Size(183, 29);
    this._label4.TabIndex = 8;
    this._label4.Text = "При различных классах \r\nстандартных изделий";
    this._label4.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBetweenDifferentObjTypes.EditValue = (object) 1;
    this._upDownBetweenDifferentObjTypes.Location = new Point(199, 95);
    this._upDownBetweenDifferentObjTypes.Name = "_upDownBetweenDifferentObjTypes";
    this._upDownBetweenDifferentObjTypes.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBetweenDifferentObjTypes.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenDifferentObjTypes.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenDifferentObjTypes.Properties.IsFloatValue = false;
    this._upDownBetweenDifferentObjTypes.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBetweenDifferentObjTypes.Properties.UseCtrlIncrement = false;
    this._upDownBetweenDifferentObjTypes.Properties.ValidateOnEnterKey = true;
    this._upDownBetweenDifferentObjTypes.Size = new Size(45, 20);
    this._upDownBetweenDifferentObjTypes.TabIndex = 4;
    this._upDownBetweenDifferentObjTypes.ToolTip = "Сколько строк пропускать между изделиями с различным типом изделия";
    this._upDownBetweenDifferentObjTypes.EditValueChanged += new EventHandler(this._upDownBetweenDifferentObjTypes_EditValueChanged);
    this._upDownBetweenDifferentObjTypes.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label5.Location = new Point(253, (int) sbyte.MaxValue);
    this._label5.Name = "_label5";
    this._label5.Size = new Size(187, 13);
    this._label5.TabIndex = 14;
    this._label5.Text = "После переменных данных";
    this._label5.TextAlign = ContentAlignment.MiddleRight;
    this._upDownAfterVariableData.EditValue = (object) 1;
    this._upDownAfterVariableData.Location = new Point(445, 124);
    this._upDownAfterVariableData.Name = "_upDownAfterVariableData";
    this._upDownAfterVariableData.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownAfterVariableData.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownAfterVariableData.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownAfterVariableData.Properties.IsFloatValue = false;
    this._upDownAfterVariableData.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownAfterVariableData.Properties.UseCtrlIncrement = false;
    this._upDownAfterVariableData.Properties.ValidateOnEnterKey = true;
    this._upDownAfterVariableData.Size = new Size(45, 20);
    this._upDownAfterVariableData.TabIndex = 8;
    this._upDownAfterVariableData.ToolTip = "Сколько строк пропускать после переменных данных";
    this._upDownAfterVariableData.EditValueChanged += new EventHandler(this._upDownAfterVariableData_EditValueChanged);
    this._upDownAfterVariableData.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label6.Location = new Point(11, (int) sbyte.MaxValue);
    this._label6.Name = "_label6";
    this._label6.Size = new Size(183, 13);
    this._label6.TabIndex = 12;
    this._label6.Text = "Перед переменными данными";
    this._label6.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBeforeVariableData.EditValue = (object) 1;
    this._upDownBeforeVariableData.Location = new Point(199, 124);
    this._upDownBeforeVariableData.Name = "_upDownBeforeVariableData";
    this._upDownBeforeVariableData.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBeforeVariableData.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeVariableData.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeVariableData.Properties.IsFloatValue = false;
    this._upDownBeforeVariableData.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBeforeVariableData.Properties.UseCtrlIncrement = false;
    this._upDownBeforeVariableData.Properties.ValidateOnEnterKey = true;
    this._upDownBeforeVariableData.Size = new Size(45, 20);
    this._upDownBeforeVariableData.TabIndex = 7;
    this._upDownBeforeVariableData.ToolTip = "Сколько строк пропускать перед переменными данными";
    this._upDownBeforeVariableData.EditValueChanged += new EventHandler(this._upDownBeforeVariableData_EditValueChanged);
    this._upDownBeforeVariableData.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label7.Location = new Point(253, 156);
    this._label7.Name = "_label7";
    this._label7.Size = new Size(187, 13);
    this._label7.TabIndex = 18;
    this._label7.Text = "После номера исполнения";
    this._label7.TextAlign = ContentAlignment.MiddleRight;
    this._upDownAfterVariantNumber.EditValue = (object) 1;
    this._upDownAfterVariantNumber.Location = new Point(445, 153);
    this._upDownAfterVariantNumber.Name = "_upDownAfterVariantNumber";
    this._upDownAfterVariantNumber.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownAfterVariantNumber.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownAfterVariantNumber.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownAfterVariantNumber.Properties.IsFloatValue = false;
    this._upDownAfterVariantNumber.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownAfterVariantNumber.Properties.UseCtrlIncrement = false;
    this._upDownAfterVariantNumber.Properties.ValidateOnEnterKey = true;
    this._upDownAfterVariantNumber.Size = new Size(45, 20);
    this._upDownAfterVariantNumber.TabIndex = 10;
    this._upDownAfterVariantNumber.ToolTip = "Сколько строк пропускать после номера исполнения";
    this._upDownAfterVariantNumber.EditValueChanged += new EventHandler(this._upDownAfterVariantNumber_EditValueChanged);
    this._upDownAfterVariantNumber.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label8.Location = new Point(11, 156);
    this._label8.Name = "_label8";
    this._label8.Size = new Size(183, 13);
    this._label8.TabIndex = 16 /*0x10*/;
    this._label8.Text = "Перед номером исполнения";
    this._label8.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBeforeVariantNumber.EditValue = (object) 1;
    this._upDownBeforeVariantNumber.Location = new Point(199, 153);
    this._upDownBeforeVariantNumber.Name = "_upDownBeforeVariantNumber";
    this._upDownBeforeVariantNumber.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBeforeVariantNumber.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeVariantNumber.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeVariantNumber.Properties.IsFloatValue = false;
    this._upDownBeforeVariantNumber.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBeforeVariantNumber.Properties.UseCtrlIncrement = false;
    this._upDownBeforeVariantNumber.Properties.ValidateOnEnterKey = true;
    this._upDownBeforeVariantNumber.Size = new Size(45, 20);
    this._upDownBeforeVariantNumber.TabIndex = 9;
    this._upDownBeforeVariantNumber.ToolTip = "Сколько строк пропускать перед номером исполнения";
    this._upDownBeforeVariantNumber.EditValueChanged += new EventHandler(this._upDownBeforeVariantNumber_EditValueChanged);
    this._upDownBeforeVariantNumber.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label9.Location = new Point(253, 68);
    this._label9.Name = "_label9";
    this._label9.Size = new Size(187, 13);
    this._label9.TabIndex = 34;
    this._label9.Text = "Между исполнениями детали";
    this._label9.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBetweenArtVariants.EditValue = (object) 1;
    this._upDownBetweenArtVariants.Location = new Point(445, 65);
    this._upDownBetweenArtVariants.Name = "_upDownBetweenArtVariants";
    this._upDownBetweenArtVariants.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBetweenArtVariants.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenArtVariants.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBetweenArtVariants.Properties.IsFloatValue = false;
    this._upDownBetweenArtVariants.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBetweenArtVariants.Properties.UseCtrlIncrement = false;
    this._upDownBetweenArtVariants.Properties.ValidateOnEnterKey = true;
    this._upDownBetweenArtVariants.Size = new Size(45, 20);
    this._upDownBetweenArtVariants.TabIndex = 3;
    this._upDownBetweenArtVariants.ToolTip = "Сколько строк пропускать между исполнениями детали";
    this._upDownBetweenArtVariants.EditValueChanged += new EventHandler(this._upDownBetweenArtVariants_EditValueChanged);
    this._upDownBetweenArtVariants.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label10.Location = new Point(133, 359);
    this._label10.Name = "_label10";
    this._label10.Size = new Size(183, 13);
    this._label10.TabIndex = 32 /*0x20*/;
    this._label10.Text = "Перед Дополнительной 2";
    this._label10.TextAlign = ContentAlignment.MiddleRight;
    this._label10.Visible = false;
    this._upDownBeforeAdd2.EditValue = (object) 1;
    this._upDownBeforeAdd2.Location = new Point(321, 356);
    this._upDownBeforeAdd2.Name = "_upDownBeforeAdd2";
    this._upDownBeforeAdd2.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBeforeAdd2.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeAdd2.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeAdd2.Properties.IsFloatValue = false;
    this._upDownBeforeAdd2.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBeforeAdd2.Properties.UseCtrlIncrement = false;
    this._upDownBeforeAdd2.Properties.ValidateOnEnterKey = true;
    this._upDownBeforeAdd2.Size = new Size(45, 20);
    this._upDownBeforeAdd2.TabIndex = 17;
    this._upDownBeforeAdd2.ToolTip = "Сколько строк пропускать перед Дополнительной 2";
    this._upDownBeforeAdd2.Visible = false;
    this._upDownBeforeAdd2.EditValueChanged += new EventHandler(this._upDownBeforeAdd2_EditValueChanged);
    this._upDownBeforeAdd2.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label11.Location = new Point(375, 330);
    this._label11.Name = "_label11";
    this._label11.Size = new Size(187, 13);
    this._label11.TabIndex = 30;
    this._label11.Text = "После Дополнительной 1";
    this._label11.TextAlign = ContentAlignment.MiddleRight;
    this._label11.Visible = false;
    this._label12.Location = new Point(133, 330);
    this._label12.Name = "_label12";
    this._label12.Size = new Size(183, 13);
    this._label12.TabIndex = 28;
    this._label12.Text = "Перед Дополнительной 1";
    this._label12.TextAlign = ContentAlignment.MiddleRight;
    this._label12.Visible = false;
    this._upDownBeforeAdd1.EditValue = (object) 1;
    this._upDownBeforeAdd1.Location = new Point(321, 327);
    this._upDownBeforeAdd1.Name = "_upDownBeforeAdd1";
    this._upDownBeforeAdd1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBeforeAdd1.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeAdd1.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeAdd1.Properties.IsFloatValue = false;
    this._upDownBeforeAdd1.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBeforeAdd1.Properties.UseCtrlIncrement = false;
    this._upDownBeforeAdd1.Properties.ValidateOnEnterKey = true;
    this._upDownBeforeAdd1.Size = new Size(45, 20);
    this._upDownBeforeAdd1.TabIndex = 15;
    this._upDownBeforeAdd1.ToolTip = "Сколько строк пропускать перед Дополнительной 1";
    this._upDownBeforeAdd1.Visible = false;
    this._upDownBeforeAdd1.EditValueChanged += new EventHandler(this._upDownBeforeAdd1_EditValueChanged);
    this._upDownBeforeAdd1.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label13.Location = new Point(253, 214);
    this._label13.Name = "_label13";
    this._label13.Size = new Size(187, 13);
    this._label13.TabIndex = 26;
    this._label13.Text = "После примечания";
    this._label13.TextAlign = ContentAlignment.MiddleRight;
    this._upDownAfterNote.EditValue = (object) 1;
    this._upDownAfterNote.Location = new Point(445, 211);
    this._upDownAfterNote.Name = "_upDownAfterNote";
    this._upDownAfterNote.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownAfterNote.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownAfterNote.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownAfterNote.Properties.IsFloatValue = false;
    this._upDownAfterNote.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownAfterNote.Properties.UseCtrlIncrement = false;
    this._upDownAfterNote.Properties.ValidateOnEnterKey = true;
    this._upDownAfterNote.Size = new Size(45, 20);
    this._upDownAfterNote.TabIndex = 14;
    this._upDownAfterNote.ToolTip = "Сколько строк пропускать после примечания";
    this._upDownAfterNote.EditValueChanged += new EventHandler(this._upDownAfterNote_EditValueChanged);
    this._upDownAfterNote.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label14.Location = new Point(11, 214);
    this._label14.Name = "_label14";
    this._label14.Size = new Size(183, 13);
    this._label14.TabIndex = 24;
    this._label14.Text = "Перед примечанием";
    this._label14.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBeforeNote.EditValue = (object) 1;
    this._upDownBeforeNote.Location = new Point(199, 211);
    this._upDownBeforeNote.Name = "_upDownBeforeNote";
    this._upDownBeforeNote.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBeforeNote.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeNote.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeNote.Properties.IsFloatValue = false;
    this._upDownBeforeNote.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBeforeNote.Properties.UseCtrlIncrement = false;
    this._upDownBeforeNote.Properties.ValidateOnEnterKey = true;
    this._upDownBeforeNote.Size = new Size(45, 20);
    this._upDownBeforeNote.TabIndex = 13;
    this._upDownBeforeNote.ToolTip = "Сколько строк пропускать перед примечанием";
    this._upDownBeforeNote.EditValueChanged += new EventHandler(this._upDownBeforeNote_EditValueChanged);
    this._upDownBeforeNote.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label15.Location = new Point(258, 185);
    this._label15.Name = "_label15";
    this._label15.Size = new Size(182, 13);
    this._label15.TabIndex = 22;
    this._label15.Text = "После наименования раздела";
    this._label15.TextAlign = ContentAlignment.MiddleRight;
    this._upDownAfterSectionName.EditValue = (object) 1;
    this._upDownAfterSectionName.Location = new Point(445, 182);
    this._upDownAfterSectionName.Name = "_upDownAfterSectionName";
    this._upDownAfterSectionName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownAfterSectionName.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownAfterSectionName.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownAfterSectionName.Properties.IsFloatValue = false;
    this._upDownAfterSectionName.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownAfterSectionName.Properties.UseCtrlIncrement = false;
    this._upDownAfterSectionName.Properties.ValidateOnEnterKey = true;
    this._upDownAfterSectionName.Size = new Size(45, 20);
    this._upDownAfterSectionName.TabIndex = 12;
    this._upDownAfterSectionName.ToolTip = "Сколько строк пропускать после наименования раздела";
    this._upDownAfterSectionName.EditValueChanged += new EventHandler(this._upDownAfterSectionName_EditValueChanged);
    this._upDownAfterSectionName.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._label16.Location = new Point(13, 185);
    this._label16.Name = "_label16";
    this._label16.Size = new Size(181, 13);
    this._label16.TabIndex = 20;
    this._label16.Text = "Перед наименованием раздела";
    this._label16.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBeforeSectionName.EditValue = (object) 1;
    this._upDownBeforeSectionName.Location = new Point(199, 182);
    this._upDownBeforeSectionName.Name = "_upDownBeforeSectionName";
    this._upDownBeforeSectionName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBeforeSectionName.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeSectionName.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeSectionName.Properties.IsFloatValue = false;
    this._upDownBeforeSectionName.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBeforeSectionName.Properties.UseCtrlIncrement = false;
    this._upDownBeforeSectionName.Properties.ValidateOnEnterKey = true;
    this._upDownBeforeSectionName.Size = new Size(45, 20);
    this._upDownBeforeSectionName.TabIndex = 11;
    this._upDownBeforeSectionName.ToolTip = "Сколько строк пропускать перед наименованием раздела";
    this._upDownBeforeSectionName.EditValueChanged += new EventHandler(this._upDownBeforeSectionName_EditValueChanged);
    this._upDownBeforeSectionName.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this._btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._btnReset.Enabled = false;
    this._btnReset.FlatStyle = FlatStyle.System;
    this._btnReset.Location = new Point(16 /*0x10*/, 384);
    this._btnReset.Name = "_btnReset";
    this._btnReset.Size = new Size(121, 27);
    this._btnReset.TabIndex = 39;
    this._btnReset.Text = "По умолчанию";
    this._btnReset.Click += new EventHandler(this._btnReset_Click);
    this._upDownAfterAdd1.EditValue = (object) 1;
    this._upDownAfterAdd1.Location = new Point(567, 327);
    this._upDownAfterAdd1.Name = "_upDownAfterAdd1";
    this._upDownAfterAdd1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownAfterAdd1.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownAfterAdd1.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownAfterAdd1.Properties.IsFloatValue = false;
    this._upDownAfterAdd1.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownAfterAdd1.Properties.UseCtrlIncrement = false;
    this._upDownAfterAdd1.Properties.ValidateOnEnterKey = true;
    this._upDownAfterAdd1.Size = new Size(45, 20);
    this._upDownAfterAdd1.TabIndex = 16 /*0x10*/;
    this._upDownAfterAdd1.ToolTip = "Сколько строк пропускать после Дополнительной 1";
    this._upDownAfterAdd1.Visible = false;
    this._upDownAfterAdd1.EditValueChanged += new EventHandler(this._upDownAfterAdd1_EditValueChanged);
    this._upDownAfterAdd1.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.label1.Location = new Point(379, 359);
    this.label1.Name = "label1";
    this.label1.Size = new Size(183, 13);
    this.label1.TabIndex = 37;
    this.label1.Text = "После Дополнительной 2";
    this.label1.TextAlign = ContentAlignment.MiddleRight;
    this.label1.Visible = false;
    this._upDownAfterAdd2.EditValue = (object) 1;
    this._upDownAfterAdd2.Location = new Point(567, 356);
    this._upDownAfterAdd2.Name = "_upDownAfterAdd2";
    this._upDownAfterAdd2.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownAfterAdd2.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownAfterAdd2.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownAfterAdd2.Properties.IsFloatValue = false;
    this._upDownAfterAdd2.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownAfterAdd2.Properties.UseCtrlIncrement = false;
    this._upDownAfterAdd2.Properties.ValidateOnEnterKey = true;
    this._upDownAfterAdd2.Size = new Size(45, 20);
    this._upDownAfterAdd2.TabIndex = 18;
    this._upDownAfterAdd2.ToolTip = "Сколько строк пропускать после Дополнительной 2";
    this._upDownAfterAdd2.Visible = false;
    this.cbNonSkipAtStartPage.Location = new Point(33, 300);
    this.cbNonSkipAtStartPage.Name = "cbNonSkipAtStartPage";
    this.cbNonSkipAtStartPage.Size = new Size(500, 17);
    this.cbNonSkipAtStartPage.TabIndex = 38;
    this.cbNonSkipAtStartPage.Text = "Игнорировать пропуски строк перед записью в начале страницы";
    this.cbNonSkipAtStartPage.CheckedChanged += new EventHandler(this.cbNonSkipAtStartPage_CheckedChanged);
    this._upDownBeforeAdditionalChapter.EditValue = (object) 1;
    this._upDownBeforeAdditionalChapter.Location = new Point(199, 239);
    this._upDownBeforeAdditionalChapter.Name = "_upDownBeforeAdditionalChapter";
    this._upDownBeforeAdditionalChapter.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBeforeAdditionalChapter.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeAdditionalChapter.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeAdditionalChapter.Properties.IsFloatValue = false;
    this._upDownBeforeAdditionalChapter.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBeforeAdditionalChapter.Properties.UseCtrlIncrement = false;
    this._upDownBeforeAdditionalChapter.Properties.ValidateOnEnterKey = true;
    this._upDownBeforeAdditionalChapter.Size = new Size(45, 20);
    this._upDownBeforeAdditionalChapter.TabIndex = 15;
    this._upDownBeforeAdditionalChapter.ToolTip = "Сколько строк пропускать перед наименованием части";
    this._upDownBeforeAdditionalChapter.EditValueChanged += new EventHandler(this._upDownBeforeAdditional_EditValueChanged);
    this._upDownBeforeAdditionalChapter.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.label2.Location = new Point(11, 242);
    this.label2.Name = "label2";
    this.label2.Size = new Size(183, 13);
    this.label2.TabIndex = 28;
    this.label2.Text = "Перед наименованием части";
    this.label2.TextAlign = ContentAlignment.MiddleRight;
    this.label3.Location = new Point(253, 242);
    this.label3.Name = "label3";
    this.label3.Size = new Size(187, 13);
    this.label3.TabIndex = 30;
    this.label3.Text = "После наименования части";
    this.label3.TextAlign = ContentAlignment.MiddleRight;
    this._upDownAfterAdditionalChapter.EditValue = (object) 1;
    this._upDownAfterAdditionalChapter.Location = new Point(445, 239);
    this._upDownAfterAdditionalChapter.Name = "_upDownAfterAdditionalChapter";
    this._upDownAfterAdditionalChapter.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownAfterAdditionalChapter.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownAfterAdditionalChapter.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownAfterAdditionalChapter.Properties.IsFloatValue = false;
    this._upDownAfterAdditionalChapter.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownAfterAdditionalChapter.Properties.UseCtrlIncrement = false;
    this._upDownAfterAdditionalChapter.Properties.ValidateOnEnterKey = true;
    this._upDownAfterAdditionalChapter.Size = new Size(45, 20);
    this._upDownAfterAdditionalChapter.TabIndex = 16 /*0x10*/;
    this._upDownAfterAdditionalChapter.ToolTip = "Сколько строк пропускать после наименования части";
    this._upDownAfterAdditionalChapter.EditValueChanged += new EventHandler(this._upDownAfterAdditional_EditValueChanged);
    this._upDownAfterAdditionalChapter.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.cbNumberingPositions.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbNumberingPositions.FormattingEnabled = true;
    this.cbNumberingPositions.Items.AddRange(new object[3]
    {
      (object) "Позиции не учитывать",
      (object) "Пустых строк по разнице позиций",
      (object) "Пустых строк по разнице позиций + 1"
    });
    this.cbNumberingPositions.Location = new Point(199, 8);
    this.cbNumberingPositions.Name = "cbNumberingPositions";
    this.cbNumberingPositions.Size = new Size(291, 21);
    this.cbNumberingPositions.TabIndex = 40;
    this.cbNumberingPositions.SelectedIndexChanged += new EventHandler(this.cbNumberingPositions_SelectedIndexChanged);
    this.label4.Location = new Point(10, 11);
    this.label4.Name = "label4";
    this.label4.Size = new Size(183, 13);
    this.label4.TabIndex = 32 /*0x20*/;
    this.label4.Text = "Записи с позициями";
    this.label4.TextAlign = ContentAlignment.MiddleRight;
    this._upDownAfterDynamicGroup.EditValue = (object) 1;
    this._upDownAfterDynamicGroup.Location = new Point(445, 265);
    this._upDownAfterDynamicGroup.Name = "_upDownAfterDynamicGroup";
    this._upDownAfterDynamicGroup.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownAfterDynamicGroup.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownAfterDynamicGroup.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownAfterDynamicGroup.Properties.IsFloatValue = false;
    this._upDownAfterDynamicGroup.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownAfterDynamicGroup.Properties.UseCtrlIncrement = false;
    this._upDownAfterDynamicGroup.Properties.ValidateOnEnterKey = true;
    this._upDownAfterDynamicGroup.Size = new Size(45, 20);
    this._upDownAfterDynamicGroup.TabIndex = 42;
    this._upDownAfterDynamicGroup.ToolTip = "Сколько строк пропускать после динамической группы записей";
    this._upDownAfterDynamicGroup.EditValueChanged += new EventHandler(this._upDownAfterDynamicGroup_EditValueChanged);
    this._upDownAfterDynamicGroup.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.label5.Location = new Point(253, 268);
    this.label5.Name = "label5";
    this.label5.Size = new Size(187, 13);
    this.label5.TabIndex = 44;
    this.label5.Text = "После динамической группы";
    this.label5.TextAlign = ContentAlignment.MiddleRight;
    this.label6.Location = new Point(11, 268);
    this.label6.Name = "label6";
    this.label6.Size = new Size(183, 13);
    this.label6.TabIndex = 43;
    this.label6.Text = "Перед динамической группой";
    this.label6.TextAlign = ContentAlignment.MiddleRight;
    this._upDownBeforeDynamicGroup.EditValue = (object) 1;
    this._upDownBeforeDynamicGroup.Location = new Point(199, 265);
    this._upDownBeforeDynamicGroup.Name = "_upDownBeforeDynamicGroup";
    this._upDownBeforeDynamicGroup.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownBeforeDynamicGroup.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeDynamicGroup.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownBeforeDynamicGroup.Properties.IsFloatValue = false;
    this._upDownBeforeDynamicGroup.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownBeforeDynamicGroup.Properties.UseCtrlIncrement = false;
    this._upDownBeforeDynamicGroup.Properties.ValidateOnEnterKey = true;
    this._upDownBeforeDynamicGroup.Size = new Size(45, 20);
    this._upDownBeforeDynamicGroup.TabIndex = 41;
    this._upDownBeforeDynamicGroup.ToolTip = "Сколько строк пропускать перед динамической группой записей";
    this._upDownBeforeDynamicGroup.EditValueChanged += new EventHandler(this._upDownBeforeDynamicGroup_EditValueChanged);
    this._upDownBeforeDynamicGroup.EditValueChanging += new ChangingEventHandler(this.UpDown_EditValueChanging);
    this.Controls.Add((Control) this._upDownAfterDynamicGroup);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this._upDownBeforeDynamicGroup);
    this.Controls.Add((Control) this.cbNumberingPositions);
    this.Controls.Add((Control) this.cbNonSkipAtStartPage);
    this.Controls.Add((Control) this.bIspoln);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._upDownAfterAdd2);
    this.Controls.Add((Control) this._upDownAfterAdditionalChapter);
    this.Controls.Add((Control) this._upDownAfterAdd1);
    this.Controls.Add((Control) this._btnReset);
    this.Controls.Add((Control) this._label9);
    this.Controls.Add((Control) this._upDownBetweenArtVariants);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this._label10);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this._upDownBeforeAdd2);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this._label11);
    this.Controls.Add((Control) this._upDownBeforeAdditionalChapter);
    this.Controls.Add((Control) this._label12);
    this.Controls.Add((Control) this._upDownBeforeAdd1);
    this.Controls.Add((Control) this._label13);
    this.Controls.Add((Control) this._upDownAfterNote);
    this.Controls.Add((Control) this._label14);
    this.Controls.Add((Control) this._upDownBeforeNote);
    this.Controls.Add((Control) this._label15);
    this.Controls.Add((Control) this._upDownAfterSectionName);
    this.Controls.Add((Control) this._label16);
    this.Controls.Add((Control) this._upDownBeforeSectionName);
    this.Controls.Add((Control) this._label7);
    this.Controls.Add((Control) this._upDownAfterVariantNumber);
    this.Controls.Add((Control) this._label8);
    this.Controls.Add((Control) this._upDownBeforeVariantNumber);
    this.Controls.Add((Control) this._label5);
    this.Controls.Add((Control) this._upDownAfterVariableData);
    this.Controls.Add((Control) this._label6);
    this.Controls.Add((Control) this._upDownBeforeVariableData);
    this.Controls.Add((Control) this._label3);
    this.Controls.Add((Control) this._upDownBetweenSameObjTypes);
    this.Controls.Add((Control) this._label4);
    this.Controls.Add((Control) this._upDownBetweenDifferentObjTypes);
    this.Controls.Add((Control) this._btnSameDesiognationSetup);
    this.Controls.Add((Control) this._label2);
    this.Controls.Add((Control) this._upDownBetweenSameDesignations);
    this.Controls.Add((Control) this._label1);
    this.Controls.Add((Control) this._upDownBetweenDifferentDesignations);
    this.MinimumSize = new Size(615, 365);
    this.Name = nameof (UserControlSetupSkipLines);
    this.Size = new Size(615, 412);
    this._upDownBetweenDifferentDesignations.Properties.EndInit();
    this._upDownBetweenSameDesignations.Properties.EndInit();
    this._upDownBetweenSameObjTypes.Properties.EndInit();
    this._upDownBetweenDifferentObjTypes.Properties.EndInit();
    this._upDownAfterVariableData.Properties.EndInit();
    this._upDownBeforeVariableData.Properties.EndInit();
    this._upDownAfterVariantNumber.Properties.EndInit();
    this._upDownBeforeVariantNumber.Properties.EndInit();
    this._upDownBetweenArtVariants.Properties.EndInit();
    this._upDownBeforeAdd2.Properties.EndInit();
    this._upDownBeforeAdd1.Properties.EndInit();
    this._upDownAfterNote.Properties.EndInit();
    this._upDownBeforeNote.Properties.EndInit();
    this._upDownAfterSectionName.Properties.EndInit();
    this._upDownBeforeSectionName.Properties.EndInit();
    this._upDownAfterAdd1.Properties.EndInit();
    this._upDownAfterAdd2.Properties.EndInit();
    this._upDownBeforeAdditionalChapter.Properties.EndInit();
    this._upDownAfterAdditionalChapter.Properties.EndInit();
    this._upDownAfterDynamicGroup.Properties.EndInit();
    this._upDownBeforeDynamicGroup.Properties.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Схема пропуска строк </summary>
  public SkipLinesSchema RootSkipLinesSchema
  {
    get => this.rootSkipLinesSchema;
    set => this.rootSkipLinesSchema = value;
  }

  /// <summary> Схема пропуска строк </summary>
  public SkipLinesSchema SkipLinesSchema
  {
    get => this._skipLinesSchema;
    set
    {
      this.LockControls();
      try
      {
        this._skipLinesSchema = value;
        this.Changed = false;
        this.RefreshReadOnly();
        this.UpdateControls(true);
        this.RaiseOnInitDataEvent((object) this._skipLinesSchema);
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
    this._upDownBetweenDifferentDesignations.Properties.ReadOnly = this.ReadOnly;
    this._upDownBetweenSameDesignations.Properties.ReadOnly = this.ReadOnly;
    this._upDownBetweenDifferentObjTypes.Properties.ReadOnly = this.ReadOnly;
    this._upDownBetweenSameObjTypes.Properties.ReadOnly = this.ReadOnly;
    this._upDownAfterVariableData.Properties.ReadOnly = this.ReadOnly;
    this._upDownBeforeVariableData.Properties.ReadOnly = this.ReadOnly;
    this._upDownAfterVariantNumber.Properties.ReadOnly = this.ReadOnly;
    this._upDownBeforeVariantNumber.Properties.ReadOnly = this.ReadOnly;
    this._upDownBetweenArtVariants.Properties.ReadOnly = this.ReadOnly;
    this._upDownBeforeAdd2.Properties.ReadOnly = this.ReadOnly;
    this._upDownAfterAdd1.Properties.ReadOnly = this.ReadOnly;
    this._upDownBeforeAdd1.Properties.ReadOnly = this.ReadOnly;
    this._upDownAfterNote.Properties.ReadOnly = this.ReadOnly;
    this._upDownBeforeNote.Properties.ReadOnly = this.ReadOnly;
    this._upDownAfterDynamicGroup.Properties.ReadOnly = this.ReadOnly;
    this._upDownBeforeDynamicGroup.Properties.ReadOnly = this.ReadOnly;
    this._upDownAfterSectionName.Properties.ReadOnly = this.ReadOnly;
    this._upDownBeforeSectionName.Properties.ReadOnly = this.ReadOnly;
    this._upDownAfterAdditionalChapter.Properties.ReadOnly = this.ReadOnly;
    this._upDownBeforeAdditionalChapter.Properties.ReadOnly = this.ReadOnly;
    this.cbNumberingPositions.Enabled = !this.ReadOnly;
    this._upDownBetweenDifferentDesignations.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBetweenSameDesignations.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBetweenDifferentObjTypes.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBetweenSameObjTypes.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownAfterVariableData.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBeforeVariableData.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownAfterVariantNumber.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBeforeVariantNumber.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBetweenArtVariants.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBeforeAdd2.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownAfterAdd1.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBeforeAdd1.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownAfterNote.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBeforeNote.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownAfterDynamicGroup.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBeforeDynamicGroup.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownAfterSectionName.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBeforeSectionName.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownAfterAdditionalChapter.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBeforeAdditionalChapter.Properties.Buttons[0].Visible = !this.ReadOnly;
    this._upDownBetweenDifferentDesignations.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBetweenSameDesignations.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBetweenDifferentObjTypes.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBetweenSameObjTypes.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownAfterVariableData.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBeforeVariableData.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownAfterVariantNumber.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBeforeVariantNumber.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBetweenArtVariants.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBeforeAdd2.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownAfterAdd1.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBeforeAdd1.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownAfterNote.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBeforeNote.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownAfterDynamicGroup.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBeforeDynamicGroup.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownAfterSectionName.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBeforeSectionName.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownAfterAdditionalChapter.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownBeforeAdditionalChapter.BackColor = this.ReadOnly ? SystemColors.Control : SystemColors.Window;
    if (this._skipLinesSchema == null)
    {
      this._upDownBetweenDifferentDesignations.Text = string.Empty;
      this._upDownBetweenSameDesignations.Text = string.Empty;
      this._upDownBetweenDifferentObjTypes.Text = string.Empty;
      this._upDownBetweenSameObjTypes.Text = string.Empty;
      this._upDownAfterVariableData.Text = string.Empty;
      this._upDownBeforeVariableData.Text = string.Empty;
      this._upDownAfterVariantNumber.Text = string.Empty;
      this._upDownBeforeVariantNumber.Text = string.Empty;
      this._upDownBetweenArtVariants.Text = string.Empty;
      this._upDownBeforeAdd2.Text = string.Empty;
      this._upDownAfterAdd1.Text = string.Empty;
      this._upDownBeforeAdd1.Text = string.Empty;
      this._upDownAfterNote.Text = string.Empty;
      this._upDownBeforeNote.Text = string.Empty;
      this._upDownAfterDynamicGroup.Text = string.Empty;
      this._upDownBeforeDynamicGroup.Text = string.Empty;
      this._upDownAfterSectionName.Text = string.Empty;
      this._upDownBeforeSectionName.Text = string.Empty;
      this._upDownAfterAdditionalChapter.Text = string.Empty;
      this._upDownBeforeAdditionalChapter.Text = string.Empty;
      this.cbNonSkipAtStartPage.Checked = false;
      this.cbNumberingPositions.SelectedValue = (object) null;
    }
    else
    {
      this._upDownBetweenDifferentDesignations.Value = (Decimal) this._skipLinesSchema.BetweenDifferentDesignations;
      this._upDownBetweenSameDesignations.Value = (Decimal) this._skipLinesSchema.BetweenSameDesignations;
      this._upDownBetweenDifferentObjTypes.Value = (Decimal) this._skipLinesSchema.BetweenDifferentObjTypes;
      this._upDownBetweenSameObjTypes.Value = (Decimal) this._skipLinesSchema.BetweenSameObjTypes;
      this._upDownAfterVariableData.Value = (Decimal) this._skipLinesSchema.AfterVariableData;
      this._upDownBeforeVariableData.Value = (Decimal) this._skipLinesSchema.BeforeVariableData;
      this._upDownAfterVariantNumber.Value = (Decimal) this._skipLinesSchema.AfterVariantNumber;
      this._upDownBeforeVariantNumber.Value = (Decimal) this._skipLinesSchema.BeforeVariantNumber;
      this._upDownBetweenArtVariants.Value = (Decimal) this._skipLinesSchema.BetweenArtVariants;
      this._upDownBeforeAdd2.Value = (Decimal) this._skipLinesSchema.BeforeAdd2;
      this._upDownAfterAdd1.Value = (Decimal) this._skipLinesSchema.AfterAdd1;
      this._upDownBeforeAdd1.Value = (Decimal) this._skipLinesSchema.BeforeAdd1;
      this._upDownAfterNote.Value = (Decimal) this._skipLinesSchema.AfterNote;
      this._upDownBeforeNote.Value = (Decimal) this._skipLinesSchema.BeforeNote;
      this._upDownAfterDynamicGroup.Value = (Decimal) this._skipLinesSchema.AfterDynamicGroup;
      this._upDownBeforeDynamicGroup.Value = (Decimal) this._skipLinesSchema.BeforeDynamicGroup;
      this._upDownAfterSectionName.Value = (Decimal) this._skipLinesSchema.AfterSectionName;
      this._upDownBeforeSectionName.Value = (Decimal) this._skipLinesSchema.BeforeSectionName;
      this._upDownAfterAdditionalChapter.Value = (Decimal) this._skipLinesSchema.AfterAdditional;
      this._upDownBeforeAdditionalChapter.Value = (Decimal) this._skipLinesSchema.BeforeAdditional;
      this.cbNonSkipAtStartPage.Checked = this._skipLinesSchema.NonSkipBeforeAtStartPage;
      this.cbNumberingPositions.SelectedIndex = (int) this._skipLinesSchema.NumberingPositions;
    }
    this.RefreshControlBold((Control) null);
    this._btnReset.Enabled = !this.ReadOnly;
    this._btnSameDesiognationSetup.Enabled = this._skipLinesSchema != null && this._skipLinesSchema.CompareDesignationSchema != null;
    bool flag = this._skipLinesSchema != null && this._skipLinesSchema.Parent != null && this._skipLinesSchema.CompareDesignationSchema != null && this._skipLinesSchema.CompareDesignationSchema.Changed;
    if ((!this._btnSameDesiognationSetup.Font.Bold || flag) && !(!this._btnSameDesiognationSetup.Font.Bold & flag))
      return;
    this._btnSameDesiognationSetup.Font = new Font(this._btnSameDesiognationSetup.Font.FontFamily, this._btnSameDesiognationSetup.Font.SizeInPoints, flag ? FontStyle.Bold : FontStyle.Regular, this._btnSameDesiognationSetup.Font.Unit, this._btnSameDesiognationSetup.Font.GdiCharSet, this._btnSameDesiognationSetup.Font.GdiVerticalFont);
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly()
  {
    return this._skipLinesSchema == null || this._skipLinesSchema.ReadOnly;
  }

  /// <summary>Обновление параметра Bold у шрифта control</summary>
  /// <param name="control">control, у которого надо обновить Bold. Если = null, то обновляется у всех</param>
  public void RefreshControlBold(Control control)
  {
    if (this._skipLinesSchema == null)
      return;
    if (control == null || this._upDownBetweenDifferentDesignations == control)
      this.ChangeControlFontBold((Control) this._upDownBetweenDifferentDesignations, this._skipLinesSchema.BetweenDifferentDesignationsChanged);
    if (control == null || this._upDownBetweenSameDesignations == control)
      this.ChangeControlFontBold((Control) this._upDownBetweenSameDesignations, this._skipLinesSchema.BetweenSameDesignationsChanged);
    if (control == null || this._upDownBetweenDifferentObjTypes == control)
      this.ChangeControlFontBold((Control) this._upDownBetweenDifferentObjTypes, this._skipLinesSchema.BetweenDifferentObjTypesChanged);
    if (control == null || this._upDownBetweenSameObjTypes == control)
      this.ChangeControlFontBold((Control) this._upDownBetweenSameObjTypes, this._skipLinesSchema.BetweenSameObjTypesChanged);
    if (control == null || this._upDownAfterVariableData == control)
      this.ChangeControlFontBold((Control) this._upDownAfterVariableData, this._skipLinesSchema.AfterVariableDataChanged);
    if (control == null || this._upDownBeforeVariableData == control)
      this.ChangeControlFontBold((Control) this._upDownBeforeVariableData, this._skipLinesSchema.BeforeVariableDataChanged);
    if (control == null || this._upDownAfterVariantNumber == control)
      this.ChangeControlFontBold((Control) this._upDownAfterVariantNumber, this._skipLinesSchema.AfterVariantNumberChanged);
    if (control == null || this._upDownBeforeVariantNumber == control)
      this.ChangeControlFontBold((Control) this._upDownBeforeVariantNumber, this._skipLinesSchema.BeforeVariantNumberChanged);
    if (control == null || this._upDownBetweenArtVariants == control)
      this.ChangeControlFontBold((Control) this._upDownBetweenArtVariants, this._skipLinesSchema.BetweenArtVariantsChanged);
    if (control == null || this._upDownBeforeAdd2 == control)
      this.ChangeControlFontBold((Control) this._upDownBeforeAdd2, this._skipLinesSchema.BeforeAdd2Changed);
    if (control == null || this._upDownAfterAdd1 == control)
      this.ChangeControlFontBold((Control) this._upDownAfterAdd1, this._skipLinesSchema.AfterAdd1Changed);
    if (control == null || this._upDownBeforeAdd1 == control)
      this.ChangeControlFontBold((Control) this._upDownBeforeAdd1, this._skipLinesSchema.BeforeAdd1Changed);
    if (control == null || this._upDownAfterNote == control)
      this.ChangeControlFontBold((Control) this._upDownAfterNote, this._skipLinesSchema.AfterNoteChanged);
    if (control == null || this._upDownBeforeNote == control)
      this.ChangeControlFontBold((Control) this._upDownBeforeNote, this._skipLinesSchema.BeforeNoteChanged);
    if (control == null || this._upDownAfterDynamicGroup == control)
      this.ChangeControlFontBold((Control) this._upDownAfterDynamicGroup, this._skipLinesSchema.AfterDynamicGroupChanged);
    if (control == null || this._upDownBeforeDynamicGroup == control)
      this.ChangeControlFontBold((Control) this._upDownBeforeDynamicGroup, this._skipLinesSchema.BeforeDynamicGroupChanged);
    if (control == null || this._upDownAfterSectionName == control)
      this.ChangeControlFontBold((Control) this._upDownAfterSectionName, this._skipLinesSchema.AfterSectionNameChanged);
    if (control == null || this._upDownBeforeSectionName == control)
      this.ChangeControlFontBold((Control) this._upDownBeforeSectionName, this._skipLinesSchema.BeforeSectionNameChanged);
    if (control == null || this._upDownAfterAdditionalChapter == control)
      this.ChangeControlFontBold((Control) this._upDownAfterAdditionalChapter, this._skipLinesSchema.AfterAdditionalChanged);
    if (control == null || this._upDownBeforeAdditionalChapter == control)
      this.ChangeControlFontBold((Control) this._upDownBeforeAdditionalChapter, this._skipLinesSchema.BeforeAdditionalChanged);
    if (control == null || this.cbNumberingPositions == control)
      this.ChangeControlFontBold((Control) this.cbNumberingPositions, this._skipLinesSchema.NumberingPositionsChanged);
    if (control != null && this.cbNonSkipAtStartPage != control)
      return;
    this.ChangeControlFontBold((Control) this.cbNonSkipAtStartPage, this._skipLinesSchema.NonSkipBeforeAtStartPageChanged);
  }

  private void ChangeControlFontBold(Control control, bool mustBeBold)
  {
    if (control.Font.Bold == mustBeBold)
      return;
    control.Font = new Font(control.Font.FontFamily, control.Font.SizeInPoints, mustBeBold ? FontStyle.Bold : FontStyle.Regular, control.Font.Unit, control.Font.GdiCharSet, control.Font.GdiVerticalFont);
  }

  private void BeforeChangeUpDown(SpinEdit spinEdit, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this._skipLinesSchema == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(spinEdit.Value);
  }

  private bool BeforeUpDownEdit()
  {
    if (this._skipLinesSchema == null || this.ControlsAreUpdating)
      return false;
    bool wasUpdated = false;
    return !(!this.CheckCanEdit(ref wasUpdated) | wasUpdated);
  }

  private void AfterUpDownEdit() => this.Changed = true;

  private void UpDown_EditValueChanging(object sender, ChangingEventArgs e)
  {
    this.BeforeChangeUpDown((SpinEdit) sender, e);
  }

  private void _upDownBetweenDifferentDesignations_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BetweenDifferentDesignations = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBetweenSameDesignations_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BetweenSameDesignations = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBetweenDifferentObjTypes_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BetweenDifferentObjTypes = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBetweenSameObjTypes_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BetweenSameObjTypes = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBeforeVariableData_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BeforeVariableData = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownAfterVariableData_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.AfterVariableData = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBeforeVariantNumber_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BeforeVariantNumber = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownAfterVariantNumber_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.AfterVariantNumber = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBeforeSectionName_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BeforeSectionName = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownAfterSectionName_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.AfterSectionName = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBeforeNote_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BeforeNote = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownAfterNote_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.AfterNote = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBeforeAdd1_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BeforeAdd1 = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownAfterAdd1_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.AfterAdd1 = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBeforeAdd2_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BeforeAdd2 = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBetweenArtVariants_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BetweenArtVariants = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _btnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || MessageBox.Show("Сбросить изменения в схеме пропуска строк к значениям по умолчанию?", "Схема пропуска строк", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockControls();
    try
    {
      this._skipLinesSchema.LoadDefaultParams();
      this.UpdateControls(true);
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void _btnSameDesiognationSetup_Click(object sender, EventArgs e)
  {
    int num = (int) new SameDesignationsSetupForm((Control) this, this._skipLinesSchema.CompareDesignationSchema, (IStructualControlSupport) this).ShowDialog();
    this.UpdateControls(true);
  }

  private void bIspoln_Click(object sender, EventArgs e)
  {
    FormSetupDesignationTrim setupDesignationTrim = (FormSetupDesignationTrim) null;
    if (this.RootSkipLinesSchema == null)
      return;
    if (this.RootSkipLinesSchema.Level.InheritanceLevel == InheritanceSettingsLevel.Document)
      setupDesignationTrim = new FormSetupDesignationTrim(this.RootSkipLinesSchema.Level.SettingsStructure, this.RootSkipLinesSchema.OwnerObjectID, this.RootSkipLinesSchema.Parent.OwnerObjectID);
    else if (this.RootSkipLinesSchema.Level.InheritanceLevel == InheritanceSettingsLevel.Template)
      setupDesignationTrim = new FormSetupDesignationTrim((SettingsStructure) null, this.RootSkipLinesSchema.OwnerObjectID);
    if (setupDesignationTrim == null || setupDesignationTrim.ShowDialog() != DialogResult.OK)
      return;
    AVSWindow activeAvsWindow = AVSPlugin.Instance.ActiveAVSWindow;
    if (activeAvsWindow == null)
      return;
    activeAvsWindow.AVSDocument.designationTrimSchema = setupDesignationTrim.DesignationTrimSchema;
    if (this.ReadOnly)
      return;
    activeAvsWindow.AVSDocument.UpdatePartProductCaptions();
    activeAvsWindow.AVSDocument.UpdateProductHeadersOnPages(true, true);
  }

  private void cbNonSkipAtStartPage_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.NonSkipBeforeAtStartPage = ((CheckBox) sender).Checked;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBeforeAdditional_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BeforeAdditional = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownAfterAdditional_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.AfterAdditional = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void cbNumberingPositions_SelectedIndexChanged(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this._skipLinesSchema == null)
      return;
    int numberingPositions = (int) this._skipLinesSchema.NumberingPositions;
    if (this.ControlsAreUpdating || numberingPositions == this.cbNumberingPositions.SelectedIndex)
      return;
    if (!this.CheckCanEdit(ref wasUpdated))
    {
      this.cbNumberingPositions.SelectedIndex = numberingPositions;
    }
    else
    {
      if (!this.BeforeUpDownEdit())
        return;
      this._skipLinesSchema.NumberingPositions = (NumberingPositionsEnum) this.cbNumberingPositions.SelectedIndex;
      this.AfterUpDownEdit();
      this.RefreshControlBold(sender as Control);
    }
  }

  private void _upDownAfterDynamicGroup_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.AfterDynamicGroup = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }

  private void _upDownBeforeDynamicGroup_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      return;
    this._skipLinesSchema.BeforeDynamicGroup = (int) ((SpinEdit) sender).Value;
    this.AfterUpDownEdit();
    this.RefreshControlBold((Control) sender);
  }
}
