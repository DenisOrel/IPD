// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.NonDraftMasterControl
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
/// Главная закладка для записи единичной спецификации и группы А
/// </summary>
internal class NonDraftMasterControl : PageUserControl
{
  /// <summary>Текущее значение атрибута "Количество"</summary>
  private MeasuredValue _quantity;
  /// <summary>Структура с информацией по объектам для классификации</summary>
  private ClassificatedObjects _classifObjects;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lFullName;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbOKPCode;
  private Label label11;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbSmotri;
  private Label label10;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbCount;
  private Label label3;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosition;
  private Label label4;
  private Label label5;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbNote;
  private Label label6;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosDesignation;
  private Label label7;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbZone;
  private Button bEditCount;
  private Button bEditName;
  private Button bEditDesignation;
  private Label label2;
  private Label label1;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbName;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbDesignation;
  private Panel panel2;
  private Panel panel1;
  private Button bClassificate;
  private Label label8;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbSize;
  private Label label9;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbMaterial;
  private Button bEditMaterial;
  private CheckBox cbPodbor;
  private ToolTip toolTip1;
  private Button bDeleteMaterial;

  public NonDraftMasterControl(
    IDBRelation relation,
    ClassificatedObjects classifObjects,
    List<AVSRow> selectedSpecRows,
    CommonDataType disableControls,
    IDBObject article)
  {
    this.InitializeComponent();
    this.Init(relation, classifObjects, selectedSpecRows, disableControls, article);
  }

  internal void Init(
    IDBRelation relation,
    ClassificatedObjects classifObjects,
    List<AVSRow> selectedSpecRows,
    CommonDataType disableControls,
    IDBObject article)
  {
    this.Init(relation != null ? relation.RelationID : -1L, AttributableElements.Relation, disableControls);
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
    if (this.selectedSpecRow != null && this.selectedSpecRow.avsDocument != null)
      this.selectedSpecRow.avsDocument.SuspendDocumentAndGridUpdates(true, false);
    try
    {
      if (this.selectedSpecRow != null)
      {
        this.SaveCount(-1, (object) this._quantity);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Position, -1, -1, (object) this.tbPosition.Text, true, false, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Zone, -1, -1, (object) this.tbZone.Text, true, true, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Note, -1, -1, (object) this.tbNote.Text, true, false, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_PosDesignation, -1, -1, (object) this.tbPosDesignation.Text, true, false, true, true, false, false);
        this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Attr_Podbor, -1, -1, (object) this.cbPodbor.Checked, true, false, true, true, false, false);
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
      this.tbMaterial.Enabled = false;
      this.bEditMaterial.Enabled = false;
      this.bDeleteMaterial.Enabled = false;
      this.tbSize.Enabled = false;
      this.bClassificate.Enabled = false;
      this.tbOKPCode.Enabled = false;
      this.tbSmotri.Enabled = false;
      this.cbPodbor.Enabled = false;
    }
    if ((this.disableControls & CommonDataType.OKPCode) == CommonDataType.OKPCode)
      this.tbOKPCode.Enabled = false;
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
    if ((this.disableControls & CommonDataType.Material) == CommonDataType.Material)
    {
      this.tbMaterial.Enabled = false;
      this.bEditMaterial.Enabled = false;
      this.bDeleteMaterial.Enabled = false;
    }
    if ((this.disableControls & CommonDataType.Size) == CommonDataType.Size)
      this.tbSize.Enabled = false;
    if ((this.disableControls & CommonDataType.Podbor) != CommonDataType.None)
      this.cbPodbor.Enabled = false;
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
    this.bClassificate.Enabled = this._classifObjects.EnableClassif;
    this.ReloadCommonData(CommonDataType.All);
    if (this.FormType == FormType.NonDraft)
    {
      this.label11.Visible = false;
      this.label10.Visible = false;
      this.tbOKPCode.Visible = false;
      this.tbSmotri.Visible = false;
      int height = this.tbName.Location.Y - this.tbPosition.Location.Y + 25;
      Label label3 = this.label3;
      label3.Location = label3.Location + new Size(0, height);
      Label label4 = this.label4;
      label4.Location = label4.Location + new Size(0, height);
      Label label7 = this.label7;
      label7.Location = label7.Location + new Size(0, height);
      Label label8 = this.label8;
      label8.Location = label8.Location + new Size(0, height);
      Label label6 = this.label6;
      label6.Location = label6.Location + new Size(0, height);
      Label label9 = this.label9;
      label9.Location = label9.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosition = this.tbPosition;
      tbPosition.Location = tbPosition.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbZone = this.tbZone;
      tbZone.Location = tbZone.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbCount = this.tbCount;
      tbCount.Location = tbCount.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosDesignation = this.tbPosDesignation;
      tbPosDesignation.Location = tbPosDesignation.Location + new Size(0, height);
      Button bEditCount = this.bEditCount;
      bEditCount.Location = bEditCount.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbMaterial = this.tbMaterial;
      tbMaterial.Location = tbMaterial.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbSize = this.tbSize;
      tbSize.Location = tbSize.Location + new Size(0, height);
      Button bEditMaterial = this.bEditMaterial;
      bEditMaterial.Location = bEditMaterial.Location + new Size(0, height);
      Button bDeleteMaterial = this.bDeleteMaterial;
      bDeleteMaterial.Location = bDeleteMaterial.Location + new Size(0, height);
      CheckBox cbPodbor = this.cbPodbor;
      cbPodbor.Location = cbPodbor.Location + new Size(0, height);
      this.Height += height;
    }
    if (this.aProcessor != null)
    {
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
        this.tbCount.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Count, relationIndex, -1, (List<RelationAttributeValuesCache>) null, true);
        this.tbCount.Enabled = !this.selectedSpecRow.GetReadOnlyCount(-1);
        this.tbPosition.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Position, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
        this.tbZone.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Zone, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
        this.tbNote.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Note, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
        this.cbPodbor.Checked = this.selectedSpecRow.GetFieldBoolValue(this.selectedSpecRow.Attr_Podbor, relationIndex, -1, (List<RelationAttributeValuesCache>) null, true);
      }
      else
      {
        if (this.aProcessor.FindAttributeValues(AvsIDCache.Attr_Podbor) != null)
          this.cbPodbor.Checked = Convert.ToBoolean(this.aProcessor.GetValue(AvsIDCache.Attr_Podbor));
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
    }
    this.tbName.ReadOnly |= this.IsReadOnly(FormHelper.AttributeNameID);
    this.tbDesignation.ReadOnly |= this.IsReadOnly(FormHelper.AttributeDesignationID);
    this.UpdateDeleteMaterialButton();
  }

  private void UpdateDeleteMaterialButton()
  {
    this.bDeleteMaterial.Enabled = this.bEditMaterial.Enabled && !string.IsNullOrWhiteSpace(this.tbMaterial.Text);
    this.bDeleteMaterial.Image = this.bDeleteMaterial.Enabled ? (Image) Resources.Del : (Image) Resources.DelDisabled;
  }

  /// <summary>Пришло сообщение об изменениии общих атрибутов</summary>
  protected override void OnCommonDataChanged(CommonDataType type) => this.ReloadCommonData(type);

  /// <summary>Перечитать общие атрибуты</summary>
  /// <param name="type"></param>
  private void ReloadCommonData(CommonDataType type)
  {
    switch (type)
    {
      case CommonDataType.All:
        this.tbDesignation.Text = this.commonData.Designation;
        this.tbName.Text = this.commonData.Name;
        this.lFullName.Text = "Полное наименование    " + this.commonData.FullName;
        this.tbMaterial.Text = this.commonData.Material.Caption;
        this.tbSize.Text = this.commonData.Size;
        this.tbOKPCode.Text = this.commonData.OKPCode;
        this.tbPosDesignation.Text = this.commonData.PosDesignation;
        this.tbSmotri.Text = this.commonData.Smotri;
        this.cbPodbor.Checked = this.commonData.Podbor;
        this.OnChanged();
        break;
      case CommonDataType.Designation:
        this.tbDesignation.Text = this.commonData.Designation;
        this.OnChanged();
        break;
      case CommonDataType.Name:
        this.tbName.Text = this.commonData.Name;
        this.lFullName.Text = "Полное наименование    " + this.commonData.FullName;
        this.OnChanged();
        break;
      case CommonDataType.OKPCode:
        this.tbOKPCode.Text = this.commonData.OKPCode;
        this.OnChanged();
        break;
      case CommonDataType.Material:
        this.tbMaterial.Text = this.commonData.Material.Caption;
        break;
      case CommonDataType.Size:
        this.tbSize.Text = this.commonData.Size;
        break;
      case CommonDataType.Podbor:
        this.cbPodbor.Checked = this.commonData.Podbor;
        this.OnChanged();
        break;
    }
  }

  /// <summary>Обработка события применения классификации</summary>
  /// <param name="values"></param>
  public override void OnSetClassifyAttributes(IObjectClassificator oc, long clasifID)
  {
    AttributeValues[] clasificatorAttributes = oc.GetClasificatorAttributes(this._classifObjects.articleID);
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
        else if (attributeValues.AttributeID == FormHelper.AttributeMaterialID)
          this.commonData.Material = this.GetMaterial(attributeValues.Values[0]);
        else if (attributeValues.AttributeID == FormHelper.AttributeSizeID)
          this.commonData.Size = Convert.ToString(attributeValues.Values[0]);
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

  /// <summary>Нажали кновку вызова редактора атрибута "Материал"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditMaterial_Click(object sender, EventArgs e)
  {
    object obj = this.ChangeInEditor(FormHelper.AttributeMaterialID, (object) this.commonData.Material.ObjectID);
    try
    {
      if (CompareValuesHelper.NormalizedValue(obj) == null)
      {
        this.commonData.Material = new MaterialInfo(0L, string.Empty);
      }
      else
      {
        long int64 = Convert.ToInt64(obj);
        if (this.commonData.Material.ObjectID != int64)
        {
          MaterialInfo material = this.GetMaterial(int64);
          this.tbMaterial.Text = material.Caption;
          this.commonData.Material = material;
        }
      }
      this.UpdateDeleteMaterialButton();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  /// <summary>Нажали кновку вызова редактора атрибута "Материал"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bDeleteMaterial_Click(object sender, EventArgs e)
  {
    try
    {
      this.commonData.Material = new MaterialInfo(0L, string.Empty);
      this.tbMaterial.Text = string.Empty;
      this.UpdateDeleteMaterialButton();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Невозможно удалить информацию о материале", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  /// <summary>Вышли из поля для редактирования атрибута "Размеры"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbSize_Leave(object sender, EventArgs e)
  {
    if (!(this.commonData.Size != this.tbSize.Text))
      return;
    this.commonData.Size = this.tbSize.Text;
    this.OnChanged();
  }

  private void OnTextChanged(object sender, EventArgs e) => this.OnChanged();

  private void TbOKPCodeLeave(object sender, EventArgs e)
  {
    if (!(this.commonData.OKPCode != this.tbOKPCode.Text))
      return;
    this.commonData.OKPCode = this.tbOKPCode.Text;
    this.OnChanged();
  }

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
    this.tbCount = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label3 = new Label();
    this.tbPosition = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label4 = new Label();
    this.label5 = new Label();
    this.tbNote = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label6 = new Label();
    this.tbPosDesignation = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label7 = new Label();
    this.tbZone = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.bEditCount = new Button();
    this.bEditName = new Button();
    this.bEditDesignation = new Button();
    this.label2 = new Label();
    this.label1 = new Label();
    this.tbName = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.tbDesignation = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.panel2 = new Panel();
    this.panel1 = new Panel();
    this.bClassificate = new Button();
    this.label8 = new Label();
    this.tbSize = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label9 = new Label();
    this.tbMaterial = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.bEditMaterial = new Button();
    this.label10 = new Label();
    this.tbSmotri = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label11 = new Label();
    this.tbOKPCode = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.lFullName = new Label();
    this.cbPodbor = new CheckBox();
    this.toolTip1 = new ToolTip(this.components);
    this.bDeleteMaterial = new Button();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.tbCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.tbCount.BackColor = Color.White;
    this.tbCount.Location = new Point(443, 196);
    this.tbCount.Name = "tbCount";
    this.tbCount.Size = new Size(90, 20);
    this.tbCount.TabIndex = 5;
    this.tbCount.Leave += new EventHandler(this.tbCount_Leave);
    this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(360, 200);
    this.label3.Name = "label3";
    this.label3.Size = new Size(69, 13);
    this.label3.TabIndex = 5;
    this.label3.Text = "Количество:";
    this.tbPosition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbPosition.BackColor = Color.White;
    this.tbPosition.Location = new Point(131, 196);
    this.tbPosition.Name = "tbPosition";
    this.tbPosition.Size = new Size(200, 20);
    this.tbPosition.TabIndex = 4;
    this.tbPosition.TextChanged += new EventHandler(this.OnTextChanged);
    this.label4.AutoSize = true;
    this.label4.Location = new Point(26, 200);
    this.label4.Name = "label4";
    this.label4.Size = new Size(54, 13);
    this.label4.TabIndex = 7;
    this.label4.Text = "Позиция:";
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
    this.tbNote.TabIndex = 74;
    this.tbNote.TextChanged += new EventHandler(this.OnTextChanged);
    this.label6.AutoSize = true;
    this.label6.Location = new Point(25, 307);
    this.label6.Name = "label6";
    this.label6.Size = new Size(101, 13);
    this.label6.TabIndex = 11;
    this.label6.Text = "Поз. обозначение:";
    this.tbPosDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbPosDesignation.BackColor = Color.White;
    this.tbPosDesignation.Location = new Point(131, 303);
    this.tbPosDesignation.Name = "tbPosDesignation";
    this.tbPosDesignation.Size = new Size(426, 20);
    this.tbPosDesignation.TabIndex = 12;
    this.tbPosDesignation.TextChanged += new EventHandler(this.OnTextChanged);
    this.label7.AutoSize = true;
    this.label7.Location = new Point(26, 227);
    this.label7.Name = "label7";
    this.label7.Size = new Size(35, 13);
    this.label7.TabIndex = 13;
    this.label7.Text = "Зона:";
    this.tbZone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbZone.BackColor = Color.White;
    this.tbZone.Location = new Point(131, 223);
    this.tbZone.Name = "tbZone";
    this.tbZone.Size = new Size(200, 20);
    this.tbZone.TabIndex = 7;
    this.tbZone.TextChanged += new EventHandler(this.OnTextChanged);
    this.bEditCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditCount.Location = new Point(533, 195);
    this.bEditCount.Name = "bEditCount";
    this.bEditCount.Size = new Size(24, 23);
    this.bEditCount.TabIndex = 6;
    this.bEditCount.Text = "...";
    this.bEditCount.UseVisualStyleBackColor = true;
    this.bEditCount.Click += new EventHandler(this.bEditCount_Click);
    this.bEditName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditName.Location = new Point(533, 112 /*0x70*/);
    this.bEditName.Name = "bEditName";
    this.bEditName.Size = new Size(24, 23);
    this.bEditName.TabIndex = 66;
    this.bEditName.Text = "...";
    this.bEditName.UseVisualStyleBackColor = true;
    this.bEditName.Click += new EventHandler(this.bEditName_Click);
    this.bEditDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditDesignation.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bEditDesignation.Location = new Point(533, 85);
    this.bEditDesignation.Name = "bEditDesignation";
    this.bEditDesignation.Size = new Size(24, 23);
    this.bEditDesignation.TabIndex = 65;
    this.bEditDesignation.Text = "...";
    this.bEditDesignation.UseVisualStyleBackColor = true;
    this.bEditDesignation.Click += new EventHandler(this.bEditDesignation_Click);
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label2.Location = new Point(26, 117);
    this.label2.Name = "label2";
    this.label2.Size = new Size(99, 13);
    this.label2.TabIndex = 64 /*0x40*/;
    this.label2.Text = "Наименование:";
    this.label1.AutoSize = true;
    this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(26, 91);
    this.label1.Name = "label1";
    this.label1.Size = new Size(89, 13);
    this.label1.TabIndex = 63 /*0x3F*/;
    this.label1.Text = "Обозначение:";
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.BackColor = Color.White;
    this.tbName.Location = new Point(131, 113);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(402, 20);
    this.tbName.TabIndex = 1;
    this.tbName.Leave += new EventHandler(this.tbName_Leave);
    this.tbDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbDesignation.BackColor = Color.White;
    this.tbDesignation.Location = new Point(131, 87);
    this.tbDesignation.Name = "tbDesignation";
    this.tbDesignation.Size = new Size(402, 20);
    this.tbDesignation.TabIndex = 0;
    this.tbDesignation.Leave += new EventHandler(this.tbDesignation_Leave);
    this.panel2.BackColor = SystemColors.ControlLight;
    this.panel2.Controls.Add((Control) this.label5);
    this.panel2.Controls.Add((Control) this.tbNote);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(3, 336);
    this.panel2.Margin = new Padding(0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(574, 80 /*0x50*/);
    this.panel2.TabIndex = 13;
    this.panel1.BackColor = SystemColors.ControlLight;
    this.panel1.Controls.Add((Control) this.bClassificate);
    this.panel1.Dock = DockStyle.Top;
    this.panel1.Location = new Point(3, 3);
    this.panel1.Name = "panel1";
    this.panel1.Padding = new Padding(3);
    this.panel1.Size = new Size(574, 47);
    this.panel1.TabIndex = 14;
    this.bClassificate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bClassificate.Image = (Image) Resources.classify;
    this.bClassificate.Location = new Point(533, 11);
    this.bClassificate.Name = "bClassificate";
    this.bClassificate.Size = new Size(26, 26);
    this.bClassificate.TabIndex = 75;
    this.bClassificate.UseVisualStyleBackColor = true;
    this.bClassificate.Click += new EventHandler(this.bClassificate_Click);
    this.label8.AutoSize = true;
    this.label8.Location = new Point(26, 280);
    this.label8.Name = "label8";
    this.label8.Size = new Size(57, 13);
    this.label8.TabIndex = 72;
    this.label8.Text = "Размеры:";
    this.tbSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbSize.BackColor = Color.White;
    this.tbSize.Location = new Point(131, 276);
    this.tbSize.Name = "tbSize";
    this.tbSize.Size = new Size(200, 20);
    this.tbSize.TabIndex = 11;
    this.tbSize.TextChanged += new EventHandler(this.OnTextChanged);
    this.tbSize.Leave += new EventHandler(this.tbSize_Leave);
    this.label9.AutoSize = true;
    this.label9.Location = new Point(26, 253);
    this.label9.Name = "label9";
    this.label9.Size = new Size(60, 13);
    this.label9.TabIndex = 70;
    this.label9.Text = "Материал:";
    this.tbMaterial.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbMaterial.BackColor = Color.White;
    this.tbMaterial.Location = new Point(131, 249);
    this.tbMaterial.Name = "tbMaterial";
    this.tbMaterial.ReadOnly = true;
    this.tbMaterial.Size = new Size(200, 20);
    this.tbMaterial.TabIndex = 9;
    this.bEditMaterial.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditMaterial.Location = new Point(331, 248);
    this.bEditMaterial.Name = "bEditMaterial";
    this.bEditMaterial.Size = new Size(24, 23);
    this.bEditMaterial.TabIndex = 10;
    this.bEditMaterial.Text = "...";
    this.bEditMaterial.UseVisualStyleBackColor = true;
    this.bEditMaterial.Click += new EventHandler(this.bEditMaterial_Click);
    this.label10.AutoSize = true;
    this.label10.Location = new Point(26, 170);
    this.label10.Name = "label10";
    this.label10.Size = new Size(48 /*0x30*/, 13);
    this.label10.TabIndex = 101;
    this.label10.Text = "Смотри:";
    this.tbSmotri.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbSmotri.BackColor = Color.White;
    this.tbSmotri.Location = new Point(131, 167);
    this.tbSmotri.Name = "tbSmotri";
    this.tbSmotri.Size = new Size(426, 20);
    this.tbSmotri.TabIndex = 3;
    this.tbSmotri.TextChanged += new EventHandler(this.OnTextChanged);
    this.tbSmotri.Leave += new EventHandler(this.TbSearchLeave);
    this.label11.AutoSize = true;
    this.label11.Location = new Point(26, 144 /*0x90*/);
    this.label11.Name = "label11";
    this.label11.Size = new Size(55, 13);
    this.label11.TabIndex = 99;
    this.label11.Text = "Код ОКП:";
    this.tbOKPCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbOKPCode.BackColor = Color.White;
    this.tbOKPCode.Location = new Point(131, 141);
    this.tbOKPCode.Name = "tbOKPCode";
    this.tbOKPCode.Size = new Size(426, 20);
    this.tbOKPCode.TabIndex = 2;
    this.tbOKPCode.TextChanged += new EventHandler(this.OnTextChanged);
    this.tbOKPCode.Leave += new EventHandler(this.TbOKPCodeLeave);
    this.lFullName.AutoSize = true;
    this.lFullName.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lFullName.Location = new Point(29, 65);
    this.lFullName.Name = "lFullName";
    this.lFullName.Size = new Size(41, 13);
    this.lFullName.TabIndex = 102;
    this.lFullName.Text = "label7";
    this.cbPodbor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.cbPodbor.AutoSize = true;
    this.cbPodbor.Location = new Point(363, 226);
    this.cbPodbor.Name = "cbPodbor";
    this.cbPodbor.Size = new Size(64 /*0x40*/, 17);
    this.cbPodbor.TabIndex = 8;
    this.cbPodbor.Text = "Подбор";
    this.toolTip1.SetToolTip((Control) this.cbPodbor, "Изделие с электромонтажом входит в подбор");
    this.cbPodbor.UseVisualStyleBackColor = true;
    this.cbPodbor.CheckedChanged += new EventHandler(this.cbPodbor_CheckedChanged);
    this.bDeleteMaterial.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bDeleteMaterial.Image = (Image) Resources.Del;
    this.bDeleteMaterial.Location = new Point(355, 248);
    this.bDeleteMaterial.Name = "bDeleteMaterial";
    this.bDeleteMaterial.Size = new Size(24, 23);
    this.bDeleteMaterial.TabIndex = 103;
    this.bDeleteMaterial.UseVisualStyleBackColor = true;
    this.bDeleteMaterial.Click += new EventHandler(this.bDeleteMaterial_Click);
    this.AutoScaleDimensions = new SizeF(96f, 96f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.Controls.Add((Control) this.bDeleteMaterial);
    this.Controls.Add((Control) this.cbPodbor);
    this.Controls.Add((Control) this.lFullName);
    this.Controls.Add((Control) this.label10);
    this.Controls.Add((Control) this.tbSmotri);
    this.Controls.Add((Control) this.label11);
    this.Controls.Add((Control) this.tbOKPCode);
    this.Controls.Add((Control) this.bEditMaterial);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this.tbSize);
    this.Controls.Add((Control) this.label9);
    this.Controls.Add((Control) this.tbMaterial);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.bEditName);
    this.Controls.Add((Control) this.bEditDesignation);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.tbDesignation);
    this.Controls.Add((Control) this.bEditCount);
    this.Controls.Add((Control) this.label7);
    this.Controls.Add((Control) this.tbZone);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this.tbPosDesignation);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.tbPosition);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.tbCount);
    this.MinimumSize = new Size(580, 350);
    this.Name = nameof (NonDraftMasterControl);
    this.Padding = new Padding(3);
    this.Size = new Size(580, 419);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
