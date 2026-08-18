// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcNumerationRules.NumRuleForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcNumerationRules;

/// <summary>Форма для параметров правила нумерации</summary>
public class NumRuleForm : UserControl, IView
{
  /// <summary>Ид. правила нумерации</summary>
  private long _objectId;
  /// <summary>Guid нумеруемого типа атрибута</summary>
  private Guid _attributeGuid;
  /// <summary>Признак изменения данных</summary>
  private bool _modified;
  /// <summary>Правила нумерации</summary>
  private TechNumerationRule _numRule;
  /// <summary>Элемент правила нумерации</summary>
  private TechNumerationNode _numNode;
  /// <summary>Область нумерации</summary>
  private TechNumerationObjectModes _objectMode;
  /// <summary>Required designer variables</summary>
  private GroupBox grbRule;
  private CheckBox chbxVarNumberBaseObject;
  private TextBox tbxVarCharSeparator;
  private TextBox tbxMainStep;
  private Label lblMainStep;
  private Label lblMainCharList;
  private TextBox tbxMainCharList;
  private TextBox tbxMainDigitsCount;
  private Label lblMainDigitsCount;
  private Label lblMainFirstNumber;
  private System.ComponentModel.Container components;
  private ComboBox cbMainNumType;
  private Label lblMainNumType;
  private TextBox tbxMainFirstNumber;
  private TableLayoutPanel plpnlMain;
  internal ComboBox cbNumMethod;
  private Label lblNumMethod;
  internal ComboBox cbNumObjectMode;
  private Label lblNumObjectMode;
  private GroupBox grbObject;
  private TableLayoutPanel tlpnlProperies;
  private Button tbnAttribute;
  private TextBox tbxAttribute;
  private Label lbl;
  private TabControl tcNumRule;
  private TabPage tpNumRuleMain;
  private TabPage tpNumRuleVariat;
  private TableLayoutPanel tlpnlNumRuleMain;
  private ComboBox cbMainArea;
  private Label lblMainArea;
  private TableLayoutPanel tableLayoutPanel1;
  private ComboBox cbVarNumType;
  private Label lblVarNumType;
  private Label lblVarFirstNumber;
  private TextBox tbxVarCharList;
  private Label lblVarCharList;
  private TextBox tbxVarDigitsCount;
  private Label lblVarStep;
  private Label lblVarDigitsCount;
  private TextBox tbxVarFirstNumber;
  private Panel pnlButtons;
  internal Button btnCancel;
  internal Button btnApply;
  private TextBox tbxVarStep;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NumRuleForm));
    this.grbRule = new GroupBox();
    this.plpnlMain = new TableLayoutPanel();
    this.cbNumObjectMode = new ComboBox();
    this.cbNumMethod = new ComboBox();
    this.grbObject = new GroupBox();
    this.tlpnlProperies = new TableLayoutPanel();
    this.tbnAttribute = new Button();
    this.tbxAttribute = new TextBox();
    this.lbl = new Label();
    this.tcNumRule = new TabControl();
    this.tpNumRuleMain = new TabPage();
    this.tlpnlNumRuleMain = new TableLayoutPanel();
    this.cbMainArea = new ComboBox();
    this.lblMainArea = new Label();
    this.cbMainNumType = new ComboBox();
    this.lblMainNumType = new Label();
    this.lblMainFirstNumber = new Label();
    this.tbxMainCharList = new TextBox();
    this.lblMainCharList = new Label();
    this.tbxMainDigitsCount = new TextBox();
    this.lblMainStep = new Label();
    this.lblMainDigitsCount = new Label();
    this.tbxMainFirstNumber = new TextBox();
    this.tbxMainStep = new TextBox();
    this.tpNumRuleVariat = new TabPage();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.cbVarNumType = new ComboBox();
    this.lblVarNumType = new Label();
    this.chbxVarNumberBaseObject = new CheckBox();
    this.lblVarFirstNumber = new Label();
    this.tbxVarCharSeparator = new TextBox();
    this.tbxVarCharList = new TextBox();
    this.lblVarCharList = new Label();
    this.tbxVarDigitsCount = new TextBox();
    this.lblVarStep = new Label();
    this.lblVarDigitsCount = new Label();
    this.tbxVarFirstNumber = new TextBox();
    this.tbxVarStep = new TextBox();
    this.lblNumMethod = new Label();
    this.lblNumObjectMode = new Label();
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.grbRule.SuspendLayout();
    this.plpnlMain.SuspendLayout();
    this.grbObject.SuspendLayout();
    this.tlpnlProperies.SuspendLayout();
    this.tcNumRule.SuspendLayout();
    this.tpNumRuleMain.SuspendLayout();
    this.tlpnlNumRuleMain.SuspendLayout();
    this.tpNumRuleVariat.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.grbRule, "grbRule");
    this.grbRule.Controls.Add((Control) this.plpnlMain);
    this.grbRule.Name = "grbRule";
    this.grbRule.TabStop = false;
    componentResourceManager.ApplyResources((object) this.plpnlMain, "plpnlMain");
    this.plpnlMain.Controls.Add((Control) this.cbNumObjectMode, 1, 3);
    this.plpnlMain.Controls.Add((Control) this.cbNumMethod, 1, 1);
    this.plpnlMain.Controls.Add((Control) this.grbObject, 0, 5);
    this.plpnlMain.Controls.Add((Control) this.lblNumMethod, 0, 1);
    this.plpnlMain.Controls.Add((Control) this.lblNumObjectMode, 0, 3);
    this.plpnlMain.Name = "plpnlMain";
    componentResourceManager.ApplyResources((object) this.cbNumObjectMode, "cbNumObjectMode");
    this.cbNumObjectMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbNumObjectMode.Name = "cbNumObjectMode";
    componentResourceManager.ApplyResources((object) this.cbNumMethod, "cbNumMethod");
    this.cbNumMethod.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbNumMethod.Name = "cbNumMethod";
    componentResourceManager.ApplyResources((object) this.grbObject, "grbObject");
    this.plpnlMain.SetColumnSpan((Control) this.grbObject, 2);
    this.grbObject.Controls.Add((Control) this.tlpnlProperies);
    this.grbObject.Name = "grbObject";
    this.grbObject.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tlpnlProperies, "tlpnlProperies");
    this.tlpnlProperies.Controls.Add((Control) this.tbnAttribute, 2, 1);
    this.tlpnlProperies.Controls.Add((Control) this.tbxAttribute, 1, 1);
    this.tlpnlProperies.Controls.Add((Control) this.lbl, 0, 1);
    this.tlpnlProperies.Controls.Add((Control) this.tcNumRule, 0, 3);
    this.tlpnlProperies.Name = "tlpnlProperies";
    componentResourceManager.ApplyResources((object) this.tbnAttribute, "tbnAttribute");
    this.tbnAttribute.Name = "tbnAttribute";
    this.tbnAttribute.Click += new EventHandler(this.tbnAttribute_Click);
    componentResourceManager.ApplyResources((object) this.tbxAttribute, "tbxAttribute");
    this.tbxAttribute.Name = "tbxAttribute";
    this.tbxAttribute.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.lbl, "lbl");
    this.lbl.Name = "lbl";
    this.lbl.Tag = (object) "";
    this.tlpnlProperies.SetColumnSpan((Control) this.tcNumRule, 3);
    this.tcNumRule.Controls.Add((Control) this.tpNumRuleMain);
    this.tcNumRule.Controls.Add((Control) this.tpNumRuleVariat);
    componentResourceManager.ApplyResources((object) this.tcNumRule, "tcNumRule");
    this.tcNumRule.Name = "tcNumRule";
    this.tcNumRule.SelectedIndex = 0;
    this.tpNumRuleMain.Controls.Add((Control) this.tlpnlNumRuleMain);
    componentResourceManager.ApplyResources((object) this.tpNumRuleMain, "tpNumRuleMain");
    this.tpNumRuleMain.Name = "tpNumRuleMain";
    this.tpNumRuleMain.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tlpnlNumRuleMain, "tlpnlNumRuleMain");
    this.tlpnlNumRuleMain.Controls.Add((Control) this.cbMainArea, 1, 1);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.lblMainArea, 0, 1);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.cbMainNumType, 1, 3);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.lblMainNumType, 0, 3);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.lblMainFirstNumber, 0, 9);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.tbxMainCharList, 1, 5);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.lblMainCharList, 0, 5);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.tbxMainDigitsCount, 1, 7);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.lblMainStep, 0, 11);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.lblMainDigitsCount, 0, 7);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.tbxMainFirstNumber, 1, 9);
    this.tlpnlNumRuleMain.Controls.Add((Control) this.tbxMainStep, 1, 11);
    this.tlpnlNumRuleMain.Name = "tlpnlNumRuleMain";
    componentResourceManager.ApplyResources((object) this.cbMainArea, "cbMainArea");
    this.cbMainArea.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbMainArea.Name = "cbMainArea";
    componentResourceManager.ApplyResources((object) this.lblMainArea, "lblMainArea");
    this.lblMainArea.Name = "lblMainArea";
    componentResourceManager.ApplyResources((object) this.cbMainNumType, "cbMainNumType");
    this.cbMainNumType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbMainNumType.Name = "cbMainNumType";
    componentResourceManager.ApplyResources((object) this.lblMainNumType, "lblMainNumType");
    this.lblMainNumType.Name = "lblMainNumType";
    componentResourceManager.ApplyResources((object) this.lblMainFirstNumber, "lblMainFirstNumber");
    this.lblMainFirstNumber.Name = "lblMainFirstNumber";
    this.lblMainFirstNumber.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxMainCharList, "tbxMainCharList");
    this.tbxMainCharList.Name = "tbxMainCharList";
    componentResourceManager.ApplyResources((object) this.lblMainCharList, "lblMainCharList");
    this.lblMainCharList.Name = "lblMainCharList";
    this.lblMainCharList.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxMainDigitsCount, "tbxMainDigitsCount");
    this.tbxMainDigitsCount.Name = "tbxMainDigitsCount";
    componentResourceManager.ApplyResources((object) this.lblMainStep, "lblMainStep");
    this.lblMainStep.Name = "lblMainStep";
    this.lblMainStep.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.lblMainDigitsCount, "lblMainDigitsCount");
    this.lblMainDigitsCount.Name = "lblMainDigitsCount";
    this.lblMainDigitsCount.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxMainFirstNumber, "tbxMainFirstNumber");
    this.tbxMainFirstNumber.Name = "tbxMainFirstNumber";
    componentResourceManager.ApplyResources((object) this.tbxMainStep, "tbxMainStep");
    this.tbxMainStep.Name = "tbxMainStep";
    this.tpNumRuleVariat.Controls.Add((Control) this.tableLayoutPanel1);
    componentResourceManager.ApplyResources((object) this.tpNumRuleVariat, "tpNumRuleVariat");
    this.tpNumRuleVariat.Name = "tpNumRuleVariat";
    this.tpNumRuleVariat.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.cbVarNumType, 1, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.lblVarNumType, 0, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.chbxVarNumberBaseObject, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.lblVarFirstNumber, 0, 9);
    this.tableLayoutPanel1.Controls.Add((Control) this.tbxVarCharSeparator, 2, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.tbxVarCharList, 1, 5);
    this.tableLayoutPanel1.Controls.Add((Control) this.lblVarCharList, 0, 5);
    this.tableLayoutPanel1.Controls.Add((Control) this.tbxVarDigitsCount, 1, 7);
    this.tableLayoutPanel1.Controls.Add((Control) this.lblVarStep, 0, 11);
    this.tableLayoutPanel1.Controls.Add((Control) this.lblVarDigitsCount, 0, 7);
    this.tableLayoutPanel1.Controls.Add((Control) this.tbxVarFirstNumber, 1, 9);
    this.tableLayoutPanel1.Controls.Add((Control) this.tbxVarStep, 1, 11);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this.cbVarNumType, "cbVarNumType");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.cbVarNumType, 2);
    this.cbVarNumType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbVarNumType.Name = "cbVarNumType";
    componentResourceManager.ApplyResources((object) this.lblVarNumType, "lblVarNumType");
    this.lblVarNumType.Name = "lblVarNumType";
    this.tableLayoutPanel1.SetColumnSpan((Control) this.chbxVarNumberBaseObject, 2);
    componentResourceManager.ApplyResources((object) this.chbxVarNumberBaseObject, "chbxVarNumberBaseObject");
    this.chbxVarNumberBaseObject.Name = "chbxVarNumberBaseObject";
    componentResourceManager.ApplyResources((object) this.lblVarFirstNumber, "lblVarFirstNumber");
    this.lblVarFirstNumber.Name = "lblVarFirstNumber";
    this.lblVarFirstNumber.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxVarCharSeparator, "tbxVarCharSeparator");
    this.tbxVarCharSeparator.Name = "tbxVarCharSeparator";
    componentResourceManager.ApplyResources((object) this.tbxVarCharList, "tbxVarCharList");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.tbxVarCharList, 2);
    this.tbxVarCharList.Name = "tbxVarCharList";
    componentResourceManager.ApplyResources((object) this.lblVarCharList, "lblVarCharList");
    this.lblVarCharList.Name = "lblVarCharList";
    this.lblVarCharList.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxVarDigitsCount, "tbxVarDigitsCount");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.tbxVarDigitsCount, 2);
    this.tbxVarDigitsCount.Name = "tbxVarDigitsCount";
    componentResourceManager.ApplyResources((object) this.lblVarStep, "lblVarStep");
    this.lblVarStep.Name = "lblVarStep";
    this.lblVarStep.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.lblVarDigitsCount, "lblVarDigitsCount");
    this.lblVarDigitsCount.Name = "lblVarDigitsCount";
    this.lblVarDigitsCount.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxVarFirstNumber, "tbxVarFirstNumber");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.tbxVarFirstNumber, 2);
    this.tbxVarFirstNumber.Name = "tbxVarFirstNumber";
    componentResourceManager.ApplyResources((object) this.tbxVarStep, "tbxVarStep");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.tbxVarStep, 2);
    this.tbxVarStep.Name = "tbxVarStep";
    componentResourceManager.ApplyResources((object) this.lblNumMethod, "lblNumMethod");
    this.lblNumMethod.Name = "lblNumMethod";
    componentResourceManager.ApplyResources((object) this.lblNumObjectMode, "lblNumObjectMode");
    this.lblNumObjectMode.Name = "lblNumObjectMode";
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.Controls.Add((Control) this.pnlButtons);
    this.Controls.Add((Control) this.grbRule);
    this.Name = nameof (NumRuleForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.grbRule.ResumeLayout(false);
    this.plpnlMain.ResumeLayout(false);
    this.plpnlMain.PerformLayout();
    this.grbObject.ResumeLayout(false);
    this.tlpnlProperies.ResumeLayout(false);
    this.tlpnlProperies.PerformLayout();
    this.tcNumRule.ResumeLayout(false);
    this.tpNumRuleMain.ResumeLayout(false);
    this.tlpnlNumRuleMain.ResumeLayout(false);
    this.tlpnlNumRuleMain.PerformLayout();
    this.tpNumRuleVariat.ResumeLayout(false);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Initialize class data</summary>
  private void InitData()
  {
    this.FillComboBox(this.cbNumObjectMode, new List<object>()
    {
      (object) EnumTypeHelper.GetCaption((Enum) TechNumerationObjectModes.FirstObj),
      (object) EnumTypeHelper.GetCaption((Enum) TechNumerationObjectModes.CurrentObj)
    }.ToArray(), (object) string.Empty);
  }

  /// <summary>Заполнение</summary>
  /// <param name="control"></param>
  /// <param name="values"></param>
  /// <param name="selValue"></param>
  private void FillComboBox(ComboBox control, object[] values, object selValue)
  {
    if (control == null)
      return;
    control.BeginUpdate();
    try
    {
      control.SelectedValueChanged -= new EventHandler(this.cb_SelectedValueChanged);
      control.Items.Clear();
      if (values != null)
        control.Items.AddRange(values);
      int num = control.Items.IndexOf(selValue);
      if (num != -1)
        control.SelectedIndex = num;
      else
        control.SelectedIndex = control.Items.Count > 0 ? 0 : control.SelectedIndex;
    }
    finally
    {
      control.SelectedValueChanged += new EventHandler(this.cb_SelectedValueChanged);
      control.EndUpdate();
    }
  }

  /// <summary>Заполнение текст бокса</summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  private void FillTextBox(TextBox control, object value)
  {
    if (control == null)
      return;
    control.TextChanged -= new EventHandler(this.tbx_TextChanged);
    try
    {
      control.Text = value != null ? value.ToString() : string.Empty;
    }
    finally
    {
      control.TextChanged += new EventHandler(this.tbx_TextChanged);
    }
  }

  /// <summary>Конструктор</summary>
  public NumRuleForm()
  {
    this.InitializeComponent();
    this.InitData();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.components?.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Правило нумерации</summary>
  public TechNumerationRule NumRule
  {
    get => this._numRule;
    set
    {
      if (this._numRule == value)
        return;
      this._numRule = value;
      this.DataLoad(value);
    }
  }

  /// <summary>Элемент правила нумерации</summary>
  public TechNumerationNode NumNode
  {
    get => this._numNode;
    set
    {
      if (this._numNode == value)
        return;
      this._numNode = value;
      this.DataLoad(value);
    }
  }

  /// <summary>Метод нумерации</summary>
  public TechNumerationMethods NumMethod
  {
    get
    {
      TechNumerationMethods numMethod = TechNumerationMethods.Auto;
      if (this.cbNumMethod.SelectedItem != null)
        numMethod = (TechNumerationMethods) EnumTypeHelper.GetEnumValue(typeof (TechNumerationMethods), this.cbNumMethod.SelectedItem.ToString(), (object) TechNumerationMethods.Auto);
      return numMethod;
    }
  }

  /// <summary>Область нумерации</summary>
  public TechNumerationObjectModes ObjectMode
  {
    get
    {
      if (this.cbNumObjectMode.SelectedItem != null)
        this._objectMode = (TechNumerationObjectModes) EnumTypeHelper.GetEnumValue(typeof (TechNumerationObjectModes), this.cbNumObjectMode.SelectedItem.ToString(), (object) TechNumerationObjectModes.FirstObj);
      return this._objectMode;
    }
    set
    {
      if (this._objectMode == value)
        return;
      this._objectMode = value;
      this.cbNumObjectMode.SelectedIndex = this.cbNumObjectMode.Items.IndexOf((object) value);
    }
  }

  /// <summary>Признак изменения</summary>
  public bool Modified
  {
    get => this._modified;
    set
    {
      if (this._modified != value)
        this._modified = true;
      this.UpdateControls();
    }
  }

  /// <summary>Обновление состояния контролов</summary>
  public void UpdateControls()
  {
    if (this._numNode != null)
    {
      this.btnApply.Enabled = this.NumMethod.Equals((object) TechNumerationMethods.Auto) || !this._attributeGuid.Equals(Guid.Empty);
      this.btnCancel.Enabled = true;
    }
    else
      this.btnApply.Enabled = this.btnCancel.Enabled = this._modified;
    this.grbObject.Enabled = this.NumMethod.Equals((object) TechNumerationMethods.Manual);
  }

  /// <summary>Загрузка информации о правиле нумерации</summary>
  /// <param name="numRule"></param>
  public void DataLoad(TechNumerationRule numRule)
  {
    if (numRule == null)
      return;
    this.FillComboBox(this.cbNumMethod, MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.NumerationMethodAttrGuid)?.PossibleValues?.ToArray(), (object) EnumTypeHelper.GetCaption((Enum) numRule.NumerationMethod));
    this.FillComboBox(this.cbMainArea, MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.NumerationAreaAttrGuid)?.PossibleValues?.ToArray(), (object) EnumTypeHelper.GetCaption((Enum) numRule.NumerationArea));
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.NumerationTypeAttrGuid);
    this.FillComboBox(this.cbMainNumType, attributeType?.PossibleValues?.ToArray(), (object) EnumTypeHelper.GetCaption((Enum) numRule.NumerationArea));
    this.FillComboBox(this.cbVarNumType, attributeType?.PossibleValues?.ToArray(), (object) EnumTypeHelper.GetCaption((Enum) numRule.NumerationTypeVariant));
    this.FillTextBox(this.tbxMainCharList, (object) numRule.CharList);
    this.FillTextBox(this.tbxVarCharList, (object) string.Empty);
    this.FillTextBox(this.tbxMainFirstNumber, (object) numRule.NumberFirst);
    this.FillTextBox(this.tbxVarFirstNumber, (object) string.Empty);
    TextBox tbxMainDigitsCount = this.tbxMainDigitsCount;
    int num = numRule.NumberLength;
    string str1 = num.ToString();
    this.FillTextBox(tbxMainDigitsCount, (object) str1);
    this.FillTextBox(this.tbxVarDigitsCount, (object) string.Empty);
    TextBox tbxMainStep = this.tbxMainStep;
    num = numRule.NumberStep;
    string str2 = num.ToString();
    this.FillTextBox(tbxMainStep, (object) str2);
    this.FillTextBox(this.tbxVarStep, (object) string.Empty);
    this.FillTextBox(this.tbxVarCharSeparator, (object) numRule.NumberSeparator.ToString());
    this.chbxVarNumberBaseObject.CheckedChanged -= new EventHandler(this.chbx_CheckedChanged);
    try
    {
      this.chbxVarNumberBaseObject.Checked = numRule.UseBaseObjectNumber == TechNumerationBool.Yes;
    }
    finally
    {
      this.chbxVarNumberBaseObject.CheckedChanged += new EventHandler(this.chbx_CheckedChanged);
    }
    this.Modified = false;
    this.UpdateControls();
  }

  /// <summary>Сохранение информации о правиле нумерации в базу</summary>
  /// <param name="numRule"></param>
  public void DataSave(TechNumerationRule numRule)
  {
    if (numRule == null)
      return;
    if (this.cbNumMethod.SelectedItem != null)
      numRule.NumerationMethod = (TechNumerationMethods) EnumTypeHelper.GetEnumValue(typeof (TechNumerationMethods), this.cbNumMethod.SelectedItem.ToString(), (object) TechNumerationMethods.Auto);
    if (this.cbMainNumType.SelectedItem != null)
      numRule.NumerationType = (TechNumerationTypes) EnumTypeHelper.GetEnumValue(typeof (TechNumerationTypes), this.cbMainNumType.SelectedItem.ToString(), (object) TechNumerationTypes.Number);
    if (this.cbMainArea.SelectedItem != null)
      numRule.NumerationArea = (TechNumerationAreas) EnumTypeHelper.GetEnumValue(typeof (TechNumerationAreas), this.cbMainArea.SelectedItem.ToString(), (object) TechNumerationAreas.Parent);
    numRule.CharList = this.tbxMainCharList.Text;
    numRule.NumberLength = Convert.ToInt32(this.tbxMainDigitsCount.Text);
    numRule.NumberFirst = this.tbxMainFirstNumber.Text;
    numRule.NumberStep = Convert.ToInt32(this.tbxMainStep.Text);
    numRule.NumberSeparator = Convert.ToChar(this.tbxVarCharSeparator.Text);
    if (this.cbVarNumType.SelectedItem != null)
      numRule.NumerationTypeVariant = (TechNumerationTypes) EnumTypeHelper.GetEnumValue(typeof (TechNumerationTypes), this.cbVarNumType.SelectedItem.ToString(), (object) TechNumerationTypes.Number);
    numRule.UseBaseObjectNumber = this.chbxVarNumberBaseObject.Checked ? TechNumerationBool.Yes : TechNumerationBool.No;
    this.Modified = false;
  }

  /// <summary>Загрузка информации о элементе правила нумерации</summary>
  /// <param name="numNode"></param>
  public void DataLoad(TechNumerationNode numNode)
  {
    this.grbObject.Visible = numNode != null;
    if (numNode == null)
      return;
    this.tbxAttribute.TextChanged -= new EventHandler(this.tbx_TextChanged);
    try
    {
      this._attributeGuid = numNode.AttributeTypeGuid;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(numNode.AttributeTypeGuid);
      if (attributeType != null)
        this.tbxAttribute.Text = attributeType.Name;
    }
    finally
    {
      this.tbxAttribute.TextChanged += new EventHandler(this.tbx_TextChanged);
    }
    this.UpdateControls();
  }

  /// <summary>Сохранение информации о элементе правила нумерации</summary>
  /// <param name="numNode"></param>
  public void DataSave(TechNumerationNode numNode)
  {
    if (numNode == null)
      return;
    numNode.AttributeTypeGuid = this._attributeGuid;
    this.Modified = false;
  }

  /// <summary>Загрузка информации о элементе правила нумерации</summary>
  /// <param name="objectId"></param>
  public void DataLoad(long objectId)
  {
    if (objectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
      if (dbObject == null)
        return;
      TechNumerationRule numRule = new TechNumerationRule();
      numRule.Load(dbObject, sessionKeeper.Session);
      this.DataLoad(numRule);
    }
  }

  /// <summary>Сохранение информации о элементе правила нумерации</summary>
  /// <param name="objectId"></param>
  public void DataSave(long objectId)
  {
    if (objectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
      if (dbObject == null)
        return;
      TechNumerationRule numRule = new TechNumerationRule();
      this.DataSave(numRule);
      numRule.Save(dbObject, sessionKeeper.Session);
    }
  }

  /// <summary>ImageIndex</summary>
  public int ImageIndex => -1;

  /// <summary>OrderID</summary>
  public int OrderID => 0;

  /// <summary>Caption</summary>
  public string Caption => LocalizationHolder.rm.GetString("TechCard.Client_229");

  /// <summary>Initialize</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    IDBObjectID itemData = (IDBObjectID) items?.GetItemData(0, typeof (IDBObjectID));
    this._objectId = itemData != null ? itemData.Value : 0L;
  }

  /// <summary>Deactivate</summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
    if (!this.Modified || MessageBox.Show(LocalizationHolder.rm.GetString(sc_19536.ssp_techcard_19537()), LocalizationHolder.rm.GetString(sc_19536.ssp_techcard_19538()), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.DataSave(this._objectId);
  }

  /// <summary>Activate</summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this.DataLoad(this._objectId);
  }

  /// <summary>Сохранение изменений</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    if (this.NumRule == null)
    {
      this.DataSave(this._objectId);
    }
    else
    {
      this.DataSave(this.NumRule);
      this.DataSave(this.NumNode);
    }
  }

  /// <summary>Отмена изменений</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cb_SelectedValueChanged(object sender, EventArgs e)
  {
    this.Modified = true;
    if (sender != this.cbNumMethod)
      return;
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbx_TextChanged(object sender, EventArgs e) => this.Modified = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chbx_CheckedChanged(object sender, EventArgs e) => this.Modified = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbnAttribute_Click(object sender, EventArgs e)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.SelectedAttributeIDOnStartup(MetaDataHelper.GetAttributeID((object) this._attributeGuid));
      if (this.NumNode != null && this.NumNode.ObjectTypeGuid != Guid.Empty)
        attributesSelectDlg.LoadAttrDialogForObjectsTypes(this.NumNode.ObjectTypeGuid);
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count == 0)
        return;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributesSelectDlg.SelectedAttributesID[0]);
      if (attributeType == null || attributeType.AttributeGuid == this._attributeGuid)
        return;
      this._attributeGuid = attributeType.AttributeGuid;
      this.tbxAttribute.Text = attributeType.Name;
      this.Modified = true;
    }
  }
}
