// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcNumerationRules.NumRuleControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcNumerationRules;

/// <summary>
/// Базовый контрол для редактирования / просмотра параметров нумерации объекта
/// </summary>
public class NumRuleControl : UserControl
{
  /// <summary>Required designer variables</summary>
  private GroupBox grbRule;
  private CheckBox chbxNumberBaseObject;
  private TextBox tbxCharSeparator;
  private Label lblCharSeparator;
  private Label lblVariants;
  private Label lblArea;
  private ComboBox cbArea;
  private TextBox tbxStep;
  private Label lblStep;
  private Label lblCharList;
  private TextBox tbxCharList;
  private TextBox tbxDigitsCount;
  private Label lblDigitsCount;
  private Label lblFirstNumber;
  private IContainer components;
  private Label lblNumerationTypeVariant;
  private ComboBox cbNumerationTypeVariant;
  private ComboBox cbNumerationType;
  private Label lblNumerationType;
  private TextBox tbxFirstNumber;
  private Label lblNumerationMethod;
  private Panel panel1;
  private GroupBox grbObject;
  private Button tbnAttribute;
  private TextBox tbxAttribute;
  private Label lbl;
  private Panel pnlButtons;
  internal ComboBox cbNumerationMethod;
  internal Button btnCancel;
  private CheckBox chbRenumOnDelete;
  private ErrorProvider errorProvider;
  internal Button btnApply;
  /// <summary>Правило нумерации объектов</summary>
  protected TechNumerationRule _numRule;
  /// <summary>Элемент правила нумерации</summary>
  protected TechNumerationNode _numNode;
  /// <summary>Guid типа нумеруемого атрибута</summary>
  private Guid _attributeGuid = Guid.Empty;
  /// <summary>Режим инициализации/загрузки данных</summary>
  protected bool _loadingData;
  /// <summary>Признак изменения</summary>
  protected bool _modified;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NumRuleControl));
    this.grbRule = new GroupBox();
    this.chbRenumOnDelete = new CheckBox();
    this.cbNumerationMethod = new ComboBox();
    this.lblNumerationMethod = new Label();
    this.chbxNumberBaseObject = new CheckBox();
    this.lblNumerationTypeVariant = new Label();
    this.cbNumerationTypeVariant = new ComboBox();
    this.tbxCharSeparator = new TextBox();
    this.lblCharSeparator = new Label();
    this.lblVariants = new Label();
    this.lblArea = new Label();
    this.cbArea = new ComboBox();
    this.tbxStep = new TextBox();
    this.lblStep = new Label();
    this.tbxFirstNumber = new TextBox();
    this.lblCharList = new Label();
    this.tbxCharList = new TextBox();
    this.tbxDigitsCount = new TextBox();
    this.lblDigitsCount = new Label();
    this.cbNumerationType = new ComboBox();
    this.lblNumerationType = new Label();
    this.lblFirstNumber = new Label();
    this.panel1 = new Panel();
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.grbObject = new GroupBox();
    this.tbnAttribute = new Button();
    this.tbxAttribute = new TextBox();
    this.lbl = new Label();
    this.errorProvider = new ErrorProvider(this.components);
    this.grbRule.SuspendLayout();
    this.panel1.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.grbObject.SuspendLayout();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.SuspendLayout();
    this.grbRule.Controls.Add((Control) this.chbRenumOnDelete);
    this.grbRule.Controls.Add((Control) this.cbNumerationMethod);
    this.grbRule.Controls.Add((Control) this.lblNumerationMethod);
    this.grbRule.Controls.Add((Control) this.chbxNumberBaseObject);
    this.grbRule.Controls.Add((Control) this.lblNumerationTypeVariant);
    this.grbRule.Controls.Add((Control) this.cbNumerationTypeVariant);
    this.grbRule.Controls.Add((Control) this.tbxCharSeparator);
    this.grbRule.Controls.Add((Control) this.lblCharSeparator);
    this.grbRule.Controls.Add((Control) this.lblVariants);
    this.grbRule.Controls.Add((Control) this.lblArea);
    this.grbRule.Controls.Add((Control) this.cbArea);
    this.grbRule.Controls.Add((Control) this.tbxStep);
    this.grbRule.Controls.Add((Control) this.lblStep);
    this.grbRule.Controls.Add((Control) this.tbxFirstNumber);
    this.grbRule.Controls.Add((Control) this.lblCharList);
    this.grbRule.Controls.Add((Control) this.tbxCharList);
    this.grbRule.Controls.Add((Control) this.tbxDigitsCount);
    this.grbRule.Controls.Add((Control) this.lblDigitsCount);
    this.grbRule.Controls.Add((Control) this.cbNumerationType);
    this.grbRule.Controls.Add((Control) this.lblNumerationType);
    this.grbRule.Controls.Add((Control) this.lblFirstNumber);
    componentResourceManager.ApplyResources((object) this.grbRule, "grbRule");
    this.grbRule.Name = "grbRule";
    this.grbRule.TabStop = false;
    componentResourceManager.ApplyResources((object) this.chbRenumOnDelete, "chbRenumOnDelete");
    this.chbRenumOnDelete.Name = "chbRenumOnDelete";
    this.chbRenumOnDelete.CheckedChanged += new EventHandler(this.chbx_CheckedChanged);
    this.cbNumerationMethod.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.cbNumerationMethod, "cbNumerationMethod");
    this.cbNumerationMethod.Name = "cbNumerationMethod";
    componentResourceManager.ApplyResources((object) this.lblNumerationMethod, "lblNumerationMethod");
    this.lblNumerationMethod.Name = "lblNumerationMethod";
    componentResourceManager.ApplyResources((object) this.chbxNumberBaseObject, "chbxNumberBaseObject");
    this.chbxNumberBaseObject.Name = "chbxNumberBaseObject";
    componentResourceManager.ApplyResources((object) this.lblNumerationTypeVariant, "lblNumerationTypeVariant");
    this.lblNumerationTypeVariant.Name = "lblNumerationTypeVariant";
    this.cbNumerationTypeVariant.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.cbNumerationTypeVariant, "cbNumerationTypeVariant");
    this.cbNumerationTypeVariant.Name = "cbNumerationTypeVariant";
    componentResourceManager.ApplyResources((object) this.tbxCharSeparator, "tbxCharSeparator");
    this.tbxCharSeparator.Name = "tbxCharSeparator";
    componentResourceManager.ApplyResources((object) this.lblCharSeparator, "lblCharSeparator");
    this.lblCharSeparator.Name = "lblCharSeparator";
    componentResourceManager.ApplyResources((object) this.lblVariants, "lblVariants");
    this.lblVariants.Name = "lblVariants";
    componentResourceManager.ApplyResources((object) this.lblArea, "lblArea");
    this.lblArea.Name = "lblArea";
    this.cbArea.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.cbArea, "cbArea");
    this.cbArea.Name = "cbArea";
    componentResourceManager.ApplyResources((object) this.tbxStep, "tbxStep");
    this.tbxStep.Name = "tbxStep";
    componentResourceManager.ApplyResources((object) this.lblStep, "lblStep");
    this.lblStep.Name = "lblStep";
    this.lblStep.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxFirstNumber, "tbxFirstNumber");
    this.tbxFirstNumber.Name = "tbxFirstNumber";
    componentResourceManager.ApplyResources((object) this.lblCharList, "lblCharList");
    this.lblCharList.Name = "lblCharList";
    this.lblCharList.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxCharList, "tbxCharList");
    this.tbxCharList.Name = "tbxCharList";
    componentResourceManager.ApplyResources((object) this.tbxDigitsCount, "tbxDigitsCount");
    this.tbxDigitsCount.Name = "tbxDigitsCount";
    componentResourceManager.ApplyResources((object) this.lblDigitsCount, "lblDigitsCount");
    this.lblDigitsCount.Name = "lblDigitsCount";
    this.lblDigitsCount.Tag = (object) "";
    this.cbNumerationType.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.cbNumerationType, "cbNumerationType");
    this.cbNumerationType.Name = "cbNumerationType";
    componentResourceManager.ApplyResources((object) this.lblNumerationType, "lblNumerationType");
    this.lblNumerationType.Name = "lblNumerationType";
    componentResourceManager.ApplyResources((object) this.lblFirstNumber, "lblFirstNumber");
    this.lblFirstNumber.Name = "lblFirstNumber";
    this.lblFirstNumber.Tag = (object) "";
    this.panel1.Controls.Add((Control) this.pnlButtons);
    this.panel1.Controls.Add((Control) this.grbObject);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnApply.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.grbObject.Controls.Add((Control) this.tbnAttribute);
    this.grbObject.Controls.Add((Control) this.tbxAttribute);
    this.grbObject.Controls.Add((Control) this.lbl);
    componentResourceManager.ApplyResources((object) this.grbObject, "grbObject");
    this.grbObject.Name = "grbObject";
    this.grbObject.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tbnAttribute, "tbnAttribute");
    this.tbnAttribute.Name = "tbnAttribute";
    this.tbnAttribute.Click += new EventHandler(this.tbnAttribute_Click);
    componentResourceManager.ApplyResources((object) this.tbxAttribute, "tbxAttribute");
    this.tbxAttribute.Name = "tbxAttribute";
    this.tbxAttribute.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.lbl, "lbl");
    this.lbl.Name = "lbl";
    this.lbl.Tag = (object) "";
    this.errorProvider.ContainerControl = (ContainerControl) this;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.grbRule);
    this.Name = nameof (NumRuleControl);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.grbRule.ResumeLayout(false);
    this.grbRule.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.grbObject.ResumeLayout(false);
    this.grbObject.PerformLayout();
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Заполнение списка</summary>
  /// <param name="control"></param>
  /// <param name="values"></param>
  /// <param name="selValue"></param>
  private void FillComboBox(ComboBox control, object selValue, object[] values = null)
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
      control.Text = Convert.ToString(value);
    }
    finally
    {
      control.TextChanged += new EventHandler(this.tbx_TextChanged);
    }
  }

  /// <summary>Инициализация данных класса / контрола</summary>
  private void InitializeData() => this.InitializeCustomControls();

  /// <summary>
  /// 
  /// </summary>
  protected virtual void InitializeCustomControls()
  {
    this._loadingData = true;
    try
    {
    }
    finally
    {
      this._loadingData = false;
    }
  }

  /// <summary>Обновление данных контрола</summary>
  protected virtual void UpdateControlData()
  {
    this.grbObject.Visible = this._numNode != null;
    this.chbRenumOnDelete.Enabled = (TechNumerationMethods) EnumTypeHelper.GetEnumValue(typeof (TechNumerationMethods), this.cbNumerationMethod.SelectedItem.ToString(), (object) TechNumerationMethods.Auto) == TechNumerationMethods.Auto;
    this.errorProvider.SetError((Control) this.chbRenumOnDelete, string.Empty);
    if (this.chbRenumOnDelete.Enabled && this.chbRenumOnDelete.Checked && (TechNumerationAreas) EnumTypeHelper.GetEnumValue(typeof (TechNumerationAreas), this.cbArea.SelectedItem.ToString(), (object) TechNumerationAreas.Parent) != TechNumerationAreas.Parent)
      this.errorProvider.SetError((Control) this.chbRenumOnDelete, LocalizationHolder.rm.GetString("TechCard.Client_529"));
    this.UpdateButtons();
  }

  /// <summary>Обновление состояние кнопок</summary>
  protected virtual void UpdateButtons()
  {
    if (this._numNode != null)
    {
      this.btnApply.Enabled = !this._attributeGuid.Equals(Guid.Empty) && this.Modified;
      this.btnCancel.Enabled = this.Modified;
    }
    else
      this.btnApply.Enabled = this.btnCancel.Enabled = this.Modified;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.components.Dispose();
      this.components = (IContainer) null;
    }
    base.Dispose(disposing);
  }

  /// <summary>Выбор нумеруемого атрибута</summary>
  protected virtual void SelectNumerationAttribute()
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

  /// <summary>Конструктор</summary>
  public NumRuleControl()
  {
    this.InitializeComponent();
    this.InitializeData();
  }

  /// <summary>Правило нумерации</summary>
  public virtual TechNumerationRule NumRule
  {
    [DebuggerStepThrough] get => this._numRule;
    set
    {
      if (this._numRule == value)
        return;
      this._numRule = value;
      this.DataLoad(value);
      this.UpdateControlData();
    }
  }

  /// <summary>Элемент правила нумерации</summary>
  public virtual TechNumerationNode NumNode
  {
    [DebuggerStepThrough] get => this._numNode;
    set
    {
      if (this._numNode == value)
        return;
      this._numNode = value;
      this.DataLoad(value);
      this.UpdateControlData();
    }
  }

  /// <summary>Признак изменения</summary>
  public virtual bool Modified
  {
    [DebuggerStepThrough] get => this._modified;
    set
    {
      if (this._modified == value)
        return;
      this._modified = value;
      this.UpdateControlData();
    }
  }

  /// <summary>Загрузка информации</summary>
  public virtual void DataLoad()
  {
    this.DataLoad(this._numRule);
    this.DataLoad(this._numNode);
    this.Modified = false;
  }

  /// <summary>Сохранение изменений</summary>
  public virtual void DataSave()
  {
    this.DataSave(this._numRule);
    this.DataSave(this._numNode);
    this.Modified = false;
  }

  /// <summary>Загрузка информации о правиле нумерации</summary>
  /// <param name="numRule"></param>
  public void DataLoad(TechNumerationRule numRule)
  {
    if (numRule == null)
      return;
    this._loadingData = true;
    try
    {
      IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.NumerationMethodAttrGuid);
      this.FillComboBox(this.cbNumerationMethod, (object) EnumTypeHelper.GetCaption((Enum) numRule.NumerationMethod), attributeType1?.PossibleValues?.ToArray());
      IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.NumerationTypeAttrGuid);
      this.FillComboBox(this.cbNumerationType, (object) EnumTypeHelper.GetCaption((Enum) numRule.NumerationType), attributeType2?.PossibleValues?.ToArray());
      IMSAttributeType attributeType3 = MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.NumerationAreaAttrGuid);
      this.FillComboBox(this.cbArea, (object) EnumTypeHelper.GetCaption((Enum) numRule.NumerationArea), attributeType3?.PossibleValues?.ToArray());
      this.FillTextBox(this.tbxCharList, (object) numRule.CharList);
      this.FillTextBox(this.tbxDigitsCount, (object) numRule.NumberLength);
      this.FillTextBox(this.tbxFirstNumber, (object) numRule.NumberFirst);
      this.FillTextBox(this.tbxStep, (object) numRule.NumberStep);
      this.FillTextBox(this.tbxCharSeparator, (object) numRule.NumberSeparator);
      IMSAttributeType attributeType4 = MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.NumerationTypeVariantAttrGuid);
      this.FillComboBox(this.cbNumerationTypeVariant, (object) EnumTypeHelper.GetCaption((Enum) numRule.NumerationTypeVariant), attributeType4?.PossibleValues?.ToArray());
      this.chbxNumberBaseObject.CheckedChanged -= new EventHandler(this.chbx_CheckedChanged);
      try
      {
        this.chbxNumberBaseObject.Checked = numRule.UseBaseObjectNumber == TechNumerationBool.Yes;
      }
      finally
      {
        this.chbxNumberBaseObject.CheckedChanged += new EventHandler(this.chbx_CheckedChanged);
      }
      this.chbRenumOnDelete.Checked = numRule.RenumOnDelete;
    }
    finally
    {
      this._loadingData = false;
    }
  }

  /// <summary>Сохранение информации о правиле нумерации в базу</summary>
  /// <param name="numRule"></param>
  public void DataSave(TechNumerationRule numRule)
  {
    if (numRule == null)
      return;
    if (this.cbNumerationMethod.SelectedItem != null)
      numRule.NumerationMethod = (TechNumerationMethods) EnumTypeHelper.GetEnumValue(typeof (TechNumerationMethods), this.cbNumerationMethod.SelectedItem.ToString(), (object) TechNumerationMethods.Auto);
    if (this.cbNumerationType.SelectedItem != null)
      numRule.NumerationType = (TechNumerationTypes) EnumTypeHelper.GetEnumValue(typeof (TechNumerationTypes), this.cbNumerationType.SelectedItem.ToString(), (object) TechNumerationTypes.Number);
    if (this.cbArea.SelectedItem != null)
      numRule.NumerationArea = (TechNumerationAreas) EnumTypeHelper.GetEnumValue(typeof (TechNumerationAreas), this.cbArea.SelectedItem.ToString(), (object) TechNumerationAreas.Parent);
    numRule.CharList = this.tbxCharList.Text;
    numRule.NumberLength = Convert.ToInt32(this.tbxDigitsCount.Text);
    numRule.NumberFirst = this.tbxFirstNumber.Text;
    numRule.NumberStep = Convert.ToInt32(this.tbxStep.Text);
    numRule.NumberSeparator = Convert.ToChar(this.tbxCharSeparator.Text);
    if (this.cbNumerationTypeVariant.SelectedItem != null)
      numRule.NumerationTypeVariant = (TechNumerationTypes) EnumTypeHelper.GetEnumValue(typeof (TechNumerationTypes), this.cbNumerationTypeVariant.SelectedItem.ToString(), (object) TechNumerationTypes.Number);
    numRule.UseBaseObjectNumber = this.chbxNumberBaseObject.Checked ? TechNumerationBool.Yes : TechNumerationBool.No;
    numRule.RenumOnDelete = this.chbRenumOnDelete.Checked;
  }

  /// <summary>Загрузка информации о элементе правила нумерации</summary>
  /// <param name="numNode"></param>
  public void DataLoad(TechNumerationNode numNode)
  {
    if (numNode == null)
      return;
    this._loadingData = true;
    try
    {
      this._attributeGuid = numNode.AttributeTypeGuid;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(numNode.AttributeTypeGuid);
      this.tbxAttribute.Text = attributeType != null ? attributeType.Name : string.Empty;
    }
    finally
    {
      this._loadingData = false;
    }
  }

  /// <summary>Сохранение информации о элементе правила нумерации</summary>
  /// <param name="numNode"></param>
  public void DataSave(TechNumerationNode numNode)
  {
    if (numNode == null)
      return;
    numNode.AttributeTypeGuid = this._attributeGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e) => this.DataSave();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e) => this.DataLoad();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cb_SelectedValueChanged(object sender, EventArgs e)
  {
    if (this._loadingData)
      return;
    this.UpdateControlData();
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbx_TextChanged(object sender, EventArgs e)
  {
    if (this._loadingData)
      return;
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chbx_CheckedChanged(object sender, EventArgs e)
  {
    if (this._loadingData)
      return;
    this.UpdateControlData();
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbnAttribute_Click(object sender, EventArgs e) => this.SelectNumerationAttribute();
}
