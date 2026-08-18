// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.AutoMasterControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>
/// Главная закладка для записи автомобильной спецификации
/// </summary>
internal class AutoMasterControl : PageUserControl
{
  /// <summary>Текущее значение атрибута "Количество"</summary>
  private MeasuredValue _quantity;
  /// <summary>Структура с информацией по объектам для классификации</summary>
  private ClassificatedObjects _classifObjects;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label5;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbNote;
  private Button bClassificate;
  private Panel panel1;
  private Panel panel2;
  private Button bEditName;
  private Button bEditDesignation;
  private Label label2;
  private Label label1;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbName;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbDesignation;
  private Button bEditCount;
  private Label label8;
  private Label label7;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbZone;
  private Label label6;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosDesignation;
  private Label label4;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosition;
  private Label label3;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbCount;
  private Label label9;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbOKPCode;
  private Label label10;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbSmotri;
  private ComboBox cbFormat;
  private CheckBox cbPodbor;
  private ToolTip toolTip1;

  public AutoMasterControl(
    IDBRelation relation,
    ClassificatedObjects classifObjects,
    List<AVSRow> selectedSpecRows,
    CommonDataType disableControls)
  {
    this.InitializeComponent();
    this.Init(relation, classifObjects, selectedSpecRows, disableControls);
  }

  internal void Init(
    IDBRelation relation,
    ClassificatedObjects classifObjects,
    List<AVSRow> selectedSpecRows,
    CommonDataType disableControls)
  {
    this.Init(relation.RelationID, AttributableElements.Relation, disableControls);
    if (selectedSpecRows != null && selectedSpecRows.Count > 0)
      this.selectedSpecRow = selectedSpecRows[0];
    if (!AvsConfig.PositionDesignation.ShowPosDesignation)
    {
      this.label6.Visible = false;
      this.tbPosDesignation.Visible = false;
    }
    else
    {
      this.label6.Visible = true;
      this.tbPosDesignation.Visible = true;
    }
    this.cbPodbor.Visible = AvsConfig.Podbor.ShowPodbor;
    this._classifObjects = classifObjects;
    this.SetFormatValues(this._classifObjects.documentID, this.cbFormat);
    if (this.aProcessor != null)
      return;
    this.aProcessor = new AttributeProcessor(-1L, AttributableElements.Relation, true);
  }

  protected override void OnSave(IUserSession session, OpenModes mode, CreatedPair pair)
  {
    this.commonData.Zona = this.tbZone.Text;
    this.commonData.Position = this.tbPosition.Text;
    this.commonData.Note = this.tbNote.Text;
    this.commonData.PosDesignation = this.tbPosDesignation.Text;
    this.commonData.Format = this.cbFormat.Text;
    this.commonData.Podbor = this.cbPodbor.Checked;
    if (this.selectedSpecRow != null && this.selectedSpecRow.avsDocument != null)
      this.selectedSpecRow.avsDocument.SuspendDocumentAndGridUpdates(true, false);
    try
    {
      if (this.selectedSpecRow != null)
      {
        int relationIndex = -1;
        int productIndex = -1;
        this.SaveCount(-1, (object) this._quantity);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Position, relationIndex, productIndex, (object) this.tbPosition.Text, true, false, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Format, relationIndex, productIndex, (object) this.cbFormat.Text, true, false, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Zone, relationIndex, productIndex, (object) this.tbZone.Text, true, true, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Note, relationIndex, productIndex, (object) this.tbNote.Text, true, false, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_PosDesignation, relationIndex, productIndex, (object) this.tbPosDesignation.Text, true, false, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_Podbor), relationIndex, productIndex, (object) this.cbPodbor.Checked, true, false, true, true, false, false);
        this.selectedSpecRow.TextLinkToMainDocument = this.tbSmotri.Text;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          AttributeValues[] valuesList = new AttributeValues[6]
          {
            new AttributeValues(AvsIDCache.Attr_Position, (object) this.tbPosition.Text),
            new AttributeValues(AvsIDCache.Attr_Zone, (object) this.tbZone.Text),
            new AttributeValues(AvsIDCache.Attr_Note, (object) this.tbNote.Text),
            new AttributeValues(AvsIDCache.Attr_PosDesignation, (object) this.tbPosDesignation.Text),
            new AttributeValues(AvsIDCache.Attr_Podbor, (object) this.cbPodbor.Checked),
            new AttributeValues(AvsIDCache.Attr_Count, (object) this._quantity)
          };
          for (int index = 0; index < pair.RelationIDs.Count; ++index)
          {
            IDBRelation relation = sessionKeeper.Session.GetRelation(pair.RelationIDs[index]);
            if (relation != null && relation.RelationType != AvsIDCache.Relation_Document)
              relation.SetAttributesValues(valuesList);
          }
        }
      }
    }
    finally
    {
      if (this.selectedSpecRow != null && this.selectedSpecRow.avsDocument != null)
        this.selectedSpecRow.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, false);
    }
  }

  protected override void OnReload(IUserSession session, OpenModes mode)
  {
    if (mode == OpenModes.InViewReadOnly)
      this.SetAllReadOnly((Control) this);
    if (mode == OpenModes.View || mode == OpenModes.InViewReadOnly)
    {
      this.tbDesignation.Enabled = false;
      this.tbName.Enabled = false;
      this.bEditDesignation.Enabled = false;
      this.bEditName.Enabled = false;
      this.cbFormat.Enabled = false;
      this.tbOKPCode.Enabled = false;
      this.bClassificate.Enabled = false;
      this.tbSmotri.Enabled = false;
      this.cbPodbor.Enabled = false;
    }
    else
    {
      if ((this.disableControls & CommonDataType.Designation) == CommonDataType.Designation)
      {
        this.tbDesignation.Enabled = false;
        this.bEditDesignation.Enabled = false;
      }
      if ((this.disableControls & CommonDataType.Name) == CommonDataType.Name)
      {
        this.tbName.Enabled = false;
        this.bEditName.Enabled = false;
      }
      if ((this.disableControls & CommonDataType.OKPCode) == CommonDataType.OKPCode)
        this.tbOKPCode.Enabled = false;
      if ((this.disableControls & CommonDataType.Format) == CommonDataType.Format)
        this.cbFormat.Enabled = false;
    }
    if (!AvsConfig.PositionDesignation.ShowPosDesignation)
    {
      this.label6.Visible = false;
      this.tbPosDesignation.Visible = false;
    }
    else
    {
      this.label6.Visible = true;
      this.tbPosDesignation.Visible = true;
    }
    this.ReloadCommonData(CommonDataType.All);
    object obj1 = this.aProcessor.GetValue(FormHelper.AttributeCountID);
    if (CompareValuesHelper.NormalizedValue(obj1) != null && obj1 is MeasuredValue)
    {
      this.tbCount.Text = ((MeasuredValue) obj1).ToString();
      this._quantity = (MeasuredValue) obj1;
    }
    else
    {
      this.tbCount.Text = string.Empty;
      this._quantity = (MeasuredValue) null;
    }
    if (this.selectedSpecRow != null)
    {
      int relationIndex = -1;
      this.tbCount.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.avsDocument.Field_Count, relationIndex, -1, (List<RelationAttributeValuesCache>) null, true);
      this.tbPosition.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Position, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
      this.tbZone.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Zone, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
      this.tbNote.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Note, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
      this.cbPodbor.Checked = this.selectedSpecRow.GetFieldBoolValue(this.selectedSpecRow.Attr_Podbor, relationIndex, -1, (List<RelationAttributeValuesCache>) null, true);
      this.tbSmotri.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Attr_Smotri, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
      this.cbFormat.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Format, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
      this.cbFormat.Visible = true;
    }
    else
    {
      if (this.aProcessor.FindAttributeValues(AvsIDCache.Attr_Podbor) != null)
        this.cbPodbor.Checked = Convert.ToBoolean(this.aProcessor.GetValue(AvsIDCache.Attr_Podbor));
      if (this.aProcessor.FindAttributeValues(AvsIDCache.Attr_Format) != null)
        this.cbFormat.Text = Convert.ToString(this.aProcessor.GetValue(AvsIDCache.Attr_Format));
      if (this.aProcessor.FindAttributeValues(FormHelper.AttributePositionID) != null)
      {
        object obj2 = this.aProcessor.GetValue(FormHelper.AttributePositionID);
        this.tbPosition.Text = CompareValuesHelper.NormalizedValue(obj2) != null ? Convert.ToString(obj2) : string.Empty;
      }
      if (this.aProcessor.FindAttributeValues(FormHelper.AttributeZoneID) != null)
      {
        object obj3 = this.aProcessor.GetValue(FormHelper.AttributeZoneID);
        this.tbZone.Text = CompareValuesHelper.NormalizedValue(obj3) != null ? Convert.ToString(obj3) : string.Empty;
      }
      if (this.aProcessor.FindAttributeValues(FormHelper.AttributeNoteID) != null)
      {
        object obj4 = this.aProcessor.GetValue(FormHelper.AttributeNoteID);
        this.tbNote.Text = CompareValuesHelper.NormalizedValue(obj4) != null ? Convert.ToString(obj4) : string.Empty;
      }
      if (this.aProcessor.FindAttributeValues(FormHelper.AttributePosDesignationID) != null)
      {
        object obj5 = this.aProcessor.GetValue(FormHelper.AttributePosDesignationID);
        this.tbPosDesignation.Text = CompareValuesHelper.NormalizedValue(obj5) != null ? Convert.ToString(obj5) : string.Empty;
      }
    }
    this.tbName.ReadOnly |= this.IsReadOnly(FormHelper.AttributeNameID);
    this.tbDesignation.ReadOnly |= this.IsReadOnly(FormHelper.AttributeDesignationID);
  }

  /// <summary>Пришло сообщение об изменениии общих атрибутов</summary>
  protected override void OnCommonDataChanged(CommonDataType type) => this.ReloadCommonData(type);

  /// <summary>Перечитать общие атрибуты</summary>
  private void ReloadCommonData(CommonDataType type)
  {
    switch (type)
    {
      case CommonDataType.All:
        this.tbDesignation.Text = this.commonData.Designation;
        this.tbName.Text = this.commonData.Name;
        this.cbFormat.Text = this.commonData.Format;
        this.tbOKPCode.Text = this.commonData.OKPCode;
        this.tbPosDesignation.Text = this.commonData.PosDesignation;
        this.tbSmotri.Text = this.commonData.Smotri;
        this.OnChanged();
        break;
      case CommonDataType.Designation:
        this.tbDesignation.Text = this.commonData.Designation;
        this.OnChanged();
        break;
      case CommonDataType.Name:
        this.tbName.Text = this.commonData.Name;
        this.OnChanged();
        break;
      case CommonDataType.OKPCode:
        this.tbOKPCode.Text = this.commonData.OKPCode;
        this.OnChanged();
        break;
      case CommonDataType.Format:
        this.cbFormat.Text = this.commonData.Format;
        this.OnChanged();
        break;
    }
  }

  /// <summary>Обработка события применения классификации</summary>
  /// <param name="values"></param>
  public override void OnSetClassifyAttributes(IObjectClassificator oc, long clasifID)
  {
    long objectID = this._classifObjects.documentID != 0L ? this._classifObjects.documentID : this._classifObjects.articleID;
    AttributeValues[] clasificatorAttributes = oc.GetClasificatorAttributes(objectID);
    if (clasificatorAttributes == null || clasificatorAttributes.Length == 0)
      return;
    foreach (AttributeValues attributeValues in clasificatorAttributes)
    {
      if (attributeValues.Values != null && attributeValues.Values.Length != 0)
      {
        if (attributeValues.AttributeID == FormHelper.AttributeDesignationID)
          this.commonData.Designation = Convert.ToString(attributeValues.Values[0]);
        else if (attributeValues.AttributeID == FormHelper.AttributeNameID)
          this.commonData.Name = Convert.ToString(attributeValues.Values[0]);
        else if (attributeValues.AttributeID == FormHelper.AttributeFormatID)
          this.commonData.Format = Convert.ToString(attributeValues.Values[0]);
        else if (attributeValues.AttributeID == FormHelper.AttributeOKPCodeID)
          this.commonData.OKPCode = Convert.ToString(attributeValues.Values[0]);
      }
    }
  }

  /// <summary>Нажали кновку вызова редактора атрибута "Обозначение"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditDesignation_Click(object sender, EventArgs e)
  {
    this.OnEditDesignation(this._classifObjects.articleID);
  }

  /// <summary>
  /// Нажали кновку вызова редактора атрибута "Наименование"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditName_Click(object sender, EventArgs e)
  {
    this.OnEditName(this._classifObjects.articleID);
  }

  /// <summary>
  /// Вышли из поля для редактирования атрибута "Обозначение"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbDesignation_Leave(object sender, EventArgs e)
  {
    this.OnDesignationLeave(this.tbDesignation.Text);
  }

  /// <summary>
  /// Вышли из поля для редактирования атрибута "Наименование"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbName_Leave(object sender, EventArgs e) => this.OnNameLeave(this.tbName.Text);

  /// <summary>Вышли из поля для редактирования атрибута "Формат"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbFormat_Leave(object sender, EventArgs e) => this.OnFormatLeave(this.cbFormat.Text);

  /// <summary>
  /// Вышли из поля для редактирования атрибута "Количество"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbCount_Leave(object sender, EventArgs e)
  {
    this.ChangeCount((TextBox) this.tbCount, this.OnLeaveCount((TextBox) this.tbCount, this._quantity), ref this._quantity);
  }

  /// <summary>Нажали кновку вызова редактора атрибута "Количество"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditCount_Click(object sender, EventArgs e)
  {
    this.ChangeCount((TextBox) this.tbCount, this.OnEditCount((TextBox) this.tbCount, this._quantity), ref this._quantity);
  }

  /// <summary>Нажали кнопку классификации</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bClassificate_Click(object sender, EventArgs e)
  {
    this.OnClassifier(this._classifObjects);
  }

  /// <summary>Вышли из поля для редактирования атрибута "Код ОКП"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbOKPCode_Leave(object sender, EventArgs e)
  {
    if (!(this.commonData.OKPCode != this.tbOKPCode.Text))
      return;
    this.commonData.OKPCode = this.tbOKPCode.Text;
    this.OnChanged();
  }

  private void tbTextChanged(object sender, EventArgs e) => this.OnChanged();

  private void TbSearchLeave(object sender, EventArgs e)
  {
    if (!(this.commonData.Smotri != this.tbSmotri.Text))
      return;
    this.commonData.Smotri = this.tbSmotri.Text;
    this.OnChanged();
  }

  private void cbPodbor_CheckedChanged(object sender, EventArgs e)
  {
    this.OnPodborChanged(this.cbPodbor.Checked);
  }

  private void cbFormat_TextChanged(object sender, EventArgs e) => this.OnChanged();

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
    this.label5 = new Label();
    this.tbNote = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.bClassificate = new Button();
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.bEditName = new Button();
    this.bEditDesignation = new Button();
    this.label2 = new Label();
    this.label1 = new Label();
    this.tbName = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.tbDesignation = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.bEditCount = new Button();
    this.label8 = new Label();
    this.label7 = new Label();
    this.tbZone = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label6 = new Label();
    this.tbPosDesignation = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label4 = new Label();
    this.tbPosition = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label3 = new Label();
    this.tbCount = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label9 = new Label();
    this.tbOKPCode = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label10 = new Label();
    this.tbSmotri = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.cbFormat = new ComboBox();
    this.cbPodbor = new CheckBox();
    this.toolTip1 = new ToolTip(this.components);
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.label5.AutoSize = true;
    this.label5.Location = new Point(22, 15);
    this.label5.Name = "label5";
    this.label5.Size = new Size(73, 13);
    this.label5.TabIndex = 9;
    this.label5.Text = "Примечание:";
    this.tbNote.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tbNote.BackColor = Color.White;
    this.tbNote.Location = new Point(25, 31 /*0x1F*/);
    this.tbNote.Multiline = true;
    this.tbNote.Name = "tbNote";
    this.tbNote.ScrollBars = ScrollBars.Both;
    this.tbNote.Size = new Size(528, 40);
    this.tbNote.TabIndex = 91;
    this.tbNote.TextChanged += new EventHandler(this.tbTextChanged);
    this.bClassificate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bClassificate.Image = (Image) Resources.classify;
    this.bClassificate.Location = new Point(533, 11);
    this.bClassificate.Name = "bClassificate";
    this.bClassificate.Size = new Size(26, 26);
    this.bClassificate.TabIndex = 92;
    this.bClassificate.UseVisualStyleBackColor = true;
    this.bClassificate.Click += new EventHandler(this.bClassificate_Click);
    this.panel1.BackColor = SystemColors.ControlLight;
    this.panel1.Controls.Add((Control) this.bClassificate);
    this.panel1.Dock = DockStyle.Top;
    this.panel1.Location = new Point(3, 3);
    this.panel1.Name = "panel1";
    this.panel1.Padding = new Padding(3);
    this.panel1.Size = new Size(574, 47);
    this.panel1.TabIndex = 11;
    this.panel2.BackColor = SystemColors.ControlLight;
    this.panel2.Controls.Add((Control) this.label5);
    this.panel2.Controls.Add((Control) this.tbNote);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(3, 287);
    this.panel2.Margin = new Padding(0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(574, 80 /*0x50*/);
    this.panel2.TabIndex = 10;
    this.bEditName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditName.Location = new Point(532, 97);
    this.bEditName.Name = "bEditName";
    this.bEditName.Size = new Size(24, 23);
    this.bEditName.TabIndex = 86;
    this.bEditName.TabStop = false;
    this.bEditName.Text = "...";
    this.bEditName.UseVisualStyleBackColor = true;
    this.bEditName.Click += new EventHandler(this.bEditName_Click);
    this.bEditDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditDesignation.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bEditDesignation.Location = new Point(532, 70);
    this.bEditDesignation.Name = "bEditDesignation";
    this.bEditDesignation.Size = new Size(24, 23);
    this.bEditDesignation.TabIndex = 85;
    this.bEditDesignation.TabStop = false;
    this.bEditDesignation.Text = "...";
    this.bEditDesignation.UseVisualStyleBackColor = true;
    this.bEditDesignation.Click += new EventHandler(this.bEditDesignation_Click);
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label2.Location = new Point(25, 102);
    this.label2.Name = "label2";
    this.label2.Size = new Size(99, 13);
    this.label2.TabIndex = 84;
    this.label2.Text = "Наименование:";
    this.label1.AutoSize = true;
    this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(25, 76);
    this.label1.Name = "label1";
    this.label1.Size = new Size(89, 13);
    this.label1.TabIndex = 83;
    this.label1.Text = "Обозначение:";
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.BackColor = Color.White;
    this.tbName.Location = new Point(145, 98);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(387, 20);
    this.tbName.TabIndex = 1;
    this.tbName.Leave += new EventHandler(this.tbName_Leave);
    this.tbDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbDesignation.BackColor = Color.White;
    this.tbDesignation.Location = new Point(145, 72);
    this.tbDesignation.Name = "tbDesignation";
    this.tbDesignation.Size = new Size(387, 20);
    this.tbDesignation.TabIndex = 0;
    this.tbDesignation.Leave += new EventHandler(this.tbDesignation_Leave);
    this.bEditCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditCount.Location = new Point(532, 187);
    this.bEditCount.Name = "bEditCount";
    this.bEditCount.Size = new Size(24, 23);
    this.bEditCount.TabIndex = 79;
    this.bEditCount.TabStop = false;
    this.bEditCount.Text = "...";
    this.bEditCount.UseVisualStyleBackColor = true;
    this.bEditCount.Click += new EventHandler(this.bEditCount_Click);
    this.label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label8.AutoSize = true;
    this.label8.Location = new Point(359, 219);
    this.label8.Name = "label8";
    this.label8.Size = new Size(52, 13);
    this.label8.TabIndex = 78;
    this.label8.Text = "Формат:";
    this.label7.AutoSize = true;
    this.label7.Location = new Point(25, 219);
    this.label7.Name = "label7";
    this.label7.Size = new Size(35, 13);
    this.label7.TabIndex = 76;
    this.label7.Text = "Зона:";
    this.tbZone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbZone.BackColor = Color.White;
    this.tbZone.Location = new Point(145, 215);
    this.tbZone.Name = "tbZone";
    this.tbZone.Size = new Size(185, 20);
    this.tbZone.TabIndex = 7;
    this.tbZone.TextChanged += new EventHandler(this.tbTextChanged);
    this.label6.AutoSize = true;
    this.label6.Location = new Point(24, 246);
    this.label6.Name = "label6";
    this.label6.Size = new Size(101, 13);
    this.label6.TabIndex = 74;
    this.label6.Text = "Поз. обозначение:";
    this.tbPosDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbPosDesignation.BackColor = Color.White;
    this.tbPosDesignation.Location = new Point(145, 242);
    this.tbPosDesignation.Name = "tbPosDesignation";
    this.tbPosDesignation.Size = new Size(411, 20);
    this.tbPosDesignation.TabIndex = 9;
    this.tbPosDesignation.TextChanged += new EventHandler(this.tbTextChanged);
    this.label4.AutoSize = true;
    this.label4.Location = new Point(25, 192 /*0xC0*/);
    this.label4.Name = "label4";
    this.label4.Size = new Size(54, 13);
    this.label4.TabIndex = 72;
    this.label4.Text = "Позиция:";
    this.tbPosition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbPosition.BackColor = Color.White;
    this.tbPosition.Location = new Point(145, 188);
    this.tbPosition.Name = "tbPosition";
    this.tbPosition.Size = new Size(185, 20);
    this.tbPosition.TabIndex = 5;
    this.tbPosition.TextChanged += new EventHandler(this.tbTextChanged);
    this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(359, 192 /*0xC0*/);
    this.label3.Name = "label3";
    this.label3.Size = new Size(69, 13);
    this.label3.TabIndex = 70;
    this.label3.Text = "Количество:";
    this.tbCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.tbCount.BackColor = Color.White;
    this.tbCount.Location = new Point(442, 188);
    this.tbCount.Name = "tbCount";
    this.tbCount.Size = new Size(90, 20);
    this.tbCount.TabIndex = 6;
    this.tbCount.Leave += new EventHandler(this.tbCount_Leave);
    this.label9.AutoSize = true;
    this.label9.Location = new Point(25, 128 /*0x80*/);
    this.label9.Name = "label9";
    this.label9.Size = new Size(55, 13);
    this.label9.TabIndex = 90;
    this.label9.Text = "Код ОКП:";
    this.tbOKPCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbOKPCode.BackColor = Color.White;
    this.tbOKPCode.Location = new Point(145, 124);
    this.tbOKPCode.Name = "tbOKPCode";
    this.tbOKPCode.Size = new Size(185, 20);
    this.tbOKPCode.TabIndex = 2;
    this.tbOKPCode.Leave += new EventHandler(this.tbOKPCode_Leave);
    this.label10.AutoSize = true;
    this.label10.Location = new Point(25, 166);
    this.label10.Name = "label10";
    this.label10.Size = new Size(48 /*0x30*/, 13);
    this.label10.TabIndex = 92;
    this.label10.Text = "Смотри:";
    this.tbSmotri.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbSmotri.BackColor = Color.White;
    this.tbSmotri.Location = new Point(145, 162);
    this.tbSmotri.Name = "tbSmotri";
    this.tbSmotri.Size = new Size(185, 20);
    this.tbSmotri.TabIndex = 4;
    this.tbSmotri.TextChanged += new EventHandler(this.tbTextChanged);
    this.tbSmotri.Leave += new EventHandler(this.TbSearchLeave);
    this.cbFormat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.cbFormat.BackColor = Color.White;
    this.cbFormat.FormattingEnabled = true;
    this.cbFormat.Location = new Point(442, 215);
    this.cbFormat.Name = "cbFormat";
    this.cbFormat.Size = new Size(114, 21);
    this.cbFormat.TabIndex = 8;
    this.cbFormat.TextChanged += new EventHandler(this.cbFormat_TextChanged);
    this.cbFormat.Leave += new EventHandler(this.cbFormat_Leave);
    this.cbPodbor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.cbPodbor.AutoSize = true;
    this.cbPodbor.Location = new Point(362, 128 /*0x80*/);
    this.cbPodbor.Name = "cbPodbor";
    this.cbPodbor.Size = new Size(64 /*0x40*/, 17);
    this.cbPodbor.TabIndex = 3;
    this.cbPodbor.Text = "Подбор";
    this.toolTip1.SetToolTip((Control) this.cbPodbor, "Изделие с электромонтажом входит в подбор");
    this.cbPodbor.UseVisualStyleBackColor = true;
    this.cbPodbor.CheckedChanged += new EventHandler(this.cbPodbor_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(96f, 96f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.Controls.Add((Control) this.cbPodbor);
    this.Controls.Add((Control) this.cbFormat);
    this.Controls.Add((Control) this.label10);
    this.Controls.Add((Control) this.tbSmotri);
    this.Controls.Add((Control) this.label9);
    this.Controls.Add((Control) this.tbOKPCode);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.bEditName);
    this.Controls.Add((Control) this.bEditDesignation);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.tbDesignation);
    this.Controls.Add((Control) this.bEditCount);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this.label7);
    this.Controls.Add((Control) this.tbZone);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this.tbPosDesignation);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.tbPosition);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.tbCount);
    this.MinimumSize = new Size(580, 350);
    this.Name = nameof (AutoMasterControl);
    this.Padding = new Padding(3);
    this.Size = new Size(580, 370);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
